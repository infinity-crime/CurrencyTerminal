using CurrencyTerminal.App.Common;
using CurrencyTerminal.App.DTOs;
using CurrencyTerminal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyTerminal.App.Interfaces
{
    public interface ICurrencyService
    {
        Task<Result<CurrencyRateDto>> GetCurrencyRateAsync(string currencyCode, DateTime? onDate = null);
        Task<Result<IEnumerable<CurrencyRateDto>>> GetAllCurrencyRatesAsync(DateTime? onDate = null);
        Task<Result<IDictionary<string, string>>> GetAllCurrencyCodesAsync();
    }
}
