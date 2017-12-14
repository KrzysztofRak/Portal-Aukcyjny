using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal_aukcyjny.Auction
{
    public partial class CreateAuction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CreateAuctionBtn_Click(object sender, EventArgs e)
        {
            PortalAukcyjnyEntities db = new PortalAukcyjnyEntities();

            Auctions auction = new Auctions();
            auction.Id = 5;
            auction.OwnerId = new Guid(Membership.GetUser().ProviderUserKey.ToString());
            auction.Views = 0;
            auction.Finalized = false;

            auction.Title = ItemTitle.Text;
            auction.Image = FileUpload();
            auction.ItemsNumber = int.Parse(ItemsNumber.Text);

            if (CheckBox_BuyItNow.Checked)
                auction.BuyItNowPrice = Convert.ToDecimal(BuyItNowPrice.Text);
            else
                auction.BuyItNowPrice = 0;

            if (CheckBox_Auction.Checked)
                auction.CurrentPrice = Convert.ToDecimal(StartPrice.Text);
            else
                auction.CurrentPrice = 0;

            auction.Location = Location.Text;
            auction.EndDate = DateTime.Now.AddDays(int.Parse(EndDate.SelectedItem.Value));
            auction.Description = ItemDescription.Text;

            auction.CategoryId = int.Parse(ItemCategory.SelectedItem.Value);
            auction.ShipmentId = int.Parse(ShipmentType.SelectedItem.Value);

            db.Auctions.Add(auction);
            db.SaveChanges();

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