using Model.RepositoriesDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repositories
{
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
}
