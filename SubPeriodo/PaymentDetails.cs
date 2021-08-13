using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class PaymentDetails
    {
        private int docNum;

        private int invoiceId;

        private int docEntry;

        private decimal sumApplied;

        private decimal appliedFC;

        private decimal appliedSys;

        private string invType;

        private decimal docRate;

        private int DocLine;

        private decimal vatApplied;

        private decimal vatAppldFC;

        private decimal vatAppldSy;

        private char selfInv;

        private string objType;

        private decimal bfDcntSum;

        private decimal bfDcntSumF;

        private decimal bfDcntSumS;

        private decimal bfNetDcnt;

        private decimal bfNetDcntF;

        private decimal bfNetDcntS;

        private decimal paidSum;

        private decimal wtAppld;

        private decimal wtAppldFC;

        private decimal wtAppldSC;

        private decimal wtInvCatS;

        private decimal wtInvCatSF;

        private decimal wtInvCatSS;

        private decimal docTransId;

        private int baseAbs;

        private string docSubType;

        private string u_IDA_CompIVA;

        private DateTime? u_IDA_FechaComp;

        private decimal u_IDA_MontoCompIVA;

        private string u_IDA_CompISLR;

        private DateTime? u_IDA_DateCompISLR;

        private int line_ID;

        public int DocNum { get => docNum; set => docNum = value; }
        public int InvoiceId { get => invoiceId; set => invoiceId = value; }
        public int DocEntry { get => docEntry; set => docEntry = value; }
        public decimal SumApplied { get => sumApplied; set => sumApplied = value; }
        public decimal AppliedFC { get => appliedFC; set => appliedFC = value; }
        public decimal AppliedSys { get => appliedSys; set => appliedSys = value; }
        public string InvType { get => invType; set => invType = value; }
        public decimal DocRate { get => docRate; set => docRate = value; }
        public int DocLine1 { get => DocLine; set => DocLine = value; }
        public decimal VatApplied { get => vatApplied; set => vatApplied = value; }
        public decimal VatAppldFC { get => vatAppldFC; set => vatAppldFC = value; }
        public decimal VatAppldSy { get => vatAppldSy; set => vatAppldSy = value; }
        public char SelfInv { get => selfInv; set => selfInv = value; }
        public string ObjType { get => objType; set => objType = value; }
        public decimal BfDcntSum { get => bfDcntSum; set => bfDcntSum = value; }
        public decimal BfDcntSumF { get => bfDcntSumF; set => bfDcntSumF = value; }
        public decimal BfDcntSumS { get => bfDcntSumS; set => bfDcntSumS = value; }
        public decimal BfNetDcnt { get => bfNetDcnt; set => bfNetDcnt = value; }
        public decimal BfNetDcntF { get => bfNetDcntF; set => bfNetDcntF = value; }
        public decimal BfNetDcntS { get => bfNetDcntS; set => bfNetDcntS = value; }
        public decimal PaidSum { get => paidSum; set => paidSum = value; }
        public decimal WtAppld { get => wtAppld; set => wtAppld = value; }
        public decimal WtAppldFC { get => wtAppldFC; set => wtAppldFC = value; }
        public decimal WtAppldSC { get => wtAppldSC; set => wtAppldSC = value; }
        public decimal WtInvCatS { get => wtInvCatS; set => wtInvCatS = value; }
        public decimal WtInvCatSF { get => wtInvCatSF; set => wtInvCatSF = value; }
        public decimal WtInvCatSS { get => wtInvCatSS; set => wtInvCatSS = value; }
        public decimal DocTransId { get => docTransId; set => docTransId = value; }
        public int BaseAbs { get => baseAbs; set => baseAbs = value; }
        public string DocSubType { get => docSubType; set => docSubType = value; }
        public string U_IDA_CompIVA { get => u_IDA_CompIVA; set => u_IDA_CompIVA = value; }
        public DateTime? U_IDA_FechaComp { get => u_IDA_FechaComp; set => u_IDA_FechaComp = value; }
        public decimal U_IDA_MontoCompIVA { get => u_IDA_MontoCompIVA; set => u_IDA_MontoCompIVA = value; }
        public string U_IDA_CompISLR { get => u_IDA_CompISLR; set => u_IDA_CompISLR = value; }
        public DateTime? U_IDA_DateCompISLR { get => u_IDA_DateCompISLR; set => u_IDA_DateCompISLR = value; }
        public int Line_ID { get => line_ID; set => line_ID = value; }
    }
}
