using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoTD
{
    class Config
    {
        public static string KEY_EXIT_ON_LOGIN = "exitOnLogin";
        public static string KEY_CODE_URL_DEBUG = "debugCodeURL";
        public static string KEY_CODE_URL = "codeURL";
        public static string KEY_LOGIN_URL_DEBUG = "debugLoginURL";
        public static string KEY_LOGIN_URL = "loginURL";

        private static Dictionary<string, string> config = new Dictionary<string, string>();

        private static char SEPERATOR = '~';

        public static void loadConfig()
        {
            System.IO.StreamReader file = new System.IO.StreamReader("test.txt");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                string[] data = line.Split(SEPERATOR);
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
            if (hasKey(key))
            {
                return config[key];
            } else {
                return "";
            }
        }

        public static bool getBoolean(string key)
        {
            return Convert.ToBoolean(getString(key));
        }

        public static void put<T>(string key, T value)
        {
            if(hasKey(key))
            {
                config.Remove(key);
            }
            config.Add(key, value.ToString());
        }

        public static void saveConfig()
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter("test.txt");

            foreach(KeyValuePair<string, string> key in config)
            {
                file.WriteLine(key.Key + SEPERATOR + key.Value);
            }

            file.Flush();
            file.Close();
        }
    }
}
