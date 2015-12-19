using System;
using System.Data;
using System.Collections.Generic;
using Ceritar.TT3LightDLL;
using Ceritar.TT3LightDLL.Static_Classes;
using C1.Win.C1FlexGrid;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Data.SqlClient;
using System.Collections.Specialized;


namespace Ceritar.TT3LightDLL.Classes
{
    /// <summary>
    /// Cette classe est un wrapper sur le contrôle de grille "C1FlexGrid". Elle permet d'offrir des fonctionnalités génériques simplifiant l'utilisation de ce contrôle.
    /// Elle ajoute également des événements sur la grille qui sont utiles pour les forms.
    /// </summary>
    public class clsC1FlexGridWrapper
    {
        //Docuumentation available here: //http://helpcentral.componentone.com/nethelp/c1flexgrid/topic132.html
        //Row 0 is the Header and col 0 is the first small fixed col
        
	    //private members
	    private const short mintDefaultActionCol = 1;
	    private bool mblnHasNoActionColumn;

	    //Private class members
        private C1FlexGrid mGrdFlex;

        private Ceritar.TT3LightDLL.Controls.IFormController mfrmParent;
        private Button mbtnAddRow;
        private Button mbtnDeleteRow;

        private CellStyle csNewRow;
        private CellStyle csUpdateRow;
        private CellStyle csRemoveRow;

	    //Public events
	    public event SetDisplayEventHandler SetGridDisplay;
	    public delegate void SetDisplayEventHandler();
	    public event ValidateGridDataEventHandler ValidateGridData;
	    public delegate void ValidateGridDataEventHandler(ValidateGridDataEventArgs eventArgs);
	    public event SaveGridDataEventHandler SaveGridData;
	    public delegate void SaveGridDataEventHandler(SaveGridDataEventArgs eventArgs);
        public event AfterRowAddEventHandler AfterRowAdd;
        public delegate void AfterRowAddEventHandler();

        //Working variables
        private bool mblnGridIsLoading = false;
        private bool mblnFromButtonAddClick;
        private int mintNbVisibleColumns = 0;


        public clsC1FlexGridWrapper()
        {
            SetGridDisplay += clsFlexGridWrapper_SetDisplay;
        }


#region "Properties"

        public bool GridIsLoading
        {
            get { return mblnGridIsLoading; }
            set { mblnGridIsLoading = value; }
        }

        public string this[int vintRow, int vintCol]
        {
            get
            {
                string objItem = null;

                if ((vintRow <= mGrdFlex.Rows.Count - 1 & vintCol <= mGrdFlex.Cols.Count - 1))
                {
                    if (mGrdFlex[vintRow, vintCol] != null)
                    {
                        objItem = mGrdFlex[vintRow, vintCol].ToString();
                    }
                }
               
                if (vintRow > 0 & vintCol <= mGrdFlex.Cols.Count & !((mGrdFlex.Cols[vintCol]) == null) & mGrdFlex.Cols[vintCol].DataType != null && Strings.UCase(mGrdFlex.Cols[vintCol].DataType.Name).Equals("BOOLEAN")) {

                    objItem = (objItem == "True" | objItem == "1" ? "1" : "0");
                }
                
                if (objItem == null)
                {
                    objItem = string.Empty;
                }
                else
                {
                    //Do nothing
                }

                return objItem;
            }
            set
            {
                if ((vintRow <= mGrdFlex.Rows.Count - 1 & vintCol <= mGrdFlex.Cols.Count - 1))
                {
                    if (!((mGrdFlex.Cols[vintCol].DataType == null)))
                    {
                        if ((Strings.UCase(mGrdFlex.Cols[vintCol].DataType.Name) == "DATE") | (Strings.UCase(mGrdFlex.Cols[vintCol].DataType.Name) == "DATETIME"))
                        {
                            mGrdFlex[vintRow, vintCol] = (value != string.Empty ? value : null);
                        }
                        else
                        {
                            mGrdFlex[vintRow, vintCol] = value;
                        }
                    }
                    else
                    {
                        mGrdFlex[vintRow, vintCol] = value;
                    }
                }
            }
        }

