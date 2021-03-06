﻿using CommandDotNet.Attributes;
using StressCLI.src.Cli.Commands.Dto;
using StressCLI.src.Entities;
using StressCLI.src.Entities.Interfaces;
using StressCLI.src.Entities.ResultSetter;
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

        [InjectProperty]
        public IExecutor Executor { get; set; }


        [ApplicationMetadata(Description = "Run new stress session")]
        public void Run(RunTestDto dto)
        {
            Validate(dto);
            if (HaveValidationErrors())
            {
                PrintValidationErrors();
                return;
            }

            Executor.SetConfig(dto.GetTestConfig());
            Executor.Configurate();
            Task session= Task.Run(() =>
            {
                Executor.StartTest();
            });
            bool manualyStopped = false;
            while (!session.IsCompleted)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.KeyChar == STOP)
                {
                    Executor.StopExecution();
                    manualyStopped = true;
                    break;
                }
            }
            List<RequestTask> result = Executor.GetResult();
            if (result.Count==0)
            {
                return;
            }
            AbstractWriter writer = WritersFactory.GetWriter(dto.ResultWriter);
            writer.SetCompletedTasks(result)
                .SetStartedAtTime(Executor.GetStartedAt())
                .SetEndedAtTime(DateTime.Now)
                .SetStopReason(manualyStopped ? StopSignal.Manual : dto.StopSignal);
            writer.Write();
        }
    }
}
