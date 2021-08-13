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
    /// Lógica de interacción para ListaClasesImpuestos.xaml
    /// </summary>
    public partial class ListaClasesImpuestos : Window
    {
        private List<Entidades.ClasesImpuestos> listaClasesImpuesto = new List<Entidades.ClasesImpuestos>();

        private string column;

        public string Column { get => column; set => column = value; }
        public List<Entidades.ClasesImpuestos> ListaClaseImpuestoResultado { get => listaClasesImpuesto; set => listaClasesImpuesto = value; }
        public ListaClasesImpuestos(List<Entidades.ClasesImpuestos> listaClasesImpuesto)
        {
            InitializeComponent();

            ListaClaseImpuestoResultado.Clear();

            ListClasesImpuesto.ItemsSource = listaClasesImpuesto;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListClasesImpuesto.ItemsSource);

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
                    return ((item as Entidades.ClasesImpuestos).Code.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else if (Column == ColumnName.Rate.ToString())
                {
                    return ((item as Entidades.ClasesImpuestos).Rate.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }

                else
                {
                    return ((item as Entidades.ClasesImpuestos).Code.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);

                }


            }
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ListClasesImpuesto.ItemsSource).Refresh();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        private void ListClasesImpuesto_Click(object sender, RoutedEventArgs e)
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

        private void ListClasesImpuesto_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = (ListBox)sender;

            int index = item.SelectedIndex;

            if (index <= -1)
            {

            }
            else
            {
                Entidades.ClasesImpuestos result = (Entidades.ClasesImpuestos)item.SelectedItem;

                ListaClaseImpuestoResultado = SaveCodigos(result);

                this.Hide();
            }
        }

        private List<Entidades.ClasesImpuestos> SaveCodigos(Entidades.ClasesImpuestos result)
        {
            List<Entidades.ClasesImpuestos> listTemporal = new List<Entidades.ClasesImpuestos>();

            Entidades.ClasesImpuestos code = new Entidades.ClasesImpuestos();

            code = result;

            listTemporal.Add(code);

            return listTemporal;
        }

        public List<Entidades.ClasesImpuestos> GetListClasesImpuesto()
        {
            return ListaClaseImpuestoResultado;
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

        enum ColumnName { Code = 0, Rate = 1 };
    }
}
