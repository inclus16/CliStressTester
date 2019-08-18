using StressCLI.src.Cli.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StressCLI.src.Cli
{
    class Handler
    {
        private readonly List<ICommand> Commands;
        public Handler()
        {
            Commands= AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(ICommand).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(x => Activator.CreateInstance(x) as ICommand).ToList();
        }

        public void Handle(string[] args)
        {
            if (args.Length == 0)
            {
                CliNotifier.PrintError("Need atleast 1 word to find abailable commands");
                return;
            }
            ICommand command = Commands.Where(x => x.GetName() == args[0]).FirstOrDefault();
            if (command == null)
            {
                CliNotifier.PrintError($"Command {args[0]} not found, type --help to list available");
                return;
            }
            command.SetData(args);
            HandleCommand(command);
        }

        private void HandleCommand(ICommand command)
        {
            command.Validate();
            if (!command.IsValid())
            {
                CliNotifier.PrintError(command.GetValidationError());
                return;
            }
            command.PrepareData();
            command.Execute();
        }
    }
}
