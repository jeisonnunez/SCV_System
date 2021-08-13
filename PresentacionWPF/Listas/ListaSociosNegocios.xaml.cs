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
    /// Lógica de interacción para ListaSociosNegocios.xaml
    /// </summary>
    public partial class ListaSociosNegocios : Window
    {
        private List<SocioNegocio> listaSocioNegocio = new List<SocioNegocio>();

        private string column;

        public List<SocioNegocio> ListaSocioNegocioResultante { get => listaSocioNegocio; set => listaSocioNegocio = value; }
        public string Column { get => column; set => column = value; }

        public ListaSociosNegocios(List<SocioNegocio> listaSocioNegocio)
        {
            InitializeComponent();

            ListaSocioNegocioResultante.Clear();

            ListBoxSN.ItemsSource = listaSocioNegocio;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListBoxSN.ItemsSource);

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

                if (Column == ColumnName.CardCode.ToString())
                {
                    return ((item as SocioNegocio).CardCode.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else if (Column == ColumnName.CardName.ToString())
                {
                    return ((item as SocioNegocio).CardName.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else if (Column == ColumnName.Balance.ToString())
                {
                    return ((item as SocioNegocio).Balance.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else
                {
                    return ((item as SocioNegocio).CardCode.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);

                }


            }
                
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ListBoxSN.ItemsSource).Refresh();
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            App.textBox_LostFocus(sender, e);
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            App.textBox_GotFocus(sender, e);
        }

        private void ListBoxSN_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = (ListBox)sender;

            int index = item.SelectedIndex;

            if (index <= -1)
            {
                
            }
            else
            {
                SocioNegocio result = (SocioNegocio)item.SelectedItem;

                ListaSocioNegocioResultante = SaveSNSeleccionado(result);

                this.Hide();
            }

        }

        private List<SocioNegocio> SaveSNSeleccionado(SocioNegocio result)
        {
            List<SocioNegocio> listTemporal = new List<SocioNegocio>();

            SocioNegocio snSeleccionado = new SocioNegocio();

            snSeleccionado = result;

            listTemporal.Add(snSeleccionado);

            return listTemporal;
        }

        public List<SocioNegocio> GetListSN()
        {
            return ListaSocioNegocioResultante;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        private void ListBoxSN_Click(object sender, RoutedEventArgs e)
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
            string opcion =null;

            if (column == "Codigo")
            {
                opcion = ColumnName.CardCode.ToString();
            }
            else if (column == "Nombre")
            {
                opcion = ColumnName.CardName.ToString();
            }
            else if (column == "Balance")
            {
                opcion = ColumnName.Balance.ToString();
            }
            else
            {
                opcion = ColumnName.CardCode.ToString();
            }

            return opcion;
        }

        enum ColumnName {CardCode = 0, CardName = 1, Balance = 2 };
    }
}
