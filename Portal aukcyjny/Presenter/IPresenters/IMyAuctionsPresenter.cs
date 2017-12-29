using Model.RepositoriesDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presenter.IPresenters
{
    public interface IMyAuctionsPresenter
    {
        List<AuctionControlData> Selling { get; set; }
        List<AuctionControlData> Sold { get; set; }
        List<AuctionControlData> Buyed { get; set; }
        List<AuctionControlData> Bidding { get; set; }
        List<AuctionControlData> Observed { get; set; }
        void LoadAuctionsToLists();
    }
}