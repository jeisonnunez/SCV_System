using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Articulos
    {
        private string oldItemCode;

        private string itemCode;

        private string itemName;

        private char vatLiable;

        private char prchseItem;

        private char sellItem;

        private char invnItem;

        private decimal onHand;

        private decimal isCommited;

        private decimal onOrders;

        private char deleted;

        private int userSign;

        private DateTime? UpdateDate;

        private int docEntry;

        private decimal stockValue;

        private string balInvntAc;

        private string saleCostAc;

        private string revenuesAc;

        private string expensesAc;

        private string transferAc;

        private string evalSystem;

        private int ugpEntry;

        private decimal numInCnt;

        private string invntryUomCode;

        private string invntryUomName;

        private int invntryUomEntry;

        public string OldItemCode { get => oldItemCode; set => oldItemCode = value; }
        public string ItemCode { get => itemCode; set => itemCode = value; }
        public string ItemName { get => itemName; set => itemName = value; }
        public char VatLiable { get => vatLiable; set => vatLiable = value; }
        public char PrchseItem { get => prchseItem; set => prchseItem = value; }
        public char SellItem { get => sellItem; set => sellItem = value; }
        public char InvnItem { get => invnItem; set => invnItem = value; }
        public decimal OnHand { get => onHand; set => onHand = value; }
        public decimal IsCommited { get => isCommited; set => isCommited = value; }
        public decimal OnOrders { get => onOrders; set => onOrders = value; }
        public char Deleted { get => deleted; set => deleted = value; }
        public int UserSign { get => userSign; set => userSign = value; }
        public DateTime? UpdateDate1 { get => UpdateDate; set => UpdateDate = value; }
        public int DocEntry { get => docEntry; set => docEntry = value; }
        public decimal StockValue { get => stockValue; set => stockValue = value; }
        public string BalInvntAc { get => balInvntAc; set => balInvntAc = value; }
        public string SaleCostAc { get => saleCostAc; set => saleCostAc = value; }
        public string RevenuesAc { get => revenuesAc; set => revenuesAc = value; }
        public string ExpensesAc { get => expensesAc; set => expensesAc = value; }
        public string TransferAc { get => transferAc; set => transferAc = value; }
        public string EvalSystem { get => evalSystem; set => evalSystem = value; }
        public int UgpEntry { get => ugpEntry; set => ugpEntry = value; }
        public decimal NumInCnt { get => numInCnt; set => numInCnt = value; }
        public string InvntryUomCode { get => invntryUomCode; set => invntryUomCode = value; }
        public string InvntryUomName { get => invntryUomName; set => invntryUomName = value; }
        public int InvntryUomEntry { get => invntryUomEntry; set => invntryUomEntry = value; }
    }
}
