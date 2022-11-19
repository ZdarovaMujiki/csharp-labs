using static PrincessAndContenders.Constants;

namespace PrincessAndContenders;

public class Hall
{
    public Contender[] Contenders { get; set; } = ContendersGenerator.GenerateContenders(ContendersAmount);
    private readonly Random _random = new();

    public (int, Contender?) GetNext()
    {
        if (Contenders.Length == 0) throw new EmptyHallException();
            
        var i = _random.Next(Contenders.Length);
        var contender = Contenders[i];
        Contenders = Contenders.Where((_, index) => index != i).ToArray();
        Logger.Log(contender.Name);

        return (ContendersAmount - Contenders.Length, contender);
    }

    public bool Contains(Contender? contender) =>
        contender != null && Contenders.Contains(contender);
}