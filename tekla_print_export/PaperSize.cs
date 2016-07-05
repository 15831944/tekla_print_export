using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tekla_print_export
{
    class PaperSize
    {
        public string _plotterName;
        public double _width;
        public double _height;

        public PaperSize(string name, double width, double height)
        {
            _plotterName = name;
            _width = width;
            _height = height;
        }
    }
}
