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

        public AuctionControlPresenter(IAuctionControlView view, string currencyCode)
        {
            this.view = view;
            this.currencyCode = currencyCode;
        }

        public IAuctionControlView SetControl(AuctionControlData auction, IAuctionControlView ac)
        {
            ac.TitleField = auction.Title;

            if (auction.Image == null)
                ac.ImageUrl = "~/Images/defaultAuctionImg.jpg";
            else
                ac.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(auction.Image);

            if (auction.BuyItNowPrice > 0)
            {
                ac.BuyItNowField = auction.BuyItNowPrice.ToString();
                ac.BuyItNowVisiblity = true;
                ac.BuyItNowLabelVisiblity = true;
            }


            if (auction.MinimumPrice.ToString() != null)
            {
                ac.BidField = currencyRepo.Exchange(auction.MinimumPrice, currencyCode);
                ac.BidVisiblity = true;
                ac.BidLabelVisiblity = true;
            }

            ac.SellerField = auction.SellerName;

            ac.ShipmentField = auction.ShipmentName + " " + currencyRepo.Exchange(auction.ShipmentPrice, currencyCode);

            var leftDateTime = (auction.EndDate.Subtract(DateTime.Now));

            if (leftDateTime.TotalMinutes < 0)
            {
                ac.TimeLeftLabelText = "Zakończono: ";
                ac.TimeLeftField = auction.EndDate.ToString("dd.MM.yyyy hh:mm");
            }
            else if (leftDateTime.TotalDays > 1)
                ac.TimeLeftField = (int)leftDateTime.TotalDays + " dni";
            else
                ac.TimeLeftField = String.Format("{0:hh\\:mm\\:ss}", leftDateTime);

            ac.OffersNumField = auction.OffersNum.ToString();
            ac.ViewsField = auction.Views.ToString();

            return ac;
        }
    }
}