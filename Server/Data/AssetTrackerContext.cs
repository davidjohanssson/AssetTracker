using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Server
{
    public class AssetTrackerContext : DbContext
    {
        public AssetTrackerContext(DbContextOptions<AssetTrackerContext> options) : base(options)
        {
        }

        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<FormFactor> FormFactors { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Asset> Assets { get; set; }
    }

    public class Currency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double ExchangeRateRelativeToDollar { get; set; }

        [JsonIgnore]
        public List<Office> Offices { get; set; } = new List<Office>();
    }

    public class Office
    {
        public int Id { get; set; }
        public string City { get; set; }

        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }

        [JsonIgnore]
        public List<Asset> Assets { get; set; } = new List<Asset>();
    }

    public class FormFactor
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public List<Model> Models { get; set; } = new List<Model>();
    }

    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public List<Model> Models { get; set; } = new List<Model>();
    }

    public class Model
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public int FormFactorId { get; set; }
        public FormFactor FormFactor { get; set; }

        [JsonIgnore]
        public List<Asset> Assets { get; set; } = new List<Asset>();
    }

    public class Asset
    {
        public int Id { get; set; }
        public DateTime PurchaseDate { get; set; }

        public int ModelId { get; set; }
        public Model Model { get; set; }

        public int OfficeId { get; set; }
        public Office Office { get; set; }
    }
}