using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using Ceritar.TT3LightDLL.Static_Classes;
using Ceritar.TT3LightDLL.Classes;
using Ceritar.CVS.Models.Module_ActivesInstallations;
using Ceritar.CVS.Models.Module_Configuration;

namespace Ceritar.CVS.Controllers
{
    /// <summary>
    /// Cette classe représente le controleur qui fait le lien entre la vue permettant de définir les révisions d'une version et le modèle mod_Ver_Version.
    /// Elle passe par l'interface IVersion afin d'extraire les informations de la vue.
    /// </summary>
    public class ctr_Revision
    {
        private Interfaces.IRevision mcView;
        private mod_Rev_Revision mcModRevision;
        private clsActionResults mcActionResult;
        private clsSQL mcSQL;

        //Messages
        private const int mintMSG_BuildSuccess = 37;

        public enum ErrorCode_Rev
        {
            MODIFICATION_LIST_MANDATORY = 1,
            REVISION_NUMBER_MANDATORY = 2,
            TEMPLATE_MANDATORY = 3,
            VERSION_MANDATORY = 4,
            CERITAR_CLIENT_MANDATORY = 5,
            EXE_OR_SCRIPT_MANDATORY = 6,
            CANT_DELETE_NOT_LAST_REVISION = 7,
            REPORT_EXE_MANDATORY = 8,
            CREATED_BY_MANDATORY = 9
        }

        public ctr_Revision(Interfaces.IRevision rView)
        {
            mcModRevision = new mod_Rev_Revision();
            
            mcView = rView;

            mcActionResult = new clsActionResults();
        }

