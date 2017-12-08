using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal_aukcyjny
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsUserLoggedIn())
            {
                TopMenu.InnerHtml = "<li><a runat=\"server\" href=\"~/Accounts/Login\">Moje konto</a></li>"
                    + "<li><a runat=\"server\" href=\"~/Accounts/Login\">Moje aukcje</a></li>"
                + "<li><a runat=\"server\" href=\"Accounts/Logout\">Wyloguj</a></li>";
            }
        }

        private bool IsUserLoggedIn()
        {
            return (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        }
    }
}