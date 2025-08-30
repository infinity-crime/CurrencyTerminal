using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyTerminal.App.Common
{
    public class Error
    {
        public string Code { get; }
        public string Description { get; }
        public ErrorType ErrorType { get; }

        private Error(string code, string description, ErrorType errorType)
        {
            Code = code;
            Description = description;
            ErrorType = errorType;
        }

        public static Error Failure(string code, string description) =>
            new(code, description, ErrorType.Failure);

        public static Error NotFound(string code, string description) =>
            new(code, description, ErrorType.NotFound);

        public static Error Validation(string code, string description) =>
            new(code, description, ErrorType.Validation);

        public static Error Conflict(string code, string description) =>
            new(code, description, ErrorType.Conflict);

        public static Error AccessUnAuthorized(string code, string description) =>
            new(code, description, ErrorType.AccessUnAuthorized);
    }
}
