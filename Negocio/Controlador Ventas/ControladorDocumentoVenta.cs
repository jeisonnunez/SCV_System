using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocio.Controlador_Ventas
{
    public class ControladorDocumentoVenta:ControladorDocumento
    {
        ModeloDocumentoVenta cn = new ModeloDocumentoVenta();

        public Tuple<int, string> DeleteSalesInvoice(int docEntryDeleted)
        {
            return cn.DeleteSalesInvoice(docEntryDeleted);
        }

        public Tuple<int, string> DeleteSalesInvoiceLines(int docEntryDeleted)
        {
            return cn.DeleteSalesInvoiceLines(docEntryDeleted);
        }

        public Tuple<int, string> DeleteSalesInvoiceRetenciones(int docEntryDeleted)
        {
            return cn.DeleteSalesInvoiceRetenciones(docEntryDeleted);
        }

        public Tuple<string, int> UpdateSales(List<DocumentoCabecera> listPurchaseInvoice)
        {
            return cn.UpdateSales(listPurchaseInvoice);
        }

        public Tuple<int, string> InsertTaxHolding(List<TablaRetencionImpuesto> listTablaRetenciones)
        {
            return cn.InsertTaxHolding(listTablaRetenciones);
        }

        public Tuple<int, string> InsertTaxHoldingPreliminar(List<TablaRetencionImpuesto> listTablaRetenciones)
        {
            return cn.InsertTaxHoldingPreliminar(listTablaRetenciones);
        }

        public Tuple<DataTable, string> FindRetencionImpuesto(int docEntry)
        {
            DataTable dtClone;

            var result = cn.FindRetencionImpuesto(docEntry);

            dtClone = ChangeTypeColumnRetenciones(result.Item1);

            return Tuple.Create(dtClone, result.Item2);

        }

        public Tuple<DataTable, string> FindSalesInvoiceLines(int docEntry)
        {
            DataTable dtClone;

            var result = cn.FindSalesInvoiceLines(docEntry);

            dtClone = ChangeTypeColumn(result.Item1);

            dtClone = TraduceSujetoRetencion(dtClone);

            return Tuple.Create(dtClone, result.Item2);
        }


        public Tuple<int, string> InsertSalesInvoice(List<DocumentoCabecera> listaPurchaseInvoice)
        {
            return cn.InsertSalesInvoice(listaPurchaseInvoice);
        }

        public Tuple<int, string> InsertSalesInvoicePreliminar(List<DocumentoCabecera> listaPurchaseInvoice)
        {
            return cn.InsertSalesInvoicePreliminar(listaPurchaseInvoice);
        }

        public Tuple<int, string> InsertSalesInvoiceLines(List<DocumentoDetalle> listPurchaseInvoiceLines)
        {
            return cn.InsertSalesInvoiceLines(listPurchaseInvoiceLines);
        }

        public Tuple<int, string> InsertSalesInvoiceLinesPreliminar(List<DocumentoDetalle> listPurchaseInvoiceLines)
        {
            return cn.InsertSalesInvoiceLinesPreliminar(listPurchaseInvoiceLines);
        }


    }
}
