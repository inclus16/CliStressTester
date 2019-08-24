using Newtonsoft.Json;
using StressCLI.src.Entities;
using StressCLI.src.TestCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace StressCLI.src.TestCore.Parser
{
    public class ConfigParser:IConfigParser
    {

        private TestConfig TestConfig;

        private readonly IRandomSeeder RandomSeeder;

        public ConfigParser(IRandomSeeder randomSeeder)
        {
            RandomSeeder = randomSeeder;
        }
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
        public ResultWriter GetResultWriterType()
        {
            return TestConfig.ResultWriter;
        }


        public int GetParallel()
        {
            return TestConfig.Parallel;
        }

        public HttpRequestMessage GetRequest()
        {
            HttpRequestMessage request = new HttpRequestMessage(GetMethod(), TestConfig.Url);
            if ((TestConfig.Method == HttpTestMethod.Post || TestConfig.Method == HttpTestMethod.Put) && TestConfig.Data!=null)
            {
                SetRequestData(ref request);
            }
            if (TestConfig.Headers != null)
            {
                SetHeaders(ref request);
            }
            return request;
        }

        private void SetRequestData(ref HttpRequestMessage request)
        {
            RequestFormat requestFormat = TestConfig.RequestFormat;
            switch (requestFormat)
            {
                case RequestFormat.Body:
                    BuildBodyData(ref request);
                    break;
                case RequestFormat.FormData:
                    BuildFromData(ref request);
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"RequestFormat {requestFormat.ToString()} not supported");

            }

        }

        private void BuildFromData(ref HttpRequestMessage request)
        {
            request.Content = new FormUrlEncodedContent(GetFormData());
        }

        private void SetHeaders(ref HttpRequestMessage request)
        {
            Dictionary<string, string> headers = JsonConvert.DeserializeObject<Dictionary<string, string>>(TestConfig.Headers);
            foreach(KeyValuePair<string,string> keyValue in headers)
            {
                request.Headers.Add(keyValue.Key, keyValue.Value);
            }
        }

        private Dictionary<string, string> GetFormData()
        {
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(RandomSeeder.SetRandom(TestConfig.Data));
        }

        private void BuildBodyData(ref HttpRequestMessage request)
        {
            request.Content = new StringContent(RandomSeeder.SetRandom(TestConfig.Data));
            request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
        }

        private HttpMethod GetMethod()
        {
            HttpTestMethod method = TestConfig.Method;
            switch (method)
            {
                case HttpTestMethod.Get:
                    return HttpMethod.Get;
                case HttpTestMethod.Delete:
                    return HttpMethod.Delete;
                case HttpTestMethod.Post:
                    return HttpMethod.Post;
                case HttpTestMethod.Put:
                    return HttpMethod.Put;
                default:
                    throw new ArgumentOutOfRangeException($"{method.ToString()} not available");
            }
        }

        public StopSignal GetStopSignal()
        {
            return TestConfig.StopSignal;
        }
    }

    
}
