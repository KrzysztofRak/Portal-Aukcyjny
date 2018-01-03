using Model.RepositoriesDataModel;
using Presenter.IPresenters;
using Presenter.IViews;
using Presenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presenter
{
    public class MyAuctionsPresenter : MasterPresenter, IMyAuctionsPresenter
    {
        private IMyAuctionsView view;

        public MyAuctionsPresenter(IMyAuctionsView view)
        {
            this.view = view;
        }
    }
}