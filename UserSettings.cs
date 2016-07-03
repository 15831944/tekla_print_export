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
                    MainWindow._form.consoleOutput("[Error parsing data, loading default settings]]");
                }
            }
            else
            {
                MainWindow._form.consoleOutput("[Settings file not found, loading default settings]");
            }
        }

        private void parseSettingsFile(string[] readText)
        {

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
                    MainWindow._form.consoleOutput("[Could not open settings file]");
                }
            }
            else
            {
                MainWindow._form.consoleOutput("[Settings file not found, making new one]");

                //GGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGg
                string[] values = { " ", " ", " ", " " };
                File.WriteAllLines(_name, values);
            }
        }

        internal void deleteSettingsFile()
        {
            if (File.Exists(_name))
            {
                try
                {
                    File.Delete(_name);
                    MainWindow._form.consoleOutput("[Settings file deleted]");
                }
                catch
                {
                    MainWindow._form.consoleOutput("[Failed to delete settings file]");
                }
            }
            else
            {
                MainWindow._form.consoleOutput("[Settings file not found]");
            }
        }

    }
}
