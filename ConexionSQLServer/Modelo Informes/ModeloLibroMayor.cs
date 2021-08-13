using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ModeloLibroMayor : ModeloReportes
    {
        public Tuple<DataTable, string> ExecuteLibroMayor(DataTable dt, DateTime? dpHFechaContabilizacion, DateTime? dpDFechaContabilizacion = null)
        {
            DataTable newDt = new DataTable();

            newDt.Columns.Add("RefDate", typeof(string));
            newDt.Columns.Add("DueDate", typeof(string));
            newDt.Columns.Add("TaxDate", typeof(string));           
            newDt.Columns.Add("TransType", typeof(string));
            newDt.Columns.Add("BaseRef", typeof(string));
            newDt.Columns.Add("TransId", typeof(string));
            newDt.Columns.Add("Memo", typeof(string));
            newDt.Columns.Add("ContraAct", typeof(string));
            newDt.Columns.Add("AcctName", typeof(string));
            newDt.Columns.Add("Debit", typeof(decimal));
            newDt.Columns.Add("Credit", typeof(decimal));
            newDt.Columns.Add("Saldo", typeof(decimal));
            newDt.Columns.Add("FCDebit", typeof(decimal));
            newDt.Columns.Add("FCCredit", typeof(decimal));
            newDt.Columns.Add("FCSaldo", typeof(decimal));
            newDt.Columns.Add("SYSDeb", typeof(decimal));
            newDt.Columns.Add("SYSCred", typeof(decimal));
            newDt.Columns.Add("SaldoSYS", typeof(decimal));
            newDt.Columns.Add("SaldoAcum", typeof(decimal));
            newDt.Columns.Add("FCSaldoAcum", typeof(decimal));
            newDt.Columns.Add("SaldoSYSAcum", typeof(decimal));
            newDt.Columns.Add("FCCurrency", typeof(string));
            newDt.Columns.Add("Title", typeof(string));
            newDt.Columns.Add("Num", typeof(int));

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    int i = 0;

                    foreach (DataRow row in dt.Rows)
                    {                        

                        foreach (DataColumn column in dt.Columns)
                        {

                            if (column.ToString() == "Seleccionado" && Convert.ToBoolean(row["Seleccionado"]) == true) //Verifica si la cuenta se selecciono
                            {
                                //Crear fila de cabecera

                                DataRow newRow = newDt.NewRow();

                                newDt.Rows.Add(newRow);

                                newRow["RefDate"] = "";
                                newRow["TaxDate"] = "";                                
                                newRow["TransType"] = "";
                                newRow["BaseRef"] = "";
                                newRow["TransId"] = "";
                                newRow["DueDate"] = row["AcctCode"].ToString();
                                newRow["Memo"] = row["AcctName"].ToString();
                                newRow["ContraAct"] = "";
                                newRow["AcctName"] = "";
                                newRow["Debit"] = 0;
                                newRow["Credit"] = 0;
                                newRow["Saldo"] = 0;
                                newRow["FCDebit"] = 0;
                                newRow["FCCredit"] = 0;
                                newRow["FCSaldo"] = 0;
                                newRow["SYSCred"] = 0;
                                newRow["SYSDeb"] = 0;
                                newRow["SaldoSYS"] = 0;
                                newRow["SaldoAcum"] = 0;
                                newRow["FCSaldoAcum"] = 0;
                                newRow["SaldoSYSAcum"] = 0;
                                newRow["Title"] = "Y";
                                newRow["FCCurrency"] = "";

                                if (i % 2 == 0)
                                {
                                    newRow["Num"] = 0;
                                }
                                else
                                {
                                    newRow["Num"] = 1;
                                }
                                
                                //------------------------------------------------------

                                using (SqlCommand cmd = Connection.CreateCommand())
                                {
                                    cmd.CommandText = "FindAccountLibroMayor";

                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.Parameters.AddWithValue("@AcctCode", row["AcctCode"].ToString());

                                    cmd.Parameters.AddWithValue("@F_RefDate", dpDFechaContabilizacion);

                                    cmd.Parameters.AddWithValue("@T_RefDate", dpHFechaContabilizacion);

                                    SqlDataReader reader = cmd.ExecuteReader();
                                 
                                    while (reader.Read())
                                    {
                                        DataRow newRow1 = newDt.NewRow();

                                        newDt.Rows.Add(newRow1);

                                        newRow1["RefDate"] = reader["RefDate"].ToString();
                                        newRow1["DueDate"] = reader["DueDate"].ToString();
                                        newRow1["TaxDate"] = reader["TaxDate"].ToString();
                                        newRow1["BaseRef"] = reader["BaseRef"].ToString();
                                        newRow1["TransType"] = reader["TransType"].ToString();                                   
                                        newRow1["TransId"] = reader["TransId"].ToString();
                                        newRow1["Memo"] = reader["Memo"].ToString();
                                        newRow1["ContraAct"] = reader["ContraAct"].ToString();
                                        newRow1["AcctName"] = reader["AcctName"].ToString();
                                        newRow1["Debit"] = Convert.ToDecimal(reader["Debit"]);
                                        newRow1["Credit"] = Convert.ToDecimal(reader["Credit"]);
                                        newRow1["Saldo"] =Convert.ToDecimal(reader["Saldo"]);
                                        newRow1["FCDebit"] =Convert.ToDecimal(reader["FCDebit"]);
                                        newRow1["FCCredit"] = Convert.ToDecimal(reader["FCCredit"]);
                                        newRow1["FCSaldo"] = Convert.ToDecimal(reader["FCSaldo"]);
                                        newRow1["SYSCred"] = Convert.ToDecimal(reader["SYSCred"]);
                                        newRow1["SYSDeb"] = Convert.ToDecimal(reader["SYSDeb"]);
                                        newRow1["SaldoSYS"] = Convert.ToDecimal(reader["SaldoSYS"]);
                                        newRow1["SaldoAcum"] = Convert.ToDecimal(newDt.Rows[newDt.Rows.Count - 2]["SaldoAcum"]) + Convert.ToDecimal(newRow1["Saldo"]);
                                        newRow1["FCSaldoAcum"] = Convert.ToDecimal(newDt.Rows[newDt.Rows.Count - 2]["FCSaldoAcum"]) + Convert.ToDecimal(newRow1["FCSaldo"]);
                                        newRow1["SaldoSYSAcum"] = Convert.ToDecimal(newDt.Rows[newDt.Rows.Count - 2]["SaldoSYSAcum"]) + Convert.ToDecimal(newRow1["SaldoSYS"]);
                                        newRow1["FCCurrency"]= reader["FCCurrency"].ToString();
                                        newRow1["Title"] = "N";

                                        if (i % 2 == 0)
                                        {
                                            newRow1["Num"] = 0;
                                        }
                                        else
                                        {
                                            newRow1["Num"] = 1;
                                        }
                                        
                                    }

                                    reader.Close();

                                    //Agrega fila en blanco

                                    DataRow newRow2 = newDt.NewRow();

                                    newDt.Rows.Add(newRow2);

                                    newRow2["RefDate"] = "";
                                    newRow2["DueDate"] = "";
                                    newRow2["TaxDate"] = "";                                    
                                    newRow2["TransType"] = "";
                                    newRow2["BaseRef"] = "";
                                    newRow2["TransId"] = "";
                                    newRow2["Memo"] = "";
                                    newRow2["ContraAct"] = "";
                                    newRow2["AcctName"] = "";
                                    newRow2["Debit"] =0;
                                    newRow2["Credit"] = 0;
                                    newRow2["Saldo"] = 0;
                                    newRow2["FCDebit"] = 0;
                                    newRow2["FCCredit"] = 0;
                                    newRow2["FCSaldo"] = 0;
                                    newRow2["SYSCred"] = 0;
                                    newRow2["SYSDeb"] = 0;
                                    newRow2["SaldoSYS"] = 0;
                                    newRow2["SaldoAcum"] = 0;
                                    newRow2["FCSaldoAcum"] = 0;
                                    newRow2["SaldoSYSAcum"] = 0;
                                    newRow2["Title"] = "Y";
                                    newRow2["FCCurrency"] = "";

                                    if (i % 2 == 0)
                                    {
                                        newRow2["Num"] = 0;
                                    }
                                    else
                                    {
                                        newRow2["Num"] = 1;
                                    }
                                    

                                }//end using
                                i++;
                            }//end if

                        }//end foreach column

                       

                    }//end foreach row
                   

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
