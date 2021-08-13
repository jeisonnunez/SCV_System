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
using Entidades;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para ListaRetencionImpuesto.xaml
    /// </summary>
    public partial class ListaRetencionImpuesto : Window
    {
        private string column;

        public string Column { get => column; set => column = value; }

        private List<Entidades.RetencionImpuesto> listaRetencionImpuesto = new List<Entidades.RetencionImpuesto>();

        public List<Entidades.RetencionImpuesto> ListaRetencionImpuestoResultante { get => listaRetencionImpuesto; set => listaRetencionImpuesto = value; }
        public ListaRetencionImpuesto(List<Entidades.RetencionImpuesto> listaRetencionImpuesto)
        {
            InitializeComponent();

            ListaRetencionImpuestoResultante.Clear();

            ListaRetencionImpuestos.ItemsSource = listaRetencionImpuesto;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListaRetencionImpuestos.ItemsSource);

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

                if (Column == ColumnName.Wt_Code.ToString())
                {
                    return ((item as Entidades.RetencionImpuesto).Wt_Code.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else if (Column == ColumnName.Wt_Name.ToString())
                {
                    return ((item as Entidades.RetencionImpuesto).Wt_Name.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else if (Column == ColumnName.Rate.ToString())
                {
                    return ((item as Entidades.RetencionImpuesto).Rate.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else if (Column == ColumnName.EffecDate.ToString())
                {
                    return ((item as Entidades.RetencionImpuesto).EffecDate.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }

                else
                {
                    return ((item as Entidades.RetencionImpuesto).Wt_Code.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);

                }


            }
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ListaRetencionImpuestos.ItemsSource).Refresh();
        }
        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            App.textBox_LostFocus(sender, e);
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            App.textBox_GotFocus(sender, e);
        }

        private void ListaRetencionImpuesto_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = (ListBox)sender;

            int index = item.SelectedIndex;

            if (index <= -1)
            {

            }
            else
            {
                Entidades.RetencionImpuesto result = (Entidades.RetencionImpuesto)item.SelectedItem;

                ListaRetencionImpuestoResultante = SaveRetencionImpuesto(result);

                this.Hide();
            }

        }

      

        private List<Entidades.RetencionImpuesto> SaveRetencionImpuesto(Entidades.RetencionImpuesto result)
        {
            List<Entidades.RetencionImpuesto> listTemporal = new List<Entidades.RetencionImpuesto>();

            Entidades.RetencionImpuesto retencionImpuesto = new Entidades.RetencionImpuesto();

            retencionImpuesto = result;

            listTemporal.Add(retencionImpuesto);

            return listTemporal;
        }

        public List<Entidades.RetencionImpuesto> GetListRetencionImpuesto()
        {
            return ListaRetencionImpuestoResultante;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        private void ListaRetencionImpuestos_Click(object sender, RoutedEventArgs e)
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
                opcion = ColumnName.Wt_Code.ToString();
            }
            else if (column == "Nombre")
            {
                opcion = ColumnName.Wt_Name.ToString();
            }
            else if (column == "Tasa")
            {
                opcion = ColumnName.Rate.ToString();
            }
            else if (column == "Fecha Efectividad")
            {
                opcion = ColumnName.EffecDate.ToString();
            }

            else
            {
                opcion = ColumnName.Wt_Code.ToString();
            }

            return opcion;
        }

        enum ColumnName { Wt_Code = 0, Wt_Name = 1, Rate = 2, EffecDate = 3 };
    }
}
