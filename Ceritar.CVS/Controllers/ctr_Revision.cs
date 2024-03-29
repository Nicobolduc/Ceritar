﻿using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using Ceritar.TT3LightDLL.Static_Classes;
using Ceritar.TT3LightDLL.Classes;
using Ceritar.CVS.Models.Module_ActivesInstallations;
using Ceritar.CVS.Models.Module_Configuration;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using Ceritar.CVS.Controllers.Interfaces;

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
        private clsTTSQL mcSQL;

        //Messages
        private const int mintMSG_BuildSuccess = 37;
        private const int mintMSG_AreYouSureToDelete = 49;
        private const int mintMSG_ManyDirectoryFound = 54;

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
            CREATED_BY_MANDATORY = 9,
            CLIENT_NAME_MANDATORY = 10,
            CLIENTS_LIST_MANDATORY = 11
        }

        public ctr_Revision(Interfaces.IRevision rView)
        {
            mcModRevision = new mod_Rev_Revision();

            mcView = rView;
        }

        public clsActionResults mcActionResult
        {
            get { return mcModRevision.ActionResults; }
        }

        private bool pfblnFeedModelWithView()
        {
            bool blnValidReturn = false;
            List<Interfaces.structSatRevision> lstStructSRe;
            mod_SRe_SatelliteRevision cSRe;
            List<Interfaces.structClientAppRevision> lstStructCAR;
            mod_CAR_ClientAppRevision cCAR;
            List<int> lstSRe_NRI = new List<int>();

            try
            {
                mcModRevision = new mod_Rev_Revision();

                mcModRevision.DML_Action = mcView.GetDML_Action();
                mcModRevision.Revision_Number = mcView.GetRevisionNo();
                mcModRevision.CreationDate = mcView.GetCreationDate();
                mcModRevision.CreatedBy = mcView.GetCreatedBy();
                mcModRevision.LstModifications = mcView.GetModificationsList();
                mcModRevision.Path_Release = mcView.GetLocation_Release();
                mcModRevision.Revision_NRI = mcView.GetRevision_NRI();
                mcModRevision.Revision_TS = mcView.GetRevision_TS();
                mcModRevision.ExeIsExternalReport = mcView.GetExeIsExternalReport();
                mcModRevision.ExeWithExternalReport = mcView.GetExeWithExternalReport();
                mcModRevision.PreparationMode = mcView.IsPreparationMode();
                mcModRevision.Note = mcView.GetNote();

                mcModRevision.Version = new mod_Ver_Version();
                mcModRevision.Version.CerApplication = new mod_CeA_CeritarApplication();
                mcModRevision.Version.CerApplication.CeritarApplication_NRI = mcView.GetCeritarApplication_NRI();
                mcModRevision.Version.CerApplication.Name = mcView.GetCeritarApplication_Name();
                mcModRevision.Version.CerApplication.ExternalReportAppName = clsTTSQL.str_ADOSingleLookUp("CeA_ExternalRPTAppName", "CerApp", "CeA_NRI = " + mcModRevision.Version.CerApplication.CeritarApplication_NRI);
                mcModRevision.Version.CerApplication.AutoGenRevisionNoScript = bool.Parse(clsTTSQL.str_ADOSingleLookUp("CeA_AutoGenRevisionNoScript", "CerApp", "CeA_NRI = " + mcModRevision.Version.CerApplication.CeritarApplication_NRI));

                if (!mcView.IsPreparationMode())
                    mcModRevision.Path_Scripts = (string.IsNullOrEmpty(mcView.GetLocation_Scripts()) & mcModRevision.Version.CerApplication.AutoGenRevisionNoScript ? "placeHolder" : mcView.GetLocation_Scripts());

                lstStructCAR = new List<structClientAppRevision>();
                lstStructCAR.Add(mcView.GetSelectedClient());

                foreach (structClientAppRevision structCAV in lstStructCAR)
                {
                    cCAR = new mod_CAR_ClientAppRevision();
                    cCAR.DML_Action = structCAV.Action;
                    cCAR.ClientAppRevision_NRI = structCAV.intClientAppRevision_NRI;
                    cCAR.ClientAppRevision_TS = structCAV.intClientAppRevision_TS;

                    cCAR.CeritarClient = new Models.Module_Configuration.mod_CeC_CeritarClient();
                    cCAR.CeritarClient.CeritarClient_NRI = structCAV.intCeritarClient_NRI;

                    mcModRevision.LstClientsUsing.Add(cCAR);
                }

                //mcModRevision.CreatedByUser = new mod_TTU_User();
                //mcModRevision.CreatedByUser.

                mcModRevision.TemplateSource = new Models.Module_Template.mod_Tpl_HierarchyTemplate();
                mcModRevision.TemplateSource.Template_NRI = mcView.GetTemplateSource_NRI();

                mcModRevision.Version.Version_NRI = mcView.GetVersion_NRI();
                mcModRevision.Version.VersionNo = mcView.GetVersionNo();

                lstStructSRe = mcView.GetRevisionSatelliteList();

                foreach (Interfaces.structSatRevision structSRe in lstStructSRe)
                {
                    cSRe = new mod_SRe_SatelliteRevision();
                    cSRe.DML_Action = structSRe.Action;
                    cSRe.SatRevision_NRI = structSRe.intSatRevision_NRI;
                    cSRe.Location_Exe = structSRe.strLocationSatelliteExe;
                    cSRe.DelaySave_Location_Exe = true;
                    cSRe.ClientSatVersion_NRI = structSRe.intClientSatVersion_NRI;
                    cSRe.CeritarClient_NRI_Spec = structSRe.inCeritarClient_NRI_Specific;

                    cSRe.Revision = new mod_Rev_Revision();
                    cSRe.Revision.Revision_NRI = mcView.GetRevision_NRI();

                    cSRe.CeritarSatelliteApp = new Models.Module_Configuration.mod_CSA_CeritarSatelliteApp();
                    cSRe.CeritarSatelliteApp.Name = structSRe.strCeritarSatelliteApp_Name;
                    cSRe.CeritarSatelliteApp.CeritarSatelliteApp_NRI = structSRe.intCeritarSatelliteApp_NRI;
                    cSRe.CeritarSatelliteApp.ExeIsFolder = structSRe.blnExeIsFolder;
                    cSRe.CeritarSatelliteApp.ExportFolderName = structSRe.strExportFolderName;
                    cSRe.CeritarSatelliteApp.ExePerCustomer = structSRe.blnExePerCustomer;

                    mcModRevision.LstSatelliteRevisions.Add(cSRe);

                    lstSRe_NRI.Add(cSRe.SatRevision_NRI);
                }

                if (mcModRevision.DML_Action == sclsConstants.DML_Mode.DELETE_MODE)
                {
                    SqlDataReader sqlRecord = null;

                    sqlRecord = clsTTSQL.ADOSelect(this.strGetSatelliteApps_SQL(mcView.GetRevision_NRI(), false));

                    while (sqlRecord.Read())
                    {
                        cSRe = new mod_SRe_SatelliteRevision();
                        cSRe.DML_Action = sclsConstants.DML_Mode.DELETE_MODE;
                        cSRe.SatRevision_NRI = Int32.Parse(sqlRecord["SRe_NRI"].ToString());
                        cSRe.Location_Exe = sqlRecord["SRe_Exe_Location"].ToString();
                        cSRe.DelaySave_Location_Exe = true;
                        if (sqlRecord["CeC_NRI_Spec"] != DBNull.Value) cSRe.CeritarClient_NRI_Spec = Int32.Parse(sqlRecord["CeC_NRI_Spec"].ToString());

                        cSRe.Revision = new mod_Rev_Revision();
                        cSRe.Revision.Revision_NRI = mcView.GetRevision_NRI();
                            
                        cSRe.CeritarSatelliteApp = new Models.Module_Configuration.mod_CSA_CeritarSatelliteApp();
                        cSRe.CeritarSatelliteApp.CeritarSatelliteApp_NRI = Int32.Parse(sqlRecord["CSA_NRI"].ToString());
                        cSRe.CeritarSatelliteApp.Name = sqlRecord["CSA_Name"].ToString();
                        cSRe.CeritarSatelliteApp.ExePerCustomer = bool.Parse(sqlRecord["CSA_ExePerCustomer"].ToString());
                        cSRe.CeritarSatelliteApp.ExeIsFolder = Convert.ToBoolean(sqlRecord["CSA_ExeIsFolder"].ToString());

                        if (!lstSRe_NRI.Contains(cSRe.SatRevision_NRI))
                            mcModRevision.LstSatelliteRevisions.Add(cSRe);
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

        public clsActionResults Validate()
        {
            bool blnValidReturn = false;
            mcModRevision = new mod_Rev_Revision();

            try
            {
                blnValidReturn = pfblnFeedModelWithView();

                if (blnValidReturn)
                {
                    mcModRevision.Validate();
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
                mcSQL = new clsTTSQL();

                if (mcSQL.bln_BeginTransaction())
                {
                    mcModRevision.SetcSQL = mcSQL;

                    blnValidReturn = mcModRevision.blnSave();

                    if (blnValidReturn & mcModRevision.ActionResults.IsValid & !mcView.IsPreparationMode())
                    {
                        blnValidReturn = blnBuildRevisionHierarchy(mcModRevision.TemplateSource.Template_NRI);

                        if (blnValidReturn)
                        {
                            blnValidReturn = mcModRevision.blnUpdateFilesLocations();
                        }
                    }

                    intSuccessMessage = mcActionResult.SuccessMessage_NRI;

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

        public bool blnDeleteSatelliteRevision(int vintSRe_NRI)
        {
            bool blnValidReturn = false;
            string strFolderPath = string.Empty;
            string strRevisionFolderPath = string.Empty;
            string strNewRevisionFolderName = string.Empty;

            try
            {
                mcSQL = new clsTTSQL();
                mcModRevision = new mod_Rev_Revision();

                strFolderPath = mcSQL.str_ADOSingleLookUp_Trans("SRe_Exe_Location", "SatRevision", "SRe_NRI = " + vintSRe_NRI);

                if (!mcSQL.bln_BeginTransaction())
                { }
                else if (!mcSQL.bln_ADODelete("SatRevision", "SRe_NRI = " + vintSRe_NRI))
                { }
                else
                {
                    if ((File.GetAttributes(strFolderPath) & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        strFolderPath = new DirectoryInfo(strFolderPath).FullName;
                        strRevisionFolderPath = new DirectoryInfo(strFolderPath).Parent.FullName;
                    }
                    else
                    {
                        strFolderPath = new FileInfo(strFolderPath).Directory.FullName;
                        strRevisionFolderPath = new FileInfo(strFolderPath).Directory.FullName;
                    }

                    pfblnFeedModelWithView();

                    foreach (mod_SRe_SatelliteRevision cSRe in mcModRevision.LstSatelliteRevisions)
                    {
                        if (cSRe.SatRevision_NRI == vintSRe_NRI)
                        {
                            pfblnDeleteDirectory(strGetSatelliteFolderPathName(cSRe), true);

                            mcModRevision.LstSatelliteRevisions.Remove(cSRe);

                            break;
                        }
                    }

                    strNewRevisionFolderName = pfstrGetRevisionFolderName();

                    blnValidReturn = false;

                    if (!mcSQL.bln_ADOExecute("UPDATE Revision SET Rev_Location_Exe = REPLACE(Rev_Location_Exe, " + clsTTApp.GetAppController.str_FixStringForSQL(new DirectoryInfo(strFolderPath).Parent.Name) + ", " + clsTTApp.GetAppController.str_FixStringForSQL(strNewRevisionFolderName) + ") WHERE Revision.Rev_NRI = " + mcView.GetRevision_NRI()))
                    { }
                    else if (!mcSQL.bln_ADOExecute("UPDATE Revision SET Rev_Location_Scripts = REPLACE(Rev_Location_Scripts, " + clsTTApp.GetAppController.str_FixStringForSQL(new DirectoryInfo(strFolderPath).Parent.Name) + ", " + clsTTApp.GetAppController.str_FixStringForSQL(strNewRevisionFolderName) + ") WHERE Revision.Rev_NRI = " + mcView.GetRevision_NRI()))
                    { }
                    else if (!mcSQL.bln_ADOExecute("UPDATE SatRevision SET SRe_Exe_Location = REPLACE(SRe_Exe_Location, " + clsTTApp.GetAppController.str_FixStringForSQL(new DirectoryInfo(strFolderPath).Parent.Name) + ", " + clsTTApp.GetAppController.str_FixStringForSQL(strNewRevisionFolderName) + ") WHERE SatRevision.Rev_NRI = " + mcView.GetRevision_NRI()))
                    { }
                    else
                    {
                        blnValidReturn = true;
                    }

                    Microsoft.VisualBasic.FileIO.FileSystem.DeleteDirectory(strFolderPath, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin, Microsoft.VisualBasic.FileIO.UICancelOption.ThrowException);

                    if (strRevisionFolderPath != Path.Combine(new DirectoryInfo(strRevisionFolderPath).Parent.FullName, strNewRevisionFolderName))
                        Directory.Move(strRevisionFolderPath, Path.Combine(new DirectoryInfo(strRevisionFolderPath).Parent.FullName, strNewRevisionFolderName));
                }
            }
            catch (System.OperationCanceledException)
            {
                blnValidReturn = false;
            }
            catch (System.IO.IOException exUA)
            {
                blnValidReturn = false;
                mcActionResult.SetInvalid(sclsConstants.Validation_Message.PATH_ACCESS_DENIED, clsActionResults.BaseErrorCode.UNHANDLED_VALIDATION, exUA.Message);
            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }
            finally
            {
                mcSQL.bln_EndTransaction(blnValidReturn);
            }

            return blnValidReturn;
        }

        public bool blnBuildRevisionHierarchy(int vintTemplate_NRI)
        {
            bool blnValidReturn = false;
            bool blnScriptsChanged = false;
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
            strSQL = strSQL + " 		   CASE WHEN FoT_NRI = " + (int)ctr_Template.FolderType.System + " THEN CAST(0 AS varbinary(max)) ELSE CAST(HierarchyComp.HiCo_NRI AS varbinary(max)) END AS Level " + Environment.NewLine;
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

                cSQLReader = clsTTSQL.ADOSelect(strSQL);

                while (cSQLReader.Read())
                {
                    switch (Int32.Parse(cSQLReader["FoT_NRI"].ToString()))
                    {
                        case (int)ctr_Template.FolderType.Version_Number:

                            strFolderName = sclsAppConfigs.GetVersionNumberPrefix + mcView.GetVersionNo().ToString();

                            strRevisionFolderRoot = Path.Combine(currentFolderInfos.FullName, strFolderName);

                            break;

                        case (int)ctr_Template.FolderType.Revision_Number:

                            string[] strLstCurrentDirectory = Directory.GetDirectories(currentFolderInfos.FullName, sclsAppConfigs.GetRevisionNumberPrefix + mcView.GetRevisionNo().ToString() + "*");

                            strFolderName = pfstrGetRevisionFolderName();

                            strRevisionFolderRoot = Path.Combine(currentFolderInfos.FullName, strFolderName);

                            if (strLstCurrentDirectory.Length == 1 && !strLstCurrentDirectory[0].ToString().Equals(strRevisionFolderRoot))
                            {
                                Directory.Move(strLstCurrentDirectory[0].ToString(), strRevisionFolderRoot);
                            }
                            else if (strLstCurrentDirectory.Length > 1)
                            {
                                //On cherche un autre répertoire avec ce numéro de révision. S'il existe, on copy son contenu dans le nouveau.
                                for (int intIndex = 0; intIndex < strLstCurrentDirectory.Length; intIndex++)
                                {
                                    string strDirectoryName = new FileInfo(strLstCurrentDirectory[intIndex].ToString()).Name;
                                    byte intVersion = 0;

                                    Byte.TryParse(Regex.Replace(strDirectoryName, @"[^\d]+", String.Empty), out intVersion);

                                    if (intVersion == mcView.GetRevisionNo() && !strLstCurrentDirectory[intIndex].ToString().Equals(strRevisionFolderRoot))
                                    {
                                        try
                                        {
                                            Directory.Move(strLstCurrentDirectory[intIndex].ToString(), strRevisionFolderRoot);
                                        }
                                        catch (System.IO.IOException)
                                        {
                                            clsTTApp.GetAppController.ShowMessage(mintMSG_ManyDirectoryFound, System.Windows.Forms.MessageBoxButtons.OK, mcModRevision.Revision_Number.ToString());
                                        }

                                        break;
                                    }
                                }
                            }

                            if (mcView.GetDML_Action() == sclsConstants.DML_Mode.DELETE_MODE) //On supprime toute la hierarchie existante et on sort
                            {
                                blnValidReturn = pfblnDeleteRevisionHierarchy(strRevisionFolderRoot);

                                string strDestinationFolder = string.Empty;

                                if (blnValidReturn && mcModRevision.LstSatelliteRevisions.Count > 0)
                                {
                                    for (int intIndex = 0; intIndex < mcModRevision.LstSatelliteRevisions.Count; intIndex++)
                                    {
                                        strDestinationFolder = strGetSatelliteFolderPathName(mcModRevision.LstSatelliteRevisions[intIndex]);

                                        blnValidReturn = pfblnDeleteDirectory(strDestinationFolder, true);
                                    }
                                }
                                                                
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

                            //Gestion du Exe de l'application principale ou des rapports si inclut
                            if (!mcView.GetExeIsExternalReport())
                            {
                                if ((File.Exists(mcView.GetLocation_Release()) || Directory.Exists(mcView.GetLocation_Release())))
                                {
                                    if ((File.GetAttributes(mcView.GetLocation_Release()) & FileAttributes.Directory) == FileAttributes.Directory)
                                    {
                                        if (mcView.GetLocation_Release() != currentFolderInfos.FullName)
                                        {
                                            if (Directory.Exists(currentFolderInfos.FullName)) Directory.Delete(currentFolderInfos.FullName, true);

                                            currentFolderInfos.Create();

                                            blnValidReturn = clsTTApp.GetAppController.blnCopyFolderContent(mcView.GetLocation_Release(), currentFolderInfos.FullName, true, false, SearchOption.TopDirectoryOnly, true, sclsAppConfigs.GetReleaseInvalidExtensions, sclsAppConfigs.GetReleaseInvalidFolders);

                                            //Supprime l'application des rapports externe au besoin
                                            if (!mcView.GetExeWithExternalReport())
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
                                else if ((File.Exists(currentFolderInfos.FullName) || Directory.Exists(currentFolderInfos.FullName)) &&
                                          mcView.GetLocation_Release() != currentFolderInfos.FullName &&
                                          !string.IsNullOrEmpty(mcView.GetLocation_Release()))
                                {
                                    //Met a jour le path si le contenu de la revision change, mais pas le release
                                    mcModRevision.Path_Release = currentFolderInfos.FullName;
                                }
                            }
                            else
                            {
                                if (Directory.Exists(currentFolderInfos.FullName)) Directory.Delete(currentFolderInfos.FullName, true);
                                //Do nothing, pas de Release de l'application
                            }

                            //Gestion des applications satellites. On les copie ici au même niveau que le Release
                            List<int> lstSRe_NRI = new List<int>();
                            string strNewDestinationFolder = string.Empty;
                            bool blnDirectoryChanged = false;

                            if (blnValidReturn && mcModRevision.LstSatelliteRevisions.Count > 0)
                            {
                                for (int intIndex = 0; intIndex < mcModRevision.LstSatelliteRevisions.Count; intIndex++)
                                {
                                    lstSRe_NRI.Add(mcModRevision.LstSatelliteRevisions[intIndex].SatRevision_NRI);

                                    strNewDestinationFolder = currentFolderInfos.Parent.FullName;

                                    strNewDestinationFolder = Path.Combine(strNewDestinationFolder, mcModRevision.LstSatelliteRevisions[intIndex].CeritarSatelliteApp.ExportFolderName + ((mcModRevision.LstSatelliteRevisions[intIndex].CeritarSatelliteApp.ExePerCustomer | mcModRevision.LstSatelliteRevisions[intIndex].CeritarClient_NRI_Spec > 0) ? (@" [" + mcView.GetCeritarClient_Name() + @"]") : ""));

                                    blnValidReturn = pfblnCopyAndSaveSatelliteLocation(mcModRevision.LstSatelliteRevisions[intIndex], strNewDestinationFolder, ref blnDirectoryChanged, true);

                                    if (blnValidReturn && blnDirectoryChanged)
                                    {
                                        //Copie des applications satellites dans leur répertoire spécifique dans les installations actives
                                        strNewDestinationFolder = strGetSatelliteFolderPathName(mcModRevision.LstSatelliteRevisions[intIndex]);

                                        blnValidReturn = pfblnCopyAndSaveSatelliteLocation(mcModRevision.LstSatelliteRevisions[intIndex], strNewDestinationFolder, ref blnDirectoryChanged, false);
                                    }

                                    if (!blnValidReturn) break;
                                }
                            }
                            else
                            {
                                //Do nothing
                            }

                            if (blnValidReturn)
                            {
                                SqlDataReader sqlRecord = null;
                                mod_SRe_SatelliteRevision cSRe;
                                bool blnSReDirectoryChanged = false;

                                sqlRecord = clsTTSQL.ADOSelect(this.strGetSatelliteApps_SQL(mcView.GetRevision_NRI(), false));

                                while (sqlRecord.Read())
                                {
                                    cSRe = new mod_SRe_SatelliteRevision();
                                    cSRe.DML_Action = sclsConstants.DML_Mode.DELETE_MODE;
                                    cSRe.SatRevision_NRI = Int32.Parse(sqlRecord["SRe_NRI"].ToString());
                                    
                                    if (!lstSRe_NRI.Contains(cSRe.SatRevision_NRI))
                                    {
                                        cSRe.Location_Exe = sqlRecord["SRe_Exe_Location"].ToString();
                                        cSRe.DelaySave_Location_Exe = true;
                                        if (sqlRecord["CeC_NRI_Spec"] != DBNull.Value) cSRe.CeritarClient_NRI_Spec = Int32.Parse(sqlRecord["CeC_NRI_Spec"].ToString());

                                        cSRe.Revision = new mod_Rev_Revision();
                                        cSRe.Revision.Revision_NRI = mcView.GetRevision_NRI();

                                        cSRe.CeritarSatelliteApp = new Models.Module_Configuration.mod_CSA_CeritarSatelliteApp();
                                        cSRe.CeritarSatelliteApp.CeritarSatelliteApp_NRI = Int32.Parse(sqlRecord["CSA_NRI"].ToString());
                                        cSRe.CeritarSatelliteApp.Name = sqlRecord["CSA_Name"].ToString();
                                        cSRe.CeritarSatelliteApp.ExePerCustomer = bool.Parse(sqlRecord["CSA_ExePerCustomer"].ToString());
                                        cSRe.CeritarSatelliteApp.ExportFolderName = sqlRecord["CSA_KitFolderName"].ToString();
                                        cSRe.CeritarSatelliteApp.ExeIsFolder = Convert.ToBoolean(sqlRecord["CSA_ExeIsFolder"].ToString());

                                        strNewDestinationFolder = currentFolderInfos.Parent.FullName;

                                        strNewDestinationFolder = Path.Combine(strNewDestinationFolder, cSRe.CeritarSatelliteApp.ExportFolderName + ((cSRe.CeritarSatelliteApp.ExePerCustomer | cSRe.CeritarClient_NRI_Spec > 0) ? (@" [" + sqlRecord["CeC_Name"].ToString() + @"]") : ""));

                                        blnValidReturn = pfblnCopyAndSaveSatelliteLocation(cSRe, strNewDestinationFolder, ref blnSReDirectoryChanged, true);
                                    }
                                    else
                                    {
                                        blnValidReturn = true;
                                    }

                                    if (!blnValidReturn) break;
                                }
                            }

                            break;

                        case (int)ctr_Template.FolderType.Scripts:

                            int intNextScriptNo = 0;
                            int intNbScriptsAlreadyPresent = 0;
                            System.Windows.Forms.DialogResult answer;
                            string strNewDestinationFileName = string.Empty;
                            List<string> lstScripts;

                            if ((File.Exists(mcView.GetLocation_Scripts()) || Directory.Exists(mcView.GetLocation_Scripts())))
                            {
                                if ((File.GetAttributes(mcView.GetLocation_Scripts()) & FileAttributes.Directory) == FileAttributes.Directory)
                                {
                                    if (mcView.GetLocation_Scripts() != currentFolderInfos.FullName)
                                    {
                                        blnValidReturn = true;

                                        if (!mcView.GetIfScriptsAreToAppend() && Directory.Exists(currentFolderInfos.FullName))
                                        {
                                            clsTTApp.GetAppController.setAttributesToNormal(new DirectoryInfo(currentFolderInfos.FullName));

                                            if (mcModRevision.DML_Action != sclsConstants.DML_Mode.INSERT_MODE)
                                            {
                                                blnValidReturn = pfblnDeleteFrom_Rev_AllScripts(currentFolderInfos.FullName);
                                            }

                                            if (mcView.GetDML_Action() == sclsConstants.DML_Mode.UPDATE_MODE)
                                            {
                                                answer = clsTTApp.GetAppController.ShowMessage(mintMSG_AreYouSureToDelete, System.Windows.Forms.MessageBoxButtons.YesNo, currentFolderInfos.FullName);
                                            }
                                            else
                                            {
                                                answer = System.Windows.Forms.DialogResult.Yes;
                                            }
                                            
                                            if (answer == System.Windows.Forms.DialogResult.Yes)
                                            {
                                                Directory.Delete(currentFolderInfos.FullName, true);
                                            }
                                            else
                                            {
                                                blnValidReturn = false;
                                            }
                                        }
                                        else if (Directory.Exists(currentFolderInfos.FullName))
                                        {
                                            intNbScriptsAlreadyPresent = Directory.GetFiles(currentFolderInfos.FullName).Length;
                                        }

                                        if (blnValidReturn)
                                        {
                                            currentFolderInfos.Create();

                                            lstScripts = Directory.GetFiles(mcView.GetLocation_Scripts(), "*.sql").OrderBy(i => i, new TT3LightDLL.Classes.NaturalStringComparer()).ToList();

                                            for (int intIndex = 0; intIndex < lstScripts.Count; intIndex++)
                                            {
                                                strNewDestinationFileName = pfstrGetNewFileNameWithNumber(currentFolderInfos.FullName, lstScripts[intIndex], ref intNextScriptNo, mcModRevision.Version.CerApplication.AutoGenRevisionNoScript);

                                                strNewDestinationFileName = Path.Combine(currentFolderInfos.FullName, strNewDestinationFileName);

                                                File.Copy(lstScripts[intIndex], strNewDestinationFileName, true);
                                            }

                                            mcModRevision.Path_Scripts = currentFolderInfos.FullName;

                                            blnScriptsChanged = true;
                                        }

                                        //Copy all client's specific scripts in the scripts folder
                                        if (blnValidReturn && Directory.GetDirectories(mcView.GetLocation_Scripts(), mcView.GetCeritarClient_Name() + "*", SearchOption.TopDirectoryOnly).Length > 0)
                                        {
                                            string[] lstSpecificScripts = Directory.GetFiles(Directory.GetDirectories(mcView.GetLocation_Scripts(), mcView.GetCeritarClient_Name() + "*", SearchOption.TopDirectoryOnly)[0]);
                                            string strClientSpecificScriptsDestinationPath = System.IO.Path.Combine(currentFolderInfos.FullName, mcView.GetCeritarClient_Name() + @" Only");
                                            int intSpecificNextScriptNo = 0;

                                            if (!Directory.Exists(strClientSpecificScriptsDestinationPath)) Directory.CreateDirectory(strClientSpecificScriptsDestinationPath);

                                            for (int intIndex = 0; intIndex < lstSpecificScripts.Length; intIndex++)
                                            {
                                                strNewDestinationFileName = pfstrGetNewFileNameWithNumber(strClientSpecificScriptsDestinationPath, lstSpecificScripts[intIndex], ref intSpecificNextScriptNo, false);

                                                strNewDestinationFileName = Path.Combine(strClientSpecificScriptsDestinationPath, strNewDestinationFileName);

                                                File.Copy(lstSpecificScripts[intIndex], strNewDestinationFileName, true);
                                            }
                                        }
                                    }
                                }
                                else if (mcView.GetLocation_Scripts() != Path.Combine(currentFolderInfos.FullName, Path.GetFileName(mcView.GetLocation_Scripts())))
                                {
                                    if (!mcView.GetIfScriptsAreToAppend() && Directory.Exists(currentFolderInfos.FullName))
                                    {
                                        clsTTApp.GetAppController.setAttributesToNormal(new DirectoryInfo(currentFolderInfos.FullName));

                                        if (mcModRevision.DML_Action != sclsConstants.DML_Mode.INSERT_MODE)
                                        {
                                            blnValidReturn = pfblnDeleteFrom_Rev_AllScripts(currentFolderInfos.FullName);
                                        }

                                        if (mcView.GetDML_Action() == sclsConstants.DML_Mode.UPDATE_MODE)
                                        {
                                            answer = clsTTApp.GetAppController.ShowMessage(mintMSG_AreYouSureToDelete, System.Windows.Forms.MessageBoxButtons.YesNo, currentFolderInfos.FullName);
                                        }
                                        else
                                        {
                                            answer = System.Windows.Forms.DialogResult.Yes;
                                        }

                                        if (answer == System.Windows.Forms.DialogResult.Yes)
                                        {
                                            Directory.Delete(currentFolderInfos.FullName, true);
                                        }
                                        else
                                        {
                                            blnValidReturn = false;
                                        }                
                                    }
                                    else if (Directory.Exists(currentFolderInfos.FullName))
                                    {
                                        intNbScriptsAlreadyPresent = Directory.GetFiles(currentFolderInfos.FullName).Length;
                                    }

                                    currentFolderInfos.Create();

                                    strNewDestinationFileName = pfstrGetNewFileNameWithNumber(currentFolderInfos.FullName, mcView.GetLocation_Scripts(), ref intNextScriptNo, mcModRevision.Version.CerApplication.AutoGenRevisionNoScript);

                                    strNewDestinationFileName = Path.Combine(currentFolderInfos.FullName, strNewDestinationFileName);

                                    File.Copy(mcView.GetLocation_Scripts(), strNewDestinationFileName, true);

                                    mcModRevision.Path_Scripts = Path.GetDirectoryName(strNewDestinationFileName);

                                    blnScriptsChanged = true;
                                }
                            }
                            else if ((File.Exists(currentFolderInfos.FullName) || Directory.Exists(currentFolderInfos.FullName)) &&
                                      mcView.GetLocation_Scripts() != currentFolderInfos.FullName &&
                                      !string.IsNullOrEmpty(mcView.GetLocation_Scripts()))
                            {
                                //Met a jour le path si le contenu de la revision change, mais pas les scripts
                                mcModRevision.Path_Scripts = currentFolderInfos.FullName;
                            }


                            //Ajout du script de mise à jour du numéro de révision
                            if (mcModRevision.Version.CerApplication.AutoGenRevisionNoScript)
                            {
                                if (string.IsNullOrEmpty(mcView.GetLocation_Scripts()))
                                {
                                    if (!Directory.Exists(currentFolderInfos.FullName)) Directory.CreateDirectory(currentFolderInfos.FullName);

                                    mcModRevision.Path_Scripts = currentFolderInfos.FullName;
                                }

                                string strAppRevisionScriptLocation = Path.Combine(mcModRevision.Path_Scripts, sclsAppConfigs.GetAppRevisionFileName(mcModRevision.Revision_Number.ToString()));

                                if (!string.IsNullOrEmpty(mcModRevision.Path_Scripts) && !File.Exists(strAppRevisionScriptLocation))
                                {
                                    File.WriteAllText(strAppRevisionScriptLocation, "UPDATE TTParam SET TTP_Value = '" + mcView.GetRevisionNo().ToString() + "' WHERE TTP_Name = " + clsTTApp.GetAppController.str_FixStringForSQL("AppRevision"));

                                    blnScriptsChanged = true;
                                }
                            }

                            if (!Directory.Exists(mcModRevision.Path_Scripts)) mcModRevision.Path_Scripts = string.Empty;

                            //Ajout dans Rev_AllScripts             ****Rev_AllScripts DEPRECATED AND HORRIBLE ****
                            //List<string> lstScriptsToCopy;
                            //string strNewScriptName = string.Empty;
                            //int intNewScriptNumber = 0;

                                //if (!string.IsNullOrEmpty(mcModRevision.Path_Scripts) & blnScriptsChanged)
                                //{
                                //    strRevAllScripts_Location = str_GetRevisionRootFolderPath(mcModRevision.TemplateSource.Template_NRI, mcModRevision.Version.VersionNo.ToString()).Replace(sclsAppConfigs.GetRevisionNumberPrefix + "XX", sclsAppConfigs.GetRevisionAllScriptFolderName);

                                //    if (!Directory.Exists(strRevAllScripts_Location))
                                //    {
                                //        Directory.CreateDirectory(strRevAllScripts_Location);
                                //    }

                                //    if ((File.GetAttributes(mcModRevision.Path_Scripts) & FileAttributes.Directory) == FileAttributes.Directory)
                                //    {
                                //        clsTTApp.GetAppController.setAttributesToNormal(new DirectoryInfo(mcModRevision.Path_Scripts));

                                //        lstScriptsToCopy = Directory.GetFiles(mcModRevision.Path_Scripts, "*.sql", SearchOption.TopDirectoryOnly).OrderBy(i => i, new TT3LightDLL.Classes.NaturalStringComparer()).ToList(); // OrderBy(f => f).ToList<string>();
                                //    }
                                //    else
                                //    {
                                //        lstScriptsToCopy = Directory.GetFiles(new FileInfo(mcModRevision.Path_Scripts).Directory.FullName, "*.sql", SearchOption.TopDirectoryOnly).OrderBy(i => i, new TT3LightDLL.Classes.NaturalStringComparer()).ToList();
                                //    }

                                //    clsTTApp.GetAppController.setAttributesToNormal(new DirectoryInfo(strRevAllScripts_Location));

                                //    for (int intIndex = intNbScriptsAlreadyPresent; intIndex < lstScriptsToCopy.Count; intIndex++)
                                //    {
                                //        strNewScriptName = pfstrGetNewFileNameWithNumber(strRevAllScripts_Location, lstScriptsToCopy[intIndex], ref intNewScriptNumber, false);

                                //        File.Copy(lstScriptsToCopy[intIndex], Path.Combine(strRevAllScripts_Location, strNewScriptName), true);
                                //    }
                                //}


                            break;

                        case (int)ctr_Template.FolderType.External_Report:

                            blnValidReturn = true;

                            //Gestion du Exe des rapports externes
                            if (mcView.GetExeIsExternalReport())
                            {
                                if ((File.Exists(mcView.GetLocation_Release()) || Directory.Exists(mcView.GetLocation_Release())))
                                {
                                    if ((File.GetAttributes(mcView.GetLocation_Release()) & FileAttributes.Directory) == FileAttributes.Directory)
                                    {
                                        if (mcView.GetLocation_Release() != currentFolderInfos.FullName)
                                        {
                                            if (Directory.Exists(currentFolderInfos.FullName)) Directory.Delete(currentFolderInfos.FullName, true);

                                            currentFolderInfos.Create();

                                            blnValidReturn = clsTTApp.GetAppController.blnCopyFolderContent(mcView.GetLocation_Release(), currentFolderInfos.FullName, true, false, SearchOption.TopDirectoryOnly, false, sclsAppConfigs.GetReleaseInvalidExtensions);

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
                                else if ((File.Exists(currentFolderInfos.FullName) || Directory.Exists(currentFolderInfos.FullName)) &&
                                          mcView.GetLocation_Release() != currentFolderInfos.FullName &&
                                          !string.IsNullOrEmpty(mcView.GetLocation_Release()))
                                {
                                    //Met a jour le path si le contenu de la revision change, mais pas le release
                                    mcModRevision.Path_Release = currentFolderInfos.FullName;
                                }
                            }
                            else
                            {
                                if (Directory.Exists(currentFolderInfos.FullName)) Directory.Delete(currentFolderInfos.FullName, true);
                            }

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
                                clsTTApp.GetAppController.blnCopyFolderContent(mcView.GetLocation_VariousFolder(), Path.Combine(currentFolderInfos.FullName, Path.GetFileName(mcView.GetLocation_VariousFolder())), true, true, SearchOption.TopDirectoryOnly, true);
                            }

                            break;

                        default:
                            blnValidReturn = true;
                            break;
                    }

                    intPreviousFolderLevel = Int32.Parse(cSQLReader["HiCo_NodeLevel"].ToString());

                    if (!blnValidReturn) break;
                }

                
                //DEPLACER PLUS HAUT EN MEME TEMPS QUE CEUX AU NIVEAU DU RELEASE, comprend pas pk fait ici?
                //if (blnValidReturn && mcModRevision.LstSatelliteRevisions.Count > 0)
                //{
                //    string strDestinationFolder = string.Empty;

                //    for (int intIndex = 0; intIndex < mcModRevision.LstSatelliteRevisions.Count; intIndex++)
                //    {
                //        strDestinationFolder = strGetSatelliteFolderPathName(mcModRevision.LstSatelliteRevisions[intIndex]);

                //        blnValidReturn = pfblnCopyAndSaveSatelliteLocation(mcModRevision.LstSatelliteRevisions[intIndex], strDestinationFolder, false);

                //        if (!blnValidReturn) break;
                //    }
                //}
            }
            catch (FileNotFoundException exPath)
            {
                blnValidReturn = false;
                mcActionResult.SetInvalid(sclsConstants.Validation_Message.INVALID_PATH, clsActionResults.BaseErrorCode.UNHANDLED_VALIDATION, exPath.FileName);
            }
            catch (System.IO.IOException exUA)
            {
                blnValidReturn = false;
                mcActionResult.SetInvalid(sclsConstants.Validation_Message.PATH_ACCESS_DENIED, clsActionResults.BaseErrorCode.UNHANDLED_VALIDATION, exUA.Message);
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

        /// <summary>
        /// Retourne le nom du fichier avec son numéro préfixé. Ex.: DB_Exemple devient 01_DB_Exemple.
        /// </summary>
        /// <param name="vstrSourceScriptPath">Le chemin vers le script à analyser.</param>
        /// <param name="rintNextScriptNumber">Le prochain numéro de script à utiliser.</param>
        /// <returns>Le nom du nouveau fichier avec son numéro.</returns>
        /// <remarks>La fonction considère que les scripts traités ont été triés par nom au préalable.</remarks>
        private string pfstrGetNewFileNameWithNumber(string vstrDestinationFolderPath, string vstrSourceScriptPath, ref int rintNextScriptNumber, bool vblnZeroBased)
        {
            string strNewScriptName = string.Empty;
            int intFirstUnderscoreIndex = 0;
            int intLastDigitCharIndex = 0;
            int intIntendedScriptNumber = 0;
            List<string> lstExistingScripts;

            try
            {
                if (rintNextScriptNumber <= 0)
                {
                    //On regarde le prochain numéro à utiliser dans le dossier de destination
                    lstExistingScripts = Directory.GetFiles(vstrDestinationFolderPath).ToList();//.OrderBy(i => i, new TT3LightDLL.Classes.NaturalStringComparer()).ToList();

                    if (lstExistingScripts.Count > 0)
                    {
                        //Int32.TryParse(new String(Path.GetFileName(lstExistingScripts[lstExistingScripts.Count - 1]).TakeWhile(Char.IsDigit).ToArray()), out intNewScriptNumber);

                        if (vblnZeroBased) //Il va y avoir un script # 00
                        {
                            rintNextScriptNumber = lstExistingScripts.Count - 1;
                        }
                        else
                        {
                            rintNextScriptNumber = lstExistingScripts.Count;
                        }
                    }

                    rintNextScriptNumber++;
                }

                strNewScriptName = Path.GetFileName(vstrSourceScriptPath);

                intFirstUnderscoreIndex = strNewScriptName.IndexOf("_");
                intLastDigitCharIndex = new String(strNewScriptName.TakeWhile(Char.IsDigit).ToArray()).Length;

                if (intFirstUnderscoreIndex < 4 && intFirstUnderscoreIndex > -1) //On ne peut jamais avoir un script # en haut de 999
                {
                    Int32.TryParse(strNewScriptName.Substring(0, intFirstUnderscoreIndex), out intIntendedScriptNumber);
                    strNewScriptName = strNewScriptName.Substring(intFirstUnderscoreIndex + 1);
                }
                else if (intLastDigitCharIndex < 4) //Les premiers caractères pourraient être le numéro de script avec oubli d'ajouter un _
                {
                    Int32.TryParse(strNewScriptName.Substring(0, intLastDigitCharIndex), out intIntendedScriptNumber);
                    strNewScriptName = strNewScriptName.Substring(intLastDigitCharIndex);
                }
                else
                {
                    //Do nothing, le script ne contient pas de numéro au début
                }

                strNewScriptName = rintNextScriptNumber.ToString("00") + "_" + strNewScriptName;
            }
            catch (Exception ex)
            {
                mcActionResult.SetInvalid(sclsConstants.Error_Message.ERROR_UNHANDLED, clsActionResults.BaseErrorCode.UNHANDLED_ERROR);
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }
            finally
            {
                rintNextScriptNumber++;
            }

            return strNewScriptName;
        }

        private string pfstrGetRevisionFolderName(mod_Rev_Revision rcRevision = null)
        {
            string strFolderName = string.Empty;
            string strSatelliteExeExportName = string.Empty;
            string strRevisionFolderName_InfosSupp = string.Empty;
            mod_Rev_Revision cRevision = null;
            SqlDataReader sqlRecord = null;
            List<int> lstCSA_NRI = new List<int>();

            if (rcRevision != null)
            {
                cRevision = rcRevision;
            }
            else
            {
                cRevision = mcModRevision;
            }

            try
            {
                strFolderName = sclsAppConfigs.GetRevisionNumberPrefix + cRevision.Revision_Number.ToString();

                strRevisionFolderName_InfosSupp = (string.IsNullOrEmpty(cRevision.Path_Release) || cRevision.ExeIsExternalReport ? string.Empty : " Exe");
                strRevisionFolderName_InfosSupp = strRevisionFolderName_InfosSupp + (cRevision.ExeIsExternalReport || cRevision.ExeWithExternalReport ? (string.IsNullOrEmpty(strRevisionFolderName_InfosSupp) ? string.Empty : " -") + " RPT" : string.Empty);
                strRevisionFolderName_InfosSupp = strRevisionFolderName_InfosSupp + (string.IsNullOrEmpty(cRevision.Path_Scripts) ? string.Empty : (string.IsNullOrEmpty(strRevisionFolderName_InfosSupp) ? string.Empty : " -") + " SCRIPTS");

                sqlRecord = clsTTSQL.ADOSelect(this.strGetSatelliteApps_SQL(cRevision.Revision_NRI, true));

                while (sqlRecord.Read())
                {
                    lstCSA_NRI.Add(Int32.Parse(sqlRecord["CSA_NRI"].ToString()));

                    strSatelliteExeExportName = sqlRecord["CSA_KitFolderName"].ToString(); //cRevision.LstSatelliteRevisions[intIndex].CeritarSatelliteApp.ExportFolderName;

                    strRevisionFolderName_InfosSupp = strRevisionFolderName_InfosSupp + (string.IsNullOrEmpty(strSatelliteExeExportName) ? string.Empty : (string.IsNullOrEmpty(strRevisionFolderName_InfosSupp) ? string.Empty : " -") + " " + strSatelliteExeExportName);

                    strSatelliteExeExportName = string.Empty;
                }

                for (int intIndex = 0; intIndex < cRevision.LstSatelliteRevisions.Count; intIndex++)
                {
                    if (!string.IsNullOrEmpty(cRevision.LstSatelliteRevisions[intIndex].Location_Exe) & !lstCSA_NRI.Contains(cRevision.LstSatelliteRevisions[intIndex].CeritarSatelliteApp.CeritarSatelliteApp_NRI))
                    {
                        strSatelliteExeExportName = cRevision.LstSatelliteRevisions[intIndex].CeritarSatelliteApp.ExportFolderName;

                        strRevisionFolderName_InfosSupp = strRevisionFolderName_InfosSupp + (string.IsNullOrEmpty(strSatelliteExeExportName) ? string.Empty : (string.IsNullOrEmpty(strRevisionFolderName_InfosSupp) ? string.Empty : " -") + " " + strSatelliteExeExportName);

                        strSatelliteExeExportName = string.Empty;
                    }
                }

                strFolderName = strFolderName + strRevisionFolderName_InfosSupp;
            }
            catch (Exception ex)
            {
                mcActionResult.SetInvalid(sclsConstants.Error_Message.ERROR_UNHANDLED, clsActionResults.BaseErrorCode.UNHANDLED_ERROR);
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return strFolderName;
        }

        public string str_GetRevisionFolderPath(int vintTemplate_NRI, string vstrVersion_No)
        {
            string strPath = string.Empty;

            strPath = str_GetRevisionRootFolderPath(vintTemplate_NRI, vstrVersion_No);

            if (pfblnFeedModelWithView())
            {
                strPath = strPath.Replace(sclsAppConfigs.GetRevisionNumberPrefix + "XX", pfstrGetRevisionFolderName());
            }

            return strPath;
        }

        private string str_GetRevisionFolderPath(mod_Rev_Revision rcRevision = null)
        {
            string strPath = string.Empty;

            strPath = str_GetRevisionRootFolderPath(rcRevision.TemplateSource.Template_NRI, rcRevision.Version.VersionNo.ToString());

            strPath = strPath.Replace(sclsAppConfigs.GetRevisionNumberPrefix + "XX", pfstrGetRevisionFolderName(rcRevision));

            return strPath;
        }

        private string str_GetRevisionRootFolderPath(int vintTemplate_NRI, string vstrVersion_No)
        {
            string strSQL = string.Empty;
            string strPath = string.Empty;
            SqlDataReader sqlRecord = null;

            try
            {
                //strSQL = strSQL + " WITH LstHierarchyComp " + Environment.NewLine;
                //strSQL = strSQL + " AS " + Environment.NewLine;
                //strSQL = strSQL + " ( " + Environment.NewLine;
                //strSQL = strSQL + "     SELECT *, " + Environment.NewLine;
                //strSQL = strSQL + " 		   CAST(0 AS varbinary(max)) AS Level " + Environment.NewLine;
                //strSQL = strSQL + " 	FROM HierarchyComp  " + Environment.NewLine;
                //strSQL = strSQL + " 	WHERE HiCo_Parent_NRI IS NULL " + Environment.NewLine;

                //strSQL = strSQL + "     UNION ALL " + Environment.NewLine;

                //strSQL = strSQL + "     SELECT HiCo_Childrens.*, " + Environment.NewLine;
                //strSQL = strSQL + " 		   Level + CAST(HiCo_Childrens.HiCo_NRI AS varbinary(max)) AS Level " + Environment.NewLine;
                //strSQL = strSQL + " 	FROM HierarchyComp HiCo_Childrens  " + Environment.NewLine;
                //strSQL = strSQL + " 		INNER JOIN LstHierarchyComp on HiCo_Childrens.HiCo_Parent_NRI = LstHierarchyComp.HiCo_NRI " + Environment.NewLine;
                //strSQL = strSQL + " 	WHERE HiCo_Childrens.HiCo_Parent_NRI IS NOT NULL " + Environment.NewLine;
                //strSQL = strSQL + " ) " + Environment.NewLine;

                //strSQL = strSQL + " SELECT Path = TTParam.TTP_Value + " + Environment.NewLine;
                //strSQL = strSQL + "               (SELECT '/' + CONVERT(VARCHAR(300), REPLACE(LstHierarchyComp.HiCo_Name, '_XXX', " + clsTTApp.GetAppController.str_FixStringForSQL("_" + vstrVersion_No) + ")), " + Environment.NewLine;
                //strSQL = strSQL + "                       LstHierarchyComp.FoT_NRI " + Environment.NewLine;
                //strSQL = strSQL + "                FROM LstHierarchyComp " + Environment.NewLine;
                //strSQL = strSQL + "                    INNER JOIN FolderType ON FolderType.FoT_NRI = LstHierarchyComp.FoT_NRI " + Environment.NewLine;

                //strSQL = strSQL + "                WHERE LstHierarchyComp.Tpl_NRI = " + vintTemplate_NRI + Environment.NewLine;
                //strSQL = strSQL + "                  AND FolderType.FoT_Modifiable = 0 " + Environment.NewLine;

                //strSQL = strSQL + "                ORDER BY Level FOR XML PATH('') ) " + Environment.NewLine;

                //strSQL = strSQL + " FROM TTParam " + Environment.NewLine;

                //strSQL = strSQL + " WHERE TTParam.TTP_Name = 'InstallationsActives' " + Environment.NewLine;

                strSQL = strSQL + " WITH LstHierarchyComp " + Environment.NewLine;
                strSQL = strSQL + " AS " + Environment.NewLine;
                strSQL = strSQL + " ( " + Environment.NewLine;
                strSQL = strSQL + "     SELECT *, " + Environment.NewLine;
                strSQL = strSQL + " 		   CASE WHEN FoT_NRI = " + (int)ctr_Template.FolderType.System + " THEN CAST(0 AS varbinary(max)) ELSE CAST(HierarchyComp.HiCo_NRI AS varbinary(max)) END AS Level " + Environment.NewLine;
                strSQL = strSQL + " 	FROM HierarchyComp  " + Environment.NewLine;
                strSQL = strSQL + " 	WHERE HiCo_Parent_NRI IS NULL " + Environment.NewLine;

                strSQL = strSQL + "     UNION ALL " + Environment.NewLine;

                strSQL = strSQL + "     SELECT HiCo_Childrens.*, " + Environment.NewLine;
                strSQL = strSQL + " 		   Level + CAST(HiCo_Childrens.HiCo_NRI AS varbinary(max)) AS Level " + Environment.NewLine;
                strSQL = strSQL + " 	FROM HierarchyComp HiCo_Childrens  " + Environment.NewLine;
                strSQL = strSQL + " 		INNER JOIN LstHierarchyComp on HiCo_Childrens.HiCo_Parent_NRI = LstHierarchyComp.HiCo_NRI " + Environment.NewLine;
                strSQL = strSQL + " 	WHERE HiCo_Childrens.HiCo_Parent_NRI IS NOT NULL " + Environment.NewLine;
                strSQL = strSQL + " ) " + Environment.NewLine;

                strSQL = strSQL + " SELECT FolderName = CASE WHEN LstHierarchyComp.FoT_NRI = " + (int)ctr_Template.FolderType.Version_Number + " THEN CONVERT(VARCHAR(300), REPLACE(LstHierarchyComp.HiCo_Name, '_XXX', " + clsTTApp.GetAppController.str_FixStringForSQL("_" + vstrVersion_No) + ")) ELSE LstHierarchyComp.HiCo_Name END, " + Environment.NewLine;
                strSQL = strSQL + "        LstHierarchyComp.FoT_NRI " + Environment.NewLine;

                strSQL = strSQL + " FROM LstHierarchyComp " + Environment.NewLine;

                strSQL = strSQL + " WHERE LstHierarchyComp.FoT_NRI = " + (int)ctr_Template.FolderType.System + Environment.NewLine;
                strSQL = strSQL + "    OR LstHierarchyComp.Tpl_NRI = " + vintTemplate_NRI + Environment.NewLine;

                strSQL = strSQL + " ORDER BY Level " + Environment.NewLine;

                sqlRecord = clsTTSQL.ADOSelect(strSQL);

                while (sqlRecord.Read())
                {
                    strPath = Path.Combine(strPath, sqlRecord["FolderName"].ToString());

                    if (sqlRecord["FoT_NRI"].ToString() == ((int)ctr_Template.FolderType.Revision_Number).ToString()) break;
                }
            } catch (Exception ex)
            {
                mcActionResult.SetInvalid(sclsConstants.Error_Message.ERROR_UNHANDLED, clsActionResults.BaseErrorCode.UNHANDLED_ERROR);
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }
            finally
            {
                if (sqlRecord != null) sqlRecord.Close();
            }

            return strPath;
        }

        /// <summary>
        /// Copie les exécutables des applications satellite dans leur dossiers respectifs dans les installations actives.
        /// </summary>
        /// <param name="rcSatRevision">La classe qui représente l'application satellite à copier.
        ///                             Passer par référence, car on veut mettre à jour la location choisie par l'utilisateur avec celle sauvegardée.</param>
        /// <returns>Vrai si tout s'est bien passé.</returns>
        private bool pfblnCopyAndSaveSatelliteLocation(mod_SRe_SatelliteRevision rcSatRevision, string vstrDestinationFolder, ref bool rblnDirectoryChanged, bool vblnSaveLocationToDB = false)
        {
            bool blnValidReturn = true;
            string strOldFolderPath = string.Empty;
            string strNewFolderOrFileCompletePath = string.Empty;
            DirectoryInfo currentFolderInfos = null;

            try
            {
                //En premier, on valide si le dossier de destination est différent de l'ancien.
                //S'il est différent, il faut déterminer si l'ancien existe afin de le déplacer dans le nouveau.
                //Ensuite, au besoin, les fichiers du Nouveau dossier (contenant potentiellement l'ancien release déplacé) seront supprimés et remplacés par les nouveaux sélectionnés
                if (vblnSaveLocationToDB)
                {
                    strOldFolderPath = mcSQL.str_ADOSingleLookUp_Trans("SRe_Exe_Location", "SatRevision", "SRe_NRI = " + rcSatRevision.SatRevision_NRI);

                    if (rcSatRevision.CeritarSatelliteApp.ExeIsFolder)
                    {
                        //On prend vstrDestinationFolder).fullName, car le répertoire racine à déjà été changé physiquement, mais on veut l'ancien nom du sous dossier
                        //strOldFolderPath = Path.Combine(Directory.GetParent(vstrDestinationFolder).FullName, new DirectoryInfo(rcSatRevision.Location_Exe).Name);
                    }
                    else
                    {
                        try
                        {
                            strOldFolderPath = Directory.GetParent(strOldFolderPath).FullName;//Path.Combine(Directory.GetParent(vstrDestinationFolder).FullName, new DirectoryInfo(strOldFolderPathInDB).Parent.Name);
                        }
                        catch (Exception)
                        {
                            strOldFolderPath = string.Empty;
                        }

                        //if (strOldFolderPath == string.Empty) strOldFolderPath = Path.Combine(new DirectoryInfo(vstrDestinationFolder).FullName);
                    }

                    if (rcSatRevision.Location_Exe != string.Empty && strOldFolderPath != string.Empty && strOldFolderPath != vstrDestinationFolder)
                    {
                        //Une genre de securite, mais normalement, devrais jamais entrer ici, car gerer avant ca
                        if (Directory.Exists(strOldFolderPath) || File.Exists(strOldFolderPath))
                        {
                            Directory.Move(strOldFolderPath, vstrDestinationFolder);
                        }
                    }
                }

                if (rcSatRevision.CeritarSatelliteApp.ExeIsFolder)
                {
                    strNewFolderOrFileCompletePath = vstrDestinationFolder;
                }
                else
                {
                    strNewFolderOrFileCompletePath = Path.Combine(vstrDestinationFolder, Path.GetFileName(rcSatRevision.Location_Exe));
                }
                

                //On entre dans ce IF seulement lorsqu'on vient de selectionner un nouvel exec / dossier pour remplacer lancien
                if ((File.Exists(rcSatRevision.Location_Exe) || Directory.Exists(rcSatRevision.Location_Exe)) && (strOldFolderPath != vstrDestinationFolder || rcSatRevision.DML_Action == sclsConstants.DML_Mode.UPDATE_MODE))
                {
                    rblnDirectoryChanged = true;

                    currentFolderInfos = new DirectoryInfo(vstrDestinationFolder);

                    if (Directory.Exists(currentFolderInfos.FullName))
                    {
                        blnValidReturn = pfblnDeleteDirectory(currentFolderInfos.FullName, vblnSaveLocationToDB, false);

                        if (!blnValidReturn) return false; //On sort et interrompt la sauvegarde
                    }

                    currentFolderInfos.Create();

                    if ((File.GetAttributes(rcSatRevision.Location_Exe) & FileAttributes.Directory) == FileAttributes.Directory) //Executable is a folder
                    {
                        if (rcSatRevision.Location_Exe != currentFolderInfos.FullName)
                        {
                            blnValidReturn = clsTTApp.GetAppController.blnCopyFolderContent(rcSatRevision.Location_Exe, currentFolderInfos.FullName, true, true, SearchOption.TopDirectoryOnly, true, sclsAppConfigs.GetReleaseInvalidExtensions, sclsAppConfigs.GetReleaseInvalidFolders);

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
                //Dans ces Else, on va mettre a jour le path des exe qui etaient deja la parce que on a jouter un nouvel element et le path parent a changer
                else if ((File.GetAttributes(strNewFolderOrFileCompletePath) & FileAttributes.Directory) == FileAttributes.Directory && Directory.Exists(vstrDestinationFolder) && new DirectoryInfo(rcSatRevision.Location_Exe).Parent.FullName != vstrDestinationFolder)
                {
                    rcSatRevision.Location_Exe = vstrDestinationFolder;
                }
                else if (File.Exists(strNewFolderOrFileCompletePath) && rcSatRevision.Location_Exe != strNewFolderOrFileCompletePath)
                {
                    rcSatRevision.Location_Exe = Path.Combine(vstrDestinationFolder, Path.GetFileName(rcSatRevision.Location_Exe));
                }
                else
                {
                    vblnSaveLocationToDB = false;
                }

                if (blnValidReturn && vblnSaveLocationToDB)
                {
                    blnValidReturn = pfblnUpdateSatelliteLocationToDB(rcSatRevision);
                }
            }
            catch (FileNotFoundException exPath)
            {
                blnValidReturn = false;
                mcActionResult.SetInvalid(sclsConstants.Validation_Message.INVALID_PATH, clsActionResults.BaseErrorCode.UNHANDLED_VALIDATION, exPath.FileName);
            }
            catch (System.IO.IOException exUA)
            {
                blnValidReturn = false;
                mcActionResult.SetInvalid(sclsConstants.Validation_Message.PATH_ACCESS_DENIED, clsActionResults.BaseErrorCode.UNHANDLED_VALIDATION, exUA.Message);
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

        private bool pfblnUpdateSatelliteLocationToDB(mod_SRe_SatelliteRevision rcSatRevision)
        {
            bool blnValidReturn = true;

            try
            {
                rcSatRevision.SetcSQL = (mcSQL == null ? new clsTTSQL() : mcSQL);
                rcSatRevision.DML_Action = rcSatRevision.SatRevision_NRI == 0 ? sclsConstants.DML_Mode.INSERT_MODE : sclsConstants.DML_Mode.UPDATE_MODE;
                rcSatRevision.DelaySave_Location_Exe = false;

                blnValidReturn = rcSatRevision.blnSave(); //Pour update le chemin ou la sauvegarde est faite
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
                answer = clsTTApp.GetAppController.ShowMessage(mintMSG_AreYouSureToDelete, System.Windows.Forms.MessageBoxButtons.YesNo, vstrRevisionFolderRoot);

                if (answer == System.Windows.Forms.DialogResult.Yes)
                {
                    blnValidReturn = pfblnDeleteFrom_Rev_AllScripts(mcView.GetLocation_Scripts());

                    if (blnValidReturn)
                    {
                        if (Directory.Exists(vstrRevisionFolderRoot))
                            Microsoft.VisualBasic.FileIO.FileSystem.DeleteDirectory(vstrRevisionFolderRoot, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin, Microsoft.VisualBasic.FileIO.UICancelOption.ThrowException);
                        //System.Diagnostics.Process process = new System.Diagnostics.Process();
                        //System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();

                        //startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                        //startInfo.FileName = "cmd.exe";
                        //startInfo.Arguments = @"/C RMDIR """ + vstrRevisionFolderRoot + @""" /S /Q";

                        //process.StartInfo = startInfo;
                        //process.Start();
                        //process.WaitForExit();
                    }
                }
                else
                {
                    blnValidReturn = false;
                }
            }
            catch (System.IO.IOException exUA)
            {
                blnValidReturn = false;
                mcActionResult.SetInvalid(sclsConstants.Validation_Message.PATH_ACCESS_DENIED, clsActionResults.BaseErrorCode.UNHANDLED_VALIDATION, exUA.Message);
            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return blnValidReturn;
        }

        /// <summary>
        /// Envoie le répertoire à l'emplacement spécifié, si désiré, dans la corbeille.
        /// </summary>
        /// <param name="vstrRevisionFolderPath">Le chemin du répertoire à supprimer</param>
        /// <returns></returns>
        private bool pfblnDeleteDirectory(string vstrRevisionFolderPath, bool vblnAskBefore = false, bool vbnlReturnTrueIfAnsweredNo = true)
        {
            bool blnValidReturn = false;
            System.Windows.Forms.DialogResult answer;

            try
            {
                if (vblnAskBefore)
                {
                    answer = clsTTApp.GetAppController.ShowMessage(mintMSG_AreYouSureToDelete, System.Windows.Forms.MessageBoxButtons.YesNo, vstrRevisionFolderPath);
                }
                else
                {
                    answer = System.Windows.Forms.DialogResult.Yes;
                }

                if (answer == System.Windows.Forms.DialogResult.Yes && Directory.Exists(vstrRevisionFolderPath))
                {
                    Microsoft.VisualBasic.FileIO.FileSystem.DeleteDirectory(vstrRevisionFolderPath, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin, Microsoft.VisualBasic.FileIO.UICancelOption.ThrowException);
                    //System.Diagnostics.Process process = new System.Diagnostics.Process();
                    //System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();

                    //startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    //startInfo.FileName = "cmd.exe";
                    //startInfo.Arguments = @"/C RMDIR """ + vstrRevisionFolderPath + @""" /S /Q";

                    //process.StartInfo = startInfo;
                    //process.Start();
                    //process.WaitForExit();

                    blnValidReturn = true;
                }
                else
                {
                    blnValidReturn = vbnlReturnTrueIfAnsweredNo;
                }
            }
            catch (System.IO.IOException exUA)
            {
                blnValidReturn = false;
                mcActionResult.SetInvalid(sclsConstants.Validation_Message.PATH_ACCESS_DENIED, clsActionResults.BaseErrorCode.UNHANDLED_VALIDATION, exUA.Message);
            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return blnValidReturn;
        }

        private bool pfblnDeleteFrom_Rev_AllScripts(string vstrFolderWithFileToFindAndDelete)
        {
            bool blnValidReturn = false;
            int intFirstUnderscoreIndex = 0;
            int intLastDigitCharIndex = 0;
            string strScriptToFindName = string.Empty;
            string strRevAllScripts_Location = str_GetRevisionRootFolderPath(mcModRevision.TemplateSource.Template_NRI, mcModRevision.Version.VersionNo.ToString()).Replace(sclsAppConfigs.GetRevisionNumberPrefix + "XX", sclsAppConfigs.GetRevisionAllScriptFolderName);
            List<string> lstScripts;
            List<string> lstCorrespondingScriptsFounds = new List<string>();

            try
            {
                if (!string.IsNullOrEmpty(vstrFolderWithFileToFindAndDelete) && Directory.Exists(vstrFolderWithFileToFindAndDelete))
                {
                    if ((File.GetAttributes(vstrFolderWithFileToFindAndDelete) & FileAttributes.Directory) != FileAttributes.Directory)
                    {
                        lstScripts = Directory.GetFiles(Path.GetDirectoryName(vstrFolderWithFileToFindAndDelete)).OrderBy(i => i, new TT3LightDLL.Classes.NaturalStringComparer()).ToList();
                    }
                    else
                    {
                        lstScripts = Directory.GetFiles(vstrFolderWithFileToFindAndDelete).OrderBy(i => i, new TT3LightDLL.Classes.NaturalStringComparer()).ToList();
                    }


                    for (int intIndex = 0; intIndex < lstScripts.Count; intIndex++)
                    {
                        strScriptToFindName = Path.GetFileName(lstScripts[intIndex]);

                        intFirstUnderscoreIndex = strScriptToFindName.IndexOf("_");
                        intLastDigitCharIndex = new String(strScriptToFindName.TakeWhile(Char.IsDigit).ToArray()).Length;

                        if (intFirstUnderscoreIndex < 4 && intFirstUnderscoreIndex > -1) //On ne peut jamais avoir un script # en haut de 999
                        {
                            strScriptToFindName = strScriptToFindName.Substring(intFirstUnderscoreIndex + 1);
                        }
                        else if (intLastDigitCharIndex < 4) //Les premiers caractères pourraient être le numéro de script avec oubli d'ajouter un _
                        {
                            strScriptToFindName = strScriptToFindName.Substring(intLastDigitCharIndex);
                        }
                        else
                        {
                            //Do nothing, le script ne contient pas de numéro au début
                        }

                        if (Directory.Exists(strRevAllScripts_Location))
                            lstCorrespondingScriptsFounds = Directory.GetFiles(strRevAllScripts_Location, "*_" + strScriptToFindName, SearchOption.TopDirectoryOnly).ToList();//.Where(s => Regex.IsMatch(s, @"\d+(?:,\d{1,3})?")).ToList();

                        if (lstCorrespondingScriptsFounds.Count > 0)
                        {
                            File.Delete(lstCorrespondingScriptsFounds[lstCorrespondingScriptsFounds.Count - 1]);
                            //Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(lstCorrespondingScriptsFounds[lstCorrespondingScriptsFounds.Count - 1], Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing);
                        }
                    }
                }
                else
                {
                    //Do nothing, aucun script
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
        /// Envoie dans un fichier Zip le Release, les rapports, les applications satellites et les scripts dans des dossiers séparés.
        /// </summary>
        /// <param name="vstrExportFolderLocation">Le chemin ou le fichier zip sera créé.</param>
        /// <returns>True si l'export s'effectue correctement, sinon False.</returns>
        public bool blnExportRevisionKit(string vstrExportFolderLocation)
        {
            bool blnValidReturn = true;
            string strSQL = string.Empty;
            string strFolderName = string.Empty;
            string strVersionFolderRoot = string.Empty;
            string strCeritarClientName = string.Empty;
            string strNewZipFileLocation = string.Empty;
            string strReleaseLocation = string.Empty;
            string strLocationSatelliteExe = string.Empty;
            string strCurrentScriptFolderLocation = string.Empty;
            ZipArchiveMode archiveMode = ZipArchiveMode.Create;

            try
            {
                strCeritarClientName = mcView.GetCeritarClient_Name();

                strNewZipFileLocation = Path.Combine(vstrExportFolderLocation, mcView.GetCeritarApplication_Name() + @" Revision " + mcView.GetRevisionNo().ToString() + " - " + strCeritarClientName + @".zip");

                strReleaseLocation = mcView.GetLocation_Release();

                //Create the new archive file and add all the folders to it.
                if (File.Exists(strNewZipFileLocation)) File.Delete(strNewZipFileLocation);

                if (!mcView.GetScriptsOnly())
                {
                    //Add the release folder with the report application to the zip archive.
                    if (!string.IsNullOrEmpty(strReleaseLocation))
                    {
                        clsTTApp.GetAppController.blnDeleteFilesFromFolder(strReleaseLocation, sclsAppConfigs.GetReleaseInvalidExtensions);

                        if ((File.GetAttributes(strReleaseLocation) & FileAttributes.Directory) == FileAttributes.Directory)
                        {
                            ZipFile.CreateFromDirectory(strReleaseLocation, strNewZipFileLocation, CompressionLevel.Optimal, true);

                            archiveMode = ZipArchiveMode.Update;
                        }
                    }
                }
                
                using (ZipArchive newZipFile = ZipFile.Open(strNewZipFileLocation, archiveMode))
                {
                    if (!mcView.GetScriptsOnly())
                    {
                        //Add the release folder with the report application to the zip archive.
                        if (!string.IsNullOrEmpty(strReleaseLocation))
                        {
                            if ((File.GetAttributes(strReleaseLocation) & FileAttributes.Directory) == FileAttributes.Directory)
                            {
                                //foreach (string strCurrentFileToCopyPath in Directory.GetFiles(strReleaseLocation, "*.*", SearchOption.AllDirectories))
                                //{
                                //    newZipFile.CreateEntryFromFile(strCurrentFileToCopyPath, Path.Combine(sclsAppConfigs.GetReleaseFolderName, Path.GetFileName(strCurrentFileToCopyPath)));
                                //}
                            }
                            else if (mcView.GetExeIsExternalReport())
                            {
                                //Add the external report application to the zip archive
                                newZipFile.CreateEntryFromFile(strReleaseLocation, Path.Combine(new DirectoryInfo(strReleaseLocation).Parent.Name, Path.GetFileName(strReleaseLocation)), CompressionLevel.NoCompression);
                            }
                        }

                        //Add every satellites applications to the zip archive.
                        List<Interfaces.structSatRevision> lstSatellites = mcView.GetRevisionSatelliteList();

                        foreach (Interfaces.structSatRevision structSat in lstSatellites)
                        {
                            //Get the executable folder location to copy (from the version kit or from the latest revision)
                            strLocationSatelliteExe = clsTTSQL.str_ADOSingleLookUp("TOP 1 SatRevision.SRe_Exe_Location", "Revision INNER JOIN SatRevision ON SatRevision.Rev_NRI = Revision.Rev_NRI", "Revision.Ver_NRI = " + mcView.GetVersion_NRI() + " AND Revision.Rev_PreparationMode = 0 AND SatRevision.SRe_Exe_Location IS NOT NULL AND SatRevision.CSA_NRI = " + structSat.intCeritarSatelliteApp_NRI + " ORDER BY Revision.Rev_No DESC");

                            strLocationSatelliteExe = strLocationSatelliteExe == string.Empty ? structSat.strLocationSatelliteExe : strLocationSatelliteExe;

                            if (structSat.blnExeIsFolder && Directory.Exists(strLocationSatelliteExe))
                            {
                                //Ajoute les fichiers seuls
                                foreach (string strCurrentFileToCopyPath in Directory.GetFiles(strLocationSatelliteExe, "*.*", SearchOption.TopDirectoryOnly))
                                {
                                    newZipFile.CreateEntryFromFile(strCurrentFileToCopyPath, Path.Combine(structSat.strExportFolderName, Path.GetFileName(strCurrentFileToCopyPath)));
                                }

                                //Réplique les structures de répertoire
                                foreach (string strCurrentFileToCopyPath in Directory.GetDirectories(strLocationSatelliteExe, "*.*", SearchOption.TopDirectoryOnly))
                                {
                                    blnValidReturn = clsTTApp.GetAppController.blnAddDirectoryStructureToZipFile(newZipFile, strCurrentFileToCopyPath, structSat.strExportFolderName);
                                }
                            }
                            else if (File.Exists(strLocationSatelliteExe))
                            {
                                newZipFile.CreateEntryFromFile(strLocationSatelliteExe, Path.Combine(structSat.strExportFolderName, Path.GetFileName(strLocationSatelliteExe)));
                            }
                        }
                    }

                    //Scripts management
                    strCurrentScriptFolderLocation = mcView.GetLocation_Scripts();

                    //Add previous scripts folder for current client since last revision
                    if (mcView.IsPreviousRevisionScriptsIncluded() & mcView.GetRevisionNo() > 1)
                    {
                        int intCurrentPreviousRevisionNo = 0;
                        int intFileCount = 1;
                        string strVersionFolderPath = string.Empty;
                        string strLastRevisionFolderPath = string.Empty;
                        string[] lstRevisions;
                        List<int> lstRevisionToInclude = new List<int>();
                        int intCurrentCharIndex = 0;
                        int intPreviousCharIndex = 0;

                        blnValidReturn = blnGetOtherRevisionsNumberBetween(lstRevisionToInclude, mcView.GetRevisionsToInclude(), false, ref intCurrentCharIndex, ref intPreviousCharIndex);
                        
                        if (blnValidReturn)
                        {
                            strVersionFolderPath = Controllers.ctr_Version.str_GetVersionFolderPath(Int32.Parse(clsTTSQL.str_ADOSingleLookUp("Tpl_NRI", "Version", "Ver_NRI = " + mcView.GetVersion_NRI())), mcView.GetVersionNo().ToString());

                            lstRevisions = Directory.GetDirectories(strVersionFolderPath, sclsAppConfigs.GetRevisionNumberPrefix + "*", SearchOption.TopDirectoryOnly).OrderBy(i => i, new TT3LightDLL.Classes.NaturalStringComparer()).ToArray();

                            foreach (string strRevisionPath in lstRevisions)
                            {
                                blnValidReturn = true;

                                if (Directory.Exists(Path.Combine(strRevisionPath, sclsAppConfigs.GetScriptsFolderName)))
                                {
                                    intCurrentPreviousRevisionNo = Int32.Parse(Regex.Replace(Path.GetFileName(strRevisionPath).Substring(0, Path.GetFileName(strRevisionPath).IndexOf(" ")).ToString(), "[^0-9.]", ""));

                                    if (intCurrentPreviousRevisionNo > 0 && 
                                        (intCurrentPreviousRevisionNo < mcView.GetRevisionNo() || mcView.GetScriptsMerged()) && 
                                        lstRevisionToInclude.Contains(intCurrentPreviousRevisionNo))
                                    {
                                        foreach (string strCurrentFileToCopyPath in Directory.GetFiles(Path.Combine(strRevisionPath, sclsAppConfigs.GetScriptsFolderName), "*.sql", SearchOption.TopDirectoryOnly).OrderBy(i => i, new TT3LightDLL.Classes.NaturalStringComparer()).ToArray())
                                        {
                                            newZipFile.CreateEntryFromFile(strCurrentFileToCopyPath, Path.Combine((mcView.GetScriptsMerged() ? sclsAppConfigs .GetRevisionAllScriptFolderName: sclsAppConfigs.GetPreviousRevisionAllScriptFolderName), intFileCount.ToString() + "__" + Path.GetFileName(strCurrentFileToCopyPath)));

                                            intFileCount++;
                                        }

                                        //Copy all client's specific scripts at the end of the Rev_AllScripts folder
                                        if (Directory.GetDirectories(Path.Combine(strRevisionPath, sclsAppConfigs.GetScriptsFolderName), mcView.GetCeritarClient_Name() + "*", SearchOption.TopDirectoryOnly).Length > 0)
                                        {
                                            string[] lstSpecificScripts = Directory.GetFiles(Directory.GetDirectories(Path.Combine(strRevisionPath, sclsAppConfigs.GetScriptsFolderName), mcView.GetCeritarClient_Name() + "*", SearchOption.TopDirectoryOnly)[0]).OrderBy(i => i, new TT3LightDLL.Classes.NaturalStringComparer()).ToArray();
                                            string strNewScriptName = string.Empty;
                                            int intIndex = 0;
                                            int intNewScriptNumber = 0;
                                            int intTotalScriptsCount = 0;

                                            for (intIndex = 0; intIndex < lstSpecificScripts.Length; intIndex++)
                                            {
                                                strNewScriptName = Path.GetFileName(lstSpecificScripts[intIndex]);
                                                strNewScriptName = strNewScriptName.Substring(strNewScriptName.IndexOf("_") + 1);

                                                intNewScriptNumber = (intNewScriptNumber == 0 ? intTotalScriptsCount : intNewScriptNumber);

                                                strNewScriptName = intNewScriptNumber.ToString("00") + "_" + strNewScriptName;

                                                newZipFile.CreateEntryFromFile(lstSpecificScripts[intIndex], Path.Combine((mcView.GetScriptsMerged() ? sclsAppConfigs.GetRevisionAllScriptFolderName : sclsAppConfigs.GetPreviousRevisionAllScriptFolderName), intFileCount.ToString() + "S__" + Path.GetFileName(lstSpecificScripts[intIndex])));

                                                intNewScriptNumber++;
                                            }

                                            if (lstSpecificScripts.Length>0) intFileCount++;

                                            intTotalScriptsCount += lstSpecificScripts.Count();
                                        }
                                        else
                                        {
                                            //Do nothing
                                        }
                                    }
                                    else
                                    {
                                        //Continue, do nothing
                                    }
                                }
                                else if (!Directory.Exists(strRevisionPath))
                                {
                                    blnValidReturn = false;
                                }
                            }
                        }
                        {
                            //mcActionResult.SetInvalid()
                        }
                    }

                    //Add all current scripts folder to the zip archive.
                    if (!string.IsNullOrEmpty(strCurrentScriptFolderLocation) && (!mcView.GetScriptsMerged() || !mcView.IsPreviousRevisionScriptsIncluded()))
                    {
                        string strZipScriptsFolderPathName = (mcView.IsPreviousRevisionScriptsIncluded() ? sclsAppConfigs.GetRevisionNumberPrefix + mcView.GetRevisionNo() : sclsAppConfigs.GetScriptsFolderName);

                        if ((File.GetAttributes(strCurrentScriptFolderLocation) & FileAttributes.Directory) == FileAttributes.Directory)
                        {
                            foreach (string strCurrentFileToCopyPath in Directory.GetFiles(strCurrentScriptFolderLocation, "*.*", SearchOption.TopDirectoryOnly).OrderBy(i => i, new TT3LightDLL.Classes.NaturalStringComparer()).ToArray())
                            {
                                newZipFile.CreateEntryFromFile(strCurrentFileToCopyPath, Path.Combine(strZipScriptsFolderPathName, Path.GetFileName(strCurrentFileToCopyPath)));
                            }
                        }
                        else
                        {
                            newZipFile.CreateEntryFromFile(strCurrentScriptFolderLocation, Path.Combine(strZipScriptsFolderPathName, Path.GetFileName(strCurrentScriptFolderLocation)), CompressionLevel.NoCompression);
                        }

                        //Copy all client's specific scripts at the end of the scripts folder
                        if (Directory.GetDirectories(strCurrentScriptFolderLocation, strCeritarClientName + "*", SearchOption.TopDirectoryOnly).Length > 0)
                        {
                            string[] lstSpecificScripts = Directory.GetFiles(Directory.GetDirectories(strCurrentScriptFolderLocation, strCeritarClientName + "*", SearchOption.TopDirectoryOnly)[0]).OrderBy(i => i, new TT3LightDLL.Classes.NaturalStringComparer()).ToArray();
                            string strNewScriptName = string.Empty;
                            int intNewScriptNumber = 0;

                            for (int intIndex = 0; intIndex < lstSpecificScripts.Length; intIndex++)
                            {
                                strNewScriptName = Path.GetFileName(lstSpecificScripts[intIndex]);
                                strNewScriptName = strNewScriptName.Substring(strNewScriptName.IndexOf("_") + 1);

                                intNewScriptNumber = (intNewScriptNumber == 0 ? Directory.GetFiles(strCurrentScriptFolderLocation).Length + 1 : intNewScriptNumber);

                                strNewScriptName = intNewScriptNumber.ToString("00") + "S__" + strNewScriptName;

                                newZipFile.CreateEntryFromFile(lstSpecificScripts[intIndex], Path.Combine(strZipScriptsFolderPathName, strNewScriptName));

                                intNewScriptNumber++;
                            }
                        }
                    }
                }
            }
            catch (FileNotFoundException exPath)
            {
                blnValidReturn = false;
                mcActionResult.SetInvalid(sclsConstants.Validation_Message.INVALID_PATH, clsActionResults.BaseErrorCode.UNHANDLED_VALIDATION, exPath.FileName);
            }
            catch (System.IO.IOException exUA)
            {
                blnValidReturn = false;
                mcActionResult.SetInvalid(sclsConstants.Validation_Message.PATH_ACCESS_DENIED, clsActionResults.BaseErrorCode.UNHANDLED_VALIDATION, exUA.Message);
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

        public string strGetRevisionScriptsList(int vintRev_NRI)
        {
            System.Text.StringBuilder scriptsList = new System.Text.StringBuilder();
            string strCurrentScriptFolderLocation = string.Empty;

            try
            {
                strCurrentScriptFolderLocation = mcView.GetLocation_Scripts();

                if (!string.IsNullOrEmpty(strCurrentScriptFolderLocation))
                {
                    if ((File.GetAttributes(strCurrentScriptFolderLocation) & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        foreach (string strCurrentFileToCopyPath in Directory.GetFiles(strCurrentScriptFolderLocation, "*.*", SearchOption.TopDirectoryOnly).OrderBy(i => i, new TT3LightDLL.Classes.NaturalStringComparer()).ToArray())
                        {
                            scriptsList.Append(Path.GetFileName(strCurrentFileToCopyPath));
                            scriptsList.AppendLine();
                        }
                    }
                    else
                    {
                        scriptsList.Append(Path.GetFileName(strCurrentScriptFolderLocation));
                        scriptsList.AppendLine();
                    }
                }
                else
                {
                    scriptsList.AppendFormat("* Aucun scripts présent *", Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                scriptsList.AppendFormat("Error appending other scripts", Environment.NewLine); ;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return scriptsList.ToString();
        }

        private string strGetSatelliteFolderPathName(mod_SRe_SatelliteRevision rcSRe)
        {
            string strDestinationFolder = string.Empty;
            string strCeritarClientName = string.Empty;

            if (rcSRe.CeritarClient_NRI_Spec > 0)
                strCeritarClientName = mcSQL.str_ADOSingleLookUp_Trans("CeC_Name", "CerClient", "CeC_NRI = " + rcSRe.CeritarClient_NRI_Spec.ToString());
            else
                strCeritarClientName = mcView.GetCeritarClient_Name();

            strDestinationFolder = Path.Combine(sclsAppConfigs.GetRoot_INSTALLATIONS_ACTIVES,
                                                    rcSRe.CeritarSatelliteApp.Name,
                                                    (rcSRe.CeritarSatelliteApp.ExePerCustomer | rcSRe.CeritarClient_NRI_Spec > 0) ? strCeritarClientName : "",
                                                    sclsAppConfigs.GetVersionNumberPrefix + mcModRevision.Version.VersionNo.ToString(),
                                                    sclsAppConfigs.GetRevisionNumberPrefix + mcModRevision.Revision_Number.ToString()
                                                    );

            return strDestinationFolder;
        }

        public bool blnGetOtherRevisionsNumberBetween(List<int> rlstRevisionToInclude, string vstrAnalysed, bool vblnAddNumberInBetween, ref int rintCurrentCharIndex, ref int rintPreviousCharIndex)
        {
            bool blnValidReturn = true;
            int intRevisionNo = -1;

            try
            {
                while (rintCurrentCharIndex <= vstrAnalysed.Length - 1)
                {
                    if (vstrAnalysed.Substring(rintCurrentCharIndex, 1) == ";" || vstrAnalysed.Substring(rintCurrentCharIndex, 1) == ",")
                    {
                        int.TryParse(vstrAnalysed.Substring(rintPreviousCharIndex, rintCurrentCharIndex - rintPreviousCharIndex), out intRevisionNo);

                        if (intRevisionNo <= 0) return false;

                        rlstRevisionToInclude.Add(intRevisionNo);

                        if (vblnAddNumberInBetween)
                        {
                            blnValidReturn = blnAddNumberInBetweenLast2(rlstRevisionToInclude);

                            vblnAddNumberInBetween = false;
                        }
                            
                        rintPreviousCharIndex = rintCurrentCharIndex + 1;
                    }
                    else if (vstrAnalysed.Substring(rintCurrentCharIndex, 1) == "-")
                    {
                        int.TryParse(vstrAnalysed.Substring(rintPreviousCharIndex, rintCurrentCharIndex - rintPreviousCharIndex), out intRevisionNo);

                        if (intRevisionNo <= 0) return false;

                        rlstRevisionToInclude.Add(intRevisionNo);

                        if (vblnAddNumberInBetween)
                        {
                            blnValidReturn = blnAddNumberInBetweenLast2(rlstRevisionToInclude);

                            vblnAddNumberInBetween = false;
                        }

                        rintPreviousCharIndex = rintCurrentCharIndex + 1;
                        rintCurrentCharIndex = rintPreviousCharIndex + 1;

                        blnGetOtherRevisionsNumberBetween(rlstRevisionToInclude, vstrAnalysed, true, ref rintCurrentCharIndex, ref rintPreviousCharIndex);
                    }
                    rintCurrentCharIndex += 1;

                    if (!blnValidReturn) break;
                }

                if (blnValidReturn && rintCurrentCharIndex == vstrAnalysed.Length)
                {
                    int.TryParse(vstrAnalysed.Substring(rintPreviousCharIndex, rintCurrentCharIndex - rintPreviousCharIndex), out intRevisionNo);

                    if (intRevisionNo > 0)
                    {
                        rlstRevisionToInclude.Add(intRevisionNo);

                        if (vblnAddNumberInBetween) blnValidReturn = blnAddNumberInBetweenLast2(rlstRevisionToInclude);
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

        private bool blnAddNumberInBetweenLast2(List<int> rlstRevisionToInclude)
        {
            int intLastRevisionNo;

            if (rlstRevisionToInclude.Count < 2) return true;

            if (rlstRevisionToInclude[rlstRevisionToInclude.Count - 1] < rlstRevisionToInclude[rlstRevisionToInclude.Count - 2]) return false;

            intLastRevisionNo = rlstRevisionToInclude[rlstRevisionToInclude.Count - 1];

            rlstRevisionToInclude.RemoveAt(rlstRevisionToInclude.Count - 1);

            for (int intIndex = (rlstRevisionToInclude[rlstRevisionToInclude.Count - 1] + 1); intIndex <= intLastRevisionNo; intIndex++)
            {
                rlstRevisionToInclude.Add(intIndex);
            }

            return true;
        }

        public bool blnRevisionPathIsValid(int vintRev_NRI)
        {
            bool blnIsValid = false;
            int intRecordCount = -1;
            string strSQL = string.Empty;
            string strRevisionFolderPath = string.Empty;
            SqlDataReader sqlRecord = null;
            mod_Rev_Revision cRevision = new mod_Rev_Revision();

            try
            {
                strSQL = strSQL + " SELECT Revision.Rev_No, " + Environment.NewLine;
                strSQL = strSQL + "        Revision.Rev_NRI, " + Environment.NewLine;
                strSQL = strSQL + "        Revision.Rev_Location_Exe, " + Environment.NewLine;
                strSQL = strSQL + "        Revision.Rev_ExeIsReport, " + Environment.NewLine;
                strSQL = strSQL + "        Revision.Rev_ExeWithReport, " + Environment.NewLine;
                strSQL = strSQL + "        Revision.Rev_Location_Scripts, " + Environment.NewLine;
                strSQL = strSQL + "        Revision.Tpl_NRI, " + Environment.NewLine;
                strSQL = strSQL + "        SatRevision.SRe_Exe_Location, " + Environment.NewLine;
                strSQL = strSQL + "        CerSatApp.CSA_NRI, " + Environment.NewLine;
                strSQL = strSQL + "        CerSatApp.CSA_KitFolderName, " + Environment.NewLine;
                strSQL = strSQL + "        Version.Ver_No " + Environment.NewLine;

                strSQL = strSQL + " FROM Revision " + Environment.NewLine;
                strSQL = strSQL + "     INNER JOIN Version ON Version.Ver_NRI = Revision.Ver_NRI " + Environment.NewLine;
                strSQL = strSQL + "     LEFT JOIN SatRevision " + Environment.NewLine;
                strSQL = strSQL + "         INNER JOIN CerSatApp ON CerSatApp.CSA_NRI = SatRevision.CSA_NRI " + Environment.NewLine;
                strSQL = strSQL + "     ON SatRevision.Rev_NRI = Revision.Rev_NRI " + Environment.NewLine;

                strSQL = strSQL + " WHERE Revision.Rev_NRI = " + vintRev_NRI + Environment.NewLine;
                strSQL = strSQL + "   AND Revision.Rev_PreparationMode = 0 " + Environment.NewLine;

                strSQL = strSQL + " ORDER BY CASE WHEN CerSatApp.CSA_Name IS NULL THEN 0 ELSE 1 END, CerSatApp.CSA_Name " + Environment.NewLine;

                sqlRecord = clsTTSQL.ADOSelect(strSQL);

                while (sqlRecord.Read())
                {
                    if (intRecordCount == -1)
                    {
                        cRevision.Version = new mod_Ver_Version();
                        cRevision.Version.VersionNo = UInt16.Parse(sqlRecord["Ver_No"].ToString());
                        cRevision.Revision_Number = SByte.Parse(sqlRecord["Rev_No"].ToString());
                        cRevision.Path_Release = sqlRecord["Rev_Location_Exe"].ToString();
                        cRevision.ExeIsExternalReport = Convert.ToBoolean(sqlRecord["Rev_ExeIsReport"].ToString());
                        cRevision.ExeWithExternalReport = Convert.ToBoolean(sqlRecord["Rev_ExeWithReport"].ToString());
                        cRevision.Path_Scripts = sqlRecord["Rev_Location_Scripts"].ToString();
                        cRevision.Revision_NRI = Int32.Parse(sqlRecord["Rev_NRI"].ToString());
                        cRevision.TemplateSource = new Models.Module_Template.mod_Tpl_HierarchyTemplate();
                        cRevision.TemplateSource.Template_NRI = Int32.Parse(sqlRecord["Tpl_NRI"].ToString());

                        intRecordCount = 0;
                    }

                    cRevision.LstSatelliteRevisions.Add(new mod_SRe_SatelliteRevision());
                    cRevision.LstSatelliteRevisions[intRecordCount].Location_Exe = sqlRecord["SRe_Exe_Location"].ToString();
                    cRevision.LstSatelliteRevisions[intRecordCount].CeritarSatelliteApp = new mod_CSA_CeritarSatelliteApp();

                    if (sqlRecord["CSA_NRI"] != DBNull.Value)
                    {
                        cRevision.LstSatelliteRevisions[intRecordCount].CeritarSatelliteApp.CeritarSatelliteApp_NRI = Int32.Parse(sqlRecord["CSA_NRI"].ToString());
                        cRevision.LstSatelliteRevisions[intRecordCount].CeritarSatelliteApp.ExportFolderName = sqlRecord["CSA_KitFolderName"].ToString();
                    }
                    
                    intRecordCount++;
                }

                if (sqlRecord.HasRows & (!string.IsNullOrEmpty(cRevision.Path_Release) || !string.IsNullOrEmpty(cRevision.Path_Scripts)))
                {
                    strRevisionFolderPath = str_GetRevisionFolderPath(cRevision);

                    blnIsValid = Directory.Exists(strRevisionFolderPath);
                }
                else   
                    blnIsValid = true;
            }
            catch (FileNotFoundException exPath)
            {
                blnIsValid = false;
                mcActionResult.SetInvalid(sclsConstants.Validation_Message.INVALID_PATH, clsActionResults.BaseErrorCode.UNHANDLED_VALIDATION, exPath.FileName);
            }
            catch (Exception ex)
            {
                blnIsValid = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }
            finally
            {
                if (sqlRecord != null) sqlRecord.Dispose();
            }

            return blnIsValid;
        }

        //public string str_GetRevisionnFolderPath(int vintTemplate_NRI, string vstrVersion_No)
        //{
        //    string strSQL = string.Empty;
        //    string strPath = string.Empty;
        //    SqlDataReader sqlRecord = null;

        //    strSQL = strSQL + " WITH LstHierarchyComp " + Environment.NewLine;
        //    strSQL = strSQL + " AS " + Environment.NewLine;
        //    strSQL = strSQL + " ( " + Environment.NewLine;
        //    strSQL = strSQL + "     SELECT *, " + Environment.NewLine;
        //    strSQL = strSQL + " 		   CAST(0 AS varbinary(max)) AS Level " + Environment.NewLine;
        //    strSQL = strSQL + " 	FROM HierarchyComp  " + Environment.NewLine;
        //    strSQL = strSQL + " 	WHERE HiCo_Parent_NRI IS NULL " + Environment.NewLine;

        //    strSQL = strSQL + "     UNION ALL " + Environment.NewLine;

        //    strSQL = strSQL + "     SELECT HiCo_Childrens.*, " + Environment.NewLine;
        //    strSQL = strSQL + " 		   Level + CAST(HiCo_Childrens.HiCo_NRI AS varbinary(max)) AS Level " + Environment.NewLine;
        //    strSQL = strSQL + " 	FROM HierarchyComp HiCo_Childrens  " + Environment.NewLine;
        //    strSQL = strSQL + " 		INNER JOIN LstHierarchyComp on HiCo_Childrens.HiCo_Parent_NRI = LstHierarchyComp.HiCo_NRI " + Environment.NewLine;
        //    strSQL = strSQL + " 	WHERE HiCo_Childrens.HiCo_Parent_NRI IS NOT NULL " + Environment.NewLine;
        //    strSQL = strSQL + " ) " + Environment.NewLine;

        //    strSQL = strSQL + " SELECT FolderName = CASE WHEN LstHierarchyComp.FoT_NRI = " + (int)ctr_Template.FolderType.Revision_Number + " THEN CONVERT(VARCHAR(300), REPLACE(LstHierarchyComp.HiCo_Name, '_XXX', " + clsTTApp.GetAppController.str_FixStringForSQL("_" + vstrVersion_No) + ")) ELSE LstHierarchyComp.HiCo_Name END, " + Environment.NewLine;
        //    strSQL = strSQL + "        LstHierarchyComp.FoT_NRI " + Environment.NewLine;

        //    strSQL = strSQL + " FROM LstHierarchyComp " + Environment.NewLine;

        //    strSQL = strSQL + " WHERE LstHierarchyComp.TTP_NRI = " + (int)sclsAppConfigs.CONFIG_TYPE_NRI.PATH_INSTALLATIONS_ACTIVES + Environment.NewLine;
        //    strSQL = strSQL + "    OR LstHierarchyComp.Tpl_NRI = " + vintTemplate_NRI + Environment.NewLine;

        //    strSQL = strSQL + " ORDER BY Level " + Environment.NewLine;

        //    sqlRecord = clsTTSQL.ADOSelect(strSQL);

        //    while (sqlRecord.Read())
        //    {
        //        strPath = Path.Combine(strPath, sqlRecord["FolderName"].ToString());

        //        if (sqlRecord["FoT_NRI"].ToString() == ((int)ctr_Template.FolderType.Revision_Number).ToString()) break;
        //    }

        //    if (sqlRecord != null) sqlRecord.Dispose();

        //    return strPath;
        //}


#region "SQL Queries"

        public string strGetDataLoad_SQL(int vintVersion_NRI, int vintRevision_NRI)
        {
            string strSQL = string.Empty;
            string strNewRevisionNo = string.Empty;

            strNewRevisionNo = clsTTSQL.str_ADOSingleLookUp("MAX(ISNULL(Revision.Rev_No, 0)) + 1", "Version LEFT JOIN Revision ON Revision.Ver_NRI = Version.Ver_NRI", "(Revision.Rev_PreparationMode = 0 OR Revision.Rev_NRI IS NULL) AND Version.Ver_NRI = " + vintVersion_NRI);
            
            strSQL = strSQL + " SELECT Version.Ver_No, " + Environment.NewLine;
            strSQL = strSQL + "        Revision.Rev_TS, " + Environment.NewLine;
            strSQL = strSQL + "        Revision.Rev_DtCreation, " + Environment.NewLine;
            strSQL = strSQL + "        Revision.Tpl_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        Revision.Rev_Location_Exe, " + Environment.NewLine;
            strSQL = strSQL + "        Revision.Rev_Location_Scripts, " + Environment.NewLine;
            strSQL = strSQL + "        Revision.Rev_ExeIsReport, " + Environment.NewLine;
            strSQL = strSQL + "        Revision.Rev_ExeWithReport, " + Environment.NewLine;
            strSQL = strSQL + "        Revision.Rev_CreatedBy, " + Environment.NewLine;
            strSQL = strSQL + "        Revision.Rev_Note, " + Environment.NewLine;
            strSQL = strSQL + "        Revision.Rev_PreparationMode, " + Environment.NewLine;
            strSQL = strSQL + "        RevisionNo = CASE WHEN Revision.Rev_NRI IS NULL OR Revision.Rev_No IS NULL THEN " + clsTTApp.GetAppController.str_FixStringForSQL(strNewRevisionNo) + " ELSE Revision.Rev_No END, " + Environment.NewLine;
            strSQL = strSQL + "        CerApp.CeA_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        CerApp.CeA_Name, " + Environment.NewLine;
            strSQL = strSQL + "        CerApp.CeA_ExternalRPTAppName, " + Environment.NewLine;
            strSQL = strSQL + "        CerApp.CeA_NRI_Master " + Environment.NewLine;
            //strSQL = strSQL + "        CreatedByNom = TTUser.TTU_FirstName + ' ' + TTUser.TTU_LastName " + Environment.NewLine;
                
            strSQL = strSQL + " FROM Version " + Environment.NewLine;

            strSQL = strSQL + "     LEFT JOIN Revision ON Revision.Ver_NRI = Version.Ver_NRI " + Environment.NewLine;
            strSQL = strSQL + "                       AND Revision.Rev_NRI = " + vintRevision_NRI + Environment.NewLine;

            strSQL = strSQL + "     INNER JOIN CerApp ON CerApp.CeA_NRI = Version.CeA_NRI " + Environment.NewLine;
            //strSQL = strSQL + "     INNER JOIN TTUser ON TTUser.TTU_NRI = Revision.TTU_NRI " + Environment.NewLine;

            strSQL = strSQL + " WHERE Version.Ver_NRI = " + vintVersion_NRI + Environment.NewLine;

            return strSQL;
        }

        public string strGetListe_RevisionModifications_SQL(int vintRevision_NRI)
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT CerClient.CeC_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        CerClient.CeC_Name, " + Environment.NewLine;
            strSQL = strSQL + "        RevModifs.RevM_DtHr, " + Environment.NewLine;
            strSQL = strSQL + "        RevModifs.RevM_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        Action = '" + sclsConstants.DML_Mode.NO_MODE + "', " + Environment.NewLine;
            strSQL = strSQL + "        RevModifs.RevM_ChangeDesc " + Environment.NewLine;

            strSQL = strSQL + " FROM RevModifs " + Environment.NewLine;
            strSQL = strSQL + "     INNER JOIN CerClient ON CerClient.CeC_NRI = RevModifs.CeC_NRI_For " + Environment.NewLine;

            strSQL = strSQL + " WHERE RevModifs.Rev_NRI = " + vintRevision_NRI + Environment.NewLine;

            strSQL = strSQL + " ORDER BY CerClient.CeC_NRI, RevModifs.RevM_DtHr DESC " + Environment.NewLine;

            return strSQL;
        }

        public string strGetTemplates_SQL()
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT Template.Tpl_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        Template.Tpl_Name " + Environment.NewLine;

            strSQL = strSQL + " FROM Template " + Environment.NewLine;

            strSQL = strSQL + "     INNER JOIN Version ON Version.Tpl_NRI = Template.Tpl_NRI_Ref AND Version.Ver_NRI = " + mcView.GetVersion_NRI() + Environment.NewLine;

            strSQL = strSQL + " WHERE Template.TeT_NRI = " + (int)ctr_Template.TemplateType.REVISION + Environment.NewLine;
            strSQL = strSQL + "   AND Template.CeA_NRI = Version.CeA_NRI " + Environment.NewLine;

            strSQL = strSQL + " ORDER BY Template.Tpl_ByDefault DESC, Template.Tpl_Name " + Environment.NewLine;

            return strSQL;
        }

        public string strGetClients_SQL(int vintRevision_NRI, string vstrCeritarClientToIgnore = "")
        {
            string strSQL = string.Empty;

            vstrCeritarClientToIgnore = (string.IsNullOrEmpty(vstrCeritarClientToIgnore) ? vstrCeritarClientToIgnore = "0" : vstrCeritarClientToIgnore);

            strSQL = strSQL + " SELECT CerClient.CeC_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        CerClient.CeC_Name " + Environment.NewLine;

            strSQL = strSQL + " FROM Version " + Environment.NewLine;

            strSQL = strSQL + "     INNER JOIN ClientAppVersion " + Environment.NewLine;
            strSQL = strSQL + "          INNER JOIN CerClient ON CerClient.CeC_NRI = ClientAppVersion.CeC_NRI " + Environment.NewLine;
            strSQL = strSQL + "     ON ClientAppVersion.Ver_NRI = Version.Ver_NRI " + Environment.NewLine;
            strSQL = strSQL + "     LEFT JOIN Revision ON Revision.Rev_NRI = " + vintRevision_NRI + Environment.NewLine;

            strSQL = strSQL + " WHERE Version.Ver_NRI = " + mcView.GetVersion_NRI() + Environment.NewLine;
            strSQL = strSQL + "   AND (NOT EXISTS (SELECT 1 FROM Revision WHERE Revision.Ver_NRI = Version.Ver_NRI AND Revision.Rev_PreparationMode = 1 AND EXISTS (SELECT 1 FROM ClientAppRevision CAR WHERE CAR.CeC_NRI = ClientAppVersion.CeC_NRI AND CAR.Rev_NRI = Revision.Rev_NRI) AND Revision.Rev_NRI <> " + vintRevision_NRI.ToString() + ") " + Environment.NewLine;
            strSQL = strSQL + "        OR Revision.Rev_No IS NOT NULL) " + Environment.NewLine;
            strSQL = strSQL + "   AND CerClient.CeC_NRI NOT IN (" + vstrCeritarClientToIgnore + ") AND CerClient.CeC_IsActive = 1 " + Environment.NewLine;

            strSQL = strSQL + " ORDER BY CerClient.CeC_Name " + Environment.NewLine;

            return strSQL;
        }

        public string strGetListe_Clients_SQL(int vintRevision_NRI)
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT Action = CASE WHEN ClientAppRevision.CAR_NRI IS NULL THEN '" + sclsConstants.DML_Mode.NO_MODE + "' ELSE '" + sclsConstants.DML_Mode.UPDATE_MODE + "' END," + Environment.NewLine;
            strSQL = strSQL + "        ClientAppRevision.CAR_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        ClientAppRevision.CAR_TS, " + Environment.NewLine;
            strSQL = strSQL + "        CerClient.CeC_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        CerClient.CeC_Name " + Environment.NewLine;

            strSQL = strSQL + " FROM Revision " + Environment.NewLine;
        
            strSQL = strSQL + "     INNER JOIN ClientAppRevision " + Environment.NewLine;
            strSQL = strSQL + "          INNER JOIN CerClient ON CerClient.CeC_NRI = ClientAppRevision.CeC_NRI " + Environment.NewLine;
            strSQL = strSQL + "     ON ClientAppRevision.Rev_NRI = Revision.Rev_NRI " + Environment.NewLine;
            
            strSQL = strSQL + " WHERE Revision.Rev_NRI = " + vintRevision_NRI + Environment.NewLine;

            strSQL = strSQL + " ORDER BY CerClient.CeC_Name " + Environment.NewLine;

            return strSQL;
        }

        public string strGetListe_SatelliteApps_SQL(int vintCeritarClient_NRI)
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT Action = CASE WHEN SatRevision.SRe_NRI IS NULL THEN '" + sclsConstants.DML_Mode.NO_MODE + "' ELSE '" + sclsConstants.DML_Mode.NO_MODE + "' END," + Environment.NewLine;
            strSQL = strSQL + "        CerSatApp.CSA_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        CerSatApp.CSA_Name, " + Environment.NewLine;
            strSQL = strSQL + "        SatRevision.SRe_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        SatRevision.SRe_Exe_Location, " + Environment.NewLine;
            strSQL = strSQL + "        CerSatApp.CSA_ExeIsFolder, " + Environment.NewLine;
            strSQL = strSQL + "        CerSatApp.CSA_KitFolderName, " + Environment.NewLine;
            strSQL = strSQL + "        ClientSatVersion.CSV_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        ClientSatVersion.CSV_ExePerCustomer " + Environment.NewLine;

            strSQL = strSQL + " FROM Version " + Environment.NewLine;
          
            strSQL = strSQL + "     LEFT JOIN Revision ON Revision.Rev_NRI = " + mcView.GetRevision_NRI() + Environment.NewLine;
   
            strSQL = strSQL + "     INNER JOIN ClientSatVersion  " + Environment.NewLine;
            strSQL = strSQL + "         INNER JOIN CerSatApp ON CerSatApp.CSA_NRI = ClientSatVersion.CSA_NRI  " + Environment.NewLine;
            strSQL = strSQL + " 	ON ClientSatVersion.Ver_NRI = Version.Ver_NRI  " + Environment.NewLine;
            strSQL = strSQL + "    AND ClientSatVersion.CSA_NRI = CerSatApp.CSA_NRI  " + Environment.NewLine;
            strSQL = strSQL + "    AND ClientSatVersion.CeC_NRI = " + vintCeritarClient_NRI + Environment.NewLine;

            strSQL = strSQL + "     LEFT JOIN SatRevision ON SatRevision.Rev_NRI = Revision.Rev_NRI AND SatRevision.CSA_NRI = CerSatApp.CSA_NRI " + Environment.NewLine;
            strSQL = strSQL + "                          AND (SatRevision.CeC_NRI_Spec IS NULL OR SatRevision.CeC_NRI_Spec = " + vintCeritarClient_NRI + ")" + Environment.NewLine;

            strSQL = strSQL + " WHERE Version.Ver_NRI = " + mcView.GetVersion_NRI() + Environment.NewLine;

            strSQL = strSQL + " ORDER BY CerSatApp.CSA_Name " + Environment.NewLine;

            return strSQL;
        }

        /// <summary>
        /// Permet d'obtenir la liste des applications satellites distincte impliquees dans la revision par un ou un autre client
        /// </summary>
        /// <param name="vintCeritarClient_NRI"></param>
        /// <returns></returns>
        public string strGetSatelliteApps_SQL(int rintRevision_NRI, bool vblnDistinctOnly)
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT DISTINCT CerSatApp.CSA_NRI, " + Environment.NewLine;
            strSQL = strSQL + "                 CerSatApp.CSA_Name, " + Environment.NewLine;
            strSQL = strSQL + "                 CerSatApp.CSA_ExePerCustomer, " + Environment.NewLine;
            if (!vblnDistinctOnly)
            {
                strSQL = strSQL + "                 SatRevision.CeC_NRI_Spec, " + Environment.NewLine;
                strSQL = strSQL + "                 SatRevision.SRe_NRI, " + Environment.NewLine;
                strSQL = strSQL + "                 SatRevision.SRe_Exe_Location, " + Environment.NewLine;
                strSQL = strSQL + "                 CerClient.CeC_Name, " + Environment.NewLine;
            }
                
            strSQL = strSQL + "                 CerSatApp.CSA_KitFolderName, " + Environment.NewLine;
            strSQL = strSQL + "                 CerSatApp.CSA_ExeIsFolder " + Environment.NewLine;

            strSQL = strSQL + " FROM Revision (NOLOCK) " + Environment.NewLine;

            strSQL = strSQL + "     INNER JOIN SatRevision (NOLOCK) ON SatRevision.Rev_NRI = Revision.Rev_NRI " + Environment.NewLine;
            strSQL = strSQL + "     LEFT JOIN CerClient ON CerClient.CeC_NRI = SatRevision.CeC_NRI_Spec " + Environment.NewLine;
            strSQL = strSQL + "     INNER JOIN CerSatApp ON CerSatApp.CSA_NRI = SatRevision.CSA_NRI " + Environment.NewLine;

            strSQL = strSQL + " WHERE Revision.Rev_NRI = " + rintRevision_NRI + Environment.NewLine;


            //else if (!mcSQL.bln_AddField("CSV_NRI", _intClientSatVersion_NRI, clsTTSQL.MySQL_FieldTypes.NRI_TYPE))
            //{ }


            return strSQL;
        }

        #endregion


    }
}
