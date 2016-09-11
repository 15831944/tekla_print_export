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
    class CheckPropertys
    {
        public static bool main(TSD.Drawing drawing)
        {
            bool drawingStatus = true;

            TSM.Model _myModel = new TSM.Model();

            if (drawing is TSD.GADrawing)
            {
                drawingStatus = checkDrawing<TSD.GADrawing>(UserSettings_UDA._GA_drawingProperties, drawing as TSD.GADrawing);
            }
            else if (drawing is TSD.CastUnitDrawing)
            {
                TSD.CastUnitDrawing cu = drawing as TSD.CastUnitDrawing;
                TSM.Assembly currentAssembly = _myModel.SelectModelObject(cu.CastUnitIdentifier) as TSM.Assembly;
                TSM.Part currentMainPart = currentAssembly.GetMainPart() as TSM.Part;

                drawingStatus = checkDrawing<TSD.CastUnitDrawing>(UserSettings_UDA._CU_drawingProperties, cu);
                if (drawingStatus) drawingStatus = checkPart(UserSettings_UDA._CU_partProperties, currentMainPart, cu);
            }
            else if (drawing is TSD.AssemblyDrawing)
            {
                TSD.AssemblyDrawing asd = drawing as TSD.AssemblyDrawing;
                TSM.Assembly currentAssembly = _myModel.SelectModelObject(asd.AssemblyIdentifier) as TSM.Assembly;
                TSM.Part currentMainPart = currentAssembly.GetMainPart() as TSM.Part;

                drawingStatus = checkDrawing<TSD.AssemblyDrawing>(UserSettings_UDA._A_drawingProperties, asd);
                if (drawingStatus) drawingStatus = checkPart(UserSettings_UDA._A_partProperties, currentMainPart, asd);
            }
            else if (drawing is TSD.SinglePartDrawing)
            {
                TSD.SinglePartDrawing sp = drawing as TSD.SinglePartDrawing;
                TSM.Part currentPart = _myModel.SelectModelObject(sp.PartIdentifier) as TSM.Part;

                drawingStatus = checkDrawing<TSD.SinglePartDrawing>(UserSettings_UDA._SP_drawingProperties, sp);
                if (drawingStatus) drawingStatus = checkPart(UserSettings_UDA._SP_partProperties, currentPart, sp);
            }

            return drawingStatus;
        }

        public static bool checkDrawing<T>(List<string> properties, T drawing) where T : TSD.Drawing
        {
            foreach (string prop in properties)
            {
                bool result = checkProperty(prop, drawing);

                if (result == false) return false;
            }

            return true;
        }

        public static bool checkPart(List<string> properties, TSM.Part part, TSD.Drawing drawing)
        {
            foreach (string prop in properties)
            {
                bool result = checkProperty(prop, part, drawing);

                if (result == false) return false;
            }

            return true;
        }

        public static bool checkProperty<T>(string prop, T drawing) where T : TSD.Drawing
        {
            string temp = "dummy";
            drawing.GetUserProperty(prop, ref temp);

            if (temp != "dummy")
            {
                return true;
            }

            int tempInt = -987;
            drawing.GetUserProperty(prop, ref temp);

            if (tempInt != -987)
            {
                return true;
            }

            if (temp == "dummy" && tempInt == -987)
            {
                MainWindow._form.consoleOutput(drawing.Mark + " " + prop + " is not set", "L2");
            }

            return false;
        }

        public static bool checkProperty(string prop, TSM.Part part, TSD.Drawing drawing)
        {
            string temp = "dummy";
            part.GetUserProperty(prop, ref temp);

            if (temp != "dummy")
            {
                return true;
            }

            int tempInt = -987;
            part.GetUserProperty(prop, ref temp);

            if (tempInt != -987)
            {
                return true;
            }

            if (temp == "dummy" && tempInt == -987)
            {
                MainWindow._form.consoleOutput(drawing.Mark + " " + prop + " is not set", "L2");
            }

            return false;
        }
    }
}
