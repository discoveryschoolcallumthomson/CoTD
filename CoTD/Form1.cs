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
        public static List<string> alpha = new List<string>();
        public Form1()
        {
            InitializeComponent();
        }

        public void init()
        {
            int counter = 0;
            string line;

            // Read the file and display it line by line.
            System.IO.StreamReader file =
               new System.IO.StreamReader("test.txt");
            while ((line = file.ReadLine()) != null)
            {
                alpha.Add(line);
                counter++;
            }

            file.Close();

            // Suspend the screen.
            Console.ReadLine();
            webBrowser1.Url = new Uri(alpha[0]);
            webBrowser1.Refresh();
            string s = webBrowser1.DocumentText;
       
            if (s.Length > 0)
            {
                

                string sss = new Regex(">[a-z]{8}<").Match(s).Value.Substring(1, 8);

                MessageBox.Show(sss);
                webBrowser1.Url = new Uri(alpha[1]);
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
