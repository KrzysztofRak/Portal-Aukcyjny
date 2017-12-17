using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Portal_aukcyjny.UserControls;
using Portal_aukcyjny.Repositories;

namespace Portal_aukcyjny.PublicPages.Auction
{


    public partial class ViewAuction : System.Web.UI.Page
    {
        private PortalAukcyjnyEntities db;
        private int auctionId = -1;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                auctionId = int.Parse(Request.QueryString["id"]);
            }
            catch
            {
                Response.Redirect(Page.ResolveUrl("~/Default.aspx"));
            }

            db = new PortalAukcyjnyEntities();

            LoadAuctionPage();
        }

        private void LoadAuctionPage()
        {
            AuctionsRepository auctionsRepo = new AuctionsRepository(db);
            var auction = auctionsRepo.Get(auctionId);
            if(auction == null)
                Response.Redirect(Page.ResolveUrl("~/Default.aspx"));

            auctionsRepo.UpdateViews(auction);

            if (auction.BuyItNowPrice == 0)
            {
                BuyItNowLabel.Visible = false;
                BuyItNowPrice.Visible = false;
                BuyItNowBtn.Visible = false;
            }
            else if (auction.CurrentPrice == 0)
            {
                BidLabel.Visible = false;
                HighestBid.Visible = false;
                Bid.Visible = false;
                BidBtn.Visible = false;
            }

            AuctionTitle.Text = auction.Title;

            if (auction.Image == null)
                AuctionImg.ImageUrl = "~/Images/defaultAuctionImg.jpg";
            else
                AuctionImg.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(auction.Image);
            
            ItemsNum.Text = auction.ItemsNumber.ToString();
            var timeLeft = (auction.EndDate.Subtract(DateTime.Now));

            if (timeLeft.TotalDays > 1)
                EndTime.Text = (int)timeLeft.TotalDays + " dni";
            else
                EndTime.Text = String.Format("{0:hh\\:mm\\:ss}", timeLeft);

            var seller = db.aspnet_Users.Find(auction.OwnerId);// (from p in db.aspnet_Users where p.UserId ==  select p).First();
            SellerName.Text = seller.UserName;
            SellerName.NavigateUrl = Page.ResolveUrl("~/PublicPages/User/UserProfile?id=" + seller.UserId.ToString());
            BuyItNowPrice.Text = auction.BuyItNowPrice.ToString();
            HighestBid.Text = auction.CurrentPrice.ToString();
            Bid.Text = (auction.CurrentPrice + 1).ToString();
            Description.Text = auction.Description;
            ViewsNum.Text = auction.Views.ToString();

            if (auction.CurrentPrice > 0)
            {
                LoadOfferControls();
            }
        }

        private void LoadOfferControls()
        {
            OffersRepository offersRepo = new OffersRepository(db);

            var offers = offersRepo.GetForAuction(auctionId);


            List<OfferControl> offersControls = new List<OfferControl>();
            for (int i = 0; i < offers.Count; i++)
                offersControls.Add(new OfferControl());

            ListView_Offers.DataSource = offersControls;
            ListView_Offers.DataBind();

            int j = 0;
            OfferControl offerControl;
            HyperLink bidder;
            foreach (var item in ListView_Offers.Items)
            {
                offerControl = (OfferControl)item.FindControl("OfferControl");
                bidder = ((HyperLink)offerControl.FindControl("BidderName"));
                bidder.Text = offers[j].BiddrName;
                bidder.NavigateUrl = Page.ResolveUrl("~/PublicPages/User/UserProfile?id=" + offers[j].BidderId);
                ((Label)offerControl.FindControl("BidPrice")).Text = offers[j].Price;
                ((Label)offerControl.FindControl("BidDate")).Text = offers[j].Date;
                j++;
            }
        }
    }
}