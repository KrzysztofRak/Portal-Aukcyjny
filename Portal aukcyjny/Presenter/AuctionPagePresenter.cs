using Model;
using Model.RepositoriesDataModel;
using Presenter.IPresenters;
using Presenter.IViews;
using Presenters;
using Presenters.IViews;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Presenter
{
    public class AuctionPagePresenter : MasterPresenter, IAuctionPagePresenter
    {
        private IAuctionPageView view;
        private List<OfferControlData> offers;
        private string currencyCode;
        private Auctions auction;

        public AuctionPagePresenter(IAuctionPageView view, string currencyCode)
        {
            this.view = view;
            this.currencyCode = currencyCode;
        }

        public void StartObserve(object sender, EventArgs e)
        {
            observersRepo.Add(GetCurrentUserId(), view.AuctionId);
            view.ObserveBtnText = "Przestań obserwować";
            view.ObserveBtnEvent -= StartObserve;
            view.ObserveBtnEvent += StopObserve;
        }

        public void StopObserve(object sender, EventArgs e)
        {
            observersRepo.Delete(GetCurrentUserId(), view.AuctionId);
            view.ObserveBtnText = "Obserwuj";
            view.ObserveBtnEvent -= StopObserve;
            view.ObserveBtnEvent += StartObserve;
        }

        public bool LoadAuctionPage()
        {
            auction = auctionsRepo.Get(view.AuctionId);
            if (auction == null)
                return false;

            auctionsRepo.UpdateViewsNum(auction);

            view.AuctionTitleField = auction.Title;

            if (auction.Image == null)
                view.AuctionImgUrl = "~/Images/defaultAuctionImg.jpg";
            else
                view.AuctionImgUrl = "data:image/jpeg;base64," + Convert.ToBase64String(auction.Image);

            if (auction.Finalized)
            {
                view.ItemsNumLabelVisiblity = false;
                view.ItemsNumFieldVisiblity = false;
            }
            else
                view.ItemsNumField = auction.ItemsNumber.ToString();

            var timeLeft = (auction.EndDate.Subtract(DateTime.Now));

            if (timeLeft.TotalMinutes < 0)
            {
                view.TimeLeftLabelText = "Zakończona: ";
                view.TimeLeftField = auction.EndDate.ToString("dd.MM.yyyy hh:mm");
            }
            else if (timeLeft.TotalDays > 1)
                view.TimeLeftField = (int)timeLeft.TotalDays + " dni";
            else
                view.TimeLeftField = String.Format("{0:hh\\:mm\\:ss}", timeLeft);

            if (!IsUserLoggedIn() || GetCurrentUserId() == auction.OwnerId)
            {
                view.ObserveBtnVisiblity = false;

                if (!auction.Finalized && GetCurrentUserId() == auction.OwnerId)
                    view.CloseAuctionBtnVisiblity = true;
            }
            else
            {
                if (!observersRepo.CheckIfAuctionIsObservedByUser(GetCurrentUserId(), auction.Id))
                {
                    view.ObserveBtnText = "Obserwuj";
                    view.ObserveBtnEvent -= StopObserve;
                    view.ObserveBtnEvent += StartObserve;
                }
                else
                {
                    view.ObserveBtnText = "Przestań obserwować";
                    view.ObserveBtnEvent -= StartObserve;
                    view.ObserveBtnEvent += StopObserve;
                }
            }

            var seller = usersRepo.Get(auction.OwnerId);
            view.SellerId = seller.UserId.ToString();
            view.SellerNameField = seller.UserName;
            view.DescriptionField = auction.Description;
            view.ViewsNumField = auction.Views.ToString();

            view.ShipmentField = shipmentsRepo.GetShipmentFullName(currencyRepo, auction.ShipmentId);

            Debug.WriteLine(auction.BuyItNowPrice);

            if (auction.BuyItNowPrice != -1)
            {
                view.BuyItNowLabelVisiblity = true;
                view.BuyItNowPriceFieldVisiblity = true;
                view.BuyItNowBtnVisiblity = true;

                view.BuyItNowPriceField = currencyRepo.Exchange(auction.BuyItNowPrice, currencyCode);
            }


            if (auction.MinimumPrice != -1)
            {
                view.HighestBidField = currencyRepo.Exchange(auction.MinimumPrice, currencyCode);
                view.MinimumOffer = currencyRepo.ExchangeWithoutCC((auction.MinimumPrice + 1), currencyCode).ToString();
                LoadOffers(auction);
            }

            if (!IsUserLoggedIn() || GetCurrentUserId() == auction.OwnerId || auction.Finalized)
            {
                view.BuyItNowBtnVisiblity = false;
                view.BidFieldVisiblity = false;
                view.BidBtnVisiblity = false;
            }

            return true;
        }

        private void LoadOffers(Auctions auction)
        {
            offers = offersRepo.GetForAuction(view.AuctionId);

            view.BestBidLabelVisiblity = true;
            view.HighestBidFieldVisiblity = true;
            view.BestBidUserNameLabelVisiblity = true;
            view.BidFieldVisiblity = true;
            view.BidBtnVisiblity = true;

            if (offers.Count() == 0)
            {
                view.BestBidLabelText = "Licytacja od: ";
                view.BestBidUserNameLabelVisiblity = false;
                return;
            }

            view.BestBidUserNameFieldVisiblity = true;
            if (offers[0].Price >= auction.BuyItNowPrice)
            {
                view.BuyItNowLabelVisiblity = false;
                view.BuyItNowPriceFieldVisiblity = false;
                view.BuyItNowBtnVisiblity = false;
            }

            view.HighestBidField = currencyRepo.Exchange(offers[0].Price, currencyCode);
            view.BestBidUserNameField = offers[0].BiddrName;
            view.BestBidderId = offers[0].BidderId;
            view.MinimumOffer = currencyRepo.ExchangeWithoutCC((offers[0].Price + 1), currencyCode).ToString();

            view.LoadOffersControls(offers.Count());
        }

        public void SetControl(IOfferControlView oc, int j)
        {
            oc.BidderNameField = offers[j].BiddrName;
            oc.BidPriceField = currencyRepo.Exchange(offers[j].Price, currencyCode);
            oc.BidDateField = offers[j].Date.ToString("dd.MM.yyyy hh:mm");
            view.BidderId = offers[j].BidderId;
        }

        public void SendBid()
        {
            offersRepo.Add(GetCurrentUserId(), view.AuctionId, currencyRepo.ExchangeWithoutCC(Convert.ToDecimal(view.BidField), currencyCode, true));
            LoadAuctionPage();
        }

        public void Buy()
        {
            offersRepo.Add(GetCurrentUserId(), auction.Id, auction.BuyItNowPrice);
            auctionsRepo.BuyItem(auction, GetCurrentUserId());
            LoadAuctionPage();
        }

        public void CloseAuction()
        {
            auctionsRepo.CloseAuction(auction);
            LoadAuctionPage();
        }
    }
}