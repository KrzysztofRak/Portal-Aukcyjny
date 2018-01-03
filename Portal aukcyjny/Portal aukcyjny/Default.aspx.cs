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
using System.Globalization;
using System.Threading;

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

            if (!IsPostBack)
                presenter.LoadCategoriesTree();
            else
            {
                CategoriesTree.Nodes[4].Selected = true;
            }


            LoadAuctionsControls();
        }

        public void AddNewItemToCategoriesTree(string catName, int catId)
        {
            catName = GetLocalResourceObject(catName).ToString();
            TreeNode categoriesChild = new TreeNode
            {
                Text = catName,
                Value = catId.ToString()
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

        protected void CategoriesTree_SelectedNodeChanged(object sender, EventArgs e)
        {
            selectedCatId = int.Parse(CategoriesTree.SelectedValue);
            LoadAuctionsControls();
        }
    }
}