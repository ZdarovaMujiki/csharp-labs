using Microsoft.EntityFrameworkCore;
using NDesk.Options;
using PrincessAndContenders.Data;
using PrincessAndContenders.Utils;

namespace PrincessAndContenders.Simulator;

static class Program
{
    public static void Main(string[] args)
    {
        var options = new OptionSet
        {
            { "g|generate", "Generate 100 attempts", _ => Generate(100) },
            { "c|clear", "Remove all attempts from database.", _ => Clear() },
            { "s|simulate=", "Run attempt by id", (int id) => Simulate(id) },
            { "a|average", "Run all attempts and get average points", _ => SimulateAll() },
        };

        if (args.Length == 0)
            options.WriteOptionDescriptions(Console.Out);
        
        options.Parse(args);
    }

    private static void Generate(int amount)
    {
        using var context = new Context();
        context.Database.EnsureCreated();

        var random = new Random();
        for (var i = 0; i < amount; ++i)
        {
            var contenders = ContendersGenerator.GenerateContenders(100).ToArray();
            random.Shuffle(contenders);
            context.Attempts.Add(new Attempt { Contenders = contenders });
        }

        context.SaveChanges();
    }

    private static void Simulate(int i)
    {
        using var context = new Context();
        context.Database.EnsureCreated();

        var attempt = context.Attempts.Find(i);
        if (attempt == null)
            return;
        
        var contenders = new Queue<Contender>(attempt.Contenders);
        var hall = new Hall(contenders);
        var friend = new Friend(hall);
        var princess = new Princess(hall, friend, null);
        princess.GetMarried();
    }

    private static void SimulateAll()
    {
        using var context = new Context();
        context.Database.EnsureCreated();

        var sumPoints = 0;
        var attempts = context.Attempts.ToArray();
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

        Console.WriteLine("Average: " + (double)sumPoints / attempts.Length);
    }

    private static void Clear()
    {
        using var context = new Context();
        context.Attempts.ExecuteDelete();
        context.SaveChanges();
    }
}