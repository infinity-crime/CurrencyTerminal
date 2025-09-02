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
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public async Task<Result<IDictionary<string, string>>> GetAllCurrencyCodesAsync()
        {
            var dictionaryRate = new Dictionary<string, string>();

            var currencyDataList = await _currencyRepository.GetAllCurrenciesDataAsync();

            if (!currencyDataList.Any())
                return Result<IDictionary<string, string>>
                    .Failure(CurrencyErrors.Failure());

            foreach (var cd in currencyDataList)
            {
                dictionaryRate[cd.Vname] = cd.VchCode;
            }

            return Result<IDictionary<string, string>>.Success(dictionaryRate);
        }

        public async Task<Result<IEnumerable<CurrencyRate>>> GetAllCurrencyRatesAsync(DateTime? onDate = null)
        {
            var currencyDataList = await _currencyRepository.GetAllCurrenciesDataAsync(onDate);

            if (!currencyDataList.Any() && onDate.HasValue)
                return Result<IEnumerable<CurrencyRate>>
                    .Failure(CurrencyErrors.NotFound(onDate.Value));

            return Result<IEnumerable<CurrencyRate>>
                .Success(_mapper.Map<IEnumerable<CurrencyRate>>(currencyDataList));
        }

        public async Task<Result<CurrencyRate>> GetCurrencyRateAsync(string currencyCode, DateTime? onDate = null)
        {
            var currencyData = await _currencyRepository.GetCurrencyRateAsync(currencyCode, onDate);

            if(currencyData == null)
                return Result<CurrencyRate>
                    .Failure(CurrencyErrors.NotFound(currencyCode));

            return Result<CurrencyRate>.Success(_mapper.Map<CurrencyRate>(currencyData));
        }
    }
}
