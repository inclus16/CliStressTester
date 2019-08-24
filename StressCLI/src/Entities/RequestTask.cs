using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace StressCLI.src.Entities
{
    public class RequestTask
    {
        public TimeSpan StartedAt { get; set; }

        public TimeSpan EndedAt { get; set; }

        public TimeSpan TotalExecutionTime
        {
            get
            {
                return EndedAt - StartedAt;
            }
        }

        public Task<HttpResponseMessage> Response { get; set; }
    }
}
