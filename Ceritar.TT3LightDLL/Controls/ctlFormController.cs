using System;
using System.ComponentModel;
using System.Collections.Generic;
using Ceritar.TT3LightDLL;
using Ceritar.TT3LightDLL.Static_Classes;
using System.Windows.Forms;

namespace Ceritar.TT3LightDLL.Controls
{
    /// <summary>
    /// Cette classe est un controleur de Windows Form. Elle offre des fonctionnalités, propriétés et évènements permettant de gérer l'ouverture, 
    /// le chargement des données, la validation et la sauvegarde des informations. 
    /// Elle permet aux Forms de recevoir à leur ouverture et de retourner à leur fermeture de l'information.
    /// </summary>
    public partial class ctlFormController : System.Windows.Forms.UserControl, INotifyPropertyChanged
    {
        //Public members
        public bool mblnDisableBeNotify = false;

        //Private members
        private int mintItem_ID;
        private sclsConstants.DML_Mode mintFormMode;
        private bool mblnChangeMade;
        private bool mblnFormIsLoading;
        private bool mblnShowButtonQuitOnly;

        private System.Windows.Forms.Form mfrmParent;
        private System.Windows.Forms.Form mfrmCallingForm;
        private NotifyCallerEventArgs mcNotifyCallerEventArgs;

        //Public Events
        public delegate void BeNotifyEventHandler(BeNotifyEventArgs eventArgs);
        public event BeNotifyEventHandler BeNotify;
        public delegate void NotifyCallerEventHandler(NotifyCallerEventArgs eventArgs);
        public event NotifyCallerEventHandler NotifyCaller;
        public event PropertyChangedEventHandler PropertyChanged;
        public delegate void SetReadRightsEventHandler();
        public event SetReadRightsEventHandler SetReadRights;
        public delegate void LoadDataEventHandler(LoadDataEventArgs eventArgs);
        public event LoadDataEventHandler LoadData;
        public delegate void ValidateFormEventHandler(ValidateFormEventArgs eventArgs);
        public event ValidateFormEventHandler ValidateForm;
        public delegate void SaveDataEventHandler(SaveDataEventArgs eventArgs);
        public event SaveDataEventHandler SaveData;


#region "Contructor / Destructor"

        public ctlFormController()
        {
            InitializeComponent();
            SubscribeToEvents();

            this.PropertyChanged += ctlFormController_PropertyChanged;
        }

        ~ctlFormController()
        {
            this.Dispose();
        }

#endregion

    
#region Properties

        [Browsable(false)]
        public int Item_NRI
        {
            get
            {
                return mintItem_ID;
            }
            set
            {
                mintItem_ID = value;
            }
        }

        [Browsable(false)]
        public sclsConstants.DML_Mode FormMode
        {
            get
            {
                return mintFormMode;
            }
            set
            {
                mintFormMode = value;
                SetControlDisplay();
            }
        }

        [Browsable(false)]
        public bool ChangeMade
        {
            get { return mblnChangeMade; }
            set
            {
                if (!FormIsLoading)
                {
                    mblnChangeMade = value;
                }
                else
                {
                    mblnChangeMade = false;
                }

                SetControlDisplay();
            }
        }

        [Browsable(false)]
        public bool FormIsLoading
        {
            get
            {
                return mblnFormIsLoading;
            }
            set
            {
                mblnFormIsLoading = value;

                if (mfrmParent != null && mblnFormIsLoading)
                {
                    this.Cursor = Cursors.WaitCursor;
                    mfrmParent.SuspendLayout();
                }
                else if (mfrmParent != null)
                {
                    this.Cursor = Cursors.Default;
                    mfrmParent.ResumeLayout();
                }
            }
        }

        [Browsable(true)]
        public bool ShowButtonQuitOnly
        {
            get
            {
                return mblnShowButtonQuitOnly;
            }
            set
            {
                mblnShowButtonQuitOnly = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("ShowButtonQuitOnly"));
            }
        }

#endregion


#region Functions / Subs

