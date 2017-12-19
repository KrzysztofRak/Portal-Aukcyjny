using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using Portal_aukcyjny.UserControls;
using Portal_aukcyjny.Repositories;
using System.Globalization;
using System.Web.Security;
using Portal_aukcyjny.CustomModels;

namespace Portal_aukcyjny.Controller
{
    public class Presenter : Page
    {
        //private View view;

        //public Presenter(View _view)
        //{
        //    view = _view;
        //}

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
            var auctionControls = new List<AuctionControl>();
            for (int i = 0; i < auctions.Count(); i++)
                auctionControls.Add(new AuctionControl());

            
            listView.DataSource = auctionControls;
            listView.DataBind();

            int j = 0;
            foreach (var item in listView.Items)
            {
                var auctionControl = (AuctionControl)item.FindControl("AuctionControl");

                var title = ((HyperLink)auctionControl.FindControl("Title"));
                title.Text = auctions[j].Title;
                title.NavigateUrl = Page.ResolveUrl("~/PublicPages/Auction/ViewAuction?id=" + auctions[j].AuctionId);

                var image = ((Image)auctionControl.FindControl("Image"));

                if (auctions[j].Image == null)
                    image.ImageUrl = "~/Images/defaultAuctionImg.jpg";
                else
                    image.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(auctions[j].Image);

                if (auctions[j].BuyItNowPrice > 0)
                {
                    var buyItNow = ((Label)auctionControl.FindControl("BuyItNow"));
                    buyItNow.Text = auctions[j].BuyItNowPrice.ToString();
                    buyItNow.Visible = true;
                    ((Label)auctionControl.FindControl("BuyItNowLabel")).Visible = true;
                }


                if (auctions[j].MinimumPrice.ToString() != null)
                {
                    var bid = ((Label)auctionControl.FindControl("Bid"));
                    bid.Text = CurrencyExchangeRepository.Exchange(auctions[j].MinimumPrice);
                    bid.Visible = true;
                    ((Label)auctionControl.FindControl("BidLabel")).Visible = true;
                }

                var seller = ((HyperLink)auctionControl.FindControl("Seller"));
                seller.Text = auctions[j].SellerName;
                seller.NavigateUrl = Page.ResolveUrl("~/PublicPages/User/UserProfile?id=" + auctions[j].SellerId);

                ((Label)auctionControl.FindControl("Shipment")).Text = auctions[j].ShipmentName + " " + CurrencyExchangeRepository.Exchange(auctions[j].ShipmentPrice);

                var timeLeft = ((Label)auctionControl.FindControl("TimeLeft"));
                var leftDateTime = (auctions[j].EndDate.Subtract(DateTime.Now));

                if(leftDateTime.TotalMinutes < 0)
                {
                    ((Label)auctionControl.FindControl("TimeLeftLabel")).Text = "Zakończono: ";
                    timeLeft.Text = auctions[j].EndDate.ToString("dd.MM.yyyy hh:mm");
                }
                else if (leftDateTime.TotalDays > 1)
                    timeLeft.Text = (int)leftDateTime.TotalDays + " dni";
                else
                    timeLeft.Text = String.Format("{0:hh\\:mm\\:ss}", leftDateTime);

                ((Label)auctionControl.FindControl("OffersNum")).Text = auctions[j].OffersNum.ToString();
                ((Label)auctionControl.FindControl("Views")).Text = auctions[j].Views.ToString();
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