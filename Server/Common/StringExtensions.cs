namespace Server
{
    public static class StringExtensions
    {
        public static string ToPascalCase(this string input)
        {
            if (input.Length == 1)
            {
                return char.ToUpper(input[0]).ToString();
            }

            return char.ToUpper(input[0]) + input.Substring(1);
        }
    }
}