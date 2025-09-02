using CurrencyTerminal.Domain.Entities;
using CurrencyTerminal.Domain.Interfaces;
using ServiceReference1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace CurrencyTerminal.Infrastructure.Repositories
{
    public class CbrCurrencyRepository : ICurrencyRepository, IDisposable
    {
        private readonly DailyInfoSoapClient _soapClient;

        public CbrCurrencyRepository()
        {
            _soapClient = new DailyInfoSoapClient(DailyInfoSoapClient.EndpointConfiguration.DailyInfoSoap);
        }

        public void Dispose()
        {
            _soapClient?.CloseAsync().Wait();
        }

        public async Task<IEnumerable<CurrencyRate>> GetAllCurrenciesDataAsync(DateTime? onDate = null)
        {
            var date = onDate ?? DateTime.UtcNow;
            var result = new List<CurrencyRate>();

            XmlNode response = await _soapClient.GetCursOnDateXMLAsync(date);
            if (response == null)
                return result;

            foreach(XmlNode node in response.ChildNodes)
            {
                var code = node.SelectSingleNode("VchCode")?.InnerText.Trim()!;
                var name = node.SelectSingleNode("Vname")?.InnerText.Trim()!;
                double value = double.TryParse(node.SelectSingleNode("VunitRate")?
                    .InnerText!, CultureInfo.InvariantCulture, out double resRate) ? resRate : 0;

                result.Add(CurrencyRate.Create(code, name, value));
            }
            
            return result;
        }
        

        public async Task<CurrencyRate?> GetCurrencyRateAsync(string currencyCode, DateTime? onDate = null)
        {
            var date = onDate ?? DateTime.UtcNow;

            XmlNode response = await _soapClient.GetCursOnDateXMLAsync(date);
            if (response == null)
                return null;

            foreach(XmlNode node in response.ChildNodes)
            {
                if(node.SelectSingleNode("VchCode")!.InnerText == currencyCode)
                {
                    var code = node.SelectSingleNode("VchCode")?.InnerText.Trim()!;
                    var name = node.SelectSingleNode("Vname")?.InnerText.Trim()!;
                    double value = double.TryParse(node.SelectSingleNode("VunitRate")?
                        .InnerText!, CultureInfo.InvariantCulture, out double resRate) ? resRate : 0;

                    return CurrencyRate.Create(code, name, value);
                }
            }

            return null;
        }
    }
}
