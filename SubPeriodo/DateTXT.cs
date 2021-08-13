using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class DateTXT
    {
        private DateTime refDate;

        private int registre;

        public DateTime RefDate { get => refDate; set => refDate = value; }
        public int Registre { get => registre; set => registre = value; }

        public DateTXT(DateTime RefDate, int Registre)
        {
            this.RefDate = RefDate;

            this.Registre = Registre;
        }

        public DateTXT()
        {

        }


    }
}
