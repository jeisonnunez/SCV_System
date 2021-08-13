using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocio
{
    public class ControladorPagoEfectuado : ControladorDocumento
    {
        ModeloPagoEfectuado cn = new ModeloPagoEfectuado();
        public Tuple<List<Payment>,string> FindLastPaymentMade()
        {
           return cn.FindLastPaymentMade();
        }

        public Tuple <int,string,string> FindPaymentMadeLines(int docEntry)
        {
           return cn.FindPaymentMadeLines(docEntry);
        }

        public Tuple<List<Payment>, string> FindFirstPaymentMade()
        {
            return cn.FindFirstPaymentMade();
        }

        public Tuple<List<Payment>, string> FindNextPaymentMade(string docNum)
        {
            return cn.FindNextPaymentMade(docNum);
        }

        public Tuple<List<Payment>, string> FindPreviousPaymentMade(string docNum)
        {
            return cn.FindPreviousPaymentMade(docNum);
        }

        public Tuple<DataTable,string> FindPurchaseInvoiceSupplier(string supplier)
        {
            DataTable dtClone;             

            var result = cn.FindPurchaseInvoiceSupplier(supplier);

            dtClone = AddColumnPaymentMade(result.Item1);

            dtClone = GetTransTypeDatatable(dtClone);

            return Tuple.Create(dtClone, result.Item2);
        }

        public Tuple<DataTable, string> FindPurchaseInvoiceSupplierSpecific(int docEntry, string transType)
        {
            DataTable dtClone;

            var result = cn.FindPurchaseInvoiceSupplierSpecific(docEntry, transType);

            dtClone = AddColumnPaymentMadeSpecific(result.Item1);

            dtClone = GetTransTypeDatatable(dtClone);

            return Tuple.Create(dtClone, result.Item2);
        }

        
        private DataTable GetTransTypeDatatable(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() == "TransType" && row[column].ToString() == ((int)TransType.TT).ToString())
                    {
                        row[column] = TransType.TT.ToString();

                    }

                    if (column.ToString() == "TransType" && row[column].ToString() == ((int)TransType.AS).ToString())
                    {
                        row[column] = TransType.AS.ToString();

                    }



                }

            }

            return dt;
            
        }

        public DataTable AddColumnPaymentMade(DataTable dataTable)
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
            dtCloned.Columns.Add("U_IDA_FechaComp",typeof(DateTime));
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

        public DataTable AddColumnPaymentMadeSpecific(DataTable dataTable)
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

        public Tuple <int,string> SelectDocNum()
        {
            return cn.SelectDocNum();
        }

        public Tuple<List<Payment>, string> FindPayment(List<Payment> listPayment)
        {
            return cn.FindPayment(listPayment);
        }

        public Tuple<string,string> GetDocumentAccount(int docEntry, string transType)
        {
            return cn.GetDocumentAccount(docEntry,transType);
        }

        public Tuple<int,string> InsertPaymentMade(List<Payment> listPayment)
        {
            return cn.InsertPaymentMade(listPayment);
        }

        public Tuple<List<DocumentoCabecera>,string> FindPurchaseInvoiceSpecific(DataTable dtDocument)
        {
            return cn.FindPurchaseInvoiceSpecific(dtDocument);
        }

        public Tuple<int,string> InsertPaymentMadeLines(List<PaymentDetails> listPaymentDetails)
        {
            return cn.InsertPaymentMadeLines(listPaymentDetails);
        }

        public Tuple<int, string> DeletePaymentMade(int docNum)
        {
            return cn.DeletePaymentMade(docNum);
        }

        public Tuple<int, string> UpdatePaymentMadePurchase(PaymentDetails paymentDetails)
        {
            return cn.UpdatePaymentMadePurchase(paymentDetails);
        }

        public bool? GetPayNoDoc(char payNoDoc)
        {
            bool? result = null;

            if (payNoDoc == (char)PayNoDoc.NO)
            {
                result = false;

            }else if(payNoDoc == (char)PayNoDoc.SI)
            {
                result = true;
            }

            return result;
        }

        enum PayNoDoc { SI = 'Y', NO = 'N' };

        public Tuple<int,string> UpdatePaymentMade(List<Payment> listaPayment)
        {
            return cn.UpdatePaymentMade(listaPayment);
        }

        public Tuple<int, string> InsertPaymentMadePreliminar(List<Payment> payments)
        {
            return cn.InsertPaymentMadePreliminar(payments);
        }

        public Tuple<List<DocumentoCabecera>, string> FindPurchaseInvoiceSpecificPreliminar(int docNum)
        {
            return cn.FindPurchaseInvoiceSpecificPreliminar(docNum);
        }

        public Tuple<int, string> InsertPaymentMadeLinesPreliminar(List<PaymentDetails> payments)
        {
            return cn.InsertPaymentMadeLinesPreliminar(payments);
        }

        public string CreateDataSetPreliminarPaymentMade()
        {
            return cn.CreateDataSetPreliminarPaymentMade();
        }

        public Tuple<string, string> GetDocumentAccountPreliminar(int docNum, bool v)
        {
            return cn.GetDocumentAccountPreliminar(docNum,v);
        }

        public Tuple<int, string> FindDocEntryPreliminar(object docNumPayment)
        {
            return cn.FindDocEntryPreliminar(docNumPayment);
        }

        public Tuple<int,string> FindDocEntry(int docNumPayment)
        {
            return cn.FindDocEntry(docNumPayment);
        }

        public Tuple<DataTable,string> GetRetencionImpuesto(int DocEntry, string ObjType)
        {
            return cn.GetRetencionImpuesto(DocEntry, ObjType);
        }

        
        
    }
}
