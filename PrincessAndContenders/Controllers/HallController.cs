using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrincessAndContenders.Data;

namespace PrincessAndContenders.Controllers;

[ApiController]
[Route("hall")]
public class HallController : ControllerBase
{
    private readonly DbContext _context;
    private int _order;

    public HallController(DbContext context) =>
        _context = context;

    [HttpGet("reset")]
    public string Reset()
    {
        return "reset";
    }
    
    [HttpGet("{attemptId:int}/next")]
    public string GetNextContender(int attemptId)
    {
        _context.Database.EnsureCreated();
        var attempt = _context.Set<Attempt>()
            .Include("Contenders")
            .FirstOrDefault(x => x.Id == attemptId);

        var contenders = attempt.Contenders;
        return contenders[_order].Name;
    }
    
    [HttpGet("{attemptId:int}/select")]
    public string GetRank(int attemptId)
    {
        return "rank of contender for attempt " + attemptId;
    }
}