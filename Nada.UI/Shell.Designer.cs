namespace Nada.UI
{
    partial class Shell
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Shell));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblDeveloperMode = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsLastUpdated = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.tsQuickLinks = new System.Windows.Forms.ToolStrip();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.kryptonManager1 = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.kryptonBorderEdge1 = new ComponentFactory.Krypton.Toolkit.KryptonBorderEdge();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.kryptonBorderEdge2 = new ComponentFactory.Krypton.Toolkit.KryptonBorderEdge();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNewAdminLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNewYearDemoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNewYearDistroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSplitDistrictToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMergeDistrictToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSplitCombineDistrictToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuImportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDemographyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDiseaseDistributionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuIntvToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSurveysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuViewHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuViewTutorialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCheckForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuAboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hrTop = new Nada.UI.Controls.HR();
            this.hr1 = new Nada.UI.Controls.HR();
            this.statusStrip1.SuspendLayout();
            this.tsQuickLinks.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblDeveloperMode,
            this.tsLastUpdated});
            this.statusStrip1.Location = new System.Drawing.Point(0, 830);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1248, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblDeveloperMode
            // 
            this.lblDeveloperMode.BackColor = System.Drawing.Color.Yellow;
            this.lblDeveloperMode.ForeColor = System.Drawing.Color.Red;
            this.lblDeveloperMode.Name = "lblDeveloperMode";
            this.lblDeveloperMode.Size = new System.Drawing.Size(106, 17);
            this.lblDeveloperMode.Text = "DEVELOPER MODE";
            this.lblDeveloperMode.Visible = false;
            // 
            // tsLastUpdated
            // 
            this.tsLastUpdated.Name = "tsLastUpdated";
            this.tsLastUpdated.Size = new System.Drawing.Size(0, 17);
            // 
            // pnlMain
            // 
            this.pnlMain.AutoScroll = true;
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 31);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1248, 799);
            this.pnlMain.TabIndex = 3;
            // 
            // tsQuickLinks
            // 
            this.tsQuickLinks.AutoSize = false;
            this.tsQuickLinks.Dock = System.Windows.Forms.DockStyle.Left;
            this.tsQuickLinks.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tsQuickLinks.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsQuickLinks.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripButton5,
            this.toolStripButton6,
            this.toolStripButton1,
            this.toolStripButton4});
            this.tsQuickLinks.Location = new System.Drawing.Point(0, 0);
            this.tsQuickLinks.Name = "tsQuickLinks";
            this.tsQuickLinks.Padding = new System.Windows.Forms.Padding(12, 12, 0, 0);
            this.tsQuickLinks.Size = new System.Drawing.Size(196, 797);
            this.tsQuickLinks.TabIndex = 0;
            this.tsQuickLinks.Text = "toolStrip1";
            this.tsQuickLinks.Visible = false;
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(183, 20);
            this.toolStripButton2.Text = "Demography";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(183, 20);
            this.toolStripButton3.Text = "Surveys";
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.Enabled = false;
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(183, 20);
            this.toolStripButton5.Text = "Disease Distribution";
            this.toolStripButton5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.Enabled = false;
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(183, 20);
            this.toolStripButton6.Text = "Interventions";
            this.toolStripButton6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Enabled = false;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(183, 20);
            this.toolStripButton1.Text = "Reports";
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Enabled = false;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(183, 20);
            this.toolStripButton4.Text = "Exports";
            this.toolStripButton4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // kryptonManager1
            // 
            this.kryptonManager1.GlobalPaletteMode = ComponentFactory.Krypton.Toolkit.PaletteModeManager.Office2007Blue;
            // 
            // kryptonBorderEdge1
            // 
            this.kryptonBorderEdge1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonBorderEdge1.Location = new System.Drawing.Point(0, 0);
            this.kryptonBorderEdge1.Name = "kryptonBorderEdge1";
            this.kryptonBorderEdge1.Size = new System.Drawing.Size(1248, 1);
            this.kryptonBorderEdge1.Text = "kryptonBorderEdge1";
            // 
            // pnlLeft
            // 
            this.pnlLeft.AutoSize = true;
            this.pnlLeft.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlLeft.Controls.Add(this.kryptonBorderEdge2);
            this.pnlLeft.Controls.Add(this.tsQuickLinks);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 1);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(0, 829);
            this.pnlLeft.TabIndex = 1;
            // 
            // kryptonBorderEdge2
            // 
            this.kryptonBorderEdge2.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonBorderEdge2.Location = new System.Drawing.Point(0, 0);
            this.kryptonBorderEdge2.Name = "kryptonBorderEdge2";
            this.kryptonBorderEdge2.Size = new System.Drawing.Size(0, 1);
            this.kryptonBorderEdge2.Text = "kryptonBorderEdge2";
            // 
            // mainMenu
            // 
            this.mainMenu.BackColor = System.Drawing.Color.Transparent;
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.menuSettingsToolStripMenuItem,
            this.menuImportToolStripMenuItem,
            this.menuReportsToolStripMenuItem,
            this.menuHelpToolStripMenuItem});
            this.mainMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.mainMenu.Location = new System.Drawing.Point(0, 7);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.mainMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mainMenu.Size = new System.Drawing.Size(1248, 24);
            this.mainMenu.TabIndex = 61;
            this.mainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator1,
            this.menuExitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(74, 20);
            this.fileToolStripMenuItem.Tag = "MenuFile2";
            this.fileToolStripMenuItem.Text = "MenuFile2";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.newToolStripMenuItem.Tag = "MenuNew";
            this.newToolStripMenuItem.Text = "MenuNew";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.openToolStripMenuItem.Tag = "MenuOpen";
            this.openToolStripMenuItem.Text = "MenuOpen";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(174, 6);
            // 
            // menuExitToolStripMenuItem
            // 
            this.menuExitToolStripMenuItem.Name = "menuExitToolStripMenuItem";
            this.menuExitToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.menuExitToolStripMenuItem.Tag = "MenuExit";
            this.menuExitToolStripMenuItem.Text = "MenuExit";
            this.menuExitToolStripMenuItem.Click += new System.EventHandler(this.menuExitToolStripMenuItem_Click);
            // 
            // menuSettingsToolStripMenuItem
            // 
            this.menuSettingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuEditSettingsToolStripMenuItem,
            this.menuNewAdminLevelToolStripMenuItem,
            this.menuNewYearDemoToolStripMenuItem,
            this.menuNewYearDistroToolStripMenuItem,
            this.menuSplitDistrictToolStripMenuItem,
            this.menuMergeDistrictToolStripMenuItem,
            this.menuSplitCombineDistrictToolStripMenuItem});
            this.menuSettingsToolStripMenuItem.Name = "menuSettingsToolStripMenuItem";
            this.menuSettingsToolStripMenuItem.Size = new System.Drawing.Size(98, 20);
            this.menuSettingsToolStripMenuItem.Tag = "MenuSettings2";
            this.menuSettingsToolStripMenuItem.Text = "MenuSettings2";
            // 
            // menuEditSettingsToolStripMenuItem
            // 
            this.menuEditSettingsToolStripMenuItem.Name = "menuEditSettingsToolStripMenuItem";
            this.menuEditSettingsToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.menuEditSettingsToolStripMenuItem.Tag = "MenuEditSettings";
            this.menuEditSettingsToolStripMenuItem.Text = "MenuEditSettings";
            this.menuEditSettingsToolStripMenuItem.Click += new System.EventHandler(this.menuEditSettingsToolStripMenuItem_Click);
            // 
            // menuNewAdminLevelToolStripMenuItem
            // 
            this.menuNewAdminLevelToolStripMenuItem.Name = "menuNewAdminLevelToolStripMenuItem";
            this.menuNewAdminLevelToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.menuNewAdminLevelToolStripMenuItem.Tag = "MenuNewAdminLevel";
            this.menuNewAdminLevelToolStripMenuItem.Text = "MenuNewAdminLevel";
            this.menuNewAdminLevelToolStripMenuItem.Click += new System.EventHandler(this.menuNewAdminLevelToolStripMenuItem_Click);
            // 
            // menuNewYearDemoToolStripMenuItem
            // 
            this.menuNewYearDemoToolStripMenuItem.Name = "menuNewYearDemoToolStripMenuItem";
            this.menuNewYearDemoToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.menuNewYearDemoToolStripMenuItem.Tag = "MenuNewYearDemo";
            this.menuNewYearDemoToolStripMenuItem.Text = "MenuNewYearDemo";
            this.menuNewYearDemoToolStripMenuItem.Click += new System.EventHandler(this.menuNewYearDemoToolStripMenuItem_Click);
            // 
            // menuNewYearDistroToolStripMenuItem
            // 
            this.menuNewYearDistroToolStripMenuItem.Name = "menuNewYearDistroToolStripMenuItem";
            this.menuNewYearDistroToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.menuNewYearDistroToolStripMenuItem.Tag = "MenuNewYearDistro";
            this.menuNewYearDistroToolStripMenuItem.Text = "MenuNewYearDistro";
            this.menuNewYearDistroToolStripMenuItem.Visible = false;
            this.menuNewYearDistroToolStripMenuItem.Click += new System.EventHandler(this.menuNewYearDistroToolStripMenuItem_Click);
            // 
            // menuSplitDistrictToolStripMenuItem
            // 
            this.menuSplitDistrictToolStripMenuItem.Name = "menuSplitDistrictToolStripMenuItem";
            this.menuSplitDistrictToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.menuSplitDistrictToolStripMenuItem.Tag = "MenuSplitDistrict";
            this.menuSplitDistrictToolStripMenuItem.Text = "MenuSplitDistrict";
            this.menuSplitDistrictToolStripMenuItem.Click += new System.EventHandler(this.menuSplitDistrictToolStripMenuItem_Click);
            // 
            // menuMergeDistrictToolStripMenuItem
            // 
            this.menuMergeDistrictToolStripMenuItem.Name = "menuMergeDistrictToolStripMenuItem";
            this.menuMergeDistrictToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.menuMergeDistrictToolStripMenuItem.Tag = "MenuMergeDistrict";
            this.menuMergeDistrictToolStripMenuItem.Text = "MenuMergeDistrict";
            this.menuMergeDistrictToolStripMenuItem.Click += new System.EventHandler(this.menuMergeDistrictToolStripMenuItem_Click);
            // 
            // menuSplitCombineDistrictToolStripMenuItem
            // 
            this.menuSplitCombineDistrictToolStripMenuItem.Name = "menuSplitCombineDistrictToolStripMenuItem";
            this.menuSplitCombineDistrictToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.menuSplitCombineDistrictToolStripMenuItem.Tag = "MenuSplitCombineDistrict";
            this.menuSplitCombineDistrictToolStripMenuItem.Text = "MenuSplitCombineDistrict";
            this.menuSplitCombineDistrictToolStripMenuItem.Click += new System.EventHandler(this.menuSplitCombineDistrictToolStripMenuItem_Click);
            // 
            // menuImportToolStripMenuItem
            // 
            this.menuImportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuDemographyToolStripMenuItem,
            this.menuDiseaseDistributionsToolStripMenuItem,
            this.menuIntvToolStripMenuItem,
            this.menuProcessToolStripMenuItem,
            this.menuSurveysToolStripMenuItem});
            this.menuImportToolStripMenuItem.Name = "menuImportToolStripMenuItem";
            this.menuImportToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.menuImportToolStripMenuItem.Tag = "MenuImport";
            this.menuImportToolStripMenuItem.Text = "MenuImport";
            // 
            // menuDemographyToolStripMenuItem
            // 
            this.menuDemographyToolStripMenuItem.Enabled = false;
            this.menuDemographyToolStripMenuItem.Name = "menuDemographyToolStripMenuItem";
            this.menuDemographyToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.menuDemographyToolStripMenuItem.Tag = "MenuDemography";
            this.menuDemographyToolStripMenuItem.Text = "MenuDemography";
            this.menuDemographyToolStripMenuItem.Click += new System.EventHandler(this.menuDemographyToolStripMenuItem_Click);
            // 
            // menuDiseaseDistributionsToolStripMenuItem
            // 
            this.menuDiseaseDistributionsToolStripMenuItem.Name = "menuDiseaseDistributionsToolStripMenuItem";
            this.menuDiseaseDistributionsToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.menuDiseaseDistributionsToolStripMenuItem.Tag = "MenuDiseaseDistributions";
            this.menuDiseaseDistributionsToolStripMenuItem.Text = "MenuDiseaseDistributions";
            this.menuDiseaseDistributionsToolStripMenuItem.Click += new System.EventHandler(this.menuDiseaseDistributionsToolStripMenuItem_Click);
            // 
            // menuIntvToolStripMenuItem
            // 
            this.menuIntvToolStripMenuItem.Name = "menuIntvToolStripMenuItem";
            this.menuIntvToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.menuIntvToolStripMenuItem.Tag = "MenuIntv";
            this.menuIntvToolStripMenuItem.Text = "MenuIntv";
            this.menuIntvToolStripMenuItem.Click += new System.EventHandler(this.menuIntvToolStripMenuItem_Click);
            // 
            // menuProcessToolStripMenuItem
            // 
            this.menuProcessToolStripMenuItem.Name = "menuProcessToolStripMenuItem";
            this.menuProcessToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.menuProcessToolStripMenuItem.Tag = "MenuProcess";
            this.menuProcessToolStripMenuItem.Text = "MenuProcess";
            this.menuProcessToolStripMenuItem.Click += new System.EventHandler(this.menuProcessToolStripMenuItem_Click);
            // 
            // menuSurveysToolStripMenuItem
            // 
            this.menuSurveysToolStripMenuItem.Name = "menuSurveysToolStripMenuItem";
            this.menuSurveysToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.menuSurveysToolStripMenuItem.Tag = "MenuSurveys";
            this.menuSurveysToolStripMenuItem.Text = "MenuSurveys";
            this.menuSurveysToolStripMenuItem.Click += new System.EventHandler(this.menuSurveysToolStripMenuItem_Click);
            // 
            // menuReportsToolStripMenuItem
            // 
            this.menuReportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNewToolStripMenuItem});
            this.menuReportsToolStripMenuItem.Name = "menuReportsToolStripMenuItem";
            this.menuReportsToolStripMenuItem.Size = new System.Drawing.Size(90, 20);
            this.menuReportsToolStripMenuItem.Tag = "MenuReports";
            this.menuReportsToolStripMenuItem.Text = "MenuReports";
            // 
            // menuNewToolStripMenuItem
            // 
            this.menuNewToolStripMenuItem.Name = "menuNewToolStripMenuItem";
            this.menuNewToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.menuNewToolStripMenuItem.Tag = "MenuRunReports";
            this.menuNewToolStripMenuItem.Text = "MenuRunReports";
            this.menuNewToolStripMenuItem.Click += new System.EventHandler(this.menuNewReportToolStripMenuItem_Click);
            // 
            // menuHelpToolStripMenuItem
            // 
            this.menuHelpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuViewHelpToolStripMenuItem,
            this.menuViewTutorialToolStripMenuItem,
            this.menuCheckForUpdatesToolStripMenuItem,
            this.toolStripSeparator2,
            this.menuAboutToolStripMenuItem});
            this.menuHelpToolStripMenuItem.Name = "menuHelpToolStripMenuItem";
            this.menuHelpToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.menuHelpToolStripMenuItem.Tag = "MenuHelp";
            this.menuHelpToolStripMenuItem.Text = "MenuHelp";
            // 
            // menuViewHelpToolStripMenuItem
            // 
            this.menuViewHelpToolStripMenuItem.Name = "menuViewHelpToolStripMenuItem";
            this.menuViewHelpToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.menuViewHelpToolStripMenuItem.Tag = "MenuViewHelp";
            this.menuViewHelpToolStripMenuItem.Text = "MenuViewHelp";
            this.menuViewHelpToolStripMenuItem.Click += new System.EventHandler(this.menuViewHelpToolStripMenuItem_Click);
            // 
            // menuViewTutorialToolStripMenuItem
            // 
            this.menuViewTutorialToolStripMenuItem.Name = "menuViewTutorialToolStripMenuItem";
            this.menuViewTutorialToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.menuViewTutorialToolStripMenuItem.Tag = "MenuViewTutorial";
            this.menuViewTutorialToolStripMenuItem.Text = "MenuViewTutorial";
            this.menuViewTutorialToolStripMenuItem.Click += new System.EventHandler(this.menuViewTutorialToolStripMenuItem_Click);
            // 
            // menuCheckForUpdatesToolStripMenuItem
            // 
            this.menuCheckForUpdatesToolStripMenuItem.Name = "menuCheckForUpdatesToolStripMenuItem";
            this.menuCheckForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.menuCheckForUpdatesToolStripMenuItem.Tag = "MenuCheckForUpdates";
            this.menuCheckForUpdatesToolStripMenuItem.Text = "MenuCheckForUpdates";
            this.menuCheckForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.menuCheckForUpdatesToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(195, 6);
            // 
            // menuAboutToolStripMenuItem
            // 
            this.menuAboutToolStripMenuItem.Name = "menuAboutToolStripMenuItem";
            this.menuAboutToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.menuAboutToolStripMenuItem.Tag = "MenuAbout";
            this.menuAboutToolStripMenuItem.Text = "MenuAbout";
            this.menuAboutToolStripMenuItem.Click += new System.EventHandler(this.menuAboutToolStripMenuItem_Click);
            // 
            // hrTop
            // 
            this.hrTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
            this.hrTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.hrTop.ForeColor = System.Drawing.Color.Gray;
            this.hrTop.Location = new System.Drawing.Point(0, 1);
            this.hrTop.Margin = new System.Windows.Forms.Padding(0);
            this.hrTop.Name = "hrTop";
            this.hrTop.RuleColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
            this.hrTop.Size = new System.Drawing.Size(1248, 6);
            this.hrTop.TabIndex = 60;
            // 
            // hr1
            // 
            this.hr1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
            this.hr1.Dock = System.Windows.Forms.DockStyle.Top;
            this.hr1.ForeColor = System.Drawing.Color.Gray;
            this.hr1.Location = new System.Drawing.Point(0, 31);
            this.hr1.Margin = new System.Windows.Forms.Padding(0);
            this.hr1.Name = "hr1";
            this.hr1.RuleColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(55)))), ((int)(((byte)(93)))));
            this.hr1.Size = new System.Drawing.Size(1248, 1);
            this.hr1.TabIndex = 63;
            // 
            // Shell
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1248, 852);
            this.Controls.Add(this.hr1);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.mainMenu);
            this.Controls.Add(this.hrTop);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.kryptonBorderEdge1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Shell";
            this.Tag = "ApplicationTitle";
            this.Text = "ApplicationTitle";
            this.Load += new System.EventHandler(this.Shell_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tsQuickLinks.ResumeLayout(false);
            this.tsQuickLinks.PerformLayout();
            this.pnlLeft.ResumeLayout(false);
            this.pnlLeft.PerformLayout();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.ToolStrip tsQuickLinks;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripStatusLabel lblDeveloperMode;
        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager1;
        private ComponentFactory.Krypton.Toolkit.KryptonBorderEdge kryptonBorderEdge1;
        private System.Windows.Forms.Panel pnlLeft;
        private ComponentFactory.Krypton.Toolkit.KryptonBorderEdge kryptonBorderEdge2;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripStatusLabel tsLastUpdated;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuImportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuDemographyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuDiseaseDistributionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuIntvToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuProcessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuSurveysToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuReportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuNewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuViewHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuCheckForUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem menuAboutToolStripMenuItem;
        private Controls.HR hrTop;
        private Controls.HR hr1;
        private System.Windows.Forms.ToolStripMenuItem menuSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuNewAdminLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuEditSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuNewYearDemoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuNewYearDistroToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuSplitCombineDistrictToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuSplitDistrictToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuMergeDistrictToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuViewTutorialToolStripMenuItem;
    }
}

