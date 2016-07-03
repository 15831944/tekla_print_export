﻿using System;
using System.Reflection;
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
    class CreateList
    {
        public static void main(TSD.Drawing drawing)
        {
            StringBuilder csv = new StringBuilder();

            string name = drawing.Title1;
            string nr = drawing.Name;

            int dmtDateSeconds = 0;
            string dmtDateString = "";
            drawing.GetUserProperty("DR_RESP_DSGNR_DATE", ref dmtDateSeconds);
            if (dmtDateSeconds != 0)
            {
                DateTime dmtDate = new DateTime(1970, 1, 1);
                dmtDate = dmtDate.AddSeconds(dmtDateSeconds);
                dmtDateString = dmtDate.ToShortDateString();
            }

            int revisionDateSeconds = 0;
            string revisionMark = "";
            string revisionDateString = "";
            DateLastMark(drawing, out revisionMark, out revisionDateSeconds);
            if (revisionDateSeconds != 0)
            {
                DateTime revisionDate = new DateTime(1970, 1, 1);
                revisionDate = revisionDate.AddSeconds(revisionDateSeconds);
                revisionDateString = revisionDate.ToShortDateString();
            }

            string newLine = string.Format("{0};{1};{2};{3};{4};", name, nr, revisionMark, dmtDateString, revisionDateString);
            csv.AppendLine(newLine);

            UserReport.addCSV(csv.ToString()) ;
        }

        public static void DateLastMark(TSD.Drawing croquis, out string revisionMark, out int revisionDateSeconds)
        {
            Type drawingType = croquis.GetType();
            PropertyInfo propertyInfo = drawingType.GetProperty("Identifier", BindingFlags.Instance | BindingFlags.NonPublic);
            object value = propertyInfo.GetValue(croquis, null);

            Identifier identifier = (Identifier)value;
            TSM.Beam fakeBeam = new TSM.Beam { Identifier = identifier };

            revisionMark = "";
            fakeBeam.GetReportProperty("REVISION.LAST_MARK", ref revisionMark);

            revisionDateSeconds = 0;
            fakeBeam.GetReportProperty("REVISION.LAST_DATE_CREATE", ref revisionDateSeconds);
        }
    }
}
