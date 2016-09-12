using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace tekla_print_export
{
    class UserSettings_UDA
    {
        public static List<string> _GA_drawingProperties;

        public static List<string> _CU_drawingProperties;
        public static List<string> _CU_partProperties;

        public static List<string> _A_drawingProperties;
        public static List<string> _A_partProperties;

        public static List<string> _SP_drawingProperties;
        public static List<string> _SP_partProperties;

        public UserSettings_UDA()
        {
            _GA_drawingProperties = new List<string>();

            _CU_drawingProperties = new List<string>();
            _CU_partProperties = new List<string>();

            _A_drawingProperties = new List<string>();
            _A_partProperties = new List<string>();

            _SP_drawingProperties = new List<string>();
            _SP_partProperties = new List<string>();
        }

        internal void loadDefaults()
        {
            _GA_drawingProperties = new List<string>();

            _CU_drawingProperties = new List<string>();
            _CU_partProperties = new List<string>();

            _A_drawingProperties = new List<string>();
            _A_partProperties = new List<string>();

            _SP_drawingProperties = new List<string>();
            _SP_partProperties = new List<string>();

            _CU_partProperties.Add("DMT_EXPLOSURE");

            _GA_drawingProperties.Add("DR_RESP_DSGNR_DATE");
            _CU_drawingProperties.Add("DR_RESP_DSGNR_DATE");
            _A_drawingProperties.Add("DR_RESP_DSGNR_DATE");
            _SP_drawingProperties.Add("DR_RESP_DSGNR_DATE");
        }

        internal string getProperties()
        {
            StringBuilder txt = new StringBuilder();

            foreach (string prop in UserSettings_UDA._GA_drawingProperties)
            {
                txt.AppendLine("[GA_DRAWING] " + prop);
            }

            foreach (string prop in UserSettings_UDA._CU_drawingProperties)
            {
                txt.AppendLine("[CU_DRAWING] " + prop);
            }

            foreach (string prop in UserSettings_UDA._CU_partProperties)
            {
                txt.AppendLine("[CU_MAINPART] " + prop);
            }

            foreach (string prop in UserSettings_UDA._A_drawingProperties)
            {
                txt.AppendLine("[A_DRAWING] " + prop);
            }

            foreach (string prop in UserSettings_UDA._A_partProperties)
            {
                txt.AppendLine("[A_MAINPART] " + prop);
            }

            foreach (string prop in UserSettings_UDA._SP_drawingProperties)
            {
                txt.AppendLine("[SP_DRAWING] " + prop);
            }

            foreach (string prop in UserSettings_UDA._SP_partProperties)
            {
                txt.AppendLine("[SP_PART] " + prop);
            }

            return txt.ToString();
        }

        internal void setProperty(string setting)
        {
            Regex regex = new Regex(@"\[(.+)\]");
            Match match = regex.Match(setting);
            if (match.Success)
            {
                if (match.Value == "[GA_DRAWING]")
                {
                    string prop = setting.Replace("[GA_DRAWING]", "");
                    _GA_drawingProperties.Add(prop);
                }
                else if (match.Value == "[CU_DRAWING]")
                {
                    string prop = setting.Replace("[CU_DRAWING]", "");
                    _CU_drawingProperties.Add(prop);
                }
                else if (match.Value == "[CU_MAINPART]")
                {
                    string prop = setting.Replace("[CU_MAINPART]", "");
                    _CU_partProperties.Add(prop);
                }
                else if (match.Value == "[A_DRAWING]")
                {
                    string prop = setting.Replace("[A_DRAWING]", "");
                    _A_drawingProperties.Add(prop);
                }
                else if (match.Value == "[A_MAINPART]")
                {
                    string prop = setting.Replace("[A_MAINPART]", "");
                    _A_partProperties.Add(prop);
                }
                else if (match.Value == "[SP_DRAWING]")
                {
                    string prop = setting.Replace("[SP_DRAWING]", "");
                    _SP_drawingProperties.Add(prop);
                }
                else if (match.Value == "[SP_PART]")
                {
                    string prop = setting.Replace("[SP_PART]", "");
                    _SP_partProperties.Add(prop);
                }
            }
        }
    }
}
