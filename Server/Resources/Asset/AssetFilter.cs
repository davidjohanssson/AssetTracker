using System;

namespace Server
{
    public class AssetFilter : Filter
    {
        public int[] Ids { get; set; }
        public DateTime PurchaseDateMin { get; set; }
        public DateTime PurchaseDateMax { get; set; }
        
        // Product
        public int[] ProductIds { get; set; }
        public string[] ProductNames { get; set; }
        public Nullable<double> ProductPriceMin { get; set; }
        public Nullable<double> ProductPriceMax { get; set; }

        // Brand
        public int[] BrandIds { get; set; }
        public string[] BrandNames { get; set; }

        // FormFactor
        public int[] FormFactorIds { get; set; }
        public string[] FormFactorNames { get; set; }

        // Office
        public int[] OfficeIds { get; set; }
        public string[] OfficeCities { get; set; }
    }
}