using Portal_aukcyjny.Repositories;
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
        private PortalAukcyjnyEntities db;
        protected void Page_Load(object sender, EventArgs e)
        {
            db = new PortalAukcyjnyEntities();
        }

        protected void CreateAuctionBtn_Click(object sender, EventArgs e)
        {
            AuctionsRepository auctionsRepo = new AuctionsRepository(db);

            Auctions auction = new Auctions
            {
                Id = 5,
                OwnerId = new Guid(Membership.GetUser().ProviderUserKey.ToString()),
                Views = 0,
                Finalized = false,

                Title = ItemTitle.Text,
                Image = FileUpload(),
                ItemsNumber = int.Parse(ItemsNumber.Text)
            };

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

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if(!CheckBox_Auction.Checked && !CheckBox_BuyItNow.Checked)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }
    }
}