namespace PrincessAndContenders;

public record Contender
{
    public string Name { get; init; }
    public int Rank { get; init; }

    public override string ToString()
    {
        return $"{Name} {Rank}";
    }
}