using System;

namespace Server
{
    public class Filter
    {
        public string OrderByAsc { get; set; }
        public string OrderByDesc { get; set; }
        public Nullable<int> Skip { get; set; }
        public Nullable<long> Take { get; set; }
    }
}