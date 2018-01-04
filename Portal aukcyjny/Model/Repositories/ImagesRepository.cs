using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repositories
{
    public class ImagesRepository
    {
        private PortalAukcyjnyEntities db;

        public ImagesRepository(PortalAukcyjnyEntities _db)
        {
            db = _db;
        }

        public ImagesRepository()
        {
            db = new PortalAukcyjnyEntities();
        }

        byte[] GetImage(int auctionId)
        {
            var img = (from p in db.Images where p.AuctionId == auctionId select p.ImageData).DefaultIfEmpty(null).FirstOrDefault();

            return img;
        }
    }
}
