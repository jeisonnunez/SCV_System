using Entidades;
using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
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
    /// Lógica de interacción para ImportacionArticulos.xaml
    /// </summary>
    public partial class ImportacionArticulos : System.Windows.Window
    {
        ControladorArticulos cn = new ControladorArticulos();

        System.Data.DataTable dataTable = new System.Data.DataTable();
        public ImportacionArticulos()
        {
            InitializeComponent();
        }

        private void btnImportar_Click(object sender, RoutedEventArgs e)
        {
            int rows = dataTable.Rows.Count;

            int j = 0;

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                List<Entidades.Articulos> listaItem = new List<Entidades.Articulos>();

                DataRow row = dataTable.Rows[i];

                Entidades.Articulos item = new Entidades.Articulos();
                
                item.ItemCode = row["ItemCode"].ToString();
                item.ItemName = row["ItemName"].ToString();
                item.PrchseItem = Convert.ToChar(row["PrchseItem"]);
                item.SellItem = Convert.ToChar(row["SellItem"]);
                item.InvnItem = Convert.ToChar(row["InvnItem"]);
                item.VatLiable = Convert.ToChar(row["VatLiable"]);
                item.EvalSystem = row["EvalSystem"].ToString();
                item.UserSign = Properties.Settings.Default.Usuario;
                item.UpdateDate1 = fechaActual.GetFechaActual();
                item.Deleted = 'Y';
                item.OnHand = 0;
                item.IsCommited = 0;
                item.OnOrders = 0;
                item.StockValue = 0;
                item.UgpEntry = Convert.ToInt32(row["UgpEntry"]);
                item.InvntryUomCode = row["InvntryUomCode"].ToString();
                item.InvntryUomName = row["InvntryUomName"].ToString();
                item.NumInCnt = Convert.ToInt32(row["NumInCnt"]);
                item.InvntryUomEntry =Convert.ToInt32(row["InvntryUomEntry"]);               

                listaItem.Add(item);

                var result = cn.InsertItems(listaItem);

                if (result.Item1 == 1)
                {

                    j++;

                    dataTable.Rows.Remove(row);

                    dataTable.AcceptChanges();

                    i--;

                    dgImportarArticulos.ItemsSource = dataTable.DefaultView;

                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error en la creacion del articulo " + item.ItemCode + ": " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");

                }

            }

            if (j == rows)
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Importacion exitosa de " + rows + " articulos", Brushes.LightGreen, Brushes.Black, "001-interface.png");
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error en la importacion de algun articulo. Debe revisar el log del sistema: ", Brushes.Red, Brushes.White, "003-interface-2.png");
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataTable.Columns.Add("ItemCode");
            dataTable.Columns.Add("ItemName");
            dataTable.Columns.Add("PrchseItem");
            dataTable.Columns.Add("SellItem");
            dataTable.Columns.Add("InvnItem");
            dataTable.Columns.Add("VatLiable");
            dataTable.Columns.Add("EvalSystem");
            dataTable.Columns.Add("UgpEntry");
            dataTable.Columns.Add("InvntryUomCode");
            dataTable.Columns.Add("InvntryUomName");
            dataTable.Columns.Add("NumInCnt");
            dataTable.Columns.Add("InvntryUomEntry");
        }

        private void RemoveDataRow(System.Data.DataTable dataTable)
        {
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];

                if (AreAllColumnsEmpty(row) == true)
                {
                    dataTable.Rows.Remove(row);

                    dataTable.AcceptChanges();

                    i--;

                }

            }

            dgImportarArticulos.ItemsSource = dataTable.DefaultView;


        }

        private bool AreAllColumnsEmpty(DataRow dr)
        {

            foreach (var value in dr.ItemArray)
            {
                if (String.IsNullOrWhiteSpace(value.ToString()) == false)
                {
                    return false;
                }
            }
            return true;

        }

        public void LoadedWindow()
        {
            dataTable.Rows.Clear();

            dgImportarArticulos.ItemsSource = dataTable.DefaultView;
        }

        public System.Data.DataTable formofDataTable(Microsoft.Office.Interop.Excel.Worksheet ws)
        {
            try
            {
                string worksheetName = ws.Name;

                dataTable.Rows.Clear();

                dataTable.AcceptChanges();

                dataTable.TableName = worksheetName;
                Microsoft.Office.Interop.Excel.Range xlRange = ws.UsedRange;
                object[,] valueArray = (object[,])xlRange.get_Value(XlRangeValueDataType.xlRangeValueDefault);


                object[] singleDValue = new object[valueArray.GetLength(1)]; //value array first row contains column names. so loop starts from 2 instead of 1
                for (int i = 2; i <= valueArray.GetLength(0); i++)
                {
                    for (int j = 0; j < valueArray.GetLength(1); j++)
                    {
                        if (valueArray[i, j + 1] != null)
                        {
                            singleDValue[j] = valueArray[i, j + 1].ToString();
                        }
                        else
                        {
                            singleDValue[j] = valueArray[i, j + 1];
                        }
                    }
                    dataTable.LoadDataRow(singleDValue, System.Data.LoadOption.PreserveChanges);
                }

                return dataTable;
            }
            catch (Exception ex)
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error:" + ex.Message, Brushes.Red, Brushes.White, "003-interface-2.png");

                return dataTable;
            }


        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog choofdlog = new OpenFileDialog();
            choofdlog.Filter = "Excel file | *.xls; *.xlsx; *.xls;";
            if (choofdlog.ShowDialog() == true)
            {
                string sFileName = choofdlog.FileName;
                string path = System.IO.Path.GetFullPath(choofdlog.FileName);
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                DataSet ds = new DataSet();
                Microsoft.Office.Interop.Excel.Workbook wb = excel.Workbooks.Open(path);
                foreach (Microsoft.Office.Interop.Excel.Worksheet ws in wb.Worksheets)
                {

                    dataTable = formofDataTable(ws);
                    //ds.Tables.Add(td);//This will give the DataTable from Excel file in Dataset
                }

                RemoveDataRow(dataTable);

                //dgImportarCuentas.ItemsSource = dataTable.DefaultView;
                wb.Close();
            }
        }
    }
}
