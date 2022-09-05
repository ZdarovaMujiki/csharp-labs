using System;
using System.IO;
using System.Linq;

namespace lab1
{
    class Hall
    {
        public const int ContendersAmount = 100;
        public const int MonasteryPoints = 10;
        private const string Path = "husbands.txt";

        private Contender[] _contenders = new Contender[ContendersAmount];
        private readonly Random _random = new();
        public Hall()
        {
            var index = 0;
            foreach (var name in File.ReadLines(Path))
            {
                _contenders[index] = new Contender { Name = name, Rank = ++index };
                if (index == ContendersAmount) break;
            }
        }

        public Contender GetNext()
        {
            if (_contenders.Length == 0) return null;
            
            var i = _random.Next(_contenders.Length);
            var contender = _contenders[i];
            _contenders = _contenders.Where((_, index) => index != i).ToArray();

            return contender;
        }
    }
}