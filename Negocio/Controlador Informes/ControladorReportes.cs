using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Negocio;
using Datos;
using System.Data;

namespace Negocio
{
    public class ControladorReportes:Negocios
    {
        ModeloReportes cn = new ModeloReportes();
        public Tuple<DataTable, string> FindAllAccount()
        {
            DataTable dtClone;

            var result = cn.FindAllAccount();

            dtClone = AddColumnAccount(result.Item1);

            return Tuple.Create(dtClone, result.Item2);
        }

        public Tuple<DataTable, string> FindAllAccountReal()
        {
            DataTable dtClone;

            var result = cn.FindAllAccountReal();

            dtClone = AddColumnAccount(result.Item1);

            return Tuple.Create(dtClone, result.Item2);
        }

        public Tuple<DataTable, string> FindAllItems()
        {
            DataTable dtClone;

            var result = cn.FindAllItems();

            dtClone = AddColumnAccount(result.Item1);

            return Tuple.Create(dtClone, result.Item2);
        }

        public DataTable AddColumnAccount(DataTable dataTable)
        {
            DataTable dtCloned = dataTable.Clone();

            int i = 0;

            foreach (DataColumn column in dataTable.Columns)
            {
                dtCloned.Columns[i].DataType = typeof(string);

                i++;
            }

            dtCloned.Columns.Add("Seleccionado", typeof(bool));


            foreach (DataRow row in dataTable.Rows)
            {
                dtCloned.ImportRow(row);
            }

            return dtCloned;
        }

        protected DataTable SetTransType(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {

                    if (column.ToString() == "TransType")
                    {
                        if (String.IsNullOrWhiteSpace(row["TransType"].ToString()) == false)
                        {
                            row["TransType"] = GetTransType(Convert.ToInt32(row["TransType"].ToString()));
                            row["TransType"] = row["TransType"].ToString() + " " + row["BaseRef"].ToString();
                        }
                    }

                }
            }

            return dt;
        }

        

        protected DataTable SetTransTypeItem(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {

                    if (column.ToString() == "TransType")
                    {
                        if (String.IsNullOrWhiteSpace(row["TransType"].ToString()) == false && row["TransType"].ToString()!="Saldo Inicial")
                        {
                            row["TransType"] = GetTransType(Convert.ToInt32(row["TransType"].ToString()));
                            row["TransType"] = row["TransType"].ToString() + " " + row["BASE_REF"].ToString();
                        }
                    }

                }
            }

            return dt;
        }

        protected DataTable ChangeTypeColumn(DataTable dataTable)
        {
            DataTable dtCloned = dataTable.Clone();

            int i = 0;

            foreach (DataColumn column in dataTable.Columns)
            {
                dtCloned.Columns[i].DataType = typeof(string);

                i++;
            }

            foreach (DataRow row in dataTable.Rows)
            {
                dtCloned.ImportRow(row);
            }

            return dtCloned;
        }

    }
}
