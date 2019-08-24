using CommandDotNet;
using CommandDotNet.IoC.MicrosoftDependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using StressCLI.src.Cli;
using StressCLI.src.Cli.Commands;
using StressCLI.src.Entities;
using StressCLI.src.Entities.Interfaces;
using StressCLI.src.Entities.Parser;
using System;
using System.Threading.Tasks;

namespace StressCLI
{
    public class Program
    {
       
        private static int Main(string[] args)
        {
            AppRunner<Session> appRunner = new AppRunner<Session>().UseMicrosoftDependencyInjection(BuildServiceProvider());
            return appRunner.Run(args);
        }

        public static IServiceProvider BuildServiceProvider()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IConfigParser, ConfigParser>();
            serviceCollection.AddSingleton<IRandomSeeder, RandomSeeder>();
            serviceCollection.AddSingleton<IExecutor, Executor>();
            return serviceCollection.BuildServiceProvider();
        }
    }
}
