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

        public string Exchange(decimal value, string currencyCode = "pln", bool backToPln = false)
        {
            if (currencyCode == "pln")
                    return value + " PLN";

            try
            {
                decimal val;

                string currencyUrl = string.Format("http://api.nbp.pl/api/exchangerates/rates/a/{0}/?format=json", currencyCode);

                string currencyJsonString;
                using (var wc = new WebClient())
                {
                    currencyJsonString = wc.DownloadString(currencyUrl);
                }

                dynamic json = JObject.Parse(currencyJsonString);

                var currencyValue = (string)json.SelectToken("rates[0].mid");
                Debug.WriteLine(currencyValue);

                if (backToPln)
                    val = value * decimal.Parse(currencyValue);
                else
                    val = value / decimal.Parse(currencyValue);

                return Math.Round((val), 2, MidpointRounding.ToEven).ToString() + ' ' + currencyCode.ToUpper();
            }
            catch
            {
                    return value + " PLN";
            }
        }

        public decimal ExchangeWithoutCC(decimal value, string currencyCode = "pln", bool backToPln = false)
        {
            string valueWithCC = Exchange(value, currencyCode, backToPln);
            if (valueWithCC.IndexOf(' ') >= 0)
                valueWithCC = valueWithCC.Remove(valueWithCC.IndexOf(' '));

            return Convert.ToDecimal(valueWithCC);
        }

        public List<Currency> GetList()
        {
            var currenciesList = new List<Currency>();

            currenciesList.Add(new Currency { name = "[USD] US Dollar", code = "usd" });
            currenciesList.Add(new Currency { name = "[EUR] Euro", code = "eur" });
            currenciesList.Add(new Currency { name = "[JPY] Japanese Yen", code = "jpy" });
            currenciesList.Add(new Currency { name = "[GBP] British Pound", code = "gbp" });
            currenciesList.Add(new Currency { name = "[CHF] Swiss Franc", code = "chf" });
            currenciesList.Add(new Currency { name = "[CAD] Canadian Dollar", code = "cad" });
            currenciesList.Add(new Currency { name = "[AUD] Australian Dollar", code = "aud" });
            currenciesList.Add(new Currency { name = "[HKD] Hong Kong Dollar", code = "hkd" });
            currenciesList.Add(new Currency { name = "[PLN] Polish Zloty", code = "pln" });
            currenciesList.Add(new Currency { name = "[NZD] New Zealand Dollar", code = "nzd" });
            currenciesList.Add(new Currency { name = "[MXN] Mexican Peso", code = "mxn" });
            currenciesList.Add(new Currency { name = "[CNY] Chinese Yuan", code = "cny" });
            currenciesList.Add(new Currency { name = "[HRK] Croatian Kuna", code = "hrk" });

            return currenciesList.OrderBy(p => p.code).ToList();
        }
    }
}