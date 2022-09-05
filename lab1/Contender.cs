using System;

namespace lab1
{
    class Contender : IComparable<Contender>
    {
        public string Name { get; init; }
        public int Rank { get; init; }

        public int CompareTo(Contender other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Rank.CompareTo(other.Rank);
        }

        public override string ToString()
        {
            return $"{Name} {Rank}";
        }
    }
}