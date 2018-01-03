using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repositories
{
    public class CategoriesRepository
    {
        private PortalAukcyjnyEntities db;

        public CategoriesRepository(PortalAukcyjnyEntities _db)
        {
            db = _db;
        }

        public CategoriesRepository()
        {
            db = new PortalAukcyjnyEntities();
        }

        public List<Categories> GetList()
        {
            var categories = db.Categories.ToList();
            return categories;
        }
    }
}
