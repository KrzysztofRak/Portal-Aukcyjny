using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace Model.Repositories
{

    public class CurrencyExchangeRepository
    {
        private PortalAukcyjnyEntities db;

        public CurrencyExchangeRepository(PortalAukcyjnyEntities _db)
        {
            db = _db;
        }

        public class Currency
        {
            public string name { get; set; }
            public string code { get; set; }
        }

        public string Exchange(decimal value, string currencyCode = "pln")
        {
            if (currencyCode == "pln")
                return value + " PLN";

            try
            {
                string currencyUrl = string.Format("http://api.nbp.pl/api/exchangerates/rates/a/{0}/?format=json", currencyCode);

                string currencyJsonString;
                using (var wc = new WebClient())
                {
                    currencyJsonString = wc.DownloadString(currencyUrl);
                }

                dynamic json = JObject.Parse(currencyJsonString);

                var currencyValue = (string)json.SelectToken("rates[0].mid");
                Debug.WriteLine(currencyValue);

                return Math.Round((value / decimal.Parse(currencyValue)), 2, MidpointRounding.ToEven).ToString() + ' ' + currencyCode.ToUpper();
            }
            catch
            {
                    return value + " PLN";
            }
        }

        public List<Currency> GetList()
        {
            var currencyXmlUrl = "https://www.currency-iso.org/dam/downloads/lists/list_one.xml";
            var currencies = new List<Currency>();
            string currencyXmlString;

            using (var wc = new WebClient())
            {
                currencyXmlString = wc.DownloadString(currencyXmlUrl);
            }

            XmlDocument currencyXml = new XmlDocument();
            currencyXml.LoadXml(currencyXmlString);
            XmlNode currencyNode = currencyXml.SelectSingleNode("/ISO_4217/CcyTbl");
            XmlNodeList currencyNodeList = currencyNode.SelectNodes("CcyNtry");

            foreach (XmlNode xn in currencyNodeList)
            {
                XmlNode ccyNmNode = xn.SelectSingleNode("CcyNm");
                XmlNode ccyNode = xn.SelectSingleNode("Ccy");

                if (ccyNmNode != null && ccyNode != null)
                {
                    string currencyCode = ccyNode.InnerText;
                    currencies.Add(new Currency() { name = "[" + currencyCode + "] " + ccyNmNode.InnerText, code = currencyCode });
                }
            }

            currencies = currencies
              .GroupBy(p => p.code)
              .Select(g => g.First())
              .OrderBy(p => p.code)
              .ToList();

            return currencies;
        }
    }
}