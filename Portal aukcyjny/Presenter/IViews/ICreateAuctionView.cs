using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presenter.IViews
{
    public interface ICreateAuctionView
    {
        string AuctionTitleField { get; set; }
        byte[] ImageFileBytes { get; set; }
        string ItemsNumberField { get; set; }
        bool IsBuyItNow { get; set; }
        bool IsBiddable { get; set; }
        string BuyItNowPriceField { get; set; }
        string StartPriceField { get; set; }
        string LocationField { get; set; }
        string ShipmentId { get; set; }
        string CategoryId { get; set; }
        string TimeLeftField { get; set; }
        string ItemDescriptionField { get; set; }
        object ShipmentsSource { get; set; }
        object CategoriesSource { get; set; }
    }
}
