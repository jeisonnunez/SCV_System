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
    public class ControladorEntregaMercancia: ControladorDocumento
    {
        ModeloEntregaMercancia cn = new ModeloEntregaMercancia();
        public Tuple <int,string> SelectDocNum()
        {
            return cn.SelectDocNum();
        }

        public Tuple <List<DocumentoCabecera>,string> FindEntregaMercancia(List<DocumentoCabecera> listaPurchaseInvoice)
        {
            return cn.FindEntregaMercancia(listaPurchaseInvoice);
        }

        public Tuple<int, string> InsertEntregaMercanciaPreliminar(List<DocumentoCabecera> listaPurchaseInvoice)
        {
            return cn.InsertEntregaMercanciaPreliminar(listaPurchaseInvoice);
        }

        public Tuple<int,string> InsertEntregaMercanciaLinesPreliminar(List<DocumentoDetalle> listaEntregaMercanciaLines)
        {
            return cn.InsertEntregaMercanciaLinesPreliminar(listaEntregaMercanciaLines);
        }


        public Tuple <DataTable,string> FindEntregaMercanciaLines(int docEntry)
        {
            DataTable dtClone;

            var result = cn.FindEntregaMercanciaLines(docEntry);

            dtClone= ChangeTypeColumn(result.Item1);

            dtClone = TraduceSujetoRetencion(dtClone);

            return Tuple.Create(dtClone,result.Item2);
        }


        public Tuple<List<DocumentoCabecera>, string> FindLastEntregaMercancia()
        {
            return cn.FindLastEntregaMercancia();
        }

        public Tuple <DataTable,string> FindRetencionImpuestoOPDN(int docEntry)
        {
            DataTable dtClone;

            var result = cn.FindRetencionImpuestoODLN(docEntry);

            dtClone = ChangeTypeColumnRetenciones(result.Item1);

            return Tuple.Create(dtClone,result.Item2);

        }


        public Tuple<List<DocumentoCabecera>, string> FindNextEntregaMercancia(string docNum)
        {
            return cn.FindNextEntregaMercancia(docNum);
        }

        public Tuple<int, string> InsertEntregaMercancia(List<DocumentoCabecera> listaPurchaseInvoice)
        {
            return cn.InsertEntregaMercancia(listaPurchaseInvoice);
        }

        public Tuple<int, string> InsertEntregaMercanciaLines(List<DocumentoDetalle> listPurchaseInvoiceLines)
        {
            return cn.InsertEntregaMercanciaLines(listPurchaseInvoiceLines);
        }


        public Tuple<int, string> FindDocEntry(int docNum)
        {
            return cn.FindDocEntry(docNum);
        }

        public Tuple<int, string> InsertTaxHolding(List<TablaRetencionImpuesto> listTablaRetenciones)
        {
            return cn.InsertTaxHolding(listTablaRetenciones);
        }


        public Tuple<List<DocumentoCabecera>,string> FindPreviousEntregaMercancia(string docNum)
        {
            return cn.FindPreviousEntregaMercancia(docNum);
        }

        public Tuple<List<DocumentoCabecera>, string> FindFirstEntregaMercancia()
        {
            return cn.FindFirstEntregaMercancia();
        }


        public Tuple<string, int> UpdateEntregaMercancia(List<DocumentoCabecera> listPurchaseInvoice)
        {
            return cn.UpdateEntregaMercancia(listPurchaseInvoice);
        }

        public Tuple<int,string> DeleteEntregaMercanciaLines(int docEntryDeleted)
        {
            return cn.DeleteEntregaMercanciaLines(docEntryDeleted);
        }

        public Tuple<int, string> DeleteEntregaMercancia(int docEntryDeleted)
        {
            return cn.DeleteEntregaMercancia(docEntryDeleted);
        }

        public Tuple<int,string> FindDocEntryPreliminar(int docNum)
        {
            return cn.FindDocEntryPreliminar(docNum);
        }
    }
}
