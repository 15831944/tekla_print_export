using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace tekla_print_export
{
    class UserSettings_PDF
    {
        public static List<PaperSize> _plotters;

        public UserSettings_PDF()
        {
            _plotters = new List<PaperSize>();
        }

        internal void loadDefaults()
        {
            PaperSize A4 = new PaperSize("PDF_A4", 210, 297);
            PaperSize A4b = new PaperSize("PDF_A4", 297, 210);
            PaperSize A3 = new PaperSize("PDF_A3", 420, 297);
            PaperSize A2 = new PaperSize("PDF_A2", 594, 420);
            PaperSize A1 = new PaperSize("PDF_A1", 841, 594);
            PaperSize A0 = new PaperSize("PDF_A0", 1189, 841);

            _plotters.Add(A4);
            _plotters.Add(A4b);
            _plotters.Add(A3);
            _plotters.Add(A2);
            _plotters.Add(A1);
            _plotters.Add(A0);
        }

        internal string getProperties()
        {
            StringBuilder txt = new StringBuilder();

            foreach (PaperSize prop in UserSettings_PDF._plotters)
            {
                txt.AppendLine("[PLOT] " + prop._plotterName + " = " + prop._width.ToString() + "x" + prop._height.ToString() );
            }

            return txt.ToString();
        }

        internal void setProperty(string setting)
        {
            Regex regex = new Regex(@"\[(.+)\]");
            Match match = regex.Match(setting);
            if (match.Success)
            {
                if (match.Value == "[PLOT]")
                {
                    string prop = setting.Replace("[PLOT]", "");
                    string[] props = prop.Split('=');
                    if (props.Count() == 2)
                    {
                        string[] size = props[1].Split('x');
                        if (size.Count() == 2)
                        {
                            double widthz = 0;
                            Double.TryParse(size[0], out widthz);
                            double heightz = 0;
                            Double.TryParse(size[1], out heightz);

                            if (widthz != 0 && heightz != 0)
                            {
                                PaperSize A = new PaperSize(props[0], widthz, heightz);
                                _plotters.Add(A);
                            }
                            
                        }
                    }
                }
            }
        }
    }
}
