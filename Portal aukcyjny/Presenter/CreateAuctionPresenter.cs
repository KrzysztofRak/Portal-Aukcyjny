using Model;
using Presenter.IPresenters;
using Presenter.IViews;
using Presenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presenter
{
    public class CreateAuctionPresenter : MasterPresenter, ICreateAuctionPresenter
    {
        private ICreateAuctionView view;

        public CreateAuctionPresenter(ICreateAuctionView view)
        {
            this.view = view;
        }

        public int CreateAuction()
        {
            Auctions auction = new Auctions
            {
                OwnerId = GetCurrentUserId(),
                Views = 0,
                Finalized = false,

                Title = view.AuctionTitleField,
                Image = view.ImageFileBytes,
                ItemsNumber = int.Parse(view.ItemsNumberField),

                Location = view.LocationField,
                EndDate = DateTime.Now.AddDays(int.Parse(view.TimeLeftField)),
                Description = view.ItemsNumberField,

                CategoryId = int.Parse(view.CategoryId),
                ShipmentId = int.Parse(view.ShipmentId)
            };

            auctionsRepo.Add(auction);

            return auction.Id;
        }

        public void SetSources(string currencyCode)
        {
            view.ShipmentsSource = shipmentsRepo.GetList(currencyRepo, currencyCode);
            view.CategoriesSource = catRepo.GetList();
        }
    }
}