namespace Ceritar.Logirack_CVS
{
    partial class frmGenericList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGenericList));
            this.grdList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnConsult = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnRefresh = new Ceritar.TT3LightDLL.Controls.ctlRefresh();
            this.formController = new Ceritar.TT3LightDLL.Controls.ctlFormController();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.SuspendLayout();
            // 
            // grdList
            // 
            this.grdList.AutoResize = true;
            this.grdList.ColumnInfo = "4,1,0,0,0,90,Columns:0{Width:5;}\t1{Width:94;Caption:\"Latitude\";}\t2{Width:113;Capt" +
    "ion:\"Longitude\";}\t3{Caption:\"Sél.\";}\t";
            this.grdList.ExtendLastCol = true;
            this.grdList.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdList.Location = new System.Drawing.Point(5, 64);
            this.grdList.Name = "grdList";
            this.grdList.Rows.Count = 2;
            this.grdList.Rows.DefaultSize = 18;
            this.grdList.Size = new System.Drawing.Size(525, 499);
            this.grdList.StyleInfo = resources.GetString("grdList.StyleInfo");
            this.grdList.TabIndex = 9;
            this.grdList.DoubleClick += new System.EventHandler(this.grdList_DoubleClick);
            // 
            // btnAdd
            // 
            this.btnAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.BackgroundImage")));
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnAdd.Location = new System.Drawing.Point(536, 105);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(35, 35);
            this.btnAdd.TabIndex = 14;
            this.toolTip.SetToolTip(this.btnAdd, "Ajouter");
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnConsult
            // 
            this.btnConsult.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnConsult.BackgroundImage")));
            this.btnConsult.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnConsult.Location = new System.Drawing.Point(536, 64);
            this.btnConsult.Name = "btnConsult";
            this.btnConsult.Size = new System.Drawing.Size(35, 35);
            this.btnConsult.TabIndex = 15;
            this.toolTip.SetToolTip(this.btnConsult, "Consulter");
            this.btnConsult.UseVisualStyleBackColor = true;
            this.btnConsult.Click += new System.EventHandler(this.btnConsult_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDelete.BackgroundImage")));
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDelete.Location = new System.Drawing.Point(536, 187);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(35, 35);
            this.btnDelete.TabIndex = 16;
            this.toolTip.SetToolTip(this.btnDelete, "Supprimer");
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUpdate.BackgroundImage")));
            this.btnUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnUpdate.Location = new System.Drawing.Point(536, 146);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(35, 35);
            this.btnUpdate.TabIndex = 17;
            this.toolTip.SetToolTip(this.btnUpdate, "Modifier");
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(536, 12);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(35, 35);
            this.btnRefresh.TabIndex = 18;
            // 
            // formController
            // 
            this.formController.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.formController.FormIsLoading = false;
            this.formController.FormMode = Ceritar.TT3LightDLL.Static_Classes.sclsConstants.DML_Mode.NO_MODE;
            this.formController.Item_NRI = 0;
            this.formController.Location = new System.Drawing.Point(5, 569);
            this.formController.Name = "formController";
            this.formController.ShowButtonQuitOnly = true;
            this.formController.Size = new System.Drawing.Size(574, 33);
            this.formController.TabIndex = 10;
            this.formController.LoadData += new Ceritar.TT3LightDLL.Controls.ctlFormController.LoadDataEventHandler(this.formController_LoadData);
            // 
            // toolTip
            // 
            this.toolTip.AutomaticDelay = 100;
            // 
            // frmGenericList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 602);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnConsult);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.formController);
            this.Controls.Add(this.grdList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmGenericList";
            this.Text = "Liste générique";
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal C1.Win.C1FlexGrid.C1FlexGrid grdList;
        public TT3LightDLL.Controls.ctlFormController formController;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnConsult;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private TT3LightDLL.Controls.ctlRefresh btnRefresh;
        private System.Windows.Forms.ToolTip toolTip;
    }
}