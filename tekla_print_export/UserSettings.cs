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
        public static bool _drawingSave;
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
            _drawingSave = false;
            _UDA = new UserSettings_UDA();
            _UDA.loadDefaults();
            _DWG = new UserSettings_DWG();
            _PDF = new UserSettings_PDF();
            _PDF.loadDefaults();
        }

        private void printCurrentSettings()
        {
            MainWindow._form.consoleOutput("[GLOBAL] " + "drawingSave" + " = " + _drawingSave.ToString() + "\n", "L0");
            MainWindow._form.consoleOutput( _UDA.getProperties() , "L0");
            MainWindow._form.consoleOutput( _DWG.getProperties() , "L0");
            MainWindow._form.consoleOutput( _PDF.getProperties() , "L0" );
        }

        private void parseSettingsFile(string[] readText)
        {
            _drawingSave = false;
            _UDA = new UserSettings_UDA();
            _DWG = new UserSettings_DWG();
            _PDF = new UserSettings_PDF();

            foreach (string setting in readText)
            {
                string parsed = setting.Replace(" ", "");
                setGlobalProperty(parsed);
                _UDA.setProperty(parsed);
                _DWG.setProperty(parsed);
                _PDF.setProperty(parsed);
            }

        }

        private void setGlobalProperty(string setting)
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
                        if (props[0] == "drawingSave") _drawingSave = Boolean.Parse(props[1]);
                    }
                }
            }
        }

        internal void writeSettingsFile()
        {
            StringBuilder txt = new StringBuilder();

            txt.AppendLine("[GLOBAL] " + "drawingSave" + " = " + "false");
            txt.Append(_UDA.getProperties());
            txt.Append(_DWG.getProperties());
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
