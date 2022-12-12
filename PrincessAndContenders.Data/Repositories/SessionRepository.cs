using Microsoft.EntityFrameworkCore;

namespace PrincessAndContenders.Data.Repositories;

public class SessionRepository
{
    private readonly DbContext _context;

    public SessionRepository(DbContext context) =>
        _context = context;

    public Session? GetSession(int sessionId, int attemptId)
    {
        _context.Database.EnsureCreated();
        return _context
            .Set<Session>()
            .Include(x => x.Attempt.Contenders)
            .Where(session => session.SessionId == sessionId)
            .FirstOrDefault(session => session.Attempt.Id == attemptId);
    }

    public void UpdateSession(Session session)
    {
        _context.Database.EnsureCreated();
        _context.Set<Session>().Update(session);
        _context.SaveChanges();
    }

    public void RemoveSession(int sessionId)
    {
        _context.Database.EnsureCreated();
        _context.Set<Session>()
            .RemoveRange(_context.Set<Session>().Where(session => session.SessionId == sessionId));

        _context.SaveChanges();
    }
}