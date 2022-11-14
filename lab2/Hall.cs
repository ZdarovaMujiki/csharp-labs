using static lab2.Constants;

namespace lab2;

public class Hall
{
    private Contender[] _contenders = ContendersGenerator.GenerateContenders(ContendersAmount);
    private readonly Random _random = new();

    public (int, Contender?) GetNext()
    {
        if (_contenders.Length == 0) throw new EmptyHallException();
            
        var i = _random.Next(_contenders.Length);
        var contender = _contenders[i];
        _contenders = _contenders.Where((_, index) => index != i).ToArray();
        Logger.Log(contender.Name);

        return (ContendersAmount - _contenders.Length, contender);
    }
}