using PrincessAndContenders.Data;
using PrincessAndContenders.Exceptions;
using PrincessAndContenders.Interfaces;
using PrincessAndContenders.Utils;

namespace PrincessAndContenders;

public class Hall : IHall
{
    public Queue<Contender> Contenders { get; set; }

    public Hall(Queue<Contender> contenders) =>
        Contenders = contenders;
    
    public Hall(IContendersGenerator contendersGenerator) =>
        Contenders = contendersGenerator.GenerateContenders();

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