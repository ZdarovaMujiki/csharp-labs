using PrincessAndContenders.Utils;
using static PrincessAndContenders.Utils.Constants;

namespace PrincessAndContenders.Client;

public class Princess
{
    private readonly SortedSet<string> _contendersNamesTop = new(new ContendersComparator());
    private readonly double[] _eSkipArray = new double[ContendersAmount];
    private class ContendersComparator : IComparer<string>
    {
        public int Compare(string name1, string name2)
        {
            var name = Api.CompareContenders(name1, name2);
            
            return name == name1 ? 1 : -1;
        }
    }

    public Princess()
    {
        var length = _eSkipArray.Length;
        _eSkipArray[length - 1] = MonasteryPoints;

        for (var i = length - 2; i >= 0; i--)
            _eSkipArray[i] = ESkip(i);
    }

    private int GetRelativeRank(string newContenderName)
    {
        _contendersNamesTop.Add(newContenderName);
        var rank = 0;
        foreach (var contenderName in _contendersNamesTop)
        {
            rank++;
            if (contenderName == newContenderName)
                break;
        }

        return rank;
    }

    private double EChoose(int i, int rank) =>
        _contendersNamesTop.Count - rank + 1 < BadMarriageRankBorder
            ? rank * (ContendersAmount + 1D) / (i + 1)
            : BadMarriagePoints;

    private double ESkip(int i)
    {
        double sum = 0;
        for (var rank = 1; rank <= i + 1; rank++)
            sum += Math.Max(EChoose(i + 1, rank), _eSkipArray[i + 1]);

        return sum / (i + 1);
    }

    public async Task GetMarried()
    {
        await Api.ResetSession();
        
        var contender = await GetBestContender();
        var points = await GetPoints(contender);

        Logger.Log("-----");
        Logger.Log(points);
    }

    private async Task<string?> GetBestContender()
    {
        _contendersNamesTop.Clear();
        for (var i = 0; i < ContendersAmount; ++i)
        {
            var name = await Api.GetNextContender();

            var eChoose = EChoose(i + 1, GetRelativeRank(name));
            var eSkip = _eSkipArray[i];

            if (eChoose >= eSkip)
                return name;
        }

        return null;
    }

    private static async Task<int> GetPoints(string? contenderName)
    {
        if (contenderName == null) return MonasteryPoints;
        var rank = await Api.GetContenderRank();
        Console.WriteLine(contenderName);
        Console.WriteLine(rank);

        return rank switch
        {
            (int)ContendersRanks.First => (int)ContendersPoints.Worst,
            (int)ContendersRanks.Third => (int)ContendersPoints.Fine,
            (int)ContendersRanks.Fifth => (int)ContendersPoints.Best,
            _ => BadMarriagePoints
        };
    }
}