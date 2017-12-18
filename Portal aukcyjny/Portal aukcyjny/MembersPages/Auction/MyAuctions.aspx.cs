using Portal_aukcyjny.Controller;
using Portal_aukcyjny.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal_aukcyjny.MembersPages.Auction
{
    public partial class MyAuctions : System.Web.UI.Page
    {
        private PortalAukcyjnyEntities db;
        private Guid userId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Presenter.IsUserLoggedIn())
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

            Controller.Presenter controls = new Controller.Presenter();
            controls.LoadAuctionControls(myAuctions, ListView_MyAuctions);
            controls.LoadAuctionControls(myFinishedAuctions, ListView_Finished);
            controls.LoadAuctionControls(watchedAuctions, ListView_Watched);
            controls.LoadAuctionControls(bidAuctions, ListView_Bid);
        }
    }
}