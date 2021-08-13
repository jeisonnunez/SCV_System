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
    public class ModeloNotaCreditoDeudores: ModeloDocumento
    {
        public Tuple<int,string> SelectDocNum()
        {
            int DocNum = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "SelectDocNumORIN";

                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            DocNum = Convert.ToInt32(reader["DocNum"]);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(DocNum,error);

            }
            catch (Exception e)
            {                

                Connection.Close();

                return Tuple.Create(DocNum,e.Message);

            }
        }

        public Tuple<DataTable,string> FindSalesCreditNoteLines(int docEntry)
        {
            string error = null;

            DataTable tabla = new DataTable();

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FindSalesCreditNoteLines";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@DocEntry", docEntry);

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

                return Tuple.Create(tabla,e.Message);
            }
        }

        public Tuple<int, string> InsertTaxHoldingCreditNote(List<TablaRetencionImpuesto> listTablaRetenciones)
        {
            int flag = 0;

            int i = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (TablaRetencionImpuesto TablaRetenciones in listTablaRetenciones)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "InsertTaxHoldingORIN";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@AbsEntry", TablaRetenciones.AbsEntry);
                            cmd.Parameters.AddWithValue("@WTCode", TablaRetenciones.WTCode);
                            cmd.Parameters.AddWithValue("@Type", TablaRetenciones.Type);
                            cmd.Parameters.AddWithValue("@Category", TablaRetenciones.Category);
                            cmd.Parameters.AddWithValue("@BaseType", TablaRetenciones.BaseType);
                            cmd.Parameters.AddWithValue("@Rate", TablaRetenciones.Rate);
                            cmd.Parameters.AddWithValue("@TaxbleAmnt", TablaRetenciones.TaxbleAmnt);
                            cmd.Parameters.AddWithValue("@TaxbleAmntFC", TablaRetenciones.TaxbleAmntFC);
                            cmd.Parameters.AddWithValue("@TaxbleAmntSC", TablaRetenciones.TaxbleAmntSC);
                            cmd.Parameters.AddWithValue("@WTAmnt", TablaRetenciones.WTAmnt);
                            cmd.Parameters.AddWithValue("@WTAmntFC", TablaRetenciones.WTAmntFC);
                            cmd.Parameters.AddWithValue("@WTAmntSC", TablaRetenciones.WTAmntSC);
                            cmd.Parameters.AddWithValue("@BaseAmnt", TablaRetenciones.BaseAmnt);
                            cmd.Parameters.AddWithValue("@BaseAmntFC", TablaRetenciones.BaseAmntFC);
                            cmd.Parameters.AddWithValue("@BaseAmntSC", TablaRetenciones.BaseAmntSC);
                            cmd.Parameters.AddWithValue("@LineNum", TablaRetenciones.LineNum);
                            cmd.Parameters.AddWithValue("@ObjType", TablaRetenciones.ObjType);
                            cmd.Parameters.AddWithValue("@Status", TablaRetenciones.Status);
                            cmd.Parameters.AddWithValue("@Account", TablaRetenciones.Account);

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

        public Tuple<int, string> InsertTaxHoldingPreliminarSalesCreditNote(List<TablaRetencionImpuesto> listTablaRetenciones)
        {
            //LoadDatatable RIN5_TEST

            dtRIN5 = dataSet.Tables["RIN5_TEST"];

            int flag = 0;

            string error = null;

            int DocEntry = 0;

            try
            {
                foreach (TablaRetencionImpuesto TablaRetenciones in listTablaRetenciones)
                {
                    DocEntry = TablaRetenciones.AbsEntry;

                    DataRow newRow = dtRIN5.NewRow();

                    newRow["AbsEntry"] = TablaRetenciones.AbsEntry;
                    newRow["WTCode"] = TablaRetenciones.WTCode;
                    newRow["Type"] = TablaRetenciones.Type;
                    newRow["Category"] = TablaRetenciones.Category;
                    newRow["BaseType"] = TablaRetenciones.BaseType;
                    newRow["Rate"] = TablaRetenciones.Rate;
                    newRow["TaxbleAmnt"] = TablaRetenciones.TaxbleAmnt;
                    newRow["TaxbleAmntFC"] = TablaRetenciones.TaxbleAmntFC;
                    newRow["TaxbleAmntSC"] = TablaRetenciones.TaxbleAmntSC;
                    newRow["WTAmnt"] = TablaRetenciones.WTAmnt;
                    newRow["WTAmntFC"] = TablaRetenciones.WTAmntFC;
                    newRow["WTAmntSC"] = TablaRetenciones.WTAmntSC;
                    newRow["BaseAmnt"] = TablaRetenciones.BaseAmnt;
                    newRow["BaseAmntFC"] = TablaRetenciones.BaseAmntFC;
                    newRow["BaseAmntSC"] = TablaRetenciones.BaseAmntSC;
                    newRow["LineNum"] = TablaRetenciones.LineNum;
                    newRow["ObjType"] = TablaRetenciones.ObjType;
                    newRow["Status"] = TablaRetenciones.Status;
                    newRow["Account"] = TablaRetenciones.Account;

                    dtRIN5.Rows.Add(newRow);

                    dtRIN5.AcceptChanges();
                }

                DataRow[] selected = dtRIN5.Select("AbsEntry = " + DocEntry);

                flag = selected.Count();

                return Tuple.Create(flag, error);

            }
            catch (Exception e)
            {
                return Tuple.Create(flag, e.Message);
            }
        }

        public Tuple<int, string> InsertSalesCreditNoteLinesPreliminar(List<DocumentoDetalle> item1)
        {
            //LoadDatatable RIN1_TEST

            dtRIN1 = dataSet.Tables["RIN1_TEST"];

            int flag = 0;

            string error = null;

            int DocEntry = 0;

            try
            {
                foreach (DocumentoDetalle PurchaseInvoice in item1)
                {
                    DocEntry = PurchaseInvoice.DocEntry;

                    DataRow newRow = dtRIN1.NewRow();

                    newRow["DocEntry"] = PurchaseInvoice.DocEntry;
                    newRow["DocDate"] = PurchaseInvoice.DocDate;
                    newRow["LineNum"] = PurchaseInvoice.LineNum;
                    newRow["LineStatus"] = PurchaseInvoice.LineStatus;
                    newRow["ItemCode"] = PurchaseInvoice.ItemCode;
                    newRow["Dscription"] = PurchaseInvoice.Dscription;
                    newRow["Quantity"] = PurchaseInvoice.Quantity;
                    newRow["OpenQty"] = PurchaseInvoice.OpenQty;
                    newRow["InvQty"] = PurchaseInvoice.InvQty;
                    newRow["OpenInvQty"] = PurchaseInvoice.OpenInvQty;
                    newRow["Price"] = PurchaseInvoice.Price;
                    newRow["Currency"] = PurchaseInvoice.Currency;
                    newRow["LineTotal"] = PurchaseInvoice.LineTotal;
                    newRow["ObjType"] = PurchaseInvoice.ObjType;
                    newRow["AcctCode"] = PurchaseInvoice.AcctCode;
                    newRow["DocDate"] = PurchaseInvoice.DocDate;
                    newRow["BaseCard"] = PurchaseInvoice.BaseCard;
                    newRow["TotalSumSy"] = PurchaseInvoice.TotalSumSy;
                    newRow["TotalFrgn"] = PurchaseInvoice.TotalFrgn;
                    newRow["VatSum"] = PurchaseInvoice.VatSum;
                    newRow["VatSumFrgn"] = PurchaseInvoice.VatSumFrgn;
                    newRow["VatGroup"] = PurchaseInvoice.VatGroup;
                    newRow["VatPrcnt"] = PurchaseInvoice.VatPrcnt;
                    newRow["FinncPriod"] = PurchaseInvoice.FinncPriod;
                    newRow["ObjType"] = PurchaseInvoice.ObjType;
                    newRow["Address"] = PurchaseInvoice.Address;
                    newRow["GTotal"] = PurchaseInvoice.Gtotal;
                    newRow["GTotalFC"] = PurchaseInvoice.GtotalFC;
                    newRow["GTotalSC"] = PurchaseInvoice.GtotalSC;
                    newRow["StockSum"] = PurchaseInvoice.StockSum;
                    newRow["StockSumFc"] = PurchaseInvoice.StockSumFc;
                    newRow["StockSumSc"] = PurchaseInvoice.StockSumSc;
                    newRow["InvntSttus"] = PurchaseInvoice.InvntSttus;
                    newRow["WtLiable"] = PurchaseInvoice.WtLiable;
                    newRow["DataSource"] = PurchaseInvoice.DataSource;

                    dtRIN1.Rows.Add(newRow);

                    dtRIN1.AcceptChanges();
                }

                DataRow[] selected = dtRIN1.Select("DocEntry = " + DocEntry);

                flag = selected.Count();

                return Tuple.Create(flag, error);

            }
            catch (Exception e)
            {
                return Tuple.Create(flag, e.Message);
            }
        }

        public Tuple<int, string> FindDocEntryPreliminar(int docNum)
        {
            int DocEntry = 0;

            string error = null;

            try
            {

                DataRow dataRow = dtORIN.AsEnumerable().Where(r => ((int)r["DocNum"]).Equals(docNum) && ((string)r["DocSubType"]).Equals("--")).First();

                DocEntry = Convert.ToInt32(dataRow["DocEntry"]);

                return Tuple.Create(DocEntry, error);

            }
            catch (Exception e)
            {

                return Tuple.Create(DocEntry, e.Message);

            }
        }

        public Tuple<int, string> InsertSalesCreditNotePreliminar(List<DocumentoCabecera> listPurchaseInvoice)
        {
            //LoadDatatable OPCH_TEST

            dtORIN = dataSet.Tables["ORIN_TEST"];

            int flag = 0;

            string error = null;

            try
            {
                foreach (DocumentoCabecera PurchaseInvoice in listPurchaseInvoice)
                {
                    int DocEntry = dtORIN.Rows.Count;

                    DataRow newRow = dtORIN.NewRow();

                    newRow["DocNum"] = PurchaseInvoice.DocNum;
                    newRow["DocDate"] = PurchaseInvoice.DocDate;
                    newRow["TaxDate"] = PurchaseInvoice.TaxDate;
                    newRow["DocDueDate"] = PurchaseInvoice.DocDueDate;
                    newRow["Comments"] = PurchaseInvoice.Comments;
                    newRow["DocType"] = PurchaseInvoice.DocType;
                    newRow["CANCELED"] = PurchaseInvoice.Canceled;
                    newRow["UserSign"] = PurchaseInvoice.UserSign;
                    newRow["UpdateDate"] = PurchaseInvoice.UpdateDate;
                    newRow["FinncPriod"] = PurchaseInvoice.FinncPriod;
                    newRow["Address2"] = PurchaseInvoice.Address2;
                    newRow["InvntSttus"] = PurchaseInvoice.InvntSttus;
                    newRow["VatSum"] = PurchaseInvoice.VatSum;
                    newRow["VatSumFC"] = PurchaseInvoice.VatSumFC;
                    newRow["VatSumSy"] = PurchaseInvoice.VatSumSy1;
                    newRow["DocCur"] = PurchaseInvoice.DocCurr;
                    newRow["ObjType"] = PurchaseInvoice.ObjType;
                    newRow["CardCode"] = PurchaseInvoice.CardCode;
                    newRow["CardName"] = PurchaseInvoice.CardName;
                    newRow["JrnlMemo"] = PurchaseInvoice.JrnlMemo;
                    newRow["LicTradNum"] = PurchaseInvoice.LicTradNum;
                    newRow["TransId"] = PurchaseInvoice.TransId;
                    newRow["VatPaid"] = PurchaseInvoice.VatPaid;
                    newRow["VatPaidFC"] = PurchaseInvoice.VatPaidFC;
                    newRow["VatPaidSys"] = PurchaseInvoice.VatPaidSys;
                    newRow["PaidToDate"] = PurchaseInvoice.PaidToDate;
                    newRow["PaidSum"] = PurchaseInvoice.PaidSum;
                    newRow["PaidSumFc"] = PurchaseInvoice.PaidSumFc;
                    newRow["PaidSumSc"] = PurchaseInvoice.PaidSumSc;
                    newRow["WTApplied"] = PurchaseInvoice.WTApplied;
                    newRow["WTAppliedF"] = PurchaseInvoice.WTAppliedF;
                    newRow["WTAppliedS"] = PurchaseInvoice.WTAppliedS;
                    newRow["WTSum"] = PurchaseInvoice.WTSum;
                    newRow["WTSumFC"] = PurchaseInvoice.WTSumFC;
                    newRow["WTSumSC"] = PurchaseInvoice.WTSumSC;
                    newRow["SysRate"] = PurchaseInvoice.SysRate;
                    newRow["DocRate"] = PurchaseInvoice.DocRate;
                    newRow["CtlAccount"] = PurchaseInvoice.CtlAccount;
                    newRow["BaseAmnt"] = PurchaseInvoice.BaseAmnt;
                    newRow["BaseAmntFC"] = PurchaseInvoice.BaseAmntFC;
                    newRow["DocTotal"] = PurchaseInvoice.DocTotal;
                    newRow["DocTotalFC"] = PurchaseInvoice.DocTotalFC;
                    newRow["DocTotalSy"] = PurchaseInvoice.DocTotalSy;
                    newRow["DocSubType"] = PurchaseInvoice.DocSubType;
                    newRow["NumAtCard"] = PurchaseInvoice.NumAtCard;
                    newRow["U_IDA_NumControl"] = PurchaseInvoice.NumControl;
                    newRow["U_IDA_TipoTrans"] = PurchaseInvoice.TipoTrans;
                    newRow["Max1099"] = PurchaseInvoice.Max1099;

                    dtORIN.Rows.Add(newRow);

                    if (dtORIN.Rows.Contains(DocEntry + 1) == true)
                    {
                        flag = 1;
                    }
                    else
                    {
                        flag = 0;
                    }

                    dtORIN.AcceptChanges();
                }

                return Tuple.Create(flag, error);

            }
            catch (Exception e)
            {

                return Tuple.Create(flag, e.Message);
            }
        }

        public Tuple<List<DocumentoCabecera>,string> FindSalesCreditNote(List<DocumentoCabecera> listaPurchaseInvoice)
        {
            List<DocumentoCabecera> newListSales = new List<DocumentoCabecera>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (DocumentoCabecera SalesInvoice in listaPurchaseInvoice)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "FindSalesCreditNote";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@DocNum", SalesInvoice.DocNum);
                            cmd.Parameters.AddWithValue("@DocDate", SalesInvoice.DocDate);
                            cmd.Parameters.AddWithValue("@TaxDate", SalesInvoice.TaxDate);
                            cmd.Parameters.AddWithValue("@DocDueDate", SalesInvoice.DocDueDate);
                            cmd.Parameters.AddWithValue("@CardCode", SalesInvoice.CardCode);
                            cmd.Parameters.AddWithValue("@CardName", SalesInvoice.CardName);
                            cmd.Parameters.AddWithValue("@NumAtCard", SalesInvoice.NumAtCard);
                            cmd.Parameters.AddWithValue("@NumControl", SalesInvoice.NumControl);
                            cmd.Parameters.AddWithValue("@TipoTrans", SalesInvoice.TipoTrans);
                            cmd.Parameters.AddWithValue("@Comments", SalesInvoice.Comments);

                            SqlDataReader reader = cmd.ExecuteReader();

                            while (reader.Read())
                            {
                                DocumentoCabecera newSales = new DocumentoCabecera();

                                newSales.DocEntry = Convert.ToInt32(reader["DocEntry"]);
                                newSales.DocNum = Convert.ToInt32(reader["DocNum"]);
                                newSales.CardCode = reader["CardCode"].ToString();
                                newSales.CardName = reader["CardName"].ToString();
                                newSales.NumAtCard = reader["NumAtCard"].ToString();
                                newSales.DocStatus = Convert.ToChar(reader["DocStatus"]);
                                newSales.NumControl = reader["U_IDA_NumControl"].ToString();
                                newSales.DocCurr = reader["DocCur"].ToString();
                                newSales.TipoTrans = reader["U_IDA_TipoTrans"].ToString();
                                newSales.DocDate = Convert.ToDateTime(reader["DocDate"]);
                                newSales.TaxDate = Convert.ToDateTime(reader["TaxDate"]);
                                newSales.DocDueDate = Convert.ToDateTime(reader["DocDueDate"]);
                                newSales.Address2 = reader["Address2"].ToString();
                                newSales.Comments = reader["Comments"].ToString();
                                newSales.CtlAccount = reader["CtlAccount"].ToString();
                                newSales.JrnlMemo = reader["JrnlMemo"].ToString();
                                newSales.LicTradNum = reader["LicTradNum"].ToString();
                                newSales.DocType = Convert.ToChar(reader["DocType"]);
                                newSales.DocTotal = Convert.ToDecimal(reader["DocTotal"]);
                                newSales.DocTotalFC = Convert.ToDecimal(reader["DocTotalFC"]);
                                newSales.DocTotalSy = Convert.ToDecimal(reader["DocTotalSy"]);
                                newSales.VatSum = Convert.ToDecimal(reader["VatSum"]);
                                newSales.VatSumFC = Convert.ToDecimal(reader["VatSumFC"]);
                                newSales.VatSumSy1 = Convert.ToDecimal(reader["VatSumSy"]);
                                newSales.WTSum = Convert.ToDecimal(reader["WTSum"]);
                                newSales.WTSumFC = Convert.ToDecimal(reader["WTSumFC"]);
                                newSales.WTSumSC = Convert.ToDecimal(reader["WTSumSC"]);
                                newSales.BaseAmnt = Convert.ToDecimal(reader["BaseAmnt"]);
                                newSales.BaseAmntFC = Convert.ToDecimal(reader["BaseAmntFC"]);
                                newSales.BaseAmntSC = Convert.ToDecimal(reader["BaseAmntFC"]);

                                newListSales.Add(newSales);

                            }

                            reader.Close();
                        }
                    }

                }

                Connection.Close();

                return Tuple.Create(newListSales,error);

            }
            catch (Exception e)
            {                

                Connection.Close();

                return Tuple.Create(newListSales,e.Message);
            }
        }

        
        public Tuple<int, string> DeleteSalesCreditNoteRetenciones(int docEntryDeleted)
        {
            int flag = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "DeleteSalesCreditNoteRetenciones";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@DocEntry", docEntryDeleted);

                        flag = cmd.ExecuteNonQuery();

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

        public Tuple<int, string> DeleteSalesCreditNoteLines(int docEntryDeleted)
        {
            int flag = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();


                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "DeleteSalesCreditNoteLines";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@DocEntry", docEntryDeleted);

                        flag = cmd.ExecuteNonQuery();

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

        public Tuple<int, string> DeleteSalesCreditNote(int docNumDeleted)
        {
            int flag = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();


                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "DeleteSalesCreditNote";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@DocNum", docNumDeleted);

                        flag = cmd.ExecuteNonQuery();

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

        public Tuple<int,string> InsertSalesCreditNote(List<DocumentoCabecera> listaSalesInvoice)
        {
            int flag = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (DocumentoCabecera SalesInvoice in listaSalesInvoice)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "InsertSalesCreditNote";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@DocNum", SalesInvoice.DocNum);
                            cmd.Parameters.AddWithValue("@DocDate", SalesInvoice.DocDate);
                            cmd.Parameters.AddWithValue("@TaxDate", SalesInvoice.TaxDate);
                            cmd.Parameters.AddWithValue("@DocDueDate", SalesInvoice.DocDueDate);
                            cmd.Parameters.AddWithValue("@Comments", SalesInvoice.Comments);
                            cmd.Parameters.AddWithValue("@DocType", SalesInvoice.DocType);
                            cmd.Parameters.AddWithValue("@Canceled", SalesInvoice.Canceled);
                            cmd.Parameters.AddWithValue("@UserSign", SalesInvoice.UserSign);
                            cmd.Parameters.AddWithValue("@UpdateDate", SalesInvoice.UpdateDate);
                            cmd.Parameters.AddWithValue("@DocStatus", SalesInvoice.DocStatus);
                            cmd.Parameters.AddWithValue("@FinncPriod", SalesInvoice.FinncPriod);
                            cmd.Parameters.AddWithValue("@Address2", SalesInvoice.Address2);
                            cmd.Parameters.AddWithValue("@InvntSttus", SalesInvoice.InvntSttus);
                            cmd.Parameters.AddWithValue("@VatSum", SalesInvoice.VatSum);
                            cmd.Parameters.AddWithValue("@VatSumFC", SalesInvoice.VatSumFC);
                            cmd.Parameters.AddWithValue("@VatSumSy", SalesInvoice.VatSumSy1);
                            cmd.Parameters.AddWithValue("@DocCurr", SalesInvoice.DocCurr);
                            cmd.Parameters.AddWithValue("@ObjType", SalesInvoice.ObjType);
                            cmd.Parameters.AddWithValue("@CardCode", SalesInvoice.CardCode);
                            cmd.Parameters.AddWithValue("@CardName", SalesInvoice.CardName);
                            cmd.Parameters.AddWithValue("@JrnlMemo", SalesInvoice.JrnlMemo);
                            cmd.Parameters.AddWithValue("@LicTradNum", SalesInvoice.LicTradNum);
                            cmd.Parameters.AddWithValue("@TransId", SalesInvoice.TransId);
                            cmd.Parameters.AddWithValue("@VatPaid", SalesInvoice.VatPaid);
                            cmd.Parameters.AddWithValue("@VatPaidFC", SalesInvoice.VatPaidFC);
                            cmd.Parameters.AddWithValue("@VatPaidSys", SalesInvoice.VatPaidSys);
                            cmd.Parameters.AddWithValue("@PaidToDate", SalesInvoice.PaidToDate);
                            cmd.Parameters.AddWithValue("@PaidSum", SalesInvoice.PaidSum);
                            cmd.Parameters.AddWithValue("@PaidSumFc", SalesInvoice.PaidSumFc);
                            cmd.Parameters.AddWithValue("@PaidSumSc", SalesInvoice.PaidSumSc);
                            cmd.Parameters.AddWithValue("@WTApplied", SalesInvoice.WTApplied);
                            cmd.Parameters.AddWithValue("@WTAppliedF", SalesInvoice.WTAppliedF);
                            cmd.Parameters.AddWithValue("@WTAppliedS", SalesInvoice.WTAppliedS);
                            cmd.Parameters.AddWithValue("@WTSum", SalesInvoice.WTSum);
                            cmd.Parameters.AddWithValue("@WTSumFC", SalesInvoice.WTSumFC);
                            cmd.Parameters.AddWithValue("@WTSumSC", SalesInvoice.WTSumSC);
                            cmd.Parameters.AddWithValue("@SysRate", SalesInvoice.SysRate);
                            cmd.Parameters.AddWithValue("@DocRate", SalesInvoice.DocRate);
                            cmd.Parameters.AddWithValue("@CtlAccount", SalesInvoice.CtlAccount);
                            cmd.Parameters.AddWithValue("@BaseAmnt", SalesInvoice.BaseAmnt);
                            cmd.Parameters.AddWithValue("@BaseAmntFC", SalesInvoice.BaseAmntFC);
                            cmd.Parameters.AddWithValue("@BaseAmntSC", SalesInvoice.BaseAmntSC);
                            cmd.Parameters.AddWithValue("@DocTotal", SalesInvoice.DocTotal);
                            cmd.Parameters.AddWithValue("@DocTotalFC", SalesInvoice.DocTotalFC);
                            cmd.Parameters.AddWithValue("@DocTotalSy", SalesInvoice.DocTotalSy);
                            cmd.Parameters.AddWithValue("@DocSubType", SalesInvoice.DocSubType);
                            cmd.Parameters.AddWithValue("@NumAtCard", SalesInvoice.NumAtCard);
                            cmd.Parameters.AddWithValue("@NumControl", SalesInvoice.NumControl);
                            cmd.Parameters.AddWithValue("@TipoTrans", SalesInvoice.TipoTrans);
                            cmd.Parameters.AddWithValue("@Max1099", SalesInvoice.Max1099);
                            cmd.Parameters.AddWithValue("@Fafe", SalesInvoice.Fafe);

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

                return Tuple.Create(flag,e.Message);
            }
        }

        public Tuple<List<DocumentoCabecera>,string> FindLastSalesCreditNote()
        {
            List<DocumentoCabecera> newListSalesInvoice = new List<DocumentoCabecera>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "LastSalesCreditNote";

                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            DocumentoCabecera newSalesInvoice = new DocumentoCabecera();

                            newSalesInvoice.DocEntry = Convert.ToInt32(reader["DocEntry"]);
                            newSalesInvoice.DocNum = Convert.ToInt32(reader["DocNum"]);
                            newSalesInvoice.CardCode = reader["CardCode"].ToString();
                            newSalesInvoice.CardName = reader["CardName"].ToString();
                            newSalesInvoice.NumAtCard = reader["NumAtCard"].ToString();
                            newSalesInvoice.DocStatus = Convert.ToChar(reader["DocStatus"]);
                            newSalesInvoice.NumControl = reader["U_IDA_NumControl"].ToString();
                            newSalesInvoice.DocCurr = reader["DocCur"].ToString();
                            newSalesInvoice.TipoTrans = reader["U_IDA_TipoTrans"].ToString();
                            newSalesInvoice.DocDate = Convert.ToDateTime(reader["DocDate"]);
                            newSalesInvoice.TaxDate = Convert.ToDateTime(reader["TaxDate"]);
                            newSalesInvoice.DocDueDate = Convert.ToDateTime(reader["DocDueDate"]);
                            newSalesInvoice.Address2 = reader["Address2"].ToString();
                            newSalesInvoice.Comments = reader["Comments"].ToString();
                            newSalesInvoice.CtlAccount = reader["CtlAccount"].ToString();
                            newSalesInvoice.JrnlMemo = reader["JrnlMemo"].ToString();
                            newSalesInvoice.LicTradNum = reader["LicTradNum"].ToString();
                            newSalesInvoice.DocType = Convert.ToChar(reader["DocType"]);
                            newSalesInvoice.DocTotal = Convert.ToDecimal(reader["DocTotal"]);
                            newSalesInvoice.DocTotalFC = Convert.ToDecimal(reader["DocTotalFC"]);
                            newSalesInvoice.DocTotalSy = Convert.ToDecimal(reader["DocTotalSy"]);
                            newSalesInvoice.VatSum = Convert.ToDecimal(reader["VatSum"]);
                            newSalesInvoice.VatSumFC = Convert.ToDecimal(reader["VatSumFC"]);
                            newSalesInvoice.VatSumSy1 = Convert.ToDecimal(reader["VatSumSy"]);
                            newSalesInvoice.WTSum = Convert.ToDecimal(reader["WTSum"]);
                            newSalesInvoice.WTSumFC = Convert.ToDecimal(reader["WTSumFC"]);
                            newSalesInvoice.WTSumSC = Convert.ToDecimal(reader["WTSumSC"]);
                            newSalesInvoice.BaseAmnt = Convert.ToDecimal(reader["BaseAmnt"]);
                            newSalesInvoice.BaseAmntFC = Convert.ToDecimal(reader["BaseAmntFC"]);
                            newSalesInvoice.BaseAmntSC = Convert.ToDecimal(reader["BaseAmntFC"]);

                            newListSalesInvoice.Add(newSalesInvoice);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(newListSalesInvoice,error);

            }
            catch (Exception e)
            {                
                Connection.Close();

                return Tuple.Create(newListSalesInvoice,e.Message);
            }
        }



        public Tuple<string, int> UpdateSalesCreditNote(List<DocumentoCabecera> listSalesInvoice)
        {
            string error = null;

            int flag = 0;


            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (DocumentoCabecera Sales in listSalesInvoice)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "UpdateSalesCreditNote";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@DocEntry", Sales.DocEntry);
                            cmd.Parameters.AddWithValue("@DocNum", Sales.DocNum);
                            cmd.Parameters.AddWithValue("@TaxDate", Sales.TaxDate);
                            cmd.Parameters.AddWithValue("@DocDueDate", Sales.DocDueDate);
                            cmd.Parameters.AddWithValue("@NumAtCard", Sales.NumAtCard);
                            cmd.Parameters.AddWithValue("@NumControl", Sales.NumControl);
                            cmd.Parameters.AddWithValue("@TipoTrans", Sales.TipoTrans);
                            cmd.Parameters.AddWithValue("@Comments", Sales.Comments);
                            cmd.Parameters.AddWithValue("@JrnlMemo", Sales.JrnlMemo);
                            cmd.Parameters.AddWithValue("@LicTradNum", Sales.LicTradNum);
                            cmd.Parameters.AddWithValue("@UserSign", Sales.UserSign);
                            cmd.Parameters.AddWithValue("@UpdateDate", Sales.UpdateDate);


                            flag = cmd.ExecuteNonQuery();

                        }
                    }

                }

                Connection.Close();

                return Tuple.Create(error, flag);

            }
            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(e.Message, flag);
            }


        }

        public Tuple<DataTable,string> FindRetencionImpuesto(int docEntry)
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
                        cmd.CommandText = "FindRetencionImpuestoORIN";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@DocEntry", docEntry);

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

                return Tuple.Create(tabla,e.Message);
            }
        }

        public Tuple<List<DocumentoCabecera>,string> FindNextSalesCreditNote(string docNum)
        {
            List<DocumentoCabecera> newListSalesInvoice = new List<DocumentoCabecera>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "NextSalesCreditNote";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@DocNum", docNum);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            DocumentoCabecera newSalesInvoice = new DocumentoCabecera();

                            newSalesInvoice.DocEntry = Convert.ToInt32(reader["DocEntry"]);
                            newSalesInvoice.DocNum = Convert.ToInt32(reader["DocNum"]);
                            newSalesInvoice.CardCode = reader["CardCode"].ToString();
                            newSalesInvoice.CardName = reader["CardName"].ToString();
                            newSalesInvoice.NumAtCard = reader["NumAtCard"].ToString();
                            newSalesInvoice.DocStatus = Convert.ToChar(reader["DocStatus"]);
                            newSalesInvoice.NumControl = reader["U_IDA_NumControl"].ToString();
                            newSalesInvoice.DocCurr = reader["DocCur"].ToString();
                            newSalesInvoice.TipoTrans = reader["U_IDA_TipoTrans"].ToString();
                            newSalesInvoice.DocDate = Convert.ToDateTime(reader["DocDate"]);
                            newSalesInvoice.TaxDate = Convert.ToDateTime(reader["TaxDate"]);
                            newSalesInvoice.DocDueDate = Convert.ToDateTime(reader["DocDueDate"]);
                            newSalesInvoice.Address2 = reader["Address2"].ToString();
                            newSalesInvoice.Comments = reader["Comments"].ToString();
                            newSalesInvoice.CtlAccount = reader["CtlAccount"].ToString();
                            newSalesInvoice.JrnlMemo = reader["JrnlMemo"].ToString();
                            newSalesInvoice.LicTradNum = reader["LicTradNum"].ToString();
                            newSalesInvoice.DocType = Convert.ToChar(reader["DocType"]);
                            newSalesInvoice.DocTotal = Convert.ToDecimal(reader["DocTotal"]);
                            newSalesInvoice.DocTotalFC = Convert.ToDecimal(reader["DocTotalFC"]);
                            newSalesInvoice.DocTotalSy = Convert.ToDecimal(reader["DocTotalSy"]);
                            newSalesInvoice.VatSum = Convert.ToDecimal(reader["VatSum"]);
                            newSalesInvoice.VatSumFC = Convert.ToDecimal(reader["VatSumFC"]);
                            newSalesInvoice.VatSumSy1 = Convert.ToDecimal(reader["VatSumSy"]);
                            newSalesInvoice.WTSum = Convert.ToDecimal(reader["WTSum"]);
                            newSalesInvoice.WTSumFC = Convert.ToDecimal(reader["WTSumFC"]);
                            newSalesInvoice.WTSumSC = Convert.ToDecimal(reader["WTSumSC"]);
                            newSalesInvoice.BaseAmnt = Convert.ToDecimal(reader["BaseAmnt"]);
                            newSalesInvoice.BaseAmntFC = Convert.ToDecimal(reader["BaseAmntFC"]);
                            newSalesInvoice.BaseAmntSC = Convert.ToDecimal(reader["BaseAmntFC"]);

                            newListSalesInvoice.Add(newSalesInvoice);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(newListSalesInvoice,error);

            }
            catch (Exception e)
            {               

                Connection.Close();

                return Tuple.Create(newListSalesInvoice,e.Message);
            }
        }


        public Tuple<List<DocumentoCabecera>,string> FindFirstSalesCreditNote()
        {
            string error = null;

            List<DocumentoCabecera> newListSalesInvoice = new List<DocumentoCabecera>();

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FirstSalesCreditNote";

                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            DocumentoCabecera newSalesInvoice = new DocumentoCabecera();

                            newSalesInvoice.DocEntry = Convert.ToInt32(reader["DocEntry"]);
                            newSalesInvoice.DocNum = Convert.ToInt32(reader["DocNum"]);
                            newSalesInvoice.CardCode = reader["CardCode"].ToString();
                            newSalesInvoice.CardName = reader["CardName"].ToString();
                            newSalesInvoice.NumAtCard = reader["NumAtCard"].ToString();
                            newSalesInvoice.DocStatus = Convert.ToChar(reader["DocStatus"]);
                            newSalesInvoice.NumControl = reader["U_IDA_NumControl"].ToString();
                            newSalesInvoice.DocCurr = reader["DocCur"].ToString();
                            newSalesInvoice.TipoTrans = reader["U_IDA_TipoTrans"].ToString();
                            newSalesInvoice.DocDate = Convert.ToDateTime(reader["DocDate"]);
                            newSalesInvoice.TaxDate = Convert.ToDateTime(reader["TaxDate"]);
                            newSalesInvoice.DocDueDate = Convert.ToDateTime(reader["DocDueDate"]);
                            newSalesInvoice.Address2 = reader["Address2"].ToString();
                            newSalesInvoice.Comments = reader["Comments"].ToString();
                            newSalesInvoice.CtlAccount = reader["CtlAccount"].ToString();
                            newSalesInvoice.JrnlMemo = reader["JrnlMemo"].ToString();
                            newSalesInvoice.LicTradNum = reader["LicTradNum"].ToString();
                            newSalesInvoice.DocType = Convert.ToChar(reader["DocType"]);
                            newSalesInvoice.DocTotal = Convert.ToDecimal(reader["DocTotal"]);
                            newSalesInvoice.DocTotalFC = Convert.ToDecimal(reader["DocTotalFC"]);
                            newSalesInvoice.DocTotalSy = Convert.ToDecimal(reader["DocTotalSy"]);
                            newSalesInvoice.VatSum = Convert.ToDecimal(reader["VatSum"]);
                            newSalesInvoice.VatSumFC = Convert.ToDecimal(reader["VatSumFC"]);
                            newSalesInvoice.VatSumSy1 = Convert.ToDecimal(reader["VatSumSy"]);
                            newSalesInvoice.WTSum = Convert.ToDecimal(reader["WTSum"]);
                            newSalesInvoice.WTSumFC = Convert.ToDecimal(reader["WTSumFC"]);
                            newSalesInvoice.WTSumSC = Convert.ToDecimal(reader["WTSumSC"]);
                            newSalesInvoice.BaseAmnt = Convert.ToDecimal(reader["BaseAmnt"]);
                            newSalesInvoice.BaseAmntFC = Convert.ToDecimal(reader["BaseAmntFC"]);
                            newSalesInvoice.BaseAmntSC = Convert.ToDecimal(reader["BaseAmntFC"]);

                            newListSalesInvoice.Add(newSalesInvoice);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(newListSalesInvoice,error);

            }
            catch (Exception e)
            {                

                Connection.Close();

                return Tuple.Create(newListSalesInvoice,e.Message);
            }
        }

        public Tuple<List<DocumentoCabecera>,string> FindPreviousSalesCreditNote(string docNum)
        {
            string error = null;

            List<DocumentoCabecera> newListSalesInvoice = new List<DocumentoCabecera>();

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "PreviousSalesCreditNote";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@DocNum", docNum);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            DocumentoCabecera newSalesInvoice = new DocumentoCabecera();

                            newSalesInvoice.DocEntry = Convert.ToInt32(reader["DocEntry"]);
                            newSalesInvoice.DocNum = Convert.ToInt32(reader["DocNum"]);
                            newSalesInvoice.CardCode = reader["CardCode"].ToString();
                            newSalesInvoice.CardName = reader["CardName"].ToString();
                            newSalesInvoice.NumAtCard = reader["NumAtCard"].ToString();
                            newSalesInvoice.DocStatus = Convert.ToChar(reader["DocStatus"]);
                            newSalesInvoice.NumControl = reader["U_IDA_NumControl"].ToString();
                            newSalesInvoice.DocCurr = reader["DocCur"].ToString();
                            newSalesInvoice.TipoTrans = reader["U_IDA_TipoTrans"].ToString();
                            newSalesInvoice.DocDate = Convert.ToDateTime(reader["DocDate"]);
                            newSalesInvoice.TaxDate = Convert.ToDateTime(reader["TaxDate"]);
                            newSalesInvoice.DocDueDate = Convert.ToDateTime(reader["DocDueDate"]);
                            newSalesInvoice.Address2 = reader["Address2"].ToString();
                            newSalesInvoice.Comments = reader["Comments"].ToString();
                            newSalesInvoice.CtlAccount = reader["CtlAccount"].ToString();
                            newSalesInvoice.JrnlMemo = reader["JrnlMemo"].ToString();
                            newSalesInvoice.LicTradNum = reader["LicTradNum"].ToString();
                            newSalesInvoice.DocType = Convert.ToChar(reader["DocType"]);
                            newSalesInvoice.DocTotal = Convert.ToDecimal(reader["DocTotal"]);
                            newSalesInvoice.DocTotalFC = Convert.ToDecimal(reader["DocTotalFC"]);
                            newSalesInvoice.DocTotalSy = Convert.ToDecimal(reader["DocTotalSy"]);
                            newSalesInvoice.VatSum = Convert.ToDecimal(reader["VatSum"]);
                            newSalesInvoice.VatSumFC = Convert.ToDecimal(reader["VatSumFC"]);
                            newSalesInvoice.VatSumSy1 = Convert.ToDecimal(reader["VatSumSy"]);
                            newSalesInvoice.WTSum = Convert.ToDecimal(reader["WTSum"]);
                            newSalesInvoice.WTSumFC = Convert.ToDecimal(reader["WTSumFC"]);
                            newSalesInvoice.WTSumSC = Convert.ToDecimal(reader["WTSumSC"]);
                            newSalesInvoice.BaseAmnt = Convert.ToDecimal(reader["BaseAmnt"]);
                            newSalesInvoice.BaseAmntFC = Convert.ToDecimal(reader["BaseAmntFC"]);
                            newSalesInvoice.BaseAmntSC = Convert.ToDecimal(reader["BaseAmntFC"]);

                            newListSalesInvoice.Add(newSalesInvoice);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(newListSalesInvoice,error);

            }
            catch (Exception e)
            {               

                Connection.Close();

                return Tuple.Create(newListSalesInvoice,e.Message);
            }
        }



        public Tuple<int,string> InsertSalesCreditNoteLines(List<DocumentoDetalle> listSalesInvoiceLines)
        {
            int flag = 0;

            int i = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (DocumentoDetalle SalesInvoiceLines in listSalesInvoiceLines)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "InsertSalesCreditNoteLines";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@DocEntry", SalesInvoiceLines.DocEntry);
                            cmd.Parameters.AddWithValue("@LineNum", SalesInvoiceLines.LineNum);
                            cmd.Parameters.AddWithValue("@LineStatus", SalesInvoiceLines.LineStatus);
                            cmd.Parameters.AddWithValue("@ItemCode", SalesInvoiceLines.ItemCode);
                            cmd.Parameters.AddWithValue("@Dscription", SalesInvoiceLines.Dscription);
                            cmd.Parameters.AddWithValue("@Quantity", SalesInvoiceLines.Quantity);
                            cmd.Parameters.AddWithValue("@OpenQty", SalesInvoiceLines.OpenQty);
                            cmd.Parameters.AddWithValue("@InvQty", SalesInvoiceLines.InvQty);
                            cmd.Parameters.AddWithValue("@OpenInvQty", SalesInvoiceLines.OpenInvQty);
                            cmd.Parameters.AddWithValue("@Price", SalesInvoiceLines.Price);
                            cmd.Parameters.AddWithValue("@Currency", SalesInvoiceLines.Currency);
                            cmd.Parameters.AddWithValue("@LineTotal", SalesInvoiceLines.LineTotal);
                            cmd.Parameters.AddWithValue("@AcctCode", SalesInvoiceLines.AcctCode);
                            cmd.Parameters.AddWithValue("@DocDate", SalesInvoiceLines.DocDate);
                            cmd.Parameters.AddWithValue("@BaseCard", SalesInvoiceLines.BaseCard);
                            cmd.Parameters.AddWithValue("@TotalSumSy", SalesInvoiceLines.TotalSumSy);
                            cmd.Parameters.AddWithValue("@TotalFrgn", SalesInvoiceLines.TotalFrgn);
                            cmd.Parameters.AddWithValue("@VatSum", SalesInvoiceLines.VatSum);
                            cmd.Parameters.AddWithValue("@VatSumFrgn", SalesInvoiceLines.VatSumFrgn);
                            cmd.Parameters.AddWithValue("@VatSumSy", SalesInvoiceLines.VatSumSy);
                            cmd.Parameters.AddWithValue("@VatGroup", SalesInvoiceLines.VatGroup);
                            cmd.Parameters.AddWithValue("@VatPrcnt", SalesInvoiceLines.VatPrcnt);
                            cmd.Parameters.AddWithValue("@FinncPriod", SalesInvoiceLines.FinncPriod);
                            cmd.Parameters.AddWithValue("@ObjType", SalesInvoiceLines.ObjType);
                            cmd.Parameters.AddWithValue("@Address", SalesInvoiceLines.Address);
                            cmd.Parameters.AddWithValue("@GTotal", SalesInvoiceLines.Gtotal);
                            cmd.Parameters.AddWithValue("@GTotalFC", SalesInvoiceLines.GtotalFC);
                            cmd.Parameters.AddWithValue("@GTotalSC", SalesInvoiceLines.GtotalSC);
                            cmd.Parameters.AddWithValue("@StockSum", SalesInvoiceLines.StockSum);
                            cmd.Parameters.AddWithValue("@StockSumFc", SalesInvoiceLines.StockSumFc);
                            cmd.Parameters.AddWithValue("@StockSumSc", SalesInvoiceLines.StockSumSc);
                            cmd.Parameters.AddWithValue("@InvntSttus", SalesInvoiceLines.InvntSttus);
                            cmd.Parameters.AddWithValue("@WtLiable", SalesInvoiceLines.WtLiable);
                            cmd.Parameters.AddWithValue("@DataSource", SalesInvoiceLines.DataSource);
                            cmd.Parameters.AddWithValue("@UomCode", SalesInvoiceLines.UomCode);
                            cmd.Parameters.AddWithValue("@UomCode2", SalesInvoiceLines.UomCode2);
                            cmd.Parameters.AddWithValue("@UomEntry", SalesInvoiceLines.UomEntry);
                            cmd.Parameters.AddWithValue("@UomEntry2", SalesInvoiceLines.UomEntry2);
                            cmd.Parameters.AddWithValue("@NumPerMsr", SalesInvoiceLines.NumPerMsr);
                            cmd.Parameters.AddWithValue("@NumPerMsr2", SalesInvoiceLines.NumPerMsr2);
                            cmd.Parameters.AddWithValue("@unitMsr", SalesInvoiceLines.unitMsr);
                            cmd.Parameters.AddWithValue("@unitMsr2", SalesInvoiceLines.unitMsr2);
                            cmd.Parameters.AddWithValue("@StartValue", SalesInvoiceLines.StartValue);
                            cmd.Parameters.AddWithValue("@IsTax", SalesInvoiceLines.IsTax);

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

                return Tuple.Create(i,e.Message);
            }
        }



        public Tuple<int,string> InsertTaxHolding(List<TablaRetencionImpuesto> listTablaRetenciones)
        {
            int flag = 0;

            int i = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (TablaRetencionImpuesto TablaRetenciones in listTablaRetenciones)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "InsertTaxHoldingORIN";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@AbsEntry", TablaRetenciones.AbsEntry);
                            cmd.Parameters.AddWithValue("@WTCode", TablaRetenciones.WTCode);
                            cmd.Parameters.AddWithValue("@Type", TablaRetenciones.Type);
                            cmd.Parameters.AddWithValue("@Category", TablaRetenciones.Category);
                            cmd.Parameters.AddWithValue("@BaseType", TablaRetenciones.BaseType);
                            cmd.Parameters.AddWithValue("@Rate", TablaRetenciones.Rate);
                            cmd.Parameters.AddWithValue("@TaxbleAmnt", TablaRetenciones.TaxbleAmnt);
                            cmd.Parameters.AddWithValue("@TaxbleAmntFC", TablaRetenciones.TaxbleAmntFC);
                            cmd.Parameters.AddWithValue("@TaxbleAmntSC", TablaRetenciones.TaxbleAmntSC);
                            cmd.Parameters.AddWithValue("@WTAmnt", TablaRetenciones.WTAmnt);
                            cmd.Parameters.AddWithValue("@WTAmntFC", TablaRetenciones.WTAmntFC);
                            cmd.Parameters.AddWithValue("@WTAmntSC", TablaRetenciones.WTAmntSC);
                            cmd.Parameters.AddWithValue("@BaseAmnt", TablaRetenciones.BaseAmnt);
                            cmd.Parameters.AddWithValue("@BaseAmntFC", TablaRetenciones.BaseAmntFC);
                            cmd.Parameters.AddWithValue("@BaseAmntSC", TablaRetenciones.BaseAmntSC);
                            cmd.Parameters.AddWithValue("@LineNum", TablaRetenciones.LineNum);
                            cmd.Parameters.AddWithValue("@ObjType", TablaRetenciones.ObjType);
                            cmd.Parameters.AddWithValue("@Status", TablaRetenciones.Status);
                            cmd.Parameters.AddWithValue("@Account", TablaRetenciones.Account);

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

                return Tuple.Create(i,e.Message);
            }
        }


        public Tuple<int,string> FindDocEntry(int docNum)
        {
            int DocEntry = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FindDocEntryORIN";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@DocNum", docNum);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            DocEntry = Convert.ToInt32(reader["DocEntry"]);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(DocEntry,error);

            }
            catch (Exception e)
            {               

                Connection.Close();

                return Tuple.Create(DocEntry,e.Message);

            }
        }
    }
}
