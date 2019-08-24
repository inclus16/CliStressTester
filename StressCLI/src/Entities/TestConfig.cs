using System;

namespace StressCLI.src.Entities
{
    public struct TestConfig
    {
        public TimeSpan TimeOut { get; set; }

        public Uri Url { get; set; }

        public HttpTestMethod Method { get; set; }

        public RequestFormat RequestFormat { get; set; }

        public StopSignal StopSignal { get; set; }

        public string Data { get; set; }

        public string Headers { get; set; }

        public int Parallel { get; set; }

        public ResultWriter ResultWriter { get; set; }

    }
}
