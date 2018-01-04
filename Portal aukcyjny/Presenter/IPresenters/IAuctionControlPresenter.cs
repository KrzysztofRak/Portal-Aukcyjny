using Model.RepositoriesDataModel;
using Presenter.IViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presenter.IPresenters
{
    public interface IAuctionControlPresenter
    {
        int GetAuctionsNum();
        void LoadAuctionsBySearch(string searchString);
        void LoadAuctionsByCatId(int catId);
        void LoadSold();
        void LoadBuyed();
        void LoadBidding();
        void LoadObserved();
        void LoadSelling(Guid userId);
        void LoadByFilters(IFilterControlView filterView, int catId);
        IAuctionControlView SetControl(IAuctionControlView ac, int j);
    }
}