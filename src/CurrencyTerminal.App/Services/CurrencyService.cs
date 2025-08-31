using AutoMapper;
using CurrencyTerminal.App.Common;
using CurrencyTerminal.App.Errors;
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
        private readonly IMapper _mapper;

        public CurrencyService(ICurrencyRepository currencyRepository, IMapper mapper)
        {
            _currencyRepository = currencyRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<CurrencyRate>>> GetAllCurrencyRatesAsync(DateTime? onDate = null)
        {
            try
            {
                var currencyRates = await _currencyRepository.GetAllCurrenciesDataAsync(onDate);

                if (currencyRates.Count() < 1 && onDate.HasValue)
                    return Result<IEnumerable<CurrencyRate>>
                        .Failure(CurrencyErrors.NotFound(onDate.Value));

                return Result<IEnumerable<CurrencyRate>>
                    .Success(_mapper.Map<IEnumerable<CurrencyRate>>(currencyRates));
            }
            catch(ApplicationException)
            {
                return Result<IEnumerable<CurrencyRate>>.Failure(CurrencyErrors.Failure());
            }
        }

        public Task<Result<CurrencyRate>> GetCurrencyRateAsync(string currencyCode, DateTime? onDate = null)
        {
            throw new NotImplementedException();
        }
    }
}
