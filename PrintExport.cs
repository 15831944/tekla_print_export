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
            bool continuePrinting = true;

            if (UserControls._dwg == false && UserControls._pdf == false) continuePrinting = false;

            if (UserControls._prop) continuePrinting = CheckPropertys.main();
            if (UserControls._list) CreateList.main(drawing);

            if (continuePrinting)
            {
                TSD.DrawingHandler drawingHandler = new TSD.DrawingHandler();
                drawingHandler.SetActiveDrawing(drawing, true);

                if (UserControls._clouds) RemoveClouds.main();
                if (UserControls._pdf) ExportPDF.main(drawing);
                if (UserControls._dwg) ExportDWG.main(drawing);

                drawingHandler.CloseActiveDrawing(false);
            }
        }
    }
}
