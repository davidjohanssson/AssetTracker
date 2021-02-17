using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server
{
    public class CurrencyService : ICurrencyService
    {
        private static List<Currency> currencies = new List<Currency> {
            new Currency { Id = 1, Name = "SEK", ExchangeRateRelativeToDollar = 8.93 },
            new Currency { Id = 2, Name = "RUB", ExchangeRateRelativeToDollar = 18.12 },
        };

        public async Task<List<Currency>> GetMany()
        {
            return currencies;
        }

        public async Task<Currency> Get(int id)
        {
            return currencies.FirstOrDefault(currency => currency.Id == id);
        }

        public async Task<List<Currency>> Create(CreateCurrencyDto dto)
        {
            var currency = new Currency();
            currency.Name = dto.Name;
            currency.ExchangeRateRelativeToDollar = dto.ExchangeRateRelativeToDollar;

            currencies.Add(currency);
            return currencies;
        }
    }
}