using CurrencyTerminal.App.Common;
using CurrencyTerminal.App.Interfaces;
using CurrencyTerminal.Domain.Entities;
using CurrencyTerminal.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyTerminal.App.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyRepository _currencyRepository;

        public CurrencyService(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        public Task<Result<IEnumerable<CurrencyRate>>> GetAllCurrencyRatesAsync(DateTime? onDate = null)
        {
            throw new NotImplementedException();
        }

        public Task<Result<CurrencyRate>> GetCurrencyRateAsync(string currencyCode, DateTime? onDate = null)
        {
            throw new NotImplementedException();
        }
    }
}
