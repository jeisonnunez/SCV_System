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
    /// Lógica de interacción para ListaGruposUnidadesMedida.xaml
    /// </summary>
    public partial class ListaGruposUnidadesMedida : Window
    {
        private List<Entidades.GrupoUnidadesMedidaCabecera> listaGruposUnidades = new List<Entidades.GrupoUnidadesMedidaCabecera>();

        private string column;
        public List<Entidades.GrupoUnidadesMedidaCabecera> ListaGruposUnidadesResultantes { get => listaGruposUnidades; set => listaGruposUnidades = value; }

        public string Column { get => column; set => column = value; }
        public ListaGruposUnidadesMedida(List<Entidades.GrupoUnidadesMedidaCabecera> listaGrupoMedidas)
        {
            InitializeComponent();

            ListaGruposUnidadesResultantes.Clear();

            ListBoxItem.ItemsSource = listaGrupoMedidas;

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

                if (Column == ColumnName.UgpCode.ToString())
                {
                    return ((item as Entidades.GrupoUnidadesMedidaCabecera).UgpCode.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else if (Column == ColumnName.UgpName.ToString())
                {
                    return ((item as Entidades.GrupoUnidadesMedidaCabecera).UgpName.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }                
                else
                {
                    return ((item as Entidades.GrupoUnidadesMedidaCabecera).UgpCode.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);

                }


            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
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
                Entidades.GrupoUnidadesMedidaCabecera result = (Entidades.GrupoUnidadesMedidaCabecera)item.SelectedItem;

                ListaGruposUnidadesResultantes = SaveGrupoUnidadesMedidaSeleccionado(result);

                this.Hide();
            }

        }

        private List<Entidades.GrupoUnidadesMedidaCabecera> SaveGrupoUnidadesMedidaSeleccionado(Entidades.GrupoUnidadesMedidaCabecera result)
        {
            List<Entidades.GrupoUnidadesMedidaCabecera> listTemporal = new List<Entidades.GrupoUnidadesMedidaCabecera>();

            Entidades.GrupoUnidadesMedidaCabecera itemSeleccionado = new Entidades.GrupoUnidadesMedidaCabecera();

            itemSeleccionado = result;

            listTemporal.Add(itemSeleccionado);

            return listTemporal;
        }

        public List<Entidades.GrupoUnidadesMedidaCabecera> GetListGrupoUnidadesMedida()
        {
            return ListaGruposUnidadesResultantes;
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

        private string SetColumnName(string column)
        {
            string opcion = null;

            if (column == "Grupo")
            {
                opcion = ColumnName.UgpCode.ToString();
            }
            else if (column == "Descripcion Grupo")
            {
                opcion = ColumnName.UgpName.ToString();
            }
            
            else
            {
                opcion = ColumnName.UgpCode.ToString();
            }

            return opcion;
        }

        enum ColumnName { UgpCode = 0, UgpName = 1};
    }
}
