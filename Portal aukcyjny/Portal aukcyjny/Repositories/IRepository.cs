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
        IEnumerable<AuctionControlData> GetAuctionsListOfCategory(int catId);
        IEnumerable<AuctionControlData> GetAuctionsList(Guid userId, bool unfinished = true, bool onlyObservated = false);
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
