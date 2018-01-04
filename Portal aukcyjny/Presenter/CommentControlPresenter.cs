using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Presenters;
using Presenter.IPresenters;
using Presenter.IViews;
using Model.RepositoriesDataModel;

namespace Presenter
{
    public class CommentControlPresenter : MasterPresenter, ICommentControlPresenter
    {
        private ICommentControlView view;
        private List<CommentControlData> comments;

        public CommentControlPresenter(ICommentControlView view)
        {
            this.view = view;
        }

        public int GetCommentsNum()
        {
            return comments.Count();
        }

        public void LoadByUserId(Guid userId)
        {
            comments = commentsRepo.GetByUserId(userId);
        }

        public void SetControl(ICommentControlView cc, int j)
        {
            cc.AuthorNameField = comments[j].AuthorName;

            if (comments[j].AuthorIsSeller)
                cc.AuthorType = cc.Seller;
            else
                cc.AuthorType = cc.Buyer;

            cc.CommentField = comments[j].Comment;
            cc.DateField = comments[j].Date.ToString("dd.MM.yyyy hh:mm");
            cc.AuctionField = comments[j].AuctionTitle;

            cc.AuthorNameNavUrl += comments[j].AuthorId;
            cc.AuctionNavUrl += comments[j].AuctionId;
        }
    }
}