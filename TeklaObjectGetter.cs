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
    class TeklaObjectGetter
    {
        public static List<TSD.Drawing> getSelectedDrawings()
        {
            List<TSD.Drawing> parsed = new List<TSD.Drawing>();

            TSD.DrawingHandler myDrawingHandler = new TSD.DrawingHandler();
            TSD.DrawingEnumerator selectionEnum = myDrawingHandler.GetDrawingSelector().GetSelected();

            foreach (TSD.Drawing current in selectionEnum)
            {
                if (current is TSD.Drawing)
                {
                    parsed.Add(current as TSD.Drawing);
                }
            }

            return parsed;
        }
    }
}
