
using StressCLI.src.Entities;
using System;
using System.Collections.Generic;

namespace StressCLI.src.Entities.Interfaces
{
    public interface IExecutor
    {
        void SetConfig(TestConfig config);
        void Configurate();
        void StartTest();
        void StopExecution();

        List<RequestTask> GetResult();

        DateTime GetStartedAt();
    }
}