        private bool pfblnFeedModelWithView()
        {
            bool blnValidReturn = false;
            List<Interfaces.structSatRevision> lstStructSRe;
            mod_SRe_SatelliteRevision cSRe;

            try
            {
                mcModRevision.DML_Action = mcView.GetDML_Action();
                mcModRevision.Revision_Number = mcView.GetRevisionNo();
                mcModRevision.CreationDate = mcView.GetCreationDate();
                mcModRevision.CreatedBy = mcView.GetCreatedBy();
                mcModRevision.LstModifications = mcView.GetModificationsList();
                mcModRevision.Path_Release = mcView.GetLocation_Release();
                mcModRevision.Path_Scripts = mcView.GetLocation_Scripts();
                mcModRevision.Revision_NRI = mcView.GetRevision_NRI();
                mcModRevision.Revision_TS = mcView.GetRevision_TS();
                mcModRevision.ExeIsExternalReport = mcView.GetExeIsExternalReport();
                mcModRevision.ExeWithExternalReport = mcView.GetExeWithExternalReport();

                mcModRevision.CeritarClient = new Models.Module_Configuration.mod_CeC_CeritarClient();
                mcModRevision.CeritarClient.CeritarClient_NRI = mcView.GetCeritarClient_NRI();
                mcModRevision.CeritarClient.CompanyName = mcView.GetCeritarClient_Name();

                //mcModRevision.CreatedByUser = new mod_TTU_User();
                //mcModRevision.CreatedByUser.

                mcModRevision.TemplateSource = new Models.Module_Template.mod_Tpl_HierarchyTemplate();
                mcModRevision.TemplateSource.Template_NRI = mcView.GetTemplateSource_NRI();

                mcModRevision.Version = new mod_Ver_Version();
                mcModRevision.Version.Version_NRI = mcView.GetVersion_NRI();
                mcModRevision.Version.VersionNo = mcView.GetVersionNo();

                lstStructSRe = mcView.GetRevisionSatelliteList();

                foreach (Interfaces.structSatRevision structSRe in lstStructSRe)
                {
                    cSRe = new mod_SRe_SatelliteRevision();
                    cSRe.DML_Action = structSRe.Action;
                    cSRe.SatRevision_NRI = structSRe.intSatRevision_NRI;
                    cSRe.Location_Exe = structSRe.strLocationSatelliteExe;

                    cSRe.Revision = new mod_Rev_Revision();
                    cSRe.Revision.Revision_NRI = mcView.GetRevision_NRI();

                    cSRe.CeritarSatelliteApp = new Models.Module_Configuration.mod_CSA_CeritarSatelliteApp();
                    cSRe.CeritarSatelliteApp.Name = structSRe.strCeritarSatelliteApp_Name;
                    cSRe.CeritarSatelliteApp.CeritarSatelliteApp_NRI = structSRe.intCeritarSatelliteApp_NRI;
                    cSRe.CeritarSatelliteApp.ExeIsFolder = structSRe.blnExeIsFolder;
                    cSRe.CeritarSatelliteApp.ExportFolderName = structSRe.strExportFolderName;

                    mcModRevision.LstSatelliteRevisions.Add(cSRe);
                }

                mcModRevision.Version.CerApplication = new mod_CeA_CeritarApplication();
                mcModRevision.Version.CerApplication.CeritarApplication_NRI = mcView.GetCeritarApplication_NRI();
                mcModRevision.Version.CerApplication.Name = mcView.GetCeritarApplication_Name();
                mcModRevision.Version.CerApplication.ExternalReportAppName = clsSQL.str_ADOSingleLookUp("CeA_ExternalRPTAppName", "CerApp", "CeA_NRI = " + mcModRevision.Version.CerApplication.CeritarApplication_NRI);

                blnValidReturn = true;
            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return blnValidReturn;
        }

        public clsActionResults Validate()
        {
            bool blnValidReturn = false;
            mcModRevision = new mod_Rev_Revision();
            
            try
            {
                blnValidReturn = pfblnFeedModelWithView();

                if (blnValidReturn)
                {
                    mcActionResult = mcModRevision.Validate();
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
            int intSuccessMessage = 0;

            try
            {
                mcSQL = new clsSQL();

                if (mcSQL.bln_BeginTransaction())
                {
                    mcModRevision.SetcSQL = mcSQL;

                    blnValidReturn = mcModRevision.blnSave();

                    if (blnValidReturn & mcModRevision.ActionResults.IsValid)
                    {
                        blnValidReturn = blnBuildRevisionHierarchy(mcModRevision.TemplateSource.Template_NRI);

                        if (blnValidReturn)
                        {
                            mcModRevision.blnUpdateFilesLocations();
                        }
                    }

                    intSuccessMessage = mcActionResult.SuccessMessage_NRI;

                    mcActionResult = mcModRevision.ActionResults;

                    mcActionResult.SuccessMessage_NRI = intSuccessMessage;
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
            }

            return mcActionResult;
        }

        public bool blnBuildRevisionHierarchy(int vintTemplate_NRI)
        {
            bool blnValidReturn = false;
            string strSQL = string.Empty;
            string strFolderName = string.Empty;
            string strRevisionFolderRoot = string.Empty;
            string strRevAllScripts_Location = string.Empty;
            int intPreviousFolderLevel = -1;
            SqlDataReader cSQLReader = null;
            DirectoryInfo currentFolderInfos = null;

            mcActionResult.SetDefault();

            strSQL = strSQL + " WITH LstHierarchyComp " + Environment.NewLine;
            strSQL = strSQL + " AS " + Environment.NewLine;
            strSQL = strSQL + " ( " + Environment.NewLine;
            strSQL = strSQL + "     SELECT *, " + Environment.NewLine;
            strSQL = strSQL + " 		   CAST(0 AS varbinary(max)) AS Level " + Environment.NewLine;
            strSQL = strSQL + " 	FROM HierarchyComp  " + Environment.NewLine;
            strSQL = strSQL + " 	WHERE 1 = 1 " + Environment.NewLine;
            strSQL = strSQL + "       AND HiCo_Parent_NRI IS NULL " + Environment.NewLine;

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
            strSQL = strSQL + " 	INNER JOIN FolderType ON FolderType.FoT_NRI = LstHierarchyComp.FoT_NRI " + Environment.NewLine;

            strSQL = strSQL + " WHERE LstHierarchyComp.Tpl_NRI = " + vintTemplate_NRI + Environment.NewLine;

            strSQL = strSQL + " ORDER BY Level " + Environment.NewLine;

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

                            strRevisionFolderRoot = Path.Combine(currentFolderInfos.FullName, strFolderName);
                                                        
                            break;

                        case (int)ctr_Template.FolderType.Revision_Number:

                            string[] strLstCurrentDirectory = Directory.GetDirectories(currentFolderInfos.FullName, sclsAppConfigs.GetRevisionNumberPrefix + mcView.GetRevisionNo().ToString() + " *");

                            strFolderName = pfstrGetRevisionFolderName();

                            strRevisionFolderRoot = Path.Combine(currentFolderInfos.FullName, strFolderName);

                            if (strLstCurrentDirectory.Length == 1 && !strLstCurrentDirectory[0].ToString().Equals(strRevisionFolderRoot))
                            {
                                Directory.Move(strLstCurrentDirectory[0].ToString(), strRevisionFolderRoot);
                            }

                            if (mcView.GetDML_Action() == sclsConstants.DML_Mode.DELETE_MODE) //On supprime toute la hierarchie existante et on sort
                            {
                                blnValidReturn = pfblnDeleteRevisionHierarchy(strRevisionFolderRoot);

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

                    //Construction du répertoire courant
                    switch (Int32.Parse(cSQLReader["FoT_NRI"].ToString()))
                    {
                        case (int)ctr_Template.FolderType.Release:

                            blnValidReturn = true;

                            //Gestion du Exe de l'application principale ou des rapports
                            if ((File.Exists(mcView.GetLocation_Release()) || Directory.Exists(mcView.GetLocation_Release())))
                            {
                                if ((File.GetAttributes(mcView.GetLocation_Release()) & FileAttributes.Directory) == FileAttributes.Directory)
                                {
                                    if (mcView.GetLocation_Release() != currentFolderInfos.FullName)
                                    {
                                        if (Directory.Exists(currentFolderInfos.FullName)) Directory.Delete(currentFolderInfos.FullName, true);

                                        currentFolderInfos.Create();

                                        blnValidReturn = clsApp.GetAppController.blnCopyFolderContent(mcView.GetLocation_Release(), currentFolderInfos.FullName, true, false, SearchOption.TopDirectoryOnly, sclsAppConfigs.GetReleaseValidExtensions);

                                        //Supprime l'application des rapports externe au besoin
                                        if (!mcView.GetExeWithExternalReport() && !mcView.GetExeIsExternalReport())
                                        {
                                            string strExternalReport_AppName = mcModRevision.Version.CerApplication.ExternalReportAppName;

                                            strExternalReport_AppName = (string.IsNullOrEmpty(strExternalReport_AppName) ? "*RPT.exe" : strExternalReport_AppName);

                                            string[] reportExe = Directory.GetFiles(currentFolderInfos.FullName, strExternalReport_AppName, SearchOption.TopDirectoryOnly);

                                            if (reportExe.Length > 0) File.Delete(reportExe[0]);
                                        }

                                        mcModRevision.Path_Release = currentFolderInfos.FullName;
                                    }
                                }
                                else if (mcView.GetLocation_Release() != Path.Combine(currentFolderInfos.FullName, Path.GetFileName(mcView.GetLocation_Release())))
                                {
                                    if (Directory.Exists(currentFolderInfos.FullName)) Directory.Delete(currentFolderInfos.FullName, true);

                                    currentFolderInfos.Create();

                                    File.Copy(mcView.GetLocation_Release(), Path.Combine(currentFolderInfos.FullName, Path.GetFileName(mcView.GetLocation_Release())), true);

                                    mcModRevision.Path_Release = Path.Combine(currentFolderInfos.FullName, Path.GetFileName(mcView.GetLocation_Release()));
                                }
                            }
                            else if ((File.Exists(currentFolderInfos.FullName) || Directory.Exists(currentFolderInfos.FullName)) && mcView.GetLocation_Release() != currentFolderInfos.FullName)
                            {
                                //Met a jour le path si le contenu de la revision change, mais pas le release
                                mcModRevision.Path_Release = currentFolderInfos.FullName;
                            }

                            //Gestion des applications satellites. On les copie ici au même niveau que le Release
                            if (blnValidReturn && mcModRevision.LstSatelliteRevisions.Count > 0)
                            {
                                string strDestinationFolder = string.Empty;

                                for (int intIndex = 0; intIndex < mcModRevision.LstSatelliteRevisions.Count; intIndex++)
                                {
                                    strDestinationFolder = currentFolderInfos.Parent.FullName;

                                    strDestinationFolder = Path.Combine(strDestinationFolder, mcModRevision.LstSatelliteRevisions[intIndex].CeritarSatelliteApp.ExportFolderName);

                                    blnValidReturn = pfblnCopyAndSaveSatelliteLocation(mcModRevision.LstSatelliteRevisions[intIndex], strDestinationFolder, true);

                                    if (!blnValidReturn) break;
                                }
                            }
                            else
                            {
                                //Do nothing
                            }

                            break;

                        case (int)ctr_Template.FolderType.Scripts:

                            bool blnScriptsChanged = false;

                            if ((File.Exists(mcView.GetLocation_Scripts()) || Directory.Exists(mcView.GetLocation_Scripts())))
                            {
                                if ((File.GetAttributes(mcView.GetLocation_Scripts()) & FileAttributes.Directory) == FileAttributes.Directory)
                                {
                                    if (mcView.GetLocation_Scripts() != currentFolderInfos.FullName)
                                    {
                                        if (!mcView.GetIfScriptsAreToAppend() && Directory.Exists(currentFolderInfos.FullName))
                                        {
                                            clsApp.GetAppController.setAttributesToNormal(new DirectoryInfo(currentFolderInfos.FullName));

                                            Directory.Delete(currentFolderInfos.FullName, true);
                                        }
                                        
                                        currentFolderInfos.Create();

                                        clsApp.GetAppController.blnCopyFolderContent(mcView.GetLocation_Scripts(), currentFolderInfos.FullName, true, true);

                                        mcModRevision.Path_Scripts = currentFolderInfos.FullName;

                                        blnScriptsChanged = true;
                                    }
                                }
                                else if (mcView.GetLocation_Scripts() != Path.Combine(currentFolderInfos.FullName, Path.GetFileName(mcView.GetLocation_Scripts())))
                                {
                                    if (!mcView.GetIfScriptsAreToAppend() && Directory.Exists(currentFolderInfos.FullName))
                                    {
                                        clsApp.GetAppController.setAttributesToNormal(new DirectoryInfo(currentFolderInfos.FullName));

                                        Directory.Delete(currentFolderInfos.FullName, true);
                                    }

                                    currentFolderInfos.Create();

                                    File.Copy(mcView.GetLocation_Scripts(), Path.Combine(currentFolderInfos.FullName, Path.GetFileName(mcView.GetLocation_Scripts())), true);

                                    mcModRevision.Path_Scripts = Path.Combine(currentFolderInfos.FullName, Path.GetFileName(mcView.GetLocation_Scripts()));
                                    
                                    blnScriptsChanged = true;
                                }
                            } 
                            else if ((File.Exists(currentFolderInfos.FullName) || Directory.Exists(currentFolderInfos.FullName)) && mcView.GetLocation_Scripts() != currentFolderInfos.FullName)
                            {
                                //Met a jour le path si le contenu de la revision change, mais pas les scripts
                                mcModRevision.Path_Scripts = currentFolderInfos.FullName;
                            }

                            //Ajout dans Rev_AllScripts
                            List<string> lstExistingScripts;
                            List<string> lstTempScripts;
                            string[] lstScriptsToCopy;
                            string strNewScriptName = string.Empty;
                            int intNewScriptNumber = 1;
                            int intPlaceHolder = 0;

                            if (!string.IsNullOrEmpty(mcView.GetLocation_Scripts()) & blnScriptsChanged)
                            {
                                strRevAllScripts_Location = Path.Combine(sclsAppConfigs.GetRoot_INSTALLATIONS_ACTIVES, mcView.GetCeritarApplication_Name(), sclsAppConfigs.GetVersionNumberPrefix +  mcModRevision.Version.VersionNo.ToString(), sclsAppConfigs.GetRevisionAllScriptFolderName);

                                if (!Directory.Exists(strRevAllScripts_Location)) Directory.CreateDirectory(strRevAllScripts_Location);

                                if ((File.GetAttributes(mcModRevision.Path_Scripts) & FileAttributes.Directory) == FileAttributes.Directory)
                                {
                                    clsApp.GetAppController.setAttributesToNormal(new DirectoryInfo(mcModRevision.Path_Scripts));

                                    lstTempScripts = Directory.GetFiles(mcModRevision.Path_Scripts, "*.*", SearchOption.TopDirectoryOnly).OrderBy(f => f).ToList<string>();
                                }
                                else
                                {
                                    lstTempScripts = Directory.GetFiles(new FileInfo(mcModRevision.Path_Scripts).Directory.FullName, "*.*", SearchOption.TopDirectoryOnly).OrderBy(f => f).ToList<string>();
                                }

                                lstScriptsToCopy = lstTempScripts.ToArray();

                                lstExistingScripts = Directory.GetFiles(strRevAllScripts_Location).OrderBy(f => f).ToList<string>();

                                if (lstExistingScripts.Count > 0)
                                {
                                    intNewScriptNumber = Int32.Parse(new String(Path.GetFileName(lstExistingScripts[lstExistingScripts.Count - 1]).TakeWhile(Char.IsDigit).ToArray())) + 1;
                                }

                                clsApp.GetAppController.setAttributesToNormal(new DirectoryInfo(strRevAllScripts_Location));
                                
                                foreach (string strCurrentFile in lstScriptsToCopy)
                                {
                                    strNewScriptName = Path.GetFileName(strCurrentFile);

                                    if (strNewScriptName.Contains('_') && Int32.TryParse(strNewScriptName.Substring(0, strNewScriptName.IndexOf("_")), out intPlaceHolder))
                                    {
                                        strNewScriptName = strNewScriptName.Substring(strNewScriptName.IndexOf("_") + 1);
                                    }

                                    strNewScriptName = intNewScriptNumber.ToString("00") + "_" + strNewScriptName;

                                    File.Copy(strCurrentFile, Path.Combine(strRevAllScripts_Location, strNewScriptName), true);

                                    intNewScriptNumber++;
                                }
                            }

                            break;

                        case (int)ctr_Template.FolderType.Report:

                            //blnValidReturn = pfblnCopyAllReportsForClients(currentFolderInfos.FullName);

                            break;

                        case (int)ctr_Template.FolderType.Normal:

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

                            break;

                        default:
                            blnValidReturn = true;
                            break;
                    }

                    intPreviousFolderLevel = Int32.Parse(cSQLReader["HiCo_NodeLevel"].ToString());

                    if (!blnValidReturn) break;
                }

                //Copie des applications satellites dans leur répertoire spécifique dans les installations actives
                if (blnValidReturn && mcModRevision.LstSatelliteRevisions.Count > 0)
                {
                    string strDestinationFolder = string.Empty;

                    for (int intIndex = 0; intIndex < mcModRevision.LstSatelliteRevisions.Count; intIndex++)
                    {
                        strDestinationFolder = Path.Combine(sclsAppConfigs.GetRoot_INSTALLATIONS_ACTIVES,
                                                               mcModRevision.LstSatelliteRevisions[intIndex].CeritarSatelliteApp.Name,
                                                               mcModRevision.CeritarClient.CompanyName,
                                                               sclsAppConfigs.GetVersionNumberPrefix + mcModRevision.Version.VersionNo.ToString(),
                                                               sclsAppConfigs.GetRevisionNumberPrefix + mcModRevision.Revision_Number.ToString()
                                                              );

                        blnValidReturn = pfblnCopyAndSaveSatelliteLocation(mcModRevision.LstSatelliteRevisions[intIndex], strDestinationFolder, false);

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
            finally
            {
                if (cSQLReader != null) cSQLReader.Dispose();

                if (blnValidReturn) mcActionResult.SetValid(mintMSG_BuildSuccess);
            }

            return blnValidReturn;
        }

        private string pfstrGetRevisionFolderName()
        {
            string strFolderName = string.Empty;
            string strSatelliteExeLocation = string.Empty;
            string strRevisionFolderName_InfosSupp = string.Empty;

            strFolderName = sclsAppConfigs.GetRevisionNumberPrefix + mcView.GetRevisionNo().ToString();

            strRevisionFolderName_InfosSupp = (string.IsNullOrEmpty(mcModRevision.Path_Release) || mcModRevision.ExeIsExternalReport ? string.Empty : " Exe");
            strRevisionFolderName_InfosSupp = strRevisionFolderName_InfosSupp + (mcModRevision.ExeIsExternalReport || mcView.GetExeWithExternalReport() ? (string.IsNullOrEmpty(strRevisionFolderName_InfosSupp) ? string.Empty : " -") + " RPT" : string.Empty);
            strRevisionFolderName_InfosSupp = strRevisionFolderName_InfosSupp + (string.IsNullOrEmpty(mcModRevision.Path_Scripts) ? string.Empty : (string.IsNullOrEmpty(strRevisionFolderName_InfosSupp) ? string.Empty : " -") + " SCRIPTS");

            for (int intIndex = 0; intIndex < mcModRevision.LstSatelliteRevisions.Count; intIndex++)
            {
                if (!string.IsNullOrEmpty(mcModRevision.LstSatelliteRevisions[intIndex].Location_Exe))
                {
                    strSatelliteExeLocation = mcModRevision.LstSatelliteRevisions[intIndex].CeritarSatelliteApp.ExportFolderName;

                    strRevisionFolderName_InfosSupp = strRevisionFolderName_InfosSupp + (string.IsNullOrEmpty(strSatelliteExeLocation) ? string.Empty : (string.IsNullOrEmpty(strRevisionFolderName_InfosSupp) ? string.Empty : " -") + " " + strSatelliteExeLocation);

                    strSatelliteExeLocation = string.Empty;
                }
            }

            strFolderName = strFolderName + strRevisionFolderName_InfosSupp;

            return strFolderName;
        }

        public string str_GetRevisionFolderPath(int vintTemplate_NRI, string vstrVersion_No)
        {
            string strSQL = string.Empty;
            string strPath = string.Empty;
            SqlDataReader sqlRecord = null;

            mcModRevision = new mod_Rev_Revision();

            if (pfblnFeedModelWithView())
            {
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

                    strPath = strPath.Replace(sclsAppConfigs.GetRevisionNumberPrefix + "XX", pfstrGetRevisionFolderName());
                }
            }

            return strPath;
        }

        /// <summary>
        /// Copie les exécutables des applications satellite dans leur dossiers respectifs dans les installations actives.
        /// </summary>
        /// <param name="rcSatRevision">La classe qui représente l'application satellite à copier.
        ///                             Passer par référence, car on veut mettre à jour la location choisie par l'utilisateur avec celle sauvegardée.</param>
        /// <returns>Vrai si tout s'est bien passé.</returns>
        private bool pfblnCopyAndSaveSatelliteLocation(mod_SRe_SatelliteRevision rcSatRevision, string vstrDestinationFolder, bool vblnSaveLocationToDB = false)
        {
            bool blnValidReturn = true;
            DirectoryInfo currentFolderInfos = null;

            try
            {
                if (File.Exists(rcSatRevision.Location_Exe) || Directory.Exists(rcSatRevision.Location_Exe))
                {
                    currentFolderInfos = new DirectoryInfo(vstrDestinationFolder);

                    if (!Directory.Exists(currentFolderInfos.FullName)) currentFolderInfos.Create();

                    if ((File.GetAttributes(rcSatRevision.Location_Exe) & FileAttributes.Directory) == FileAttributes.Directory) //Executable is a folder
                    {
                        if (rcSatRevision.Location_Exe != currentFolderInfos.FullName)
                        {
                            clsApp.GetAppController.blnCopyFolderContent(rcSatRevision.Location_Exe, currentFolderInfos.FullName, true, true, SearchOption.TopDirectoryOnly, sclsAppConfigs.GetReleaseValidExtensions);

                            rcSatRevision.Location_Exe = currentFolderInfos.FullName;
                        }
                    }
                    else //Executable is a file
                    {
                        if (rcSatRevision.Location_Exe != Path.Combine(currentFolderInfos.FullName, Path.GetFileName(rcSatRevision.Location_Exe)))
                        {
                            File.Copy(rcSatRevision.Location_Exe, Path.Combine(currentFolderInfos.FullName, Path.GetFileName(rcSatRevision.Location_Exe)), true);

                            rcSatRevision.Location_Exe = Path.Combine(currentFolderInfos.FullName, Path.GetFileName(rcSatRevision.Location_Exe));
                        }
                    }
                }
                else if ((File.Exists(vstrDestinationFolder) || Directory.Exists(vstrDestinationFolder)) && rcSatRevision.Location_Exe != vstrDestinationFolder)
                {
                    rcSatRevision.Location_Exe = vstrDestinationFolder;
                }

                if (vblnSaveLocationToDB)
                {
                    rcSatRevision.SetcSQL = (mcSQL == null ? new clsSQL() : mcSQL);
                    rcSatRevision.DML_Action = rcSatRevision.SatRevision_NRI == 0 ? sclsConstants.DML_Mode.INSERT_MODE : sclsConstants.DML_Mode.UPDATE_MODE;

                    blnValidReturn = rcSatRevision.blnSave(); //Pour update le chemin ou la sauvegarde est faite

                    if (!blnValidReturn)
                    {
                        mcActionResult = mcModRevision.ActionResults;
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
            finally
            {
                if (blnValidReturn) mcActionResult.SetValid();
            }

            return blnValidReturn;
        }

        /// <summary>
        /// Efface toute la hierarchy de dossiers et son contenu du disque pour la révision données. Supprime le dossier racine également.
        /// </summary>
        /// <param name="vstrRevisionFolderRoot">Le chemin du répertoire à supprimer</param>
        /// <returns></returns>
        private bool pfblnDeleteRevisionHierarchy(string vstrRevisionFolderRoot)
        {
            bool blnValidReturn = false;
            System.Windows.Forms.DialogResult answer;

            try
            {
                answer = System.Windows.Forms.MessageBox.Show("Voulez-vous supprimer le répertoire à l'emplacement suivant : " + vstrRevisionFolderRoot, "Attention", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning, System.Windows.Forms.MessageBoxDefaultButton.Button2);
                
                if (answer == System.Windows.Forms.DialogResult.Yes)
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    //TODO UPADTE REV ALL SCRIPTS
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    startInfo.FileName = "cmd.exe";
                    startInfo.Arguments = @"/C RMDIR """ + vstrRevisionFolderRoot + @""" /S /Q";

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

        public bool blnExportRevisionKit(string vstrExportFolderLocation)
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
            string strCurrentScriptFolderLocation = string.Empty;

            try
            {
                strCeritarClientName = mcView.GetCeritarClient_Name();

                strNewZipFileLocation = Path.Combine(vstrExportFolderLocation, mcView.GetCeritarApplication_Name() + @" Revision " + mcView.GetRevisionNo().ToString() + " - " + strCeritarClientName + @".zip");

                //Create the new archive file and add all the folders to it.
                if (File.Exists(strNewZipFileLocation)) File.Delete(strNewZipFileLocation);

                using (ZipArchive newZipFile = ZipFile.Open(strNewZipFileLocation, ZipArchiveMode.Create))
                {
                    //Add the release folder with the report application to the zip archive.
                    //Get the release folder location to copy
                    strReleaseLocation = mcView.GetLocation_Release();

                    if (!string.IsNullOrEmpty(strReleaseLocation))
                    {
                        foreach (string strCurrentFileToCopyPath in Directory.GetFiles(strReleaseLocation, "*.*", SearchOption.AllDirectories))
                        {
                            newZipFile.CreateEntryFromFile(strCurrentFileToCopyPath, Path.Combine(sclsAppConfigs.GetReleaseFolderName, Path.GetFileName(strCurrentFileToCopyPath)));
                        }
                    }             

                    //Get the report folder location to copy 
                    if (mcView.GetExeIsExternalReport())
                    {
                        strReportLocation = mcView.GetLocation_Release();

                        //Add the external report application to the zip archive
                        newZipFile.CreateEntryFromFile(strReportLocation, Path.Combine(sclsAppConfigs.GetReleaseFolderName, Path.GetFileName(strReportLocation)));
                    }
                    
                    //Add every satellites applications to the zip archive.
                    List<Interfaces.structSatRevision> lstSatellites = mcView.GetRevisionSatelliteList();

                    foreach (Interfaces.structSatRevision structSat in lstSatellites)
                    {
                        //Get the executable folder location to copy (from the version kit or from the latest revision)
                        strLocationSatelliteExe = clsSQL.str_ADOSingleLookUp("TOP 1 SatRevision.SRe_Exe_Location", "Revision INNER JOIN SatRevision ON SatRevision.Rev_NRI = Revision.Rev_NRI", "Revision.Ver_NRI = " + mcView.GetVersion_NRI() + " AND SatRevision.SRe_Exe_Location IS NOT NULL AND SatRevision.CSA_NRI = " + structSat.intCeritarSatelliteApp_NRI + " ORDER BY Revision.Rev_No DESC");

                        strLocationSatelliteExe = strLocationSatelliteExe == string.Empty ? structSat.strLocationSatelliteExe : strLocationSatelliteExe;

                        if (structSat.blnExeIsFolder && Directory.Exists(strLocationSatelliteExe))
                        {
                            foreach (string strCurrentFileToCopyPath in Directory.GetFiles(strLocationSatelliteExe, "*.*", SearchOption.AllDirectories))
                            {
                                newZipFile.CreateEntryFromFile(strCurrentFileToCopyPath, Path.Combine(structSat.strExportFolderName, Path.GetFileName(strCurrentFileToCopyPath)));
                            }
                        }
                        else if (File.Exists(strLocationSatelliteExe))
                        {
                            newZipFile.CreateEntryFromFile(strLocationSatelliteExe, Path.Combine(structSat.strExportFolderName, Path.GetFileName(strLocationSatelliteExe)));
                        }
                    }

                    //Add all scripts folder to the zip archive.
                    strCurrentScriptFolderLocation = mcView.GetLocation_Scripts();
            
                    if (!string.IsNullOrEmpty(strCurrentScriptFolderLocation))
                    {
                        if ((File.GetAttributes(strCurrentScriptFolderLocation) & FileAttributes.Directory) == FileAttributes.Directory)
                        {
                            foreach (string strCurrentFileToCopyPath in Directory.GetFiles(strCurrentScriptFolderLocation, "*.*", SearchOption.TopDirectoryOnly))
                            {
                                newZipFile.CreateEntryFromFile(strCurrentFileToCopyPath, Path.Combine(sclsAppConfigs.GetScriptsFolderName, Path.GetFileName(strCurrentFileToCopyPath)));
                            }
                        }
                        else
                        {
                            newZipFile.CreateEntryFromFile(strCurrentScriptFolderLocation, Path.Combine(sclsAppConfigs.GetScriptsFolderName, Path.GetFileName(strCurrentScriptFolderLocation)), CompressionLevel.NoCompression);
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

#region "SQL Queries"

        public string strGetDataLoad_SQL(int vintVersion_NRI, int vintRevision_NRI)
        {
            string strSQL = string.Empty;
            string strNewRevisionNo = string.Empty;

            if (mcView.GetDML_Action() == sclsConstants.DML_Mode.INSERT_MODE)
            {
                strNewRevisionNo = clsSQL.str_ADOSingleLookUp("MAX(ISNULL(Revision.Rev_No, 0)) + 1", "Version LEFT JOIN Revision ON Revision.Ver_NRI = Version.Ver_NRI", "Version.Ver_NRI = " + vintVersion_NRI);
            }

            strSQL = strSQL + " SELECT Version.Ver_No, " + Environment.NewLine;
            strSQL = strSQL + "        Revision.Rev_TS, " + Environment.NewLine;
            strSQL = strSQL + "        Revision.Rev_DtCreation, " + Environment.NewLine;
            strSQL = strSQL + "        Revision.Tpl_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        Revision.Rev_Location_Exe, " + Environment.NewLine;
            strSQL = strSQL + "        Revision.Rev_Location_Scripts, " + Environment.NewLine;
            strSQL = strSQL + "        Revision.Rev_ExeIsReport, " + Environment.NewLine;
            strSQL = strSQL + "        Revision.Rev_ExeWithReport, " + Environment.NewLine;
            strSQL = strSQL + "        Revision.Rev_CreatedBy, " + Environment.NewLine;
            strSQL = strSQL + "        RevisionNo = CASE WHEN Revision.Rev_NRI IS NULL THEN " + clsApp.GetAppController.str_FixStringForSQL(strNewRevisionNo) + " ELSE Revision.Rev_No END, " + Environment.NewLine;
            strSQL = strSQL + "        CerApp.CeA_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        CerApp.CeA_Name, " + Environment.NewLine;
            strSQL = strSQL + "        CerApp.CeA_ExternalRPTAppName, " + Environment.NewLine;
            strSQL = strSQL + "        CerClient.CeC_NRI " + Environment.NewLine;
                
            strSQL = strSQL + " FROM Version " + Environment.NewLine;

            strSQL = strSQL + "     LEFT JOIN Revision " + Environment.NewLine;
            strSQL = strSQL + "         INNER JOIN CerClient ON CerClient.CeC_NRI = Revision.CeC_NRI " + Environment.NewLine;
            strSQL = strSQL + "     ON Revision.Ver_NRI = Version.Ver_NRI " + Environment.NewLine;
            strSQL = strSQL + "     AND Revision.Rev_NRI = " + vintRevision_NRI + Environment.NewLine;

            strSQL = strSQL + "     INNER JOIN CerApp ON CerApp.CeA_NRI = Version.CeA_NRI " + Environment.NewLine;
            //strSQL = strSQL + "     INNER JOIN TTUser ON TTUser.TTU_NRI = Revision.TTU_NRI " + Environment.NewLine;

            strSQL = strSQL + " WHERE Version.Ver_NRI = " + vintVersion_NRI + Environment.NewLine;

            return strSQL;
        }

        public string strGetListe_RevisionModifications_SQL(int vintRevision_NRI)
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT Action = '" + sclsConstants.DML_Mode.NO_MODE + "', " + Environment.NewLine;
            strSQL = strSQL + "        RevModifs.RevM_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        RevModifs.RevM_ChangeDesc " + Environment.NewLine;

            strSQL = strSQL + " FROM RevModifs " + Environment.NewLine;

            strSQL = strSQL + " WHERE RevModifs.Rev_NRI = " + vintRevision_NRI + Environment.NewLine;

            return strSQL;
        }

        public string strGetTemplates_SQL()
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT Template.Tpl_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        Template.Tpl_Name " + Environment.NewLine;

            strSQL = strSQL + " FROM Template " + Environment.NewLine;

            strSQL = strSQL + "     INNER JOIN Version ON Version.Ver_NRI = " + mcView.GetVersion_NRI() + Environment.NewLine;

            strSQL = strSQL + " WHERE Template.TeT_NRI = " + (int)ctr_Template.TemplateType.REVISION + Environment.NewLine;
            strSQL = strSQL + "   AND Template.CeA_NRI = Version.CeA_NRI " + Environment.NewLine;

            strSQL = strSQL + " ORDER BY Template.Tpl_ByDefault DESC, Template.Tpl_Name " + Environment.NewLine;

            return strSQL;
        }

        public string strGetClients_SQL()
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT CerClient.CeC_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        CerClient.CeC_Name " + Environment.NewLine;

            strSQL = strSQL + " FROM Version " + Environment.NewLine;

            strSQL = strSQL + "     INNER JOIN ClientAppVersion " + Environment.NewLine;
            strSQL = strSQL + "          INNER JOIN CerClient ON CerClient.CeC_NRI = ClientAppVersion.CeC_NRI " + Environment.NewLine;
            strSQL = strSQL + "     ON ClientAppVersion.Ver_NRI = Version.Ver_NRI " + Environment.NewLine;

            strSQL = strSQL + " WHERE Version.Ver_NRI = " + mcView.GetVersion_NRI() + Environment.NewLine;

            strSQL = strSQL + " ORDER BY CerClient.CeC_Name " + Environment.NewLine;

            return strSQL;
        }

        public string strGetListe_SatelliteApps_SQL()
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT Action = CASE WHEN SatRevision.SRe_NRI IS NULL THEN '" + sclsConstants.DML_Mode.NO_MODE + "' ELSE '" + sclsConstants.DML_Mode.UPDATE_MODE + "' END," + Environment.NewLine;
            strSQL = strSQL + "        CerSatApp.CSA_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        CerSatApp.CSA_Name, " + Environment.NewLine;
            strSQL = strSQL + "        SatRevision.SRe_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        SatRevision.SRe_Exe_Location, " + Environment.NewLine;
            strSQL = strSQL + "        CerSatApp.CSA_ExeIsFolder, " + Environment.NewLine;
            strSQL = strSQL + "        CerSatApp.CSA_KitFolderName " + Environment.NewLine;

            strSQL = strSQL + " FROM Version " + Environment.NewLine;
          
            strSQL = strSQL + "     LEFT JOIN Revision ON Revision.Rev_NRI = " + mcView.GetRevision_NRI() + Environment.NewLine;
   
            strSQL = strSQL + "     INNER JOIN ClientSatVersion  " + Environment.NewLine;
            strSQL = strSQL + "         INNER JOIN CerSatApp ON CerSatApp.CSA_NRI = ClientSatVersion.CSA_NRI  " + Environment.NewLine;
            strSQL = strSQL + " 	ON ClientSatVersion.Ver_NRI = Version.Ver_NRI  " + Environment.NewLine;
            strSQL = strSQL + "    AND ClientSatVersion.CSA_NRI = CerSatApp.CSA_NRI  " + Environment.NewLine;
            strSQL = strSQL + "    AND ClientSatVersion.CeC_NRI = " + mcView.GetCeritarClient_NRI() + Environment.NewLine;

            strSQL = strSQL + "     LEFT JOIN SatRevision ON SatRevision.Rev_NRI = Revision.Rev_NRI AND SatRevision.CSA_NRI = CerSatApp.CSA_NRI " + Environment.NewLine;

            strSQL = strSQL + " WHERE Version.Ver_NRI = " + mcView.GetVersion_NRI() + Environment.NewLine;

            strSQL = strSQL + " ORDER BY CerSatApp.CSA_Name " + Environment.NewLine;

            return strSQL;
        }

#endregion
        

    }
}
