using System.Collections.Generic;
using System.Threading.Tasks;

namespace server
{
    public interface ICurrencyService
    {
        Task<List<Currency>> GetManyCurrencies();
        Task<Currency> GetCurrency(int id);
        Task<List<Currency>> AddCurrency(AddCurrencyDto addCurrencyDto);
    }
}