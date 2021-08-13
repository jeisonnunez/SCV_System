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
    public class ControladorFacturaDeudores: Controlador_Ventas.ControladorDocumentoVenta
    {
        ModeloFacturaDeudores cn = new ModeloFacturaDeudores();

        public Tuple<int, string> FindDocEntry(int docNum)
        {
            return cn.FindDocEntry(docNum);
        }

        public Tuple<int, string> FindDocEntryPreliminar(int docNum)
        {
            return cn.FindDocEntryPreliminar(docNum);
        }

        public Tuple <int,string> SelectDocNum()
        {
            return cn.SelectDocNum();
        }

        public Tuple <List<DocumentoCabecera>,string> FindSalesInvoice(List<DocumentoCabecera> listaPurchaseInvoice)
        {
            return cn.FindSalesInvoice(listaPurchaseInvoice);
        }

        public Tuple<List<DocumentoCabecera>, string> FindLastSalesInvoice()
        {
            return cn.FindLastSalesInvoice();
        }

        public Tuple<List<DocumentoCabecera>, string> FindNextSalesInvoice(string docNum)
        {
            return cn.FindNextSalesInvoice(docNum);
        }

        public Tuple<List<DocumentoCabecera>, string> FindPreviousSalesInvoice(string docNum)
        {
            return cn.FindPreviousSalesInvoice(docNum);
        }
        public Tuple<List<DocumentoCabecera>, string> FindFirstSalesInvoice()
        {
            return cn.FindFirstSalesInvoice();
        }

        
    }
}
