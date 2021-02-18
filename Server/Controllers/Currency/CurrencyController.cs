using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Server
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly AssetTrackerContext _context;
        private readonly ICreateCurrencyService _createCurrencyService;

        public CurrencyController(AssetTrackerContext context, ICreateCurrencyService createCurrencyService)
        {
            _context = context;
            _createCurrencyService = createCurrencyService;
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
                return new NotFoundResult();
            }

            return new OkObjectResult(currency);
        }

        [HttpPost]
        public IActionResult Create(CreateCurrencyDto dto)
        {
            var result = _createCurrencyService.Run(dto);

            return result;
        }
    }
}