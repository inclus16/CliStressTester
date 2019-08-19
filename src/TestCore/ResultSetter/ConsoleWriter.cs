using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace StressCLI.src.TestCore.ResultSetter
{
    class ConsoleWriter : AbstractResultSetter
    {
        private readonly ConsoleTable TotalResultTable;
        
        private ConsoleTable CodesResultTable;


        public ConsoleWriter()
        {
            TotalResultTable = new ConsoleTable("completed_by","stopped_by","min_req_time","max_req_time","total_requests");
        }

        public void PrintTotalResult()
        {
            TotalResultTable.AddRow(Result.EndedAt - Result.StartedAt,
                Result.StopReason,
                Result.CompletedRequests.Where(x=>x.CompletedBy>TimeSpan.Zero).Min(x => x.CompletedBy),
                Result.CompletedRequests.Max(x => x.CompletedBy),
                Result.CompletedRequests.Length);
            TotalResultTable.Write();
        }

        public void PrintCodesResultTable()
        {
            var grouping = Result.CompletedRequests.GroupBy(x => x.ResponseCode);
            HttpStatusCode[] codes = grouping.Select(x=>x.Key).ToArray();
            CodesResultTable = new ConsoleTable(string.Join(",", codes));
            int[] count = grouping.Select(x => x.Count()).ToArray();
            CodesResultTable.AddRow(string.Join(",", count));
            CodesResultTable.Write();
        }

    }
}
