using System;

namespace StressCLI.src.TestCore.ResultSetter
{
    internal struct ResultStruct
    {
        public DateTime StartedAt { get; set; }

        public DateTime EndedAt { get; set; }

        public string StopReason { get; set; }

        public CompletedRequest[] CompletedRequests { get; set; }
    }
}
