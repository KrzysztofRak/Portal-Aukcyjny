using Model.RepositoriesDataModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Model.Repositories
{
    public class AuctionsRepository
    {
        private PortalAukcyjnyEntities db;

        public AuctionsRepository(PortalAukcyjnyEntities _db)
        {
            db = _db;
        }

        public AuctionsRepository()
        {
            db = new PortalAukcyjnyEntities();
        }

        public void CloseAuction(Auctions auction)
        {
            auction.Finalized = true;
            auction.EndDate = DateTime.Now;
            db.SaveChanges();
        }

        public void BuyItem(Auctions auction, Guid userId)
        {
            var ac = (from a in db.Auctions where a.Id == auction.Id select a).DefaultIfEmpty(null).FirstOrDefault();
            if (ac == null)
                return;

            if(!ac.Finalized && auction.EndDate > DateTime.Now && ac.ItemsNumber > 0 && userId != ac.OwnerId)
            {
                ac.ItemsNumber--;
                Buyed b = new Buyed() {AuctionId = ac.Id, Date = DateTime.Now, UserId = userId, ItemsNum = 1 };
                db.Buyed.Add(b);
                
                if (ac.ItemsNumber == 0)
                    CloseAuction(ac);
                else
                    db.SaveChanges();
            }
            db.SaveChanges();
        }

        public List<AuctionControlData> GetByCategoryId(int catId = -1)
        {
            var auctionsData =
           (from a in db.Auctions
            where !a.Finalized && a.EndDate > DateTime.Now && (catId == -1 || a.CategoryId == catId)
            join u in db.aspnet_Users on a.OwnerId equals u.UserId
            join t in db.Bidders on a.Id equals t.AuctionId into Bids
            join s in db.Shipments on a.ShipmentId equals s.Id
            select new AuctionControlData()
            {
                Title = a.Title,
                AuctionId = a.Id,
                Image = a.Image,
                BuyItNowPrice = a.BuyItNowPrice,
                BestPrice = Bids.Max(p => p.Price).ToString().Length == 0 ? 0 : Bids.Max(p => p.Price),
                BestPriceUsername = (from o in db.aspnet_Users where o.UserId == Bids.OrderByDescending(p => p.Price).FirstOrDefault().BidderId select o).FirstOrDefault().UserName,//o.UserName,
                MinimumPrice = a.MinimumPrice,
                SellerName = u.UserName,
                SellerId = a.OwnerId,
                ShipmentName = s.Name,
                ShipmentPrice = s.Price,
                EndDate = a.EndDate,
                OffersNum = Bids.Count(),
                Views = a.Views
            }).ToList();

            return auctionsData;
        }

        public List<AuctionControlData> GetByUserId(Guid userId, bool onlyFinished = false)
        {
            var auctions =
           (from a in db.Auctions
            where ((!onlyFinished && !a.Finalized && a.EndDate > DateTime.Now) || (onlyFinished && (a.Finalized || a.EndDate <= DateTime.Now))) && a.OwnerId == userId
            join u in db.aspnet_Users on a.OwnerId equals u.UserId
            join t in db.Bidders on a.Id equals t.AuctionId into Bids
            join s in db.Shipments on a.ShipmentId equals s.Id
            select new AuctionControlData()
            {
                Title = a.Title,
                AuctionId = a.Id,
                Image = a.Image,
                BuyItNowPrice = a.BuyItNowPrice,
                BestPrice = Bids.Max(p => p.Price).ToString().Length == 0 ? 0 : Bids.Max(p => p.Price),
                BestPriceUsername = (from o in db.aspnet_Users where o.UserId == Bids.OrderByDescending(p => p.Price).FirstOrDefault().BidderId select o).FirstOrDefault().UserName,//o.UserName,
                MinimumPrice = a.MinimumPrice,
                SellerName = u.UserName,
                SellerId = a.OwnerId,
                ShipmentName = s.Name,
                ShipmentPrice = s.Price,
                EndDate = a.EndDate,
                OffersNum = Bids.Count(),
                Views = a.Views
            }).ToList();

            return auctions;
        }

        public List<AuctionControlData> GetObserved(Guid userId)
        {
            var auctionsData =
           (from w in db.Observers
            where w.ObserverId == userId
            join a in db.Auctions on new { A = w.AuctionId, B = true } equals new { A = a.Id, B = a.EndDate > DateTime.Now }
            join u in db.aspnet_Users on a.OwnerId equals u.UserId
            join t in db.Bidders on a.Id equals t.AuctionId into Bids
            join s in db.Shipments on a.ShipmentId equals s.Id
            select new AuctionControlData()
            {
                Title = a.Title,
                AuctionId = a.Id,
                Image = a.Image,
                BuyItNowPrice = a.BuyItNowPrice,
                BestPrice = Bids.Max(p => p.Price).ToString().Length == 0 ? 0 : Bids.Max(p => p.Price),
                BestPriceUsername = (from o in db.aspnet_Users where o.UserId == Bids.OrderByDescending(p => p.Price).FirstOrDefault().BidderId select o).FirstOrDefault().UserName,//o.UserName,
                MinimumPrice = a.MinimumPrice,
                SellerName = u.UserName,
                SellerId = a.OwnerId,
                ShipmentName = s.Name,
                ShipmentPrice = s.Price,
                EndDate = a.EndDate,
                OffersNum = Bids.Count(),
                Views = a.Views
            }).ToList();

            return auctionsData;
        }

        public List<AuctionControlData> Search(string searchString)
        {
            try
            {
                var auctions =
               (from a in db.Auctions.Where(au => !au.Finalized && au.EndDate > DateTime.Now && au.Title.Contains(searchString))
                join u in db.aspnet_Users on a.OwnerId equals u.UserId
                join t in db.Bidders on a.Id equals t.AuctionId into Bids
                join s in db.Shipments on a.ShipmentId equals s.Id
                select new AuctionControlData()
                {
                    Title = a.Title,
                    AuctionId = a.Id,
                    Image = a.Image,
                    BuyItNowPrice = a.BuyItNowPrice,
                    BestPrice = Bids.Max(p => p.Price).ToString().Length == 0 ? 0 : Bids.Max(p => p.Price),
                    BestPriceUsername = (from o in db.aspnet_Users where o.UserId == Bids.OrderByDescending(p => p.Price).FirstOrDefault().BidderId select o).FirstOrDefault().UserName,//o.UserName,
                    MinimumPrice = a.MinimumPrice,
                    SellerName = u.UserName,
                    SellerId = a.OwnerId,
                    ShipmentName = s.Name,
                    ShipmentPrice = s.Price,
                    EndDate = a.EndDate,
                    OffersNum = Bids.Count(),
                    Views = a.Views
                }).ToList();

                return auctions;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
            return new List<AuctionControlData>();
        }

        public void Add(Auctions auction)
        {
            db.Auctions.Add(auction);
            db.SaveChanges();
        }

        public Auctions Get(int auctionId)
        {
            var auction = (from p in db.Auctions where p.Id == auctionId select p).DefaultIfEmpty(null).FirstOrDefault();

            return auction;
        }

        public void UpdateViewsNum(Auctions auction)
        {
            auction.Views++;
            db.SaveChanges();
        }
    }



    public class CategoriesRepository
    {
        private PortalAukcyjnyEntities db;

        public CategoriesRepository(PortalAukcyjnyEntities _db)
        {
            db = _db;
        }

        public CategoriesRepository()
        {
            db = new PortalAukcyjnyEntities();
        }

        public List<Categories> GetList()
        {
            var categories = db.Categories.ToList();
            return categories;
        }
    }


    public class OffersRepository
    {
        private PortalAukcyjnyEntities db;

        public OffersRepository(PortalAukcyjnyEntities _db)
        {
            db = _db;
        }

        public OffersRepository()
        {
            db = new PortalAukcyjnyEntities();
        }

        public List<OfferControlData> GetForAuction(int auctionId)
        {
            var offers =
               (from o in db.Bidders
                where o.AuctionId == auctionId
                join u in db.aspnet_Users on o.BidderId equals u.UserId
                select new OfferControlData() { BiddrName = u.UserName, BidderId = u.UserId.ToString(), Price = o.Price, Date = o.BidDate }).OrderByDescending(p => p.Price).ToList();

            return offers;
        }

        public decimal GetBestOffer(int auctionId)
        {
            var offers =
               (from o in db.Bidders
                where o.AuctionId == auctionId
                select o).OrderByDescending(p => p.Price).ToList();

            if (offers.Count > 0)
                return offers.First().Price;
            else
                return 0;
        }

        public void Add(Guid userId, int auctionId, decimal price)
        {
            if (price > GetBestOffer(auctionId))
            {
                db.Bidders.Add(new Bidders { AuctionId = auctionId, BidderId = userId, Price = price, BidDate = DateTime.Now });
                db.SaveChanges();
            }
        }
    }

    public class UsersRepository
    {
        private PortalAukcyjnyEntities db;

        public UsersRepository(PortalAukcyjnyEntities _db)
        {
            db = _db;
        }

        public UsersRepository()
        {
            db = new PortalAukcyjnyEntities();
        }

        public UserProfileData GetUserProfile(Guid userId)
        {
            var user = (from m in db.aspnet_Membership
                        where m.UserId == userId
                        join u in db.aspnet_Users on userId equals u.UserId
                        select new UserProfileData
                        {
                            Username = u.UserName,
                            Email = m.Email,
                            RegistrationDate = m.CreateDate,
                            SoldItemsNum = m.SoldItemsNum, // Potrzebna dodatkowa kolumna w Membership
                        }).First();

            return user;
        }

        public aspnet_Users Get(Guid userId)
        {
            return db.aspnet_Users.Find(userId);
        }
    }

    public class CommentsRepository
    {
        private PortalAukcyjnyEntities db;

        public CommentsRepository(PortalAukcyjnyEntities _db)
        {
            db = _db;
        }

        public CommentsRepository()
        {
            db = new PortalAukcyjnyEntities();
        }

        public List<CommentControlData> GetByUserId(Guid userId)
        {
            var comments =
            (from c in db.Comments
             where c.RecipientId == userId
             join u in db.aspnet_Users on userId equals u.UserId
             join a in db.Auctions on c.AuctionId equals a.Id
             select new CommentControlData()
             {
                 AuthorId = c.AuthorId,
                 AuthorName = u.UserName,
                 AuthorIsSeller = c.AuthorIsSeller,
                 Comment = c.Comment,
                 Date = c.Date,
                 AuctionTitle = a.Title,
                 AuctionId = c.AuctionId
             }).ToList();

            return comments;
        }
    }

    public class ObserversRepository
    {
        private PortalAukcyjnyEntities db;

        public ObserversRepository(PortalAukcyjnyEntities _db)
        {
            db = _db;
        }

        public ObserversRepository()
        {
            db = new PortalAukcyjnyEntities();
        }

        public bool CheckIfAuctionIsObservedByUser(Guid userId, int auctionId)
        {
            var observer = (from o in db.Observers where o.ObserverId == userId && o.AuctionId == auctionId select o).ToList();
            return observer.Count() > 0;
        }

        public void Add(Guid userId, int auctionId)
        {
            if (!db.Observers.Any(o => o.ObserverId == userId && o.AuctionId == auctionId))
            {
                db.Observers.Add(new Observers { AuctionId = auctionId, ObserverId = userId });
                db.SaveChanges();
            }
        }

        public void Delete(Guid userId, int auctionId)
        {
            try
            {
                if (db.Observers.Any(o => o.ObserverId == userId && o.AuctionId == auctionId))
                {
                    var observer = (from o in db.Observers where o.ObserverId == userId && o.AuctionId == auctionId select o).FirstOrDefault();
                    db.Observers.Attach(observer);
                    db.Observers.Remove(observer);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }
    }

    public class ShipmentsRepository
    {
        private PortalAukcyjnyEntities db;

        public ShipmentsRepository(PortalAukcyjnyEntities _db)
        {
            db = _db;
        }

        public ShipmentsRepository()
        {
            db = new PortalAukcyjnyEntities();
        }



        public List<ShipmentsWithFullNames> GetList(CurrencyExchangeRepository currencyRepo, string currencyCode)
        {

            var shipments =
            (from s in db.Shipments select s).ToList();

            var shipmentsWithFullNames = new List<ShipmentsWithFullNames>();

            foreach (var s in shipments)
            {
                shipmentsWithFullNames.Add(new ShipmentsWithFullNames() { Id = s.Id, Name = s.Name + " " + currencyRepo.Exchange(s.Price, currencyCode) });
            }

            return shipmentsWithFullNames;
        }

        public string GetShipmentFullName(CurrencyExchangeRepository currencyRepo, int shipmentId)
        {
            var shipment =
            (from s in db.Shipments
             where s.Id == shipmentId
             select s).First();

            var shipmentName = shipment.Name + " " + currencyRepo.Exchange(shipment.Price);

            return shipmentName;
        }
    }
}