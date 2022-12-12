using Microsoft.EntityFrameworkCore;

namespace PrincessAndContenders.Data.Repositories;

public class ContenderRepository
{
    private readonly DbContext _context;

    public ContenderRepository(DbContext context) =>
        _context = context;

    public Contender? GetContenderByName(string name)
    {
        _context.Database.EnsureCreated();
        return _context
            .Set<Contender>()
            .FirstOrDefault(contender => contender.Name == name);
    }
}