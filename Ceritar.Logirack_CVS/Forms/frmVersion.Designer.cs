namespace Ceritar.Logirack_CVS.Forms
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
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
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
            this.btnSelectVariousFilePath = new System.Windows.Forms.Button();
            this.btnSelectVariousFolderPath = new System.Windows.Forms.Button();
            this.btnShowRootFolder = new System.Windows.Forms.Button();
            this.btnShowDB_UpgradeScripts = new System.Windows.Forms.Button();
            this.tabRevision = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnRefresh = new Ceritar.TT3LightDLL.Controls.ctlRefresh();
            this.grdRevisions = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.tabVersion = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cboClients = new System.Windows.Forms.ComboBox();
            this.btnGrdClientsDel = new System.Windows.Forms.Button();
            this.btnGrdClientsAdd = new System.Windows.Forms.Button();
            this.grdClients = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.grdSatellite = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.mnuClientSatVersion = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.txtReleasePath = new System.Windows.Forms.TextBox();
            this.txtTTAppPath = new System.Windows.Forms.TextBox();
            this.txtWordAppChangePath = new System.Windows.Forms.TextBox();
            this.txtExcelAppChangePath = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.tab = new System.Windows.Forms.TabControl();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.label7 = new System.Windows.Forms.Label();
            this.tmrGenerateBlink = new System.Windows.Forms.Timer(this.components);
            this.formController = new Ceritar.TT3LightDLL.Controls.ctlFormController();
            this.groupBox1.SuspendLayout();
            this.tabRevision.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdRevisions)).BeginInit();
            this.tabVersion.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdClients)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSatellite)).BeginInit();
            this.mnuClientSatVersion.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tab.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtDescription);
            this.groupBox1.Controls.Add(this.label10);
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
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(1102, 91);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Informations";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(680, 24);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDescription.MaxLength = 10;
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(162, 55);
            this.txtDescription.TabIndex = 5;
            this.txtDescription.TextChanged += new System.EventHandler(this.txtDescription_TextChanged);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(600, 27);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 21);
            this.label10.TabIndex = 52;
            this.label10.Text = "Description: ";
            // 
            // txtCreatedBy
            // 
            this.txtCreatedBy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCreatedBy.Location = new System.Drawing.Point(907, 57);
            this.txtCreatedBy.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCreatedBy.Name = "txtCreatedBy";
            this.txtCreatedBy.ReadOnly = true;
            this.txtCreatedBy.Size = new System.Drawing.Size(186, 22);
            this.txtCreatedBy.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(848, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 16);
            this.label6.TabIndex = 73;
            this.label6.Text = "Usager:";
            // 
            // chkDemoVersion
            // 
            this.chkDemoVersion.AutoSize = true;
            this.chkDemoVersion.Location = new System.Drawing.Point(523, 25);
            this.chkDemoVersion.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkDemoVersion.Name = "chkDemoVersion";
            this.chkDemoVersion.Size = new System.Drawing.Size(61, 20);
            this.chkDemoVersion.TabIndex = 3;
            this.chkDemoVersion.Text = "Démo";
            this.chkDemoVersion.UseVisualStyleBackColor = true;
            this.chkDemoVersion.CheckedChanged += new System.EventHandler(this.chkDemoVersion_CheckedChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(848, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 16);
            this.label2.TabIndex = 69;
            this.label2.Text = "Date de création:";
            // 
            // dtpCreation
            // 
            this.dtpCreation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpCreation.CustomFormat = "MM-dd-yyyy hh:mm";
            this.dtpCreation.Enabled = false;
            this.dtpCreation.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCreation.Location = new System.Drawing.Point(956, 24);
            this.dtpCreation.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpCreation.Name = "dtpCreation";
            this.dtpCreation.Size = new System.Drawing.Size(137, 22);
            this.dtpCreation.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(7, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 21);
            this.label5.TabIndex = 49;
            this.label5.Text = "Gabarit:";
            // 
            // cboTemplates
            // 
            this.cboTemplates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTemplates.FormattingEnabled = true;
            this.cboTemplates.Location = new System.Drawing.Point(86, 57);
            this.cboTemplates.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboTemplates.Name = "cboTemplates";
            this.cboTemplates.Size = new System.Drawing.Size(249, 24);
            this.cboTemplates.TabIndex = 1;
            this.cboTemplates.SelectedIndexChanged += new System.EventHandler(this.cboGabarits_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(346, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 21);
            this.label4.TabIndex = 44;
            this.label4.Text = "Numéro:";
            // 
            // txtVersionNo
            // 
            this.txtVersionNo.Location = new System.Drawing.Point(439, 24);
            this.txtVersionNo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtVersionNo.MaxLength = 10;
            this.txtVersionNo.Name = "txtVersionNo";
            this.txtVersionNo.Size = new System.Drawing.Size(76, 22);
            this.txtVersionNo.TabIndex = 2;
            this.txtVersionNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtVersionNo.TextChanged += new System.EventHandler(this.txtVersionNo_TextChanged);
            this.txtVersionNo.Validating += new System.ComponentModel.CancelEventHandler(this.txtVersionNo_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(346, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 21);
            this.label1.TabIndex = 42;
            this.label1.Text = "Compilé par:";
            // 
            // txtCompiledBy
            // 
            this.txtCompiledBy.Location = new System.Drawing.Point(439, 57);
            this.txtCompiledBy.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCompiledBy.Name = "txtCompiledBy";
            this.txtCompiledBy.Size = new System.Drawing.Size(225, 22);
            this.txtCompiledBy.TabIndex = 4;
            this.txtCompiledBy.TextChanged += new System.EventHandler(this.txtCompiledBy_TextChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(7, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 21);
            this.label3.TabIndex = 40;
            this.label3.Text = "Application:";
            // 
            // cboApplications
            // 
            this.cboApplications.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboApplications.FormattingEnabled = true;
            this.cboApplications.Location = new System.Drawing.Point(86, 23);
            this.cboApplications.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboApplications.Name = "cboApplications";
            this.cboApplications.Size = new System.Drawing.Size(249, 24);
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
            this.btnGrdRevDel.Location = new System.Drawing.Point(1030, 127);
            this.btnGrdRevDel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnGrdRevDel.Name = "btnGrdRevDel";
            this.btnGrdRevDel.Size = new System.Drawing.Size(41, 43);
            this.btnGrdRevDel.TabIndex = 17;
            this.toolTip.SetToolTip(this.btnGrdRevDel, "Supprimer une révision");
            this.btnGrdRevDel.UseVisualStyleBackColor = true;
            this.btnGrdRevDel.Click += new System.EventHandler(this.btnGrdRevDel_Click);
            // 
            // btnGrdRevAdd
            // 
            this.btnGrdRevAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGrdRevAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGrdRevAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnGrdRevAdd.Image")));
            this.btnGrdRevAdd.Location = new System.Drawing.Point(1030, 26);
            this.btnGrdRevAdd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnGrdRevAdd.Name = "btnGrdRevAdd";
            this.btnGrdRevAdd.Size = new System.Drawing.Size(41, 43);
            this.btnGrdRevAdd.TabIndex = 15;
            this.toolTip.SetToolTip(this.btnGrdRevAdd, "Créer une révision");
            this.btnGrdRevAdd.UseVisualStyleBackColor = true;
            this.btnGrdRevAdd.Click += new System.EventHandler(this.btnGrdRevAdd_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnGenerate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGenerate.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerate.Image")));
            this.btnGenerate.Location = new System.Drawing.Point(33, 20);
            this.btnGenerate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(47, 49);
            this.btnGenerate.TabIndex = 0;
            this.toolTip.SetToolTip(this.btnGenerate, "Mettre à jour les installations actives sans resauvegarder tout l\'écran");
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnExportInstallationKit
            // 
            this.btnExportInstallationKit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportInstallationKit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnExportInstallationKit.Image = ((System.Drawing.Image)(resources.GetObject("btnExportInstallationKit.Image")));
            this.btnExportInstallationKit.Location = new System.Drawing.Point(591, 155);
            this.btnExportInstallationKit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportInstallationKit.Name = "btnExportInstallationKit";
            this.btnExportInstallationKit.Size = new System.Drawing.Size(41, 43);
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
            this.btnGrdRevUpdate.Location = new System.Drawing.Point(1030, 76);
            this.btnGrdRevUpdate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnGrdRevUpdate.Name = "btnGrdRevUpdate";
            this.btnGrdRevUpdate.Size = new System.Drawing.Size(41, 43);
            this.btnGrdRevUpdate.TabIndex = 16;
            this.toolTip.SetToolTip(this.btnGrdRevUpdate, "Modifier une révision");
            this.btnGrdRevUpdate.UseVisualStyleBackColor = true;
            this.btnGrdRevUpdate.Click += new System.EventHandler(this.btnGrdRevUpdate_Click);
            // 
            // chkIncludeScripts
            // 
            this.chkIncludeScripts.AutoSize = true;
            this.chkIncludeScripts.Location = new System.Drawing.Point(5, 73);
            this.chkIncludeScripts.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkIncludeScripts.Name = "chkIncludeScripts";
            this.chkIncludeScripts.Size = new System.Drawing.Size(108, 20);
            this.chkIncludeScripts.TabIndex = 1;
            this.chkIncludeScripts.Text = "Inclure scripts";
            this.toolTip.SetToolTip(this.chkIncludeScripts, "Reconstruit la structures des scripts à partir de DB_UPGRADE_SCRIPTS");
            this.chkIncludeScripts.UseVisualStyleBackColor = true;
            // 
            // btnReplaceExecutable
            // 
            this.btnReplaceExecutable.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnReplaceExecutable.Image = ((System.Drawing.Image)(resources.GetObject("btnReplaceExecutable.Image")));
            this.btnReplaceExecutable.Location = new System.Drawing.Point(7, 192);
            this.btnReplaceExecutable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnReplaceExecutable.Name = "btnReplaceExecutable";
            this.btnReplaceExecutable.Size = new System.Drawing.Size(35, 37);
            this.btnReplaceExecutable.TabIndex = 3;
            this.toolTip.SetToolTip(this.btnReplaceExecutable, "Sélectionner l\'exécutable");
            this.btnReplaceExecutable.UseVisualStyleBackColor = true;
            this.btnReplaceExecutable.Click += new System.EventHandler(this.btnReplaceExecutable_Click);
            // 
            // btnReplaceTTApp
            // 
            this.btnReplaceTTApp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnReplaceTTApp.Image = ((System.Drawing.Image)(resources.GetObject("btnReplaceTTApp.Image")));
            this.btnReplaceTTApp.Location = new System.Drawing.Point(7, 137);
            this.btnReplaceTTApp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnReplaceTTApp.Name = "btnReplaceTTApp";
            this.btnReplaceTTApp.Size = new System.Drawing.Size(35, 37);
            this.btnReplaceTTApp.TabIndex = 2;
            this.toolTip.SetToolTip(this.btnReplaceTTApp, "Sélectionner le TTApp");
            this.btnReplaceTTApp.UseVisualStyleBackColor = true;
            this.btnReplaceTTApp.Click += new System.EventHandler(this.btnReplaceTTApp_Click);
            // 
            // btnReplaceAppChangeDOC
            // 
            this.btnReplaceAppChangeDOC.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnReplaceAppChangeDOC.Image = ((System.Drawing.Image)(resources.GetObject("btnReplaceAppChangeDOC.Image")));
            this.btnReplaceAppChangeDOC.Location = new System.Drawing.Point(7, 80);
            this.btnReplaceAppChangeDOC.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnReplaceAppChangeDOC.Name = "btnReplaceAppChangeDOC";
            this.btnReplaceAppChangeDOC.Size = new System.Drawing.Size(35, 37);
            this.btnReplaceAppChangeDOC.TabIndex = 1;
            this.toolTip.SetToolTip(this.btnReplaceAppChangeDOC, "Sélectionner le App_Changements");
            this.btnReplaceAppChangeDOC.UseVisualStyleBackColor = true;
            this.btnReplaceAppChangeDOC.Click += new System.EventHandler(this.btnReplaceAppChangeDOC_Click);
            // 
            // btnReplaceAppChangeXLS
            // 
            this.btnReplaceAppChangeXLS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnReplaceAppChangeXLS.Image = ((System.Drawing.Image)(resources.GetObject("btnReplaceAppChangeXLS.Image")));
            this.btnReplaceAppChangeXLS.Location = new System.Drawing.Point(7, 22);
            this.btnReplaceAppChangeXLS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnReplaceAppChangeXLS.Name = "btnReplaceAppChangeXLS";
            this.btnReplaceAppChangeXLS.Size = new System.Drawing.Size(35, 37);
            this.btnReplaceAppChangeXLS.TabIndex = 0;
            this.toolTip.SetToolTip(this.btnReplaceAppChangeXLS, "Sélectionner le App_Changements");
            this.btnReplaceAppChangeXLS.UseVisualStyleBackColor = true;
            this.btnReplaceAppChangeXLS.Click += new System.EventHandler(this.btnReplaceAppChangeXLS_Click);
            // 
            // btnShowExecutable
            // 
            this.btnShowExecutable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowExecutable.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnShowExecutable.BackgroundImage")));
            this.btnShowExecutable.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnShowExecutable.Location = new System.Drawing.Point(1024, 185);
            this.btnShowExecutable.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnShowExecutable.Name = "btnShowExecutable";
            this.btnShowExecutable.Size = new System.Drawing.Size(47, 49);
            this.btnShowExecutable.TabIndex = 11;
            this.toolTip.SetToolTip(this.btnShowExecutable, "Consulter l\'exécutable");
            this.btnShowExecutable.UseVisualStyleBackColor = true;
            this.btnShowExecutable.Click += new System.EventHandler(this.btnShowExecutable_Click);
            // 
            // btnShowAccess
            // 
            this.btnShowAccess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowAccess.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnShowAccess.BackgroundImage")));
            this.btnShowAccess.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnShowAccess.Location = new System.Drawing.Point(1024, 129);
            this.btnShowAccess.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnShowAccess.Name = "btnShowAccess";
            this.btnShowAccess.Size = new System.Drawing.Size(47, 49);
            this.btnShowAccess.TabIndex = 10;
            this.toolTip.SetToolTip(this.btnShowAccess, "Consulter TTApp");
            this.btnShowAccess.UseVisualStyleBackColor = true;
            this.btnShowAccess.Click += new System.EventHandler(this.btnShowAccess_Click);
            // 
            // btnShowWord
            // 
            this.btnShowWord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowWord.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnShowWord.BackgroundImage")));
            this.btnShowWord.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnShowWord.Location = new System.Drawing.Point(1024, 73);
            this.btnShowWord.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnShowWord.Name = "btnShowWord";
            this.btnShowWord.Size = new System.Drawing.Size(47, 49);
            this.btnShowWord.TabIndex = 9;
            this.toolTip.SetToolTip(this.btnShowWord, "Consulter App_Changements.docx");
            this.btnShowWord.UseVisualStyleBackColor = true;
            this.btnShowWord.Click += new System.EventHandler(this.btnShowWord_Click);
            // 
            // btnShowExcel
            // 
            this.btnShowExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowExcel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnShowExcel.BackgroundImage")));
            this.btnShowExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnShowExcel.Location = new System.Drawing.Point(1024, 16);
            this.btnShowExcel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnShowExcel.Name = "btnShowExcel";
            this.btnShowExcel.Size = new System.Drawing.Size(47, 49);
            this.btnShowExcel.TabIndex = 8;
            this.toolTip.SetToolTip(this.btnShowExcel, "Consulter App_Changements.xls");
            this.btnShowExcel.UseVisualStyleBackColor = true;
            this.btnShowExcel.Click += new System.EventHandler(this.btnShowExcel_Click);
            // 
            // btnSelectVariousFilePath
            // 
            this.btnSelectVariousFilePath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSelectVariousFilePath.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectVariousFilePath.Image")));
            this.btnSelectVariousFilePath.Location = new System.Drawing.Point(120, 20);
            this.btnSelectVariousFilePath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSelectVariousFilePath.Name = "btnSelectVariousFilePath";
            this.btnSelectVariousFilePath.Size = new System.Drawing.Size(35, 37);
            this.btnSelectVariousFilePath.TabIndex = 1;
            this.toolTip.SetToolTip(this.btnSelectVariousFilePath, "Permet de copier un fichier quelconque dans le dossier racine de la version");
            this.btnSelectVariousFilePath.UseVisualStyleBackColor = true;
            this.btnSelectVariousFilePath.Click += new System.EventHandler(this.btnSelectVariousFilePath_Click);
            // 
            // btnSelectVariousFolderPath
            // 
            this.btnSelectVariousFolderPath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSelectVariousFolderPath.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectVariousFolderPath.Image")));
            this.btnSelectVariousFolderPath.Location = new System.Drawing.Point(307, 18);
            this.btnSelectVariousFolderPath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSelectVariousFolderPath.Name = "btnSelectVariousFolderPath";
            this.btnSelectVariousFolderPath.Size = new System.Drawing.Size(35, 37);
            this.btnSelectVariousFolderPath.TabIndex = 2;
            this.toolTip.SetToolTip(this.btnSelectVariousFolderPath, "Permet de copier un dossier quelconque dans le dossier racine de la version");
            this.btnSelectVariousFolderPath.UseVisualStyleBackColor = true;
            this.btnSelectVariousFolderPath.Click += new System.EventHandler(this.btnSelectVariousFolderPath_Click);
            // 
            // btnShowRootFolder
            // 
            this.btnShowRootFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowRootFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnShowRootFolder.Image = ((System.Drawing.Image)(resources.GetObject("btnShowRootFolder.Image")));
            this.btnShowRootFolder.Location = new System.Drawing.Point(896, 492);
            this.btnShowRootFolder.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnShowRootFolder.Name = "btnShowRootFolder";
            this.btnShowRootFolder.Size = new System.Drawing.Size(47, 49);
            this.btnShowRootFolder.TabIndex = 3;
            this.toolTip.SetToolTip(this.btnShowRootFolder, "Accéder à la racine de la version");
            this.btnShowRootFolder.UseVisualStyleBackColor = true;
            this.btnShowRootFolder.Click += new System.EventHandler(this.btnShowRootFolder_Click);
            // 
            // btnShowDB_UpgradeScripts
            // 
            this.btnShowDB_UpgradeScripts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowDB_UpgradeScripts.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnShowDB_UpgradeScripts.BackgroundImage")));
            this.btnShowDB_UpgradeScripts.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnShowDB_UpgradeScripts.Location = new System.Drawing.Point(842, 492);
            this.btnShowDB_UpgradeScripts.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnShowDB_UpgradeScripts.Name = "btnShowDB_UpgradeScripts";
            this.btnShowDB_UpgradeScripts.Size = new System.Drawing.Size(47, 49);
            this.btnShowDB_UpgradeScripts.TabIndex = 2;
            this.toolTip.SetToolTip(this.btnShowDB_UpgradeScripts, "Accéder à DB_UpgradeScripts");
            this.btnShowDB_UpgradeScripts.UseVisualStyleBackColor = true;
            this.btnShowDB_UpgradeScripts.Click += new System.EventHandler(this.btnShowDB_UpgradeScripts_Click);
            // 
            // tabRevision
            // 
            this.tabRevision.Controls.Add(this.groupBox3);
            this.tabRevision.Location = new System.Drawing.Point(4, 25);
            this.tabRevision.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabRevision.Name = "tabRevision";
            this.tabRevision.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabRevision.Size = new System.Drawing.Size(1094, 581);
            this.tabRevision.TabIndex = 1;
            this.tabRevision.Text = "Révisions";
            this.tabRevision.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.btnRefresh);
            this.groupBox3.Controls.Add(this.btnGrdRevUpdate);
            this.groupBox3.Controls.Add(this.btnGrdRevDel);
            this.groupBox3.Controls.Add(this.btnGrdRevAdd);
            this.groupBox3.Controls.Add(this.grdRevisions);
            this.groupBox3.Location = new System.Drawing.Point(7, 6);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Size = new System.Drawing.Size(1079, 553);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Liste des révisions";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(1030, 503);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(41, 43);
            this.btnRefresh.TabIndex = 19;
            this.btnRefresh.Click += new Ceritar.TT3LightDLL.Controls.ctlRefresh.ClickEventHandler(this.btnRefresh_Click);
            // 
            // grdRevisions
            // 
            this.grdRevisions.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.grdRevisions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdRevisions.AutoSearch = C1.Win.C1FlexGrid.AutoSearchEnum.FromTop;
            this.grdRevisions.ColumnInfo = resources.GetString("grdRevisions.ColumnInfo");
            this.grdRevisions.ExtendLastCol = true;
            this.grdRevisions.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdRevisions.Location = new System.Drawing.Point(7, 26);
            this.grdRevisions.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grdRevisions.Name = "grdRevisions";
            this.grdRevisions.Rows.Count = 6;
            this.grdRevisions.Rows.DefaultSize = 18;
            this.grdRevisions.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
            this.grdRevisions.ShowSort = false;
            this.grdRevisions.Size = new System.Drawing.Size(1015, 519);
            this.grdRevisions.StyleInfo = resources.GetString("grdRevisions.StyleInfo");
            this.grdRevisions.TabIndex = 14;
            this.grdRevisions.Tag = "20";
            this.grdRevisions.DoubleClick += new System.EventHandler(this.grdRevisions_DoubleClick);
            // 
            // tabVersion
            // 
            this.tabVersion.Controls.Add(this.tableLayoutPanel1);
            this.tabVersion.Controls.Add(this.btnShowDB_UpgradeScripts);
            this.tabVersion.Controls.Add(this.btnShowRootFolder);
            this.tabVersion.Controls.Add(this.groupBox7);
            this.tabVersion.Controls.Add(this.groupBox6);
            this.tabVersion.Controls.Add(this.groupBox5);
            this.tabVersion.Location = new System.Drawing.Point(4, 25);
            this.tabVersion.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabVersion.Name = "tabVersion";
            this.tabVersion.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabVersion.Size = new System.Drawing.Size(1094, 581);
            this.tabVersion.TabIndex = 0;
            this.tabVersion.Text = "Version";
            this.tabVersion.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 59.01814F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.98186F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox4, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1093, 219);
            this.tableLayoutPanel1.TabIndex = 85;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnExportInstallationKit);
            this.groupBox2.Controls.Add(this.cboClients);
            this.groupBox2.Controls.Add(this.btnGrdClientsDel);
            this.groupBox2.Controls.Add(this.btnGrdClientsAdd);
            this.groupBox2.Controls.Add(this.grdClients);
            this.groupBox2.Location = new System.Drawing.Point(3, 4);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(639, 211);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Clients à qui la version est destinée";
            // 
            // cboClients
            // 
            this.cboClients.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClients.FormattingEnabled = true;
            this.cboClients.Location = new System.Drawing.Point(43, 59);
            this.cboClients.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboClients.Name = "cboClients";
            this.cboClients.Size = new System.Drawing.Size(196, 24);
            this.cboClients.TabIndex = 15;
            this.cboClients.Visible = false;
            this.cboClients.SelectedIndexChanged += new System.EventHandler(this.cboClients_SelectedIndexChanged);
            // 
            // btnGrdClientsDel
            // 
            this.btnGrdClientsDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGrdClientsDel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGrdClientsDel.Image = ((System.Drawing.Image)(resources.GetObject("btnGrdClientsDel.Image")));
            this.btnGrdClientsDel.Location = new System.Drawing.Point(591, 71);
            this.btnGrdClientsDel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnGrdClientsDel.Name = "btnGrdClientsDel";
            this.btnGrdClientsDel.Size = new System.Drawing.Size(41, 43);
            this.btnGrdClientsDel.TabIndex = 2;
            this.btnGrdClientsDel.UseVisualStyleBackColor = true;
            this.btnGrdClientsDel.Click += new System.EventHandler(this.btnGrdClientsDel_Click);
            // 
            // btnGrdClientsAdd
            // 
            this.btnGrdClientsAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGrdClientsAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGrdClientsAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnGrdClientsAdd.Image")));
            this.btnGrdClientsAdd.Location = new System.Drawing.Point(591, 21);
            this.btnGrdClientsAdd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnGrdClientsAdd.Name = "btnGrdClientsAdd";
            this.btnGrdClientsAdd.Size = new System.Drawing.Size(41, 43);
            this.btnGrdClientsAdd.TabIndex = 0;
            this.btnGrdClientsAdd.UseVisualStyleBackColor = true;
            // 
            // grdClients
            // 
            this.grdClients.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdClients.AutoSearch = C1.Win.C1FlexGrid.AutoSearchEnum.FromTop;
            this.grdClients.ColumnInfo = "1,1,0,0,0,90,Columns:0{Width:5;}\t";
            this.grdClients.ExtendLastCol = true;
            this.grdClients.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdClients.Location = new System.Drawing.Point(7, 21);
            this.grdClients.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grdClients.Name = "grdClients";
            this.grdClients.Rows.Count = 1;
            this.grdClients.Rows.DefaultSize = 18;
            this.grdClients.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
            this.grdClients.Size = new System.Drawing.Size(579, 181);
            this.grdClients.StyleInfo = resources.GetString("grdClients.StyleInfo");
            this.grdClients.TabIndex = 1;
            this.grdClients.Tag = "15";
            this.grdClients.BeforeRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.grdClients_BeforeRowColChange);
            this.grdClients.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.grdClients_AfterRowColChange);
            this.grdClients.Click += new System.EventHandler(this.grdClients_Click);
            this.grdClients.DoubleClick += new System.EventHandler(this.grdClients_DoubleClick);
            this.grdClients.MouseMove += new System.Windows.Forms.MouseEventHandler(this.grdClients_MouseMove);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.grdSatellite);
            this.groupBox4.Location = new System.Drawing.Point(648, 4);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox4.Size = new System.Drawing.Size(442, 211);
            this.groupBox4.TabIndex = 62;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Applications satellites";
            // 
            // grdSatellite
            // 
            this.grdSatellite.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdSatellite.AutoSearch = C1.Win.C1FlexGrid.AutoSearchEnum.FromTop;
            this.grdSatellite.ColumnInfo = "1,1,0,0,0,90,Columns:0{Width:5;}\t";
            this.grdSatellite.ContextMenuStrip = this.mnuClientSatVersion;
            this.grdSatellite.ExtendLastCol = true;
            this.grdSatellite.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdSatellite.Location = new System.Drawing.Point(7, 21);
            this.grdSatellite.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grdSatellite.Name = "grdSatellite";
            this.grdSatellite.Rows.Count = 1;
            this.grdSatellite.Rows.DefaultSize = 18;
            this.grdSatellite.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
            this.grdSatellite.Size = new System.Drawing.Size(429, 181);
            this.grdSatellite.StyleInfo = resources.GetString("grdSatellite.StyleInfo");
            this.grdSatellite.TabIndex = 0;
            this.grdSatellite.Tag = "26";
            // 
            // mnuClientSatVersion
            // 
            this.mnuClientSatVersion.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuiDelete});
            this.mnuClientSatVersion.Name = "mnuClientSatVersion";
            this.mnuClientSatVersion.Size = new System.Drawing.Size(130, 26);
            this.mnuClientSatVersion.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.mnuClientSatVersion_ItemClicked);
            // 
            // mnuiDelete
            // 
            this.mnuiDelete.Name = "mnuiDelete";
            this.mnuiDelete.Size = new System.Drawing.Size(129, 22);
            this.mnuiDelete.Text = "Supprimer";
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.groupBox7.Location = new System.Drawing.Point(7, 226);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox7.Size = new System.Drawing.Size(1079, 240);
            this.groupBox7.TabIndex = 82;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Fichiers et documents obligatoires de l\'application principale";
            // 
            // txtReleasePath
            // 
            this.txtReleasePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReleasePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReleasePath.Location = new System.Drawing.Point(49, 196);
            this.txtReleasePath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtReleasePath.Name = "txtReleasePath";
            this.txtReleasePath.ReadOnly = true;
            this.txtReleasePath.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtReleasePath.Size = new System.Drawing.Size(968, 22);
            this.txtReleasePath.TabIndex = 7;
            // 
            // txtTTAppPath
            // 
            this.txtTTAppPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTTAppPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTTAppPath.Location = new System.Drawing.Point(49, 140);
            this.txtTTAppPath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTTAppPath.Name = "txtTTAppPath";
            this.txtTTAppPath.ReadOnly = true;
            this.txtTTAppPath.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtTTAppPath.Size = new System.Drawing.Size(968, 22);
            this.txtTTAppPath.TabIndex = 6;
            // 
            // txtWordAppChangePath
            // 
            this.txtWordAppChangePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWordAppChangePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWordAppChangePath.Location = new System.Drawing.Point(49, 84);
            this.txtWordAppChangePath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtWordAppChangePath.Name = "txtWordAppChangePath";
            this.txtWordAppChangePath.ReadOnly = true;
            this.txtWordAppChangePath.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtWordAppChangePath.Size = new System.Drawing.Size(968, 22);
            this.txtWordAppChangePath.TabIndex = 5;
            // 
            // txtExcelAppChangePath
            // 
            this.txtExcelAppChangePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExcelAppChangePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExcelAppChangePath.Location = new System.Drawing.Point(49, 27);
            this.txtExcelAppChangePath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtExcelAppChangePath.Name = "txtExcelAppChangePath";
            this.txtExcelAppChangePath.ReadOnly = true;
            this.txtExcelAppChangePath.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtExcelAppChangePath.Size = new System.Drawing.Size(968, 22);
            this.txtExcelAppChangePath.TabIndex = 4;
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox6.Controls.Add(this.btnSelectVariousFilePath);
            this.groupBox6.Controls.Add(this.label8);
            this.groupBox6.Controls.Add(this.label9);
            this.groupBox6.Controls.Add(this.btnSelectVariousFolderPath);
            this.groupBox6.Location = new System.Drawing.Point(3, 503);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox6.Size = new System.Drawing.Size(353, 68);
            this.groupBox6.TabIndex = 1;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Documents divers";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(183, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(117, 21);
            this.label8.TabIndex = 78;
            this.label8.Text = "Ajouter un dossier :";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(7, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(108, 21);
            this.label9.TabIndex = 77;
            this.label9.Text = "Ajouter un fichier :";
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.btnGenerate);
            this.groupBox5.Controls.Add(this.chkIncludeScripts);
            this.groupBox5.Location = new System.Drawing.Point(973, 472);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox5.Size = new System.Drawing.Size(113, 97);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Actions";
            // 
            // tab
            // 
            this.tab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tab.Controls.Add(this.tabVersion);
            this.tab.Controls.Add(this.tabRevision);
            this.tab.Location = new System.Drawing.Point(6, 105);
            this.tab.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tab.Name = "tab";
            this.tab.SelectedIndex = 0;
            this.tab.Size = new System.Drawing.Size(1102, 610);
            this.tab.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Green;
            this.label7.Location = new System.Drawing.Point(47, 725);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(537, 20);
            this.label7.TabIndex = 51;
            this.label7.Text = "DB_UpgradeScripts et Installations_Actives sont synchronisés";
            this.label7.Visible = false;
            // 
            // tmrGenerateBlink
            // 
            this.tmrGenerateBlink.Interval = 1000;
            this.tmrGenerateBlink.Tick += new System.EventHandler(this.tmrGenerateBlink_Tick);
            // 
            // formController
            // 
            this.formController.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.formController.ChangeMade = false;
            this.formController.FormIsLoading = false;
            this.formController.FormMode = Ceritar.TT3LightDLL.Static_Classes.sclsConstants.DML_Mode.CONSULT_MODE;
            this.formController.Item_NRI = 0;
            this.formController.Location = new System.Drawing.Point(-3, 723);
            this.formController.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.formController.Name = "formController";
            this.formController.ShowButtonQuitOnly = false;
            this.formController.Size = new System.Drawing.Size(1112, 34);
            this.formController.TabIndex = 2;
            this.formController.BeNotify += new Ceritar.TT3LightDLL.Controls.ctlFormController.BeNotifyEventHandler(this.formController_BeNotify);
            this.formController.SetReadRights += new Ceritar.TT3LightDLL.Controls.ctlFormController.SetReadRightsEventHandler(this.formController_SetReadRights);
            this.formController.LoadData += new Ceritar.TT3LightDLL.Controls.ctlFormController.LoadDataEventHandler(this.formController_LoadData);
            this.formController.ValidateForm += new Ceritar.TT3LightDLL.Controls.ctlFormController.ValidateFormEventHandler(this.formController_ValidateForm);
            this.formController.SaveData += new Ceritar.TT3LightDLL.Controls.ctlFormController.SaveDataEventHandler(this.formController_SaveData);
            // 
            // frmVersion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1111, 759);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.formController);
            this.Controls.Add(this.tab);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1127, 797);
            this.Name = "frmVersion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestion de version";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabRevision.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdRevisions)).EndInit();
            this.tabVersion.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdClients)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdSatellite)).EndInit();
            this.mnuClientSatVersion.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.tab.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

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
        private System.Windows.Forms.Button btnShowRootFolder;
        private System.Windows.Forms.Timer tmrGenerateBlink;
        private System.Windows.Forms.Button btnShowDB_UpgradeScripts;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public TT3LightDLL.Controls.ctlFormController formController;
        private System.Windows.Forms.ContextMenuStrip mnuClientSatVersion;
        private System.Windows.Forms.ToolStripMenuItem mnuiDelete;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label10;
    }
}