	    public bool ChangeMade {
		    set {
               
			    if (mGrdFlex[mGrdFlex.Row, mintDefaultActionCol].ToString().Equals(sclsConstants.DML_Mode.INSERT_MODE.ToString())) {

                    CellRange crUpdate;

				    if (value == true) {

                        mfrmParent.GetFormController().ChangeMade = true;

                        crUpdate = mGrdFlex.GetCellRange(mGrdFlex.Row, 0, mGrdFlex.Row, mGrdFlex.Cols.Count - 1);
                        crUpdate.Style = csUpdateRow;
                        mGrdFlex[mGrdFlex.Row, mintDefaultActionCol] = sclsConstants.DML_Mode.UPDATE_MODE;
				    } else {
                        mfrmParent.GetFormController().ChangeMade = false;

                        crUpdate = mGrdFlex.GetCellRange(mGrdFlex.Row, 0, mGrdFlex.Row, mGrdFlex.Cols.Count - 1);
                        crUpdate.Style = mGrdFlex.Styles["Default"];
                        mGrdFlex[mGrdFlex.Row, mintDefaultActionCol] = sclsConstants.DML_Mode.NO_MODE;
				    }
			    }
		    }
	    }

	    public bool HasActionColumn {
		    set { mblnHasNoActionColumn = value; }
	    }

        public int GetNbVisibleColumns
        {
            get { return mintNbVisibleColumns; }
        }

#endregion


#region "Functions / Subs"

	    public bool bln_Init(ref C1FlexGrid rgrdGrid, ref Button rbtnAddRow, ref Button rbtnRemoveRow, bool vblnIsTree = false)
	    {
		    bool blnValidReturn = true;
            
		    try {
                mblnGridIsLoading = true;

			    mfrmParent = (Ceritar.TT3LightDLL.Controls.IFormController) rgrdGrid.FindForm();

			    SetGrdFlexEvents = rgrdGrid;

                SetBtnAddRowEvents = rbtnAddRow;
			    SetBtnDeleteRowEvents = rbtnRemoveRow;

			    mGrdFlex.BeginInit();

                mGrdFlex.DataSource = null;

                mGrdFlex.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

			    mGrdFlex.Rows.Count = 1;
			    mGrdFlex.Cols.Count = 1;
                mGrdFlex.Rows.Fixed = 1;
                mGrdFlex.Cols.Fixed = 1;

                if (vblnIsTree)
                {
                    mGrdFlex.Tree.Style = TreeStyleFlags.Simple;
                    mGrdFlex.AllowMerging = AllowMergingEnum.Nodes;
                    mGrdFlex.AllowResizing = AllowResizingEnum.Columns;
                    mGrdFlex.SelectionMode = SelectionModeEnum.Cell;
                }
                else
                {
                    mGrdFlex.SelectionMode = SelectionModeEnum.Row;
                }
               
                mGrdFlex.AllowEditing = false;
                mGrdFlex.AllowResizing = AllowResizingEnum.Columns;

                mGrdFlex.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Fixed3D;

                mGrdFlex.Rows.DefaultSize = 20; 
			    mGrdFlex.Cols.DefaultSize = 70;
			    mGrdFlex.Cols[0].Width = 5;

                mGrdFlex.EditOptions = EditFlags.ExitOnLeftRightKeys;
                mGrdFlex.FocusRect = FocusRectEnum.Inset;

			    if (SetGridDisplay != null) {
				    SetGridDisplay();
			    }

                csNewRow = mGrdFlex.Styles["NewRow"];
                csNewRow.BackColor = System.Drawing.Color.LightGreen;

                if (!mGrdFlex.Styles.Contains("UpdateRow"))
                {
                    csUpdateRow = mGrdFlex.Styles.Add("UpdateRow");
                    csUpdateRow.BackColor = System.Drawing.Color.Yellow;
                }
                else
                {
                    csUpdateRow = mGrdFlex.Styles["UpdateRow"];
                }
                
                if (!mGrdFlex.Styles.Contains("RemoveRow"))
                {
                    csRemoveRow = mGrdFlex.Styles.Add("RemoveRow");
                    csRemoveRow.BackColor = System.Drawing.Color.Red;
                }
                else
                {
                    csRemoveRow = mGrdFlex.Styles["RemoveRow"];
                }             

		    } catch (Exception ex) {
			    blnValidReturn = false;
			    sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
		    } finally {
			    mGrdFlex.EndInit();
                mblnGridIsLoading = false;
		    }

		    return blnValidReturn;
	    }

