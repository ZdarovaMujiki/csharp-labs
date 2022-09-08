using System;
using System.IO;

namespace lab1;

public static class Logger
{
    private const string Path = "log.txt";

    static Logger() => 
        File.WriteAllText(Path, string.Empty);

    public static void Log<T>(T message)
    {
        Console.WriteLine(message.ToString());
        File.AppendAllText(Path, message + "\n");
    }
}