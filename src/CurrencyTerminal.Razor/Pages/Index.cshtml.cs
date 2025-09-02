using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CurrencyTerminal.Razor.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel()
        {
            
        }

        public IEnumerable<SelectListItem> AvailableCurrencies { get; private set; } = Enumerable.Empty<SelectListItem>();

        [BindProperty]
        public string SelectedCurrency { get; set; }

        public string? ErrorMessage { get; private set; }

        public async Task OnGetAsync()
        {

        }
    }
}
