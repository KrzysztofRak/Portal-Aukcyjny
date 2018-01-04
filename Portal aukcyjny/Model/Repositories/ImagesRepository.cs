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

        public byte[] GetImage(int auctionId)
        {
            byte[] img = (from p in db.Images where p.AuctionId == auctionId select p.ImageData).FirstOrDefault();

            return img;
        }

        public void Add(Images img)
        {
                db.Images.Add(img);
                db.SaveChanges();
        }
    }
}
