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
        UserReport _stats;

        public MainLoop(List<TSD.Drawing> source)
        {
            _collection = source;
            _stats = new UserReport(_collection.Count);
        }

        internal void main()
        {
            try
            {
                drawingClosedLoop();
                drawingOpenLoop();
            }
            catch
            {
                MainWindow._form.consoleOutput("[ERROR] - 2", "L1");
            }


            MainWindow._form.consoleOutput(_stats.results(), "L0");
            MainWindow._form.consoleOutput("[Done] " + DateTime.Now.ToString("h:mm:ss"), "L0");
        }

        private void drawingClosedLoop()
        {
            bool globalCheckOK = true;

            int i = 1;
            int tot = _collection.Count;

            foreach (TSD.Drawing current in _collection)
            {
                if (!UserControls.Running) { MainWindow._form.consoleOutput("[KILLED]", "L0"); break; }

                MainWindow._form.consoleOutput("Checking " + i.ToString() + " out of " + tot.ToString(), "L1");
                CheckReport.main(current, ref globalCheckOK);

                i++;
            }

            if (globalCheckOK == false)
            {
                MainWindow._form.consoleOutput("[ERROR] UDA-s not set, canceling printing!", "L1");
                throw new DivideByZeroException();
            }
        }

        private void drawingOpenLoop()
        {
            int i = 1;
            int tot = _collection.Count;

            foreach (TSD.Drawing current in _collection)
            {
                if (!UserControls.Running) { MainWindow._form.consoleOutput("[KILLED]", "L0"); break; }

                if (UserControls._pdf || UserControls._dwg || UserSettings_GLOBAL._removeClouds)
                {
                    MainWindow._form.consoleOutput("Printing/Exporting " + i.ToString() + " out of " + tot.ToString(), "L1");

                    try
                    {
                        PrintExport.main(current);
                        _stats.success();
                    }
                    catch
                    {
                        MainWindow._form.consoleOutput("[ERROR] - 3", "L2");
                    }
                }

                i++;
            }
        }


    }
}
