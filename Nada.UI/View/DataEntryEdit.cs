﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model.Survey;
using Nada.Model.Repositories;
using Nada.UI.AppLogic;
using Nada.Model;
using Nada.Globalization;
using Nada.UI.View.Survey;
using Nada.Model.Intervention;

using Nada.Model.Diseases;
using Nada.UI.ViewModel;
using Nada.UI.Base;
using Nada.UI.Controls;
using System.Web.Security;
using System.IO;
using System.Configuration;

namespace Nada.UI.View.DiseaseDistribution
{
    public partial class DataEntryEdit : BaseControl, IView
    {
        private IDataEntryVm viewModel = null;
        public Action OnClose { get; set; }
        public Action<string> StatusChanged { get; set; }
        public string Title { get { return viewModel.Title; } }
        public void SetFocus()
        {
            btnHelp.Focus();
        }

        public DataEntryEdit()
            : base()
        {
            InitializeComponent();
        }

        public DataEntryEdit(IDataEntryVm vm)
            : base()
        {
            viewModel = vm;
            InitializeComponent();
        }

        private void DataEntryEdit_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                tblTitle.Focus();
                lblLocation.ForeColor = viewModel.FormColor;
                lblAdminLevel.Text = viewModel.LocationName;
                tbNotes.Text = viewModel.Notes;
                if (viewModel.MetaData != null && viewModel.MetaData.Count > 0)
                    indicatorControl1.LoadMetaData(viewModel.MetaData);
                if (viewModel.Indicators != null && viewModel.Indicators.Count() > 0)
                    indicatorControl1.LoadIndicators(viewModel.Indicators, viewModel.IndicatorValues, viewModel.IndicatorDropdownValues, viewModel.EntityType);
                indicatorControl1.OnAddRemove += customIndicatorControl1_OnAddRemove;
                StatusChanged(viewModel.StatusMessage);
                // design
                lblTitle.Text = TranslationLookup.GetValue(viewModel.Title, viewModel.Title);
                lblDiseaseType.Text = TranslationLookup.GetValue(viewModel.TypeTitle, viewModel.TypeTitle);
                lblTitle.ForeColor = viewModel.FormColor;
                lblDiseaseType.ForeColor = viewModel.FormColor;
                statCalculator1.TextColor = viewModel.FormColor;
                indicatorControl1.TextColor = viewModel.FormColor;
                hrTop.RuleColor = viewModel.FormColor;
                hr4.RuleColor = viewModel.FormColor;
                hr5.RuleColor = viewModel.FormColor;
                // calclulator
                if (viewModel.Calculator != null)
                {
                    statCalculator1.Calc = viewModel.Calculator;
                    statCalculator1.OnCalc += statCalculator1_OnCalc;
                    statCalculator1_OnCalc();
                }
                else
                    statCalculator1.Visible = false;
                // special controls
                viewModel.AddSpecialControls(indicatorControl1);
                if (!Roles.IsUserInRole(ApplicationData.Instance.CurrentUser.UserName, "RoleDataEnterer") &&
                !Roles.IsUserInRole(ApplicationData.Instance.CurrentUser.UserName, "RoleAdmin"))
                {
                    tblEdit.Visible = false;
                }
            }
        }

        private void statCalculator1_OnCalc()
        {
            statCalculator1.DoCalc(viewModel.Indicators, indicatorControl1.GetValues(), viewModel.Location.Id, viewModel.CalculatorTypeId);
            c1Button1.Focus();
        }
                
        /// <summary>
        /// SAVE Method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void save_Click(object sender, EventArgs e)
        {
            if (!viewModel.IsValid() || !indicatorControl1.IsValid())
            {
                MessageBox.Show(Translations.ValidationError, Translations.ValidationErrorTitle);
                return;
            }
            
            viewModel.DoSave(indicatorControl1.GetValues(), tbNotes.Text);
            OnClose();
        }

        void customIndicatorControl1_OnAddRemove()
        {
            IndicatorTypeEdit editor = new IndicatorTypeEdit(viewModel);
            editor.OnSave += editType_OnSave;
            ViewForm form = new ViewForm(editor);
            form.ShowDialog();
        }

        void editType_OnSave()
        {
            indicatorControl1.LoadIndicators(viewModel.Indicators, viewModel.IndicatorDropdownValues, viewModel.EntityType);
        }
        
        private void cancel_Click(object sender, EventArgs e)
        {
            OnClose();
        }

        private void btnDash_Click(object sender, EventArgs e)
        {
            OnClose();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "file:///" + Directory.GetCurrentDirectory() + ConfigurationManager.AppSettings["HelpFile"]);
            //HelpView help = new HelpView();
            //help.Show();
        }
    }
}
