using PresentacionWPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubPeriodos
{
    public class SubPeriodos
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

        public SubPeriodos(int usuario, string codigoPeriodo, string nombrePeriodo, DateTime? fechaContabilizacion, DateTime? DfechaVencimiento, DateTime? DfechaDocumento, DateTime? HfechaVencimiento, DateTime? HfechaDocumento)
        {
            List<SubPeriodos> ListSubPeriodos = new List<SubPeriodos>();

            int meses = (int)Periodos.Meses;

            DateTime? fechaSubPeriodo;

            int categoria;

            for (int i = 1; i <= meses; i++)
            {
                fechaSubPeriodo = SumaMes(fechaContabilizacion);

                categoria = IndicaCategoria(fechaContabilizacion);

                ListSubPeriodos.Add(new SubPeriodos() { Code1 = codigoPeriodo + "-0" + i, Name1 = nombrePeriodo + "-0" + i, F_DueDate1 = DfechaVencimiento, T_DueDate1 = HfechaVencimiento, F_TaxDate1 = DfechaDocumento, T_TaxDate1 = HfechaDocumento, UserSing = usuario, UserSing2 = usuario, SubNum1 = i, UpdateDate1 = FechaActual.GetFechaActual(), PeriodState1 = Estatus.N.ToString(), Category1 = categoria, F_RefDate1 = fechaContabilizacion, T_RefDate1 = fechaSubPeriodo });

                fechaContabilizacion = fechaSubPeriodo;
            }

            //envia la lista
            Empresa.SetListSubPeriodos(ListSubPeriodos);

        }

        private int IndicaCategoria(DateTime? fecha)
        {
            int year = fecha.Value.Year;

            return year;
        }

        private DateTime? SumaMes(DateTime? fecha)
        {
            return fecha.Value.AddMonths(1);
        }

        public SubPeriodos()
        {

        }
        enum Estatus { A, C, N, S, Y };
        enum Periodos { Meses = 12 };

    
}
    }

