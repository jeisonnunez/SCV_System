using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class AsientoDetalle
    {
        private int transId;

        private int line_ID;

        private string account;

        private decimal debit;

        private decimal credit;

        private decimal sysCred;

        private decimal sysDeb;

        private decimal fCDebit;

        private decimal fCCredit;

        private string fCCurrency;

        private DateTime? dueDate;

        private string shortName;

        private string contraAct;

        private string lineMemo;

        private int transType;

        private DateTime? refDate;

        private DateTime? taxDate;

        private int userSign;

        private int finncPriod;

        private decimal balDueDeb;

        private decimal balDueCred;

        private decimal balFcDeb;

        private decimal balFcCred;

        private decimal balScDeb;

        private decimal balScCred;

        private char dataSource;

        public int TransId { get => transId; set => transId = value; }
        public int Line_ID { get => line_ID; set => line_ID = value; }
        public string Account { get => account; set => account = value; }
        public decimal Debit { get => debit; set => debit = value; }
        public decimal Credit { get => credit; set => credit = value; }
        public decimal SysCred { get => sysCred; set => sysCred = value; }
        public decimal SysDeb { get => sysDeb; set => sysDeb = value; }
        public decimal FCDebit { get => fCDebit; set => fCDebit = value; }
        public decimal FCCredit { get => fCCredit; set => fCCredit = value; }
        public string FCCurrency { get => fCCurrency; set => fCCurrency = value; }
        public DateTime? DueDate { get => dueDate; set => dueDate = value; }
        public string ShortName { get => shortName; set => shortName = value; }
        public string ContraAct { get => contraAct; set => contraAct = value; }
        public string LineMemo { get => lineMemo; set => lineMemo = value; }
        public int TransType { get => transType; set => transType = value; }
        public DateTime? RefDate { get => refDate; set => refDate = value; }
        public DateTime? TaxDate { get => taxDate; set => taxDate = value; }
        public int UserSign { get => userSign; set => userSign = value; }
        public int FinncPriod { get => finncPriod; set => finncPriod = value; }
        public decimal BalDueDeb { get => balDueDeb; set => balDueDeb = value; }
        public decimal BalDueCred { get => balDueCred; set => balDueCred = value; }
        public decimal BalFcDeb { get => balFcDeb; set => balFcDeb = value; }
        public decimal BalFcCred { get => balFcCred; set => balFcCred = value; }
        public decimal BalScDeb { get => balScDeb; set => balScDeb = value; }
        public decimal BalScCred { get => balScCred; set => balScCred = value; }
        public char DataSource { get => dataSource; set => dataSource = value; }
    }
}
