using Model.RepositoriesDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repositories
{
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
}
