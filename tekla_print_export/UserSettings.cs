using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Diagnostics;

using System.IO;

namespace tekla_print_export
{
    class UserSettings
    {
        //public static bool _drawingSave;
        public static UserSettings_GLOBAL _GLOBAL;
        public static UserSettings_UDA _UDA;
        public static UserSettings_DWG _DWG;
        public static UserSettings_PDF _PDF;
        private string _name;

        public UserSettings(string name)
        {
            _name = name;
            resetSettings();
        }

        internal void resetSettings()
        {
            if (File.Exists(_name))
            {
                try
                {
                    MainWindow._form.consoleOutput("Loading settings from file:", "L0");
                    string[] readText = File.ReadAllLines(_name);
                    parseSettingsFile(readText);
                }
                catch
                {
                    MainWindow._form.consoleOutput("[ERROR] Problem parsing data, loading default settings:", "L0");
                    loadDefaultSettings();
               }
            }
            else
            {
                MainWindow._form.consoleOutput("[ERROR] Settings file not found, loading default settings:", "L0");
                loadDefaultSettings();
            }

            printCurrentSettings();
        }

        private void loadDefaultSettings()
        {
            _GLOBAL = new UserSettings_GLOBAL();
            _UDA = new UserSettings_UDA();
            _DWG = new UserSettings_DWG();
            _PDF = new UserSettings_PDF();

            _UDA.loadDefaults();
            _PDF.loadDefaults();
        }

        private void printCurrentSettings()
        {
            MainWindow._form.consoleOutput( _GLOBAL.getProperties(), "L0");
            MainWindow._form.consoleOutput( _UDA.getProperties() , "L0");
            MainWindow._form.consoleOutput( _DWG.getProperties() , "L0");
            MainWindow._form.consoleOutput( _PDF.getProperties() , "L0" );
        }

        private void parseSettingsFile(string[] readText)
        {
            _GLOBAL = new UserSettings_GLOBAL();
            _UDA = new UserSettings_UDA();
            _DWG = new UserSettings_DWG();
            _PDF = new UserSettings_PDF();

            foreach (string setting in readText)
            {
                if (setting.StartsWith("###"))
                {
                    continue;
                }
                else
                {
                    string parsed = setting.Replace(" ", "");
                    _GLOBAL.setProperty(parsed);
                    _UDA.setProperty(parsed);
                    _DWG.setProperty(parsed);
                    _PDF.setProperty(parsed);
                }
            }
        }

        internal void writeSettingsFile()
        {
            StringBuilder txt = new StringBuilder();
            txt.AppendLine("### [GLOBAL]");
            txt.Append(_GLOBAL.getProperties());
            txt.AppendLine("### [GA_DRAWING], [CU_DRAWING], [CU_MAINPART], [A_DRAWING], [A_MAINPART], [SP_DRAWING], [SP_PART]");
            txt.Append(_UDA.getProperties());
            txt.AppendLine("### [DWG]");
            txt.Append(_DWG.getProperties());
            txt.AppendLine("### [PDF]");
            txt.Append(_PDF.getProperties());

            string settings = txt.ToString();

            try
            {
                File.AppendAllText(_name, settings);
            }
            catch
            {
                MainWindow._form.consoleOutput("[Failed to write settings file]", "L0");
            }
        }

        internal void openSettingsFile()
        {
            if (File.Exists(_name))
            {
                try
                {
                    Process.Start(_name);
                }
                catch
                {
                    MainWindow._form.consoleOutput("[Could not open settings file]", "L0");
                }
            }
            else
            {
                MainWindow._form.consoleOutput("[Settings file not found, making new one]", "L0");

                writeSettingsFile();
            }
        }

        internal void deleteSettingsFile()
        {
            if (File.Exists(_name))
            {
                try
                {
                    File.Delete(_name);
                    MainWindow._form.consoleOutput("[Settings file deleted]", "L0");
                }
                catch
                {
                    MainWindow._form.consoleOutput("[Failed to delete settings file]", "L0");
                }
            }
            else
            {
                MainWindow._form.consoleOutput("[Settings file not found]", "L0");
            }
        }

    }
}

