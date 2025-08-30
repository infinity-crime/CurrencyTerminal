using CurrencyTerminal.Domain.Interfaces;
using ServiceReference1;
using System;
using System.Collections.Generic;
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
            try
            {
                var date = onDate ?? DateTime.UtcNow;
                var response = await _soapClient.GetCursOnDateXMLAsync(date);
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(response.OuterXml);

                var result = new List<CurrencyData>();
                foreach (XmlNode node in xmlDoc.SelectNodes("ValuteCursOnDate")!)
                {
                    result.Add(new CurrencyData
                    {
                        Vname = node.SelectSingleNode("Vname")?.InnerText!,
                        Vnom = decimal.Parse(node.SelectSingleNode("Vnom")?.InnerText!),
                        Vcurs = decimal.Parse(node.SelectSingleNode("Vcurs")?.InnerText!),
                        Vcode = int.Parse(node.SelectSingleNode("Vcode")?.InnerText!),
                        VchCode = node.SelectSingleNode("VchCode")?.InnerText!,
                        VunitRate = double.Parse(node.SelectSingleNode("VunitRate")?.InnerText!)
                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ошибка получения данных о валютах", ex);
            }
        }
        

        public async Task<CurrencyData> GetCurrencyRateAsync(string currencyCode, DateTime? onDate = null)
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
                    return new CurrencyData
                    {
                        Vname = valuteNode.SelectSingleNode("Vname")?.InnerText!,
                        Vnom = decimal.Parse(valuteNode.SelectSingleNode("Vnom")?.InnerText!),
                        Vcurs = decimal.Parse(valuteNode.SelectSingleNode("Vcurs")?.InnerText!),
                        Vcode = int.Parse(valuteNode.SelectSingleNode("Vcode")?.InnerText!),
                        VchCode = valuteNode.SelectSingleNode("VchCode")?.InnerText!,
                        VunitRate = double.Parse(valuteNode.SelectSingleNode("VunitRate")?.InnerText!)
                    };
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
