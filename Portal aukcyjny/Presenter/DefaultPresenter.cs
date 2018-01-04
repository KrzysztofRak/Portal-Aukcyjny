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

        public void LoadCategoriesTree()
        {
            var categories = catRepo.GetList();
            foreach (var cat in categories)
                view.AddNewItemToCategoriesTree(cat.Name, cat.Id);
        }
    }
}