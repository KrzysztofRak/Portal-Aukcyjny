using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Presenters.IViews;
using Presenters.IPresenters;
using Model;
using Model.Repositories;
using Model.RepositoriesDataModel;

namespace Presenters
{
    public class DefaultPresenter : MasterPresenter, IDefaultPresenter
    {
        private IDefaultView view;

        public DefaultPresenter(IDefaultView view)
        {
            this.view = view;
        }

        public List<Categories> GetCategoriesList()
        {
            var categories = catRepo.GetList();

            return categories;
        }

        public List<AuctionControlData> GetAuctionsList()
        {
            if (view.SearchString != null)
                return auctionsRepo.Search(view.SearchString);
            else
                return auctionsRepo.GetByCategoryId(view.CatId);
        }
    }
}