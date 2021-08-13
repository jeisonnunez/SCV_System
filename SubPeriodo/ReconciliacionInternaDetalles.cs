using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ReconciliacionInternaDetalles
    {
        private int reconNum;

        private int lineSeq;

        private string shortName;

        private int transId;

        private int transRowId;

        private string srcObjTyp;

        private int srcObjAbs;

        private decimal reconSum;

        private decimal reconSumFC;

        private decimal reconSumSC;

        private string frgnCurr;

        private decimal sumMthCurr;

        private char isCredit;

        private string account;

        private decimal cashDisSum;

        private decimal wTSum;

        private decimal wTSumFC;

        private decimal wTSumSC;

        private decimal expSum;

        private decimal expSumFC;

        private decimal expSumSC;

        public int ReconNum { get => reconNum; set => reconNum = value; }
        public int LineSeq { get => lineSeq; set => lineSeq = value; }
        public string ShortName { get => shortName; set => shortName = value; }
        public int TransId { get => transId; set => transId = value; }
        public int TransRowId { get => transRowId; set => transRowId = value; }
        public string SrcObjTyp { get => srcObjTyp; set => srcObjTyp = value; }
        public int SrcObjAbs { get => srcObjAbs; set => srcObjAbs = value; }
        public decimal ReconSum { get => reconSum; set => reconSum = value; }
        public decimal ReconSumFC { get => reconSumFC; set => reconSumFC = value; }
        public decimal ReconSumSC { get => reconSumSC; set => reconSumSC = value; }
        public string FrgnCurr { get => frgnCurr; set => frgnCurr = value; }
        public decimal SumMthCurr { get => sumMthCurr; set => sumMthCurr = value; }
        public char IsCredit { get => isCredit; set => isCredit = value; }
        public string Account { get => account; set => account = value; }
        public decimal CashDisSum { get => cashDisSum; set => cashDisSum = value; }
        public decimal WTSum { get => wTSum; set => wTSum = value; }
        public decimal WTSumFC { get => wTSumFC; set => wTSumFC = value; }
        public decimal WTSumSC { get => wTSumSC; set => wTSumSC = value; }
        public decimal ExpSum { get => expSum; set => expSum = value; }
        public decimal ExpSumFC { get => expSumFC; set => expSumFC = value; }
        public decimal ExpSumSC { get => expSumSC; set => expSumSC = value; }
    }
}
