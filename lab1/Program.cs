using static lab1.Constants;

namespace lab1;

static class Program
{
    private static void Main()
    {
        var hall = new Hall();
        var princess = new Princess();

        var (i, contender) = hall.GetNext();
        while (contender != null)
        {
            var eChoose = princess.EChoose(i, princess.GetRelativeRank(contender));
            var eSkip = princess.ESkipArray[i - 1];

            if (eChoose >= eSkip)
                break;
                
            (i, contender) = hall.GetNext();
        }

        Logger.Log("-----");
        if (contender != null)
            Logger.Log(contender.Rank > BadMarriageRankBorder ? contender.Rank : BadMarriagePoints);
        else
            Logger.Log(MonasteryPoints);
    }
}