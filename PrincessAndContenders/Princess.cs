using Microsoft.Extensions.Hosting;
using static PrincessAndContenders.Constants;

namespace PrincessAndContenders;

public class Princess : IHostedService
{
    private readonly SortedSet<Contender> _contendersTop;
    private readonly double[] _eSkipArray = new double[ContendersAmount];
    
    private readonly IHostApplicationLifetime _applicationLifetime;
    private readonly Hall _hall;
    
    private Task? _applicationTask;

    public Princess(Hall hall, Friend friend, IHostApplicationLifetime applicationLifetime)
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

    private Contender? GetBestContender()
    {
        var (i, contender) = _hall.GetNext();
        while (i < ContendersAmount)
        {
            var eChoose = EChoose(i, GetRelativeRank(contender!));
            var eSkip = _eSkipArray[i - 1];

            if (eChoose >= eSkip)
                break;
                
            (i, contender) = _hall.GetNext();
        }

        return contender;
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
            var contender = GetBestContender();
            var points = GetPoints(contender);

            Logger.Log("-----");
            Logger.Log(points);
            _applicationLifetime.StopApplication();
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