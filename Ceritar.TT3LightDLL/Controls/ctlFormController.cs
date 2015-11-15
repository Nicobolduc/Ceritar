﻿using System;
using System.ComponentModel;
using System.Collections.Generic;
using Ceritar.TT3LightDLL;
using Ceritar.TT3LightDLL.Static_Classes;
using System.Windows.Forms;

namespace Ceritar.TT3LightDLL.Controls
{

    public partial class ctlFormController : System.Windows.Forms.UserControl, INotifyPropertyChanged
    {

        //Private members
        public ctlFormController()
        {
            InitializeComponent();
            SubscribeToEvents();
        }

        private int mintItem_ID;
        private sclsConstants.Form_Mode mintFormMode;
        private bool mblnChangeMade;
        private bool mblnFormIsLoading;
        private bool mblnShowButtonQuitOnly;

        private System.Windows.Forms.Form mfrmParent;

        //Public Events
        public delegate void BeNotifyEventHandler(BeNotifyEventArgs eventArgs);
        //public event BeNotifyEventHandler BeNotify;
        public event PropertyChangedEventHandler PropertyChanged;
        public EventHandler PropertyChanged2;
        public delegate void SetReadRightsEventHandler();
        public event SetReadRightsEventHandler SetReadRights;
        public delegate void LoadDataEventHandler(LoadDataEventArgs eventArgs);
        public event LoadDataEventHandler LoadData;
        public delegate void ValidateFormEventHandler(ValidateFormEventArgs eventArgs);
        public event ValidateFormEventHandler ValidateForm;
        public delegate void SaveDataEventHandler(SaveDataEventArgs eventArgs);
        public event SaveDataEventHandler SaveData;


#region Properties

        [Browsable(false)]
        public int Item_ID
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
        public sclsConstants.Form_Mode FormMode
        {
            get
            {
                return mintFormMode;
            }
            set
            {
                mintFormMode = value;
                SetVisualStyle();
            }
        }

        [Browsable(false)]
        public bool ChangeMade
        {
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

                SetVisualStyle();
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

        public void ShowForm(sclsConstants.Form_Mode vintFormMode, ref int rintItem_ID, bool vblnIsModal = false)
        {

            mintFormMode = vintFormMode;
            mintItem_ID = rintItem_ID;

            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

                mfrmParent = base.FindForm();

                SetVisualStyle();

                LoadFormData();

                if (!mfrmParent.IsDisposed)
                {
                    if (!vblnIsModal)
                    {
                        mfrmParent.MdiParent = mfrmParent.MdiParent;
                        
                        mfrmParent.Show();
                    }
                    else
                    {
                        mfrmParent.MdiParent = null;
                        mfrmParent.ShowInTaskbar = false;

                        this.Cursor = System.Windows.Forms.Cursors.Default;

                        mfrmParent.ShowDialog();
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
                sclsErrorsLog.WriteToErrorLog(ex.Message, ex.StackTrace, ex.Source);
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }

        private void SetVisualStyle()
        {

            switch (mintFormMode)
            {
                case sclsConstants.Form_Mode.INSERT_MODE:
                    btnApply.Text = "Enregistrer";
                    imgFormMode.Image = Ceritar.TT3LightDLL.Properties.Resources.Add;

                    break;

                case sclsConstants.Form_Mode.UPDATE_MODE:
                    btnApply.Text = "Appliquer";
                    imgFormMode.Image = Ceritar.TT3LightDLL.Properties.Resources.Update;

                    break;

                case sclsConstants.Form_Mode.CONSULT_MODE:
                    btnApply.Enabled = false;
                    btnCancel.Enabled = false;
                    btnQuit.Enabled = true;

                    btnApply.Text = "Appliquer";
                    imgFormMode.Image = Ceritar.TT3LightDLL.Properties.Resources.Consult;

                    break;

                case sclsConstants.Form_Mode.DELETE_MODE:
                    btnApply.Enabled = true;
                    btnCancel.Enabled = false;
                    btnQuit.Enabled = true;

                    btnApply.Text = "Supprimer";
                    imgFormMode.Image = Ceritar.TT3LightDLL.Properties.Resources.Delete;

                    break;
            }

            if ((mintFormMode == sclsConstants.Form_Mode.INSERT_MODE) || (mintFormMode == sclsConstants.Form_Mode.UPDATE_MODE))
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

        public void LoadFormData()
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

        ~ctlFormController()
        {
            this.Dispose();
        }

#endregion


        private void btnApply_Click(object sender, System.EventArgs e)
        {
            SaveDataEventArgs saveEvent = new SaveDataEventArgs();
            ValidateFormEventArgs validationEvent = new ValidateFormEventArgs();

            this.Cursor = Cursors.WaitCursor;

            if (ValidateForm != null)
                ValidateForm(validationEvent);

            if (validationEvent.IsValid)
            {

                if (SaveData != null)
                    SaveData(saveEvent);

                if (saveEvent.SaveSuccessful)
                {

                    ChangeMade = false;

                    switch (mintFormMode)
                    {
                        case sclsConstants.Form_Mode.INSERT_MODE:
                            FormMode = sclsConstants.Form_Mode.UPDATE_MODE;
                            LoadFormData();

                            break;

                        case sclsConstants.Form_Mode.UPDATE_MODE:
                            LoadFormData();

                            break;

                        case sclsConstants.Form_Mode.DELETE_MODE:
                            mfrmParent.Close();

                            break;
                    }
                }
                else
                {
                   Classes.clsApplication.GetAppController.ShowMessage((int) sclsConstants.Error_Message.ERROR_SAVE_MSG);
                }
            }

            this.Cursor = Cursors.Default;
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            Classes.clsApplication.GetAppController.EmptyAllFormControls(mfrmParent);
            LoadFormData();
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

        private void ctlFormControler_PropertyChanged(object sender, PropertyChangedEventArgs e) 
        {
            SetControlsVisility();
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

        public List<object> LstReceivedValues
        {
            get
            {
                return new List<object>();
            }
            set
            {
                _lstReceivedValues = value;
            }
        }
    }

#endregion

}