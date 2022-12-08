using Microsoft.AspNetCore.Mvc;

namespace PrincessAndContenders.Controllers;

[ApiController]
[Route("friend")]
public class FriendController : ControllerBase
{
    [HttpPost("{attemptId:int}/compare")]
    public string CompareContenders(int attemptId, [FromBody] ContendersNames contendersNames)
    {
        return $"name1={contendersNames.name1}, name2={contendersNames.name2}";
    }
}