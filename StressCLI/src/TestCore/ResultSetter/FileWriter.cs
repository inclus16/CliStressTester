using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace StressCLI.src.TestCore.ResultSetter
{
    internal class FileWriter : AbstractResultSetter
    {

        private readonly string ArchivePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Archive");

        public FileWriter()
        {

            if (!Directory.Exists(ArchivePath))
            {
                Directory.CreateDirectory(ArchivePath);
            }

        }


        public override void Write()
        {
            string resultsFilePath = Path.Combine(ArchivePath, Result.StartedAt.ToString().Replace(' ', '_').Replace(':', '-') + ".json");
            File.WriteAllText(resultsFilePath, JsonConvert.SerializeObject(Result));
            WriteResponsesCsv();
        }

        private void WriteResponsesCsv()
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
