using Ceritar.TT3LightDLL.Classes;
using Ceritar.TT3LightDLL.Static_Classes;
using System;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.IO.Compression;
using System.Runtime.InteropServices;

namespace Ceritar.TT3LightDLL.Classes
{
    /// <summary>
    /// Cette classe est un genre de controleur des propriétés de l'application. Par exemple, elle gère la connexion au server de base de données et offre des fonctionnalités 
    /// communes et utilisables par tous les écrans de l'application.
    /// </summary>
    public sealed class clsTTApp
    {
        [DllImport("shlwapi.dll", EntryPoint = "PathRelativePathTo")]
        static extern bool PathRelativePathTo(System.Text.StringBuilder lpszDst, string from, UInt32 attrFrom, string to, UInt32 attrTo);

        //Private class members
        private SqlConnection mcSQLConnection;
        private clsTTUser mcTTUser;
        private System.Text.RegularExpressions.Regex mcStringCleaner = new System.Text.RegularExpressions.Regex("'", System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.CultureInvariant | System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        private string mstrServerDateFormat = string.Empty;
        private Form mfrmMdi = null;
        private static clsTTApp _myUniqueInstance;
        

#region "Constructors"

        private clsTTApp(Form rfrmMDI)
        {
            mcTTUser = new clsTTUser();

            mfrmMdi = rfrmMDI;

            clsTTSQL.OpenSQLServerConnection(ref mcSQLConnection);
        }

#endregion


#region "Properties"

        public static clsTTApp GetAppController
        {
            get
            {
                return _myUniqueInstance;
            }
        }

        public SqlConnection SQLConnection
        {
            get { return mcSQLConnection; }
        }

        public clsTTUser cUser
        {
            get { return this.mcTTUser; }
        }

        public Form GetMDI
        {
            get { return mfrmMdi;  }
        }

        public string str_GetUserDateFormat
        {
            get
            {
                switch (mcTTUser.UserLanguage)
                {
                    case (short)sclsConstants.Language.FRENCH_QC:
                        // Return "dd-MM-yyyy"

                        return "yyyy-MM-dd";

                    case (short)sclsConstants.Language.ENGLISH_CA:
                        // Return "MM-dd-yyyy"

                        return "yyyy-MM-dd";

                    default:
                        return System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern;
                }
            }
        }

        public string str_GetUserDateTimeFormat
        {
            get
            {
                switch (mcTTUser.UserLanguage)
                {
                    case (short)sclsConstants.Language.FRENCH_QC:
                        //Return "dd-MM-yyyy HH:mm:ss"

                        return "yyyy-MM-dd HH:mm:ss";
                    case (short)sclsConstants.Language.ENGLISH_CA:
                        //Return "MM-dd-yyyy HH:mm:ss"

                        return "yyyy-MM-dd HH:mm:ss";
                    default:
                        return System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern + " " + System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat.ShortTimePattern;
                }
            }
        }

        public string str_GetServerTimeFormat
        {
            get { return "hh:mm"; }
        }

        public string str_GetServerDateFormat
        {
            get { return "MM-dd-yyyy"; }
        }

        public string str_GetServerDateTimeFormat
        {
            get 
            {
                if (mstrServerDateFormat == string.Empty) mstrServerDateFormat = clsTTSQL.str_ADOSingleLookUp("TTP_Value", "TTParam", "TTP_Name = 'Server date format'");

                return mstrServerDateFormat; 
            }
        }

#endregion


#region "Functions / Subs"

        public static void Instanciate(Form rfrmMDI)
        {
            _myUniqueInstance = new clsTTApp(rfrmMDI);
        }

        public bool bln_CTLBindCaption(ref Control rControl)
        {
            bool blnValidReturn = false;
            string strCaption = string.Empty;

            try
            {

                clsTTSQL.str_ADOSingleLookUp("TTAC_Text", "TTAppCaption", "TTAC_NRI = " + Convert.ToString(rControl.Tag));

            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return blnValidReturn;
        }

        public bool blnDeleteFilesFromFolder(string vstrFolderPath, string[] vlstExtensionsToDelete = null)
        {
            bool blnValidReturn = false;
            
            try
            {
                if (Directory.Exists(vstrFolderPath))
                {
                    blnValidReturn = true;

                    foreach (string strFile in Directory.GetFiles(vstrFolderPath, "*.*", SearchOption.AllDirectories).Where(f => vlstExtensionsToDelete.Contains(System.IO.Path.GetExtension(f).ToLower())))
                    {
                        File.Delete(strFile);
                    }
                    foreach (string strFolder in Directory.GetDirectories(vstrFolderPath))
                    {
                        blnValidReturn = blnDeleteFilesFromFolder(strFolder, vlstExtensionsToDelete);

                        if (!blnValidReturn) break;
                    }
                }
                else
                {
                    blnValidReturn = true;
                }
            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return blnValidReturn;
        }
            

        public string str_GetCaption(int intCaptionID, short intLanguage)
        {
            string strCaption = string.Empty;

            try
            {
                strCaption = clsTTSQL.str_ADOSingleLookUp("TTAC_Text", "TTAppCaption", "TTAC_No = " + intCaptionID.ToString() + " AND ApL_NRI = " + intLanguage);

            }
            catch (Exception ex)
            {
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return strCaption;
        }

        public string str_FixStringForSQL(string vstrStringToFix)
        {
            return "'" + mcStringCleaner.Replace(vstrStringToFix, "''") + "'";
        }

        public string str_FixDateForSQL(string vstrDateToFix)
        {
            return str_FixStringForSQL(String.Format(str_GetServerDateTimeFormat, Convert.ToDateTime(vstrDateToFix)));
        }

        public System.DateTime str_SetDateToMidnightServerFormat(string vdtDateToSet)
        {
            return Convert.ToDateTime(String.Format(str_GetServerDateFormat, Convert.ToDateTime(String.Format(str_GetServerDateTimeFormat, Convert.ToDateTime(vdtDateToSet)) + " 00:00:00")));
        }

        public string str_GetServerFormatedDate(string vstrDateToFormat)
        {
            return String.Format("{0:" + str_GetServerDateFormat + "}", DateTime.Parse(vstrDateToFormat));
        }

        public DialogResult ShowMessage(int vintCaption_NRI, MessageBoxButtons vmsgType = MessageBoxButtons.OK, params string[] vlstMsgParam)
        {
            string strMessage = string.Empty;
            int intParamCpt = 1;
            DialogResult msgResult = DialogResult.OK;

            try
            {
                strMessage = _myUniqueInstance.str_GetCaption(vintCaption_NRI, mcTTUser.UserLanguage);

                if ((vlstMsgParam != null))
                {

                    foreach (string strParam in vlstMsgParam)
                    {

                        if (strMessage.Contains("@" + intParamCpt.ToString()))
                        {
                            strMessage = strMessage.Replace("@" + intParamCpt.ToString(), strParam);

                            intParamCpt += 1;
                        }
                        else
                        {
                            break;
                        }
                    }
                }


                msgResult = MessageBox.Show(clsTTApp.GetAppController.GetMDI, strMessage, "Message", vmsgType, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                //msgResult = MessageBox.Show(strMessage, "Message", vmsgType, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly, );

            }
            catch (Exception ex)
            {
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return msgResult;
        }

        public String str_ShowInputMessage(int vintCaption_NRI, string vstrTitle = "Attention", string vstrDefaultInput = "", params string[] vlstMsgParam)
        {
            string strMessage = string.Empty;
            int intParamCpt = 1;
            string msgResult = string.Empty;

            try
            {
                strMessage = _myUniqueInstance.str_GetCaption(vintCaption_NRI, mcTTUser.UserLanguage);

                if ((vlstMsgParam != null))
                {

                    foreach (string strParam in vlstMsgParam)
                    {

                        if (strMessage.Contains("@" + intParamCpt.ToString()))
                        {
                            strMessage = strMessage.Replace("@" + intParamCpt.ToString(), strParam);

                            intParamCpt += 1;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                msgResult = Microsoft.VisualBasic.Interaction.InputBox(strMessage, vstrTitle, vstrDefaultInput);

            }
            catch (Exception ex)
            {
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return msgResult;
        }

        public T ConvertToEnum<T>(object vobjValue)
        {
            T enumVal = (T)Enum.Parse(typeof(T), vobjValue.ToString());

            return enumVal;
        }

        /// <summary>
        /// Cettte fonction copie tous les fichiers contenu dans une source vers la destination spécifiée.
        /// </summary>
        /// <param name="vstrSourceFolderPath">Le chemin sur le disque vers le dossier contenant les fichiers à copier.</param>
        /// <param name="vstrDestinationFolderPath">Le chemin sur le disque vers le dossier où les fichiers doivent être copiés.</param>
        /// <param name="vblnOverwrite">Si true, les fichiers existant déjà seront remplacés.</param>
        /// <param name="vblnCreateFolderIfNotExist">Si le dossier de destination spécifié n'existe pas, il sera créé.</param>
        /// <param name="vlstExtensionsToAvoid">La liste des extensions de fichier qui seront copiés. Si vide, tout est copié.</param>
        /// <returns></returns>
        public bool blnCopyFolderContent(string vstrSourceFolderPath, 
                                         string vstrDestinationFolderPath, 
                                         bool vblnOverwrite = true, 
                                         bool vblnCreateFolderIfNotExist = false,
                                         SearchOption vSearchOption = SearchOption.TopDirectoryOnly,
                                         bool vblnIncludeSubFolders = false,
                                         string[] vlstExtensionsToAvoid = null,
                                         string[] vlstFoldersToAvoid = null)
        {
            bool blnValidReturn = true;
            string strFileDestinationPath = string.Empty;
            string[] lstFilesToCopy;
            string[] lstFoldersToCopy;

            try
            {
                if (!Directory.Exists(vstrDestinationFolderPath)) Directory.CreateDirectory(vstrDestinationFolderPath);

                //Copie des fichiers
                if (vlstExtensionsToAvoid != null && vlstExtensionsToAvoid.Length > 0)
                {
                    var lstFilesWithFilters = Directory.GetFiles(vstrSourceFolderPath, "*.*", vSearchOption).Where(f => !vlstExtensionsToAvoid.Contains(System.IO.Path.GetExtension(f).ToLower())).ToArray();

                    lstFilesToCopy = lstFilesWithFilters;
                }
                else
                {
                    string[] lstFilesNoFilter = Directory.GetFiles(vstrSourceFolderPath, "*.*", vSearchOption);

                    lstFilesToCopy = lstFilesNoFilter;
                }

                foreach (string strCurrentFile in lstFilesToCopy)
                {
                    if (vblnCreateFolderIfNotExist && !Directory.Exists(vstrDestinationFolderPath)) Directory.CreateDirectory(vstrDestinationFolderPath);

                    strFileDestinationPath = Path.Combine(vstrDestinationFolderPath, Path.GetFileName(strCurrentFile));

                    File.Copy(strCurrentFile, strFileDestinationPath, vblnOverwrite);
                }

                //Copie des sous-dossiers, recursivement
                if (vblnIncludeSubFolders && vSearchOption != SearchOption.AllDirectories)
                {
                    lstFoldersToCopy = Directory.GetDirectories(vstrSourceFolderPath);

                    foreach (string strFolder in lstFoldersToCopy)
                    {
                        if (vlstFoldersToAvoid == null || !vlstFoldersToAvoid.Contains(new DirectoryInfo(strFolder).Name))
                        {
                            if (!Directory.Exists(Path.Combine(vstrDestinationFolderPath, new DirectoryInfo(strFolder).Name))) Directory.CreateDirectory(Path.Combine(vstrDestinationFolderPath, new DirectoryInfo(strFolder).Name));

                            blnValidReturn = blnCopyFolderContent(strFolder, Path.Combine(vstrDestinationFolderPath, new DirectoryInfo(strFolder).Name), vblnOverwrite, vblnCreateFolderIfNotExist, vSearchOption, true, vlstExtensionsToAvoid);
                        }
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

        public void setAttributesToNormal(DirectoryInfo vRootDirectory) 
        {
            foreach (DirectoryInfo currentSubDir in vRootDirectory.GetDirectories())
            {
                setAttributesToNormal(currentSubDir);
            }

            foreach (FileInfo filePath in vRootDirectory.GetFiles()) 
            {
                filePath.Attributes = FileAttributes.Normal;
            }
        }

        public bool blnAddDirectoryStructureToZipFile(System.IO.Compression.ZipArchive rZipArchive, string vstrSourceFolderPath, string vstrRootFolderName)
        {
            bool blnValidReturn = true;
            string[] lstDirectories;

            try
            {
                if (File.GetAttributes(vstrSourceFolderPath) == FileAttributes.Directory)
                {
                    lstDirectories = Directory.GetDirectories(vstrSourceFolderPath); //Va chercher les répertoires

                    if (lstDirectories.Length == 0)
                    {
                        foreach (string strCurrentFileToCopyPath in Directory.GetFiles(vstrSourceFolderPath, "*.*", SearchOption.TopDirectoryOnly))
                        {
                            DirectoryInfo cEndDirectoryInfo = new DirectoryInfo(str_GetPathRelativePathTo(strCurrentFileToCopyPath, vstrSourceFolderPath));

                            ZipFileExtensions.CreateEntryFromFile(rZipArchive, strCurrentFileToCopyPath, Path.Combine(vstrRootFolderName, cEndDirectoryInfo.Name, Path.GetFileName(strCurrentFileToCopyPath)));
                        }

                        blnValidReturn = true;
                    }
                    else
                    {
                        for (int intIndex = 0; intIndex < lstDirectories.Length; intIndex++)
                        {
                            DirectoryInfo cEndDirectoryInfo = new DirectoryInfo(lstDirectories[intIndex]);
                            
                            blnValidReturn = blnAddDirectoryStructureToZipFile(rZipArchive, lstDirectories[intIndex], Path.Combine(vstrRootFolderName, cEndDirectoryInfo.Parent.Name));
                        }

                        blnValidReturn = true;
                    }
                }
                else
                {
                    blnValidReturn = true;
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
        /// Retourne la portion de path qui diffère entre le Path1 et le Path2.
        /// </summary>
        /// <param name="vstrPath1">Le path sur lequel le Path2 sera comparé.</param>
        /// <param name="vstrPath2">Le path pour lequel on veut la différence.</param>
        /// <returns>Un path qui représente la différence entre les 2 paths.</returns>
        public static string str_GetPathRelativePathTo(string vstrPath1, string vstrPath2)
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder(1024);
            bool result = PathRelativePathTo(builder, vstrPath1, 0, vstrPath2, 0);
            return builder.ToString();
        }

        #endregion

    }


    public sealed class NaturalStringComparer : IComparer<String>
    {
        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
        private static extern int StrCmpLogicalW(string psz1, string psz2);

        int IComparer<String>.Compare(String a, String b)
        {
            return StrCmpLogicalW(a, b);
        }
    }
}