using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class PeriodosContables
    {
        private string OldCode;

        private string code;

        private string name;

        private DateTime? f_RefDate;

        private DateTime? t_RefDate;

        private DateTime? f_TaxDate;

        private DateTime? t_TaxDate;

        private DateTime? f_DueDate;

        private DateTime? t_DueDate;

        private char periodStat;

        private int userSign2;

        private DateTime? updateDate;

        private string category;

        public string Code { get => code; set => code = value; }
        public string Name { get => name; set => name = value; }
        public DateTime? F_RefDate { get => f_RefDate; set => f_RefDate = value; }
        public DateTime? T_RefDate { get => t_RefDate; set => t_RefDate = value; }
        public DateTime? F_TaxDate { get => f_TaxDate; set => f_TaxDate = value; }
        public DateTime? T_TaxDate { get => t_TaxDate; set => t_TaxDate = value; }
        public DateTime? F_DueDate { get => f_DueDate; set => f_DueDate = value; }
        public DateTime? T_DueDate { get => t_DueDate; set => t_DueDate = value; }
        public char PeriodStat { get => periodStat; set => periodStat = value; }
        public int UserSign2 { get => userSign2; set => userSign2 = value; }
        public DateTime? UpdateDate { get => updateDate; set => updateDate = value; }
        public string Category { get => category; set => category = value; }
        public string OldCode1 { get => OldCode; set => OldCode = value; }

        public PeriodosContables(string years)
        {
            this.Category = years;         
        }

        public PeriodosContables(string Code, string Name)
        {
            this.Code = Code;

            this.Name = Name;
        }

        public PeriodosContables()
        {
            
        }
    }
}
