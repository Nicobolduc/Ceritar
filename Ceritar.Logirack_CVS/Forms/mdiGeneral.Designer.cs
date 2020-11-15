namespace Ceritar.Logirack_CVS.Forms
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mdiGeneral));
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLogIn = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLogOut = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuVersion = new System.Windows.Forms.ToolStripMenuItem();
            this.gestionDeDBUpgradeScriptsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuConfiguration = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCerApp = new System.Windows.Forms.ToolStripMenuItem();
            this.clientCeritarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTemplate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUser = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuWindows = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCloseAllWindows = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuConfigurations = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.lblUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatus_User = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatus_Space = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatus_BD = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.mnuOutils = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGenererTTApp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMain.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuManagement,
            this.mnuConfiguration,
            this.mnuWindows,
            this.mnuOutils,
            this.helpMenu});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.MdiWindowListItem = this.mnuWindows;
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.mnuMain.Size = new System.Drawing.Size(1283, 24);
            this.mnuMain.TabIndex = 0;
            this.mnuMain.Text = "MenuStrip";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuLogIn,
            this.mnuLogOut});
            this.mnuFile.ImageTransparentColor = System.Drawing.SystemColors.ActiveBorder;
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(59, 20);
            this.mnuFile.Text = "&Fichier";
            // 
            // mnuLogIn
            // 
            this.mnuLogIn.Name = "mnuLogIn";
            this.mnuLogIn.Size = new System.Drawing.Size(111, 22);
            this.mnuLogIn.Text = "&Entrer";
            this.mnuLogIn.Visible = false;
            this.mnuLogIn.Click += new System.EventHandler(this.mnuLogIn_Click);
            // 
            // mnuLogOut
            // 
            this.mnuLogOut.Name = "mnuLogOut";
            this.mnuLogOut.Size = new System.Drawing.Size(111, 22);
            this.mnuLogOut.Text = "&Sortir";
            this.mnuLogOut.Click += new System.EventHandler(this.mnuLogOut_Click);
            // 
            // mnuManagement
            // 
            this.mnuManagement.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuVersion,
            this.gestionDeDBUpgradeScriptsToolStripMenuItem});
            this.mnuManagement.Name = "mnuManagement";
            this.mnuManagement.Size = new System.Drawing.Size(65, 20);
            this.mnuManagement.Text = "&Gestion";
            // 
            // mnuVersion
            // 
            this.mnuVersion.Name = "mnuVersion";
            this.mnuVersion.Size = new System.Drawing.Size(307, 22);
            this.mnuVersion.Text = "&Gestion de version et révision";
            this.mnuVersion.Click += new System.EventHandler(this.mnuVersion_Click);
            // 
            // gestionDeDBUpgradeScriptsToolStripMenuItem
            // 
            this.gestionDeDBUpgradeScriptsToolStripMenuItem.Name = "gestionDeDBUpgradeScriptsToolStripMenuItem";
            this.gestionDeDBUpgradeScriptsToolStripMenuItem.Size = new System.Drawing.Size(307, 22);
            this.gestionDeDBUpgradeScriptsToolStripMenuItem.Text = "A venir - Gestion de DB_UpgradeScripts";
            // 
            // mnuConfiguration
            // 
            this.mnuConfiguration.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCerApp,
            this.clientCeritarToolStripMenuItem,
            this.mnuTemplate,
            this.mnuUser});
            this.mnuConfiguration.Name = "mnuConfiguration";
            this.mnuConfiguration.Size = new System.Drawing.Size(95, 20);
            this.mnuConfiguration.Text = "&Configuration";
            // 
            // mnuCerApp
            // 
            this.mnuCerApp.Name = "mnuCerApp";
            this.mnuCerApp.Size = new System.Drawing.Size(182, 22);
            this.mnuCerApp.Text = "&Application Ceritar";
            this.mnuCerApp.Click += new System.EventHandler(this.mnuCerApp_Click);
            // 
            // clientCeritarToolStripMenuItem
            // 
            this.clientCeritarToolStripMenuItem.Name = "clientCeritarToolStripMenuItem";
            this.clientCeritarToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.clientCeritarToolStripMenuItem.Text = "&Client Ceritar";
            this.clientCeritarToolStripMenuItem.Click += new System.EventHandler(this.mnuCerClient_Click);
            // 
            // mnuTemplate
            // 
            this.mnuTemplate.Name = "mnuTemplate";
            this.mnuTemplate.Size = new System.Drawing.Size(182, 22);
            this.mnuTemplate.Text = "&Gabarit";
            this.mnuTemplate.Click += new System.EventHandler(this.mnuGabarit_Click);
            // 
            // mnuUser
            // 
            this.mnuUser.Name = "mnuUser";
            this.mnuUser.Size = new System.Drawing.Size(182, 22);
            this.mnuUser.Text = "&Usager";
            this.mnuUser.Click += new System.EventHandler(this.mnuUser_Click);
            // 
            // mnuWindows
            // 
            this.mnuWindows.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCloseAllWindows});
            this.mnuWindows.Name = "mnuWindows";
            this.mnuWindows.Size = new System.Drawing.Size(71, 20);
            this.mnuWindows.Text = "F&enêtres";
            // 
            // mnuCloseAllWindows
            // 
            this.mnuCloseAllWindows.Name = "mnuCloseAllWindows";
            this.mnuCloseAllWindows.Size = new System.Drawing.Size(221, 22);
            this.mnuCloseAllWindows.Text = "&Fermer toute les fenetres";
            this.mnuCloseAllWindows.Click += new System.EventHandler(this.mnuCloseAllWindows_Click);
            // 
            // helpMenu
            // 
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuConfigurations,
            this.toolStripSeparator8,
            this.aboutToolStripMenuItem});
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(46, 20);
            this.helpMenu.Text = "&Aide";
            // 
            // mnuConfigurations
            // 
            this.mnuConfigurations.Name = "mnuConfigurations";
            this.mnuConfigurations.Size = new System.Drawing.Size(194, 22);
            this.mnuConfigurations.Text = "Configurations";
            this.mnuConfigurations.Click += new System.EventHandler(this.mnuConfigurations_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(191, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.aboutToolStripMenuItem.Text = "&About Logirack CVS";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // statusBar
            // 
            this.statusBar.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblUser,
            this.lblStatus_User,
            this.lblStatus_Space,
            this.lblStatus_BD});
            this.statusBar.Location = new System.Drawing.Point(0, 711);
            this.statusBar.Name = "statusBar";
            this.statusBar.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusBar.Size = new System.Drawing.Size(1283, 22);
            this.statusBar.TabIndex = 2;
            this.statusBar.Text = "StatusStrip";
            // 
            // lblUser
            // 
            this.lblUser.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(60, 17);
            this.lblUser.Text = "Usager: ";
            // 
            // lblStatus_User
            // 
            this.lblStatus_User.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus_User.Name = "lblStatus_User";
            this.lblStatus_User.Size = new System.Drawing.Size(0, 17);
            // 
            // lblStatus_Space
            // 
            this.lblStatus_Space.Name = "lblStatus_Space";
            this.lblStatus_Space.Size = new System.Drawing.Size(116, 17);
            this.lblStatus_Space.Text = "                           ";
            // 
            // lblStatus_BD
            // 
            this.lblStatus_BD.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus_BD.Name = "lblStatus_BD";
            this.lblStatus_BD.Size = new System.Drawing.Size(1090, 17);
            this.lblStatus_BD.Spring = true;
            this.lblStatus_BD.Text = "Base de données: ";
            // 
            // mnuOutils
            // 
            this.mnuOutils.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuGenererTTApp});
            this.mnuOutils.Name = "mnuOutils";
            this.mnuOutils.Size = new System.Drawing.Size(54, 20);
            this.mnuOutils.Text = "&Outils";
            // 
            // mnuGenererTTApp
            // 
            this.mnuGenererTTApp.Name = "mnuGenererTTApp";
            this.mnuGenererTTApp.Size = new System.Drawing.Size(198, 22);
            this.mnuGenererTTApp.Text = "Générer TTApp script";
            this.mnuGenererTTApp.Click += new System.EventHandler(this.mnuGenererTTApp_Click);
            // 
            // mdiGeneral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1283, 733);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.mnuMain);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mnuMain;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "mdiGeneral";
            this.Text = "Logirack CVS";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
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
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripStatusLabel lblUser;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuLogOut;
        private System.Windows.Forms.ToolStripMenuItem mnuWindows;
        private System.Windows.Forms.ToolStripMenuItem mnuCloseAllWindows;
        private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem mnuManagement;
        private System.Windows.Forms.ToolStripMenuItem mnuVersion;
        private System.Windows.Forms.ToolStripMenuItem mnuConfiguration;
        private System.Windows.Forms.ToolStripMenuItem mnuCerApp;
        private System.Windows.Forms.ToolStripMenuItem clientCeritarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuTemplate;
        private System.Windows.Forms.ToolStripMenuItem mnuUser;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus_BD;
        private System.Windows.Forms.ToolStripMenuItem mnuLogIn;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus_Space;
        public System.Windows.Forms.ToolStripStatusLabel lblStatus_User;
        private System.Windows.Forms.ToolStripMenuItem mnuConfigurations;
        private System.Windows.Forms.ToolStripMenuItem gestionDeDBUpgradeScriptsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuOutils;
        private System.Windows.Forms.ToolStripMenuItem mnuGenererTTApp;
    }
}



