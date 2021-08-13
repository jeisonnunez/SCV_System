using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class GrupoUnidadesMedidaCabecera
    {
        private int ugpEntry;

        private string oldUgpCode;

        private string ugpCode;

        private string ugpName;

        private int baseUom;

        private int oldBaseUom;

        private int userSign;

        private DateTime updateDate;

        private int logInstanc;

        private char locked;

        public int UgpEntry { get => ugpEntry; set => ugpEntry = value; }
        public string OldUgpCode { get => oldUgpCode; set => oldUgpCode = value; }
        public string UgpCode { get => ugpCode; set => ugpCode = value; }
        public string UgpName { get => ugpName; set => ugpName = value; }
        public int BaseUom { get => baseUom; set => baseUom = value; }
        public int UserSign { get => userSign; set => userSign = value; }
        public DateTime UpdateDate { get => updateDate; set => updateDate = value; }
        public int LogInstanc { get => logInstanc; set => logInstanc = value; }
        public char Locked { get => locked; set => locked = value; }
        public int OldBaseUom { get => oldBaseUom; set => oldBaseUom = value; }
    }
}
