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
        /// Получение данных о конкретной валюте. Исключения: 
        /// KeyNotFoundException() при неверном currencyCode,
        /// ApplicationException() при сбое получения данных.
        /// </summary>
        /// <param name="currencyCode"></param>
        /// <param name="onDate"></param>
        /// <returns></returns>
        public Task<CurrencyData?> GetCurrencyRateAsync(string currencyCode, DateTime? onDate = null);

        /// <summary>
        /// Получение данных о всех валютах. Исключения:
        /// ApplicationException() при сбое получения данных.
        /// </summary>
        /// <param name="onDate"></param>
        /// <returns></returns>
        public Task<IEnumerable<CurrencyData>> GetAllCurrenciesDataAsync(DateTime? onDate = null);
    }

    /// <summary>
    /// Класс для инкапсуляции xml-данных, полученых из службы банка:
    /// Vname — Название валюты;
    /// Vnom — Номинал;
    /// Vcurs — Курс;
    /// Vcode — ISO Цифровой код валюты;
    /// VchCode — ISO Символьный код валюты;
    /// VunitRate — Курс за 1 единицу валюты;
    /// </summary>
    public class CurrencyData
    {
        public string Vname { get; set; } = string.Empty;
        public decimal Vnom { get; set; }
        public decimal Vcurs { get; set; }
        public int Vcode { get; set; }
        public string VchCode { get; set; } = string.Empty;
        public double VunitRate { get; set; }
    }
}
