﻿using Newtonsoft.Json;
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
            HttpRequestMessage request = new HttpRequestMessage(GetMethod(), TestConfig.Url);
            if (TestConfig.Method == HttpTestMethod.Post || TestConfig.Method == HttpTestMethod.Put)
            {
                SetRequestData(ref request);
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
                    throw new ArgumentOutOfRangeException($"RequestFormat {requestFormat.ToString()} not availdable");

            }
            
        }

        private void BuildFromData(ref HttpRequestMessage request)
        {
            request.Content = new FormUrlEncodedContent(GetFormData());
        }

        private Dictionary<string,string> GetFormData()
        {
            try
            {
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(TestConfig.Data.ToString());
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        private void BuildBodyData(ref HttpRequestMessage request)
        {
            request.Content = new StringContent(TestConfig.Data.ToString());
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