using CommandDotNet;
using Newtonsoft.Json;
using StressCLI.src.Cli.Commands.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;

namespace StressCLI.src.Cli.Commands.Dto
{
    class RunTestDto : IArgumentModel
    {
        [Required]
        [Validation.FileExists]
        [FileExtensions(Extensions = ".json")]
        public string RequestPath { get; set; }

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

        public TestConfig GetTestConfig()
        {
            TestConfig config = new TestConfig();
            config.Data = JsonConvert.DeserializeObject(File.ReadAllText(RequestPath));
            config.Method = Method;
            config.RequestFormat = RequestFormat;
            config.StopSignal = StopSignal;
            config.TimeOut = TimeSpan.FromSeconds(60);//@TODO
            config.Parallel = Parallel;
            return config;
        }

    }
}
