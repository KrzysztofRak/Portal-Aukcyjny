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
using Model.Repositories;
using Presenter.IViews;
using Presenter;

namespace Portal_aukcyjny.PublicPages.Auction
{
    public partial class AuctionPage : System.Web.UI.Page, IAuctionPageView
    {
        private AuctionPagePresenter presenter;

        private int auctionId = -1;
        private string sellerId;
        private string bestBidderId;
        private string bidderId;
        private string minimumOffer;

        public int AuctionId
        {
            get { return auctionId; }
            set { auctionId = value; }
        }

        public string SellerId
        {
            get { return sellerId; }
            set { sellerId = value; }
        }

        public string BestBidderId
        {
            get { return bestBidderId; }
            set { bestBidderId = value; }
        }

        public string BidderId
        {
            get { return bidderId; }
            set { bidderId = value; }
        }

        public string MinimumOffer
        {
            get { return minimumOffer; }
            set { minimumOffer = value; }
        }

        public string ObserveBtnText
        {
            get { return Observe.Text; }
            set { Observe.Text = value; }
        }

        public bool ObserveBtnVisiblity
        {
            get { return Observe.Visible; }
            set { Observe.Visible = value; }
        }

        public EventHandler ObserveBtnEvent
        {
            get { return null; }
            set { Observe.Click += value; }
        }

        public string AuctionTitleField
        {
            get { return AuctionTitle.Text; }
            set { AuctionTitle.Text = value; }
        }

        public string AuctionImgUrl
        {
            get { return AuctionImg.ImageUrl; }
            set { AuctionImg.ImageUrl = value; }
        }

        public string TimeLeftField
        {
            get { return TimeLeft.Text; }
            set { TimeLeft.Text = value; }
        }

        public string ItemsNumField
        {
            get { return ItemsNum.Text; }
            set { ItemsNum.Text = value; }
        }

        public string BuyItNowPriceField
        {
            get { return BuyItNowPrice.Text; }
            set { BuyItNowPrice.Text = value; }
        }

        public bool BuyItNowPriceFieldVisiblity
        {
            get { return BuyItNowPrice.Visible; }
            set { BuyItNowPrice.Visible = value; }
        }
        public bool BuyItNowLabelVisiblity
        {
            get { return BuyItNowLabel.Visible; }
            set { BuyItNowLabel.Visible = value; }
        }

        public bool BuyItNowBtnVisiblity
        {
            get { return BuyItNowBtn.Visible; }
            set { BuyItNowBtn.Visible = value; }
        }


        public string HighestBidField
        {
            get { return HighestBid.Text; }
            set { HighestBid.Text = value; }
        }

        public bool HighestBidFieldVisiblity
        {
            get { return HighestBid.Visible; }
            set { HighestBid.Visible = value; }
        }

        public string BestBidUserNameField
        {
            get { return BestBidUserName.Text; }
            set { BestBidUserName.Text = value; }
        }

        public bool BestBidUserNameFieldVisiblity
        {
            get { return BestBidUserName.Visible; }
            set { BestBidUserName.Visible = value; }
        }

        public string BestBidUserNameNavUrl
        {
            get { return BestBidUserName.NavigateUrl; }
            set { BestBidUserName.NavigateUrl = value; }
        }

        public string BestBidLabelText
        {
            get { return BidLabel.Text; }
            set { BidLabel.Text = value; }
        }

        public bool BestBidLabelVisiblity
        {
            get { return BidLabel.Visible; }
            set { BidLabel.Visible = value; }
        }

        public bool BestBidUserNameLabelVisiblity
        {
            get { return BidUsernameLabel.Visible; }
            set { BidUsernameLabel.Visible = value; }
        }

        public string BidField
        {
            get { return Bid.Text; }
            set { Bid.Text = value; }
        }

        public bool BidFieldVisiblity
        {
            get { return Bid.Visible; }
            set { Bid.Visible = value; }
        }

        public bool BidBtnVisiblity
        {
            get { return BidBtn.Visible; }
            set { BidBtn.Visible = value; }
        }

        public string ShipmentField
        {
            get { return Shipment.Text; }
            set { Shipment.Text = value; }
        }

        public string SellerNameField
        {
            get { return SellerName.Text; }
            set { SellerName.Text = value; }
        }

        public string SellerNameNavUrl
        {
            get { return SellerName.NavigateUrl; }
            set { SellerName.NavigateUrl = value; }
        }

        public string DescriptionField
        {
            get { return Description.Text; }
            set { Description.Text = value; }
        }

        public string ViewsNumField
        {
            get { return ViewsNum.Text; }
            set { ViewsNum.Text = value; }
        }

        public AuctionPage()
        {
            presenter = new AuctionPagePresenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                auctionId = int.Parse(Request.QueryString["id"]);
            }
            catch
            {
                Response.Redirect(Page.ResolveUrl("~/Default.aspx"));
                return;
            }

            if(presenter.LoadAuctionPage() == false)
                Response.Redirect(Page.ResolveUrl("~/Default.aspx"));

            SellerNameNavUrl = Page.ResolveUrl("~/PublicPages/User/UserProfile?id=" + SellerId);
        }

        public void LoadOffersControls(int offersNum)
        {
            BestBidUserNameNavUrl = Page.ResolveUrl("~/PublicPages/User/UserProfile?id=" + BestBidderId);

            List<OfferControl> offersControls = new List<OfferControl>();
            for (int i = 0; i < offersNum; i++)
                offersControls.Add(new OfferControl());

            ListView_Offers.DataSource = offersControls;
            ListView_Offers.DataBind();

            int j = 0;
            foreach (var item in ListView_Offers.Items)
            {
                var offerControl = (OfferControl)item.FindControl("OfferControl");
                presenter.SetControl(offerControl, j);
                offerControl.BidderNameNavUrl = Page.ResolveUrl("~/PublicPages/User/UserProfile?id=" + bidderId);
                j++;
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            BidField = MinimumOffer;
        }

        protected void StopObserve_Click(object sender, EventArgs e)
        {
            presenter.StopObserve(sender, e);
        }

        protected void StartObserve_Click(object sender, EventArgs e)
        {
            presenter.StartObserve(sender, e);
        }

        protected void BidBtn_Click(object sender, EventArgs e)
        {
            presenter.SendBid();
        }

        protected void BuyItNowBtn_Click(object sender, EventArgs e)
        {
            presenter.Buy();
        }
    }
}