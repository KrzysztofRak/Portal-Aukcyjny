using Model.RepositoriesDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presenter.IViews
{
    public interface IAuctionControlView
    {
        string TitleField { get; set; }
        string AuctionUrl { get; set; }
        string ImageUrl { get; set; }
        string BuyItNowField { get; set; }
        bool BuyItNowVisiblity { set; }
        bool BuyItNowLabelVisiblity { set; }
        string BidField { get; set; }
        bool BidVisiblity { get; set; }
        bool BidLabelVisiblity { get; set; }
        string SellerField { get; set; }
        string SellerNavUrl { get; set; }
        string ShipmentField { get; set; }
        string TimeLeftField { get; set; }
        string TimeLeftLabelText { get; set; }
        string OffersNumField { get; set; }
        string ViewsField { get; set; }
        void LoadAuctionsBySearch(string searchString);
        void LoadAuctionsByCatId(int catId);
        void LoadSold();
        void LoadBuyed();
        void LoadBidding();
        void LoadObserved();
        void LoadSelling(Guid userId);
    }
}