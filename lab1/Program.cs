using System;

namespace lab1
{
    static class Program
    {
        private static void Main()
        {
            var hall = new Hall();
            var friend = new Friend();

            var contender = hall.GetNext();
            var i = 1;
            while (contender != null)
            {
                Logger.Log(contender.ToString());
                var eChoose = friend.EChoose(i, friend.Remember(contender));
                var eSkip = friend.ESkipArray[i - 1];

                if (eChoose >= eSkip)
                    break;
                
                contender = hall.GetNext();
                i++;
            }

            Logger.Log("-----");
            Logger.Log(contender?.ToString());
        }
    }
}