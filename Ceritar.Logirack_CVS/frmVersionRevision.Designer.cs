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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVersionRevision));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ctrlForm = new Ceritar.TT3LightDLL.Controls.ctlFormController();
            this.grdTest = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdTest)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(5, 121);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1123, 543);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.grdTest);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1115, 517);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1115, 517);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ctrlForm
            // 
            this.ctrlForm.FormIsLoading = false;
            this.ctrlForm.FormMode = Ceritar.TT3LightDLL.Static_Classes.sclsConstants.DML_Mode.CONSULT_MODE;
            this.ctrlForm.Item_ID = 0;
            this.ctrlForm.Location = new System.Drawing.Point(5, 674);
            this.ctrlForm.Name = "ctrlForm";
            this.ctrlForm.ShowButtonQuitOnly = false;
            this.ctrlForm.Size = new System.Drawing.Size(1123, 33);
            this.ctrlForm.TabIndex = 1;
            this.ctrlForm.LoadData += new Ceritar.TT3LightDLL.Controls.ctlFormController.LoadDataEventHandler(this.LoadDataEventHandler);
            // 
            // grdTest
            // 
            this.grdTest.ColumnInfo = "4,1,0,0,0,90,Columns:0{Width:5;}\t1{Width:94;Caption:\"Latitude\";}\t2{Width:113;Capt" +
    "ion:\"Longitude\";}\t3{Caption:\"Sél.\";}\t";
            this.grdTest.ExtendLastCol = true;
            this.grdTest.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdTest.Location = new System.Drawing.Point(216, 60);
            this.grdTest.Name = "grdTest";
            this.grdTest.Rows.Count = 2;
            this.grdTest.Rows.DefaultSize = 18;
            this.grdTest.Size = new System.Drawing.Size(592, 367);
            this.grdTest.StyleInfo = resources.GetString("grdTest.StyleInfo");
            this.grdTest.TabIndex = 10;
            // 
            // frmVersionRevision
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1130, 705);
            this.Controls.Add(this.ctrlForm);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmVersionRevision";
            this.Text = "Gestion de version et de revision";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdTest)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        public TT3LightDLL.Controls.ctlFormController ctrlForm;
        internal C1.Win.C1FlexGrid.C1FlexGrid grdTest;
    }
}