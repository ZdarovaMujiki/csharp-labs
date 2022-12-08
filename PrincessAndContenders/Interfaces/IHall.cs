using PrincessAndContenders.Data;

namespace PrincessAndContenders.Interfaces;

public interface IHall
{
    public Queue<Contender> Contenders { get; set; }
    public Contender GetNext();
    public bool Contains(Contender? contender);
}