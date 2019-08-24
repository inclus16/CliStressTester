using CommandDotNet;
using CommandDotNet.Attributes;
using Newtonsoft.Json;
using StressCLI.src.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;

namespace StressCLI.src.Cli.Commands.Dto
{
    class RunTestDto : IArgumentModel
    {

        [Option(Description = "Request body in json")]
        [Validation.FileExists]
        [FileExtensions(Extensions = ".json")]
        public string RequestPath { get; set; }


        [Option(Description = "Request headers in json")]
        [Validation.FileExists]
        [FileExtensions(Extensions = ".json")]
        public string RequestHeadersPath { get; set; }

        [Required]
        [Url]
        public string Uri { get; set; }

        [Required]
        [EnumDataType(typeof(HttpTestMethod))]
        public HttpTestMethod Method { get; set; }

        [Required]
        [EnumDataType(typeof(RequestFormat))]
        public RequestFormat RequestFormat { get; set; }

        [Required]
        [EnumDataType(typeof(StopSignal))]
        public StopSignal StopSignal { get; set; }

        [Required]
        [Range(1,50)]
        public  ushort Parallel { get; set; }

        [Option(Description = "Where write results")]
        [Required]
        [EnumDataType(typeof(ResultWriter))]
        public ResultWriter ResultWriter { get; set; }

        public TestConfig GetTestConfig()
        {
            TestConfig config = new TestConfig();
            config.Data = string.IsNullOrWhiteSpace(RequestPath)?null:File.ReadAllText(RequestPath);
            config.Headers = string.IsNullOrWhiteSpace(RequestHeadersPath)?null :File.ReadAllText(RequestHeadersPath);
            config.Method = Method;
            config.RequestFormat = RequestFormat;
            config.Url = new Uri(Uri);
            config.StopSignal = StopSignal;
            config.TimeOut = TimeSpan.FromSeconds(60);//@TODO
            config.ResultWriter = ResultWriter;
            config.Parallel = Parallel;
            return config;
        }

    }
}
