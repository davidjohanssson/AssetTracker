using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Server
{
    public class AssetTrackerContext : DbContext
    {
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<FormFactor> FormFactors { get; set; }
        public DbSet<Asset> Assets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(@"Server=.\SQLEXPRESS;Database=AssetTracker;Trusted_Connection=True;");
    }

    public class Currency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double ExchangeRateRelativeToDollar { get; set; }
        public List<Office> Offices { get; } = new List<Office>();
    }

    public class Office
    {
        public int Id { get; set; }
        public string City { get; set; }
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public List<Asset> Assets { get; } = new List<Asset>();
    }

    public class FormFactor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Asset> Assets { get; } = new List<Asset>();
    }

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