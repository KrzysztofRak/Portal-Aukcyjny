using System;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI;
using Model.RepositoriesDataModel;
using Model.Repositories;
using Model;
using Portal_aukcyjny.UserControls;
using Presenter;
using Presenter.IViews;

namespace Portal_aukcyjny.MembersPages.Auction
{
    public partial class MyAuctions : System.Web.UI.Page, IMyAuctionsView
    {
        private MyAuctionsPresenter presenter;

        public MyAuctions()
        {
            presenter = new MyAuctionsPresenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!presenter.IsUserLoggedIn())
                Response.Redirect(Page.ResolveUrl("~/Default.aspx"));

            LoadAuctionControls();
        }

        private void LoadAuctionControls()
        {
            AuctionControl ac = new AuctionControl();
            ac.LoadSelling();
            ac.LoadControls(ListView_MyAuctions);

            ac.LoadSold();
            ac.LoadControls(ListView_Sold);

            ac.LoadBuyed();
            ac.LoadControls(ListView_Buyed);

            ac.LoadObserved();
            ac.LoadControls(ListView_Watched);

            ac.LoadBidding();
            ac.LoadControls(ListView_Bid);
        }


    }
}