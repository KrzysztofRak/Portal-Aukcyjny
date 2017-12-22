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
using Portal_aukcyjny.Presenters;
using Model;

namespace Portal_aukcyjny.PublicPages.Auction
{


    public partial class ViewAuction : System.Web.UI.Page
    {
        private PortalAukcyjnyEntities db;
        private Guid currentUserId;
        private CurrencyExchangeRepository currencyRepo;

        private int auctionId = -1;

        private string minimumOffer;

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
            if (MyPresenter.IsUserLoggedIn())
                currentUserId = MyPresenter.GetCurrentUserId();
            else
                currentUserId = new Guid();

            currencyRepo = new CurrencyExchangeRepository(db);

            LoadAuctionPage();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            Bid.Text = minimumOffer;
        }

        protected void StopObserve_Click(object sender, EventArgs e)
        {
            ObserversRepository observersRepo = new ObserversRepository(db);
            observersRepo.Delete(MyPresenter.GetCurrentUserId(), auctionId);
            Observe.Text = "Obserwuj";
            Observe.Click -= StopObserve_Click;
            Observe.Click += StartObserve_Click;
        }

        protected void StartObserve_Click(object sender, EventArgs e)
        {
            ObserversRepository observersRepo = new ObserversRepository(db);
            observersRepo.Add(MyPresenter.GetCurrentUserId(), auctionId);
            Observe.Text = "Przestań obserwować";
            Observe.Click -= StartObserve_Click;
            Observe.Click += StopObserve_Click;
        }

        private void LoadAuctionPage()
        {
            AuctionsRepository auctionsRepo = new AuctionsRepository(db);
            
            var auction = auctionsRepo.Get(auctionId);
            if(auction == null)
                Response.Redirect(Page.ResolveUrl("~/Default.aspx"));

            auctionsRepo.UpdateViewsNum(auction);

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

            if(!MyPresenter.IsUserLoggedIn() || MyPresenter.GetCurrentUserId() == auction.OwnerId)
            {
                Observe.Visible = false;
            }
            else
            {
                ObserversRepository observersRepo = new ObserversRepository(db);
                if(!observersRepo.CheckIfAuctionIsObservedByUser(MyPresenter.GetCurrentUserId(), auction.Id))
                {
                    Observe.Text = "Obserwuj";
                    Observe.Click -= StopObserve_Click;
                    Observe.Click += StartObserve_Click;
                }
                else
                {
                    Observe.Text = "Przestań obserwować";
                    Observe.Click -= StartObserve_Click;
                    Observe.Click += StopObserve_Click;
                }
            }

            UsersRepository usersRepo = new UsersRepository(db);

            var seller = usersRepo.Get(auction.OwnerId);// (from p in db.aspnet_Users where p.UserId ==  select p).First();
            SellerName.Text = seller.UserName;
            SellerName.NavigateUrl = Page.ResolveUrl("~/PublicPages/User/UserProfile?id=" + seller.UserId.ToString());
            Description.Text = auction.Description;
            ViewsNum.Text = auction.Views.ToString();

            ShipmentsRepository shipmentsRepo = new ShipmentsRepository(db);
            Shipment.Text = shipmentsRepo.GetShipmentFullName(currencyRepo, auction.ShipmentId);

            if (auction.BuyItNowPrice != -1)
            {
                BuyItNowLabel.Visible = true;
                BuyItNowPrice.Visible = true;
                BuyItNowBtn.Visible = true;

                BuyItNowPrice.Text = currencyRepo.Exchange(auction.BuyItNowPrice);
            }

            if (auction.MinimumPrice != -1)
            {
                HighestBid.Text = currencyRepo.Exchange(auction.MinimumPrice);
                minimumOffer = (auction.MinimumPrice + 1).ToString();
                LoadOfferControls(auction);
            }

            if(!MyPresenter.IsUserLoggedIn())
            {
                BuyItNowBtn.Visible = false;
                Bid.Visible = false;
                BidBtn.Visible = false;
            }
        }

        private void LoadOfferControls(Auctions auction)
        {
            BidLabel.Visible = true;
            BidUsernameLabel.Visible = true;
            HighestBid.Visible = true;
            Bid.Visible = true;
            BidBtn.Visible = true;

            OffersRepository offersRepo = new OffersRepository(db);

            var offers = offersRepo.GetForAuction(auctionId);
            if (offers.Count() == 0)
            {
                BidLabel.Text = "Cena minimalna: ";
                BidUsernameLabel.Visible = false;
                return;
            }

            List<OfferControl> offersControls = new List<OfferControl>();
            for (int i = 0; i < offers.Count; i++)
                offersControls.Add(new OfferControl());

            ListView_Offers.DataSource = offersControls;
            ListView_Offers.DataBind();

            int j = 0;
            OfferControl offerControl;
            HyperLink bidder;

            BestBidUserName.Visible = true;
            if(offers[j].Price >= auction.BuyItNowPrice)
            {
                BuyItNowLabel.Visible = false;
                BuyItNowPrice.Visible = false;
                BuyItNowBtn.Visible = false;
            }
            HighestBid.Text = currencyRepo.Exchange(offers[j].Price);
            BestBidUserName.Text = offers[j].BiddrName;
            BestBidUserName.NavigateUrl = Page.ResolveUrl("~/PublicPages/User/UserProfile?id=" + offers[j].BidderId);
            minimumOffer = (offers[j].Price + 1).ToString();

            foreach (var item in ListView_Offers.Items)
            {
                offerControl = (OfferControl)item.FindControl("OfferControl");
                bidder = ((HyperLink)offerControl.FindControl("BidderName"));
                bidder.Text = offers[j].BiddrName;
                bidder.NavigateUrl = Page.ResolveUrl("~/PublicPages/User/UserProfile?id=" + offers[j].BidderId);
                ((Label)offerControl.FindControl("BidPrice")).Text = currencyRepo.Exchange(offers[j].Price);
                ((Label)offerControl.FindControl("BidDate")).Text = offers[j].Date.ToString("dd.MM.yyyy hh:mm");
                j++;
            }
        }

        protected void BidBtn_Click(object sender, EventArgs e)
        {
            OffersRepository offersRepo = new OffersRepository(db);
            Debug.WriteLine(Bid.Text);
            offersRepo.Add(MyPresenter.GetCurrentUserId(), auctionId, Convert.ToDecimal(Bid.Text));
            LoadAuctionPage();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(Bid.Text);
        }
    }
}