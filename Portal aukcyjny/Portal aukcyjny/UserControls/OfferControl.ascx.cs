using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace Portal_aukcyjny.UserControls
{
    public class MyBaseControl : System.Web.UI.UserControl
    {
        protected string name = "cafsfsa", userId, price;

        public MyBaseControl(string na)
        {
            name = na;
        }
        public string Name
        {
            set { name = value; }
        }
    }
    //public MyBaseControl(string _name, string _userId, string _price)
    //{
    //    name = _name;
    //    userId = _userId;
    //    price = _price;
    //}


    public partial class OfferControl : System.Web.UI.UserControl
    {
        public string name = "cafsfsa", userId, price = "11111";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Reload()
        {
            BidderName.Text = name;
            BidderName.NavigateUrl = Page.ResolveUrl("~/PublicPages/User/UserProfile?id=" + userId);
            BidPrice.Text = price;
        }


        protected void OnPreRender(object sender, EventArgs e)
        {


        }
    }
}