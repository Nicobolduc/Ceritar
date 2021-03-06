﻿namespace Ceritar.Logirack_CVS.Forms
{
    partial class frmCeritarApp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCeritarApp));
            this.lblNom = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnGrdModDel = new System.Windows.Forms.Button();
            this.btnGrdModAdd = new System.Windows.Forms.Button();
            this.grdModules = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnGrdSatDel = new System.Windows.Forms.Button();
            this.btnGrdSatAdd = new System.Windows.Forms.Button();
            this.chkMasterApp = new System.Windows.Forms.CheckBox();
            this.btnShowWord = new System.Windows.Forms.Button();
            this.btnReplaceWordDOC = new System.Windows.Forms.Button();
            this.cboDomain = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.formController = new Ceritar.TT3LightDLL.Controls.ctlFormController();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkReportAppExternal = new System.Windows.Forms.CheckBox();
            this.txtReportAppExternal = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.grdSatApp = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.chkGenRevNoScript = new System.Windows.Forms.CheckBox();
            this.chkManageTTApp = new System.Windows.Forms.CheckBox();
            this.cboMasterApp = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.txtServeurDev = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBDDev = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdModules)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSatApp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNom
            // 
            this.lblNom.Location = new System.Drawing.Point(2, 8);
            this.lblNom.Name = "lblNom";
            this.lblNom.Size = new System.Drawing.Size(100, 21);
            this.lblNom.TabIndex = 0;
            this.lblNom.Text = "Nom:";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(82, 5);
            this.txtName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(563, 22);
            this.txtName.TabIndex = 0;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(2, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "Description:";
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(82, 29);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(563, 24);
            this.txtDescription.TabIndex = 1;
            this.txtDescription.TextChanged += new System.EventHandler(this.txtDescription_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnGrdModDel);
            this.groupBox1.Controls.Add(this.btnGrdModAdd);
            this.groupBox1.Controls.Add(this.grdModules);
            this.groupBox1.Location = new System.Drawing.Point(6, 587);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(639, 179);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Liste des modules";
            // 
            // btnGrdModDel
            // 
            this.btnGrdModDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGrdModDel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGrdModDel.Image = ((System.Drawing.Image)(resources.GetObject("btnGrdModDel.Image")));
            this.btnGrdModDel.Location = new System.Drawing.Point(594, 73);
            this.btnGrdModDel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnGrdModDel.Name = "btnGrdModDel";
            this.btnGrdModDel.Size = new System.Drawing.Size(41, 43);
            this.btnGrdModDel.TabIndex = 1;
            this.toolTip1.SetToolTip(this.btnGrdModDel, "Supprimer la ligne");
            this.btnGrdModDel.UseVisualStyleBackColor = true;
            this.btnGrdModDel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnGrdDel_MouseUp);
            // 
            // btnGrdModAdd
            // 
            this.btnGrdModAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGrdModAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGrdModAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnGrdModAdd.Image")));
            this.btnGrdModAdd.Location = new System.Drawing.Point(594, 22);
            this.btnGrdModAdd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnGrdModAdd.Name = "btnGrdModAdd";
            this.btnGrdModAdd.Size = new System.Drawing.Size(41, 43);
            this.btnGrdModAdd.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btnGrdModAdd, "Ajouter une ligne");
            this.btnGrdModAdd.UseVisualStyleBackColor = true;
            // 
            // grdModules
            // 
            this.grdModules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdModules.AutoSearch = C1.Win.C1FlexGrid.AutoSearchEnum.FromTop;
            this.grdModules.ColumnInfo = resources.GetString("grdModules.ColumnInfo");
            this.grdModules.ExtendLastCol = true;
            this.grdModules.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdModules.Location = new System.Drawing.Point(7, 23);
            this.grdModules.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grdModules.Name = "grdModules";
            this.grdModules.Rows.Count = 8;
            this.grdModules.Rows.DefaultSize = 18;
            this.grdModules.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
            this.grdModules.Size = new System.Drawing.Size(587, 149);
            this.grdModules.StyleInfo = resources.GetString("grdModules.StyleInfo");
            this.grdModules.TabIndex = 2;
            this.grdModules.Tag = "1";
            this.grdModules.DoubleClick += new System.EventHandler(this.grdModules_DoubleClick);
            this.grdModules.Validated += new System.EventHandler(this.grdModules_Validated);
            // 
            // btnGrdSatDel
            // 
            this.btnGrdSatDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGrdSatDel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGrdSatDel.Image = ((System.Drawing.Image)(resources.GetObject("btnGrdSatDel.Image")));
            this.btnGrdSatDel.Location = new System.Drawing.Point(594, 73);
            this.btnGrdSatDel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnGrdSatDel.Name = "btnGrdSatDel";
            this.btnGrdSatDel.Size = new System.Drawing.Size(41, 43);
            this.btnGrdSatDel.TabIndex = 1;
            this.toolTip1.SetToolTip(this.btnGrdSatDel, "Supprimer la ligne");
            this.btnGrdSatDel.UseVisualStyleBackColor = true;
            // 
            // btnGrdSatAdd
            // 
            this.btnGrdSatAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGrdSatAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGrdSatAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnGrdSatAdd.Image")));
            this.btnGrdSatAdd.Location = new System.Drawing.Point(594, 22);
            this.btnGrdSatAdd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnGrdSatAdd.Name = "btnGrdSatAdd";
            this.btnGrdSatAdd.Size = new System.Drawing.Size(41, 43);
            this.btnGrdSatAdd.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btnGrdSatAdd, "Ajouter une ligne");
            this.btnGrdSatAdd.UseVisualStyleBackColor = true;
            // 
            // chkMasterApp
            // 
            this.chkMasterApp.Checked = true;
            this.chkMasterApp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMasterApp.Location = new System.Drawing.Point(427, 111);
            this.chkMasterApp.Name = "chkMasterApp";
            this.chkMasterApp.Size = new System.Drawing.Size(134, 20);
            this.chkMasterApp.TabIndex = 4;
            this.chkMasterApp.Text = "Application maître";
            this.toolTip1.SetToolTip(this.chkMasterApp, "Applications qui gèrent la BD et ses scripts.");
            this.chkMasterApp.UseVisualStyleBackColor = true;
            this.chkMasterApp.CheckedChanged += new System.EventHandler(this.chkMasterApp_CheckedChanged);
            // 
            // btnShowWord
            // 
            this.btnShowWord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowWord.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnShowWord.BackgroundImage")));
            this.btnShowWord.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnShowWord.Location = new System.Drawing.Point(600, 146);
            this.btnShowWord.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnShowWord.Name = "btnShowWord";
            this.btnShowWord.Size = new System.Drawing.Size(47, 49);
            this.btnShowWord.TabIndex = 18;
            this.btnShowWord.Tag = "0";
            this.toolTip1.SetToolTip(this.btnShowWord, "Procédure de déploiement de version / révision");
            this.btnShowWord.UseVisualStyleBackColor = true;
            this.btnShowWord.Click += new System.EventHandler(this.btnShowWord_Click);
            // 
            // btnReplaceWordDOC
            // 
            this.btnReplaceWordDOC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReplaceWordDOC.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnReplaceWordDOC.Image = ((System.Drawing.Image)(resources.GetObject("btnReplaceWordDOC.Image")));
            this.btnReplaceWordDOC.Location = new System.Drawing.Point(561, 157);
            this.btnReplaceWordDOC.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnReplaceWordDOC.Name = "btnReplaceWordDOC";
            this.btnReplaceWordDOC.Size = new System.Drawing.Size(35, 37);
            this.btnReplaceWordDOC.TabIndex = 19;
            this.toolTip1.SetToolTip(this.btnReplaceWordDOC, "Sélectionner nouvelle procédure");
            this.btnReplaceWordDOC.UseVisualStyleBackColor = true;
            this.btnReplaceWordDOC.Click += new System.EventHandler(this.btnReplaceAppChangeDOC_Click);
            // 
            // cboDomain
            // 
            this.cboDomain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDomain.FormattingEnabled = true;
            this.cboDomain.Location = new System.Drawing.Point(82, 80);
            this.cboDomain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboDomain.Name = "cboDomain";
            this.cboDomain.Size = new System.Drawing.Size(339, 24);
            this.cboDomain.TabIndex = 2;
            this.cboDomain.SelectedIndexChanged += new System.EventHandler(this.cboDomain_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(2, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 21);
            this.label2.TabIndex = 15;
            this.label2.Text = "Domaine:";
            // 
            // formController
            // 
            this.formController.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.formController.ChangeMade = false;
            this.formController.FormIsLoading = false;
            this.formController.FormMode = Ceritar.TT3LightDLL.Static_Classes.sclsConstants.DML_Mode.CONSULT_MODE;
            this.formController.Item_NRI = 0;
            this.formController.Location = new System.Drawing.Point(-6, 775);
            this.formController.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.formController.Name = "formController";
            this.formController.ShowButtonQuitOnly = false;
            this.formController.Size = new System.Drawing.Size(653, 34);
            this.formController.TabIndex = 9;
            this.formController.SetReadRights += new Ceritar.TT3LightDLL.Controls.ctlFormController.SetReadRightsEventHandler(this.formController_SetReadRights);
            this.formController.LoadData += new Ceritar.TT3LightDLL.Controls.ctlFormController.LoadDataEventHandler(this.formController_LoadData);
            this.formController.ValidateForm += new Ceritar.TT3LightDLL.Controls.ctlFormController.ValidateFormEventHandler(this.formController_ValidateForm);
            this.formController.SaveData += new Ceritar.TT3LightDLL.Controls.ctlFormController.SaveDataEventHandler(this.formController_SaveData);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.chkReportAppExternal);
            this.groupBox2.Controls.Add(this.txtReportAppExternal);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.btnGrdSatDel);
            this.groupBox2.Controls.Add(this.btnGrdSatAdd);
            this.groupBox2.Controls.Add(this.grdSatApp);
            this.groupBox2.Location = new System.Drawing.Point(6, 190);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(639, 393);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Liste des applications satellites";
            // 
            // chkReportAppExternal
            // 
            this.chkReportAppExternal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkReportAppExternal.Location = new System.Drawing.Point(206, 363);
            this.chkReportAppExternal.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkReportAppExternal.Name = "chkReportAppExternal";
            this.chkReportAppExternal.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkReportAppExternal.Size = new System.Drawing.Size(19, 21);
            this.chkReportAppExternal.TabIndex = 3;
            this.chkReportAppExternal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkReportAppExternal.UseVisualStyleBackColor = true;
            this.chkReportAppExternal.CheckedChanged += new System.EventHandler(this.chkReportAppExternal_CheckedChanged);
            // 
            // txtReportAppExternal
            // 
            this.txtReportAppExternal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtReportAppExternal.Location = new System.Drawing.Point(231, 361);
            this.txtReportAppExternal.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtReportAppExternal.MaxLength = 75;
            this.txtReportAppExternal.Name = "txtReportAppExternal";
            this.txtReportAppExternal.ReadOnly = true;
            this.txtReportAppExternal.Size = new System.Drawing.Size(363, 22);
            this.txtReportAppExternal.TabIndex = 4;
            this.txtReportAppExternal.TextChanged += new System.EventHandler(this.txtReportAppExternal_TextChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.Location = new System.Drawing.Point(3, 364);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(214, 21);
            this.label3.TabIndex = 4;
            this.label3.Text = "Nom du Exe externe des rapports:";
            // 
            // grdSatApp
            // 
            this.grdSatApp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdSatApp.AutoSearch = C1.Win.C1FlexGrid.AutoSearchEnum.FromTop;
            this.grdSatApp.ColumnInfo = resources.GetString("grdSatApp.ColumnInfo");
            this.grdSatApp.ExtendLastCol = true;
            this.grdSatApp.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdSatApp.Location = new System.Drawing.Point(7, 23);
            this.grdSatApp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grdSatApp.Name = "grdSatApp";
            this.grdSatApp.Rows.Count = 2;
            this.grdSatApp.Rows.DefaultSize = 18;
            this.grdSatApp.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
            this.grdSatApp.Size = new System.Drawing.Size(587, 331);
            this.grdSatApp.StyleInfo = resources.GetString("grdSatApp.StyleInfo");
            this.grdSatApp.TabIndex = 2;
            this.grdSatApp.Tag = "25";
            this.grdSatApp.ValidateEdit += new C1.Win.C1FlexGrid.ValidateEditEventHandler(this.grdSatApp_ValidateEdit);
            this.grdSatApp.DoubleClick += new System.EventHandler(this.grdAppSat_DoubleClick);
            this.grdSatApp.Validated += new System.EventHandler(this.grdAppSat_Validated);
            // 
            // chkGenRevNoScript
            // 
            this.chkGenRevNoScript.Location = new System.Drawing.Point(82, 137);
            this.chkGenRevNoScript.Name = "chkGenRevNoScript";
            this.chkGenRevNoScript.Size = new System.Drawing.Size(388, 20);
            this.chkGenRevNoScript.TabIndex = 5;
            this.chkGenRevNoScript.Text = "Auto. générer le script de changement de numéro de révision";
            this.chkGenRevNoScript.UseVisualStyleBackColor = true;
            this.chkGenRevNoScript.Click += new System.EventHandler(this.chkGenRevNoScript_Click);
            // 
            // chkManageTTApp
            // 
            this.chkManageTTApp.Checked = true;
            this.chkManageTTApp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkManageTTApp.Location = new System.Drawing.Point(82, 163);
            this.chkManageTTApp.Name = "chkManageTTApp";
            this.chkManageTTApp.Size = new System.Drawing.Size(388, 20);
            this.chkManageTTApp.TabIndex = 6;
            this.chkManageTTApp.Text = "Les versions nécessitent un TTApp.mdb";
            this.chkManageTTApp.UseVisualStyleBackColor = true;
            this.chkManageTTApp.CheckedChanged += new System.EventHandler(this.chkTTAppNeeded_CheckedChanged);
            // 
            // cboMasterApp
            // 
            this.cboMasterApp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMasterApp.FormattingEnabled = true;
            this.cboMasterApp.Location = new System.Drawing.Point(82, 108);
            this.cboMasterApp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboMasterApp.Name = "cboMasterApp";
            this.cboMasterApp.Size = new System.Drawing.Size(339, 24);
            this.cboMasterApp.TabIndex = 3;
            this.cboMasterApp.SelectedIndexChanged += new System.EventHandler(this.cboMasterApp_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(2, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 21);
            this.label4.TabIndex = 17;
            this.label4.Text = "App. maître:";
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // txtServeurDev
            // 
            this.txtServeurDev.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtServeurDev.Location = new System.Drawing.Point(82, 55);
            this.txtServeurDev.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtServeurDev.Name = "txtServeurDev";
            this.txtServeurDev.Size = new System.Drawing.Size(238, 22);
            this.txtServeurDev.TabIndex = 20;
            this.txtServeurDev.TextChanged += new System.EventHandler(this.txtServeurDev_TextChanged);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(2, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 21);
            this.label5.TabIndex = 21;
            this.label5.Text = "Serveur Dev:";
            // 
            // txtBDDev
            // 
            this.txtBDDev.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBDDev.Location = new System.Drawing.Point(387, 55);
            this.txtBDDev.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtBDDev.Name = "txtBDDev";
            this.txtBDDev.Size = new System.Drawing.Size(258, 22);
            this.txtBDDev.TabIndex = 22;
            this.txtBDDev.TextChanged += new System.EventHandler(this.txtBDDev_TextChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(321, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 21);
            this.label6.TabIndex = 23;
            this.label6.Text = "BD Dev:";
            // 
            // frmCeritarApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 811);
            this.Controls.Add(this.txtBDDev);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtServeurDev);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnReplaceWordDOC);
            this.Controls.Add(this.btnShowWord);
            this.Controls.Add(this.chkMasterApp);
            this.Controls.Add(this.cboMasterApp);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chkManageTTApp);
            this.Controls.Add(this.chkGenRevNoScript);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cboDomain);
            this.Controls.Add(this.formController);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblNom);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(584, 790);
            this.Name = "frmCeritarApp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Solution logiciel de Ceritar";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdModules)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSatApp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNom;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnGrdModDel;
        private System.Windows.Forms.Button btnGrdModAdd;
        private System.Windows.Forms.ToolTip toolTip1;
        public TT3LightDLL.Controls.ctlFormController formController;
        public C1.Win.C1FlexGrid.C1FlexGrid grdModules;
        private System.Windows.Forms.ComboBox cboDomain;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnGrdSatDel;
        private System.Windows.Forms.Button btnGrdSatAdd;
        public C1.Win.C1FlexGrid.C1FlexGrid grdSatApp;
        private System.Windows.Forms.TextBox txtReportAppExternal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkReportAppExternal;
        private System.Windows.Forms.CheckBox chkGenRevNoScript;
        private System.Windows.Forms.CheckBox chkManageTTApp;
        private System.Windows.Forms.ComboBox cboMasterApp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkMasterApp;
        private System.Windows.Forms.Button btnShowWord;
        private System.Windows.Forms.Button btnReplaceWordDOC;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.TextBox txtBDDev;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtServeurDev;
        private System.Windows.Forms.Label label5;
    }
}