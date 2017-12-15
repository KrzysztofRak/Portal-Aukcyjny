﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using Portal_aukcyjny.UserControls;
using Portal_aukcyjny.Repositories;

namespace Portal_aukcyjny
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int catId;
            try
            {
                catId = int.Parse(Request.QueryString["catId"]);
            }
            catch
            {
                catId = -1;
            }

            PortalAukcyjnyEntities db = new PortalAukcyjnyEntities();

            if (!this.IsPostBack)
            {
                var categories = db.Categories.ToList();

                TreeNode categoriesChild;
                foreach (var cat in categories)
                {
                    categoriesChild = new TreeNode();
                    categoriesChild.Text = cat.Name;
                    categoriesChild.NavigateUrl = ResolveUrl("~/Default.aspx?catId=" + cat.Id);
                    CategoriesTree.Nodes.Add(categoriesChild);
                }
            }

            var auctionsData =
               (from a in db.Auctions where !a.Finalized && a.EndDate > DateTime.Now && (catId == -1 || a.CategoryId == catId)
                join u in db.aspnet_Users on a.OwnerId equals u.UserId
                join b in db.Bidders on a.Id equals b.AuctionId into Bids
                join s in db.Shipments on a.ShipmentId equals s.Id
                select new AuctionControlData()
                {
                    Title = a.Title,
                    AuctionId = a.Id,
                    Image = a.Image,
                    BuyItNowPrice = a.BuyItNowPrice,
                    CurrentPrice = Bids.Max(p => p.Price).ToString(),
                    SellerName = u.UserName,
                    SellerId = a.OwnerId,
                    ShipmentName = s.Name,
                    ShipmentPrice = s.Price,
                    EndDate = a.EndDate,
                    OffersNum = Bids.Count(),
                    Views = a.Views
                }).ToList();

            var auctionControls = new List<AuctionControl>();
            for (int i = 0; i < auctionsData.Count(); i++)
                auctionControls.Add(new AuctionControl());

            ListView_Auctions.DataSource = auctionControls;
            ListView_Auctions.DataBind();

            int j = 0;
            foreach (var item in ListView_Auctions.Items)
            {
                var auctionControl = (AuctionControl)item.FindControl("AuctionControl");

                var title = ((HyperLink)auctionControl.FindControl("Title"));
                title.Text = auctionsData[j].Title;
                title.NavigateUrl = Page.ResolveUrl("~/PublicPages/Auction/ViewAuction?id=" + auctionsData[j].AuctionId);

                var image = ((Image)auctionControl.FindControl("Image"));

                if (auctionsData[j].Image == null)
                    image.ImageUrl = "~/Images/defaultAuctionImg.jpg";
                else
                    image.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(auctionsData[j].Image);

                if (auctionsData[j].BuyItNowPrice > 0)
                {
                    var buyItNow = ((Label)auctionControl.FindControl("BuyItNow"));
                    buyItNow.Text = auctionsData[j].BuyItNowPrice.ToString();
                    buyItNow.Visible = true;
                    ((Label)auctionControl.FindControl("BuyItNowLabel")).Visible = true;
                }

                
                if (auctionsData[j].CurrentPrice != null)
                {
                    var bid = ((Label)auctionControl.FindControl("Bid"));
                    bid.Text = auctionsData[j].CurrentPrice.ToString();
                    bid.Visible = true;
                    ((Label)auctionControl.FindControl("BidLabel")).Visible = true;
                }

                var seller = ((HyperLink)auctionControl.FindControl("Seller"));
                seller.Text = auctionsData[j].SellerName;
                seller.NavigateUrl = Page.ResolveUrl("~/PublicPages/User/UserProfile?id=" + auctionsData[j].SellerId);

                ((Label)auctionControl.FindControl("Shipment")).Text = auctionsData[j].ShipmentName + " " + auctionsData[j].ShipmentPrice;

                var timeLeft = ((Label)auctionControl.FindControl("TimeLeft"));
                var leftDateTime = (auctionsData[j].EndDate.Subtract(DateTime.Now));

                if (leftDateTime.TotalDays > 1)
                    timeLeft.Text = (int)leftDateTime.TotalDays + " dni";
                else
                    timeLeft.Text = String.Format("{0:hh\\:mm\\:ss}", leftDateTime);

                ((Label)auctionControl.FindControl("OffersNum")).Text = auctionsData[j].OffersNum.ToString();
                ((Label)auctionControl.FindControl("Views")).Text = auctionsData[j].Views.ToString();
                j++;
            }
        }

    }
}