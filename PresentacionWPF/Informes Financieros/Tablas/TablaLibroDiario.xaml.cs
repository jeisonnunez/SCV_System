using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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
    /// Lógica de interacción para TablaLibroDiario.xaml
    /// </summary>
    public partial class TablaLibroDiario : Converter
    {
        DataTable dt = new DataTable();

        DataTable dtExcel = new DataTable();

       
        public TablaLibroDiario()
        {
            InitializeComponent();
        }

        public void ClearDatatable()
        {
            dt.Rows.Clear();

        }

        public void SetDataTable(DataTable dataTable)
        {
            dt = AddCurrencyCode(dataTable);         

            dgLibroDiario.ItemsSource = dt.DefaultView;

        }
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        public void SetDatePicker(DateTime? dpDesde, DateTime? dpHasta)
        {
            dpFechaDesde.Background = Brushes.White;

            dpFechaHasta.Background = Brushes.White;

            ReestablecerDatetime(dpFechaDesde);

            ReestablecerDatetime(dpFechaDesde);

            ReadOnlyDatetime(dpFechaDesde);

            ReadOnlyDatetime(dpFechaHasta);

            dpFechaDesde.SelectedDate = dpDesde;

            dpFechaHasta.SelectedDate = dpHasta;

            dpFechaDesde.IsEnabled = false;

            dpFechaHasta.IsEnabled = false;
        }

        public void ReadOnlyDatetime(DatePicker dpk)
        {
            TextBox tb = (TextBox)dpk.Template.FindName("PART_TextBox", dpk);

            tb.IsReadOnly = true;
            tb.Background = Brushes.LightGray;

        }

       
        
        public void ReestablecerDatetime(DatePicker dpk)
        {
          
            TextBox tb = (TextBox)dpk.Template.FindName("PART_TextBox", dpk);

            tb.Background = Brushes.White;

        }

        public void SetVisibility(bool? monedaLocalSistema, bool? monedaSistema, bool? monedaExtranjera)
        {
            if (monedaLocalSistema == true)
            {
                debit.Visibility = Visibility.Visible;
                credit.Visibility = Visibility.Visible;
                saldo.Visibility = Visibility.Visible;
                FCDebit.Visibility = Visibility.Hidden;
                FCCredit.Visibility = Visibility.Hidden;
                FCSaldo.Visibility = Visibility.Hidden;
                SYSDeb.Visibility = Visibility.Visible;
                SYSCred.Visibility = Visibility.Visible;
                SYSSaldo.Visibility = Visibility.Visible;
            }

            if (monedaLocalSistema == false)
            {
                debit.Visibility = Visibility.Hidden;
                credit.Visibility = Visibility.Hidden;
                saldo.Visibility = Visibility.Hidden;

            }
            if (monedaSistema == true)
            {
                debit.Visibility = Visibility.Hidden;
                credit.Visibility = Visibility.Hidden;
                saldo.Visibility = Visibility.Hidden;
                SYSDeb.Visibility = Visibility.Visible;
                SYSCred.Visibility = Visibility.Visible;
                SYSSaldo.Visibility = Visibility.Visible;
            }

            if (monedaSistema == false && monedaLocalSistema == false)
            {

                SYSDeb.Visibility = Visibility.Hidden;
                SYSCred.Visibility = Visibility.Hidden;
                SYSSaldo.Visibility = Visibility.Hidden;
            }

            if (monedaExtranjera == true)
            {

                FCDebit.Visibility = Visibility.Visible;
                FCCredit.Visibility = Visibility.Visible;
                FCSaldo.Visibility = Visibility.Visible;

            }

            if (monedaExtranjera == false)
            {

                FCDebit.Visibility = Visibility.Hidden;
                FCCredit.Visibility = Visibility.Hidden;
                FCSaldo.Visibility = Visibility.Hidden;

            }

            if (monedaLocalSistema == false && monedaExtranjera == false && monedaSistema == false)
            {
                debit.Visibility = Visibility.Visible;
                credit.Visibility = Visibility.Visible;
                saldo.Visibility = Visibility.Visible;
                FCDebit.Visibility = Visibility.Hidden;
                FCCredit.Visibility = Visibility.Hidden;
                FCSaldo.Visibility = Visibility.Hidden;
                SYSDeb.Visibility = Visibility.Hidden;
                SYSCred.Visibility = Visibility.Hidden;
                SYSSaldo.Visibility = Visibility.Hidden;
            }
        }

        private DataTable AddCurrencyCode(DataTable dt)
        {

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {

                    if (column.ToString() == "Debit" && Convert.ToDecimal(row["Debit"]) != 0)
                    {
                        row["Debit"] = Properties.Settings.Default.MainCurrency + " " + (ConvertDecimalTwoPlaces(row["Debit"])).ToString("N", nfi);

                    }

                    else if (column.ToString() == "Credit" && Convert.ToDecimal(row["Credit"]) != 0)
                    {
                        row["Credit"] = Properties.Settings.Default.MainCurrency + " " + (ConvertDecimalTwoPlaces(row["Credit"])).ToString("N", nfi);
                    }

                    else if (column.ToString() == "Saldo" && Convert.ToDecimal(row["Saldo"]) != 0)
                    {
                        row["Saldo"] = Properties.Settings.Default.MainCurrency + " " + (ConvertDecimalTwoPlaces(row["Saldo"])).ToString("N", nfi);
                    }

                    else if (column.ToString() == "SYSDeb" && Convert.ToDecimal(row["SYSDeb"]) != 0)
                    {
                        row["SYSDeb"] = Properties.Settings.Default.SysCurrency + " " + (ConvertDecimalTwoPlaces(row["SYSDeb"])).ToString("N", nfi);
                    }

                    else if (column.ToString() == "SYSCred" && Convert.ToDecimal(row["SYSCred"]) != 0)
                    {
                        row["SYSCred"] = Properties.Settings.Default.SysCurrency + " " + (ConvertDecimalTwoPlaces(row["SYSCred"])).ToString("N",nfi);
                    }

                    else if (column.ToString() == "SaldoSYS" && Convert.ToDecimal(row["SaldoSYS"]) != 0)
                    {
                        row["SaldoSYS"] = Properties.Settings.Default.SysCurrency + " " + (ConvertDecimalTwoPlaces(row["SaldoSYS"])).ToString("N", nfi);
                    }

                    else if (column.ToString() == "FCDebit" && Convert.ToDecimal(row["FCDebit"]) != 0)
                    {
                        row["FCDebit"] = row["FCCurrency"] + (ConvertDecimalTwoPlaces(row["FCDebit"])).ToString("N", nfi);
                    }

                    else if (column.ToString() == "FCCredit" && Convert.ToDecimal(row["FCCredit"]) != 0)
                    {
                        row["FCCredit"] = row["FCCurrency"] + (ConvertDecimalTwoPlaces(row["FCCredit"])).ToString("N", nfi);
                    }

                    else if (column.ToString() == "FCSaldo" && Convert.ToDecimal(row["FCSaldo"]) != 0)
                    {
                        row["FCSaldo"] = row["FCCurrency"] + (ConvertDecimalTwoPlaces(row["FCSaldo"])).ToString("N", nfi);
                    }

                    else if (column.ToString() == "Credit" && Convert.ToDecimal(row["Credit"]) == 0)
                    {
                        row["Credit"] = "";
                    }

                    else if (column.ToString() == "SYSDeb" && Convert.ToDecimal(row["SYSDeb"]) == 0)
                    {
                        row["SYSDeb"] = "";
                    }

                    else if (column.ToString() == "SYSCred" && Convert.ToDecimal(row["SYSCred"]) == 0)
                    {
                        row["SYSCred"] = "";
                    }

                    else if (column.ToString() == "FCDebit" && Convert.ToDecimal(row["FCDebit"]) == 0)
                    {
                        row["FCDebit"] = "";
                    }

                    else if (column.ToString() == "FCCredit" && Convert.ToDecimal(row["FCCredit"]) == 0)
                    {
                        row["FCCredit"] = "";
                    }

                    else if (column.ToString() == "Debit" && Convert.ToDecimal(row["Debit"]) == 0)
                    {
                        row["Debit"] = "";

                    }

                    else if (column.ToString() == "Saldo" && Convert.ToDecimal(row["Saldo"]) == 0)
                    {
                        row["Saldo"] = "";
                    }

                    else if (column.ToString() == "FCSaldo" && Convert.ToDecimal(row["FCSaldo"]) == 0)
                    {
                        row["FCSaldo"] = "";
                    }

                    else if (column.ToString() == "SaldoSYS" && Convert.ToDecimal(row["SaldoSYS"]) == 0)
                    {
                        row["SaldoSYS"] = "";

                    }

                    
                }

            }

            return dt;
        }

       

        private void imgExcel_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            dtExcel=RemoveColumn(dt);

            GenerateExcel(dtExcel);

            //workBook.SaveAs(@"C:\Users\jamara\Documents\Addon TXT\Book1.xlsx");


            // workBook.Close();

            //excel.Quit();
        }

       
        private void img_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }

    
}
