using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Payment
    {
        private int docEntry;

        private int docNum;

        private char docType;

        private char canceled;

        private DateTime? docDate;

        private DateTime? docDueDate;

        private string cardCode;

        private string cardName;

        private string address;

        private string cashAcct;

        private decimal cashSum;

        private decimal cashSumFC;

        private decimal cashSumSy;

        private decimal creditSum;

        private decimal credSumFC;

        private decimal credSumSy;

        private string checkAcct;

        private decimal checkSum;

        private decimal checkSumFC;

        private decimal checkSumSy;

        private string trsfrAcct;

        private decimal trsfrSum;

        private decimal trsfrSumFC;

        private decimal trsfrSumSy;

        private DateTime? trsfrDate;

        private string trsfrRef;

        private char payNoDoc;

        private decimal noDocSum;

        private decimal noDocSumFC;

        private decimal noDocSumSy;

        private char diffCurr;

        private decimal docTotal;

        private decimal docTotalFC;

        private string ref1;

        private string counterRef;

        private decimal docTotalSy;

        private decimal docRate;

        private decimal sysRate;

        private string objType;

        private string comments;

        private string jrnlMemo;

        private int transId;

        private int cntctCode;

        private DateTime? updateDate;

        private DateTime? taxDate;

        private char dataSource;

        private int userSign;

        private string vatGroup;

        private decimal vatSum;

        private decimal vatSumFC;

        private decimal vatSumSy;

        private int finncPriod;

        private decimal vatPrcnt;

        private string wtCode;

        private decimal wtSum;

        private decimal wtSumFrgn;

        private decimal wtSumSys;

        private string wtAccount;

        private decimal wtBaseAmnt;

        private string bpAct;

        private decimal bcgSum;

        private decimal bcgSumFC;

        private decimal bcgSumSy;

        private decimal wtBaseSum;

        private decimal wtBaseSumF;

        private decimal wtBaseSumS;

        private DateTime? vatDate;

        private DateTime? cancelDate;

        private int u_IDA_TransIdDebit;

        private int u_IDA_TransIdDebAnu;

        private string u_IDA_CostingCode;

        private int u_NX_IdAsiento;

        private int u_NX_IdND;

        private int u_NX_IdNC;

        private string docCurr;

        public int DocEntry { get => docEntry; set => docEntry = value; }
        public int DocNum { get => docNum; set => docNum = value; }
        public char DocType { get => docType; set => docType = value; }
        public char Canceled { get => canceled; set => canceled = value; }
        public DateTime? DocDate { get => docDate; set => docDate = value; }
        public DateTime? DocDueDate { get => docDueDate; set => docDueDate = value; }
        public string CardCode { get => cardCode; set => cardCode = value; }
        public string CardName { get => cardName; set => cardName = value; }
        public string Address { get => address; set => address = value; }
        public string CashAcct { get => cashAcct; set => cashAcct = value; }
        public decimal CashSum { get => cashSum; set => cashSum = value; }
        public decimal CashSumFC { get => cashSumFC; set => cashSumFC = value; }
        public decimal CashSumSy { get => cashSumSy; set => cashSumSy = value; }
        public decimal CreditSum { get => creditSum; set => creditSum = value; }
        public decimal CredSumFC { get => credSumFC; set => credSumFC = value; }
        public decimal CredSumSy { get => credSumSy; set => credSumSy = value; }
        public string CheckAcct { get => checkAcct; set => checkAcct = value; }
        public decimal CheckSum { get => checkSum; set => checkSum = value; }
        public decimal CheckSumFC { get => checkSumFC; set => checkSumFC = value; }
        public decimal CheckSumSy { get => checkSumSy; set => checkSumSy = value; }
        public string TrsfrAcct { get => trsfrAcct; set => trsfrAcct = value; }
        public decimal TrsfrSum { get => trsfrSum; set => trsfrSum = value; }
        public decimal TrsfrSumFC { get => trsfrSumFC; set => trsfrSumFC = value; }
        public decimal TrsfrSumSy { get => trsfrSumSy; set => trsfrSumSy = value; }
        public DateTime? TrsfrDate { get => trsfrDate; set => trsfrDate = value; }
        public string TrsfrRef { get => trsfrRef; set => trsfrRef = value; }
        public char PayNoDoc { get => payNoDoc; set => payNoDoc = value; }
        public decimal NoDocSum { get => noDocSum; set => noDocSum = value; }
        public decimal NoDocSumFC { get => noDocSumFC; set => noDocSumFC = value; }
        public decimal NoDocSumSy { get => noDocSumSy; set => noDocSumSy = value; }
        public char DiffCurr { get => diffCurr; set => diffCurr = value; }
        public decimal DocTotal { get => docTotal; set => docTotal = value; }
        public decimal DocTotalFC { get => docTotalFC; set => docTotalFC = value; }
        public string Ref1 { get => ref1; set => ref1 = value; }
        public string CounterRef { get => counterRef; set => counterRef = value; }
        public decimal DocTotalSy { get => docTotalSy; set => docTotalSy = value; }
        public decimal DocRate { get => docRate; set => docRate = value; }
        public decimal SysRate { get => sysRate; set => sysRate = value; }
        public string ObjType { get => objType; set => objType = value; }
        public string Comments { get => comments; set => comments = value; }
        public string JrnlMemo { get => jrnlMemo; set => jrnlMemo = value; }
        public int TransId { get => transId; set => transId = value; }
        public int CntctCode { get => cntctCode; set => cntctCode = value; }
        public DateTime? UpdateDate { get => updateDate; set => updateDate = value; }
        public DateTime? TaxDate { get => taxDate; set => taxDate = value; }
        public char DataSource { get => dataSource; set => dataSource = value; }
        public int UserSign { get => userSign; set => userSign = value; }
        public string VatGroup { get => vatGroup; set => vatGroup = value; }
        public decimal VatSum { get => vatSum; set => vatSum = value; }
        public decimal VatSumFC { get => vatSumFC; set => vatSumFC = value; }
        public decimal VatSumSy { get => vatSumSy; set => vatSumSy = value; }
        public int FinncPriod { get => finncPriod; set => finncPriod = value; }
        public decimal VatPrcnt { get => vatPrcnt; set => vatPrcnt = value; }
        public string WtCode { get => wtCode; set => wtCode = value; }
        public decimal WtSum { get => wtSum; set => wtSum = value; }
        public decimal WtSumFrgn { get => wtSumFrgn; set => wtSumFrgn = value; }
        public decimal WtSumSys { get => wtSumSys; set => wtSumSys = value; }
        public string WtAccount { get => wtAccount; set => wtAccount = value; }
        public decimal WtBaseAmnt { get => wtBaseAmnt; set => wtBaseAmnt = value; }
        public string BpAct { get => bpAct; set => bpAct = value; }
        public decimal BcgSum { get => bcgSum; set => bcgSum = value; }
        public decimal BcgSumFC { get => bcgSumFC; set => bcgSumFC = value; }
        public decimal BcgSumSy { get => bcgSumSy; set => bcgSumSy = value; }
        public decimal WtBaseSum { get => wtBaseSum; set => wtBaseSum = value; }
        public decimal WtBaseSumF { get => wtBaseSumF; set => wtBaseSumF = value; }
        public decimal WtBaseSumS { get => wtBaseSumS; set => wtBaseSumS = value; }
        public DateTime? VatDate { get => vatDate; set => vatDate = value; }
        public DateTime? CancelDate { get => cancelDate; set => cancelDate = value; }
        public int U_IDA_TransIdDebit { get => u_IDA_TransIdDebit; set => u_IDA_TransIdDebit = value; }
        public int U_IDA_TransIdDebAnu { get => u_IDA_TransIdDebAnu; set => u_IDA_TransIdDebAnu = value; }
        public string U_IDA_CostingCode { get => u_IDA_CostingCode; set => u_IDA_CostingCode = value; }
        public int U_NX_IdAsiento { get => u_NX_IdAsiento; set => u_NX_IdAsiento = value; }
        public int U_NX_IdND { get => u_NX_IdND; set => u_NX_IdND = value; }
        public int U_NX_IdNC { get => u_NX_IdNC; set => u_NX_IdNC = value; }
        public string DocCurr { get => docCurr; set => docCurr = value; }
    }
}
