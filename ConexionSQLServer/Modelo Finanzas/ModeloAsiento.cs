using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Datos
{
    public class ModeloAsiento : ModeloDocumento
    {
        public Tuple<List<AsientoCabecera>, string> FindLastJournalEntry()
        {
            List<AsientoCabecera> newListJournalEntry = new List<AsientoCabecera>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "LastJournalEntry";

                        cmd.CommandType = CommandType.StoredProcedure;                       

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            AsientoCabecera newJournalEntry = new AsientoCabecera();

                            newJournalEntry.TransId = Convert.ToInt32(reader["TransId"]);
                            newJournalEntry.RefDate = Convert.ToDateTime(reader["RefDate"]);
                            newJournalEntry.TaxDate = Convert.ToDateTime(reader["TaxDate"]);
                            newJournalEntry.DueDate = Convert.ToDateTime(reader["DueDate"]);
                            newJournalEntry.Memo = reader["Memo"].ToString();
                            newJournalEntry.TransType = Convert.ToInt32(reader["TransType"]);
                            newJournalEntry.Ref1 = reader["Ref1"].ToString();
                            newJournalEntry.Ref2 = reader["Ref2"].ToString();
                            newJournalEntry.BaseRef = Convert.ToInt32(reader["BaseRef"]);

                            newListJournalEntry.Add(newJournalEntry);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(newListJournalEntry,error);

            }
            catch (Exception e)
            {
                Connection.Close();

                return Tuple.Create(newListJournalEntry, e.Message);
            }
        }

        public Tuple<List<AsientoCabecera>, string> FindJournalEntrySpecific(int transId)
        {
            List<AsientoCabecera> newListJournalEntry = new List<AsientoCabecera>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FindJournalEntrySpecific";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@TransId", transId);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            AsientoCabecera newJournalEntry = new AsientoCabecera();

                            newJournalEntry.TransId = Convert.ToInt32(reader["TransId"]);
                            newJournalEntry.RefDate = Convert.ToDateTime(reader["RefDate"]);
                            newJournalEntry.TaxDate = Convert.ToDateTime(reader["TaxDate"]);
                            newJournalEntry.DueDate = Convert.ToDateTime(reader["DueDate"]);
                            newJournalEntry.Memo = reader["Memo"].ToString();
                            newJournalEntry.TransType = Convert.ToInt32(reader["TransType"]);
                            newJournalEntry.Ref1 = reader["Ref1"].ToString();
                            newJournalEntry.Ref2 = reader["Ref2"].ToString();
                            newJournalEntry.BaseRef = Convert.ToInt32(reader["BaseRef"]);

                            newListJournalEntry.Add(newJournalEntry);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(newListJournalEntry, error);

            }
            catch (Exception e)
            {
                Connection.Close();

                return Tuple.Create(newListJournalEntry, e.Message);
            }
        }

        public Tuple<int,string> InsertJournalEntryComplete(List<AsientoCabecera> listJournalEntry, List<AsientoDetalle> listJournalEntryLines)
        {
            int flag = 0;

            string error = null;

            try
            {
                using (Connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        Connection.Open();

                        transaction = Connection.BeginTransaction(); //inicia la transaccion   

                        foreach (AsientoCabecera journalEntry in listJournalEntry)
                        {
                            using (SqlCommand cmd = new SqlCommand("InsertJournalEntry", Connection, transaction))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@RefDate", journalEntry.RefDate);
                                cmd.Parameters.AddWithValue("@TaxDate", journalEntry.TaxDate);
                                cmd.Parameters.AddWithValue("@DueDate", journalEntry.DueDate);
                                cmd.Parameters.AddWithValue("@Memo", journalEntry.Memo);
                                cmd.Parameters.AddWithValue("@TransCurr", journalEntry.TransCurr);
                                cmd.Parameters.AddWithValue("@Ref1", journalEntry.Ref1);
                                cmd.Parameters.AddWithValue("@Ref2", journalEntry.Ref2);
                                cmd.Parameters.AddWithValue("@UserSign", journalEntry.UserSign);
                                cmd.Parameters.AddWithValue("@CreateDate", journalEntry.UpdateDate);
                                cmd.Parameters.AddWithValue("@BaseRef", journalEntry.BaseRef);
                                cmd.Parameters.AddWithValue("@FinncPriod", journalEntry.FinncPriod);
                                cmd.Parameters.AddWithValue("@LocTotal", journalEntry.LocTotal);
                                cmd.Parameters.AddWithValue("@FCTotal", journalEntry.FcTotal);
                                cmd.Parameters.AddWithValue("@SysTotal", journalEntry.SysTotal);
                                cmd.Parameters.AddWithValue("@TransType", journalEntry.TransType);

                                cmd.ExecuteNonQuery();

                                flag = 1;

                            }
                        }

                        foreach (AsientoDetalle journalEntryLines in listJournalEntryLines)
                        {
                            using (SqlCommand cmd = new SqlCommand("InsertJournalEntryLines", Connection, transaction))
                            {
                                //cmd.CommandText = "InsertJournalEntryLines";

                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@TransId", journalEntryLines.TransId);
                                cmd.Parameters.AddWithValue("@LineId", journalEntryLines.Line_ID);
                                cmd.Parameters.AddWithValue("@RefDate", journalEntryLines.RefDate);
                                cmd.Parameters.AddWithValue("@DueDate", journalEntryLines.DueDate);
                                cmd.Parameters.AddWithValue("@TaxDate", journalEntryLines.TaxDate);
                                cmd.Parameters.AddWithValue("@Account", journalEntryLines.Account);
                                cmd.Parameters.AddWithValue("@ShortName", journalEntryLines.ShortName);
                                cmd.Parameters.AddWithValue("@LineMemo", journalEntryLines.LineMemo);
                                cmd.Parameters.AddWithValue("@TransType", journalEntryLines.TransType);
                                cmd.Parameters.AddWithValue("@Debit", journalEntryLines.Debit);
                                cmd.Parameters.AddWithValue("@Credit", journalEntryLines.Credit);
                                cmd.Parameters.AddWithValue("@FCDebit", journalEntryLines.FCDebit);
                                cmd.Parameters.AddWithValue("@FCCredit", journalEntryLines.FCCredit);
                                cmd.Parameters.AddWithValue("@SysCred", journalEntryLines.SysCred);
                                cmd.Parameters.AddWithValue("@SysDeb", journalEntryLines.SysDeb);
                                cmd.Parameters.AddWithValue("@BalDueDeb", journalEntryLines.BalDueDeb);
                                cmd.Parameters.AddWithValue("@BalDueCred", journalEntryLines.BalDueCred);
                                cmd.Parameters.AddWithValue("@BalFcDeb", journalEntryLines.BalFcDeb);
                                cmd.Parameters.AddWithValue("@BalFcCred", journalEntryLines.BalFcCred);
                                cmd.Parameters.AddWithValue("@BalScCred", journalEntryLines.BalScCred);
                                cmd.Parameters.AddWithValue("@BalScDeb", journalEntryLines.BalScDeb);
                                cmd.Parameters.AddWithValue("@UserSign", journalEntryLines.UserSign);
                                cmd.Parameters.AddWithValue("@FinncPriod", journalEntryLines.FinncPriod);
                                cmd.Parameters.AddWithValue("@FCCurrency", journalEntryLines.FCCurrency);
                                cmd.Parameters.AddWithValue("@DataSource", journalEntryLines.DataSource);
                                cmd.Parameters.AddWithValue("@ContraAct", journalEntryLines.ContraAct);

                                cmd.ExecuteNonQuery();
                            }
                        }

                        foreach (AsientoDetalle journalEntryLines in listJournalEntryLines)
                        {
                            using (SqlCommand cmd = new SqlCommand("UpdateDebitCreditAccount", Connection, transaction))
                            {
                                
                                cmd.CommandType = CommandType.StoredProcedure;                               

                                cmd.Parameters.AddWithValue("@AcctCode", journalEntryLines.Account);
                                cmd.Parameters.AddWithValue("@CurrTotal", journalEntryLines.Debit-journalEntryLines.Credit);
                                cmd.Parameters.AddWithValue("@SysTotal", journalEntryLines.SysDeb- journalEntryLines.SysCred);
                                cmd.Parameters.AddWithValue("@FcTotal", journalEntryLines.FCDebit- journalEntryLines.FCCredit);


                                cmd.ExecuteNonQuery();
                            }

                            if(journalEntryLines.Account != journalEntryLines.ShortName)
                            {
                                using (SqlCommand cmd = new SqlCommand("UpdateDebitCreditAccountBusinessPartner", Connection, transaction))
                                {
                                    
                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.Parameters.AddWithValue("@CardCode", journalEntryLines.ShortName);
                                    cmd.Parameters.AddWithValue("@Balance", journalEntryLines.Debit - journalEntryLines.Credit);
                                    cmd.Parameters.AddWithValue("@BalanceSys", journalEntryLines.SysDeb - journalEntryLines.SysCred);
                                    cmd.Parameters.AddWithValue("@BalanceFC", journalEntryLines.FCDebit - journalEntryLines.FCCredit);

                                    cmd.ExecuteNonQuery();

                                }
                            }
                        }

                        transaction.Commit();

                        Connection.Close();

                        return Tuple.Create(flag,error);


                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();

                        Connection.Close();

                        return Tuple.Create (flag,e.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                return Tuple.Create(flag, ex.Message);
            }


        }

        public string checkidentOjdt(int identiny)
        {           
            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();


                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "ResetCheckidentOjdt";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Ident", identiny);

                        cmd.ExecuteNonQuery();

                    }


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

        public Tuple<int, string> InsertJournalEntryLinesTest(List<AsientoDetalle> listaJournalEntryLinesDef)
        {
            int flag = 0;

            int i = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (AsientoDetalle journalEntryLines in listaJournalEntryLinesDef)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "InsertJournalEntryLinesTest";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@TransId", journalEntryLines.TransId);
                            cmd.Parameters.AddWithValue("@LineId", journalEntryLines.Line_ID);
                            cmd.Parameters.AddWithValue("@RefDate", journalEntryLines.RefDate);
                            cmd.Parameters.AddWithValue("@DueDate", journalEntryLines.DueDate);
                            cmd.Parameters.AddWithValue("@TaxDate", journalEntryLines.TaxDate);
                            cmd.Parameters.AddWithValue("@Account", journalEntryLines.Account);
                            cmd.Parameters.AddWithValue("@ShortName", journalEntryLines.ShortName);
                            cmd.Parameters.AddWithValue("@LineMemo", journalEntryLines.LineMemo);
                            cmd.Parameters.AddWithValue("@TransType", journalEntryLines.TransType);
                            cmd.Parameters.AddWithValue("@Debit", journalEntryLines.Debit);
                            cmd.Parameters.AddWithValue("@Credit", journalEntryLines.Credit);
                            cmd.Parameters.AddWithValue("@FCDebit", journalEntryLines.FCDebit);
                            cmd.Parameters.AddWithValue("@FCCredit", journalEntryLines.FCCredit);
                            cmd.Parameters.AddWithValue("@SysCred", journalEntryLines.SysCred);
                            cmd.Parameters.AddWithValue("@SysDeb", journalEntryLines.SysDeb);
                            cmd.Parameters.AddWithValue("@BalDueDeb", journalEntryLines.BalDueDeb);
                            cmd.Parameters.AddWithValue("@BalDueCred", journalEntryLines.BalDueCred);
                            cmd.Parameters.AddWithValue("@BalFcDeb", journalEntryLines.BalFcDeb);
                            cmd.Parameters.AddWithValue("@BalFcCred", journalEntryLines.BalFcCred);
                            cmd.Parameters.AddWithValue("@BalScCred", journalEntryLines.BalScCred);
                            cmd.Parameters.AddWithValue("@BalScDeb", journalEntryLines.BalScDeb);
                            cmd.Parameters.AddWithValue("@UserSign", journalEntryLines.UserSign);
                            cmd.Parameters.AddWithValue("@FinncPriod", journalEntryLines.FinncPriod);
                            cmd.Parameters.AddWithValue("@FCCurrency", journalEntryLines.FCCurrency);
                            cmd.Parameters.AddWithValue("@DataSource", journalEntryLines.DataSource);
                            cmd.Parameters.AddWithValue("@ContraAct", journalEntryLines.ContraAct);

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

        public Tuple<int, string> InsertJournalEntryPreliminarTest(List<AsientoCabecera> listaJournalEntryDef)
        {
            int flag = 0;

            int i = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (AsientoCabecera journalEntry in listaJournalEntryDef)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "InsertJournalEntryTest";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@TransId", journalEntry.TransId);
                            cmd.Parameters.AddWithValue("@RefDate", journalEntry.RefDate);
                            cmd.Parameters.AddWithValue("@TaxDate", journalEntry.TaxDate);
                            cmd.Parameters.AddWithValue("@DueDate", journalEntry.DueDate);
                            cmd.Parameters.AddWithValue("@Memo", journalEntry.Memo);
                            cmd.Parameters.AddWithValue("@TransCurr", journalEntry.TransCurr);
                            cmd.Parameters.AddWithValue("@Ref1", journalEntry.Ref1);
                            cmd.Parameters.AddWithValue("@Ref2", journalEntry.Ref2);
                            cmd.Parameters.AddWithValue("@UserSign", journalEntry.UserSign);
                            cmd.Parameters.AddWithValue("@CreateDate", journalEntry.UpdateDate);
                            cmd.Parameters.AddWithValue("@BaseRef", journalEntry.BaseRef);
                            cmd.Parameters.AddWithValue("@FinncPriod", journalEntry.FinncPriod);
                            cmd.Parameters.AddWithValue("@LocTotal", journalEntry.LocTotal);
                            cmd.Parameters.AddWithValue("@FCTotal", journalEntry.FcTotal);
                            cmd.Parameters.AddWithValue("@SysTotal", journalEntry.SysTotal);
                            cmd.Parameters.AddWithValue("@TransType", journalEntry.TransType);

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

        public Tuple<int, string> InsertJournalEntryLinesPreliminarAlternativo(List<AsientoDetalle> listaJournalEntryLines)
        {
            //LoadDatatable INV1_TEST

            //dtJDT1 = dataSet.Tables["JDT1_TEST"];

            int flag = 0;

            string error = null;

            int TransId = 0;

            try
            {
                foreach (AsientoDetalle journalEntryLines in listaJournalEntryLines)
                {
                    TransId = journalEntryLines.TransId;

                    DataRow newRow = dtJDT1.NewRow();

                    newRow["TransId"] = journalEntryLines.TransId;
                    newRow["Line_ID"] = journalEntryLines.Line_ID;
                    newRow["RefDate"] = journalEntryLines.RefDate;
                    newRow["DueDate"] = journalEntryLines.DueDate;
                    newRow["TaxDate"] = journalEntryLines.TaxDate;
                    newRow["Account"] = journalEntryLines.Account;
                    newRow["ShortName"] = journalEntryLines.ShortName;
                    newRow["LineMemo"] = journalEntryLines.LineMemo;
                    newRow["TransType"] = journalEntryLines.TransType;
                    newRow["Debit"] = journalEntryLines.Debit;
                    newRow["Credit"] = journalEntryLines.Credit;
                    newRow["FCDebit"] = journalEntryLines.FCDebit;
                    newRow["FCCredit"] = journalEntryLines.FCCredit;
                    newRow["SYSCred"] = journalEntryLines.SysCred;
                    newRow["SYSDeb"] = journalEntryLines.SysDeb;
                    newRow["BalDueDeb"] = journalEntryLines.BalDueDeb;
                    newRow["BalDueCred"] = journalEntryLines.BalDueCred;
                    newRow["BalFcDeb"] = journalEntryLines.BalFcDeb;
                    newRow["BalFcCred"] = journalEntryLines.BalFcCred;
                    newRow["BalScCred"] = journalEntryLines.BalScCred;
                    newRow["BalScDeb"] = journalEntryLines.BalScDeb;
                    newRow["UserSign"] = journalEntryLines.UserSign;
                    newRow["FinncPriod"] = journalEntryLines.FinncPriod;
                    newRow["FCCurrency"] = journalEntryLines.FCCurrency;
                    newRow["DataSource"] = journalEntryLines.DataSource;
                    newRow["ContraAct"] = journalEntryLines.ContraAct;

                    dtJDT1.Rows.Add(newRow);

                    dtJDT1.AcceptChanges();
                }

                DataRow[] selected = dtJDT1.Select("TransId = " + TransId);

                flag = selected.Count();

                return Tuple.Create(flag, error);

            }
            catch (Exception e)
            {
                return Tuple.Create(flag, e.Message);
            }
        }

        public Tuple<int, string> InsertJournalEntryPreliminarAlternativo(List<AsientoCabecera> listaJournalEntry)
        {
            //LoadDatatable INV5_TEST

            //dtOJDT = dataSet.Tables["OJDT_TEST"];

            int flag = 0;

            string error = null;

            try
            {

                foreach (AsientoCabecera journalEntry in listaJournalEntry)
                {

                    int TransId = dtOJDT.Rows.Count - 1;

                    DataRow newRow = dtOJDT.NewRow();

                    newRow["RefDate"] = journalEntry.RefDate;
                    newRow["TaxDate"] = journalEntry.TaxDate;
                    newRow["DueDate"] = journalEntry.DueDate;
                    newRow["Memo"] = journalEntry.Memo;
                    newRow["TransCurr"] = journalEntry.TransCurr;
                    newRow["Ref1"] = journalEntry.Ref1;
                    newRow["Ref2"] = journalEntry.Ref2;
                    newRow["UserSign"] = journalEntry.UserSign;
                    newRow["CreateDate"] = journalEntry.UpdateDate;
                    newRow["BaseRef"] = journalEntry.BaseRef;
                    newRow["FinncPriod"] = journalEntry.FinncPriod;
                    newRow["LocTotal"] = journalEntry.LocTotal;
                    newRow["FcTotal"] = journalEntry.FcTotal;
                    newRow["SysTotal"] = journalEntry.SysTotal;
                    newRow["TransType"] = journalEntry.TransType;

                    dtOJDT.Rows.Add(newRow);

                    if (dtOJDT.Rows.Contains(TransId + 1) == true)
                    {
                        flag = 1;
                    }
                    else
                    {
                        flag = 0;
                    }

                    dtOJDT.AcceptChanges();


                }

                return Tuple.Create(flag, error);

            }
            catch (Exception e)
            {
                return Tuple.Create(flag, e.Message);
            }
        }

        public Tuple<int, string> UpdateJournalEntryLines(List<AsientoDetalle> listaJournalEntryLines)
        {
            int flag = 0;

            int i = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (AsientoDetalle journalEntryLines in listaJournalEntryLines)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "UpdateJournalEntryLines";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@TransId", journalEntryLines.TransId);
                            cmd.Parameters.AddWithValue("@LineId", journalEntryLines.Line_ID);
                            cmd.Parameters.AddWithValue("@LineMemo", journalEntryLines.LineMemo);                            
                            cmd.Parameters.AddWithValue("@UserSign", journalEntryLines.UserSign);
                            cmd.Parameters.AddWithValue("@DueDate", journalEntryLines.DueDate);
                            cmd.Parameters.AddWithValue("@TaxDate", journalEntryLines.TaxDate);

                            flag = cmd.ExecuteNonQuery();

                            i = flag + i;
                        }
                    }

                }

                Connection.Close();

                return Tuple.Create(i,error);

            }
            catch (Exception e)
            {
                
                Connection.Close();

                return Tuple.Create(i, e.Message);
            }
        }

        public Tuple<int, string> UpdateJournalEntry(List<AsientoCabecera> listaJournalEntry)
        {
            int flag = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (AsientoCabecera journalEntry in listaJournalEntry)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "UpdateJournalEntry";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@TransId", journalEntry.TransId);                            
                            cmd.Parameters.AddWithValue("@TaxDate", journalEntry.TaxDate);
                            cmd.Parameters.AddWithValue("@DueDate", journalEntry.DueDate);
                            cmd.Parameters.AddWithValue("@Memo", journalEntry.Memo);                            
                            cmd.Parameters.AddWithValue("@Ref1", journalEntry.Ref1);
                            cmd.Parameters.AddWithValue("@Ref2", journalEntry.Ref2);
                            cmd.Parameters.AddWithValue("@UserSign", journalEntry.UserSign);
                            cmd.Parameters.AddWithValue("@UpdateDate", journalEntry.UpdateDate);
                            
                            flag = cmd.ExecuteNonQuery();

                        }
                    }

                }

                Connection.Close();

                return Tuple.Create(flag,error);

            }
            catch (Exception e)
            {
                
                Connection.Close();

                return Tuple.Create(flag, e.Message);
            }
        }

        public Tuple<int, string> InsertJournalEntryLines(List<AsientoDetalle> listaJournalEntryLines)
        {
            int flag = 0;

            int i = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (AsientoDetalle journalEntryLines in listaJournalEntryLines)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "InsertJournalEntryLines";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@TransId", journalEntryLines.TransId);
                            cmd.Parameters.AddWithValue("@LineId", journalEntryLines.Line_ID);
                            cmd.Parameters.AddWithValue("@RefDate", journalEntryLines.RefDate);
                            cmd.Parameters.AddWithValue("@DueDate", journalEntryLines.DueDate);
                            cmd.Parameters.AddWithValue("@TaxDate", journalEntryLines.TaxDate);
                            cmd.Parameters.AddWithValue("@Account", journalEntryLines.Account);
                            cmd.Parameters.AddWithValue("@ShortName", journalEntryLines.ShortName);
                            cmd.Parameters.AddWithValue("@LineMemo", journalEntryLines.LineMemo);
                            cmd.Parameters.AddWithValue("@TransType", journalEntryLines.TransType);
                            cmd.Parameters.AddWithValue("@Debit", journalEntryLines.Debit);
                            cmd.Parameters.AddWithValue("@Credit", journalEntryLines.Credit);
                            cmd.Parameters.AddWithValue("@FCDebit", journalEntryLines.FCDebit);
                            cmd.Parameters.AddWithValue("@FCCredit", journalEntryLines.FCCredit);
                            cmd.Parameters.AddWithValue("@SysCred", journalEntryLines.SysCred);
                            cmd.Parameters.AddWithValue("@SysDeb", journalEntryLines.SysDeb);
                            cmd.Parameters.AddWithValue("@BalDueDeb", journalEntryLines.BalDueDeb);
                            cmd.Parameters.AddWithValue("@BalDueCred", journalEntryLines.BalDueCred);
                            cmd.Parameters.AddWithValue("@BalFcDeb", journalEntryLines.BalFcDeb);
                            cmd.Parameters.AddWithValue("@BalFcCred", journalEntryLines.BalFcCred);
                            cmd.Parameters.AddWithValue("@BalScCred", journalEntryLines.BalScCred);
                            cmd.Parameters.AddWithValue("@BalScDeb", journalEntryLines.BalScDeb);
                            cmd.Parameters.AddWithValue("@UserSign", journalEntryLines.UserSign);
                            cmd.Parameters.AddWithValue("@FinncPriod", journalEntryLines.FinncPriod);
                            cmd.Parameters.AddWithValue("@FCCurrency", journalEntryLines.FCCurrency);
                            cmd.Parameters.AddWithValue("@DataSource", journalEntryLines.DataSource);
                            cmd.Parameters.AddWithValue("@ContraAct", journalEntryLines.ContraAct);

                            flag = cmd.ExecuteNonQuery();

                            i = flag + i;
                        }
                    }

                }

                Connection.Close();

                return Tuple.Create(i,error);

            }
            catch (Exception e)
            {
                
                Connection.Close();

                return Tuple.Create(i, e.Message);
            }
        }

        public Tuple<int, string> InsertJournalEntry(List<AsientoCabecera> listaJournalEntry)
        {
            int flag = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (AsientoCabecera journalEntry in listaJournalEntry)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "InsertJournalEntry";

                            cmd.CommandType = CommandType.StoredProcedure;
                            
                            cmd.Parameters.AddWithValue("@RefDate", journalEntry.RefDate);
                            cmd.Parameters.AddWithValue("@TaxDate", journalEntry.TaxDate);
                            cmd.Parameters.AddWithValue("@DueDate", journalEntry.DueDate);
                            cmd.Parameters.AddWithValue("@Memo", journalEntry.Memo);
                            cmd.Parameters.AddWithValue("@TransCurr", journalEntry.TransCurr);
                            cmd.Parameters.AddWithValue("@Ref1", journalEntry.Ref1);
                            cmd.Parameters.AddWithValue("@Ref2", journalEntry.Ref2);
                            cmd.Parameters.AddWithValue("@UserSign", journalEntry.UserSign);
                            cmd.Parameters.AddWithValue("@CreateDate", journalEntry.UpdateDate);
                            cmd.Parameters.AddWithValue("@BaseRef", journalEntry.BaseRef);
                            cmd.Parameters.AddWithValue("@FinncPriod", journalEntry.FinncPriod);
                            cmd.Parameters.AddWithValue("@LocTotal", journalEntry.LocTotal);
                            cmd.Parameters.AddWithValue("@FCTotal", journalEntry.FcTotal);
                            cmd.Parameters.AddWithValue("@SysTotal", journalEntry.SysTotal);
                            cmd.Parameters.AddWithValue("@TransType", journalEntry.TransType);

                            flag = cmd.ExecuteNonQuery();

                        }
                    }

                }

                Connection.Close();

                return Tuple.Create(flag,error);

            }
            catch (Exception e)
            {
               
                Connection.Close();

                return Tuple.Create(flag, e.Message);
            }
        }

        public Tuple<int, string> InsertJournalEntryLinesPreliminar(List<AsientoDetalle> listaJournalEntryLines)
        {
            //LoadDatatable INV1_TEST

            dtJDT1 = dataSet.Tables["JDT1_TEST"];

            int flag = 0;

            string error = null;

            int TransId = 0;

            try
            {
                foreach (AsientoDetalle journalEntryLines in listaJournalEntryLines)
                {
                    TransId = journalEntryLines.TransId;

                    DataRow newRow = dtJDT1.NewRow();

                    newRow["TransId"] = journalEntryLines.TransId;
                    newRow["Line_ID"] = journalEntryLines.Line_ID;
                    newRow["RefDate"] = journalEntryLines.RefDate;
                    newRow["DueDate"] = journalEntryLines.DueDate;
                    newRow["TaxDate"] = journalEntryLines.TaxDate;
                    newRow["Account"] = journalEntryLines.Account;
                    newRow["ShortName"] = journalEntryLines.ShortName;
                    newRow["LineMemo"] = journalEntryLines.LineMemo;
                    newRow["TransType"] = journalEntryLines.TransType;
                    newRow["Debit"] = journalEntryLines.Debit;
                    newRow["Credit"] = journalEntryLines.Credit;
                    newRow["FCDebit"] = journalEntryLines.FCDebit;
                    newRow["FCCredit"] = journalEntryLines.FCCredit;
                    newRow["SYSCred"] = journalEntryLines.SysCred;
                    newRow["SYSDeb"] = journalEntryLines.SysDeb;
                    newRow["BalDueDeb"] = journalEntryLines.BalDueDeb;
                    newRow["BalDueCred"] = journalEntryLines.BalDueCred;
                    newRow["BalFcDeb"] = journalEntryLines.BalFcDeb;
                    newRow["BalFcCred"] = journalEntryLines.BalFcCred;
                    newRow["BalScCred"] = journalEntryLines.BalScCred;
                    newRow["BalScDeb"] = journalEntryLines.BalScDeb;
                    newRow["UserSign"] = journalEntryLines.UserSign;
                    newRow["FinncPriod"] = journalEntryLines.FinncPriod;
                    newRow["FCCurrency"] = journalEntryLines.FCCurrency;
                    newRow["DataSource"] = journalEntryLines.DataSource;
                    newRow["ContraAct"] = journalEntryLines.ContraAct;

                    dtJDT1.Rows.Add(newRow);

                    dtJDT1.AcceptChanges();
                }

                DataRow[] selected = dtJDT1.Select("TransId = " + TransId);

                flag = selected.Count();

                return Tuple.Create(flag, error);

            }
            catch (Exception e)
            {
                return Tuple.Create(flag, e.Message);
            }
        }

        public Tuple<int, string> InsertJournalEntryPreliminar(List<AsientoCabecera> listaJournalEntry)
        {
            //LoadDatatable INV5_TEST

            dtOJDT = dataSet.Tables["OJDT_TEST"];

            int flag = 0;

            string error = null;
          
            try
            {

                foreach (AsientoCabecera journalEntry in listaJournalEntry)
                {

                    int TransId = dtOJDT.Rows.Count-1;

                    DataRow newRow = dtOJDT.NewRow();

                    newRow["RefDate"] = journalEntry.RefDate;
                    newRow["TaxDate"] = journalEntry.TaxDate;
                    newRow["DueDate"] = journalEntry.DueDate;
                    newRow["Memo"] = journalEntry.Memo;
                    newRow["TransCurr"] = journalEntry.TransCurr;
                    newRow["Ref1"] = journalEntry.Ref1;
                    newRow["Ref2"] = journalEntry.Ref2;
                    newRow["UserSign"] = journalEntry.UserSign;
                    newRow["CreateDate"] = journalEntry.UpdateDate;
                    newRow["BaseRef"] = journalEntry.BaseRef;
                    newRow["FinncPriod"] = journalEntry.FinncPriod;
                    newRow["LocTotal"] = journalEntry.LocTotal;
                    newRow["FcTotal"] = journalEntry.FcTotal;
                    newRow["SysTotal"] = journalEntry.SysTotal;
                    newRow["TransType"] = journalEntry.TransType;

                    dtOJDT.Rows.Add(newRow);
                  
                    if (dtOJDT.Rows.Contains(TransId + 1) == true)
                    {
                        flag = 1;
                    }
                    else
                    {
                        flag = 1; //revisar
                    }

                    dtOJDT.AcceptChanges();


                }

                return Tuple.Create(flag, error);

            }
            catch (Exception e)
            {
                return Tuple.Create(flag, e.Message);
            }
        }

        public Tuple<List<AsientoCabecera>, string> ConsultaJournalEntry(List<AsientoCabecera> listaJournalEntry)
        {
            List<AsientoCabecera> newListJournalEntry = new List<AsientoCabecera>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (AsientoCabecera journalEntry in listaJournalEntry)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "FindJournalEntry";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@TransId", journalEntry.TransId);
                            cmd.Parameters.AddWithValue("@RefDate", journalEntry.RefDate);
                            cmd.Parameters.AddWithValue("@TaxDate", journalEntry.TaxDate);
                            cmd.Parameters.AddWithValue("@DueDate", journalEntry.DueDate);
                            cmd.Parameters.AddWithValue("@Memo", journalEntry.Memo);                            
                            cmd.Parameters.AddWithValue("@Ref1", journalEntry.Ref1);
                            cmd.Parameters.AddWithValue("@Ref2", journalEntry.Ref2);


                            SqlDataReader reader = cmd.ExecuteReader();

                            while (reader.Read())
                            {
                                AsientoCabecera newJournalEntry = new AsientoCabecera();

                                newJournalEntry.TransId =Convert.ToInt32(reader["TransId"]);
                                newJournalEntry.RefDate =Convert.ToDateTime(reader["RefDate"]);
                                newJournalEntry.TaxDate = Convert.ToDateTime(reader["TaxDate"]);
                                newJournalEntry.DueDate = Convert.ToDateTime(reader["DueDate"]);
                                newJournalEntry.Memo = reader["Memo"].ToString();
                                newJournalEntry.TransType =Convert.ToInt32(reader["TransType"]);
                                newJournalEntry.Ref1 = reader["Ref1"].ToString();
                                newJournalEntry.Ref2 = reader["Ref2"].ToString();
                                newJournalEntry.BaseRef =Convert.ToInt32(reader["BaseRef"]);


                                newListJournalEntry.Add(newJournalEntry);

                            }

                            reader.Close();
                        }
                    }

                }

                Connection.Close();

                return Tuple.Create(newListJournalEntry,error);

            }
            catch (Exception e)
            {                

                Connection.Close();

                return Tuple.Create(newListJournalEntry, e.Message);
            }
        }

        

        public Tuple<List<AsientoCabecera>, string> FindNextJournalEntry(string transid)
        {
            List<AsientoCabecera> newListJournalEntry = new List<AsientoCabecera>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "NextJournalEntry";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@TransId", transid);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            AsientoCabecera newJournalEntry = new AsientoCabecera();

                            newJournalEntry.TransId = Convert.ToInt32(reader["TransId"]);
                            newJournalEntry.RefDate = Convert.ToDateTime(reader["RefDate"]);
                            newJournalEntry.TaxDate = Convert.ToDateTime(reader["TaxDate"]);
                            newJournalEntry.DueDate = Convert.ToDateTime(reader["DueDate"]);
                            newJournalEntry.Memo = reader["Memo"].ToString();
                            newJournalEntry.TransType = Convert.ToInt32(reader["TransType"]);
                            newJournalEntry.Ref1 = reader["Ref1"].ToString();
                            newJournalEntry.Ref2 = reader["Ref2"].ToString();
                            newJournalEntry.BaseRef = Convert.ToInt32(reader["BaseRef"]);

                            newListJournalEntry.Add(newJournalEntry);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(newListJournalEntry,error);

            }
            catch (Exception e)
            {
               
                Connection.Close();

                return Tuple.Create(newListJournalEntry, e.Message);
            }
        }

        public Tuple<DataTable, string> FindJournalEntryLines(int transid)
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
                        cmd.CommandText = "FindJournalEntryLines";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@TransId", transid);

                        data = new SqlDataAdapter(cmd);
                        
                        data.Fill(tabla);

                    }

                }

                Connection.Close();

                return Tuple.Create(tabla,error);

            }
            catch (Exception e)
            {                

                Connection.Close();

                return Tuple.Create(tabla, e.Message);
            }
        }

       

        public Tuple<List<AsientoCabecera>, string> FindPreviousJournalEntry(string transid)
        {
            List<AsientoCabecera> newListJournalEntry = new List<AsientoCabecera>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "PreviousJournalEntry";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@TransId", transid);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            AsientoCabecera newJournalEntry = new AsientoCabecera();

                            newJournalEntry.TransId = Convert.ToInt32(reader["TransId"]);
                            newJournalEntry.RefDate = Convert.ToDateTime(reader["RefDate"]);
                            newJournalEntry.TaxDate = Convert.ToDateTime(reader["TaxDate"]);
                            newJournalEntry.DueDate = Convert.ToDateTime(reader["DueDate"]);
                            newJournalEntry.Memo = reader["Memo"].ToString();
                            newJournalEntry.TransType = Convert.ToInt32(reader["TransType"]);
                            newJournalEntry.Ref1 = reader["Ref1"].ToString();
                            newJournalEntry.Ref2 = reader["Ref2"].ToString();
                            newJournalEntry.BaseRef = Convert.ToInt32(reader["BaseRef"]);

                            newListJournalEntry.Add(newJournalEntry);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(newListJournalEntry, error);

            }
            catch (Exception e)
            {                
                
                Connection.Close();

                return Tuple.Create(newListJournalEntry, e.Message);
            }
        }

        public Tuple<List<AsientoCabecera>, string> FindFirstJournalEntry()
        {
            List<AsientoCabecera> newListJournalEntry = new List<AsientoCabecera>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FirstJournalEntry";

                        cmd.CommandType = CommandType.StoredProcedure;                      

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            AsientoCabecera newJournalEntry = new AsientoCabecera();

                            newJournalEntry.TransId = Convert.ToInt32(reader["TransId"]);
                            newJournalEntry.RefDate = Convert.ToDateTime(reader["RefDate"]);
                            newJournalEntry.TaxDate = Convert.ToDateTime(reader["TaxDate"]);
                            newJournalEntry.DueDate = Convert.ToDateTime(reader["DueDate"]);
                            newJournalEntry.Memo = reader["Memo"].ToString();
                            newJournalEntry.TransType = Convert.ToInt32(reader["TransType"]);
                            newJournalEntry.Ref1 = reader["Ref1"].ToString();
                            newJournalEntry.Ref2 = reader["Ref2"].ToString();
                            newJournalEntry.BaseRef = Convert.ToInt32(reader["BaseRef"]);

                            newListJournalEntry.Add(newJournalEntry);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(newListJournalEntry,error);

            }
            catch (Exception e)
            {               

                Connection.Close();

                return Tuple.Create(newListJournalEntry, e.Message);
            }
        }

    }
}
