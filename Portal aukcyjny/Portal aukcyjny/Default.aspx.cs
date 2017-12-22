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
        private PortalAukcyjnyEntities db;
        private DefaultPresenter presenter;

        private int catId;
        private string searchString;

        protected void Page_Load(object sender, EventArgs e)
        {
            db = new PortalAukcyjnyEntities();
            presenter = new DefaultPresenter(this);

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
            AuctionsRepository auctionsRepo = new AuctionsRepository(db);

            List<AuctionControlData> auctions;

            try
            {
                searchString = Request.QueryString["search"];
                if (searchString == null)
                    throw new Exception();
                auctions = auctionsRepo.Search(searchString);
            }
            catch
            {
                try
                {
                    catId = int.Parse(Request.QueryString["catId"]);
                }
                catch
                {
                    catId = -1;
                }
                finally
                {
                    auctions = auctionsRepo.GetByCategoryId(catId);
                }
            }

            MyPresenter controls = new MyPresenter();
            controls.LoadAuctionControls(auctions, ListView_Auctions);
        }

            
    }
}