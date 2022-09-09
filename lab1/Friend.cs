using System.Collections.Generic;

namespace lab1;

class Friend : IComparer<Contender>
{
    public int Compare(Contender x, Contender y)
    {
        if (ReferenceEquals(x, y)) return 0;
        if (ReferenceEquals(null, y)) return 1;
        if (ReferenceEquals(null, x)) return -1;
        return x.Rank.CompareTo(y.Rank);
    }
}