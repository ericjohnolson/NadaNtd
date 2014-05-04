using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Globalization;
using Nada.Model;
using Nada.Model.Csv;
using Nada.Model.Imports;
using Nada.Model.Reports;
using Nada.Model.Repositories;
using Nada.UI.AppLogic;
using Nada.UI.Base;
using OfficeOpenXml;
using Nada.Model.Demography;

namespace Nada.UI.View.Wizard
{
    public partial class SplittingDemography : BaseControl, IWizardStep
    {
        DemoRepository repo = new DemoRepository();
        private int index = 0;
        private List<AdminLevel> available = new List<AdminLevel>();
        private List<AdminLevel> selected = new List<AdminLevel>();
        private RedistrictingOptions options;
        public Action OnFinish { get; set; }

        public Action<IWizardStep> OnSwitchStep { get; set; }
        public Action<SavedReport> OnRunReport { get; set; }
        public bool ShowNext { get { return true; } }
        public bool EnableNext { get { return true; } }
        public bool ShowPrev { get { return true; } }
        public bool EnablePrev { get { return true; } }
        public bool ShowFinish { get { return false; } }
        public bool EnableFinish { get { return false; } }
        public string StepTitle { get { return Translations.AdministrativeLevels; } }

        public SplittingDemography(RedistrictingOptions o, int i)
            : base()
        {
            options = o;
            index = i;
            InitializeComponent();
        }

        private void ImportOptions_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                if (options.SplitType == SplittingType.Split)
                {
                    lblDestination.Text = options.SplitDestinations[index].Unit.Name;
                    lblSource.Text = options.Source.Name;
                }
                else
                {
                    lblSource.Text = options.MergeSources[index].Name;
                    lblDestination.Text = options.MergeDestination.Name;
                }
                LoadLists();
            }
        }

        public void DoPrev()
        {
            if (0 == index)
                OnSwitchStep(new SplittingAdminLevel(options));
            else
                OnSwitchStep(new SplittingDemography(options, index - 1));
        }

        public void DoNext()
        {

            if (options.SplitDestinations.Count() - 1 == index)
            {
                if (options.SplitType == SplittingType.SplitCombine)
                {
                    options.MergeDestination.Children.AddRange(selected);
                    // HERE
                    OnSwitchStep(new SplitCombineConfirm(options, this));
                }
                else
                {
                    if (options.SplitChildren.Count != 0)
                    {
                        MessageBox.Show(Translations.SplitChildrenAllocatedError, Translations.ValidationErrorTitle);
                        return;
                    }
                    // HERE!
                    OnSwitchStep(new SplitReviewConfirm(options, Translations.SplitConfirmReview));
                }
                return;
            }
        
            if (options.SplitType == SplittingType.SplitCombine)
                options.MergeDestination.Children.AddRange(selected);
            OnSwitchStep(new SplittingDemography(options, index + 1));
        }

        public void ExecuteRedistricting()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            OnSwitchStep(new WorkingStep(Translations.Running));
            worker.RunWorkerAsync(options);
        }

        public void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RedistrictingResult result = (RedistrictingResult)e.Result;
            if (result.HasError)
                OnSwitchStep(new MessageBoxStep(Translations.ErrorOccured, result.ErrorMessage, true, this));
            else
                OnSwitchStep(new SplitReviewConfirm(options, Translations.SplitMergeConfirmReview));
        }

        public void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                RedistrictingExpert expert = new RedistrictingExpert();
                e.Result = expert.DoMerge((RedistrictingOptions)e.Argument);
            }
            catch (Exception ex)
            {
                Logger log = new Logger();
                log.Error("Exception occured when performing merge (MergeDestination:worker_DoWork).", ex);
                e.Result = new RedistrictingResult { HasError = true, ErrorMessage = Translations.UnexpectedException + " " + ex.Message };
            }
        }

        public void DoFinish()
        {

        }

        public void LoadLists()
        {
            if (options.SplitType == SplittingType.Split)
            {
                available = options.SplitChildren;
                selected = new List<AdminLevel>();
                options.SplitDestinations[index].Unit.Children = selected;
            }
            else
            {
                available = options.MergeSources[index].Children;
                selected = new List<AdminLevel>();
            }
            ReloadLists();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (var item in available)
                selected.Add(item);
            available.Clear();
            ReloadLists();
        }

        private void treeAvailable_DoubleClick(object sender, EventArgs e)
        {
            SelectItems();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            SelectItems();
        }

        private void SelectItems()
        {
            foreach (var item in lstAvailable.SelectedObjects.Cast<AdminLevel>())
            {
                selected.Add(item);
                available.Remove(item);
            }
            ReloadLists();
        }

        private void btnDeselect_Click(object sender, EventArgs e)
        {
            DeselectItems();
        }

        private void treeSelected_DoubleClick(object sender, EventArgs e)
        {
            DeselectItems();
        }

        private void DeselectItems()
        {
            foreach (var item in lstSelected.SelectedObjects.Cast<AdminLevel>())
            {
                available.Add(item);
                selected.Remove(item);
            }
            ReloadLists();
        }

        private void btnDeselectAll_Click(object sender, EventArgs e)
        {
            foreach (var item in selected)
                available.Add(item);
            selected.Clear();
            ReloadLists();
        }

        private void ReloadLists()
        {
            lstAvailable.ClearObjects();
            lstAvailable.SetObjects(available.OrderBy(i => i.Name).ToList());
            lstSelected.ClearObjects();
            lstSelected.SetObjects(selected.OrderBy(i => i.Name).ToList());
        }

    }
}
