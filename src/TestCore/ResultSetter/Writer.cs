using Newtonsoft.Json;
using StressCLI.src.Cli.Commands.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace StressCLI.src.TestCore.ResultSetter
{
    class Writer
    {

        private ResultStruct Result;

        private readonly string ArchivePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Archive");

        public Writer()
        {
            Result = new ResultStruct();
            if (!Directory.Exists(ArchivePath))
            {
                Directory.CreateDirectory(ArchivePath);
            }
            
        }
        public Writer SetCompletedTasks(IEnumerable<RequestTask> tasks)
        {
            Result.CompletedRequests = tasks.Select(x => new CompletedRequest(x)).ToArray();
            return this;
        }


        public Writer SetStartedAtTime(DateTime startedAt)
        {
            Result.StartedAt = startedAt;
            return this;
        }

        public Writer SetEndedAtTime(DateTime endedAt)
        {
            Result.EndedAt = endedAt;
            return this;
        }

        public Writer SetStopReason(StopSignal? stopSignal)
        {
            if (stopSignal == null)
            {
                Result.StopReason = "By user";
            }
            else
            {
                Result.StopReason = stopSignal.Value.ToString();
            }
            
            return this;
        }

        public void Write()
        {
            string resultsFilePath = Path.Combine(ArchivePath, Result.StartedAt.ToString().Replace(' ','_').Replace(':','-') + ".json");
            File.WriteAllText(resultsFilePath, JsonConvert.SerializeObject(Result));
        }

        public void WriteResponsesCsv()
        {
            string resultsFilePath = Path.Combine(ArchivePath, Result.StartedAt.ToString().Replace(' ', '_').Replace(':', '-') + ".csv");
            int responsesCount = Result.CompletedRequests.Length;
            using (FileStream fs = File.OpenWrite(resultsFilePath))
            {
                byte[] header = Encoding.UTF8.GetBytes("StartedAt;ResponseTime\n");
                fs.Write(header, 0, header.Length);
                for (int i = 0; i < responsesCount; i++)
                {
                    TimeSpan completedBy = Result.CompletedRequests[i].CompletedBy;
                    if (completedBy < TimeSpan.Zero)
                    {
                        continue;
                    }
                    byte[] buffer = Encoding.UTF8.GetBytes($"{Result.CompletedRequests[i].StartedAt};{completedBy}\n");
                    fs.Write(buffer, 0, buffer.Length);
                }
            }
        }
    }
}
