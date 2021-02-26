using System;

namespace Server
{
    public class AssetFilter : Filter
    {
        public int[] Ids { get; set; }
        public Nullable<DateTime> PurchaseDateMin { get; set; }
        public Nullable<DateTime> PurchaseDateMax { get; set; }
        public ProductFilter ProductFilter { get; set; }
        public OfficeFilter OfficeFilter { get; set; }
    }
}