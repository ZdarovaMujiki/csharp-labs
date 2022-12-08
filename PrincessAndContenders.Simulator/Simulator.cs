using Microsoft.EntityFrameworkCore;
using PrincessAndContenders.Data;
using PrincessAndContenders.Utils;

namespace PrincessAndContenders.Simulator;

public class Simulator
{
    private readonly DbContext _context;

    public Simulator(DbContext context) =>
        _context = context;

    public void Generate(int amount)
    {
        _context.Database.EnsureCreated();
        var contendersGenerator = new ContendersGenerator(Constants.ContendersAmount);

        for (var i = 0; i < amount; ++i)
        {
            var contenders = contendersGenerator.GenerateContenders();
            _context.Set<Attempt>().Add(new Attempt { Contenders = contenders.ToList() });
        }

        _context.SaveChanges();
    }

    public Contender? Simulate(int i)
    {
        _context.Database.EnsureCreated();

        var attempt = _context.Set<Attempt>().Find(i);
        if (attempt == null)
            return null;
        
        var contenders = new Queue<Contender>(attempt.Contenders);
        var hall = new Hall(contenders);
        var friend = new Friend(hall);
        var princess = new Princess(hall, friend, null);
        return princess.GetMarried();
    }

    public void SimulateAll()
    {
        _context.Database.EnsureCreated();

        var attempts = _context.Set<Attempt>().ToArray();
        var attemptsLength = attempts.Length;

        if (attemptsLength == 0)
            return;

        var sumPoints = 0;
        foreach (var attempt in attempts)
        {
            var contenders = new Queue<Contender>(attempt.Contenders);
            var hall = new Hall(contenders);
            var friend = new Friend(hall);
            var princess = new Princess(hall, friend, null);

            var best = princess.GetBestContender();
            var points = Princess.GetPoints(best);
            Console.WriteLine(points);
            
            sumPoints += points;
        }

        Console.WriteLine("Average: " + (double)sumPoints / attemptsLength);
    }

    public void Clear()
    {
        _context.Set<Attempt>().RemoveRange(_context.Set<Attempt>());
        _context.Set<Contender>().RemoveRange(_context.Set<Contender>());
        _context.SaveChanges();
    }
}