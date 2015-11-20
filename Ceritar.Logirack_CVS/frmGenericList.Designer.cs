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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGenericList));
            this.grdList = new C1.Win.C1FlexGrid.C1FlexGrid();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.SuspendLayout();
            // 
            // grdList
            // 
            this.grdList.ColumnInfo = "4,1,0,0,0,90,Columns:0{Width:5;}\t1{Width:94;Caption:\"Latitude\";}\t2{Width:113;Capt" +
    "ion:\"Longitude\";}\t3{Caption:\"Sél.\";}\t";
            this.grdList.ExtendLastCol = true;
            this.grdList.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdList.Location = new System.Drawing.Point(5, 114);
            this.grdList.Name = "grdList";
            this.grdList.Rows.Count = 2;
            this.grdList.Rows.DefaultSize = 18;
            this.grdList.Size = new System.Drawing.Size(592, 367);
            this.grdList.StyleInfo = resources.GetString("grdList.StyleInfo");
            this.grdList.TabIndex = 9;
            // 
            // frmGenericList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 630);
            this.Controls.Add(this.grdList);
            this.Name = "frmGenericList";
            this.Text = "frmGenericList";
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal C1.Win.C1FlexGrid.C1FlexGrid grdList;
    }
}