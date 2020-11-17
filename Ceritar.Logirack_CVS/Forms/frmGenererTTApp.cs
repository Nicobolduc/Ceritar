using Ceritar.TT3LightDLL.Static_Classes;
using System;
using Ceritar.CVS.Controllers;
using System.Windows.Forms;
using Ceritar.TT3LightDLL.Controls;
using Ceritar.TT3LightDLL.Classes;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Ceritar.Logirack_CVS.Forms
{
    public partial class frmGenererTTApp : Form, Ceritar.TT3LightDLL.Controls.IFormController
    {
        //Controller
        private ctr_OutilCeritar mcCtrOutil;

        //Classes
        private Ceritar.CVS.clsActionResults mcActionResults;

        //Working variables


        public frmGenererTTApp()
        {
            InitializeComponent();

            mcCtrOutil = new ctr_OutilCeritar();
        }

        #region "Interfaces functions"

        public ctlFormController GetFormController()
        {
            return formController;
        }

        #endregion


        #region "Functions"


        #endregion

        private void cboApplications_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnGenererTTApp.Enabled = false;

            if (cboApplications.SelectedIndex >= 0 && !formController.FormIsLoading)
            {
                btnGenererTTApp.Enabled = true;

                SetDefaultTTAppScriptFolderPath();
            }
        }

        private void formController_SetReadRights()
        {
            switch (formController.FormMode)
            {
                case sclsConstants.DML_Mode.INSERT_MODE:
                case sclsConstants.DML_Mode.UPDATE_MODE:
                case sclsConstants.DML_Mode.DELETE_MODE:
                case sclsConstants.DML_Mode.CONSULT_MODE:

                    btnGenererTTApp.Enabled = true;
                    break;
            }
        }

        private void formController_LoadData(TT3LightDLL.Controls.LoadDataEventArgs eventArgs)
        {
            string strDefaultApplication;

            if (!sclsWinControls_Utilities.blnComboBox_LoadFromSQL(mcCtrOutil.strGetApplications_SQL(), "CeA_NRI", "CeA_Name", false, ref cboApplications))
            { }
            else
            {
                strDefaultApplication = clsTTSQL.str_ADOSingleLookUp("CeA_NRI_Default", "TTUser", "TTU_NRI = " + clsTTApp.GetAppController.cUser.User_NRI);

                if (!string.IsNullOrEmpty(strDefaultApplication)) cboApplications.SelectedValue = int.Parse(strDefaultApplication);

                formController.FormIsLoading = false;

                SetDefaultTTAppScriptFolderPath();
            }
        }

        private void SetDefaultTTAppScriptFolderPath()
        {
            string[] lstDir = Directory.GetDirectories(Path.Combine(CVS.sclsAppConfigs.GetRoot_DB_UPGRADE_SCRIPTS, cboApplications.Text), "*VENIR*", SearchOption.TopDirectoryOnly);

            if (lstDir.Length == 1)
            {
                txtTTAppScriptFolderPath.Text = lstDir[0];
            }
            else
            {
                txtTTAppScriptFolderPath.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            }
        }

        private void frmGenererTTApp_Load(object sender, EventArgs e)
        {

        }

        private void btnTTAppScriptFolderPath_Click(object sender, EventArgs e)
        {
            ShowFolderBrowserDialog(ref txtTTAppScriptFolderPath, btnTTAppScriptFolderPath);
        }

        private void ShowFolderBrowserDialog(ref TextBox txtAffected, Button rbtnSource, string vstrDialogDescription = "", bool vblnChangeMade = true)
        {
            DialogResult dialogResult;

            if (formController.FormMode == sclsConstants.DML_Mode.INSERT_MODE || formController.FormMode == sclsConstants.DML_Mode.UPDATE_MODE)
            {
                folderBrowserDialog.ShowNewFolderButton = false;
                folderBrowserDialog.Description = vstrDialogDescription;

                if (rbtnSource.BackColor == System.Drawing.Color.Yellow && !string.IsNullOrEmpty(txtAffected.Text)) //Disabled, c/'est fatiguant et inutile maintenant qu'on a le right click pour ouvrir l'explorer
                {
                    folderBrowserDialog.SelectedPath = txtAffected.Text;
                }
                else
                {
                    folderBrowserDialog.RootFolder = Environment.SpecialFolder.Desktop;
                    folderBrowserDialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                }

                dialogResult = folderBrowserDialog.ShowDialog();

                if (dialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    txtAffected.Text = folderBrowserDialog.SelectedPath;

                    if (txtAffected.Text != string.Empty) rbtnSource.BackColor = System.Drawing.Color.Yellow;

                    formController.ChangeMade = vblnChangeMade;

                    rbtnSource.FlatAppearance.BorderColor = System.Drawing.Color.Yellow;
                }
            }
        }

        private void btnGenererTTApp_Click(object sender, EventArgs e)
        {
            if (System.IO.Directory.Exists(txtTTAppScriptFolderPath.Text))
            {
                Cursor.Current = Cursors.WaitCursor;

                mcActionResults = mcCtrOutil.GenerateTTAppScript((int)cboApplications.SelectedValue, System.IO.Path.Combine(txtTTAppScriptFolderPath.Text, CVS.sclsAppConfigs.GetTTAppDataFileName()));

                if (chkDroitsApp_Table.Checked)
                {
                    string localFilePath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), @"Scripts\Missing_Ceritar_Security_Permissions_AUTO.sql");

                    if (File.Exists(System.IO.Path.Combine(txtTTAppScriptFolderPath.Text, CVS.sclsAppConfigs.GetMissingCeritarSecurityFileName()))) File.Delete(System.IO.Path.Combine(txtTTAppScriptFolderPath.Text, CVS.sclsAppConfigs.GetMissingCeritarSecurityFileName()));

                    File.Copy(localFilePath, System.IO.Path.Combine(txtTTAppScriptFolderPath.Text, CVS.sclsAppConfigs.GetMissingCeritarSecurityFileName()));
                }

                Cursor.Current = Cursors.Default;

                if (mcActionResults.IsValid)
                {
                    MessageBox.Show("Export effectué avec SUCCÈS!" + Environment.NewLine + "À l'emplacement suivant : " + txtTTAppScriptFolderPath.Text, "Message", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("ERREUR lors de l'export." + Environment.NewLine, "Message", MessageBoxButtons.OK);
                }
            }
        }

        private void txtTTAppScriptFolderPath_TextChanged(object sender, EventArgs e)
        {
            if (txtTTAppScriptFolderPath.Text == "")
            {
                txtTTAppScriptFolderPath.ForeColor = System.Drawing.Color.Black;
            }
            else if (System.IO.Directory.Exists(txtTTAppScriptFolderPath.Text))
            {
                txtTTAppScriptFolderPath.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                txtTTAppScriptFolderPath.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void btnShowLocation_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(txtTTAppScriptFolderPath.Text))
            {
                System.Diagnostics.Process.Start(txtTTAppScriptFolderPath.Text);
            }
        }
    }
}
