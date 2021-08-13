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
    public class ControladorNotaDebitoProveedores: Controlador_Compras.ControladorDocumentoCompra
    {
        ModeloNotaDebitoProveedor cn = new ModeloNotaDebitoProveedor();
        public Tuple<int,string> SelectDocNum()
        {
            return cn.SelectDocNum();
        }

        public Tuple<int, string> FindDocEntry(int docNum)
        {
            return cn.FindDocEntry(docNum);
        }

        public Tuple<List<DocumentoCabecera>,string> FindPurchaseDebitNote(List<DocumentoCabecera> listaPurchaseInvoice)
        {
            return cn.FindPurchaseDebitNote(listaPurchaseInvoice);
        }

        public Tuple<DataTable,string> FindPurchaseInvoiceLines(int docEntry)
        {
            DataTable dtClone;

            var result = cn.FindPurchaseInvoiceLines(docEntry);

            dtClone = ChangeTypeColumn(result.Item1);

            dtClone = TraduceSujetoRetencion(dtClone);

            return Tuple.Create(dtClone, result.Item2);
        }


        public Tuple<List<DocumentoCabecera>,string> FindLastPurchaseDebitNote()
        {
            return cn.FindLastPurchaseDebitNote();
        }

        public Tuple<List<DocumentoCabecera>,string> FindNextPurchaseDebitNote(string docNum)
        {
            return cn.FindNextPurchaseDebitNote(docNum);
        }

       
      
        public Tuple<List<DocumentoCabecera>, string> FindPreviousPurchaseDebitNote(string docNum)
        {
            return cn.FindPreviousPurchaseDebitNote(docNum);
        }

        public Tuple<List<DocumentoCabecera>, string> FindFirstPurchaseDebitNote()
        {
            return cn.FindFirstPurchaseDebitNote();
        }

        public Tuple<int,string> FindDocEntryPreliminar(int docNum)
        {
            return cn.FindDocEntryPreliminar(docNum);
        }
    }
}
