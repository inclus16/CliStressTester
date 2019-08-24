using System;

namespace StressCLI.src.Cli
{
    internal class CliNotifier
    {
        public static void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Print(message);
        }

        public static void PrinWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Print(message);
        }

        public static void PrintInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Print(message);
        }

        private static void Print(string message)
        {
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
