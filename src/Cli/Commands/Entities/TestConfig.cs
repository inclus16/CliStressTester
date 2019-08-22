using System;

namespace StressCLI.src.Cli.Commands.Entities
{
    struct TestConfig
    {
        public TimeSpan TimeOut { get; set; }

        public Uri Url { get; set; }

        public HttpTestMethod Method { get; set; }

        public RequestFormat RequestFormat { get; set; }

        public StopSignal StopSignal { get; set; }

        public object Data { get; set; }

        public int Parallel { get; set; }

    }
}
