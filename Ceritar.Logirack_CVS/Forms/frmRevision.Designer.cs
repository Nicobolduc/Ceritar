namespace Ceritar.Logirack_CVS.Forms
{
    partial class frmRevision
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRevision));
            this.dtpCreation = new System.Windows.Forms.DateTimePicker();
            this.cboClients = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnGrdRevDel = new System.Windows.Forms.Button();
            this.btnGrdRevAdd = new System.Windows.Forms.Button();
            this.grdRevModifs = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.mnuRevModif = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuCopyLines = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkPreparation = new System.Windows.Forms.CheckBox();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.txtCreatedBy = new System.Windows.Forms.TextBox();
            this.txtRevisionNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtVersionNo = new System.Windows.Forms.TextBox();
            this.cboTemplates = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.toolTips = new System.Windows.Forms.ToolTip(this.components);
            this.btnExportRevision = new System.Windows.Forms.Button();
            this.btnSelectScriptsFilePath = new System.Windows.Forms.Button();
            this.btnSelectScriptsFolderPath = new System.Windows.Forms.Button();
            this.btnShowScriptsFolder = new System.Windows.Forms.Button();
            this.btnSelectExecutableFilePath = new System.Windows.Forms.Button();
            this.btnSelectExecutableFolderPath = new System.Windows.Forms.Button();
            this.btnShowExecutableFolder = new System.Windows.Forms.Button();
            this.btnSelectVariousFilePath = new System.Windows.Forms.Button();
            this.btnSelectVariousFolderPath = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnShowRootFolder = new System.Windows.Forms.Button();
            this.chkAddScripts = new System.Windows.Forms.CheckBox();
            this.btnPrintPairValidation = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.gbSatellites = new System.Windows.Forms.GroupBox();
            this.grdSatellites = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.mnuSatRevision = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuiShowInExplorer = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.gbScripts = new System.Windows.Forms.GroupBox();
            this.txtScriptsPath = new System.Windows.Forms.TextBox();
            this.gbExe = new System.Windows.Forms.GroupBox();
            this.optExeOnly = new System.Windows.Forms.RadioButton();
            this.optExeAndRpt = new System.Windows.Forms.RadioButton();
            this.txtReleasePath = new System.Windows.Forms.TextBox();
            this.optRptOnly = new System.Windows.Forms.RadioButton();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.formController = new Ceritar.TT3LightDLL.Controls.ctlFormController();
            this.chkExcludePreviousRevScripts = new System.Windows.Forms.CheckBox();
            this.chkIncludePreviousRevScripts = new System.Windows.Forms.GroupBox();
            this.txtRevisionIncluses = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdRevModifs)).BeginInit();
            this.mnuRevModif.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbSatellites.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSatellites)).BeginInit();
            this.mnuSatRevision.SuspendLayout();
            this.gbScripts.SuspendLayout();
            this.gbExe.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.chkIncludePreviousRevScripts.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpCreation
            // 
            this.dtpCreation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpCreation.CustomFormat = "MM-dd-yyyy hh:mm";
            this.dtpCreation.Enabled = false;
            this.dtpCreation.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCreation.Location = new System.Drawing.Point(481, 23);
            this.dtpCreation.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpCreation.Name = "dtpCreation";
            this.dtpCreation.Size = new System.Drawing.Size(135, 22);
            this.dtpCreation.TabIndex = 2;
            // 
            // cboClients
            // 
            this.cboClients.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClients.FormattingEnabled = true;
            this.cboClients.Location = new System.Drawing.Point(79, 53);
            this.cboClients.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboClients.Name = "cboClients";
            this.cboClients.Size = new System.Drawing.Size(284, 24);
            this.cboClients.TabIndex = 3;
            this.cboClients.SelectedIndexChanged += new System.EventHandler(this.cboClients_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.btnGrdRevDel);
            this.groupBox3.Controls.Add(this.btnGrdRevAdd);
            this.groupBox3.Controls.Add(this.grdRevModifs);
            this.groupBox3.Location = new System.Drawing.Point(6, 177);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Size = new System.Drawing.Size(966, 256);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Livrables couverts / Modifications incluses (Shift click pour multi-ligne)";
            // 
            // btnGrdRevDel
            // 
            this.btnGrdRevDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGrdRevDel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGrdRevDel.Image = ((System.Drawing.Image)(resources.GetObject("btnGrdRevDel.Image")));
            this.btnGrdRevDel.Location = new System.Drawing.Point(918, 71);
            this.btnGrdRevDel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnGrdRevDel.Name = "btnGrdRevDel";
            this.btnGrdRevDel.Size = new System.Drawing.Size(41, 43);
            this.btnGrdRevDel.TabIndex = 1;
            this.btnGrdRevDel.UseVisualStyleBackColor = true;
            // 
            // btnGrdRevAdd
            // 
            this.btnGrdRevAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGrdRevAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGrdRevAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnGrdRevAdd.Image")));
            this.btnGrdRevAdd.Location = new System.Drawing.Point(918, 21);
            this.btnGrdRevAdd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnGrdRevAdd.Name = "btnGrdRevAdd";
            this.btnGrdRevAdd.Size = new System.Drawing.Size(41, 43);
            this.btnGrdRevAdd.TabIndex = 0;
            this.btnGrdRevAdd.UseVisualStyleBackColor = true;
            // 
            // grdRevModifs
            // 
            this.grdRevModifs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdRevModifs.AutoSearch = C1.Win.C1FlexGrid.AutoSearchEnum.FromTop;
            this.grdRevModifs.ColumnInfo = resources.GetString("grdRevModifs.ColumnInfo");
            this.grdRevModifs.ContextMenuStrip = this.mnuRevModif;
            this.grdRevModifs.ExtendLastCol = true;
            this.grdRevModifs.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdRevModifs.Location = new System.Drawing.Point(7, 22);
            this.grdRevModifs.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grdRevModifs.Name = "grdRevModifs";
            this.grdRevModifs.Rows.Count = 1;
            this.grdRevModifs.Rows.DefaultSize = 18;
            this.grdRevModifs.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
            this.grdRevModifs.Size = new System.Drawing.Size(903, 227);
            this.grdRevModifs.StyleInfo = resources.GetString("grdRevModifs.StyleInfo");
            this.grdRevModifs.TabIndex = 2;
            this.grdRevModifs.Tag = "28";
            this.grdRevModifs.DoubleClick += new System.EventHandler(this.grdRevModifs_DoubleClick);
            // 
            // mnuRevModif
            // 
            this.mnuRevModif.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCopyLines});
            this.mnuRevModif.Name = "mnuRevModif";
            this.mnuRevModif.Size = new System.Drawing.Size(224, 26);
            // 
            // mnuCopyLines
            // 
            this.mnuCopyLines.Name = "mnuCopyLines";
            this.mnuCopyLines.Size = new System.Drawing.Size(223, 22);
            this.mnuCopyLines.Text = "Copier à partir de cette ligne";
            this.mnuCopyLines.Click += new System.EventHandler(this.mnuCopyLines_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 21);
            this.label2.TabIndex = 60;
            this.label2.Text = "Client:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkPreparation);
            this.groupBox1.Controls.Add(this.txtNote);
            this.groupBox1.Controls.Add(this.txtCreatedBy);
            this.groupBox1.Controls.Add(this.txtRevisionNo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtVersionNo);
            this.groupBox1.Controls.Add(this.cboTemplates);
            this.groupBox1.Controls.Add(this.dtpCreation);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cboClients);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(622, 165);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Informations";
            // 
            // chkPreparation
            // 
            this.chkPreparation.Location = new System.Drawing.Point(447, 88);
            this.chkPreparation.Name = "chkPreparation";
            this.chkPreparation.Size = new System.Drawing.Size(169, 21);
            this.chkPreparation.TabIndex = 77;
            this.chkPreparation.Text = "Mode préparation";
            this.toolTips.SetToolTip(this.chkPreparation, "Permet de préparer la révision sans la générée dans les installations actives");
            this.chkPreparation.UseVisualStyleBackColor = true;
            this.chkPreparation.CheckStateChanged += new System.EventHandler(this.chkPreparation_CheckStateChanged);
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(79, 121);
            this.txtNote.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtNote.MaxLength = 256;
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(537, 38);
            this.txtNote.TabIndex = 75;
            this.txtNote.TextChanged += new System.EventHandler(this.txtNote_TextChanged);
            // 
            // txtCreatedBy
            // 
            this.txtCreatedBy.Location = new System.Drawing.Point(447, 53);
            this.txtCreatedBy.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCreatedBy.Name = "txtCreatedBy";
            this.txtCreatedBy.Size = new System.Drawing.Size(169, 22);
            this.txtCreatedBy.TabIndex = 4;
            this.txtCreatedBy.TextChanged += new System.EventHandler(this.txtCreatedBy_TextChanged);
            // 
            // txtRevisionNo
            // 
            this.txtRevisionNo.Location = new System.Drawing.Point(274, 23);
            this.txtRevisionNo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRevisionNo.Name = "txtRevisionNo";
            this.txtRevisionNo.ReadOnly = true;
            this.txtRevisionNo.Size = new System.Drawing.Size(67, 22);
            this.txtRevisionNo.TabIndex = 1;
            this.txtRevisionNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(183, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 21);
            this.label3.TabIndex = 69;
            this.label3.Text = "No révision:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(369, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 16);
            this.label1.TabIndex = 68;
            this.label1.Text = "Date de création:";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 21);
            this.label5.TabIndex = 51;
            this.label5.Text = "Gabarit:";
            // 
            // txtVersionNo
            // 
            this.txtVersionNo.Location = new System.Drawing.Point(79, 23);
            this.txtVersionNo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtVersionNo.Name = "txtVersionNo";
            this.txtVersionNo.ReadOnly = true;
            this.txtVersionNo.Size = new System.Drawing.Size(96, 22);
            this.txtVersionNo.TabIndex = 0;
            this.txtVersionNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cboTemplates
            // 
            this.cboTemplates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTemplates.FormattingEnabled = true;
            this.cboTemplates.Location = new System.Drawing.Point(79, 86);
            this.cboTemplates.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboTemplates.Name = "cboTemplates";
            this.cboTemplates.Size = new System.Drawing.Size(360, 24);
            this.cboTemplates.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(3, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 21);
            this.label6.TabIndex = 64;
            this.label6.Text = "No version:";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(369, 56);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 21);
            this.label7.TabIndex = 74;
            this.label7.Text = "Créée par:";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(3, 124);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 21);
            this.label9.TabIndex = 76;
            this.label9.Text = "Note:";
            // 
            // toolTips
            // 
            this.toolTips.AutoPopDelay = 10000;
            this.toolTips.InitialDelay = 50;
            this.toolTips.ReshowDelay = 100;
            // 
            // btnExportRevision
            // 
            this.btnExportRevision.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportRevision.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnExportRevision.Image = ((System.Drawing.Image)(resources.GetObject("btnExportRevision.Image")));
            this.btnExportRevision.Location = new System.Drawing.Point(280, 13);
            this.btnExportRevision.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportRevision.Name = "btnExportRevision";
            this.btnExportRevision.Size = new System.Drawing.Size(47, 49);
            this.btnExportRevision.TabIndex = 8;
            this.toolTips.SetToolTip(this.btnExportRevision, "Exporter la révision");
            this.btnExportRevision.UseVisualStyleBackColor = true;
            this.btnExportRevision.Click += new System.EventHandler(this.btnExportRevision_Click);
            // 
            // btnSelectScriptsFilePath
            // 
            this.btnSelectScriptsFilePath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSelectScriptsFilePath.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectScriptsFilePath.Image")));
            this.btnSelectScriptsFilePath.Location = new System.Drawing.Point(9, 24);
            this.btnSelectScriptsFilePath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSelectScriptsFilePath.Name = "btnSelectScriptsFilePath";
            this.btnSelectScriptsFilePath.Size = new System.Drawing.Size(35, 37);
            this.btnSelectScriptsFilePath.TabIndex = 0;
            this.toolTips.SetToolTip(this.btnSelectScriptsFilePath, "Sélectionner un script");
            this.btnSelectScriptsFilePath.UseVisualStyleBackColor = true;
            this.btnSelectScriptsFilePath.Click += new System.EventHandler(this.btnSelectScriptsFilePath_Click);
            // 
            // btnSelectScriptsFolderPath
            // 
            this.btnSelectScriptsFolderPath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSelectScriptsFolderPath.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectScriptsFolderPath.Image")));
            this.btnSelectScriptsFolderPath.Location = new System.Drawing.Point(51, 24);
            this.btnSelectScriptsFolderPath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSelectScriptsFolderPath.Name = "btnSelectScriptsFolderPath";
            this.btnSelectScriptsFolderPath.Size = new System.Drawing.Size(35, 37);
            this.btnSelectScriptsFolderPath.TabIndex = 1;
            this.toolTips.SetToolTip(this.btnSelectScriptsFolderPath, "Sélectionner un dossier de scripts");
            this.btnSelectScriptsFolderPath.UseVisualStyleBackColor = true;
            this.btnSelectScriptsFolderPath.Click += new System.EventHandler(this.btnSelectScriptsFolderPath_Click);
            // 
            // btnShowScriptsFolder
            // 
            this.btnShowScriptsFolder.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnShowScriptsFolder.BackgroundImage")));
            this.btnShowScriptsFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnShowScriptsFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowScriptsFolder.Location = new System.Drawing.Point(910, 17);
            this.btnShowScriptsFolder.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnShowScriptsFolder.Name = "btnShowScriptsFolder";
            this.btnShowScriptsFolder.Size = new System.Drawing.Size(47, 49);
            this.btnShowScriptsFolder.TabIndex = 3;
            this.toolTips.SetToolTip(this.btnShowScriptsFolder, "Consulter les scripts");
            this.btnShowScriptsFolder.UseVisualStyleBackColor = true;
            this.btnShowScriptsFolder.Click += new System.EventHandler(this.btnShowScriptsFolder_Click);
            // 
            // btnSelectExecutableFilePath
            // 
            this.btnSelectExecutableFilePath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSelectExecutableFilePath.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectExecutableFilePath.Image")));
            this.btnSelectExecutableFilePath.Location = new System.Drawing.Point(9, 24);
            this.btnSelectExecutableFilePath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSelectExecutableFilePath.Name = "btnSelectExecutableFilePath";
            this.btnSelectExecutableFilePath.Size = new System.Drawing.Size(35, 37);
            this.btnSelectExecutableFilePath.TabIndex = 0;
            this.toolTips.SetToolTip(this.btnSelectExecutableFilePath, "Sélectionner un fichier exécutable");
            this.btnSelectExecutableFilePath.UseVisualStyleBackColor = true;
            this.btnSelectExecutableFilePath.Click += new System.EventHandler(this.btnSelectExecutableFilePath_Click);
            // 
            // btnSelectExecutableFolderPath
            // 
            this.btnSelectExecutableFolderPath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSelectExecutableFolderPath.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectExecutableFolderPath.Image")));
            this.btnSelectExecutableFolderPath.Location = new System.Drawing.Point(51, 24);
            this.btnSelectExecutableFolderPath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSelectExecutableFolderPath.Name = "btnSelectExecutableFolderPath";
            this.btnSelectExecutableFolderPath.Size = new System.Drawing.Size(35, 37);
            this.btnSelectExecutableFolderPath.TabIndex = 1;
            this.toolTips.SetToolTip(this.btnSelectExecutableFolderPath, "Sélectionner un dossier de Release");
            this.btnSelectExecutableFolderPath.UseVisualStyleBackColor = true;
            this.btnSelectExecutableFolderPath.Click += new System.EventHandler(this.btnSelectExecutableFolderPath_Click);
            // 
            // btnShowExecutableFolder
            // 
            this.btnShowExecutableFolder.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnShowExecutableFolder.BackgroundImage")));
            this.btnShowExecutableFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnShowExecutableFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowExecutableFolder.Location = new System.Drawing.Point(910, 17);
            this.btnShowExecutableFolder.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnShowExecutableFolder.Name = "btnShowExecutableFolder";
            this.btnShowExecutableFolder.Size = new System.Drawing.Size(47, 49);
            this.btnShowExecutableFolder.TabIndex = 3;
            this.toolTips.SetToolTip(this.btnShowExecutableFolder, "Consulter l\'exécutable");
            this.btnShowExecutableFolder.UseVisualStyleBackColor = true;
            this.btnShowExecutableFolder.Click += new System.EventHandler(this.btnShowExecutableFolder_Click);
            // 
            // btnSelectVariousFilePath
            // 
            this.btnSelectVariousFilePath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSelectVariousFilePath.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectVariousFilePath.Image")));
            this.btnSelectVariousFilePath.Location = new System.Drawing.Point(120, 20);
            this.btnSelectVariousFilePath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSelectVariousFilePath.Name = "btnSelectVariousFilePath";
            this.btnSelectVariousFilePath.Size = new System.Drawing.Size(35, 37);
            this.btnSelectVariousFilePath.TabIndex = 0;
            this.toolTips.SetToolTip(this.btnSelectVariousFilePath, "Permet de copier un fichier quelconque dans le dossier racine de la version");
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
            this.btnSelectVariousFolderPath.TabIndex = 1;
            this.toolTips.SetToolTip(this.btnSelectVariousFolderPath, "Permet de copier un dossier quelconque dans le dossier racine de la version");
            this.btnSelectVariousFolderPath.UseVisualStyleBackColor = true;
            this.btnSelectVariousFolderPath.Click += new System.EventHandler(this.btnSelectVariousFolderPath_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGenerate.BackgroundImage")));
            this.btnGenerate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGenerate.Location = new System.Drawing.Point(864, 68);
            this.btnGenerate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(28, 31);
            this.btnGenerate.TabIndex = 7;
            this.toolTips.SetToolTip(this.btnGenerate, "Mettre à jour la version");
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Visible = false;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnShowRootFolder
            // 
            this.btnShowRootFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowRootFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnShowRootFolder.Image = ((System.Drawing.Image)(resources.GetObject("btnShowRootFolder.Image")));
            this.btnShowRootFolder.Location = new System.Drawing.Point(925, 673);
            this.btnShowRootFolder.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnShowRootFolder.Name = "btnShowRootFolder";
            this.btnShowRootFolder.Size = new System.Drawing.Size(47, 49);
            this.btnShowRootFolder.TabIndex = 6;
            this.toolTips.SetToolTip(this.btnShowRootFolder, "Accéder à la racine de la révision");
            this.btnShowRootFolder.UseVisualStyleBackColor = true;
            this.btnShowRootFolder.Click += new System.EventHandler(this.btnShowRootFolder_Click);
            // 
            // chkAddScripts
            // 
            this.chkAddScripts.AutoSize = true;
            this.chkAddScripts.Checked = true;
            this.chkAddScripts.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAddScripts.Location = new System.Drawing.Point(10, 73);
            this.chkAddScripts.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkAddScripts.Name = "chkAddScripts";
            this.chkAddScripts.Size = new System.Drawing.Size(271, 20);
            this.chkAddScripts.TabIndex = 4;
            this.chkAddScripts.Text = "Ajouter le(s) script(s) au répertoire courant";
            this.toolTips.SetToolTip(this.chkAddScripts, "Le(s) script(s) du dossier, sélectionné(s) seront ajoutés à la suite des scripts " +
        "existant. Sinon ceux existant sont supprimés.");
            this.chkAddScripts.UseVisualStyleBackColor = true;
            // 
            // btnPrintPairValidation
            // 
            this.btnPrintPairValidation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrintPairValidation.Image = ((System.Drawing.Image)(resources.GetObject("btnPrintPairValidation.Image")));
            this.btnPrintPairValidation.Location = new System.Drawing.Point(843, 673);
            this.btnPrintPairValidation.Name = "btnPrintPairValidation";
            this.btnPrintPairValidation.Size = new System.Drawing.Size(47, 49);
            this.btnPrintPairValidation.TabIndex = 5;
            this.toolTips.SetToolTip(this.btnPrintPairValidation, "Imprimer pour signature");
            this.btnPrintPairValidation.UseVisualStyleBackColor = true;
            this.btnPrintPairValidation.Click += new System.EventHandler(this.btnPrintPairValidation_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // gbSatellites
            // 
            this.gbSatellites.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSatellites.Controls.Add(this.grdSatellites);
            this.gbSatellites.Location = new System.Drawing.Point(635, 6);
            this.gbSatellites.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbSatellites.Name = "gbSatellites";
            this.gbSatellites.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbSatellites.Size = new System.Drawing.Size(337, 165);
            this.gbSatellites.TabIndex = 1;
            this.gbSatellites.TabStop = false;
            this.gbSatellites.Text = "La révision est pour l\'application sattellite suivante:";
            // 
            // grdSatellites
            // 
            this.grdSatellites.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.grdSatellites.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdSatellites.AutoSearch = C1.Win.C1FlexGrid.AutoSearchEnum.FromTop;
            this.grdSatellites.ColumnInfo = "2,1,0,0,0,90,Columns:0{Width:5;}\t";
            this.grdSatellites.ContextMenuStrip = this.mnuSatRevision;
            this.grdSatellites.ExtendLastCol = true;
            this.grdSatellites.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdSatellites.Location = new System.Drawing.Point(4, 18);
            this.grdSatellites.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grdSatellites.Name = "grdSatellites";
            this.grdSatellites.Rows.Count = 1;
            this.grdSatellites.Rows.DefaultSize = 18;
            this.grdSatellites.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
            this.grdSatellites.Size = new System.Drawing.Size(330, 144);
            this.grdSatellites.StyleInfo = resources.GetString("grdSatellites.StyleInfo");
            this.grdSatellites.TabIndex = 0;
            this.grdSatellites.Tag = "35";
            // 
            // mnuSatRevision
            // 
            this.mnuSatRevision.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuiShowInExplorer,
            this.mnuiDelete});
            this.mnuSatRevision.Name = "mnuSatRevision";
            this.mnuSatRevision.Size = new System.Drawing.Size(213, 48);
            // 
            // mnuiShowInExplorer
            // 
            this.mnuiShowInExplorer.Name = "mnuiShowInExplorer";
            this.mnuiShowInExplorer.Size = new System.Drawing.Size(212, 22);
            this.mnuiShowInExplorer.Text = "Afficher dans l\'explorateur";
            this.mnuiShowInExplorer.Click += new System.EventHandler(this.mnuiShowInExplorer_Click);
            // 
            // mnuiDelete
            // 
            this.mnuiDelete.Name = "mnuiDelete";
            this.mnuiDelete.Size = new System.Drawing.Size(212, 22);
            this.mnuiDelete.Text = "Supprimer";
            this.mnuiDelete.Click += new System.EventHandler(this.mnuiDelete_Click);
            // 
            // gbScripts
            // 
            this.gbScripts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbScripts.Controls.Add(this.chkAddScripts);
            this.gbScripts.Controls.Add(this.btnSelectScriptsFilePath);
            this.gbScripts.Controls.Add(this.btnSelectScriptsFolderPath);
            this.gbScripts.Controls.Add(this.txtScriptsPath);
            this.gbScripts.Controls.Add(this.btnShowScriptsFolder);
            this.gbScripts.Location = new System.Drawing.Point(6, 440);
            this.gbScripts.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbScripts.Name = "gbScripts";
            this.gbScripts.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbScripts.Size = new System.Drawing.Size(966, 102);
            this.gbScripts.TabIndex = 3;
            this.gbScripts.TabStop = false;
            this.gbScripts.Text = "Scripts *** Si spécifique pour client, inclure dans un sous dossier avec le NOM E" +
    "XACT du client (Même si dossier racine vide)) ***";
            // 
            // txtScriptsPath
            // 
            this.txtScriptsPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtScriptsPath.Location = new System.Drawing.Point(93, 22);
            this.txtScriptsPath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtScriptsPath.Multiline = true;
            this.txtScriptsPath.Name = "txtScriptsPath";
            this.txtScriptsPath.ReadOnly = true;
            this.txtScriptsPath.Size = new System.Drawing.Size(809, 40);
            this.txtScriptsPath.TabIndex = 2;
            // 
            // gbExe
            // 
            this.gbExe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbExe.Controls.Add(this.optExeOnly);
            this.gbExe.Controls.Add(this.optExeAndRpt);
            this.gbExe.Controls.Add(this.btnSelectExecutableFilePath);
            this.gbExe.Controls.Add(this.btnSelectExecutableFolderPath);
            this.gbExe.Controls.Add(this.btnGenerate);
            this.gbExe.Controls.Add(this.txtReleasePath);
            this.gbExe.Controls.Add(this.btnShowExecutableFolder);
            this.gbExe.Controls.Add(this.optRptOnly);
            this.gbExe.Location = new System.Drawing.Point(6, 550);
            this.gbExe.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbExe.Name = "gbExe";
            this.gbExe.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbExe.Size = new System.Drawing.Size(966, 105);
            this.gbExe.TabIndex = 4;
            this.gbExe.TabStop = false;
            this.gbExe.Text = "Exécutable";
            // 
            // optExeOnly
            // 
            this.optExeOnly.Checked = true;
            this.optExeOnly.Location = new System.Drawing.Point(9, 74);
            this.optExeOnly.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.optExeOnly.Name = "optExeOnly";
            this.optExeOnly.Size = new System.Drawing.Size(167, 25);
            this.optExeOnly.TabIndex = 4;
            this.optExeOnly.TabStop = true;
            this.optExeOnly.Text = "Exécutable seulement";
            this.optExeOnly.UseVisualStyleBackColor = true;
            this.optExeOnly.CheckedChanged += new System.EventHandler(this.optExeOnly_CheckedChanged);
            // 
            // optExeAndRpt
            // 
            this.optExeAndRpt.Location = new System.Drawing.Point(352, 74);
            this.optExeAndRpt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.optExeAndRpt.Name = "optExeAndRpt";
            this.optExeAndRpt.Size = new System.Drawing.Size(204, 25);
            this.optExeAndRpt.TabIndex = 6;
            this.optExeAndRpt.Text = "Exécutable + Rapports externe";
            this.optExeAndRpt.UseVisualStyleBackColor = true;
            this.optExeAndRpt.CheckedChanged += new System.EventHandler(this.optExeAndRpt_CheckedChanged);
            // 
            // txtReleasePath
            // 
            this.txtReleasePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReleasePath.Location = new System.Drawing.Point(93, 22);
            this.txtReleasePath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtReleasePath.Multiline = true;
            this.txtReleasePath.Name = "txtReleasePath";
            this.txtReleasePath.ReadOnly = true;
            this.txtReleasePath.Size = new System.Drawing.Size(809, 40);
            this.txtReleasePath.TabIndex = 2;
            // 
            // optRptOnly
            // 
            this.optRptOnly.Location = new System.Drawing.Point(183, 74);
            this.optRptOnly.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.optRptOnly.Name = "optRptOnly";
            this.optRptOnly.Size = new System.Drawing.Size(146, 25);
            this.optRptOnly.TabIndex = 5;
            this.optRptOnly.Text = "Rapports seulement";
            this.optRptOnly.UseVisualStyleBackColor = true;
            this.optRptOnly.CheckedChanged += new System.EventHandler(this.optRptOnly_CheckedChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox6.Controls.Add(this.btnSelectVariousFilePath);
            this.groupBox6.Controls.Add(this.label8);
            this.groupBox6.Controls.Add(this.label4);
            this.groupBox6.Controls.Add(this.btnSelectVariousFolderPath);
            this.groupBox6.Location = new System.Drawing.Point(6, 662);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox6.Size = new System.Drawing.Size(353, 68);
            this.groupBox6.TabIndex = 5;
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
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(7, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 21);
            this.label4.TabIndex = 77;
            this.label4.Text = "Ajouter un fichier :";
            // 
            // formController
            // 
            this.formController.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.formController.ChangeMade = false;
            this.formController.FormIsLoading = false;
            this.formController.FormMode = Ceritar.TT3LightDLL.Static_Classes.sclsConstants.DML_Mode.NO_MODE;
            this.formController.Item_NRI = 0;
            this.formController.Location = new System.Drawing.Point(1, 734);
            this.formController.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.formController.Name = "formController";
            this.formController.ShowButtonQuitOnly = false;
            this.formController.Size = new System.Drawing.Size(974, 34);
            this.formController.TabIndex = 9;
            this.formController.SetReadRights += new Ceritar.TT3LightDLL.Controls.ctlFormController.SetReadRightsEventHandler(this.formController_SetReadRights);
            this.formController.LoadData += new Ceritar.TT3LightDLL.Controls.ctlFormController.LoadDataEventHandler(this.formController_LoadData);
            this.formController.ValidateForm += new Ceritar.TT3LightDLL.Controls.ctlFormController.ValidateFormEventHandler(this.formController_ValidateForm);
            this.formController.SaveData += new Ceritar.TT3LightDLL.Controls.ctlFormController.SaveDataEventHandler(this.formController_SaveData);
            // 
            // chkExcludePreviousRevScripts
            // 
            this.chkExcludePreviousRevScripts.Checked = true;
            this.chkExcludePreviousRevScripts.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExcludePreviousRevScripts.Location = new System.Drawing.Point(6, 18);
            this.chkExcludePreviousRevScripts.Name = "chkExcludePreviousRevScripts";
            this.chkExcludePreviousRevScripts.Size = new System.Drawing.Size(270, 18);
            this.chkExcludePreviousRevScripts.TabIndex = 10;
            this.chkExcludePreviousRevScripts.Text = "Exclure scripts revisions précédentes";
            this.chkExcludePreviousRevScripts.UseVisualStyleBackColor = true;
            this.chkExcludePreviousRevScripts.CheckedChanged += new System.EventHandler(this.chkExcludePreviousRevScripts_CheckedChanged);
            // 
            // chkIncludePreviousRevScripts
            // 
            this.chkIncludePreviousRevScripts.Controls.Add(this.txtRevisionIncluses);
            this.chkIncludePreviousRevScripts.Controls.Add(this.label10);
            this.chkIncludePreviousRevScripts.Controls.Add(this.btnExportRevision);
            this.chkIncludePreviousRevScripts.Controls.Add(this.chkExcludePreviousRevScripts);
            this.chkIncludePreviousRevScripts.Location = new System.Drawing.Point(373, 662);
            this.chkIncludePreviousRevScripts.Name = "chkIncludePreviousRevScripts";
            this.chkIncludePreviousRevScripts.Size = new System.Drawing.Size(333, 68);
            this.chkIncludePreviousRevScripts.TabIndex = 10;
            this.chkIncludePreviousRevScripts.TabStop = false;
            this.chkIncludePreviousRevScripts.Text = "    Export vers fichier Zip";
            // 
            // txtRevisionIncluses
            // 
            this.txtRevisionIncluses.Enabled = false;
            this.txtRevisionIncluses.Location = new System.Drawing.Point(129, 38);
            this.txtRevisionIncluses.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRevisionIncluses.Name = "txtRevisionIncluses";
            this.txtRevisionIncluses.Size = new System.Drawing.Size(146, 22);
            this.txtRevisionIncluses.TabIndex = 76;
            this.txtRevisionIncluses.Validating += new System.ComponentModel.CancelEventHandler(this.txtRevisionIncluses_Validating);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(3, 41);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(135, 17);
            this.label10.TabIndex = 75;
            this.label10.Text = "Révisions à inclures:";
            // 
            // frmRevision
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 770);
            this.Controls.Add(this.chkIncludePreviousRevScripts);
            this.Controls.Add(this.btnPrintPairValidation);
            this.Controls.Add(this.btnShowRootFolder);
            this.Controls.Add(this.gbExe);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.gbScripts);
            this.Controls.Add(this.gbSatellites);
            this.Controls.Add(this.formController);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "frmRevision";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestion de révision";
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdRevModifs)).EndInit();
            this.mnuRevModif.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbSatellites.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdSatellites)).EndInit();
            this.mnuSatRevision.ResumeLayout(false);
            this.gbScripts.ResumeLayout(false);
            this.gbScripts.PerformLayout();
            this.gbExe.ResumeLayout(false);
            this.gbExe.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.chkIncludePreviousRevScripts.ResumeLayout(false);
            this.chkIncludePreviousRevScripts.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpCreation;
        private System.Windows.Forms.ComboBox cboClients;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnGrdRevDel;
        private System.Windows.Forms.Button btnGrdRevAdd;
        public C1.Win.C1FlexGrid.C1FlexGrid grdRevModifs;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtVersionNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboTemplates;
        private System.Windows.Forms.Label label1;
        public TT3LightDLL.Controls.ctlFormController formController;
        private System.Windows.Forms.TextBox txtRevisionNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolTip toolTips;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.GroupBox gbSatellites;
        public C1.Win.C1FlexGrid.C1FlexGrid grdSatellites;
        private System.Windows.Forms.Button btnExportRevision;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCreatedBy;
        private System.Windows.Forms.GroupBox gbScripts;
        private System.Windows.Forms.Button btnSelectScriptsFilePath;
        private System.Windows.Forms.Button btnSelectScriptsFolderPath;
        private System.Windows.Forms.TextBox txtScriptsPath;
        private System.Windows.Forms.Button btnShowScriptsFolder;
        private System.Windows.Forms.GroupBox gbExe;
        private System.Windows.Forms.Button btnSelectExecutableFilePath;
        private System.Windows.Forms.Button btnSelectExecutableFolderPath;
        private System.Windows.Forms.TextBox txtReleasePath;
        private System.Windows.Forms.Button btnShowExecutableFolder;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSelectVariousFolderPath;
        private System.Windows.Forms.Button btnSelectVariousFilePath;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.RadioButton optExeOnly;
        private System.Windows.Forms.RadioButton optExeAndRpt;
        private System.Windows.Forms.RadioButton optRptOnly;
        private System.Windows.Forms.Button btnShowRootFolder;
        private System.Windows.Forms.CheckBox chkAddScripts;
        private System.Windows.Forms.ContextMenuStrip mnuSatRevision;
        private System.Windows.Forms.ToolStripMenuItem mnuiDelete;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ToolStripMenuItem mnuiShowInExplorer;
        private System.Windows.Forms.Button btnPrintPairValidation;
        private System.Windows.Forms.CheckBox chkPreparation;
        private System.Windows.Forms.CheckBox chkExcludePreviousRevScripts;
        private System.Windows.Forms.ContextMenuStrip mnuRevModif;
        private System.Windows.Forms.ToolStripMenuItem mnuCopyLines;
        private System.Windows.Forms.GroupBox chkIncludePreviousRevScripts;
        private System.Windows.Forms.TextBox txtRevisionIncluses;
        private System.Windows.Forms.Label label10;
    }
}