namespace Ceritar.Logirack_CVS
{
    partial class mdiGeneral
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mdiGeneral));
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLogOut = new System.Windows.Forms.ToolStripMenuItem();
            this.muManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuVersion = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuConfiguration = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCerApp = new System.Windows.Forms.ToolStripMenuItem();
            this.clientCeritarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTemplate = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.lblCurrentUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblDatabase = new System.Windows.Forms.ToolStripStatusLabel();
            this.mnuMain.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.muManagement,
            this.mnuConfiguration});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(1314, 29);
            this.mnuMain.TabIndex = 2;
            this.mnuMain.Text = "menuStrip2";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuLogOut});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(68, 25);
            this.mnuFile.Text = "Fichier";
            this.mnuFile.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // mnuLogOut
            // 
            this.mnuLogOut.Name = "mnuLogOut";
            this.mnuLogOut.Size = new System.Drawing.Size(186, 26);
            this.mnuLogOut.Text = "Se déconnecter";
            // 
            // muManagement
            // 
            this.muManagement.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuVersion});
            this.muManagement.Name = "muManagement";
            this.muManagement.Size = new System.Drawing.Size(75, 25);
            this.muManagement.Text = "Gestion";
            this.muManagement.Click += new System.EventHandler(this.installationsActivesToolStripMenuItem_Click);
            // 
            // mnuVersion
            // 
            this.mnuVersion.Name = "mnuVersion";
            this.mnuVersion.Size = new System.Drawing.Size(285, 26);
            this.mnuVersion.Text = "Gestion de version et révision";
            this.mnuVersion.Click += new System.EventHandler(this.versionToolStripMenuItem_Click);
            // 
            // mnuConfiguration
            // 
            this.mnuConfiguration.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCerApp,
            this.clientCeritarToolStripMenuItem,
            this.mnuTemplate});
            this.mnuConfiguration.Name = "mnuConfiguration";
            this.mnuConfiguration.Size = new System.Drawing.Size(118, 25);
            this.mnuConfiguration.Text = "Configuration";
            // 
            // mnuCerApp
            // 
            this.mnuCerApp.Name = "mnuCerApp";
            this.mnuCerApp.Size = new System.Drawing.Size(209, 26);
            this.mnuCerApp.Text = "Application Ceritar";
            this.mnuCerApp.Click += new System.EventHandler(this.mnuCerApp_Click);
            // 
            // clientCeritarToolStripMenuItem
            // 
            this.clientCeritarToolStripMenuItem.Name = "clientCeritarToolStripMenuItem";
            this.clientCeritarToolStripMenuItem.Size = new System.Drawing.Size(209, 26);
            this.clientCeritarToolStripMenuItem.Text = "Client Ceritar";
            this.clientCeritarToolStripMenuItem.Click += new System.EventHandler(this.clientCeritarToolStripMenuItem_Click);
            // 
            // mnuTemplate
            // 
            this.mnuTemplate.Name = "mnuTemplate";
            this.mnuTemplate.Size = new System.Drawing.Size(209, 26);
            this.mnuTemplate.Text = "Gabarit";
            this.mnuTemplate.Click += new System.EventHandler(this.gabaritToolStripMenuItem_Click);
            // 
            // statusBar
            // 
            this.statusBar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblCurrentUser,
            this.toolStripStatusLabel3,
            this.lblDatabase});
            this.statusBar.Location = new System.Drawing.Point(0, 745);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(1314, 25);
            this.statusBar.TabIndex = 4;
            this.statusBar.Text = "statusStrip1";
            // 
            // lblCurrentUser
            // 
            this.lblCurrentUser.AutoSize = false;
            this.lblCurrentUser.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentUser.Name = "lblCurrentUser";
            this.lblCurrentUser.Size = new System.Drawing.Size(500, 20);
            this.lblCurrentUser.Text = "lblUser";
            this.lblCurrentUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(64, 20);
            this.toolStripStatusLabel3.Text = "Database: ";
            this.toolStripStatusLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDatabase
            // 
            this.lblDatabase.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDatabase.Name = "lblDatabase";
            this.lblDatabase.Size = new System.Drawing.Size(56, 20);
            this.lblDatabase.Text = "database";
            // 
            // mdiGeneral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1314, 770);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.mnuMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "mdiGeneral";
            this.Text = "Logirack CVS";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mdiGeneral_FormClosing);
            this.Load += new System.EventHandler(this.mdiGeneral_Load);
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem muManagement;
        private System.Windows.Forms.ToolStripMenuItem mnuConfiguration;
        private System.Windows.Forms.ToolStripMenuItem mnuVersion;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel lblCurrentUser;
        private System.Windows.Forms.ToolStripStatusLabel lblDatabase;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripMenuItem mnuCerApp;
        private System.Windows.Forms.ToolStripMenuItem mnuTemplate;
        private System.Windows.Forms.ToolStripMenuItem mnuLogOut;
        private System.Windows.Forms.ToolStripMenuItem clientCeritarToolStripMenuItem;
    }
}