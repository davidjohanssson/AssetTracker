using System;
using System.Linq;

namespace Server
{
    public static class DbInitializer
    {
        public static void Initialize(AssetTrackerContext context)
        {
            context.Database.EnsureCreated();
            SeedCurrencies(context);
            SeedOffices(context);
            SeedFormFactors(context);
            SeedBrands(context);
            SeedModels(context);
            SeedAssets(context);
        }

        private static void SeedCurrencies(AssetTrackerContext context)
        {
            if (context.Currencies.Any())
            {
                return;
            }

            var currencies = new Currency[]
            {
                new Currency { 
                    Name = "EUR",
                    ExchangeRateRelativeToDollar = 0.82 
                },
                new Currency { 
                    Name = "SEK",
                    ExchangeRateRelativeToDollar = 8.29
                },
                new Currency { 
                    Name = "USD",
                    ExchangeRateRelativeToDollar = 1.00
                },
            };

            foreach (var currency in currencies)
            {
                context.Currencies.Add(currency);
            }

            context.SaveChanges();
        }

        private static void SeedOffices(AssetTrackerContext context)
        {
            if (context.Offices.Any())
            {
                return;
            }

            var offices = new Office[]
            {
                new Office {
                    City = "Berlin",
                    Currency = context.Currencies.Where(currency => currency.Name == "EUR").FirstOrDefault(),
                },
                new Office {
                    City = "Madrid",
                    Currency = context.Currencies.Where(currency => currency.Name == "EUR").FirstOrDefault(),
                },
                new Office { 
                    City = "Stockholm",
                    Currency = context.Currencies.Where(currency => currency.Name == "SEK").FirstOrDefault(),
                },
                new Office {
                    City = "Boston",
                    Currency = context.Currencies.Where(currency => currency.Name == "USD").FirstOrDefault(),
                },
                new Office {
                    City = "New York",
                    Currency = context.Currencies.Where(currency => currency.Name == "USD").FirstOrDefault(),
                },
            };

            foreach (var office in offices)
            {
                context.Offices.Add(office);
            }

            context.SaveChanges();
        }

        private static void SeedFormFactors(AssetTrackerContext context)
        {
            if (context.FormFactors.Any())
            {
                return;
            }

            var formFactors = new FormFactor[]
            {
                new FormFactor {
                    Name = "Desktop",
                },
                new FormFactor {
                    Name = "Laptop",
                },
                new FormFactor {
                    Name = "Phone",
                },
                new FormFactor {
                    Name = "Tablet",
                }
            };

            foreach (var formFactor in formFactors)
            {
                context.FormFactors.Add(formFactor);
            }

            context.SaveChanges();
        }

        private static void SeedBrands(AssetTrackerContext context)
        {
            if (context.Brands.Any())
            {
                return;
            }

            var brands = new Brand[]
            {
                new Brand {
                    Name = "Apple",
                },
                new Brand {
                    Name = "HP",
                },
                new Brand {
                    Name = "Lenovo",
                },
                new Brand {
                    Name = "Samsung",
                }
            };

            foreach (var brand in brands)
            {
                context.Brands.Add(brand);
            }

            context.SaveChanges();
        }

        private static void SeedModels(AssetTrackerContext context)
        {
            if (context.Models.Any())
            {
                return;
            }

            var models = new Model[]
            {
                new Model {
                    Name = "iPhone X",
                    Price = 630.00,
                    Brand = context.Brands.Where(brand => brand.Name == "Apple").FirstOrDefault(),
                    FormFactor = context.FormFactors.Where(formFactor => formFactor.Name == "Phone").FirstOrDefault(),
                },
                new Model {
                    Name = "iPad",
                    Price = 1100.50,
                    Brand = context.Brands.Where(brand => brand.Name == "Apple").FirstOrDefault(),
                    FormFactor = context.FormFactors.Where(formFactor => formFactor.Name == "Tablet").FirstOrDefault(),
                },
                new Model {
                    Name = "ProDesk 600",
                    Price = 310.00,
                    Brand = context.Brands.Where(brand => brand.Name == "HP").FirstOrDefault(),
                    FormFactor = context.FormFactors.Where(formFactor => formFactor.Name == "Desktop").FirstOrDefault(),
                },
                new Model {
                    Name = "IdeaPad 330",
                    Price = 550.99,
                    Brand = context.Brands.Where(brand => brand.Name == "Lenovo").FirstOrDefault(),
                    FormFactor = context.FormFactors.Where(formFactor => formFactor.Name == "Laptop").FirstOrDefault(),
                },
                new Model {
                    Name = "Galaxy S10e",
                    Price = 620.50,
                    Brand = context.Brands.Where(brand => brand.Name == "Samsung").FirstOrDefault(),
                    FormFactor = context.FormFactors.Where(formFactor => formFactor.Name == "Phone").FirstOrDefault(),
                }
            };

            foreach (var model in models)
            {
                context.Models.Add(model);
            }

            context.SaveChanges();
        }

        private static void SeedAssets(AssetTrackerContext context)
        {
            if (context.Assets.Any())
            {
                return;
            }

            var assets = new Asset[]
            {
                new Asset {
                    PurchaseDate = new DateTime(2017, 5, 11),
                    Model = context.Models.Where(model => model.Name == "iPhone X").FirstOrDefault(),
                    Office = context.Offices.Where(office => office.City == "Berlin").FirstOrDefault(),
                },
                new Asset {
                    PurchaseDate = new DateTime(2018, 3, 12),
                    Model = context.Models.Where(model => model.Name == "iPad").FirstOrDefault(),
                    Office = context.Offices.Where(office => office.City == "Madrid").FirstOrDefault(),
                },
                new Asset {
                    PurchaseDate = new DateTime(2019, 9, 26),
                    Model = context.Models.Where(model => model.Name == "IdeaPad 330").FirstOrDefault(),
                    Office = context.Offices.Where(office => office.City == "Madrid").FirstOrDefault(),
                },
                new Asset {
                    PurchaseDate = new DateTime(2017, 2, 17),
                    Model = context.Models.Where(model => model.Name == "ProDesk 600").FirstOrDefault(),
                    Office = context.Offices.Where(office => office.City == "Stockholm").FirstOrDefault(),
                },
                new Asset {
                    PurchaseDate = new DateTime(2018, 6, 2),
                    Model = context.Models.Where(model => model.Name == "Galaxy S10e").FirstOrDefault(),
                    Office = context.Offices.Where(office => office.City == "Boston").FirstOrDefault(),
                }
            };

            foreach (var asset in assets)
            {
                context.Assets.Add(asset);
            }

            context.SaveChanges();
        }
    }
}