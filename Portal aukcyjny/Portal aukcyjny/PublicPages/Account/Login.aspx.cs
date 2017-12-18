using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Portal_aukcyjny.Controller;

namespace Portal_aukcyjny.Accounts
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Presenter.IsUserLoggedIn())
            {
                Response.Redirect(Page.ResolveUrl("~/Default.aspx"));
            }
        }
    }
}