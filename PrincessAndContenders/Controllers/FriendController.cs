using Microsoft.AspNetCore.Mvc;
using PrincessAndContenders.Data.Repositories;

namespace PrincessAndContenders.Controllers;

[ApiController]
[Route("friend")]
public class FriendController : ControllerBase
{
    private readonly ContenderRepository _contenderRepository;

    public FriendController(ContenderRepository contenderRepository) =>
        _contenderRepository = contenderRepository;

    [HttpPost("{attemptId:int}/compare")]
    public string CompareContenders(int attemptId, [FromQuery(Name="session")] int sessionId, [FromBody] ContendersNames contendersNames)
    {
        var contender1 = _contenderRepository.GetContenderByName(contendersNames.name1);
        var contender2 = _contenderRepository.GetContenderByName(contendersNames.name2);
        
        return contender1.Rank > contender2.Rank ? contender1.Name : contender2.Name;
    }
}