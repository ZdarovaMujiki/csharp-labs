using Microsoft.EntityFrameworkCore;

namespace PrincessAndContenders.Data.Repositories;

public class AttemptRepository
{
    private readonly DbContext _context;

    public AttemptRepository(DbContext context) =>
        _context = context;

    public Attempt? GetAttempt(int attemptId)
    {
        _context.Database.EnsureCreated();
        return _context
            .Set<Attempt>()
            .Include("Contenders")
            .FirstOrDefault(attempt => attempt.Id == attemptId);
    }
}