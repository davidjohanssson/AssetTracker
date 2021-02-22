using System;

namespace Server
{
    public class AssetFilter : Filter
    {
        public int[] Ids { get; set; }
        public DateTime PurchaseDateMin { get; set; }
        public DateTime PurchaseDateMax { get; set; }
        
    }
}