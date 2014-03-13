using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.UI.AppLogic;
using Nada.Model;
using Nada.Model.Repositories;
using Nada.UI.Base;
using Nada.Globalization;

namespace Nada.UI.View.Demography
{
    public partial class AdminUnitAdd : BaseControl, IView
    {
        private DemoRepository repo = new DemoRepository();
        private SettingsRepository settings = new SettingsRepository();
        public Action OnClose { get; set; }
        private AdminLevel model = new AdminLevel();
        public Action<string> StatusChanged { get; set; }
        public string Title { get { return Translations.AdminLevel; } }
        public event Action<AdminLevel> OnSelect = (e) => { };
        public void SetFocus() { }

        public AdminUnitAdd()
        {
            InitializeComponent();
        }

        private void StepCategory_Load(object sender, EventArgs e)
        {
        }

        public void LoadLevel(int levelTypeId)
        {
            Localizer.TranslateControl(this);
            bindingSource1.DataSource = model;

            var t = repo.GetAdminLevelTree(levelTypeId, 0, true);
            treeAvailable.CanExpandGetter = m => ((AdminLevel)m).Children.Count > 0;
            treeAvailable.ChildrenGetter = delegate(object m)
            {
                return ((AdminLevel)m).Children;
            };
            treeAvailable.SetObjects(t);
        }

        public bool IsValid()
        {
            if (!model.IsValid())
            {
                MessageBox.Show(Translations.ValidationError, Translations.ValidationErrorTitle);
                return false;
            }

            if (treeAvailable.SelectedObjects.Count == 0)
            {
                MessageBox.Show(Translations.ParentIsRequired, Translations.ValidationErrorTitle);
                return false;
            }
            return true;
        }

        public AdminLevel GetModel()
        {
            var parent = (treeAvailable.SelectedObjects.Cast<AdminLevel>().First() as AdminLevel);
            model.ParentId = parent.Id;
            var childLevel = settings.GetAdminLevelTypeByLevel(parent.LevelNumber + 1);

            model.AdminLevelTypeId = childLevel.Id;
            bindingSource1.EndEdit();
            return model;
        }

    }
}
