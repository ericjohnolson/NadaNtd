using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model.Repositories;
using Nada.Model;

namespace Nada.UI.View.Demography
{
    public partial class DemographyTree : UserControl
    {
        private List<AdminLevel> tree = null;
        public event Action<AdminLevel> OnSelect = (e) => { };

        public DemographyTree()
        {
            InitializeComponent();
        }

        public DemographyTree(List<AdminLevel> tree)
        {
            InitializeComponent();
            this.tree = tree;
        }

        private void DemographyTree_Load(object sender, EventArgs e)
        {
            if (!DesignMode && tree != null)
            {
                LoadTree(tree);
            }
        }

        public void LoadTree(List<AdminLevel> t)
        {
            treeListView1.CanExpandGetter = model => ((AdminLevel)model).
                                                          Children.Count > 0;
            treeListView1.ChildrenGetter = delegate(object model)
            {
                return ((AdminLevel)model).
                        Children;
            };
            treeListView1.SetObjects(t);
        }

        private void treeListView1_HyperlinkClicked(object sender, BrightIdeasSoftware.HyperlinkClickedEventArgs e)
        {
            e.Handled = true;
            OnSelect((AdminLevel)e.Model);
        }
    }
}
