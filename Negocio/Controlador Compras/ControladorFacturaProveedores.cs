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
    public class ControladorFacturaProveedores:Controlador_Compras.ControladorDocumentoCompra
    {
        ModeloFacturaProveedores cn = new ModeloFacturaProveedores();

        public Tuple<int,string> SelectDocNum()
        {
            return cn.SelectDocNum();
        }

        public Tuple<List<DocumentoCabecera>,string> FindPurchaseInvoice(List<DocumentoCabecera> listaPurchaseInvoice)
        {
            return cn.FindPurchaseInvoice(listaPurchaseInvoice);
        }

        public Tuple<DataTable, string> FindPurchaseInvoiceLines(int docEntry)
        {
            DataTable dtClone;

            var result = cn.FindPurchaseInvoiceLines(docEntry);

            dtClone = ChangeTypeColumn(result.Item1);

            dtClone = TraduceSujetoRetencion(dtClone);

            return Tuple.Create(dtClone,result.Item2);
        }

        public Tuple<int, string> FindDocEntry(int docNum)
        {
            return cn.FindDocEntry(docNum);
        }

        public Tuple<List<DocumentoCabecera>, string> FindLastPurchaseInvoice()
        {
            return cn.FindLastPurchaseInvoice();
        }

        
        public Tuple<List<DocumentoCabecera>, string> FindNextPurchaseInvoice(string docNum)
        {
            return cn.FindNextPurchaseInvoice(docNum);
        }

        public Tuple<List<DocumentoCabecera>, string> FindPreviousPurchaseInvoice(string docNum)
        {
            return cn.FindPreviousPurchaseInvoice(docNum);
        }

        public Tuple<List<DocumentoCabecera>, string> FindFirstPurchaseInvoice()
        {
            return cn.FindFirstPurchaseInvoice();
        }

        public Tuple<int, string> FindDocEntryPreliminar(int docNum)
        {
            return cn.FindDocEntryPreliminar(docNum);
        }

       
    }
}
