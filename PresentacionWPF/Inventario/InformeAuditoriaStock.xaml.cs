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
    /// Lógica de interacción para InformeAuditoriaStock.xaml
    /// </summary>
    public partial class InformeAuditoriaStock : Converter
    {
        DataTable dt = new DataTable();

        ControladorAuditoriaStock cn = new ControladorAuditoriaStock();

        public static TablaAuditoriaStock dtAuditoriaStock;
        public InformeAuditoriaStock()
        {
            InitializeComponent();
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

        private void btnEjecutar_Click(object sender, RoutedEventArgs e)
        {
            switch (btnEjecutar.Content.ToString())
            {
                case "OK":

                    var result = cn.ExecuteAuditoriaStock(dt, TraduceCheckBox(cbxCompras.IsChecked), TraduceCheckBox(cbxVentas.IsChecked),dpHFechaContabilizacion.SelectedDate, dpDFechaContabilizacion.SelectedDate);

                    if (result.Item2 == null)
                    {

                        if (result.Item1.Rows.Count >= 1)
                        {
                            dtAuditoriaStock.ClearDatatable();

                            dtAuditoriaStock.Show();

                            dtAuditoriaStock.SetDatePicker(dpDFechaContabilizacion.SelectedDate, dpHFechaContabilizacion.SelectedDate);                            

                            dtAuditoriaStock.SetDataTable(result.Item1);
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

        private char TraduceCheckBox(bool? isChecked)
        {
            char chec = 'Y';

            if (isChecked == true)
            {
                chec = 'Y';
            }
            else if(isChecked == false) {

                chec = 'N';
            }

            return chec;
        }

        public void InicializacionBasica()
        {
            var result = cn.FindAllItems();

            if (result.Item2 == null)
            {
                GetItems(result.Item1);
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
            }
        }

        private void GetItems(DataTable listItems)
        {
            dt = SetSeleccionado(listItems);

            dgStock.ItemsSource = dt.DefaultView;

            dgStock.CanUserAddRows = false;

            dgStock.CanUserDeleteRows = false;

            dgStock.CanUserSortColumns = false;

            dgStock.IsReadOnly = false;
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

        private void Converter_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        private void CreateTableAuditoriaStock()
        {
            TablaAuditoriaStock auditoriaStock = new TablaAuditoriaStock();

            dtAuditoriaStock = auditoriaStock;
        }

        private void Converter_Loaded(object sender, RoutedEventArgs e)
        {
            InicializacionBasica();

            CreateTableAuditoriaStock();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
