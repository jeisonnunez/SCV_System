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
using Vista;
using Entidades;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para ListaArticulos.xaml
    /// </summary>
    public partial class ListaArticulos : Window
    {
        private List<Entidades.Articulos> listaArticulos = new List<Entidades.Articulos>();

        private string column;
        public List<Entidades.Articulos> ListaArticulosResultantes { get => listaArticulos; set => listaArticulos = value; }

        public string Column { get => column; set => column = value; }
        public ListaArticulos(List<Entidades.Articulos> listaArticulos)
        {
            InitializeComponent();

            ListaArticulosResultantes.Clear();

            ListBoxItem.ItemsSource = listaArticulos;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListBoxItem.ItemsSource);

            view.Filter = UserFilter;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
            {
                return true;
            }

            else
            {

                if (Column == ColumnName.ItemCode.ToString())
                {
                    return ((item as Entidades.Articulos).ItemCode.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else if (Column == ColumnName.ItemName.ToString())
                {
                    return ((item as Entidades.Articulos).ItemName.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else if (Column == ColumnName.OnHand.ToString())
                {
                    return ((item as Entidades.Articulos).OnHand.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else
                {
                    return ((item as Entidades.Articulos).ItemCode.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);

                }


            }
        }

        private void ListBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = (ListBox)sender;

            int index = item.SelectedIndex;

            if (index <= -1)
            {

            }
            else
            {
                Entidades.Articulos result = (Entidades.Articulos)item.SelectedItem;

                ListaArticulosResultantes = SaveItemSeleccionado(result);

                this.Hide();
            }

           
        }

        private List<Entidades.Articulos> SaveItemSeleccionado(Entidades.Articulos result)
        {
            List<Entidades.Articulos> listTemporal = new List<Entidades.Articulos>();

            Entidades.Articulos itemSeleccionado = new Entidades.Articulos();

            itemSeleccionado = result;

            listTemporal.Add(itemSeleccionado);

            return listTemporal;
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
          
                App.textBox_LostFocus(sender, e);
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
          
                App.textBox_GotFocus(sender, e);
        }


        public List<Entidades.Articulos> GetListItem()
        {
            return ListaArticulosResultantes;
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ListBoxItem.ItemsSource).Refresh();
        }

        private void ListBoxItem_Click(object sender, RoutedEventArgs e)
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
                opcion = ColumnName.ItemCode.ToString();
            }
            else if (column == "Nombre")
            {
                opcion = ColumnName.ItemName.ToString();
            }
            else if (column == "En Stock")
            {
                opcion = ColumnName.OnHand.ToString();
            }
            else
            {
                opcion = ColumnName.ItemCode.ToString();
            }

            return opcion;
        }

        enum ColumnName { ItemCode = 0, ItemName = 1, OnHand = 2 };
    }

   
}
