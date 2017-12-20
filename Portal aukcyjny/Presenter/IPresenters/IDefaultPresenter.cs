using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Presenter.IViews;

namespace Presenter.IPresenters
{
    public interface IDefaultPresenter
    {
         void GetCategoriesList();
         void LoadAuctionControls();
    }
}