using System.Collections.Generic;

namespace server
{
    public class Currency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double ExchangeRateRelativeToDollar { get; set; }
        public List<Office> Offices { get; } = new List<Office>();
    }
}