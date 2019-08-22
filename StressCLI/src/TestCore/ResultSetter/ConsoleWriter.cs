using ConsoleTables;
using System.Linq;
using System.Net;

namespace StressCLI.src.TestCore.ResultSetter
{
    internal class ConsoleWriter : AbstractResultSetter
    {
        private void PrintTotalResult()
        {
            ConsoleTable TotalResultTable = new ConsoleTable("completed_by", "stopped_by", "min_req_time", "max_req_time", "total_requests");
            TotalResultTable.AddRow(Result.EndedAt - Result.StartedAt,
                Result.StopReason,
                Result.CompletedRequests.Min(x => x.CompletedBy),
                Result.CompletedRequests.Max(x => x.CompletedBy),
                Result.CompletedRequests.Length);
            TotalResultTable.Write();
        }

        private void PrintCodesResultTable()
        {
            var grouping = Result.CompletedRequests.GroupBy(x => x.ResponseCode);
            string[] codes = grouping.Select(x => x.Key.ToString()).ToArray();
            ConsoleTable CodesResultTable = new ConsoleTable(codes);
            string[] count = grouping.Select(x => x.Count().ToString()).ToArray();
            CodesResultTable.AddRow(count);
            CodesResultTable.Write();
        }

        public override void Write()
        {
            PrintTotalResult();
            PrintCodesResultTable();
        }

    }
}
