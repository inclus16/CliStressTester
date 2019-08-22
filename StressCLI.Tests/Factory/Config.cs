using Newtonsoft.Json;
using StressCLI.src.Cli.Commands.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StressCLI.Tests.Factory
{
    class Config
    {
        public readonly TestConfig TestConfig;
        public Config()
        {
            TestConfig = new TestConfig()
            {
                Data = "{\"data\":\"testValue\"}",
                Headers = JsonConvert.SerializeObject(new Dictionary<string, string>()
                {
                    {"Authorization","Bearer 228"},
                    { "X-Some-Header","4:20" }
                }),
                Method = HttpTestMethod.Post,
                Parallel=20,
                RequestFormat = RequestFormat.Body,
                ResultWriter = ResultWriter.Console,
                StopSignal= StopSignal.InternalServerError,
                TimeOut = TimeSpan.FromSeconds(60),
                Url = new Uri("https://localhost")
            };
        }

    }
}
