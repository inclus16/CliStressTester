using System;
using System.Collections.Generic;
using System.Text;
using StressCLI.src.TestCore.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using StressCLI.src.Entities;
using StressCLI.Tests.Faker;
using Xunit;
using System.Threading.Tasks;
using StressCLI.src.TestCore;

namespace StressCLI.Tests
{
    public class ExecutorTests
    {
        private readonly IExecutor Executor;

        private TestConfig TestConfig;

        private readonly Config Config;

        public ExecutorTests()
        {
            Executor = Program.BuildServiceProvider().GetService<IExecutor>();
            Config = new Config();
            TestConfig = Config.GetTestConfig();
        }

        [Fact]
        public void TestExecutorUnreachable()
        {
            Executor.SetConfig(TestConfig);
            Executor.Configurate();
            Task.Run(Executor.StartTest);
            Task.Delay(5000).Wait();
            Executor.StopExecution();
            List<RequestTask> completed = Executor.GetResult();
            Assert.Empty(completed);
        }

        [Fact]
        public void IsStopOnSignal()
        {
            StopSignal signal = StopSignal.TooManyRequests;
            using (TestServer testServer = new TestServer())
            {
                TestConfig testConfig = TestConfig;
                testConfig.Url = new Uri(testServer.Address);
                testConfig.StopSignal = signal;
                Executor.SetConfig(testConfig);
                Executor.Configurate();
                Task.Run(() => testServer.Start(signal));
                Executor.StartTest();
                Task.Delay(5000).Wait();
            }
            int count = Executor.GetResult().Count;
            Assert.True(count < 5&& count>0);
        }

        [Fact]
        public void IsRunningWhileStopSignal()
        {
            StopSignal signal = StopSignal.InternalServerError;
            using (TestServer testServer = new TestServer())
            {
                TestConfig testConfig = TestConfig;
                testConfig.Url = new Uri(testServer.Address);
                testConfig.StopSignal = signal;
                Executor.SetConfig(testConfig);
                Executor.Configurate();
                Task.Run(()=>testServer.Start(StopSignal.Manual));
                Task.Run(Executor.StartTest);
                Task.Delay(5000).Wait();
                Executor.StopExecution();
            }
            int count = Executor.GetResult().Count;
            Assert.True(count > 5);
        }
    }
}
