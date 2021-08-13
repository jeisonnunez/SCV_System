using Datos;
using Datos.Modelo_Compras;
using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Controlador_Compras
{
    public class ControladorDocumentoCompra:ControladorDocumento
    {
        ModeloDocumentoCompra cn = new ModeloDocumentoCompra();
        public Tuple<int, string> DeletePurchaseInvoice(int docEntryDeleted)
        {
            return cn.DeletePurchaseInvoice(docEntryDeleted);
        }


        public Tuple<int, string> InsertPurchaseInvoicePreliminar(List<DocumentoCabecera> listPurchaseInvoiceLines)
        {
            return cn.InsertPurchaseInvoicePreliminar(listPurchaseInvoiceLines);
        }

        public Tuple<int, string> InsertPurchaseInvoiceLinesPreliminar(List<DocumentoDetalle> listPurchaseInvoiceLines)
        {
            return cn.InsertPurchaseInvoiceLinesPreliminar(listPurchaseInvoiceLines);
        }

        public Tuple<int, string> InsertTaxHoldingPreliminar(List<TablaRetencionImpuesto> listTablaRetenciones)
        {
            return cn.InsertTaxHoldingPreliminarPurchaseInvoice(listTablaRetenciones);
        }

        public Tuple<int, string> DeletePurchaseInvoiceLines(int docEntryDeleted)
        {
            return cn.DeletePurchaseInvoiceLines(docEntryDeleted);
        }

        public Tuple<int, string> DeletePurchaseInvoiceRetenciones(int docEntryDeleted)
        {
            return cn.DeletePurchaseInvoiceRetenciones(docEntryDeleted);
        }

        public Tuple<string, int> UpdatePurchase(List<DocumentoCabecera> listPurchaseInvoice)
        {
            return cn.UpdatePurchase(listPurchaseInvoice);
        }

        public Tuple<int, string> InsertTaxHolding(List<TablaRetencionImpuesto> listTablaRetenciones)
        {
            return cn.InsertTaxHolding(listTablaRetenciones);
        }

        public Tuple<DataTable, string> FindRetencionImpuesto(int docEntry)
        {
            DataTable dtClone;

            var result = cn.FindRetencionImpuesto(docEntry);

            dtClone = ChangeTypeColumnRetenciones(result.Item1);

            return Tuple.Create(dtClone, result.Item2);

        }

       

        public Tuple<int, string> InsertPurchaseInvoice(List<DocumentoCabecera> listaPurchaseInvoice)
        {
            return cn.InsertPurchaseInvoice(listaPurchaseInvoice);
        }

        public Tuple<int, string> InsertPurchaseInvoiceLines(List<DocumentoDetalle> listPurchaseInvoiceLines)
        {
            return cn.InsertPurchaseInvoiceLines(listPurchaseInvoiceLines);
        }


    }
}
