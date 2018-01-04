using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repositories
{
    public class BuyedItemsRepository
    {
        private PortalAukcyjnyEntities db;

        public BuyedItemsRepository(PortalAukcyjnyEntities _db)
        {
            db = _db;
        }

        public BuyedItemsRepository()
        {
            db = new PortalAukcyjnyEntities();
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
                {
                    AuctionsRepository aucitonsRepo = new AuctionsRepository(db);
                    aucitonsRepo.CloseAuction(ac);
                }
                else
                    db.SaveChanges();
            }
            db.SaveChanges();
        }
    }

}