        public bool bln_FillData(string vstrSQL) 
	    {
		    bool blnValidReturn = true;
		    SqlCommand sqlCmd = default(SqlCommand);
		    SqlDataReader mySQLReader = null;
            System.Data.DataTable myDataTable = new System.Data.DataTable();
		    string strGridCaption = string.Empty;
            string[] dataTableArray = null;

		    try {
                mblnGridIsLoading = true;

                mGrdFlex.BeginUpdate();

                sqlCmd = new SqlCommand(vstrSQL, clsApp.GetAppController.SQLConnection);

			    mySQLReader = sqlCmd.ExecuteReader();

			    myDataTable.Load(mySQLReader);

                mGrdFlex.Rows.Count = 1;

                if (mGrdFlex.Cols.Count != myDataTable.Columns.Count + 1)
                {
                    blnValidReturn = false;
                    System.Windows.Forms.MessageBox.Show("Caption columns count and SQL columns count differs.");
                }
                else
                {
                    dataTableArray = new string[myDataTable.Columns.Count + 1];

                    //Add rows to grid with data
                    for (int intTableRowIndex = 0; intTableRowIndex <= myDataTable.Rows.Count - 1; intTableRowIndex++)
                    {
                        for (int intTableColIndex = 0; intTableColIndex <= myDataTable.Columns.Count - 1; intTableColIndex++)
                        {
                            dataTableArray[intTableColIndex + 1] = myDataTable.Rows[intTableRowIndex][intTableColIndex].ToString();
                        }

                        mGrdFlex.AddItem(dataTableArray);
                    }
                }

                //Binded
                //mGrdFlex.DataSource = myDataTable;

			    mySQLReader.Dispose();

			    blnValidReturn = pblnSetHeaderAndColsDisplay();

			    if (SetGridDisplay != null) {
				    SetGridDisplay();
			    }

			    if (mGrdFlex.Rows.Count > 1) {
				    mGrdFlex.Row = 1;
			    }

		    } catch (Exception ex) {
			    blnValidReturn = false;
			    sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
		    } finally {
			    if ((mySQLReader != null)) {
				    mySQLReader.Dispose();
			    }

			    mGrdFlex.EndUpdate();
                mblnGridIsLoading = false;
		    }

		    return blnValidReturn;
	    }

	    public bool CellIsEmpty(int vintRow, int vintCol)
	    {
		    bool blnIsEmpty = true;

		    try {
                if (!Information.IsDBNull(mGrdFlex[vintRow, vintCol]) & mGrdFlex[vintRow, vintCol] != null & !string.IsNullOrEmpty(Strings.Trim(mGrdFlex[vintRow, vintCol].ToString())))
                {
                    blnIsEmpty = false;
                }

		    } catch (Exception ex) {
			    sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
		    }

		    return blnIsEmpty;
	    }

	    public bool CurrentCellIsEmpty()
	    {
		    bool blnIsEmpty = true;

		    try {
                if (!Information.IsDBNull(mGrdFlex[mGrdFlex.Row, mGrdFlex.Col]) & mGrdFlex[mGrdFlex.Row, mGrdFlex.Col] != null & !string.IsNullOrEmpty(Strings.Trim(mGrdFlex[mGrdFlex.Row, mGrdFlex.Col].ToString())))
                {
                    blnIsEmpty = false;
                }

		    } catch (Exception ex) {
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
		    }

		    return blnIsEmpty;
	    }

