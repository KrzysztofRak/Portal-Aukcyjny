using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using Model.Repositories;
using Portal_aukcyjny.Presenters;

namespace Portal_aukcyjny.Auction
{
    public partial class CreateAuction : System.Web.UI.Page
    {
        private PortalAukcyjnyEntities db;
        protected void Page_Load(object sender, EventArgs e)
        {
            db = new PortalAukcyjnyEntities();

            CurrencyExchangeRepository currencyRepo = new CurrencyExchangeRepository(db);
            CategoriesRepository catRepo = new CategoriesRepository(db);
            ShipmentsRepository shipmentsRepo = new ShipmentsRepository(db);

            ShipmentType.DataSource = shipmentsRepo.GetList(currencyRepo, "PLN");
            ShipmentType.DataTextField = "Name";
            ShipmentType.DataValueField = "Id";
            ShipmentType.DataBind();

            ItemCategory.DataSource = catRepo.GetList();
            ItemCategory.DataTextField = "Name";
            ItemCategory.DataBind();
        }

        protected void CreateAuctionBtn_Click(object sender, EventArgs e)
        {
            AuctionsRepository auctionsRepo = new AuctionsRepository(db);

            Auctions auction = new Auctions
            {
                OwnerId = MyPresenter.GetCurrentUserId(),
                Views = 0,
                Finalized = false,

                Title = ItemTitle.Text,
                Image = FileUpload(),
                ItemsNumber = int.Parse(ItemsNumber.Text)
            };

            if (CheckBox_BuyItNow.Checked)
                auction.BuyItNowPrice = Convert.ToDecimal(BuyItNowPrice.Text);
            else
                auction.BuyItNowPrice = -1;

            if (CheckBox_Auction.Checked)
                auction.MinimumPrice = Convert.ToDecimal(StartPrice.Text);
            else
                auction.MinimumPrice = Convert.ToDecimal(-1);

            auction.Location = Location.Text;
            auction.EndDate = DateTime.Now.AddDays(int.Parse(EndDate.SelectedItem.Value));
            auction.Description = ItemDescription.Text;

            auction.CategoryId = int.Parse(ItemCategory.SelectedItem.Value);
            auction.ShipmentId = int.Parse(ShipmentType.SelectedItem.Value);

            auctionsRepo.Add(auction);

            Response.Redirect(Page.ResolveUrl("~/PublicPages/Auction/ViewAuction?id=" + auction.Id.ToString()));
        }

        private byte[] FileUpload()
        {
            if (ImageFile.HasFile)
            {
                int length = ImageFile.PostedFile.ContentLength;
                byte[] pic = new byte[length];


                ImageFile.PostedFile.InputStream.Read(pic, 0, length);

                return pic;
            }
            else
                return null;
        }
    }
}