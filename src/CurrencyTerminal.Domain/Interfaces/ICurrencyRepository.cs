using CurrencyTerminal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyTerminal.Domain.Interfaces
{
    public interface ICurrencyRepository
    {
        /// <summary>
        /// Получение данных о конкретной валюте
        /// </summary>
        /// <param name="currencyCode"></param>
        /// <param name="onDate"></param>
        /// <returns></returns>
        public Task<CurrencyRate?> GetCurrencyRateAsync(string currencyCode, DateTime? onDate = null);

        /// <summary>
        /// Получение данных о всех валютах
        /// </summary>
        /// <param name="onDate"></param>
        /// <returns></returns>
        public Task<IEnumerable<CurrencyRate>> GetAllCurrenciesDataAsync(DateTime? onDate = null);
    }
}
