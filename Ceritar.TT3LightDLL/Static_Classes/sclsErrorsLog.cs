//using Microsoft.VisualBasic;
using System;
using System.Collections;
//using System.Collections.Generic;
using System.Data;
//using System.Diagnostics;
using System.IO;
//using System.Text;

namespace Ceritar.TT3LightDLL.Static_Classes
{

    public static class sclsErrorsLog 
    {

        public static void WriteToErrorLog(string strErrorMessage, string strStackTrace, string strTitle) 
        {

            FileStream myFileStream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\Log.txt", FileMode.Append, FileAccess.Write);
            StreamWriter myStreamWriter = new StreamWriter(myFileStream);
            string strMessageToShow = string.Empty;

            if (!System.IO.Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\")) 
            {

                System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\");
            }

            myStreamWriter.Write("Title: " + strTitle + Environment.NewLine);
            myStreamWriter.Write("Message: " + strErrorMessage + Environment.NewLine);
            myStreamWriter.Write("StackTrace: " + strStackTrace + Environment.NewLine);
            myStreamWriter.Write("Date/Time: " + DateTime.Now.ToString() + Environment.NewLine);
            myStreamWriter.Write("======================== END TRACE ========================" + Environment.NewLine);
            
            myStreamWriter.Close();
            myFileStream.Close();

#if DEBUG
            strMessageToShow = strTitle + Environment.NewLine;
            strMessageToShow = strMessageToShow + strErrorMessage + Environment.NewLine;
            strMessageToShow = strMessageToShow + strStackTrace + Environment.NewLine;


           //System.Windows.Forms.MessageBox.Show(strMessageToShow, "An error occurred", System.Windows.Forms.MessageBoxButtons.OK);
#endif

        }

    }
}
