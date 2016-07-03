using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tekla_print_export
{
    class UserControls
    {
        private static bool _running = true;

        public static bool _prop;
        public static bool _clouds;
        public static bool _pdf;
        public static bool _dwg;
        public static bool _list;

        public static void setControls(bool prop, bool clouds, bool pdf, bool dwg, bool list)
        {
            _prop = prop;
            _clouds = clouds;
            _pdf = pdf;
            _dwg = dwg;
            _list = list;
        }

        public static bool Running
        {
            get
            {
                return _running;
            }
        }

        public static void toggleOnOff()
        {
            _running = !_running;
        }
    }
}
