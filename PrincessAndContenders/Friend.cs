namespace PrincessAndContenders;

public class Friend : IComparer<Contender>
{
    private Hall _hall;
    public Friend(Hall hall) => _hall = hall;

    public int Compare(Contender? x, Contender? y)
    {
        if (_hall.Contains(x) || _hall.Contains(y))
            throw new UnknownContenderException();
        
        if (ReferenceEquals(x, y)) return 0;
        if (ReferenceEquals(null, y)) return 1;
        if (ReferenceEquals(null, x)) return -1;
        return x.Rank.CompareTo(y.Rank);
    }
}