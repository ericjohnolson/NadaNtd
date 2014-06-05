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
using Nada.UI.Controls;
using Nada.Model.Demography;
using Nada.UI.View.Reports;

namespace Nada.UI.View.Wizard
{
    public partial class SplittingAdminLevel : BaseControl, IWizardStep
    {
        public class ChooseAdminLevel
        {
            public AdminUnitChooser Chooser { get; set; }
            public AdminLevel Source { get; set; }
        }

        private DemoRepository repo = new DemoRepository();
        private SettingsRepository settings = new SettingsRepository();
        private RedistrictingOptions options = null;
        private List<ChooseAdminLevel> choosers = new List<ChooseAdminLevel>();
        private List<TextBox> percents = new List<TextBox>();
        private List<H3bLabel> labels = new List<H3bLabel>();
        public Action OnFinish { get; set; }
        public Action<SavedReport> OnRunReport { get; set; }
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public bool ShowNext { get { return true; } }
        public bool EnableNext { get { return true; } }
        public bool ShowPrev { get { return true; } }
        public bool EnablePrev { get { return true; } }
        public bool ShowFinish { get { return false; } }
        public bool EnableFinish { get { return false; } }
        public string StepTitle { get { return Translations.SplittingPercentages; } }

        public SplittingAdminLevel(RedistrictingOptions o)
            : base()
        {
            options = o;
            InitializeComponent();
        }

        public void DoPrev()
        {
            OnSwitchStep(new SplittingIntoNumber(options));
        }

        public void DoNext()
        {
            options.SplitDestinations = new List<AdminLevelAllocation>();
            double total = 0;
            for (int i = 0; i < options.SplitIntoNumber.Value; i++)
            {
                double p = 0;
                if (!double.TryParse(percents[i].Text, out p))
                {
                    MessageBox.Show(Translations.SplitPercentsRequired, Translations.ValidationErrorTitle);
                    return;
                }
                if (p < 0 || p > 100)
                {
                    MessageBox.Show(Translations.SplitPercentsValid, Translations.ValidationErrorTitle);
                    return;
                }

                AdminLevel unit = choosers[i].Chooser.GetSelected();
                if (unit == null)
                {
                    MessageBox.Show(Translations.SplitAdminUnitsRequired, Translations.ValidationErrorTitle);
                    return;
                }
                total += p;
                options.SplitDestinations.Add(new AdminLevelAllocation { Percent = p, Unit = unit });
            }

            if (options.SplitType == SplittingType.Split && total != 100)
            {
                MessageBox.Show(Translations.SplitPercentsTotal, Translations.ValidationErrorTitle);
                return;
            }

            OnSwitchStep(new SplittingDemography(options, 0));
        }

        public void DoFinish()
        {
        }

        private void SplittingAdminLevel_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                int levelNumber = 0;
                
                if (options.SplitType == SplittingType.SplitCombine)
                    levelNumber = options.MergeSources[0].LevelNumber;
                else
                    levelNumber = options.Source.LevelNumber;

                for (int i = 0; i < options.SplitIntoNumber.Value; i++)
                {
                    var index = tblNewUnits.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });

                    var cntrl = new TextBox { Width = 100, Margin = new Padding(0, 5, 10, 5), Tag = i };
                    var chooser = new AdminUnitChooser(levelNumber);
                    chooser.Margin = new Padding(0, 5, 10, 5);
                    AdminLevel source = null;
                    if (options.SplitType == SplittingType.SplitCombine)
                    {
                        chooser.HideNewLink();
                        chooser.Select(options.MergeSources[i], true);
                        options.MergeSources[i].Children = repo.GetAdminLevelChildren(options.MergeSources[i].Id);
                        options.MergeSources[i].CurrentDemography = repo.GetRecentDemography(options.MergeSources[i].Id);
                        source = options.MergeSources[i];
                    }
                    else if (options.SplitType == SplittingType.Split)
                    {
                        if (options.SplitDestinations.Count() >= i + 1)
                        {
                            chooser.Select(options.SplitDestinations[i].Unit, true);
                            cntrl.Text = options.SplitDestinations[i].Percent.ToString();
                        }
                        source = options.Source;
                    }

                    // Add control to screen
                    choosers.Add(new ChooseAdminLevel { Chooser = chooser, Source = source });
                    tblNewUnits.Controls.Add(chooser, 0, index);
                    var label = new H3bLabel { AutoSize = true, Text = "0", Margin = new Padding(0, 5, 10, 5) };
                    label.MakeBold();
                    labels.Add(label);
                    tblNewUnits.Controls.Add(label, 2, index);
                    cntrl.TextChanged += cntrl_TextChanged;
                    percents.Add(cntrl);
                    tblNewUnits.Controls.Add(cntrl, 1, index);
                }
            }
        }

        void cntrl_TextChanged(object sender, EventArgs e)
        {
            int index = (int)(sender as TextBox).Tag;
            Nullable<double> totalPop;
            totalPop = choosers[index].Source.CurrentDemography.TotalPopulation;
            if (!totalPop.HasValue)
                return;
            double percent = 0;
            if (double.TryParse((sender as TextBox).Text, out percent))
            {
                double multiplier = percent / 100;
                labels[index].Text = Convert.ToDouble(totalPop.Value * multiplier).ToString("N");
            }
        }



    }
}
