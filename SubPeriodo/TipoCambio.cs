using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class TipoCambio
    {
        private DateTime rateDate;

        private string currency;

        private decimal rate;

        private int userSign;

        public DateTime RateDate { get => rateDate; set => rateDate = value; }
        public string Currency { get => currency; set => currency = value; }
        public decimal Rate { get => rate; set => rate = value; }
        public int UserSign { get => userSign; set => userSign = value; }
    }
}
