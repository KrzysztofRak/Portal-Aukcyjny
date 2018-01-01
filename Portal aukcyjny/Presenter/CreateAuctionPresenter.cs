using Model;
using Presenter.IPresenters;
using Presenter.IViews;
using Presenters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Debug.WriteLine("Cat ID: " + view.CategoryId);
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

                BuyItNowPrice = -1,
                MinimumPrice = -1,

                CategoryId = int.Parse(view.CategoryId),
                ShipmentId = int.Parse(view.ShipmentId)
            };

            if (view.IsBuyItNow)
                auction.BuyItNowPrice = decimal.Parse(view.BuyItNowPriceField);
            if (view.IsBiddable)
                auction.MinimumPrice = decimal.Parse(view.StartPriceField);

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