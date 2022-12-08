using PrincessAndContenders.Data;
using PrincessAndContenders.Interfaces;
using PrincessAndContenders.Utils;

namespace PrincessAndContenders;

public class ContendersGenerator : IContendersGenerator
{
    private readonly int _amount;

    public ContendersGenerator(int amount) =>
        _amount = amount;

    public Queue<Contender> GenerateContenders()
    {
        var random = new Random();
        var path = Environment.GetEnvironmentVariable("CONTENDERS_PATH");
        var contenders = new Contender[_amount];
        var index = 0;
        foreach (var name in File.ReadLines(path ?? throw new FileNotFoundException()))
        {
            contenders[index] = new Contender { Name = name, Rank = ++index };
            if (index == _amount) break;
        }
        random.Shuffle(contenders);

        return new Queue<Contender>(contenders);
    }
}