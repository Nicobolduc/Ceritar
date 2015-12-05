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
            this.grdModules = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.ctlFormController1 = new Ceritar.TT3LightDLL.Controls.ctlFormController();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdModules)).BeginInit();
            this.SuspendLayout();
            // 
            // grdModules
            // 
            this.grdModules.AutoSearch = C1.Win.C1FlexGrid.AutoSearchEnum.FromTop;
            this.grdModules.ColumnInfo = resources.GetString("grdModules.ColumnInfo");
            this.grdModules.ExtendLastCol = true;
            this.grdModules.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdModules.Location = new System.Drawing.Point(8, 71);
            this.grdModules.Margin = new System.Windows.Forms.Padding(4);
            this.grdModules.Name = "grdModules";
            this.grdModules.Rows.Count = 2;
            this.grdModules.Rows.DefaultSize = 18;
            this.grdModules.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
            this.grdModules.Size = new System.Drawing.Size(435, 182);
            this.grdModules.StyleInfo = resources.GetString("grdModules.StyleInfo");
            this.grdModules.TabIndex = 3;
            this.grdModules.Tag = "1";
            // 
            // ctlFormController1
            // 
            this.ctlFormController1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ctlFormController1.FormIsLoading = false;
            this.ctlFormController1.FormMode = Ceritar.TT3LightDLL.Static_Classes.sclsConstants.DML_Mode.NO_MODE;
            this.ctlFormController1.Item_ID = 0;
            this.ctlFormController1.Location = new System.Drawing.Point(5, 307);
            this.ctlFormController1.Name = "ctlFormController1";
            this.ctlFormController1.ShowButtonQuitOnly = false;
            this.ctlFormController1.Size = new System.Drawing.Size(502, 33);
            this.ctlFormController1.TabIndex = 4;
            this.ctlFormController1.LoadData += new Ceritar.TT3LightDLL.Controls.ctlFormController.LoadDataEventHandler(this.ctlFormController1_LoadData);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "label1";
            // 
            // frmCeritarClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 344);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctlFormController1);
            this.Controls.Add(this.grdModules);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmCeritarClient";
            this.Text = "frmCeritarClient";
            ((System.ComponentModel.ISupportInitialize)(this.grdModules)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public C1.Win.C1FlexGrid.C1FlexGrid grdModules;
        private TT3LightDLL.Controls.ctlFormController ctlFormController1;
        private System.Windows.Forms.Label label1;
    }
}