namespace PrincessAndContenders;

public static class Logger
{
    private const string Path = "log.txt";

    static Logger() => 
        File.WriteAllText(Path, string.Empty);

    public static void Log<T>(T message) =>
        File.AppendAllText(Path, message + "\n");
}