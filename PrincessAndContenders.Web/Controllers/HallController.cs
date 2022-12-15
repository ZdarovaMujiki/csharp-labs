using Consumer;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using PrincessAndContenders.Data;
using PrincessAndContenders.Data.Repositories;

namespace PrincessAndContenders.Web.Controllers;

[ApiController]
[Route("hall")]
public class HallController : ControllerBase
{
    private readonly SessionRepository _sessionRepository;
    private readonly AttemptRepository _attemptRepository;
    private readonly IPublishEndpoint _publishEndpoint;

    public HallController(SessionRepository sessionRepository, AttemptRepository attemptRepository, IPublishEndpoint publishEndpoint)
    {
        _sessionRepository = sessionRepository;
        _attemptRepository = attemptRepository;
        _publishEndpoint = publishEndpoint;
    }

    [HttpPost("reset")]
    public void Reset([FromQuery(Name="session")] int sessionId) =>
        _sessionRepository.RemoveSession(sessionId);

    [HttpPost("{attemptId:int}/next")]
    public async Task<IActionResult> GetNextContender(int attemptId, [FromQuery(Name="session")] int sessionId)
    {
        var session = _sessionRepository.GetSession(sessionId, attemptId);

        if (session == null)
        {
            var attempt = _attemptRepository.GetAttempt(attemptId);
        
            if (attempt == null)
                throw new HttpRequestException("Attempt not found");
        
            session = new Session
            {
                SessionId = sessionId,
                Attempt = attempt,
                NextContenderId = 0
            };
        }
        else if (session.NextContenderId == session.Attempt.Contenders.Count)
        {
            await _publishEndpoint.Publish<NextContender>(new
            {
                session.NextContenderId,
            });
            return Ok();
        }


        var contender = session.Attempt.Contenders[session.NextContenderId];
        
        await _publishEndpoint.Publish<NextContender>(new
        {
            Id = session.NextContenderId,
            contender.Name,
        });
        
        session.NextContenderId++;
        
        _sessionRepository.UpdateSession(session);
        return Ok();
    }
    
    [HttpPost("{attemptId:int}/select")]
    public int GetRank(int attemptId, [FromQuery(Name="session")] int sessionId)
    {
        var session = _sessionRepository.GetSession(sessionId, attemptId);
        
        if (session == null)
            throw new HttpRequestException("Session not found");

        return session.Attempt.Contenders[session.NextContenderId].Rank;
    }
}