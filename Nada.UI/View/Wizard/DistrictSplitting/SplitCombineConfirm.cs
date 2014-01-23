using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.UI.AppLogic;
using Nada.Globalization;
using Nada.Model.Reports;
using Nada.Model.Repositories;
using Nada.Model;
using Nada.UI.Base;
using Nada.Model.Imports;
using Nada.UI.View.Wizard.DistrictSplitting;
using Nada.UI.Controls;

namespace Nada.UI.View.Wizard
{

    public partial class SplitCombineConfirm : BaseControl, IWizardStep
    {
        private DemoRepository repo = new DemoRepository();
        private SettingsRepository settings = new SettingsRepository();
        private SplittingOptions options = null;
        private IWizardStep prev = null;
        public Action OnFinish { get; set; }
        public Action<ReportOptions> OnRunReport { get; set; }
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public bool ShowNext { get { return true; } }
        public bool EnableNext { get { return true; } }
        public bool ShowPrev { get { return true; } }
        public bool EnablePrev { get { return true; } }
        public bool ShowFinish { get { return false; } }
        public bool EnableFinish { get { return false; } }
        public string StepTitle { get { return Translations.ReviewAndConfirm; } }

        public SplitCombineConfirm(SplittingOptions o, IWizardStep p)
            : base()
        {
            prev = p;
            options = o;
            InitializeComponent();
        }

        public void DoPrev()
        {
            OnSwitchStep(prev);
        }

        public void DoNext()
        {
            OnSwitchStep(new SplitReviewConfirm(options, Translations.SplitConfirmReview));
        }

        public void DoFinish()
        {
        }

        private void StepCategory_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                h3bLabel1.MakeBold();
                h3bLabel2.MakeBold();
                h3bLabel3.MakeBold();

                for (int i = 0; i < options.SplitIntoNumber.Value; i++)
                {
                    var index = tblNewUnits.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
                    var lblName = new H3bLabel { AutoSize = true, Text = options.MergeSources[i].Name, Margin = new Padding(0, 5, 10, 5) };
                    tblNewUnits.Controls.Add(lblName, 0, index);
                    string pop = "0";
                    if (options.MergeSources[i].CurrentDemography.TotalPopulation.HasValue)
                        pop = Convert.ToDouble(options.MergeSources[i].CurrentDemography.TotalPopulation.Value * 
                            options.SplitDestinations[i].Percent).ToString("N");
                    var label = new H3bLabel { AutoSize = true, Text = pop, Margin = new Padding(0, 5, 10, 5) };
                    tblNewUnits.Controls.Add(label, 1, index);
                    string villages = "N/A";
                    if(options.MergeDestination.Children.FirstOrDefault(a => a.ParentId == options.MergeSources[i].Id) != null)
                        villages = String.Join(", ", options.MergeDestination.Children.Where(a => a.ParentId == options.MergeSources[i].Id).Select(b => b.Name).ToArray());
                    var lblVillages = new H3bLabel { AutoSize = true, Text = villages, Margin = new Padding(0, 5, 10, 5) };
                    tblNewUnits.Controls.Add(lblVillages, 2, index);
                }
            }
        }




    }
}
