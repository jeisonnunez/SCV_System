using System;
using System.Collections.Generic;
using System.Data;
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
using Negocio;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para LibroDiario.xaml
    /// </summary>
    public partial class LibroDiario : Window
    {
        ControladorLibroDiario cn = new ControladorLibroDiario();

        DataTable dt = new DataTable();

        public static TablaLibroDiario dtLibroDiario;
        public LibroDiario()
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

        public void LoadedWindow()
        {
            InicializacionBasica();
        }
        private void cbxCuentas_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void cbxCuentas_Unchecked(object sender, RoutedEventArgs e)
        {

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

            dgDiario.ItemsSource = dt.DefaultView;

            dgDiario.CanUserAddRows = false;

            dgDiario.CanUserDeleteRows = false;

            dgDiario.CanUserSortColumns = false;

            dgDiario.IsReadOnly = false;
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


        private void cbxFechaContabilizacion_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void cbxFechaContabilizacion_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void cbxFechaVencimiento_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void cbxFechaVencimiento_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void cbxFechaDocumento_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void cbxFechaDocumento_Unchecked(object sender, RoutedEventArgs e)
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

                    var result = cn.ExecuteLibroDiario(dt, dpHFechaContabilizacion.SelectedDate, cbxMonedaLocalySystema.IsChecked, cbxMonedaSistema.IsChecked, cbxMonedaExtranjera.IsChecked, dpDFechaContabilizacion.SelectedDate);

                    if (result.Item2 == null)
                    {

                        if (result.Item1.Rows.Count >= 1)
                        {
                            dtLibroDiario.ClearDatatable();

                            dtLibroDiario.Show();

                            dtLibroDiario.SetDatePicker(dpDFechaContabilizacion.SelectedDate, dpHFechaContabilizacion.SelectedDate);

                            dtLibroDiario.SetVisibility(cbxMonedaLocalySystema.IsChecked, cbxMonedaSistema.IsChecked, cbxMonedaExtranjera.IsChecked);

                            dtLibroDiario.SetDataTable(result.Item1);
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InicializacionBasica();

            CreateTableLibroDiario();
        }

        private void CreateTableLibroDiario()
        {
            TablaLibroDiario tablaLibroDiario = new TablaLibroDiario();

            dtLibroDiario = tablaLibroDiario;
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
