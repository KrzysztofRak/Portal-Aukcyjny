using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presenter.IViews
{
    public interface IOfferControlView
    {
        string BidderNameField { get; set; }
        string BidderNameNavUrl { get; set; }
        string BidPriceField { get; set; }
        string BidDateField { get; set; }
    }
}