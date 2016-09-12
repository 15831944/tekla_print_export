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
    class CheckReport
    {
        public static void main (TSD.Drawing drawing, ref bool globalCheckOK)
        {
            bool localCheckOK = true;





            if (localCheckOK == false)
            {
                globalCheckOK = false;
            }
        }

    }
}
