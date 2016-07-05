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
            bool UDAstatus = true;

            foreach (string prop in UserSettings_UDA._drawingProperties)
            {
                string temp = "dummy";
                drawing.GetUserProperty(prop, ref temp);

                if (temp == "dummy")
                {
                    MainWindow._form.consoleOutput(drawing.Mark + prop + " is not set", "L2");
                    UDAstatus = false;
                }
            }

            foreach (string prop in UserSettings_UDA._drawingPropertiesInt)
            {
                int temp = 0;
                drawing.GetUserProperty(prop, ref temp);

                if (temp == 0)
                {
                    MainWindow._form.consoleOutput(drawing.Mark + " " + prop + " is not set", "L2");
                    UDAstatus = false;
                }
            }

            if (drawing is TSD.CastUnitDrawing)
            {
                TSM.Model _myModel = new TSM.Model();
                TSD.CastUnitDrawing cu = drawing as TSD.CastUnitDrawing;
                var currentModelObject = _myModel.SelectModelObject(cu.CastUnitIdentifier);
                TSM.Assembly currentAssembly = currentModelObject as TSM.Assembly;
                TSM.Part currentMainPart = currentAssembly.GetMainPart() as TSM.Part;

                foreach (string prop in UserSettings_UDA._partProperties)
                {
                    string temp = "dummy";
                    currentMainPart.GetUserProperty(prop, ref temp);

                    if (temp == "dummy")
                    {
                        MainWindow._form.consoleOutput(drawing.Mark + " " + prop + " is not set", "L2");
                        UDAstatus = false;
                    }
                }
            }

            return UDAstatus;
        }
    }
}
