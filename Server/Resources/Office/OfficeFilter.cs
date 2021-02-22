using System;

namespace Server
{
    public class OfficeFilter
    {
        public int[] Ids { get; set; }
        public string[] Cities { get; set; }
        public int[] CurrencyIds { get; set; }

        public string OrderByAsc { get; set; }
        public string OrderByDesc { get; set; }
        public Nullable<int> Skip { get; set; }
    }
}