	    public bool CellIsChecked(int vintRowIdx, int vintColIdx)
	    {
		    bool blnCellIsChecked = false;

		    blnCellIsChecked = this[vintRowIdx, vintColIdx].ToUpper().Equals("TRUE");

		    return blnCellIsChecked;
	    }

	    private bool pblnSetHeaderAndColsDisplay()
	    {
		    bool blnValidReturn = true;
		    string strGridCaption = string.Empty;
		    string[] lstColumns = null;
            CellRange crColHeader;

            mintNbVisibleColumns = 0;

		    try {
                strGridCaption = clsApp.GetAppController.str_GetCaption(Convert.ToInt32(mGrdFlex.Tag), clsApp.GetAppController.cUser.GetUserLanguage);

                lstColumns = Strings.Split(strGridCaption.Insert(0, "|"), "|"); //The first fixed col must not be inclued in the caption!
                
			    mGrdFlex.Cols.Count = lstColumns.Length;

			    //Definition of headers columns         
                for (int colHeaderCpt = 1; colHeaderCpt <= lstColumns.Length - 1; colHeaderCpt++)
                {
				    if (lstColumns[colHeaderCpt] == string.Empty) {
                        mGrdFlex.Cols[colHeaderCpt].Visible = false;
                        
				    } else {
                        mGrdFlex[0, colHeaderCpt] = (string)lstColumns[colHeaderCpt].Substring(1, lstColumns[colHeaderCpt].Length - 1);

                        crColHeader = mGrdFlex.GetCellRange(0, colHeaderCpt, mGrdFlex.Rows.Count - 1, colHeaderCpt);

                        crColHeader.Style = mGrdFlex.Styles.Add(null);

					    switch (lstColumns[colHeaderCpt].ToCharArray()[0]) {
						    case '<':
                                //mGrdFlex.Cols[colHeaderCpt].StyleFixed.TextAlign = TextAlignEnum.LeftCenter;
                                crColHeader.Style.TextAlign = TextAlignEnum.LeftCenter;
                                
							    break;

						    case '^':
                                //mGrdFlex.Cols[colHeaderCpt].StyleFixed.TextAlign = TextAlignEnum.CenterCenter;
                                crColHeader.Style.TextAlign = TextAlignEnum.CenterCenter;
                                
							    break;

						    case '>':
                                //mGrdFlex.Cols[colHeaderCpt].StyleFixed.TextAlign = TextAlignEnum.RightCenter;
                                crColHeader.Style.TextAlign = TextAlignEnum.RightCenter;
                                
							    break;
					    }

                        mintNbVisibleColumns++;
				    }
			    }

			    blnValidReturn = true;

		    } catch (Exception ex) {
			    blnValidReturn = false;
			    sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
		    }

		    return blnValidReturn;
	    }

        public bool bln_SetRowActionToInsert(int vintRowIndex)
        {
            bool blnValidReturn = false;

            if (mGrdFlex.Rows.Count > 1 && mGrdFlex.Row > 0 && mfrmParent.GetFormController().FormMode != sclsConstants.DML_Mode.DELETE_MODE && mfrmParent.GetFormController().FormMode != sclsConstants.DML_Mode.CONSULT_MODE)
            {
                CellRange crCell = mGrdFlex.GetCellRange(vintRowIndex, 0);
                crCell.Style = csNewRow;

                mGrdFlex[vintRowIndex, mintDefaultActionCol] = sclsConstants.DML_Mode.INSERT_MODE;

                mfrmParent.GetFormController().ChangeMade = true;

                blnValidReturn = true;
            }

            return blnValidReturn;
        }

