namespace PrincessAndContenders.Utils;

public static class Constants
{
    public static int ContendersAmount => 100;
    public static int MonasteryPoints => 10;
    public static int BadMarriagePoints => 0;
    public static int BadMarriageRankBorder => 50;
    public enum ContendersRanks
    {
        First = 100,
        Third = 98,
        Fifth = 96,
    }
    
    public enum ContendersPoints
    {
        Best = 100,
        Fine = 50,
        Worst = 20,
    }
}