using CurrencyTerminal.App.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyTerminal.App.Errors
{
    public static class CurrencyErrors
    {
        public static Error NotFound(string currencyCode) =>
            Error.NotFound("Currency.NotFound", $"Валюта с кодом {currencyCode} не найдена!");

        public static Error NotFound(DateTime date) =>
            Error.NotFound("Currency.NotFound", $"Список валют с датой {date} отсутствует в базе!");

        public static Error Failure() =>
            Error.Failure("Currency.Failure", "Ошибка запроса к службам банка, данные не получены");
    }
}
