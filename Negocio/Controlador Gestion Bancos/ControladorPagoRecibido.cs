using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Datos;
using System.Data;

namespace Negocio
{
    public class ControladorPagoRecibido: ControladorDocumento
    {
        ModeloPagoRecibido cn = new ModeloPagoRecibido();
        public Tuple<List<Payment>, string> FindLastPaymentReceived()
        {
            return cn.FindLastPaymentReceived();
        }

        public Tuple<int, string, string> FindPaymentReceivedLines(int docEntry)
        {
            return cn.FindPaymentReceivedLines(docEntry);
        }

        public Tuple<List<Payment>, string> FindFirstPaymentReceived()
        {
            return cn.FindFirstPaymentReceived();
        }

        public Tuple<List<Payment>, string> FindNextPaymentReceived(string docNum)
        {
            return cn.FindNextPaymentReceived(docNum);
        }

        public Tuple<List<Payment>, string> FindPreviousPaymentReceived(string docNum)
        {
            return cn.FindPreviousPaymentReceived(docNum);
        }

        public Tuple<DataTable, string> FindSalesInvoiceCustomer(string customer)
        {
            DataTable dtClone;

            var result = cn.FindSalesInvoiceCustomer(customer);

            dtClone = AddColumnPaymentReceived(result.Item1);

            dtClone = GetTransTypeDatatable(dtClone);

            return Tuple.Create(dtClone, result.Item2);
        }

        public Tuple<DataTable, string> FindSalesInvoiceCustomerSpecific(int docEntry, string transType)
        {
            DataTable dtClone;

            var result = cn.FindSalesInvoiceCustomerSpecific(docEntry, transType);

            dtClone = AddColumnPaymentMadeSpecific(result.Item1);

            dtClone = GetTransTypeDatatable(dtClone);

            return Tuple.Create(dtClone, result.Item2);
        }

        private DataTable AddColumnPaymentMadeSpecific(DataTable dataTable)
        {
            DataTable dtCloned = dataTable.Clone();

            int i = 0;

            foreach (DataColumn column in dataTable.Columns)
            {
                dtCloned.Columns[i].DataType = typeof(string);

                i++;
            }

            dtCloned.Columns.Add("Seleccionado", typeof(bool));
            dtCloned.Columns.Add("PaidWithHoldingTax", typeof(string));
            dtCloned.Columns.Add("PaidSysWithHoldingTax", typeof(string));
            dtCloned.Columns.Add("PaidFCWithHoldingTax", typeof(string));
            dtCloned.Columns.Add("PaidWithOutHoldingTax", typeof(string));
            dtCloned.Columns.Add("PaidSysWithOutHoldingTax", typeof(string));
            dtCloned.Columns.Add("PaidFCWithOutHoldingTax", typeof(string));


            foreach (DataRow row in dataTable.Rows)
            {
                dtCloned.ImportRow(row);
            }

            return dtCloned;
        }

        private DataTable GetTransTypeDatatable(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() == "TransType" && row[column].ToString() == ((int)TransType.RF).ToString())
                    {
                        row[column] = TransType.RF.ToString();

                    }

