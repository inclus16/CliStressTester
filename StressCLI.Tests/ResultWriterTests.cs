using StressCLI.src.Helpers;
using StressCLI.src.Entities;
using StressCLI.src.Entities.ResultSetter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StressCLI.Tests
{
    public class ResultWriterTests
    {
        private readonly List<RequestTask> Requests;
        public ResultWriterTests()
        {
            Requests = new List<RequestTask>();
            FillRequests();
        }

        private void FillRequests()
        {
            for(int i = 0; i < 20; i++)
            {
                Requests.Add(new RequestTask
                {
                    EndedAt = DateTime.Now.TimeOfDay,
                    StartedAt = DateTime.Now.TimeOfDay-TimeSpan.FromSeconds(10),
                    Response=new Task<HttpResponseMessage>(()=>
                    {
                        return new HttpResponseMessage
                        {
                            Content=new StringContent("fake"),
                            StatusCode=System.Net.HttpStatusCode.OK
                        };
                    })
                });
                Requests.Last().Response.Start();
            }
        }

        [Fact]
        public void TestFileWriter()
        {
            AbstractWriter abstractWriter = WritersFactory.GetWriter(src.Entities.ResultWriter.File);
            DateTime endedAt = DateTime.Now;
            DateTime staetedAt = endedAt - TimeSpan.FromSeconds(25);
            FillWriter(ref abstractWriter,staetedAt,endedAt);            
            string fileNameByDate = Path.Combine(PathResolver.GetAbsolutePath(),PathResolver.GetFileNameByDate(staetedAt));
            string jsonFile = fileNameByDate + ".json";
            string csvFile = fileNameByDate + ".csv";
            Assert.True(File.Exists(jsonFile));
            Assert.True(File.Exists(csvFile));
            File.Delete(jsonFile);
            File.Delete(csvFile);
        }

        [Fact]
        public void TestCsvFileWriter()
        {
            AbstractWriter abstractWriter = WritersFactory.GetWriter(src.Entities.ResultWriter.File);
            DateTime endedAt = DateTime.Now;
            DateTime staetedAt = endedAt - TimeSpan.FromSeconds(25);
            FillWriter(ref abstractWriter, staetedAt, endedAt);
            string fileNameByDate = Path.Combine(PathResolver.GetAbsolutePath(), PathResolver.GetFileNameByDate(staetedAt));
            string csvFile = fileNameByDate + ".csv";
            string jsonFile = fileNameByDate + ".json";
            string[] linesCsv= File.ReadLines(csvFile).ToArray();
            int linesCsvCount = linesCsv.Length;
            Assert.True(Requests.Count + 1 == linesCsvCount);            
            for(int i=0;i< linesCsvCount; i++)
            {
                if (i == 0)
                {
                    Assert.True(linesCsv[i] == "StartedAt;ResponseTime");
                }
                else
                {
                    RequestTask request = Requests[i - 1];
                    Assert.True($"{request.StartedAt};{request.TotalExecutionTime}" == linesCsv[i]);
                }
            }
            File.Delete(jsonFile);
            File.Delete(csvFile);
        }

        private void FillWriter(ref AbstractWriter writer,DateTime startedAt,DateTime endedAt)
        {
            
            writer.SetCompletedTasks(Requests).
                SetEndedAtTime(endedAt)
                .SetStartedAtTime(startedAt)
                .SetStopReason(src.Entities.StopSignal.Manual);
            writer.Write();
        }
    }
}
