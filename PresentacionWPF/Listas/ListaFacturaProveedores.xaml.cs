using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para ListaFacturaProveedores.xaml
    /// </summary>
    public partial class ListaFacturaProveedores : Window
    {
        private List<Entidades.DocumentoCabecera> listaDocumentoCabecera = new List<Entidades.DocumentoCabecera>();
        public List<DocumentoCabecera> ListaDocumentoCabecera { get => listaDocumentoCabecera; set => listaDocumentoCabecera = value; }

        private string column;

        public string Column { get => column; set => column = value; }

        public ListaFacturaProveedores(List<DocumentoCabecera> listaDocumentoCabecera)
        {
            InitializeComponent();

            ListaDocumentoCabecera.Clear();

            ListFacturaProveedores.ItemsSource = listaDocumentoCabecera;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListFacturaProveedores.ItemsSource);

            view.Filter = UserFilter;
        }

        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
            {
                return true;
            }

            else
            {

                if (Column == ColumnName.DocNum.ToString())
                {
                    return ((item as DocumentoCabecera).DocNum.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else if (Column == ColumnName.DocDate.ToString())
                {
                    return ((item as DocumentoCabecera).DocDate.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else if (Column == ColumnName.CardCode.ToString())
                {
                    return ((item as DocumentoCabecera).CardCode.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else if (Column == ColumnName.Comments.ToString())
                {
                    return ((item as DocumentoCabecera).Comments.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }

                else
                {
                    return ((item as DocumentoCabecera).DocNum.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);

                }


            }
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ListFacturaProveedores.ItemsSource).Refresh();
        }



        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {

            App.textBox_LostFocus(sender, e);
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {

            App.textBox_GotFocus(sender, e);
        }

        private void ListBoxPurchase_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = (ListBox)sender;

            int index = item.SelectedIndex;

            if (index <= -1)
            {

            }
            else
            {
                DocumentoCabecera result = (DocumentoCabecera)item.SelectedItem;

                ListaDocumentoCabecera = SaveDocument(result);

                this.Hide();
            }

        }

        private List<DocumentoCabecera> SaveDocument(DocumentoCabecera result)
        {
            List<DocumentoCabecera> listTemporal = new List<DocumentoCabecera>();

            DocumentoCabecera itemSeleccionado = new DocumentoCabecera();

            itemSeleccionado = result;

            listTemporal.Add(itemSeleccionado);

            return listTemporal;
        }

        public List<DocumentoCabecera> GetListDocument()
        {
            return ListaDocumentoCabecera;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        private void ListFacturaProveedores_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Column = ((GridViewColumnHeader)e.OriginalSource).Column.Header.ToString();

                Column = SetColumnName(Column);
            }
            catch (InvalidCastException ex)
            {

            }
        }

        private string SetColumnName(string column)
        {
            string opcion = null;

            if (column == "Numero")
            {
                opcion = ColumnName.DocNum.ToString();
            }
            else if (column == "Fecha")
            {
                opcion = ColumnName.DocDate.ToString();
            }
            else if (column == "Proveedor")
            {
                opcion = ColumnName.CardCode.ToString();
            }
            else if (column == "Comentario")
            {
                opcion = ColumnName.Comments.ToString();
            }

            else
            {
                opcion = ColumnName.DocNum.ToString();
            }

            return opcion;
        }

        enum ColumnName { DocNum = 0, DocDate = 1, CardCode = 2, Comments = 3 };
    }
}
