using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Monedas 
    {

        private string oldCurrCode;

        private string currCode;

        private string currName;

        private string docCurrCod;

        private int userSign;

        private char locked;
        public string CurrCode { get => currCode; set => currCode = value; }
        public string CurrName { get => currName; set => currName = value; }
        public string DocCurrCod { get => docCurrCod; set => docCurrCod = value; }
        public int UserSign { get => userSign; set => userSign = value; }
        public char Locked { get => locked; set => locked = value; }
        public string OldCurrCode { get => oldCurrCode; set => oldCurrCode = value; }

        public Monedas(string currCode, string currName)
        {
            this.CurrCode = currCode;
            this.CurrName = currName;
        }

        public Monedas()
        {
           
        }

        

    }

   
}
