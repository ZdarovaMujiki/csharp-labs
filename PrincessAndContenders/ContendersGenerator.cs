namespace PrincessAndContenders;

public static class ContendersGenerator
{
    public static Queue<Contender> GenerateContenders(int amount)
    {
        var path = Environment.GetEnvironmentVariable("CONTENDERS_PATH");
        var contenders = new Contender[amount];
        var index = 0;
        foreach (var name in File.ReadLines(path ?? throw new FileNotFoundException()))
        {
            contenders[index] = new Contender { Name = name, Rank = ++index };
            if (index == amount) break;
        }

        return new Queue<Contender>(contenders);
    }
}