using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Presenter;
using Presenter.IViews;

namespace Portal_aukcyjny.UserControls
{
    public partial class AuctionControl : System.Web.UI.UserControl, IAuctionControlView
    {
        private AuctionControlPresenter presenter;

        public AuctionControl()
        {
            presenter = new AuctionControlPresenter(this, Global.GetDefaultCurrency());
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string TitleField
        {
            get { return Title.Text; }
            set { Title.Text = value; }
        }

        public string AuctionUrl
        {
            get { return Title.NavigateUrl; }
            set { Title.NavigateUrl = value; }
        }

        public string ImageUrl
        {
            get { return Image.ImageUrl; }
            set { Image.ImageUrl = value; }
        }

        public string BuyItNowField
        {
            get { return BuyItNow.Text; }
            set { BuyItNow.Text = value; }
        }

        public bool BuyItNowVisiblity
        {
            set { BuyItNow.Visible = value; }
        }

        public bool BuyItNowLabelVisiblity
        {
            set { BuyItNowLabel.Visible = value; }
        }

        public string BidField
        {
            get { return Bid.Text; }
            set { Bid.Text = value; }
        }

        public bool BidVisiblity
        {
            get { return Bid.Visible; }
            set { Bid.Visible = value; }
        }

        public bool BidLabelVisiblity
        {
            get { return BidLabel.Visible; }
            set { BidLabel.Visible = value; }
        }

        public string SellerField
        {
            get { return Seller.Text; }
            set { Seller.Text = value; }
        }

        public string SellerNavUrl
        {
            get { return Seller.NavigateUrl; }
            set { Seller.NavigateUrl = value; }
        }

        public string ShipmentField
        {
            get { return Shipment.Text; }
            set { Shipment.Text = value; }
        }

        public string TimeLeftField
        {
            get { return TimeLeft.Text; }
            set { TimeLeft.Text = value; }
        }

        public string TimeLeftLabelText
        {
            get { return TimeLeftLabel.Text; }
            set { TimeLeftLabel.Text = value; }
        }

        public string OffersNumField
        {
            get { return OffersNum.Text; }
            set { OffersNum.Text = value; }
        }

        public string ViewsField
        {
            get { return Views.Text; }
            set { Views.Text = value; }
        }

        public void LoadAuctionsBySearch(string searchString)
        {
            presenter.LoadAuctionsBySearch(searchString);
        }

        public void LoadAuctionsByCatId(int catId)
        {
            presenter.LoadAuctionsByCatId(catId);
        }

        public void LoadSelling()
        {
            presenter.LoadSelling();
        }

        public void LoadSold()
        {
            presenter.LoadSold();
        }

        public void LoadBuyed()
        {
            presenter.LoadBuyed();
        }

        public void LoadBidding()
        {
            presenter.LoadBidding();
        }

        public void LoadObserved()
        {
            presenter.LoadObserved();
        }

        public void LoadByUserId(Guid userId)
        {
            presenter.LoadByUserId(userId);
        }

        public void LoadControls(ListView listView)
        {
            var auctionControls = new List<AuctionControl>();
            for (int i = 0; i < presenter.GetAuctionsNum(); i++)
                auctionControls.Add(new AuctionControl());
            listView.DataSource = auctionControls;
            listView.DataBind();

            int j = 0;
            foreach (var item in listView.Items)
            {
                IAuctionControlView ac = (IAuctionControlView)item.FindControl("AuctionControl");
                ac.AuctionUrl = (HttpContext.Current.Handler as Page).ResolveUrl("~/PublicPages/Auction/AuctionPage?id=");
                ac.SellerNavUrl = (HttpContext.Current.Handler as Page).ResolveUrl("~/PublicPages/User/UserProfile?id=");
                presenter.SetControl(ac, j);
                j++;
            }
        }
    }
}