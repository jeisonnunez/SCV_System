using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class TablaRetencionImpuesto
    {
        private int absEntry;

        private string wTCode;

        private decimal rate;

        private decimal taxbleAmnt;

        private decimal taxbleAmntSC;

        private decimal taxbleAmntFC;

        private decimal wTAmnt;

        private decimal wTAmntSC;

        private decimal wTAmntFC;

        private decimal applAmnt;

        private decimal applAmntSC;

        private decimal applAmntFC;

        private char category;

        private string account;

        private char type;

        private char baseType;

        private int lineNum;

        private char status;

        private string objType;

        private decimal baseAmnt;

        private decimal baseAmntFC;

        private decimal baseAmntSC;

        public int AbsEntry { get => absEntry; set => absEntry = value; }
        public string WTCode { get => wTCode; set => wTCode = value; }
        public decimal Rate { get => rate; set => rate = value; }
        public decimal TaxbleAmnt { get => taxbleAmnt; set => taxbleAmnt = value; }
        public decimal TaxbleAmntSC { get => taxbleAmntSC; set => taxbleAmntSC = value; }
        public decimal TaxbleAmntFC { get => taxbleAmntFC; set => taxbleAmntFC = value; }
        public decimal WTAmnt { get => wTAmnt; set => wTAmnt = value; }
        public decimal WTAmntSC { get => wTAmntSC; set => wTAmntSC = value; }
        public decimal WTAmntFC { get => wTAmntFC; set => wTAmntFC = value; }
        public decimal ApplAmnt { get => applAmnt; set => applAmnt = value; }
        public decimal ApplAmntSC { get => applAmntSC; set => applAmntSC = value; }
        public decimal ApplAmntFC { get => applAmntFC; set => applAmntFC = value; }
        public char Category { get => category; set => category = value; }
        public string Account { get => account; set => account = value; }
        public char Type { get => type; set => type = value; }
        public char BaseType { get => baseType; set => baseType = value; }
        public int LineNum { get => lineNum; set => lineNum = value; }
        public char Status { get => status; set => status = value; }
        public string ObjType { get => objType; set => objType = value; }
        public decimal BaseAmnt { get => baseAmnt; set => baseAmnt = value; }
        public decimal BaseAmntFC { get => baseAmntFC; set => baseAmntFC = value; }
        public decimal BaseAmntSC { get => baseAmntSC; set => baseAmntSC = value; }
    }
}
