using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace CoTD
{
    public partial class Form1 : Form
    {
        public static Dictionary<string, string> config = new Dictionary<string, string>();

        public Form1()
        {
            InitializeComponent();
            loadConfig();
        }

        public void loadConfig()
        {
            System.IO.StreamReader file = new System.IO.StreamReader("test.txt");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                string[] data = line.Split('=');
                config.Add(data[0], data[1]);
            }

            file.Close();
        }

        public void loadPage(string url)
        {
            webBrowser1.Url = new Uri(url);
            webBrowser1.Refresh();
        }

        public void init()
        {
            // Suspend the screen.
            webBrowser1.Url = new Uri(config["CodeURL"]);
            webBrowser1.Refresh();
            string s = webBrowser1.DocumentText;
       
            if (s.Length > 0)
            {
                

                string sss = new Regex(">[a-z]{8}<").Match(s).Value.Substring(1, 8);

                MessageBox.Show(sss);
                webBrowser1.Url = new Uri(config["loginURL"]);
                HtmlElementCollection inputObjects = webBrowser1.Document.GetElementsByTagName("user_password");
                inputObjects[0].SetAttribute("",sss);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            init();
        }
    }
}
