
using StressCLI.src.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StressCLI.src.TestCore.ResultSetter
{
    public abstract class AbstractWriter
    {
        public AbstractWriter()
        {
            Result = new ResultStruct();
        }

        protected ResultStruct Result;
        public AbstractWriter SetCompletedTasks(IEnumerable<RequestTask> tasks)
        {
            Result.CompletedRequests = tasks.Select(x => new CompletedRequest(x)).ToArray();
            return this;
        }


        public AbstractWriter SetStartedAtTime(DateTime startedAt)
        {
            Result.StartedAt = startedAt;
            return this;
        }

        public AbstractWriter SetEndedAtTime(DateTime endedAt)
        {
            Result.EndedAt = endedAt;
            return this;
        }

        public AbstractWriter SetStopReason(StopSignal stopSignal)
        {
            Result.StopReason = stopSignal.ToString();
            return this;
        }

        public abstract void Write();
    }
}
