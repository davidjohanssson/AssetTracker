using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Server
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyController : ControllerBase
    {
        private AssetTrackerContext _context;

        public CurrencyController(AssetTrackerContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var currency = _context.Currencies.FirstOrDefault(currency => currency.Id == id);

            if (currency == null)
            {
                return new NotFoundObjectResult($"Currency with id {id} not found");
            }

            return new OkObjectResult(currency);
        }

        [HttpPost]
        public IActionResult Create(Currency dto)
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
                return new BadRequestObjectResult("ExchangeRateRelativeToDollar must have two decimals");
            }

            _context.Currencies.Add(dto);
            _context.SaveChanges();

            return new OkObjectResult(dto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Currency dto)
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

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var currency = _context.Currencies.FirstOrDefault(currency => currency.Id == id);

            if (currency == null)
            {
                return new NotFoundObjectResult($"Currency with id {id} not found");
            }

            _context.Currencies.Remove(currency);
            _context.SaveChanges();

            return new OkResult();
        }

        [HttpPost("filter")]
        public IActionResult Filter(CurrencyFilter filter)
        {
            var query = _context.Currencies.AsQueryable();

            if (filter.Ids != null)
            {
                query = query.Where(currency => filter.Ids.Contains(currency.Id));
            }

            if (filter.Names != null)
            {
                query = query.Where(currency => filter.Names.Contains(currency.Name));
            }

            if (filter.ExchangeRateRelativeToDollarMin != null)
            {
                query = query.Where(currency => currency.ExchangeRateRelativeToDollar >= filter.ExchangeRateRelativeToDollarMin);
            }

            if (filter.ExchangeRateRelativeToDollarMax != null)
            {
                query = query.Where(currency => currency.ExchangeRateRelativeToDollar <= filter.ExchangeRateRelativeToDollarMax);
            }

            if (filter.OrderByAsc != null)
            {
                query = query.OrderBy(filter.OrderByAsc);
            }

            if (filter.OrderByDesc != null)
            {
                query = query.OrderByDescending(filter.OrderByDesc);
            }

            if (filter.Skip != null)
            {
                query = query.Skip(filter.Skip.Value);
            }

            var count = query.Count();

            query = query.Take(20);

            var currencies = query.ToList();

            var result = new { currencies, count };

            return new OkObjectResult(result);
        }
    }
}