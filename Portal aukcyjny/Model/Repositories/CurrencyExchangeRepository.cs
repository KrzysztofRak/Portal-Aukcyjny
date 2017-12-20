using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;

namespace Model.Repositories
{
    public class CurrencyExchangeRepository
    {
        public static string Exchange(decimal value)
        {
          //  return value + " PLN";
            var m_strFilePath = "http://api.nbp.pl/api/exchangerates/rates/a/eur/";
            string xmlStr;
            using (var wc = new WebClient())
            {
                xmlStr = wc.DownloadString(m_strFilePath);
            }
            Debug.WriteLine(xmlStr);
            dynamic json = JObject.Parse(xmlStr);

            var fValue= (string)json.SelectToken("rates[0].mid");
            Debug.WriteLine(fValue);

            return Math.Ceiling((value/(decimal.Parse(fValue))*100)/100).ToString();
        }
        
    }
}