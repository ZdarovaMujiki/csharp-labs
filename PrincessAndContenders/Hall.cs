using static PrincessAndContenders.Constants;

namespace PrincessAndContenders;

public class Hall
{
    public readonly Queue<Contender> Contenders = ContendersGenerator.GenerateContenders(ContendersAmount);

    public Contender GetNext()
    {
        if (!Contenders.TryDequeue(out var contender))
            throw new EmptyHallException();
        
        Logger.Log(contender.Name);

        return contender;
    }

    public bool Contains(Contender? contender) =>
        contender != null && Contenders.Contains(contender);
}