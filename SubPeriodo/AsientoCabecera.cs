using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class AsientoCabecera
    {
        private int transId;

        private string ref1;

        private string ref2;

        private int transType;

        private DateTime? refDate;

        private string memo;       

        private decimal locTotal;

        private decimal fcTotal;

        private decimal sysTotal;

        private DateTime? dueDate;

        private DateTime? taxDate;

        private int finncPriod;

        private DateTime? updateDate;

        private int userSign;

        private int baseRef;

        private string transCurr;

        public int TransId { get => transId; set => transId = value; }
        public int TransType { get => transType; set => transType = value; }
        public DateTime? RefDate { get => refDate; set => refDate = value; }
        public string Memo { get => memo; set => memo = value; }       
        public decimal LocTotal { get => locTotal; set => locTotal = value; }
        public decimal FcTotal { get => fcTotal; set => fcTotal = value; }
        public decimal SysTotal { get => sysTotal; set => sysTotal = value; }
        public DateTime? DueDate { get => dueDate; set => dueDate = value; }
        public DateTime? TaxDate { get => taxDate; set => taxDate = value; }
        public int FinncPriod { get => finncPriod; set => finncPriod = value; }
        public DateTime? UpdateDate { get => updateDate; set => updateDate = value; }
        public int UserSign { get => userSign; set => userSign = value; }           
        public string Ref1 { get => ref1; set => ref1 = value; }
        public string Ref2 { get => ref2; set => ref2 = value; }
        public int BaseRef { get => baseRef; set => baseRef = value; }
        public string TransCurr { get => transCurr; set => transCurr = value; }
    }

    
}
