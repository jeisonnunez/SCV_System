using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Meses
    {
        private string code;

        private string month;

        public string Code { get => code; set => code = value; }
        public string Month { get => month; set => month = value; }

        public Meses(string code, string mes)
        {
            this.Code = code;
            this.Month =mes;
        }

        public Meses()
        {

        }
    }
}
