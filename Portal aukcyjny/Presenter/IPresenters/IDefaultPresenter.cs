using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;
using Model.RepositoriesDataModel;
using Presenters.IViews;

namespace Presenters.IPresenters
{
    public interface IDefaultPresenter
    {
        List<Categories> GetCategoriesList();
        List<AuctionControlData> GetAuctionsList();
    }
}