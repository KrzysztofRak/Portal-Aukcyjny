using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Portal_aukcyjny.Repositories
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

        public List<AuctionControlData> GetByCategoryId(int catId = -1)
        {
            var auctionsData =
           (from a in db.Auctions
            where !a.Finalized && a.EndDate > DateTime.Now && (catId == -1 || a.CategoryId == catId)
            join u in db.aspnet_Users on a.OwnerId equals u.UserId
            join b in db.Bidders on a.Id equals b.AuctionId into Bids
            join s in db.Shipments on a.ShipmentId equals s.Id
            select new AuctionControlData()
            {
                Title = a.Title,
                AuctionId = a.Id,
                Image = a.Image,
                BuyItNowPrice = a.BuyItNowPrice,
                CurrentPrice = Bids.Max(p => p.Price).ToString(),
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
            join b in db.Bidders on a.Id equals b.AuctionId into Bids
            join s in db.Shipments on a.ShipmentId equals s.Id
            select new AuctionControlData()
            {
                Title = a.Title,
                AuctionId = a.Id,
                Image = a.Image,
                BuyItNowPrice = a.BuyItNowPrice,
                CurrentPrice = Bids.Max(p => p.Price).ToString(),
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
           (from o in db.Observers
            where o.ObserverId == userId
            join a in db.Auctions on new { A = o.AuctionId, B = true } equals new { A = a.Id, B = a.EndDate > DateTime.Now }
            join u in db.aspnet_Users on a.OwnerId equals u.UserId
            join b in db.Bidders on a.Id equals b.AuctionId into Bids
            join s in db.Shipments on a.ShipmentId equals s.Id
            select new AuctionControlData()
            {
                Title = a.Title,
                AuctionId = a.Id,
                Image = a.Image,
                BuyItNowPrice = a.BuyItNowPrice,
                CurrentPrice = Bids.Max(p => p.Price).ToString(),
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

        public List<AuctionControlData> GetAuctioned(Guid userId)
        {
            var auctionsData =
           (from b in db.Bidders
            where b.BidderId == userId
            join a in db.Auctions on new { A = b.AuctionId, B = true } equals new { A = a.Id, B = a.EndDate > DateTime.Now }
            join u in db.aspnet_Users on a.OwnerId equals u.UserId
            join s in db.Shipments on a.ShipmentId equals s.Id
            join bid in db.Bidders on a.Id equals bid.AuctionId into Bids
            select new AuctionControlData()
            {
                Title = a.Title,
                AuctionId = a.Id,
                Image = a.Image,
                BuyItNowPrice = a.BuyItNowPrice,
                CurrentPrice = Bids.Max(p => p.Price).ToString(),
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
                join b in db.Bidders on a.Id equals b.AuctionId into Bids
                join s in db.Shipments on a.ShipmentId equals s.Id
                select new AuctionControlData()
                {
                    Title = a.Title,
                    AuctionId = a.Id,
                    Image = a.Image,
                    BuyItNowPrice = a.BuyItNowPrice,
                    CurrentPrice = Bids.Max(p => p.Price).ToString(),
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

        public void UpdateViews(Auctions auction)
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

        public List<Categories> GetCategoriesList()
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
                select new OfferControlData() { BiddrName = u.UserName, BidderId = u.UserId.ToString(), Price = o.Price.ToString(), Date = o.BidDate.ToString() }).ToList();

            return offers;
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
             select new CommentControlData()
             {
                 AuthorId = c.AuthorId,
                 AuthorName = u.UserName,
                 AuthorIsSeller = c.AuthorIsSeller,
                 Comment = c.Comment,
                 Date = c.Date
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

        public void Add(Guid userId, int auctionId)
        {
            db.Observers.Add(new Observers { AuctionId = auctionId, ObserverId = userId });
            db.SaveChanges();
        }

        public void Delete(Guid userId, int auctionId)
        {
            db.Observers.Remove(new Observers { AuctionId = auctionId, ObserverId = userId });
            db.SaveChanges();
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

        public string GetShipmentName(int shipmentId)
        {
            var shipmentName =
            (from s in db.Shipments
             where s.Id == shipmentId
             select s.Name + " " + s.Price).DefaultIfEmpty("").FirstOrDefault();

            return shipmentName;
        }
    }
}