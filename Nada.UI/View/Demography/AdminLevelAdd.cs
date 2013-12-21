using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Globalization;
using Nada.Model;
using Nada.Model.Intervention;
using Nada.Model.Repositories;
using Nada.UI.AppLogic;
using Nada.UI.Base;

namespace Nada.UI.View
{
    public partial class AdminLevelAdd : BaseForm
    {
        public event Action OnSave = () => { };
        private AdminLevel model = new AdminLevel();
        private DemoRepository repo = new DemoRepository();
        private SettingsRepository settings = new SettingsRepository();

        public AdminLevelAdd()
            : base()
        {
            InitializeComponent();
        }

        public AdminLevelAdd(AdminLevel m)
            : base()
        {
            model = m;
            InitializeComponent();
        }

        private void DistributionMethodAdd_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                bindingSource1.DataSource = model;

                var levels = settings.GetAllAdminLevels();
                var t = repo.GetAdminLevelTree(levels.OrderByDescending(l => l.LevelNumber).ToArray()[1].Id, 0, true);
                treeAvailable.CanExpandGetter = m => ((AdminLevel)m).Children.Count > 0;
                treeAvailable.ChildrenGetter = delegate(object m)
                {
                    return ((AdminLevel)m).Children;
                };
                treeAvailable.SetObjects(t);
            }
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (!model.IsValid())
            {
                MessageBox.Show(Translations.ValidationError, Translations.ValidationErrorTitle);
                return;
            }

            if (treeAvailable.SelectedObjects.Count == 0)
            {
                MessageBox.Show(Translations.ParentIsRequired, Translations.ValidationErrorTitle);
                return;
            }

            var parent = (treeAvailable.SelectedObjects.Cast<AdminLevel>().First() as AdminLevel);
            model.ParentId = parent.Id;
            var childLevel = settings.GetAdminLevelTypeByLevel(parent.LevelNumber + 1);
            
            model.AdminLevelTypeId = childLevel.Id;
            bindingSource1.EndEdit();
            int userid = ApplicationData.Instance.GetUserId();
            repo.Save(model, userid);
            OnSave();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
