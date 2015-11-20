using System;
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

    public class clsFlexGridWrapper
    {
        //Row 0 is the Header and col 0 is the first small fixed col
        
	    //private members
	    private const short mintDefaultActionCol = 1;
	    private int mintPreviousCellChangedRow;

	    private bool mblnHasNoActionColumn;

	    //Private class members
	    private C1FlexGrid mGrdFlex;

        private object mfrmGridParent;
        private Button mbtnAddRow;
        private Button mbtnDeleteRow;

	    //Public events
	    public event SetDisplayEventHandler SetDisplay;
	    public delegate void SetDisplayEventHandler();
	    public event ValidateGridDataEventHandler ValidateGridData;
	    public delegate void ValidateGridDataEventHandler(ValidateGridEventArgs eventArgs);
	    public event SaveGridDataEventHandler SaveGridData;
	    public delegate void SaveGridDataEventHandler(SaveGridDataEventArgs eventArgs);

	    //Public enums
	    public enum GridRowActions
	    {
		    NO_ACTION = sclsConstants.Form_Mode.CONSULT_MODE,
            INSERT_ACTION = sclsConstants.Form_Mode.INSERT_MODE,
            UPDATE_ACTION = sclsConstants.Form_Mode.UPDATE_MODE,
            DELETE_ACTION = sclsConstants.Form_Mode.DELETE_MODE
	    }


        public clsFlexGridWrapper()
        {
            SetDisplay += SyncfusionGridController_SetDisplay;
        }


#region "Properties"

        private C1FlexGrid mGrdSync
        {
            get { return mGrdFlex; }
            set
            {
                if (mGrdFlex != null)
                {
                    mGrdFlex.CellChanged -= mGrdSync_CellsChanged;
                    mGrdFlex.CellChecked -= mGrdSync_CheckBoxClick;
                    mGrdFlex.ComboCloseUp -= GrdSyncController_CurrentCellCloseDropDown;
                }
                mGrdFlex = value;
                if (mGrdFlex != null)
                {
                    mGrdFlex.CellChanged += mGrdSync_CellsChanged;
                    mGrdFlex.CellChecked += mGrdSync_CheckBoxClick;
                    mGrdFlex.ComboCloseUp += GrdSyncController_CurrentCellCloseDropDown;
                }
            }
        }

        private Button SetBtnAddRow
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

        private Button SetBtnDeleteRow
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

        public string this[int vintRow, int vintCol]
        {
            get
            {
                string objItem = null;

                if ((vintRow <= mGrdFlex.Rows.Count - 1 & vintCol <= mGrdFlex.Cols.Count - 1))
                {
                    objItem = mGrdFlex[vintRow, vintCol].ToString();
                }
               
                if (vintRow > 0 & vintCol <= mGrdFlex.Cols.Count & !((mGrdFlex.Cols[vintCol]) == null) & Strings.UCase(mGrdFlex.Cols[vintCol].DataType.Name).Equals("BOOLEAN")) {

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
               
			    if (mGrdSync[mGrdFlex.Row, mintDefaultActionCol].ToString().Equals(GridRowActions.INSERT_ACTION.ToString())) {

				    if (value == true) {

                        //mfrmGridParent.formController.ChangeMade = true; TODO

					    mGrdFlex.Rows[mGrdFlex.Row].Style.BackColor = System.Drawing.Color.Yellow;
                        mGrdFlex[mGrdFlex.Row, mintDefaultActionCol] = GridRowActions.UPDATE_ACTION;
				    } else {
                        //mfrmGridParent.formController.ChangeMade = false;

                        mGrdFlex.Rows[mGrdFlex.Row].Style.BackColor = System.Drawing.Color.Empty;
                        mGrdFlex[mGrdFlex.Row, mintDefaultActionCol] = GridRowActions.NO_ACTION;
				    }
			    }
		    }
	    }

	    public bool SetNoActionColumn {
		    set { mblnHasNoActionColumn = value; }
	    }

#endregion


#region "Functions / Subs"

	    public bool bln_Init(ref C1FlexGrid rgrdGrid, ref Button rbtnAddRow, ref Button rbtnRemoveRow)
	    {
		    bool blnValidReturn = true;
		    DataGridViewCellStyle columnsHeaderStyle = new DataGridViewCellStyle();

		    try {
			    mfrmGridParent = rgrdGrid.FindForm();

			    mGrdFlex = rgrdGrid;
			    mbtnAddRow = rbtnAddRow;
			    SetBtnDeleteRow = rbtnRemoveRow;

			    mGrdFlex.BeginInit();

			    mGrdFlex.Rows.Count = 1;
			    mGrdFlex.Cols.Count = 1;
                mGrdFlex.Rows.Fixed = 1;
                mGrdFlex.Cols.Fixed = 1;
               
                mGrdFlex.SelectionMode = SelectionModeEnum.Row;

                mGrdFlex.AllowResizing = AllowResizingEnum.Columns;

                mGrdFlex.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Fixed3D;

                mGrdFlex.Rows.DefaultSize = 20; 
			    mGrdFlex.Cols.DefaultSize = 70;
			    mGrdFlex.Cols[0].Width = 9;

			    if (SetDisplay != null) {
				    SetDisplay();
			    }

		    } catch (Exception ex) {
			    blnValidReturn = false;
			    sclsErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, ex.Source);
		    } finally {
			    mGrdFlex.EndInit();
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
            //object[,] dataTableArray = null;
            SqlDataAdapter dataAdapter = new SqlDataAdapter();

		    try {
                //blnBrowseOnly = mGrdFlex.Read;
                //mGrdFlex.BrowseOnly = false;

			    //Retrieve grid data from database
                sqlCmd = new SqlCommand(vstrSQL, clsApp.GetAppController.MySQLConnection);

			    mySQLReader = sqlCmd.ExecuteReader();

			    myDataTable.Load(mySQLReader);

                //dataTableArray = new object[myDataTable.Rows.Count, myDataTable.Columns.Count + 1];

			    //Set the grid data

                //for (int intTableRowIndex = 0; intTableRowIndex <= myDataTable.Rows.Count - 1; intTableRowIndex++) {

                //    for (int intTableColIndex = 0; intTableColIndex <= myDataTable.Columns.Count - 1; intTableColIndex++) {
                //        dataTableArray[intTableRowIndex, intTableColIndex] = myDataTable.Rows[intTableRowIndex][intTableColIndex];
                //    }
                //}

			    //Reset the grid
                mGrdFlex.Clear(); //ClearWhatSettings.flexClearData
			    mGrdFlex.BeginUpdate();

			    mGrdFlex.Rows.Count = myDataTable.Rows.Count;
			    mGrdFlex.Cols.Count = myDataTable.Columns.Count;

                dataAdapter.Fill(myDataTable);

                mGrdFlex.DataSource = dataAdapter;

			    mySQLReader.Dispose();

			    blnValidReturn = blnSetColsDisplay();

			    if (SetDisplay != null) {
				    SetDisplay();
			    }

			    if (mGrdFlex.Rows.Count > 0) {
				    mGrdFlex.Row = 1;
			    }

		    } catch (Exception ex) {
			    blnValidReturn = false;
			    sclsErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, ex.Source);
		    } finally {
			    if ((mySQLReader != null)) {
				    mySQLReader.Dispose();
			    }

			    mGrdFlex.EndUpdate();
		    }

		    return blnValidReturn;
	    }

	    public void AddRow(int vintPosition = -1)
	    {
		    mGrdFlex.Rows.InsertRange((vintPosition == -1 ? mGrdFlex.Rows.Count + 1 : vintPosition), 1);

		    if (!mblnHasNoActionColumn) {
			    mGrdFlex[mGrdFlex.Rows.Count, mintDefaultActionCol] = GridRowActions.INSERT_ACTION;
		    }

            mGrdFlex.Rows[(vintPosition == -1 ? mGrdFlex.Rows.Count + 1 : vintPosition)].Style.BackColor = System.Drawing.Color.LightGreen;
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
			    sclsErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, ex.Source);
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
                sclsErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, ex.Source);
		    }

		    return blnIsEmpty;
	    }

	    public bool CellIsChecked(int vintRowIdx, int vintColIdx)
	    {
		    bool blnCellIsChecked = false;

		    blnCellIsChecked = this[vintRowIdx, vintColIdx].ToUpper().Equals("TRUE");

		    return blnCellIsChecked;
	    }

	    private bool blnSetColsDisplay()
	    {
		    bool blnValidReturn = true;
		    string strGridCaption = string.Empty;
		    string[] lstColumns = null;
            CellStyle individualColStyle = mGrdFlex.Styles.Add("column");

		    try {
                strGridCaption = clsApp.GetAppController.str_GetCaption(Convert.ToInt32(mGrdFlex.Tag), clsApp.GetAppController.cUser.GetUserLangage);

			    lstColumns = Strings.Split(strGridCaption.Insert(0, "|"), "|");

			    mGrdFlex.Cols.Count = lstColumns.Length - 1;

			    //Definition of columns         
                for (int colHeaderCpt = 1; colHeaderCpt <= lstColumns.Length - 1; colHeaderCpt++)
                {
				    if (lstColumns[colHeaderCpt] == string.Empty) {
					    mGrdFlex.Cols[colHeaderCpt].Visible = true;

				    } else {
					    mGrdFlex[0, colHeaderCpt] = mGrdFlex[0, colHeaderCpt].ToString().Substring(1, lstColumns[colHeaderCpt].Length - 1);

					    switch (lstColumns[colHeaderCpt].ToCharArray()[0]) {
						    case '<':
                                individualColStyle.TextAlign = TextAlignEnum.LeftCenter;

							    break;

						    case '^':
                                individualColStyle.TextAlign = TextAlignEnum.CenterCenter;

							    break;
						    case '>':
                                individualColStyle.TextAlign = TextAlignEnum.RightCenter;

							    break;
					    }

					    mGrdFlex.Cols[colHeaderCpt].Style = individualColStyle;
				    }
			    }

			    blnValidReturn = true;

		    } catch (Exception ex) {
			    blnValidReturn = false;
			    sclsErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, ex.Source);
		    }

		    return blnValidReturn;
	    }

	    public void SetSelectedCell(int vintRowIndex, int vintColIndex, bool vblnShowDropDown = false)
	    {

            //if (vintRowIndex <= mGrdFlex.RowCount && vintColIndex <= mGrdFlex.ColCount && vintRowIndex > 0 & vintColIndex > 0) {
            //    mGrdFlex.CurrentCell.MoveTo(GridRangeInfo.Cell(vintRowIndex, vintColIndex), GridSetCurrentCellOptions.SetFocus & GridSetCurrentCellOptions.ScrollInView);


            //    if (vblnShowDropDown) {
            //        mGrdFlex.get
            //    } else {
            //        mGrdFlex.CurrentCell.BeginEdit();
            //    }
            //} else {
            //    //Do nothing
            //}
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

			    mySQLCmd = new SqlCommand(vstrSQL, clsApp.GetAppController.MySQLConnection);

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
			    sclsErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, ex.Source + " - " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name);
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
			    sclsErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, ex.Source + " - " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name);
		    }
	    }

