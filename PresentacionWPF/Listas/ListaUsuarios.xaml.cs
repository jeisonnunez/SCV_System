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
    /// Lógica de interacción para ListaUsuarios.xaml
    /// </summary>
    public partial class ListaUsuarios : Window
    {
        private string column;

        public string Column { get => column; set => column = value; }

        private List<Entidades.Usuarios> listaUsuarios = new List<Entidades.Usuarios>();
        public List<Entidades.Usuarios> ListaUsuariosResultantes { get => listaUsuarios; set => listaUsuarios = value; }
        public ListaUsuarios(List<Entidades.Usuarios> listaUsuarios)
        {
            InitializeComponent();

            ListaUsuariosResultantes.Clear();

            ListaUsuario.ItemsSource = listaUsuarios;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListaUsuario.ItemsSource);

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

                if (Column == ColumnName.User_code.ToString())
                {
                    return ((item as Entidades.Usuarios).User_code.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else if (Column == ColumnName.User_name.ToString())
                {
                    return ((item as Entidades.Usuarios).User_name.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }

                else
                {
                    return ((item as Entidades.Usuarios).User_code.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);

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

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ListaUsuario.ItemsSource).Refresh();
        }

        private void ListaUsuario_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = (ListBox)sender;

            int index = item.SelectedIndex;

            if (index <= -1)
            {

            }
            else
            {
                Entidades.Usuarios result = (Entidades.Usuarios)item.SelectedItem;

                ListaUsuariosResultantes = SaveUsuarioSeleccionado(result);

                this.Hide();
            }

        }

        private List<Entidades.Usuarios> SaveUsuarioSeleccionado(Entidades.Usuarios result)
        {
            List<Entidades.Usuarios> listTemporal = new List<Entidades.Usuarios>();

            Entidades.Usuarios usuarioSeleccionado = new Entidades.Usuarios();

            usuarioSeleccionado.User_code = result.User_code;

            usuarioSeleccionado.User_name = result.User_name;

            usuarioSeleccionado.Locked = result.Locked;           

            usuarioSeleccionado.UserID = result.UserID;

            listTemporal.Add(usuarioSeleccionado);

            return listTemporal;
        }

        public List<Entidades.Usuarios> GetListUsuarios()
        {
            return ListaUsuariosResultantes;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        private void ListaUsuario_Click(object sender, RoutedEventArgs e)
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
                opcion = ColumnName.User_code.ToString();
            }
            else if (column == "Nombre")
            {
                opcion = ColumnName.User_name.ToString();
            }

            else
            {
                opcion = ColumnName.User_code.ToString();
            }

            return opcion;
        }

        enum ColumnName { User_code = 0, User_name = 1 };
    }
}
