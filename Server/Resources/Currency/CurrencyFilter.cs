using System;

namespace Server
{
    public class CurrencyFilter : Filter
    {
        public int[] Ids { get; set; }
        public string Name { get; set; }
        public string[] Codes { get; set; }
        public Nullable<double> ExchangeRateRelativeToDollarMin { get; set; }
        public Nullable<double> ExchangeRateRelativeToDollarMax { get; set; }
    }
}