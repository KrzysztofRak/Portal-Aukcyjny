using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Presenters.IViews;
using Presenters.IPresenters;
using Model;
using Model.Repositories;

namespace Presenters
{
    public class DefaultPresenter : MasterPresenter//: IDefaultPresenter
    {
        private IDefaultView view;

        public DefaultPresenter(IDefaultView view)
        {
            this.view = view;
        }

        public List<Categories> GetCategoriesList()
        {
            var categories = catRepo.GetCategoriesList();

            return categories;
        }
    }
}