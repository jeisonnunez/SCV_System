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
    /// Lógica de interacción para TablaAuditoriaStock.xaml
    /// </summary>
    public partial class TablaAuditoriaStock : Converter
    {
        DataTable dt = new DataTable();

        DataTable dtExcel = new DataTable();

        private InformeAuditoriaSotckUnidades objectAuditoriaStockUnidades;

        ControladorAuditoriaStock cn = new ControladorAuditoriaStock();

        public TablaAuditoriaStock()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        public void ClearDatatable()
        {
            dt.Rows.Clear();

        }

        public void SetDataTable(DataTable dataTable)
        {
            dt = AddCurrencyCode(dataTable);    

            dgInformeStock.ItemsSource = dt.DefaultView;

        }

        private DataTable AddCurrencyCode(DataTable dt)
        {

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {

                    if (column.ToString() == "Quantity" && ConvertDecimalTwoPlaces(row["Quantity"]) != 0)
                    {
                        row["Quantity"] = (ConvertDecimalTwoPlaces(row["Quantity"])).ToString("N", nfi);

                    }

                    else if (column.ToString() == "InQty" && ConvertDecimalTwoPlaces(row["InQty"]) != 0)
                    {
                        row["InQty"] = (ConvertDecimalTwoPlaces(row["InQty"])).ToString("N", nfi);

                    }

                    else if (column.ToString() == "OutQty" && ConvertDecimalTwoPlaces(row["OutQty"]) != 0)
                    {
                        row["OutQty"] = (ConvertDecimalTwoPlaces(row["OutQty"])).ToString("N", nfi);

                    }

                    else if (column.ToString() == "InValue" && ConvertDecimalTwoPlaces(row["InValue"]) != 0)
                    {
                        row["InValue"] = (ConvertDecimalTwoPlaces(row["InValue"])).ToString("N", nfi);

                    }

                    else if (column.ToString() == "OutValue" && ConvertDecimalTwoPlaces(row["OutValue"]) != 0)
                    {
                        row["OutValue"] = (ConvertDecimalTwoPlaces(row["OutValue"])).ToString("N", nfi);

                    }

                    else if (column.ToString() == "CalcPrice" && ConvertDecimalTwoPlaces(row["CalcPrice"]) != 0)
                    {
                        row["CalcPrice"] = (ConvertDecimalTwoPlaces(row["CalcPrice"])).ToString("N", nfi);
                    }

                    else if (column.ToString() == "TransValue" && ConvertDecimalTwoPlaces(row["TransValue"]) != 0)
                    {
                        row["TransValue"] = (ConvertDecimalTwoPlaces(row["TransValue"])).ToString("N", nfi);
                    }

                    else if (column.ToString() == "Balance" && ConvertDecimalTwoPlaces(row["Balance"]) != 0)
                    {
                        row["Balance"] = (ConvertDecimalTwoPlaces(row["Balance"])).ToString("N", nfi);
                    }

                    else if (column.ToString() == "QuantityAcum" && ConvertDecimalTwoPlaces(row["QuantityAcum"]) != 0)
                    {
                        row["QuantityAcum"] = (ConvertDecimalTwoPlaces(row["QuantityAcum"])).ToString("N", nfi);
                    }

                    else if (column.ToString() == "InValueAcum" && ConvertDecimalTwoPlaces(row["InValueAcum"]) != 0)
                    {
                        row["InValueAcum"] = (ConvertDecimalTwoPlaces(row["InValueAcum"])).ToString("N", nfi);
                    }

                    else if (column.ToString() == "OutValueAcum" && ConvertDecimalTwoPlaces(row["OutValueAcum"]) != 0)
                    {
                        row["OutValueAcum"] = (ConvertDecimalTwoPlaces(row["OutValueAcum"])).ToString("N", nfi);
                    }

                    else if (column.ToString() == "OutQtyAcum" && ConvertDecimalTwoPlaces(row["OutQtyAcum"]) != 0)
                    {
                        row["OutQtyAcum"] = (ConvertDecimalTwoPlaces(row["OutQtyAcum"])).ToString("N", nfi);
                    }

                    else if (column.ToString() == "InQtyAcum" && ConvertDecimalTwoPlaces(row["InQtyAcum"]) != 0)
                    {
                        row["InQtyAcum"] = (ConvertDecimalTwoPlaces(row["InQtyAcum"])).ToString("N", nfi);
                    }


                    else if (column.ToString() == "Quantity" && (row["Title"].ToString() == "Y" || row["Title"].ToString() == "E") )
                    {
                        row["Quantity"] = "";
                    }

                    else if (column.ToString() == "InQty" && (row["Title"].ToString() == "Y" || row["Title"].ToString() == "E"))
                    {
                        row["InQty"] = "";

                    }

                    else if (column.ToString() == "OutQty" && (row["Title"].ToString() == "Y" || row["Title"].ToString() == "E"))
                    {
                        row["OutQty"] = "";

                    }

                    else if (column.ToString() == "InValue" && (row["Title"].ToString() == "Y" || row["Title"].ToString() == "E"))
                    {
                        row["InValue"] = "";

                    }

                    else if (column.ToString() == "OutValue" && (row["Title"].ToString() == "Y" || row["Title"].ToString() == "E"))
                    {
                        row["OutValue"] = "";

                    }

                    else if (column.ToString() == "CalcPrice" && (row["Title"].ToString() == "Y" || row["Title"].ToString() == "E"))
                    {
                        row["CalcPrice"] = "";
                    }

                    else if (column.ToString() == "TransValue" && (row["Title"].ToString() == "Y" || row["Title"].ToString() == "E"))
                    {
                        row["TransValue"] = "";
                    }

                    else if (column.ToString() == "Balance" && row["Title"].ToString() == "Y" )
                    {
                        row["Balance"] = "";
                    }

                    else if (column.ToString() == "QuantityAcum" && row["Title"].ToString() == "Y")
                    {
                        row["QuantityAcum"] = "";
                    }

                    else if (column.ToString() == "InValueAcum" && row["Title"].ToString() == "Y")
                    {
                        row["InValueAcum"] = "";
                    }

                    else if (column.ToString() == "OutValueAcum" && row["Title"].ToString() == "Y")
                    {
                        row["OutValueAcum"] = "";
                    }

                    else if (column.ToString() == "OutQtyAcum" && row["Title"].ToString() == "Y")
                    {
                        row["OutQtyAcum"] = "";
                    }

                    else if (column.ToString() == "InQtyAcum" && row["Title"].ToString() == "Y")
                    {
                        row["InQtyAcum"] = "";
                    }


                    else if (column.ToString() == "Quantity" && ConvertDecimalTwoPlaces(row["Quantity"]) == 0)
                    {
                        row["Quantity"] = 0;
                    }

                    else if (column.ToString() == "CalcPrice" && ConvertDecimalTwoPlaces(row["CalcPrice"]) == 0)
                    {
                        row["CalcPrice"] = 0;
                    }

                    else if (column.ToString() == "TransValue" && ConvertDecimalTwoPlaces(row["TransValue"]) == 0)
                    {
                        row["TransValue"] = 0;
                    }

                    else if (column.ToString() == "Balance" && ConvertDecimalTwoPlaces(row["Balance"]) == 0)
                    {
                        row["Balance"] = 0;
                    }

                    else if (column.ToString() == "QuantityAcum" && ConvertDecimalTwoPlaces(row["QuantityAcum"]) == 0)
                    {
                        row["QuantityAcum"] = 0;
                    }

                    else if (column.ToString() == "InValueAcum" && ConvertDecimalTwoPlaces(row["InValueAcum"]) == 0)
                    {
                        row["InValueAcum"] = "";
                    }

                    else if (column.ToString() == "OutValueAcum" && ConvertDecimalTwoPlaces(row["OutValueAcum"]) == 0)
                    {
                        row["OutValueAcum"] = "";
                    }

                    else if (column.ToString() == "OutQtyAcum" && ConvertDecimalTwoPlaces(row["OutQtyAcum"]) == 0)
                    {
                        row["OutQtyAcum"] = "";
                    }

                    else if (column.ToString() == "InQtyAcum" && ConvertDecimalTwoPlaces(row["InQtyAcum"]) == 0)
                    {
                        row["InQtyAcum"] = "";
                    }

                    else if (column.ToString() == "InQty" && ConvertDecimalTwoPlaces(row["InQty"]) == 0)
                    {
                        row["InQty"] = "";

                    }

                    else if (column.ToString() == "OutQty" &&  ConvertDecimalTwoPlaces(row["OutQty"]) == 0)
                    {
                        row["OutQty"] = "";

                    }

                    else if (column.ToString() == "InValue" && ConvertDecimalTwoPlaces(row["InValue"]) == 0)
                    {
                        row["InValue"] = "";

                    }

                    else if (column.ToString() == "OutValue" && ConvertDecimalTwoPlaces(row["OutValue"]) == 0)
                    {
                        row["OutValue"] = "";

                    }
                }

            }

            return dt;
        }

        private void img_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

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

       

        private void imgExcel_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            dtExcel = RemoveColumn(dt);

            GenerateExcel(dtExcel);

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void btnStockUnidadesMedida_Click(object sender, RoutedEventArgs e)
        {
            DataRow[] tableResult = DataRowResult(dt);

            int rows=tableResult.Count();

            if (rows > 0)//cuenta los registros donde la fila sea de titulo.
            {
                var result = cn.ExecuteAuditoriaStockUnidades(tableResult);

                if (result.Item2 == null)
                {
                    if (result.Item1.Rows.Count >= 1)
                    {
                        objectAuditoriaStockUnidades.ClearDatatable();

                        objectAuditoriaStockUnidades.Show();

                        objectAuditoriaStockUnidades.LoadedWindow();

                        objectAuditoriaStockUnidades.SetDatePicker(dpFechaDesde.SelectedDate, dpFechaHasta.SelectedDate);

                        objectAuditoriaStockUnidades.SetDataTable(result.Item1);
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

            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se encontraron resultados. Cantidad de registros igual a cero: ", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");
            }


        }

        private DataRow[] DataRowResult(DataTable dt)
        {
            DataTable newDt = new DataTable();

            newDt = dt.Copy();

            string[] ColumnsToBeDeleted = { "DocDate", "TransType", "BASE_REF", "CalcPrice", "Quantity", "TransValue",

            "Num","InvntryUomCode","InvntryUomEntry","UomCode","UomCode2"       
            };


            foreach (string ColName in ColumnsToBeDeleted)
            {
                if (newDt.Columns.Contains(ColName))
                    newDt.Columns.Remove(ColName);
            }

            newDt.AcceptChanges();

            DataRow[] result= newDt.Select("Title = 'Y'");

            return result;
        }

        private void Converter_Loaded(object sender, RoutedEventArgs e)
        {
            CreateTableAuditoriaStockUnidades();
        }

        private void CreateTableAuditoriaStockUnidades()
        {
            InformeAuditoriaSotckUnidades sotckUnidades = new InformeAuditoriaSotckUnidades();

            objectAuditoriaStockUnidades = sotckUnidades;
        }

        private void cbxOcultarDetalle_Checked(object sender, RoutedEventArgs e)
        {
            DataView dv = dgInformeStock.ItemsSource as DataView;

            FiltraAuditoriaStock(dv);
        }

        private void cbxOcultarDetalle_Unchecked(object sender, RoutedEventArgs e)
        {
            DataView dv = dgInformeStock.ItemsSource as DataView;

            FiltraAuditoriaStockUnchecked(dv);
        }

        private void FiltraAuditoriaStockUnchecked(DataView dv)
        {
            if (dv != null)
            {
                dv.RowFilter = "Title = " + "'Y'";
            }
        }

        private void FiltraAuditoriaStock(DataView dv)
        {
           
            if (dv != null)
            {
                dv.RowFilter = "Title = " + "'Y'" + " OR " + "Title = " + "'N'" + " OR " + "Title = " + "'E'";
            }
            
        }
    }
}
