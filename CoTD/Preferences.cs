using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoTD
{
    public partial class Preferences : Form
    {
        public Preferences()
        {
            InitializeComponent();
            FormClosing += new FormClosingEventHandler(onFormQuit);

            checkBox1.Checked = Config.getBoolean(Config.KEY_EXIT_ON_LOGIN);
            checkBox2.Checked = Config.getBoolean(Config.KEY_DEBUG);
        }

        public void onFormQuit(object sender, FormClosingEventArgs asdf)
        {
            Config.put<Boolean>(Config.KEY_EXIT_ON_LOGIN, checkBox1.Checked);
            Config.put<Boolean>(Config.KEY_DEBUG, checkBox2.Checked);
            Config.saveConfig();
        }
    }
}
