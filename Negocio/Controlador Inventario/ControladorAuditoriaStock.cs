using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio
{
    public class ControladorAuditoriaStock : ControladorReportes
    {
        ModeloAuditoriaStock cn = new ModeloAuditoriaStock();
        public Tuple<DataTable,string> ExecuteAuditoriaStock(DataTable dt, char cbxCompras, char cbxVentas, DateTime? dpHFechaContabilizacion, DateTime? dpDFechaContabilizacion = null)
        {
            var result = cn.ExecuteAuditoriaStock(dt, cbxCompras, cbxVentas, dpHFechaContabilizacion, dpDFechaContabilizacion);

            dt = ChangeTypeColumn(result.Item1);

            dt = SetTransTypeItem(dt);

            dt = ClearDatatableItem(dt);

            return Tuple.Create(dt, result.Item2);
        }

        private DataTable ClearDatatableItem(DataTable dt)
        {

            foreach (DataRow row in dt.Rows)
            {

                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() == "ItemCode" && row["Title"].ToString()=="N")
                    {
                        row["ItemCode"] = "";
                    }

                    else if (column.ToString() == "CalcPrice" && row["Title"].ToString() == "Y")
                    {
                        row["CalcPrice"] = "";
                    }

                    else if (column.ToString() == "CalcPrice" && row["Title"].ToString() == "E")
                    {
                        row["CalcPrice"] = "";
                    }

                    else if (column.ToString() == "Quantity" && row["Title"].ToString() == "E")
                    {
                        row["Quantity"] = "";
                    }

                    else if (column.ToString() == "Quantity" && row["Title"].ToString() == "Y")
                    {
                        row["Quantity"] = "";
                    }

                    else if (column.ToString() == "TransValue" && row["Title"].ToString() == "E")
                    {
                        row["TransValue"] = "";
                    }

                    else if (column.ToString() == "TransValue" && row["Title"].ToString() == "Y")
                    {
                        row["TransValue"] = "";
                    }


                }
            }

            return dt;
        }

        public Tuple<DataTable, string> ExecuteAuditoriaStockUnidades(DataRow[] tableResult)
        {
            DataTable dt = new DataTable();

            var result = cn.ExecuteAuditoriaStockUnidades(tableResult);

            dt = ChangeTypeColumn(result.Item1);            

            return Tuple.Create(dt, result.Item2);
        }
    }
}
