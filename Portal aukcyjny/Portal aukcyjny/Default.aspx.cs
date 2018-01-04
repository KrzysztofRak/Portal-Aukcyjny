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

        public _Default()
        {
            presenter = new DefaultPresenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                presenter.LoadCategoriesTree();
                LoadAuctionsControls();
            }
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
            int catId;
            if (CategoriesTree.SelectedNode == null)
                catId = -1;
            else
                catId = int.Parse(CategoriesTree.SelectedValue);

            AuctionControl ac = new AuctionControl();
            ac.LoadAuctionsByCatId(catId);
            ac.LoadControls(ListView_Auctions);
        }

        protected void CategoriesTree_SelectedNodeChanged(object sender, EventArgs e)
        {
            LoadAuctionsControls();
        }
    }
}