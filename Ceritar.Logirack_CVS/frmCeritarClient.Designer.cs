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
            this.lblNom = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboApp = new System.Windows.Forms.ComboBox();
            this.btnGrdDel = new System.Windows.Forms.Button();
            this.btnGrdAdd = new System.Windows.Forms.Button();
            this.grdApp = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.formController = new Ceritar.TT3LightDLL.Controls.ctlFormController();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdApp)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNom
            // 
            this.lblNom.AutoSize = true;
            this.lblNom.Location = new System.Drawing.Point(4, 11);
            this.lblNom.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNom.Name = "lblNom";
            this.lblNom.Size = new System.Drawing.Size(35, 13);
            this.lblNom.TabIndex = 5;
            this.lblNom.Text = "Nom :";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(43, 8);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(258, 20);
            this.txtName.TabIndex = 6;
            this.txtName.TextChanged += new System.EventHandler(this.txtNom_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboApp);
            this.groupBox1.Controls.Add(this.btnGrdDel);
            this.groupBox1.Controls.Add(this.btnGrdAdd);
            this.groupBox1.Controls.Add(this.grdApp);
            this.groupBox1.Location = new System.Drawing.Point(5, 53);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(371, 174);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Liste des modules";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // cboApp
            // 
            this.cboApp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboApp.FormattingEnabled = true;
            this.cboApp.Location = new System.Drawing.Point(104, 68);
            this.cboApp.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cboApp.Name = "cboApp";
            this.cboApp.Size = new System.Drawing.Size(92, 21);
            this.cboApp.TabIndex = 3;
            this.cboApp.Visible = false;
            this.cboApp.SelectedIndexChanged += new System.EventHandler(this.cboApp_SelectedIndexChanged);
            // 
            // btnGrdDel
            // 
            this.btnGrdDel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGrdDel.Image = ((System.Drawing.Image)(resources.GetObject("btnGrdDel.Image")));
            this.btnGrdDel.Location = new System.Drawing.Point(333, 60);
            this.btnGrdDel.Name = "btnGrdDel";
            this.btnGrdDel.Size = new System.Drawing.Size(35, 35);
            this.btnGrdDel.TabIndex = 1;
            this.btnGrdDel.UseVisualStyleBackColor = true;
            // 
            // btnGrdAdd
            // 
            this.btnGrdAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGrdAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnGrdAdd.Image")));
            this.btnGrdAdd.Location = new System.Drawing.Point(333, 19);
            this.btnGrdAdd.Name = "btnGrdAdd";
            this.btnGrdAdd.Size = new System.Drawing.Size(35, 35);
            this.btnGrdAdd.TabIndex = 0;
            this.btnGrdAdd.UseVisualStyleBackColor = true;
            // 
            // grdApp
            // 
            this.grdApp.AutoSearch = C1.Win.C1FlexGrid.AutoSearchEnum.FromTop;
            this.grdApp.ColumnInfo = resources.GetString("grdApp.ColumnInfo");
            this.grdApp.ExtendLastCol = true;
            this.grdApp.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdApp.Location = new System.Drawing.Point(6, 19);
            this.grdApp.Name = "grdApp";
            this.grdApp.Rows.Count = 2;
            this.grdApp.Rows.DefaultSize = 18;
            this.grdApp.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
            this.grdApp.Size = new System.Drawing.Size(327, 149);
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
            this.checkBox1.Location = new System.Drawing.Point(322, 10);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(56, 17);
            this.checkBox1.TabIndex = 14;
            this.checkBox1.Text = "Active";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // formController
            // 
            this.formController.FormIsLoading = false;
            this.formController.FormMode = Ceritar.TT3LightDLL.Static_Classes.sclsConstants.DML_Mode.NO_MODE;
            this.formController.Item_NRI = 0;
            this.formController.Location = new System.Drawing.Point(1, 245);
            this.formController.Name = "formController";
            this.formController.ShowButtonQuitOnly = false;
            this.formController.Size = new System.Drawing.Size(378, 33);
            this.formController.TabIndex = 15;
            // 
            // frmCeritarClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 280);
            this.Controls.Add(this.formController);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblNom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmCeritarClient";
            this.Text = "Définition d\'un client Ceritar";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdApp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNom;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnGrdDel;
        private System.Windows.Forms.Button btnGrdAdd;
        public C1.Win.C1FlexGrid.C1FlexGrid grdApp;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ComboBox cboApp;
        public TT3LightDLL.Controls.ctlFormController formController;
    }
}