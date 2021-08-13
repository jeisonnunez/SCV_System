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
    /// Lógica de interacción para ListaPeriodoISLR.xaml
    /// </summary>
    public partial class ListaPeriodoISLR : Window
    {
        private List<PeriodoISLR> listaPeriodoISLR = new List<PeriodoISLR>();
        public List<PeriodoISLR> ListaPeriodoISLRResultante { get => listaPeriodoISLR; set => listaPeriodoISLR = value; }

        private string column;
        public string Column { get => column; set => column = value; }
        public ListaPeriodoISLR(List<PeriodoISLR> listaPeriodoISLR)
        {
            InitializeComponent();

            ListaPeriodoISLRResultante.Clear();

            ListPeriodoISLR.ItemsSource = listaPeriodoISLR;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListPeriodoISLR.ItemsSource);

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
                    return ((item as PeriodoISLR).Code.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else if (Column == ColumnName.Quantity.ToString())
                {
                    return ((item as PeriodoISLR).Quantity.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }

                else
                {
                    return ((item as PeriodoISLR).Code.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);

                }


            }
        }

        public List<PeriodoISLR> GetListPeriodoISLR()
        {
            return ListaPeriodoISLRResultante;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        private void ListPeriodoISLR_Click(object sender, RoutedEventArgs e)
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

        private void ListPeriodoISLR_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = (ListView)sender;

            int index = item.SelectedIndex;

            if (index <= -1)
            {

            }
            else
            {
                PeriodoISLR result = (PeriodoISLR)item.SelectedItem;

                ListaPeriodoISLRResultante = SaveDateTXT(result);

                this.Hide();
            }
        }

        private List<PeriodoISLR> SaveDateTXT(PeriodoISLR result)
        {
            List<PeriodoISLR> listTemporal = new List<PeriodoISLR>();

            PeriodoISLR xml = new PeriodoISLR();

            xml = result;

            listTemporal.Add(xml);

            return listTemporal;
        }

        private string SetColumnName(string column)
        {
            string opcion = null;

            if (column == "Periodo")
            {
                opcion = ColumnName.Code.ToString();
            }
            else if (column == "Cantidad")
            {
                opcion = ColumnName.Quantity.ToString();
            }

            else
            {
                opcion = ColumnName.Code.ToString();
            }

            return opcion;
        }

        enum ColumnName { Code = 0, Quantity = 1 };

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ListPeriodoISLR.ItemsSource).Refresh();
        }

        private void txtFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            App.textBox_LostFocus(sender, e);
        }

        private void txtFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            App.textBox_GotFocus(sender, e);
        }
    }
}
