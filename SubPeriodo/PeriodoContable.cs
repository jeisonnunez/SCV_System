using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class PeriodoContable
    {
        private string periodCat;

        private DateTime? financYear;

        private string year;

        private string periodName;

        private char subType;

        private int periodNum;

        private DateTime? f_RefDate;

        private DateTime? t_RefDate;

        private DateTime? f_TaxDate;

        private DateTime? t_TaxDate;

        private DateTime? f_DueDate;

        private DateTime? t_DueDate;

        private DateTime? updateDate;

        private int userSign;

        public string PeriodCat { get => periodCat; set => periodCat = value; }
        public DateTime? FinancYear { get => financYear; set => financYear = value; }
        public string Year { get => year; set => year = value; }
        public string PeriodName { get => periodName; set => periodName = value; }
        public char SubType { get => subType; set => subType = value; }
        public int PeriodNum { get => periodNum; set => periodNum = value; }
        public DateTime? F_RefDate { get => f_RefDate; set => f_RefDate = value; }
        public DateTime? T_RefDate { get => t_RefDate; set => t_RefDate = value; }
        public DateTime? F_TaxDate { get => f_TaxDate; set => f_TaxDate = value; }
        public DateTime? T_TaxDate { get => t_TaxDate; set => t_TaxDate = value; }
        public DateTime? F_DueDate { get => f_DueDate; set => f_DueDate = value; }
        public DateTime? T_DueDate { get => t_DueDate; set => t_DueDate = value; }
        public DateTime? UpdateDate { get => updateDate; set => updateDate = value; }
        public int UserSign { get => userSign; set => userSign = value; }
    }
}
