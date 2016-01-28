namespace Ceritar.Logirack_CVS
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
            this.cboDomain = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.formController = new Ceritar.TT3LightDLL.Controls.ctlFormController();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grdSatApp = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdModules)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSatApp)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNom
            // 
            this.lblNom.Location = new System.Drawing.Point(2, 15);
            this.lblNom.Name = "lblNom";
            this.lblNom.Size = new System.Drawing.Size(51, 17);
            this.lblNom.TabIndex = 0;
            this.lblNom.Text = "Nom:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(70, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(298, 20);
            this.txtName.TabIndex = 0;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(2, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Description:";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(70, 38);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(298, 40);
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
            this.groupBox1.Location = new System.Drawing.Point(5, 285);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(401, 167);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Liste des modules";
            // 
            // btnGrdModDel
            // 
            this.btnGrdModDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGrdModDel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGrdModDel.Image = ((System.Drawing.Image)(resources.GetObject("btnGrdModDel.Image")));
            this.btnGrdModDel.Location = new System.Drawing.Point(363, 60);
            this.btnGrdModDel.Name = "btnGrdModDel";
            this.btnGrdModDel.Size = new System.Drawing.Size(35, 35);
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
            this.btnGrdModAdd.Location = new System.Drawing.Point(363, 19);
            this.btnGrdModAdd.Name = "btnGrdModAdd";
            this.btnGrdModAdd.Size = new System.Drawing.Size(35, 35);
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
            this.grdModules.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdModules.Location = new System.Drawing.Point(6, 19);
            this.grdModules.Name = "grdModules";
            this.grdModules.Rows.Count = 2;
            this.grdModules.Rows.DefaultSize = 18;
            this.grdModules.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
            this.grdModules.Size = new System.Drawing.Size(357, 144);
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
            this.btnGrdSatDel.Location = new System.Drawing.Point(363, 60);
            this.btnGrdSatDel.Name = "btnGrdSatDel";
            this.btnGrdSatDel.Size = new System.Drawing.Size(35, 35);
            this.btnGrdSatDel.TabIndex = 1;
            this.toolTip1.SetToolTip(this.btnGrdSatDel, "Supprimer la ligne");
            this.btnGrdSatDel.UseVisualStyleBackColor = true;
            // 
            // btnGrdSatAdd
            // 
            this.btnGrdSatAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGrdSatAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGrdSatAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnGrdSatAdd.Image")));
            this.btnGrdSatAdd.Location = new System.Drawing.Point(363, 19);
            this.btnGrdSatAdd.Name = "btnGrdSatAdd";
            this.btnGrdSatAdd.Size = new System.Drawing.Size(35, 35);
            this.btnGrdSatAdd.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btnGrdSatAdd, "Ajouter une ligne");
            this.btnGrdSatAdd.UseVisualStyleBackColor = true;
            // 
            // cboDomain
            // 
            this.cboDomain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDomain.FormattingEnabled = true;
            this.cboDomain.Location = new System.Drawing.Point(70, 85);
            this.cboDomain.Name = "cboDomain";
            this.cboDomain.Size = new System.Drawing.Size(185, 21);
            this.cboDomain.TabIndex = 2;
            this.cboDomain.SelectedIndexChanged += new System.EventHandler(this.cboDomain_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(2, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 17);
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
            this.formController.Location = new System.Drawing.Point(-5, 456);
            this.formController.Name = "formController";
            this.formController.ShowButtonQuitOnly = false;
            this.formController.Size = new System.Drawing.Size(413, 33);
            this.formController.TabIndex = 3;
            this.formController.LoadData += new Ceritar.TT3LightDLL.Controls.ctlFormController.LoadDataEventHandler(this.formController_LoadData);
            this.formController.ValidateForm += new Ceritar.TT3LightDLL.Controls.ctlFormController.ValidateFormEventHandler(this.formController_ValidateForm);
            this.formController.SaveData += new Ceritar.TT3LightDLL.Controls.ctlFormController.SaveDataEventHandler(this.formController_SaveData);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnGrdSatDel);
            this.groupBox2.Controls.Add(this.btnGrdSatAdd);
            this.groupBox2.Controls.Add(this.grdSatApp);
            this.groupBox2.Location = new System.Drawing.Point(5, 112);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(401, 167);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Liste des applications satellites";
            // 
            // grdSatApp
            // 
            this.grdSatApp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdSatApp.AutoSearch = C1.Win.C1FlexGrid.AutoSearchEnum.FromTop;
            this.grdSatApp.ColumnInfo = resources.GetString("grdSatApp.ColumnInfo");
            this.grdSatApp.ExtendLastCol = true;
            this.grdSatApp.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdSatApp.Location = new System.Drawing.Point(6, 19);
            this.grdSatApp.Name = "grdSatApp";
            this.grdSatApp.Rows.Count = 2;
            this.grdSatApp.Rows.DefaultSize = 18;
            this.grdSatApp.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
            this.grdSatApp.Size = new System.Drawing.Size(357, 144);
            this.grdSatApp.StyleInfo = resources.GetString("grdSatApp.StyleInfo");
            this.grdSatApp.TabIndex = 2;
            this.grdSatApp.Tag = "25";
            this.grdSatApp.DoubleClick += new System.EventHandler(this.grdAppSat_DoubleClick);
            this.grdSatApp.Validated += new System.EventHandler(this.grdAppSat_Validated);
            // 
            // frmCeritarApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 489);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboDomain);
            this.Controls.Add(this.formController);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblNom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmCeritarApp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Définition d\'une application Ceritar";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdModules)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdSatApp)).EndInit();
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
    }
}