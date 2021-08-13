using Datos.Modelo_Informes;
using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Controlador_Informes
{
    public class ControladorTipoCambioAlternativo: ControladorDocumento
    {
        ModeloTipoCambioAlternativo cn = new ModeloTipoCambioAlternativo();

        public Tuple<DataTable, string> ExecuteExchangeRateDifference(string cuentaGanancia, string cuentaPerdida,DataTable dt, decimal rate, DateTime? dFechaVencimiento = null, DateTime? hFechaVencimiento = null)
        {
            DataTable newDt = new DataTable();            

            var result = cn.ExecuteExchangeRateDifference(cuentaGanancia,cuentaPerdida, dt, rate, dFechaVencimiento, hFechaVencimiento);

            newDt = result.Item1.Copy();
          
            return Tuple.Create(newDt, result.Item2);
        }

        public string CreateDatasetPreliminarTipoCambio()
        {
            return cn.CreateDatasetPreliminarTipoCambio();
        }

        public Tuple<int,string> SelectTransIdPreliminar()
        {
            return cn.SelectTransIdPreliminar();
        }

        public string LoadTables()
        {
            return cn.LoadTables();
        }

        public Tuple<DataTable,string> ExecuteExchangeConversion(string cuentaBeneficio, string cuentaPerdida, DataTable dt, decimal rate)
        {
            DataTable newDt = new DataTable();          

            var result = cn.ExecuteExchangeConversion(cuentaBeneficio, cuentaPerdida, dt, rate);

            newDt = result.Item1.Copy();

            return Tuple.Create(newDt, result.Item2);
            
        }

        public DataTable GetOJDT()
        {
            DataTable newDt= cn.GetOJDT();

            //newDt = ChangeTypeColumnOJDT(newDt);

            return newDt;
        }

        public DataTable ChangeTypeColumnOJDT(DataTable dataTable)
        {
            //dataTable.Columns["TransType"].MaxLength = 30;

            //dataTable.AcceptChanges();

            DataTable dtCloned = dataTable.Clone();

            int i = 0;

            foreach (DataColumn column in dataTable.Columns)
            {
                if (dtCloned.Columns[i].ColumnName.ToString() == "TransType")
                {
                    dtCloned.Columns[i].DataType = typeof(int);
                    dtCloned.Columns[i].MaxLength = 30;
                }

                i++;
            }

            foreach (DataRow row in dataTable.Rows)
            {
               string prueba= row["TransType"].ToString();

                dtCloned.ImportRow(row);
            }

            return dtCloned;
        }

        public DataTable GetJDT1()
        {
            return cn.GetJDT1();
        }

        public Tuple<DataTable,string> ExecuteBalanceAlternate(DataTable dt, DateTime? dpDesde, DateTime? dpHasta, bool? monedaLocalySystema, bool? monedaSystema, bool? monedaExtranjera, bool? cuentaCero)
        {
            DataTable newDt = new DataTable();

            var result = cn.ExecuteBalanceAlternate(dt, dpDesde, dpHasta,  cuentaCero);

            newDt = result.Item1.Copy();

            newDt = ChangeTypeColumn(newDt);

            newDt = RemoveColumn(newDt, monedaLocalySystema, monedaSystema, monedaExtranjera);

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

      
        public string DeleteJournalEntrysTest()
        {
            return cn.DeleteJournalEntrysTest();
        }
    }
}
