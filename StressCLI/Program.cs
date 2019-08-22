using CommandDotNet;
using StressCLI.src.Cli;
using StressCLI.src.Cli.Commands;
using System;
using System.Threading.Tasks;

namespace StressCLI
{
    internal class Program
    {
        private static int Main(string[] args)
        {
            AppRunner<Session> appRunner = new AppRunner<Session>();
            return appRunner.Run(args);
        }
    }
}
