using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presenter.IViews
{
    public interface ICommentControlView
    {
        string Seller { get; }
        string Buyer { get; }
        string AuthorNameField { get; set; }
        string AuthorNameNavUrl { get; set; }
        string AuthorType { get; set; }
        string CommentField { get; set; }
        string DateField { get; set; }
        string AuctionField { get; set; }
        string AuctionNavUrl { get; set; }
        void LoadByUserId(Guid userId);
    }
}
