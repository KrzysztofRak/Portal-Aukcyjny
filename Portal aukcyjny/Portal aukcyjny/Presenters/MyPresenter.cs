using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using Portal_aukcyjny;
using Model.Repositories;
using System.Globalization;
using System.Web.Security;
using Model.RepositoriesDataModel;
using Portal_aukcyjny.UserControls;

namespace Portal_aukcyjny.Presenters
{
    public class MyPresenter : Page
    {
        //private View view;

        //public Presenter(View _view)
        //{
        //    view = _view;
        //}

        private Guid currentUserId;

        public string GetDefaultCurrency()
        {
            if (HttpContext.Current.Request.Cookies["defaultCurrency"] != null)
                return HttpContext.Current.Request.Cookies["defaultCurrency"].Value.ToString();
            else
                return "pln";
        }

        public MyPresenter()
        {
            if (IsUserLoggedIn())
                currentUserId = GetCurrentUserId();
            else
                currentUserId = new Guid();
        }
        public static bool IsUserLoggedIn()
        {
            return (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        }

        public static Guid GetCurrentUserId()
        {
            return new Guid(Membership.GetUser().ProviderUserKey.ToString());
        }

        public void LoadAuctionControls(List<AuctionControlData> auctions, ListView listView)
        {
;            CurrencyExchangeRepository currencyRepo = new CurrencyExchangeRepository(new Model.PortalAukcyjnyEntities());

            var auctionControls = new List<AuctionControl>();
            for (int i = 0; i < auctions.Count(); i++)
                auctionControls.Add(new AuctionControl());

            
            listView.DataSource = auctionControls;
            listView.DataBind();

            int j = 0;
            foreach (var item in listView.Items)
            {
                var ac = (AuctionControl)item.FindControl("AuctionControl");
                ac.AuctionUrl = Page.ResolveUrl("~/PublicPages/Auction/ViewAuction?id=" + auctions[j].AuctionId);
                ac.SellerNavUrl = Page.ResolveUrl("~/PublicPages/User/UserProfile?id=" + auctions[j].SellerId);
                j++;
            }
        }

        public void LoadCommentControls(List<CommentControlData> comments, ListView listView)
        {
            var commentControls = new List<CommentControl>();
            for (int i = 0; i < comments.Count(); i++)
                commentControls.Add(new CommentControl());

            listView.DataSource = commentControls;
            listView.DataBind();

            int j = 0;
            foreach (var item in listView.Items)
            {
                var commentControl = (CommentControl)item.FindControl("CommentControl");

                var authorName = ((HyperLink)commentControl.FindControl("AuthorName"));
                authorName.Text = comments[j].AuthorName;
                authorName.NavigateUrl = Page.ResolveUrl("~/PublicPages/User/UserProfile?id=" + comments[j].AuthorId);

                if (comments[j].AuthorIsSeller)
                    ((Label)commentControl.FindControl("IsSeller")).Text = "[Sprzedający]";
                else
                    ((Label)commentControl.FindControl("IsSeller")).Text = "[Kupujący]";

                ((Label)commentControl.FindControl("Comment")).Text = comments[j].Comment;

                ((Label)commentControl.FindControl("Date")).Text = comments[j].Date.ToString("dd.MM.yyyy hh:mm"); ;

                j++;
            }
        }
    }
}