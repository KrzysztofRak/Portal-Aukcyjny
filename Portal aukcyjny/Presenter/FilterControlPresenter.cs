using Presenter.IPresenters;
using Presenter.IViews;
using Presenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presenter
{
    public class FilterControlPresenter : MasterPresenter, IFilterControlPresenter
    {
        IFilterControlView view;

        public FilterControlPresenter(IFilterControlView view)
        {
            this.view = view;
        }
    }
}