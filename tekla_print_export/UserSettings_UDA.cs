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
        public static List<string> _drawingProperties;
        public static List<string> _drawingPropertiesInt;
        public static List<string> _partProperties;
        public static List<string> _partPropertiesInt;

        public UserSettings_UDA()
        {
            _drawingProperties = new List<string>();
            _drawingPropertiesInt = new List<string>();
            _partProperties = new List<string>();
            _partPropertiesInt = new List<string>();
        }

        internal void loadDefaults()
        {
            _drawingProperties = new List<string>();
            _drawingPropertiesInt = new List<string>();
            _partProperties = new List<string>();
            _partPropertiesInt = new List<string>();

            _partProperties.Add("DMT_EXPLOSURE");
            _partProperties.Add("DMT_SURFACE_MODEL");
            _drawingPropertiesInt.Add("DR_RESP_DSGNR_DATE");
        }

        internal string getProperties()
        {
            StringBuilder txt = new StringBuilder();

            foreach (string prop in UserSettings_UDA._drawingProperties)
            {
                txt.AppendLine("[DRAWING_UDA_TEXT] " + prop);
            }
            foreach (string prop in UserSettings_UDA._drawingPropertiesInt)
            {
                txt.AppendLine("[DRAWING_UDA_NUMBER] " + prop);
            }
            foreach (string prop in UserSettings_UDA._partProperties)
            {
                txt.AppendLine("[MAINPART_UDA_TEXT] " + prop);
            }
            foreach (string prop in UserSettings_UDA._partPropertiesInt)
            {
                txt.AppendLine("[MAINPART_UDA_NUMBER] " + prop);
            }

            return txt.ToString();
        }

        internal void setProperty(string setting)
        {
            Regex regex = new Regex(@"\[(.+)\]");
            Match match = regex.Match(setting);
            if (match.Success)
            {
                if (match.Value == "[DRAWING_UDA_TEXT]")
                {
                    string prop = setting.Replace("[DRAWING_UDA_TEXT]", "");
                    _drawingProperties.Add(prop);
                }
                else if (match.Value == "[DRAWING_UDA_NUMBER]")
                {
                    string prop = setting.Replace("[DRAWING_UDA_NUMBER]", "");
                    _drawingProperties.Add(prop);
                }
                else if (match.Value == "[MAINPART_UDA_TEXT]")
                {
                    string prop = setting.Replace("[MAINPART_UDA_TEXT]", "");
                    _drawingProperties.Add(prop);
                }
                else if (match.Value == "[MAINPART_UDA_NUMBER]")
                {
                    string prop = setting.Replace("[MAINPART_UDA_NUMBER]", "");
                    _drawingProperties.Add(prop);
                }
            }
        }
    }
}
