using System;
using System.Net;
using System.Threading.Tasks;

namespace StressCLI.src.Entities
{
    public class CompletedRequest
    {
        public TimeSpan StartedAt { get; set; }

        public TimeSpan CompletedBy { get; set; }

        public HttpStatusCode ResponseCode { get; set; }

        public string ResponseMessage { get; set; }

        public CompletedRequest(RequestTask task)
        {
            StartedAt = task.StartedAt;
            CompletedBy = task.TotalExecutionTime;
            ResponseCode = task.Response.Result.StatusCode;
            Task<string> responseBody = task.Response.Result.Content.ReadAsStringAsync();
            responseBody.GetAwaiter().OnCompleted(() =>
            {
                ResponseMessage = responseBody.Result;
                return;
            });
        }
    }
}
