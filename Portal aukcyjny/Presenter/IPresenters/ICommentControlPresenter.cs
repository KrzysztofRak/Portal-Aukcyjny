using Presenter.IViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presenter.IPresenters
{
    public interface ICommentControlPresenter
    {
        int GetCommentsNum();
        void LoadByUserId(Guid userId);
        void SetControl(ICommentControlView cc, int j);
    }
}