        public bool bln_SetRowActionToDelete(int vintRowIndex)
        {
            bool blnValidReturn = false;

            if (mGrdFlex.Rows.Count > 1 && mGrdFlex.Row > 0 && mfrmParent.GetFormController().FormMode != sclsConstants.DML_Mode.DELETE_MODE && mfrmParent.GetFormController().FormMode != sclsConstants.DML_Mode.CONSULT_MODE)
            {
                CellRange crCell = mGrdFlex.GetCellRange(vintRowIndex, 0);
                crCell.Style= csRemoveRow;

                mGrdFlex[vintRowIndex, mintDefaultActionCol] = sclsConstants.DML_Mode.DELETE_MODE;

                mfrmParent.GetFormController().ChangeMade = true;

                blnValidReturn = true;
            }
            
            return blnValidReturn;
        }

	    public void SetColType_CheckBox(int vintColumnIndex)
	    {
		    mGrdFlex.Cols[vintColumnIndex].Style.DataType = typeof(bool);
            mGrdFlex.Cols[vintColumnIndex].Style.TextAlign = TextAlignEnum.CenterCenter;
	    }

	    public void SetColType_ComboBox(string vstrSQL, int vintColumnIndex, string vstrValueMember, string vstrDisplayMember, bool vblnAllowEmpty)
	    {
		    SqlCommand mySQLCmd = default(SqlCommand);
		    SqlDataReader mySQLReader = null;
		    ListDictionary  myBindingList = new ListDictionary();
            CellStyle individualColStyle = mGrdFlex.Styles.Add("ComboBox" + vintColumnIndex);

		    try {
                //mGrdFlex.Cols[vintColumnIndex].Style.DataType = "ComboBox";

			    mySQLCmd = new SqlCommand(vstrSQL, clsApp.GetAppController.SQLConnection);

			    mySQLReader = mySQLCmd.ExecuteReader();

			    if (vblnAllowEmpty) {
                    myBindingList.Add("", "");
			    }

			    while (mySQLReader.Read()) {

				    if (!Information.IsDBNull(mySQLReader[vstrValueMember])) {
					    myBindingList.Add(Convert.ToInt32(mySQLReader[vstrValueMember]), Convert.ToString(mySQLReader[vstrDisplayMember]));
				    }
			    }

			    mGrdFlex.Cols[vintColumnIndex].DataMap = myBindingList;

		    } catch (Exception ex) {
			    sclsErrorsLog.WriteToErrorLog(ex, ex.Source + " - " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name);
		    } finally {
			    if ((mySQLReader != null)) {
				    mySQLReader.Dispose();
			    }
		    }
	    }

	    public void SetColType_DateTimePicker(int vintColumnIndex, bool vblnNullable)
	    {
		    try {
                CellStyle individualColStyle = mGrdFlex.Styles.Add("DateTime" + vintColumnIndex);

                individualColStyle.DataType = typeof(DateTime);
                individualColStyle.Format = clsApp.GetAppController.str_GetServerDateFormat;

                mGrdFlex.Cols[vintColumnIndex].Style = individualColStyle;

                mGrdFlex.Cols[vintColumnIndex].Width = 85;

		    } catch (Exception ex) {
			    sclsErrorsLog.WriteToErrorLog(ex, ex.Source + " - " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name);
		    }
	    }

        public void AddRow(int vintPosition = -1)
        {
            CellRange crRow;

            mblnFromButtonAddClick = true;

            mGrdFlex.Rows.InsertRange((vintPosition == -1 ? mGrdFlex.Rows.Count : vintPosition), 1);

            if (!mblnHasNoActionColumn)
            {
                mGrdFlex[mGrdFlex.Rows.Count - 1, mintDefaultActionCol] = sclsConstants.DML_Mode.INSERT_MODE;
            }

            mGrdFlex.Row = (vintPosition == -1 ? mGrdFlex.Rows.Count - 1 : vintPosition);

            crRow = mGrdFlex.GetCellRange((vintPosition == -1 ? mGrdFlex.Rows.Count - 1 : vintPosition), 0, (vintPosition == -1 ? mGrdFlex.Rows.Count - 1 : vintPosition), mGrdFlex.Cols.Count - 1);
            crRow.Style = csNewRow;

            
        }

