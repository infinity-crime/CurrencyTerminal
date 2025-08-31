using CurrencyTerminal.App.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyTerminal.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet("currncy-allRates")]
        public async Task<IActionResult> GetAllRates()
        {
            var currancyRates = await _currencyService.GetAllCurrencyRatesAsync();
            if(currancyRates.IsSuccess)
            {
                return Ok(currancyRates);
            }

            return BadRequest();
        }
    }
}