        public void ShowForm(IWin32Window vintParentHandle, sclsConstants.DML_Mode vintFormMode, ref int rintItem_ID, bool vblnIsModal = false, bool vblnDisableParent =false)
        {

            mintFormMode = vintFormMode;
            mintItem_ID = rintItem_ID;

            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

                mfrmParent = this.FindForm();

                switch (FormMode)
                {
                    case sclsConstants.DML_Mode.DELETE_MODE:
                    case sclsConstants.DML_Mode.CONSULT_MODE:

                        sclsWinControls_Utilities.DisableAllFormControls(mfrmParent, null, null);

                        break;
                }

                SetControlDisplay();

                LoadLinkedFormData();

                if (!mfrmParent.IsDisposed)
                {
                    mfrmParent.FormClosing += mfrmParent_FormClosing;
                    mfrmParent.FormClosed += mfrmParent_FormClosed;

                    if (!vblnIsModal)
                    {
                        mfrmCallingForm = (Form)Form.FromHandle(vintParentHandle.Handle);

                        if (mfrmParent.MdiParent != null)
                        {
                            mfrmParent.Show();
                        }
                        else
                        {
                            if (vblnDisableParent)
                            {
                                mfrmCallingForm.Enabled = false;
                            }

                            mfrmParent.MdiParent = mfrmCallingForm.MdiParent;
                            mfrmParent.Show();
                        }
                    }
                    else
                    {
                        mfrmParent.MdiParent = null;
                        mfrmParent.ShowInTaskbar = false;

                        this.Cursor = System.Windows.Forms.Cursors.Default;

                        mfrmParent.ShowDialog(vintParentHandle);
                    }

                    rintItem_ID = mintItem_ID;
                }
                else
                {
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                sclsErrorsLog.WriteToErrorLog(ex, ex.Source);
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }

        private void SetControlDisplay()
        {
            switch (mintFormMode)
            {
                case sclsConstants.DML_Mode.INSERT_MODE:
                    btnApply.Text = "&Enregistrer";
                    imgFormMode.Image = Ceritar.TT3LightDLL.Properties.Resources.AddItem;

                    break;

                case sclsConstants.DML_Mode.UPDATE_MODE:
                    btnApply.Text = "&Appliquer";
                    imgFormMode.Image = Ceritar.TT3LightDLL.Properties.Resources.ModifyItem;

                    break;

                case sclsConstants.DML_Mode.CONSULT_MODE:
                    btnApply.Enabled = false;
                    btnCancel.Enabled = false;
                    btnQuit.Enabled = true;

                    btnApply.Text = "&Appliquer";
                    imgFormMode.Image = Ceritar.TT3LightDLL.Properties.Resources.ConsultItem;

                    break;

                case sclsConstants.DML_Mode.DELETE_MODE:
                    btnApply.Enabled = true;
                    btnCancel.Enabled = false;
                    btnQuit.Enabled = true;

                    btnApply.Text = "&Supprimer";
                    imgFormMode.Image = Ceritar.TT3LightDLL.Properties.Resources.DeleteItem;
                    
                    break;
            }

            imgFormMode.BackgroundImageLayout = ImageLayout.Zoom;
            imgFormMode.SizeMode = PictureBoxSizeMode.Zoom;

            if ((mintFormMode == sclsConstants.DML_Mode.INSERT_MODE) || (mintFormMode == sclsConstants.DML_Mode.UPDATE_MODE))
            {
                if (mblnChangeMade)
                {
                    btnApply.Enabled = true;
                    btnCancel.Enabled = true;
                    btnQuit.Enabled = false;
                }
                else
                {
                    btnApply.Enabled = false;
                    btnCancel.Enabled = false;
                    btnQuit.Enabled = true;
                }
            }
        }

        public void LoadLinkedFormData()
        {
            FormIsLoading = true;

            ChangeMade = false;

            if (LoadData != null)
                LoadData(new LoadDataEventArgs(mintItem_ID));

            if (SetReadRights != null)
                SetReadRights();

            FormIsLoading = false;
        }

        private void SetControlsVisility()
        {
            if (mblnShowButtonQuitOnly)
            {

                this.Width = btnQuit.Width + 10;
                imgFormMode.Visible = false;
            }
            else
            {
                this.Width = 324;
                imgFormMode.Visible = true;
            }
        }

        internal void CallBeNotify(BeNotifyEventArgs e)
        {
            if (!mblnDisableBeNotify && BeNotify != null)
                BeNotify(e);
        }

        public bool pfblnValidate_Grids()
        {
            bool blnValidReturn = false;

            Type type = this.mfrmParent.GetType();
            System.Reflection.MemberInfo[] fields = type.GetFields();

            foreach (System.Reflection.MemberInfo property in fields)

            {
                if (fields.GetType() == typeof(Ceritar.TT3LightDLL.Classes.clsTTC1FlexGridWrapper))
                {
                    MessageBox.Show("Name: " + property.Name); 
                }
            }
            //foreach (Ceritar.TT3LightDLL.Classes.clsC1FlexGridWrapper cGrid in this.mfrmParent.Controls)
            //{

            //}

            return blnValidReturn;
        }

#endregion

        void mfrmParent_FormClosing(object sender, FormClosingEventArgs e)
        {
            mcNotifyCallerEventArgs = new NotifyCallerEventArgs();
            mcNotifyCallerEventArgs.LstReceivedValues.Add(mfrmParent.Name);

            if (NotifyCaller != null)
            {
                NotifyCaller(mcNotifyCallerEventArgs);
            }
        }

        void mfrmParent_FormClosed(object sender, FormClosedEventArgs e)
        {
            BeNotifyEventArgs beNotifyEventArgs;

            if (mfrmCallingForm != null)
            {
                beNotifyEventArgs = new BeNotifyEventArgs(mcNotifyCallerEventArgs.LstReceivedValues);

                mfrmCallingForm.Enabled = true;
                mfrmCallingForm.Focus();
                mfrmCallingForm.BringToFront();

                if (FormMode == sclsConstants.DML_Mode.INSERT_MODE && mintItem_ID != 0)
                    beNotifyEventArgs.NewItemInserted = true;

                if (mfrmCallingForm is IFormController)
                    ((IFormController)mfrmCallingForm).GetFormController().CallBeNotify(beNotifyEventArgs);
            }
        }

        void ctlFormController_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (mblnShowButtonQuitOnly)
            {
                imgFormMode.Visible = false;
                btnCancel.Visible = false;
                btnApply.Visible = false;

                this.Width = 80;
            }
            else
            {
                imgFormMode.Visible = true;
                btnCancel.Visible = true;
                btnApply.Visible = true;

                this.Width = 324;
            }
        }

