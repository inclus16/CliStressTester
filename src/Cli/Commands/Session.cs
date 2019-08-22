using CommandDotNet.Attributes;
using StressCLI.src.Cli.Commands.Dto;
using StressCLI.src.Cli.Commands.Entities;
using StressCLI.src.TestCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace StressCLI.src.Cli.Commands
{
    class Session: AbstractCommand
    {
        private const char STOP = 'q';

        [ApplicationMetadata(Description = "Run new stress session")]
        public void Run(RunTestDto dto)
        {
            Validate(dto);
            if (HaveValidationErrors())
            {
                PrintValidationErrors();
                return;
            }
            Executor exec = new Executor();
            exec.SetConfig(dto.GetTestConfig());
            exec.Configurate();
            Task session= Task.Run(() =>
            {
                exec.StartTest();
            });
            while (!session.IsCompleted)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.KeyChar == STOP)
                {
                    exec.StopExecution(true);
                    return;
                }
            }
        }
    }
}
