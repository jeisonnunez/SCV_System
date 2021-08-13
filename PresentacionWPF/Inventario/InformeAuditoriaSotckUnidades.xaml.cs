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
    /// Lógica de interacción para InformeAuditoriaSotckUnidades.xaml
    /// </summary>
    public partial class InformeAuditoriaSotckUnidades : Converter
    {
      

        DataTable dt = new DataTable();

        DataTable dtExcel = new DataTable();
        public InformeAuditoriaSotckUnidades()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public void LoadedWindow()
        {

        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void imgExcel_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            dtExcel = RemoveColumn(dt);

            GenerateExcel(dtExcel);
        }

        public void ClearDatatable()
        {
            dt.Rows.Clear();

        }

        public void SetDataTable(DataTable dataTable)
        {
            dt = AddCurrencyCode(dataTable);

            dgInformeStockUnidades.ItemsSource = dt.DefaultView;

        }

        private DataTable AddCurrencyCode(DataTable dt)
        {

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {

                    //if (column.ToString() == "Quantity" && ConvertDecimalTwoPlaces(row["Quantity"]) != 0)
                    //{
                    //    row["Quantity"] = (ConvertDecimalTwoPlaces(row["Quantity"])).ToString("N", nfi);

                    //}

                    //else if (column.ToString() == "CalcPrice" && ConvertDecimalTwoPlaces(row["CalcPrice"]) != 0)
                    //{
                    //    row["CalcPrice"] = (ConvertDecimalTwoPlaces(row["CalcPrice"])).ToString("N", nfi);
                    //}

                    //else if (column.ToString() == "TransValue" && ConvertDecimalTwoPlaces(row["TransValue"]) != 0)
                    //{
                    //    row["TransValue"] = (ConvertDecimalTwoPlaces(row["TransValue"])).ToString("N", nfi);
                    //}

                    //else if (column.ToString() == "Balance" && ConvertDecimalTwoPlaces(row["Balance"]) != 0)
                    //{
                    //    row["Balance"] = (ConvertDecimalTwoPlaces(row["Balance"])).ToString("N", nfi);
                    //}

                    //else if (column.ToString() == "QuantityAcum" && ConvertDecimalTwoPlaces(row["QuantityAcum"]) != 0)
                    //{
                    //    row["QuantityAcum"] = (ConvertDecimalTwoPlaces(row["QuantityAcum"])).ToString("N", nfi);
                    //}

                    //else if (column.ToString() == "Quantity" && (row["Title"].ToString() == "Y" || row["Title"].ToString() == "E"))
                    //{
                    //    row["Quantity"] = "";
                    //}

                    //else if (column.ToString() == "CalcPrice" && (row["Title"].ToString() == "Y" || row["Title"].ToString() == "E"))
                    //{
                    //    row["CalcPrice"] = "";
                    //}

                    //else if (column.ToString() == "TransValue" && (row["Title"].ToString() == "Y" || row["Title"].ToString() == "E"))
                    //{
                    //    row["TransValue"] = "";
                    //}

                    //else if (column.ToString() == "Balance" && row["Title"].ToString() == "Y")
                    //{
                    //    row["Balance"] = "";
                    //}

                    //else if (column.ToString() == "QuantityAcum" && row["Title"].ToString() == "Y")
                    //{
                    //    row["QuantityAcum"] = "";
                    //}


                    //else if (column.ToString() == "Quantity" && ConvertDecimalTwoPlaces(row["Quantity"]) == 0)
                    //{
                    //    row["Quantity"] = 0;
                    //}

                    //else if (column.ToString() == "CalcPrice" && ConvertDecimalTwoPlaces(row["CalcPrice"]) == 0)
                    //{
                    //    row["CalcPrice"] = 0;
                    //}

                    //else if (column.ToString() == "TransValue" && ConvertDecimalTwoPlaces(row["TransValue"]) == 0)
                    //{
                    //    row["TransValue"] = 0;
                    //}

                    //else if (column.ToString() == "Balance" && ConvertDecimalTwoPlaces(row["Balance"]) == 0)
                    //{
                    //    row["Balance"] = 0;
                    //}

                    //else if (column.ToString() == "QuantityAcum" && ConvertDecimalTwoPlaces(row["QuantityAcum"]) == 0)
                    //{
                    //    row["QuantityAcum"] = 0;
                    //}



                }

            }

            return dt;
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
    }
}