        private void btnApply_Click(object sender, System.EventArgs e)
        {
            SaveDataEventArgs saveEvent = new SaveDataEventArgs();
            ValidateFormEventArgs validationEvent = new ValidateFormEventArgs();

            this.Cursor = Cursors.WaitCursor;

            try
            {
                if (ValidateForm != null)
                    ValidateForm(validationEvent);

                if (validationEvent.IsValid)
                {
                    btnApply.Enabled = false;

                    if (SaveData != null)
                        SaveData(saveEvent);

                    if (saveEvent.SaveSuccessful)
                    {
                        ChangeMade = false;

                        switch (mintFormMode)
                        {
                            case sclsConstants.DML_Mode.INSERT_MODE:
                                FormMode = sclsConstants.DML_Mode.UPDATE_MODE;
                                LoadLinkedFormData();

                                break;

                            case sclsConstants.DML_Mode.UPDATE_MODE:
                                LoadLinkedFormData();

                                break;

                            case sclsConstants.DML_Mode.DELETE_MODE:
                                mfrmParent.Close();

                                break;
                        }
                    }
                    else
                    {
                        //Do nothing
                    }
                }
            }
            finally
            {
                if (!saveEvent.SaveSuccessful) btnApply.Enabled = true;
            }
            
            this.Cursor = Cursors.Default;
        }

