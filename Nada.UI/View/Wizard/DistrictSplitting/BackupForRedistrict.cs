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
using System.IO;
using Nada.Model.Demography;

namespace Nada.UI.View.Wizard
{
    public partial class BackupForRedistrict : BaseControl, IWizardStep
    {
        private DemoRepository repo = new DemoRepository();
        private SettingsRepository settings = new SettingsRepository();
        private RedistrictingOptions options = null;
        public Action OnFinish { get; set; }
        public Action<SavedReport> OnRunReport { get; set; }
        public Action<IWizardStep> OnSwitchStep { get; set; }
        public bool ShowNext { get { return true; } }
        public bool EnableNext { get { return true; } }
        public bool ShowPrev { get { return false; } }
        public bool EnablePrev { get { return false; } }
        public bool ShowFinish { get { return false; } }
        public bool EnableFinish { get { return false; } }
        public string StepTitle { get { return Translations.SplittingBackup; } }

        public BackupForRedistrict(RedistrictingOptions o)
            : base()
        {
            options = o;
            InitializeComponent();
        }
        
        public void DoPrev()
        {
        }
        public void DoNext()
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.Copy(DatabaseData.Instance.FilePath, saveFileDialog1.FileName, true);
                if (options.SplitType == SplittingType.SplitCombine)
                    OnSwitchStep(new MergingSources(options, Translations.SplitCombineSource));
                else if (options.SplitType == SplittingType.Split)
                    OnSwitchStep(new SplittingSource(options));
                else if (options.SplitType == SplittingType.Merge)
                    OnSwitchStep(new MergingSources(options, Translations.SplitMergeSource));
            }

        }
        public void DoFinish()
        {
        }

        private void BackupForRedistricting_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                lblMessage.SetMaxWidth(500);
                Localizer.TranslateControl(this);
                FileInfo fi = new FileInfo(DatabaseData.Instance.FilePath);
                saveFileDialog1.InitialDirectory = fi.Directory.FullName;
                saveFileDialog1.FileName = DatabaseData.Instance.FilePath.Replace(".accdb", "_Backup" + DateTime.Now.ToString("yyyyMMdd") + ".accdb");
                
            }
        }

    }
}