        public void AddTreeItem(int vintColIndex, string vstrNodeName, int vintLevel, bool vblnIsNode,  int vintRowIndex = 0)
        {
            int intNewRowidx;

            if (vintRowIndex == 0)
            {
                intNewRowidx = mGrdFlex.Rows.Count;
            }
            else
            {
                intNewRowidx = vintRowIndex;
            }

            mGrdFlex.AddItem(new object[1] { vstrNodeName }, intNewRowidx, vintColIndex);  
            mGrdFlex.Rows[intNewRowidx].IsNode = vblnIsNode;
            mGrdFlex.Rows[intNewRowidx].Node.Level = vintLevel;
            mGrdFlex.SetCellStyle(intNewRowidx, vintColIndex, mGrdFlex.Styles["Data"]);
        }

        public bool blnValidateGridData()
        {
            ValidateGridDataEventArgs cGridValidResults = new ValidateGridDataEventArgs();

            this.ValidateGridData(cGridValidResults);

            return cGridValidResults.IsValid;
        }

        private C1FlexGrid SetGrdFlexEvents
        {
            set
            {
                mGrdFlex = value;

                if (mGrdFlex == null)
                {
                    mGrdFlex.CellChanged -= mGrdFlex_CellsChanged;
                    mGrdFlex.CellChecked -= mGrdFlex_CheckBoxClick;
                    mGrdFlex.AfterRowColChange -= mGrdFlex_AfterRowColChange;
                }

                if (mGrdFlex != null)
                {
                    mGrdFlex.CellChanged += mGrdFlex_CellsChanged;
                    mGrdFlex.CellChecked += mGrdFlex_CheckBoxClick;
                    mGrdFlex.AfterRowColChange += mGrdFlex_AfterRowColChange;
                }
            }
        }

        private Button SetBtnAddRowEvents
        {
            set
            {
                if (mbtnAddRow != null)
                {
                    mbtnAddRow.Click -= btnAddRow_Click;
                }
                mbtnAddRow = value;
                if (mbtnAddRow != null)
                {
                    mbtnAddRow.Click += btnAddRow_Click;
                }
            }
        }

        private Button SetBtnDeleteRowEvents
        {
            set
            {
                if (mbtnDeleteRow != null)
                {
                    mbtnDeleteRow.Click -= btnDeleteRow_Click;
                }
                mbtnDeleteRow = value;
                if (mbtnDeleteRow != null)
                {
                    mbtnDeleteRow.Click += btnDeleteRow_Click;
                }
            }
        }
     
#endregion


	    private void btnAddRow_Click(object sender, EventArgs e)
	    {
            AddRow();
            //System.Data.DataRow cRow = ((DataTable) mGrdFlex.DataSource).NewRow();
            
            //((DataTable)mGrdFlex.DataSource).Rows.InsertAt(cRow, mGrdFlex.Rows.Count - 1);
            //((DataTable)mGrdFlex.DataSource).Rows.Add(0);
	    }

	    private void btnDeleteRow_Click(object sender, EventArgs e)
	    {
            CellRange crRow;
		    int intSelectedRow = mGrdFlex.Row;
           
		    if (intSelectedRow > 0) {
                
                if (this[intSelectedRow, mintDefaultActionCol] == sclsConstants.DML_Mode.INSERT_MODE.ToString())
                {
                    mGrdFlex.RemoveItem(intSelectedRow);
				    mGrdFlex.Row = (intSelectedRow >= 2 ? intSelectedRow - 1 : -1);
			    } else {
				    mGrdFlex[intSelectedRow, mintDefaultActionCol] = sclsConstants.DML_Mode.DELETE_MODE;

                    //crRow = mGrdFlex.GetCellRange(intSelectedRow, 0, intSelectedRow, mGrdFlex.Cols.Count - 1); //TODO: en attendant de voir comment eviter d'affecter le style et le remplacer
                    crRow = mGrdFlex.GetCellRange(intSelectedRow, 0);
                    crRow.Style = csRemoveRow;

                    mfrmParent.GetFormController().ChangeMade = true;
			    }
		    }
	    }

