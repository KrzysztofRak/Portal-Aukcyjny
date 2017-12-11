using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace Portal_aukcyjny
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                PortalAukcyjnyEntities db = new PortalAukcyjnyEntities();
                var categories = db.Categories.ToList();

                TreeNode categoriesChild;
                foreach (var cat in categories)
                {
                    categoriesChild = new TreeNode();
                    categoriesChild.Text = cat.Name;
                    CategoriesTree.Nodes.Add(categoriesChild);
                }
            }
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {

        }
    }
}