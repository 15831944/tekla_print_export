using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using System.IO;

namespace tekla_print_export
{
    class UserSettings
    {
        public static bool _drawingSave;
        public static List<string> _drawingProperties = new List<string>();
        public static List<string> _drawingPropertiesInt = new List<string>();
        public static List<string> _partProperties = new List<string>();
        public static List<string> _partPropertiesInt = new List<string>();
        private string _name;

        public UserSettings(string name)
        {
            _name = name;
            readFile();
        }

        private void readFile()
        {
            if (File.Exists(_name))
            {
                try
                {
                    string[] readText = File.ReadAllLines(_name);
                    parseSettingsFile(readText);
                }
                catch
                {
                    MainWindow._form.consoleOutput("[ERROR] Problem parsing data, loading default settings\n", "L0");
                    loadDefaultSettings();
               }
            }
            else
            {
                MainWindow._form.consoleOutput("[ERROR] Settings file not found, loading default settings\n", "L0");
                loadDefaultSettings();
            }

            printCurrentSettings();
        }

        private void loadDefaultSettings()
        {
            _drawingSave = false;
            _partProperties.Add("DMT_EXPLOSURE");
            _partProperties.Add("DMT_SURFACE_MODEL");
            _drawingPropertiesInt.Add("DR_RESP_DSGNR_DATE");
        }

        private void printCurrentSettings()
        {
            MainWindow._form.consoleOutput("[GLOBAL]" + "drawingSave=" + "false", "L0");

            foreach (string prop in _drawingProperties)
            {
                MainWindow._form.consoleOutput("[DRAWING_UDA_TEXT]" + prop, "L0");
            }
            foreach (string prop in _drawingPropertiesInt)
            {
                MainWindow._form.consoleOutput("[DRAWING_UDA_NUMBER]" + prop, "L0");
            }
            foreach (string prop in _partProperties)
            {
                MainWindow._form.consoleOutput("[MAINPART_UDA_TEXT]" + prop, "L0");
            }
            foreach (string prop in _partPropertiesInt)
            {
                MainWindow._form.consoleOutput("[MAINPART_UDA_NUMBER]" + prop, "L0");
            }
        }

        private void parseSettingsFile(string[] readText)
        {

        }

        internal void writeSettingsFile() //
        {
            StringBuilder txt = new StringBuilder();

            txt.AppendLine("[GLOBAL]" + "drawingSave=" + "false");

            foreach (string prop in _drawingProperties)
            {
                txt.AppendLine("[DRAWING_UDA_TEXT]" + prop);
            }
            foreach (string prop in _drawingPropertiesInt)
            {
                txt.AppendLine("[DRAWING_UDA_NUMBER]" + prop);
            }
            foreach (string prop in _partProperties)
            {
                txt.AppendLine("[MAINPART_UDA_TEXT]" + prop);
            }
            foreach (string prop in _partPropertiesInt)
            {
                txt.AppendLine("[MAINPART_UDA_NUMBER]" + prop);
            }

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
