using System;

namespace Server
{
    public class OfficeFilter : Filter
    {
        public int[] Ids { get; set; }
        public string[] Cities { get; set; }

        // Currency
        public int[] CurrencyIds { get; set; }
        public string[] CurrencyNames { get; set; }
        public Nullable<double> CurrencyExchangeRateRelativeToDollarMin { get; set; }
        public Nullable<double> CurrencyExchangeRateRelativeToDollarMax { get; set; }
    }
}