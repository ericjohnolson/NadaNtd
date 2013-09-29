using System;
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
using Nada.UI.View.Help;
using Nada.Model.Diseases;

namespace Nada.UI.View.Intervention
{
    public partial class PcMdaView : UserControl, IView
    {
        private AdminLevel adminLevel = null;
        private PcMda model = null;
        private IntvRepository r = null;
        private DemoRepository demo = null;
        public Action OnClose { get; set; }
        public Action<string> StatusChanged { get; set; }

        public PcMdaView()
        {
            InitializeComponent();
        }

        public PcMdaView(AdminLevel a)
        {
            adminLevel = a;
            InitializeComponent();
        }

        public PcMdaView(PcMda s)
        {
            this.model = s;
            InitializeComponent();
        }

        private void PcMda_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Localizer.TranslateControl(this);
                adminLevelPickerControl1.OnSelect += adminLevelPickerControl1_OnSelect;
                r = new IntvRepository();
                demo = new DemoRepository();
                if (model == null) 
                {
                    model = r.CreateIntv<PcMda>(StaticIntvType.IvmAlbMda);
                    adminLevelPickerControl1.Select(adminLevel);
                    model.AdminLevelId = adminLevel.Id;
                }
                else
                    adminLevelPickerControl1.Select(model.AdminLevelId.Value);

                bsIntv.DataSource = model;

                if (model.IntvType.Indicators != null && model.IntvType.Indicators.Count() > 0)
                    customIndicatorControl1.LoadIndicators(model.IntvType.Indicators, model.IndicatorValues);

                customIndicatorControl1.OnAddRemove += customIndicatorControl1_OnAddRemove;
                fundersControl1.LoadItems(model.Partners);
                StatusChanged(Translations.LastUpdated + model.UpdatedBy);
            }
        }

        void adminLevelPickerControl1_OnSelect(Model.AdminLevel obj)
        {
            model.AdminLevelId = obj.Id;
        }

        /// <summary>
        /// SAVE Method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void save_Click(object sender, EventArgs e)
        {
            if (!model.IsValid())
            {
                MessageBox.Show(Translations.ValidationError);
                return;
            }
            if (!model.AdminLevelId.HasValue || model.AdminLevelId.Value < 1)
            {
                MessageBox.Show(Translations.LocationRequired);
                return;
            }

            model.Partners = fundersControl1.GetSelected();
            model.DiseasesTargeted = diseasesControl1.GetSelected();
            if (model.DiseasesTargeted == null || model.DiseasesTargeted.Count == 0)
            {
                MessageBox.Show(Translations.DiseasesRequired);
                return;
            }
            

            bsIntv.EndEdit();
            
            model.IndicatorValues = customIndicatorControl1.GetValues();
            model.MapPropertiesToIndicators();
            int userId = ApplicationData.Instance.GetUserId();
                r.Save(model, userId);
            OnClose();
        }

        void customIndicatorControl1_OnAddRemove()
        {
            IntvTypeEdit editor = new IntvTypeEdit(model.IntvType);
            editor.OnSave += editType_OnSave;
            editor.ShowDialog();
        }

        void editType_OnSave()
        {
            customIndicatorControl1.LoadIndicators(model.IntvType.Indicators);
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
            HelpView help = new HelpView();
            help.Show();
        }
    }
}
