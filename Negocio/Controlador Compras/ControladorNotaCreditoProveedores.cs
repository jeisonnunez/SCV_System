using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using System.Windows;
using System.Net.Http.Headers;

namespace Negocio
{
    public class ControladorNotaCreditoProveedores :ControladorDocumento
    {
        ModeloNotaCreditoProveedores cn = new ModeloNotaCreditoProveedores();
        public Tuple<int,string> SelectDocNum()
        {
            return cn.SelectDocNum();
        }

        public Tuple<List<DocumentoCabecera>,string> FindPurchaseCreditNote(List<DocumentoCabecera> listaPurchaseInvoice)
        {
            return cn.FindPurchaseCreditNote(listaPurchaseInvoice);
        }



        public Tuple<DataTable,string> FindPurchaseCreditNoteLines(int docEntry)
        {
            DataTable dtClone;

            var result = cn.FindPurchaseCreditNoteLines(docEntry);

            dtClone = ChangeTypeColumn(result.Item1);

            dtClone = TraduceSujetoRetencion(dtClone);

            return Tuple.Create(dtClone, result.Item2);
        }


        public Tuple<List<DocumentoCabecera>,string> FindLastPurchaseCreditNote()
        {
            return cn.FindLastPurchaseCreditNote();
        }



        public Tuple<DataTable,string> FindRetencionImpuesto(int docEntry)
        {
            DataTable dtClone;

            var result = cn.FindRetencionImpuesto(docEntry);

            dtClone = ChangeTypeColumnRetenciones(result.Item1);

            return Tuple.Create(dtClone, result.Item2);

        }


        public Tuple<List<DocumentoCabecera>,string> FindNextPurchaseCreditNote(string docNum)
        {
            return cn.FindNextPurchaseCreditNote(docNum);
        }

        public Tuple<int,string> InsertPurchaseCreditNote(List<DocumentoCabecera> listaPurchaseInvoice)
        {
            return cn.InsertPurchaseCreditNote(listaPurchaseInvoice);
        }

        public Tuple<int,string> InsertPurchaseCreditNoteLines(List<DocumentoDetalle> listPurchaseInvoiceLines)
        {
            return cn.InsertPurchaseCreditNoteLines(listPurchaseInvoiceLines);
        }


        public Tuple<int,string> FindDocEntry(int docNum)
        {
            return cn.FindDocEntry(docNum);
        }

        public Tuple<int,string> InsertTaxHolding(List<TablaRetencionImpuesto> listTablaRetenciones)
        {
            return cn.InsertTaxHolding(listTablaRetenciones);
        }



        public Tuple<List<DocumentoCabecera>,string> FindPreviousPurchaseCreditNote(string docNum)
        {
            return cn.FindPreviousPurchaseCreditNote(docNum);
        }

        public Tuple<List<DocumentoCabecera>,string> FindFirstPurchaseCreditNote()
        {
            return cn.FindFirstPurchaseCreditNote();
        }


        public Tuple<string, int> UpdatePurchaseCreditNote(List<DocumentoCabecera> listPurchaseInvoice)
        {
            return cn.UpdatePurchaseCreditNote(listPurchaseInvoice);
        }

        public Tuple<int, string> DeletePurchaseCreditNote(int docEntryDeleted)
        {
            return cn.DeletePurchaseCreditNote(docEntryDeleted);
        }

        public Tuple<int, string> DeletePurchaseCreditNoteRetenciones(int docEntryDeleted)
        {
            return cn.DeletePurchaseCreditNoteRetenciones(docEntryDeleted);
        }

        public Tuple<int,string> DeletePurchaseCreditNoteLines(int docEntryDeleted)
        {
            return cn.DeletePurchaseCreditNoteLines(docEntryDeleted);
        }

        public Tuple<int, string> InsertSalesInvoicePreliminar(List<DocumentoCabecera> listSalesInvoice)
        {
            return cn.InsertSalesCreditNotePreliminar(listSalesInvoice);
        }

        public Tuple<int, string> FindDocEntryPreliminar(int docNum)
        {
            return cn.FindDocEntryPreliminar(docNum);
        }

        public Tuple<int, string> InsertSalesInvoiceLinesPreliminar(List<DocumentoDetalle> listPurchaseInvoiceLines)
        {
            return cn.InsertPurchaseCreditNoteLinesPreliminar(listPurchaseInvoiceLines);
        }

        public Tuple<int, string> InsertTaxHoldingPreliminar(List<TablaRetencionImpuesto> listTablaRetenciones)
        {
            return cn.InsertTaxHoldingPurchaseCreditNotePreliminar(listTablaRetenciones);
        }
    }
}
