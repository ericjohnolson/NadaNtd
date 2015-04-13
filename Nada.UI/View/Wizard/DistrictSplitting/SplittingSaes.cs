using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.UI.AppLogic;
using Nada.Model.Reports;
using Nada.Globalization;
using Nada.Model;
using Nada.UI.View.Wizard;
using System.Threading;
using Nada.UI.Base;
using Nada.Model.Repositories;
using Nada.Model.Exports;
using Nada.Model.Demography;

namespace Nada.UI.View.Reports
{
    public partial class SplittingSaes : BaseControl, IWizardStep
    {
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public Action<SavedReport> OnRunReport { get; set; }
        public Action OnFinish { get; set; }
        public bool ShowNext { get { return true; } }
        public bool EnableNext { get { return true; } }
        public bool ShowPrev { get { return true; } }
        public bool EnablePrev { get { return true; } }
        public bool ShowFinish { get { return false; } }
        public bool EnableFinish { get { return false; } }
        public string StepTitle { get { return Translations.SplittingSaes; } }
        private DemoRepository demo = new DemoRepository();
        private List<SaeMatcher> matchers = new List<SaeMatcher>();
        IWizardStep prev;
        RedistrictingOptions options;

        public SplittingSaes(RedistrictingOptions o, IWizardStep p)
            : base()
        {
            InitializeComponent();
            options = o;
            prev = p;
        }

        private void SplittingSaes_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);

                ProcessRepository repo = new ProcessRepository();

                if (options.SplitType == SplittingType.Split)
                {
                    var saes = repo.GetAllForAdminLevel(options.Source.Id).Where(i => i.TypeId == (int)StaticProcessType.SAEs);

                    foreach (var s in saes)
                    {
                        var sae = repo.GetById(s.Id);
                        var index = tblNewUnits.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
                        var chooser = new SaeMatcher(sae, options.SplitDestinations.Select(d => d.Unit).ToList(), options.Source);
                        chooser.Margin = new Padding(0, 5, 10, 5);
                        tblNewUnits.Controls.Add(chooser, 0, index);
                        matchers.Add(chooser);
                    }
                }
                else if (options.SplitType == SplittingType.SplitCombine)
                {
                    foreach (var source in options.MergeSources)
                    {
                        var saes = repo.GetAllForAdminLevel(source.Id).Where(i => i.TypeId == (int)StaticProcessType.SAEs);

                        foreach (var s in saes)
                        {
                            var sae = repo.GetById(s.Id);
                            var index = tblNewUnits.RowStyles.Add(new RowStyle { SizeType = SizeType.AutoSize });
                            List<AdminLevel> dests = new List<AdminLevel> { source, options.MergeDestination };
                            var chooser = new SaeMatcher(sae, dests, options.Source);
                            chooser.Margin = new Padding(0, 5, 10, 5);
                            tblNewUnits.Controls.Add(chooser, 0, index);
                            matchers.Add(chooser);
                        }
                    }
                }

                if (matchers.Count() == 0)
                    DoNextStep();
            }
        }

        public void DoPrev()
        {
            OnSwitchStep(prev);
        }

        public void DoNext()
        {
            var invalid = matchers.FirstOrDefault(m => !m.IsValid());
            if (invalid != null)
            {
                MessageBox.Show(Translations.SplittingMustAllocateSaes, Translations.ValidationErrorTitle);
                return;
            }

            options.Saes = new List<Model.Process.ProcessBase>();
            DemoRepository demo = new DemoRepository();
            foreach (SaeMatcher m in matchers)
            {
                var sae = m.GetSae();
                if(sae != null)
                    options.Saes.Add(sae);
            }

            DoNextStep();
        }

        private void DoNextStep()
        {
            if (options.SplitType == SplittingType.SplitCombine)
                OnSwitchStep(new SplitCombineConfirm(options, this));
            else
                OnSwitchStep(new SplittingDemography(options, true));
        }

        public void DoFinish()
        {
        }
    }
}
