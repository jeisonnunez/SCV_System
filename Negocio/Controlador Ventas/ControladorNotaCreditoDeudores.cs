using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Datos;
using Entidades;


namespace Negocio
{
    public class ControladorNotaCreditoDeudores: ControladorDocumento
    {
        ModeloNotaCreditoDeudores cn = new ModeloNotaCreditoDeudores();

        public Tuple<int,string> SelectDocNum()
        {
            return cn.SelectDocNum();
        }

        public Tuple<List<DocumentoCabecera>,string> FindSalesCreditNote(List<DocumentoCabecera> listaPurchaseInvoice)
        {
            return cn.FindSalesCreditNote(listaPurchaseInvoice);
        }


        public Tuple<DataTable,string> FindSalesCreditNoteLines(int docEntry)
        {
            DataTable dtClone;

            var result = cn.FindSalesCreditNoteLines(docEntry);

            dtClone = ChangeTypeColumn(result.Item1);

            dtClone = TraduceSujetoRetencion(dtClone);

            return Tuple.Create(dtClone, result.Item2);
        }


        public Tuple<List<DocumentoCabecera>,string> FindLastSalesCreditNote()
        {
            return cn.FindLastSalesCreditNote();
        }



        public Tuple<DataTable,string> FindRetencionImpuestoCreditNote(int docEntry)
        {
            DataTable dtClone;

            var result = cn.FindRetencionImpuesto(docEntry);

            dtClone = ChangeTypeColumnRetenciones(result.Item1);

            return Tuple.Create(dtClone, result.Item2);

        }

        public Tuple<List<DocumentoCabecera>,string> FindNextSalesCreditNote(string docNum)
        {
            return cn.FindNextSalesCreditNote(docNum);
        }

        public Tuple<int,string> InsertSalesCreditNote(List<DocumentoCabecera> listaPurchaseInvoice)
        {
            return cn.InsertSalesCreditNote(listaPurchaseInvoice);
        }

        public Tuple<int, string> InsertSalesCreditNoteLines(List<DocumentoDetalle> listPurchaseInvoiceLines)
        {
            return cn.InsertSalesCreditNoteLines(listPurchaseInvoiceLines);
        }


        public Tuple<int, string> FindDocEntry(int docNum)
        {
            return cn.FindDocEntry(docNum);
        }

        public Tuple<int, string> InsertTaxHoldingCreditNote(List<TablaRetencionImpuesto> listTablaRetenciones)
        {
            return cn.InsertTaxHoldingCreditNote(listTablaRetenciones);
        }


        public Tuple<List<DocumentoCabecera>,string> FindPreviousSalesCreditNote(string docNum)
        {
            return cn.FindPreviousSalesCreditNote(docNum);
        }

        public Tuple<List<DocumentoCabecera>,string> FindFirstSalesCreditNote()
        {
            return cn.FindFirstSalesCreditNote();
        }

        public Tuple<string, int> UpdateSalesCreditNote(List<DocumentoCabecera> listPurchaseInvoice)
        {
            return cn.UpdateSalesCreditNote(listPurchaseInvoice);
        }

        public Tuple<int,string> DeleteSalesCreditNoteLines(int docEntryDeleted)
        {
            return cn.DeleteSalesCreditNoteLines(docEntryDeleted);
        }

        public Tuple<int, string> DeleteSalesCreditNote(int docEntryDeleted)
        {
            return cn.DeleteSalesCreditNote(docEntryDeleted);
        }

        public Tuple<int, string> DeleteSalesCreditNoteRetenciones(int docEntryDeleted)
        {
            return cn.DeleteSalesCreditNoteRetenciones(docEntryDeleted);
        }

        public Tuple<int,string> InsertSalesCreditNotePreliminar(List<DocumentoCabecera> listPurchaseInvoice)
        {
            return cn.InsertSalesCreditNotePreliminar(listPurchaseInvoice);
        }

        public Tuple<int, string> FindDocEntryPreliminar(int docNum)
        {
            return cn.FindDocEntryPreliminar(docNum);
        }

        public Tuple<int, string> InsertSalesCreditNoteLinesPreliminar(List<DocumentoDetalle> item1)
        {
            return cn.InsertSalesCreditNoteLinesPreliminar(item1);
        }

        public Tuple<int, string> InsertTaxHoldingPreliminar(List<TablaRetencionImpuesto> listTablaRetenciones)
        {
            return cn.InsertTaxHoldingPreliminarSalesCreditNote(listTablaRetenciones);
        }
    }
}
