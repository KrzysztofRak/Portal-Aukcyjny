using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Presenters.IViews;

namespace Presenters.IPresenters
{
    public interface IDefaultPresenter
    {
         void GetCategoriesList();
         void LoadAuctionControls();
    }
}