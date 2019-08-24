using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StressCLI.src.Helpers
{
    public class PathResolver
    {
        public static string GetAbsolutePath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Archive");
        }

        public static string GetFileNameByDate(DateTime dateTime)
        {
            return dateTime.ToString().Replace(' ', '_').Replace(':', '-');
        }
    }
}
