using CurrencyTerminal.App.Interfaces;
using CurrencyTerminal.Domain.Entities;
using CurrencyTerminal.WebAPI.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace CurrencyTerminal.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class CurrencyController : BaseController
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet("all-rates/{date?}")]
        public async Task<IActionResult> GetAllCurrencyRatesAsync([FromRoute] DateTime? date)
        {
            var currancyRates = await _currencyService.GetAllCurrencyRatesAsync(date);
            return HandleResult<IEnumerable<CurrencyRate>>(currancyRates);
        }

        [HttpGet("rate/{code}/{date?}")]
        public async Task<IActionResult> GetCurrencyRateAsync([FromRoute] string code, [FromRoute] DateTime? date)
        {
            var currencyRate = await _currencyService.GetCurrencyRateAsync(code, date);
            return HandleResult<CurrencyRate>(currencyRate);
        }

        [HttpGet("codes")]
        public async Task<IActionResult> GetCurrencyCodesAsync()
        {
            var dictionaryOfCodes = await _currencyService.GetAllCurrencyCodesAsync();
            return HandleResult(dictionaryOfCodes);
        }
    }
}
