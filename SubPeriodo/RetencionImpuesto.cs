using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class RetencionImpuesto
    {
        private string oldWtCode;

        private string wt_Code;

        private string wt_Name;

        private decimal rate;

        private DateTime? effecDate;

        private char category;

        private char baseType;

        private decimal prctBsAmnt;

        private string account;

        private string offclCode;

        private int userSign;

        private char inactive;

        private string tipoRetencion;

        private decimal baseMinima;

        private decimal sustraendo;

        public string Wt_Code { get => wt_Code; set => wt_Code = value; }
        public string Wt_Name { get => wt_Name; set => wt_Name = value; }
        public decimal Rate { get => rate; set => rate = value; }
        public DateTime? EffecDate { get => effecDate; set => effecDate = value; }
        public char Category { get => category; set => category = value; }
        public char BaseType { get => baseType; set => baseType = value; }
        public decimal PrctBsAmnt { get => prctBsAmnt; set => prctBsAmnt = value; }
        public string Account { get => account; set => account = value; }
        public int UserSign { get => userSign; set => userSign = value; }
        public char Inactive { get => inactive; set => inactive = value; }
        public string TipoRetencion { get => tipoRetencion; set => tipoRetencion = value; }
        public decimal BaseMinima { get => baseMinima; set => baseMinima = value; }
        public decimal Sustraendo { get => sustraendo; set => sustraendo = value; }
        public string OldWtCode { get => oldWtCode; set => oldWtCode = value; }
        public string OffclCode { get => offclCode; set => offclCode = value; }
    }
}
