using System;

namespace Server
{
    public class ProductFilter : Filter
    {
        public int[] Ids { get; set; }
        public string[] Names { get; set; }
        public Nullable<double> PriceMin { get; set; }
        public Nullable<double> PriceMax { get; set; }

        // Brand
        public int[] BrandIds { get; set; }
        public string[] BrandNames { get; set; }

        // FormFactor
        public int[] FormFactorIds { get; set; }
        public string[] FormFactorNames { get; set; }
    }
}