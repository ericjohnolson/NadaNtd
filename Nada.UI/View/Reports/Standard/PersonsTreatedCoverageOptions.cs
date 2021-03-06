﻿using Nada.Globalization;
using Nada.Model;
using Nada.Model.Diseases;
using Nada.Model.Intervention;
using Nada.Model.Reports;
using Nada.Model.Repositories;
using Nada.UI.AppLogic;
using Nada.UI.Base;
using Nada.UI.View.Reports.CustomReport;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nada.UI.View.Reports.Standard
{
    public partial class PersonsTreatedCoverageOptions : BaseControl, IWizardStep
    {
        private PersonsTreatedCoverageReportOptions options = null;
        private SavedReport report = null;
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public Action<SavedReport> OnRunReport { get; set; }
        public Action OnFinish { get; set; }
        public bool ShowNext { get { return true; } }
        public bool EnableNext { get { return true; } }
        public bool ShowPrev { get { return false; } }
        public bool EnablePrev { get { return false; } }
        public bool ShowFinish { get { return false; } }
        public bool EnableFinish { get { return false; } }
        public string StepTitle { get { return Translations.ReportOptions; } }

        // TODO Replace with Translations
        private static string ReportTypeDisease = "Diseases";
        private static string ReportTypeDrugPackage = "Drug Package";

        public PersonsTreatedCoverageOptions()
            : base()
        {
            InitializeComponent();
        }

        public PersonsTreatedCoverageOptions(SavedReport o)
            : base()
        {
            report = o;
            options = (PersonsTreatedCoverageReportOptions)o.StandardReportOptions;

            // Clear previous report results
            report.ReportOptions.Columns = new Dictionary<string, AggregateIndicator>();
            report.ReportOptions.SelectedIndicators = new List<ReportIndicator>();

            InitializeComponent();
        }

        private void cbReportTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Vary the form based on the report type
            ToggleReportType();
        }

        public void DoNext()
        {
            // Initial report options
            report.ReportOptions.IsByLevelAggregation = false;
            report.ReportOptions.IsCountryAggregation = false;
            report.ReportOptions.IsNoAggregation = true;
            report.ReportOptions.Years = lbYears.SelectedItems.Cast<int>().ToList(); // Need to set this now since this report options does not use a date range selector, uses year selector
            report.ReportOptions.MonthYearStarts = 1;
            // Save the report options
            // Report type
            if (cbReportTypes.SelectedItem.ToString() == ReportTypeDisease)
            {
                options.isReportTypeDisease = true;
                // Selected diseases
                options.Diseases = lbDiseases.SelectedItems.Cast<Disease>().ToList();
                report.ReportOptions.ReportGenerator = new PersonsTreatedCoverageDiseaseReportGenerator();
            }
            else
            {
                options.isReportTypeDisease = false;
                // Selected drug packages
                options.DrugPackages = lbDrugPackages.SelectedItems.Cast<IntvType>().ToList();
                report.ReportOptions.ReportGenerator = new PersonsTreatedCoverageDrugPackageReportGenerator();
            }
            // Admin level type
            options.DistrictType = (AdminLevelType)cbAggregateBy.SelectedItem;
            //  Selected years
            options.Years = report.ReportOptions.Years;

            // Set the title
            report.TypeName = Translations.PersonsTreatedAndCoverageReport;

            // Next step
            OnSwitchStep(new StepLocations(report, this));
        }

        public void DoPrev()
        {
            throw new NotImplementedException();
        }

        public void DoFinish()
        {
            throw new NotImplementedException();
        }

        private void StepOptions_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                // Set up the report inputs
                SetupReportTypeInput();
                SetupYearListBox();

                // Diseases
                DiseaseRepository diseaseRepo = new DiseaseRepository();
                var diseases = diseaseRepo.GetSelectedDiseases().Where(d => d.DiseaseType == Translations.PC).ToList();
                diseaseBindingSource.DataSource = diseases;
                // Add the diseases to the collection of available ones
                options.AvailableDiseases = diseases;

                // Interventions
                IntvRepository intvRepo = new IntvRepository();
                // The interventions we want to use to popualte the list
                List<int> interventionTypeIds = new List<int>
                {
                    (int)StaticIntvType.Alb, (int)StaticIntvType.Alb2, (int)StaticIntvType.DecAlb, (int)StaticIntvType.Ivm, (int)StaticIntvType.IvmAlb,
                    (int)StaticIntvType.IvmPzq, (int)StaticIntvType.IvmPzqAlb, (int)StaticIntvType.Mbd, (int)StaticIntvType.Pzq, (int)StaticIntvType.PzqAlb,
                    (int)StaticIntvType.PzqMbd, (int)StaticIntvType.ZithroTeo
                };
                List<IntvType> intvTypes = intvRepo.GetAllTypes().Where(i => interventionTypeIds.Contains(i.Id)).OrderBy(i => i.IntvTypeName).ToList();
                intvTypeBindingSource.DataSource = intvTypes;
                // Add the interventions to the collection of available ones
                options.AvailableDrugPackages = intvTypes;

                // Admin level types
                SettingsRepository settingsRepo = new SettingsRepository();
                var allLevelTypes = settingsRepo.GetAllAdminLevels();
                adminLevelTypeBindingSource.DataSource = allLevelTypes;

                // Repopulate the previous report options
                if (options != null)
                    RepopulateOptions();
            }
        }

        private void ToggleReportType()
        {
            if (cbReportTypes.SelectedItem.ToString() == ReportTypeDisease)
            {
                lbDrugPackageLabel.Visible = false;
                lbDrugPackages.Visible = false;
                lbDiseasesLabel.Visible = true;
                lbDiseases.Visible = true;
            }
            else
            {
                lbDiseasesLabel.Visible = false;
                lbDiseases.Visible = false;
                lbDrugPackageLabel.Visible = true;
                lbDrugPackages.Visible = true;
            }
        }

        private void SetupReportTypeInput()
        {
            cbReportTypes.Items.Add(ReportTypeDisease);
            cbReportTypes.Items.Add(ReportTypeDrugPackage);
            // Set the default report type
            cbReportTypes.SelectedItem = ReportTypeDisease;
            // Set the initial state of the dropdown
            ToggleReportType();
        }

        private void SetupYearListBox()
        {
            for (int i = DateTime.Now.Year; i > 1900; i--)
            {
                lbYears.Items.Add(i);
            }
        }

        private void RepopulateOptions()
        {
            // Report type
            if (options.isReportTypeDisease)
            {
                cbReportTypes.SelectedItem = ReportTypeDisease;
                // Reselect diseases
                if (options.Diseases != null)
                {
                    // Clear the selected items
                    lbDiseases.ClearSelected();
                    // Get the items in the list as Disease objects
                    List<Disease> diseases = lbDiseases.Items.Cast<Disease>().ToList();
                    // Iterate through the previous options and select the corresponding options in the listbox
                    foreach (Disease disease in options.Diseases)
                    {
                        Disease diseaseItem = diseases.Where(d => d.Id == disease.Id).FirstOrDefault();
                        if (diseaseItem != null)
                            lbDiseases.SetSelected(diseases.IndexOf(diseaseItem), true);
                    }
                }
            }
            else
            {
                cbReportTypes.SelectedItem = ReportTypeDrugPackage;
                // Reselect drug packages
                if (options.DrugPackages != null)
                {
                    // Clear the selected items
                    lbDrugPackages.ClearSelected();
                    // Get the items in the list as IntvType objects
                    List<IntvType> interventions = lbDrugPackages.Items.Cast<IntvType>().ToList();
                    // Iterate through the previous options and select the corresponding options in the listbox
                    foreach (IntvType intervention in options.DrugPackages)
                    {
                        IntvType interventionItem = interventions.Where(i => i.Id == intervention.Id).FirstOrDefault();
                        if (interventionItem != null)
                            lbDrugPackages.SetSelected(interventions.IndexOf(interventionItem), true);
                    }
                }
            }
            // Admin level type
            if (options.DistrictType != null)
                cbAggregateBy.SelectedIndex = cbAggregateBy.FindStringExact(options.DistrictType.DisplayName);
            // Years
            if (options.Years != null)
            {
                foreach (int year in options.Years)
                {
                    lbYears.SetSelected(lbYears.Items.IndexOf(year), true);
                }
            }
        }
    }
}
