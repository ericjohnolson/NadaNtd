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
        private AdminLevel model = new AdminLevel();
        private DemoRepository repo = new DemoRepository();
        private SettingsRepository settings = new SettingsRepository();
        private AdminLevelType parentType;
        private int childLevel = 0;
        public Action OnClose { get; set; }
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

        public void LoadUnit(AdminLevel m)
        {
            model = m;
            DoLoad();
        }

        public void LoadUnit(int levelNumber)
        {
            childLevel = levelNumber;
            DoLoad();
        }

        public void DoLoad()
        {
            if (model.Id > 0)
            {
                treeAvailable.Enabled = false;
                lblParent.Enabled = false;
            }

            if (model.LatWho.HasValue)
                model.BindingLat = model.LatWho.Value.ToString();
            if (model.LngWho.HasValue)
                model.BindingLng = model.LngWho.Value.ToString();

            Localizer.TranslateControl(this);
            bindingSource1.DataSource = model;

            var levels = settings.GetAllAdminLevelsWithCountry().OrderByDescending(l => l.LevelNumber).ToList();
            int levelTypeId = 0;
            if (childLevel == 0)
                levelTypeId = levels.ToArray()[1].Id;
            else
            {
                var child = levels.FirstOrDefault(l => l.LevelNumber == childLevel);
                parentType = levels.ToArray()[levels.IndexOf(child) + 1];
                levelTypeId = parentType.Id;
            }

            var t = repo.GetAdminLevelTree(levelTypeId, 0, true);
            treeAvailable.CanExpandGetter = m => ((AdminLevel)m).Children.Count > 0;
            treeAvailable.ChildrenGetter = delegate(object m)
            {
                return ((AdminLevel)m).Children;
            };
            treeAvailable.SetObjects(t);
        }
        
        public AdminLevel GetModel()
        {
            errorProvider1.DataSource = bindingSource1;
            errorProvider1.SetError(tbLat, "");
            errorProvider1.SetError(tbLng, "");
            errorProvider1.SetError(tbName, "");
            model.LatWho = null;
            model.LngWho = null;
            double d = -999, d2 = -999;
            if (tbLat.Text.Length > 0)
                if (!double.TryParse(tbLat.Text, out d) || (double.TryParse(tbLat.Text, out d) && (d > 90 || d < -90)))
                {
                    errorProvider1.SetError(tbLat, Translations.ValidLatitude);
                    model.LatWho = -999;
                }
                else
                    model.LatWho = d;


            if (tbLng.Text.Length > 0)
                if (!double.TryParse(tbLng.Text, out d2) || (double.TryParse(tbLng.Text, out d2) && (d2 > 180 || d2 < -180)))
                {
                    errorProvider1.SetError(tbLng, Translations.ValidLongitude);
                    model.LngWho = -999;
                }
                else
                    model.LngWho = d2;

            if (string.IsNullOrEmpty(tbName.Text))
                errorProvider1.SetError(tbName, Translations.Required);

            if (!model.IsValid())
            {
                MessageBox.Show(Translations.ValidationError, Translations.ValidationErrorTitle);
                return new AdminLevel();
            }

            if (model.Id == 0)
            {
                if (treeAvailable.SelectedObjects.Count == 0)
                {
                    MessageBox.Show(Translations.ParentIsRequired, Translations.ValidationErrorTitle);
                    return new AdminLevel();
                }

                var parent = (treeAvailable.SelectedObjects.Cast<AdminLevel>().First() as AdminLevel);
                if (parentType != null && parent.LevelNumber != parentType.LevelNumber)
                {
                    MessageBox.Show(string.Format(Translations.ParentLevelMustBeSame, parentType.DisplayName), Translations.ValidationErrorTitle);
                    return new AdminLevel();
                }

                model.ParentId = parent.Id;
                var childLevel = settings.GetAdminLevelTypeByLevel(parent.LevelNumber + 1);
                model.AdminLevelTypeId = childLevel.Id;
            }

            bindingSource1.EndEdit();
            int userid = ApplicationData.Instance.GetUserId();
            repo.Save(model, userid);
            return model;
        }


    }
}
