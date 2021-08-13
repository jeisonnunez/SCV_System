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
    /// Lógica de interacción para ListaDateTXT.xaml
    /// </summary>
    public partial class ListaDateTXT : Window
    {
        private List<DateTXT> listaDateTXT = new List<DateTXT>();
        public List<DateTXT> ListaDateTXTResultante { get => listaDateTXT; set => listaDateTXT = value; }

        private string column;
        public string Column { get => column; set => column = value; }
        public ListaDateTXT(List<DateTXT> listaDateTXT)
        {
            InitializeComponent();

            ListaDateTXTResultante.Clear();

            ListDateTXT.ItemsSource = listaDateTXT;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListDateTXT.ItemsSource);
            
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

                if (Column == ColumnName.RefDate.ToString())
                {
                    return ((item as DateTXT).RefDate.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else if (Column == ColumnName.Registre.ToString())
                {
                    return ((item as DateTXT).Registre.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }

                else
                {
                    return ((item as DateTXT).RefDate.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);

                }


            }
        }

        public List<DateTXT> GetListDateTXT()
        {
            return ListaDateTXTResultante;
        }

        private void txtFilter_LostFocus(object sender, RoutedEventArgs e)
        {

            App.textBox_LostFocus(sender, e);
        }

        private void txtFilter_GotFocus(object sender, RoutedEventArgs e)
        {

            App.textBox_GotFocus(sender, e);
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ListDateTXT.ItemsSource).Refresh();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        private void ListDateTXT_Click(object sender, RoutedEventArgs e)
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

        private void ListDateTXT_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = (ListView)sender;

            int index = item.SelectedIndex;

            if (index <= -1)
            {

            }
            else
            {
                DateTXT result = (DateTXT)item.SelectedItem;

                ListaDateTXTResultante = SaveDateTXT(result);

                this.Hide();
            }

        }

        private List<DateTXT> SaveDateTXT(DateTXT result)
        {
            List<DateTXT> listTemporal = new List<DateTXT>();

            DateTXT txt = new DateTXT();

            txt = result;

            listTemporal.Add(txt);

            return listTemporal;
        }

        private string SetColumnName(string column)
        {
            string opcion = null;

            if (column == "Fecha Contabilizacion")
            {
                opcion = ColumnName.RefDate.ToString();
            }
            else if (column == "Numero Registros")
            {
                opcion = ColumnName.Registre.ToString();
            }

            else
            {
                opcion = ColumnName.RefDate.ToString();
            }

            return opcion;
        }

        enum ColumnName { RefDate = 0, Registre = 1 };


    }
}
