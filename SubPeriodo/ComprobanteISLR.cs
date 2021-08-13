using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ComprobanteISLR
    {
        private string u_IDA_NroComp;

        public string U_IDA_NroComp { get => u_IDA_NroComp; set => u_IDA_NroComp = value; }

        public ComprobanteISLR(string U_IDA_NroComp)
        {
            this.U_IDA_NroComp = U_IDA_NroComp;
        }
    }
}
