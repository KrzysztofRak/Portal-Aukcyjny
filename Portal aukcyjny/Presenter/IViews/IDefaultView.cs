using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presenters.IViews
{
    public interface IDefaultView
    {
        int CatId { get; }
        string SearchString { get; }
    }
}