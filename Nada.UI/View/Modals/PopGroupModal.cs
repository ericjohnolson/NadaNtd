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
    public partial class PopGroupModal : Form
    {
        private PopGroup model = null;
        public event Action<PopGroup> OnSave = (e) => { };

        public PopGroupModal()
        {
            InitializeComponent();
        }

        private void Modal_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                model = new PopGroup();
                bsPopGroup.DataSource = model;
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            SettingsRepository repo = new SettingsRepository();
            repo.InsertPopGroup(model, ApplicationData.Instance.GetUserId());
            OnSave(model);
            this.Close();
        }

    }
}
