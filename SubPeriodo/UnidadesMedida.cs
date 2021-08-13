using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class UnidadesMedida
    {

        private int uomEntry;

        private string uomCode;

        private string oldUomCode;

        private string uomName;

        private char locked;

        private int userSign;

        private DateTime updateDate;

        private decimal length;

        private decimal width;

        private decimal height;

        private decimal volume;

        private decimal weight;

        private int volUnit;

        private decimal altQty;

        private decimal baseQty;


        public int UomEntry { get => uomEntry; set => uomEntry = value; }
        public string UomCode { get => uomCode; set => uomCode = value; }
        public string OldUomCode { get => oldUomCode; set => oldUomCode = value; }
        public string UomName { get => uomName; set => uomName = value; }
        public char Locked { get => locked; set => locked = value; }
        public int UserSign { get => userSign; set => userSign = value; }
        public DateTime UpdateDate { get => updateDate; set => updateDate = value; }
        public decimal Length { get => length; set => length = value; }
        public decimal Width { get => width; set => width = value; }
        public decimal Height { get => height; set => height = value; }
        public decimal Volume { get => volume; set => volume = value; }
        public decimal Weight { get => weight; set => weight = value; }
        public int VolUnit { get => volUnit; set => volUnit = value; }
        public decimal AltQty { get => altQty; set => altQty = value; }
        public decimal BaseQty { get => baseQty; set => baseQty = value; }
    }
}
