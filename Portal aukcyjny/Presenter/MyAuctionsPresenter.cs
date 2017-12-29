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

        private List<AuctionControlData> selling;
        private List<AuctionControlData> sold;
        private List<AuctionControlData> buyed;
        private List<AuctionControlData> bidding;
        private List<AuctionControlData> observed;

        public List<AuctionControlData> Selling
        {
            get { return selling; }
            set { selling = value; }
        }

        public List<AuctionControlData> Sold
        {
            get { return sold; }
            set { sold = value; }
        }

        public List<AuctionControlData> Buyed
        {
            get { return buyed; }
            set { buyed = value; }
        }

        public List<AuctionControlData> Bidding
        {
            get { return bidding; }
            set { bidding = value; }
        }

        public List<AuctionControlData> Observed
        {
            get { return observed; }
            set { observed = value; }
        }

        public MyAuctionsPresenter(IMyAuctionsView view)
        {
            this.view = view;
        }

        public void LoadAuctionsToLists()
        {
            Guid userId = GetCurrentUserId();
            Selling = auctionsRepo.GetByUserId(userId);
            Sold = auctionsRepo.GetByUserId(userId, true);
            Buyed = auctionsRepo.GetObserved(userId);
            Bidding = auctionsRepo.GetAuctioned(userId);
            Observed = auctionsRepo.GetAuctioned(userId);
        }
    }
}