using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal_aukcyjny.Repositories
{
    public class CurrencyExchangeRepository
    {
        public static string Exchange(decimal value)
        {
            return value + " PLN";
        }
    }
}