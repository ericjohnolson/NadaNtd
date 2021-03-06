﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.UI.AppLogic;

using Nada.UI.View.Reports.CustomReport;
using Nada.Model.Reports;
using Nada.Globalization;
using Nada.Model;
using Nada.Model.Exports;
using Nada.UI.Base;
using System.IO;
using System.Configuration;
using Nada.Model.Repositories;
using Nada.UI.Controls;
using Nada.UI.View.Reports.Standard;
using Nada.UI.View.Reports.ExportWizard;

namespace Nada.UI.View.Reports
{
    public partial class ReportsDashboard : BaseControl, IView
    {
        public Action OnClose { get; set; }
        public Action<string> StatusChanged { get; set; }
        public string Title { get { return ""; } }

        public void SetFocus()
        {
        }

        public ReportsDashboard()
            : base()
        {
            InitializeComponent();
        }

        private void ReportsDashboard_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                lblCmJrf.SetMaxWidth(400);
                LoadSavedReports();
            }
        }

        private void LoadSavedReports()
        {
            ReportRepository repo = new ReportRepository();
            var reports = repo.GetCustomReports();
            tblReportBuilder.Visible = false;
            this.SuspendLayout();
            tblReportBuilder.Controls.Clear();
            int rowIndex = tblReportBuilder.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
            var tblNew = new TableLayoutPanel { AutoSize = true, AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink };
            tblNew.RowStyles.Clear();
            tblNew.ColumnStyles.Clear();
            tblNew.ColumnStyles.Add(new ColumnStyle { SizeType = System.Windows.Forms.SizeType.AutoSize });
            tblNew.ColumnStyles.Add(new ColumnStyle { SizeType = System.Windows.Forms.SizeType.AutoSize });
            tblNew.RowStyles.Add(new RowStyle { SizeType = System.Windows.Forms.SizeType.AutoSize });
            var name2 = new H3bLabel { Text = Translations.CustomReport, Name = "rpt_cr", AutoSize = true, };
            name2.SetMaxWidth(400);
            var edit2 = new H3Link { Text = Translations.NewLink, Margin = new Padding(0, 2, 0, 0) };
            edit2.ClickOverride += () =>
            {
                WizardForm wiz = new WizardForm(new StepCategory(), Translations.CustomReportBuilder);
                wiz.Height = 695;
                wiz.OnRunReport = RunCustomReport;
                wiz.Show();
            };
            tblNew.Controls.Add(name2, 0, 0);
            tblNew.Controls.Add(edit2, 1, 0);
            tblReportBuilder.Controls.Add(tblNew, 0, rowIndex);

            foreach (var report in reports)
            {
                rowIndex = tblReportBuilder.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
                var tbl = new TableLayoutPanel { AutoSize = true, AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink };
                tbl.RowStyles.Clear();
                tbl.ColumnStyles.Clear();
                tbl.ColumnStyles.Add(new ColumnStyle { SizeType = System.Windows.Forms.SizeType.AutoSize });
                tbl.ColumnStyles.Add(new ColumnStyle { SizeType = System.Windows.Forms.SizeType.AutoSize });
                tbl.ColumnStyles.Add(new ColumnStyle { SizeType = System.Windows.Forms.SizeType.AutoSize });
                tbl.RowStyles.Add(new RowStyle { SizeType = System.Windows.Forms.SizeType.AutoSize });
                var name = new H3bLabel { Text = report.DisplayName, Name = "rpt_" + report.DisplayName, AutoSize = true, };
                name.SetMaxWidth(400);
                var edit = new H3Link { Text = Translations.Edit + "...", Margin = new Padding(0, 2, 0, 0) };
                edit.ClickOverride += () =>
                {
                    WizardForm wiz = new WizardForm(new StepIndicators(report), Translations.CustomReportBuilder);
                    wiz.Height = 695;
                    wiz.OnRunReport = RunCustomReport;
                    wiz.Show();
                };
                var delete = new H3Link { Text = Translations.Delete + "...", Margin = new Padding(0, 2, 0, 0) };
                delete.ClickOverride += () => 
                {
                    DeleteConfirm confirm = new DeleteConfirm(Translations.Delete, Translations.DeleteConfirmMessage);
                    if (confirm.ShowDialog() == DialogResult.OK)
                    {
                        repo.DeleteCustomReport(report, ApplicationData.Instance.GetUserId());
                        LoadSavedReports();
                    }
                };
                tbl.Controls.Add(name, 0, 0);
                tbl.Controls.Add(edit, 1, 0);
                tbl.Controls.Add(delete, 2, 0);
                tblReportBuilder.Controls.Add(tbl, 0, rowIndex);
            }
            this.ResumeLayout();
            tblReportBuilder.Visible = true;
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "file:///" + Directory.GetCurrentDirectory() + Translations.HelpFile);
            //HelpView help = new HelpView();
            //help.Show();
        }


        private void RunCustomReport(SavedReport r)
        {
            CustomReportView report = new CustomReportView(r);
            report.OnEditReport = EditCustomReport;
            report.OnSave = LoadSavedReports;
            report.Show();
        }

        private void EditCustomReport(SavedReport r)
        {
            WizardForm wiz = new WizardForm(new StepIndicators(r), Translations.CustomReportBuilder);
            wiz.Height = 695;
            wiz.OnRunReport = RunCustomReport;
            wiz.Show();
        }

        private void lnkPcJrfForm_ClickOverride()
        {
            PcJrfExporter exporter = new PcJrfExporter();
            WizardForm wiz = new WizardForm(new JrfExportStep(exporter, exporter.ExportName), exporter.ExportName);
            wiz.OnFinish = () => { };
            wiz.ShowDialog();
        }

        private void lnkCmJrf_ClickOverride()
        {
            CmJrfExporter exporter = new CmJrfExporter();
            WizardForm wiz = new WizardForm(new ExportStep(exporter, exporter.ExportName), exporter.ExportName);
            wiz.OnFinish = () => { };
            wiz.ShowDialog();
        }
        
        private void lnkApocReport_ClickOverride()
        {
            WizardForm wiz = new WizardForm(new ApocExport(), Translations.ExportApocReport);
            wiz.OnFinish = () => { };
            wiz.ShowDialog();
        }

        private void eliminationReport_ClickOverride()
        {
            SavedReport sr = new SavedReport { StandardReportOptions = new EliminationReportOptions() };
            sr.ReportOptions.IsAllLocations = true;
            sr.ReportOptions.IsCountryAggregation = true;
            sr.ReportOptions.IsByLevelAggregation = false;
            sr.ReportOptions.IsNoAggregation = false;
            WizardForm wiz = new WizardForm(new EliminationOptions(sr), Translations.ReportProgressTowardsEliminiation);
            wiz.OnFinish = () => { };
            wiz.Height = 685;
            wiz.OnRunReport = RunEliminationReport;
            wiz.Show();
        }

        private void RunEliminationReport(SavedReport r)
        {
            CustomReportView report = new CustomReportView(r);
            report.OnEditReport = EditEliminationReport;
            report.Show();
        }

        private void EditEliminationReport(SavedReport r)
        {
            WizardForm wiz = new WizardForm(new EliminationOptions(r), Translations.ReportProgressTowardsEliminiation);
            wiz.Height = 685;
            wiz.OnRunReport = RunEliminationReport;
            wiz.Show();
        }

        private void rtiWorkbooks_ClickOverride()
        {
            WizardForm wiz = new WizardForm(new TaskForceCountryStep(), Translations.RtiCountryDiseaseWorkbook);
            wiz.OnFinish = () => { };
            wiz.ShowDialog();
        }

        private void pcEpiExportClick_ClickOverride()
        {
            ExportRepository r = new ExportRepository();
            ExportType t = r.GetExportType(ExportTypeId.PcEpi);
            t.Exporter = new PcEpiExporter();
            WizardForm wiz = new WizardForm(new GenericExportStep(t), Translations.ExportsPcEpiDataForm);
            wiz.OnFinish = () => { };
            wiz.ShowDialog();
        }

        private void c1Button2_Click(object sender, EventArgs e)
        {
            OnClose();
        }

        private void c1Button1_Click(object sender, EventArgs e)
        {
            OnClose();
        }

        private void redistrictingReport_ClickOverride()
        {
            SavedReport r = new SavedReport { TypeName = Translations.RedistrictingReport };
            r.ReportOptions = new ReportOptions();
            r.ReportOptions.ReportGenerator = new RedistrictingReportGenerator();
            CustomReportView report = new CustomReportView(r);
            report.OnEditReport = null;
            report.Show();
        }

        private void personsTreatedAndCoverageReport_ClickOverride()
        {
            SavedReport sr = new SavedReport { StandardReportOptions = new PersonsTreatedCoverageReportOptions() };
            sr.ReportOptions.IsAllLocations = true;
            sr.ReportOptions.IsCountryAggregation = true;
            sr.ReportOptions.IsByLevelAggregation = false;
            sr.ReportOptions.IsNoAggregation = false;
            WizardForm wiz = new WizardForm(new PersonsTreatedCoverageOptions(sr), Translations.PersonsTreatedAndCoverageReport);
            wiz.OnFinish = () => { };
            wiz.Height = 685;
            wiz.OnRunReport = RunPersonsTreatedAndCoverageReport;
            wiz.Show();
        }

        private void RunPersonsTreatedAndCoverageReport(SavedReport r)
        {
            CustomReportView report = new CustomReportView(r);
            report.OnEditReport = EditPersonsTreatedAndCoverageReport;
            report.Show();
        }

        private void EditPersonsTreatedAndCoverageReport(SavedReport r)
        {
            WizardForm wiz = new WizardForm(new PersonsTreatedCoverageOptions(r), Translations.PersonsTreatedAndCoverageReport);
            wiz.Height = 685;
            wiz.OnRunReport = RunPersonsTreatedAndCoverageReport;
            wiz.Show();
        }

        private void leishReport_ClickOverride()
        {
            LeishReportExporter exporter = new LeishReportExporter();
            WizardForm wiz = new WizardForm(new LeishReportStep(exporter, exporter.ExportName), Translations.LeishReport);
            wiz.OnFinish = () => { };
            wiz.ShowDialog();
        }


    }
}
