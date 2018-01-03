using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using Portal_aukcyjny.UserControls;
using Presenter.IViews;
using Presenter;

namespace Portal_aukcyjny.PublicPages.User
{
    public partial class UserProfile : System.Web.UI.Page, IUserProfileView
    {
        private Guid userId;
        private UserProfilePresenter presenter;

        public string UsernameField
        {
            get { return Username.Text; }
            set { Username.Text = value; }
        }

        public string EmailField
        {
            get { return Email.Text; }
            set { Email.Text = value; }
        }
        public string SoldItemsNumField
        {
            get { return SoldItemsNum.Text; }
            set { SoldItemsNum.Text = value; }
        }
        public string RegistrationDateField
        {
            get { return RegistrationDate.Text; }
            set { RegistrationDate.Text = value; }
        }

        public UserProfile()
        {
            presenter = new UserProfilePresenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                userId = Guid.Parse(Request.QueryString["id"]);
            }
            catch
            {
                Response.Redirect(Page.ResolveUrl("~/Default.aspx"));
            }

            LoadUserProfile();
        }

        private void LoadUserProfile()
        {
           if(presenter.LoadUserProfile(userId) == false)
                Response.Redirect(Page.ResolveUrl("~/Default.aspx"));

            LoadAuctionsSection();
            LoadCommentsSection();
        }

        private void LoadAuctionsSection()
        {
            AuctionControl ac = new AuctionControl();
            ac.LoadSelling(userId);
            ac.LoadControls(ListView_UserAuctions);
        }

        private void LoadCommentsSection()
        {
            CommentControl cc = new CommentControl();
            cc.LoadByUserId(userId);
            cc.LoadControls(ListView_Comments);
        }
    }
}