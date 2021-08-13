using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ArticuloDetalle
    {      

        private int transNum;

        private int transType;

        private int createdBy;

        private string bASE_REF;

        private int docLineNum;

        private DateTime? docDate;

        private DateTime? docDueDate;

        private string cardCode;

        private string cardName;

        private string comments;

        private string jrnlMemo;

        private string itemCode;

        private string dscription;

        private decimal inQty;

        private decimal outQty;

        private decimal price;

        private string currency;

        private decimal rate;

        private decimal sysRate;

        private DateTime? taxDate;

        private int userSign;

        private decimal calcPrice;

        private decimal openQty;

        private DateTime? createDate;

        private decimal balance;

        private decimal transValue;

        private int transSeq;

        private string invntAct;

        private decimal openValue;

        private char costMethod;

        private char type;

        public int TransNum { get => transNum; set => transNum = value; }
        public int TransType { get => transType; set => transType = value; }
        public int CreatedBy { get => createdBy; set => createdBy = value; }
        public string BASE_REF { get => bASE_REF; set => bASE_REF = value; }
        public int DocLineNum { get => docLineNum; set => docLineNum = value; }
        public DateTime? DocDate { get => docDate; set => docDate = value; }
        public DateTime? DocDueDate { get => docDueDate; set => docDueDate = value; }
        public string CardCode { get => cardCode; set => cardCode = value; }
        public string CardName { get => cardName; set => cardName = value; }
        public string Comments { get => comments; set => comments = value; }
        public string JrnlMemo { get => jrnlMemo; set => jrnlMemo = value; }
        public string ItemCode { get => itemCode; set => itemCode = value; }
        public string Dscription { get => dscription; set => dscription = value; }
        public decimal InQty { get => inQty; set => inQty = value; }
        public decimal OutQty { get => outQty; set => outQty = value; }
        public decimal Price { get => price; set => price = value; }
        public string Currency { get => currency; set => currency = value; }
        public decimal Rate { get => rate; set => rate = value; }
        public DateTime? TaxDate { get => taxDate; set => taxDate = value; }
        public int UserSign { get => userSign; set => userSign = value; }
        public decimal CalcPrice { get => calcPrice; set => calcPrice = value; }
        public decimal OpenQty { get => openQty; set => openQty = value; }
        public DateTime? CreateDate { get => createDate; set => createDate = value; }
        public decimal Balance { get => balance; set => balance = value; }
        public decimal TransValue { get => transValue; set => transValue = value; }
        public int TransSeq { get => transSeq; set => transSeq = value; }
        public string InvntAct { get => invntAct; set => invntAct = value; }
        public decimal OpenValue { get => openValue; set => openValue = value; }
        public char CostMethod { get => costMethod; set => costMethod = value; }
        public char Type { get => type; set => type = value; }
        public decimal SysRate { get => sysRate; set => sysRate = value; }

       
       
    }
}
