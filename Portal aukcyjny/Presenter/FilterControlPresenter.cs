using Presenter.IPresenters;
using Presenter.IViews;
using Presenters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Model.RepositoriesDataModel;

namespace Presenter
{
    public class FilterControlPresenter : MasterPresenter, IFilterControlPresenter
    {
        IFilterControlView view;

        public FilterControlPresenter(IFilterControlView view)
        {
            this.view = view;
        }

        public void SetListsSources(string currencyCode)
        {
            view.ShipmentsSource = shipmentsRepo.GetList(currencyRepo, currencyCode);
        }

        public void ResetFilters()
        {
            view.IsBuyItNow = false;
            view.IsBidding = false;
            view.IsMinPrice = false;
            view.IsMaxPrice = false;
            view.IsMinOffersNum = false;
            view.IsMaxOffersNum = false;
            view.IsMinViewsCount = false;
            view.IsMaxViewsCount = false;
            view.IsMaxTimeLeft = false;
            view.IsShipmentType = false;
            view.MinPrice = "";
            view.MaxPrice = "";
            view.MinOffersNum = "";
            view.MaxOffersNum = "";
            view.MinViewsCount = "";
            view.MaxViewsCount = "";
            view.MaxDaysLeft = "";
            view.Search = "";
            view.ShipmentId = "";
        }
    }
}