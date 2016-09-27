namespace Ceritar.Logirack_CVS.Forms
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
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.formController = new Ceritar.TT3LightDLL.Controls.ctlFormController();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.SuspendLayout();
            // 
            // grdList
            // 
            this.grdList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdList.AutoResize = true;
            this.grdList.ColumnInfo = "4,1,0,0,0,90,Columns:0{Width:5;}\t1{Width:94;Caption:\"Latitude\";}\t2{Width:113;Capt" +
    "ion:\"Longitude\";}\t3{Caption:\"Sél.\";}\t";
            this.grdList.ExtendLastCol = true;
            this.grdList.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdList.Location = new System.Drawing.Point(6, 79);
            this.grdList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grdList.Name = "grdList";
            this.grdList.Rows.Count = 2;
            this.grdList.Rows.DefaultSize = 18;
            this.grdList.Size = new System.Drawing.Size(670, 618);
            this.grdList.StyleInfo = resources.GetString("grdList.StyleInfo");
            this.grdList.TabIndex = 0;
            this.grdList.DoubleClick += new System.EventHandler(this.grdList_DoubleClick);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.BackgroundImage")));
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnAdd.Location = new System.Drawing.Point(683, 135);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(47, 49);
            this.btnAdd.TabIndex = 3;
            this.toolTip.SetToolTip(this.btnAdd, "Ajouter");
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnConsult
            // 
            this.btnConsult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConsult.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnConsult.Image = ((System.Drawing.Image)(resources.GetObject("btnConsult.Image")));
            this.btnConsult.Location = new System.Drawing.Point(683, 79);
            this.btnConsult.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnConsult.Name = "btnConsult";
            this.btnConsult.Size = new System.Drawing.Size(47, 49);
            this.btnConsult.TabIndex = 2;
            this.toolTip.SetToolTip(this.btnConsult, "Consulter");
            this.btnConsult.UseVisualStyleBackColor = true;
            this.btnConsult.Click += new System.EventHandler(this.btnConsult_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Location = new System.Drawing.Point(683, 249);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(47, 49);
            this.btnDelete.TabIndex = 5;
            this.toolTip.SetToolTip(this.btnDelete, "Supprimer");
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnUpdate.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdate.Image")));
            this.btnUpdate.Location = new System.Drawing.Point(683, 192);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(47, 49);
            this.btnUpdate.TabIndex = 4;
            this.toolTip.SetToolTip(this.btnUpdate, "Modifier");
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(687, 11);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(37, 37);
            this.btnRefresh.TabIndex = 1;
            // 
            // toolTip
            // 
            this.toolTip.AutomaticDelay = 100;
            // 
            // formController
            // 
            this.formController.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.formController.ChangeMade = false;
            this.formController.FormIsLoading = false;
            this.formController.FormMode = Ceritar.TT3LightDLL.Static_Classes.sclsConstants.DML_Mode.NO_MODE;
            this.formController.Item_NRI = 0;
            this.formController.Location = new System.Drawing.Point(654, 705);
            this.formController.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.formController.Name = "formController";
            this.formController.ShowButtonQuitOnly = true;
            this.formController.Size = new System.Drawing.Size(80, 34);
            this.formController.TabIndex = 6;
            this.formController.BeNotify += new Ceritar.TT3LightDLL.Controls.ctlFormController.BeNotifyEventHandler(this.formController_BeNotify);
            this.formController.LoadData += new Ceritar.TT3LightDLL.Controls.ctlFormController.LoadDataEventHandler(this.formController_LoadData);
            // 
            // frmGenericList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 741);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnConsult);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.formController);
            this.Controls.Add(this.grdList);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "frmGenericList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Liste générique";
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal C1.Win.C1FlexGrid.C1FlexGrid grdList;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnConsult;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private TT3LightDLL.Controls.ctlRefresh btnRefresh;
        private System.Windows.Forms.ToolTip toolTip;
        public TT3LightDLL.Controls.ctlFormController formController;
    }
}