using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Presenter.IViews;
using Presenter;

namespace Portal_aukcyjny.UserControls
{
    public partial class FilterControl : System.Web.UI.UserControl, IFilterControlView
    {
        private FilterControlPresenter presenter;

        public object ShipmentsSource
        {
            get { return DDListShipmentType.DataSource; }
            set { DDListShipmentType.DataSource = value; }
        }

        public bool IsBuyItNow
        {
            get { return CheckBox_OnlyBuyItNow.Checked; }
            set { CheckBox_OnlyBuyItNow.Checked = value; }
        }

        public bool IsBidding
        {
            get { return CheckBox_OnlyBidding.Checked; }
            set { CheckBox_OnlyBidding.Checked = value; }
        }

        public bool IsMinPrice
        {
            get { return CheckBox_MinPrice.Checked; }
            set { CheckBox_MinPrice.Checked = value; }
        }

        public bool IsMaxPrice
        {
            get { return CheckBox_MaxPrice.Checked; }
            set { CheckBox_MaxPrice.Checked = value; }
        }

        public bool IsMinOffersNum
        {
            get { return CheckBox_MinOffersNum.Checked; }
            set { CheckBox_MinOffersNum.Checked = value; }
        }

        public bool IsMaxOffersNum
        {
            get { return CheckBox_MaxOffersNum.Checked; }
            set { CheckBox_MaxOffersNum.Checked = value; }
        }

        public bool IsMinViewsCount
        {
            get { return CheckBox_MinViews.Checked; }
            set { CheckBox_MinViews.Checked = value; }
        }

        public bool IsMaxViewsCount
        {
            get { return CheckBox_MaxViews.Checked; }
            set { CheckBox_MaxViews.Checked = value; }
        }

        public bool IsMaxTimeLeft
        {
            get { return CheckBox_MaxTimeLeft.Checked; }
            set { CheckBox_MaxTimeLeft.Checked = value; }
        }

        public bool IsShipmentType
        {
            get { return CheckBox_ShipmentType.Checked; }
            set { CheckBox_ShipmentType.Checked = value; }
        }

        public string MinPrice
        {
            get { return TextBox_MinPrice.Text; }
            set { TextBox_MinPrice.Text = value; }
        }

        public string MaxPrice
        {
            get { return TextBox_MaxPrice.Text; }
            set { TextBox_MaxPrice.Text = value; }
        }

        public string MinOffersNum
        {
            get { return TextBox_MinOffersNum.Text; }
            set { TextBox_MinOffersNum.Text = value; }
        }

        public string MaxOffersNum
        {
            get { return TextBox_MaxOffersNum.Text; }
            set { TextBox_MaxOffersNum.Text = value; }
        }

        public string MinViewsCount
        {
            get { return TextBox_MinViews.Text; }
            set { TextBox_MinViews.Text = value; }
        }

        public string MaxViewsCount
        {
            get { return TextBox_MaxViews.Text; }
            set { TextBox_MaxViews.Text = value; }
        }

        public string MaxDaysLeft
        {
            get { return TextBox_MaxTimeLeft.Text; }
            set { TextBox_MaxTimeLeft.Text = value; }
        }

        public string Search
        {
            get { return SearchBox.Text; }
            set { SearchBox.Text = value; }
        }

        public string ShipmentId
        {
            get { return DDListShipmentType.SelectedValue; }
            set { DDListShipmentType.SelectedValue = value; }
        }

        public FilterControl()
        {
            presenter = new FilterControlPresenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadShipmentsList();
        }

        private void LoadShipmentsList()
        {
            presenter.SetListsSources(Global.GetDefaultCurrency());

            DDListShipmentType.DataTextField = "Name";
            DDListShipmentType.DataValueField = "Id";
            DDListShipmentType.DataBind();
        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            int catId;
            ListView auctionsListView = this.Parent.FindControl("ListView_Auctions") as ListView;
            TreeView categoriesTreeView = this.Parent.FindControl("CategoriesTree") as TreeView;

            if (categoriesTreeView.SelectedNode == null)
                catId = -1;
            else
                catId = int.Parse(categoriesTreeView.SelectedValue);

            AuctionControl ac = new AuctionControl();
            ac.LoadByFilters(this, catId);
            ac.LoadControls(auctionsListView);
        }

        protected void BtnReset_Click(object sender, EventArgs e)
        {
            presenter.ResetFilters();
        }
    }
}