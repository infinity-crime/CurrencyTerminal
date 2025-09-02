using CurrencyTerminal.Domain.Common;
using CurrencyTerminal.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyTerminal.Domain.Entities
{
    public sealed class CurrencyRate : BaseRate
    {
        public double PreviousValueRate { get; private set; }

        public static CurrencyRate Create(string code, string name, double value)
        {
            return new CurrencyRate
            {
                Code = code,
                Name = name,
                RateToRuble = value,
            };
        }

        public void SetPreviousValueRate(double value)
        {
            if (value < 0)
                throw new CurrencyRateException("Цена (пред.) не может быть отрицательной!");

            PreviousValueRate = value;
        }
    }
}
