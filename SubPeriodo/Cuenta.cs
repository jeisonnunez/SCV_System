using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Cuenta
    {
        private string oldAcctCode;

        private string acctCode;

        private string acctName;

        private decimal currTotal;

        private char finanse;

        private char postable;

        private int levels;

        private int grpLine;

        private char actType;

        private decimal sysTotal;

        private decimal fcTotal;

        private char advance;

        private DateTime? createDate;

        private int userSign;

        private char locManTran;

        private string fatherNum;

        private string actCurr;

        private int groupMask;

        public string OldAcctCode { get => oldAcctCode; set => oldAcctCode = value; }
        public string AcctCode { get => acctCode; set => acctCode = value; }
        public string AcctName { get => acctName; set => acctName = value; }
        public decimal CurrTotal { get => currTotal; set => currTotal = value; }
        public char Finanse { get => finanse; set => finanse = value; }
        public char Postable { get => postable; set => postable = value; }
        public int Levels { get => levels; set => levels = value; }
        public int GrpLine { get => grpLine; set => grpLine = value; }
        public char ActType { get => actType; set => actType = value; }
        public decimal SysTotal { get => sysTotal; set => sysTotal = value; }
        public decimal FcTotal { get => fcTotal; set => fcTotal = value; }
        public char Advance { get => advance; set => advance = value; }
        public DateTime? CreateDate { get => createDate; set => createDate = value; }
        public int UserSign { get => userSign; set => userSign = value; }
        public char LocManTran { get => locManTran; set => locManTran = value; }
        public string FatherNum { get => fatherNum; set => fatherNum = value; }
        public string ActCurr { get => actCurr; set => actCurr = value; }
        public int GroupMask { get => groupMask; set => groupMask = value; }
    }
}
