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
    public class ControladorNotaDebitoDeudores : Controlador_Ventas.ControladorDocumentoVenta
    {
        ModeloNotaDebitoDeudores cn = new ModeloNotaDebitoDeudores();

        public Tuple<int, string> FindDocEntry(int docNum)
        {
            return cn.FindDocEntry(docNum);
        }

        public Tuple<int,string> SelectDocNum()
        {
            return cn.SelectDocNum();
        }

        public Tuple<List<DocumentoCabecera>,string> FindSalesDebitNote(List<DocumentoCabecera> listaPurchaseInvoice)
        {
            return cn.FindSalesDebitNote(listaPurchaseInvoice);
        }

        public Tuple<List<DocumentoCabecera>, string> FindFirstSalesDebitNote()
        {
            return cn.FindFirstSalesDebitNote();
        }

        public Tuple<List<DocumentoCabecera>, string> FindLastSalesDebitNote()
        {
            return cn.FindLastSalesDebitNote();
        }

        public Tuple<List<DocumentoCabecera>, string> FindNextSalesDebitNote(string docNum)
        {
            return cn.FindNextSalesDebitNote(docNum);
        }

        public Tuple<List<DocumentoCabecera>, string> FindPreviousSalesDebitNote(string docNum)
        {
            return cn.FindPreviousSalesDebitNote(docNum);
        }

        public Tuple<int, string> FindDocEntryPreliminar(int docNum)
        {
            return cn.FindDocEntryPreliminar(docNum);
        }

       
    }
}
