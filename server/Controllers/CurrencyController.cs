using System.Collections.Generic;
using System.Linq;
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
            return Ok(await _currencyService.GetManyCurrencies());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            return Ok(await _currencyService.GetCurrency(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddCurrency(AddCurrencyDto addCurrencyDto)
        {
            return Ok(await _currencyService.AddCurrency(addCurrencyDto));
        }
    }
}