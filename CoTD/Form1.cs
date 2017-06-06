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
        public static bool DEBUG = true;

        public static Regex COTDMatcher = new Regex("codeURL");

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

        public string getPageTextFromConfig(string keyName)
        {
            if(DEBUG)
            {
                if(keyName == "codeURL")
                {
                    return "asdfsadf >asdfasdf< asdfasdf";
                }
            }


            loadPage(config[keyName]);
            return webBrowser1.DocumentText;
        }



        public string getCoTD()
        {
            try
            {
                return COTDMatcher.Match(getPageTextFromConfig("codeURL")).Value.Substring(1, 8);
            }catch(Exception e) {
                MessageBox.Show("Unable to get the CoTD, Are you connected to DSCoTD?");
            }

            return null;
        }

        public void init()
        {
            string cotd = getCoTD();
       
            if (cotd != null && cotd.Length > 0)
            {
                label1.Text = "COTD: " + cotd;
                MessageBox.Show(cotd);

                loadPage(config["loginURL"]);
                
                //HtmlElementCollection inputObjects = webBrowser1.Document.GetElementsByTagName("user_password");
               // inputObjects[0].SetAttribute("",sss);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            init();
        }

        private void label1_Click(object sender, EventArgs e) {}
    }
}
