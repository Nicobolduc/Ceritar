using System;
using System.Collections.Generic;
using Ceritar.TT3LightDLL;

namespace Ceritar.TT3LightDLL.Classes
{
    class clsFlexGridController
    {
        //Col 0 is the RowHeader and Row 0 is the Header of cols
        /*
	    //Private members
	    private const short mintDefaultActionCol = 1;
	    private int mintPreviousCellChangedRow;
	    private string mstrUndeterminedCheckBoxState = "TO_DEFINE";

	    private bool mblnHasNoActionColumn;
	    //Private class members
	    private GridControl withEventsField_mGrdSync;
	    private GridControl mGrdSync {
		    get { return withEventsField_mGrdSync; }
		    set {
			    if (withEventsField_mGrdSync != null) {
				    withEventsField_mGrdSync.CellsChanged -= mGrdSync_CellsChanged;
				    withEventsField_mGrdSync.CheckBoxClick -= mGrdSync_CheckBoxClick;
				    withEventsField_mGrdSync.CurrentCellAcceptedChanges -= mGrdSync_CurrentCellAcceptedChanges;
				    withEventsField_mGrdSync.CurrentCellActivating -= mGrdSync_CurrentCellActivating;
				    withEventsField_mGrdSync.CurrentCellChanged -= GrdSyncController_CurrentCellChanged;
				    withEventsField_mGrdSync.CurrentCellCloseDropDown -= GrdSyncController_CurrentCellCloseDropDown;
			    }
			    withEventsField_mGrdSync = value;
			    if (withEventsField_mGrdSync != null) {
				    withEventsField_mGrdSync.CellsChanged += mGrdSync_CellsChanged;
				    withEventsField_mGrdSync.CheckBoxClick += mGrdSync_CheckBoxClick;
				    withEventsField_mGrdSync.CurrentCellAcceptedChanges += mGrdSync_CurrentCellAcceptedChanges;
				    withEventsField_mGrdSync.CurrentCellActivating += mGrdSync_CurrentCellActivating;
				    withEventsField_mGrdSync.CurrentCellChanged += GrdSyncController_CurrentCellChanged;
				    withEventsField_mGrdSync.CurrentCellCloseDropDown += GrdSyncController_CurrentCellCloseDropDown;
			    }
		    }
	    }
	    private Button withEventsField_mbtnAddRow;
	    private Button mbtnAddRow {
		    get { return withEventsField_mbtnAddRow; }
		    set {
			    if (withEventsField_mbtnAddRow != null) {
				    withEventsField_mbtnAddRow.Click -= btnAddRow_Click;
			    }
			    withEventsField_mbtnAddRow = value;
			    if (withEventsField_mbtnAddRow != null) {
				    withEventsField_mbtnAddRow.Click += btnAddRow_Click;
			    }
		    }
	    }
	    private Button withEventsField_mbtnDeleteRow;
	    private Button mbtnDeleteRow {
		    get { return withEventsField_mbtnDeleteRow; }
		    set {
			    if (withEventsField_mbtnDeleteRow != null) {
				    withEventsField_mbtnDeleteRow.Click -= btnDeleteRow_Click;
			    }
			    withEventsField_mbtnDeleteRow = value;
			    if (withEventsField_mbtnDeleteRow != null) {
				    withEventsField_mbtnDeleteRow.Click += btnDeleteRow_Click;
			    }
		    }
	    }
	    private object mfrmGridParent;

	    private ColsSizeBehaviorsController mcGridColsSizeBehavior = null;
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
		    NO_ACTION = mConstants.Form_Mode.CONSULT_MODE,
		    INSERT_ACTION = mConstants.Form_Mode.INSERT_MODE,
		    UPDATE_ACTION = mConstants.Form_Mode.UPDATE_MODE,
		    DELETE_ACTION = mConstants.Form_Mode.DELETE_MODE
	    }


	    #region "Properties"

	    public string this[int intRowIndex, int intColIndex] {
		    get {

			    if (mGrdSync(intRowIndex, intColIndex).CellType == Syncfusion.GridHelperClasses.CustomCellTypes.DateTimePicker.ToString) {
				    return mGrdSync(intRowIndex, intColIndex).FormattedText;
			    } else {
				    return mGrdSync(intRowIndex, intColIndex).CellValue.ToString;
			    }
		    }
		    set { mGrdSync(intRowIndex, intColIndex).CellValue = value; }
	    }


	    public ColsSizeBehaviorsController.colsSizeBehaviors SetColsSizeBehavior {
		    set {
			    mcGridColsSizeBehavior = new ColsSizeBehaviorsController();
			    mcGridColsSizeBehavior.AttachGrid(mGrdSync);
			    mcGridColsSizeBehavior.ColsSizeBehavior = value;
		    }
	    }

	    public int GetSelectedRowsCount {
		    get { return mGrdSync.Selections.GetSelectedRows(true, true).Count; }
	    }

	    public int GetSelectedRow {
		    get {

			    if (mGrdSync.Selections.GetSelectedRows(true, true).Count > 0) {
				    return mGrdSync.Selections.GetSelectedRows(true, true).Item(0).Top;
			    } else {
				    return -1;
			    }
		    }
	    }

	    public int GetSelectedCol {
		    get {

			    if (mGrdSync.Selections.GetSelectedCols(true, true).Count > 0) {
				    return mGrdSync.Selections.GetSelectedCols(true, true).Item(0).Left;
			    } else {
				    return -1;
			    }
		    }
	    }

	    public int SetSelectedRow {
		    set {

			    if (value <= mGrdSync.RowCount & value > 0) {
				    mGrdSync.CurrentCell.MoveTo(GridRangeInfo.Row(value), GridSetCurrentCellOptions.SetFocus & GridSetCurrentCellOptions.ScrollInView);
			    } else {
				    //Do nothing
			    }
		    }
	    }

	    public int SetSelectedCol {
		    set {

			    if (value <= mGrdSync.ColCount & value > 0) {
				    mGrdSync.CurrentCell.MoveTo(GridRangeInfo.Cell(GetSelectedRow, value), GridSetCurrentCellOptions.SetFocus & GridSetCurrentCellOptions.ScrollInView);


				    if (vblnShowDropDown) {
					    mGrdSync.CurrentCell.ShowDropDown();
				    } else {
					    mGrdSync.CurrentCell.BeginEdit();
				    }
			    } else {
				    //Do nothing
			    }
		    }
	    }

	    public bool ChangeMade {
		    set {

			    if (mGrdSync(GetSelectedRow, mintDefaultActionCol).CellValue != GridRowActions.INSERT_ACTION) {

				    if (value == true) {
					    mfrmGridParent.formController.ChangeMade = true;

					    mGrdSync.RowStyles(GetSelectedRow).BackColor = Color.Yellow;
					    mGrdSync(GetSelectedRow, mintDefaultActionCol).CellValue = GridRowActions.UPDATE_ACTION;
				    } else {
					    mfrmGridParent.formController.ChangeMade = false;

					    mGrdSync.RowStyles(GetSelectedRow).BackColor = Color.Empty;
					    mGrdSync(GetSelectedRow, mintDefaultActionCol).CellValue = GridRowActions.NO_ACTION;
				    }
			    }
		    }
	    }

	    public string GetUndeterminedCheckBoxState {
		    get { return mstrUndeterminedCheckBoxState; }
	    }

	    public bool SetNoActionColumn {
		    set { mblnHasNoActionColumn = value; }
	    }
	    #endregion

	    #region "Functions / Subs"

	    public bool bln_Init(ref GridControl rgrdGrid, ref Button rbtnAddRow = null, ref Button rbtnRemoveRow = null)
	    {
		    bool blnValidReturn = true;
		    DataGridViewCellStyle columnsHeaderStyle = new DataGridViewCellStyle();

		    try {
			    mfrmGridParent = rgrdGrid.FindParentForm;

			    mGrdSync = rgrdGrid;
			    mbtnAddRow = rbtnAddRow;
			    mbtnDeleteRow = rbtnRemoveRow;

			    mGrdSync.BeginInit();

			    mGrdSync.RowCount = 0;
			    mGrdSync.ColCount = 0;

			    mGrdSync.ControllerOptions = GridControllerOptions.ClickCells | GridControllerOptions.ResizeCells | GridControllerOptions.SelectCells;
			    mGrdSync.CommandStack.Enabled = true;
			    mGrdSync.ResizeColsBehavior = GridResizeCellsBehavior.ResizeSingle | GridResizeCellsBehavior.OutlineHeaders | GridResizeCellsBehavior.InsideGrid;
			    mGrdSync.ListBoxSelectionMode = SelectionMode.One;
			    //mGrdSync.Model.Options.AllowSelection = GridSelectionFlags.Row
			    mGrdSync.Model.Options.ActivateCurrentCellBehavior = GridCellActivateAction.DblClickOnCell;
			    mGrdSync.TableStyle.VerticalAlignment = GridVerticalAlignment.Middle;

			    mGrdSync.ThemesEnabled = true;
			    mGrdSync.UnHideColsOnDblClick = false;
			    mGrdSync.GridVisualStyles = Syncfusion.Windows.Forms.GridVisualStyles.Office2010Blue;
			    mGrdSync.Properties.BackgroundColor = Color.LightGray;
			    mGrdSync.TableStyle.Font = new GridFontInfo(new Font("Tahoma", 10f));

			    mGrdSync.NumberedRowHeaders = false;
			    mGrdSync.NumberedColHeaders = false;
			    mGrdSync.DefaultGridBorderStyle = GridBorderStyle.Solid;
			    mGrdSync.BorderStyle = BorderStyle.Fixed3D;

			    mGrdSync.DefaultRowHeight = 20;
			    mGrdSync.DefaultColWidth = 70;
			    mGrdSync.SetColWidth(0, 0, 9);

			    if (SetDisplay != null) {
				    SetDisplay();
			    }

		    } catch (Exception ex) {
			    blnValidReturn = false;
			    gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source);
		    } finally {
			    mGrdSync.EndInit();
		    }

		    return blnValidReturn;
	    }

	    public bool bln_FillData(string vstrSQL)
	    {
		    bool blnValidReturn = true;
		    MySqlCommand sqlCmd = default(MySqlCommand);
		    MySqlDataReader mySQLReader = null;
		    DataTable myDataTable = new DataTable();
		    string strGridCaption = string.Empty;
		    object[,] dataTableArray = null;
		    bool blnBrowseOnly = false;

		    try {
			    blnBrowseOnly = mGrdSync.BrowseOnly;
			    mGrdSync.BrowseOnly = false;

			    //Retrieve grid data from database
			    sqlCmd = new MySqlCommand(vstrSQL, gcAppCtrl.MySQLConnection);

			    mySQLReader = sqlCmd.ExecuteReader;

			    myDataTable.Load(mySQLReader);

			    dataTableArray = new object[myDataTable.Rows.Count, myDataTable.Columns.Count + 1];

			    //Set the grid data

			    for (int intTableRowIndex = 0; intTableRowIndex <= myDataTable.Rows.Count - 1; intTableRowIndex++) {

				    for (int intTableColIndex = 0; intTableColIndex <= myDataTable.Columns.Count - 1; intTableColIndex++) {
					    dataTableArray(intTableRowIndex, intTableColIndex) = myDataTable.Rows(intTableRowIndex)(intTableColIndex);
				    }
			    }

			    //Reset the grid
			    mGrdSync.Model.Data.Clear();
			    mGrdSync.Model.ResetVolatileData();
			    mGrdSync.CellModels.Clear();
			    mGrdSync.RowCount = 0;
			    mGrdSync.ColCount = 0;
			    mGrdSync.BeginUpdate();

			    mGrdSync.RowCount = myDataTable.Rows.Count;
			    mGrdSync.ColCount = myDataTable.Columns.Count;

			    mGrdSync.Model.PopulateValues(GridRangeInfo.Cells(1, 1, myDataTable.Rows.Count, myDataTable.Columns.Count), dataTableArray);

			    mySQLReader.Dispose();

			    blnValidReturn = blnSetColsDisplay();

			    if (SetDisplay != null) {
				    SetDisplay();
			    }


			    if (mGrdSync.RowCount > 0) {
				    SetSelectedRow = 1;
			    }

		    } catch (Exception ex) {
			    blnValidReturn = false;
			    gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source);
		    } finally {
			    if ((mySQLReader != null)) {
				    mySQLReader.Dispose();
			    }

			    mGrdSync.EndUpdate(true);

			    mGrdSync.BrowseOnly = blnBrowseOnly;
		    }

		    return blnValidReturn;
	    }

	    public void AddRow(int vintPosition = -1)
	    {
		    mGrdSync.IgnoreReadOnly = true;

		    mGrdSync.Rows.InsertRange((vintPosition - 1 ? mGrdSync.RowCount + 1 : vintPosition), 1);
		    mGrdSync(mGrdSync.RowCount, -1) = mGrdSync(1, -1);


		    if (!mblnHasNoActionColumn) {
			    mGrdSync(mGrdSync.RowCount, mintDefaultActionCol).CellValue = SyncfusionGridController.GridRowActions.INSERT_ACTION;
		    }

		    mGrdSync.RowStyles(mGrdSync.RowCount).BackColor = Color.LightGreen;

		    mGrdSync.IgnoreReadOnly = false;
	    }

	    public bool CellIsEmpty(int vintRow, int vintCol)
	    {
		    bool blnIsEmpty = true;

		    try {
			    switch (false) {
				    case !Information.IsDBNull(mGrdSync(vintRow, vintCol).CellValue):
					    break;
				    case (mGrdSync(vintRow, vintCol).CellValue != null):
					    break;
				    case !string.IsNullOrEmpty(Strings.Trim(mGrdSync(vintRow, vintCol).CellValue.ToString)):
					    break;
				    default:
					    blnIsEmpty = false;
					    break;
			    }

		    } catch (Exception ex) {
			    gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source);
		    }

		    return blnIsEmpty;
	    }

	    public bool CurrentCellIsEmpty()
	    {
		    bool blnIsEmpty = true;

		    try {
			    switch (false) {
				    case !Information.IsDBNull(mGrdSync(GetSelectedRow, GetSelectedCol).CellValue):
					    break;
				    case (mGrdSync(GetSelectedRow, GetSelectedCol).CellValue != null):
					    break;
				    case !string.IsNullOrEmpty(Strings.Trim(mGrdSync(GetSelectedRow, GetSelectedCol).CellValue.ToString)):
					    break;
				    default:
					    blnIsEmpty = false;
					    break;
			    }

		    } catch (Exception ex) {
			    gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source);
		    }

		    return blnIsEmpty;
	    }

	    public bool CellIsChecked(int vintRowIdx, int vintColIdx)
	    {
		    bool blnCellIsChecked = false;

		    blnCellIsChecked = this[vintRowIdx, vintColIdx].ToUpper.Equals("TRUE");

		    return blnCellIsChecked;
	    }

	    private bool blnSetColsDisplay()
	    {
		    bool blnValidReturn = true;
		    string strGridCaption = string.Empty;
		    string[] lstColumns = null;
		    GridStyleInfo individualColStyle = default(GridStyleInfo);

		    try {
			    strGridCaption = gcAppCtrl.str_GetCaption(Convert.ToInt32(mGrdSync.Tag), gcAppCtrl.cUser.GetLanguage);

			    lstColumns = Strings.Split(strGridCaption.Insert(0, "|"), "|");

			    mGrdSync.ColCount = lstColumns.Count - 1;

			    //Definition of columns

			    for (int colHeaderCpt = 1; colHeaderCpt <= lstColumns.Count - 1; colHeaderCpt++) {
				    individualColStyle = new GridStyleInfo();
				    individualColStyle.HorizontalAlignment = GridHorizontalAlignment.Center;

				    if (lstColumns(colHeaderCpt) == string.Empty) {
					    mGrdSync.SetColHidden(colHeaderCpt, colHeaderCpt, true);

				    } else {
					    mGrdSync(0, colHeaderCpt).Text = Microsoft.VisualBasic.Right(lstColumns(colHeaderCpt), lstColumns(colHeaderCpt).Length - 1);

					    switch (lstColumns(colHeaderCpt).Chars(0)) {
						    case Convert.ToChar("<"):
							    individualColStyle.HorizontalAlignment = GridHorizontalAlignment.Left;

							    break;

						    case Convert.ToChar("^"):
							    individualColStyle.HorizontalAlignment = GridHorizontalAlignment.Center;

							    break;
						    case Convert.ToChar(">"):
							    individualColStyle.HorizontalAlignment = GridHorizontalAlignment.Right;

							    break;
					    }

					    //mGrdSync.ChangeCells(GridRangeInfo.Cells(0, colHeaderCpt, mGrdSync.RowCount, colHeaderCpt), individualColStyle)
					    mGrdSync(0, colHeaderCpt).HorizontalAlignment = individualColStyle.HorizontalAlignment;
					    mGrdSync.ColStyles(colHeaderCpt) = individualColStyle;
				    }

			    }

			    //Definition of visual styles
			    //headerStyle = New GridStyleInfo

			    //headerStyle.BackColor = Color.WhiteSmoke

			    //grdSync.ChangeCells(GridRangeInfo.Cells(0, 0, 0, grdSync.ColCount), headerStyle, Syncfusion.Styles.StyleModifyType.ApplyNew)
			    //grdSync.ChangeCells(GridRangeInfo.Cells(0, 0, grdSync.RowCount, 0), headerStyle)

			    blnValidReturn = true;

		    } catch (Exception ex) {
			    blnValidReturn = false;
			    gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source);
		    }

		    return blnValidReturn;
	    }

	    public int FindRow(object vValueToFind, int vintColToSearch)
	    {
		    int intReturnValue = -1;


		    for (int intRowIdx = 1; intRowIdx <= mGrdSync.RowCount; intRowIdx++) {

			    if (mGrdSync(intRowIdx, vintColToSearch).CellValue == vValueToFind) {
				    return intRowIdx;
			    } else {
				    //Continue searching
			    }
		    }

		    return intReturnValue;
	    }

	    public int FindCol(object vValueToFind, int vintRowToSearch)
	    {
		    int intReturnValue = -1;


		    for (int intColIdx = 1; intColIdx <= mGrdSync.ColCount; intColIdx++) {

			    if (mGrdSync(intColIdx, vintRowToSearch).CellValue == vValueToFind) {
				    return intColIdx;
			    } else {
				    //Continue searching
			    }
		    }

		    return intReturnValue;
	    }


	    public void SetSelectedCell(int vintRowIndex, int vintColIndex, bool vblnShowDropDown = false)
	    {

		    if (vintRowIndex <= mGrdSync.RowCount && vintColIndex <= mGrdSync.ColCount && vintRowIndex > 0 & vintColIndex > 0) {
			    mGrdSync.CurrentCell.MoveTo(GridRangeInfo.Cell(vintRowIndex, vintColIndex), GridSetCurrentCellOptions.SetFocus & GridSetCurrentCellOptions.ScrollInView);


			    if (vblnShowDropDown) {
				    mGrdSync.CurrentCell.ShowDropDown();
			    } else {
				    mGrdSync.CurrentCell.BeginEdit();
			    }
		    } else {
			    //Do nothing
		    }
	    }

	    public void SetColType_CheckBox(int vintColumnIndex, bool vblnAllowTriStates = false)
	    {
		    mGrdSync.IgnoreReadOnly = true;

		    mGrdSync.ColStyles(vintColumnIndex).CellType = "CheckBox";
		    mGrdSync.ColStyles(vintColumnIndex).CheckBoxOptions = new GridCheckBoxCellInfo(true.ToString(), false.ToString(), mstrUndeterminedCheckBoxState, false);
		    mGrdSync.ColStyles(vintColumnIndex).TriState = vblnAllowTriStates;
		    mGrdSync.ColStyles(vintColumnIndex).VerticalAlignment = GridVerticalAlignment.Middle;

		    mGrdSync.IgnoreReadOnly = false;
	    }

	    public void SetColType_ComboBox(string vstrSQL, int vintColumnIndex, string vstrValueMember, string vstrDisplayMember, bool vblnAllowEmpty)
	    {
		    MySqlCommand mySQLCmd = default(MySqlCommand);
		    MySqlDataReader mySQLReader = null;
		    BindingList<KeyValuePair<int, string>> myBindingList = new BindingList<KeyValuePair<int, string>>();
		    GridStyleInfo style = new GridStyleInfo();

		    try {
			    mGrdSync.ColStyles(vintColumnIndex).CellType = "ComboBox";
			    mGrdSync.ColStyles(vintColumnIndex).DataSource = null;
			    mGrdSync.ColStyles(vintColumnIndex).ExclusiveChoiceList = true;

			    mySQLCmd = new MySqlCommand(vstrSQL, gcAppCtrl.MySQLConnection);

			    mySQLReader = mySQLCmd.ExecuteReader;

			    if (vblnAllowEmpty) {
				    myBindingList.Add(new KeyValuePair<int, string>(0, ""));
			    }
			    //mySQLReader.resultset.fields(0).columnname
			    while (mySQLReader.Read) {
				    if (!Information.IsDBNull(mySQLReader(vstrValueMember))) {
					    myBindingList.Add(new KeyValuePair<int, string>(Convert.ToInt32(mySQLReader(vstrValueMember)), Convert.ToString(mySQLReader(vstrDisplayMember))));
				    }
			    }

			    mGrdSync.ColStyles(vintColumnIndex).DataSource = myBindingList;
			    mGrdSync.ColStyles(vintColumnIndex).ValueMember = "Key";
			    mGrdSync.ColStyles(vintColumnIndex).DisplayMember = "Value";

		    } catch (Exception ex) {
			    gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source + " - " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name);
		    } finally {
			    if ((mySQLReader != null)) {
				    mySQLReader.Dispose();
			    }
		    }
	    }

	    public void SetColType_DateTimePicker(int vintColumnIndex, bool vblnNullable)
	    {
		    DateTimePickerCell.DateTimePickerCellModel dtPickerCell = new DateTimePickerCell.DateTimePickerCellModel(mGrdSync.Model, vblnNullable);

		    try {
			    if (!mGrdSync.CellModels.ContainsKey(CustomCellTypes.DateTimePicker.ToString)) {
				    RegisterCellModel.GridCellType(mGrdSync, CustomCellTypes.DateTimePicker);
			    }

			    //If Not mGrdSync.CellModels.ContainsKey("DateTimePicker") Then
			    //    mGrdSync.CellModels.Add("DateTimePicker", dtPickerCell)
			    //End If

			    //mGrdSync.ColStyles(vintColumnIndex).CellType = "DateTimePicker"
			    mGrdSync.ColStyles(vintColumnIndex).CellType = CustomCellTypes.DateTimePicker.ToString();
			    mGrdSync.ColStyles(vintColumnIndex).CellValueType = typeof(DateTime);
			    mGrdSync.ColStyles(vintColumnIndex).Format = gcAppCtrl.str_GetUserDateFormat;

			    mGrdSync.ColWidths(vintColumnIndex) = 85;

			    if (!vblnNullable) {
				    DateTimeCellRenderer renderer = mGrdSync.CellRenderers(CustomCellTypes.DateTimePicker.ToString()) as DateTimeCellRenderer;
				    MyDateTimePicker dtPicker = default(MyDateTimePicker);


				    foreach (Control contl in renderer.Grid.Controls) {

					    if (contl is MyDateTimePicker) {
						    dtPicker = contl as MyDateTimePicker;
						    dtPicker.NoneButtonVisible = false;
					    }
				    }
			    }

		    } catch (Exception ex) {
			    gcAppCtrl.cErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, Err.Source + " - " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name);
		    }
	    }

	    public bool bln_ValidateGridEvent()
	    {
		    ValidateGridEventArgs validateGridEventArgs = new ValidateGridEventArgs();

		    if (ValidateGridData != null) {
			    ValidateGridData(validateGridEventArgs);
		    }

		    return validateGridEventArgs.IsValid;
	    }

	    public bool bln_SaveGridDataEvent()
	    {
		    SaveGridDataEventArgs saveGridDataArgs = new SaveGridDataEventArgs();

		    if (SaveGridData != null) {
			    SaveGridData(saveGridDataArgs);
		    }

		    return saveGridDataArgs.SaveSuccessful;
	    }

	    #endregion


	    private void btnAddRow_Click(object sender, EventArgs e)
	    {
		    mGrdSync.RowCount += 1;

		    mGrdSync.RowStyles(mGrdSync.RowCount).BackColor = Color.LightGreen;
		    mGrdSync(mGrdSync.RowCount, mintDefaultActionCol).CellValue = GridRowActions.INSERT_ACTION;
		    SetSelectedRow = mGrdSync.RowCount;

		    mfrmGridParent.formController.ChangeMade = true;
	    }

	    private void btnDeleteRow_Click(object sender, EventArgs e)
	    {
		    int intSelectedRow = GetSelectedRow;


		    if (intSelectedRow > 0) {

			    if (mGrdSync(intSelectedRow, mintDefaultActionCol).CellValue == SyncfusionGridController.GridRowActions.INSERT_ACTION) {
				    mGrdSync.Rows.RemoveRange(intSelectedRow, intSelectedRow);
				    SetSelectedRow = (intSelectedRow >= 2 ? intSelectedRow - 1 : -1);
			    } else {
				    mGrdSync.RowStyles(intSelectedRow).BackColor = Color.Red;
				    mGrdSync(intSelectedRow, mintDefaultActionCol).CellValue = GridRowActions.DELETE_ACTION;

				    mfrmGridParent.formController.ChangeMade = true;
			    }

		    }
	    }


	    private void mGrdSync_CellsChanged(object sender, GridCellsChangedEventArgs e)
	    {
		    //If Not mblnHasNoActionColumn AndAlso Not mfrmGridParent.formController.FormIsLoading AndAlso e.Range.Top > 0 AndAlso mGrdSync(e.Range.Top, mintDefaultActionCol).CellValue <> GridRowActions.INSERT_ACTION Then

		    //    mGrdSync.BeginUpdate()

		    //    If mintPreviousCellChangedRow <> e.Range.Top Then

		    //        mintPreviousCellChangedRow = e.Range.Top
		    //        mGrdSync.RowStyles(e.Range.Top).BackColor = Color.Yellow
		    //        mGrdSync(GetSelectedRow, mintDefaultActionCol).CellValue = GridRowActions.UPDATE_ACTION
		    //    End If

		    //    mGrdSync.EndUpdate()
		    //End If
	    }


	    private void mGrdSync_CheckBoxClick(object sender, GridCellClickEventArgs e)
	    {
		    mfrmGridParent.formController.ChangeMade = true;
	    }


	    private void mGrdSync_CurrentCellAcceptedChanges(object sender, CancelEventArgs e)
	    {

		    if (!mblnHasNoActionColumn && Conversion.Val(mGrdSync(GetSelectedRow, mintDefaultActionCol).CellValue) != GridRowActions.INSERT_ACTION && !mGrdSync(GetSelectedRow, GetSelectedCol).ReadOnly) {
			    mGrdSync.RowStyles(GetSelectedRow).BackColor = Color.Yellow;
			    mGrdSync(GetSelectedRow, mintDefaultActionCol).CellValue = GridRowActions.UPDATE_ACTION;
		    }
	    }


	    private void mGrdSync_CurrentCellActivating(object sender, Syncfusion.Windows.Forms.Grid.GridCurrentCellActivatingEventArgs e)
	    {

		    if (mGrdSync(e.RowIndex, e.ColIndex).CellType == CustomCellTypes.DateTimePicker.ToString && mGrdSync(e.RowIndex, e.ColIndex).ReadOnly) {
			    e.Cancel = true;
		    }
	    }


	    private void GrdSyncController_CurrentCellChanged(object sender, EventArgs e)
	    {

		    if (!mblnHasNoActionColumn && Conversion.Val(mGrdSync(GetSelectedRow, mintDefaultActionCol).CellValue) != GridRowActions.INSERT_ACTION && !mGrdSync(GetSelectedRow, GetSelectedCol).ReadOnly) {
			    mGrdSync.RowStyles(GetSelectedRow).BackColor = Color.Yellow;
			    mGrdSync(GetSelectedRow, mintDefaultActionCol).CellValue = GridRowActions.UPDATE_ACTION;
		    }
	    }

	    private void GrdSyncController_CurrentCellCloseDropDown(object sender, Syncfusion.Windows.Forms.PopupClosedEventArgs e)
	    {
		    mGrdSync.CurrentCell.ConfirmChanges();
	    }

	    private void SyncfusionGridController_SetDisplay()
	    {
		    blnSetColsDisplay();
	    }
	    public SyncfusionGridController()
	    {
		    SetDisplay += SyncfusionGridController_SetDisplay;
	    }
        */
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
