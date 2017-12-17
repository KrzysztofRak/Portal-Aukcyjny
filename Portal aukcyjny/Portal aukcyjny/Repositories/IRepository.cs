using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal_aukcyjny.Repositories
{
    public interface IUsersRepository
    {
        UserProfileData GetUserProfileData();
    }

    public interface IAuctionsRepository
    {
        IEnumerable<AuctionControlData> GetCategoryAuctionsList(int catId = -1);
        IEnumerable<AuctionControlData> GetUsetrAuctionsList(Guid userId);
        IEnumerable<AuctionControlData> GetObservatedAuctionsList();
        Auctions Get(int id);
        void Add(Auctions auction);
        void Delete(int id);
    }

    public interface ICommentsRepository
    {
        IEnumerable<CommentControlData> GetCommentControlData();
        void Add(Comments comment);
    }

    public interface IOffersRepository
    {
        IEnumerable<OfferControlData> GetOfferControlData();
        void Add(Bidders offer);
    }

    public interface ICategoriesRepository
    {
        IEnumerable<Categories> GetAll();
    }

    public interface IShipmentsRepository
    {
        IEnumerable<Shipments> GetAll();
    }

    public interface IUnitOfWork
    {

    }
}
