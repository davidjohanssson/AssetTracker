using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server
{
    public interface ICurrencyService
    {
        Task<List<Currency>> GetMany();
        Task<Currency> Get(int id);
        Task<List<Currency>> Create(CreateCurrencyDto dto);
    }
}