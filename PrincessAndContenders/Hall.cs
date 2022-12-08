using PrincessAndContenders.Data;
using PrincessAndContenders.Exceptions;
using PrincessAndContenders.Interfaces;
using PrincessAndContenders.Utils;

namespace PrincessAndContenders;

public class Hall : IHall
{
    private readonly Queue<Contender> _contenders;

    public Hall(Queue<Contender> contenders) =>
        _contenders = contenders;
    public Hall(IContendersGenerator contendersGenerator) =>
        _contenders = contendersGenerator.GenerateContenders();

    public Contender GetNext()
    {
        if (!_contenders.TryDequeue(out var contender))
            throw new EmptyHallException();
        
        Logger.Log(contender.Name);

        return contender;
    }

    public bool Contains(Contender? contender) =>
        contender != null && _contenders.Contains(contender);
}