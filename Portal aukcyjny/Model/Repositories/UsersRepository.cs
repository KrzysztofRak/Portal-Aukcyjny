using Model.RepositoriesDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repositories
{
    public class UsersRepository
    {
        private PortalAukcyjnyEntities db;

        public UsersRepository(PortalAukcyjnyEntities _db)
        {
            db = _db;
        }

        public UsersRepository()
        {
            db = new PortalAukcyjnyEntities();
        }

        public UserProfileData GetUserProfile(Guid userId)
        {
            var user = (from m in db.aspnet_Membership
                        where m.UserId == userId
                        join u in db.aspnet_Users on userId equals u.UserId
                        select new UserProfileData
                        {
                            Username = u.UserName,
                            Email = m.Email,
                            RegistrationDate = m.CreateDate,
                            SoldItemsNum = m.SoldItemsNum, // Potrzebna dodatkowa kolumna w Membership
                        }).First();

            return user;
        }

        public aspnet_Users Get(Guid userId)
        {
            return db.aspnet_Users.Find(userId);
        }
    }
}
