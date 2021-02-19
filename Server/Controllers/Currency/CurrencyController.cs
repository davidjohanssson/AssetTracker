using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Server
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyController : ControllerBase
    {
        private AssetTrackerContext _context;
        private ICreateCurrencyService _createCurrencyService;
        private IUpdateCurrencyService _updateCurrencyService; 

        public CurrencyController(
            AssetTrackerContext context,
            ICreateCurrencyService createCurrencyService,
            IUpdateCurrencyService updateCurrencyService
        )
        {
            _context = context;
            _createCurrencyService = createCurrencyService;
            _updateCurrencyService = updateCurrencyService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var currencies = _context.Currencies.ToList();

            return new OkObjectResult(currencies);
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
        public IActionResult Create(CreateCurrencyDto dto)
        {
            var result = _createCurrencyService.Run(dto);

            return result;
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateCurrencyDto dto)
        {
            var result = _updateCurrencyService.Run(id, dto);

            return result;
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
    }
}