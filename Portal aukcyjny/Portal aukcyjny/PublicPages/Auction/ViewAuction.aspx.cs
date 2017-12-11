using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Portal_aukcyjny.UserControls;

namespace Portal_aukcyjny.PublicPages.Auction
{
    public partial class ViewAuction : System.Web.UI.Page
    {
        private List<OfferControl> mycontrols = new List<OfferControl>();
        protected void Page_Init(object sender, EventArgs e)
        {

        }

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
                

            PortalAukcyjnyEntities db = new PortalAukcyjnyEntities();

            var auction = (from p in db.Auctions where p.Id == auctionId select p).First();

            AuctionTitle.Text = auction.Title;
            AuctionImg.ImageUrl = Page.ResolveUrl("~/Images/defaultAuctionImg.jpg");
            ItemsNum.Text = auction.ItemsNumber.ToString();
            EndTime.Text = auction.EndDate.Subtract(DateTime.Now).ToString();
            
            var seller = db.aspnet_Users.Find(auction.OwnerId);// (from p in db.aspnet_Users where p.UserId ==  select p).First();
            SellerName.Text = seller.UserName;
            SellerName.NavigateUrl = Page.ResolveUrl("~/PublicPages/User/UserProfile?id=" + seller.UserId.ToString()); 
            BuyItNowPrice.Text = auction.BuyItNowPrice.ToString();
            HighestBid.Text = auction.CurrentPrice.ToString();
            Description.Text = auction.Description;
            ViewsNum.Text = auction.Views.ToString();


            OfferControl myBase;

            for (int i = 0; i < 5; i++)
            {
                myBase = new OfferControl();
                myBase.name = "krzysio";
                mycontrols.Add(myBase);
            }

            ListView_Offers.DataSource = mycontrols;
            ListView_Offers.DataBind();



        }

    }
}