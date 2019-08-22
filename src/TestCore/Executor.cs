using StressCLI.src.Cli;
using StressCLI.src.Cli.Commands.Entities;
using StressCLI.src.TestCore.Parser;
using StressCLI.src.TestCore.ResultSetter;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.Http;
using System.Threading;

namespace StressCLI.src.TestCore
{
    internal class Executor
    {
        private readonly ConfigParser ConfigParser;

        private readonly CancellationTokenSource CancellationTokenSource;

        private readonly BlockingCollection<RequestTask> Tasks;

        private readonly BlockingCollection<RequestTask> Completed;

        private bool IsComplete;

        private int RuningTasks;

        private readonly HttpClient Client;

        private DateTime StartedAt;

        public Executor()
        {
            ConfigParser = new ConfigParser();
            CancellationTokenSource = new CancellationTokenSource();
            Tasks = new BlockingCollection<RequestTask>();
            Completed = new BlockingCollection<RequestTask>();
            Client = new HttpClient();
            IsComplete = false;
        }

        public void SetConfig(TestConfig config)
        {
            ConfigParser.SetConfig(config);
        }

        public void Configurate()
        {
            Client.Timeout = ConfigParser.GetTimeOut();
        }

        public void StartTest()
        {
            StartedAt = DateTime.Now;
            for (int i = 0; i < ConfigParser.GetParallel(); i++)
            {
                AddTask();
            }
            while (!IsComplete)
            {
                continue;
            }
        }

        private void OnRequestComplete()
        {
            if (CancellationTokenSource.IsCancellationRequested)
            {
                return;
            }
            RequestTask task = Tasks.Last(x => x.Response.IsCompleted);            
            if (task.Response.IsCanceled)
            {
                CliNotifier.PrinWarning("Cannot load response");
            }
            else
            {
                CliNotifier.PrintInfo($"Request finished by: {task.TotalExecutionTime} with status code: { task.Response.Result.StatusCode}");
                task.EndedAt = DateTime.Now.TimeOfDay;
                Completed.Add(task);
                if (IsStopSignal(task.Response.Result))
                {
                    StopExecution();
                }
            }
            RuningTasks--;           
            if (RuningTasks < ConfigParser.GetParallel())
            {
                AddTask();
            }
        }

        private bool IsStopSignal(HttpResponseMessage response)
        {
            StopSignal stopSignal = ConfigParser.GetStopSignal();
            switch (stopSignal)
            {
                case StopSignal.BadGateway:
                    if (response.StatusCode == System.Net.HttpStatusCode.BadGateway)
                    {
                        return true;
                    }
                    break;
                case StopSignal.BadRequest:
                    if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        return true;
                    }
                    break;
                case StopSignal.InternalServerError:
                    if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    {
                        return true;
                    }
                    break;
                case StopSignal.TooManyRequests:
                    if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                    {
                        return true;
                    }
                    break;
            }
            return false;
        }

        public void StopExecution(bool manual = false)
        {
            CancellationTokenSource.Cancel();
            if (Completed.Count==0)
            {
                return;
            }
            while (Tasks.Count > 0)
            {
                Tasks.Take();
            }
            WriteResults(manual);
            IsComplete = true;
        }

        

        private void WriteResults(bool manual)
        {
            AbstractResultSetter writer = WritersFactory.GetWriter(ConfigParser.GetResultWriterType());
            writer.SetCompletedTasks(Completed)
                .SetStartedAtTime(StartedAt)
                .SetEndedAtTime(DateTime.Now)
                .SetStopReason(manual ? StopSignal.Manual : ConfigParser.GetStopSignal());
            writer.Write();
        }

        private void AddTask()
        {
            RequestTask requestTask = new RequestTask();
            requestTask.StartedAt = DateTime.Now.TimeOfDay;
            requestTask.Response = Client.SendAsync(ConfigParser.GetRequest(), CancellationTokenSource.Token);
            requestTask.Response.GetAwaiter().OnCompleted(OnRequestComplete);
            Tasks.Add(requestTask);
            RuningTasks++;
        }
    }
}
