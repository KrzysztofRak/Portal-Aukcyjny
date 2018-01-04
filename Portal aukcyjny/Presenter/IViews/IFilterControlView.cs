using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presenter.IViews
{
    public interface IFilterControlView
    {
        object ShipmentsSource { get; set; }
        bool IsBuyItNow { get; set; }
        bool IsBidding { get; set; }
        bool IsMinPrice { get; set; }
        bool IsMaxPrice { get; set; }
        bool IsMinOffersNum { get; set; }
        bool IsMaxOffersNum { get; set; }
        bool IsMinViewsCount { get; set; }
        bool IsMaxViewsCount { get; set; }
        bool IsMaxTimeLeft { get; set; }
        bool IsShipmentType { get; set; }
        string MinPrice { get; set; }
        string MaxPrice { get; set; }
        string MinOffersNum { get; set; }
        string MaxOffersNum { get; set; }
        string MinViewsCount { get; set; }
        string MaxViewsCount { get; set; }
        string MaxDaysLeft { get; set; }
        string Search { get; set; }
        string ShipmentId { get; }
    }
}
