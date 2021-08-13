using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Controlador_Inicio
{
    public class Base
    {

        public static NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
        public static void SetNumericConfiguration()
        {
            nfi.NumberGroupSeparator = ".";
            nfi.NumberDecimalSeparator = ",";
        }

    }
}
