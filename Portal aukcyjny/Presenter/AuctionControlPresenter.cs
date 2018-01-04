using Extensions;
using Model.RepositoriesDataModel;
using Presenter.IPresenters;
using Presenter.IViews;
using Presenters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Presenter
{
    public class AuctionControlPresenter : MasterPresenter, IAuctionControlPresenter
    {
        private IAuctionControlView view;
        private string currencyCode;
        private List<AuctionControlData> auctions;

        public AuctionControlPresenter(IAuctionControlView view, string currencyCode)
        {
            this.view = view;
            this.currencyCode = currencyCode;
        }

        public int GetAuctionsNum()
        {
            return auctions.Count();
        }

        public void LoadAuctionsBySearch(string searchString)
        {
            auctions = auctionsRepo.Search(searchString);
        }

        public void LoadAuctionsByCatId(int catId)
        {
            auctions = auctionsRepo.GetByCategoryId(catId);
        }

        public void LoadSold()
        {
            auctions = auctionsRepo.GetSold(GetCurrentUserId());
        }

        public void LoadBuyed()
        {
            auctions = auctionsRepo.GetBuyed(GetCurrentUserId());
        }

        public void LoadBidding()
        {
            auctions = auctionsRepo.GetBidding(GetCurrentUserId());
        }

        public void LoadObserved()
        {
            auctions = auctionsRepo.GetObserved(GetCurrentUserId());
        }

        public void LoadSelling(Guid userId)
        {
            auctions = auctionsRepo.GetSelling(userId);
        }

        public void LoadByFilters(IFilterControlView filterView, int catId)
        {
            SearchFilters filters = new SearchFilters()
            {
                IsBuyItNow = filterView.IsBuyItNow,
                IsBidding = filterView.IsBidding,
                IsMinPrice = filterView.IsMinPrice,
                IsMaxPrice = filterView.IsMaxPrice,
                IsMinOffersNum = filterView.IsMinOffersNum,
                IsMaxOffersNum = filterView.IsMaxOffersNum,
                IsMinViewsCount = filterView.IsMinViewsCount,
                IsMaxViewsCount = filterView.IsMaxViewsCount,
                IsMaxTimeLeft = filterView.IsMaxTimeLeft,
                IsShipmentType = filterView.IsShipmentType,
                Search = filterView.Search,
                CatId = catId
            };

            filters.ShipmentId = ExtensionMethod.TryIntParse(filterView.ShipmentId);
            filters.MinPrice = ExtensionMethod.TryIntParse(filterView.MinPrice);
            filters.MaxPrice = ExtensionMethod.TryIntParse(filterView.MaxPrice);
            filters.MinOffersNum = ExtensionMethod.TryIntParse(filterView.MinOffersNum);
            filters.MaxOffersNum = ExtensionMethod.TryIntParse(filterView.MaxOffersNum);
            filters.MinViewsCount = ExtensionMethod.TryIntParse(filterView.MinViewsCount);
            filters.MaxViewsCount = ExtensionMethod.TryIntParse(filterView.MaxViewsCount);
            filters.MaxDaysLeft = ExtensionMethod.TryIntParse(filterView.MaxDaysLeft);

            auctions = auctionsRepo.SearchWithFilters(filters);
        }

        public IAuctionControlView SetControl(IAuctionControlView ac, int j)
        {
            ac.TitleField = auctions[j].Title;


            ac.ImageUrl = "image?auctionId=" + auctions[j].AuctionId +
                          "&width=" + ac.ImageWidth + "&height=" + ac.ImageHeight;

            if (auctions[j].BuyItNowPrice > 0)
            {
                ac.BuyItNowField = currencyRepo.Exchange(auctions[j].BuyItNowPrice, currencyCode);
                ac.BuyItNowVisiblity = true;
                ac.BuyItNowLabelVisiblity = true;
            }


            if (auctions[j].MinimumPrice != -1)
            {
                ac.BidField = currencyRepo.Exchange(auctions[j].MinimumPrice, currencyCode);
                ac.BidVisiblity = true;
                ac.BidLabelVisiblity = true;
            }

            ac.SellerField = auctions[j].SellerName;

            ac.ShipmentField = auctions[j].ShipmentName + " " + currencyRepo.Exchange(auctions[j].ShipmentPrice, currencyCode);

            var leftDateTime = (auctions[j].EndDate.Subtract(DateTime.Now));

            if (leftDateTime.TotalMinutes < 0)
            {
                ac.TimeLeftLabelText = "Zakończono: ";//view.ResFinished;
                ac.TimeLeftField = auctions[j].EndDate.ToString("dd.MM.yyyy hh:mm");
            }
            else if (leftDateTime.TotalDays > 1)
                ac.TimeLeftField = (int) leftDateTime.TotalDays + " dni";// + view.ResDays;
            else
                ac.TimeLeftField = string.Format("{0:hh\\:mm\\:ss}", leftDateTime);

            ac.OffersNumField = auctions[j].OffersNum.ToString();
            ac.ViewsField = auctions[j].Views.ToString();

            ac.AuctionUrl += auctions[j].AuctionId;
            ac.SellerNavUrl += auctions[j].SellerId;
            return ac;
        }
    }
}