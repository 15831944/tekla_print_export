using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tekla_print_export
{
    static class Debuger
    {
        public static void p(string txt)
        {
            MainWindow._form.consoleOutput("DEBUG: " + txt, "L2");
        }
    }
}
