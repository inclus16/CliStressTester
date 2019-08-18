using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StressCLI.src.TestCore
{
    class RequestTask
    {
        public TimeSpan StartedAt { get; set; }

        public TimeSpan EndedAt { get; set; }

        public TimeSpan TotalExecutionTime {
            get {
               return EndedAt - StartedAt;
            }
        }

        public Task<HttpResponseMessage> Response { get; set; }
    }
}
