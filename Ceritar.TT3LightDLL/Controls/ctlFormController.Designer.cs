using System.ComponentModel;

namespace Ceritar.TT3LightDLL.Controls
{
    partial class ctlFormController
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnQuit = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.imgFormMode = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)this.imgFormMode).BeginInit();
            this.SuspendLayout();
            //
            //btnQuit
            //
            this.btnQuit.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            this.btnQuit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnQuit.Location = new System.Drawing.Point(246, 5);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(75, 23);
            this.btnQuit.TabIndex = 22;
            this.btnQuit.Text = "Fermer";
            this.btnQuit.UseVisualStyleBackColor = true;
            //
            //btnApply
            //
            this.btnApply.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            this.btnApply.Enabled = false;
            this.btnApply.Location = new System.Drawing.Point(84, 5);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 21;
            this.btnApply.Text = "Enregistrer";
            this.btnApply.UseVisualStyleBackColor = true;
            //
            //btnCancel
            //
            this.btnCancel.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(165, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 23;
            this.btnCancel.Text = "Annuler";
            this.btnCancel.UseVisualStyleBackColor = true;
            //
            //imgFormMode
            //
            this.imgFormMode.Image = Ceritar.TT3LightDLL.Properties.Resources.Add; 
            this.imgFormMode.Location = new System.Drawing.Point(5, 0);
            this.imgFormMode.Name = "imgFormMode";
            this.imgFormMode.Size = new System.Drawing.Size(36, 33);
            this.imgFormMode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgFormMode.TabIndex = 24;
            this.imgFormMode.TabStop = false;
            //
            //ctlFormControler
            //
            this.Controls.Add(this.imgFormMode);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.btnApply);
            this.Name = "ctlFormControler";
            this.Size = new System.Drawing.Size(324, 33);
            ((System.ComponentModel.ISupportInitialize)this.imgFormMode).EndInit();
            this.ResumeLayout(false);

            //INSTANT C# NOTE: Converted design-time event handler wireups:
            btnApply.Click += new System.EventHandler(btnApply_Click);
            btnCancel.Click += new System.EventHandler(btnCancel_Click);
            btnQuit.Click += new System.EventHandler(btnQuit_Click);
            this.LocationChanged += new System.EventHandler(ctlFormControler_LocationChanged);
            this.Move += new System.EventHandler(ctlFormControler_Move);
            //this.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ctlFormControler_PropertyChanged);
        }
        internal System.Windows.Forms.Button btnQuit;
        internal System.Windows.Forms.Button btnApply;
        internal System.Windows.Forms.Button btnCancel;
        internal System.Windows.Forms.PictureBox imgFormMode;


        private bool EventsSubscribed = false;
        private void SubscribeToEvents()
        {
            if (EventsSubscribed)
                return;
            else
                EventsSubscribed = true;

            //mfrmParent.ResizeBegin += mfrmParent_ResizeBegin;
            //mfrmParent.ResizeEnd += mfrmParent_ResizeEnd;
        }

        #endregion
    }
}
