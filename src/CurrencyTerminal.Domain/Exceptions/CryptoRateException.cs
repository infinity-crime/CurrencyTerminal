using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyTerminal.Domain.Exceptions
{
    public class CryptoRateException : Exception
    {
        public CryptoRateException(string message) : base(message) { }
    }
}
