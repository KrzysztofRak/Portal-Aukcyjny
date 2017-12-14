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

namespace Portal_aukcyjny.PublicPages.Auction
{
    class Bid
    {
        public string BiddrName { get; set; }
        public string BidderId { get; set; }
        public string Price { get; set; }
        public string Date { get; set; }
    }

    public partial class ViewAuction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int auctionId;
            try
            {
                auctionId = int.Parse(Request.QueryString["id"]);
            }
            catch
            {
                auctionId = 0;
            }

           // auctionId = 1; ////////////////////////////////////////////

            PortalAukcyjnyEntities db = new PortalAukcyjnyEntities();


            var auction = (from p in db.Auctions where p.Id == auctionId select p).First();
            auction.Views++;
            db.SaveChanges();

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
                var bids = (from p in db.Bidders where p.AuctionId == auctionId select p).ToList();
                List<OfferControl> offersControls = new List<OfferControl>();


                var bidsData =
                   (from o in bids
                    join u in db.aspnet_Users on o.BidderId equals u.UserId
                    select new Bid() { BiddrName = u.UserName, BidderId = u.UserId.ToString(), Price = o.Price.ToString(), Date = o.BidDate.ToString() }).ToList();

                for (int i = 0; i < bidsData.Count; i++)
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
                    bidder.Text = bidsData[j].BiddrName;
                    bidder.NavigateUrl = Page.ResolveUrl("~/PublicPages/User/UserProfile?id=" + bidsData[j].BidderId);
                    ((Label)offerControl.FindControl("BidPrice")).Text = bidsData[j].Price;
                    ((Label)offerControl.FindControl("BidDate")).Text = bidsData[j].Date;
                    j++;
                }
            }
        }
    }
}