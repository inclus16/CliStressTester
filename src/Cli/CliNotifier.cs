using System;
using System.Collections.Generic;
using System.Text;

namespace StressCLI.src.Cli
{
    class CliNotifier
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
