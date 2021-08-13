using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Negocio
{
    public class ControladorSubPeriodos
    {           
        public List<SubPeriodo> EstableceSubPeriodos(List<PeriodoContable> listaPeriodoBase)
        {
            List<SubPeriodo> ListSubPeriodos = new List<SubPeriodo>();

            foreach (PeriodoContable periodoContable in listaPeriodoBase)
            {
                             
                int meses = (int)Periodos.Meses;

                DateTime? fechaSubPeriodo;

                int categoria;

                for (int i = 1; i <= meses; i++)
                {
                    if (i == 12)
                    {
                        fechaSubPeriodo = SumaUltimoMes(periodoContable.F_RefDate);
                    }
                    else
                    {
                        fechaSubPeriodo = SumaMes(periodoContable.F_RefDate);
                    }


                    categoria = IndicaCategoria(periodoContable.F_RefDate);

                    ListSubPeriodos.Add(new SubPeriodo() { Code1 = periodoContable.PeriodCat + "-" + i, Name1 = periodoContable.PeriodName + "-" + i, F_DueDate1 = periodoContable.F_DueDate, T_DueDate1 = periodoContable.T_DueDate, F_TaxDate1 = periodoContable.F_TaxDate, T_TaxDate1 = periodoContable.T_TaxDate, UserSing = periodoContable.UserSign, UserSing2 = periodoContable.UserSign, SubNum1 = i, UpdateDate1 = fechaActual.GetFechaActual(), PeriodState1 = Estatus.N.ToString(), Category1 = categoria, F_RefDate1 = periodoContable.F_RefDate, T_RefDate1 = fechaSubPeriodo });

                    periodoContable.F_RefDate = SumaDia(fechaSubPeriodo);
                }

               

            }          

            return ListSubPeriodos;
        }

        private int IndicaCategoria(DateTime? fecha)
        {
            int year = fecha.Value.Year;

            return year;
        }

        private DateTime? SumaDia(DateTime? fecha)
        {
            return fecha.Value.AddDays(1);
        }

        private DateTime? SumaMes(DateTime? fecha)
        {
            return fecha.Value.AddMonths(1);
        }

        private DateTime? SumaUltimoMes(DateTime? fecha)
        {
            return fecha.Value.AddDays(-1).AddMonths(1);
        }

        enum Estatus { A, C, N, S, Y };
        enum Periodos { Meses = 12 };
    }
    }

