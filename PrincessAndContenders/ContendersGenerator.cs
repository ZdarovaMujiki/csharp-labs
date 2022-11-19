using PrincessAndContenders.Utils;

namespace PrincessAndContenders;

public static class ContendersGenerator
{
    private const string Path = "husbands.txt";
    private static readonly Random Rng = new();

    public static Queue<Contender> GenerateContenders(int amount)
    {
        var contenders = new Contender[amount];
        var index = 0;
        foreach (var name in File.ReadLines(Path))
        {
            contenders[index] = new Contender { Name = name, Rank = ++index };
            if (index == amount) break;
        }
        
        Rng.Shuffle(contenders);

        return new Queue<Contender>(contenders);
    }
}