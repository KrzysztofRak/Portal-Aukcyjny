using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal_aukcyjny.Repositories
{
    class AuctionRepository : IAuctionsRepository
    {
        public void Add(Auctions auction)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Auctions Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AuctionControlData> GetAuctionsList(Guid userId, bool unfinished = true, bool onlyObservated = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AuctionControlData> GetAuctionsListOfCategory(int catId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AuctionControlData> GetObservatedAuctionsList()
        {
            throw new NotImplementedException();
        }
    }
}