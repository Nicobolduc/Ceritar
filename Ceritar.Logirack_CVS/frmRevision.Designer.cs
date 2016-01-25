﻿namespace Ceritar.Logirack_CVS
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
            this.txtScriptsPath = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnGrdRevDel = new System.Windows.Forms.Button();
            this.btnGrdRevAdd = new System.Windows.Forms.Button();
            this.grdRevModifs = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label2 = new System.Windows.Forms.Label();
            this.btnShowScriptsFolder = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtCreatedBy = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRevisionNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtVersionNo = new System.Windows.Forms.TextBox();
            this.cboTemplates = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.formController = new Ceritar.TT3LightDLL.Controls.ctlFormController();
            this.toolTips = new System.Windows.Forms.ToolTip(this.components);
            this.btnSelectScriptsFolderPath = new System.Windows.Forms.Button();
            this.btnSelectScriptsFilePath = new System.Windows.Forms.Button();
            this.btnSelectExecutableFilePath = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnSelectExecutableFolderPath = new System.Windows.Forms.Button();
            this.btnShowExecutableFolder = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.txtReleasePath = new System.Windows.Forms.TextBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdRevModifs)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpCreation
            // 
            this.dtpCreation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpCreation.CustomFormat = "MM-dd-yy";
            this.dtpCreation.Enabled = false;
            this.dtpCreation.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCreation.Location = new System.Drawing.Point(409, 16);
            this.dtpCreation.Name = "dtpCreation";
            this.dtpCreation.Size = new System.Drawing.Size(90, 20);
            this.dtpCreation.TabIndex = 67;
            // 
            // cboClients
            // 
            this.cboClients.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClients.FormattingEnabled = true;
            this.cboClients.Location = new System.Drawing.Point(68, 46);
            this.cboClients.Name = "cboClients";
            this.cboClients.Size = new System.Drawing.Size(203, 21);
            this.cboClients.TabIndex = 59;
            // 
            // txtScriptsPath
            // 
            this.txtScriptsPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtScriptsPath.Location = new System.Drawing.Point(83, 340);
            this.txtScriptsPath.Name = "txtScriptsPath";
            this.txtScriptsPath.ReadOnly = true;
            this.txtScriptsPath.Size = new System.Drawing.Size(608, 26);
            this.txtScriptsPath.TabIndex = 65;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.btnGrdRevDel);
            this.groupBox3.Controls.Add(this.btnGrdRevAdd);
            this.groupBox3.Controls.Add(this.grdRevModifs);
            this.groupBox3.Location = new System.Drawing.Point(5, 113);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(815, 216);
            this.groupBox3.TabIndex = 63;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Livrables / Modifications inclus";
            // 
            // btnGrdRevDel
            // 
            this.btnGrdRevDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGrdRevDel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGrdRevDel.Image = ((System.Drawing.Image)(resources.GetObject("btnGrdRevDel.Image")));
            this.btnGrdRevDel.Location = new System.Drawing.Point(774, 59);
            this.btnGrdRevDel.Name = "btnGrdRevDel";
            this.btnGrdRevDel.Size = new System.Drawing.Size(35, 35);
            this.btnGrdRevDel.TabIndex = 18;
            this.btnGrdRevDel.UseVisualStyleBackColor = true;
            // 
            // btnGrdRevAdd
            // 
            this.btnGrdRevAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGrdRevAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGrdRevAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnGrdRevAdd.Image")));
            this.btnGrdRevAdd.Location = new System.Drawing.Point(774, 18);
            this.btnGrdRevAdd.Name = "btnGrdRevAdd";
            this.btnGrdRevAdd.Size = new System.Drawing.Size(35, 35);
            this.btnGrdRevAdd.TabIndex = 17;
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
            this.grdRevModifs.Size = new System.Drawing.Size(762, 192);
            this.grdRevModifs.StyleInfo = resources.GetString("grdRevModifs.StyleInfo");
            this.grdRevModifs.TabIndex = 14;
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
            // btnShowScriptsFolder
            // 
            this.btnShowScriptsFolder.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnShowScriptsFolder.BackgroundImage")));
            this.btnShowScriptsFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnShowScriptsFolder.Location = new System.Drawing.Point(697, 334);
            this.btnShowScriptsFolder.Name = "btnShowScriptsFolder";
            this.btnShowScriptsFolder.Size = new System.Drawing.Size(40, 40);
            this.btnShowScriptsFolder.TabIndex = 64;
            this.toolTips.SetToolTip(this.btnShowScriptsFolder, "Consulter les scripts");
            this.btnShowScriptsFolder.UseVisualStyleBackColor = true;
            this.btnShowScriptsFolder.Click += new System.EventHandler(this.btnShowScriptsFolder_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCreatedBy);
            this.groupBox1.Controls.Add(this.label4);
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
            this.groupBox1.Location = new System.Drawing.Point(5, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(505, 102);
            this.groupBox1.TabIndex = 68;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Informations";
            // 
            // txtCreatedBy
            // 
            this.txtCreatedBy.Location = new System.Drawing.Point(409, 46);
            this.txtCreatedBy.Name = "txtCreatedBy";
            this.txtCreatedBy.ReadOnly = true;
            this.txtCreatedBy.Size = new System.Drawing.Size(90, 20);
            this.txtCreatedBy.TabIndex = 72;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(314, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 71;
            this.label4.Text = "Créé par:";
            // 
            // txtRevisionNo
            // 
            this.txtRevisionNo.Location = new System.Drawing.Point(213, 19);
            this.txtRevisionNo.Name = "txtRevisionNo";
            this.txtRevisionNo.ReadOnly = true;
            this.txtRevisionNo.Size = new System.Drawing.Size(58, 20);
            this.txtRevisionNo.TabIndex = 70;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(134, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 17);
            this.label3.TabIndex = 69;
            this.label3.Text = "No révision:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(314, 19);
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
            this.txtVersionNo.Size = new System.Drawing.Size(60, 20);
            this.txtVersionNo.TabIndex = 65;
            // 
            // cboTemplates
            // 
            this.cboTemplates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTemplates.FormattingEnabled = true;
            this.cboTemplates.Location = new System.Drawing.Point(68, 75);
            this.cboTemplates.Name = "cboTemplates";
            this.cboTemplates.Size = new System.Drawing.Size(203, 21);
            this.cboTemplates.TabIndex = 50;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(3, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 17);
            this.label6.TabIndex = 64;
            this.label6.Text = "No version:";
            // 
            // formController
            // 
            this.formController.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.formController.ChangeMade = false;
            this.formController.FormIsLoading = false;
            this.formController.FormMode = Ceritar.TT3LightDLL.Static_Classes.sclsConstants.DML_Mode.NO_MODE;
            this.formController.Item_NRI = 0;
            this.formController.Location = new System.Drawing.Point(1, 422);
            this.formController.Name = "formController";
            this.formController.ShowButtonQuitOnly = false;
            this.formController.Size = new System.Drawing.Size(822, 33);
            this.formController.TabIndex = 69;
            this.formController.LoadData += new Ceritar.TT3LightDLL.Controls.ctlFormController.LoadDataEventHandler(this.formController_LoadData);
            this.formController.ValidateForm += new Ceritar.TT3LightDLL.Controls.ctlFormController.ValidateFormEventHandler(this.formController_ValidateForm);
            this.formController.SaveData += new Ceritar.TT3LightDLL.Controls.ctlFormController.SaveDataEventHandler(this.formController_SaveData);
            // 
            // btnSelectScriptsFolderPath
            // 
            this.btnSelectScriptsFolderPath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSelectScriptsFolderPath.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectScriptsFolderPath.Image")));
            this.btnSelectScriptsFolderPath.Location = new System.Drawing.Point(47, 339);
            this.btnSelectScriptsFolderPath.Name = "btnSelectScriptsFolderPath";
            this.btnSelectScriptsFolderPath.Size = new System.Drawing.Size(30, 30);
            this.btnSelectScriptsFolderPath.TabIndex = 70;
            this.toolTips.SetToolTip(this.btnSelectScriptsFolderPath, "Sélectionner un dossier de scripts");
            this.btnSelectScriptsFolderPath.UseVisualStyleBackColor = true;
            this.btnSelectScriptsFolderPath.Click += new System.EventHandler(this.btnSelectScriptsFolderPath_Click);
            // 
            // btnSelectScriptsFilePath
            // 
            this.btnSelectScriptsFilePath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSelectScriptsFilePath.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectScriptsFilePath.Image")));
            this.btnSelectScriptsFilePath.Location = new System.Drawing.Point(11, 339);
            this.btnSelectScriptsFilePath.Name = "btnSelectScriptsFilePath";
            this.btnSelectScriptsFilePath.Size = new System.Drawing.Size(30, 30);
            this.btnSelectScriptsFilePath.TabIndex = 75;
            this.toolTips.SetToolTip(this.btnSelectScriptsFilePath, "Sélectionner un script");
            this.btnSelectScriptsFilePath.UseVisualStyleBackColor = true;
            this.btnSelectScriptsFilePath.Click += new System.EventHandler(this.btnSelectScriptsFilePath_Click);
            // 
            // btnSelectExecutableFilePath
            // 
            this.btnSelectExecutableFilePath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSelectExecutableFilePath.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectExecutableFilePath.Image")));
            this.btnSelectExecutableFilePath.Location = new System.Drawing.Point(11, 383);
            this.btnSelectExecutableFilePath.Name = "btnSelectExecutableFilePath";
            this.btnSelectExecutableFilePath.Size = new System.Drawing.Size(30, 30);
            this.btnSelectExecutableFilePath.TabIndex = 76;
            this.toolTips.SetToolTip(this.btnSelectExecutableFilePath, "Sélectionner un fichier exécutable");
            this.btnSelectExecutableFilePath.UseVisualStyleBackColor = true;
            this.btnSelectExecutableFilePath.Click += new System.EventHandler(this.btnSelectExecutableFilePath_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGenerate.BackgroundImage")));
            this.btnGenerate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGenerate.Location = new System.Drawing.Point(779, 378);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(40, 40);
            this.btnGenerate.TabIndex = 71;
            this.toolTips.SetToolTip(this.btnGenerate, "Mettre à jour la version");
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnSelectExecutableFolderPath
            // 
            this.btnSelectExecutableFolderPath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSelectExecutableFolderPath.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectExecutableFolderPath.Image")));
            this.btnSelectExecutableFolderPath.Location = new System.Drawing.Point(47, 383);
            this.btnSelectExecutableFolderPath.Name = "btnSelectExecutableFolderPath";
            this.btnSelectExecutableFolderPath.Size = new System.Drawing.Size(30, 30);
            this.btnSelectExecutableFolderPath.TabIndex = 74;
            this.toolTips.SetToolTip(this.btnSelectExecutableFolderPath, "Sélectionner un dossier de Release");
            this.btnSelectExecutableFolderPath.UseVisualStyleBackColor = true;
            this.btnSelectExecutableFolderPath.Click += new System.EventHandler(this.btnSelectExecutableFolderPath_Click);
            // 
            // btnShowExecutableFolder
            // 
            this.btnShowExecutableFolder.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnShowExecutableFolder.BackgroundImage")));
            this.btnShowExecutableFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnShowExecutableFolder.Location = new System.Drawing.Point(697, 378);
            this.btnShowExecutableFolder.Name = "btnShowExecutableFolder";
            this.btnShowExecutableFolder.Size = new System.Drawing.Size(40, 40);
            this.btnShowExecutableFolder.TabIndex = 72;
            this.toolTips.SetToolTip(this.btnShowExecutableFolder, "Consulter l\'exécutable");
            this.btnShowExecutableFolder.UseVisualStyleBackColor = true;
            this.btnShowExecutableFolder.Click += new System.EventHandler(this.btnShowExecutableFolder_Click);
            // 
            // txtReleasePath
            // 
            this.txtReleasePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReleasePath.Location = new System.Drawing.Point(83, 385);
            this.txtReleasePath.Name = "txtReleasePath";
            this.txtReleasePath.ReadOnly = true;
            this.txtReleasePath.Size = new System.Drawing.Size(608, 26);
            this.txtReleasePath.TabIndex = 73;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // frmRevision
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 456);
            this.Controls.Add(this.btnSelectExecutableFilePath);
            this.Controls.Add(this.btnSelectScriptsFilePath);
            this.Controls.Add(this.btnSelectExecutableFolderPath);
            this.Controls.Add(this.txtReleasePath);
            this.Controls.Add(this.btnShowExecutableFolder);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.btnSelectScriptsFolderPath);
            this.Controls.Add(this.formController);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtScriptsPath);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnShowScriptsFolder);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpCreation;
        private System.Windows.Forms.ComboBox cboClients;
        private System.Windows.Forms.TextBox txtScriptsPath;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnGrdRevDel;
        private System.Windows.Forms.Button btnGrdRevAdd;
        public C1.Win.C1FlexGrid.C1FlexGrid grdRevModifs;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnShowScriptsFolder;
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
        private System.Windows.Forms.Button btnSelectScriptsFolderPath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnSelectExecutableFolderPath;
        private System.Windows.Forms.TextBox txtReleasePath;
        private System.Windows.Forms.Button btnShowExecutableFolder;
        private System.Windows.Forms.Button btnSelectScriptsFilePath;
        private System.Windows.Forms.Button btnSelectExecutableFilePath;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TextBox txtCreatedBy;
        private System.Windows.Forms.Label label4;

    }
}