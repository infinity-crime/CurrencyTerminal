using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyTerminal.Domain.Exceptions
{
    public class CurrencyRateException : Exception
    {
        public CurrencyRateException(string message) : base(message) { }
    }
}
