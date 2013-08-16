using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model;
using Nada.Model.Repositories;
using Nada.UI.AppLogic;

namespace Nada.UI.View
{
    public partial class AdminLevelPicker : Form
    {
        public event Action<AdminLevel> OnSelect = (e) => { };

        public AdminLevelPicker()
        {
            InitializeComponent();
        }

        private void Modal_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                DemoRepository r = new DemoRepository();
                var tree = r.GetAdminLevelTree();
                demographyTree1.LoadTree(tree);
                demographyTree1.OnSelect += demographyTree1_OnSelect;
            }
        }

        void demographyTree1_OnSelect(AdminLevel obj)
        {
            OnSelect(obj);
            this.Close();
        }
    }
}
