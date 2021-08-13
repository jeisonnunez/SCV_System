using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Entidades
{
    public class fechaActual
    {
        static public DateTime? GetFechaActual()
        {
            return DateTime.Now;

           
        }

        static public int GetMesActual()
        {
            return DateTime.Now.Month;
           
            
        }

        static public int GetYearActual()
        {
            return DateTime.Now.Year;
        }


    }
}
