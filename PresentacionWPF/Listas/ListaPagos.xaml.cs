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
    /// Lógica de interacción para ListaPagos.xaml
    /// </summary>
    public partial class ListaPagos : Window
    {
        private string column;

        public string Column { get => column; set => column = value; }

        private List<Payment> listaPayment = new List<Payment>();
        public List<Payment> ListaPayment { get => listaPayment; set => listaPayment = value; }
        public ListaPagos(List<Payment> listaPayment)
        {
            InitializeComponent();

            ListaPayment.Clear();

            ListPayment.ItemsSource = listaPayment;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListPayment.ItemsSource);

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
                    return ((item as Payment).DocNum.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else if (Column == ColumnName.DocDate.ToString())
                {
                    return ((item as Payment).DocDate.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else if (Column == ColumnName.CardCode.ToString())
                {
                    return ((item as Payment).CardCode.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else if (Column == ColumnName.JrnlMemo.ToString())
                {
                    return ((item as Payment).JrnlMemo.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }

                else
                {
                    return ((item as Payment).DocNum.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);

                }


            }
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            App.textBox_LostFocus(sender, e);
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            App.textBox_GotFocus(sender, e);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        private void ListPayment_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = (ListBox)sender;

            int index = item.SelectedIndex;

            if (index <= -1)
            {

            }
            else
            {

                Payment result = (Payment)item.SelectedItem;

                ListaPayment = SavePayment(result);

                this.Hide();
            }

        }

        private List<Payment> SavePayment(Payment result)
        {
            List<Payment> listTemporal = new List<Payment>();

            Payment itemSeleccionado = new Payment();

            itemSeleccionado = result;

            listTemporal.Add(itemSeleccionado);

            return listTemporal;
        }

        public List<Payment> GetListPayment()
        {
            return ListaPayment;
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ListPayment.ItemsSource).Refresh();
        }

        private void ListPayment_Click(object sender, RoutedEventArgs e)
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
                opcion = ColumnName.JrnlMemo.ToString();
            }

            else
            {
                opcion = ColumnName.DocNum.ToString();
            }

            return opcion;
        }

        enum ColumnName { DocNum = 0, DocDate = 1, CardCode = 2, JrnlMemo = 3 };
    }
}
