namespace StressCLI.src.Helpers
{
    internal class CheckType
    {
        public static bool IsInt(object data)
        {
            int temp;
            return int.TryParse(data.ToString(), out temp);
        }
    }
}
