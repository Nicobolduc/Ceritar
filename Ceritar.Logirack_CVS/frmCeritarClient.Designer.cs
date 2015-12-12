namespace Ceritar.Logirack_CVS
{
    partial class frmCeritarClient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCeritarClient));
            this.formController = new Ceritar.TT3LightDLL.Controls.ctlFormController();
            this.lblNom = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnGrdDel = new System.Windows.Forms.Button();
            this.btnGrdAdd = new System.Windows.Forms.Button();
            this.grdApp = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.cboApp = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdApp)).BeginInit();
            this.SuspendLayout();
            // 
            // formController
            // 
            this.formController.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.formController.FormIsLoading = false;
            this.formController.FormMode = Ceritar.TT3LightDLL.Static_Classes.sclsConstants.DML_Mode.NO_MODE;
            this.formController.Item_ID = 0;
            this.formController.Location = new System.Drawing.Point(5, 307);
            this.formController.Name = "formController";
            this.formController.ShowButtonQuitOnly = false;
            this.formController.Size = new System.Drawing.Size(502, 33);
            this.formController.TabIndex = 4;
            this.formController.LoadData += new Ceritar.TT3LightDLL.Controls.ctlFormController.LoadDataEventHandler(this.ctlFormController1_LoadData);
            // 
            // lblNom
            // 
            this.lblNom.AutoSize = true;
            this.lblNom.Location = new System.Drawing.Point(5, 13);
            this.lblNom.Name = "lblNom";
            this.lblNom.Size = new System.Drawing.Size(45, 17);
            this.lblNom.TabIndex = 5;
            this.lblNom.Text = "Nom :";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(57, 10);
            this.txtName.Margin = new System.Windows.Forms.Padding(4);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(342, 22);
            this.txtName.TabIndex = 6;
            this.txtName.TextChanged += new System.EventHandler(this.txtNom_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboApp);
            this.groupBox1.Controls.Add(this.btnGrdDel);
            this.groupBox1.Controls.Add(this.btnGrdAdd);
            this.groupBox1.Controls.Add(this.grdApp);
            this.groupBox1.Location = new System.Drawing.Point(7, 65);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(495, 214);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Liste des modules";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // btnGrdDel
            // 
            this.btnGrdDel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGrdDel.Image = ((System.Drawing.Image)(resources.GetObject("btnGrdDel.Image")));
            this.btnGrdDel.Location = new System.Drawing.Point(444, 74);
            this.btnGrdDel.Margin = new System.Windows.Forms.Padding(4);
            this.btnGrdDel.Name = "btnGrdDel";
            this.btnGrdDel.Size = new System.Drawing.Size(47, 43);
            this.btnGrdDel.TabIndex = 1;
            this.btnGrdDel.UseVisualStyleBackColor = true;
            // 
            // btnGrdAdd
            // 
            this.btnGrdAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGrdAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnGrdAdd.Image")));
            this.btnGrdAdd.Location = new System.Drawing.Point(444, 23);
            this.btnGrdAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnGrdAdd.Name = "btnGrdAdd";
            this.btnGrdAdd.Size = new System.Drawing.Size(47, 43);
            this.btnGrdAdd.TabIndex = 0;
            this.btnGrdAdd.UseVisualStyleBackColor = true;
            // 
            // grdApp
            // 
            this.grdApp.AutoSearch = C1.Win.C1FlexGrid.AutoSearchEnum.FromTop;
            this.grdApp.ColumnInfo = resources.GetString("grdApp.ColumnInfo");
            this.grdApp.ExtendLastCol = true;
            this.grdApp.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdApp.Location = new System.Drawing.Point(8, 23);
            this.grdApp.Margin = new System.Windows.Forms.Padding(4);
            this.grdApp.Name = "grdApp";
            this.grdApp.Rows.Count = 2;
            this.grdApp.Rows.DefaultSize = 18;
            this.grdApp.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
            this.grdApp.Size = new System.Drawing.Size(435, 182);
            this.grdApp.StyleInfo = resources.GetString("grdApp.StyleInfo");
            this.grdApp.TabIndex = 2;
            this.grdApp.Tag = "16";
            this.grdApp.DoubleClick += new System.EventHandler(this.grdApp_DoubleClick);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(429, 12);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(68, 21);
            this.checkBox1.TabIndex = 14;
            this.checkBox1.Text = "Active";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // cboApp
            // 
            this.cboApp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboApp.FormattingEnabled = true;
            this.cboApp.Location = new System.Drawing.Point(139, 84);
            this.cboApp.Name = "cboApp";
            this.cboApp.Size = new System.Drawing.Size(121, 24);
            this.cboApp.TabIndex = 3;
            this.cboApp.Visible = false;
            this.cboApp.SelectedIndexChanged += new System.EventHandler(this.cboApp_SelectedIndexChanged);
            // 
            // frmCeritarClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 344);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblNom);
            this.Controls.Add(this.formController);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmCeritarClient";
            this.Text = "Définition d\'un client Ceritar";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdApp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private TT3LightDLL.Controls.ctlFormController formController;
        private System.Windows.Forms.Label lblNom;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnGrdDel;
        private System.Windows.Forms.Button btnGrdAdd;
        public C1.Win.C1FlexGrid.C1FlexGrid grdApp;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ComboBox cboApp;
    }
}