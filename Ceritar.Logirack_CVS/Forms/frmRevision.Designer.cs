namespace Ceritar.Logirack_CVS
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
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtCreatedBy = new System.Windows.Forms.TextBox();
            this.txtRevisionNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtVersionNo = new System.Windows.Forms.TextBox();
            this.cboTemplates = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
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
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.grdSatellites = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtScriptsPath = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.optExeOnly = new System.Windows.Forms.RadioButton();
            this.optExeAndRpt = new System.Windows.Forms.RadioButton();
            this.txtReleasePath = new System.Windows.Forms.TextBox();
            this.optRptOnly = new System.Windows.Forms.RadioButton();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.formController = new Ceritar.TT3LightDLL.Controls.ctlFormController();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdRevModifs)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSatellites)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpCreation
            // 
            this.dtpCreation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpCreation.CustomFormat = "MM-dd-yyyy hh:mm";
            this.dtpCreation.Enabled = false;
            this.dtpCreation.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCreation.Location = new System.Drawing.Point(394, 19);
            this.dtpCreation.Name = "dtpCreation";
            this.dtpCreation.Size = new System.Drawing.Size(132, 20);
            this.dtpCreation.TabIndex = 4;
            // 
            // cboClients
            // 
            this.cboClients.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClients.FormattingEnabled = true;
            this.cboClients.Location = new System.Drawing.Point(68, 46);
            this.cboClients.Name = "cboClients";
            this.cboClients.Size = new System.Drawing.Size(225, 21);
            this.cboClients.TabIndex = 2;
            this.cboClients.SelectedIndexChanged += new System.EventHandler(this.cboClients_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.btnGrdRevDel);
            this.groupBox3.Controls.Add(this.btnGrdRevAdd);
            this.groupBox3.Controls.Add(this.grdRevModifs);
            this.groupBox3.Location = new System.Drawing.Point(5, 138);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(828, 208);
            this.groupBox3.TabIndex = 63;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Livrables couverts / Modifications incluses";
            // 
            // btnGrdRevDel
            // 
            this.btnGrdRevDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGrdRevDel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGrdRevDel.Image = ((System.Drawing.Image)(resources.GetObject("btnGrdRevDel.Image")));
            this.btnGrdRevDel.Location = new System.Drawing.Point(787, 58);
            this.btnGrdRevDel.Name = "btnGrdRevDel";
            this.btnGrdRevDel.Size = new System.Drawing.Size(35, 35);
            this.btnGrdRevDel.TabIndex = 2;
            this.btnGrdRevDel.UseVisualStyleBackColor = true;
            // 
            // btnGrdRevAdd
            // 
            this.btnGrdRevAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGrdRevAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGrdRevAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnGrdRevAdd.Image")));
            this.btnGrdRevAdd.Location = new System.Drawing.Point(787, 17);
            this.btnGrdRevAdd.Name = "btnGrdRevAdd";
            this.btnGrdRevAdd.Size = new System.Drawing.Size(35, 35);
            this.btnGrdRevAdd.TabIndex = 0;
            this.btnGrdRevAdd.UseVisualStyleBackColor = true;
            // 
            // grdRevModifs
            // 
            this.grdRevModifs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdRevModifs.AutoSearch = C1.Win.C1FlexGrid.AutoSearchEnum.FromTop;
            this.grdRevModifs.ColumnInfo = resources.GetString("grdRevModifs.ColumnInfo");
            this.grdRevModifs.ExtendLastCol = true;
            this.grdRevModifs.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdRevModifs.Location = new System.Drawing.Point(6, 18);
            this.grdRevModifs.Name = "grdRevModifs";
            this.grdRevModifs.Rows.Count = 1;
            this.grdRevModifs.Rows.DefaultSize = 18;
            this.grdRevModifs.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
            this.grdRevModifs.Size = new System.Drawing.Size(775, 185);
            this.grdRevModifs.StyleInfo = resources.GetString("grdRevModifs.StyleInfo");
            this.grdRevModifs.TabIndex = 1;
            this.grdRevModifs.Tag = "28";
            this.grdRevModifs.DoubleClick += new System.EventHandler(this.grdRevModifs_DoubleClick);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 17);
            this.label2.TabIndex = 60;
            this.label2.Text = "Client:";
            // 
            // groupBox1
            // 
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
            this.groupBox1.Location = new System.Drawing.Point(5, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(533, 127);
            this.groupBox1.TabIndex = 68;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Informations";
            // 
            // txtCreatedBy
            // 
            this.txtCreatedBy.Location = new System.Drawing.Point(355, 46);
            this.txtCreatedBy.Name = "txtCreatedBy";
            this.txtCreatedBy.Size = new System.Drawing.Size(171, 20);
            this.txtCreatedBy.TabIndex = 5;
            this.txtCreatedBy.TextChanged += new System.EventHandler(this.txtCreatedBy_TextChanged);
            // 
            // txtRevisionNo
            // 
            this.txtRevisionNo.Location = new System.Drawing.Point(235, 19);
            this.txtRevisionNo.Name = "txtRevisionNo";
            this.txtRevisionNo.ReadOnly = true;
            this.txtRevisionNo.Size = new System.Drawing.Size(58, 20);
            this.txtRevisionNo.TabIndex = 1;
            this.txtRevisionNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(157, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 17);
            this.label3.TabIndex = 69;
            this.label3.Text = "No révision:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(299, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 68;
            this.label1.Text = "Date de création:";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 17);
            this.label5.TabIndex = 51;
            this.label5.Text = "Gabarit:";
            // 
            // txtVersionNo
            // 
            this.txtVersionNo.Location = new System.Drawing.Point(68, 19);
            this.txtVersionNo.Name = "txtVersionNo";
            this.txtVersionNo.ReadOnly = true;
            this.txtVersionNo.Size = new System.Drawing.Size(83, 20);
            this.txtVersionNo.TabIndex = 0;
            this.txtVersionNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cboTemplates
            // 
            this.cboTemplates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTemplates.FormattingEnabled = true;
            this.cboTemplates.Location = new System.Drawing.Point(68, 75);
            this.cboTemplates.Name = "cboTemplates";
            this.cboTemplates.Size = new System.Drawing.Size(225, 21);
            this.cboTemplates.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(3, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 17);
            this.label6.TabIndex = 64;
            this.label6.Text = "No version:";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(299, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 17);
            this.label7.TabIndex = 74;
            this.label7.Text = "Créée par:";
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
            this.btnExportRevision.Location = new System.Drawing.Point(790, 541);
            this.btnExportRevision.Name = "btnExportRevision";
            this.btnExportRevision.Size = new System.Drawing.Size(40, 40);
            this.btnExportRevision.TabIndex = 8;
            this.toolTips.SetToolTip(this.btnExportRevision, "Exporter la révision");
            this.btnExportRevision.UseVisualStyleBackColor = true;
            this.btnExportRevision.Click += new System.EventHandler(this.btnExportRevision_Click);
            // 
            // btnSelectScriptsFilePath
            // 
            this.btnSelectScriptsFilePath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSelectScriptsFilePath.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectScriptsFilePath.Image")));
            this.btnSelectScriptsFilePath.Location = new System.Drawing.Point(8, 21);
            this.btnSelectScriptsFilePath.Name = "btnSelectScriptsFilePath";
            this.btnSelectScriptsFilePath.Size = new System.Drawing.Size(30, 30);
            this.btnSelectScriptsFilePath.TabIndex = 66;
            this.toolTips.SetToolTip(this.btnSelectScriptsFilePath, "Sélectionner un script");
            this.btnSelectScriptsFilePath.UseVisualStyleBackColor = true;
            this.btnSelectScriptsFilePath.Click += new System.EventHandler(this.btnSelectScriptsFilePath_Click);
            // 
            // btnSelectScriptsFolderPath
            // 
            this.btnSelectScriptsFolderPath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSelectScriptsFolderPath.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectScriptsFolderPath.Image")));
            this.btnSelectScriptsFolderPath.Location = new System.Drawing.Point(44, 21);
            this.btnSelectScriptsFolderPath.Name = "btnSelectScriptsFolderPath";
            this.btnSelectScriptsFolderPath.Size = new System.Drawing.Size(30, 30);
            this.btnSelectScriptsFolderPath.TabIndex = 67;
            this.toolTips.SetToolTip(this.btnSelectScriptsFolderPath, "Sélectionner un dossier de scripts");
            this.btnSelectScriptsFolderPath.UseVisualStyleBackColor = true;
            this.btnSelectScriptsFolderPath.Click += new System.EventHandler(this.btnSelectScriptsFolderPath_Click);
            // 
            // btnShowScriptsFolder
            // 
            this.btnShowScriptsFolder.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnShowScriptsFolder.BackgroundImage")));
            this.btnShowScriptsFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnShowScriptsFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowScriptsFolder.Location = new System.Drawing.Point(780, 14);
            this.btnShowScriptsFolder.Name = "btnShowScriptsFolder";
            this.btnShowScriptsFolder.Size = new System.Drawing.Size(40, 40);
            this.btnShowScriptsFolder.TabIndex = 68;
            this.toolTips.SetToolTip(this.btnShowScriptsFolder, "Consulter les scripts");
            this.btnShowScriptsFolder.UseVisualStyleBackColor = true;
            this.btnShowScriptsFolder.Click += new System.EventHandler(this.btnShowScriptsFolder_Click);
            // 
            // btnSelectExecutableFilePath
            // 
            this.btnSelectExecutableFilePath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSelectExecutableFilePath.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectExecutableFilePath.Image")));
            this.btnSelectExecutableFilePath.Location = new System.Drawing.Point(8, 21);
            this.btnSelectExecutableFilePath.Name = "btnSelectExecutableFilePath";
            this.btnSelectExecutableFilePath.Size = new System.Drawing.Size(30, 30);
            this.btnSelectExecutableFilePath.TabIndex = 74;
            this.toolTips.SetToolTip(this.btnSelectExecutableFilePath, "Sélectionner un fichier exécutable");
            this.btnSelectExecutableFilePath.UseVisualStyleBackColor = true;
            this.btnSelectExecutableFilePath.Click += new System.EventHandler(this.btnSelectExecutableFilePath_Click);
            // 
            // btnSelectExecutableFolderPath
            // 
            this.btnSelectExecutableFolderPath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSelectExecutableFolderPath.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectExecutableFolderPath.Image")));
            this.btnSelectExecutableFolderPath.Location = new System.Drawing.Point(44, 21);
            this.btnSelectExecutableFolderPath.Name = "btnSelectExecutableFolderPath";
            this.btnSelectExecutableFolderPath.Size = new System.Drawing.Size(30, 30);
            this.btnSelectExecutableFolderPath.TabIndex = 75;
            this.toolTips.SetToolTip(this.btnSelectExecutableFolderPath, "Sélectionner un dossier de Release");
            this.btnSelectExecutableFolderPath.UseVisualStyleBackColor = true;
            this.btnSelectExecutableFolderPath.Click += new System.EventHandler(this.btnSelectExecutableFolderPath_Click);
            // 
            // btnShowExecutableFolder
            // 
            this.btnShowExecutableFolder.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnShowExecutableFolder.BackgroundImage")));
            this.btnShowExecutableFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnShowExecutableFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowExecutableFolder.Location = new System.Drawing.Point(780, 16);
            this.btnShowExecutableFolder.Name = "btnShowExecutableFolder";
            this.btnShowExecutableFolder.Size = new System.Drawing.Size(40, 40);
            this.btnShowExecutableFolder.TabIndex = 76;
            this.toolTips.SetToolTip(this.btnShowExecutableFolder, "Consulter l\'exécutable");
            this.btnShowExecutableFolder.UseVisualStyleBackColor = true;
            this.btnShowExecutableFolder.Click += new System.EventHandler(this.btnShowExecutableFolder_Click);
            // 
            // btnSelectVariousFilePath
            // 
            this.btnSelectVariousFilePath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSelectVariousFilePath.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectVariousFilePath.Image")));
            this.btnSelectVariousFilePath.Location = new System.Drawing.Point(103, 16);
            this.btnSelectVariousFilePath.Name = "btnSelectVariousFilePath";
            this.btnSelectVariousFilePath.Size = new System.Drawing.Size(30, 30);
            this.btnSelectVariousFilePath.TabIndex = 75;
            this.toolTips.SetToolTip(this.btnSelectVariousFilePath, "Permet de copier un fichier quelconque dans le dossier racine de la version");
            this.btnSelectVariousFilePath.UseVisualStyleBackColor = true;
            this.btnSelectVariousFilePath.Click += new System.EventHandler(this.btnSelectVariousFilePath_Click);
            // 
            // btnSelectVariousFolderPath
            // 
            this.btnSelectVariousFolderPath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSelectVariousFolderPath.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectVariousFolderPath.Image")));
            this.btnSelectVariousFolderPath.Location = new System.Drawing.Point(263, 15);
            this.btnSelectVariousFolderPath.Name = "btnSelectVariousFolderPath";
            this.btnSelectVariousFolderPath.Size = new System.Drawing.Size(30, 30);
            this.btnSelectVariousFolderPath.TabIndex = 76;
            this.toolTips.SetToolTip(this.btnSelectVariousFolderPath, "Permet de copier un dossier quelconque dans le dossier racine de la version");
            this.btnSelectVariousFolderPath.UseVisualStyleBackColor = true;
            this.btnSelectVariousFolderPath.Click += new System.EventHandler(this.btnSelectVariousFolderPath_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGenerate.BackgroundImage")));
            this.btnGenerate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGenerate.Location = new System.Drawing.Point(741, 541);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(40, 40);
            this.btnGenerate.TabIndex = 81;
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
            this.btnShowRootFolder.Location = new System.Drawing.Point(693, 541);
            this.btnShowRootFolder.Name = "btnShowRootFolder";
            this.btnShowRootFolder.Size = new System.Drawing.Size(40, 40);
            this.btnShowRootFolder.TabIndex = 84;
            this.toolTips.SetToolTip(this.btnShowRootFolder, "Accéder à la racine de la révision");
            this.btnShowRootFolder.UseVisualStyleBackColor = true;
            this.btnShowRootFolder.Click += new System.EventHandler(this.btnShowRootFolder_Click);
            // 
            // chkAddScripts
            // 
            this.chkAddScripts.AutoSize = true;
            this.chkAddScripts.Location = new System.Drawing.Point(9, 59);
            this.chkAddScripts.Name = "chkAddScripts";
            this.chkAddScripts.Size = new System.Drawing.Size(221, 17);
            this.chkAddScripts.TabIndex = 70;
            this.chkAddScripts.Text = "Ajouter le(s) script(s) au répertoire courant";
            this.toolTips.SetToolTip(this.chkAddScripts, "Le(s) script(s) du dossier, sélectionné(s) seront ajoutés à la suite des scripts " +
        "existant. Sinon ceux existant sont supprimés.");
            this.chkAddScripts.UseVisualStyleBackColor = true;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.grdSatellites);
            this.groupBox4.Location = new System.Drawing.Point(544, 5);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(289, 127);
            this.groupBox4.TabIndex = 77;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "La révision est pour l\'application sattellite suivante:";
            // 
            // grdSatellites
            // 
            this.grdSatellites.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdSatellites.AutoSearch = C1.Win.C1FlexGrid.AutoSearchEnum.FromTop;
            this.grdSatellites.ColumnInfo = "2,1,0,0,0,90,Columns:0{Width:5;}\t";
            this.grdSatellites.ExtendLastCol = true;
            this.grdSatellites.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdSatellites.Location = new System.Drawing.Point(4, 17);
            this.grdSatellites.Name = "grdSatellites";
            this.grdSatellites.Rows.Count = 1;
            this.grdSatellites.Rows.DefaultSize = 18;
            this.grdSatellites.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
            this.grdSatellites.Size = new System.Drawing.Size(282, 107);
            this.grdSatellites.StyleInfo = resources.GetString("grdSatellites.StyleInfo");
            this.grdSatellites.TabIndex = 0;
            this.grdSatellites.Tag = "35";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.chkAddScripts);
            this.groupBox2.Controls.Add(this.btnSelectScriptsFilePath);
            this.groupBox2.Controls.Add(this.btnSelectScriptsFolderPath);
            this.groupBox2.Controls.Add(this.txtScriptsPath);
            this.groupBox2.Controls.Add(this.btnShowScriptsFolder);
            this.groupBox2.Location = new System.Drawing.Point(5, 352);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(828, 83);
            this.groupBox2.TabIndex = 78;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Scripts";
            // 
            // txtScriptsPath
            // 
            this.txtScriptsPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtScriptsPath.Location = new System.Drawing.Point(80, 26);
            this.txtScriptsPath.Name = "txtScriptsPath";
            this.txtScriptsPath.ReadOnly = true;
            this.txtScriptsPath.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtScriptsPath.Size = new System.Drawing.Size(694, 22);
            this.txtScriptsPath.TabIndex = 69;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.optExeOnly);
            this.groupBox5.Controls.Add(this.optExeAndRpt);
            this.groupBox5.Controls.Add(this.btnSelectExecutableFilePath);
            this.groupBox5.Controls.Add(this.btnSelectExecutableFolderPath);
            this.groupBox5.Controls.Add(this.txtReleasePath);
            this.groupBox5.Controls.Add(this.btnShowExecutableFolder);
            this.groupBox5.Controls.Add(this.optRptOnly);
            this.groupBox5.Location = new System.Drawing.Point(5, 441);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(828, 85);
            this.groupBox5.TabIndex = 79;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Exécutable(s) de l\'application principale";
            // 
            // optExeOnly
            // 
            this.optExeOnly.Checked = true;
            this.optExeOnly.Location = new System.Drawing.Point(8, 60);
            this.optExeOnly.Name = "optExeOnly";
            this.optExeOnly.Size = new System.Drawing.Size(143, 20);
            this.optExeOnly.TabIndex = 82;
            this.optExeOnly.TabStop = true;
            this.optExeOnly.Text = "Exécutable seulement";
            this.optExeOnly.UseVisualStyleBackColor = true;
            this.optExeOnly.CheckedChanged += new System.EventHandler(this.optExeOnly_CheckedChanged);
            // 
            // optExeAndRpt
            // 
            this.optExeAndRpt.Location = new System.Drawing.Point(302, 60);
            this.optExeAndRpt.Name = "optExeAndRpt";
            this.optExeAndRpt.Size = new System.Drawing.Size(175, 20);
            this.optExeAndRpt.TabIndex = 81;
            this.optExeAndRpt.Text = "Exécutable + Rapports externe";
            this.optExeAndRpt.UseVisualStyleBackColor = true;
            this.optExeAndRpt.CheckedChanged += new System.EventHandler(this.optExeAndRpt_CheckedChanged);
            // 
            // txtReleasePath
            // 
            this.txtReleasePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReleasePath.Location = new System.Drawing.Point(80, 25);
            this.txtReleasePath.Name = "txtReleasePath";
            this.txtReleasePath.ReadOnly = true;
            this.txtReleasePath.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtReleasePath.Size = new System.Drawing.Size(694, 22);
            this.txtReleasePath.TabIndex = 79;
            // 
            // optRptOnly
            // 
            this.optRptOnly.Location = new System.Drawing.Point(157, 60);
            this.optRptOnly.Name = "optRptOnly";
            this.optRptOnly.Size = new System.Drawing.Size(125, 20);
            this.optRptOnly.TabIndex = 80;
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
            this.groupBox6.Location = new System.Drawing.Point(5, 532);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(303, 55);
            this.groupBox6.TabIndex = 80;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Documents divers";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(157, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 17);
            this.label8.TabIndex = 78;
            this.label8.Text = "Ajouter un dossier :";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 17);
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
            this.formController.Location = new System.Drawing.Point(1, 592);
            this.formController.Name = "formController";
            this.formController.ShowButtonQuitOnly = false;
            this.formController.Size = new System.Drawing.Size(835, 33);
            this.formController.TabIndex = 9;
            this.formController.SetReadRights += new Ceritar.TT3LightDLL.Controls.ctlFormController.SetReadRightsEventHandler(this.formController_SetReadRights);
            this.formController.LoadData += new Ceritar.TT3LightDLL.Controls.ctlFormController.LoadDataEventHandler(this.formController_LoadData);
            this.formController.ValidateForm += new Ceritar.TT3LightDLL.Controls.ctlFormController.ValidateFormEventHandler(this.formController_ValidateForm);
            this.formController.SaveData += new Ceritar.TT3LightDLL.Controls.ctlFormController.SaveDataEventHandler(this.formController_SaveData);
            // 
            // frmRevision
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 626);
            this.Controls.Add(this.btnShowRootFolder);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnExportRevision);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.formController);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmRevision";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestion de révision";
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdRevModifs)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdSatellites)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
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
        private System.Windows.Forms.GroupBox groupBox4;
        public C1.Win.C1FlexGrid.C1FlexGrid grdSatellites;
        private System.Windows.Forms.Button btnExportRevision;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCreatedBy;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSelectScriptsFilePath;
        private System.Windows.Forms.Button btnSelectScriptsFolderPath;
        private System.Windows.Forms.TextBox txtScriptsPath;
        private System.Windows.Forms.Button btnShowScriptsFolder;
        private System.Windows.Forms.GroupBox groupBox5;
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

    }
}