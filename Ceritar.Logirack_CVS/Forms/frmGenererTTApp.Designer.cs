namespace Ceritar.Logirack_CVS.Forms
{
    partial class frmGenererTTApp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGenererTTApp));
            this.btnGenererTTApp = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cboApplications = new System.Windows.Forms.ComboBox();
            this.formController = new Ceritar.TT3LightDLL.Controls.ctlFormController();
            this.txtTTAppScriptFolderPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnTTAppScriptFolderPath = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.toolTips = new System.Windows.Forms.ToolTip(this.components);
            this.chkDroitsApp_Table = new System.Windows.Forms.CheckBox();
            this.btnShowLocation = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnGenererTTApp
            // 
            this.btnGenererTTApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGenererTTApp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGenererTTApp.Image = ((System.Drawing.Image)(resources.GetObject("btnGenererTTApp.Image")));
            this.btnGenererTTApp.Location = new System.Drawing.Point(210, 133);
            this.btnGenererTTApp.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnGenererTTApp.Name = "btnGenererTTApp";
            this.btnGenererTTApp.Size = new System.Drawing.Size(50, 50);
            this.btnGenererTTApp.TabIndex = 3;
            this.toolTips.SetToolTip(this.btnGenererTTApp, "Génère le script à l\'emplacement spécifié.");
            this.btnGenererTTApp.UseVisualStyleBackColor = true;
            this.btnGenererTTApp.Click += new System.EventHandler(this.btnGenererTTApp_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(4, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 20);
            this.label3.TabIndex = 42;
            this.label3.Text = "Application:";
            // 
            // cboApplications
            // 
            this.cboApplications.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboApplications.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboApplications.FormattingEnabled = true;
            this.cboApplications.Location = new System.Drawing.Point(92, 16);
            this.cboApplications.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.cboApplications.Name = "cboApplications";
            this.cboApplications.Size = new System.Drawing.Size(668, 24);
            this.cboApplications.TabIndex = 0;
            this.cboApplications.SelectedIndexChanged += new System.EventHandler(this.cboApplications_SelectedIndexChanged);
            // 
            // formController
            // 
            this.formController.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.formController.ChangeMade = false;
            this.formController.FormIsLoading = false;
            this.formController.FormMode = Ceritar.TT3LightDLL.Static_Classes.sclsConstants.DML_Mode.NO_MODE;
            this.formController.Item_NRI = 0;
            this.formController.Location = new System.Drawing.Point(684, 153);
            this.formController.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.formController.Name = "formController";
            this.formController.ShowButtonQuitOnly = true;
            this.formController.Size = new System.Drawing.Size(80, 31);
            this.formController.TabIndex = 43;
            this.formController.SetReadRights += new Ceritar.TT3LightDLL.Controls.ctlFormController.SetReadRightsEventHandler(this.formController_SetReadRights);
            this.formController.LoadData += new Ceritar.TT3LightDLL.Controls.ctlFormController.LoadDataEventHandler(this.formController_LoadData);
            // 
            // txtTTAppScriptFolderPath
            // 
            this.txtTTAppScriptFolderPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTTAppScriptFolderPath.Location = new System.Drawing.Point(129, 51);
            this.txtTTAppScriptFolderPath.Multiline = true;
            this.txtTTAppScriptFolderPath.Name = "txtTTAppScriptFolderPath";
            this.txtTTAppScriptFolderPath.Size = new System.Drawing.Size(581, 25);
            this.txtTTAppScriptFolderPath.TabIndex = 1;
            this.txtTTAppScriptFolderPath.TextChanged += new System.EventHandler(this.txtTTAppScriptFolderPath_TextChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(4, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 20);
            this.label1.TabIndex = 45;
            this.label1.Text = "Exporter vers:";
            // 
            // btnTTAppScriptFolderPath
            // 
            this.btnTTAppScriptFolderPath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnTTAppScriptFolderPath.Image = ((System.Drawing.Image)(resources.GetObject("btnTTAppScriptFolderPath.Image")));
            this.btnTTAppScriptFolderPath.Location = new System.Drawing.Point(92, 46);
            this.btnTTAppScriptFolderPath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnTTAppScriptFolderPath.Name = "btnTTAppScriptFolderPath";
            this.btnTTAppScriptFolderPath.Size = new System.Drawing.Size(33, 33);
            this.btnTTAppScriptFolderPath.TabIndex = 2;
            this.btnTTAppScriptFolderPath.UseVisualStyleBackColor = true;
            this.btnTTAppScriptFolderPath.Click += new System.EventHandler(this.btnTTAppScriptFolderPath_Click);
            // 
            // toolTips
            // 
            this.toolTips.AutoPopDelay = 10000;
            this.toolTips.InitialDelay = 50;
            this.toolTips.ReshowDelay = 100;
            // 
            // chkDroitsApp_Table
            // 
            this.chkDroitsApp_Table.AutoSize = true;
            this.chkDroitsApp_Table.Location = new System.Drawing.Point(266, 149);
            this.chkDroitsApp_Table.Name = "chkDroitsApp_Table";
            this.chkDroitsApp_Table.Size = new System.Drawing.Size(406, 20);
            this.chkDroitsApp_Table.TabIndex = 46;
            this.chkDroitsApp_Table.Text = "Générer script pour droits App_Table/Logirack_Security manquant";
            this.chkDroitsApp_Table.UseVisualStyleBackColor = true;
            // 
            // btnShowLocation
            // 
            this.btnShowLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowLocation.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnShowLocation.BackgroundImage")));
            this.btnShowLocation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnShowLocation.Location = new System.Drawing.Point(716, 43);
            this.btnShowLocation.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnShowLocation.Name = "btnShowLocation";
            this.btnShowLocation.Size = new System.Drawing.Size(45, 45);
            this.btnShowLocation.TabIndex = 47;
            this.btnShowLocation.UseVisualStyleBackColor = true;
            this.btnShowLocation.Click += new System.EventHandler(this.btnShowLocation_Click);
            // 
            // frmGenererTTApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 186);
            this.Controls.Add(this.btnShowLocation);
            this.Controls.Add(this.chkDroitsApp_Table);
            this.Controls.Add(this.btnTTAppScriptFolderPath);
            this.Controls.Add(this.txtTTAppScriptFolderPath);
            this.Controls.Add(this.formController);
            this.Controls.Add(this.cboApplications);
            this.Controls.Add(this.btnGenererTTApp);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmGenererTTApp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Génération d\'un script TTApp";
            this.Load += new System.EventHandler(this.frmGenererTTApp_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGenererTTApp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboApplications;
        public TT3LightDLL.Controls.ctlFormController formController;
        private System.Windows.Forms.TextBox txtTTAppScriptFolderPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnTTAppScriptFolderPath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.ToolTip toolTips;
        private System.Windows.Forms.CheckBox chkDroitsApp_Table;
        private System.Windows.Forms.Button btnShowLocation;
    }
}