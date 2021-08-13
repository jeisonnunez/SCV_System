using System;
using System.Collections.Generic;
using System.Data;
using System.Collections;
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
using Negocio;
using Entidades;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para LibroMayor.xaml
    /// </summary>
    public partial class LibroMayor : Window
    {
        ControladorLibroMayor cn = new ControladorLibroMayor();

        DataTable dt = new DataTable();

        public static TablaLibroMayor dtLibroMayor;
        public LibroMayor()
        {
            InitializeComponent();
        }

        private void comboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            App.comboBox_LostFocus(sender, e);
        }

        private void comboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            App.comboBox_GotFocus(sender, e);
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            App.textBox_LostFocus(sender, e);
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            App.textBox_GotFocus(sender, e);
        }

        private void comboBoxItem_GotFocus(object sender, RoutedEventArgs e)
        {
            App.comboBoxItem_GotFocus(sender, e);
        }

        private void comboBoxItem_LostFocus(object sender, RoutedEventArgs e)
        {
            App.comboBoxItem_LostFocus(sender, e);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        private void btnWindow_Close(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
        private void cbxCuentas_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void cbxCuentas_Unchecked(object sender, RoutedEventArgs e)
        {

        }       

        private void cbxSN_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void cbxSN_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void btnEjecutar_Click(object sender, RoutedEventArgs e)
        {
            switch (btnEjecutar.Content.ToString())
            {
                case "OK":

                    var result = cn.ExecuteLibroMayor(dt, dpHFechaContabilizacion.SelectedDate, cbxMonedaLocalySystema.IsChecked, cbxMonedaSistema.IsChecked, cbxMonedaExtranjera.IsChecked, dpDFechaContabilizacion.SelectedDate);

                    if (result.Item2 == null)
                    {

                        if (result.Item1.Rows.Count >= 1)
                        {
                            dtLibroMayor.ClearDatatable();

                            dtLibroMayor.Show();

                            dtLibroMayor.SetDatePicker(dpDFechaContabilizacion.SelectedDate, dpHFechaContabilizacion.SelectedDate);

                            dtLibroMayor.SetVisibility(cbxMonedaLocalySystema.IsChecked, cbxMonedaSistema.IsChecked, cbxMonedaExtranjera.IsChecked);

                            dtLibroMayor.SetDataTable(result.Item1);
                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se encontraron resultados con los parametros establecidos: " + result.Item2, Brushes.LightBlue, Brushes.Black, "002-interface-1.png");
                        }
                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                    }

                    break;

            }
        }

        private void chkSelectAll_Checked(object sender, RoutedEventArgs e)
        {
            SetChecked();
        }

        private void chkSelectAll_Unchecked(object sender, RoutedEventArgs e)
        {
            SetUnChecked();
        }

        private void GetAccount(DataTable listAccount)
        {
            dt = SetSeleccionado(listAccount);

            dgLibroMayor.ItemsSource = dt.DefaultView;

            dgLibroMayor.CanUserAddRows = false;

            dgLibroMayor.CanUserDeleteRows = false;

            dgLibroMayor.CanUserSortColumns = false;

            dgLibroMayor.IsReadOnly = false;
        }

        private DataTable SetSeleccionado(DataTable dt)
        {

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {

                    if (column.ToString() == "Seleccionado")
                    {
                        row["Seleccionado"] = false;

                    }

                }

            }

            return dt;
        }

        private void SetChecked()
        {

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {

                    if (column.ToString() == "Seleccionado")
                    {
                        row["Seleccionado"] = true;

                    }

                }

            }


        }

        private void SetUnChecked()
        {

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {

                    if (column.ToString() == "Seleccionado")
                    {
                        row["Seleccionado"] = false;

                    }

                }

            }


        }

        private void InicializacionBasica()
        {
            var result = cn.FindAllAccount();

            if (result.Item2 == null)
            {
                GetAccount(result.Item1);
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InicializacionBasica();

            CreateTableLibroMayor();

        }

        private void CreateTableLibroMayor()
        {
            TablaLibroMayor tablaLibroMayor = new TablaLibroMayor();

            dtLibroMayor = tablaLibroMayor;

        }

        public void LoadedWindow()
        {
            InicializacionBasica();
        }

        private void cbxMonedaExtranjera_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void cbxMonedaExtranjera_Checked(object sender, RoutedEventArgs e)
        {
            cbxMonedaLocalySystema.IsChecked = false;
        }

        private void cbxMonedaSistema_Checked(object sender, RoutedEventArgs e)
        {
            cbxMonedaLocalySystema.IsChecked = false;
        }

        private void cbxMonedaSistema_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void cbxMonedaLocalySystema_Checked(object sender, RoutedEventArgs e)
        {
            cbxMonedaExtranjera.IsChecked = false;
            cbxMonedaSistema.IsChecked = false;
        }

        private void cbxMonedaLocalySystema_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void seleccionado_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void seleccionado_Unchecked(object sender, RoutedEventArgs e)
        {

        }


    }
}
