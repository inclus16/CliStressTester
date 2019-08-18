using StressCLI.src.Cli;
using StressCLI.src.Cli.Commands.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StressCLI.src.TestCore
{
    class Executor
    {
        private readonly ConfigParser ConfigParser;

        private readonly CancellationTokenSource CancellationTokenSource;

        private BlockingCollection<RequestTask> Tasks;

        private bool IsComplete;

        private int RuningTasks;

        private readonly HttpClient Client;

        public Executor()
        {
            ConfigParser = new ConfigParser();
            CancellationTokenSource = new CancellationTokenSource();
            Tasks = new BlockingCollection<RequestTask>();
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
            for(int i = 0; i < ConfigParser.GetParralel(); i++)
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
            RequestTask task = Tasks.Last(x => x.Response.IsCompletedSuccessfully);
            task.EndedAt = DateTime.Now.TimeOfDay;
            Task.Run(() =>
            {
                CliNotifier.PrintInfo($"Request finished by :{task.TotalExecutionTime} with status code: {task.Response.Result.StatusCode}");
            });
            if (CancellationTokenSource.IsCancellationRequested)
            {
                return;
            }
            RuningTasks--;
            if (IsStopSignal(task.Response.Result))
            {
                StopExecution();
            }
            if (RuningTasks < ConfigParser.GetParralel())
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
                    if(response.StatusCode== System.Net.HttpStatusCode.BadRequest)
                    {
                        return true;
                    }
                    break;
                case StopSignal.InternalServerError:
                    if(response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    {
                        return true;
                    }
                    break;
            }
            return false;
        }

        public void StopExecution()
        {
            CancellationTokenSource.Cancel();
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
