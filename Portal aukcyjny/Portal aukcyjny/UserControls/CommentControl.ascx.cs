using Presenter;
using Presenter.IViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal_aukcyjny.UserControls
{
    public partial class CommentControl : System.Web.UI.UserControl, ICommentControlView
    {
        private CommentControlPresenter presenter;

        public string AuthorNameField
        {
            get { return AuthorName.Text; }
            set { AuthorName.Text = value; }
        }

        public string AuthorNameNavUrl
        {
            get { return AuthorName.NavigateUrl; }
            set { AuthorName.NavigateUrl = value; }
        }

        public string IsSellerField
        {
            get { return IsSeller.Text; }
            set { IsSeller.Text = value; }
        }

        public string CommentField
        {
            get { return Comment.Text; }
            set { Comment.Text = value; }
        }

        public string DateField
        {
            get { return Date.Text; }
            set { Date.Text = value; }
        }

        public string AuctionField
        {
            get { return Auction.Text; }
            set { Auction.Text = value; }
        }

        public string AuctionNavUrl
        {
            get { return Auction.NavigateUrl; }
            set { Auction.NavigateUrl = value; }
        }

        public CommentControl()
        {
            presenter = new CommentControlPresenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void LoadByUserId(Guid userId)
        {
            presenter.LoadByUserId(userId);
        }

        public void LoadControls(ListView listView)
        {
            var commentControls = new List<CommentControl>();
            for (int i = 0; i < presenter.GetCommentsNum(); i++)
                commentControls.Add(new CommentControl());
            listView.DataSource = commentControls;
            listView.DataBind();

            int j = 0;
            foreach (var item in listView.Items)
            {
                ICommentControlView cc = (ICommentControlView)item.FindControl("CommentControl");
                cc.AuthorNameNavUrl = (HttpContext.Current.Handler as Page).ResolveUrl("~/PublicPages/User/UserProfile?id=");
                cc.AuctionNavUrl = (HttpContext.Current.Handler as Page).ResolveUrl("~/PublicPages/Auction/AuctionPage?id=");
                presenter.SetControl(cc, j);
                j++;
            }
        }
    }
}