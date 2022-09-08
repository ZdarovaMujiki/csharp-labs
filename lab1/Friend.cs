using System;
using System.Collections.Generic;
using static lab1.Constants;

namespace lab1;

class Friend
{
    private readonly SortedSet<Contender> _contendersTop = new();
    public readonly double[] ESkipArray = new double[ContendersAmount];
        
    public Friend()
    {
        var length = ESkipArray.Length;
        ESkipArray[length - 1] = MonasteryPoints;

        for (var i = length - 2; i >= 0; i--)
            ESkipArray[i] = ESkip(i);
    }

    public int GetRelativeRank(Contender newContender)
    {
        _contendersTop.Add(newContender);
        var rank = 0;
        foreach (var contender in _contendersTop)
        {
            rank++;
            if (contender == newContender)
                break;
        }

        return rank;
    }
    public double EChoose(int i, int rank) =>
        _contendersTop.Count - rank + 1 < BadMarriageRankBorder
            ? rank * (ContendersAmount + 1D) / (i + 1)
            : BadMarriagePoints;

    private double ESkip(int i)
    {
        double sum = 0;
        for (var rank = 1; rank <= i + 1; rank++)
            sum += Math.Max(EChoose(i + 1, rank), ESkipArray[i + 1]);

        return sum / (i + 1);
    }
}