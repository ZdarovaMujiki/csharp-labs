namespace PrincessAndContenders.Data;

public record Attempt : Entity
{
    public List<Contender> Contenders { get; set; }
}