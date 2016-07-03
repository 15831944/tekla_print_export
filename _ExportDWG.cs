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
    class ExportDWG
    {
        public static void main(TSD.Drawing drawing)
        {
            double scaleFactor = getHighestScale(drawing);
            double lineTypeScale = scaleFactor;

            if (scaleFactor == 0)
            {
                throw new DivideByZeroException();
            }

            //new TeklaMacroBuilder.MacroBuilder()
            //    .Callback("acmdDisplayExportDrawingsDialog", "", "main_frame")

            //    .ValueChange("diaExportDrawings", "textFileName", UserSettings.textFileName) //Name
            //    .ValueChange("diaExportDrawings", "optMnuFileType", UserSettings.optMnuFileType) //Type DWG
            //    .ValueChange("diaExportDrawings", "chkButRevisionMark", UserSettings.chkButRevisionMark) //Include Revision Mark

            //    //.TabChange("diaExportDrawings", "tabWndProperties", "tabLayerOptions")
            //    .ValueChange("diaExportDrawings", "optMnuLayerFile", UserSettings.optMnuLayerFile)  //Layer rules
            //    .ValueChange("diaExportDrawings", "chkUseAdvancedLineTypeConversio", UserSettings.chkUseAdvancedLineTypeConversio) //Use advanced line type and layer conversion
            //    .ValueChange("diaExportDrawings", "txtLineTypeMappingFile", UserSettings.txtLineTypeMappingFile) //Conversion type
            //    .ValueChange("diaExportDrawings", "chkButIncludeEmptyLayers", UserSettings.chkButIncludeEmptyLayers) //Include empty layer
            //    .ValueChange("diaExportDrawings", "chkButObjectColorByLayer", UserSettings.chkButObjectColorByLayer) //Object color by layer

            //    //.TabChange("diaExportDrawings", "tabWndProperties", "tabOptions")
            //    .ValueChange("diaExportDrawings", "textScaleFactor", scaleFactor.ToString()) //Drawing scale
            //    .ValueChange("diaExportDrawings", "txtLineTypeScale", lineTypeScale.ToString()) //Line type scale
            //    .ValueChange("diaExportDrawings", "chkUseGrouping", UserSettings.chkUseGrouping) //Export objects as groups
            //    .ValueChange("diaExportDrawings", "chkUseLineCliping", UserSettings.chkUseLineCliping) //Cut lines with text
            //    .ValueChange("diaExportDrawings", "chkSplitSoftLines", UserSettings.chkSplitSoftLines) //Export custom lines as split lines
            //    .ValueChange("diaExportDrawings", "chkUsePaperSpace", UserSettings.chkUsePaperSpace) //Use paper space

            //    .PushButton("butExport", "diaExportDrawings")
            //    .PushButton("butCancel", "diaExportDrawings")
            //    .Run();
        }

        public static double getHighestScale(TSD.Drawing currentDrawing)
        {
            double highestScale = 0;

            TSD.DrawingObjectEnumerator ViewEnum = currentDrawing.GetSheet().GetViews();

            foreach (TSD.View currentView in ViewEnum)
            {
                if (isView2D(currentView))
                {
                    double currentScale = currentView.Attributes.Scale;
                    highestScale = Math.Max(currentScale, highestScale);
                }
            }

            MainWindow._form.consoleOutputL2("Scale: " + highestScale.ToString());

            return highestScale;
        }

        private static bool isView2D(TSD.View currentView)
        {
            T3D.CoordinateSystem disp = currentView.DisplayCoordinateSystem as T3D.CoordinateSystem;
            T3D.CoordinateSystem viewp = currentView.ViewCoordinateSystem as T3D.CoordinateSystem;

            if (disp.AxisX.Z != viewp.AxisX.Z || disp.AxisY.Z != viewp.AxisY.Z)
            {
                return false;
            }

            return true;
        }
    }
}
