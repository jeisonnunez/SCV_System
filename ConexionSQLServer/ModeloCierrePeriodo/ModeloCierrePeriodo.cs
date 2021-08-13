using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.ModeloCierrePeriodo
{
    public class ModeloCierrePeriodo:ConexionSQLServer
    {
        public Tuple<DataTable, string> ExecuteClosePeriod(DateTime starDate, DateTime endDate, string cuentaArrastre, string cuentaCierre, DataTable dtAccount, string desdeSN, string hastaSN)
        {
            DataTable newDt = new DataTable();

            newDt.Columns.Add("Account", typeof(string));
            newDt.Columns.Add("ShortName", typeof(string));
            newDt.Columns.Add("SaldoML", typeof(decimal));
            newDt.Columns.Add("SaldoME", typeof(decimal));
            newDt.Columns.Add("SaldoMS", typeof(decimal));
            newDt.Columns.Add("ActCurr", typeof(string));
            newDt.Columns.Add("ActType", typeof(string));
            newDt.Columns.Add("AcctName", typeof(string));


            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (DataRow row in dtAccount.Rows)
                    {
                        foreach (DataColumn column in dtAccount.Columns)
                        {

                            if (column.ToString() == "Seleccionado" && Convert.ToBoolean(row["Seleccionado"]) == true) //Verifica si la cuenta se selecciono
                            {

                                using (SqlCommand cmd = Connection.CreateCommand())
                                {
                                    cmd.CommandText = "GenerateClosePeriodAccount";

                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.Parameters.AddWithValue("@StarDate", starDate);

                                    cmd.Parameters.AddWithValue("@EndDate", endDate);

                                    cmd.Parameters.AddWithValue("@AcctCode", row["AcctCode"].ToString());

                                    cmd.Parameters.AddWithValue("@AcctCodeCierre", cuentaCierre);

                                    cmd.Parameters.AddWithValue("@AcctCodeArrastre", cuentaArrastre);

                                    SqlDataReader reader = cmd.ExecuteReader();

                                    while (reader.Read())
                                    {
                                        DataRow newRow = newDt.NewRow();

                                        newDt.Rows.Add(newRow);

                                        newRow["Account"] = reader["Account"].ToString();
                                        newRow["ShortName"] = reader["ShortName"].ToString();
                                        newRow["SaldoML"] = Convert.ToDecimal(reader["SaldoML"]);
                                        newRow["SaldoME"] = Convert.ToDecimal(reader["SaldoME"]);
                                        newRow["SaldoMS"] = Convert.ToDecimal(reader["SaldoMS"]);
                                        newRow["ActCurr"] = reader["ActCurr"].ToString();
                                        newRow["ActType"] = reader["ActType"].ToString();
                                        newRow["AcctName"] = reader["AcctName"].ToString();

                                    }

                                    reader.Close();

                                }//end using

                            }//end if

                        }//end foreach column

                    }//end foreach row

                    //Consultar Cuentas Asociadas y Socios de Negocio

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "GenerateClosePeriodBP";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@StarDate", starDate);

                        cmd.Parameters.AddWithValue("@EndDate", endDate);

                        cmd.Parameters.AddWithValue("@StarBP", desdeSN);

                        cmd.Parameters.AddWithValue("@EndBP", hastaSN);

                        cmd.Parameters.AddWithValue("@AcctCodeCierre", cuentaCierre);

                        cmd.Parameters.AddWithValue("@AcctCodeArrastre", cuentaArrastre);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            DataRow newRow = newDt.NewRow();

                            newDt.Rows.Add(newRow);

                            newRow["Account"] = reader["Account"].ToString();
                            newRow["ShortName"] = reader["ShortName"].ToString();
                            newRow["SaldoML"] = Convert.ToDecimal(reader["SaldoML"]);
                            newRow["SaldoME"] = Convert.ToDecimal(reader["SaldoME"]);
                            newRow["SaldoMS"] = Convert.ToDecimal(reader["SaldoMS"]);
                            newRow["ActCurr"] = reader["ActCurr"].ToString();
                            newRow["ActType"] = reader["ActType"].ToString();
                            newRow["AcctName"] = reader["AcctName"].ToString();

                        }

                        reader.Close();

                    }//end using

                }//end using

                Connection.Close();

                return Tuple.Create(newDt, error);

            }
            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(newDt, e.Message);
            }
        }
    }
}
