using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using System.Windows;

namespace Negocio
{
    public class ControladorEntradaMercancia: ControladorDocumento
    {
        ModeloEntradaMercancia cn = new ModeloEntradaMercancia();
        public Tuple <int,string> SelectDocNum()
        {
            return cn.SelectDocNum();
        }

        public Tuple <List<DocumentoCabecera>,string> FindEntradaMercancia(List<DocumentoCabecera> listaPurchaseInvoice)
        {
            return cn.FindEntradaMercancia(listaPurchaseInvoice);
        }

        public Tuple<DataTable,string> FindEntradaMercanciaLines(int docEntry)
        {
            DataTable dtClone;

            var result = cn.FindEntradaMercanciaLines(docEntry);

            dtClone = ChangeTypeColumn(result.Item1);

            dtClone = TraduceSujetoRetencion(dtClone);

            return Tuple.Create(dtClone,result.Item2);
        }


        public Tuple<List<DocumentoCabecera>, string> FindLastEntradaMercancia()
        {
            return cn.FindLastEntradaMercancia();
        }



        public Tuple <DataTable,string> FindRetencionImpuestoOPDN(int docEntry)
        {
            DataTable dtClone;

            var result = cn.FindRetencionImpuestoOPDN(docEntry);

            dtClone = ChangeTypeColumnRetenciones(result.Item1);

            return Tuple.Create(dtClone,result.Item2);

        }


        public Tuple<List<DocumentoCabecera>, string> FindNextEntradaMercancia(string docNum)
        {
            return cn.FindNextEntradaMercancia(docNum);
        }

        public Tuple <int,string> InsertEntradaMercancia(List<DocumentoCabecera> listaPurchaseInvoice)
        {
            return cn.InsertEntradaMercancia(listaPurchaseInvoice);
        }

        public Tuple<int, string> InsertEntradaMercanciaLines(List<DocumentoDetalle> listPurchaseInvoiceLines)
        {
            return cn.InsertEntradaMercanciaLines(listPurchaseInvoiceLines);
        }


        public Tuple<int, string> FindDocEntry(int docNum)
        {
            return cn.FindDocEntry(docNum);
        }

        public Tuple<int, string> InsertTaxHolding(List<TablaRetencionImpuesto> listTablaRetenciones)
        {
            return cn.InsertTaxHolding(listTablaRetenciones);
        }


        public Tuple<List<DocumentoCabecera>, string> FindPreviousEntradaMercancia(string docNum)
        {
            return cn.FindPreviousEntradaMercancia(docNum);
        }

        public Tuple<List<DocumentoCabecera>, string> FindFirstEntradaMercancia()
        {
            return cn.FindFirstEntradaMercancia();
        }


        public Tuple<string, int> UpdateEntradaMercancia(List<DocumentoCabecera> listPurchaseInvoice)
        {
            return cn.UpdateEntradaMercancia(listPurchaseInvoice);
        }

        public Tuple<int,string> DeleteEntradaMercanciaLines(int docEntryDeleted)
        {
            return cn.DeleteEntradaMercanciaLines(docEntryDeleted);
        }

        public Tuple<int, string> DeleteEntradaMercancia(int docEntryDeleted)
        {
            return cn.DeleteEntradaMercancia(docEntryDeleted);
        }

        public Tuple<int, string> InsertEntradaMercanciaPreliminar(List<DocumentoCabecera> listPurchaseInvoice)
        {
            return cn.InsertEntradaMercanciaPreliminar(listPurchaseInvoice);
        }

        public Tuple<int, string> InsertEntradaMercanciaLinesPreliminar(List<DocumentoDetalle> listPurchaseInvoiceLines)
        {
            return cn.InsertEntradaMercanciaLinesPreliminar(listPurchaseInvoiceLines);
        }

        public Tuple<int, string> FindDocEntryPreliminar(int docNum)
        {
            return cn.FindDocEntryPreliminar(docNum);
        }

        
    }
}
