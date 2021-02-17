using System;

namespace server
{
    public class Asset
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public double PurchasePrice { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int OfficeId { get; set; }
        public Office Office { get; set; }
        public int FormFactorId { get; set; }
        public FormFactor FormFactor { get; set; }
    }
}