                    if (column.ToString() == "TransType" && row[column].ToString() == ((int)TransType.AS).ToString())
                    {
                        row[column] = TransType.AS.ToString();

                    }

                }

            }

            return dt;

        }

        public DataTable AddColumnPaymentReceived(DataTable dataTable)
        {
            DataTable dtCloned = dataTable.Clone();

            int i = 0;

            foreach (DataColumn column in dataTable.Columns)
            {
                dtCloned.Columns[i].DataType = typeof(string);

                i++;
            }

            dtCloned.Columns.Add("Seleccionado", typeof(bool));
            //dtCloned.Columns.Add("WTImporteRetencion", typeof(string));
            dtCloned.Columns.Add("U_IDA_CompIVA", typeof(string));
            dtCloned.Columns.Add("U_IDA_FechaComp", typeof(DateTime));
            dtCloned.Columns.Add("U_IDA_MontoCompIVA", typeof(string));
            dtCloned.Columns.Add("U_IDA_CompISLR", typeof(string));
            dtCloned.Columns.Add("U_IDA_DateCompISLR", typeof(DateTime));
            dtCloned.Columns.Add("PaidWithHoldingTax", typeof(string));
            dtCloned.Columns.Add("PaidSysWithHoldingTax", typeof(string));
            dtCloned.Columns.Add("PaidFCWithHoldingTax", typeof(string));
            dtCloned.Columns.Add("PaidWithOutHoldingTax", typeof(string));
            dtCloned.Columns.Add("PaidSysWithOutHoldingTax", typeof(string));
            dtCloned.Columns.Add("PaidFCWithOutHoldingTax", typeof(string));

            foreach (DataRow row in dataTable.Rows)
            {
                dtCloned.ImportRow(row);
            }

            return dtCloned;
        }

        public DataTable AddColumnPaymentReceivedSpecific(DataTable dataTable)
        {
            DataTable dtCloned = dataTable.Clone();

            int i = 0;

            foreach (DataColumn column in dataTable.Columns)
            {
                dtCloned.Columns[i].DataType = typeof(string);

                i++;
            }

            dtCloned.Columns.Add("Seleccionado", typeof(bool));
            dtCloned.Columns.Add("WTImporteRetencion", typeof(string));

            foreach (DataRow row in dataTable.Rows)
            {
                dtCloned.ImportRow(row);
            }

            return dtCloned;
        }

        public Tuple<int, string> SelectDocNum()
        {
            return cn.SelectDocNum();
        }

        public Tuple<List<Payment>, string> FindPaymentReceived(List<Payment> listPayment)
        {
            return cn.FindPaymentReceived(listPayment);
        }

        public Tuple<string, string> GetDocumentAccount(int docNum, string objType)
        {
            return cn.GetDocumentAccount(docNum,objType);
        }

        public Tuple<int, string> InsertPaymentReceived(List<Payment> listPayment)
        {
            return cn.InsertPaymentReceived(listPayment);
        }

        public Tuple<List<DocumentoCabecera>, string> FindSalesInvoiceSpecific(int docNum)
        {
            return cn.FindSalesInvoiceSpecific(docNum);
        }

        public Tuple<int, string> InsertPaymentReceivedLines(List<PaymentDetails> listPaymentDetails)
        {
            return cn.InsertPaymentReceivedLines(listPaymentDetails);
        }

        public Tuple<int, string> DeletePaymentReceived(int docNum)
        {
            return cn.DeletePaymentReceived(docNum);
        }

        public Tuple<int, string> UpdatePaymentReceivedSales(PaymentDetails paymentDetails)
        {
            return cn.UpdatePaymentReceivedSales(paymentDetails);
        }

        public bool? GetPayNoDoc(char payNoDoc)
        {
            bool? result = null;

            if (payNoDoc == (char)PayNoDoc.NO)
            {
                result = false;

            }
            else if (payNoDoc == (char)PayNoDoc.SI)
            {
                result = true;
            }

            return result;
        }

        enum PayNoDoc { SI = 'Y', NO = 'N' };

        public Tuple<int, string> UpdatePaymentReceived(List<Payment> listaPayment)
        {
            return cn.UpdatePaymentReceived(listaPayment);
        }

        public string CreateDataSetPreliminarPaymentReceived()
        {
            return cn.CreateDataSetPreliminarPaymentReceived();
        }

        public Tuple<int, string> InsertPaymentReceivedPreliminar(List<Payment> payments)
        {
            return cn.InsertPaymentReceivedPreliminar(payments);
        }

        public Tuple<List<DocumentoCabecera>, string> FindSalesInvoiceSpecificPreliminar(int docNum)
        {
            return cn.FindSalesInvoiceSpecificPreliminar(docNum);
        }

        public Tuple<int,string> InsertPaymentReceivedLinesPreliminar(List<PaymentDetails> listPayment)
        {
            return cn.InsertPaymentReceivedLinesPreliminar(listPayment);
        }

        public Tuple<string, string> GetDocumentAccountPreliminar(int docNum, bool v)
        {
            return cn.GetDocumentAccountPreliminar(docNum,v);
        }

        public Tuple<int, string> FindDocEntryPreliminar(int docNumPayment)
        {
            return cn.FindDocEntryPreliminar(docNumPayment);
        }

        public Tuple<int,string> FindDocEntry(int docNumPayment)
        {
            return cn.FindDocEntry(docNumPayment);
        }

        public Tuple<DataTable,string> GetRetencionImpuesto(int docEntry, string transType)
        {
            return cn.GetRetencionImpuesto(docEntry, transType);
        }

       
    }
}

