using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server
{
    public interface ICurrencyService
    {
        Task<List<Currency>> GetManyCurrencies();
        Task<Currency> GetCurrency(int id);
        Task<List<Currency>> AddCurrency(AddCurrencyDto addCurrencyDto);
    }
}