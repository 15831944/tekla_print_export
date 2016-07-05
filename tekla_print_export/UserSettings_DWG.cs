using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace tekla_print_export
{
    class UserSettings_DWG
    {
        public static string textFileName;
        public static string optMnuFileType;
        public static string chkButRevisionMark;

        public static string optMnuLayerFile;
        public static string chkUseAdvancedLineTypeConversio;
        public static string txtLineTypeMappingFile;
        public static string chkButIncludeEmptyLayers;
        public static string chkButObjectColorByLayer;

        public static string chkUseGrouping;
        public static string chkUseLineCliping;
        public static string chkSplitSoftLines;
        public static string chkUsePaperSpace;

        public UserSettings_DWG()
        {
            loadDefaults();
        }

        internal void loadDefaults()
        {
            textFileName = ""; //Name
            optMnuFileType = "1"; //Type DWG
            chkButRevisionMark = "0"; //Include Revision Mark

            optMnuLayerFile = "standard";  //Layer rules
            chkUseAdvancedLineTypeConversio = "0"; //Use advanced line type and layer conversion
            txtLineTypeMappingFile = "LineTypeMapping.xml"; //Conversion type
            chkButIncludeEmptyLayers = "0"; //Include empty layer
            chkButObjectColorByLayer = "0"; //Object color by layer

            chkUseGrouping = "1"; //Export objects as groups
            chkUseLineCliping = "0"; //Cut lines with text
            chkSplitSoftLines = "0"; //Export custom lines as split lines
            chkUsePaperSpace = "0"; //Use paper space
        }

        internal string getProperties()
        {
            StringBuilder txt = new StringBuilder();

            txt.AppendLine("[DWG] " + "textFileName" + " = " + textFileName);
            txt.AppendLine("[DWG] " + "optMnuFileType" + " = " + optMnuFileType);
            txt.AppendLine("[DWG] " + "chkButRevisionMark" + " = " + chkButRevisionMark);

            txt.AppendLine("[DWG] " + "optMnuLayerFile" + " = " + optMnuLayerFile);
            txt.AppendLine("[DWG] " + "chkUseAdvancedLineTypeConversio" + " = " + chkUseAdvancedLineTypeConversio);
            txt.AppendLine("[DWG] " + "txtLineTypeMappingFile" + " = " + txtLineTypeMappingFile);
            txt.AppendLine("[DWG] " + "chkButIncludeEmptyLayers" + " = " + chkButIncludeEmptyLayers);
            txt.AppendLine("[DWG] " + "chkButObjectColorByLayer" + " = " + chkButObjectColorByLayer);

            txt.AppendLine("[DWG] " + "chkUseGrouping" + " = " + chkUseGrouping);
            txt.AppendLine("[DWG] " + "chkUseLineCliping" + " = " + chkUseLineCliping);
            txt.AppendLine("[DWG] " + "chkSplitSoftLines" + " = " + chkSplitSoftLines);
            txt.AppendLine("[DWG] " + "chkUsePaperSpace" + " = " + chkUsePaperSpace);

            return txt.ToString();
        }

        internal void setProperty(string setting)
        {
            Regex regex = new Regex(@"\[(.+)\]");
            Match match = regex.Match(setting);
            if (match.Success)
            {
                if (match.Value == "[DWG]")
                {
                    string prop = setting.Replace("[DWG]", "");
                    string[] props = prop.Split('=');
                    if (props.Count() == 2)
                    {
                        if (props[0] == "textFileName") textFileName = props[1];
                        else if (props[0] == "optMnuFileType") optMnuFileType = props[1];
                        else if (props[0] == "chkButRevisionMark") chkButRevisionMark = props[1];
                        else if (props[0] == "optMnuLayerFile") optMnuLayerFile = props[1];
                        else if (props[0] == "chkUseAdvancedLineTypeConversio") chkUseAdvancedLineTypeConversio = props[1];
                        else if (props[0] == "txtLineTypeMappingFile") txtLineTypeMappingFile = props[1];
                        else if (props[0] == "chkButIncludeEmptyLayers") chkButIncludeEmptyLayers = props[1];
                        else if (props[0] == "chkButObjectColorByLayer") chkButObjectColorByLayer = props[1];
                        else if (props[0] == "chkUseGrouping") chkUseGrouping = props[1];
                        else if (props[0] == "chkUseLineCliping") chkUseLineCliping = props[1];
                        else if (props[0] == "chkSplitSoftLines") chkSplitSoftLines = props[1];
                        else if (props[0] == "chkUsePaperSpace") chkUsePaperSpace = props[1];
                    }
                }
            }
        }
    }
}
