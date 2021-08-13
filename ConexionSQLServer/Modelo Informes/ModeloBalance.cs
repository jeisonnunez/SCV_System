using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ModeloBalance : ModeloReportes
    {      

        public Tuple<DataTable, string> ExecuteBalance(DataTable dt, DateTime? dpHFechaContabilizacion, bool? cuentasCero, DateTime? dpDFechaContabilizacion = null)
        {

           
            DataTable newDt = new DataTable();

            newDt.Columns.Add("Name",typeof(string));
            newDt.Columns.Add("AcctCode", typeof(string));
            newDt.Columns.Add("Debit", typeof(string));
            newDt.Columns.Add("Credit", typeof(string));
            newDt.Columns.Add("Saldo", typeof(string));
            newDt.Columns.Add("FCDebit", typeof(string));
            newDt.Columns.Add("FCCredit", typeof(string));
            newDt.Columns.Add("FCSaldo", typeof(string));
            newDt.Columns.Add("SYSDeb", typeof(string));
            newDt.Columns.Add("SYSCred", typeof(string));
            newDt.Columns.Add("SaldoSYS", typeof(string));
            newDt.Columns.Add("FCCurrency", typeof(string));

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (DataRow row in dt.Rows)
                    {
                        foreach (DataColumn column in dt.Columns)
                        {

                            if (column.ToString() == "Seleccionado" && Convert.ToBoolean(row["Seleccionado"]) == true) //Verifica si la cuenta se selecciono
                            {
                               
                                using (SqlCommand cmd = Connection.CreateCommand())
                                {
                                    cmd.CommandText = "FindAccountBalance";

                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.Parameters.AddWithValue("@AcctCode", row["AcctCode"].ToString());

                                    cmd.Parameters.AddWithValue("@F_RefDate", dpDFechaContabilizacion);

                                    cmd.Parameters.AddWithValue("@T_RefDate", dpHFechaContabilizacion);

                                    SqlDataReader reader = cmd.ExecuteReader();

                                    int i = 0;

                                    while (reader.Read())
                                    {
                                        DataRow newRow = newDt.NewRow();

                                        newDt.Rows.Add(newRow);

                                        newRow["AcctCode"] = reader["AcctCode"].ToString();
                                        newRow["Name"] = reader["AcctCode"].ToString() + "-" + reader["AcctName"].ToString();
                                        newRow["Debit"] = reader["Debit"].ToString();                                        
                                        newRow["Credit"] = reader["Credit"].ToString();
                                        newRow["Saldo"] = reader["Saldo"].ToString();
                                        newRow["SYSDeb"] = reader["SYSDeb"].ToString();
                                        newRow["SYSCred"] = reader["SYSCred"].ToString();
                                        newRow["SaldoSYS"] = reader["SaldoSYS"].ToString();
                                        newRow["FCDebit"] = reader["FCDebit"].ToString();
                                        newRow["FCCredit"] = reader["FCCredit"].ToString();
                                        newRow["FCSaldo"] = reader["FCSaldo"].ToString();
                                        newRow["FCCurrency"] = reader["FCCurrency"].ToString();

                                        i++;

                                    }
                                    
                                    reader.Close();

                                    if (cuentasCero == false) { 

                                        if (i == 0)
                                        {
                                            DataRow newRow = newDt.NewRow();

                                            newDt.Rows.Add(newRow);

                                            newRow["AcctCode"] = row["AcctCode"].ToString();
                                            newRow["Name"] = row["AcctCode"].ToString() + "-" + row["AcctName"].ToString();
                                            newRow["Debit"] = 0;
                                            newRow["Credit"] = 0;
                                            newRow["Saldo"] = 0;
                                            newRow["SYSDeb"] = 0;
                                            newRow["SYSCred"] = 0;
                                            newRow["SaldoSYS"] = 0;
                                            newRow["FCDebit"] = 0;
                                            newRow["FCCredit"] = 0;
                                            newRow["FCSaldo"] = 0;
                                            newRow["FCCurrency"] = "";

                                        }
                                    }
                                }//end using

                            }//end if

                        }//end foreach column

                    }//end foreach row

                }//end using

                Connection.Close();

                return Tuple.Create(newDt, error);

            }
            catch (Exception e)
            {
                //DataSet ds = new DataSet();
                //DataTable dt=data.FillSchema(ds,SchemaType.Mapped,"");

                Connection.Close();

                return Tuple.Create(newDt, e.Message);
            }
        }

        
    }
}
