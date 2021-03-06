﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.UI.View;
using Nada.Model.Repositories;
using Nada.Model;
using Nada.UI.AppLogic;
using System.Configuration;
using Nada.UI.Base;
using Nada.Globalization;
using Nada.UI.View.Wizard;

namespace Nada.UI.Controls
{
    public partial class AdminLevelTypesControl : BaseControl
    {
        private int maxLevelsAllowed = 0;
        private List<AdminLevelType> types = null;
        private SettingsRepository r = null;
        public bool IsStartUp { get; set; }
        public AdminLevelTypesControl()
            : base()
        {
            InitializeComponent();
        }

        private void AdminLevelTypesControl_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                lblAggLevel.SetMaxWidth(550);
                maxLevelsAllowed = Convert.ToInt32(ConfigurationManager.AppSettings["MaxLevelsAllowed"]);
                r = new SettingsRepository();
                RefreshList();
            }
        }

        private void fieldLink1_OnClick()
        {
            int max = types.Max(t => t.LevelNumber);
            AdminLevelType type = new AdminLevelType { LevelNumber = max + 1 };
            AdminLevelTypeAdd add = new AdminLevelTypeAdd(type);
            // Tell the control if is being launched from StartUp
            add.IsStartUp = IsStartUp;
            add.OnSave += () => { RefreshList(); };
            add.ShowDialog();
        }

        private void lvLevels_HyperlinkClicked(object sender, BrightIdeasSoftware.HyperlinkClickedEventArgs e)
        {
            e.Handled = true;
            if (e.Column.AspectName == "EditText")
            {
                AdminLevelTypeAdd add = new AdminLevelTypeAdd((AdminLevelType)e.Model);
                // Tell the control if is being launched from StartUp
                add.IsStartUp = IsStartUp;
                add.OnSave += () => { RefreshList(); };
                add.ShowDialog();
            }
            else if (e.Column.AspectName == "DeleteText")
            {
                r.Delete((AdminLevelType)e.Model, ApplicationData.Instance.GetUserId());
                RefreshList();
            }
            else if (e.Column.AspectName == "ImportText")
            {
                WizardForm wiz = new WizardForm(new StepAdminLevelImport((AdminLevelType)e.Model, null, null), Translations.Import);
                wiz.OnFinish = () => { };
                wiz.OnClose = () => { };
                wiz.ShowDialog();
            }
        }

        public void RefreshList()
        {
            types = r.GetAllAdminLevels();
            lvLevels.SetObjects(types);
            if (types.Max(a => a.LevelNumber) >= maxLevelsAllowed)
                fieldLink1.Visible = false;
        }

        public bool HasAggregatingLevel()
        {
            return (types.FirstOrDefault(t => t.IsAggregatingLevel) != null);
        }

    }
}
