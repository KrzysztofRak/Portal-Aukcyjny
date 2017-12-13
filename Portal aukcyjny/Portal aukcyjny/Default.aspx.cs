using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using Portal_aukcyjny.UserControls;

namespace Portal_aukcyjny
{
    class AuctionControlData
    {
        public string Title { get; set; }
        public byte[] Image { get; set; }
        public decimal BuyItNowPrice { get; set; }
        public decimal HighestOffer { get; set; }
        public string SellerName { get; set; }
        public Guid SellerId { get; set; }
        public string ShipmentName { get; set; }
        public decimal ShipmentPrice { get; set; }
        public DateTime EndDate { get; set; }
        public int OffersNum { get; set; }
        public int Views { get; set; }
    }

    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PortalAukcyjnyEntities db = new PortalAukcyjnyEntities();

            if (!this.IsPostBack)
            {
                var categories = db.Categories.ToList();

                TreeNode categoriesChild;
                foreach (var cat in categories)
                {
                    categoriesChild = new TreeNode();
                    categoriesChild.Text = cat.Name;
                    CategoriesTree.Nodes.Add(categoriesChild);
                }
            }

            var auctions = (from p in db.Auctions where !p.Finalized && p.EndDate > DateTime.Now select p).ToList();

            //AuctionImg.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(auction.Image);
            //ItemsNum.Text = auction.ItemsNumber.ToString();
            //var timeLeft = (auction.EndDate.Subtract(DateTime.Now));

            //if (timeLeft.TotalDays > 1)
            //    EndTime.Text = (int)timeLeft.TotalDays + " dni";
            //else
            //    EndTime.Text = String.Format("{0:hh\\:mm\\:ss}", timeLeft);

            //  var bids = (from p in db.Bidders where p.AuctionId == auctionId select p).ToList();
            //   List<OfferControl> offersControls = new List<OfferControl>();


            var auctionsData =
               (from a in db.Auctions where !a.Finalized && a.EndDate > DateTime.Now
                join u in db.aspnet_Users on a.OwnerId equals u.UserId
                join b in db.Bidders on a.Id equals b.AuctionId into Bids
                join s in db.Shipments on a.ShipmentId equals s.Id
                select new AuctionControlData()
                {
                    Title = a.Title,
                    Image = a.Image,
                    BuyItNowPrice = a.BuyItNowPrice,
                    HighestOffer = Bids.Max(p => p.Price),
                    SellerName = u.UserName,
                    SellerId = a.OwnerId,
                    ShipmentName = s.Name,
                    ShipmentPrice = s.Price,
                    EndDate = a.EndDate,
                    OffersNum = Bids.Count(),
                    Views = a.Views
                }).ToList();

            var auctionControls = new List<AuctionControl>();
            for (int i = 0; i < auctionsData.Count; i++)
                auctionControls.Add(new AuctionControl());

            ListView_Auctions.DataSource = auctionControls;
            ListView_Auctions.DataBind();

            int j = 0;
            foreach (var item in ListView_Auctions.Items)
            {
                var auctionControl = (AuctionControl)item.FindControl("AuctionControl");\

                ((HyperLink)auctionControl.FindControl("Title")).Text = auctionsData[j].Title;

                var buyItNow = ((Label)auctionControl.FindControl("BuyItNow"));
                if (auctionsData[j].BuyItNowPrice > 0)
                {
                    buyItNow.Text = auctionsData[j].BuyItNowPrice.ToString();
                    buyItNow.Visible = true;
                    ((Label)auctionControl.FindControl("BuyItNowLabel")).Visible = true;
                }

                var bid = ((Label)auctionControl.FindControl("BuyItNow"));
                if (auctionsData[j].HighestOffer > 0)
                {
                    bid.Text = auctionsData[j].HighestOffer.ToString();
                    bid.Visible = true;
                    ((Label)auctionControl.FindControl("BidLabel")).Visible = true;
                }

                var seller = ((HyperLink)auctionControl.FindControl("Seller"));
                seller.Text = auctionsData[j].SellerName;
                seller.NavigateUrl = Page.ResolveUrl("~/PublicPages/User/UserProfile?id=" + auctionsData[j].SellerId);

                ((Label)auctionControl.FindControl("Shipment")).Text = auctionsData[j].ShipmentName + " " + auctionsData[j].ShipmentPrice;

                var timeLeft = ((Label)auctionControl.FindControl("TimeLeft"))
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