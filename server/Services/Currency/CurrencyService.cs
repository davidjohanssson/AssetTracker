using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server
{
    public class CurrencyService : ICurrencyService
    {
        private static List<Currency> currencies = new List<Currency> {
            new Currency { Id = 1, Name = "SEK", ExchangeRateRelativeToDollar = 8.93 },
            new Currency { Id = 2, Name = "RUB", ExchangeRateRelativeToDollar = 18.12 },
        };

        public async Task<Currency> GetCurrency(int id)
        {
            return currencies.FirstOrDefault(currency => currency.Id == id);
        }

        public async Task<List<Currency>> GetManyCurrencies()
        {
            return currencies;
        }

        public async Task<List<Currency>> AddCurrency(AddCurrencyDto addCurrencyDto)
        {
            var currency = new Currency();
            currency.Name = addCurrencyDto.Name;
            currency.ExchangeRateRelativeToDollar = addCurrencyDto.ExchangeRateRelativeToDollar;

            currencies.Add(currency);
            return currencies;
        }
    }
}