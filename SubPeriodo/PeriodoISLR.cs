using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class PeriodoISLR
    {
        private string code;

        private int quantity;
        public string Code { get => code; set => code = value; }
        public int Quantity { get => quantity; set => quantity = value; }

        public PeriodoISLR(string Code, int Quantity)
        {
            this.Code = Code;

            this.Quantity = Quantity;
        }

        public PeriodoISLR()
        {

        }
    }
}
