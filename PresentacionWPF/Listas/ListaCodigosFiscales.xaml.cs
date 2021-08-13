using Entidades;
using Vista;
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
    /// Lógica de interacción para ListaCodigosFiscales.xaml
    /// </summary>
    public partial class ListaCodigosFiscales : Window
    {
        private List<Entidades.CodigosFiscales> listaCodigosFiscales = new List<Entidades.CodigosFiscales>();

        private string column;

        public string Column { get => column; set => column = value; }
        public List<Entidades.CodigosFiscales> ListaCodigosResultado { get => listaCodigosFiscales; set => listaCodigosFiscales = value; }
        public ListaCodigosFiscales(List<Entidades.CodigosFiscales> listaCodigosFiscales)
        {
            InitializeComponent();

            ListaCodigosResultado.Clear();

            ListCodigosFiscales.ItemsSource = listaCodigosFiscales;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListCodigosFiscales.ItemsSource);

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

                if (Column == ColumnName.Code.ToString())
                {
                    return ((item as Entidades.CodigosFiscales).Code.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else if (Column == ColumnName.Rate.ToString())
                {
                    return ((item as Entidades.CodigosFiscales).Rate.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                
                else
                {
                    return ((item as Entidades.CodigosFiscales).Code.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);

                }


            }
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ListCodigosFiscales.ItemsSource).Refresh();
        }
        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {

            App.textBox_LostFocus(sender, e);
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {

            App.textBox_GotFocus(sender, e);
        }

        private void ListCodigosFiscales_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = (ListBox)sender;

            int index = item.SelectedIndex;

            if (index <= -1)
            {

            }
            else
            {
                Entidades.CodigosFiscales result = (Entidades.CodigosFiscales)item.SelectedItem;

                ListaCodigosResultado = SaveCodigos(result);

                this.Hide();
            }

        }

        private List<Entidades.CodigosFiscales> SaveCodigos(Entidades.CodigosFiscales result)
        {
            List<Entidades.CodigosFiscales> listTemporal = new List<Entidades.CodigosFiscales>();

            Entidades.CodigosFiscales code = new Entidades.CodigosFiscales();

            code = result;

            listTemporal.Add(code);

            return listTemporal;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        public List<Entidades.CodigosFiscales> GetListCodigosFiscales()
        {
            return ListaCodigosResultado;
        }

        private void ListCodigosFiscales_Click(object sender, RoutedEventArgs e)
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

            if (column == "Codigo")
            {
                opcion = ColumnName.Code.ToString();
            }
            else if (column == "Tasa")
            {
                opcion = ColumnName.Rate.ToString();
            }
           
            else
            {
                opcion = ColumnName.Code.ToString();
            }

            return opcion;
        }

        enum ColumnName { Code = 0, Rate = 1};
    }
}
