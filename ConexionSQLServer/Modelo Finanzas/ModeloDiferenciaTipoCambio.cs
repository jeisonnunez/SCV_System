using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Modelo_Finanzas
{
    public class ModeloDiferenciaTipoCambio:ConexionSQLServer
    {
        public Tuple<DataTable, string> ExecuteExchangeRateDifference(string cuentaGananciaCliente, string cuentaGananciaProveedores, string cuentaGananciaCuenta, string cuentaPerdidaCliente, string cuentaPerdidaProveedores, string cuentaPerdidaCuenta, DataTable dtAccount, string DesdeSN, string HastaSN, string currency, DateTime? fechaEjecucion, DateTime? dFechaVencimiento = null, DateTime? hFechaVencimiento = null)
        {
            DataTable newDt = new DataTable();

            newDt.Columns.Add("Account", typeof(string));
            newDt.Columns.Add("ShortName", typeof(string));
            newDt.Columns.Add("Saldo", typeof(decimal));
            newDt.Columns.Add("SaldoME", typeof(decimal));
            newDt.Columns.Add("Diferencia", typeof(decimal));
            newDt.Columns.Add("FCCurrency", typeof(string));
            newDt.Columns.Add("Rate", typeof(decimal));
            newDt.Columns.Add("Type", typeof(string));
            newDt.Columns.Add("AcctName", typeof(string));

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
                                    cmd.CommandText = "GenerateExchangeRateDifferenceAccount";

                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.Parameters.AddWithValue("@AccountGanancia", cuentaGananciaCuenta);

                                    cmd.Parameters.AddWithValue("@AccountPerdida", cuentaPerdidaCuenta);

                                    cmd.Parameters.AddWithValue("@RefDate", fechaEjecucion);

                                    cmd.Parameters.AddWithValue("@F_DueDate", dFechaVencimiento);

                                    cmd.Parameters.AddWithValue("@T_DueDate", hFechaVencimiento);

                                    cmd.Parameters.AddWithValue("@AcctCode", row["AcctCode"].ToString());

                                    cmd.Parameters.AddWithValue("@Currency", currency);

                                    SqlDataReader reader = cmd.ExecuteReader();

                                    while (reader.Read())
                                    {
                                        DataRow newRow = newDt.NewRow();

                                        newDt.Rows.Add(newRow);

                                        newRow["Account"] = reader["Account"].ToString();
                                        newRow["ShortName"] = reader["ShortName"].ToString();
                                        newRow["Saldo"] = Convert.ToDecimal(reader["Saldo"]);
                                        newRow["SaldoME"] = Convert.ToDecimal(reader["SaldoME"]);
                                        newRow["Diferencia"] = Convert.ToDecimal(reader["Diferencia"]);
                                        newRow["Rate"] = Convert.ToDecimal(reader["Rate"]);
                                        newRow["FCCurrency"] = reader["FCCurrency"].ToString();
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
                        cmd.CommandText = "GenerateExchangeRateDifferenceBP";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@SupplierGanancia", cuentaGananciaProveedores);

                        cmd.Parameters.AddWithValue("@SupplierPerdida", cuentaPerdidaProveedores);

                        cmd.Parameters.AddWithValue("@CustomerGanancia", cuentaGananciaCliente);

                        cmd.Parameters.AddWithValue("@CustomerPerdida", cuentaPerdidaCliente);

                        cmd.Parameters.AddWithValue("@StarBP", DesdeSN);

                        cmd.Parameters.AddWithValue("@EndBP", HastaSN);

                        cmd.Parameters.AddWithValue("@RefDate", fechaEjecucion);

                        cmd.Parameters.AddWithValue("@F_DueDate", dFechaVencimiento);

                        cmd.Parameters.AddWithValue("@T_DueDate", hFechaVencimiento);

                        cmd.Parameters.AddWithValue("@Currency", currency);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            DataRow newRow = newDt.NewRow();

                            newDt.Rows.Add(newRow);

                            newRow["Account"] = reader["Account"].ToString();
                            newRow["ShortName"] = reader["ShortName"].ToString();
                            newRow["Saldo"] = Convert.ToDecimal(reader["Saldo"]);
                            newRow["SaldoME"] = Convert.ToDecimal(reader["SaldoME"]);
                            newRow["Diferencia"] = Convert.ToDecimal(reader["Diferencia"]);
                            newRow["Rate"] = Convert.ToDecimal(reader["Rate"]);
                            newRow["FCCurrency"] = reader["FCCurrency"].ToString();
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
