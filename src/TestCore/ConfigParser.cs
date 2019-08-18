using StressCLI.src.Cli.Commands.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace StressCLI.src.TestCore
{
    class ConfigParser
    {

        private TestConfig TestConfig;
        public void SetConfig(TestConfig config)
        {
            TestConfig = config;
        }


        public TimeSpan GetTimeOut()
        {
            return TestConfig.TimeOut;
        }

        public Uri GetUri()
        {
            return TestConfig.Url;
        }

        public bool HaveBody()
        {
            return TestConfig.Method == HttpTestMethod.Delete || TestConfig.Method == HttpTestMethod.Get;
        }

        public int GetParralel()
        {
            return TestConfig.Parallel;
        }

        public HttpRequestMessage GetRequest()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, TestConfig.Url);
            return request;
        }

        public StopSignal GetStopSignal()
        {
            return TestConfig.StopSignal;
        }
    }
}
