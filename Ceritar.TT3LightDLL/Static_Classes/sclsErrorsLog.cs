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

        public static void WriteToErrorLog(Exception vcException, string strTitle) 
        {

            FileStream myFileStream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\Log.txt", FileMode.Append, FileAccess.Write);
            StreamWriter myStreamWriter = new StreamWriter(myFileStream);
            string strMessageToShow = string.Empty;

            if (!System.IO.Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\")) 
            {

                System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\");
            }

            myStreamWriter.Write("Title: " + strTitle + Environment.NewLine);
            myStreamWriter.Write("Message: " + vcException.Message + Environment.NewLine);
            myStreamWriter.Write("StackTrace: " + vcException.StackTrace + Environment.NewLine);
            myStreamWriter.Write("Date/Time: " + DateTime.Now.ToString() + Environment.NewLine);
            myStreamWriter.Write("======================== END TRACE ========================" + Environment.NewLine);
            
            myStreamWriter.Close();
            myFileStream.Close();

#if DEBUG
            strMessageToShow = strTitle + Environment.NewLine;
            strMessageToShow = strMessageToShow + vcException.Message + Environment.NewLine;
            strMessageToShow = strMessageToShow + vcException.StackTrace + Environment.NewLine;


            System.Windows.Forms.MessageBox.Show(strMessageToShow, "An error occurred", System.Windows.Forms.MessageBoxButtons.OK);
#endif

        }

    }
}
