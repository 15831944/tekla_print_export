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
                .ValueChange("Plot", "add_revision_info_to_filename", UserSettings_PDF.add_revision_info_to_filename) //Include Revision Mark
                .ListSelect("Plot", "component_list", printerName)
                .PushButton("butPrint", "Plot")
                .PushButton("cancel_pb", "Plot")
                .Run();
        }

        public static string selectPrinter(TSD.Drawing drawing)
        {
            string plotter = "NOT FOUND";

            int height = Convert.ToInt32(drawing.Layout.SheetSize.Height);
            int width = Convert.ToInt32(drawing.Layout.SheetSize.Width);

            foreach (PaperSize current in UserSettings_PDF._plotters)
            {
                if ((width % current._width == 0) && height == current._height)
                    plotter = current._plotterName;
            }

            MainWindow._form.consoleOutput("PRINT - " + plotter + " ("+ width.ToString() + "x" + height.ToString() + ")", "L2");
            return plotter;
        }
    }
}

