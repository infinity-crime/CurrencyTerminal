using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyTerminal.App.DTOs
{
    public class CurrencyRateDto
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public double RateInRubles { get; set; }
        public double PreviousRateValue { get; set; }
    }
}
