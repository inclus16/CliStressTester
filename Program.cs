using StressCLI.src.Cli;
using System;
using System.Threading.Tasks;

namespace StressCLI
{
    class Program
    {
        private  const char STOP = 'q';
        static void Main(string[] args)
        {
            Handler handler = new Handler();
            Task.Run(()=>handler.Handle(args));
            while (true)
            {
                ConsoleKeyInfo consoleKey = Console.ReadKey();
                if (consoleKey.KeyChar == Program.STOP)
                {
                    handler.Cancel();
                    return;
                }
            }
            

        }
    }
}
