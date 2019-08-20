using StressCLI.src.Cli;
using System;
using System.Threading.Tasks;

namespace StressCLI
{
    internal class Program
    {
        private const char STOP = 'q';

        private static void Main(string[] args)
        {
            Handler handler = new Handler();
            Task handleTask= Task.Run(() => handler.Handle(args));
            while (!handleTask.IsCompleted)
            {
                ConsoleKeyInfo consoleKey = Console.ReadKey();
                if (consoleKey.KeyChar == Program.STOP)
                {
                    handler.Cancel();
                    Environment.Exit(0);
                }
            }


        }
    }
}
