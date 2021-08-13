using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class DocumentoDetalle
    {
        private int docEntry;

        private int lineNum;

        private char lineStatus;

        private string itemCode;

        private string dscription;

        private decimal quantity;

        private decimal openQty;

        private decimal price;

        private string currency;

        private decimal lineTotal;


        private string acctCode;

        private DateTime? docDate;

        private decimal openCreQty;

        private string baseCard;

        private decimal totalSumSy;

        private char invntSttus;

        private decimal vatPrcnt;

        private string vatGroup;

        private decimal priceAfVAT;

        private decimal vatSum;

        private decimal vatSumFrgn;

        private decimal vatSumSy;

        private int finncPriod;

        private string objType;

        private decimal dedVatSum;

        private decimal dedVatSumF;

        private decimal dedVatSumS;

        private string address;

        private decimal vatAppld;

        private decimal vatAppldFC;

        private decimal vatAppldSC;

        private decimal baseQty;

        private decimal baseOpnQty;

        private decimal vatDscntPr;

        private char wtLiable;

        private decimal lineVat;

        private decimal lineVatlF;

        private decimal stockSum;

        private decimal stockSumFc;

        private decimal stockSumSc;

        private decimal invQty;

        private decimal openInvQty;

        private decimal gtotal;

        private decimal gtotalFC;

        private decimal gtotalSC;

        private decimal totalFrgn;

        private char dataSource;

        private int uomEntry;

        private int uomEntry2;

        private string uomCode;

        private string uomCode2;

        private string UnitMsr2;

        private string UnitMsr;

        private decimal numPerMsr;

        private decimal numPerMsr2;

        private char startValue;

        private char isTax;

        public int DocEntry { get => docEntry; set => docEntry = value; }
        public int LineNum { get => lineNum; set => lineNum = value; }
        public char LineStatus { get => lineStatus; set => lineStatus = value; }
        public string ItemCode { get => itemCode; set => itemCode = value; }
        public string Dscription { get => dscription; set => dscription = value; }
        public decimal Quantity { get => quantity; set => quantity = value; }
        public decimal OpenQty { get => openQty; set => openQty = value; }
        public decimal Price { get => price; set => price = value; }
        public string Currency { get => currency; set => currency = value; }
        public decimal LineTotal { get => lineTotal; set => lineTotal = value; }      
        public string AcctCode { get => acctCode; set => acctCode = value; }
        public DateTime? DocDate { get => docDate; set => docDate = value; }
        public decimal OpenCreQty { get => openCreQty; set => openCreQty = value; }
        public string BaseCard { get => baseCard; set => baseCard = value; }
        public decimal TotalSumSy { get => totalSumSy; set => totalSumSy = value; }       
        public char InvntSttus { get => invntSttus; set => invntSttus = value; }
        public decimal VatPrcnt { get => vatPrcnt; set => vatPrcnt = value; }
        public string VatGroup { get => vatGroup; set => vatGroup = value; }
        public decimal PriceAfVAT { get => priceAfVAT; set => priceAfVAT = value; }
        public decimal VatSum { get => vatSum; set => vatSum = value; }
        public decimal VatSumFrgn { get => vatSumFrgn; set => vatSumFrgn = value; }
        public decimal VatSumSy { get => vatSumSy; set => vatSumSy = value; }
        public int FinncPriod { get => finncPriod; set => finncPriod = value; }
        public string ObjType { get => objType; set => objType = value; }
        public decimal DedVatSum { get => dedVatSum; set => dedVatSum = value; }
        public decimal DedVatSumF { get => dedVatSumF; set => dedVatSumF = value; }
        public decimal DedVatSumS { get => dedVatSumS; set => dedVatSumS = value; }
        public string Address { get => address; set => address = value; }
        public decimal VatAppld { get => vatAppld; set => vatAppld = value; }
        public decimal VatAppldFC { get => vatAppldFC; set => vatAppldFC = value; }
        public decimal VatAppldSC { get => vatAppldSC; set => vatAppldSC = value; }
        public decimal BaseQty { get => baseQty; set => baseQty = value; }
        public decimal BaseOpnQty { get => baseOpnQty; set => baseOpnQty = value; }
        public decimal VatDscntPr { get => vatDscntPr; set => vatDscntPr = value; }
        public char WtLiable { get => wtLiable; set => wtLiable = value; }
        public decimal LineVat { get => lineVat; set => lineVat = value; }
        public decimal LineVatlF { get => lineVatlF; set => lineVatlF = value; }
        public decimal StockSum { get => stockSum; set => stockSum = value; }
        public decimal StockSumFc { get => stockSumFc; set => stockSumFc = value; }
        public decimal StockSumSc { get => stockSumSc; set => stockSumSc = value; }
        public decimal InvQty { get => invQty; set => invQty = value; }
        public decimal OpenInvQty { get => openInvQty; set => openInvQty = value; }
        public decimal Gtotal { get => gtotal; set => gtotal = value; }
        public decimal GtotalFC { get => gtotalFC; set => gtotalFC = value; }
        public decimal GtotalSC { get => gtotalSC; set => gtotalSC = value; }
        public decimal TotalFrgn { get => totalFrgn; set => totalFrgn = value; }
        public char DataSource { get => dataSource; set => dataSource = value; }
        public int UomEntry { get => uomEntry; set => uomEntry = value; }
        public int UomEntry2 { get => uomEntry2; set => uomEntry2 = value; }
        public string UomCode { get => uomCode; set => uomCode = value; }
        public string UomCode2 { get => uomCode2; set => uomCode2 = value; }
        public decimal NumPerMsr { get => numPerMsr; set => numPerMsr = value; }
        public string unitMsr2 { get => UnitMsr2; set => UnitMsr2 = value; }
        public string unitMsr { get => UnitMsr; set => UnitMsr = value; }
        public decimal NumPerMsr2 { get => numPerMsr2; set => numPerMsr2 = value; }
        public char StartValue { get => startValue; set => startValue = value; }
        public char IsTax { get => isTax; set => isTax = value; }
    }
}
