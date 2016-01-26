using System;
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

                    mcActionResult = mcModVersion.ActionResults;

                    if (blnValidReturn & mcActionResult.IsValid)
                    {
                        blnValidReturn = blnBuildVersionHierarchy(mcModVersion.TemplateSource.Template_NRI);
                    }
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

                    structClientAppVersion structCAV = mcView.GetSelectedClient();
                    mod_CAV_ClientAppVersion cCAV = new mod_CAV_ClientAppVersion();
                    cCAV.DML_Action = structCAV.Action;
                    cCAV.ClientAppVersion_NRI = structCAV.intClientAppVersion_NRI;
                    cCAV.ClientAppVersion_TS = structCAV.intClientAppVersion_TS;
                    cCAV.CeritarApplication_NRI = mcView.GetCeritarApplication_NRI();
                    cCAV.Installed = structCAV.blnInstalled;
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
            finally
            {
                if (blnValidReturn) mcActionResult.SetValid();
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
                currentFolderInfos = new DirectoryInfo(sclsAppConfigs.GetRoot_INSTALLATIONS_ACTIVES +
                                                        (sclsAppConfigs.GetRoot_INSTALLATIONS_ACTIVES.Substring(sclsAppConfigs.GetRoot_INSTALLATIONS_ACTIVES.Length - 1, 1) == "\\" ? "" : "\\")
                                                        );

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

                                //TODO: Find an other solution for this
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

                            blnValidReturn = true;

                            break;

                        case (int)ctr_Template.FolderType.Report:

                            blnValidReturn = pfblnCopyAllReportsForClients(currentFolderInfos.FullName);

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
                    blnValidReturn = pfblnUpdateSatellitesAndLocations();
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

                if (blnValidReturn) mcActionResult.SetValid();

                if (!blnValidReturn && mcModVersion.DML_Action == sclsConstants.DML_Mode.INSERT_MODE)
                {
                    pfblnDeleteVersionHierarchy(strVersionFolderRoot);
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
            ushort intActiveVersionInProd;
            ushort intCurrentFolder_VersionNo;
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
                            intActiveVersionInProd = UInt16.Parse(clsSQL.str_ADOSingleLookUp("ISNULL(MAX(Version.Ver_No), 0)", "ClientAppVersion INNER JOIN Version ON Version.Ver_NRI = ClientAppVersion.Ver_NRI", "ClientAppVersion.CAV_IsCurrentVersion = 1 AND Version.CeA_NRI = " + mcView.GetCeritarApplication_NRI() + " AND ClientAppVersion.CeC_NRI = " + cCAV.CeritarClient.CeritarClient_NRI));
                        }
                        else
                        {
                            intActiveVersionInProd = UInt16.Parse(mcSQL.str_ADOSingleLookUp_Trans("ISNULL(MAX(Version.Ver_No), 0)", "ClientAppVersion INNER JOIN Version ON Version.Ver_NRI = ClientAppVersion.Ver_NRI", "ClientAppVersion.CAV_IsCurrentVersion = 1 AND Version.CeA_NRI = " + mcView.GetCeritarApplication_NRI() + " AND ClientAppVersion.CeC_NRI = " + cCAV.CeritarClient.CeritarClient_NRI));
                        }

                        lstVersionsFolders = Directory.GetDirectories(Path.Combine(sclsAppConfigs.GetRoot_DB_UPGRADE_SCRIPTS, mcView.GetCeritarApplication_Name()));

                        foreach (string strCurrentVersionFolderToCopy_Path in lstVersionsFolders)
                        {
                            intCurrentFolder_VersionNo = UInt16.Parse(Regex.Replace(new DirectoryInfo(strCurrentVersionFolderToCopy_Path).Name, @"[^0-9]+", ""));

                            if (intCurrentFolder_VersionNo > intActiveVersionInProd & intCurrentFolder_VersionNo <= mcView.GetVersionNo())
                            {
                                clsApp.GetAppController.blnCopyFolderContent(strCurrentVersionFolderToCopy_Path,
                                                                             Path.Combine(vstrDestinationFolderPath,
                                                                                          cCAV.CeritarClient.CompanyName,
                                                                                          sclsAppConfigs.GetVersionNumberPrefix + intCurrentFolder_VersionNo.ToString()),
                                                                             true,
                                                                             true);

                                cCAV.DML_Action = sclsConstants.DML_Mode.UPDATE_MODE;
                                cCAV.LocationScriptsRoot = Path.Combine(vstrDestinationFolderPath, cCAV.CeritarClient.CompanyName);

                                //Copy all client's specific scripts at the end of the current folder
                                if (Directory.Exists(Path.Combine(strCurrentVersionFolderToCopy_Path, cCAV.CeritarClient.CompanyName)))
                                {
                                    string[] lstSpecificScripts = Directory.GetFiles(Path.Combine(strCurrentVersionFolderToCopy_Path, cCAV.CeritarClient.CompanyName));
                                    string strNewScriptName = string.Empty;
                                    int intNewScriptNumber = 0;

                                    for (int intIndex = 0; intIndex < lstSpecificScripts.Length; intIndex++)
                                    {
                                        List<string> lstScripts = Directory.GetFiles(strCurrentVersionFolderToCopy_Path).OrderBy(f => f).ToList<string>();

                                        strNewScriptName = Path.GetFileName(lstSpecificScripts[intIndex]);
                                        strNewScriptName = strNewScriptName.Substring(strNewScriptName.IndexOf("_") + 1);

                                        intNewScriptNumber = (intNewScriptNumber == 0 ? Int32.Parse(new String(Path.GetFileName(lstScripts[lstScripts.Count - 1]).TakeWhile(Char.IsDigit).ToArray())) + 1 : intNewScriptNumber + 1);
                                        
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
                        }

                        //Ce segment de code sert à sauvegarder le nouveau path du dossier racine des scripts
                        if (cCAV.DML_Action == sclsConstants.DML_Mode.UPDATE_MODE)
                        {
                            cCAV.SetcSQL = mcSQL;

                            blnValidReturn = cCAV.blnSave();
                        }
                        else
                        {
                            blnValidReturn = true;
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
                lstClients = mcView.GetClientsList();

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

            try
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
                    vstrVersionFolderRoot = Path.Combine(sclsAppConfigs.GetRoot_INSTALLATIONS_ACTIVES +
                                                    (sclsAppConfigs.GetRoot_INSTALLATIONS_ACTIVES.Substring(sclsAppConfigs.GetRoot_INSTALLATIONS_ACTIVES.Length - 1, 1) == "\\" ? "" : "\\"),
                                                    cCSV.CeritarSatelliteApp.Name,
                                                    cCSV.CeritarClient.CompanyName,
                                                    sclsAppConfigs.GetVersionNumberPrefix + mcModVersion.VersionNo.ToString()
                                                   );

                    process = new System.Diagnostics.Process();
                    startInfo = new System.Diagnostics.ProcessStartInfo();

                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    startInfo.FileName = "cmd.exe";
                    startInfo.Arguments = @"/C RMDIR """ + vstrVersionFolderRoot + @""" /S /Q";

                    process.StartInfo = startInfo;
                    process.Start();
                    process.WaitForExit();
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
                mcModVersion.CerApplication = new Ceritar.CVS.Models.Module_Configuration.mod_CeA_CeritarApplication();
                mcModVersion.CerApplication.CeritarApplication_NRI = mcView.GetCeritarApplication_NRI();
                mcModVersion.Location_APP_CHANGEMENT = mcView.GetLocation_APP_CHANGEMENT();
                mcModVersion.Location_Release = mcView.GetLocation_Release();
                mcModVersion.Location_CaptionsAndMenus = mcView.GetLocation_TTApp();
                mcModVersion.TemplateSource = new Models.Module_Template.mod_Tpl_HierarchyTemplate();
                mcModVersion.TemplateSource.Template_NRI = mcView.GetTemplateSource_NRI();
                mcModVersion.CreationDate = mcView.GetCreationDate();
                mcModVersion.IsDemo = mcView.GetIsDemo();
                mcModVersion.IncludeScriptsOnRefresh = mcView.GetIncludeScriptsOnRefresh();

                lstStructCAV = mcView.GetClientsList();

                foreach (structClientAppVersion structCAV in lstStructCAV)
                {
                    cCAV = new mod_CAV_ClientAppVersion();
                    cCAV.DML_Action = structCAV.Action;
                    cCAV.ClientAppVersion_NRI = structCAV.intClientAppVersion_NRI;
                    cCAV.ClientAppVersion_TS = structCAV.intClientAppVersion_TS;
                    cCAV.CeritarApplication_NRI = mcView.GetCeritarApplication_NRI();
                    cCAV.Installed = structCAV.blnInstalled;
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
                    cCSV.CeritarSatelliteApp.CeritarSatelliteApp_NRI = structCSV.intCeritarAppSat_NRI;
                    cCSV.CeritarSatelliteApp.ExeIsFolder = structCSV.blnExeIsFolder;
                    cCSV.CeritarSatelliteApp.KitFolderName = structCSV.strKitFolderName;

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
            string strNewZipFileLocation = vstrExportFolderLocation + @"\Installation Kit " + mcView.GetVersionNo().ToString() + @".zip";
            string strReleaseLocation = mcView.GetLocation_Release();
            string strReportLocation = mcView.GetSelectedClient().strLocationReportExe;
            string strCaptionsAndMenusLocation = mcView.GetLocation_TTApp();
            string strCurrentScriptFolderLocation = mcView.GetSelectedClient().strLocationScriptsRoot;

            try
            {
                if (File.Exists(strNewZipFileLocation))
                {
                    File.Delete(strNewZipFileLocation);
                }

                //Create the new archive file and add all the folders to it.
                using (ZipArchive newZipFile = ZipFile.Open(strNewZipFileLocation, ZipArchiveMode.Create))
                {
                    //Add the release folder with the report application to the zip archive.
                    foreach (string strCurrentFileToCopyPath in Directory.GetFiles(strReleaseLocation, "*.*", SearchOption.AllDirectories))
                    {
                        newZipFile.CreateEntryFromFile(strCurrentFileToCopyPath, Path.Combine(sclsAppConfigs.GetReleaseFolderName, Path.GetFileName(strCurrentFileToCopyPath)));
                    }
                    newZipFile.CreateEntryFromFile(strReportLocation, Path.Combine(sclsAppConfigs.GetReleaseFolderName, Path.GetFileName(strReportLocation)));

                    //Add the TTApp to the Zip archive.
                    //File.SetAttributes(strCaptionsAndMenusLocation, FileAttributes.Normal);
                    newZipFile.CreateEntryFromFile(strCaptionsAndMenusLocation, Path.Combine(new DirectoryInfo(strCaptionsAndMenusLocation).Parent.Name, Path.GetFileName(strCaptionsAndMenusLocation)));
                
                    //Add every satellites applications to the zip archive.
                    List<structClientSatVersion> lstSatellites = mcView.GetClientSatellitesList();

                    foreach (structClientSatVersion structSat in lstSatellites)
                    {
                        if (structSat.blnExeIsFolder && Directory.Exists(structSat.strLocationSatelliteExe))
                        {
                            foreach (string strCurrentFileToCopyPath in Directory.GetFiles(structSat.strLocationSatelliteExe, "*.*", SearchOption.AllDirectories))
                            {
                                newZipFile.CreateEntryFromFile(strCurrentFileToCopyPath, Path.Combine(structSat.strKitFolderName, Path.GetFileName(strCurrentFileToCopyPath)));
                            }
                        }
                        else if (File.Exists(structSat.strLocationSatelliteExe))
                        {
                            newZipFile.CreateEntryFromFile(structSat.strLocationSatelliteExe, Path.Combine(structSat.strKitFolderName, Path.GetFileName(structSat.strLocationSatelliteExe)));
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

        private bool pfblnUpdateSatellitesAndLocations()
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
                        currentFolderInfos = new DirectoryInfo(Path.Combine(sclsAppConfigs.GetRoot_INSTALLATIONS_ACTIVES +
                                                                        (sclsAppConfigs.GetRoot_INSTALLATIONS_ACTIVES.Substring(sclsAppConfigs.GetRoot_INSTALLATIONS_ACTIVES.Length - 1, 1) == "\\" ? "" : "\\"),
                                                                        cCSV.CeritarSatelliteApp.Name,
                                                                        cCSV.CeritarClient.CompanyName,
                                                                        sclsAppConfigs.GetVersionNumberPrefix + mcModVersion.VersionNo.ToString()
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
                        cCSV.DML_Action = (mcModVersion.DML_Action == sclsConstants.DML_Mode.INSERT_MODE ? sclsConstants.DML_Mode.UPDATE_MODE : cCSV.DML_Action);

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


#region "SQL Queries"

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
            strSQL = strSQL + "        Version.Ver_IsDemo " + Environment.NewLine;

            strSQL = strSQL + " FROM Version " + Environment.NewLine;

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
            strSQL = strSQL + "        ClientAppVersion.CAV_Installed, " + Environment.NewLine;
            strSQL = strSQL + "        ClientAppVersion.CAV_IsCurrentVersion, " + Environment.NewLine;
            strSQL = strSQL + "        ClientAppVersion.CAV_ReportExe_Location, " + Environment.NewLine;
            strSQL = strSQL + "        ClientAppVersion.CAV_License, " + Environment.NewLine;
            strSQL = strSQL + "        SelCol = 0, " + Environment.NewLine;
            strSQL = strSQL + "        ClientAppVersion.CAV_ScriptsRoot_Location " + Environment.NewLine;

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
            strSQL = strSQL + "        CerSatApp.CSA_KitFolderName, " + Environment.NewLine;
            strSQL = strSQL + "        CerSatApp.CSA_ExeIsFolder, " + Environment.NewLine;
            strSQL = strSQL + "        ClientSatVersion.CSV_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        ClientSatVersion.CSV_Exe_Location " + Environment.NewLine;        

            strSQL = strSQL + " FROM CerSatApp " + Environment.NewLine;

            strSQL = strSQL + "     LEFT JOIN ClientSatVersion  " + Environment.NewLine;
            strSQL = strSQL + " 		INNER JOIN Version ON Version.Ver_NRI = ClientSatVersion.Ver_NRI  " + Environment.NewLine;
            strSQL = strSQL + " 	ON ClientSatVersion.Ver_NRI = Version.Ver_NRI  " + Environment.NewLine;
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
            strSQL = strSQL + "        NULL " + Environment.NewLine;

            strSQL = strSQL + " FROM Revision " + Environment.NewLine;

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

        public string strGetTemplates_SQL()
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT Template.Tpl_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        Template.Tpl_Name " + Environment.NewLine;

            strSQL = strSQL + " FROM Template " + Environment.NewLine;

            strSQL = strSQL + " WHERE Template.TeT_NRI = " + (int)ctr_Template.TemplateType.VERSION + Environment.NewLine;

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
