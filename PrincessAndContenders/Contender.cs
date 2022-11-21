namespace PrincessAndContenders;

public record Contender
{
    public string Name { get; init; }
    public int Rank { get; init; }

    public override string ToString()
    {
        return $"{Name} {Rank}";
    }

    public static Contender Parse(string stringContender)
    {
        var fields = stringContender.Split(" ");
        return new Contender {Name = $"{fields[0]} {fields[1]}", Rank = int.Parse(fields[2])};
    }
}