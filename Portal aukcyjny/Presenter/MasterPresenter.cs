using Model;
using Model.Repositories;
using Model.RepositoriesDataModel;
using Presenter.IPresenters;
using Presenter.IViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Model.Repositories.CurrencyExchangeRepository;

namespace Presenters
{
    public class MasterPresenter : IMasterPresenter
    {
        protected PortalAukcyjnyEntities db;

        protected CurrencyExchangeRepository currencyRepo;
        protected AuctionsRepository auctionsRepo;
        protected CategoriesRepository catRepo;
        protected OffersRepository offersRepo;
        protected UsersRepository usersRepo;
        protected CommentsRepository commentsRepo;
        protected ObserversRepository observersRepo;
        protected ShipmentsRepository shipmentsRepo;
        protected ImagesRepository imagesRepo;
        protected BuyedItemsRepository buyedItemsRepo;

        protected Guid currentUserId;

        protected IMasterView masterView;

        public MasterPresenter(IMasterView masterView = null)
        {
            this.masterView = masterView;

            db = new PortalAukcyjnyEntities();

            if (IsUserLoggedIn())
                currentUserId = GetCurrentUserId();
            else
                currentUserId = Guid.Empty;

            currencyRepo = new CurrencyExchangeRepository(db);
            auctionsRepo = new AuctionsRepository(db);
            catRepo = new CategoriesRepository(db);
            offersRepo = new OffersRepository(db);
            usersRepo = new UsersRepository(db);
            commentsRepo = new CommentsRepository(db);
            observersRepo = new ObserversRepository(db);
            shipmentsRepo = new ShipmentsRepository(db);
            imagesRepo = new ImagesRepository(db);
            buyedItemsRepo = new BuyedItemsRepository(db);
        }

        public bool IsUserLoggedIn()
        {
            return (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        }

        public Guid GetCurrentUserId()
        {
            if (IsUserLoggedIn())
                return new Guid(System.Web.Security.Membership.GetUser().ProviderUserKey.ToString());
            else
                return new Guid();
        }

        public void SetCurrenciesListSource()
        {
            masterView.CurrenciesSource = currencyRepo.GetList();
        }
    }
}