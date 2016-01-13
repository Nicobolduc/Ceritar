using System;
using System.IO;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Ceritar.CVS.Models.Module_ActivesInstallations;
using Ceritar.CVS.Controllers.Interfaces;
using Ceritar.TT3LightDLL.Static_Classes;
using Ceritar.TT3LightDLL.Classes;

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
            VERSION_NO_UNIQUE_AND_BIGGER_PREVIOUS = 10
        }

        public clsActionResults GetActionResult
        {
            get { return mcActionResult; }
        }

        public ctr_Version(IVersion rView)
        {
            mcModVersion = new mod_Ver_Version();
            
            mcView = rView;

            mcActionResult = new clsActionResults();
        }

        public clsActionResults Validate()
        {
            try
            {
                mcModVersion.DML_Action = mcView.GetDML_Action();
                mcModVersion.Version_NRI = mcView.GetVersion_NRI();
                mcModVersion.Version_TS = mcView.GetVersion_TS();
                mcModVersion.VersionNo = mcView.GetVersionNo();
                mcModVersion.CompiledBy = mcView.GetCompiledBy();
                mcModVersion.CerApplication = new Models.Module_ActivesInstallations.mod_CeA_CeritarApplication();
                mcModVersion.CerApplication.CeritarApplication_NRI = mcView.GetCeritarApplication_NRI();
                mcModVersion.Location_APP_CHANGEMENT = mcView.GetLocation_APP_CHANGEMENT();
                mcModVersion.Location_Release = mcView.GetLocation_Release();
                mcModVersion.Location_TTApp = mcView.GetLocation_TTApp();
                mcModVersion.TemplateSource = new Models.Module_Template.mod_Tpl_HierarchyTemplate();
                mcModVersion.TemplateSource.Template_NRI = mcView.GetTemplateSource_NRI();
                mcModVersion.CreationDate = mcView.GetCreationDate();

                mcActionResult = mcModVersion.Validate();
            }
            catch (Exception ex)
            {
                mcActionResult.SetInvalid(sclsConstants.Error_Message.ERROR_UNHANDLED, clsActionResults.BaseErrorCode.UNHANDLED_EXCEPTION);
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }
            finally
            {
                if (!mcActionResult.IsValid) mcModVersion = new mod_Ver_Version();
            }

            return mcActionResult;
        }

        public clsActionResults Validate_Client(structClientAppVersion vstructCAV)
        {
            try
            {
                mod_CAV_ClientAppVersion cCAV;

                cCAV = new mod_CAV_ClientAppVersion();
                cCAV.DML_Action = vstructCAV.Action;
                cCAV.ClientAppVersion_NRI = vstructCAV.intClientAppVersion_NRI;
                cCAV.ClientAppVersion_TS = vstructCAV.intClientAppVersion_TS;
                cCAV.CeritarApplication_NRI = mcView.GetCeritarApplication_NRI();
                cCAV.CeritarClient_NRI = vstructCAV.intCeritarClient_NRI;
                cCAV.Installed = vstructCAV.blnInstalled;
                cCAV.IsCurrentVersion = vstructCAV.blnIsCurrentVersion;
                cCAV.License = vstructCAV.strLicense;
                cCAV.Version_NRI = mcView.GetVersion_NRI();

                mcModVersion.LstClientsUsing.Add(cCAV);

                mcActionResult = mcModVersion.LstClientsUsing[mcModVersion.LstClientsUsing.Count - 1].Validate();
            }
            catch (Exception ex)
            {
                mcActionResult.SetInvalid(sclsConstants.Error_Message.ERROR_UNHANDLED, clsActionResults.BaseErrorCode.UNHANDLED_EXCEPTION);
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }
            finally
            {
                if (!mcActionResult.IsValid) mcModVersion.LstClientsUsing.Clear();
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

                    if (blnValidReturn & mcActionResult.IsValid & mcModVersion.DML_Action == sclsConstants.DML_Mode.INSERT_MODE)
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
                mcModVersion = new mod_Ver_Version();
            }

            return mcActionResult;
        }

        /// <summary>
        /// Construit la hiérarchie reçu en paramètre à partir de ce qui est dans la base de données.
        /// Boucle sur chaque niveau de la hiérarchie et en fonction du type de dossier, effectue divers traitements.
        /// </summary>
        /// <param name="vintTemplate_NRI">Le NRI du gabarit à utiliser.</param>
        /// <returns>Une valeur indiquant si la génération s'est effectuée avec succès.</returns>
        public bool blnBuildVersionHierarchy(int vintTemplate_NRI)
        {
            bool blnValidReturn = false;
            string strSQL = string.Empty;
            string strFolderName = string.Empty;
            int intPreviousFolderLevel = -1;
            SqlDataReader cSQLReader = null;
            DirectoryInfo currentFolderInfos = null;


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

                            if (!string.IsNullOrEmpty(mcView.GetLocation_Release()))
                            {
                                //TODO clean release folder

                                clsApp.GetAppController.blnCopyFolderContent(mcView.GetLocation_Release(), currentFolderInfos.FullName);
                            }

                            break;

                        case (int)ctr_Template.FolderType.CaptionsAndMenus:

                            if (!string.IsNullOrEmpty(mcView.GetLocation_TTApp()))
                            {
                                File.Copy(mcView.GetLocation_TTApp(), Path.Combine(currentFolderInfos.FullName, sclsAppConfigs.GetCaptionsAndMenusFileName), true);
                            }

                            break;

                        case (int)ctr_Template.FolderType.Scripts:

                            blnValidReturn = pfblnCopyAllScriptsForClients(currentFolderInfos.FullName);

                            break;

                        case (int)ctr_Template.FolderType.Version_Number:

                            if (!string.IsNullOrEmpty(mcView.GetLocation_APP_CHANGEMENT()))
                            {
                                File.Copy(mcView.GetLocation_APP_CHANGEMENT(), Path.Combine(currentFolderInfos.FullName, Path.GetFileName(mcView.GetLocation_APP_CHANGEMENT())), true);
                            }

                            break;
                    }

                    intPreviousFolderLevel = Int32.Parse(cSQLReader["HiCo_NodeLevel"].ToString());
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
            List<structClientAppVersion> lstClients;

            try
            {
                lstClients = mcView.GetClientsList();

                foreach (structClientAppVersion structClient in lstClients)
                {
                    if (mcSQL == null)
                    {
                        intActiveVersionInProd = UInt16.Parse(clsSQL.str_ADOSingleLookUp("ISNULL(MAX(Version.Ver_No), 0)", "ClientAppVersion INNER JOIN Version ON Version.Ver_NRI = ClientAppVersion.Ver_NRI", "ClientAppVersion.CAV_IsCurrentVersion = 1 AND Version.CeA_NRI = " + mcView.GetCeritarApplication_NRI() + " AND ClientAppVersion.CeC_NRI = " + structClient.intCeritarClient_NRI));
                    }
                    else
                    {
                        intActiveVersionInProd = UInt16.Parse(mcSQL.str_ADOSingleLookUp_Trans("ISNULL(MAX(Version.Ver_No), 0)", "ClientAppVersion INNER JOIN Version ON Version.Ver_NRI = ClientAppVersion.Ver_NRI", "ClientAppVersion.CAV_IsCurrentVersion = 1 AND Version.CeA_NRI = " + mcView.GetCeritarApplication_NRI() + " AND ClientAppVersion.CeC_NRI = " + structClient.intCeritarClient_NRI));
                    }
                    
                    lstVersionsFolders = Directory.GetDirectories(Path.Combine(sclsAppConfigs.GetRoot_DB_UPGRADE_SCRIPTS, mcView.GetCeritarApplication_Name()));

                    foreach (string strCurrentVersionFolderToCopy_Path in lstVersionsFolders)
                    {
                        intCurrentFolder_VersionNo = UInt16.Parse(Regex.Replace(new DirectoryInfo(strCurrentVersionFolderToCopy_Path).Name, @"[^0-9]+", ""));

                        if (intCurrentFolder_VersionNo > intActiveVersionInProd & intCurrentFolder_VersionNo <= mcView.GetVersionNo())
                        {
                            clsApp.GetAppController.blnCopyFolderContent(strCurrentVersionFolderToCopy_Path, 
                                                                         Path.Combine(vstrDestinationFolderPath,
                                                                                      structClient.strCeritarClient_Name,
                                                                                      intCurrentFolder_VersionNo.ToString()),
                                                                         true,
                                                                         true);
                        }
                        else
                        {
                            //Do nothing
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

            return blnValidReturn;
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
            strSQL = strSQL + "        Version.CeA_NRI " + Environment.NewLine;

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
            strSQL = strSQL + "        ClientAppVersion.CAV_License " + Environment.NewLine;

            strSQL = strSQL + " FROM ClientAppVersion " + Environment.NewLine;
            strSQL = strSQL + "     INNER JOIN CerClient ON CerClient.CeC_NRI = ClientAppVersion.CeC_NRI " + Environment.NewLine;

            strSQL = strSQL + " WHERE ClientAppVersion.Ver_NRI = " + vintVersion_NRI + Environment.NewLine;

            strSQL = strSQL + " ORDER BY CerClient.CeC_Name " + Environment.NewLine;

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

        public string strGetClients_SQL()
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT CerClient.CeC_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        CerClient.CeC_Name " + Environment.NewLine;

            strSQL = strSQL + " FROM CerClient " + Environment.NewLine;

            strSQL = strSQL + " ORDER BY CerClient.CeC_Name " + Environment.NewLine;

            return strSQL;
        }

#endregion

    }
}
