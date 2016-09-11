using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace tekla_print_export
{
    class UserSettings_GLOBAL
    {
        public static bool _drawingSave;
        public static bool _removeClouds;

        public UserSettings_GLOBAL()
        {
            loadDefaults();
        }

        internal void loadDefaults()
        {
            _drawingSave = false;
            _removeClouds = false;
        }

        internal string getProperties()
        {
            StringBuilder txt = new StringBuilder();

            txt.AppendLine("[GLOBAL] " + "drawingSave" + " = " + _drawingSave.ToString());
            txt.AppendLine("[GLOBAL] " + "removeClouds" + " = " + _removeClouds.ToString());

            return txt.ToString();
        }

        internal void setProperty(string setting)
        {
            Regex regex = new Regex(@"\[(.+)\]");
            Match match = regex.Match(setting);
            if (match.Success)
            {
                if (match.Value == "[GLOBAL]")
                {
                    string prop = setting.Replace("[GLOBAL]", "");
                    string[] props = prop.Split('=');
                    if (props.Count() == 2)
                    {
                        if (props[0] == "drawingSave") _drawingSave = getBool(props[0], props[1]);
                        else if (props[0] == "removeClouds") _removeClouds = getBool(props[0], props[1]);
                        else MainWindow._form.consoleOutput("[" + prop[0] + " NOT FOUND]", "L0");
                    }
                }
            }
        }

        private bool getBool(string setting, string val)
        {
            val = val.ToLower();

            bool result = false;
            bool parsing = bool.TryParse(val, out result);

            if (parsing)
            {
                
            }
            else
            {
                MainWindow._form.consoleOutput("[ERROR] " + setting + " unknown value, using False", "L0");
                result = false;
            }

            return result;
        }
    }
}
