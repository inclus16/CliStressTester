using StressCLI.src.Cli.Commands.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StressCLI.src.TestCore.ResultSetter
{
    internal abstract class AbstractResultSetter
    {
        public AbstractResultSetter()
        {
            Result = new ResultStruct();
        }

        protected ResultStruct Result;
        public AbstractResultSetter SetCompletedTasks(IEnumerable<RequestTask> tasks)
        {
            Result.CompletedRequests = tasks.Select(x => new CompletedRequest(x)).ToArray();
            return this;
        }


        public AbstractResultSetter SetStartedAtTime(DateTime startedAt)
        {
            Result.StartedAt = startedAt;
            return this;
        }

        public AbstractResultSetter SetEndedAtTime(DateTime endedAt)
        {
            Result.EndedAt = endedAt;
            return this;
        }

        public AbstractResultSetter SetStopReason(StopSignal stopSignal)
        {
            Result.StopReason = stopSignal.ToString();
            return this;
        }
    }
}
