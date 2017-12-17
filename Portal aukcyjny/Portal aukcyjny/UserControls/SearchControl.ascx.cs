using Portal_aukcyjny.Controller;
using Portal_aukcyjny.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal_aukcyjny
{
    public partial class SearchControl : System.Web.UI.UserControl
    {
        private PortalAukcyjnyEntities db;

        protected void Page_Load(object sender, EventArgs e)
        {
            db = new PortalAukcyjnyEntities();
        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {

            Response.Redirect(Page.ResolveUrl("~/Default.aspx?search=" + HttpUtility.UrlEncode(SearchBox.Text)));
        }
    }
}