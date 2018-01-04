using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presenters.IViews
{
    public interface IDefaultView
    {
        void AddNewItemToCategoriesTree(string catName, int catId);
    }
}