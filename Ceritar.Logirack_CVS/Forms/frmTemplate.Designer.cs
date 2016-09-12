namespace Ceritar.Logirack_CVS.Forms
{
    partial class frmTemplate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTemplate));
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblNom = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboTypes = new System.Windows.Forms.ComboBox();
            this.chkByDefault = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboApplications = new System.Windows.Forms.ComboBox();
            this.formController = new Ceritar.TT3LightDLL.Controls.ctlFormController();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnMoveRight = new System.Windows.Forms.Button();
            this.btnMoveLeft = new System.Windows.Forms.Button();
            this.AddSibbling = new System.Windows.Forms.Button();
            this.btnDeleteRow = new System.Windows.Forms.Button();
            this.btnAddChild = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboFolderType = new System.Windows.Forms.ComboBox();
            this.grdTemplate = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(83, 15);
            this.txtName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(356, 22);
            this.txtName.TabIndex = 3;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // lblNom
            // 
            this.lblNom.Location = new System.Drawing.Point(3, 18);
            this.lblNom.Name = "lblNom";
            this.lblNom.Size = new System.Drawing.Size(59, 21);
            this.lblNom.TabIndex = 2;
            this.lblNom.Text = "Nom:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 21);
            this.label2.TabIndex = 17;
            this.label2.Text = "Type:";
            // 
            // cboTypes
            // 
            this.cboTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTypes.FormattingEnabled = true;
            this.cboTypes.Location = new System.Drawing.Point(83, 47);
            this.cboTypes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboTypes.Name = "cboTypes";
            this.cboTypes.Size = new System.Drawing.Size(236, 24);
            this.cboTypes.TabIndex = 16;
            this.cboTypes.SelectedIndexChanged += new System.EventHandler(this.cboTypes_SelectedIndexChanged);
            // 
            // chkByDefault
            // 
            this.chkByDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkByDefault.AutoSize = true;
            this.chkByDefault.Location = new System.Drawing.Point(686, 17);
            this.chkByDefault.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkByDefault.Name = "chkByDefault";
            this.chkByDefault.Size = new System.Drawing.Size(86, 20);
            this.chkByDefault.TabIndex = 18;
            this.chkByDefault.Text = "Par défaut";
            this.chkByDefault.UseVisualStyleBackColor = true;
            this.chkByDefault.CheckedChanged += new System.EventHandler(this.chkByDefault_CheckedChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 21);
            this.label3.TabIndex = 22;
            this.label3.Text = "Application:";
            // 
            // cboApplications
            // 
            this.cboApplications.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboApplications.FormattingEnabled = true;
            this.cboApplications.Location = new System.Drawing.Point(83, 80);
            this.cboApplications.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboApplications.Name = "cboApplications";
            this.cboApplications.Size = new System.Drawing.Size(236, 24);
            this.cboApplications.TabIndex = 21;
            this.cboApplications.SelectedIndexChanged += new System.EventHandler(this.cboApplications_SelectedIndexChanged);
            // 
            // formController
            // 
            this.formController.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.formController.ChangeMade = false;
            this.formController.FormIsLoading = false;
            this.formController.FormMode = Ceritar.TT3LightDLL.Static_Classes.sclsConstants.DML_Mode.NO_MODE;
            this.formController.Item_NRI = 0;
            this.formController.Location = new System.Drawing.Point(3, 718);
            this.formController.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.formController.Name = "formController";
            this.formController.ShowButtonQuitOnly = false;
            this.formController.Size = new System.Drawing.Size(783, 34);
            this.formController.TabIndex = 24;
            this.formController.SetReadRights += new Ceritar.TT3LightDLL.Controls.ctlFormController.SetReadRightsEventHandler(this.formController_SetReadRights);
            this.formController.LoadData += new Ceritar.TT3LightDLL.Controls.ctlFormController.LoadDataEventHandler(this.formController_LoadData);
            this.formController.ValidateForm += new Ceritar.TT3LightDLL.Controls.ctlFormController.ValidateFormEventHandler(this.formController_ValidateForm);
            this.formController.SaveData += new Ceritar.TT3LightDLL.Controls.ctlFormController.SaveDataEventHandler(this.formController_SaveData);
            // 
            // btnMoveRight
            // 
            this.btnMoveRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveRight.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnMoveRight.BackgroundImage")));
            this.btnMoveRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMoveRight.Location = new System.Drawing.Point(738, 135);
            this.btnMoveRight.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnMoveRight.Name = "btnMoveRight";
            this.btnMoveRight.Size = new System.Drawing.Size(29, 31);
            this.btnMoveRight.TabIndex = 30;
            this.toolTip.SetToolTip(this.btnMoveRight, "Augmenter d\'un niveau");
            this.btnMoveRight.UseVisualStyleBackColor = true;
            this.btnMoveRight.Click += new System.EventHandler(this.btnMoveRight_Click);
            // 
            // btnMoveLeft
            // 
            this.btnMoveLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveLeft.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnMoveLeft.BackgroundImage")));
            this.btnMoveLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMoveLeft.Location = new System.Drawing.Point(738, 100);
            this.btnMoveLeft.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnMoveLeft.Name = "btnMoveLeft";
            this.btnMoveLeft.Size = new System.Drawing.Size(29, 31);
            this.btnMoveLeft.TabIndex = 29;
            this.toolTip.SetToolTip(this.btnMoveLeft, "Reculer d\'un niveau");
            this.btnMoveLeft.UseVisualStyleBackColor = true;
            this.btnMoveLeft.Click += new System.EventHandler(this.btnMoveLeft_Click);
            // 
            // AddSibbling
            // 
            this.AddSibbling.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddSibbling.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("AddSibbling.BackgroundImage")));
            this.AddSibbling.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.AddSibbling.Location = new System.Drawing.Point(738, 62);
            this.AddSibbling.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.AddSibbling.Name = "AddSibbling";
            this.AddSibbling.Size = new System.Drawing.Size(29, 31);
            this.AddSibbling.TabIndex = 28;
            this.toolTip.SetToolTip(this.AddSibbling, "Ajouter un noeud");
            this.AddSibbling.UseVisualStyleBackColor = true;
            this.AddSibbling.Click += new System.EventHandler(this.btnAddChild_Click);
            // 
            // btnDeleteRow
            // 
            this.btnDeleteRow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteRow.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDeleteRow.BackgroundImage")));
            this.btnDeleteRow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDeleteRow.Location = new System.Drawing.Point(738, 174);
            this.btnDeleteRow.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDeleteRow.Name = "btnDeleteRow";
            this.btnDeleteRow.Size = new System.Drawing.Size(29, 31);
            this.btnDeleteRow.TabIndex = 32;
            this.toolTip.SetToolTip(this.btnDeleteRow, "Supprimer la ligne");
            this.btnDeleteRow.UseVisualStyleBackColor = true;
            // 
            // btnAddChild
            // 
            this.btnAddChild.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddChild.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddChild.BackgroundImage")));
            this.btnAddChild.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddChild.Location = new System.Drawing.Point(738, 23);
            this.btnAddChild.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAddChild.Name = "btnAddChild";
            this.btnAddChild.Size = new System.Drawing.Size(29, 31);
            this.btnAddChild.TabIndex = 33;
            this.toolTip.SetToolTip(this.btnAddChild, "Ajouter un enfant");
            this.btnAddChild.UseVisualStyleBackColor = true;
            this.btnAddChild.Click += new System.EventHandler(this.cmdAddSibbling_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAddChild);
            this.groupBox1.Controls.Add(this.btnDeleteRow);
            this.groupBox1.Controls.Add(this.cboFolderType);
            this.groupBox1.Controls.Add(this.btnMoveRight);
            this.groupBox1.Controls.Add(this.btnMoveLeft);
            this.groupBox1.Controls.Add(this.AddSibbling);
            this.groupBox1.Controls.Add(this.grdTemplate);
            this.groupBox1.Location = new System.Drawing.Point(7, 113);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(775, 597);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Hiérarchie de dossiers";
            // 
            // cboFolderType
            // 
            this.cboFolderType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFolderType.FormattingEnabled = true;
            this.cboFolderType.Location = new System.Drawing.Point(526, 143);
            this.cboFolderType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboFolderType.Name = "cboFolderType";
            this.cboFolderType.Size = new System.Drawing.Size(140, 24);
            this.cboFolderType.TabIndex = 31;
            this.cboFolderType.SelectedIndexChanged += new System.EventHandler(this.cboFolderType_SelectedIndexChanged);
            // 
            // grdTemplate
            // 
            this.grdTemplate.AllowEditing = false;
            this.grdTemplate.AutoSearch = C1.Win.C1FlexGrid.AutoSearchEnum.FromTop;
            this.grdTemplate.ColumnInfo = "1,1,0,0,0,90,Columns:0{Width:5;}\t";
            this.grdTemplate.ExtendLastCol = true;
            this.grdTemplate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdTemplate.Location = new System.Drawing.Point(7, 23);
            this.grdTemplate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grdTemplate.Name = "grdTemplate";
            this.grdTemplate.Rows.Count = 1;
            this.grdTemplate.Rows.DefaultSize = 18;
            this.grdTemplate.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
            this.grdTemplate.Size = new System.Drawing.Size(724, 569);
            this.grdTemplate.StyleInfo = resources.GetString("grdTemplate.StyleInfo");
            this.grdTemplate.TabIndex = 24;
            this.grdTemplate.Tag = "9";
            this.grdTemplate.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.grdTemplate_AfterRowColChange);
            this.grdTemplate.DoubleClick += new System.EventHandler(this.grdTemplate_DoubleClick);
            // 
            // frmTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 754);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.formController);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboApplications);
            this.Controls.Add(this.chkByDefault);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboTypes);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblNom);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "frmTemplate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Définition de gabarit";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdTemplate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblNom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboTypes;
        private System.Windows.Forms.CheckBox chkByDefault;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboApplications;
        private TT3LightDLL.Controls.ctlFormController formController;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnMoveRight;
        private System.Windows.Forms.Button btnMoveLeft;
        private System.Windows.Forms.Button AddSibbling;
        public C1.Win.C1FlexGrid.C1FlexGrid grdTemplate;
        private System.Windows.Forms.ComboBox cboFolderType;
        private System.Windows.Forms.Button btnDeleteRow;
        private System.Windows.Forms.Button btnAddChild;
    }
}