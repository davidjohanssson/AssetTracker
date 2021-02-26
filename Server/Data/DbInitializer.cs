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
            SeedProducts(context);
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
                    Name = "American dollar", 
                    Code = "USD",
                    ExchangeRateRelativeToDollar = 1.00
                },
                new Currency {
                    Name = "Brazilian real",
                    Code = "BRL",
                    ExchangeRateRelativeToDollar = 5.53
                },
                new Currency {
                    Name = "Canadian dollar",
                    Code = "CAD",
                    ExchangeRateRelativeToDollar = 1.26
                },
                new Currency {
                    Name = "Danish krone",
                    Code = "DKK",
                    ExchangeRateRelativeToDollar = 6.12
                },
                new Currency {
                    Name = "Euro",
                    Code = "EUR",
                    ExchangeRateRelativeToDollar = 0.82
                },
                new Currency {
                    Name = "Indian rupee",
                    Code = "INR",
                    ExchangeRateRelativeToDollar = 73.08
                },
                new Currency {
                    Name = "Japanese yen",
                    Code = "JPY",
                    ExchangeRateRelativeToDollar = 106.12
                },
                new Currency {
                    Name = "Mexican peso",
                    Code = "MXN",
                    ExchangeRateRelativeToDollar = 20.82
                },
                new Currency {
                    Name = "Polish zloty",
                    Code = "PLN",
                    ExchangeRateRelativeToDollar = 3.71
                },
                new Currency {
                    Name = "Pound sterling",
                    Code = "GBP",
                    ExchangeRateRelativeToDollar = 0.72
                },
                new Currency {
                    Name = "Russian ruble",
                    Code = "RUB",
                    ExchangeRateRelativeToDollar = 74.25
                },
                new Currency {
                    Name = "Swedish crown", 
                    Code = "SEK",
                    ExchangeRateRelativeToDollar = 8.29
                },
                new Currency {
                    Name = "Swiss franc",
                    Code = "CHF",
                    ExchangeRateRelativeToDollar = 0.90
                },
                new Currency {
                    Name = "Thai baht",
                    Code = "THB",
                    ExchangeRateRelativeToDollar = 30.27
                }
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
                    Currency = context.Currencies.Where(currency => currency.Code == "EUR").FirstOrDefault(),
                },
                new Office {
                    City = "Madrid",
                    Currency = context.Currencies.Where(currency => currency.Code == "EUR").FirstOrDefault(),
                },
                new Office { 
                    City = "Stockholm",
                    Currency = context.Currencies.Where(currency => currency.Code == "SEK").FirstOrDefault(),
                },
                new Office {
                    City = "Boston",
                    Currency = context.Currencies.Where(currency => currency.Code == "USD").FirstOrDefault(),
                },
                new Office {
                    City = "New York",
                    Currency = context.Currencies.Where(currency => currency.Code == "USD").FirstOrDefault(),
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

        private static void SeedProducts(AssetTrackerContext context)
        {
            if (context.Products.Any())
            {
                return;
            }

            var products = new Product[]
            {
                new Product {
                    Name = "iPhone X",
                    Price = 630.00,
                    Brand = context.Brands.Where(brand => brand.Name == "Apple").FirstOrDefault(),
                    FormFactor = context.FormFactors.Where(formFactor => formFactor.Name == "Phone").FirstOrDefault(),
                },
                new Product {
                    Name = "iPad",
                    Price = 1100.50,
                    Brand = context.Brands.Where(brand => brand.Name == "Apple").FirstOrDefault(),
                    FormFactor = context.FormFactors.Where(formFactor => formFactor.Name == "Tablet").FirstOrDefault(),
                },
                new Product {
                    Name = "ProDesk 600",
                    Price = 310.00,
                    Brand = context.Brands.Where(brand => brand.Name == "HP").FirstOrDefault(),
                    FormFactor = context.FormFactors.Where(formFactor => formFactor.Name == "Desktop").FirstOrDefault(),
                },
                new Product {
                    Name = "IdeaPad 330",
                    Price = 550.99,
                    Brand = context.Brands.Where(brand => brand.Name == "Lenovo").FirstOrDefault(),
                    FormFactor = context.FormFactors.Where(formFactor => formFactor.Name == "Laptop").FirstOrDefault(),
                },
                new Product {
                    Name = "Galaxy S10e",
                    Price = 620.50,
                    Brand = context.Brands.Where(brand => brand.Name == "Samsung").FirstOrDefault(),
                    FormFactor = context.FormFactors.Where(formFactor => formFactor.Name == "Phone").FirstOrDefault(),
                }
            };

            foreach (var product in products)
            {
                context.Products.Add(product);
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
                    Product = context.Products.Where(product => product.Name == "iPhone X").FirstOrDefault(),
                    Office = context.Offices.Where(office => office.City == "Berlin").FirstOrDefault(),
                },
                new Asset {
                    PurchaseDate = new DateTime(2018, 3, 12),
                    Product = context.Products.Where(product => product.Name == "iPad").FirstOrDefault(),
                    Office = context.Offices.Where(office => office.City == "Madrid").FirstOrDefault(),
                },
                new Asset {
                    PurchaseDate = new DateTime(2019, 9, 26),
                    Product = context.Products.Where(product => product.Name == "IdeaPad 330").FirstOrDefault(),
                    Office = context.Offices.Where(office => office.City == "Madrid").FirstOrDefault(),
                },
                new Asset {
                    PurchaseDate = new DateTime(2017, 2, 17),
                    Product = context.Products.Where(product => product.Name == "ProDesk 600").FirstOrDefault(),
                    Office = context.Offices.Where(office => office.City == "Stockholm").FirstOrDefault(),
                },
                new Asset {
                    PurchaseDate = new DateTime(2018, 6, 2),
                    Product = context.Products.Where(product => product.Name == "Galaxy S10e").FirstOrDefault(),
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