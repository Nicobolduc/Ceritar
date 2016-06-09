namespace Ceritar.Logirack_CVS
{
    partial class frmVersion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVersion));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtCreatedBy = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chkDemoVersion = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpCreation = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.cboTemplates = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtVersionNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCompiledBy = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboApplications = new System.Windows.Forms.ComboBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnGrdRevDel = new System.Windows.Forms.Button();
            this.btnGrdRevAdd = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnExportInstallationKit = new System.Windows.Forms.Button();
            this.btnGrdRevUpdate = new System.Windows.Forms.Button();
            this.chkIncludeScripts = new System.Windows.Forms.CheckBox();
            this.btnReplaceExecutable = new System.Windows.Forms.Button();
            this.btnReplaceTTApp = new System.Windows.Forms.Button();
            this.btnReplaceAppChangeDOC = new System.Windows.Forms.Button();
            this.btnReplaceAppChangeXLS = new System.Windows.Forms.Button();
            this.btnShowExecutable = new System.Windows.Forms.Button();
            this.btnShowAccess = new System.Windows.Forms.Button();
            this.btnShowWord = new System.Windows.Forms.Button();
            this.btnShowExcel = new System.Windows.Forms.Button();
            this.tabRevision = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnRefresh = new Ceritar.TT3LightDLL.Controls.ctlRefresh();
            this.grdRevisions = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.tabVersion = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.txtReleasePath = new System.Windows.Forms.TextBox();
            this.txtTTAppPath = new System.Windows.Forms.TextBox();
            this.txtWordAppChangePath = new System.Windows.Forms.TextBox();
            this.txtExcelAppChangePath = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnSelectVariousFilePath = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnSelectVariousFolderPath = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.grdSatellite = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cboClients = new System.Windows.Forms.ComboBox();
            this.btnGrdClientsDel = new System.Windows.Forms.Button();
            this.btnGrdClientsAdd = new System.Windows.Forms.Button();
            this.grdClients = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.tab = new System.Windows.Forms.TabControl();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.formController = new Ceritar.TT3LightDLL.Controls.ctlFormController();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.tabRevision.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdRevisions)).BeginInit();
            this.tabVersion.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSatellite)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdClients)).BeginInit();
            this.tab.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCreatedBy);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.chkDemoVersion);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpCreation);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cboTemplates);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtVersionNo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtCompiledBy);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cboApplications);
            this.groupBox1.Location = new System.Drawing.Point(5, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(804, 73);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Informations";
            // 
            // txtCreatedBy
            // 
            this.txtCreatedBy.Location = new System.Drawing.Point(666, 45);
            this.txtCreatedBy.Name = "txtCreatedBy";
            this.txtCreatedBy.ReadOnly = true;
            this.txtCreatedBy.Size = new System.Drawing.Size(132, 20);
            this.txtCreatedBy.TabIndex = 74;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(574, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 73;
            this.label6.Text = "Usager:";
            // 
            // chkDemoVersion
            // 
            this.chkDemoVersion.AutoSize = true;
            this.chkDemoVersion.Location = new System.Drawing.Point(446, 48);
            this.chkDemoVersion.Name = "chkDemoVersion";
            this.chkDemoVersion.Size = new System.Drawing.Size(54, 17);
            this.chkDemoVersion.TabIndex = 4;
            this.chkDemoVersion.Text = "Démo";
            this.chkDemoVersion.UseVisualStyleBackColor = true;
            this.chkDemoVersion.CheckedChanged += new System.EventHandler(this.chkDemoVersion_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(574, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 69;
            this.label2.Text = "Date de création:";
            // 
            // dtpCreation
            // 
            this.dtpCreation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpCreation.CustomFormat = "MM-dd-yyyy hh:mm";
            this.dtpCreation.Enabled = false;
            this.dtpCreation.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCreation.Location = new System.Drawing.Point(666, 19);
            this.dtpCreation.Name = "dtpCreation";
            this.dtpCreation.Size = new System.Drawing.Size(132, 20);
            this.dtpCreation.TabIndex = 53;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(6, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 17);
            this.label5.TabIndex = 49;
            this.label5.Text = "Gabarit:";
            // 
            // cboTemplates
            // 
            this.cboTemplates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTemplates.FormattingEnabled = true;
            this.cboTemplates.Location = new System.Drawing.Point(74, 46);
            this.cboTemplates.Name = "cboTemplates";
            this.cboTemplates.Size = new System.Drawing.Size(214, 21);
            this.cboTemplates.TabIndex = 1;
            this.cboTemplates.SelectedIndexChanged += new System.EventHandler(this.cboGabarits_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(294, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 17);
            this.label4.TabIndex = 44;
            this.label4.Text = "Numéro:";
            // 
            // txtVersionNo
            // 
            this.txtVersionNo.Location = new System.Drawing.Point(374, 46);
            this.txtVersionNo.MaxLength = 10;
            this.txtVersionNo.Name = "txtVersionNo";
            this.txtVersionNo.Size = new System.Drawing.Size(66, 20);
            this.txtVersionNo.TabIndex = 3;
            this.txtVersionNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtVersionNo.TextChanged += new System.EventHandler(this.txtVersionNo_TextChanged);
            this.txtVersionNo.Validating += new System.ComponentModel.CancelEventHandler(this.txtVersionNo_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(294, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 17);
            this.label1.TabIndex = 42;
            this.label1.Text = "Compilé par:";
            // 
            // txtCompiledBy
            // 
            this.txtCompiledBy.Location = new System.Drawing.Point(374, 19);
            this.txtCompiledBy.Name = "txtCompiledBy";
            this.txtCompiledBy.Size = new System.Drawing.Size(198, 20);
            this.txtCompiledBy.TabIndex = 2;
            this.txtCompiledBy.TextChanged += new System.EventHandler(this.txtCompiledBy_TextChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 17);
            this.label3.TabIndex = 40;
            this.label3.Text = "Application:";
            // 
            // cboApplications
            // 
            this.cboApplications.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboApplications.FormattingEnabled = true;
            this.cboApplications.Location = new System.Drawing.Point(74, 19);
            this.cboApplications.Name = "cboApplications";
            this.cboApplications.Size = new System.Drawing.Size(214, 21);
            this.cboApplications.TabIndex = 0;
            this.cboApplications.SelectedIndexChanged += new System.EventHandler(this.cboApplications_SelectedIndexChanged);
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 10000;
            this.toolTip.InitialDelay = 50;
            this.toolTip.ReshowDelay = 100;
            // 
            // btnGrdRevDel
            // 
            this.btnGrdRevDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGrdRevDel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGrdRevDel.Image = ((System.Drawing.Image)(resources.GetObject("btnGrdRevDel.Image")));
            this.btnGrdRevDel.Location = new System.Drawing.Point(752, 103);
            this.btnGrdRevDel.Name = "btnGrdRevDel";
            this.btnGrdRevDel.Size = new System.Drawing.Size(35, 35);
            this.btnGrdRevDel.TabIndex = 17;
            this.toolTip.SetToolTip(this.btnGrdRevDel, "Supprimer une révision");
            this.btnGrdRevDel.UseVisualStyleBackColor = true;
            this.btnGrdRevDel.Click += new System.EventHandler(this.btnGrdRevDel_Click);
            // 
            // btnGrdRevAdd
            // 
            this.btnGrdRevAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGrdRevAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGrdRevAdd.BackgroundImage")));
            this.btnGrdRevAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGrdRevAdd.Location = new System.Drawing.Point(752, 21);
            this.btnGrdRevAdd.Name = "btnGrdRevAdd";
            this.btnGrdRevAdd.Size = new System.Drawing.Size(35, 35);
            this.btnGrdRevAdd.TabIndex = 15;
            this.toolTip.SetToolTip(this.btnGrdRevAdd, "Créer une révision");
            this.btnGrdRevAdd.UseVisualStyleBackColor = true;
            this.btnGrdRevAdd.Click += new System.EventHandler(this.btnGrdRevAdd_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnGenerate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGenerate.BackgroundImage")));
            this.btnGenerate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGenerate.Location = new System.Drawing.Point(28, 16);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(40, 40);
            this.btnGenerate.TabIndex = 0;
            this.toolTip.SetToolTip(this.btnGenerate, "Mettre à jour les installations actives sans resauvegarder tout l\'écran");
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnExportInstallationKit
            // 
            this.btnExportInstallationKit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnExportInstallationKit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnExportInstallationKit.Image = ((System.Drawing.Image)(resources.GetObject("btnExportInstallationKit.Image")));
            this.btnExportInstallationKit.Location = new System.Drawing.Point(395, 126);
            this.btnExportInstallationKit.Name = "btnExportInstallationKit";
            this.btnExportInstallationKit.Size = new System.Drawing.Size(35, 35);
            this.btnExportInstallationKit.TabIndex = 3;
            this.toolTip.SetToolTip(this.btnExportInstallationKit, "Exporter un kit d\'installation");
            this.btnExportInstallationKit.UseVisualStyleBackColor = true;
            this.btnExportInstallationKit.Click += new System.EventHandler(this.btnExportInstallationKit_Click);
            // 
            // btnGrdRevUpdate
            // 
            this.btnGrdRevUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGrdRevUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGrdRevUpdate.Image = ((System.Drawing.Image)(resources.GetObject("btnGrdRevUpdate.Image")));
            this.btnGrdRevUpdate.Location = new System.Drawing.Point(752, 62);
            this.btnGrdRevUpdate.Name = "btnGrdRevUpdate";
            this.btnGrdRevUpdate.Size = new System.Drawing.Size(35, 35);
            this.btnGrdRevUpdate.TabIndex = 16;
            this.toolTip.SetToolTip(this.btnGrdRevUpdate, "Modifier une révision");
            this.btnGrdRevUpdate.UseVisualStyleBackColor = true;
            this.btnGrdRevUpdate.Click += new System.EventHandler(this.btnGrdRevUpdate_Click);
            // 
            // chkIncludeScripts
            // 
            this.chkIncludeScripts.AutoSize = true;
            this.chkIncludeScripts.Location = new System.Drawing.Point(4, 59);
            this.chkIncludeScripts.Name = "chkIncludeScripts";
            this.chkIncludeScripts.Size = new System.Drawing.Size(91, 17);
            this.chkIncludeScripts.TabIndex = 1;
            this.chkIncludeScripts.Text = "Inclure scripts";
            this.toolTip.SetToolTip(this.chkIncludeScripts, "Reconstruit la structures des scripts à partir de DB_UPGRADE_SCRIPTS");
            this.chkIncludeScripts.UseVisualStyleBackColor = true;
            // 
            // btnReplaceExecutable
            // 
            this.btnReplaceExecutable.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnReplaceExecutable.Image = ((System.Drawing.Image)(resources.GetObject("btnReplaceExecutable.Image")));
            this.btnReplaceExecutable.Location = new System.Drawing.Point(6, 156);
            this.btnReplaceExecutable.Name = "btnReplaceExecutable";
            this.btnReplaceExecutable.Size = new System.Drawing.Size(30, 30);
            this.btnReplaceExecutable.TabIndex = 64;
            this.toolTip.SetToolTip(this.btnReplaceExecutable, "Sélectionner l\'exécutable");
            this.btnReplaceExecutable.UseVisualStyleBackColor = true;
            this.btnReplaceExecutable.Click += new System.EventHandler(this.btnReplaceExecutable_Click);
            // 
            // btnReplaceTTApp
            // 
            this.btnReplaceTTApp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnReplaceTTApp.Image = ((System.Drawing.Image)(resources.GetObject("btnReplaceTTApp.Image")));
            this.btnReplaceTTApp.Location = new System.Drawing.Point(6, 111);
            this.btnReplaceTTApp.Name = "btnReplaceTTApp";
            this.btnReplaceTTApp.Size = new System.Drawing.Size(30, 30);
            this.btnReplaceTTApp.TabIndex = 63;
            this.toolTip.SetToolTip(this.btnReplaceTTApp, "Sélectionner le TTApp");
            this.btnReplaceTTApp.UseVisualStyleBackColor = true;
            this.btnReplaceTTApp.Click += new System.EventHandler(this.btnReplaceTTApp_Click);
            // 
            // btnReplaceAppChangeDOC
            // 
            this.btnReplaceAppChangeDOC.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnReplaceAppChangeDOC.Image = ((System.Drawing.Image)(resources.GetObject("btnReplaceAppChangeDOC.Image")));
            this.btnReplaceAppChangeDOC.Location = new System.Drawing.Point(6, 65);
            this.btnReplaceAppChangeDOC.Name = "btnReplaceAppChangeDOC";
            this.btnReplaceAppChangeDOC.Size = new System.Drawing.Size(30, 30);
            this.btnReplaceAppChangeDOC.TabIndex = 62;
            this.toolTip.SetToolTip(this.btnReplaceAppChangeDOC, "Sélectionner le App_Changements");
            this.btnReplaceAppChangeDOC.UseVisualStyleBackColor = true;
            this.btnReplaceAppChangeDOC.Click += new System.EventHandler(this.btnReplaceAppChangeDOC_Click);
            // 
            // btnReplaceAppChangeXLS
            // 
            this.btnReplaceAppChangeXLS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnReplaceAppChangeXLS.Image = ((System.Drawing.Image)(resources.GetObject("btnReplaceAppChangeXLS.Image")));
            this.btnReplaceAppChangeXLS.Location = new System.Drawing.Point(6, 18);
            this.btnReplaceAppChangeXLS.Name = "btnReplaceAppChangeXLS";
            this.btnReplaceAppChangeXLS.Size = new System.Drawing.Size(30, 30);
            this.btnReplaceAppChangeXLS.TabIndex = 61;
            this.toolTip.SetToolTip(this.btnReplaceAppChangeXLS, "Sélectionner le App_Changements");
            this.btnReplaceAppChangeXLS.UseVisualStyleBackColor = true;
            this.btnReplaceAppChangeXLS.Click += new System.EventHandler(this.btnReplaceAppChangeXLS_Click);
            // 
            // btnShowExecutable
            // 
            this.btnShowExecutable.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnShowExecutable.BackgroundImage")));
            this.btnShowExecutable.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnShowExecutable.Location = new System.Drawing.Point(747, 150);
            this.btnShowExecutable.Name = "btnShowExecutable";
            this.btnShowExecutable.Size = new System.Drawing.Size(40, 40);
            this.btnShowExecutable.TabIndex = 68;
            this.toolTip.SetToolTip(this.btnShowExecutable, "Consulter l\'exécutable");
            this.btnShowExecutable.UseVisualStyleBackColor = true;
            this.btnShowExecutable.Click += new System.EventHandler(this.btnShowExecutable_Click);
            // 
            // btnShowAccess
            // 
            this.btnShowAccess.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnShowAccess.BackgroundImage")));
            this.btnShowAccess.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnShowAccess.Location = new System.Drawing.Point(747, 105);
            this.btnShowAccess.Name = "btnShowAccess";
            this.btnShowAccess.Size = new System.Drawing.Size(40, 40);
            this.btnShowAccess.TabIndex = 67;
            this.toolTip.SetToolTip(this.btnShowAccess, "Consulter TTApp");
            this.btnShowAccess.UseVisualStyleBackColor = true;
            this.btnShowAccess.Click += new System.EventHandler(this.btnShowAccess_Click);
            // 
            // btnShowWord
            // 
            this.btnShowWord.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnShowWord.BackgroundImage")));
            this.btnShowWord.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnShowWord.Location = new System.Drawing.Point(747, 59);
            this.btnShowWord.Name = "btnShowWord";
            this.btnShowWord.Size = new System.Drawing.Size(40, 40);
            this.btnShowWord.TabIndex = 66;
            this.toolTip.SetToolTip(this.btnShowWord, "Consulter App_Changements.docx");
            this.btnShowWord.UseVisualStyleBackColor = true;
            this.btnShowWord.Click += new System.EventHandler(this.btnShowWord_Click);
            // 
            // btnShowExcel
            // 
            this.btnShowExcel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnShowExcel.BackgroundImage")));
            this.btnShowExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnShowExcel.Location = new System.Drawing.Point(747, 13);
            this.btnShowExcel.Name = "btnShowExcel";
            this.btnShowExcel.Size = new System.Drawing.Size(40, 40);
            this.btnShowExcel.TabIndex = 65;
            this.toolTip.SetToolTip(this.btnShowExcel, "Consulter App_Changements.xls");
            this.btnShowExcel.UseVisualStyleBackColor = true;
            this.btnShowExcel.Click += new System.EventHandler(this.btnShowExcel_Click);
            // 
            // tabRevision
            // 
            this.tabRevision.Controls.Add(this.groupBox3);
            this.tabRevision.Location = new System.Drawing.Point(4, 22);
            this.tabRevision.Name = "tabRevision";
            this.tabRevision.Padding = new System.Windows.Forms.Padding(3);
            this.tabRevision.Size = new System.Drawing.Size(806, 462);
            this.tabRevision.TabIndex = 1;
            this.tabRevision.Text = "Révisions";
            this.tabRevision.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnRefresh);
            this.groupBox3.Controls.Add(this.btnGrdRevUpdate);
            this.groupBox3.Controls.Add(this.btnGrdRevDel);
            this.groupBox3.Controls.Add(this.btnGrdRevAdd);
            this.groupBox3.Controls.Add(this.grdRevisions);
            this.groupBox3.Location = new System.Drawing.Point(6, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(794, 351);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Liste des révisions";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(752, 310);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(35, 35);
            this.btnRefresh.TabIndex = 19;
            this.btnRefresh.Click += new Ceritar.TT3LightDLL.Controls.ctlRefresh.ClickEventHandler(this.btnRefresh_Click);
            // 
            // grdRevisions
            // 
            this.grdRevisions.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.grdRevisions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdRevisions.AutoSearch = C1.Win.C1FlexGrid.AutoSearchEnum.FromTop;
            this.grdRevisions.ColumnInfo = resources.GetString("grdRevisions.ColumnInfo");
            this.grdRevisions.ExtendLastCol = true;
            this.grdRevisions.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdRevisions.Location = new System.Drawing.Point(6, 21);
            this.grdRevisions.Name = "grdRevisions";
            this.grdRevisions.Rows.Count = 6;
            this.grdRevisions.Rows.DefaultSize = 18;
            this.grdRevisions.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
            this.grdRevisions.ShowSort = false;
            this.grdRevisions.Size = new System.Drawing.Size(740, 324);
            this.grdRevisions.StyleInfo = resources.GetString("grdRevisions.StyleInfo");
            this.grdRevisions.TabIndex = 14;
            this.grdRevisions.Tag = "20";
            this.grdRevisions.DoubleClick += new System.EventHandler(this.grdRevisions_DoubleClick);
            // 
            // tabVersion
            // 
            this.tabVersion.Controls.Add(this.groupBox7);
            this.tabVersion.Controls.Add(this.groupBox6);
            this.tabVersion.Controls.Add(this.groupBox5);
            this.tabVersion.Controls.Add(this.groupBox4);
            this.tabVersion.Controls.Add(this.groupBox2);
            this.tabVersion.Location = new System.Drawing.Point(4, 22);
            this.tabVersion.Name = "tabVersion";
            this.tabVersion.Padding = new System.Windows.Forms.Padding(3);
            this.tabVersion.Size = new System.Drawing.Size(806, 462);
            this.tabVersion.TabIndex = 0;
            this.tabVersion.Text = "Version";
            this.tabVersion.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btnReplaceExecutable);
            this.groupBox7.Controls.Add(this.btnReplaceTTApp);
            this.groupBox7.Controls.Add(this.btnReplaceAppChangeDOC);
            this.groupBox7.Controls.Add(this.btnReplaceAppChangeXLS);
            this.groupBox7.Controls.Add(this.txtReleasePath);
            this.groupBox7.Controls.Add(this.btnShowExecutable);
            this.groupBox7.Controls.Add(this.txtTTAppPath);
            this.groupBox7.Controls.Add(this.txtWordAppChangePath);
            this.groupBox7.Controls.Add(this.txtExcelAppChangePath);
            this.groupBox7.Controls.Add(this.btnShowAccess);
            this.groupBox7.Controls.Add(this.btnShowWord);
            this.groupBox7.Controls.Add(this.btnShowExcel);
            this.groupBox7.Location = new System.Drawing.Point(6, 179);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(794, 195);
            this.groupBox7.TabIndex = 82;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Fichiers et documents obligatoires de l\'application principale";
            // 
            // txtReleasePath
            // 
            this.txtReleasePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReleasePath.Location = new System.Drawing.Point(42, 159);
            this.txtReleasePath.Name = "txtReleasePath";
            this.txtReleasePath.ReadOnly = true;
            this.txtReleasePath.Size = new System.Drawing.Size(699, 22);
            this.txtReleasePath.TabIndex = 72;
            // 
            // txtTTAppPath
            // 
            this.txtTTAppPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTTAppPath.Location = new System.Drawing.Point(42, 114);
            this.txtTTAppPath.Name = "txtTTAppPath";
            this.txtTTAppPath.ReadOnly = true;
            this.txtTTAppPath.Size = new System.Drawing.Size(699, 22);
            this.txtTTAppPath.TabIndex = 71;
            // 
            // txtWordAppChangePath
            // 
            this.txtWordAppChangePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWordAppChangePath.Location = new System.Drawing.Point(42, 68);
            this.txtWordAppChangePath.Name = "txtWordAppChangePath";
            this.txtWordAppChangePath.ReadOnly = true;
            this.txtWordAppChangePath.Size = new System.Drawing.Size(699, 22);
            this.txtWordAppChangePath.TabIndex = 70;
            // 
            // txtExcelAppChangePath
            // 
            this.txtExcelAppChangePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExcelAppChangePath.Location = new System.Drawing.Point(42, 22);
            this.txtExcelAppChangePath.Name = "txtExcelAppChangePath";
            this.txtExcelAppChangePath.ReadOnly = true;
            this.txtExcelAppChangePath.Size = new System.Drawing.Size(699, 22);
            this.txtExcelAppChangePath.TabIndex = 69;
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.Controls.Add(this.btnSelectVariousFilePath);
            this.groupBox6.Controls.Add(this.label8);
            this.groupBox6.Controls.Add(this.label9);
            this.groupBox6.Controls.Add(this.btnSelectVariousFolderPath);
            this.groupBox6.Location = new System.Drawing.Point(3, 404);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(303, 55);
            this.groupBox6.TabIndex = 81;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Documents divers";
            // 
            // btnSelectVariousFilePath
            // 
            this.btnSelectVariousFilePath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSelectVariousFilePath.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectVariousFilePath.Image")));
            this.btnSelectVariousFilePath.Location = new System.Drawing.Point(103, 16);
            this.btnSelectVariousFilePath.Name = "btnSelectVariousFilePath";
            this.btnSelectVariousFilePath.Size = new System.Drawing.Size(30, 30);
            this.btnSelectVariousFilePath.TabIndex = 75;
            this.toolTip.SetToolTip(this.btnSelectVariousFilePath, "Permet de copier un fichier quelconque dans le dossier racine de la version");
            this.btnSelectVariousFilePath.UseVisualStyleBackColor = true;
            this.btnSelectVariousFilePath.Click += new System.EventHandler(this.btnSelectVariousFilePath_Click);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(157, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 17);
            this.label8.TabIndex = 78;
            this.label8.Text = "Ajouter un dossier :";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(6, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(93, 17);
            this.label9.TabIndex = 77;
            this.label9.Text = "Ajouter un fichier :";
            // 
            // btnSelectVariousFolderPath
            // 
            this.btnSelectVariousFolderPath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSelectVariousFolderPath.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectVariousFolderPath.Image")));
            this.btnSelectVariousFolderPath.Location = new System.Drawing.Point(263, 15);
            this.btnSelectVariousFolderPath.Name = "btnSelectVariousFolderPath";
            this.btnSelectVariousFolderPath.Size = new System.Drawing.Size(30, 30);
            this.btnSelectVariousFolderPath.TabIndex = 76;
            this.toolTip.SetToolTip(this.btnSelectVariousFolderPath, "Permet de copier un dossier quelconque dans le dossier racine de la version");
            this.btnSelectVariousFolderPath.UseVisualStyleBackColor = true;
            this.btnSelectVariousFolderPath.Click += new System.EventHandler(this.btnSelectVariousFolderPath_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnGenerate);
            this.groupBox5.Controls.Add(this.chkIncludeScripts);
            this.groupBox5.Location = new System.Drawing.Point(706, 380);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(97, 79);
            this.groupBox5.TabIndex = 63;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Actions";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.grdSatellite);
            this.groupBox4.Location = new System.Drawing.Point(448, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(352, 167);
            this.groupBox4.TabIndex = 62;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Applications satellites";
            // 
            // grdSatellite
            // 
            this.grdSatellite.AutoSearch = C1.Win.C1FlexGrid.AutoSearchEnum.FromTop;
            this.grdSatellite.ColumnInfo = "1,1,0,0,0,90,Columns:0{Width:5;}\t";
            this.grdSatellite.ExtendLastCol = true;
            this.grdSatellite.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdSatellite.Location = new System.Drawing.Point(6, 17);
            this.grdSatellite.Name = "grdSatellite";
            this.grdSatellite.Rows.Count = 1;
            this.grdSatellite.Rows.DefaultSize = 18;
            this.grdSatellite.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
            this.grdSatellite.Size = new System.Drawing.Size(341, 144);
            this.grdSatellite.StyleInfo = resources.GetString("grdSatellite.StyleInfo");
            this.grdSatellite.TabIndex = 0;
            this.grdSatellite.Tag = "26";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnExportInstallationKit);
            this.groupBox2.Controls.Add(this.cboClients);
            this.groupBox2.Controls.Add(this.btnGrdClientsDel);
            this.groupBox2.Controls.Add(this.btnGrdClientsAdd);
            this.groupBox2.Controls.Add(this.grdClients);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(436, 167);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Clients à qui la version est destinée";
            // 
            // cboClients
            // 
            this.cboClients.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClients.FormattingEnabled = true;
            this.cboClients.Location = new System.Drawing.Point(37, 48);
            this.cboClients.Name = "cboClients";
            this.cboClients.Size = new System.Drawing.Size(169, 21);
            this.cboClients.TabIndex = 15;
            this.cboClients.Visible = false;
            this.cboClients.SelectedIndexChanged += new System.EventHandler(this.cboClients_SelectedIndexChanged);
            // 
            // btnGrdClientsDel
            // 
            this.btnGrdClientsDel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGrdClientsDel.Image = ((System.Drawing.Image)(resources.GetObject("btnGrdClientsDel.Image")));
            this.btnGrdClientsDel.Location = new System.Drawing.Point(395, 58);
            this.btnGrdClientsDel.Name = "btnGrdClientsDel";
            this.btnGrdClientsDel.Size = new System.Drawing.Size(35, 35);
            this.btnGrdClientsDel.TabIndex = 2;
            this.btnGrdClientsDel.UseVisualStyleBackColor = true;
            this.btnGrdClientsDel.Click += new System.EventHandler(this.btnGrdClientsDel_Click);
            // 
            // btnGrdClientsAdd
            // 
            this.btnGrdClientsAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGrdClientsAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnGrdClientsAdd.Image")));
            this.btnGrdClientsAdd.Location = new System.Drawing.Point(395, 17);
            this.btnGrdClientsAdd.Name = "btnGrdClientsAdd";
            this.btnGrdClientsAdd.Size = new System.Drawing.Size(35, 35);
            this.btnGrdClientsAdd.TabIndex = 0;
            this.btnGrdClientsAdd.UseVisualStyleBackColor = true;
            // 
            // grdClients
            // 
            this.grdClients.AutoSearch = C1.Win.C1FlexGrid.AutoSearchEnum.FromTop;
            this.grdClients.ColumnInfo = "1,1,0,0,0,90,Columns:0{Width:5;}\t";
            this.grdClients.ExtendLastCol = true;
            this.grdClients.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdClients.Location = new System.Drawing.Point(6, 17);
            this.grdClients.Name = "grdClients";
            this.grdClients.Rows.Count = 1;
            this.grdClients.Rows.DefaultSize = 18;
            this.grdClients.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
            this.grdClients.Size = new System.Drawing.Size(385, 144);
            this.grdClients.StyleInfo = resources.GetString("grdClients.StyleInfo");
            this.grdClients.TabIndex = 1;
            this.grdClients.Tag = "15";
            this.grdClients.BeforeRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.grdClients_BeforeRowColChange);
            this.grdClients.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.grdClients_AfterRowColChange);
            this.grdClients.DoubleClick += new System.EventHandler(this.grdClients_DoubleClick);
            // 
            // tab
            // 
            this.tab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tab.Controls.Add(this.tabVersion);
            this.tab.Controls.Add(this.tabRevision);
            this.tab.Location = new System.Drawing.Point(5, 85);
            this.tab.Name = "tab";
            this.tab.SelectedIndex = 0;
            this.tab.Size = new System.Drawing.Size(814, 488);
            this.tab.TabIndex = 0;
            // 
            // formController
            // 
            this.formController.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.formController.ChangeMade = false;
            this.formController.FormIsLoading = false;
            this.formController.FormMode = Ceritar.TT3LightDLL.Static_Classes.sclsConstants.DML_Mode.CONSULT_MODE;
            this.formController.Item_NRI = 0;
            this.formController.Location = new System.Drawing.Point(-1, 583);
            this.formController.Name = "formController";
            this.formController.ShowButtonQuitOnly = false;
            this.formController.Size = new System.Drawing.Size(820, 33);
            this.formController.TabIndex = 1;
            this.formController.SetReadRights += new Ceritar.TT3LightDLL.Controls.ctlFormController.SetReadRightsEventHandler(this.formController_SetReadRights);
            this.formController.LoadData += new Ceritar.TT3LightDLL.Controls.ctlFormController.LoadDataEventHandler(this.formController_LoadData);
            this.formController.ValidateForm += new Ceritar.TT3LightDLL.Controls.ctlFormController.ValidateFormEventHandler(this.formController_ValidateForm);
            this.formController.SaveData += new Ceritar.TT3LightDLL.Controls.ctlFormController.SaveDataEventHandler(this.formController_SaveData);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Green;
            this.label7.Location = new System.Drawing.Point(40, 589);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(537, 20);
            this.label7.TabIndex = 51;
            this.label7.Text = "DB_UpgradeScripts et Installations_Actives sont synchronisés";
            // 
            // frmVersion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 617);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.formController);
            this.Controls.Add(this.tab);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmVersion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestion de version";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabRevision.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdRevisions)).EndInit();
            this.tabVersion.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdSatellite)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdClients)).EndInit();
            this.tab.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public TT3LightDLL.Controls.ctlFormController formController;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboTemplates;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtVersionNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCompiledBy;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboApplications;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.DateTimePicker dtpCreation;
        private System.Windows.Forms.TabPage tabRevision;
        private System.Windows.Forms.TabPage tabVersion;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnGrdClientsDel;
        private System.Windows.Forms.Button btnGrdClientsAdd;
        public C1.Win.C1FlexGrid.C1FlexGrid grdClients;
        private System.Windows.Forms.TabControl tab;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnGrdRevDel;
        private System.Windows.Forms.Button btnGrdRevAdd;
        public C1.Win.C1FlexGrid.C1FlexGrid grdRevisions;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.ComboBox cboClients;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkDemoVersion;
        private System.Windows.Forms.GroupBox groupBox4;
        public C1.Win.C1FlexGrid.C1FlexGrid grdSatellite;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnExportInstallationKit;
        private System.Windows.Forms.CheckBox chkIncludeScripts;
        private System.Windows.Forms.Button btnGrdRevUpdate;
        private System.Windows.Forms.TextBox txtCreatedBy;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private TT3LightDLL.Controls.ctlRefresh btnRefresh;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnSelectVariousFilePath;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnSelectVariousFolderPath;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btnReplaceExecutable;
        private System.Windows.Forms.Button btnReplaceTTApp;
        private System.Windows.Forms.Button btnReplaceAppChangeDOC;
        private System.Windows.Forms.Button btnReplaceAppChangeXLS;
        private System.Windows.Forms.TextBox txtReleasePath;
        private System.Windows.Forms.Button btnShowExecutable;
        private System.Windows.Forms.TextBox txtTTAppPath;
        private System.Windows.Forms.TextBox txtWordAppChangePath;
        private System.Windows.Forms.TextBox txtExcelAppChangePath;
        private System.Windows.Forms.Button btnShowAccess;
        private System.Windows.Forms.Button btnShowWord;
        private System.Windows.Forms.Button btnShowExcel;
    }
}