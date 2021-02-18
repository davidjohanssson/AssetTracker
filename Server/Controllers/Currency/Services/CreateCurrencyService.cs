using System;
using Microsoft.AspNetCore.Mvc;

namespace Server
{
    public interface ICreateCurrencyService
    {
        IActionResult Run(CreateCurrencyDto dto);
    }

    public class CreateCurrencyService : ICreateCurrencyService
    {
        private readonly AssetTrackerContext _context;

        public CreateCurrencyService(AssetTrackerContext context)
        {
            _context = context;
        }

        public IActionResult Run(CreateCurrencyDto dto)
        {
            if (dto.Name == null)
            {
                return new BadRequestObjectResult("Name must not be null");
            }

            if (dto.Name.Length < 3)
            {
                return new BadRequestObjectResult("Name must be 3 characters");
            }

            if (dto.Name.Length > 3)
            {
                return new BadRequestObjectResult("Name must be 3 characters");
            }

            if (double.IsNegative(dto.ExchangeRateRelativeToDollar))
            {
                return new BadRequestObjectResult("ExchangeRateRelativeToDollar must be positive");
            }

            if (Math.Round(dto.ExchangeRateRelativeToDollar, 2) != dto.ExchangeRateRelativeToDollar)
            {
                return new BadRequestObjectResult("ExchangeRateRelativeToDollar must be two decimals");
            }

            var currency = new Currency();
            currency.Name = dto.Name;
            currency.ExchangeRateRelativeToDollar = dto.ExchangeRateRelativeToDollar;

            _context.Currencies.Add(currency);
            _context.SaveChanges();

            return new OkObjectResult(currency);
        }
    }
}