using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class DocumentoCabecera
    {
       
        private int docEntry;

        private int docNum;

        private char docType;

        private char canceled;

        private char docStatus;

        private char invntSttus;

        private DateTime? docDate;

        private DateTime? docDueDate;

        private string cardCode;

        private string cardName;

        private string address;

        private string numAtCard;

        private decimal vatSum;

        private decimal vatSumFC;

        private string docCurr;

        private decimal docTotal;

        private decimal docTotalFC;

        private decimal paidToDate;

        private decimal paidFC;

        private string ref1;

        private string comments;

        private string jrnlMemo;

        private int transId;

        private decimal sysRate;

        private decimal docRate;

        private decimal VatSumSy;

        private decimal docTotalSy;

        private decimal paidSys;

        private DateTime? updateDate;

        private DateTime? taxDate;

        private int finncPriod;

        private int userSign;

        private decimal vatPaid;

        private decimal vatPaidFC;

        private decimal vatPaidSys;

        private string licTradNum;

        private decimal wTSum;

        private decimal wTSumFC;

        private decimal wTSumSC;

        private decimal wTApplied;

        private decimal wTAppliedF;

        private decimal wTAppliedS;

        private string ctlAccount;

        private decimal baseVtAt;

        private decimal baseVtAtSC;

        private decimal baseVtAtFC;

        private decimal nnSbVAt;

        private decimal nnSbVAtSC;

        private decimal nbSbVAtFC;

        private decimal paidSum;

        private decimal paidSumFc;

        private decimal paidSumSc;

        private string numControl;

        private string tipoTrans;

        private string compIVA;

        private DateTime? fechaComp;

        private DateTime? fechaContComp;

        private decimal montoCompIVA;

        private string retenTercero;

        private string tipoRet;

        private string tercero;

        private string compISLR;

        private DateTime? dateCompISLR;

        private DateTime? dateContISLR;

        private string docSubType;

        private string objType;

        private string address2;

        private decimal nnSbAmnt;

        private decimal nnSbAmntSC;

        private decimal NbSbAmntFC;

        private decimal baseAmnt;

        private decimal baseAmntSC;

        private decimal baseAmntFC;

        private decimal max1099;

        private string fafe;        

        public int DocEntry { get => docEntry; set => docEntry = value; }
        public int DocNum { get => docNum; set => docNum = value; }
        public char DocType { get => docType; set => docType = value; }
        public char Canceled { get => canceled; set => canceled = value; }
        public char DocStatus { get => docStatus; set => docStatus = value; }
        public char InvntSttus { get => invntSttus; set => invntSttus = value; }
        public DateTime? DocDate { get => docDate; set => docDate = value; }
        public DateTime? DocDueDate { get => docDueDate; set => docDueDate = value; }
        public string CardCode { get => cardCode; set => cardCode = value; }
        public string CardName { get => cardName; set => cardName = value; }
        public string Address { get => address; set => address = value; }
        public string NumAtCard { get => numAtCard; set => numAtCard = value; }
        public decimal VatSum { get => vatSum; set => vatSum = value; }
        public decimal VatSumFC { get => vatSumFC; set => vatSumFC = value; }
        public string DocCurr { get => docCurr; set => docCurr = value; }
        public decimal DocTotal { get => docTotal; set => docTotal = value; }
        public decimal DocTotalFC { get => docTotalFC; set => docTotalFC = value; }
        public decimal PaidToDate { get => paidToDate; set => paidToDate = value; }
        public decimal PaidFC { get => paidFC; set => paidFC = value; }
        public string Ref1 { get => ref1; set => ref1 = value; }
        public string Comments { get => comments; set => comments = value; }
        public string JrnlMemo { get => jrnlMemo; set => jrnlMemo = value; }
        public int TransId { get => transId; set => transId = value; }
        public decimal SysRate { get => sysRate; set => sysRate = value; }
        public decimal VatSumSy1 { get => VatSumSy; set => VatSumSy = value; }
        public decimal DocTotalSy { get => docTotalSy; set => docTotalSy = value; }
        public decimal PaidSys { get => paidSys; set => paidSys = value; }
        public DateTime? UpdateDate { get => updateDate; set => updateDate = value; }
        public DateTime? TaxDate { get => taxDate; set => taxDate = value; }
        public int FinncPriod { get => finncPriod; set => finncPriod = value; }
        public int UserSign { get => userSign; set => userSign = value; }
        public decimal VatPaid { get => vatPaid; set => vatPaid = value; }
        public decimal VatPaidFC { get => vatPaidFC; set => vatPaidFC = value; }
        public decimal VatPaidSys { get => vatPaidSys; set => vatPaidSys = value; }
        public string LicTradNum { get => licTradNum; set => licTradNum = value; }
        public decimal WTSum { get => wTSum; set => wTSum = value; }
        public decimal WTSumFC { get => wTSumFC; set => wTSumFC = value; }
        public decimal WTSumSC { get => wTSumSC; set => wTSumSC = value; }
        public decimal WTApplied { get => wTApplied; set => wTApplied = value; }
        public decimal WTAppliedF { get => wTAppliedF; set => wTAppliedF = value; }
        public decimal WTAppliedS { get => wTAppliedS; set => wTAppliedS = value; }
        public string CtlAccount { get => ctlAccount; set => ctlAccount = value; }
        public decimal BaseVtAt { get => baseVtAt; set => baseVtAt = value; }
        public decimal BaseVtAtSC { get => baseVtAtSC; set => baseVtAtSC = value; }
        public decimal BaseVtAtFC { get => baseVtAtFC; set => baseVtAtFC = value; }
        public decimal NnSbVAt { get => nnSbVAt; set => nnSbVAt = value; }
        public decimal NnSbVAtSC { get => nnSbVAtSC; set => nnSbVAtSC = value; }
        public decimal NbSbVAtFC { get => nbSbVAtFC; set => nbSbVAtFC = value; }
        public decimal PaidSum { get => paidSum; set => paidSum = value; }
        public decimal PaidSumFc { get => paidSumFc; set => paidSumFc = value; }
        public decimal PaidSumSc { get => paidSumSc; set => paidSumSc = value; }
        public string NumControl { get => numControl; set => numControl = value; }
        public string TipoTrans { get => tipoTrans; set => tipoTrans = value; }
        public string CompIVA { get => compIVA; set => compIVA = value; }
        public DateTime? FechaComp { get => fechaComp; set => fechaComp = value; }
        public DateTime? FechaContComp { get => fechaContComp; set => fechaContComp = value; }
        public decimal MontoCompIVA { get => montoCompIVA; set => montoCompIVA = value; }
        public string RetenTercero { get => retenTercero; set => retenTercero = value; }
        public string TipoRet { get => tipoRet; set => tipoRet = value; }
        public string Tercero { get => tercero; set => tercero = value; }
        public string CompISLR { get => compISLR; set => compISLR = value; }
        public DateTime? DateCompISLR { get => dateCompISLR; set => dateCompISLR = value; }
        public DateTime? DateContISLR { get => dateContISLR; set => dateContISLR = value; }
        public string DocSubType { get => docSubType; set => docSubType = value; }
        public string ObjType { get => objType; set => objType = value; }
        public string Address2 { get => address2; set => address2 = value; }
        public decimal DocRate { get => docRate; set => docRate = value; }
        public decimal NnSbAmnt { get => nnSbAmnt; set => nnSbAmnt = value; }
        public decimal NnSbAmntSC { get => nnSbAmntSC; set => nnSbAmntSC = value; }
        public decimal NbSbAmntFC1 { get => NbSbAmntFC; set => NbSbAmntFC = value; }
        public decimal BaseAmnt { get => baseAmnt; set => baseAmnt = value; }
        public decimal BaseAmntSC { get => baseAmntSC; set => baseAmntSC = value; }
        public decimal BaseAmntFC { get => baseAmntFC; set => baseAmntFC = value; }
        public decimal Max1099 { get => max1099; set => max1099 = value; }
        public string Fafe { get => fafe; set => fafe = value; }
    }
}
