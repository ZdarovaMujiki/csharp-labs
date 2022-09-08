using static lab1.Constants;

namespace lab1;

static class Program
{
    private static void Main()
    {
        var hall = new Hall();
        var friend = new Friend();

        var (i, contender) = hall.GetNext();
        while (contender != null)
        {
            var eChoose = friend.EChoose(i, friend.GetRelativeRank(contender));
            var eSkip = friend.ESkipArray[i - 1];

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