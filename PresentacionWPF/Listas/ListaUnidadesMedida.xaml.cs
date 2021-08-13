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
    /// Lógica de interacción para ListaUnidadesMedida.xaml
    /// </summary>
    public partial class ListaUnidadesMedida : Window
    {
        private List<Entidades.UnidadesMedida> listaUnidades = new List<Entidades.UnidadesMedida>();

        private string column;
        public List<Entidades.UnidadesMedida> ListaUnidadesResultantes { get => listaUnidades; set => listaUnidades = value; }

        public string Column { get => column; set => column = value; }
        public ListaUnidadesMedida(List<Entidades.UnidadesMedida> listaMedidas)
        {
            InitializeComponent();

            ListaUnidadesResultantes.Clear();

            ListBoxItem.ItemsSource = listaMedidas;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListBoxItem.ItemsSource);

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

                if (Column == ColumnName.UomCode.ToString())
                {
                    return ((item as Entidades.UnidadesMedida).UomCode.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else if (Column == ColumnName.UomName.ToString())
                {
                    return ((item as Entidades.UnidadesMedida).UomName.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else
                {
                    return ((item as Entidades.UnidadesMedida).UomCode.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);

                }


            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ListBoxItem.ItemsSource).Refresh();
        }

        private void txtFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            App.textBox_LostFocus(sender, e);
        }

        private void txtFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            App.textBox_GotFocus(sender, e);
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

        private void ListBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = (ListBox)sender;

            int index = item.SelectedIndex;

            if (index <= -1)
            {

            }
            else
            {
                Entidades.UnidadesMedida result = (Entidades.UnidadesMedida)item.SelectedItem;

                ListaUnidadesResultantes = SaveUnidadesMedidaSeleccionado(result);

                this.Hide();
            }
        }

        private List<Entidades.UnidadesMedida> SaveUnidadesMedidaSeleccionado(Entidades.UnidadesMedida result)
        {
            List<Entidades.UnidadesMedida> listTemporal = new List<Entidades.UnidadesMedida>();

            Entidades.UnidadesMedida itemSeleccionado = new Entidades.UnidadesMedida();

            itemSeleccionado = result;

            listTemporal.Add(itemSeleccionado);

            return listTemporal;
        }

        public List<Entidades.UnidadesMedida> GetListUnidadesMedida()
        {
            return ListaUnidadesResultantes;
        }

        private string SetColumnName(string column)
        {
            string opcion = null;

            if (column == "Codigo")
            {
                opcion = ColumnName.UomCode.ToString();
            }
            else if (column == "Nombre de Unidad")
            {
                opcion = ColumnName.UomName.ToString();
            }

            else
            {
                opcion = ColumnName.UomCode.ToString();
            }

            return opcion;
        }

        enum ColumnName { UomCode = 0, UomName = 1 };
    }
}
