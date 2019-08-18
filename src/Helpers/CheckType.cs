using System;
using System.Collections.Generic;
using System.Text;

namespace StressCLI.src.Helpers
{
    class CheckType
    {
        public static bool IsInt(object data)
        {
            int temp;
            return int.TryParse(data.ToString(), out temp);
        }
    }
}
