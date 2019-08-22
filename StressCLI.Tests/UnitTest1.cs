using System;
using Xunit;
using StressCLI.src.TestCore.Parser;
using StressCLI.src.Cli.Commands.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using StressCLI.Tests.Factory;
using System.Linq;

namespace StressCLI.UnitTests
{
    public class ConfigParserTests
    {
        private readonly ConfigParser ConfigParser;

        private readonly Config Config;

        public ConfigParserTests()
        {
            ConfigParser = new ConfigParser();
            Config = new Config();
            ConfigParser.SetConfig(Config.TestConfig);
        }

      

        [Fact]
        public void TestUri()
        {
            Uri uri = ConfigParser.GetUri();
            Assert.Equal(Config.TestConfig.Url, uri);
        }

        [Fact]
        public async Task TestRequestBody()
        {
            HttpRequestMessage request = ConfigParser.GetRequest();
            string body = await request.Content.ReadAsStringAsync();
            Assert.Equal(Config.TestConfig.Data, body);
        }

        [Fact]
        public void TestRequestHeaders()
        {
            HttpRequestMessage request = ConfigParser.GetRequest();
            Dictionary<string, string> actualHeaders = JsonConvert.DeserializeObject<Dictionary<string, string>>(Config.TestConfig.Headers);
            foreach(KeyValuePair<string,string> keyValue in actualHeaders)
            {
                Assert.True(request.Headers.Contains(keyValue.Key));
                Assert.Equal(keyValue.Value, request.Headers.Where(x => x.Key == keyValue.Key).FirstOrDefault().Value.First());
            }
        }
    }
}
