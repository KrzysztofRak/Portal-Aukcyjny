using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using Portal_aukcyjny.UserControls;
using Portal_aukcyjny.Repositories;
using Portal_aukcyjny.Controller;

namespace Portal_aukcyjny
{
    public partial class _Default : Page
    {
        private PortalAukcyjnyEntities db;
        private int catId;
        private string searchString;

        protected void Page_Load(object sender, EventArgs e)
        {
            db = new PortalAukcyjnyEntities();

            if (!this.IsPostBack)
            {
                LoadCategoriesTree();
            }

            LoadAuctionControls();
        }

        private void LoadCategoriesTree()
        {
            CategoriesRepository catRepo = new CategoriesRepository(db);
            var categories = catRepo.GetCategoriesList();

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
            AuctionsRepository auctionsRepo = new AuctionsRepository();

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

            LoadControls controls = new LoadControls();
            controls.LoadAuctionControls(auctions, ListView_Auctions);
        }

    }
}