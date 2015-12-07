namespace Ceritar.Logirack_CVS
{
    partial class frmVersionRevision
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVersionRevision));
            this.tab = new System.Windows.Forms.TabControl();
            this.tabVersion = new System.Windows.Forms.TabPage();
            this.tabRevision = new System.Windows.Forms.TabPage();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.formController = new Ceritar.TT3LightDLL.Controls.ctlFormController();
            this.btnCreate = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboGabarits = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCompiledBy = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboApplications = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grdClients = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label2 = new System.Windows.Forms.Label();
            this.cboClients = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cboVersions = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.grdRevModifs = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.btnAccessPath = new System.Windows.Forms.Button();
            this.btnWordPath = new System.Windows.Forms.Button();
            this.btnExcelPath = new System.Windows.Forms.Button();
            this.btnExePath = new System.Windows.Forms.Button();
            this.btnScriptsPath = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.dtpBuild = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.txtExecutablePath = new System.Windows.Forms.TextBox();
            this.btnGrdClientsDel = new System.Windows.Forms.Button();
            this.btnGrdClientsAdd = new System.Windows.Forms.Button();
            this.txtExcelAppChangePath = new System.Windows.Forms.TextBox();
            this.txtWordAppChangePath = new System.Windows.Forms.TextBox();
            this.txtTTAppPath = new System.Windows.Forms.TextBox();
            this.txtScriptsPath = new System.Windows.Forms.TextBox();
            this.cmdGrdRevDel = new System.Windows.Forms.Button();
            this.cmdGrdRevAdd = new System.Windows.Forms.Button();
            this.tab.SuspendLayout();
            this.tabVersion.SuspendLayout();
            this.tabRevision.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdClients)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdRevModifs)).BeginInit();
            this.SuspendLayout();
            // 
            // tab
            // 
            this.tab.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tab.Controls.Add(this.tabVersion);
            this.tab.Controls.Add(this.tabRevision);
            this.tab.Location = new System.Drawing.Point(5, 142);
            this.tab.Name = "tab";
            this.tab.SelectedIndex = 0;
            this.tab.Size = new System.Drawing.Size(807, 329);
            this.tab.TabIndex = 0;
            // 
            // tabVersion
            // 
            this.tabVersion.Controls.Add(this.txtTTAppPath);
            this.tabVersion.Controls.Add(this.txtWordAppChangePath);
            this.tabVersion.Controls.Add(this.txtExcelAppChangePath);
            this.tabVersion.Controls.Add(this.btnAccessPath);
            this.tabVersion.Controls.Add(this.btnWordPath);
            this.tabVersion.Controls.Add(this.btnExcelPath);
            this.tabVersion.Controls.Add(this.groupBox2);
            this.tabVersion.Location = new System.Drawing.Point(4, 22);
            this.tabVersion.Name = "tabVersion";
            this.tabVersion.Padding = new System.Windows.Forms.Padding(3);
            this.tabVersion.Size = new System.Drawing.Size(799, 303);
            this.tabVersion.TabIndex = 0;
            this.tabVersion.Text = "Nouvelle version";
            this.tabVersion.UseVisualStyleBackColor = true;
            // 
            // tabRevision
            // 
            this.tabRevision.Controls.Add(this.cboVersions);
            this.tabRevision.Controls.Add(this.cboClients);
            this.tabRevision.Controls.Add(this.txtScriptsPath);
            this.tabRevision.Controls.Add(this.btnScriptsPath);
            this.tabRevision.Controls.Add(this.groupBox3);
            this.tabRevision.Controls.Add(this.label6);
            this.tabRevision.Controls.Add(this.label2);
            this.tabRevision.Location = new System.Drawing.Point(4, 22);
            this.tabRevision.Name = "tabRevision";
            this.tabRevision.Padding = new System.Windows.Forms.Padding(3);
            this.tabRevision.Size = new System.Drawing.Size(799, 303);
            this.tabRevision.TabIndex = 1;
            this.tabRevision.Text = "Nouvelle révision";
            this.tabRevision.UseVisualStyleBackColor = true;
            // 
            // formController
            // 
            this.formController.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.formController.FormIsLoading = false;
            this.formController.FormMode = Ceritar.TT3LightDLL.Static_Classes.sclsConstants.DML_Mode.CONSULT_MODE;
            this.formController.Item_ID = 0;
            this.formController.Location = new System.Drawing.Point(732, 481);
            this.formController.Name = "formController";
            this.formController.ShowButtonQuitOnly = true;
            this.formController.Size = new System.Drawing.Size(80, 33);
            this.formController.TabIndex = 1;
            this.formController.LoadData += new Ceritar.TT3LightDLL.Controls.ctlFormController.LoadDataEventHandler(this.LoadDataEventHandler);
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCreate.BackgroundImage")));
            this.btnCreate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCreate.Location = new System.Drawing.Point(677, 473);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(40, 40);
            this.btnCreate.TabIndex = 36;
            this.toolTip.SetToolTip(this.btnCreate, "Créer la version/révision");
            this.btnCreate.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtExecutablePath);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.dtpBuild);
            this.groupBox1.Controls.Add(this.btnExePath);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cboGabarits);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtNumber);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtCompiledBy);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cboApplications);
            this.groupBox1.Location = new System.Drawing.Point(5, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(804, 131);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Informations";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(6, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 17);
            this.label5.TabIndex = 49;
            this.label5.Text = "Gabarit:";
            // 
            // cboGabarits
            // 
            this.cboGabarits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGabarits.FormattingEnabled = true;
            this.cboGabarits.Location = new System.Drawing.Point(74, 46);
            this.cboGabarits.Name = "cboGabarits";
            this.cboGabarits.Size = new System.Drawing.Size(203, 21);
            this.cboGabarits.TabIndex = 48;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(294, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 17);
            this.label4.TabIndex = 44;
            this.label4.Text = "Numéro:";
            // 
            // txtNumber
            // 
            this.txtNumber.Location = new System.Drawing.Point(380, 46);
            this.txtNumber.MaxLength = 10;
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(66, 20);
            this.txtNumber.TabIndex = 43;
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
            this.txtCompiledBy.Location = new System.Drawing.Point(380, 19);
            this.txtCompiledBy.Name = "txtCompiledBy";
            this.txtCompiledBy.Size = new System.Drawing.Size(182, 20);
            this.txtCompiledBy.TabIndex = 41;
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
            this.cboApplications.Size = new System.Drawing.Size(203, 21);
            this.cboApplications.TabIndex = 39;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnGrdClientsDel);
            this.groupBox2.Controls.Add(this.btnGrdClientsAdd);
            this.groupBox2.Controls.Add(this.grdClients);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(414, 154);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Clients qui installerons la version";
            // 
            // grdClients
            // 
            this.grdClients.AutoSearch = C1.Win.C1FlexGrid.AutoSearchEnum.FromTop;
            this.grdClients.ColumnInfo = resources.GetString("grdClients.ColumnInfo");
            this.grdClients.ExtendLastCol = true;
            this.grdClients.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdClients.Location = new System.Drawing.Point(6, 21);
            this.grdClients.Name = "grdClients";
            this.grdClients.Rows.Count = 6;
            this.grdClients.Rows.DefaultSize = 18;
            this.grdClients.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
            this.grdClients.Size = new System.Drawing.Size(358, 129);
            this.grdClients.StyleInfo = resources.GetString("grdClients.StyleInfo");
            this.grdClients.TabIndex = 14;
            this.grdClients.Tag = "1";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 17);
            this.label2.TabIndex = 40;
            this.label2.Text = "Client:";
            // 
            // cboClients
            // 
            this.cboClients.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClients.FormattingEnabled = true;
            this.cboClients.Location = new System.Drawing.Point(67, 7);
            this.cboClients.Name = "cboClients";
            this.cboClients.Size = new System.Drawing.Size(203, 21);
            this.cboClients.TabIndex = 39;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(6, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 17);
            this.label6.TabIndex = 42;
            this.label6.Text = "Version:";
            // 
            // cboVersions
            // 
            this.cboVersions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVersions.FormattingEnabled = true;
            this.cboVersions.Location = new System.Drawing.Point(67, 34);
            this.cboVersions.Name = "cboVersions";
            this.cboVersions.Size = new System.Drawing.Size(203, 21);
            this.cboVersions.TabIndex = 41;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmdGrdRevDel);
            this.groupBox3.Controls.Add(this.cmdGrdRevAdd);
            this.groupBox3.Controls.Add(this.grdRevModifs);
            this.groupBox3.Location = new System.Drawing.Point(6, 61);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(787, 189);
            this.groupBox3.TabIndex = 43;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Livrables / Modifications inclus";
            // 
            // grdRevModifs
            // 
            this.grdRevModifs.AutoSearch = C1.Win.C1FlexGrid.AutoSearchEnum.FromTop;
            this.grdRevModifs.ColumnInfo = resources.GetString("grdRevModifs.ColumnInfo");
            this.grdRevModifs.ExtendLastCol = true;
            this.grdRevModifs.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdRevModifs.Location = new System.Drawing.Point(6, 18);
            this.grdRevModifs.Name = "grdRevModifs";
            this.grdRevModifs.Rows.Count = 10;
            this.grdRevModifs.Rows.DefaultSize = 18;
            this.grdRevModifs.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
            this.grdRevModifs.Size = new System.Drawing.Size(734, 166);
            this.grdRevModifs.StyleInfo = resources.GetString("grdRevModifs.StyleInfo");
            this.grdRevModifs.TabIndex = 14;
            this.grdRevModifs.Tag = "1";
            // 
            // btnAccessPath
            // 
            this.btnAccessPath.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAccessPath.BackgroundImage")));
            this.btnAccessPath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAccessPath.Location = new System.Drawing.Point(5, 258);
            this.btnAccessPath.Name = "btnAccessPath";
            this.btnAccessPath.Size = new System.Drawing.Size(40, 40);
            this.btnAccessPath.TabIndex = 50;
            this.btnAccessPath.UseVisualStyleBackColor = true;
            // 
            // btnWordPath
            // 
            this.btnWordPath.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnWordPath.BackgroundImage")));
            this.btnWordPath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnWordPath.Location = new System.Drawing.Point(5, 212);
            this.btnWordPath.Name = "btnWordPath";
            this.btnWordPath.Size = new System.Drawing.Size(40, 40);
            this.btnWordPath.TabIndex = 49;
            this.btnWordPath.UseVisualStyleBackColor = true;
            // 
            // btnExcelPath
            // 
            this.btnExcelPath.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExcelPath.BackgroundImage")));
            this.btnExcelPath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnExcelPath.Location = new System.Drawing.Point(5, 166);
            this.btnExcelPath.Name = "btnExcelPath";
            this.btnExcelPath.Size = new System.Drawing.Size(40, 40);
            this.btnExcelPath.TabIndex = 48;
            this.btnExcelPath.UseVisualStyleBackColor = true;
            // 
            // btnExePath
            // 
            this.btnExePath.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExePath.BackgroundImage")));
            this.btnExePath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnExePath.Location = new System.Drawing.Point(9, 81);
            this.btnExePath.Name = "btnExePath";
            this.btnExePath.Size = new System.Drawing.Size(40, 40);
            this.btnExePath.TabIndex = 52;
            this.toolTip.SetToolTip(this.btnExePath, "Chemin vers l\'exécutable");
            this.btnExePath.UseVisualStyleBackColor = true;
            // 
            // btnScriptsPath
            // 
            this.btnScriptsPath.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnScriptsPath.BackgroundImage")));
            this.btnScriptsPath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnScriptsPath.Location = new System.Drawing.Point(6, 256);
            this.btnScriptsPath.Name = "btnScriptsPath";
            this.btnScriptsPath.Size = new System.Drawing.Size(40, 40);
            this.btnScriptsPath.TabIndex = 54;
            this.btnScriptsPath.UseVisualStyleBackColor = true;
            // 
            // dtpBuild
            // 
            this.dtpBuild.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpBuild.Location = new System.Drawing.Point(668, 19);
            this.dtpBuild.Name = "dtpBuild";
            this.dtpBuild.Size = new System.Drawing.Size(130, 20);
            this.dtpBuild.TabIndex = 53;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.Location = new System.Drawing.Point(665, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 17);
            this.label7.TabIndex = 54;
            this.label7.Text = "Current user";
            // 
            // txtExecutablePath
            // 
            this.txtExecutablePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExecutablePath.Location = new System.Drawing.Point(74, 89);
            this.txtExecutablePath.Name = "txtExecutablePath";
            this.txtExecutablePath.ReadOnly = true;
            this.txtExecutablePath.Size = new System.Drawing.Size(488, 26);
            this.txtExecutablePath.TabIndex = 55;
            // 
            // btnGrdClientsDel
            // 
            this.btnGrdClientsDel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGrdClientsDel.Image = ((System.Drawing.Image)(resources.GetObject("btnGrdClientsDel.Image")));
            this.btnGrdClientsDel.Location = new System.Drawing.Point(370, 62);
            this.btnGrdClientsDel.Name = "btnGrdClientsDel";
            this.btnGrdClientsDel.Size = new System.Drawing.Size(35, 35);
            this.btnGrdClientsDel.TabIndex = 16;
            this.btnGrdClientsDel.UseVisualStyleBackColor = true;
            // 
            // btnGrdClientsAdd
            // 
            this.btnGrdClientsAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGrdClientsAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnGrdClientsAdd.Image")));
            this.btnGrdClientsAdd.Location = new System.Drawing.Point(370, 21);
            this.btnGrdClientsAdd.Name = "btnGrdClientsAdd";
            this.btnGrdClientsAdd.Size = new System.Drawing.Size(35, 35);
            this.btnGrdClientsAdd.TabIndex = 15;
            this.btnGrdClientsAdd.UseVisualStyleBackColor = true;
            // 
            // txtExcelAppChangePath
            // 
            this.txtExcelAppChangePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExcelAppChangePath.Location = new System.Drawing.Point(51, 172);
            this.txtExcelAppChangePath.Name = "txtExcelAppChangePath";
            this.txtExcelAppChangePath.ReadOnly = true;
            this.txtExcelAppChangePath.Size = new System.Drawing.Size(507, 26);
            this.txtExcelAppChangePath.TabIndex = 56;
            // 
            // txtWordAppChangePath
            // 
            this.txtWordAppChangePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWordAppChangePath.Location = new System.Drawing.Point(51, 218);
            this.txtWordAppChangePath.Name = "txtWordAppChangePath";
            this.txtWordAppChangePath.ReadOnly = true;
            this.txtWordAppChangePath.Size = new System.Drawing.Size(507, 26);
            this.txtWordAppChangePath.TabIndex = 57;
            // 
            // txtTTAppPath
            // 
            this.txtTTAppPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTTAppPath.Location = new System.Drawing.Point(51, 264);
            this.txtTTAppPath.Name = "txtTTAppPath";
            this.txtTTAppPath.ReadOnly = true;
            this.txtTTAppPath.Size = new System.Drawing.Size(507, 26);
            this.txtTTAppPath.TabIndex = 58;
            // 
            // txtScriptsPath
            // 
            this.txtScriptsPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtScriptsPath.Location = new System.Drawing.Point(52, 262);
            this.txtScriptsPath.Name = "txtScriptsPath";
            this.txtScriptsPath.ReadOnly = true;
            this.txtScriptsPath.Size = new System.Drawing.Size(488, 26);
            this.txtScriptsPath.TabIndex = 56;
            // 
            // cmdGrdRevDel
            // 
            this.cmdGrdRevDel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmdGrdRevDel.Image = ((System.Drawing.Image)(resources.GetObject("cmdGrdRevDel.Image")));
            this.cmdGrdRevDel.Location = new System.Drawing.Point(746, 59);
            this.cmdGrdRevDel.Name = "cmdGrdRevDel";
            this.cmdGrdRevDel.Size = new System.Drawing.Size(35, 35);
            this.cmdGrdRevDel.TabIndex = 18;
            this.cmdGrdRevDel.UseVisualStyleBackColor = true;
            // 
            // cmdGrdRevAdd
            // 
            this.cmdGrdRevAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cmdGrdRevAdd.Image = ((System.Drawing.Image)(resources.GetObject("cmdGrdRevAdd.Image")));
            this.cmdGrdRevAdd.Location = new System.Drawing.Point(746, 18);
            this.cmdGrdRevAdd.Name = "cmdGrdRevAdd";
            this.cmdGrdRevAdd.Size = new System.Drawing.Size(35, 35);
            this.cmdGrdRevAdd.TabIndex = 17;
            this.cmdGrdRevAdd.UseVisualStyleBackColor = true;
            // 
            // frmVersionRevision
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 515);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.formController);
            this.Controls.Add(this.tab);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmVersionRevision";
            this.Text = "Créer une version ou revision";
            this.tab.ResumeLayout(false);
            this.tabVersion.ResumeLayout(false);
            this.tabVersion.PerformLayout();
            this.tabRevision.ResumeLayout(false);
            this.tabRevision.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdClients)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdRevModifs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tab;
        private System.Windows.Forms.TabPage tabVersion;
        private System.Windows.Forms.TabPage tabRevision;
        public TT3LightDLL.Controls.ctlFormController formController;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboGabarits;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCompiledBy;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboApplications;
        private System.Windows.Forms.GroupBox groupBox2;
        public C1.Win.C1FlexGrid.C1FlexGrid grdClients;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboVersions;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboClients;
        private System.Windows.Forms.GroupBox groupBox3;
        public C1.Win.C1FlexGrid.C1FlexGrid grdRevModifs;
        private System.Windows.Forms.Button btnAccessPath;
        private System.Windows.Forms.Button btnWordPath;
        private System.Windows.Forms.Button btnExcelPath;
        private System.Windows.Forms.Button btnScriptsPath;
        private System.Windows.Forms.Button btnExePath;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.DateTimePicker dtpBuild;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtExecutablePath;
        private System.Windows.Forms.Button btnGrdClientsDel;
        private System.Windows.Forms.Button btnGrdClientsAdd;
        private System.Windows.Forms.TextBox txtTTAppPath;
        private System.Windows.Forms.TextBox txtWordAppChangePath;
        private System.Windows.Forms.TextBox txtExcelAppChangePath;
        private System.Windows.Forms.TextBox txtScriptsPath;
        private System.Windows.Forms.Button cmdGrdRevDel;
        private System.Windows.Forms.Button cmdGrdRevAdd;
    }
}