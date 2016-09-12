using System;
using System.Collections.Generic;
using Ceritar.TT3LightDLL.Classes;
using Ceritar.TT3LightDLL.Static_Classes;
using Ceritar.CVS.Models.Module_ActivesInstallations;

namespace Ceritar.CVS.Controllers
{
    /// <summary>
    /// Cette classe représente le controleur qui fait le lien entre la vue permettant de définir les usagers de l'application et le modèle mod_TTU_User.
    /// Elle passe par l'interface IUser afin d'extraire les informations de la vue.
    /// </summary>
    public class ctr_User
    {
        private Interfaces.IUser mcView;
        private mod_TTU_User mcModUser = null;
        private clsActionResults mcActionResult;
        private clsSQL mcSQL;
        
        public enum ErrorCode_TTU
        {
            FIRST_NAME_MANDATORY = 1,
            LAST_NAME_MANDATORY = 2,
            CODE_MANDATORY = 3,
            CODE_UNIQUE = 4,
            EMAIL_INVALID = 5
        }


        public ctr_User(Interfaces.IUser rView)
        {
            mcModUser = new mod_TTU_User();

            mcView = rView;
        }

        public clsActionResults Validate()
        {
            try
            {
                mcModUser = new mod_TTU_User();

                mcModUser.DML_Action = mcView.GetDML_Mode();
                mcModUser.Email = mcView.GetEMail();
                mcModUser.Firstname = mcView.GetFirstName();
                mcModUser.Lastname = mcView.GetLastName();
                mcModUser.Password = mcView.GetPassword();
                mcModUser.User_NRI = mcView.GetUser_NRI();
                mcModUser.User_TS = mcView.GetUser_TS();
                mcModUser.UserCode = mcView.GetCode();

                mcActionResult = mcModUser.Validate();
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
            try
            {
                mcSQL = new clsSQL();

                if (mcSQL.bln_BeginTransaction())
                {
                    mcModUser.SetcSQL = mcSQL;

                    mcModUser.blnSave();

                    mcActionResult = mcModUser.ActionResults;
                }
            }
            catch (Exception ex)
            {
                mcActionResult.SetInvalid(sclsConstants.Error_Message.ERROR_UNHANDLED, clsActionResults.BaseErrorCode.UNHANDLED_EXCEPTION);
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }
            finally
            {
                mcSQL.bln_EndTransaction(mcActionResult.IsValid);
                mcSQL = null;
            }

            return mcActionResult;
        }


#region "SQL Queries"

        public string strGetDataLoad_SQL(int vintTTUser_NRI)
        {
            string strSQL = string.Empty;

            strSQL = strSQL + " SELECT TTUser.TTU_NRI, " + Environment.NewLine;
            strSQL = strSQL + "        TTUser.TTU_TS, " + Environment.NewLine;
            strSQL = strSQL + "        TTUser.TTU_Code, " + Environment.NewLine;
            strSQL = strSQL + "        TTUser.TTU_FirstName, " + Environment.NewLine;
            strSQL = strSQL + "        TTUser.TTU_LastName, " + Environment.NewLine;
            strSQL = strSQL + "        TTUser.TTU_Password, " + Environment.NewLine;
            strSQL = strSQL + "        TTUser.TTU_Email " + Environment.NewLine;

            strSQL = strSQL + " FROM TTUser " + Environment.NewLine;

            strSQL = strSQL + " WHERE TTUser.TTU_NRI = " + vintTTUser_NRI + Environment.NewLine;

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

#endregion


    }
}
