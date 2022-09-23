using static lab2.Constants;

namespace lab2;

public class Hall
{
    private const string Path = "husbands.txt";

    private Contender[] _contenders = new Contender[ContendersAmount];
    private readonly Random _random = new();
    public Hall()
    {
        var index = 0;
        foreach (var name in File.ReadLines(Path))
        {
            _contenders[index] = new Contender { Name = name, Rank = ++index };
            if (index == ContendersAmount) break;
        }
    }

    public (int, Contender?) GetNext()
    {
        if (_contenders.Length == 0) return (-1, null);
            
        var i = _random.Next(_contenders.Length);
        var contender = _contenders[i];
        _contenders = _contenders.Where((_, index) => index != i).ToArray();
        Logger.Log(contender.Name);

        return (ContendersAmount - _contenders.Length ,contender);
    }
}