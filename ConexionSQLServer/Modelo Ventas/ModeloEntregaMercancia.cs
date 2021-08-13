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
    public class ModeloEntregaMercancia: ModeloDocumentoVenta
    {
        public Tuple<int, string> SelectDocNum()
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
                        cmd.CommandText = "SelectDocNumODLN";

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

                return Tuple.Create(DocNum, e.Message);

            }
        }

        public Tuple<int, string> InsertEntregaMercanciaLinesPreliminar(List<DocumentoDetalle> listaEntregaMercanciaLines)
        {
            //LoadDatatable INV1_TEST

            dtDLN1 = dataSet.Tables["DLN1_TEST"];

            int flag = 0;

            string error = null;

            int DocEntry = 0;

            try
            {
                foreach (DocumentoDetalle SalesInvoice in listaEntregaMercanciaLines)
                {
                    DocEntry = SalesInvoice.DocEntry;

                    DataRow newRow = dtDLN1.NewRow();

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

                    dtDLN1.Rows.Add(newRow);

                    dtDLN1.AcceptChanges();
                }

                DataRow[] selected = dtDLN1.Select("DocEntry = " + DocEntry);

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

                DataRow dataRow = dtODLN.AsEnumerable().Where(r => ((int)r["DocNum"]).Equals(docNum)).First();

                DocEntry = Convert.ToInt32(dataRow["DocEntry"]);                

                return Tuple.Create(DocEntry, error);

            }
            catch (Exception e)
            {
                return Tuple.Create(DocEntry, e.Message);

            }
        }

        public Tuple<int, string> InsertEntregaMercanciaPreliminar(List<DocumentoCabecera> listaEntregaMercancia)
        {
            
           //LoadDatatable OINV_TEST

                dtODLN = dataSet.Tables["ODLN_TEST"];

                int flag = 0;

                string error = null;

                try
                {
                    foreach (DocumentoCabecera SalesInvoice in listaEntregaMercancia)
                    {
                        int DocEntry = dtODLN.Rows.Count;

                        DataRow newRow = dtODLN.NewRow();

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
                        newRow["NumAtCard"] = SalesInvoice.NumAtCard;                       
                        newRow["Max1099"] = SalesInvoice.Max1099;

                        dtODLN.Rows.Add(newRow);

                    dtODLN.AcceptChanges();

                    if (dtODLN.Rows.Count == DocEntry + 1)
                    {
                        flag = 1;
                    }
                    else
                    {
                        flag = 0;
                    }

                   

                  
                    }

                    return Tuple.Create(flag, error);

                }
                catch (Exception e)
                {

                    return Tuple.Create(flag, e.Message);
                }


            
        }

        public Tuple<DataTable, string> FindEntregaMercanciaLines(int docEntry)
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
                        cmd.CommandText = "FindEntregaMercanciaLines";

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

                return Tuple.Create(tabla, e.Message);
            }
        }

        public Tuple<List<DocumentoCabecera>, string> FindEntregaMercancia(List<DocumentoCabecera> listaPurchaseInvoice)
        {
            List<DocumentoCabecera> newListSalesInvoice = new List<DocumentoCabecera>();

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
                            cmd.CommandText = "FindEntregaMercancia";

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

                }

                Connection.Close();

                return Tuple.Create(newListSalesInvoice,error);

            }
            catch (Exception e)
            {
                
                Connection.Close();

                return Tuple.Create(newListSalesInvoice, e.Message);
            }
        }

        public Tuple<int, string> DeleteEntregaMercancia(int docEntryDeleted)
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
                        cmd.CommandText = "DeleteEntregaMercancia";

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

        public Tuple<int, string> DeleteEntregaMercanciaLines(int docEntryDeleted)
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
                        cmd.CommandText = "DeleteEntregaMercanciaLines";

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

        public Tuple<string, string> GetAccountSaleCostAc(string itemCode)
        {
            throw new NotImplementedException();
        }

        public Tuple<int, string> InsertEntregaMercancia(List<DocumentoCabecera> listaPurchaseInvoice)
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
                            cmd.CommandText = "InsertEntregaMercancia";

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
                            cmd.Parameters.AddWithValue("@NumAtCard", PurchaseInvoice.NumAtCard);                           
                            cmd.Parameters.AddWithValue("@Max1099", PurchaseInvoice.Max1099);

                            flag = cmd.ExecuteNonQuery();

                        }
                    }

                }

                Connection.Close();

                return Tuple.Create(flag,error);

            }
            catch (Exception e)
            {
                MessageBox.Show("Error General: " + e.Message, "Journal Entry", MessageBoxButton.OK, MessageBoxImage.Error);

                Connection.Close();

                return Tuple.Create(flag, e.Message);
            }
        }

        public Tuple<List<DocumentoCabecera>, string> FindLastEntregaMercancia()
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
                        cmd.CommandText = "LastEntregaMercancia";

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

                return Tuple.Create(newListSalesInvoice, e.Message);
            }
        }



        public Tuple<string, int> UpdateEntregaMercancia(List<DocumentoCabecera> listPurchaseInvoice)
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
                            cmd.CommandText = "UpdateEntregaMercancia";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@DocEntry", Purchase.DocEntry);
                            cmd.Parameters.AddWithValue("@DocNum", Purchase.DocNum);
                            cmd.Parameters.AddWithValue("@TaxDate", Purchase.TaxDate);
                            cmd.Parameters.AddWithValue("@DocDueDate", Purchase.DocDueDate);
                            cmd.Parameters.AddWithValue("@NumAtCard", Purchase.NumAtCard);                           
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

        public Tuple<DataTable, string> FindRetencionImpuestoODLN(int docEntry)
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
                        cmd.CommandText = "FindRetencionImpuestoODLN";

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

                return Tuple.Create(tabla, e.Message);
            }
        }

        public Tuple<List<DocumentoCabecera>, string> FindNextEntregaMercancia(string docNum)
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
                        cmd.CommandText = "NextEntregaMercancia";

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

                return Tuple.Create(newListSalesInvoice, e.Message);
            }
        }





        public Tuple<List<DocumentoCabecera>, string> FindFirstEntregaMercancia()
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
                        cmd.CommandText = "FirstEntradaMercancia";

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

                return Tuple.Create(newListSalesInvoice, e.Message);
            }
        }

        public Tuple<List<DocumentoCabecera>, string> FindPreviousEntregaMercancia(string docNum)
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
                        cmd.CommandText = "PreviousEntregaMercancia";

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

                return Tuple.Create(newListSalesInvoice, e.Message);
            }
        }



        public Tuple<int, string> InsertEntregaMercanciaLines(List<DocumentoDetalle> listPurchaseInvoiceLines)
        {
            int flag = 0;

            int i = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (DocumentoDetalle SalesInvoiceLines in listPurchaseInvoiceLines)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "InsertEntregaMercanciaLines";

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

                return Tuple.Create(i, e.Message);
            }
        }



        public Tuple<int, string> InsertTaxHolding(List<TablaRetencionImpuesto> listTablaRetenciones)
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
                            cmd.CommandText = "InsertTaxHoldingODLN";

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


        public Tuple<int, string> FindDocEntry(int docNum)
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
                        cmd.CommandText = "FindDocEntryODLN";

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

                return Tuple.Create(DocEntry, e.Message);

            }
        }
    }
}
