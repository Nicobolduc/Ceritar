using System;
using Ceritar.TT3LightDLL.Static_Classes;
using Ceritar.TT3LightDLL.Classes;
using System.Windows.Forms;
using System.Reflection;

namespace Ceritar.Logirack_CVS
{
    public partial class frmGenericList : Form
    {

        //Public members
        public string mintGridTag = string.Empty;
        public string mstrGridSQL = string.Empty;

        //Private members
        private const int mintItem_ID_col = 1;
        private int mintSelectedRow = 1;
        private string mstrFormToOpenName = string.Empty;
        private bool mblnChildFormIsModal = true;
        private sclsGenList.GeneralLists_ID mListToOpen;       

        //Private class members
        private clsFlexGridWrapper mcGrdList;

#region "Constructor"

        public frmGenericList(sclsGenList.GeneralLists_ID vstrGenList_ID)
        {
	        InitializeComponent();
            
	        mListToOpen = vstrGenList_ID;

        }

#endregion


#region "Properties"

        public string SetFormToOpenName
        {
            set { mstrFormToOpenName = value; }
        }

        public bool ChildFormIsModal
        {
            get { return mblnChildFormIsModal; }
            set { mblnChildFormIsModal = value; }
        }

#endregion


#region "Functions / Subs"

        private bool blnOpenForm(sclsConstants.DML_Mode vFormMode)
        {
            bool blnValidReturn = true;
            Form frmToOpen = null;
            string strFormName = string.Empty;
            int intItem_ID = 0;

            try
            {
                strFormName = typeof(mdiGeneral).Namespace + "." + mstrFormToOpenName;

                frmToOpen = (Form)System.Reflection.Assembly.GetEntryAssembly().CreateInstance(strFormName, true);

                if (grdList.Row > 0)
                {
                    mintSelectedRow = grdList.Row;
                    intItem_ID = Convert.ToInt32(grdList[mintSelectedRow, mintItem_ID_col]);
                }

                switch (vFormMode)
                {
                    case sclsConstants.DML_Mode.INSERT_MODE:
                        Type delegateType = typeof(frmVersionRevision).GetEvent("ev").EventHandlerType;
                        MethodInfo invoke = delegateType.GetMethod("Invoke");
                       
                        //p.ShowForm(vFormMode, 0, mblnChildFormIsModal);
                        
                        break;
                    case sclsConstants.DML_Mode.UPDATE_MODE:
                        //frmToOpen.formController.ShowForm(vFormMode, intItem_ID, mblnChildFormIsModal);

                        break;
                    case sclsConstants.DML_Mode.DELETE_MODE:
                        clsApp.GetAppController.DisableAllFormControls((Form)frmToOpen, null, null);
                        
                        //frmToOpen.formController.ShowForm(vFormMode, intItem_ID, mblnChildFormIsModal);

                        break;
                }

                //formController.LoadFormData();

                switch (vFormMode)
                {
                    case sclsConstants.DML_Mode.INSERT_MODE:
                    case sclsConstants.DML_Mode.UPDATE_MODE:

                        for (int intRowIndex = 1; intRowIndex <= grdList.Rows.Count; intRowIndex++)
                        {
                            if (Microsoft.VisualBasic.Conversion.Val(grdList[intRowIndex, mintItem_ID_col]) == intItem_ID)
                            {
                                mintSelectedRow = intRowIndex;

                                break;
                            }
                        }

                        break;
                }

                if (mintSelectedRow >= 0 & grdList.Rows.Count > 0)
                {
                    grdList.Row = mintSelectedRow;
                }
            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }

            return blnValidReturn;
        }

        private bool blnGrdList_Load()
        {
            bool blnValidReturn = false;

            try
            {
                SuspendLayout();

                blnValidReturn = mcGrdList.bln_FillData(mstrGridSQL);

                if (blnValidReturn & grdList.Rows.Count > 0)
                {
                    grdList.Row = mintSelectedRow;
                }
            }
            catch (Exception ex)
            {
                blnValidReturn = false;
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }
            finally
            {
                ResumeLayout();
            }

            return blnValidReturn;
        }

#endregion
    }
}
