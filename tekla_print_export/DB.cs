using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tekla_print_export
{
    static class DB
    {
        public static void pp(string txt)
        {
            MainWindow._form.consoleOutput("### DEBUG: " + txt, "L0");
        }
    }
}
