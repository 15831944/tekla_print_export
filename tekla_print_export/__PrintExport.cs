using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tekla.Structures;
using TSM = Tekla.Structures.Model;
using TSD = Tekla.Structures.Drawing;
using T3D = Tekla.Structures.Geometry3d;

namespace tekla_print_export
{
    static class PrintExport
    {
        public static void main(TSD.Drawing drawing)
        {
            TSD.DrawingHandler drawingHandler = new TSD.DrawingHandler();
            drawingHandler.SetActiveDrawing(drawing, true);

            bool printingStatus = true;

            if (UserSettings_GLOBAL._removeClouds)
            {
                RemoveClouds.main();
            }

            if (UserControls._pdf)
            {
                try
                {
                    ExportPDF.main(drawing);
                }
                catch
                {
                    printingStatus = false;
                    MainWindow._form.consoleOutput("[ERROR - 4] Failed printing!", "L2");
                }
            }

            if (UserControls._dwg)
            {
                try
                {
                    ExportDWG.main(drawing);
                }
                catch
                {
                    printingStatus = false;
                    MainWindow._form.consoleOutput("[ERROR - 5] Failed exporting!", "L2");
                }
            }

            drawingHandler.CloseActiveDrawing(UserSettings_GLOBAL._drawingSave);

            if (printingStatus == false)
            {
                throw new DivideByZeroException();
            }

        }
    }
}
