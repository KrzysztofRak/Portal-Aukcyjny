using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Presenter.IViews;
using Presenter.IPresenters;
using Model;
using Model.Repositories;

namespace Presenter
{
    public class DefaultPresenter //: IDefaultPresenter
    {
        private IDefaultView view;
        private PortalAukcyjnyEntities db;

        public DefaultPresenter(IDefaultView view)
        {
            this.view = view;
            db = new PortalAukcyjnyEntities();
        }

        public List<Categories> GetCategoriesList()
        {
            CategoriesRepository catRepo = new CategoriesRepository(db);
            var categories = catRepo.GetCategoriesList();

            return categories;
        }
    }
}