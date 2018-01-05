using Model;
using Model.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Newtonsoft.Json;

namespace WebService
{
    [WebService(Namespace = "http://localhost/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AuctionsService : System.Web.Services.WebService
    {

        [WebMethod]
        public string GetFirstAuction()
        {
            AuctionsRepository auctionsRepo = new AuctionsRepository();
            var auc = auctionsRepo.GetAllActual().FirstOrDefault();
            return JsonConvert.SerializeObject(auc, Formatting.Indented);
        }
    }
}
