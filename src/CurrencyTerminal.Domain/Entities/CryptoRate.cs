using CurrencyTerminal.Domain.Common;
using CurrencyTerminal.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyTerminal.Domain.Entities
{
    public class CryptoRate : BaseRate
    {
        public decimal PriceUSDT { get; private set; }
        public decimal PriceChange24h { get; private set; }
        public decimal Volume24h { get; private set; }
        public decimal MarketCap { get; private set; }

        public static CryptoRate Create(string code, string name, decimal value)
        {
            return new CryptoRate
            {
                Code = code,
                Name = name,
                RateToRuble = value
            };
        }

        public void SetIndicators(decimal priceUSDT, decimal priceChange, decimal volume, decimal marketCap)
        {
            if (priceUSDT < 0 || volume < 0 || marketCap < 0)
                throw new CryptoRateException("Цена, объем торгов и рыночная капитализация не могут быть меньше нуля");

            PriceUSDT = priceUSDT;
            PriceChange24h = priceChange;
            Volume24h = volume;
            MarketCap = marketCap;
        }

        public void ChangeSource(string otherSource)
        {
            if (string.IsNullOrEmpty(otherSource))
                throw new CryptoRateException("Источник ресурсов не может быть NULL!");

            Source = otherSource;
        }
    }
}
