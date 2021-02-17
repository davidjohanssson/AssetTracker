using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Server
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMany()
        {
            return Ok(await _currencyService.GetMany());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _currencyService.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCurrencyDto dto)
        {
            return Ok(await _currencyService.Create(dto));
        }
    }
}