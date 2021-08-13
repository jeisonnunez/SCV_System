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
    /// Lógica de interacción para ListaCuentas.xaml
    /// </summary>
    public partial class ListaCuentas : Window
    {
        private List<Cuenta> listaCuentas = new List<Cuenta>();
        public List<Cuenta> ListaCuentasResultado { get => listaCuentas; set => listaCuentas = value; }

        private string column;

        public string Column { get => column; set => column = value; }
        public ListaCuentas(List<Cuenta> listaCuentasAsociadas)
        {
            InitializeComponent();

            ListaCuentasResultado.Clear();
                        
            ListaCuenta.ItemsSource = listaCuentasAsociadas;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListaCuenta.ItemsSource);

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

                if (Column == ColumnName.AcctCode.ToString())
                {
                    return ((item as Cuenta).AcctCode.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else if (Column == ColumnName.AcctName.ToString())
                {
                    return ((item as Cuenta).AcctName.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }

                else
                {
                    return ((item as Cuenta).AcctCode.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);

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
            CollectionViewSource.GetDefaultView(ListaCuenta.ItemsSource).Refresh();
        }



        private void ListaCuenta_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = (ListView)sender;

            int index = item.SelectedIndex;

            if (index <= -1)
            {

            }
            else
            {
                Cuenta result = (Cuenta)item.SelectedItem;

                ListaCuentasResultado = SaveAccount(result);

                this.Hide();
            }

        }

        private List<Cuenta> SaveAccount(Cuenta result)
        {
            List<Cuenta> listTemporal = new List<Cuenta>();

            Cuenta account = new Cuenta();

            account = result;

            listTemporal.Add(account);

            return listTemporal;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        public List<Cuenta> GetListAccount()
        {
            return ListaCuentasResultado;
        }

        private void ListaCuenta_Click(object sender, RoutedEventArgs e)
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
                opcion = ColumnName.AcctCode.ToString();
            }
            else if (column == "Nombre")
            {
                opcion = ColumnName.AcctName.ToString();
            }

            else
            {
                opcion = ColumnName.AcctCode.ToString();
            }

            return opcion;
        }

        enum ColumnName { AcctCode = 0, AcctName = 1 };
    }
}
