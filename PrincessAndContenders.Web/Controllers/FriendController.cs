using Microsoft.AspNetCore.Mvc;
using PrincessAndContenders.Data.Repositories;
using PrincessAndContenders.Web.DTOs;

namespace PrincessAndContenders.Web.Controllers;

[ApiController]
[Route("friend")]
public class FriendController : ControllerBase
{
    private readonly SessionRepository _sessionRepository;

    public FriendController(SessionRepository sessionRepository) =>
        _sessionRepository = sessionRepository;

    [HttpPost("{attemptId:int}/compare")]
    public string? CompareContenders(int attemptId, [FromQuery(Name="session")] int sessionId, [FromBody] ContendersNames contendersNames)
    {
        var session = _sessionRepository.GetSession(sessionId, attemptId);
        if (session == null)
            throw new HttpRequestException("Session not found");
        
        var knownContenders = session.Attempt.Contenders.Take(session.NextContenderId).ToList();

        var contender1 = knownContenders.Find(contender => contender.Name == contendersNames.name1);
        var contender2 = knownContenders.Find(contender => contender.Name == contendersNames.name2);

        if (contender1 == null || contender2 == null)
            throw new HttpRequestException("Unknown contender");
        
        return contender1.Rank > contender2.Rank ? contender1.Name : contender2.Name;
    }
}