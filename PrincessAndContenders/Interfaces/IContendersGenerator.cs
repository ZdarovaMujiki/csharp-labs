using PrincessAndContenders.Data;

namespace PrincessAndContenders.Interfaces;

public interface IContendersGenerator
{
    public Queue<Contender> GenerateContenders();
}