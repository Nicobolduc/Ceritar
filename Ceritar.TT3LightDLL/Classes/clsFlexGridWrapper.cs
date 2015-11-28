﻿using System;
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

    public class clsFlexGridWrapper
    {
        //Docuumentation available here: //http://helpcentral.componentone.com/nethelp/c1flexgrid/topic132.html
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

        private CellStyle csNewRow;
        private CellStyle csRemoveRow;

	    //Public events
	    public event SetDisplayEventHandler SetGridDisplay;
	    public delegate void SetDisplayEventHandler();
	    public event ValidateGridDataEventHandler ValidateGridData;
	    public delegate void ValidateGridDataEventHandler(ValidateGridEventArgs eventArgs);
	    public event SaveGridDataEventHandler SaveGridData;
	    public delegate void SaveGridDataEventHandler(SaveGridDataEventArgs eventArgs);

	    //Public enums
	    public enum GridRowActions
	    {
		    NO_ACTION = sclsConstants.DML_Mode.CONSULT_MODE,
            INSERT_ACTION = sclsConstants.DML_Mode.INSERT_MODE,
            UPDATE_ACTION = sclsConstants.DML_Mode.UPDATE_MODE,
            DELETE_ACTION = sclsConstants.DML_Mode.DELETE_MODE
	    }


        public clsFlexGridWrapper()
        {
            SetGridDisplay += clsFlexGridWrapper_SetDisplay;
        }


#region "Properties"

        private C1FlexGrid GrdFlex
        {
            get { return mGrdFlex; }
            set
            {
                if (mGrdFlex != null)
                {
                    mGrdFlex.CellChanged -= mGrdFlex_CellsChanged;
                    mGrdFlex.CellChecked -= mGrdFlex_CheckBoxClick;
                    mGrdFlex.ComboCloseUp -= GrdFlex_CurrentCellCloseDropDown;
                }
                mGrdFlex = value;
                if (mGrdFlex != null)
                {
                    mGrdFlex.CellChanged += mGrdFlex_CellsChanged;
                    mGrdFlex.CellChecked += mGrdFlex_CheckBoxClick;
                    mGrdFlex.ComboCloseUp += GrdFlex_CurrentCellCloseDropDown;
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
               
			    if (mGrdFlex[mGrdFlex.Row, mintDefaultActionCol].ToString().Equals(GridRowActions.INSERT_ACTION.ToString())) {

				    if (value == true) {

                        //mfrmGridParent.formController.ChangeMade = true; TODO

					    mGrdFlex.Rows[mGrdFlex.Row].StyleDisplay.BackColor = System.Drawing.Color.Yellow;
                        mGrdFlex[mGrdFlex.Row, mintDefaultActionCol] = GridRowActions.UPDATE_ACTION;
				    } else {
                        //mfrmGridParent.formController.ChangeMade = false;

                        mGrdFlex.Rows[mGrdFlex.Row].StyleDisplay.BackColor = System.Drawing.Color.Empty;
                        mGrdFlex[mGrdFlex.Row, mintDefaultActionCol] = GridRowActions.NO_ACTION;
				    }
			    }
		    }
	    }

	    public bool HasActionColumn {
		    set { mblnHasNoActionColumn = value; }
	    }

#endregion


#region "Functions / Subs"

	    public bool bln_Init(ref C1FlexGrid rgrdGrid, ref Button rbtnAddRow, ref Button rbtnRemoveRow)
	    {
		    bool blnValidReturn = true;

		    try {
			    mfrmGridParent = rgrdGrid.FindForm();

			    mGrdFlex = rgrdGrid;

                SetBtnAddRow = rbtnAddRow;
			    SetBtnDeleteRow = rbtnRemoveRow;

			    mGrdFlex.BeginInit();

                mGrdFlex.DataSource = null;

                mGrdFlex.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

			    mGrdFlex.Rows.Count = 1;
			    mGrdFlex.Cols.Count = 1;
                mGrdFlex.Rows.Fixed = 1;
                mGrdFlex.Cols.Fixed = 1;
               
                mGrdFlex.SelectionMode = SelectionModeEnum.Row;
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

                csRemoveRow = mGrdFlex.Styles.Add("RemoveRow");
                csRemoveRow.BackColor = System.Drawing.Color.Red;

		    } catch (Exception ex) {
			    blnValidReturn = false;
			    sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
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
            string[] dataTableArray = null;

		    try {
                mGrdFlex.BeginUpdate();

                sqlCmd = new SqlCommand(vstrSQL, clsApp.GetAppController.SQLConnection);

			    mySQLReader = sqlCmd.ExecuteReader();

			    myDataTable.Load(mySQLReader);

                mGrdFlex.Rows.Count = myDataTable.Rows.Count;

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

                if (mGrdFlex.Cols.Count != myDataTable.Columns.Count)
                {
                    blnValidReturn = false;
                }  

                //Binded
                //mGrdFlex.DataSource = myDataTable;

			    mySQLReader.Dispose();

			    blnValidReturn = blnSetColsDisplay();

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

	    private bool blnSetColsDisplay()
	    {
		    bool blnValidReturn = true;
		    string strGridCaption = string.Empty;
		    string[] lstColumns = null;
            CellStyle individualColStyle = mGrdFlex.Styles.Add("column");

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
			    sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
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

            mGrdFlex.Rows.InsertRange((vintPosition == -1 ? mGrdFlex.Rows.Count : vintPosition), 1);

            if (!mblnHasNoActionColumn)
            {
                mGrdFlex[mGrdFlex.Rows.Count - 1, mintDefaultActionCol] = GridRowActions.INSERT_ACTION;
            }

            mGrdFlex.Row = (vintPosition == -1 ? mGrdFlex.Rows.Count - 1 : vintPosition);

            crRow = mGrdFlex.GetCellRange((vintPosition == -1 ? mGrdFlex.Rows.Count - 1 : vintPosition), 0, (vintPosition == -1 ? mGrdFlex.Rows.Count - 1 : vintPosition), mGrdFlex.Cols.Count - 1);
            crRow.Style = csNewRow;
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
                
                if (this[intSelectedRow, mintDefaultActionCol] == GridRowActions.INSERT_ACTION.ToString())
                {
                    mGrdFlex.RemoveItem(intSelectedRow);
				    mGrdFlex.Row = (intSelectedRow >= 2 ? intSelectedRow - 1 : -1);
			    } else {
				    mGrdFlex[intSelectedRow, mintDefaultActionCol] = GridRowActions.DELETE_ACTION;

                    crRow = mGrdFlex.GetCellRange(intSelectedRow, 0, intSelectedRow, mGrdFlex.Cols.Count - 1);
                    crRow.Style = csRemoveRow;

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

	    private void clsFlexGridWrapper_SetDisplay()
	    {
            blnSetColsDisplay();
	    }

        private void GrdFlex_CurrentCellCloseDropDown(object sender, RowColEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void mGrdFlex_CheckBoxClick(object sender, RowColEventArgs e)
        {
            //mfrmGridParent.formController.ChangeMade = true;
        }

        private void mGrdFlex_CellsChanged(object sender, RowColEventArgs e)
        {
            if (!mblnHasNoActionColumn && mGrdFlex[mGrdFlex.Row, mintDefaultActionCol].ToString().Equals(GridRowActions.INSERT_ACTION.ToString()))
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