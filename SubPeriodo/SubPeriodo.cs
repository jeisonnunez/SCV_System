using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entidades
{
    public class SubPeriodo
    {
        private string Code;

        private string Name;

        private DateTime? F_RefDate;

        private DateTime? T_RefDate;

        private DateTime? F_DueDate;

        private DateTime? T_DueDate;

        private DateTime? F_TaxDate;

        private DateTime? T_TaxDate;

        private int userSing;

        private int userSing2;

        private int SubNum;

        private int Category;

        private string PeriodState;

        private DateTime? UpdateDate;

        public string Code1 { get => Code; set => Code = value; }
        public string Name1 { get => Name; set => Name = value; }
        public DateTime? F_RefDate1 { get => F_RefDate; set => F_RefDate = value; }
        public DateTime? T_RefDate1 { get => T_RefDate; set => T_RefDate = value; }
        public DateTime? F_DueDate1 { get => F_DueDate; set => F_DueDate = value; }
        public DateTime? T_DueDate1 { get => T_DueDate; set => T_DueDate = value; }
        public DateTime? F_TaxDate1 { get => F_TaxDate; set => F_TaxDate = value; }
        public DateTime? T_TaxDate1 { get => T_TaxDate; set => T_TaxDate = value; }
        public int UserSing { get => userSing; set => userSing = value; }
        public int UserSing2 { get => userSing2; set => userSing2 = value; }
        public int SubNum1 { get => SubNum; set => SubNum = value; }
        public int Category1 { get => Category; set => Category = value; }
        public string PeriodState1 { get => PeriodState; set => PeriodState = value; }
        public DateTime? UpdateDate1 { get => UpdateDate; set => UpdateDate = value; }      

    }
}

