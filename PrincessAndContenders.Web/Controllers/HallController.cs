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

    public HallController(SessionRepository sessionRepository, AttemptRepository attemptRepository)
    {
        _sessionRepository = sessionRepository;
        _attemptRepository = attemptRepository;
    }

    [HttpPost("reset")]
    public void Reset([FromQuery(Name="session")] int sessionId) =>
        _sessionRepository.RemoveSession(sessionId);

    [HttpPost("{attemptId:int}/next")]
    public string? GetNextContender(int attemptId, [FromQuery(Name="session")] int sessionId)
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
            return null;

        var contender = session.Attempt.Contenders[session.NextContenderId];
        session.NextContenderId++;
        
        _sessionRepository.UpdateSession(session);
        
        return contender.Name;
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