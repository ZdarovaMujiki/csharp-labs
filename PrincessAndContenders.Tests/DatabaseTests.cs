using Microsoft.EntityFrameworkCore;
using PrincessAndContenders;
using PrincessAndContenders.Data;
using PrincessAndContenders.Simulator;

namespace Tests;

public class DatabaseTests
{
    private DbContextOptions<DbContext> _options;
    private DbContext _context;
    private Simulator _simulator;

    private const int Amount = 100;

    [SetUp]
    public void SetUp()
    {
        _options = new DbContextOptionsBuilder<DbContext>()
            .UseInMemoryDatabase("InMemoryDb")
            .UseModel(new Context().Model)
            .Options;
        
        _context =  new DbContext(_options);

        _simulator = new Simulator(_context);
        _simulator.Clear();
    }
    
    [Test]
    public void Generate_ShouldGenerateAttempts()
    {
        _simulator.Generate(Amount);
        Assert.That(_context.Set<Attempt>().Count(), Is.EqualTo(Amount));
    }
    
    [Test]
    public void Simulate_ShouldSimulate()
    {
        _simulator.Generate(Amount);
        var attempt = _context.Set<Attempt>().First();
        
        var contenders = new Queue<Contender>(attempt.Contenders);
        var hall = new Hall(contenders);
        var friend = new Friend(hall);
        var princess = new Princess(hall, friend, null);
        var contenderA = princess.GetMarried();
        
        var contenderB = _simulator.Simulate(attempt.Id);

        Assert.That(contenderA, Is.EqualTo(contenderB));
    }
}