using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Negocio
{
    public class ControladorMoneda:ControladorDocumento
    {
        ModeloMoneda cn = new ModeloMoneda();

        public new Tuple <DataTable,string> ConsultaMonedas()
        {
            DataTable dt = new DataTable();

            DataTable newDt = new DataTable();
            
            var result= cn.ConsultaMonedas();

            dt = result.Item1.Copy();

            dt.AcceptChanges();

            newDt = TraduceMonedas(dt);

            newDt = AddColumnOld(newDt, "OldCurrCode", "CurrCode");

            newDt = ChangeTypeColumnMonedas(newDt);

            return Tuple.Create(newDt, result.Item2);
        }

        public DataTable TraduceMonedas(DataTable table)
        {
            DataTable newTable = table.Copy();

            foreach (DataRow row in newTable.Rows)
            {
                foreach (DataColumn column in newTable.Columns)
                {

                    if (column.ToString() == "Locked" && row[column].ToString() == ((char)EstadosMoneda.Activo).ToString())
                    {
                        row[column] = true;

                    }
                    else

                    if (column.ToString() == "Locked" && row[column].ToString() == ((char)EstadosMoneda.Inactivo).ToString())
                    {
                        row[column] = false;

                    }


                }
            }

            newTable.AcceptChanges();

            return newTable;
        }

        public DataTable ChangeTypeColumnMonedas(DataTable dataTable)
        {
            DataTable dtCloned = dataTable.Clone();

            int i = 0;

            foreach (DataColumn column in dataTable.Columns)
            {
                if (dtCloned.Columns[i].ColumnName.ToString() == "Locked") {

                    dtCloned.Columns[i].DataType = typeof(bool);
                }
                else
                {
                    dtCloned.Columns[i].DataType = typeof(string);
                }
              

                i++;
            }

            foreach (DataRow row in dataTable.Rows)
            {
                dtCloned.ImportRow(row);
            }

            return dtCloned;
        }

        public Tuple<int, string> EliminaMoneda(string moneda)
        {
            return cn.EliminaMoneda(moneda);
        }

        public Tuple <int,string> InsertaMonedas(List<Monedas> listaMonedas)
        {
            return cn.InsertaMonedas(listaMonedas);

        }

        enum EstadosMoneda { Activo = 'Y', Inactivo = 'N' };

    }
}
