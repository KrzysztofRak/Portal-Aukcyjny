using Presenter.IViews;
using Presenters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using Presenter;
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

        public SiteMaster()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Global.GetDefaultCultureCode());
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

                TopMenu.InnerHtml = "<li><a runat=\"server\" href=\"" + createAuctionUrl + "\">" + GetLocalResourceObject("Dodaj aukcje") + "</a></li>"
                    + "<li><a runat=\"server\" href=\"" + myAuctionsUrl + "\">" + GetLocalResourceObject("Moje aukcje") + "</a></li>"
                + "<li><a runat=\"server\" href=\"" + logoutUrl + "\">" + GetLocalResourceObject("Wyloguj") + "</a></li>";
            }

            if (!IsPostBack)
            {
                LoadDefaultCurrencyList();
                SetDefaultLanguageOnList();
            }
        }

        private void LoadDefaultCurrencyList()
        {
            presenter.SetCurrenciesListSource();

            ListDefaultCurrency.DataTextField = "name";
            ListDefaultCurrency.DataValueField = "code";
            ListDefaultCurrency.DataBind();
            var defaultCurrency = Global.GetDefaultCurrency();
            ListDefaultCurrency.Items.FindByValue(defaultCurrency).Selected = true;
        }

        private void SetDefaultCurrency(string currencyCode)
        {
            Response.Cookies["defaultCurrency"].Value = currencyCode;
            Response.Cookies["defaultCurrency"].Expires = DateTime.Now.AddDays(10000);
        }

        private void SetDefaultCultureInfo(string cultureCode)
        {
            Response.Cookies["defaultCultureInfo"].Value = cultureCode;
            Response.Cookies["defaultCultureInfo"].Expires = DateTime.Now.AddDays(10000);
        }

        private void SetDefaultLanguageOnList()
        {
            var defaultLang = Global.GetDefaultCultureCode();
            ListDefaultLanguage.Items.FindByValue(defaultLang).Selected = true;
        }

        protected void ListDefaultCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDefaultCurrency(ListDefaultCurrency.SelectedValue);
            Response.Redirect(Request.RawUrl);
        }

        protected void ListLanguages_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDefaultCultureInfo(ListDefaultLanguage.SelectedValue);
            Response.Redirect(Request.RawUrl);
        }
    }
}