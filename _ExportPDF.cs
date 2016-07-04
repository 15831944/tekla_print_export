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
    class ExportPDF
    {
        public static void main(TSD.Drawing drawing)
        {
            string printerName = selectPrinter(drawing);

            if (printerName == "NOT FOUND")
            {
                throw new DivideByZeroException();
            }

            new TeklaMacroBuilder.MacroBuilder()
                .Callback("acmd_display_plot_dialog", "", "main_frame")
                .ListSelect("Plot", "component_list", printerName)
                .PushButton("butPrint", "Plot")
                .PushButton("cancel_pb", "Plot")
                .Run();
        }

        public static string selectPrinter(TSD.Drawing drawing)
        {
            string paper = "NOT FOUND";

            int height = Convert.ToInt32(drawing.Layout.SheetSize.Height);
            int width = Convert.ToInt32(drawing.Layout.SheetSize.Width);

            if (width == 210 && height == 297)
                paper = "PDF_A4";
            else if ((width % 420 == 0) && height == 297)
                paper = "PDF_A3";
            else if ((width % 594 == 0) && height == 420)
                paper = "PDF_A2";
            else if ((width % 841 == 0) && height == 594)
                paper = "PDF_A1";
            else if ((width % 1189 == 0) && height == 841)
                paper = "PDF_A0";

            MainWindow._form.consoleOutput("PRINT - " + paper + " ("+ width.ToString() + "x" + height.ToString() + ")", "L2");
            return paper;
        }
    }
}

