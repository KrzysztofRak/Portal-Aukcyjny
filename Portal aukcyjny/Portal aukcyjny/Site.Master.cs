using Portal_aukcyjny.Controller;
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
            if (Presenter.IsUserLoggedIn())
            {
                string myAccountUrl = Page.ResolveUrl("~/MembersPages/Account/MyAccount");
                string createAuctionUrl = Page.ResolveUrl("~/MembersPages/Auction/CreateAuction");
                string myAuctionsUrl = Page.ResolveUrl("~/MembersPages/Auction/MyAuctions");
                string logoutUrl = Page.ResolveUrl("~/MembersPages/Account/Logout");

                TopMenu.InnerHtml = "<li><a runat=\"server\" href=\"" + myAccountUrl + "\">Moje konto</a></li>"
                    + "<li><a runat=\"server\" href=\"" + createAuctionUrl + "\">Dodaj aukcje</a></li>"
                    + "<li><a runat=\"server\" href=\"" + myAuctionsUrl + "\">Moje aukcje</a></li>"
                + "<li><a runat=\"server\" href=\"" + logoutUrl + "\">Wyloguj</a></li>";
            }
        }
    }
}