using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyTerminal.Domain.Common
{
    public abstract class BaseRate : IEquatable<BaseRate>
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public decimal RateToRuble { get; set; }

        // Metadata
        public string RequestDate { get; set; } = DateTime.UtcNow.ToShortDateString();

        public decimal ConvertFromRub(decimal amountInRub) =>
            amountInRub / RateToRuble;

        public bool Equals(BaseRate? other)
        {
            if (other is null) return false;

            return (Code == other.Code) && (RequestDate == other.RequestDate)
                && (RateToRuble == other.RateToRuble);
        }
    }
}
