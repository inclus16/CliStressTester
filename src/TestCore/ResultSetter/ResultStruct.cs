using System;
using System.Collections.Generic;
using System.Text;

namespace StressCLI.src.TestCore.ResultSetter
{
    struct ResultStruct
    {
        public DateTime StartedAt { get; set; }

        public DateTime EndedAt { get; set; }

        public string StopReason { get; set; }

        public CompletedRequest[] CompletedRequests { get; set; }
    }
}
