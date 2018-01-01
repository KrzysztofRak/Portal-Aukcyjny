using Presenter.IViews;
using Presenters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal_aukcyjny
{
    public partial class SiteMaster : MasterPage, IMasterView
    {
        private MasterPresenter presenter;

        public object CurrenciesSource
        {
            get { return ListDefaultCurrency.DataSource; }
            set { ListDefaultCurrency.DataSource = value; }
        }

        public object LanguagesSource
        {
            get { return ListDefaultCurrency.DataSource; }
            set { ListDefaultCurrency.DataSource = value; }
        }

        public SiteMaster()
        {
            presenter = new MasterPresenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {        
            if (presenter.IsUserLoggedIn())
            {
                string createAuctionUrl = Page.ResolveUrl("~/MembersPages/Auction/CreateAuction");
                string myAuctionsUrl = Page.ResolveUrl("~/MembersPages/Auction/MyAuctions");
                string settingsUrl = Page.ResolveUrl("~/MembersPages/Account/Settings");
                string logoutUrl = Page.ResolveUrl("~/MembersPages/Account/Logout");

                TopMenu.InnerHtml = "<li><a runat=\"server\" href=\"" + createAuctionUrl + "\">Dodaj aukcje</a></li>"
                    + "<li><a runat=\"server\" href=\"" + myAuctionsUrl + "\">Moje aukcje</a></li>"
                + "<li><a runat=\"server\" href=\"" + logoutUrl + "\">Wyloguj</a></li>";
            }

            if (!IsPostBack)
                LoadDefaultCurrencyList();
        }

        private void LoadDefaultCurrencyList()
        {
            presenter.SetCurrenciesListSource();

            ListDefaultCurrency.DataTextField = "name";
            ListDefaultCurrency.DataValueField = "code";
            ListDefaultCurrency.DataBind();
        }

        private void SetDefaultCurrency(string currencyCode)
        {
            Response.Cookies["defaultCurrency"].Value = currencyCode;
            Response.Cookies["defaultCurrency"].Expires = DateTime.Now.AddDays(10000);
        }

        protected void ListDefaultCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDefaultCurrency(ListDefaultCurrency.SelectedValue);
            Response.Redirect(Request.RawUrl);
        }
    }
}