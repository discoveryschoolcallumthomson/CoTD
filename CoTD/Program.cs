using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoTD
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Config.loadConfig();
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(onExit);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        static void onExit(object sender, EventArgs asdf)
        {
            Config.saveConfig();
        }
    }
}
