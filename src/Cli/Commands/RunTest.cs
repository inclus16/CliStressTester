using StressCLI.src.Cli.Commands.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using StressCLI.src.Helpers;
using StressCLI.src.TestCore;

namespace StressCLI.src.Cli.Commands
{
    class RunTest : ICommand
    {
        private string[] Args;

        private string ValidationError;

        private TestConfig TestConfig;

        private readonly Executor Executor = new Executor();
        public void Execute()
        {
            Executor.SetConfig(TestConfig);
            Executor.StartTest();
        }

        public void PrepareData()
        {
            TestConfig = new TestConfig();
            TestConfig.Method = (HttpTestMethod)int.Parse(Args[3]);
            TestConfig.RequestFormat = (RequestFormat)int.Parse(Args[2]);
            TestConfig.StopSignal = (StopSignal)int.Parse(Args[6]);
            TestConfig.TimeOut = TimeSpan.FromSeconds(int.Parse(Args[5]));
            TestConfig.Data = File.ReadAllText(Args[1]);
            TestConfig.Url = new Uri(Args[4]);
            TestConfig.Parallel = int.Parse(Args[7]);

        }

        public string GetValidationError()
        {
            return ValidationError;
        }

        public void Validate()
        {
            if (Args.Length < 8)
            {
                ValidationError =
                    "Not anougth parameters. Available are: run-test 'request_data_file_path' 'request_format' 'request_method' 'uri' 'timeout' 'stop_signal' 'parallel'";
                return;
            }
            if (!File.Exists(Args[1]))
            {
                ValidationError = "request_data_file_path not found";
                return;
            }
            if (Path.GetExtension(Args[1]) != ".json")
            {
                ValidationError = "request_data_file_path must be in json format";
                return;
            }
            if (!CheckType.IsInt(Args[2]))
            {
                ValidationError = $"request_format must be present as int, got {Args[2]} as {Args[2].GetType()}";
                return;
            }
            if (!Enum.GetValues(typeof(RequestFormat)).Cast<int>().Contains(Convert.ToInt32(Args[2])))
            {
                ValidationError = $"Unknown request_format {Args[2]}, check --help";
                return;
            }
            if (!CheckType.IsInt(Args[3]))
            {
                ValidationError = $"request_method must be present as int, got {Args[3]} as {Args[3].GetType()}";
                return;
            }
            if (!Enum.GetValues(typeof(HttpTestMethod)).Cast<int>().Contains(Convert.ToInt32(Args[3])))
            {
                ValidationError = $"Unknown request_method {Args[3]}, check --help";
                return;
            }
            Uri tempUri;
            if (!Uri.TryCreate(Args[4], UriKind.Absolute, out tempUri))
            {
                ValidationError = $"Cannot parse {Args[4]} as absolute url";
                return;
            }
            int tempTimeout;
            if (!int.TryParse(Args[5], out tempTimeout))
            {
                ValidationError = $"Expect intenger in timeout, got {Args[5]} as {Args[5].GetType()}";
                return;
            }
            if (!CheckType.IsInt(Args[6]))
            {
                ValidationError = $"stop_signal must be present as int, got {Args[6]} as {Args[6].GetType()}";
                return;
            }
            if (!Enum.GetValues(typeof(StopSignal)).Cast<int>().Contains(Convert.ToInt32(Args[6])))
            {
                ValidationError = $"Unknown stop_signal {Args[6]}, check --help";
                return;
            }
            if (!CheckType.IsInt(Args[7]))
            {
                ValidationError = $"parallel must be present as int, got {Args[6]} as {Args[6].GetType()}";
                return;
            }

        }

        public bool IsValid()
        {
            return ValidationError==null;
        }

        public void SetData(string[] args)
        {
            Args = args;
        }

        public string GetName()
        {
            return "run-test";
        }

        public void Cancel()
        {
            CliNotifier.PrinWarning("Manualy stopping execution...");
            Executor.StopExecution();
        }
    }
}
