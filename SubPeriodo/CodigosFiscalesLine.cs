using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class CodigosFiscalesLine
    {
        private string sTCCode;

        private int line_ID;

        private string sTACode;

        private decimal efctivRate;

        public string STCCode { get => sTCCode; set => sTCCode = value; }
        public int Line_ID { get => line_ID; set => line_ID = value; }
        public string STACode { get => sTACode; set => sTACode = value; }
        public decimal EfctivRate { get => efctivRate; set => efctivRate = value; }
    }
}
