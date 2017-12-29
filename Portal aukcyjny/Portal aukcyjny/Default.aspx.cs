using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using Portal_aukcyjny.UserControls;
using Presenters;
using Presenters.IViews;

namespace Portal_aukcyjny
{
    public partial class _Default : Page, IDefaultView
    {
        private DefaultPresenter presenter;

        private int selectedCatId;
        private string searchString;

        public int SelectedCatId
        {
            get { return selectedCatId; }
        }

        public string SearchString
        {
            get { return searchString; }
        }
        public _Default()
        {
            presenter = new DefaultPresenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            searchString = Request.QueryString["search"];
            try
            {
                selectedCatId = int.Parse(Request.QueryString["catId"]);
            }
            catch
            {
                selectedCatId = -1;
            }

            if (!this.IsPostBack)
            {
                presenter.LoadCategoriesTree();
            }

            LoadAuctionsControls();
        }

        public void AddNewItemToCategoriesTree(string catName, int catId)
        {
            TreeNode categoriesChild = new TreeNode
            {
                Text = catName,
                NavigateUrl = ResolveUrl("~/Default.aspx?catId=" + catId)
            };
            CategoriesTree.Nodes.Add(categoriesChild);
        }

        private void LoadAuctionsControls()
        {
            AuctionControl ac = new AuctionControl();
            if (searchString != null)
                ac.LoadAuctionsBySearch(searchString);
            else
                ac.LoadAuctionsByCatId(selectedCatId);

            ac.LoadControls(ListView_Auctions);
        }


    }
}