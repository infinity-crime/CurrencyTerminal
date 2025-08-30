using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyTerminal.App.Common
{
    public enum ErrorType
    {
        Failure,
        NotFound,
        Validation,
        Conflict,
        AccessUnAuthorized
    }
}
