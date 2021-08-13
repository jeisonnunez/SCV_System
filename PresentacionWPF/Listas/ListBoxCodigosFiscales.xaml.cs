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
    /// Lógica de interacción para ListBoxCodigosFiscales.xaml
    /// </summary>
    public partial class ListBoxCodigosFiscales : Window
    {
        private string column;

        public string Column { get => column; set => column = value; }

        private List<Entidades.CodigosFiscales> listaCodigosFiscales = new List<Entidades.CodigosFiscales>();
        public List<Entidades.CodigosFiscales> ListaCodigosFiscales { get => listaCodigosFiscales; set => listaCodigosFiscales = value; }
        public ListBoxCodigosFiscales(List<Entidades.CodigosFiscales> listaCodigosFiscales)
        {
            InitializeComponent();

            ListaCodigosFiscales.Clear();

            CodigosFiscales.ItemsSource = listaCodigosFiscales;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(CodigosFiscales.ItemsSource);

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
                else if (Column == ColumnName.Name.ToString())
                {
                    return ((item as Entidades.CodigosFiscales).Name.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
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

        private void CodigosFiscales_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = (ListBox)sender;            

            int index = item.SelectedIndex;

            if (index <= -1)
            {

            }
            else
            {
                Entidades.CodigosFiscales result = (Entidades.CodigosFiscales)item.SelectedItem;

                ListaCodigosFiscales = SaveCodigoSeleccionado(result);

                this.Hide();
            }

           

        }

        private List<Entidades.CodigosFiscales> SaveCodigoSeleccionado(Entidades.CodigosFiscales result)
        {
            List<Entidades.CodigosFiscales> listTemporal = new List<Entidades.CodigosFiscales>();

            Entidades.CodigosFiscales codigoSeleccionado = new Entidades.CodigosFiscales();

            codigoSeleccionado.Code = result.Code;
            codigoSeleccionado.Name = result.Name;
            codigoSeleccionado.Rate = result.Rate;
            codigoSeleccionado.U_IDA_Alicuota = result.U_IDA_Alicuota;
            codigoSeleccionado.ValidForAP = result.ValidForAP;
            codigoSeleccionado.ValidForAR = result.ValidForAR;
            codigoSeleccionado.Lock1 = result.Lock1;

            listTemporal.Add(codigoSeleccionado);

            return listTemporal;
        }

        public List<Entidades.CodigosFiscales> GetListCodigosFiscales()
        {
            return ListaCodigosFiscales;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {

            App.textBox_LostFocus(sender, e);
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {

            App.textBox_GotFocus(sender, e);
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(CodigosFiscales.ItemsSource).Refresh();
        }


        private void CodigosFiscales_Click(object sender, RoutedEventArgs e)
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
            else if (column == "Nombre")
            {
                opcion = ColumnName.Name.ToString();
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

        enum ColumnName { Code = 0, Name = 1, Rate = 2 };
    }
}
