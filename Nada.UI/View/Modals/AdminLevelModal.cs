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
    public partial class AdminLevelModal : Form
    {
        private AdminLevelType model = null;
        private int level = 1;
        public event Action<AdminLevelType> OnSave = (e) => { };

        public AdminLevelModal()
        {
            InitializeComponent();
        }

        public AdminLevelModal(int nextLevel)
        {
            InitializeComponent();
            level = nextLevel;
        }

        private void Modal_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                model = new AdminLevelType { LevelNumber = level };
                bsAdminLevel.DataSource = model;
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            SettingsRepository repo = new SettingsRepository();
            repo.InsertAdminLevel(model, ApplicationData.Instance.GetUserId());
            OnSave(model);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
