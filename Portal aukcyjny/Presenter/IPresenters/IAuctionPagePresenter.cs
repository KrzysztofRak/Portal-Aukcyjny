using Presenter.IViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presenter.IPresenters
{
    public interface IAuctionPagePresenter
    {
        void StartObserve(object sender, EventArgs e);
        void StopObserve(object sender, EventArgs e);
        bool LoadAuctionPage();
        void SetControl(IOfferControlView oc, int j);
        void CloseAuction();

    }
}