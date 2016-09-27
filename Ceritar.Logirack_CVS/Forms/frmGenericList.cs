using System;
using System.Windows.Forms;
using System.Reflection;
using Ceritar.TT3LightDLL.Static_Classes;
using Ceritar.TT3LightDLL.Classes;
using Ceritar.Logirack_CVS.Static_Classes;
using Ceritar.TT3LightDLL.Controls;

namespace Ceritar.Logirack_CVS.Forms
{
    /// <summary>
    /// Cette classe permet d'afficher une liste représentant n'importe quel objet dans le système. Elle offre la possibilité de consulter, ajouter, modifier ou supprimer ces objets via
    /// leur vue respective.
    /// </summary>
    public partial class frmGenericList : Form, IFormController
    {
        //Public members
        public string mintGridTag = string.Empty;
        public string mstrGridSQL = string.Empty;

        //Private members
        private const int mintItem_ID_col = 1;
        private int mintSelectedRow = 1;
        private string mstrFormToOpenName = string.Empty;
        private bool mblnChildFormIsModal = false;
        private sclsGenList.GeneralLists_ID mListToOpen;

        //Private class members
        private clsTTC1FlexGridWrapper mcGrdList;


#region "Constructor"

        public frmGenericList(sclsGenList.GeneralLists_ID vstrGenList_ID)
        {
	        InitializeComponent();
            
	        mListToOpen = vstrGenList_ID;

            mcGrdList = new clsTTC1FlexGridWrapper();
            mcGrdList.SetGridDisplay += mcGrdList_SetGridDisplay;

            btnRefresh.Click += btnRefresh_Click;
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
            IFormController frmToOpen = null;
            string strFormName = string.Empty;
            int intItem_NRI = 0;

            try
            {
                strFormName = typeof(Forms.mdiGeneral).Namespace + "." + mstrFormToOpenName;

                frmToOpen = (IFormController) System.Reflection.Assembly.GetEntryAssembly().CreateInstance(strFormName, true);
                ((Form)frmToOpen).MdiParent = this.MdiParent;

                if (grdList.Row > 0)
                {
                    mintSelectedRow = grdList.Row;
                    intItem_NRI = Convert.ToInt32(grdList[mintSelectedRow, mintItem_ID_col]);
                }

                switch (vFormMode)
                {
                    case sclsConstants.DML_Mode.INSERT_MODE:
                        intItem_NRI = 0;
                        frmToOpen.GetFormController().ShowForm(this, vFormMode, ref intItem_NRI, mblnChildFormIsModal);
                        
                        break;

                    case sclsConstants.DML_Mode.UPDATE_MODE:
                        frmToOpen.GetFormController().ShowForm(this, vFormMode, ref intItem_NRI, mblnChildFormIsModal);
       
                        break;

                    case sclsConstants.DML_Mode.CONSULT_MODE:
                    case sclsConstants.DML_Mode.DELETE_MODE:

                        frmToOpen.GetFormController().ShowForm(this, vFormMode, ref intItem_NRI, mblnChildFormIsModal);

                        break;
                }

                formController.LoadLinkedFormData();

                switch (vFormMode)
                {
                    case sclsConstants.DML_Mode.INSERT_MODE:
                    case sclsConstants.DML_Mode.UPDATE_MODE:

                        if (grdList.Rows.Count > 1)
                        {
                            int intCurrentItem = grdList.FindRow(intItem_NRI.ToString(), 1, mintItem_ID_col, false, true, false);

                            grdList.Row = (intCurrentItem > 0 ? intCurrentItem : 1);
                        }  

                        break;
                }

                if (mintSelectedRow >= 0 & grdList.Rows.Count > 1)
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

        private bool pfblnGrdList_Load()
        {
            bool blnValidReturn = false;

            blnValidReturn = mcGrdList.bln_FillData(mstrGridSQL);

            if (!blnValidReturn || grdList.Rows.Count == 1)
            {
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
                btnConsult.Enabled = false;
            }
            else
            {
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
                btnConsult.Enabled = true;
            }

            btnAdd.Enabled = true;

            return blnValidReturn;
        }

#endregion


        private void formController_LoadData(TT3LightDLL.Controls.LoadDataEventArgs eventArgs)
        {
            bool blnValidReturn = false;
            Button placeHolder = null;

            grdList.Tag = mintGridTag;

            if (!mcGrdList.bln_Init(ref grdList, ref placeHolder, ref placeHolder))
            { }
            else if (!pfblnGrdList_Load())
            { }
            else
            {
                blnValidReturn = true;
            }

            if (!blnValidReturn)
            {
                this.Close();
            }
        }

        private void grdList_DoubleClick(object sender, EventArgs e)
        {
            if (grdList.Rows.Count > 1 & grdList.RowSel > 0) {

                blnOpenForm(sclsConstants.DML_Mode.UPDATE_MODE);
            }
        }

        private void btnConsult_Click(object sender, EventArgs e)
        {
            try
            {
                blnOpenForm(sclsConstants.DML_Mode.CONSULT_MODE);
            }
            catch (Exception ex)
            {
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                blnOpenForm(sclsConstants.DML_Mode.INSERT_MODE);
            }
            catch (Exception ex)
            {
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                blnOpenForm(sclsConstants.DML_Mode.UPDATE_MODE);
            }
            catch (Exception ex)
            {
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                blnOpenForm(sclsConstants.DML_Mode.DELETE_MODE);
            }
            catch (Exception ex)
            {
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }
        }

        private void btnRefresh_Click()
        {
            pfblnGrdList_Load();
        }

        void mcGrdList_SetGridDisplay()
        {
            C1.Win.C1FlexGrid.CellRange crMerged;

            foreach (C1.Win.C1FlexGrid.Column cCol in grdList.Cols)
            {
                if (cCol.IsVisible && cCol.Index > 1)
                {
                    if (grdList.Cols.Count > 5)
                    {
                        if (cCol.IsVisible && cCol.Index > 1) cCol.Width = TextRenderer.MeasureText(cCol.Caption, grdList.Font).Width;
                    }
                    else
                    {
                        if (cCol.IsVisible && cCol.Index > 1) cCol.Width = grdList.Width / mcGrdList.GetNbVisibleColumns - 10;
                    }
                }
            }

            switch (mListToOpen)
            {
                case sclsGenList.GeneralLists_ID.VERSION_REVISION_LIST_NRI:

                    grdList.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Free;
                    crMerged = grdList.GetCellRange(1, 2, grdList.Rows.Count - 1, 2);

                    grdList.Cols[2].AllowMerging = true;
                    
                    break;

                case sclsGenList.GeneralLists_ID.TEMPLATE_LIST_NRI:

                    grdList.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Free;
                    crMerged = grdList.GetCellRange(1, 2, grdList.Rows.Count - 1, 3);

                    grdList.Cols[2].AllowMerging = true;
                    grdList.Cols[3].AllowMerging = true;

                    break;

                case sclsGenList.GeneralLists_ID.CERITAR_CLIENT_LIST_NRI:

                    grdList.Cols[3].DataType = typeof(Boolean);
                    
                    break;
            }
        }

        ctlFormController IFormController.GetFormController()
        {
            return this.formController;
        }

        private void formController_BeNotify(BeNotifyEventArgs eventArgs)
        {
             if (eventArgs.NewItemInserted)
             {
                 pfblnGrdList_Load();
             }
             else
             {
                 btnRefresh.SetToRefresh = true;
             }   
        }
    }
}
