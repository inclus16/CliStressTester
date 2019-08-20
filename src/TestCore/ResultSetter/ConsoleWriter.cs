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
        public void PrintTotalResult()
        {
            ConsoleTable TotalResultTable = new ConsoleTable("completed_by", "stopped_by", "min_req_time", "max_req_time", "total_requests");
            TotalResultTable.AddRow(Result.EndedAt - Result.StartedAt,
                Result.StopReason,
                Result.CompletedRequests.Min(x => x.CompletedBy),
                Result.CompletedRequests.Max(x => x.CompletedBy),
                Result.CompletedRequests.Length);
            TotalResultTable.Write();
        }

        public void PrintCodesResultTable()
        {
            var grouping = Result.CompletedRequests.GroupBy(x => x.ResponseCode);
            HttpStatusCode[] codes = grouping.Select(x=>x.Key).ToArray();
            ConsoleTable CodesResultTable = new ConsoleTable(string.Join(",", codes));
            int[] count = grouping.Select(x => x.Count()).ToArray();
            CodesResultTable.AddRow(count);
            CodesResultTable.Write();
        }

    }
}
