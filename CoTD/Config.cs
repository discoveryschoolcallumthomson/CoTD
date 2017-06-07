using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoTD
{
    class Config
    {
        private static Dictionary<string, string> config = new Dictionary<string, string>();

        public static void loadConfig()
        {
            System.IO.StreamReader file = new System.IO.StreamReader("test.txt");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                string[] data = line.Split('~');
                config.Add(data[0], data[1]);
            }

            file.Close();
        }

        public static bool hasKey(string key)
        {
            return config.ContainsKey(key);
        }

        public static string getString(string key)
        {
            return config[key];
        }
    }
}
