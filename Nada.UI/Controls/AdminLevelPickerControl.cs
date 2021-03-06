﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nada.Model;
using Nada.Model.Repositories;
using Nada.UI.Base;

namespace Nada.UI.View
{
    public partial class AdminLevelPickerControl : BaseControl
    {
        public event Action<AdminLevel> OnSelect = (e) => { };

        public AdminLevelPickerControl()
            : base()
        {
            InitializeComponent();
        }

        public void Select(AdminLevel obj)
        {
            lblAdminLevel.Text = obj.Name;
        }

        public void Select(int id)
        {
            DemoRepository repo = new DemoRepository();
            AdminLevel level = repo.GetAdminLevelById(id);
            Select(level);
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
            MessageBox.Show("Disabled");
            //AdminLevelPicker picker = new AdminLevelPicker();
            //picker.OnSelect += picker_OnSelect;
            //picker.ShowDialog();
        }

        void picker_OnSelect(Model.AdminLevel obj)
        {
            lblAdminLevel.Text = obj.Name;
            OnSelect(obj);
        }
    }
}
