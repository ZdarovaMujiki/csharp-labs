using Microsoft.Extensions.Hosting;
using PrincessAndContenders.Data;
using PrincessAndContenders.Interfaces;
using PrincessAndContenders.Utils;
using static PrincessAndContenders.Utils.Constants;

namespace PrincessAndContenders;

public class Princess : IPrincess
{
    private readonly SortedSet<Contender> _contendersTop;
    private readonly double[] _eSkipArray = new double[ContendersAmount];
    
    private readonly IHostApplicationLifetime? _applicationLifetime;
    private readonly IHall _hall;
    
    private Task? _applicationTask;

    public Princess(IHall hall, IFriend friend, IHostApplicationLifetime? applicationLifetime)
    {
        _applicationLifetime = applicationLifetime;
        _hall = hall;
        
        _contendersTop = new SortedSet<Contender>(friend);
        
        var length = _eSkipArray.Length;
        _eSkipArray[length - 1] = MonasteryPoints;

        for (var i = length - 2; i >= 0; i--)
            _eSkipArray[i] = ESkip(i);
    }

    private int GetRelativeRank(Contender newContender)
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

    private double EChoose(int i, int rank) =>
        _contendersTop.Count - rank + 1 < BadMarriageRankBorder
            ? rank * (ContendersAmount + 1D) / (i + 1)
            : BadMarriagePoints;

    private double ESkip(int i)
    {
        double sum = 0;
        for (var rank = 1; rank <= i + 1; rank++)
            sum += Math.Max(EChoose(i + 1, rank), _eSkipArray[i + 1]);

        return sum / (i + 1);
    }

    public Contender? GetMarried()
    {
        var contender = GetBestContender();
        var points = GetPoints(contender);

        Logger.Log("-----");
        Logger.Log(points);
        return contender;
    }

    public Contender? GetBestContender()
    {
        for (var i = 0; i < ContendersAmount; ++i)
        {
            var contender = _hall.GetNext();

            var eChoose = EChoose(i + 1, GetRelativeRank(contender));
            var eSkip = _eSkipArray[i];

            if (eChoose >= eSkip)
                return contender;
        }

        return null;
    }

    public static int GetPoints(Contender? contender)
    {
        if (contender == null) return MonasteryPoints;

        return contender.Rank switch
        {
            100 => 20,
            98 => 50,
            96 => 100,
            _ => 0
        };
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _applicationTask = Task.Run(() =>
        {
            GetMarried();
            _applicationLifetime?.StopApplication();
        }, cancellationToken);
        
        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_applicationTask != null)
        {
            await _applicationTask;
        }
    }
}