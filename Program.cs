using StressCLI.src.Cli;
using System;

namespace StressCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Handler handler = new Handler();
            handler.Handle(args);
        }
    }
}
