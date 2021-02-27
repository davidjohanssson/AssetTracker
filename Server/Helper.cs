namespace Server
{
    public static class Helper
    {
        public static string CapitalizeFirstLetter(string str)
        {
            if (str.Length == 1)
            {
                return char.ToUpper(str[0]).ToString();
            }

            return char.ToUpper(str[0]) + str.Substring(1);
        }
    }
}