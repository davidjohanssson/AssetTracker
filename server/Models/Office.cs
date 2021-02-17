using System.Collections.Generic;

namespace server
{
    public class Office
    {
        public int Id { get; set; }
        public string City { get; set; }
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public List<Asset> Assets { get; } = new List<Asset>();
    }
}