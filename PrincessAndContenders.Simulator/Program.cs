using NDesk.Options;
using PrincessAndContenders.Data;

namespace PrincessAndContenders.Simulator;

static class Program
{
    public static void Main(string[] args)
    {
        var simulator = new Simulator(new Context());
        var options = new OptionSet
        {
            { "g|generate", "Generate 100 attempts", _ => simulator.Generate(100) },
            { "c|clear", "Remove all attempts from database.", _ => simulator.Clear() },
            { "s|simulate=", "Run attempt by id", (int id) => simulator.Simulate(id) },
            { "a|average", "Run all attempts and get average points", _ => simulator.SimulateAll() },
        };
        if (args.Length == 0)
            options.WriteOptionDescriptions(Console.Out);
        
        options.Parse(args);
    }
}