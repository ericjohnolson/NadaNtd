using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model;

namespace Nada.UI.View
{
    public partial class AdminLevelPickerControl : UserControl
    {
        public event Action<AdminLevel> OnSelect = (e) => { };

        public AdminLevelPickerControl()
        {
            InitializeComponent();
        }
        
        public void Select(AdminLevel obj)
        {
            lblAdminLevel.Text = obj.Name;
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color TextColor
        {
            get { return lblLocation.ForeColor; }
            set { lblLocation.ForeColor = value; }
        }

        private void fieldLink1_OnClick()
        {
            AdminLevelPicker picker = new AdminLevelPicker();
            picker.OnSelect += picker_OnSelect;
            picker.ShowDialog();
        }

        void picker_OnSelect(Model.AdminLevel obj)
        {
            lblAdminLevel.Text = obj.Name;
            OnSelect(obj);
        }
    }
}
