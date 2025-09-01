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

        public async Task<IEnumerable<CurrencyData>> GetAllCurrenciesDataAsync(DateTime? onDate = null)
        {
            var date = onDate ?? DateTime.UtcNow;
            var result = new List<CurrencyData>();

            XmlNode response = await _soapClient.GetCursOnDateXMLAsync(date);
            if (response == null)
                return result;

            foreach(XmlNode node in response.ChildNodes)
            {
                result.Add(new CurrencyData
                {
                    Vname = node.SelectSingleNode("Vname")?.InnerText.Trim()!,

                    Vnom = decimal.TryParse(node.SelectSingleNode("Vnom")?
                    .InnerText!, CultureInfo.InvariantCulture, out decimal resNom) ? resNom : 0m,

                    Vcurs = decimal.TryParse(node.SelectSingleNode("Vcurs")?
                    .InnerText!, CultureInfo.InvariantCulture, out decimal resCurs) ? resCurs : 0m,

                    Vcode = int.TryParse(node.SelectSingleNode("Vcode")?
                    .InnerText!, CultureInfo.InvariantCulture, out int resCode) ? resCode : 0,

                    VchCode = node.SelectSingleNode("VchCode")?.InnerText.Trim()!,

                    VunitRate = double.TryParse(node.SelectSingleNode("VunitRate")?
                    .InnerText!, CultureInfo.InvariantCulture, out double resRate) ? resRate : 0
                });
            }
            
            return result;
        }
        

        public async Task<CurrencyData?> GetCurrencyRateAsync(string currencyCode, DateTime? onDate = null)
        {
            var date = onDate ?? DateTime.UtcNow;

            XmlNode response = await _soapClient.GetCursOnDateXMLAsync(date);
            if (response == null)
                return null;

            foreach(XmlNode node in response.ChildNodes)
            {
                if(node.SelectSingleNode("VchCode")!.InnerText == currencyCode)
                {
                    return new CurrencyData
                    {
                        Vname = node.SelectSingleNode("Vname")?.InnerText.Trim()!,

                        Vnom = decimal.TryParse(node.SelectSingleNode("Vnom")?
                        .InnerText!, CultureInfo.InvariantCulture, out decimal resNom) ? resNom : 0m,

                        Vcurs = decimal.TryParse(node.SelectSingleNode("Vcurs")?
                        .InnerText!, CultureInfo.InvariantCulture, out decimal resCurs) ? resCurs : 0m,

                        Vcode = int.TryParse(node.SelectSingleNode("Vcode")?
                        .InnerText!, CultureInfo.InvariantCulture, out int resCode) ? resCode : 0,

                        VchCode = node.SelectSingleNode("VchCode")?.InnerText.Trim()!,

                        VunitRate = double.TryParse(node.SelectSingleNode("VunitRate")?
                        .InnerText!, CultureInfo.InvariantCulture, out double resRate) ? resRate : 0
                    };
                }
            }

            return null;
        }
    }
}
