using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class CodigosFiscales
    {

        private string code;

        private string old_Code;

        private string name;

        private decimal rate;

        private char freight;

        private int userSign;

        private char validForAP;

        private char validForAR;

        private char Lock;

        private DateTime? updateDate;

        private string u_IDA_Alicuota;

        public string Code { get => code; set => code = value; }
        public string Name { get => name; set => name = value; }
        public decimal Rate { get => rate; set => rate = value; }
        public char Freight { get => freight; set => freight = value; }
        public int UserSign { get => userSign; set => userSign = value; }
        public char ValidForAP { get => validForAP; set => validForAP = value; }
        public char ValidForAR { get => validForAR; set => validForAR = value; }
        public char Lock1 { get => Lock; set => Lock = value; }
        public DateTime? UpdateDate { get => updateDate; set => updateDate = value; }
        public string U_IDA_Alicuota { get => u_IDA_Alicuota; set => u_IDA_Alicuota = value; }
        public string Old_Code { get => old_Code; set => old_Code = value; }

    }
}
