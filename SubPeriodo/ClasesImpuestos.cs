using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ClasesImpuestos
    {
      
        private string code;

        private string name;

        private decimal rate;

        private string salesTax;

        private string purchTax;

        private int userSign;

        public string Code { get => code; set => code = value; }
        public string Name { get => name; set => name = value; }
        public decimal Rate { get => rate; set => rate = value; }
        public string SalesTax { get => salesTax; set => salesTax = value; }
        public string PurchTax { get => purchTax; set => purchTax = value; }
        public int UserSign { get => userSign; set => userSign = value; }
    }
}
