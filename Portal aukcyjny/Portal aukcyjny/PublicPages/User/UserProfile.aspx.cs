using Portal_aukcyjny.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Portal_aukcyjny.Repositories;

namespace Portal_aukcyjny.PublicPages.User
{
    public partial class UserProfile : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            Guid userId = new Guid();
            try
            {
                userId = Guid.Parse(Request.QueryString["id"]);
            }
            catch
            {
                Response.Redirect(Page.ResolveUrl("~/Default.aspx"));
            }

            PortalAukcyjnyEntities db = new PortalAukcyjnyEntities();

            var userData = (from m in db.aspnet_Membership
                        where m.UserId == userId
                        join u in db.aspnet_Users on userId equals u.UserId
                        join a in db.Auctions on userId equals a.OwnerId into AuctionsList
                        join c in db.Comments on userId equals c.RecipientId into CommentsList
                        select new UserProfileData
                        {
                            Username = u.UserName,
                            Email = m.Email,
                            RegistrationDate = m.CreateDate,
                            SoldItemsNum = AuctionsList.Count(),
                            Auctions = AuctionsList.ToList(),
                            Comments = CommentsList.ToList()
                        }).First();

            List<AuctionControl> auctionsControl = new List<AuctionControl>();
            List<CommentControl> commentsControl = new List<CommentControl>();

            for (int i = 0; i < userData.Auctions.Count; i++)
                auctionsControl.Add(new AuctionControl());
            for (int i = 0; i < userData.Comments.Count; i++)
                commentsControl.Add(new CommentControl());

            ListView_UserAuctions.DataSource = auctionsControl;
            ListView_UserAuctions.DataBind();

            ListView_Comments.DataSource = commentsControl;
            ListView_Comments.DataBind();

            Username.Text = userData.Username;
            Email.Text = userData.Email;
            SoldItemsNum.Text = userData.SoldItemsNum.ToString();
            RegistrationDate.Text = userData.RegistrationDate.ToShortDateString();

            int j = 0;

        }
    }
}