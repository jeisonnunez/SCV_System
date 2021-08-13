using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Sociedad
    {
        private string compnyName;

        private string compnyAddr;

        private string country;

        private string printHeadr;

        private string phone1;

        private string phone2;

        private string fax;

        private string zipCode;

        private string e_Mail;

        private string mainCurncy;

        private string sysCurncy;

        private string taxIdNum;

        private string revOffice;

        private DateTime? updateDate;

        private int userSign;

        public string CompnyName { get => compnyName; set => compnyName = value; }
        public string CompnyAddr { get => compnyAddr; set => compnyAddr = value; }
        public string Country { get => country; set => country = value; }
        public string PrintHeadr { get => printHeadr; set => printHeadr = value; }
        public string Phone1 { get => phone1; set => phone1 = value; }
        public string Phone2 { get => phone2; set => phone2 = value; }
        public string Fax { get => fax; set => fax = value; }
        public string E_Mail { get => e_Mail; set => e_Mail = value; }
        public string MainCurncy { get => mainCurncy; set => mainCurncy = value; }
        public string SysCurncy { get => sysCurncy; set => sysCurncy = value; }
        public string TaxIdNum { get => taxIdNum; set => taxIdNum = value; }
        public string RevOffice { get => revOffice; set => revOffice = value; }
        public DateTime? UpdateDate { get => updateDate; set => updateDate = value; }
        public int UserSign { get => userSign; set => userSign = value; }
        public string ZipCode { get => zipCode; set => zipCode = value; }
    }
}
