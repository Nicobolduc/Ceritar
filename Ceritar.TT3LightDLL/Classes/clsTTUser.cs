using System;
using System.Collections.Generic;
using System.Management;

namespace Ceritar.TT3LightDLL.Classes
{
    /// <summary>
    /// Cette classe est une petite classe générique représentant l'utilisateur courant de l'application.
    /// </summary>
    public class clsTTUser
    {
        //Working variables
        private short _intLanguage = 1;
        private int _intUser_NRI = 101;
        private string _strUser_Code = string.Empty;
        private string _strUserComputerID = string.Empty;


#region "Properties"

        public short UserLanguage
        {
            get { return _intLanguage; }
            set { _intLanguage = value; }
        }

        public int User_NRI
        {
            get { return _intUser_NRI; }
            set { _intUser_NRI = value; }
        }

        public string User_Code
        {
            get { return _strUser_Code; }
            set { _strUser_Code = value; }
        }

        public string GetUserComputerID
        {
            get { return (string.IsNullOrEmpty(_strUserComputerID) ? str_GetUserComputerID() : _strUserComputerID); }
        }

#endregion


        private string str_GetUserComputerID()
        {
            string strCPU_ID = string.Empty;
            string strDrive = "C";
            string strVolumeSerial;

            try
            {
                ManagementClass cManClass = new ManagementClass("win32_processor");
                ManagementObjectCollection lstManObject = cManClass.GetInstances();
                ManagementObject localDisk_ID;

                foreach (ManagementObject cManObject in lstManObject)
                {
                    strCPU_ID = cManObject.Properties["processorID"].Value.ToString();

                    break;
                }

                localDisk_ID = new ManagementObject(@"win32_logicaldisk.deviceid=""" + strDrive + @":""");
                localDisk_ID.Get();
                strVolumeSerial = localDisk_ID["VolumeSerialNumber"].ToString();

                _strUserComputerID = strCPU_ID + strVolumeSerial;
            }
            catch (Exception ex)
            {
                Static_Classes.sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return _strUserComputerID;
        }

        public bool bln_SaveIniConfiguration(string vstrIni_Section, string vstrIni_Config, string vstrIni_Value)
        {
            bool blnValidReturn = false;
            int intPlaceHolder = 0;
            clsTTSQL cSQL = new clsTTSQL();

            try
            {
                if (!cSQL.bln_RefreshFields())
                { }
                else if (!cSQL.bln_AddField("TTIni_Section", vstrIni_Section, clsTTSQL.MySQL_FieldTypes.VARCHAR_TYPE))
                { }
                else if (!cSQL.bln_AddField("TTIni_Config", vstrIni_Config, clsTTSQL.MySQL_FieldTypes.VARCHAR_TYPE))
                { }
                else if (!cSQL.bln_AddField("TTIni_Value", vstrIni_Value, clsTTSQL.MySQL_FieldTypes.VARCHAR_TYPE))
                { }
                else if (!cSQL.bln_AddField("TTIni_ComputerID", GetUserComputerID, clsTTSQL.MySQL_FieldTypes.VARCHAR_TYPE))
                { }
                else
                {
                    blnValidReturn = true;
                }
                
                if (blnValidReturn)
                {
                    if (string.IsNullOrEmpty(clsTTSQL.str_ADOSingleLookUp("TTIni_ComputerID", "TTini", "TTIni_Section = '" + vstrIni_Section + "' AND TTIni_Config = '" + vstrIni_Config + "' AND TTIni_ComputerID = '" + GetUserComputerID + "'")))
                    {
                        blnValidReturn = cSQL.bln_ADOInsert("TTIni", out intPlaceHolder);
                    }
                    else
                    {
                        blnValidReturn = cSQL.bln_ADOUpdate("TTIni", "TTIni_Section = '" + vstrIni_Section + "' AND TTIni_Config = '" + vstrIni_Config + "' AND TTIni_ComputerID = '" + GetUserComputerID + "'");
                    }
                }
            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                Static_Classes.sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return blnValidReturn;
        }
    }
}
