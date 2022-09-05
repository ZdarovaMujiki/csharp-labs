using System;
using System.IO;

namespace lab1
{
    public static class Logger
    {
        private const string Path = "log.txt";

        static Logger() => 
            File.WriteAllText(Path, string.Empty);

        public static void Log(string message)
        {
            Console.WriteLine(message);
            File.AppendAllText(Path, message + "\n");
        }
    }
}