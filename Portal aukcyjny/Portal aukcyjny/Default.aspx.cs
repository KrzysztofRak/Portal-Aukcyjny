using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using Portal_aukcyjny.UserControls;
using Model.Repositories;
using Model.RepositoriesDataModel;
using Portal_aukcyjny.Presenters;
using Model;
using Presenters;
using Presenters.IViews;

namespace Portal_aukcyjny
{
    public partial class _Default : Page, IDefaultView
    {
        private DefaultPresenter presenter;

        private int catId;
        private string searchString;

        public int CatId
        {
            get { return catId; }
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
                catId = int.Parse(Request.QueryString["catId"]);
            }
            catch
            {
                catId = -1;
            }

            if (!this.IsPostBack)
            {
                LoadCategoriesTree();
            }

            LoadAuctionControls();
        }

        private void LoadCategoriesTree()
        {
            var categories = presenter.GetCategoriesList();

            TreeNode categoriesChild;
            foreach (var cat in categories)
            {
                categoriesChild = new TreeNode();
                categoriesChild.Text = cat.Name;
                categoriesChild.NavigateUrl = ResolveUrl("~/Default.aspx?catId=" + cat.Id);
                CategoriesTree.Nodes.Add(categoriesChild);
            }
        }

        private void LoadAuctionControls()
        {
            var auctions = presenter.GetAuctionsList();

            AuctionControl ac = new AuctionControl();
            ac.LoadControl("~/UserControls/AuctionControl.ascx");
            ac.LoadControls(auctions, ListView_Auctions);
        }


    }
}