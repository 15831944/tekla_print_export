using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


using Tekla.Structures;
using TSM = Tekla.Structures.Model;
using TSD = Tekla.Structures.Drawing;
using T3D = Tekla.Structures.Geometry3d;

namespace tekla_print_export
{
    class MainLoop
    {
        private List<TSD.Drawing> _collection;

        public MainLoop(List<TSD.Drawing> source)
        {
            _collection = source;
        }

        internal void main()
        {
            try
            {
                drawingClosedLoop();
                drawingOpenLoop();

                if (UserControls._list) _UserReport.makeCSV();
            }
            catch
            {
                MainWindow._form.consoleOutput("[ERROR - 2]", "L1");
            }

            
            MainWindow._form.consoleOutput("[Done] " + DateTime.Now.ToString("h:mm:ss"), "L0");
        }

        private void drawingClosedLoop()
        {
            bool globalCheck = true;
            _UserReport.newCSV();

            int i = 1;
            int fail = 0;
            int tot = _collection.Count;

            foreach (TSD.Drawing current in _collection)
            {
                if (!UserControls.Running) { MainWindow._form.consoleOutput("[KILLED]", "L0"); break; }
                MainWindow._form.consoleOutput("Checking " + i.ToString() + " out of " + tot.ToString(), "L1");

                if (UserControls._list)
                {
                    _UserReport.main(current);
                }

                if (UserControls._prop)
                {
                    bool localCheck = CheckPropertys.main(current);
                    if (localCheck == false) { globalCheck = false; fail++; }
                }

                i++;
            }

            if (globalCheck == false)
            {
                MainWindow._form.consoleOutput("[FAILED] UDA-s not set in " + fail.ToString() + " drawings, canceling printing!", "L1");
                throw new DivideByZeroException();
            }

            MainWindow._form.consoleOutput("[OK] Check finished", "L0");
        }

        private void drawingOpenLoop()
        {
            if (UserControls._pdf || UserControls._dwg || UserSettings_GLOBAL._removeClouds)
            {
                int i = 1;
                int ok = 0;
                int tot = _collection.Count;

                foreach (TSD.Drawing current in _collection)
                {
                    if (!UserControls.Running) { MainWindow._form.consoleOutput("[KILLED]", "L0"); break; }

                    MainWindow._form.consoleOutput("Printing/Exporting " + i.ToString() + " out of " + tot.ToString(), "L1");

                    try
                    {
                        PrintExport.main(current);
                        ok++;
                    }
                    catch
                    {
                        MainWindow._form.consoleOutput("[ERROR - 3]", "L2");
                    }

                    i++;
                }

                MainWindow._form.consoleOutput("[OK] PRINT/EXPORT Success: " + ok.ToString() + @" / Failed: " + (tot - ok).ToString(), "L0");
            }

        }
    }
}
