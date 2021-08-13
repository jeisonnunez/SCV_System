using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Modelo_Finanzas
{
    public class ModeloDiferenciaConversion: ConexionSQLServer
    {
        public Tuple<DataTable, string> ExecuteExchangeConversion(DataTable dtAccount, string DesdeSN, string HastaSN, DateTime? fechaEjecucion)
        {
            DataTable newDt = new DataTable();

            newDt.Columns.Add("Account", typeof(string));
            newDt.Columns.Add("ShortName", typeof(string));
            newDt.Columns.Add("Saldo", typeof(decimal));
            newDt.Columns.Add("SaldoMS", typeof(decimal));
            newDt.Columns.Add("Diferencia", typeof(decimal));
            newDt.Columns.Add("SaldoConvertido", typeof(string));
            newDt.Columns.Add("Rate", typeof(decimal));
            newDt.Columns.Add("AcctName", typeof(string));
            newDt.Columns.Add("Type", typeof(string));

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (DataRow row in dtAccount.Rows)
                    {
                      
                            if (Convert.ToBoolean(row["Seleccionado"]) == true) //Verifica si la cuenta se selecciono
                            {

                                using (SqlCommand cmd = Connection.CreateCommand())
                                {
                                    cmd.CommandText = "GenerateExchangeConversionAccount";

                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.Parameters.AddWithValue("@RefDate", fechaEjecucion);

                                    cmd.Parameters.AddWithValue("@AcctCode", row["AcctCode"].ToString());

                                    SqlDataReader reader = cmd.ExecuteReader();

                                    while (reader.Read())
                                    {
                                        DataRow newRow = newDt.NewRow();

                                        newDt.Rows.Add(newRow);

                                        newRow["Account"] = reader["Account"].ToString();
                                        newRow["ShortName"] = reader["ShortName"].ToString();
                                        newRow["Saldo"] = Convert.ToDecimal(reader["Saldo"]);
                                        newRow["SaldoMS"] = Convert.ToDecimal(reader["SaldoMS"]);
                                        newRow["SaldoConvertido"] = Convert.ToDecimal(reader["SaldoConvertido"]);
                                        newRow["Diferencia"] = Convert.ToDecimal(reader["Diferencia"]);
                                        newRow["Type"] = reader["Type"].ToString();
                                        newRow["AcctName"] = reader["AcctName"].ToString();

                                    }

                                    reader.Close();

                                }//end using

                            }//end if                      
                    }//end foreach row

                    //Consultar Cuentas Asociadas y Socios de Negocio

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "GenerateExchangeConversionBP";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@StarBP", DesdeSN);

                        cmd.Parameters.AddWithValue("@EndBP", HastaSN);

                        cmd.Parameters.AddWithValue("@RefDate", fechaEjecucion);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            DataRow newRow = newDt.NewRow();

                            newDt.Rows.Add(newRow);

                            newRow["Account"] = reader["Account"].ToString();
                            newRow["ShortName"] = reader["ShortName"].ToString();
                            newRow["Saldo"] = Convert.ToDecimal(reader["Saldo"]);
                            newRow["SaldoMS"] = Convert.ToDecimal(reader["SaldoMS"]);
                            newRow["SaldoConvertido"] = Convert.ToDecimal(reader["SaldoConvertido"]);
                            newRow["Diferencia"] = Convert.ToDecimal(reader["Diferencia"]);
                            newRow["Type"] = reader["Type"].ToString();
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
