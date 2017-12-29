using Portal_aukcyjny.Presenters;
using Presenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal_aukcyjny.Accounts
{
    public partial class Login : System.Web.UI.Page
    {
        private MasterPresenter presenter;

        public Login()
        {
            presenter = new MasterPresenter();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (presenter.IsUserLoggedIn())
            {
                Response.Redirect(Page.ResolveUrl("~/Default.aspx"));
            }
        }
    }
}