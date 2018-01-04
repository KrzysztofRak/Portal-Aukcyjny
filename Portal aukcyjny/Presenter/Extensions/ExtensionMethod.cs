using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Extensions
{
    public class ExtensionMethod
    {
        public static int TryIntParse(string source)
        {
            int val;
            return int.TryParse(source, out val) ? val : 0;
        }

    }
}