using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Modelo_Socio_Negocio
{
    public class ModeloReconciliacionSN : ConexionSQLServer
    {
        public Tuple<DataTable,string> ExecuteReconciliacionSN(string SN, DateTime? f_RefDate=null, DateTime? t_RefDate=null)
        {
            DataTable newDt = new DataTable();

            newDt.Columns.Add("TransId", typeof(int));
            newDt.Columns.Add("TransType", typeof(string));
            newDt.Columns.Add("Importe", typeof(decimal));
            newDt.Columns.Add("SaldoVencido", typeof(decimal));
            newDt.Columns.Add("ImporteReconciliar", typeof(decimal));
            newDt.Columns.Add("SaldoME", typeof(decimal));
            newDt.Columns.Add("RefDate", typeof(string));
            newDt.Columns.Add("SaldoMS", typeof(decimal));
            newDt.Columns.Add("LineMemo", typeof(string));
            newDt.Columns.Add("Account", typeof(string));
            newDt.Columns.Add("ShortName", typeof(string));
            newDt.Columns.Add("FCCurrency", typeof(string));
            newDt.Columns.Add("FinncPriod", typeof(int));
            newDt.Columns.Add("BaseRef", typeof(int));
            newDt.Columns.Add("Line_ID", typeof(int));
            newDt.Columns.Add("WTSum", typeof(decimal));
            newDt.Columns.Add("WTSumSC", typeof(decimal));
            newDt.Columns.Add("WTSumFC", typeof(decimal));


            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                      
                        cmd.CommandText = "GenerateInternalReconciliation";

                        cmd.CommandType = CommandType.StoredProcedure;                       

                        cmd.Parameters.AddWithValue("@SN", SN);

                        cmd.Parameters.AddWithValue("@F_RefDate", f_RefDate);

                        cmd.Parameters.AddWithValue("@T_RefDate", t_RefDate);

                        SqlDataReader reader = cmd.ExecuteReader();

                         while (reader.Read())
                         {
                            DataRow newRow = newDt.NewRow();

                            newDt.Rows.Add(newRow);

                            newRow["TransId"] = Convert.ToInt32(reader["TransId"].ToString());
                            newRow["TransType"] = reader["TransType"].ToString();
                            newRow["Importe"] = Convert.ToDecimal(reader["Importe"]);
                            newRow["SaldoVencido"] = Convert.ToDecimal(reader["SaldoVencido"]);
                            newRow["ImporteReconciliar"] = Convert.ToDecimal(reader["ImporteReconciliar"]);
                            newRow["SaldoME"] = Convert.ToDecimal(reader["SaldoME"]);
                            newRow["SaldoMS"] = Convert.ToDecimal(reader["SaldoMS"]);
                            newRow["LineMemo"] = reader["LineMemo"].ToString();
                            newRow["ShortName"] = reader["ShortName"].ToString();
                            newRow["Account"] = reader["Account"].ToString();
                            newRow["FCCurrency"] = reader["FCCurrency"].ToString();
                            newRow["FinncPriod"] = Convert.ToInt32(reader["FinncPriod"].ToString());
                            newRow["BaseRef"] = Convert.ToInt32(reader["BaseRef"].ToString());
                            newRow["RefDate"] = reader["RefDate"].ToString();
                            newRow["Line_ID"] = Convert.ToInt32(reader["Line_ID"]);

                            try
                            {
                                newRow["WTSum"] = Convert.ToDecimal(reader["WTSum"]);
                                newRow["WTSumFC"] = Convert.ToDecimal(reader["WTSumFC"]);
                                newRow["WTSumSC"] = Convert.ToDecimal(reader["WTSumSC"]);
                            }
                            catch(Exception ex)
                            {
                                newRow["WTSum"] = 0;
                                newRow["WTSumFC"] = 0;
                                newRow["WTSumSC"] = 0;

                            }

                          

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

        public Tuple<int, string> UpdateJournalEntryLinesReverse(List<ReconciliacionInternaDetalles> reconciliacionInternaDetallesLines)
        {
            throw new NotImplementedException();
        }

        public Tuple<int, string> SelectReconNum()
        {
            int reconNum = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "SelectReconNum";

                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            reconNum = Convert.ToInt32(reader["ReconNum"]);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(reconNum, error);

            }
            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(reconNum, e.Message);

            }
        }

        public Tuple<int, string> InsertReconciliationInternal(List<ReconciliacionInternaCabecera> listaInternalReconciliation)
        {
            int flag = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (ReconciliacionInternaCabecera reconciliacionInternaCabecera in listaInternalReconciliation)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "InsertReconciliationInternal";

                            cmd.CommandType = CommandType.StoredProcedure;
                           
                            cmd.Parameters.AddWithValue("@IsCard", reconciliacionInternaCabecera.IsCard);
                            cmd.Parameters.AddWithValue("@ReconType", reconciliacionInternaCabecera.ReconType);
                            cmd.Parameters.AddWithValue("@ReconDate", reconciliacionInternaCabecera.ReconDate);
                            cmd.Parameters.AddWithValue("@Total", reconciliacionInternaCabecera.Total);
                            cmd.Parameters.AddWithValue("@ReconCurr", reconciliacionInternaCabecera.ReconCurr);
                            cmd.Parameters.AddWithValue("@CancelAbs", reconciliacionInternaCabecera.CancelAbs);
                            cmd.Parameters.AddWithValue("@Canceled", reconciliacionInternaCabecera.Canceled);
                            cmd.Parameters.AddWithValue("@IsSystem", reconciliacionInternaCabecera.IsSystem);

                            if (reconciliacionInternaCabecera.InitObjTyp == null)
                            {
                                cmd.Parameters.AddWithValue("@InitObjTyp", DBNull.Value);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@InitObjTyp", reconciliacionInternaCabecera.InitObjTyp);
                            }
                            
                            cmd.Parameters.AddWithValue("@InitObjAbs", reconciliacionInternaCabecera.InitObjAbs);
                            cmd.Parameters.AddWithValue("@CreateDate", reconciliacionInternaCabecera.CreateDate);
                            cmd.Parameters.AddWithValue("@UserSign", reconciliacionInternaCabecera.UserSign);
                            cmd.Parameters.AddWithValue("@ReconJEId", reconciliacionInternaCabecera.ReconJEId);
                            cmd.Parameters.AddWithValue("@ObjType", reconciliacionInternaCabecera.ObjType);

                            flag = cmd.ExecuteNonQuery();

                        }
                    }

                }

                Connection.Close();

                return Tuple.Create(flag, error);

            }
            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(flag, e.Message);
            }
        }

        public Tuple<int, string> UpdateDocument(ReconciliacionInternaDetalles ReconciliationInternalLines)
        {
            int flag = 0;

            int i = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "UpdateDocumentReconciliation";

                            cmd.CommandType = CommandType.StoredProcedure;
                           
                            cmd.Parameters.AddWithValue("@TransId", ReconciliationInternalLines.TransId);                           
                            cmd.Parameters.AddWithValue("@SrcObjTyp", ReconciliationInternalLines.SrcObjTyp);                          
                            cmd.Parameters.AddWithValue("@ReconSum", ReconciliationInternalLines.ReconSum);
                            cmd.Parameters.AddWithValue("@ReconSumFC", ReconciliationInternalLines.ReconSumFC);
                            cmd.Parameters.AddWithValue("@ReconSumSC", ReconciliationInternalLines.ReconSumSC);                           
                            cmd.Parameters.AddWithValue("@WTSum", ReconciliationInternalLines.WTSum);
                            cmd.Parameters.AddWithValue("@WTSumFC", ReconciliationInternalLines.WTSumFC);
                            cmd.Parameters.AddWithValue("@WTSumSC", ReconciliationInternalLines.WTSumSC);
                            
                            flag = cmd.ExecuteNonQuery();

                            i = flag + i;
                        }
                    

                }

                Connection.Close();

                return Tuple.Create(i, error);

            }
            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(i, e.Message);
            }
        }

        public Tuple<int, string> InsertReconciliationInternalLines(List<ReconciliacionInternaDetalles> listReconciliationInternalLines)
        {
            int flag = 0;

            int i = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (ReconciliacionInternaDetalles ReconciliationInternalLines in listReconciliationInternalLines)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "InsertReconciliationInternalLines";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@ReconNum", ReconciliationInternalLines.ReconNum);
                            cmd.Parameters.AddWithValue("@LineSeq", ReconciliationInternalLines.LineSeq);
                            cmd.Parameters.AddWithValue("@TransId", ReconciliationInternalLines.TransId);
                            cmd.Parameters.AddWithValue("@Account", ReconciliationInternalLines.Account);
                            cmd.Parameters.AddWithValue("@ShortName", ReconciliationInternalLines.ShortName);                           
                            cmd.Parameters.AddWithValue("@TransRowId", ReconciliationInternalLines.TransRowId);
                            cmd.Parameters.AddWithValue("@SrcObjTyp", ReconciliationInternalLines.SrcObjTyp);
                            cmd.Parameters.AddWithValue("@SrcObjAbs", ReconciliationInternalLines.SrcObjAbs);
                            cmd.Parameters.AddWithValue("@ReconSum", ReconciliationInternalLines.ReconSum);
                            cmd.Parameters.AddWithValue("@ReconSumFC", ReconciliationInternalLines.ReconSumFC);
                            cmd.Parameters.AddWithValue("@ReconSumSC", ReconciliationInternalLines.ReconSumSC);
                            cmd.Parameters.AddWithValue("@FrgnCurr", ReconciliationInternalLines.FrgnCurr);
                            cmd.Parameters.AddWithValue("@SumMthCurr", ReconciliationInternalLines.SumMthCurr);
                            cmd.Parameters.AddWithValue("@IsCredit", ReconciliationInternalLines.IsCredit);
                            cmd.Parameters.AddWithValue("@WTSum", ReconciliationInternalLines.WTSum);
                            cmd.Parameters.AddWithValue("@WTSumFC", ReconciliationInternalLines.WTSumFC);
                            cmd.Parameters.AddWithValue("@WTSumSC", ReconciliationInternalLines.WTSumSC);
                            cmd.Parameters.AddWithValue("@ExpSum", ReconciliationInternalLines.ExpSum);
                            cmd.Parameters.AddWithValue("@ExpSumFC", ReconciliationInternalLines.ExpSumFC);
                            cmd.Parameters.AddWithValue("@ExpSumSC", ReconciliationInternalLines.ExpSumSC);
                            

                            flag = cmd.ExecuteNonQuery();

                            i = flag + i;
                        }
                    }

                }

                Connection.Close();

                return Tuple.Create(i, error);

            }
            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(i, e.Message);
            }
        }

        public Tuple<int, string> DeleteOITR(int reconNum)
        {
            throw new NotImplementedException();
        }

        public Tuple<int, string> DeleteITR1(int reconNum)
        {
            throw new NotImplementedException();
        }

        public Tuple<int, string> UpdateJournalEntryLines(List<ReconciliacionInternaDetalles> listReconciliationDetails)
        {
            int flag = 0;

            int i = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (ReconciliacionInternaDetalles ReconciliationInternalLines in listReconciliationDetails)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "UpdateJournalEntryLinesReconciliation";

                            cmd.CommandType = CommandType.StoredProcedure;
                           
                            cmd.Parameters.AddWithValue("@TransId", ReconciliationInternalLines.TransId);
                            cmd.Parameters.AddWithValue("@Account", ReconciliationInternalLines.Account);
                            cmd.Parameters.AddWithValue("@ShortName", ReconciliationInternalLines.ShortName);
                            cmd.Parameters.AddWithValue("@TransRowId", ReconciliationInternalLines.TransRowId);                           
                            cmd.Parameters.AddWithValue("@ReconSum", ReconciliationInternalLines.ReconSum);
                            cmd.Parameters.AddWithValue("@ReconSumFC", ReconciliationInternalLines.ReconSumFC);
                            cmd.Parameters.AddWithValue("@ReconSumSC", ReconciliationInternalLines.ReconSumSC);                            
                            cmd.Parameters.AddWithValue("@IsCredit", ReconciliationInternalLines.IsCredit);
                            cmd.Parameters.AddWithValue("@WTSum", ReconciliationInternalLines.WTSum);
                            cmd.Parameters.AddWithValue("@WTSumFC", ReconciliationInternalLines.WTSumFC);
                            cmd.Parameters.AddWithValue("@WTSumSC", ReconciliationInternalLines.WTSumSC);    

                            flag = cmd.ExecuteNonQuery();

                            i = flag + i;
                        }
                    }

                }

                Connection.Close();

                return Tuple.Create(i, error);

            }
            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(i, e.Message);
            }
        }

        public Tuple<DataTable, string> GetReconciliationInternalLines(int reconNum)
        {
            DataTable tabla = new DataTable();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "GetReconciliationInternalDiferenceLines";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ReconNum", reconNum);

                        data = new SqlDataAdapter(cmd);

                        data.Fill(tabla);

                    }

                }

                Connection.Close();

                return Tuple.Create(tabla, error);

            }
            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(tabla, e.Message);
            }
        }

        public Tuple<decimal, string> VerifiedDiferenceReconciliation(int reconNum)
        {
            decimal ReconSumTransSC = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "ExecuteFuncionReconciliationInternalDiference";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ReconNum", reconNum);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            ReconSumTransSC = Convert.ToDecimal(reader["ReconSumTransSC"]);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(ReconSumTransSC, error);

            }
            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(ReconSumTransSC, e.Message);

            }
        }
    }
}
