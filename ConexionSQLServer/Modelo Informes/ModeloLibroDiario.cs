using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ModeloLibroDiario : ConexionSQLServer
    {
        public Tuple<DataTable, string> ExecuteLibroDiario(DataTable dt, DateTime? dpHFechaContabilizacion, DateTime? dpDFechaContabilizacion = null)
        {
            DataTable dtTransId = new DataTable();

            DataTable newDt = new DataTable();

            newDt.Columns.Add("TransId", typeof(string));
            newDt.Columns.Add("RefDate", typeof(string));            
            newDt.Columns.Add("TransType", typeof(string));
            newDt.Columns.Add("BaseRef", typeof(string));
            newDt.Columns.Add("ShortName", typeof(string));
            newDt.Columns.Add("Account", typeof(string));
            newDt.Columns.Add("AcctName", typeof(string));
            newDt.Columns.Add("Saldo", typeof(decimal));
            newDt.Columns.Add("LineMemo", typeof(string));           
            newDt.Columns.Add("Debit", typeof(decimal));
            newDt.Columns.Add("Credit", typeof(decimal));            
            newDt.Columns.Add("FCDebit", typeof(decimal));
            newDt.Columns.Add("FCCredit", typeof(decimal));
            newDt.Columns.Add("FCSaldo", typeof(decimal));
            newDt.Columns.Add("SYSDeb", typeof(decimal));
            newDt.Columns.Add("SYSCred", typeof(decimal));
            newDt.Columns.Add("SaldoSYS", typeof(decimal));            
            newDt.Columns.Add("Title", typeof(string));
            newDt.Columns.Add("Num", typeof(int));
            newDt.Columns.Add("FCCurrency", typeof(string));

            string error = null;

            var result =FindTransId(dt, dpHFechaContabilizacion, dpDFechaContabilizacion);

            if (result.Item2 == null)
            {
                dtTransId = result.Item1;

                dtTransId = RemoveDuplicateRows(result.Item1,"TransId");

                dtTransId = OrderBy(dtTransId, "TransId", "ASC");

                try
                {

                    using (Connection = new SqlConnection(connectionString))
                    {

                        Connection.Open();

                        int i = 0;

                        foreach (DataRow row in dtTransId.Rows)
                        {

                            foreach (DataColumn column in dtTransId.Columns)
                            {

                                if (column.ToString() == "TransId") 
                                {
                                    //Crear fila de cabecera

                                    DataRow newRow = newDt.NewRow();

                                    newDt.Rows.Add(newRow);

                                    newRow["TransId"] = row["TransId"].ToString();
                                    newRow["RefDate"] = row["RefDate"].ToString();                                  
                                    newRow["TransType"] = row["TransType"].ToString();
                                    newRow["BaseRef"] = row["BaseRef"].ToString();
                                    newRow["LineMemo"] = row["Memo"].ToString();
                                    newRow["ShortName"] = "";
                                    newRow["Account"] = "";
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
                                    newRow["Title"] = "N";
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
                                        cmd.CommandText = "FindLibroDiario";

                                        cmd.CommandType = CommandType.StoredProcedure;

                                        cmd.Parameters.AddWithValue("@TransId", Convert.ToInt32(row["TransId"]));
                                       
                                        SqlDataReader reader = cmd.ExecuteReader();

                                        while (reader.Read())
                                        {
                                            DataRow newRow1 = newDt.NewRow();

                                            newDt.Rows.Add(newRow1);

                                            newRow1["RefDate"] = reader["RefDate"].ToString();
                                            newRow1["BaseRef"] = "";
                                            newRow1["TransType"] = "";
                                            newRow1["TransId"] = "";
                                            newRow1["LineMemo"] = reader["LineMemo"].ToString();
                                            newRow1["ShortName"] = reader["ShortName"].ToString();
                                            newRow1["Account"] = reader["Account"].ToString();
                                            newRow1["AcctName"] = reader["AcctName"].ToString();
                                            newRow1["Debit"] = Convert.ToDecimal(reader["Debit"]);
                                            newRow1["Credit"] = Convert.ToDecimal(reader["Credit"]);
                                            newRow1["Saldo"] = Convert.ToDecimal(reader["Saldo"]);
                                            newRow1["FCDebit"] = Convert.ToDecimal(reader["FCDebit"]);
                                            newRow1["FCCredit"] = Convert.ToDecimal(reader["FCCredit"]);
                                            newRow1["FCSaldo"] = Convert.ToDecimal(reader["FCSaldo"]);
                                            newRow1["FCCurrency"] = reader["FCCurrency"].ToString();
                                            newRow1["SYSCred"] = Convert.ToDecimal(reader["SYSCred"]);
                                            newRow1["SYSDeb"] = Convert.ToDecimal(reader["SYSDeb"]);
                                            newRow1["SaldoSYS"] = Convert.ToDecimal(reader["SaldoSYS"]);                                            
                                            newRow1["Title"] = "Y";

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
            else
            {

                return Tuple.Create(newDt, result.Item2);
            }

        }

        public Tuple<DataTable, string> FindTransId(DataTable dt, DateTime? dpHFechaContabilizacion, DateTime? dpDFechaContabilizacion = null)
        {
            DataTable dtTransId = new DataTable();

            dtTransId.Columns.Add("TransId", typeof(int));
            dtTransId.Columns.Add("RefDate", typeof(string));
            dtTransId.Columns.Add("TransType", typeof(string));
            dtTransId.Columns.Add("BaseRef", typeof(string));
            dtTransId.Columns.Add("Memo", typeof(string));

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

                            if (column.ToString() == "Seleccionado" && Convert.ToBoolean(row["Seleccionado"])) //Verifica el nombre de la cuenta
                            {
                                
                                using (SqlCommand cmd = Connection.CreateCommand())
                                {
                                    cmd.CommandText = "FindTransIdLD";

                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.Parameters.AddWithValue("@AcctCode", row["AcctCode"].ToString());

                                    cmd.Parameters.AddWithValue("@F_RefDate", dpDFechaContabilizacion);

                                    cmd.Parameters.AddWithValue("@T_RefDate", dpHFechaContabilizacion);

                                    SqlDataReader reader = cmd.ExecuteReader();

                                    while (reader.Read())
                                    {
                                        DataRow newRow1 = dtTransId.NewRow();

                                        dtTransId.Rows.Add(newRow1);
                                       
                                        newRow1["TransId"] = Convert.ToInt32(reader["TransId"]);
                                        newRow1["TransType"] = reader["TransType"].ToString();
                                        newRow1["RefDate"] = reader["RefDate"].ToString();
                                        newRow1["BaseRef"] = reader["BaseRef"].ToString();
                                        newRow1["Memo"] = reader["Memo"].ToString();


                                    }

                                    reader.Close();

                                   
                                }//end using

                            }//end if

                        }//end foreach column                      

                    }//end foreach row


                }//end using

                Connection.Close();

                return Tuple.Create(dtTransId, error);
            }
             catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(dtTransId, e.Message);
            }
        }

        public DataTable RemoveDuplicateRows(DataTable dTable, string colName)
        {
            Hashtable hTable = new Hashtable();
            ArrayList duplicateList = new ArrayList();

            //Add list of all the unique item value to hashtable, which stores combination of key, value pair.
            //And add duplicate item value in arraylist.
            foreach (DataRow drow in dTable.Rows)
            {
                if (hTable.Contains(drow[colName]))
                    duplicateList.Add(drow);
                else
                    hTable.Add(drow[colName], string.Empty);
            }

            //Removing a list of duplicate items from datatable.
            foreach (DataRow dRow in duplicateList)
                dTable.Rows.Remove(dRow);

            //Datatable which contains unique records will be return as output.
            return dTable;
        }

        public DataTable OrderBy(DataTable dt, string colName, string direction)
        {
            DataTable dtOut = null;
            dt.DefaultView.Sort = colName + " " + direction;
            dtOut = dt.DefaultView.ToTable();
            return dtOut;
        }
    }
}
