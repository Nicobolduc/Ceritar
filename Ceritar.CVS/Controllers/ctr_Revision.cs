﻿using System;
using System.IO;
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

        public enum ErrorCode_Rev
        {
            MODIFICATION_LIST_MANDATORY = 1,
            REVISION_NUMBER_MANDATORY = 2,
            TEMPLATE_MANDATORY = 3,
            VERSION_MANDATORY = 4,
            CERITAR_CLIENT_MANDATORY = 5,
            EXE_OR_SCRIPT_MANDATORY = 6,
            CANT_DELETE_NOT_LAST_REVISION = 7,
            REPORT_EXE_MANDATORY = 8
        }

        public ctr_Revision(Interfaces.IRevision rView)
        {
            mcModRevision = new mod_Rev_Revision();
            
            mcView = rView;

            mcActionResult = new clsActionResults();
        }

        public clsActionResults Validate()
        {
            mcModRevision = new mod_Rev_Revision();

            try
            {
                mcModRevision.DML_Action = mcView.GetDML_Action();
                mcModRevision.Revision_Number = mcView.GetRevisionNo();
                mcModRevision.CreationDate = mcView.GetCreationDate();
                mcModRevision.LstModifications = mcView.GetModificationsList();
                mcModRevision.Path_Release = mcView.GetLocation_Release();
                mcModRevision.Path_Scripts = mcView.GetLocation_Scripts();
                mcModRevision.Revision_NRI = mcView.GetRevision_NRI();
                mcModRevision.Revision_TS = mcView.GetRevision_TS();
                mcModRevision.ExeIsExternalReport = mcView.GetExeIsExternalReport();

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

                mcModRevision.SatelliteApp = new mod_CSA_CeritarSatelliteApp();
                mcModRevision.SatelliteApp.CeritarSatelliteApp_NRI = mcView.GetSelectedSatellitteApp_NRI();
                mcModRevision.SatelliteApp.Name = mcView.GetSelectedSatellitteApp_Name();


                mcActionResult = mcModRevision.Validate();
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
                    mcModRevision.SetcSQL = mcSQL;

                    blnValidReturn = mcModRevision.blnSave();

                    mcActionResult = mcModRevision.ActionResults;
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
            string strSatelliteApp_RevisionLocation = string.Empty;
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

                            strRevisionFolderRoot = Path.Combine(currentFolderInfos.FullName, strFolderName);
                                                        
                            break;

                        case (int)ctr_Template.FolderType.Revision_Number:

                            strFolderName = sclsAppConfigs.GetRevisionNumberPrefix + mcView.GetRevisionNo().ToString();

                            strRevisionFolderRoot = Path.Combine(currentFolderInfos.FullName, strFolderName);

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

                    switch (Int32.Parse(cSQLReader["FoT_NRI"].ToString()))
                    {
                        case (int)ctr_Template.FolderType.Release:

                            if (!string.IsNullOrEmpty(mcModRevision.SatelliteApp.Name))
                            {
                                strSatelliteApp_RevisionLocation = Path.Combine(sclsAppConfigs.GetRoot_INSTALLATIONS_ACTIVES +
                                                                                (sclsAppConfigs.GetRoot_INSTALLATIONS_ACTIVES.Substring(sclsAppConfigs.GetRoot_INSTALLATIONS_ACTIVES.Length - 1, 1) == "\\" ? "" : "\\"),
                                                                                mcModRevision.SatelliteApp.Name,
                                                                                mcModRevision.CeritarClient.CompanyName,
                                                                                sclsAppConfigs.GetVersionNumberPrefix + mcModRevision.Version.VersionNo.ToString(),
                                                                                sclsAppConfigs.GetRevisionNumberPrefix + mcModRevision.Revision_Number
                                                                               );
                            }

                            if ((File.Exists(mcView.GetLocation_Release()) || Directory.Exists(mcView.GetLocation_Release())))
                            {
                                if ((File.GetAttributes(mcView.GetLocation_Release()) & FileAttributes.Directory) == FileAttributes.Directory)
                                {
                                    if (mcView.GetLocation_Release() != currentFolderInfos.FullName)
                                    {
                                        if (Directory.Exists(currentFolderInfos.FullName)) Directory.Delete(currentFolderInfos.FullName, true);

                                        currentFolderInfos.Create();

                                        blnValidReturn = clsApp.GetAppController.blnCopyFolderContent(mcView.GetLocation_Release(), currentFolderInfos.FullName, true, false, SearchOption.TopDirectoryOnly, sclsAppConfigs.GetReleaseValidExtensions);

                                        /*//TODO: Use checkbox in the screen
                                        string[] reportExe = Directory.GetFiles(currentFolderInfos.FullName, "*RPT.exe", SearchOption.TopDirectoryOnly);
                                        
                                        if (reportExe.Length > 0) File.Delete(reportExe[0]);*/

                                        mcModRevision.Path_Release = currentFolderInfos.FullName;

                                        if (blnValidReturn & !string.IsNullOrEmpty(strSatelliteApp_RevisionLocation))
                                        {
                                            //Copy the sattellite application's revision in the appropriate directory
                                            if (Directory.Exists(strSatelliteApp_RevisionLocation)) Directory.Delete(strSatelliteApp_RevisionLocation, true);

                                            Directory.CreateDirectory(strSatelliteApp_RevisionLocation);

                                            blnValidReturn = clsApp.GetAppController.blnCopyFolderContent(currentFolderInfos.FullName, strSatelliteApp_RevisionLocation, true, false, SearchOption.TopDirectoryOnly, sclsAppConfigs.GetReleaseValidExtensions);
                                        }
                                    }
                                }
                                else if (mcView.GetLocation_Release() != Path.Combine(currentFolderInfos.FullName, Path.GetFileName(mcView.GetLocation_Release())))
                                {
                                    if (Directory.Exists(currentFolderInfos.FullName)) Directory.Delete(currentFolderInfos.FullName, true);

                                    currentFolderInfos.Create();

                                    File.Copy(mcView.GetLocation_Release(), Path.Combine(currentFolderInfos.FullName, Path.GetFileName(mcView.GetLocation_Release())), true);

                                    mcModRevision.Path_Release = Path.Combine(currentFolderInfos.FullName, Path.GetFileName(mcView.GetLocation_Release()));

                                    if (!string.IsNullOrEmpty(strSatelliteApp_RevisionLocation))
                                    {
                                        //Copy the sattellite application's revision in the appropriate directory
                                        if (Directory.Exists(strSatelliteApp_RevisionLocation)) Directory.Delete(strSatelliteApp_RevisionLocation, true);

                                        Directory.CreateDirectory(strSatelliteApp_RevisionLocation);

                                        File.Copy(mcView.GetLocation_Release(), Path.Combine(strSatelliteApp_RevisionLocation, Path.GetFileName(mcView.GetLocation_Release())), true);
                                    }
                                }
                            }
                            else
                            {
                                blnValidReturn = true;
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
                                        clsApp.GetAppController.setAttributesToNormal(new DirectoryInfo(currentFolderInfos.FullName));

                                        if (Directory.Exists(currentFolderInfos.FullName)) Directory.Delete(currentFolderInfos.FullName, true);

                                        currentFolderInfos.Create();

                                        clsApp.GetAppController.blnCopyFolderContent(mcView.GetLocation_Scripts(), currentFolderInfos.FullName, true, true);

                                        mcModRevision.Path_Scripts = currentFolderInfos.FullName;

                                        blnScriptsChanged = true;
                                    }
                                }
                                else if (mcView.GetLocation_Scripts() != Path.Combine(currentFolderInfos.FullName, Path.GetFileName(mcView.GetLocation_Scripts())))
                                {
                                    if (Directory.Exists(currentFolderInfos.FullName))
                                    {
                                        clsApp.GetAppController.setAttributesToNormal(new DirectoryInfo(currentFolderInfos.FullName));

                                        Directory.Delete(currentFolderInfos.FullName, true);
                                    }

                                    currentFolderInfos.Create();

                                    File.Copy(mcView.GetLocation_Scripts(), Path.Combine(currentFolderInfos.FullName, Path.GetFileName(mcView.GetLocation_Scripts())));

                                    mcModRevision.Path_Scripts = Path.Combine(currentFolderInfos.FullName, Path.GetFileName(mcView.GetLocation_Scripts()));

                                    blnScriptsChanged = true;
                                }
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
                                
                                foreach (string strCurrentFile in lstScriptsToCopy)
                                {
                                    strNewScriptName = Path.GetFileName(strCurrentFile);

                                    if (Int32.TryParse(strNewScriptName.Substring(0, strNewScriptName.IndexOf("_")), out intPlaceHolder))
                                    {
                                        strNewScriptName = strNewScriptName.Substring(strNewScriptName.IndexOf("_") + 1);
                                    }

                                    strNewScriptName = intNewScriptNumber.ToString("00") + "_" + strNewScriptName;

                                    File.Copy(strCurrentFile, Path.Combine(strRevAllScripts_Location, strNewScriptName), false);

                                    intNewScriptNumber++;
                                }
                            }

                            break;

                        case (int)ctr_Template.FolderType.Report:

                            //blnValidReturn = pfblnCopyAllReportsForClients(currentFolderInfos.FullName);

                            break;

                        default:
                            blnValidReturn = true;
                            break;
                    }

                    intPreviousFolderLevel = Int32.Parse(cSQLReader["HiCo_NodeLevel"].ToString());

                    if (!blnValidReturn) break;
                }

                mcActionResult.SetValid();
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

            try
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();

                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = @"/C RMDIR """ + vstrRevisionFolderRoot + @""" /S /Q";

                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();
                
                blnValidReturn = true;
            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
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
            strSQL = strSQL + "        RevisionNo = CASE WHEN Revision.Rev_NRI IS NULL THEN " + clsApp.GetAppController.str_FixStringForSQL(strNewRevisionNo) + " ELSE Revision.Rev_No END, " + Environment.NewLine;
            strSQL = strSQL + "        CerApp.CeA_Name, " + Environment.NewLine;
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

            strSQL = strSQL + " SELECT Action = '" + sclsConstants.DML_Mode.NO_MODE + "', " + Environment.NewLine;
            strSQL = strSQL + "        CerSatApp.CSA_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        CerSatApp.CSA_Name, " + Environment.NewLine;
            strSQL = strSQL + "        SelCol = CASE WHEN Revision.CSA_NRI = CerSatApp.CSA_NRI THEN 1 ELSE 0 END " + Environment.NewLine;

            strSQL = strSQL + " FROM Version " + Environment.NewLine;

            strSQL = strSQL + "     LEFT JOIN Revision ON Revision.Rev_NRI = " + mcView.GetRevision_NRI() + Environment.NewLine;
            strSQL = strSQL + "     INNER JOIN ClientSatVersion  " + Environment.NewLine;
            strSQL = strSQL + "         INNER JOIN CerSatApp ON CerSatApp.CSA_NRI = ClientSatVersion.CSA_NRI  " + Environment.NewLine;
            strSQL = strSQL + " 	ON ClientSatVersion.Ver_NRI = Version.Ver_NRI  " + Environment.NewLine;
            strSQL = strSQL + "    AND ClientSatVersion.CSA_NRI = CerSatApp.CSA_NRI  " + Environment.NewLine;
            strSQL = strSQL + "    AND ClientSatVersion.CeC_NRI = " + mcView.GetCeritarClient_NRI() + Environment.NewLine;

            strSQL = strSQL + " WHERE Version.Ver_NRI = " + mcView.GetVersion_NRI() + Environment.NewLine;

            strSQL = strSQL + " ORDER BY CerSatApp.CSA_Name " + Environment.NewLine;

            return strSQL;
        }

#endregion
        

    }
}
