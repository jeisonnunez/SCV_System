using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para TablaLibroMayor.xaml
    /// </summary>
    public partial class TablaLibroMayor : Converter
    {     

        DataTable dt = new DataTable();

        DataTable dtExcel = new DataTable();

        public decimal FCSaldoAcumDecimal;

        public decimal FCSaldoDecimal;

        public decimal FCDebitDecimal;

        public decimal FCCreditDecimal;

        public decimal DebitDecimal;

        public decimal CreditDecimal;

        public decimal SYSDebitDecimal;

        public decimal SYSCreditDecimal;

        public decimal SaldoSYSDecimal;

        public decimal SaldoDecimal;

        public decimal SaldoDecimalAcum;

        public decimal SaldoSYSAcumDecimal;

        CheckBox chk;

        List<CheckBox> checkBoxList = new List<CheckBox>();
        public TablaLibroMayor()
        {
            InitializeComponent();

            NumericConfiguration.SetNumericConfiguration();
        }

        public void ClearDatatable()
        {
            dt.Rows.Clear();

        }

        public void SetDataTable(DataTable dataTable)
        {
            dt = AddCurrencyCode(dataTable);

            Prueba();           

            dgLibroMayor.ItemsSource = dt.DefaultView;        

        }

       

        private void imgExcel_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            dtExcel = RemoveColumn(dt);

            GenerateExcel(dtExcel);

            //workBook.SaveAs(@"C:\Users\jamara\Documents\Addon TXT\Book1.xlsx");


            // workBook.Close();

            //excel.Quit();
        }

        public void Prueba()
        {
            foreach (DataGridColumn item in dgLibroMayor.Columns)
            {
               
                    //chk = new CheckBox();
                    //checkBoxList.Add(chk);
                    //wrapColumns.Children.Add(chk);
                    //chk.Width = 100;
                    //chk.Height = 22;
                    //chk.Content = item.Header;
                    //chk.IsChecked = true;
                    //chk.Checked += new RoutedEventHandler(chk_Checked);
                    //chk.Unchecked += new RoutedEventHandler(chk_Unchecked);
               
            }
        }


        void chk_Unchecked(object sender, RoutedEventArgs e)
        {
            List<string> chkUnchekList = new List<string>();

            chkUnchekList.Clear();

            foreach (CheckBox item in checkBoxList)
            {
                if (item.IsChecked == false)
                {
                    chkUnchekList.Add(item.Content.ToString());
                }
            }

            foreach (DataGridColumn item in dgLibroMayor.Columns)
            {
                //if (item.Visibility == Visibility.Visible)
                //{
                if (chkUnchekList.Contains(item.Header.ToString()))
                {
                    dgLibroMayor.Columns.Remove(item);
                    break;
                }

                //}
            }
        }

        void chk_Checked(object sender, RoutedEventArgs e)
        {
            // Prueba();

            List<string> chkCheckList = new List<string>();
            chkCheckList.Clear();

            foreach (CheckBox item in checkBoxList)
            {
                if (item.IsChecked == false)
                {
                    chkCheckList.Add(item.Content.ToString());
                }
            }

            dgLibroMayor.ItemsSource = null;
            dgLibroMayor.ItemsSource = dt.DefaultView;

            foreach (string item in chkCheckList)
            {
                foreach (DataGridColumn column in dgLibroMayor.Columns)
                {
                    //    if (column.Visibility == Visibility.Visible)
                    //    {
                    if (column.Header.ToString() == item)
                    {
                        dgLibroMayor.Columns.Remove(column);
                        break;
                    }

                    //}
                }
            }
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


        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        public void SetVisibility(bool? monedaLocalSistema, bool? monedaSistema, bool? monedaExtranjera)
        {
            if (monedaLocalSistema == true)
            {
                debit.Visibility = Visibility.Visible;
                credit.Visibility = Visibility.Visible;
                saldo.Visibility = Visibility.Visible;
                saldoAcum.Visibility = Visibility.Visible;
                FCDebit.Visibility = Visibility.Hidden;
                FCCredit.Visibility = Visibility.Hidden;
                FCSaldo.Visibility = Visibility.Hidden;
                FCSaldoAcum.Visibility = Visibility.Hidden;
                SYSDeb.Visibility = Visibility.Visible;
                SYSCred.Visibility = Visibility.Visible;
                SYSSaldo.Visibility = Visibility.Visible;
                SaldoSYSAcoum.Visibility = Visibility.Visible;
            }

            if (monedaLocalSistema == false)
            {
                debit.Visibility = Visibility.Hidden;
                credit.Visibility = Visibility.Hidden;
                saldo.Visibility = Visibility.Hidden;
                saldoAcum.Visibility = Visibility.Hidden;

            }
            if (monedaSistema == true)
            {
                debit.Visibility = Visibility.Hidden;
                credit.Visibility = Visibility.Hidden;
                saldo.Visibility = Visibility.Hidden;
                saldoAcum.Visibility = Visibility.Hidden;
                SYSDeb.Visibility = Visibility.Visible;
                SYSCred.Visibility = Visibility.Visible;
                SYSSaldo.Visibility = Visibility.Visible;
                SaldoSYSAcoum.Visibility = Visibility.Visible;
            }

            if (monedaSistema == false && monedaLocalSistema == false)
            {

                SYSDeb.Visibility = Visibility.Hidden;
                SYSCred.Visibility = Visibility.Hidden;
                SYSSaldo.Visibility = Visibility.Hidden;
                SaldoSYSAcoum.Visibility = Visibility.Hidden;
            }

            if (monedaExtranjera == true)
            {

                FCDebit.Visibility = Visibility.Visible;
                FCCredit.Visibility = Visibility.Visible;
                FCSaldo.Visibility = Visibility.Visible;
                FCSaldoAcum.Visibility = Visibility.Visible;

            }

            if (monedaExtranjera == false)
            {

                FCDebit.Visibility = Visibility.Hidden;
                FCCredit.Visibility = Visibility.Hidden;
                FCSaldo.Visibility = Visibility.Hidden;
                FCSaldoAcum.Visibility = Visibility.Hidden;

            }

            if (monedaLocalSistema == false && monedaExtranjera == false && monedaSistema == false)
            {
                debit.Visibility = Visibility.Visible;
                credit.Visibility = Visibility.Visible;
                saldo.Visibility = Visibility.Visible;
                SYSSaldo.Visibility = Visibility.Visible;
                FCDebit.Visibility = Visibility.Hidden;
                FCCredit.Visibility = Visibility.Hidden;
                FCSaldo.Visibility = Visibility.Hidden;
                FCSaldoAcum.Visibility = Visibility.Hidden;
                SYSDeb.Visibility = Visibility.Hidden;
                SYSCred.Visibility = Visibility.Hidden;
                SYSSaldo.Visibility = Visibility.Hidden;
                SaldoSYSAcoum.Visibility = Visibility.Hidden;
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
                        row["Debit"] = Properties.Settings.Default.MainCurrency + " " + (ConvertDecimalTwoPlaces(row["Debit"])).ToString("N",nfi);

                    }

                    else if (column.ToString() == "Credit" && Convert.ToDecimal(row["Credit"]) != 0)
                    {
                        
                        row["Credit"] = Properties.Settings.Default.MainCurrency + " " + (ConvertDecimalTwoPlaces(row["Credit"])).ToString("N", nfi);
                    }

                    else if (column.ToString() == "Saldo" && Convert.ToDecimal(row["Saldo"]) != 0)
                    {                       

                        row["Saldo"] = Properties.Settings.Default.MainCurrency + " " + (ConvertDecimalTwoPlaces(row["Saldo"])).ToString("N", nfi);
                    }                

                    else if (column.ToString() == "SaldoAcum" && Convert.ToDecimal(row["SaldoAcum"]) != 0)
                    {
                        

                        row["SaldoAcum"] = Properties.Settings.Default.MainCurrency + " " + (ConvertDecimalTwoPlaces(row["SaldoAcum"])).ToString("N", nfi);


                    }

                    else if (column.ToString() == "SYSDeb" && Convert.ToDecimal(row["SYSDeb"]) != 0)
                    {
                        
                        row["SYSDeb"] = Properties.Settings.Default.SysCurrency + " " + (ConvertDecimalTwoPlaces(row["SYSDeb"])).ToString("N", nfi);
                    }

                    else if (column.ToString() == "SYSCred" && Convert.ToDecimal(row["SYSCred"]) != 0)
                    {                       

                        row["SYSCred"] = Properties.Settings.Default.SysCurrency + " " + (ConvertDecimalTwoPlaces(row["SYSCred"])).ToString("N", nfi);
                    }

                    else if (column.ToString() == "SaldoSYS" && Convert.ToDecimal(row["SaldoSYS"]) != 0)
                    {
                       

                        row["SaldoSYS"] = Properties.Settings.Default.SysCurrency + " " + (ConvertDecimalTwoPlaces(row["SaldoSYS"])).ToString("N", nfi);
                    }

                    else if (column.ToString() == "SaldoSYSAcum" && Convert.ToDecimal(row["SaldoSYSAcum"]) != 0)
                    {                       

                        row["SaldoSYSAcum"] = Properties.Settings.Default.SysCurrency + " " + (ConvertDecimalTwoPlaces(row["SaldoSYSAcum"])).ToString("N", nfi);
                    }

                    else if (column.ToString() == "FCDebit" && Convert.ToDecimal(row["FCDebit"]) != 0)
                    {
                       
                        row["FCDebit"] = row["FCCurrency"].ToString() + " " + (ConvertDecimalTwoPlaces(row["FCDebit"])).ToString("N", nfi);
                    }

                    else if (column.ToString() == "FCSaldo" && Convert.ToDecimal(row["FCSaldo"]) != 0)
                    {
                        
                        row["FCSaldo"] = row["FCCurrency"].ToString() + " " + (ConvertDecimalTwoPlaces(row["FCSaldo"])).ToString("N", nfi);
                    }

                    else if (column.ToString() == "FCSaldoAcum" && Convert.ToDecimal(row["FCSaldoAcum"]) != 0)
                    {                       

                        row["FCSaldoAcum"] = row["FCCurrency"].ToString() + " " + (ConvertDecimalTwoPlaces(row["FCSaldoAcum"])).ToString("N", nfi);
                    }

                    else if (column.ToString() == "FCCredit" && Convert.ToDecimal(row["FCCredit"]) != 0)
                    {                        

                        row["FCCredit"] = row["FCCurrency"].ToString() + " " + (ConvertDecimalTwoPlaces(row["FCCredit"])).ToString("N", nfi);
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

                    else if (column.ToString() == "SaldoAcum" && Convert.ToDecimal(row["SaldoAcum"]) == 0)
                    {
                        row["SaldoAcum"] = "";

                    }


                    else if (column.ToString() == "FCSaldoAcum" && Convert.ToDecimal(row["FCSaldoAcum"]) == 0)
                    {
                        row["FCSaldoAcum"] = "";

                    }


                    else if (column.ToString() == "SaldoSYSAcum" && Convert.ToDecimal(row["SaldoSYSAcum"]) == 0)
                    {
                        row["SaldoSYSAcum"] = "";

                    }
                }

            }

            return dt;
        }

        private void img_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

       
    }

   
}
