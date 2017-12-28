using Model;
using Presenter.IPresenters;
using Presenter.IViews;
using Presenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model.RepositoriesDataModel;

namespace Presenter
{
    public class OfferControlPresenter : MasterPresenter, IOfferControlPresenter
    {
        private IAuctionPageView view;
        private IOfferControlView controlView;

        public OfferControlPresenter(IAuctionPageView view, IOfferControlView controlView)
        {
            this.view = view;
            this.controlView = controlView;
        }
    }
}