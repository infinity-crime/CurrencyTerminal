using CurrencyTerminal.Domain.Interfaces;
using ServiceReference1;
using System;
using System.Collections.Generic;
using System.Data;
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

                string xml = response.OuterXml;

                var ds = new DataSet();

                using (var sr = new StringReader(xml))
                using (var xr = XmlReader.Create(sr))
                {
                    ds.ReadXml(xr);
                }

                var result = new List<CurrencyData>();
                if(ds.Tables.Contains("ValuteCursOnDate"))
                {
                    foreach (DataRow row in ds.Tables["ValuteCursOnDate"]!.Rows)
                    {
                        result.Add(new CurrencyData
                        {
                            Vname = row["Vname"]?.ToString() ?? string.Empty,

                            Vnom = Convert.ToDecimal(row["Vnom"], 
                            System.Globalization.CultureInfo.InvariantCulture),

                            Vcurs = Convert.ToDecimal(row["Vcurs"],
                            System.Globalization.CultureInfo.InvariantCulture),

                            Vcode = Convert.ToInt32(row["Vcode"]),

                            VchCode = row["VchCode"]?.ToString() ?? string.Empty,

                            VunitRate = Convert.ToDouble(row["VunitRate"], 
                            System.Globalization.CultureInfo.InvariantCulture)
                        });
                    }
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
