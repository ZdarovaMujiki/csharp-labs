namespace lab2;

public static class ContendersGenerator
{
    private const string Path = "husbands.txt";

    public static Contender[] GenerateContenders(int amount)
    {
        var contenders = new Contender[amount];
        var index = 0;
        foreach (var name in File.ReadLines(Path))
        {
            contenders[index] = new Contender { Name = name, Rank = ++index };
            if (index == amount) break;
        }

        return contenders;
    }
}