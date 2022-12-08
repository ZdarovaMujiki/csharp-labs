using System.Net.Http.Json;
using PrincessAndContenders.Controllers;
using PrincessAndContenders.Utils;
namespace PrincessAndContenders.Client;

public class Princess
{
    private static readonly HttpClient HttpClient = new();
    private readonly SortedSet<string> _contendersNamesTop = new(new ContendersComparator());
    private readonly double[] _eSkipArray = new double[Constants.ContendersAmount];
    
    private class ContendersComparator : IComparer<string>
    {
        public int Compare(string name1, string name2)
        {
            var names = new ContendersNames { name1 = name1, name2 = name2 };
            var response = HttpClient.PostAsync("/friend/123/compare", JsonContent.Create(names));
            response.Wait();
            var str = response.Result.Content.ReadAsStringAsync();
            str.Wait();
            var name = str.Result;
            
            return name == name1 ? 1 : -1;
        }
    }

    public Princess()
    {
        HttpClient.BaseAddress = new Uri("http://localhost:5000");
        
        var length = _eSkipArray.Length;
        _eSkipArray[length - 1] = Constants.MonasteryPoints;

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
        _contendersNamesTop.Count - rank + 1 < Constants.BadMarriageRankBorder
            ? rank * (Constants.ContendersAmount + 1D) / (i + 1)
            : Constants.BadMarriagePoints;

    private double ESkip(int i)
    {
        double sum = 0;
        for (var rank = 1; rank <= i + 1; rank++)
            sum += Math.Max(EChoose(i + 1, rank), _eSkipArray[i + 1]);

        return sum / (i + 1);
    }

    public void GetMarried()
    {
        var contender = GetBestContender();
        var points = GetPoints(contender);

        Logger.Log("-----");
        Logger.Log(points);
    }

    public string? GetBestContender()
    {
        _contendersNamesTop.Clear();
        for (var i = 0; i < Constants.ContendersAmount; ++i)
        {
            var response = HttpClient.PostAsync("/hall/123/next", null);
            response.Wait();
            var nameResponse = response.Result.Content.ReadAsStringAsync();
            nameResponse.Wait();
            var name = nameResponse.Result;

            var eChoose = EChoose(i + 1, GetRelativeRank(name));
            var eSkip = _eSkipArray[i];

            if (eChoose >= eSkip)
                return name;
        }

        return null;
    }

    public static int GetPoints(string? contenderName)
    {
        if (contenderName == null) return Constants.MonasteryPoints;
        var response = HttpClient.PostAsync("/hall/123/select", null);
        response.Wait();
        var rankResponse = response.Result.Content.ReadAsStringAsync();
        rankResponse.Wait();
        var rank = rankResponse.Result;

        return rank switch
        {
            "100" => 20,
            "98" => 50,
            "96" => 100,
            _ => 0
        };
    }
}