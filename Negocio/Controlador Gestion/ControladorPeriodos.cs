using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ControladorPeriodos: Negocios
    {
        ModeloPeriodos cn = new ModeloPeriodos();

        public Tuple<DataTable,string> ConsultaPeriodos()
        {
            DataTable dt;
            
            var result= cn.ConsultaPeriodos();

            dt= VerificaPeriodos(result.Item1);

            dt = AddColumnOld(dt, "OldCode", "Code");

            dt = VerificaPeriodos(dt);

            return Tuple.Create(dt, result.Item2);
        }

        public DataTable VerificaPeriodos(DataTable table)
        {
            DataTable newTable = table;

            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn column in table.Columns)
                {
                    if (column.ToString() == "F_RefDate" || column.ToString() == "T_RefDate" || column.ToString() == "F_DueDate" || column.ToString() == "T_DueDate" || column.ToString() == "F_TaxDate" || column.ToString() == "T_TaxDate")
                    {
                        row[column] = String.Format("{0:dd/MM/yyyy}", row[column]);
                    }

                    if (column.ToString() == "PeriodStat" && row[column].ToString() == ((char)EstadosPeriodos.Desbloqueado).ToString())
                    {
                        row[column] = "Desbloqueado";

                    }
                    else

                    if (column.ToString() == "PeriodStat" && row[column].ToString() == ((char)EstadosPeriodos.Bloqueado).ToString())
                    {
                        row[column] = "Bloqueado";

                    }

                    else if (column.ToString() == "PeriodStat" && row[column].ToString() == ((char)EstadosPeriodos.CierrePeriodo).ToString())
                    {
                        row[column] = "Cierre Periodo";

                    }

                    else if (column.ToString() == "PeriodStat" && row[column].ToString() == ((char)EstadosPeriodos.BloquedadoExecVentas).ToString())
                    {
                        row[column] = "Cierre Execpto Ventas";

                    }
                }
            }
            return newTable;
        }

       
        enum EstadosPeriodos { Desbloqueado = 'N', CierrePeriodo = 'C', Bloqueado = 'Y', BloquedadoExecVentas = 'S' };
    }
}
