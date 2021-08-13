using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Modelo_Informes
{
    public class ModeloTipoCambioAlternativo : ModeloDocumento
    {
       
        public Tuple<DataTable,string> ExecuteExchangeRateDifference(string cuentaGanancia, string cuentaPerdida, DataTable dt, decimal rate, DateTime? dFechaVencimiento, DateTime? hFechaVencimiento)
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

                    foreach (DataRow row in dt.Rows)
                    {
                       
                            string acctCode = row["AcctCode"].ToString();

                                using (SqlCommand cmd = Connection.CreateCommand())
                                {
                                    cmd.CommandText = "GenerateExchangeRateDifferenceAccountAlternate";

                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.Parameters.AddWithValue("@AccountGanancia", cuentaGanancia);

                                    cmd.Parameters.AddWithValue("@AccountPerdida", cuentaPerdida);
                                
                                    cmd.Parameters.AddWithValue("@F_DueDate", dFechaVencimiento);

                                    cmd.Parameters.AddWithValue("@T_DueDate", hFechaVencimiento);

                                    cmd.Parameters.AddWithValue("@AcctCode", row["AcctCode"].ToString());

                                    cmd.Parameters.AddWithValue("@Rate", rate);

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
                                        newRow["Rate"] = rate;
                                        newRow["FCCurrency"] = reader["FCCurrency"].ToString();
                                        newRow["Type"] = reader["Type"].ToString();
                                        newRow["AcctName"] = reader["AcctName"].ToString();

                                    }

                                    reader.Close();

                                }//end using

                     

                    }//end foreach row

                    //Consultar Cuentas Asociadas y Socios de Negocio

                    //using (SqlCommand cmd = Connection.CreateCommand())
                    //{
                    //    cmd.CommandText = "GenerateExchangeRateDifferenceBP";

                    //    cmd.CommandType = CommandType.StoredProcedure;

                    //    cmd.Parameters.AddWithValue("@SupplierGanancia", cuentaGanancia);

                    //    cmd.Parameters.AddWithValue("@SupplierPerdida", cuentaPerdida);

                    //    cmd.Parameters.AddWithValue("@CustomerGanancia", cuentaGanancia);

                    //    cmd.Parameters.AddWithValue("@CustomerPerdida", cuentaPerdida);

                    //    cmd.Parameters.AddWithValue("@StarBP", DesdeSN);

                    //    cmd.Parameters.AddWithValue("@EndBP", HastaSN);

                    //    cmd.Parameters.AddWithValue("@RefDate", fechaEjecucion);

                    //    cmd.Parameters.AddWithValue("@F_DueDate", dFechaVencimiento);

                    //    cmd.Parameters.AddWithValue("@T_DueDate", hFechaVencimiento);

                    //    SqlDataReader reader = cmd.ExecuteReader();

                    //    while (reader.Read())
                    //    {
                    //        DataRow newRow = newDt.NewRow();

                    //        newDt.Rows.Add(newRow);

                    //        newRow["Account"] = reader["Account"].ToString();
                    //        newRow["ShortName"] = reader["ShortName"].ToString();
                    //        newRow["Saldo"] = Convert.ToDecimal(reader["Saldo"]);
                    //        newRow["SaldoME"] = Convert.ToDecimal(reader["SaldoME"]);
                    //        newRow["Diferencia"] = Convert.ToDecimal(reader["Diferencia"]);
                    //        newRow["Rate"] = Convert.ToDecimal(reader["Rate"]);
                    //        newRow["FCCurrency"] = reader["FCCurrency"].ToString();
                    //        newRow["Type"] = reader["Type"].ToString();
                    //        newRow["AcctName"] = reader["AcctName"].ToString();

                    //    }

                    //    reader.Close();

                    //}//end using

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

        public string DeleteJournalEntrysTest()
        {            
            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();
                  
                     using (SqlCommand cmd = Connection.CreateCommand())
                     {

                            cmd.CommandText = "DeleteJournalEntrysTests";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.ExecuteNonQuery();
                    }
                   

                }

                Connection.Close();

                return error;

            }
            catch (Exception e)
            {

                Connection.Close();

                return e.Message ;
            }
        }

      
        public Tuple<DataTable,string> ExecuteBalanceAlternate(DataTable dt, DateTime? dpDesde, DateTime? dpHasta, bool? cuentaCero)
        {


            DataTable newDt = new DataTable();

            newDt.Columns.Add("Name", typeof(string));
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
                      
                                using (SqlCommand cmd = Connection.CreateCommand())
                                {
                                    cmd.CommandText = "FindAccountBalanceAlternate";

                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.Parameters.AddWithValue("@AcctCode", row["AcctCode"].ToString());

                                    cmd.Parameters.AddWithValue("@F_RefDate", dpDesde);

                                    cmd.Parameters.AddWithValue("@T_RefDate", dpHasta);

                                    SqlDataReader reader = cmd.ExecuteReader();

                                    int i = 0;

                                    while (reader.Read())
                                    {
                                        DataRow newRow = newDt.NewRow();

                                        newDt.Rows.Add(newRow);

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

                                    if (cuentaCero == false)
                                    {

                                        if (i == 0)
                                        {
                                            DataRow newRow = newDt.NewRow();

                                            newDt.Rows.Add(newRow);

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

        public DataTable GetJDT1()
        {
           

            return dtJDT1;
        }

        public DataTable GetOJDT()
        {
            return dtOJDT;
        }

        

        public Tuple<DataTable, string> ExecuteExchangeConversion(string cuentaBeneficio, string cuentaPerdida, DataTable dt, decimal rate)
        {
            //    var JoinResult = (from ojdt in dtOJDT.AsEnumerable()
            //                      join jdt1 in dtJDT1.AsEnumerable()
            //                      on ojdt.Field<int>("TransId") equals jdt1.Field<int>("TransId")
            //                      join oact in dtOACT.AsEnumerable() on jdt1.Field<string>("Account") equals oact.Field<string>("AcctCode") into group1
            //                      from g1 in group1.DefaultIfEmpty()
            //                      join ocrd in dtOCRD.AsEnumerable() on jdt1.Field<string>("ShortName") equals ocrd.Field<string>("CardCode") into group2
            //                      from g2 in group2.DefaultIfEmpty()
            //                      where (jdt1.Field<string>("ShortName") == "AcctCode" && jdt1.Field<DateTime>("RefDate") <= DateTime.Now && g1.Field<string>("ActType") == "N")
            //                      group new { ojdt, jdt1, g1, g2 } by new { ShortName = ojdt.Field<string>("ShortName"), Account = ojdt.Field<string>("Account"), AcctName = g1.Field<string>("AcctName"), CardType = g1.Field<string>("CardType") } into grp
            //                      where grp.Sum(x => x.Field<decimal>("population")) > 1000000
            //                      select new
            //                      {
            //                        A=  grp.Sum(x => x.Field<decimal>("population"))
            //                          //EmpId = grp.Population<int>("EmpId"),
            //                          //EmpName = p.Field<string>("EmpName"),
            //                          //Grade = leftJoin == null ? 0 : leftJoin.Field<int>("Grade")
            //                      }
            //                           into x
            //                      group x by x.bookingid into g
            //                      select g); ;


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

                    foreach (DataRow row in dt.Rows)
                    {
                       
                                using (SqlCommand cmd = Connection.CreateCommand())
                                {
                                    cmd.CommandText = "GenerateExchangeConversionAccountAlternate";

                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.Parameters.AddWithValue("@Rate", rate);

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

                     

                    }//end foreach row

                    ////Consultar Cuentas Asociadas y Socios de Negocio

                    //using (SqlCommand cmd = Connection.CreateCommand())
                    //{
                    //    cmd.CommandText = "GenerateExchangeConversionBP";

                    //    cmd.CommandType = CommandType.StoredProcedure;

                    //    cmd.Parameters.AddWithValue("@StarBP", DesdeSN);

                    //    cmd.Parameters.AddWithValue("@EndBP", HastaSN);

                    //    cmd.Parameters.AddWithValue("@RefDate", fechaEjecucion);

                    //    SqlDataReader reader = cmd.ExecuteReader();

                    //    while (reader.Read())
                    //    {
                    //        DataRow newRow = newDt.NewRow();

                    //        newDt.Rows.Add(newRow);

                    //        newRow["Account"] = reader["Account"].ToString();
                    //        newRow["ShortName"] = reader["ShortName"].ToString();
                    //        newRow["Saldo"] = Convert.ToDecimal(reader["Saldo"]);
                    //        newRow["SaldoMS"] = Convert.ToDecimal(reader["SaldoMS"]);
                    //        newRow["SaldoConvertido"] = Convert.ToDecimal(reader["SaldoConvertido"]);
                    //        newRow["Diferencia"] = Convert.ToDecimal(reader["Diferencia"]);
                    //        newRow["Type"] = reader["Type"].ToString();
                    //        newRow["AcctName"] = reader["AcctName"].ToString();

                    //    }

                    //    reader.Close();

                    //}//end using

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

        public string LoadTables()
        {
            string error = null;
            try
            {
                dtOJDT = dataSet.Tables["OJDT_TEST"];
                dtJDT1 = dataSet.Tables["JDT1_TEST"];
                //dtOACT = dataSet.Tables["OACT_TEST"];
                //dtORTT = dataSet.Tables["ORTT_TEST"];
                //dtOCRD = dataSet.Tables["OCRD_TEST"];

                return error;


            }
            
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        public Tuple<int, string> SelectTransIdPreliminar()
        {
            int TransId = 0;

            string error = null;

            try
            {

                DataRow dataRow = dtOJDT.AsEnumerable().OrderByDescending(x => x.Field<int>("TransId")).First();

                TransId = Convert.ToInt32(dataRow["TransId"])+1;

                return Tuple.Create(TransId, error);

            }
            catch (Exception e)
            {

                return Tuple.Create(TransId, e.Message);

            }
        }

        public string CreateDatasetPreliminarTipoCambio()
        {
            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    dataSet = new DataSet("ds");

                    data = new SqlDataAdapter("SELECT * FROM dbo.OJDT", Connection);

                    data.FillSchema(dataSet, SchemaType.Source, "OJDT_TEST");

                    data.Fill(dataSet, "OJDT_TEST");

                    data = new SqlDataAdapter("SELECT * FROM dbo.JDT1", Connection);

                    data.FillSchema(dataSet, SchemaType.Source, "JDT1_TEST");

                    data.Fill(dataSet, "JDT1_TEST");

                    //data = new SqlDataAdapter("SELECT * FROM dbo.ORTT", Connection);

                    //data.FillSchema(dataSet, SchemaType.Source, "ORTT_TEST");

                    //data.Fill(dataSet, "ORTT_TEST");

                    //data = new SqlDataAdapter("SELECT * FROM dbo.OACT", Connection);

                    //data.FillSchema(dataSet, SchemaType.Source, "OACT_TEST");

                    //data.Fill(dataSet, "OACT_TEST");

                    //data = new SqlDataAdapter("SELECT * FROM dbo.OCRD", Connection);

                    //data.FillSchema(dataSet, SchemaType.Source, "OCRD_TEST");

                    //data.Fill(dataSet, "OCRD_TEST");                    

                    CreateConstraintJournalEntry("OJDT_TEST", "JDT1_TEST");

                    CreateDataRelationJournalEntry("OJDT_TEST", "JDT1_TEST");      

                    dataSet.AcceptChanges();

                }

                Connection.Close();

                return error;

            }
            catch (Exception e)
            {

                Connection.Close();

                return e.Message;
            }
        }
    }
}
