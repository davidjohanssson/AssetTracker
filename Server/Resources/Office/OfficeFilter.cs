namespace Server
{
    public class OfficeFilter : Filter
    {
        public int[] Ids { get; set; }
        public string[] Cities { get; set; }
        public int[] CurrencyIds { get; set; }
    }
}