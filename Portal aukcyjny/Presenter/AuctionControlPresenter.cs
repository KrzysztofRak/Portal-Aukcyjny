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

        public void LoadSelling()
        {
            auctions = auctionsRepo.GetByUserId(GetCurrentUserId());
        }

        public void LoadSold()
        {
            auctions = auctionsRepo.GetByUserId(GetCurrentUserId(), true);
        }

        public void LoadBuyed()
        {
            auctions = auctionsRepo.GetObserved(GetCurrentUserId());
        }

        public void LoadBidding()
        {
            auctions = auctionsRepo.GetAuctioned(GetCurrentUserId());
        }

        public void LoadObserved()
        {
            auctions = auctionsRepo.GetAuctioned(GetCurrentUserId());
        }

        public void LoadByUserId(Guid userId)
        {
            auctions = auctionsRepo.GetByUserId(userId);
        }

        public IAuctionControlView SetControl(IAuctionControlView ac, int j)
        {
            ac.TitleField = auctions[j].Title;

            if (auctions[j].Image == null)
                ac.ImageUrl = "~/Images/defaultAuctionImg.jpg";
            else
                ac.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(auctions[j].Image);

            if (auctions[j].BuyItNowPrice > 0)
            {
                ac.BuyItNowField = currencyRepo.Exchange(auctions[j].BuyItNowPrice, currencyCode);
                ac.BuyItNowVisiblity = true;
                ac.BuyItNowLabelVisiblity = true;
            }


            if (auctions[j].MinimumPrice.ToString() != null)
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
                ac.TimeLeftLabelText = "Zakończono: ";
                ac.TimeLeftField = auctions[j].EndDate.ToString("dd.MM.yyyy hh:mm");
            }
            else if (leftDateTime.TotalDays > 1)
                ac.TimeLeftField = (int)leftDateTime.TotalDays + " dni";
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