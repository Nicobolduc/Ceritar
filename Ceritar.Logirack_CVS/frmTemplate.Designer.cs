namespace Ceritar.Logirack_CVS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTemplate));
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblNom = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboTypes = new System.Windows.Forms.ComboBox();
            this.chkByDefault = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboApplications = new System.Windows.Forms.ComboBox();
            this.grdTemplate = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.formController = new Ceritar.TT3LightDLL.Controls.ctlFormController();
            this.btnAddNode = new System.Windows.Forms.Button();
            this.btnMoveLeft = new System.Windows.Forms.Button();
            this.btnMoveRight = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(71, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(306, 20);
            this.txtName.TabIndex = 3;
            // 
            // lblNom
            // 
            this.lblNom.Location = new System.Drawing.Point(3, 15);
            this.lblNom.Name = "lblNom";
            this.lblNom.Size = new System.Drawing.Size(51, 17);
            this.lblNom.TabIndex = 2;
            this.lblNom.Text = "Nom:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 17);
            this.label2.TabIndex = 17;
            this.label2.Text = "Type:";
            // 
            // cboTypes
            // 
            this.cboTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTypes.FormattingEnabled = true;
            this.cboTypes.Location = new System.Drawing.Point(71, 38);
            this.cboTypes.Name = "cboTypes";
            this.cboTypes.Size = new System.Drawing.Size(203, 21);
            this.cboTypes.TabIndex = 16;
            // 
            // chkByDefault
            // 
            this.chkByDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkByDefault.AutoSize = true;
            this.chkByDefault.Location = new System.Drawing.Point(420, 14);
            this.chkByDefault.Name = "chkByDefault";
            this.chkByDefault.Size = new System.Drawing.Size(75, 17);
            this.chkByDefault.TabIndex = 18;
            this.chkByDefault.Text = "Par défaut";
            this.chkByDefault.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 17);
            this.label3.TabIndex = 22;
            this.label3.Text = "Application:";
            // 
            // cboApplications
            // 
            this.cboApplications.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboApplications.FormattingEnabled = true;
            this.cboApplications.Location = new System.Drawing.Point(71, 65);
            this.cboApplications.Name = "cboApplications";
            this.cboApplications.Size = new System.Drawing.Size(203, 21);
            this.cboApplications.TabIndex = 21;
            this.cboApplications.SelectedIndexChanged += new System.EventHandler(this.cboApplications_SelectedIndexChanged);
            // 
            // grdTemplate
            // 
            this.grdTemplate.AllowEditing = false;
            this.grdTemplate.AutoSearch = C1.Win.C1FlexGrid.AutoSearchEnum.FromTop;
            this.grdTemplate.ColumnInfo = "1,1,0,0,0,90,Columns:0{Width:5;}\t";
            this.grdTemplate.ExtendLastCol = true;
            this.grdTemplate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdTemplate.Location = new System.Drawing.Point(5, 106);
            this.grdTemplate.Name = "grdTemplate";
            this.grdTemplate.Rows.Count = 1;
            this.grdTemplate.Rows.DefaultSize = 18;
            this.grdTemplate.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
            this.grdTemplate.Size = new System.Drawing.Size(464, 463);
            this.grdTemplate.StyleInfo = resources.GetString("grdTemplate.StyleInfo");
            this.grdTemplate.TabIndex = 23;
            this.grdTemplate.Tag = "9";
            this.grdTemplate.DoubleClick += new System.EventHandler(this.grdTemplate_DoubleClick);
            // 
            // formController
            // 
            this.formController.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.formController.FormIsLoading = false;
            this.formController.FormMode = Ceritar.TT3LightDLL.Static_Classes.sclsConstants.DML_Mode.NO_MODE;
            this.formController.Item_ID = 0;
            this.formController.Location = new System.Drawing.Point(3, 575);
            this.formController.Name = "formController";
            this.formController.ShowButtonQuitOnly = false;
            this.formController.Size = new System.Drawing.Size(504, 33);
            this.formController.TabIndex = 24;
            this.formController.SetReadRights += new Ceritar.TT3LightDLL.Controls.ctlFormController.SetReadRightsEventHandler(this.formController_SetReadRights);
            this.formController.LoadData += new Ceritar.TT3LightDLL.Controls.ctlFormController.LoadDataEventHandler(this.formController_LoadData);
            this.formController.SaveData += new Ceritar.TT3LightDLL.Controls.ctlFormController.SaveDataEventHandler(this.formController_SaveData);
            // 
            // btnAddNode
            // 
            this.btnAddNode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddNode.BackgroundImage")));
            this.btnAddNode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddNode.Location = new System.Drawing.Point(475, 126);
            this.btnAddNode.Name = "btnAddNode";
            this.btnAddNode.Size = new System.Drawing.Size(25, 25);
            this.btnAddNode.TabIndex = 25;
            this.btnAddNode.UseVisualStyleBackColor = true;
            this.btnAddNode.Click += new System.EventHandler(this.btnAddNode_Click);
            // 
            // btnMoveLeft
            // 
            this.btnMoveLeft.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnMoveLeft.BackgroundImage")));
            this.btnMoveLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMoveLeft.Location = new System.Drawing.Point(475, 155);
            this.btnMoveLeft.Name = "btnMoveLeft";
            this.btnMoveLeft.Size = new System.Drawing.Size(25, 25);
            this.btnMoveLeft.TabIndex = 26;
            this.btnMoveLeft.UseVisualStyleBackColor = true;
            // 
            // btnMoveRight
            // 
            this.btnMoveRight.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnMoveRight.BackgroundImage")));
            this.btnMoveRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMoveRight.Location = new System.Drawing.Point(475, 184);
            this.btnMoveRight.Name = "btnMoveRight";
            this.btnMoveRight.Size = new System.Drawing.Size(25, 25);
            this.btnMoveRight.TabIndex = 27;
            this.btnMoveRight.UseVisualStyleBackColor = true;
            // 
            // frmTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 609);
            this.Controls.Add(this.btnMoveRight);
            this.Controls.Add(this.btnMoveLeft);
            this.Controls.Add(this.btnAddNode);
            this.Controls.Add(this.formController);
            this.Controls.Add(this.grdTemplate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboApplications);
            this.Controls.Add(this.chkByDefault);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboTypes);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblNom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmTemplate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Définition de gabarit";
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
        public C1.Win.C1FlexGrid.C1FlexGrid grdTemplate;
        private TT3LightDLL.Controls.ctlFormController formController;
        private System.Windows.Forms.Button btnAddNode;
        private System.Windows.Forms.Button btnMoveLeft;
        private System.Windows.Forms.Button btnMoveRight;
    }
}