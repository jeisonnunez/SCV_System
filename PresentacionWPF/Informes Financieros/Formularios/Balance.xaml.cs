using Negocio;
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

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para Balance.xaml
    /// </summary>
    public partial class Balance : Window
    {
        ControladorBalance cn = new ControladorBalance();

        DataTable dt = new DataTable();

        public static DateTime? dpDesde;

        public static DateTime? dpHasta;

        public static bool? cuentaCero;

        public static bool? monedaLocalySystema;

        public static bool? monedaSystema;

        public static bool? monedaExtranjera;

        public TablaBalance dtBalance;
        public Balance()
        {
            InitializeComponent();
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

        private void comboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            App.comboBox_LostFocus(sender, e);
        }

        private void comboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            App.comboBox_GotFocus(sender, e);
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        private void btnWindow_Close(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {

            App.textBox_GotFocus(sender, e);
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            App.textBox_LostFocus(sender, e);
        }

        private void cbxSN_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void cbxSN_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void cbxCuentas_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void cbxCuentas_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void btnEjecutar_Click(object sender, RoutedEventArgs e)
        {
            switch (btnEjecutar.Content.ToString())
            {
                case "OK":

                    SetFields();

                    var result = cn.ExecuteBalance(dt,dpHFechaContabilizacion.SelectedDate, cbxCuentasCero.IsChecked, cbxMonedaLocalySystema.IsChecked, cbxMonedaSistema.IsChecked, cbxMonedaExtranjera.IsChecked, dpDFechaContabilizacion.SelectedDate);

                    if (result.Item2 == null)
                    {

                        if (result.Item1.Rows.Count >= 1)
                        {                        
                            dtBalance.ClearDatatable();

                            dtBalance.Show();

                            dtBalance.SetDatePicker(dpDFechaContabilizacion.SelectedDate, dpHFechaContabilizacion.SelectedDate);

                            dtBalance.SetVisibility(cbxMonedaLocalySystema.IsChecked,cbxMonedaSistema.IsChecked,cbxMonedaExtranjera.IsChecked);

                            dtBalance.SetDataTable(result.Item1);
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

        private void SetFields()
        {
            dpDesde = dpDFechaContabilizacion.SelectedDate;

            dpHasta = dpHFechaContabilizacion.SelectedDate;

            cuentaCero = cbxCuentasCero.IsChecked;

            monedaLocalySystema = cbxMonedaLocalySystema.IsChecked;

            monedaSystema = cbxMonedaSistema.IsChecked;

            monedaExtranjera = cbxMonedaExtranjera.IsChecked;

        }

        private void CreateTableBalance()
        {
            TablaBalance tablaBalance = new TablaBalance();

            dtBalance = tablaBalance;
        }

       
        private void chkSelectAll_Checked(object sender, RoutedEventArgs e)
        {
            SetChecked();
        }

        private void chkSelectAll_Unchecked(object sender, RoutedEventArgs e)
        {
            SetUnChecked();
        }

        private void seleccionado_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void seleccionado_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InicializacionBasica();

            CreateTableBalance();
        }

        public void InicializacionBasica()
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

        private void GetAccount(DataTable listAccount)
        {
            dt = SetSeleccionado(listAccount);

            dgBalance.ItemsSource = dt.DefaultView;

            dgBalance.CanUserAddRows = false;

            dgBalance.CanUserDeleteRows = false;

            dgBalance.CanUserSortColumns = false;

            dgBalance.IsReadOnly = false;
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

        public void LoadedWindow()
        {
            InicializacionBasica();
        }
    }
}
