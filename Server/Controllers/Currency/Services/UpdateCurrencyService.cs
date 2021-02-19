using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Server
{
    public interface IUpdateCurrencyService
    {
        IActionResult Run(int id, UpdateCurrencyDto dto);
    }

    public class UpdateCurrencyService : IUpdateCurrencyService
    {
        private readonly AssetTrackerContext _context;

        public UpdateCurrencyService(AssetTrackerContext context)
        {
            _context = context;
        }

        public IActionResult Run(int id, UpdateCurrencyDto dto)
        {
            var currency = _context.Currencies.FirstOrDefault(currency => currency.Id == id);

            if (currency == null)
            {
                return new NotFoundObjectResult($"Currency with id {id} not found");
            }

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

            currency.Name = dto.Name;
            currency.ExchangeRateRelativeToDollar = dto.ExchangeRateRelativeToDollar;

            _context.SaveChanges();

            return new OkObjectResult(currency);
        }
    }
}