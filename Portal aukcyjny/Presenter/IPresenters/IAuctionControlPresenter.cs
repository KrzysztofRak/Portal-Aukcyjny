using Model.RepositoriesDataModel;
using Presenter.IViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presenter.IPresenters
{
    public interface IAuctionControlPresenter
    {
        IAuctionControlView SetControl(AuctionControlData auction, IAuctionControlView ac);
    }
}