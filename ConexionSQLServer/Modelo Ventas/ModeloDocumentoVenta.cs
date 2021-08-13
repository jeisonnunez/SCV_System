using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ModeloDocumentoVenta:ModeloDocumento
    {
        
        public Tuple<int, string> DeleteSalesInvoiceRetenciones(int docEntryDeleted)
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
                        cmd.CommandText = "DeleteSalesInvoiceRetenciones";

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

        

        public Tuple<int, string> DeleteSalesInvoiceLines(int docEntryDeleted)
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
                        cmd.CommandText = "DeleteSalesInvoiceLines";

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

        

        public Tuple<int, string> DeleteSalesInvoice(int docEntryDeleted)
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
                        cmd.CommandText = "DeleteSalesInvoice";

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


        public Tuple<int, string> InsertSalesInvoice(List<DocumentoCabecera> listaSalesInvoice)
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
                            cmd.CommandText = "InsertSalesInvoice";

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

        public Tuple<string, int> UpdateSales(List<DocumentoCabecera> listSalesInvoice)
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
                            cmd.CommandText = "UpdateSales";

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
                            cmd.Parameters.AddWithValue("@DocSubType", Sales.DocSubType);

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
                        cmd.CommandText = "FindRetencionImpuestoSales";

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



        public Tuple<int, string> InsertSalesInvoiceLines(List<DocumentoDetalle> listSalesInvoiceLines)
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
                            cmd.CommandText = "InsertSalesInvoiceLines";

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
                            cmd.CommandText = "InsertTaxHoldingOINV";

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
