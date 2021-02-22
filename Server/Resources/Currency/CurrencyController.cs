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

        [HttpPost("search")]
        public IActionResult Search(CurrencySearch search)
        {
            var query = _context.Currencies.AsQueryable();

            if (search.Ids != null)
            {
                query = query.Where(currency => search.Ids.Contains(currency.Id));
            }

            if (search.Names != null)
            {
                query = query.Where(currency => search.Names.Contains(currency.Name));
            }

            if (search.ExchangeRateRelativeToDollarMin != null)
            {
                query = query.Where(currency => currency.ExchangeRateRelativeToDollar >= search.ExchangeRateRelativeToDollarMin);
            }

            if (search.ExchangeRateRelativeToDollarMax != null)
            {
                query = query.Where(currency => currency.ExchangeRateRelativeToDollar <= search.ExchangeRateRelativeToDollarMax);
            }

            if (search.OrderByAsc != null)
            {
                query = query.OrderBy(search.OrderByAsc);
            }

            if (search.OrderByDesc != null)
            {
                query = query.OrderByDescending(search.OrderByDesc);
            }

            if (search.Skip != null)
            {
                query = query.Skip(search.Skip.Value);
            }

            query.Take(25);

            var currencies = query.ToList();

            return new OkObjectResult(currencies);
        }
    }
}