        public bool bln_ValidateGridEvent()
        {
            ValidateGridDataEventArgs validateGridEventArgs = new ValidateGridDataEventArgs();

            if (ValidateGridData != null)
            {
                ValidateGridData(validateGridEventArgs);
            }

            return validateGridEventArgs.IsValid;
        }

        public bool bln_SaveGridDataEvent()
        {
            SaveGridDataEventArgs saveGridDataArgs = new SaveGridDataEventArgs();

            if (SaveGridData != null)
            {
                SaveGridData(saveGridDataArgs);
            }

            return saveGridDataArgs.SaveSuccessful;
        }

	    private void clsFlexGridWrapper_SetDisplay()
	    {
            pblnSetHeaderAndColsDisplay();
	    }

        private void mGrdFlex_CheckBoxClick(object sender, RowColEventArgs e)
        {
            mfrmParent.GetFormController().ChangeMade = true;
        }

        private void mGrdFlex_CellsChanged(object sender, RowColEventArgs e)
        {
            if (!mblnGridIsLoading && 
                !mblnHasNoActionColumn && 
                mGrdFlex.Rows.Count > 1 &&
                mGrdFlex.Row > 0 &&
                mGrdFlex[mGrdFlex.Row, mintDefaultActionCol] != null && 
                (mGrdFlex[mGrdFlex.Row, mintDefaultActionCol].ToString().Equals(((int)sclsConstants.DML_Mode.NO_MODE).ToString()) || 
                 mGrdFlex[mGrdFlex.Row, mintDefaultActionCol].ToString().Equals((sclsConstants.DML_Mode.NO_MODE).ToString())
                ) && 
                mfrmParent.GetFormController().FormMode == sclsConstants.DML_Mode.UPDATE_MODE &&
                e.Row == mGrdFlex.Row
               )
            {
                //NE FONCTIONNE PAS BIEN, CAUSE LE STYLE TO CHANGE
                //CellRange crUpdate = mGrdFlex.GetCellRange(mGrdFlex.Row, 0, mGrdFlex.Row, mGrdFlex.Cols.Count - 1);
                //crUpdate.Style = mGrdFlex.Styles.Add(null);
                //crUpdate.Style = csUpdateRow;
                mGrdFlex[mGrdFlex.Row, mintDefaultActionCol] = sclsConstants.DML_Mode.UPDATE_MODE;
              
                CellRange crUpdate = mGrdFlex.GetCellRange(mGrdFlex.Row, 0);
                crUpdate.Style = csUpdateRow;

                mfrmParent.GetFormController().ChangeMade = true;
            }
        }

        void mGrdFlex_AfterRowColChange(object sender, RangeEventArgs e)
        {
            if (!mblnGridIsLoading && mblnFromButtonAddClick)
            {
                if (AfterRowAdd != null)
                {
                    AfterRowAdd();
                    mblnFromButtonAddClick = false;
                }     
            }
        }
}


#region "Custom events"

    public class ValidateGridDataEventArgs : System.EventArgs
    {
	    private bool mblnIsValid;

	    public bool IsValid {
		    get { return mblnIsValid; }
		    set { mblnIsValid = value; }
	    }
    }

    public class SaveGridDataEventArgs : System.EventArgs
    {
	    private bool mblnSaveSuccessful;

	    public bool SaveSuccessful {
		    get { return mblnSaveSuccessful; }
		    set { mblnSaveSuccessful = value; }
	    }
    }

#endregion


}