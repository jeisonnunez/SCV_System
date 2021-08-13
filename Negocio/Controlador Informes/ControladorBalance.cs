using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Datos;
using System.Data;
using System.Windows.Controls;

namespace Negocio
{
    public class ControladorBalance : ControladorReportes
    {
        ModeloBalance cn = new ModeloBalance();   
        public Tuple <DataTable,string> ExecuteBalance(DataTable dt, DateTime? dpHFechaContabilizacion, bool? cuentasCero, bool? cbxMonedaLocalySystema, bool? cbxMonedaSistema, bool? cbxMonedaExtranjera, DateTime? dpDFechaContabilizacion = null)
        {
            DataTable newDt = new DataTable();

            var result = cn.ExecuteBalance(dt,dpHFechaContabilizacion,cuentasCero, dpDFechaContabilizacion);

            newDt = result.Item1.Copy();

            newDt = ChangeTypeColumn(newDt);

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

            }
            else if (cbxMonedaLocalySystema == false && cbxMonedaSistema == false && cbxMonedaExtranjera == false)
            {
                newDt.Columns.Remove("FCDebit");
                newDt.Columns.Remove("FCCredit");
                newDt.Columns.Remove("FCCurrency");
                newDt.Columns.Remove("FCSaldo");
                newDt.Columns.Remove("SYSDeb");
                newDt.Columns.Remove("SYSCred");
                newDt.Columns.Remove("SaldoSYS");

            }
            else if (cbxMonedaSistema == true && cbxMonedaExtranjera == true)
            {
                newDt.Columns.Remove("Debit");
                newDt.Columns.Remove("Credit");
                newDt.Columns.Remove("Saldo");

            }
            else if (cbxMonedaSistema == false && cbxMonedaExtranjera == true)
            {
                newDt.Columns.Remove("Debit");
                newDt.Columns.Remove("Credit");
                newDt.Columns.Remove("Saldo");
                newDt.Columns.Remove("SYSDeb");
                newDt.Columns.Remove("SYSCred");
                newDt.Columns.Remove("SaldoSYS");
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
            }

            newDt.AcceptChanges();

            return newDt;
        }

        
        
    }
}
