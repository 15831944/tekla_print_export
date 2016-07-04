using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace tekla_print_export
{
    class UserReport
    {
        static int _success = 0;
        static int _total = 0;
        public static string _csv = "";

        public UserReport(int count)
        {
            _success = 0;
            _total = count;
        }

        internal void success()
        {
            _success++;
        }

        internal string results()
        {
            int fail = _total - _success;
            string result = @"Success: " + _success.ToString() + @" / Failed: " + fail.ToString();

            if (UserControls._list)
            {
                makeCSV();
            }

            return result;
        }

        public static void addCSV(string txt)
        {
            _csv = _csv + txt;
        }

        private void makeCSV()
        {
            string fileName = "printed.csv";

            try
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
            }
            catch
            {
                MainWindow._form.consoleOutput("[Failed to remove existing CSV]", "L0");
            }

            try
            {
                File.AppendAllText(fileName, _csv);
                Process.Start(fileName);
            }
            catch
            {
                MainWindow._form.consoleOutput("[CSV write failed]", "L0");
            }
        }
    }
}
