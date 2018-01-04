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
            join i in db.Images on a.Id equals i.AuctionId
            select new AuctionControlData()
            {
                Title = a.Title,
                AuctionId = a.Id,
                Image = i.ImageData,
                BuyItNowPrice = a.BuyItNowPrice,
                BestPrice = Bids.Max(p => p.Price).ToString().Length == 0 ? 0 : Bids.Max(p => p.Price),
                BestPriceUsername = (from o in db.aspnet_Users where o.UserId == Bids.OrderByDescending(p => p.Price).FirstOrDefault().BidderId select o).FirstOrDefault().UserName,//o.UserName,
                MinimumPrice = a.MinimumPrice,
                SellerName = u.UserName,
                SellerId = a.OwnerId,
                ShipmentName = s.Name,
                ShipmentId = a.ShipmentId,
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
            join i in db.Images on a.Id equals i.AuctionId
            select new AuctionControlData()
            {
                Title = a.Title,
                AuctionId = a.Id,
                Image = i.ImageData,
                BuyItNowPrice = a.BuyItNowPrice,
                BestPrice = Bids.Max(p => p.Price).ToString().Length == 0 ? 0 : Bids.Max(p => p.Price),
                BestPriceUsername = (from o in db.aspnet_Users where o.UserId == Bids.OrderByDescending(p => p.Price).FirstOrDefault().BidderId select o).FirstOrDefault().UserName,//o.UserName,
                MinimumPrice = a.MinimumPrice,
                SellerName = u.UserName,
                SellerId = a.OwnerId,
                ShipmentName = s.Name,
                ShipmentId = a.ShipmentId,
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
            join i in db.Images on a.Id equals i.AuctionId
            select new AuctionControlData()
            {
                Title = a.Title,
                AuctionId = a.Id,
                Image = i.ImageData,
                BuyItNowPrice = a.BuyItNowPrice,
                BestPrice = Bids.Max(p => p.Price).ToString().Length == 0 ? 0 : Bids.Max(p => p.Price),
                BestPriceUsername = (from o in db.aspnet_Users where o.UserId == Bids.OrderByDescending(p => p.Price).FirstOrDefault().BidderId select o).FirstOrDefault().UserName,//o.UserName,
                MinimumPrice = a.MinimumPrice,
                SellerName = u.UserName,
                SellerId = a.OwnerId,
                ShipmentName = s.Name,
                ShipmentId = a.ShipmentId,
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
            join i in db.Images on a.Id equals i.AuctionId
            select new AuctionControlData()
            {
                Title = a.Title,
                AuctionId = a.Id,
                Image = i.ImageData,
                BuyItNowPrice = a.BuyItNowPrice,
                BestPrice = Bids.Max(p => p.Price).ToString().Length == 0 ? 0 : Bids.Max(p => p.Price),
                BestPriceUsername = (from o in db.aspnet_Users where o.UserId == Bids.OrderByDescending(p => p.Price).FirstOrDefault().BidderId select o).FirstOrDefault().UserName,//o.UserName,
                MinimumPrice = a.MinimumPrice,
                SellerName = u.UserName,
                SellerId = a.OwnerId,
                ShipmentName = s.Name,
                ShipmentId = a.ShipmentId,
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
            join i in db.Images on a.Id equals i.AuctionId
            select new AuctionControlData()
            {
                Title = a.Title,
                AuctionId = a.Id,
                Image = i.ImageData,
                BuyItNowPrice = a.BuyItNowPrice,
                BestPrice = Bids.Max(p => p.Price).ToString().Length == 0 ? 0 : Bids.Max(p => p.Price),
                BestPriceUsername = (from o in db.aspnet_Users where o.UserId == Bids.OrderByDescending(p => p.Price).FirstOrDefault().BidderId select o).FirstOrDefault().UserName,//o.UserName,
                MinimumPrice = a.MinimumPrice,
                SellerName = u.UserName,
                SellerId = a.OwnerId,
                ShipmentName = s.Name,
                ShipmentId = a.ShipmentId,
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
            join i in db.Images on a.Id equals i.AuctionId
            select new AuctionControlData()
            {
                Title = a.Title,
                AuctionId = a.Id,
                Image = i.ImageData,
                BuyItNowPrice = a.BuyItNowPrice,
                BestPrice = Bids.Max(p => p.Price).ToString().Length == 0 ? 0 : Bids.Max(p => p.Price),
                BestPriceUsername = (from o in db.aspnet_Users where o.UserId == Bids.OrderByDescending(p => p.Price).FirstOrDefault().BidderId select o).FirstOrDefault().UserName,//o.UserName,
                MinimumPrice = a.MinimumPrice,
                SellerName = u.UserName,
                SellerId = a.OwnerId,
                ShipmentName = s.Name,
                ShipmentId = a.ShipmentId,
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
                join i in db.Images on a.Id equals i.AuctionId
                select new AuctionControlData()
                {
                    Title = a.Title,
                    AuctionId = a.Id,
                    Image = i.ImageData,
                    BuyItNowPrice = a.BuyItNowPrice,
                    BestPrice = Bids.Max(p => p.Price).ToString().Length == 0 ? 0 : Bids.Max(p => p.Price),
                    BestPriceUsername = (from o in db.aspnet_Users where o.UserId == Bids.OrderByDescending(p => p.Price).FirstOrDefault().BidderId select o).FirstOrDefault().UserName,//o.UserName,
                    MinimumPrice = a.MinimumPrice,
                    SellerName = u.UserName,
                    SellerId = a.OwnerId,
                    ShipmentName = s.Name,
                    ShipmentId = a.ShipmentId,
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

        public List<AuctionControlData> SearchWithFilters(SearchFilters filters)
        {
            var auctions = GetByCategoryId(filters.CatId);
            auctions = (from a in auctions where a.Title.ToLower().Contains(filters.Search.ToLower()) select a).ToList();

            if (filters.IsBuyItNow)
                auctions = (from a in auctions where a.BuyItNowPrice != -1 select a).ToList();
            if (filters.IsBidding)
                auctions = (from a in auctions where a.MinimumPrice != -1 select a).ToList();
            if (filters.IsMinPrice)
                auctions = (from a in auctions where a.BuyItNowPrice >= filters.MinPrice || (a.MinimumPrice >= filters.MinPrice && a.BestPrice >= filters.MinPrice) select a).ToList();
            if (filters.IsMaxPrice)
                auctions = (from a in auctions where a.BuyItNowPrice <= filters.MaxPrice || (a.BestPrice <= filters.MaxPrice && a.MinimumPrice <= filters.MaxPrice) select a).ToList();
            if (filters.IsMinOffersNum)
                auctions = (from a in auctions where a.OffersNum >= filters.MinOffersNum select a).ToList();
            if (filters.IsMaxOffersNum)
                auctions = (from a in auctions where a.OffersNum <= filters.MaxOffersNum select a).ToList();
            if (filters.IsMinViewsCount)
                auctions = (from a in auctions where a.Views >= filters.MinViewsCount select a).ToList();
            if (filters.IsMaxViewsCount)
                auctions = (from a in auctions where a.Views <= filters.MaxViewsCount select a).ToList();
            if (filters.IsMaxTimeLeft)
                auctions = (from a in auctions where a.EndDate.Subtract(DateTime.Now).TotalDays <= filters.MaxDaysLeft select a).ToList();
            if (filters.IsShipmentType)
                auctions = (from a in auctions where a.ShipmentId == filters.ShipmentId select a).ToList();

            return auctions;
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
