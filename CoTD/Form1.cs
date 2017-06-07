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
        public static bool DEBUG = true;
        public static Regex COTDMatcher = new Regex(">[a-z]{8}<");

        public static WebBrowserDocumentCompletedEventHandler oldEventHandle = null;

        public static string COTD = "";

        public Form1()
        {
            InitializeComponent();
            Config.loadConfig();
            this.webBrowser1.Visible = false;
        }

        public void loadPage(string url)
        {
            webBrowser1.Navigate(url);
            webBrowser1.Refresh();
        }

        public void loadPageFromConfig(string keyName)
        {
            loadPage(Config.getString(keyName));
        }

        public void setDocumentLoadHandler(Action<object, WebBrowserDocumentCompletedEventArgs> func)
        {
            //Remove the old event listener
            if(oldEventHandle != null)
            { 
                webBrowser1.DocumentCompleted -= oldEventHandle;
            }
            oldEventHandle = new WebBrowserDocumentCompletedEventHandler(func);
            webBrowser1.DocumentCompleted += oldEventHandle;
        }

        public void getCoTD()
        {
            toolStripStatusLabel1.Text = "Grabbing CoTD";
            setDocumentLoadHandler(onCoTDPageLoad);
            loadPageFromConfig(DEBUG ? "debugCodeURL" : "codeURL"); 
        }

        public void onCoTDPageLoad(object sender, WebBrowserDocumentCompletedEventArgs asdf)
        {
            WebBrowser browser = sender as WebBrowser;

            try
            {
                COTD = COTDMatcher.Match(browser.DocumentText).Value.Substring(1, 8);

                if (COTD != null && COTD.Length > 0)
                {
                    label1.Text = "COTD: " + COTD;
                    toolStripStatusLabel1.Text = "Logging in";
                    setDocumentLoadHandler(onLoginPageLoaded);
                    loadPageFromConfig(DEBUG ? "debugLoginURL" : "loginURL");
                }


            } catch (Exception e) {
                toolStripStatusLabel1.Text = "Unable to get the CoTD, Are you connected to DSCoTD?";
            }
        }

        public void onLoginPageLoaded(object sender, WebBrowserDocumentCompletedEventArgs asdf)
        {
            try
            {
                HtmlElementCollection inputObjects = webBrowser1.Document.GetElementsByTagName("input");

                foreach (HtmlElement obj in inputObjects)
                {
                    if (obj.Name == "user_password")
                    {
                        obj.SetAttribute("value", COTD);
                    }
                }

                setDocumentLoadHandler(onLoginComplete);

                HtmlElement submit = webBrowser1.Document.GetElementById("fsloginButton");
                submit.InvokeMember("Click");
            } catch (Exception e) {
                toolStripStatusLabel1.Text = "Unable to login to CoTD";
            }
        }

        public void onLoginComplete(object sender, WebBrowserDocumentCompletedEventArgs asdf)
        {
            toolStripStatusLabel1.Text = "You should now be logged in";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            getCoTD();
        }

        private void label1_Click(object sender, EventArgs e) {}

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
