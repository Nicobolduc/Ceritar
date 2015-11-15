using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ceritar.Logirack_CVS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new mdiGeneral());
        }
    }
}
