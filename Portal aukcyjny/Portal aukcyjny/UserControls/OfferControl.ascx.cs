using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using Presenter.IViews;
using Presenter;

namespace Portal_aukcyjny.UserControls
{
    public partial class OfferControl : System.Web.UI.UserControl, IOfferControlView
    {
        public string BidderNameField {
            get { return BidderName.Text; }
            set { BidderName.Text = value; }
        }
        public string BidderNameNavUrl {
            get { return BidderName.NavigateUrl; }
            set { BidderName.NavigateUrl = value; }
        }
        public string BidPriceField {
            get { return BidPrice.Text; }
            set { BidPrice.Text = value; }
        }
        public string BidDateField {
            get { return BidDate.Text; }
            set { BidDate.Text = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}