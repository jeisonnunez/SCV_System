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

namespace Vista.Importaciones
{
    /// <summary>
    /// Lógica de interacción para ImportacionPlanCuentas.xaml
    /// </summary>
    public partial class ImportacionPlanCuentas : System.Windows.Window
    {
        System.Data.DataTable dataTable = new System.Data.DataTable();

        ControladorPlanCuentas cn = new ControladorPlanCuentas();
        public ImportacionPlanCuentas()
        {
            InitializeComponent();
        }

        public void LoadedWindow()
        {
            dataTable.Rows.Clear();
          
            dgImportarCuentas.ItemsSource = dataTable.DefaultView;
        }

        private void btnImportar_Click(object sender, RoutedEventArgs e)
        {
            int rows = dataTable.Rows.Count;

            int j = 0;

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                List<Cuenta> listaCuenta = new List<Cuenta>();

                DataRow row = dataTable.Rows[i];

                Cuenta cuenta = new Cuenta();

                cuenta.AcctCode = row["AcctCode"].ToString();
                cuenta.AcctName = row["AcctName"].ToString();
                cuenta.ActCurr = row["ActCurr"].ToString();
                cuenta.LocManTran =Convert.ToChar(row["LocManTran"]);
                cuenta.UserSign = Properties.Settings.Default.Usuario;
                cuenta.ActType = Convert.ToChar(row["ActType"]);
                cuenta.CreateDate = fechaActual.GetFechaActual();
                cuenta.FatherNum = row["FatherNum"].ToString();
                cuenta.CurrTotal = 0;
                cuenta.Finanse = 'N';
                cuenta.Levels = Convert.ToInt32(row["Levels"]);
                cuenta.SysTotal = 0;
                cuenta.FcTotal = 0;
                cuenta.Advance = 'N';
                cuenta.GroupMask = Convert.ToInt32(row["GroupMask"]);
                cuenta.Postable = Convert.ToChar(row["Postable"]);

                listaCuenta.Add(cuenta);

                var result = cn.InsertAccount(listaCuenta);

                if (result.Item1 == 1)
                {

                    j++;

                    dataTable.Rows.Remove(row);

                    dataTable.AcceptChanges();

                    i--;

                    dgImportarCuentas.ItemsSource = dataTable.DefaultView;

                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error en la creacion de la cuenta " + cuenta.AcctCode + ": " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");

                }

            }

            if (j==rows)
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Importacion exitosa de " + rows + " cuentas", Brushes.LightGreen, Brushes.Black, "001-interface.png");
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error en la importacion de alguna cuenta. Debe revisar el log del sistema: ", Brushes.Red, Brushes.White, "003-interface-2.png");
            }

            
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

            dgImportarCuentas.ItemsSource = dataTable.DefaultView;            


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
            catch(Exception ex)
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error:" + ex.Message, Brushes.Red, Brushes.White, "003-interface-2.png");

                return dataTable;
            }
            
            
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {


            dataTable.Columns.Add("AcctCode");
            dataTable.Columns.Add("AcctName");
            dataTable.Columns.Add("Levels");
            dataTable.Columns.Add("FatherNum");
            dataTable.Columns.Add("Postable");
            dataTable.Columns.Add("ActType");
            dataTable.Columns.Add("ActCurr");
            dataTable.Columns.Add("LocManTran");
            dataTable.Columns.Add("GroupMask");



        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        private  void btnBuscar_Click(object sender, RoutedEventArgs e)
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
