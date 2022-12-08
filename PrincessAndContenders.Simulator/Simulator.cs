using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using PrincessAndContenders.Data;
using PrincessAndContenders.Interfaces;
using PrincessAndContenders.Utils;

namespace PrincessAndContenders.Simulator;

public class Simulator : IHostedService
{
    private readonly DbContext _context;
    private readonly IHall _hall;
    private readonly IPrincess _princess;

    private readonly IHostApplicationLifetime? _applicationLifetime;
    private Task? _applicationTask;

    public Simulator(DbContext context, IHall hall, IPrincess princess, IHostApplicationLifetime? applicationLifetime)
    {
        _context = context;
        _hall = hall;
        _princess = princess;
        _applicationLifetime = applicationLifetime;
    }

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

        _hall.Contenders = new Queue<Contender>(attempt.Contenders);
        return _princess.GetMarried();
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
            _hall.Contenders = new Queue<Contender>(attempt.Contenders);

            var points = Princess.GetPoints(_princess.GetBestContender());
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

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _applicationTask = Task.Run(() =>
        {
            Generate(100);
            SimulateAll();
            _applicationLifetime?.StopApplication();
        }, cancellationToken);
        
        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_applicationTask != null)
        {
            await _applicationTask;
        }
    }
}