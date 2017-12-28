using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presenter.IViews
{
    public interface IAuctionPageView
    {
        int AuctionId { get; set; }
        string SellerId { get; set; }
        string BestBidderId { get; set; }
        string BidderId { get; set; }
        string MinimumOffer { get; set; }
        string ObserveBtnText { get; set; }
        bool ObserveBtnVisiblity { get; set; }
        EventHandler ObserveBtnEvent { get; set; }
        string AuctionTitleField { get; set; }
        string AuctionImgUrl { get; set; }
        string TimeLeftField { get; set; }
        string ItemsNumField { get; set; }
        string BuyItNowPriceField { get; set; }
        bool BuyItNowPriceFieldVisiblity { get; set; }
        bool BuyItNowLabelVisiblity { get; set; }
        bool BuyItNowBtnVisiblity { get; set; }
        string HighestBidField { get; set; }
        bool HighestBidFieldVisiblity { get; set; }
        string BestBidUserNameField { get; set; }
        bool BestBidUserNameFieldVisiblity { get; set; }
        string BestBidUserNameNavUrl { get; set; }
        string BestBidLabelText { get; set; }
        bool BestBidLabelVisiblity { get; set; }
        bool BestBidUserNameLabelVisiblity { get; set; }
        string BidField { get; set; }
        bool BidFieldVisiblity { get; set; }
        bool BidBtnVisiblity { get; set; }
        string ShipmentField { get; set; }
       string SellerNameField { get; set; }
        string SellerNameNavUrl { get; set; }
        string DescriptionField { get; set; }
        string ViewsNumField { get; set; }

        void LoadOffersControls(int offersNum);
    }
}