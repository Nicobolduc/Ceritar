namespace Ceritar.Logirack_CVS
{
    partial class frmRevision
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRevision));
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.cboVersions = new System.Windows.Forms.ComboBox();
            this.cboClients = new System.Windows.Forms.ComboBox();
            this.txtScriptsPath = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmdGrdRevDel = new System.Windows.Forms.Button();
            this.cmdGrdRevAdd = new System.Windows.Forms.Button();
            this.grdRevModifs = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnScriptsPath = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdRevModifs)).BeginInit();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePicker1.CustomFormat = "MM-dd-yy";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(710, 129);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(96, 20);
            this.dateTimePicker1.TabIndex = 67;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.Location = new System.Drawing.Point(765, 380);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(40, 40);
            this.button1.TabIndex = 66;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // cboVersions
            // 
            this.cboVersions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVersions.FormattingEnabled = true;
            this.cboVersions.Location = new System.Drawing.Point(80, 157);
            this.cboVersions.Name = "cboVersions";
            this.cboVersions.Size = new System.Drawing.Size(203, 21);
            this.cboVersions.TabIndex = 61;
            // 
            // cboClients
            // 
            this.cboClients.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClients.FormattingEnabled = true;
            this.cboClients.Location = new System.Drawing.Point(80, 130);
            this.cboClients.Name = "cboClients";
            this.cboClients.Size = new System.Drawing.Size(203, 21);
            this.cboClients.TabIndex = 59;
            // 
            // txtScriptsPath
            // 
            this.txtScriptsPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtScriptsPath.Location = new System.Drawing.Point(65, 385);
            this.txtScriptsPath.Name = "txtScriptsPath";
            this.txtScriptsPath.ReadOnly = true;
            this.txtScriptsPath.Size = new System.Drawing.Size(488, 26);
            this.txtScriptsPath.TabIndex = 65;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmdGrdRevDel);
            this.groupBox3.Controls.Add(this.cmdGrdRevAdd);
            this.groupBox3.Controls.Add(this.grdRevModifs);
            this.groupBox3.Location = new System.Drawing.Point(19, 184);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(787, 189);
            this.groupBox3.TabIndex = 63;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Livrables / Modifications inclus";
            // 
            // cmdGrdRevDel
            // 
            this.cmdGrdRevDel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmdGrdRevDel.Image = ((System.Drawing.Image)(resources.GetObject("cmdGrdRevDel.Image")));
            this.cmdGrdRevDel.Location = new System.Drawing.Point(746, 59);
            this.cmdGrdRevDel.Name = "cmdGrdRevDel";
            this.cmdGrdRevDel.Size = new System.Drawing.Size(35, 35);
            this.cmdGrdRevDel.TabIndex = 18;
            this.cmdGrdRevDel.UseVisualStyleBackColor = true;
            // 
            // cmdGrdRevAdd
            // 
            this.cmdGrdRevAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cmdGrdRevAdd.Image = ((System.Drawing.Image)(resources.GetObject("cmdGrdRevAdd.Image")));
            this.cmdGrdRevAdd.Location = new System.Drawing.Point(746, 18);
            this.cmdGrdRevAdd.Name = "cmdGrdRevAdd";
            this.cmdGrdRevAdd.Size = new System.Drawing.Size(35, 35);
            this.cmdGrdRevAdd.TabIndex = 17;
            this.cmdGrdRevAdd.UseVisualStyleBackColor = true;
            // 
            // grdRevModifs
            // 
            this.grdRevModifs.AutoSearch = C1.Win.C1FlexGrid.AutoSearchEnum.FromTop;
            this.grdRevModifs.ColumnInfo = resources.GetString("grdRevModifs.ColumnInfo");
            this.grdRevModifs.ExtendLastCol = true;
            this.grdRevModifs.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdRevModifs.Location = new System.Drawing.Point(6, 18);
            this.grdRevModifs.Name = "grdRevModifs";
            this.grdRevModifs.Rows.Count = 10;
            this.grdRevModifs.Rows.DefaultSize = 18;
            this.grdRevModifs.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.ListBox;
            this.grdRevModifs.Size = new System.Drawing.Size(734, 166);
            this.grdRevModifs.StyleInfo = resources.GetString("grdRevModifs.StyleInfo");
            this.grdRevModifs.TabIndex = 14;
            this.grdRevModifs.Tag = "1";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(19, 160);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 17);
            this.label6.TabIndex = 62;
            this.label6.Text = "Version:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(19, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 17);
            this.label2.TabIndex = 60;
            this.label2.Text = "Client:";
            // 
            // btnScriptsPath
            // 
            this.btnScriptsPath.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnScriptsPath.BackgroundImage")));
            this.btnScriptsPath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnScriptsPath.Location = new System.Drawing.Point(19, 379);
            this.btnScriptsPath.Name = "btnScriptsPath";
            this.btnScriptsPath.Size = new System.Drawing.Size(40, 40);
            this.btnScriptsPath.TabIndex = 64;
            this.btnScriptsPath.UseVisualStyleBackColor = true;
            // 
            // frmRevision
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 548);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cboVersions);
            this.Controls.Add(this.cboClients);
            this.Controls.Add(this.txtScriptsPath);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnScriptsPath);
            this.Name = "frmRevision";
            this.Text = "Gestion de révision";
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdRevModifs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cboVersions;
        private System.Windows.Forms.ComboBox cboClients;
        private System.Windows.Forms.TextBox txtScriptsPath;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button cmdGrdRevDel;
        private System.Windows.Forms.Button cmdGrdRevAdd;
        public C1.Win.C1FlexGrid.C1FlexGrid grdRevModifs;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnScriptsPath;

    }
}