using System;

namespace Server
{
    public class CurrencySearch
    {
        public int[] Ids { get; set; }
        public string[] Names { get; set; }
        public Nullable<double> ExchangeRateRelativeToDollarMin { get; set; }
        public Nullable<double> ExchangeRateRelativeToDollarMax { get; set; }
        public string OrderByAsc { get; set; }
        public string OrderByDesc { get; set; }
        public Nullable<int> Skip { get; set; }
    }
}