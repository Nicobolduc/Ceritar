using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ceritar.TT3LightDLL.Controls
{
    public partial class ctlRefresh : UserControl
    {
        //Private members
        private bool _toRefresh;
        private bool mblnChangeImage;

        public delegate void ClickEventHandler();
        public new event ClickEventHandler Click;


        public bool SetToRefresh
        {
            set
            {
                _toRefresh = value;

                if (_toRefresh)
                {
                    mblnChangeImage = true;

                    tmrBlink.Start();
                }
                else
                {
                    tmrBlink.Stop();
                }
            }
        }

        public ctlRefresh()
        {
            InitializeComponent();

            mblnChangeImage = false;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            tmrBlink.Stop();

            //btnRefresh.Enabled = false;

            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

            Click();

            this.Cursor = System.Windows.Forms.Cursors.Default;
        }

        private void tmrBlink_Tick(object sender, EventArgs e)
        {
            if (mblnChangeImage)
            {
                btnRefresh.Enabled = false;
                mblnChangeImage = false;
            }
            else
            {
                btnRefresh.Enabled = true;
                mblnChangeImage = true;
            }
            
        }
    }
}
