using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Modelo_Compras
{
    public class ModeloDocumentoCompra:ModeloDocumento
    {
       
        public Tuple<int, string> DeletePurchaseInvoiceRetenciones(int docEntryDeleted)
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
                        cmd.CommandText = "DeletePurchaseInvoiceRetenciones";

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

        public Tuple<int, string> InsertTaxHoldingPreliminarPurchaseInvoice(List<TablaRetencionImpuesto> listTablaRetenciones)
        {
            //LoadDatatable INV5_TEST

            dtPCH5 = dataSet.Tables["PCH5_TEST"];

            int flag = 0;

            string error = null;

            int DocEntry = 0;

            try
            {
                foreach (TablaRetencionImpuesto TablaRetenciones in listTablaRetenciones)
                {
                    DocEntry = TablaRetenciones.AbsEntry;

                    DataRow newRow = dtPCH5.NewRow();

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

                    dtPCH5.Rows.Add(newRow);

                    dtPCH5.AcceptChanges();
                }

                DataRow[] selected = dtPCH5.Select("AbsEntry = " + DocEntry);

                flag = selected.Count();

                return Tuple.Create(flag, error);

            }
            catch (Exception e)
            {
                return Tuple.Create(flag, e.Message);
            }
        }

        public Tuple<int, string> InsertPurchaseInvoiceLinesPreliminar(List<DocumentoDetalle> listPurchaseInvoiceLines)
        {
            //LoadDatatable PCH1_TEST

            dtPCH1 = dataSet.Tables["PCH1_TEST"];

            int flag = 0;

            string error = null;

            int DocEntry = 0;

            try
            {
                foreach (DocumentoDetalle PurchaseInvoice in listPurchaseInvoiceLines)
                {
                    DocEntry = PurchaseInvoice.DocEntry;

                    DataRow newRow = dtPCH1.NewRow();

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

                    dtPCH1.Rows.Add(newRow);

                    dtPCH1.AcceptChanges();
                }

                DataRow[] selected = dtPCH1.Select("DocEntry = " + DocEntry);

                flag = selected.Count();

                return Tuple.Create(flag, error);

            }
            catch (Exception e)
            {
                return Tuple.Create(flag, e.Message);
            }

        }

        public Tuple<int, string> InsertPurchaseInvoicePreliminar(List<DocumentoCabecera> listPurchaseInvoiceLines)
        {
            //LoadDatatable OPCH_TEST

            dtOPCH = dataSet.Tables["OPCH_TEST"];

            int flag = 0;

            string error = null;

            try
            {
                foreach (DocumentoCabecera PurchaseInvoice in listPurchaseInvoiceLines)
                {
                    int DocEntry = dtOPCH.Rows.Count;

                    DataRow newRow = dtOPCH.NewRow();

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

                    dtOPCH.Rows.Add(newRow);

                    if (dtOPCH.Rows.Contains(DocEntry + 1) == true)
                    {
                        flag = 1;
                    }
                    else
                    {
                        flag = 0;
                    }

                    dtOPCH.AcceptChanges();
                }

                return Tuple.Create(flag, error);

            }
            catch (Exception e)
            {

                return Tuple.Create(flag, e.Message);
            }

        }

        public Tuple<int, string> DeletePurchaseInvoiceLines(int docEntryDeleted)
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
                        cmd.CommandText = "DeletePurchaseInvoiceLines";

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

        public Tuple<int, string> DeletePurchaseInvoice(int docEntryDeleted)
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
                        cmd.CommandText = "DeletePurchaseInvoice";

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


        public Tuple<int, string> InsertPurchaseInvoice(List<DocumentoCabecera> listaPurchaseInvoice)
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
                            cmd.CommandText = "InsertPurchaseInvoice";

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

        public Tuple<string, int> UpdatePurchase(List<DocumentoCabecera> listPurchaseInvoice)
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
                            cmd.CommandText = "UpdatePurchase";

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
                            cmd.Parameters.AddWithValue("@DocSubType", Purchase.DocSubType);

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

        public Tuple<DataTable, string> FindRetencionImpuesto(int docEntry)
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
                        cmd.CommandText = "FindRetencionImpuesto";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@DocEntry", docEntry);

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



        public Tuple<int, string> InsertPurchaseInvoiceLines(List<DocumentoDetalle> listPurchaseInvoiceLines)
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
                            cmd.CommandText = "InsertPurchaseInvoiceLines";

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

                return Tuple.Create(i, error);

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
                            cmd.CommandText = "InsertTaxHoldingOPCH";

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
    }
}
