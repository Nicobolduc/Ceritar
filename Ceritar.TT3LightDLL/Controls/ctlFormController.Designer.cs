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
            ((System.ComponentModel.ISupportInitialize)(this.imgFormMode)).BeginInit();
            this.SuspendLayout();
            // 
            // btnQuit
            // 
            this.btnQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnQuit.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuit.Location = new System.Drawing.Point(246, 5);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(75, 25);
            this.btnQuit.TabIndex = 22;
            this.btnQuit.Text = "Fermer";
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Enabled = false;
            this.btnApply.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApply.Location = new System.Drawing.Point(77, 5);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(82, 25);
            this.btnApply.TabIndex = 21;
            this.btnApply.Text = "Enregistrer";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Enabled = false;
            this.btnCancel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(165, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 23;
            this.btnCancel.Text = "Annuler";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // imgFormMode
            // 
            this.imgFormMode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.imgFormMode.Image = global::Ceritar.TT3LightDLL.Properties.Resources.AddItem;
            this.imgFormMode.Location = new System.Drawing.Point(5, 0);
            this.imgFormMode.Name = "imgFormMode";
            this.imgFormMode.Size = new System.Drawing.Size(36, 33);
            this.imgFormMode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgFormMode.TabIndex = 24;
            this.imgFormMode.TabStop = false;
            // 
            // ctlFormController
            // 
            this.Controls.Add(this.imgFormMode);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.btnApply);
            this.Name = "ctlFormController";
            this.Size = new System.Drawing.Size(324, 34);
            this.LocationChanged += new System.EventHandler(this.ctlFormControler_LocationChanged);
            this.Move += new System.EventHandler(this.ctlFormControler_Move);
            ((System.ComponentModel.ISupportInitialize)(this.imgFormMode)).EndInit();
            this.ResumeLayout(false);

        }
        internal System.Windows.Forms.Button btnQuit;
        internal System.Windows.Forms.Button btnApply;
        internal System.Windows.Forms.Button btnCancel;


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

        private System.Windows.Forms.PictureBox imgFormMode;
    }
}
