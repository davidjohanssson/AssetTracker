using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Server
{
    [ApiController]
    [Route("[controller]")]
    public class OfficeController : ControllerBase
    {
        private AssetTrackerContext _context;

        public OfficeController(AssetTrackerContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var office = _context.Offices
                .Include(office => office.Currency)
                .FirstOrDefault(office => office.Id == id);

            if (office == null)
            {
                return new NotFoundObjectResult($"Office with id {id} not found");
            }

            return new OkObjectResult(office);
        }

        [HttpPost]
        public IActionResult Create(Office dto)
        {
            if (dto.City == null)
            {
                return new BadRequestObjectResult("City must not be null");
            }

            if (dto.City.Length < 1)
            {
                return new BadRequestObjectResult("City must be atleast 1 character");
            }

            if (dto.City.Length > 256)
            {
                return new BadRequestObjectResult("City must not be more than 256 characters");
            }

            var currency = _context.Currencies.FirstOrDefault(currency => currency.Id == dto.CurrencyId);

            if (currency == null)
            {
                return new NotFoundObjectResult($"Currency with id {dto.CurrencyId} not found");
            }

            _context.Offices.Add(dto);
            _context.SaveChanges();

            return new OkObjectResult(dto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Office dto)
        {
            var office = _context.Offices
                .Include(office => office.Currency)
                .FirstOrDefault(office => office.Id == id);

            if (office == null)
            {
                return new NotFoundObjectResult($"Office with id {id} not found");
            }

            if (dto.City == null)
            {
                return new BadRequestObjectResult("City must not be null");
            }

            if (dto.City.Length < 1)
            {
                return new BadRequestObjectResult("City must be atleast 1 character");
            }

            if (dto.City.Length > 256)
            {
                return new BadRequestObjectResult("City must not be more than 256 characters");
            }

            var currency = _context.Currencies.FirstOrDefault(currency => currency.Id == dto.CurrencyId);

            if (currency == null)
            {
                return new NotFoundObjectResult($"Currency with id {dto.CurrencyId} not found");
            }

            office.City = dto.City;
            office.Currency = currency;

            _context.SaveChanges();

            return new OkObjectResult(office);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var office = _context.Offices
                .FirstOrDefault(office => office.Id == id);

            if (office == null)
            {
                return new NotFoundObjectResult($"Office with id {id} not found");
            }

            _context.Offices.Remove(office);
            _context.SaveChanges();

            return new OkResult();
        }

        [HttpPost("filter")]
        public IActionResult Filter(OfficeFilter filter)
        {
            var query = _context.Offices.AsQueryable();

            if (filter.Ids != null)
            {
                query = query.Where(office => filter.Ids.Contains(office.Id));
            }

            if (filter.Cities != null)
            {
                query = query.Where(office => filter.Cities.Contains(office.City));
            }

            if (filter.CurrencyFilter != null)
            {
                if (filter.CurrencyFilter.Ids != null)
                {
                    query = query.Where(office => filter.CurrencyFilter.Ids.Contains(office.Currency.Id));
                }

                if (filter.CurrencyFilter.Name != null)
                {
                    query = query.Where(office => office.Currency.Name.Contains(filter.CurrencyFilter.Name));
                }

                if (filter.CurrencyFilter.Codes != null)
                {
                    query = query.Where(office => filter.CurrencyFilter.Codes.Contains(office.Currency.Code));
                }

                if (filter.CurrencyFilter.ExchangeRateRelativeToDollarMin != null)
                {
                    query = query.Where(office => filter.CurrencyFilter.ExchangeRateRelativeToDollarMin >= office.Currency.ExchangeRateRelativeToDollar);
                }

                if (filter.CurrencyFilter.ExchangeRateRelativeToDollarMax != null)
                {
                    query = query.Where(office => filter.CurrencyFilter.ExchangeRateRelativeToDollarMax <= office.Currency.ExchangeRateRelativeToDollar);
                }
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

            query = query.Take(10);

            var offices = query
                .Include(office => office.Currency)
                .ToList();

            var result = new Object[] { offices, count };

            return new OkObjectResult(result);
        }
    }
}