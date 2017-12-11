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
            {
                auction.BuyItNowPrice = Convert.ToDecimal(BuyItNowPrice.Text);
                auction.BuyItNow = true;
            }
            else
            {
                auction.BuyItNowPrice = 0;
                auction.BuyItNow = false;
            }

            if (CheckBox_Auction.Checked)
            {
                auction.CurrentPrice = Convert.ToDecimal(StartPrice.Text);
                auction.Auction = true;
            }
            else
            {
                auction.CurrentPrice = 0;
                auction.Auction = false;
            }

            auction.Location = Location.Text;
            auction.EndDate = DateTime.Now.AddDays(int.Parse(EndDate.SelectedItem.Value));
            auction.Description = ItemDescription.Text;

            auction.CategoryId = 0;//int.Parse(ItemCategory.SelectedItem.Value);
            auction.ShipmentId = 0;//int.Parse(ShipmentType.SelectedItem.Value);

            db.Auctions.Add(auction);
            db.SaveChanges();

            Response.Redirect(Page.ResolveUrl("~/PublicPages/Auction/ViewAuction?id=" + auction.Id.ToString()));
        }

        private string FileUpload()
        {
            if ((ImageFile.PostedFile != null) && (ImageFile.PostedFile.ContentLength > 0))
            {
                string fn = System.IO.Path.GetFileName(ImageFile.PostedFile.FileName);
                string SaveLocation = Server.MapPath("Data") + "\\" + fn;
                try
                {
                    ImageFile.PostedFile.SaveAs(SaveLocation);
                    Response.Write("The file has been uploaded to: " + SaveLocation);
                    return SaveLocation;
                }
                catch (Exception ex)
                {
                    return "123";
                }
            }
            else
                return "321";
        }
    }
}