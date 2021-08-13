using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Datos
{
    public class ModeloNotaCreditoProveedores : ModeloDocumento
    {
        public Tuple<string, int> UpdatePurchaseCreditNote(List<DocumentoCabecera> listPurchaseInvoice)
        {
            string error = null;

            int flag = 0;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (DocumentoCabecera Purchase in listPurchaseInvoice)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "UpdatePurchaseCreditNote";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@DocEntry", Purchase.DocEntry);
                            cmd.Parameters.AddWithValue("@DocNum", Purchase.DocNum);
                            cmd.Parameters.AddWithValue("@TaxDate", Purchase.TaxDate);
                            cmd.Parameters.AddWithValue("@DocDueDate", Purchase.DocDueDate);
                            cmd.Parameters.AddWithValue("@NumAtCard", Purchase.NumAtCard);
                            cmd.Parameters.AddWithValue("@NumControl", Purchase.NumControl);
                            cmd.Parameters.AddWithValue("@TipoTrans", Purchase.TipoTrans);
                            cmd.Parameters.AddWithValue("@Comments", Purchase.Comments);
                            cmd.Parameters.AddWithValue("@JrnlMemo", Purchase.JrnlMemo);
                            cmd.Parameters.AddWithValue("@LicTradNum", Purchase.LicTradNum);
                            cmd.Parameters.AddWithValue("@UserSign", Purchase.UserSign);
                            cmd.Parameters.AddWithValue("@UpdateDate", Purchase.UpdateDate);

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
                        cmd.CommandText = "SelectDocNumORPC";

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

        public Tuple<int, string> DeletePurchaseCreditNoteLines(int docEntryDeleted)
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
                        cmd.CommandText = "DeletePurchaseCreditNoteLines";

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

        public Tuple<int, string> InsertTaxHoldingPurchaseCreditNotePreliminar(List<TablaRetencionImpuesto> listTablaRetenciones)
        {
            //LoadDatatable INV5_TEST

            dtRPC5 = dataSet.Tables["RPC5_TEST"];

            int flag = 0;

            string error = null;

            int DocEntry = 0;

            try
            {
                foreach (TablaRetencionImpuesto TablaRetenciones in listTablaRetenciones)
                {
                    DocEntry = TablaRetenciones.AbsEntry;

                    DataRow newRow = dtRPC5.NewRow();

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

                    dtRPC5.Rows.Add(newRow);

                    dtRPC5.AcceptChanges();
                }

                DataRow[] selected = dtRPC5.Select("AbsEntry = " + DocEntry);

                flag = selected.Count();

                return Tuple.Create(flag, error);

            }
            catch (Exception e)
            {
                return Tuple.Create(flag, e.Message);
            }
        }

        public Tuple<int, string> InsertPurchaseCreditNoteLinesPreliminar(List<DocumentoDetalle> listPurchaseInvoiceLines)
        {
            //LoadDatatable INV1_TEST

            dtRPC1 = dataSet.Tables["RPC1_TEST"];

            int flag = 0;

            string error = null;

            int DocEntry = 0;

            try
            {
                foreach (DocumentoDetalle SalesInvoice in listPurchaseInvoiceLines)
                {
                    DocEntry = SalesInvoice.DocEntry;

                    DataRow newRow = dtRPC1.NewRow();

                    newRow["DocEntry"] = SalesInvoice.DocEntry;
                    newRow["DocDate"] = SalesInvoice.DocDate;
                    newRow["LineNum"] = SalesInvoice.LineNum;
                    newRow["LineStatus"] = SalesInvoice.LineStatus;
                    newRow["ItemCode"] = SalesInvoice.ItemCode;
                    newRow["Dscription"] = SalesInvoice.Dscription;
                    newRow["Quantity"] = SalesInvoice.Quantity;
                    newRow["OpenQty"] = SalesInvoice.OpenQty;
                    newRow["InvQty"] = SalesInvoice.InvQty;
                    newRow["OpenInvQty"] = SalesInvoice.OpenInvQty;
                    newRow["Price"] = SalesInvoice.Price;
                    newRow["Currency"] = SalesInvoice.Currency;
                    newRow["LineTotal"] = SalesInvoice.LineTotal;
                    newRow["ObjType"] = SalesInvoice.ObjType;
                    newRow["AcctCode"] = SalesInvoice.AcctCode;
                    newRow["DocDate"] = SalesInvoice.DocDate;
                    newRow["BaseCard"] = SalesInvoice.BaseCard;
                    newRow["TotalSumSy"] = SalesInvoice.TotalSumSy;
                    newRow["TotalFrgn"] = SalesInvoice.TotalFrgn;
                    newRow["VatSum"] = SalesInvoice.VatSum;
                    newRow["VatSumFrgn"] = SalesInvoice.VatSumFrgn;
                    newRow["VatGroup"] = SalesInvoice.VatGroup;
                    newRow["VatPrcnt"] = SalesInvoice.VatPrcnt;
                    newRow["FinncPriod"] = SalesInvoice.FinncPriod;
                    newRow["ObjType"] = SalesInvoice.ObjType;
                    newRow["Address"] = SalesInvoice.Address;
                    newRow["GTotal"] = SalesInvoice.Gtotal;
                    newRow["GTotalFC"] = SalesInvoice.GtotalFC;
                    newRow["GTotalSC"] = SalesInvoice.GtotalSC;
                    newRow["StockSum"] = SalesInvoice.StockSum;
                    newRow["StockSumFc"] = SalesInvoice.StockSumFc;
                    newRow["StockSumSc"] = SalesInvoice.StockSumSc;
                    newRow["InvntSttus"] = SalesInvoice.InvntSttus;
                    newRow["WtLiable"] = SalesInvoice.WtLiable;
                    newRow["DataSource"] = SalesInvoice.DataSource;

                    dtRPC1.Rows.Add(newRow);

                    dtRPC1.AcceptChanges();
                }

                DataRow[] selected = dtRPC1.Select("DocEntry = " + DocEntry);

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

                DataRow dataRow = dtORPC.AsEnumerable().Where(r => ((int)r["DocNum"]).Equals(docNum) && ((string)r["DocSubType"]).Equals("--")).First();

                DocEntry = Convert.ToInt32(dataRow["DocEntry"]);

                return Tuple.Create(DocEntry, error);

            }
            catch (Exception e)
            {

                return Tuple.Create(DocEntry, e.Message);

            }
        }

        public Tuple<int, string> InsertSalesCreditNotePreliminar(List<DocumentoCabecera> listSalesInvoice)
        {
            //LoadDatatable OINV_TEST

            dtORPC = dataSet.Tables["ORPC_TEST"];

            int flag = 0;

            string error = null;

            try
            {
                foreach (DocumentoCabecera SalesInvoice in listSalesInvoice)
                {
                    int DocEntry = dtORPC.Rows.Count;

                    DataRow newRow = dtORPC.NewRow();

                    newRow["DocNum"] = SalesInvoice.DocNum;
                    newRow["DocDate"] = SalesInvoice.DocDate;
                    newRow["TaxDate"] = SalesInvoice.TaxDate;
                    newRow["DocDueDate"] = SalesInvoice.DocDueDate;
                    newRow["Comments"] = SalesInvoice.Comments;
                    newRow["DocType"] = SalesInvoice.DocType;
                    newRow["CANCELED"] = SalesInvoice.Canceled;
                    newRow["UserSign"] = SalesInvoice.UserSign;
                    newRow["UpdateDate"] = SalesInvoice.UpdateDate;
                    newRow["FinncPriod"] = SalesInvoice.FinncPriod;
                    newRow["Address2"] = SalesInvoice.Address2;
                    newRow["InvntSttus"] = SalesInvoice.InvntSttus;
                    newRow["VatSum"] = SalesInvoice.VatSum;
                    newRow["VatSumFC"] = SalesInvoice.VatSumFC;
                    newRow["VatSumSy"] = SalesInvoice.VatSumSy1;
                    newRow["DocCur"] = SalesInvoice.DocCurr;
                    newRow["ObjType"] = SalesInvoice.ObjType;
                    newRow["CardCode"] = SalesInvoice.CardCode;
                    newRow["CardName"] = SalesInvoice.CardName;
                    newRow["JrnlMemo"] = SalesInvoice.JrnlMemo;
                    newRow["LicTradNum"] = SalesInvoice.LicTradNum;
                    newRow["TransId"] = SalesInvoice.TransId;
                    newRow["VatPaid"] = SalesInvoice.VatPaid;
                    newRow["VatPaidFC"] = SalesInvoice.VatPaidFC;
                    newRow["VatPaidSys"] = SalesInvoice.VatPaidSys;
                    newRow["PaidToDate"] = SalesInvoice.PaidToDate;
                    newRow["PaidSum"] = SalesInvoice.PaidSum;
                    newRow["PaidSumFc"] = SalesInvoice.PaidSumFc;
                    newRow["PaidSumSc"] = SalesInvoice.PaidSumSc;
                    newRow["WTApplied"] = SalesInvoice.WTApplied;
                    newRow["WTAppliedF"] = SalesInvoice.WTAppliedF;
                    newRow["WTAppliedS"] = SalesInvoice.WTAppliedS;
                    newRow["WTSum"] = SalesInvoice.WTSum;
                    newRow["WTSumFC"] = SalesInvoice.WTSumFC;
                    newRow["WTSumSC"] = SalesInvoice.WTSumSC;
                    newRow["SysRate"] = SalesInvoice.SysRate;
                    newRow["DocRate"] = SalesInvoice.DocRate;
                    newRow["CtlAccount"] = SalesInvoice.CtlAccount;
                    newRow["BaseAmnt"] = SalesInvoice.BaseAmnt;
                    newRow["BaseAmntFC"] = SalesInvoice.BaseAmntFC;
                    newRow["DocTotal"] = SalesInvoice.DocTotal;
                    newRow["DocTotalFC"] = SalesInvoice.DocTotalFC;
                    newRow["DocTotalSy"] = SalesInvoice.DocTotalSy;
                    newRow["DocSubType"] = SalesInvoice.DocSubType;
                    newRow["NumAtCard"] = SalesInvoice.NumAtCard;
                    newRow["U_IDA_NumControl"] = SalesInvoice.NumControl;
                    newRow["U_IDA_TipoTrans"] = SalesInvoice.TipoTrans;
                    newRow["Max1099"] = SalesInvoice.Max1099;

                    dtORPC.Rows.Add(newRow);

                    if (dtORPC.Rows.Contains(DocEntry + 1) == true)
                    {
                        flag = 1;
                    }
                    else
                    {
                        flag = 0;
                    }

                    dtORPC.AcceptChanges();
                }

                return Tuple.Create(flag, error);

            }
            catch (Exception e)
            {

                return Tuple.Create(flag, e.Message);
            }
        }

        public Tuple<int, string> DeletePurchaseCreditNoteRetenciones(int docEntryDeleted)
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
                        cmd.CommandText = "DeletePurchaseCreditNoteRetenciones";

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

        public Tuple<int, string> DeletePurchaseCreditNote(int docEntryDeleted)
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
                        cmd.CommandText = "DeletePurchaseCreditNote";

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

        public Tuple<List<DocumentoCabecera>,string> FindFirstPurchaseCreditNote()
        {
            List<DocumentoCabecera> newListPurchaseInvoice = new List<DocumentoCabecera>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FirstPurchaseCreditNote";

                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            DocumentoCabecera newPurchaseInvoice = new DocumentoCabecera();

                            newPurchaseInvoice.DocEntry = Convert.ToInt32(reader["DocEntry"]);
                            newPurchaseInvoice.DocNum = Convert.ToInt32(reader["DocNum"]);
                            newPurchaseInvoice.CardCode = reader["CardCode"].ToString();
                            newPurchaseInvoice.CardName = reader["CardName"].ToString();
                            newPurchaseInvoice.NumAtCard = reader["NumAtCard"].ToString();
                            newPurchaseInvoice.DocStatus = Convert.ToChar(reader["DocStatus"]);
                            newPurchaseInvoice.NumControl = reader["U_IDA_NumControl"].ToString();
                            newPurchaseInvoice.DocCurr = reader["DocCur"].ToString();
                            newPurchaseInvoice.TipoTrans = reader["U_IDA_TipoTrans"].ToString();
                            newPurchaseInvoice.DocDate = Convert.ToDateTime(reader["DocDate"]);
                            newPurchaseInvoice.TaxDate = Convert.ToDateTime(reader["TaxDate"]);
                            newPurchaseInvoice.DocDueDate = Convert.ToDateTime(reader["DocDueDate"]);
                            newPurchaseInvoice.Address2 = reader["Address2"].ToString();
                            newPurchaseInvoice.Comments = reader["Comments"].ToString();
                            newPurchaseInvoice.CtlAccount = reader["CtlAccount"].ToString();
                            newPurchaseInvoice.JrnlMemo = reader["JrnlMemo"].ToString();
                            newPurchaseInvoice.LicTradNum = reader["LicTradNum"].ToString();
                            newPurchaseInvoice.DocType = Convert.ToChar(reader["DocType"]);
                            newPurchaseInvoice.DocTotal = Convert.ToDecimal(reader["DocTotal"]);
                            newPurchaseInvoice.DocTotalFC = Convert.ToDecimal(reader["DocTotalFC"]);
                            newPurchaseInvoice.DocTotalSy = Convert.ToDecimal(reader["DocTotalSy"]);
                            newPurchaseInvoice.VatSum = Convert.ToDecimal(reader["VatSum"]);
                            newPurchaseInvoice.VatSumFC = Convert.ToDecimal(reader["VatSumFC"]);
                            newPurchaseInvoice.VatSumSy1 = Convert.ToDecimal(reader["VatSumSy"]);
                            newPurchaseInvoice.WTSum = Convert.ToDecimal(reader["WTSum"]);
                            newPurchaseInvoice.WTSumFC = Convert.ToDecimal(reader["WTSumFC"]);
                            newPurchaseInvoice.WTSumSC = Convert.ToDecimal(reader["WTSumSC"]);
                            newPurchaseInvoice.BaseAmnt = Convert.ToDecimal(reader["BaseAmnt"]);
                            newPurchaseInvoice.BaseAmntFC = Convert.ToDecimal(reader["BaseAmntFC"]);
                            newPurchaseInvoice.BaseAmntSC = Convert.ToDecimal(reader["BaseAmntSC"]);

                            newListPurchaseInvoice.Add(newPurchaseInvoice);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(newListPurchaseInvoice,error);

            }
            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(newListPurchaseInvoice,e.Message);
            }
        }

        

        public Tuple<List<DocumentoCabecera>,string> FindPurchaseCreditNote(List<DocumentoCabecera> listaPurchaseInvoice)
        {
            List<DocumentoCabecera> newListPurchase = new List<DocumentoCabecera>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (DocumentoCabecera PurchaseInvoice in listaPurchaseInvoice)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "FindPurchaseCreditNote";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@DocNum", PurchaseInvoice.DocNum);
                            cmd.Parameters.AddWithValue("@DocDate", PurchaseInvoice.DocDate);
                            cmd.Parameters.AddWithValue("@TaxDate", PurchaseInvoice.TaxDate);
                            cmd.Parameters.AddWithValue("@DocDueDate", PurchaseInvoice.DocDueDate);
                            cmd.Parameters.AddWithValue("@CardCode", PurchaseInvoice.CardCode);
                            cmd.Parameters.AddWithValue("@CardName", PurchaseInvoice.CardName);
                            cmd.Parameters.AddWithValue("@NumAtCard", PurchaseInvoice.NumAtCard);
                            cmd.Parameters.AddWithValue("@NumControl", PurchaseInvoice.NumControl);
                            cmd.Parameters.AddWithValue("@TipoTrans", PurchaseInvoice.TipoTrans);
                            cmd.Parameters.AddWithValue("@Comments", PurchaseInvoice.Comments);

                            SqlDataReader reader = cmd.ExecuteReader();

                            while (reader.Read())
                            {
                                DocumentoCabecera newPurchase = new DocumentoCabecera();

                                newPurchase.DocEntry = Convert.ToInt32(reader["DocEntry"]);
                                newPurchase.DocNum = Convert.ToInt32(reader["DocNum"]);
                                newPurchase.CardCode = reader["CardCode"].ToString();
                                newPurchase.CardName = reader["CardName"].ToString();
                                newPurchase.NumAtCard = reader["NumAtCard"].ToString();
                                newPurchase.DocStatus = Convert.ToChar(reader["DocStatus"]);
                                newPurchase.NumControl = reader["U_IDA_NumControl"].ToString();
                                newPurchase.DocCurr = reader["DocCur"].ToString();
                                newPurchase.TipoTrans = reader["U_IDA_TipoTrans"].ToString();
                                newPurchase.DocDate = Convert.ToDateTime(reader["DocDate"]);
                                newPurchase.TaxDate = Convert.ToDateTime(reader["TaxDate"]);
                                newPurchase.DocDueDate = Convert.ToDateTime(reader["DocDueDate"]);
                                newPurchase.Address2 = reader["Address2"].ToString();
                                newPurchase.Comments = reader["Comments"].ToString();
                                newPurchase.CtlAccount = reader["CtlAccount"].ToString();
                                newPurchase.JrnlMemo = reader["JrnlMemo"].ToString();
                                newPurchase.LicTradNum = reader["LicTradNum"].ToString();
                                newPurchase.DocType = Convert.ToChar(reader["DocType"]);
                                newPurchase.DocTotal = Convert.ToDecimal(reader["DocTotal"]);
                                newPurchase.DocTotalFC = Convert.ToDecimal(reader["DocTotalFC"]);
                                newPurchase.DocTotalSy = Convert.ToDecimal(reader["DocTotalSy"]);
                                newPurchase.VatSum = Convert.ToDecimal(reader["VatSum"]);
                                newPurchase.VatSumFC = Convert.ToDecimal(reader["VatSumFC"]);
                                newPurchase.VatSumSy1 = Convert.ToDecimal(reader["VatSumSy"]);
                                newPurchase.WTSum = Convert.ToDecimal(reader["WTSum"]);
                                newPurchase.WTSumFC = Convert.ToDecimal(reader["WTSumFC"]);
                                newPurchase.WTSumSC = Convert.ToDecimal(reader["WTSumSC"]);
                                newPurchase.BaseAmnt = Convert.ToDecimal(reader["BaseAmnt"]);
                                newPurchase.BaseAmntFC = Convert.ToDecimal(reader["BaseAmntFC"]);
                                newPurchase.BaseAmntSC = Convert.ToDecimal(reader["BaseAmntSC"]);

                                newListPurchase.Add(newPurchase);

                            }

                            reader.Close();
                        }
                    }

                }

                Connection.Close();

                return Tuple.Create(newListPurchase,error);

            }
            catch (Exception e)
            {
                Connection.Close();

                return Tuple.Create(newListPurchase,e.Message);
            }
        }

        public Tuple<List<DocumentoCabecera>,string> FindPreviousPurchaseCreditNote(string docNum)
        {
            List<DocumentoCabecera> newListPurchaseInvoice = new List<DocumentoCabecera>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "PreviousPurchaseCreditNote";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@DocNum", docNum);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            DocumentoCabecera newPurchaseInvoice = new DocumentoCabecera();

                            newPurchaseInvoice.DocEntry = Convert.ToInt32(reader["DocEntry"]);
                            newPurchaseInvoice.DocNum = Convert.ToInt32(reader["DocNum"]);
                            newPurchaseInvoice.CardCode = reader["CardCode"].ToString();
                            newPurchaseInvoice.CardName = reader["CardName"].ToString();
                            newPurchaseInvoice.NumAtCard = reader["NumAtCard"].ToString();
                            newPurchaseInvoice.DocStatus = Convert.ToChar(reader["DocStatus"]);
                            newPurchaseInvoice.NumControl = reader["U_IDA_NumControl"].ToString();
                            newPurchaseInvoice.DocCurr = reader["DocCur"].ToString();
                            newPurchaseInvoice.TipoTrans = reader["U_IDA_TipoTrans"].ToString();
                            newPurchaseInvoice.DocDate = Convert.ToDateTime(reader["DocDate"]);
                            newPurchaseInvoice.TaxDate = Convert.ToDateTime(reader["TaxDate"]);
                            newPurchaseInvoice.DocDueDate = Convert.ToDateTime(reader["DocDueDate"]);
                            newPurchaseInvoice.Address2 = reader["Address2"].ToString();
                            newPurchaseInvoice.Comments = reader["Comments"].ToString();
                            newPurchaseInvoice.CtlAccount = reader["CtlAccount"].ToString();
                            newPurchaseInvoice.JrnlMemo = reader["JrnlMemo"].ToString();
                            newPurchaseInvoice.LicTradNum = reader["LicTradNum"].ToString();
                            newPurchaseInvoice.DocType = Convert.ToChar(reader["DocType"]);
                            newPurchaseInvoice.DocTotal = Convert.ToDecimal(reader["DocTotal"]);
                            newPurchaseInvoice.DocTotalFC = Convert.ToDecimal(reader["DocTotalFC"]);
                            newPurchaseInvoice.DocTotalSy = Convert.ToDecimal(reader["DocTotalSy"]);
                            newPurchaseInvoice.VatSum = Convert.ToDecimal(reader["VatSum"]);
                            newPurchaseInvoice.VatSumFC = Convert.ToDecimal(reader["VatSumFC"]);
                            newPurchaseInvoice.VatSumSy1 = Convert.ToDecimal(reader["VatSumSy"]);
                            newPurchaseInvoice.WTSum = Convert.ToDecimal(reader["WTSum"]);
                            newPurchaseInvoice.WTSumFC = Convert.ToDecimal(reader["WTSumFC"]);
                            newPurchaseInvoice.WTSumSC = Convert.ToDecimal(reader["WTSumSC"]);
                            newPurchaseInvoice.BaseAmnt = Convert.ToDecimal(reader["BaseAmnt"]);
                            newPurchaseInvoice.BaseAmntFC = Convert.ToDecimal(reader["BaseAmntFC"]);
                            newPurchaseInvoice.BaseAmntSC = Convert.ToDecimal(reader["BaseAmntSC"]);

                            newListPurchaseInvoice.Add(newPurchaseInvoice);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(newListPurchaseInvoice,error);

            }
            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(newListPurchaseInvoice,e.Message);
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
                            cmd.CommandText = "InsertTaxHoldingORPC";

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

        public Tuple<DataTable,string> FindPurchaseCreditNoteLines(int docEntry)
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
                        cmd.CommandText = "FindPurchaseCreditNoteLines";

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
                        cmd.CommandText = "FindDocEntryORPC";

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

        public Tuple<int,string> InsertPurchaseCreditNote(List<DocumentoCabecera> listaPurchaseInvoice)
        {
            int flag = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (DocumentoCabecera PurchaseInvoice in listaPurchaseInvoice)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "InsertPurchaseCreditNote";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@DocNum", PurchaseInvoice.DocNum);
                            cmd.Parameters.AddWithValue("@DocDate", PurchaseInvoice.DocDate);
                            cmd.Parameters.AddWithValue("@TaxDate", PurchaseInvoice.TaxDate);
                            cmd.Parameters.AddWithValue("@DocDueDate", PurchaseInvoice.DocDueDate);
                            cmd.Parameters.AddWithValue("@Comments", PurchaseInvoice.Comments);
                            cmd.Parameters.AddWithValue("@DocType", PurchaseInvoice.DocType);
                            cmd.Parameters.AddWithValue("@Canceled", PurchaseInvoice.Canceled);
                            cmd.Parameters.AddWithValue("@UserSign", PurchaseInvoice.UserSign);
                            cmd.Parameters.AddWithValue("@UpdateDate", PurchaseInvoice.UpdateDate);
                            cmd.Parameters.AddWithValue("@DocStatus", PurchaseInvoice.DocStatus);
                            cmd.Parameters.AddWithValue("@FinncPriod", PurchaseInvoice.FinncPriod);
                            cmd.Parameters.AddWithValue("@Address2", PurchaseInvoice.Address2);
                            cmd.Parameters.AddWithValue("@InvntSttus", PurchaseInvoice.InvntSttus);
                            cmd.Parameters.AddWithValue("@VatSum", PurchaseInvoice.VatSum);
                            cmd.Parameters.AddWithValue("@VatSumFC", PurchaseInvoice.VatSumFC);
                            cmd.Parameters.AddWithValue("@VatSumSy", PurchaseInvoice.VatSumSy1);
                            cmd.Parameters.AddWithValue("@DocCurr", PurchaseInvoice.DocCurr);
                            cmd.Parameters.AddWithValue("@ObjType", PurchaseInvoice.ObjType);
                            cmd.Parameters.AddWithValue("@CardCode", PurchaseInvoice.CardCode);
                            cmd.Parameters.AddWithValue("@CardName", PurchaseInvoice.CardName);
                            cmd.Parameters.AddWithValue("@JrnlMemo", PurchaseInvoice.JrnlMemo);
                            cmd.Parameters.AddWithValue("@LicTradNum", PurchaseInvoice.LicTradNum);
                            cmd.Parameters.AddWithValue("@TransId", PurchaseInvoice.TransId);
                            cmd.Parameters.AddWithValue("@VatPaid", PurchaseInvoice.VatPaid);
                            cmd.Parameters.AddWithValue("@VatPaidFC", PurchaseInvoice.VatPaidFC);
                            cmd.Parameters.AddWithValue("@VatPaidSys", PurchaseInvoice.VatPaidSys);
                            cmd.Parameters.AddWithValue("@PaidToDate", PurchaseInvoice.PaidToDate);
                            cmd.Parameters.AddWithValue("@PaidSum", PurchaseInvoice.PaidSum);
                            cmd.Parameters.AddWithValue("@PaidSumFc", PurchaseInvoice.PaidSumFc);
                            cmd.Parameters.AddWithValue("@PaidSumSc", PurchaseInvoice.PaidSumSc);
                            cmd.Parameters.AddWithValue("@WTApplied", PurchaseInvoice.WTApplied);
                            cmd.Parameters.AddWithValue("@WTAppliedF", PurchaseInvoice.WTAppliedF);
                            cmd.Parameters.AddWithValue("@WTAppliedS", PurchaseInvoice.WTAppliedS);
                            cmd.Parameters.AddWithValue("@WTSum", PurchaseInvoice.WTSum);
                            cmd.Parameters.AddWithValue("@WTSumFC", PurchaseInvoice.WTSumFC);
                            cmd.Parameters.AddWithValue("@WTSumSC", PurchaseInvoice.WTSumSC);
                            cmd.Parameters.AddWithValue("@SysRate", PurchaseInvoice.SysRate);
                            cmd.Parameters.AddWithValue("@DocRate", PurchaseInvoice.DocRate);
                            cmd.Parameters.AddWithValue("@CtlAccount", PurchaseInvoice.CtlAccount);
                            cmd.Parameters.AddWithValue("@BaseAmnt", PurchaseInvoice.BaseAmnt);
                            cmd.Parameters.AddWithValue("@BaseAmntFC", PurchaseInvoice.BaseAmntFC);
                            cmd.Parameters.AddWithValue("@BaseAmntSC", PurchaseInvoice.BaseAmntSC);
                            cmd.Parameters.AddWithValue("@DocTotal", PurchaseInvoice.DocTotal);
                            cmd.Parameters.AddWithValue("@DocTotalFC", PurchaseInvoice.DocTotalFC);
                            cmd.Parameters.AddWithValue("@DocTotalSy", PurchaseInvoice.DocTotalSy);
                            cmd.Parameters.AddWithValue("@DocSubType", PurchaseInvoice.DocSubType);
                            cmd.Parameters.AddWithValue("@NumAtCard", PurchaseInvoice.NumAtCard);
                            cmd.Parameters.AddWithValue("@NumControl", PurchaseInvoice.NumControl);
                            cmd.Parameters.AddWithValue("@TipoTrans", PurchaseInvoice.TipoTrans);
                            cmd.Parameters.AddWithValue("@Max1099", PurchaseInvoice.Max1099);
                            cmd.Parameters.AddWithValue("@Fafe", PurchaseInvoice.Fafe);

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

        public Tuple<List<DocumentoCabecera>,string> FindLastPurchaseCreditNote()
        {
            List<DocumentoCabecera> newListPurchaseInvoice = new List<DocumentoCabecera>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "LastPurchaseCreditNote";

                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            DocumentoCabecera newPurchaseInvoice = new DocumentoCabecera();

                            newPurchaseInvoice.DocEntry = Convert.ToInt32(reader["DocEntry"]);
                            newPurchaseInvoice.DocNum = Convert.ToInt32(reader["DocNum"]);
                            newPurchaseInvoice.CardCode = reader["CardCode"].ToString();
                            newPurchaseInvoice.CardName = reader["CardName"].ToString();
                            newPurchaseInvoice.NumAtCard = reader["NumAtCard"].ToString();
                            newPurchaseInvoice.DocStatus = Convert.ToChar(reader["DocStatus"]);
                            newPurchaseInvoice.NumControl = reader["U_IDA_NumControl"].ToString();
                            newPurchaseInvoice.DocCurr = reader["DocCur"].ToString();
                            newPurchaseInvoice.TipoTrans = reader["U_IDA_TipoTrans"].ToString();
                            newPurchaseInvoice.DocDate = Convert.ToDateTime(reader["DocDate"]);
                            newPurchaseInvoice.TaxDate = Convert.ToDateTime(reader["TaxDate"]);
                            newPurchaseInvoice.DocDueDate = Convert.ToDateTime(reader["DocDueDate"]);
                            newPurchaseInvoice.Address2 = reader["Address2"].ToString();
                            newPurchaseInvoice.Comments = reader["Comments"].ToString();
                            newPurchaseInvoice.CtlAccount = reader["CtlAccount"].ToString();
                            newPurchaseInvoice.JrnlMemo = reader["JrnlMemo"].ToString();
                            newPurchaseInvoice.LicTradNum = reader["LicTradNum"].ToString();
                            newPurchaseInvoice.DocType = Convert.ToChar(reader["DocType"]);
                            newPurchaseInvoice.DocTotal = Convert.ToDecimal(reader["DocTotal"]);
                            newPurchaseInvoice.DocTotalFC = Convert.ToDecimal(reader["DocTotalFC"]);
                            newPurchaseInvoice.DocTotalSy = Convert.ToDecimal(reader["DocTotalSy"]);
                            newPurchaseInvoice.VatSum = Convert.ToDecimal(reader["VatSum"]);
                            newPurchaseInvoice.VatSumFC = Convert.ToDecimal(reader["VatSumFC"]);
                            newPurchaseInvoice.VatSumSy1 = Convert.ToDecimal(reader["VatSumSy"]);
                            newPurchaseInvoice.WTSum = Convert.ToDecimal(reader["WTSum"]);
                            newPurchaseInvoice.WTSumFC = Convert.ToDecimal(reader["WTSumFC"]);
                            newPurchaseInvoice.WTSumSC = Convert.ToDecimal(reader["WTSumSC"]);
                            newPurchaseInvoice.BaseAmnt = Convert.ToDecimal(reader["BaseAmnt"]);
                            newPurchaseInvoice.BaseAmntFC = Convert.ToDecimal(reader["BaseAmntFC"]);
                            newPurchaseInvoice.BaseAmntSC = Convert.ToDecimal(reader["BaseAmntSC"]);

                            newListPurchaseInvoice.Add(newPurchaseInvoice);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(newListPurchaseInvoice,error);

            }
            catch (Exception e)
            {
                Connection.Close();

                return Tuple.Create(newListPurchaseInvoice,e.Message);
            }
        }

        public Tuple<int,string> InsertPurchaseCreditNoteLines(List<DocumentoDetalle> listPurchaseInvoiceLines)
        {
            int flag = 0;

            int i = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (DocumentoDetalle PurchaseInvoiceLines in listPurchaseInvoiceLines)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "InsertPurchaseCreditNoteLines";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@DocEntry", PurchaseInvoiceLines.DocEntry);
                            cmd.Parameters.AddWithValue("@LineNum", PurchaseInvoiceLines.LineNum);
                            cmd.Parameters.AddWithValue("@LineStatus", PurchaseInvoiceLines.LineStatus);
                            cmd.Parameters.AddWithValue("@ItemCode", PurchaseInvoiceLines.ItemCode);
                            cmd.Parameters.AddWithValue("@Dscription", PurchaseInvoiceLines.Dscription);
                            cmd.Parameters.AddWithValue("@Quantity", PurchaseInvoiceLines.Quantity);
                            cmd.Parameters.AddWithValue("@OpenQty", PurchaseInvoiceLines.OpenQty);
                            cmd.Parameters.AddWithValue("@InvQty", PurchaseInvoiceLines.InvQty);
                            cmd.Parameters.AddWithValue("@OpenInvQty", PurchaseInvoiceLines.OpenInvQty);
                            cmd.Parameters.AddWithValue("@Price", PurchaseInvoiceLines.Price);
                            cmd.Parameters.AddWithValue("@Currency", PurchaseInvoiceLines.Currency);
                            cmd.Parameters.AddWithValue("@LineTotal", PurchaseInvoiceLines.LineTotal);
                            cmd.Parameters.AddWithValue("@AcctCode", PurchaseInvoiceLines.AcctCode);
                            cmd.Parameters.AddWithValue("@DocDate", PurchaseInvoiceLines.DocDate);
                            cmd.Parameters.AddWithValue("@BaseCard", PurchaseInvoiceLines.BaseCard);
                            cmd.Parameters.AddWithValue("@TotalSumSy", PurchaseInvoiceLines.TotalSumSy);
                            cmd.Parameters.AddWithValue("@TotalFrgn", PurchaseInvoiceLines.TotalFrgn);
                            cmd.Parameters.AddWithValue("@VatSum", PurchaseInvoiceLines.VatSum);
                            cmd.Parameters.AddWithValue("@VatSumFrgn", PurchaseInvoiceLines.VatSumFrgn);
                            cmd.Parameters.AddWithValue("@VatSumSy", PurchaseInvoiceLines.VatSumSy);
                            cmd.Parameters.AddWithValue("@VatGroup", PurchaseInvoiceLines.VatGroup);
                            cmd.Parameters.AddWithValue("@VatPrcnt", PurchaseInvoiceLines.VatPrcnt);
                            cmd.Parameters.AddWithValue("@FinncPriod", PurchaseInvoiceLines.FinncPriod);
                            cmd.Parameters.AddWithValue("@ObjType", PurchaseInvoiceLines.ObjType);
                            cmd.Parameters.AddWithValue("@Address", PurchaseInvoiceLines.Address);
                            cmd.Parameters.AddWithValue("@GTotal", PurchaseInvoiceLines.Gtotal);
                            cmd.Parameters.AddWithValue("@GTotalFC", PurchaseInvoiceLines.GtotalFC);
                            cmd.Parameters.AddWithValue("@GTotalSC", PurchaseInvoiceLines.GtotalSC);
                            cmd.Parameters.AddWithValue("@StockSum", PurchaseInvoiceLines.StockSum);
                            cmd.Parameters.AddWithValue("@StockSumFc", PurchaseInvoiceLines.StockSumFc);
                            cmd.Parameters.AddWithValue("@StockSumSc", PurchaseInvoiceLines.StockSumSc);
                            cmd.Parameters.AddWithValue("@InvntSttus", PurchaseInvoiceLines.InvntSttus);
                            cmd.Parameters.AddWithValue("@WtLiable", PurchaseInvoiceLines.WtLiable);
                            cmd.Parameters.AddWithValue("@DataSource", PurchaseInvoiceLines.DataSource);
                            cmd.Parameters.AddWithValue("@UomCode", PurchaseInvoiceLines.UomCode);
                            cmd.Parameters.AddWithValue("@UomCode2", PurchaseInvoiceLines.UomCode2);
                            cmd.Parameters.AddWithValue("@UomEntry", PurchaseInvoiceLines.UomEntry);
                            cmd.Parameters.AddWithValue("@UomEntry2", PurchaseInvoiceLines.UomEntry2);
                            cmd.Parameters.AddWithValue("@NumPerMsr", PurchaseInvoiceLines.NumPerMsr);
                            cmd.Parameters.AddWithValue("@NumPerMsr2", PurchaseInvoiceLines.NumPerMsr2);
                            cmd.Parameters.AddWithValue("@unitMsr", PurchaseInvoiceLines.unitMsr);
                            cmd.Parameters.AddWithValue("@unitMsr2", PurchaseInvoiceLines.unitMsr2);
                            cmd.Parameters.AddWithValue("@StartValue", PurchaseInvoiceLines.StartValue);
                            cmd.Parameters.AddWithValue("@IsTax", PurchaseInvoiceLines.IsTax);

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
                        cmd.CommandText = "FindRetencionImpuestoORPC";

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

        public Tuple<List<DocumentoCabecera>,string> FindNextPurchaseCreditNote(string docNum)
        {
            List<DocumentoCabecera> newListPurchaseInvoice = new List<DocumentoCabecera>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "NextPurchaseCreditNote";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@DocNum", docNum);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            DocumentoCabecera newPurchaseInvoice = new DocumentoCabecera();

                            newPurchaseInvoice.DocEntry = Convert.ToInt32(reader["DocEntry"]);
                            newPurchaseInvoice.DocNum = Convert.ToInt32(reader["DocNum"]);
                            newPurchaseInvoice.CardCode = reader["CardCode"].ToString();
                            newPurchaseInvoice.CardName = reader["CardName"].ToString();
                            newPurchaseInvoice.NumAtCard = reader["NumAtCard"].ToString();
                            newPurchaseInvoice.DocStatus = Convert.ToChar(reader["DocStatus"]);
                            newPurchaseInvoice.NumControl = reader["U_IDA_NumControl"].ToString();
                            newPurchaseInvoice.DocCurr = reader["DocCur"].ToString();
                            newPurchaseInvoice.TipoTrans = reader["U_IDA_TipoTrans"].ToString();
                            newPurchaseInvoice.DocDate = Convert.ToDateTime(reader["DocDate"]);
                            newPurchaseInvoice.TaxDate = Convert.ToDateTime(reader["TaxDate"]);
                            newPurchaseInvoice.DocDueDate = Convert.ToDateTime(reader["DocDueDate"]);
                            newPurchaseInvoice.Address2 = reader["Address2"].ToString();
                            newPurchaseInvoice.Comments = reader["Comments"].ToString();
                            newPurchaseInvoice.CtlAccount = reader["CtlAccount"].ToString();
                            newPurchaseInvoice.JrnlMemo = reader["JrnlMemo"].ToString();
                            newPurchaseInvoice.LicTradNum = reader["LicTradNum"].ToString();
                            newPurchaseInvoice.DocType = Convert.ToChar(reader["DocType"]);
                            newPurchaseInvoice.DocTotal = Convert.ToDecimal(reader["DocTotal"]);
                            newPurchaseInvoice.DocTotalFC = Convert.ToDecimal(reader["DocTotalFC"]);
                            newPurchaseInvoice.DocTotalSy = Convert.ToDecimal(reader["DocTotalSy"]);
                            newPurchaseInvoice.VatSum = Convert.ToDecimal(reader["VatSum"]);
                            newPurchaseInvoice.VatSumFC = Convert.ToDecimal(reader["VatSumFC"]);
                            newPurchaseInvoice.VatSumSy1 = Convert.ToDecimal(reader["VatSumSy"]);
                            newPurchaseInvoice.WTSum = Convert.ToDecimal(reader["WTSum"]);
                            newPurchaseInvoice.WTSumFC = Convert.ToDecimal(reader["WTSumFC"]);
                            newPurchaseInvoice.WTSumSC = Convert.ToDecimal(reader["WTSumSC"]);
                            newPurchaseInvoice.BaseAmnt = Convert.ToDecimal(reader["BaseAmnt"]);
                            newPurchaseInvoice.BaseAmntFC = Convert.ToDecimal(reader["BaseAmntFC"]);
                            newPurchaseInvoice.BaseAmntSC = Convert.ToDecimal(reader["BaseAmntSC"]);

                            newListPurchaseInvoice.Add(newPurchaseInvoice);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(newListPurchaseInvoice,error);

            }
            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(newListPurchaseInvoice,e.Message);
            }
        }
    }
}
