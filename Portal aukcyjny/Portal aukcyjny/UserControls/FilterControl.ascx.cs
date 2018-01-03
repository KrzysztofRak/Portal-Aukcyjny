using System;
using System.Collections.Generic;
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

        public bool IsBuyItNow
        {
            get { return CheckBox_OnlyBuyItNow.Checked; }
        }

        public bool IsBidding
        {
            get { return CheckBox_OnlyBidding.Checked; }
        }

        public bool IsMinPrice
        {
            get { return CheckBox_MaxPrice.Checked; }
        }

        public bool IsMaxPrice
        {
            get { return CheckBox_MaxPrice.Checked; }
        }

        public bool IsMinOffersNum
        {
            get { return CheckBox_MinOffersNum.Checked; }
        }

        public bool IsMaxOffersNum
        {
            get { return CheckBox_MaxOffersNum.Checked; }
        }

        public bool IsMinViewsCount
        {
            get { return CheckBox_MinViews.Checked; }
        }

        public bool IsMaxViewsCount
        {
            get { return CheckBox_MaxViews.Checked; }
        }

        public bool IsMaxTimeLeft
        {
            get { return CheckBox_MaxTimeLeft.Checked; }
        }

        public bool IsShipmentType
        {
            get { return CheckBox_ShipmentType.Checked; }
        }

        public string MinPrice
        {
            get { return TextBox_MinPrice.Text; }
        }

        public string MaxPrice
        {
            get { return TextBox_MaxPrice.Text; }
        }

        public string MinOffersNum
        {
            get { return TextBox_MinOffersNum.Text; }
        }

        public string MaxOffersNum
        {
            get { return TextBox_MaxOffersNum.Text; }
        }

        public string MinViewsCount
        {
            get { return TextBox_MinViews.Text; }
        }

        public string MaxViewsCount
        {
            get { return TextBox_MaxViews.Text; }
        }

        public string MaxTimeLeft
        {
            get { return TextBox_MaxTimeLeft.Text; }
        }

        public string Search
        {
            get { return SearchBox.Text; }
        }

        public string ShipmentType
        {
            get { return DropDownList_ShipmentType.SelectedValue; }
        }

        public FilterControl()
        {
            presenter = new FilterControlPresenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect(Page.ResolveUrl("~/Default.aspx?search=" + HttpUtility.UrlEncode(SearchBox.Text)));
        }
    }
}