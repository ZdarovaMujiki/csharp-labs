using PrincessAndContenders.Data;

namespace PrincessAndContenders.Interfaces;

public interface IHall
{
    public Contender GetNext();
    public bool Contains(Contender? contender);
}