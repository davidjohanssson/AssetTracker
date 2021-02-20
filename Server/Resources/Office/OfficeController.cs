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

        [HttpGet]
        public IActionResult GetAll()
        {
            var offices = _context.Offices
                .Include(office => office.Currency)
                .ToList();

            return new OkObjectResult(offices);
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
    }
}