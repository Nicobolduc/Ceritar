﻿using System;
using System.IO;
using System.Linq;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Ceritar.CVS.Models.Module_ActivesInstallations;
using Ceritar.CVS.Controllers.Interfaces;
using Ceritar.TT3LightDLL.Static_Classes;
using Ceritar.TT3LightDLL.Classes;
using System.IO.Compression;

namespace Ceritar.CVS.Controllers
{
    /// <summary>
    /// Cette classe représente le controleur qui fait le lien entre la vue permettant de définir les versions d'une application et le modèle mod_Ver_Version.
    /// Elle passe par l'interface IVersion afin d'extraire les informations de la vue.
    /// </summary>
    public class ctr_Version
    {
        private Interfaces.IVersion mcView;
        private mod_Ver_Version mcModVersion;
        private clsActionResults mcActionResult;
        private clsSQL mcSQL;

        //Messages
        private const int mintMSG_BuildSuccess = 37;
        private const int mintMSG_GenerationCanceled = 39;

        //Working variables
        private bool mblnUpdateSelectedClientOnly = false;
        
        public enum ErrorCode_Ver
        {
            COMPILED_BY_MANDATORY = 1,
            CLIENTS_LIST_MANDATORY = 2,
            VERSION_NO_MANDATORY = 3,
            APP_CHANGEMENT_MANDATORY = 4,
            TTAPP_MANDATORY = 5,
            RELEASE_MANDATORY = 6,
            CERITAR_APP_MANDATORY = 7,
            TEMPLATE_MANDATORY = 8,
            CLIENT_NAME_MANDATORY = 9,
            VERSION_NO_UNIQUE_AND_BIGGER_PREVIOUS = 10,
            REPORT_MANDATORY = 11,
            CANT_DELETE_USED_VERSION = 12,
            DEMO_CANT_BE_INSTALLED = 13,
            SCRIPTS_MANDATORY = 14
        }

        public clsActionResults GetActionResult
        {
            get { return mcActionResult; }
        }

        public ctr_Version(IVersion rView)
        {
            mcView = rView;

            mcActionResult = new clsActionResults();
        }

