using System;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI;
using Model.RepositoriesDataModel;
using Model.Repositories;
using Portal_aukcyjny.Presenters;
using Model;

namespace Portal_aukcyjny.MembersPages.Auction
{
    public partial class MyAuctions : System.Web.UI.Page
    {
        private PortalAukcyjnyEntities db;
        private Guid userId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Portal_aukcyjny.Presenters.MyPresenter.IsUserLoggedIn())
            {
                Response.Redirect(Page.ResolveUrl("~/Default.aspx"));
            }

            db = new PortalAukcyjnyEntities();
            userId = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());
            LoadAuctionControls();
        }

        private void LoadAuctionControls()
        {
            AuctionsRepository auctionsRepo = new AuctionsRepository(db);

            List<AuctionControlData> myAuctions = auctionsRepo.GetByUserId(userId);
            List<AuctionControlData> myFinishedAuctions = auctionsRepo.GetByUserId(userId, true);
            List<AuctionControlData> watchedAuctions = auctionsRepo.GetObserved(userId);
            List<AuctionControlData> bidAuctions = auctionsRepo.GetAuctioned(userId);

            Presenters.MyPresenter controls = new Presenters.MyPresenter();
            controls.LoadAuctionControls(myAuctions, ListView_MyAuctions);
            controls.LoadAuctionControls(myFinishedAuctions, ListView_Sold);
            controls.LoadAuctionControls(watchedAuctions, ListView_Watched);
            controls.LoadAuctionControls(bidAuctions, ListView_Bid);
        }
    }
}