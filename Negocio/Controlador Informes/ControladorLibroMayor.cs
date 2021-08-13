using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocio
{
    public class ControladorLibroMayor : ControladorReportes
    {
        ModeloLibroMayor cn = new ModeloLibroMayor();
        public Tuple<DataTable,string> ExecuteLibroMayor(DataTable dt, DateTime? dpHFechaContabilizacion, bool? cbxMonedaLocalySystema, bool? cbxMonedaSistema, bool? cbxMonedaExtranjera, DateTime? dpDFechaContabilizacion = null)
        {
            DataTable newDt = new DataTable();                     

            var result= cn.ExecuteLibroMayor(dt, dpHFechaContabilizacion, dpDFechaContabilizacion);

            newDt = result.Item1.Copy();

            newDt= ChangeTypeColumn(newDt);

            newDt = SetTransType(newDt);

            newDt = RemoveColumn(newDt, cbxMonedaLocalySystema, cbxMonedaSistema, cbxMonedaExtranjera);

            return Tuple.Create(newDt, result.Item2);
        }

        private DataTable RemoveColumn(DataTable dt, bool? cbxMonedaLocalySystema, bool? cbxMonedaSistema, bool? cbxMonedaExtranjera)
        {
            DataTable newDt;

            newDt = dt.Copy();

            newDt.AcceptChanges();

            if (cbxMonedaLocalySystema == true && cbxMonedaSistema == false && cbxMonedaExtranjera == false)
            {
                newDt.Columns.Remove("FCDebit");
                newDt.Columns.Remove("FCCredit");
                newDt.Columns.Remove("FCCurrency");
                newDt.Columns.Remove("FCSaldo");
                newDt.Columns.Remove("FCSaldoAcum");

            }
            else if (cbxMonedaLocalySystema == false && cbxMonedaSistema == false && cbxMonedaExtranjera == false)
            {
                newDt.Columns.Remove("FCDebit");
                newDt.Columns.Remove("FCCredit");
                newDt.Columns.Remove("FCCurrency");
                newDt.Columns.Remove("FCSaldo");
                newDt.Columns.Remove("FCSaldoAcum");
                newDt.Columns.Remove("SYSDeb");
                newDt.Columns.Remove("SYSCred");
                newDt.Columns.Remove("SaldoSYS");
                newDt.Columns.Remove("SaldoSYSAcum");


            }
            else if (cbxMonedaSistema == true && cbxMonedaExtranjera == true)
            {
                newDt.Columns.Remove("Debit");
                newDt.Columns.Remove("Credit");
                newDt.Columns.Remove("Saldo");
                newDt.Columns.Remove("SaldoAcum");

            }
            else if (cbxMonedaSistema == false && cbxMonedaExtranjera == true)
            {
                newDt.Columns.Remove("Debit");
                newDt.Columns.Remove("Credit");
                newDt.Columns.Remove("Saldo");
                newDt.Columns.Remove("SaldoAcum");
                newDt.Columns.Remove("SYSDeb");
                newDt.Columns.Remove("SYSCred");
                newDt.Columns.Remove("SaldoSYS");
                newDt.Columns.Remove("SaldoSYSAcum");
            }
            else if (cbxMonedaSistema == true && cbxMonedaExtranjera == false)
            {
                newDt.Columns.Remove("Debit");
                newDt.Columns.Remove("Credit");
                newDt.Columns.Remove("Saldo");
                newDt.Columns.Remove("FCDebit");
                newDt.Columns.Remove("FCCredit");
                newDt.Columns.Remove("FCCurrency");
                newDt.Columns.Remove("FCSaldo");
                newDt.Columns.Remove("FCSaldoAcum");
            }

            newDt.AcceptChanges();

            return newDt;
        }


        //private DataTable DeleteRows(DataTable dt)
        //{


        //    for (int i=0; i<= dt.Rows.Count; i++)
        //    {
        //        DataRow row = dt.Rows[i];

        //        if (Convert.ToBoolean(row["Seleccionado"]) == false)
        //        {                    
        //            dt.Rows.Remove(row); //Remove
        //            dt.AcceptChanges();

        //        }

        //    }

        //    return dt;
        //}
    }
}