#endregion


	    private void btnAddRow_Click(object sender, EventArgs e)
	    {
            AddRow();
	    }

	    private void btnDeleteRow_Click(object sender, EventArgs e)
	    {
		    int intSelectedRow = mGrdFlex.Row;

           
		    if (intSelectedRow > 0) {

                if (this[intSelectedRow, mintDefaultActionCol] == GridRowActions.INSERT_ACTION.ToString())
                {
				    mGrdFlex.Rows.RemoveRange(intSelectedRow, intSelectedRow);
				    mGrdFlex.Row = (intSelectedRow >= 2 ? intSelectedRow - 1 : -1);
			    } else {
                    mGrdFlex.Rows[intSelectedRow].Style.BackColor = System.Drawing.Color.Red;
				    mGrdFlex[intSelectedRow, mintDefaultActionCol] = GridRowActions.DELETE_ACTION;

                    //mfrmGridParent.formController.ChangeMade = true; TODO
			    }
		    }
	    }

        public bool bln_ValidateGridEvent()
        {
            ValidateGridEventArgs validateGridEventArgs = new ValidateGridEventArgs();

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

	    private void SyncfusionGridController_SetDisplay()
	    {
		    blnSetColsDisplay();
	    }

        private void GrdSyncController_CurrentCellCloseDropDown(object sender, RowColEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void mGrdSync_CheckBoxClick(object sender, RowColEventArgs e)
        {
            //mfrmGridParent.formController.ChangeMade = true;
        }

        private void mGrdSync_CellsChanged(object sender, RowColEventArgs e)
        {
            if (!mblnHasNoActionColumn && mGrdFlex[mGrdFlex.Row, mintDefaultActionCol].ToString().Equals(GridRowActions.INSERT_ACTION.ToString())) //&& !mGrdSync(GetSelectedRow, GetSelectedCol).ReadOnly
            {
                mGrdFlex.Rows[mGrdFlex.Row].Style.BackColor = System.Drawing.Color.Yellow;
                mGrdFlex[mGrdFlex.Row, mintDefaultActionCol] = GridRowActions.UPDATE_ACTION;
            }
        }
}


#region "Custom events"

    public class ValidateGridEventArgs : System.EventArgs
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
