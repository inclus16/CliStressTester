using System;
using Xunit;
using StressCLI.src.Entities.Parser;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using StressCLI.Tests.Faker;
using System.Linq;
using StressCLI.src.Entities.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using StressCLI.src.Entities;

namespace StressCLI.Tests
{
    public class ConfigParserTests
    {
        private readonly IConfigParser ConfigParser;

        private readonly Config Config;  

        public ConfigParserTests()
        {
            
            ConfigParser =Program.BuildServiceProvider().GetService<IConfigParser>();
            Config = new Config();
            ConfigParser.SetConfig(Config.GetTestConfig());
        }

      

        [Fact]
        public void TestUri()
        {
            Uri uri = ConfigParser.GetUri();
            Assert.Equal(Config.GetTestConfig().Url, uri);
        }

        [Fact]
        public async Task TestRequestBody()
        {
            HttpRequestMessage request = ConfigParser.GetRequest();
            string body = await request.Content.ReadAsStringAsync();
            Assert.Equal(Config.GetTestConfig().Data, body);
        }

        [Fact]
        public void TestRequestHeaders()
        {
            HttpRequestMessage request = ConfigParser.GetRequest();
            Dictionary<string, string> actualHeaders = JsonConvert.DeserializeObject<Dictionary<string, string>>(Config.GetTestConfig().Headers);
            foreach(KeyValuePair<string,string> keyValue in actualHeaders)
            {
                Assert.True(request.Headers.Contains(keyValue.Key));
                Assert.Equal(keyValue.Value, request.Headers.Where(x => x.Key == keyValue.Key).FirstOrDefault().Value.First());
            }
        }

        [Fact]
        public async Task TestSetRandomDataNameEng()
        {
            TestConfig testConfig = Config.GetTestConfig();
            testConfig.Data = "{\"data\":\"%NAME_ENG%\"}";
            ConfigParser.SetConfig(testConfig);
            HttpRequestMessage request = ConfigParser.GetRequest();
            string requestBody = await request.Content.ReadAsStringAsync();
            Assert.True(testConfig.Data != requestBody);
        }
        [Fact]
        public async Task TestSetRandomDataNameRus()
        {
            TestConfig testConfig = Config.GetTestConfig();
            testConfig.Data = "{\"data\":\"%NAME_RUS%\"}";
            ConfigParser.SetConfig(testConfig);
            HttpRequestMessage request = ConfigParser.GetRequest();
            string requestBody = await request.Content.ReadAsStringAsync();
            Assert.True(testConfig.Data != requestBody);
        }
        [Fact]
        public async Task TestSetRandomDataEmail()
        {
            TestConfig testConfig = Config.GetTestConfig();
            testConfig.Data = "{\"data\":\"%EMAIL%\"}";
            ConfigParser.SetConfig(testConfig);
            HttpRequestMessage request = ConfigParser.GetRequest();
            string requestBody = await request.Content.ReadAsStringAsync();
            Assert.True(testConfig.Data != requestBody);
        }
        [Fact]
        public async Task TestSetRandomDataNumber()
        {
            TestConfig testConfig = Config.GetTestConfig();
            testConfig.Data = "{\"data\":\"%NUMBER%\"}";
            ConfigParser.SetConfig(testConfig);
            HttpRequestMessage request = ConfigParser.GetRequest();
            string requestBody = await request.Content.ReadAsStringAsync();
            Assert.True(testConfig.Data != requestBody);
        }
    }
}
