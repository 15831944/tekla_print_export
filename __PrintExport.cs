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
            bool printingStatus = true;
            bool openDrawing = true;

            if (UserControls._list)                                         CreateList.main(drawing);
            if (UserControls._prop)                                         printingStatus = CheckPropertys.main(drawing);
            if (UserControls._dwg == false && UserControls._pdf == false)   openDrawing = false;

            if (printingStatus == false)
            {
                MainWindow._form.consoleOutput("[ERROR] UDA(s) not set, printing canceled!", "L2");
                throw new DivideByZeroException();
            }

            if (openDrawing)
            {
                TSD.DrawingHandler drawingHandler = new TSD.DrawingHandler();
                drawingHandler.SetActiveDrawing(drawing, true);

                if (UserControls._clouds) RemoveClouds.main();

                try
                {
                    if (UserControls._pdf) ExportPDF.main(drawing);
                }
                catch
                {
                    MainWindow._form.consoleOutput("[ERROR] Failed printing!", "L2");
                    printingStatus = false;
                }

                try
                {
                    if (UserControls._dwg) ExportDWG.main(drawing);
                }
                catch
                {
                    MainWindow._form.consoleOutput("[ERROR] Failed exporting!", "L2");
                    printingStatus = false;
                }

                drawingHandler.CloseActiveDrawing(UserSettings._drawingSave);
            }

            if (printingStatus == false)
            {

                throw new DivideByZeroException();
            }

        }
    }
}
