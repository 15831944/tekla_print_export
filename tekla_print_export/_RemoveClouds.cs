using System;
using System.Collections;
using System.Diagnostics;
using System.IO;

using Tekla.Structures;
using TSM = Tekla.Structures.Model;
using TSD = Tekla.Structures.Drawing;
using T3D = Tekla.Structures.Geometry3d;
using Tekla.Structures.Model.Operations;

namespace tekla_print_export
{
    class RemoveClouds
    {
        public static void main()
        {
            Operation.RunMacro(@"..\drawings\RemoveChangeClouds.cs");
        }
    }
}
