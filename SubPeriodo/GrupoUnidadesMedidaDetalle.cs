using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class GrupoUnidadesMedidaDetalle
    {
        private int ugpEntry;

        private int uomEntry;

        private decimal altQty;

        private decimal baseQty;

        private int logInstanc;

        private int lineNum;

        private int wghtFactor;

        private int udfFactor;

        private int userSign;

        public int UgpEntry { get => ugpEntry; set => ugpEntry = value; }
        public int UomEntry { get => uomEntry; set => uomEntry = value; }
        public decimal AltQty { get => altQty; set => altQty = value; }
        public decimal BaseQty { get => baseQty; set => baseQty = value; }
        public int LogInstanc { get => logInstanc; set => logInstanc = value; }
        public int LineNum { get => lineNum; set => lineNum = value; }
        public int WghtFactor { get => wghtFactor; set => wghtFactor = value; }
        public int UdfFactor { get => udfFactor; set => udfFactor = value; }
        public int UserSign { get => userSign; set => userSign = value; }
    }
}
