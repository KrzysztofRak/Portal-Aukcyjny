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
using Presenter;
using Presenter.IPresenters;
using Presenter.IViews;

namespace Portal_aukcyjny.Auction
{
    public partial class CreateAuction : System.Web.UI.Page, ICreateAuctionView
    {
        private CreateAuctionPresenter presenter;
        private byte[] imgFile;

        private PortalAukcyjnyEntities db;

        public string AuctionTitleField
        {
            get { return ItemTitle.Text; }
            set { ItemTitle.Text = value; }
        }

        public byte[] ImageFileBytes
        {
            get { return imgFile; }
            set { imgFile = value; }
        }

        public string ItemsNumberField
        {
            get { return ItemsNumber.Text; }
            set { ItemsNumber.Text = value; }
        }

        public bool IsBuyItNow
        {
            get { return CheckBox_BuyItNow.Checked; }
            set { CheckBox_BuyItNow.Checked = value; }
        }

        public bool IsBiddable
        {
            get { return CheckBox_Auction.Checked; }
            set { CheckBox_Auction.Checked = value; }
        }

        public string BuyItNowPriceField
        {
            get { return BuyItNowPrice.Text; }
            set { BuyItNowPrice.Text = value; }
        }

        public string StartPriceField
        {
            get { return StartPrice.Text; }
            set { StartPrice.Text = value; }
        }

        public string LocationField
        {
            get { return Location.Text; }
            set { Location.Text = value; }
        }

        public string ShipmentId
        {
            get { return ShipmentType.SelectedValue; }
            set { ShipmentType.SelectedValue = value; }
        }

        public string CategoryId
        {
            get { return ItemCategory.SelectedValue; }
            set { ItemCategory.SelectedValue = value; }
        }

        public string TimeLeftField
        {
            get { return EndDate.SelectedValue; }
            set { EndDate.SelectedValue = value; }
        }

        public string ItemDescriptionField
        {
            get { return ItemDescription.Text; }
            set { ItemDescription.Text = value; }
        }

        public object ShipmentsSource
        {
            get { return ShipmentType.DataSource; }
            set { ShipmentType.DataSource = value; }
        }

        public object CategoriesSource
        {
            get { return ItemCategory.DataSource; }
            set { ItemCategory.DataSource = value; }
        }

        public CreateAuction()
        {
            presenter = new CreateAuctionPresenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            db = new PortalAukcyjnyEntities();

            LoadDropDownLists();
        }

        private void LoadDropDownLists()
        {
            presenter.SetSources(Global.GetDefaultCurrency());

            ShipmentType.DataTextField = "Name";
            ShipmentType.DataValueField = "Id";
            ShipmentType.DataBind();

            ItemCategory.DataTextField = "Name";
            ItemCategory.DataValueField = "Id";
            ItemCategory.DataBind();
        }

        protected void CreateAuctionBtn_Click(object sender, EventArgs e)
        {
            GetImageBytes();
            int aucitonId = presenter.CreateAuction();
            Response.Redirect(Page.ResolveUrl("~/PublicPages/Auction/AuctionPage?id=" + aucitonId));
        }

        private void GetImageBytes()
        {
            if (ImageFile.HasFile)
            {
                int length = ImageFile.PostedFile.ContentLength;
                ImageFileBytes = new byte[length];
                ImageFile.PostedFile.InputStream.Read(ImageFileBytes, 0, length);
            }
            else
                ImageFileBytes = null;
        }
    }
}