        public void ReloadForm(sclsConstants.DML_Mode vintFormMode = TT3LightDLL.Static_Classes.sclsConstants.DML_Mode.NO_MODE)
        {
            this.Cursor = Cursors.WaitCursor;

            ChangeMade = false;

            mintFormMode = (vintFormMode == sclsConstants.DML_Mode.NO_MODE ? mintFormMode : vintFormMode);

            SetControlDisplay();

            LoadLinkedFormData();

            this.Cursor = Cursors.Default;
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.FormIsLoading = true;
            sclsWinControls_Utilities.EmptyAllFormControls(mfrmParent);
            LoadLinkedFormData();
        }

        private void btnQuit_Click(object sender, System.EventArgs e)
        {
            mfrmParent.Close();
            this.Dispose();
        }

        private void ctlFormControler_LocationChanged(object sender, System.EventArgs e)
        {
            if (mfrmParent != null && !FormIsLoading)
            {
                mfrmParent.ResumeLayout();
            }
        }

        private void ctlFormControler_Move(object sender, System.EventArgs e)
        {
            if (mfrmParent != null && !FormIsLoading)
            {
                mfrmParent.SuspendLayout();
            }
        }

        private void mfrmParent_ResizeBegin(object sender, System.EventArgs e)
        {
            mfrmParent.SuspendLayout();
        }

        private void mfrmParent_ResizeEnd(object sender, System.EventArgs e)
        {
            mfrmParent.ResumeLayout();
        }

        //Private Sub ctlFormController_ValidateForm(ByVal eventArgs As ValidateFormEventArgs) Handles Me.ValidateForm

        //    For Each cGridCtl As Object In mfrmParent.Controls

        //        If TypeOf (cGridCtl) Is SyncfusionGridController Then

        //            Dim validateGridData As System.Reflection.MethodInfo = cGridCtl.GetType().GetMethod("ValidateGridData", System.Reflection.BindingFlags.NonPublic Or System.Reflection.BindingFlags.Instance)
        //            validateGridData.Invoke(cGridCtl, New Object() {New ValidateGridEventArgs()})
        //        End If
        //    Next
        //End Sub
    }


#region Custom events

    public class LoadDataEventArgs : System.EventArgs
    {
        private int mintItem_ID;

        public int Item_ID
        {
            get
            {
                return mintItem_ID;
            }
        }

        public LoadDataEventArgs(int vintItem_ID)
        {
            mintItem_ID = vintItem_ID;
        }
    }

    public class SaveDataEventArgs : System.EventArgs
    {
        private bool mblnSaveSuccessful;

        public bool SaveSuccessful
        {
            get
            {
                return mblnSaveSuccessful;
            }
            set
            {
                mblnSaveSuccessful = value;
            }
        }
    }

    public class ValidateFormEventArgs : System.EventArgs
    {
        private bool mblnIsValid;

        public bool IsValid
        {
            get
            {
                return mblnIsValid;
            }
            set
            {
                mblnIsValid = value;
            }
        }
    }

    public class BeNotifyEventArgs : System.EventArgs
    {
        private List<object> _lstReceivedValues;
        private bool mblnNewItemInserted;

        public List<object> LstReceivedValues
        {
            get
            {
                if (_lstReceivedValues == null) _lstReceivedValues = new List<object>();

                return _lstReceivedValues;
            }
            set
            {
                _lstReceivedValues = value;
            }
        }

        public bool NewItemInserted
        {
            get
            {
                return mblnNewItemInserted;
            }
            set
            {
                mblnNewItemInserted = value;
            }
        }

        public BeNotifyEventArgs() {}

        public BeNotifyEventArgs(List<object> rlstParams)
        {
            _lstReceivedValues = rlstParams;
        }
    }

    public class NotifyCallerEventArgs : System.EventArgs
    {
        private List<object> _lstReceivedValues;

        public List<object> LstReceivedValues
        {
            get
            {
                if (_lstReceivedValues == null) _lstReceivedValues = new List<object>();

                return _lstReceivedValues;
            }
            set
            {
                _lstReceivedValues = value;
            }
        }

        public NotifyCallerEventArgs() { }

        public NotifyCallerEventArgs(List<object> rlstParams)
        {
            _lstReceivedValues = rlstParams;
        }
    }

#endregion
    
}