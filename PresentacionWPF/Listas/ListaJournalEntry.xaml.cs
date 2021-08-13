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
    /// Lógica de interacción para ListaJournalEntry.xaml
    /// </summary>
    public partial class ListaJournalEntry : Window
    {
        private string column;

        public string Column { get => column; set => column = value; }

        private List<AsientoCabecera> listaJournalEntry = new List<AsientoCabecera>();
        public List<AsientoCabecera> ListaJournalEntryResultantes { get => listaJournalEntry; set => listaJournalEntry = value; }
        public ListaJournalEntry(List<AsientoCabecera> listaJournalEntry)
        {
            InitializeComponent();

            ListaJournalEntryResultantes.Clear();

            ListJournalEntry.ItemsSource = listaJournalEntry;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListJournalEntry.ItemsSource);

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

                if (Column == ColumnName.TransId.ToString())
                {
                    return ((item as AsientoCabecera).TransId.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else if (Column == ColumnName.Memo.ToString())
                {
                    return ((item as AsientoCabecera).Memo.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                }

                else
                {
                    return ((item as AsientoCabecera).TransId.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);

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


        private void ListJournalEntry_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = (ListBox)sender;

            int index = item.SelectedIndex;

            if (index <= -1)
            {

            }
            else
            {
                AsientoCabecera result = (AsientoCabecera)item.SelectedItem;

                ListaJournalEntryResultantes = SaveJournalEntry(result);

                this.Hide();
            }

        }

        private List<AsientoCabecera> SaveJournalEntry(AsientoCabecera result)
        {
            List<AsientoCabecera> listTemporal = new List<AsientoCabecera>();

            AsientoCabecera code = new AsientoCabecera();

            code = result;

            listTemporal.Add(code);

            return listTemporal;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        public List<AsientoCabecera> GetListJournalEntry()
        {
            return ListaJournalEntryResultantes;
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ListJournalEntry.ItemsSource).Refresh();
        }

        private void ListJournalEntry_Click(object sender, RoutedEventArgs e)
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

            if (column == "Asiento")
            {
                opcion = ColumnName.TransId.ToString();
            }
            else if (column == "Comentario")
            {
                opcion = ColumnName.Memo.ToString();
            }

            else
            {
                opcion = ColumnName.TransId.ToString();
            }

            return opcion;
        }

        enum ColumnName { TransId = 0, Memo = 1 };
    }
}
