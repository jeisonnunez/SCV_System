using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class SocioNegocio
    {
        private string oldCardCode;

        private string cardCode;

        private string cardName;

        private char cardType;

        private string address;

        private string zipCode;

        private string mailAddress;

        private string mailZipCod;

        private string phone1;

        private string phone2;

        private string fax;

        private string cntctPrsn;

        private decimal balance;

        private string licTradNum;

        private string currency;

        private decimal balanceSys;

        private decimal balanceFC;

        private DateTime? updateDate;

        private int userSign;

        private string tipoPersona;

        private char contribuyente;

        private char sucursal;

        private char aplicaITF;

        private string e_mail;

        private string vatGroup;

        private string debPayAcct;

        private char deleted;

        private decimal dNotesBal;

        private decimal dNoteBalFC;

        private decimal dNoteBalSy;

        public string CardCode { get => cardCode; set => cardCode = value; }
        public string CardName { get => cardName; set => cardName = value; }
        public char CardType { get => cardType; set => cardType = value; }
        public string Address { get => address; set => address = value; }
        public string ZipCode { get => zipCode; set => zipCode = value; }
        public string MailAddress { get => mailAddress; set => mailAddress = value; }
        public string MailZipCod { get => mailZipCod; set => mailZipCod = value; }
        public string Phone1 { get => phone1; set => phone1 = value; }
        public string Phone2 { get => phone2; set => phone2 = value; }
        public string Fax { get => fax; set => fax = value; }
        public string CntctPrsn { get => cntctPrsn; set => cntctPrsn = value; }
        public decimal Balance { get => balance; set => balance = value; }
        public string LicTradNum { get => licTradNum; set => licTradNum = value; }
        public string Currency { get => currency; set => currency = value; }
        public decimal BalanceSys { get => balanceSys; set => balanceSys = value; }
        public decimal BalanceFC { get => balanceFC; set => balanceFC = value; }
        public DateTime? UpdateDate { get => updateDate; set => updateDate = value; }
        public int UserSign { get => userSign; set => userSign = value; }
        public string TipoPersona { get => tipoPersona; set => tipoPersona = value; }
        public char Contribuyente { get => contribuyente; set => contribuyente = value; }
        public char Sucursal { get => sucursal; set => sucursal = value; }
        public char AplicaITF { get => aplicaITF; set => aplicaITF = value; }
        public string E_mail { get => e_mail; set => e_mail = value; }
        public string VatGroup { get => vatGroup; set => vatGroup = value; }
        public string DebPayAcct { get => debPayAcct; set => debPayAcct = value; }
        public char Deleted { get => deleted; set => deleted = value; }
        public decimal DNotesBal { get => dNotesBal; set => dNotesBal = value; }
        public decimal DNoteBalFC { get => dNoteBalFC; set => dNoteBalFC = value; }
        public decimal DNoteBalSy { get => dNoteBalSy; set => dNoteBalSy = value; }
        public string OldCardCode { get => oldCardCode; set => oldCardCode = value; }
    }
}
