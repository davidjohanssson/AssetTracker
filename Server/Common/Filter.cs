namespace Server
{
    public class Filter
    {
        public string OrderByAsc { get; set; }
        public string OrderByDesc { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}