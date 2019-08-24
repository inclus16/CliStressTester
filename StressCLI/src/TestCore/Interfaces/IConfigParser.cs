
using StressCLI.src.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace StressCLI.src.TestCore.Interfaces
{
    public interface IConfigParser
    {
        StopSignal GetStopSignal();
        HttpRequestMessage GetRequest();
        ResultWriter GetResultWriterType();
        int GetParallel();
        Uri GetUri();
        TimeSpan GetTimeOut();
        void SetConfig(TestConfig config);

    }
}
