using Model.RepositoriesDataModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            if (!ac.Finalized && auction.EndDate > DateTime.Now && ac.ItemsNumber > 0 && userId != ac.OwnerId)
            {
                ac.ItemsNumber--;
                Buyed b = new Buyed() { AuctionId = ac.Id, Date = DateTime.Now, UserId = userId, ItemsNum = 1 };
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

        public List<AuctionControlData> GetSelling(Guid userId)
        {
            var auctions =
           (from a in db.Auctions
            where (!a.Finalized && a.EndDate > DateTime.Now) && a.OwnerId == userId
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

        public List<AuctionControlData> GetBuyed(Guid userId)
        {
            var auctionsData =
           (from b in db.Buyed
            where b.UserId == userId
            join a in db.Auctions on b.AuctionId equals a.Id
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

        public List<AuctionControlData> GetSold(Guid userId)
        {
            var auctionsData =
           (from a in db.Auctions
            where a.OwnerId == userId
            join b in db.Buyed on a.Id equals b.AuctionId
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

        public List<AuctionControlData> GetBidding(Guid userId)
        {
            var auctionsData =
           (from b in db.Bidders
            where b.BidderId == userId
            join a in db.Auctions on b.AuctionId equals a.Id
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

        public List<AuctionControlData> SearchByFilters(SearchFilters filters)
        {
            
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
}
