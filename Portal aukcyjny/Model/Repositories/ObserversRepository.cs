using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repositories
{
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
}