        public clsActionResults Validate()
        {
            try
            {
                mcModVersion = new mod_Ver_Version();

                pfblnFeedModelWithView();

                mcActionResult = mcModVersion.Validate();

                if (mcActionResult.IsValid)
                {
                    mcModVersion.blnValidateHierarchyBuildFiles();

                    mcActionResult = mcModVersion.ActionResults;
                }
            }
            catch (Exception ex)
            {
                mcActionResult.SetInvalid(sclsConstants.Error_Message.ERROR_UNHANDLED, clsActionResults.BaseErrorCode.UNHANDLED_EXCEPTION);
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return mcActionResult;
        }

        public clsActionResults Save()
        {
            bool blnValidReturn = false;

            try
            {
                mcSQL = new clsSQL();

                if (mcSQL.bln_BeginTransaction())
                {
                    mcModVersion.SetcSQL = mcSQL;

                    blnValidReturn = mcModVersion.blnSave();

                    if (blnValidReturn & mcModVersion.ActionResults.IsValid)
                    {
                        blnValidReturn = blnBuildVersionHierarchy(mcModVersion.TemplateSource.Template_NRI);
                    }

                    mcActionResult = mcModVersion.ActionResults;
                }
            }
            catch (Exception ex)
            {
                mcActionResult.SetInvalid(sclsConstants.Error_Message.ERROR_UNHANDLED, clsActionResults.BaseErrorCode.UNHANDLED_EXCEPTION);
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }
            finally
            {
                if (!blnValidReturn & mcActionResult.IsValid)
                {
                    mcActionResult.SetInvalid(sclsConstants.Error_Message.ERROR_SAVE_MSG, clsActionResults.BaseErrorCode.ERROR_SAVE);
                }
                else if (blnValidReturn & !mcActionResult.IsValid)
                {
                    blnValidReturn = false;
                }

                mcSQL.bln_EndTransaction(mcActionResult.IsValid);
                mcSQL = null;
                mcModVersion = null;
            }

            return mcActionResult;
        }

        public bool blnUpdateVersionHierarchy()
        {
            bool blnValidReturn = false;

            try
            {
                mcSQL = new clsSQL();

                mcModVersion = new mod_Ver_Version();

                mcModVersion.SetcSQL = mcSQL;

                blnValidReturn = pfblnFeedModelWithView();

                if (blnValidReturn)
                {
                    mcModVersion.LstClientsUsing.Clear();

                    mblnUpdateSelectedClientOnly = true;

                    structClientAppVersion structCAV = mcView.GetSelectedClient();
                    mod_CAV_ClientAppVersion cCAV = new mod_CAV_ClientAppVersion();
                    cCAV.DML_Action = structCAV.Action;
                    cCAV.ClientAppVersion_NRI = structCAV.intClientAppVersion_NRI;
                    cCAV.ClientAppVersion_TS = structCAV.intClientAppVersion_TS;
                    cCAV.CeritarApplication_NRI = mcView.GetCeritarApplication_NRI();
                    cCAV.DateInstalled = structCAV.strDateInstalled;
                    cCAV.IsCurrentVersion = structCAV.blnIsCurrentVersion;
                    cCAV.License = structCAV.strLicense;
                    cCAV.Version_NRI = mcView.GetVersion_NRI();
                    cCAV.LocationReportExe = structCAV.strLocationReportExe;
                    cCAV.LocationScriptsRoot = structCAV.strLocationScriptsRoot;

                    cCAV.CeritarClient = new Models.Module_Configuration.mod_CeC_CeritarClient();
                    cCAV.CeritarClient.CeritarClient_NRI = structCAV.intCeritarClient_NRI;
                    cCAV.CeritarClient.CompanyName = structCAV.strCeritarClient_Name;

                    mcModVersion.LstClientsUsing.Add(cCAV);

                    blnValidReturn = mcModVersion.blnValidateHierarchyBuildFiles();

                    if (blnValidReturn)
                    {
                        blnValidReturn = blnBuildVersionHierarchy(mcView.GetTemplateSource_NRI());
                    }
                    else
                    {
                        mcActionResult = mcModVersion.ActionResults;
                    }
                }
            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return blnValidReturn;
        }

        /// <summary>
        /// Construit la hiérarchie reçu en paramètre à partir de ce qui est dans la base de données.
        /// Boucle sur chaque niveau de la hiérarchie et en fonction du type de dossier, effectue divers traitements.
        /// </summary>
        /// <param name="vintTemplate_NRI">Le NRI du gabarit à utiliser.</param>
        /// <returns>Une valeur indiquant si la génération s'est effectuée avec succès.</returns>
        private bool blnBuildVersionHierarchy(int vintTemplate_NRI)
        {
            bool blnValidReturn = false;
            string strSQL = string.Empty;
            string strFolderName = string.Empty;
            string strVersionFolderRoot = string.Empty;
            int intPreviousFolderLevel = -1;
            SqlDataReader cSQLReader = null;
            DirectoryInfo currentFolderInfos = null;

            strSQL = pfstrGetTemplateHierarchy_SQL(vintTemplate_NRI);

            try
            {
                currentFolderInfos = new DirectoryInfo(sclsAppConfigs.GetRoot_INSTALLATIONS_ACTIVES);

                cSQLReader = clsSQL.ADOSelect(strSQL);

                while (cSQLReader.Read())
                {
                    switch (Int32.Parse(cSQLReader["FoT_NRI"].ToString()))
                    {
                        case (int)ctr_Template.FolderType.Version_Number:

                            strFolderName = sclsAppConfigs.GetVersionNumberPrefix + mcView.GetVersionNo().ToString();

                            strVersionFolderRoot = Path.Combine(currentFolderInfos.FullName, strFolderName);

                            if (mcView.GetDML_Action() == sclsConstants.DML_Mode.DELETE_MODE) //On supprime toute la hierarchie existante et on sort
                            {
                                blnValidReturn = pfblnDeleteVersionHierarchy(strVersionFolderRoot);

                                return blnValidReturn;
                            }

                            break;

                        default:

                            strFolderName = cSQLReader["HiCo_Name"].ToString();
                            break;
                    }

                    if (Int32.Parse(cSQLReader["HiCo_NodeLevel"].ToString()) > intPreviousFolderLevel) //On entre dans un sous-dossier
                    {
                        currentFolderInfos = new DirectoryInfo(Path.Combine(currentFolderInfos.FullName, strFolderName));
                    }
                    else if (Int32.Parse(cSQLReader["HiCo_NodeLevel"].ToString()) < intPreviousFolderLevel) //On recule pour revenir au dossier du niveau courant
                    {
                        int intNbLevelBack = intPreviousFolderLevel - Int32.Parse(cSQLReader["HiCo_NodeLevel"].ToString());

                        while (intNbLevelBack > 0)
                        {
                            currentFolderInfos = new DirectoryInfo(Path.Combine(currentFolderInfos.Parent.Parent.FullName, strFolderName));

                            intNbLevelBack--;
                        }
                    }
                    else //On recule d'un niveau pour revenir au dossier d'avant
                    {
                        currentFolderInfos = new DirectoryInfo(Path.Combine(currentFolderInfos.Parent.FullName, strFolderName));
                    }

                    if (!Directory.Exists(currentFolderInfos.FullName))
                    {
                        currentFolderInfos.Create();
                    }

                    switch (Int32.Parse(cSQLReader["FoT_NRI"].ToString()))
                    {
                        case (int)ctr_Template.FolderType.Release:

                            if (!string.IsNullOrEmpty(mcView.GetLocation_Release()) && mcView.GetLocation_Release() != currentFolderInfos.FullName)
                            {
                                blnValidReturn = clsApp.GetAppController.blnCopyFolderContent(mcView.GetLocation_Release(), currentFolderInfos.FullName, true, false, SearchOption.TopDirectoryOnly, sclsAppConfigs.GetReleaseValidExtensions);

                                //TODO: Apply the other solution for this
                                string[] reportExe = Directory.GetFiles(currentFolderInfos.FullName, "*RPT.exe", SearchOption.TopDirectoryOnly);

                                if (reportExe.Length > 0) File.Delete(reportExe[0]);
                            }
                            else
                            {
                                blnValidReturn = true;
                            }

                            if (blnValidReturn)
                            {
                                mcModVersion.Location_Release = currentFolderInfos.FullName;
                            }

                            break;

                        case (int)ctr_Template.FolderType.CaptionsAndMenus:

                            mcModVersion.Location_CaptionsAndMenus = Path.Combine(currentFolderInfos.FullName, sclsAppConfigs.GetCaptionsAndMenusFileName);

                            if (!string.IsNullOrEmpty(mcView.GetLocation_TTApp()) && mcModVersion.Location_CaptionsAndMenus != mcView.GetLocation_TTApp())
                            {
                                File.Copy(mcView.GetLocation_TTApp(), Path.Combine(currentFolderInfos.FullName, sclsAppConfigs.GetCaptionsAndMenusFileName), true);
                            }

                            blnValidReturn = true;

                            break;

                        case (int)ctr_Template.FolderType.Scripts:

                            blnValidReturn = pfblnCopyAllScriptsForClients(currentFolderInfos.FullName);

                            break;

                        case (int)ctr_Template.FolderType.Version_Number:

                            mcModVersion.Location_APP_CHANGEMENT = Path.Combine(currentFolderInfos.FullName, Path.GetFileName(mcView.GetLocation_APP_CHANGEMENT()));

                            if (!string.IsNullOrEmpty(mcView.GetLocation_APP_CHANGEMENT()) && mcModVersion.Location_APP_CHANGEMENT != mcView.GetLocation_APP_CHANGEMENT())
                            {
                                File.Copy(mcView.GetLocation_APP_CHANGEMENT(), Path.Combine(currentFolderInfos.FullName, Path.GetFileName(mcView.GetLocation_APP_CHANGEMENT())), true);
                            }

                            //Ajout des documents divers
                            if ((!string.IsNullOrEmpty(mcView.GetLocation_VariousFile()) || !string.IsNullOrEmpty(mcView.GetLocation_VariousFolder())) && !Directory.Exists(currentFolderInfos.FullName)) currentFolderInfos.Create();

                            if (!string.IsNullOrEmpty(mcView.GetLocation_VariousFile()))
                            {
                                File.Copy(mcView.GetLocation_VariousFile(), Path.Combine(currentFolderInfos.FullName, Path.GetFileName(mcView.GetLocation_VariousFile())), true);
                            }

                            if (!string.IsNullOrEmpty(mcView.GetLocation_VariousFolder()))
                            {
                                clsApp.GetAppController.blnCopyFolderContent(mcView.GetLocation_VariousFolder(), Path.Combine(currentFolderInfos.FullName, Path.GetFileName(mcView.GetLocation_VariousFolder())), true, true);
                            }

                            blnValidReturn = true;

                            break;

                        case (int)ctr_Template.FolderType.Report:

                            blnValidReturn = pfblnCopyAllReportsForClients(currentFolderInfos.FullName);

                            break;

                        case (int)ctr_Template.FolderType.Normal:

                            //Do nothing

                            break;

                        default:
                            blnValidReturn = true;
                            break;
                    }

                    intPreviousFolderLevel = Int32.Parse(cSQLReader["HiCo_NodeLevel"].ToString());

                    if (!blnValidReturn) break;
                }

                if (blnValidReturn)
                {
                    blnValidReturn = pfblnCopyAllSatellitesAndUpdateLocations();
                }
            }
            catch (FileNotFoundException exPath)
            {
                blnValidReturn = false;
                mcActionResult.SetInvalid(sclsConstants.Validation_Message.INVALID_PATH, clsActionResults.BaseErrorCode.UNHANDLED_VALIDATION, exPath.FileName);
            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }
            finally
            {
                if (cSQLReader != null) cSQLReader.Dispose();

                if (blnValidReturn) mcActionResult.SetValid(mintMSG_BuildSuccess);

                if (!blnValidReturn && mcActionResult.IsValid) mcActionResult.SetInvalid(sclsConstants.Error_Message.ERROR_SAVE_MSG, clsActionResults.BaseErrorCode.ERROR_SAVE);

                if (!blnValidReturn && mcModVersion.DML_Action == sclsConstants.DML_Mode.INSERT_MODE)
                {
                    //pfblnDeleteVersionHierarchy(strVersionFolderRoot);
                }
            }

            return blnValidReturn;
        }

        /// <summary>
        /// Va chercher tous les dossiers de scripts dans DB_UpgradeScripts pour une nouvelle version donnée, 
        /// puis copie chacun des scripts dans le répertoire des installations actives pour chacun des clients concernés.
        /// </summary>
        /// <param name="vstrDestinationFolderPath">Le chemin du répertoire où les scripts doivent être copiés.</param>
        /// <returns></returns>
        private bool pfblnCopyAllScriptsForClients(string vstrDestinationFolderPath)
        {
            bool blnValidReturn = false;
            ushort intPreviousActiveVersionInProd;
            ushort intCurrentFolder_VersionNo;
            string strActiveInstallation_Revision_Path = string.Empty;
            string[] lstVersionsFolders;
            List<mod_CAV_ClientAppVersion> lstCAV = mcModVersion.LstClientsUsing;

            try
            {
                blnValidReturn = true;

                foreach (mod_CAV_ClientAppVersion cCAV in lstCAV)
                {
                    if (cCAV.DML_Action == sclsConstants.DML_Mode.INSERT_MODE | mcView.GetIncludeScriptsOnRefresh())
                    {
                        if (mcSQL == null)
                        {
                            intPreviousActiveVersionInProd = UInt16.Parse(clsSQL.str_ADOSingleLookUp("ISNULL(MAX(Version.Ver_No), 0)", "ClientAppVersion INNER JOIN Version ON Version.Ver_NRI = ClientAppVersion.Ver_NRI", "ClientAppVersion.CAV_DtInstalledProd IS NOT NULL AND Version.Ver_No <" + mcView.GetVersionNo() + " AND Version.CeA_NRI = " + mcView.GetCeritarApplication_NRI() + " AND ClientAppVersion.CeC_NRI = " + cCAV.CeritarClient.CeritarClient_NRI));
                        }
                        else
                        {
                            intPreviousActiveVersionInProd = UInt16.Parse(mcSQL.str_ADOSingleLookUp_Trans("ISNULL(MAX(Version.Ver_No), 0)", "ClientAppVersion INNER JOIN Version ON Version.Ver_NRI = ClientAppVersion.Ver_NRI", "ClientAppVersion.CAV_DtInstalledProd IS NOT NULL AND Version.Ver_No <" + mcView.GetVersionNo() + " AND Version.CeA_NRI = " + mcView.GetCeritarApplication_NRI() + " AND ClientAppVersion.CeC_NRI = " + cCAV.CeritarClient.CeritarClient_NRI));
                        }

                        if (intPreviousActiveVersionInProd <= 0)
                        {
                            UInt16.TryParse(mcView.GetLatestVersionNo(), out intPreviousActiveVersionInProd);
                        }

                        if (intPreviousActiveVersionInProd <= 0)
                        {
                            mcActionResult.SetInvalid(mintMSG_GenerationCanceled, clsActionResults.BaseErrorCode.ERROR_SAVE);
                            blnValidReturn = false;
                        }

                        lstVersionsFolders = Directory.GetDirectories(Path.Combine(sclsAppConfigs.GetRoot_DB_UPGRADE_SCRIPTS, mcView.GetCeritarApplication_Name()));

                        strActiveInstallation_Revision_Path = Path.Combine(vstrDestinationFolderPath, cCAV.CeritarClient.CompanyName);

                        if (Directory.Exists(strActiveInstallation_Revision_Path)) { Directory.Delete(strActiveInstallation_Revision_Path, true); }


                        foreach (string strCurrentVersionFolderToCopy_Path in lstVersionsFolders)
                        {
                            UInt16.TryParse(Regex.Replace(new DirectoryInfo(strCurrentVersionFolderToCopy_Path).Name, @"[^0-9]+", ""), out intCurrentFolder_VersionNo);

                            if (blnValidReturn && intCurrentFolder_VersionNo > intPreviousActiveVersionInProd && intCurrentFolder_VersionNo <= mcView.GetVersionNo())
                            {
                                strActiveInstallation_Revision_Path = Path.Combine(vstrDestinationFolderPath, 
                                                                                   cCAV.CeritarClient.CompanyName, 
                                                                                   sclsAppConfigs.GetVersionNumberPrefix + intCurrentFolder_VersionNo.ToString());

                                clsApp.GetAppController.blnCopyFolderContent(strCurrentVersionFolderToCopy_Path,
                                                                             strActiveInstallation_Revision_Path,
                                                                             true,
                                                                             true);

                                cCAV.DML_Action = sclsConstants.DML_Mode.UPDATE_MODE;
                                cCAV.LocationScriptsRoot = Path.Combine(vstrDestinationFolderPath, cCAV.CeritarClient.CompanyName);
                                
                                //Copy all client's specific scripts at the end of the current folder
                                if (Directory.GetDirectories(strCurrentVersionFolderToCopy_Path, cCAV.CeritarClient.CompanyName + "*", SearchOption.TopDirectoryOnly).Length > 0)
                                {
                                    string[] lstSpecificScripts = Directory.GetFiles(Directory.GetDirectories(strCurrentVersionFolderToCopy_Path, cCAV.CeritarClient.CompanyName + "*", SearchOption.TopDirectoryOnly)[0]);
                                    string strNewScriptName = string.Empty;
                                    int intNewScriptNumber = 0;
                                    List<string> lstScripts = Directory.GetFiles(strCurrentVersionFolderToCopy_Path).OrderBy(i => i, new TT3LightDLL.Classes.NaturalStringComparer()).ToList();//.OrderBy(f => f).ToList<string>();

                                    for (int intIndex = 0; intIndex < lstSpecificScripts.Length; intIndex++)
                                    {
                                        strNewScriptName = Path.GetFileName(lstSpecificScripts[intIndex]);
                                        strNewScriptName = strNewScriptName.Substring(strNewScriptName.IndexOf("_") + 1);

                                        //Int32.Parse(new String(Path.GetFileName(lstScripts[lstScripts.Count - 1]).TakeWhile(Char.IsDigit).ToArray()))
                                        intNewScriptNumber = (intNewScriptNumber == 0 ? lstScripts.Count : intNewScriptNumber) + 1;
                                        
                                        strNewScriptName = intNewScriptNumber.ToString("00") + "_" + strNewScriptName;

                                        File.Copy(lstSpecificScripts[intIndex], Path.Combine(vstrDestinationFolderPath,
                                                                                             cCAV.CeritarClient.CompanyName,
                                                                                             sclsAppConfigs.GetVersionNumberPrefix + intCurrentFolder_VersionNo.ToString(),
                                                                                             strNewScriptName), true
                                                 );
                                    }
                                }
                                else 
                                {
                                    //Do nothing
                                }
                            }
                            else
                            {
                                //Do nothing
                            }

                            if (!blnValidReturn) break;
                        }

                        //Ce segment de code sert à sauvegarder le nouveau path du dossier racine des scripts
                        if (blnValidReturn & cCAV.DML_Action == sclsConstants.DML_Mode.UPDATE_MODE)
                        {
                            cCAV.SetcSQL = mcSQL;

                            blnValidReturn = cCAV.blnSave();
                        }      

                        if (!blnValidReturn) break;
                    }
                }
            }
            catch (FileNotFoundException exPath)
            {
                blnValidReturn = false;
                mcActionResult.SetInvalid(sclsConstants.Validation_Message.INVALID_PATH, clsActionResults.BaseErrorCode.UNHANDLED_VALIDATION, exPath.FileName);
            }
            catch (DirectoryNotFoundException exDir)
            {
                blnValidReturn = false;
                mcActionResult.SetInvalid(sclsConstants.Validation_Message.INVALID_PATH, clsActionResults.BaseErrorCode.UNHANDLED_VALIDATION, exDir.TargetSite.Name);
            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return blnValidReturn;
        }

        /// <summary>
        /// Copie chacun des executables de rapport dans le répertoire des installations actives pour chacun des clients concernés.
        /// </summary>
        /// <param name="vstrDestinationFolderPath">Le chemin du répertoire où les rapports doivent être copiés.</param>
        /// <returns></returns>
        private bool pfblnCopyAllReportsForClients(string vstrDestinationFolderPath)
        {
            bool blnValidReturn = false;
            List<structClientAppVersion> lstClients;
            string strNewReportExeLocation = string.Empty;
            int intIndex = 0;

            try
            {
                if (mblnUpdateSelectedClientOnly)
                {
                    lstClients = new List<structClientAppVersion>();
                    lstClients.Add(mcView.GetSelectedClient());
                }
                else
                {
                    lstClients = mcView.GetClientsList();
                }
                

                foreach (structClientAppVersion structClient in lstClients)
                {
                    if (!string.IsNullOrEmpty(structClient.strLocationReportExe))
                    {
                        strNewReportExeLocation = Path.Combine(vstrDestinationFolderPath, structClient.strCeritarClient_Name, Path.GetFileName(structClient.strLocationReportExe));

                        if (strNewReportExeLocation != structClient.strLocationReportExe)
                        {
                            Directory.CreateDirectory(Path.Combine(vstrDestinationFolderPath, structClient.strCeritarClient_Name));

                            File.Copy(structClient.strLocationReportExe, strNewReportExeLocation, true);

                            mcModVersion.LstClientsUsing[intIndex].LocationReportExe = strNewReportExeLocation;
                        }
                    }

                    intIndex++;
                }

                blnValidReturn = true;
            }
            catch (FileNotFoundException exPath)
            {
                blnValidReturn = false;
                mcActionResult.SetInvalid(sclsConstants.Validation_Message.INVALID_PATH, clsActionResults.BaseErrorCode.UNHANDLED_VALIDATION, exPath.FileName);
            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return blnValidReturn;
        }

        /// <summary>
        /// Efface toute la hierarchy de dossiers et son contenu du disque pour la version et application données. Supprime le dossier racine également.
        /// </summary>
        /// <param name="vstrVersionFolderRoot">Le chemin du répertoire à supprimer</param>
        /// <returns></returns>
        private bool pfblnDeleteVersionHierarchy(string vstrVersionFolderRoot)
        {
            bool blnValidReturn = false;
            List<mod_CSV_ClientSatVersion> lstSatellites;
            System.Windows.Forms.DialogResult answer;

            try
            {
                answer = System.Windows.Forms.MessageBox.Show("Voulez-vous supprimer le répertoire à l'emplacement suivant : " + vstrVersionFolderRoot, "Attention", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning, System.Windows.Forms.MessageBoxDefaultButton.Button2);

                if (answer == System.Windows.Forms.DialogResult.Yes)
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();

                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    startInfo.FileName = "cmd.exe";
                    startInfo.Arguments = @"/C RMDIR """ + vstrVersionFolderRoot + @""" /S /Q";

                    process.StartInfo = startInfo;
                    process.Start();
                    process.WaitForExit();

                    lstSatellites = mcModVersion.LstClientSatelliteApps;

                    foreach (mod_CSV_ClientSatVersion cCSV in lstSatellites)
                    {
                        vstrVersionFolderRoot = Path.Combine(sclsAppConfigs.GetRoot_INSTALLATIONS_ACTIVES,
                                                             cCSV.CeritarSatelliteApp.Name,
                                                             cCSV.CeritarClient.CompanyName,
                                                             sclsAppConfigs.GetVersionNumberPrefix + mcModVersion.VersionNo.ToString()
                                                           );

                        answer = System.Windows.Forms.MessageBox.Show("Voulez-vous supprimer le répertoire à l'emplacement suivant : " + vstrVersionFolderRoot, "Attention", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning ,System.Windows.Forms.MessageBoxDefaultButton.Button2);

                        if (answer == System.Windows.Forms.DialogResult.Yes)
                        {
                            process = new System.Diagnostics.Process();
                            startInfo = new System.Diagnostics.ProcessStartInfo();

                            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                            startInfo.FileName = "cmd.exe";
                            startInfo.Arguments = @"/C RMDIR """ + vstrVersionFolderRoot + @""" /S /Q";

                            process.StartInfo = startInfo;
                            process.Start();
                            process.WaitForExit();
                        }
                    }
                }        

                blnValidReturn = true;
            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return blnValidReturn;
        }

        /// <summary>
        /// Efface tous les  fichiers inutiles générés lors de la compilation d'un exéctutable.
        /// </summary>
        /// <param name="vstrReleaseFolderLocation">Le chemin du répertoire à supprimer</param>
        /// <returns></returns>
        private bool pfblnCleanReleaseFolder(string vstrReleaseFolderLocation)
        {
            bool blnValidReturn = false;
            string strCommands = string.Empty;

            try
            {
                //N.B.: FAIT LORS DE LA COPIE DES FICHIERS PAR FITLRES

                //strCommands = @"/C SET folder=""" + vstrReleaseFolderLocation + @"""";
                //strCommands = strCommands + @" & del /s /f /q %folder%\*.xml";
                //strCommands = strCommands + @" & del /s /f /q %folder%\*.pdb";
                //strCommands = strCommands + @" & del /s /f /q %folder%\*.ini";
                //strCommands = strCommands + @" & del /s /f /q %folder%\*.LOG";
                //strCommands = strCommands + @" & rmdir /s /q %folder%\zh-CN";
                //strCommands = strCommands + @" & rmdir /s /q %folder%\Class";

                //System.Diagnostics.Process process = new System.Diagnostics.Process();
                //System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();

                //startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                //startInfo.FileName = "cmd.exe";
                //startInfo.Arguments = strCommands;

                //process.StartInfo = startInfo;
                //process.Start();
                //process.WaitForExit();

                //string MyBatchFile = @"E:\Users\Bolduc\Desktop\CleanRelease.bat";


                //strCommands = string.Format("\"{0}\"", vstrReleaseFolderLocation);

                //process.StartInfo.FileName = MyBatchFile;
                //bool b = process.Start();

                blnValidReturn = true;
            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return blnValidReturn;
        }

        private bool pfblnFeedModelWithView()
        {
            bool blnValidReturn = false;
            List<structClientAppVersion> lstStructCAV;
            mod_CAV_ClientAppVersion cCAV;
            List<structClientSatVersion> lstStructCSV;
            mod_CSV_ClientSatVersion cCSV;

            try
            {
                mcModVersion.DML_Action = mcView.GetDML_Action();
                mcModVersion.Version_NRI = mcView.GetVersion_NRI();
                mcModVersion.Version_TS = mcView.GetVersion_TS();
                mcModVersion.VersionNo = mcView.GetVersionNo();
                mcModVersion.CompiledBy = mcView.GetCompiledBy();
                mcModVersion.Location_APP_CHANGEMENT = mcView.GetLocation_APP_CHANGEMENT();
                mcModVersion.Location_Release = mcView.GetLocation_Release();
                mcModVersion.Location_CaptionsAndMenus = mcView.GetLocation_TTApp();
                mcModVersion.CreationDate = mcView.GetCreationDate();
                mcModVersion.IsDemo = mcView.GetIsDemo();
                mcModVersion.IncludeScriptsOnRefresh = mcView.GetIncludeScriptsOnRefresh();

                mcModVersion.CerApplication = new Ceritar.CVS.Models.Module_Configuration.mod_CeA_CeritarApplication();
                mcModVersion.CerApplication.CeritarApplication_NRI = mcView.GetCeritarApplication_NRI();
                mcModVersion.CerApplication.ExternalReportAppName = clsSQL.str_ADOSingleLookUp("CeA_ExternalRPTAppName", "CerApp", "CeA_NRI = " + mcModVersion.CerApplication.CeritarApplication_NRI);

                mcModVersion.TemplateSource = new Models.Module_Template.mod_Tpl_HierarchyTemplate();
                mcModVersion.TemplateSource.Template_NRI = mcView.GetTemplateSource_NRI();
                
                lstStructCAV = mcView.GetClientsList();

                foreach (structClientAppVersion structCAV in lstStructCAV)
                {
                    cCAV = new mod_CAV_ClientAppVersion();
                    cCAV.DML_Action = structCAV.Action;
                    cCAV.ClientAppVersion_NRI = structCAV.intClientAppVersion_NRI;
                    cCAV.ClientAppVersion_TS = structCAV.intClientAppVersion_TS;
                    cCAV.CeritarApplication_NRI = mcView.GetCeritarApplication_NRI();
                    cCAV.DateInstalled = structCAV.strDateInstalled;
                    cCAV.IsCurrentVersion = structCAV.blnIsCurrentVersion;
                    cCAV.License = structCAV.strLicense;
                    cCAV.Version_NRI = mcView.GetVersion_NRI();
                    cCAV.LocationReportExe = structCAV.strLocationReportExe;
                    cCAV.LocationScriptsRoot = structCAV.strLocationScriptsRoot;

                    cCAV.CeritarClient = new Models.Module_Configuration.mod_CeC_CeritarClient();
                    cCAV.CeritarClient.CeritarClient_NRI = structCAV.intCeritarClient_NRI;
                    cCAV.CeritarClient.CompanyName = structCAV.strCeritarClient_Name;

                    mcModVersion.LstClientsUsing.Add(cCAV);
                }

                lstStructCSV = mcView.GetClientSatellitesList();

                foreach (structClientSatVersion structCSV in lstStructCSV)
                {
                    cCSV = new mod_CSV_ClientSatVersion();
                    cCSV.DML_Action = structCSV.Action;
                    cCSV.ClientSatVersion_NRI = structCSV.intClientSatVersion_NRI;
                    cCSV.Location_Exe = structCSV.strLocationSatelliteExe;
                    cCSV.Version_NRI = mcView.GetVersion_NRI();

                    cCSV.CeritarClient = new Models.Module_Configuration.mod_CeC_CeritarClient();
                    cCSV.CeritarClient.CeritarClient_NRI = structCSV.intCeritarClient_NRI;
                    cCSV.CeritarClient.CompanyName = structCSV.strCeritarClient_Name;

                    cCSV.CeritarSatelliteApp = new Models.Module_Configuration.mod_CSA_CeritarSatelliteApp();
                    cCSV.CeritarSatelliteApp.Name = structCSV.strCeritarSatelliteApp_Name;
                    cCSV.CeritarSatelliteApp.CeritarSatelliteApp_NRI = structCSV.intCeritarSatelliteApp_NRI;
                    cCSV.CeritarSatelliteApp.ExeIsFolder = structCSV.blnExeIsFolder;
                    cCSV.CeritarSatelliteApp.ExportFolderName = structCSV.strKitFolderName;

                    mcModVersion.LstClientSatelliteApps.Add(cCSV);
                }

                blnValidReturn = true;
            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return blnValidReturn;
        }

        /// <summary>
        /// Construit un fichier zip contenant tout ce qu'il faut pour déployer une nouvelle version chez un client.
        /// </summary>
        /// <param name="vintTemplate_NRI">Le NRI du gabarit à utiliser.</param>
        /// <param name="vintCeritarClient_NRI">Le client à qui le kit est destiné.</param>
        /// <param name="vstrExportFolderLocation">L'endroit ou générer le fichier zip.</param>
        /// <returns>Une valeur indiquant si l'exportation s'est effectuée avec succès.</returns>
        public bool blnExportVersionInstallationKit(int vintTemplate_NRI, int vintCeritarClient_NRI, string vstrExportFolderLocation)
        {
            bool blnValidReturn = false;
            string strSQL = string.Empty;
            string strFolderName = string.Empty;
            string strVersionFolderRoot = string.Empty;
            string strCeritarClientName = string.Empty;
            string strNewZipFileLocation = string.Empty;
            string strReleaseLocation = string.Empty;
            string strLocationSatelliteExe = string.Empty;
            string strReportLocation = string.Empty;
            string strCaptionsAndMenusLocation = mcView.GetLocation_TTApp();
            string strCurrentScriptFolderLocation = mcView.GetSelectedClient().strLocationScriptsRoot;
            string strRevAllScripts_Location = string.Empty;

            try
            {
                strCeritarClientName = clsSQL.str_ADOSingleLookUp("CeC_Name", "CerClient", "CeC_NRI = " + vintCeritarClient_NRI);

                strNewZipFileLocation = vstrExportFolderLocation + @"\Installation Kit " + mcView.GetVersionNo().ToString() + " - " + strCeritarClientName + @".zip";

                //Get the release folder location to copy (from the version kit or from the latest revision)
                strReleaseLocation = clsSQL.str_ADOSingleLookUp("TOP 1 Rev_Location_Exe", "Revision", "Revision.Ver_NRI = " + mcView.GetVersion_NRI() + " AND Rev_Location_Exe IS NOT NULL AND Revision.Rev_ExeIsReport = 0 ORDER BY Revision.Rev_No DESC");

                strReleaseLocation = strReleaseLocation == string.Empty ? mcView.GetLocation_Release() : strReleaseLocation;

                //Create the new archive file and add all the folders to it.
                if (File.Exists(strNewZipFileLocation)) File.Delete(strNewZipFileLocation);

                using (ZipArchive newZipFile = ZipFile.Open(strNewZipFileLocation, ZipArchiveMode.Create))
                {
                    //Add the release folder with the report application to the zip archive.
                    foreach (string strCurrentFileToCopyPath in Directory.GetFiles(strReleaseLocation, "*.*", SearchOption.AllDirectories))
                    {
                        newZipFile.CreateEntryFromFile(strCurrentFileToCopyPath, Path.Combine(sclsAppConfigs.GetReleaseFolderName, Path.GetFileName(strCurrentFileToCopyPath)));
                    }

                    //Get the report folder location to copy (from the version kit or from the latest revision)
                    strReportLocation = clsSQL.str_ADOSingleLookUp("TOP 1 Rev_Location_Exe", "Revision", "Revision.Ver_NRI = " + mcView.GetVersion_NRI() + " AND Rev_Location_Exe IS NOT NULL AND Revision.Rev_ExeIsReport = 1 ORDER BY Revision.Rev_No DESC");

                    strReportLocation = strReportLocation == string.Empty ? mcView.GetSelectedClient().strLocationReportExe : strReportLocation;

                    //Add the external report application to the zip archive
                    newZipFile.CreateEntryFromFile(strReportLocation, Path.Combine(sclsAppConfigs.GetReleaseFolderName, Path.GetFileName(strReportLocation)));
                    
                    //Add the TTApp to the zip archive.
                    newZipFile.CreateEntryFromFile(strCaptionsAndMenusLocation, Path.Combine(new DirectoryInfo(strCaptionsAndMenusLocation).Parent.Name, Path.GetFileName(strCaptionsAndMenusLocation)));
                
                    //Add every satellites applications to the zip archive.
                    List<structClientSatVersion> lstSatellites = mcView.GetClientSatellitesList();

                    foreach (structClientSatVersion structSat in lstSatellites)
                    {
                        //Get the executable folder location to copy (from the version kit or from the latest revision)
                        strLocationSatelliteExe = clsSQL.str_ADOSingleLookUp("TOP 1 SatRevision.SRe_Exe_Location", "Revision INNER JOIN SatRevision ON SatRevision.Rev_NRI = Revision.Rev_NRI", "Revision.Ver_NRI = " + mcView.GetVersion_NRI() + " AND SatRevision.SRe_Exe_Location IS NOT NULL AND SatRevision.CSA_NRI = " + structSat.intCeritarSatelliteApp_NRI + " ORDER BY Revision.Rev_No DESC");

                        strLocationSatelliteExe = strLocationSatelliteExe == string.Empty ? structSat.strLocationSatelliteExe : strLocationSatelliteExe;

                        if (structSat.blnExeIsFolder && Directory.Exists(strLocationSatelliteExe))
                        {
                            foreach (string strCurrentFileToCopyPath in Directory.GetFiles(strLocationSatelliteExe, "*.*", SearchOption.AllDirectories))
                            {
                                newZipFile.CreateEntryFromFile(strCurrentFileToCopyPath, Path.Combine(structSat.strKitFolderName, Path.GetFileName(strCurrentFileToCopyPath)));
                            }
                        }
                        else if (File.Exists(strLocationSatelliteExe))
                        {
                            newZipFile.CreateEntryFromFile(strLocationSatelliteExe, Path.Combine(structSat.strKitFolderName, Path.GetFileName(strLocationSatelliteExe)));
                        }
                    }

                    //Add all scripts folder to the zip archive.
                    string[] lstScriptsFoldersPath = Directory.GetDirectories(strCurrentScriptFolderLocation);

                    foreach (string strCurrentScriptsFolder in lstScriptsFoldersPath)
                    {
                        foreach (string strCurrentFileToCopyPath in Directory.GetFiles(strCurrentScriptsFolder, "*.*", SearchOption.TopDirectoryOnly))
                        {
                            newZipFile.CreateEntryFromFile(strCurrentFileToCopyPath, Path.Combine(sclsAppConfigs.GetScriptsFolderName, new DirectoryInfo(strCurrentScriptsFolder).Name, Path.GetFileName(strCurrentFileToCopyPath)));
                        }
                    }

                    //Add the Rev_AllScripts folder if it exists
                    strRevAllScripts_Location = Path.Combine(sclsAppConfigs.GetRoot_INSTALLATIONS_ACTIVES, mcView.GetCeritarApplication_Name(), sclsAppConfigs.GetVersionNumberPrefix + mcView.GetVersionNo().ToString(), sclsAppConfigs.GetRevisionAllScriptFolderName);

                    if (Directory.Exists(strRevAllScripts_Location))
                    {
                        foreach (string strCurrentFileToCopyPath in Directory.GetFiles(strRevAllScripts_Location, "*.*", SearchOption.TopDirectoryOnly))
                        {
                            newZipFile.CreateEntryFromFile(strCurrentFileToCopyPath, Path.Combine(sclsAppConfigs.GetScriptsFolderName, sclsAppConfigs.GetRevisionAllScriptFolderName, Path.GetFileName(strCurrentFileToCopyPath)));
                        }   
                    }
                }

                blnValidReturn = true;
            }
            catch (FileNotFoundException exPath)
            {
                blnValidReturn = false;
                mcActionResult.SetInvalid(sclsConstants.Validation_Message.INVALID_PATH, clsActionResults.BaseErrorCode.UNHANDLED_VALIDATION, exPath.FileName);
            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            if (!blnValidReturn)
            {
                File.Delete(strNewZipFileLocation);
            }

            return blnValidReturn;
        }

        /// <summary>
        /// Copie les exécutables des applications satellites dans leurs dossiers respectifs dans les installations actives.
        /// </summary>
        /// <returns>Vrai si tout s'est bien passé.</returns>
        private bool pfblnCopyAllSatellitesAndUpdateLocations()
        {
            bool blnValidReturn = false;
            DirectoryInfo currentFolderInfos = null;

            try
            {
                foreach (mod_CSV_ClientSatVersion cCSV in mcModVersion.LstClientSatelliteApps)
                {
                    blnValidReturn = false;

                    if (File.Exists(cCSV.Location_Exe) || Directory.Exists(cCSV.Location_Exe))
                    {
                        currentFolderInfos = new DirectoryInfo(Path.Combine(sclsAppConfigs.GetRoot_INSTALLATIONS_ACTIVES,
                                                                            cCSV.CeritarSatelliteApp.Name,
                                                                            cCSV.CeritarClient.CompanyName,
                                                                            sclsAppConfigs.GetVersionNumberPrefix + mcModVersion.VersionNo.ToString(),
                                                                            "Kit"
                                                                           )
                                                             );

                        if (!Directory.Exists(currentFolderInfos.FullName)) currentFolderInfos.Create();

                        if ((File.GetAttributes(cCSV.Location_Exe) & FileAttributes.Directory) == FileAttributes.Directory) //Executable is a folder
                        {
                            if (cCSV.Location_Exe != currentFolderInfos.FullName)
                            {
                                clsApp.GetAppController.blnCopyFolderContent(cCSV.Location_Exe, currentFolderInfos.FullName, true, true, SearchOption.TopDirectoryOnly, sclsAppConfigs.GetReleaseValidExtensions);

                                cCSV.Location_Exe = currentFolderInfos.FullName;
                            }
                        }
                        else //Executable is a file
                        {
                            if (cCSV.Location_Exe != Path.Combine(currentFolderInfos.FullName, Path.GetFileName(cCSV.Location_Exe)))
                            {
                                File.Copy(cCSV.Location_Exe, Path.Combine(currentFolderInfos.FullName, Path.GetFileName(cCSV.Location_Exe)), true);

                                cCSV.Location_Exe = Path.Combine(currentFolderInfos.FullName, Path.GetFileName(cCSV.Location_Exe));
                            }
                        }

                        cCSV.SetcSQL = mcSQL;
                        cCSV.DML_Action = cCSV.ClientSatVersion_NRI == 0 ? sclsConstants.DML_Mode.INSERT_MODE : sclsConstants.DML_Mode.UPDATE_MODE;

                        blnValidReturn = cCSV.blnSave();
                        
                        if (!blnValidReturn)
                        {
                            mcActionResult = mcModVersion.ActionResults;

                            break;
                        }
                    }
                    else
                    {
                        blnValidReturn = true;
                    }
                }

                if (blnValidReturn)
                {
                    blnValidReturn = mcModVersion.blnLocationsUpdate();
                }
            }
            catch (FileNotFoundException exPath)
            {
                blnValidReturn = false;
                mcActionResult.SetInvalid(sclsConstants.Validation_Message.INVALID_PATH, clsActionResults.BaseErrorCode.UNHANDLED_VALIDATION, exPath.FileName);
            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }
            finally
            {
                if (blnValidReturn) mcActionResult.SetValid();
            }

            return blnValidReturn;
        }

        public string str_GetVersionFolderPath(int vintTemplate_NRI, string vstrVersion_No)
        {
            string strSQL = string.Empty;
            string strPath = string.Empty;
            SqlDataReader sqlRecord = null;

            strSQL = strSQL + " WITH LstHierarchyComp " + Environment.NewLine;
            strSQL = strSQL + " AS " + Environment.NewLine;
            strSQL = strSQL + " ( " + Environment.NewLine;
            strSQL = strSQL + "     SELECT *, " + Environment.NewLine;
            strSQL = strSQL + " 		   CAST(0 AS varbinary(max)) AS Level " + Environment.NewLine;
            strSQL = strSQL + " 	FROM HierarchyComp  " + Environment.NewLine;
            strSQL = strSQL + " 	WHERE HiCo_Parent_NRI IS NULL " + Environment.NewLine;

            strSQL = strSQL + "     UNION ALL " + Environment.NewLine;

            strSQL = strSQL + "     SELECT HiCo_Childrens.*, " + Environment.NewLine;
            strSQL = strSQL + " 		   Level + CAST(HiCo_Childrens.HiCo_NRI AS varbinary(max)) AS Level " + Environment.NewLine;
            strSQL = strSQL + " 	FROM HierarchyComp HiCo_Childrens  " + Environment.NewLine;
            strSQL = strSQL + " 		INNER JOIN LstHierarchyComp on HiCo_Childrens.HiCo_Parent_NRI = LstHierarchyComp.HiCo_NRI " + Environment.NewLine;
            strSQL = strSQL + " 	WHERE HiCo_Childrens.HiCo_Parent_NRI IS NOT NULL " + Environment.NewLine;
            strSQL = strSQL + " ) " + Environment.NewLine;

            strSQL = strSQL + " SELECT Path = TTParam.TTP_Value + " + Environment.NewLine;
            strSQL = strSQL + "               (SELECT '/' + CONVERT(VARCHAR(300), REPLACE(LstHierarchyComp.HiCo_Name, '_XXX', " + clsApp.GetAppController.str_FixStringForSQL("_" + vstrVersion_No) + ")) " + Environment.NewLine;
            strSQL = strSQL + "                FROM LstHierarchyComp " + Environment.NewLine;
            strSQL = strSQL + "                    INNER JOIN FolderType ON FolderType.FoT_NRI = LstHierarchyComp.FoT_NRI " + Environment.NewLine;

            strSQL = strSQL + "                WHERE LstHierarchyComp.Tpl_NRI = " + vintTemplate_NRI + Environment.NewLine;
            strSQL = strSQL + "                  AND FolderType.FoT_Modifiable = 0 " + Environment.NewLine;

            strSQL = strSQL + "                ORDER BY Level FOR XML PATH('') ) " + Environment.NewLine;

            strSQL = strSQL + " FROM TTParam " + Environment.NewLine;

            strSQL = strSQL + " WHERE TTParam.TTP_Name = 'InstallationsActives' " + Environment.NewLine;

            sqlRecord = clsSQL.ADOSelect(strSQL);

            if (sqlRecord.Read())
            {
                strPath = sqlRecord["Path"].ToString();
            }

            return strPath;
        }


#region "SQL Queries"

        private string pfstrGetTemplateHierarchy_SQL(int vintTemplate_NRI)
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " WITH LstHierarchyComp " + Environment.NewLine;
            strSQL = strSQL + " AS " + Environment.NewLine;
            strSQL = strSQL + " ( " + Environment.NewLine;
            strSQL = strSQL + "     SELECT *, " + Environment.NewLine;
            strSQL = strSQL + " 		   CAST(0 AS varbinary(max)) AS Level " + Environment.NewLine;
            strSQL = strSQL + " 	FROM HierarchyComp  " + Environment.NewLine;
            strSQL = strSQL + " 	WHERE HiCo_Parent_NRI IS NULL " + Environment.NewLine;

            strSQL = strSQL + "     UNION ALL " + Environment.NewLine;

            strSQL = strSQL + "     SELECT HiCo_Childrens.*, " + Environment.NewLine;
            strSQL = strSQL + " 		   Level + CAST(HiCo_Childrens.HiCo_NRI AS varbinary(max)) AS Level " + Environment.NewLine;
            strSQL = strSQL + " 	FROM HierarchyComp HiCo_Childrens  " + Environment.NewLine;
            strSQL = strSQL + " 		INNER JOIN LstHierarchyComp on HiCo_Childrens.HiCo_Parent_NRI = LstHierarchyComp.HiCo_NRI " + Environment.NewLine;
            strSQL = strSQL + " 	WHERE HiCo_Childrens.HiCo_Parent_NRI IS NOT NULL " + Environment.NewLine;
            strSQL = strSQL + " ) " + Environment.NewLine;

            strSQL = strSQL + " SELECT  LstHierarchyComp.HiCo_Name, " + Environment.NewLine;
            strSQL = strSQL + "    		LstHierarchyComp.HiCo_NodeLevel, " + Environment.NewLine;
            strSQL = strSQL + "    		LstHierarchyComp.FoT_NRI " + Environment.NewLine;

            strSQL = strSQL + " FROM LstHierarchyComp " + Environment.NewLine;

            strSQL = strSQL + " WHERE LstHierarchyComp.Tpl_NRI = " + vintTemplate_NRI + Environment.NewLine;

            strSQL = strSQL + " ORDER BY Level " + Environment.NewLine;

            return strSQL;
        }

        public string strGetDataLoad_SQL(int vintVersion_NRI)
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT Version.Ver_TS, " + Environment.NewLine;
            strSQL = strSQL + "        Version.Ver_CompiledBy, " + Environment.NewLine;
            strSQL = strSQL + "        Version.Ver_No, " + Environment.NewLine;
            strSQL = strSQL + "        Version.Ver_DtCreation, " + Environment.NewLine;
            strSQL = strSQL + "        Version.Tpl_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        Version.CeA_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        Version.Ver_AppChange_Location, " + Environment.NewLine;
            strSQL = strSQL + "        Version.Ver_Release_Location, " + Environment.NewLine;
            strSQL = strSQL + "        Version.Ver_CaptionsAndMenus_Location, " + Environment.NewLine;
            strSQL = strSQL + "        Version.Ver_IsDemo, " + Environment.NewLine;
            strSQL = strSQL + "        CerApp.CeA_ExternalRPTAppName " + Environment.NewLine;

            strSQL = strSQL + " FROM Version " + Environment.NewLine;

            strSQL = strSQL + "     INNER JOIN CerApp ON CerApp.CeA_NRI = Version.CeA_NRI " + Environment.NewLine;

            strSQL = strSQL + " WHERE Version.Ver_NRI = " + vintVersion_NRI + Environment.NewLine;

            return strSQL;
        }

        public string strGetListe_Clients_SQL(int vintVersion_NRI)
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT Action = '" + sclsConstants.DML_Mode.NO_MODE + "', " + Environment.NewLine;
            strSQL = strSQL + "        ClientAppVersion.CAV_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        ClientAppVersion.CAV_TS, " + Environment.NewLine;
            strSQL = strSQL + "        CerClient.CeC_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        CerClient.CeC_Name, " + Environment.NewLine;
            strSQL = strSQL + "        CAV_Installed = CASE WHEN ClientAppVersion.CAV_DtInstalledProd IS NOT NULL THEN 1 ELSE 0 END, " + Environment.NewLine;
            strSQL = strSQL + "        ClientAppVersion.CAV_IsCurrentVersion, " + Environment.NewLine;
            strSQL = strSQL + "        ClientAppVersion.CAV_ReportExe_Location, " + Environment.NewLine;
            strSQL = strSQL + "        ClientAppVersion.CAV_License, " + Environment.NewLine;
            strSQL = strSQL + "        SelCol = 0, " + Environment.NewLine;
            strSQL = strSQL + "        ClientAppVersion.CAV_ScriptsRoot_Location, " + Environment.NewLine;
            strSQL = strSQL + "        ClientAppVersion.CAV_DtInstalledProd " + Environment.NewLine;

            strSQL = strSQL + " FROM ClientAppVersion " + Environment.NewLine;
            strSQL = strSQL + "     INNER JOIN CerClient ON CerClient.CeC_NRI = ClientAppVersion.CeC_NRI AND CerClient.CeC_IsActive = 1 " + Environment.NewLine;

            strSQL = strSQL + " WHERE ClientAppVersion.Ver_NRI = " + vintVersion_NRI + Environment.NewLine;

            strSQL = strSQL + " ORDER BY CerClient.CeC_Name " + Environment.NewLine;

            return strSQL;
        }

        public string strGetListe_SatelliteApps_SQL(int vintVersion_NRI, int vintCeritarClient_NRI)
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT Action = '" + sclsConstants.DML_Mode.NO_MODE + "', " + Environment.NewLine;
            strSQL = strSQL + "        CerSatApp.CSA_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        CerSatApp.CSA_TS, " + Environment.NewLine;
            strSQL = strSQL + "        CerSatApp.CSA_Name, " + Environment.NewLine;
            strSQL = strSQL + "        LatestExe = CASE WHEN TRevision.Rev_No IS NOT NULL THEN 'Revision '+ CONVERT(VARCHAR, TRevision.Rev_No) ELSE 'Kit ' + Version.Ver_No END, " + Environment.NewLine;
            strSQL = strSQL + "        CerSatApp.CSA_KitFolderName, " + Environment.NewLine;
            strSQL = strSQL + "        CerSatApp.CSA_ExeIsFolder, " + Environment.NewLine;
            strSQL = strSQL + "        ClientSatVersion.CSV_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        ClientSatVersion.CSV_Exe_Location " + Environment.NewLine;        

            strSQL = strSQL + " FROM CerSatApp " + Environment.NewLine;

            strSQL = strSQL + "     LEFT JOIN ClientSatVersion  " + Environment.NewLine;
            strSQL = strSQL + " 		INNER JOIN Version ON Version.Ver_NRI = ClientSatVersion.Ver_NRI  " + Environment.NewLine;
            strSQL = strSQL + " 	        LEFT JOIN ( SELECT MAX(Revision.Rev_No) AS Rev_No, Revision.Ver_NRI, Revision.CeC_NRI, SatRevision.CSA_NRI " + Environment.NewLine;
            strSQL = strSQL + " 	                    FROM Revision " + Environment.NewLine;
            strSQL = strSQL + " 	                        LEFT JOIN SatRevision ON SatRevision.Rev_NRI = Revision.Rev_NRI " + Environment.NewLine;
            strSQL = strSQL + " 	                    GROUP BY Revision.Ver_NRI, Revision.CeC_NRI, SatRevision.CSA_NRI " + Environment.NewLine;
            strSQL = strSQL + " 	                  ) As TRevision ON TRevision.Ver_NRI = ClientSatVersion.Ver_NRI AND ClientSatVersion.CSA_NRI = TRevision.CSA_NRI AND TRevision.CeC_NRI = ClientSatVersion.CeC_NRI " + Environment.NewLine;
            strSQL = strSQL + " 	ON ClientSatVersion.Ver_NRI = " + vintVersion_NRI + Environment.NewLine;
            strSQL = strSQL + "    AND ClientSatVersion.CSA_NRI = CerSatApp.CSA_NRI  " + Environment.NewLine;
            strSQL = strSQL + "    AND ClientSatVersion.CeC_NRI = " + vintCeritarClient_NRI + Environment.NewLine;

            strSQL = strSQL + " WHERE CerSatApp.CeA_NRI = " + mcView.GetCeritarApplication_NRI() + Environment.NewLine;

            strSQL = strSQL + " ORDER BY CerSatApp.CSA_Name " + Environment.NewLine;

            return strSQL;
        }

        public string strGetListe_Revisions_SQL(int vintVersion_NRI)
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT Revision.Rev_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        Revision.Rev_TS, " + Environment.NewLine;
            strSQL = strSQL + "        Revision.Rev_No, " + Environment.NewLine;
            strSQL = strSQL + "        CerClient.CeC_Name, " + Environment.NewLine;
            strSQL = strSQL + "        AppName = CASE WHEN Revision.Rev_Location_Exe IS NOT NULL THEN CerApp.CeA_Name " + Environment.NewLine;
            strSQL = strSQL + " 					  ELSE '' END + " + Environment.NewLine;
            strSQL = strSQL + " 				 CASE WHEN Revision.Rev_ExeIsReport = 1 OR Revision.Rev_ExeWithReport = 1 " + Environment.NewLine;
            strSQL = strSQL + " 				      THEN CASE WHEN Revision.Rev_Location_Exe IS NOT NULL " + Environment.NewLine;
            strSQL = strSQL + " 								THEN ' + '  " + Environment.NewLine;
            strSQL = strSQL + " 								ELSE ''  " + Environment.NewLine;
            strSQL = strSQL + " 						   END + 'RPT' " + Environment.NewLine;
            strSQL = strSQL + " 					  ELSE '' END + " + Environment.NewLine;
            strSQL = strSQL + " 				 CASE WHEN EXISTS (SELECT * FROM SatRevision WHERE SatRevision.Rev_NRI = Revision.Rev_NRI) " + Environment.NewLine;
            strSQL = strSQL + " 				      THEN CASE WHEN (Revision.Rev_ExeIsReport = 1 OR Revision.Rev_ExeWithReport = 1) OR (Revision.Rev_Location_Exe IS NOT NULL) " + Environment.NewLine;
            strSQL = strSQL + " 								THEN ' + '  " + Environment.NewLine;
            strSQL = strSQL + " 								ELSE ''  " + Environment.NewLine;
            strSQL = strSQL + " 						   END + STUFF((SELECT ', ' + CONVERT(VARCHAR(100), CSA.CSA_Name) FROM SatRevision INNER JOIN CerSatApp CSA ON CSA.CSA_NRI = SatRevision.CSA_NRI WHERE SatRevision.Rev_NRI = Revision.Rev_NRI FOR XML PATH('')), 1, 1, '') " + Environment.NewLine;
            strSQL = strSQL + " 					  ELSE '' END + " + Environment.NewLine;
            strSQL = strSQL + " 				 CASE WHEN Revision.Rev_Location_Scripts IS NOT NULL  " + Environment.NewLine;
            strSQL = strSQL + " 				      THEN CASE WHEN EXISTS (SELECT * FROM SatRevision WHERE SatRevision.Rev_NRI = Revision.Rev_NRI) OR (Revision.Rev_ExeIsReport = 1 OR Revision.Rev_ExeWithReport = 1) OR Revision.Rev_Location_Exe IS NOT NULL " + Environment.NewLine;
            strSQL = strSQL + " 								THEN ' + '  " + Environment.NewLine;
            strSQL = strSQL + " 								ELSE ''  " + Environment.NewLine;
            strSQL = strSQL + " 						   END + 'Scripts'  " + Environment.NewLine;
            strSQL = strSQL + " 					  ELSE '' END, " + Environment.NewLine;
            strSQL = strSQL + "        Revision.Rev_DtCreation " + Environment.NewLine;

            strSQL = strSQL + " FROM Revision " + Environment.NewLine;

            strSQL = strSQL + "     INNER JOIN CerClient ON CerClient.CeC_NRI = Revision.CeC_NRI " + Environment.NewLine;
            strSQL = strSQL + "     INNER JOIN Version " + Environment.NewLine;
            strSQL = strSQL + "         INNER JOIN CerApp ON CerApp.CeA_NRI = Version.CeA_NRI " + Environment.NewLine;
            strSQL = strSQL + "     ON Version.Ver_NRI = Revision.Ver_NRI " + Environment.NewLine;

            strSQL = strSQL + " WHERE Revision.Ver_NRI = " + vintVersion_NRI + Environment.NewLine;

            strSQL = strSQL + " ORDER BY Revision.Rev_No " + Environment.NewLine;

            return strSQL;
        }

        public string strGetApplications_SQL()
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT CerApp.CeA_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        CerApp.CeA_Name " + Environment.NewLine;

            strSQL = strSQL + " FROM CerApp " + Environment.NewLine;

            strSQL = strSQL + " ORDER BY CerApp.CeA_Name " + Environment.NewLine;

            return strSQL;
        }

        public string strGetTemplates_SQL(int vintCerApplication_NRI = 0)
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT Template.Tpl_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        Template.Tpl_Name " + Environment.NewLine;

            strSQL = strSQL + " FROM Template " + Environment.NewLine;

            strSQL = strSQL + " WHERE Template.TeT_NRI = " + (int)ctr_Template.TemplateType.VERSION + Environment.NewLine;

            if (vintCerApplication_NRI > 0)
            {
                strSQL = strSQL + "   AND Template.CeA_NRI = " + vintCerApplication_NRI + Environment.NewLine;
            }
            
            strSQL = strSQL + " ORDER BY Template.Tpl_ByDefault DESC, Template.Tpl_Name " + Environment.NewLine;

            return strSQL;
        }

        public string strGetClients_SQL(string vstrCeritarClientToIgnore = "")
        {
            string strSQL = string.Empty;

            vstrCeritarClientToIgnore = (string.IsNullOrEmpty(vstrCeritarClientToIgnore) ? vstrCeritarClientToIgnore = "0" : vstrCeritarClientToIgnore);

            strSQL = strSQL + " SELECT CerClient.CeC_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        CerClient.CeC_Name " + Environment.NewLine;

            strSQL = strSQL + " FROM CerClient " + Environment.NewLine;

            strSQL = strSQL + " WHERE CerClient.CeC_NRI NOT IN (" + vstrCeritarClientToIgnore + ") AND CerClient.CeC_IsActive = 1 " + Environment.NewLine;

            strSQL = strSQL + " ORDER BY CerClient.CeC_Name " + Environment.NewLine;

            return strSQL;
        }

#endregion

    }
}
