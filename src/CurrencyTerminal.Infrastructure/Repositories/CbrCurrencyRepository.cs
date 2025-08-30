using CurrencyTerminal.Domain.Interfaces;
using ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

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

        public async Task<decimal> GetCurrencyRateAsync(string currencyCode, DateTime? onDate = null)
        {
            try
            {
                var date = onDate ?? DateTime.UtcNow;

                var response = await _soapClient.GetCursOnDateXMLAsync(date);

                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(response.OuterXml);

                var valuteNode = xmlDoc.SelectSingleNode($"ValuteCursOnDate[VchCode='{currencyCode}']");
                if(valuteNode != null)
                {
                    var rate = decimal.Parse(valuteNode.SelectSingleNode("Vcurs")!.InnerText);
                    var nominal = decimal.Parse(valuteNode.SelectSingleNode("Vnom")!.InnerText);

                    return rate / nominal;
                }

                throw new KeyNotFoundException($"Валюта с кодом {currencyCode} не найдена!");
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ошибка получения курса валюты", ex);
            }
        }
    }
}
