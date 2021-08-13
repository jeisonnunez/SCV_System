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
    /// Lógica de interacción para ImportacionSocioNegocio.xaml
    /// </summary>
    public partial class ImportacionSocioNegocio : System.Windows.Window
    {
        ControladorSocioNegocio cn = new ControladorSocioNegocio();

        System.Data.DataTable dataTable = new System.Data.DataTable();
        public ImportacionSocioNegocio()
        {
            InitializeComponent();
        }

        public void LoadedWindow()
        {
            dataTable.Rows.Clear();

            dgImportarSocioNegocio.ItemsSource = dataTable.DefaultView;
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

            dgImportarSocioNegocio.ItemsSource = dataTable.DefaultView;


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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            dataTable.Columns.Add("CardCode");
            dataTable.Columns.Add("CardName");
            dataTable.Columns.Add("CardType");
            dataTable.Columns.Add("Address");
            dataTable.Columns.Add("ZipCode");
            dataTable.Columns.Add("MailAddres");          
            dataTable.Columns.Add("Phone1");
            dataTable.Columns.Add("Phone2");
            dataTable.Columns.Add("Fax");
            dataTable.Columns.Add("CntctPrsn");
            dataTable.Columns.Add("LicTradNum");
            dataTable.Columns.Add("Currency");
            dataTable.Columns.Add("E_Mail");
            dataTable.Columns.Add("DebPayAcct");
            dataTable.Columns.Add("VatGroup");
            dataTable.Columns.Add("U_IDA_TipoPersona");
            dataTable.Columns.Add("U_IDA_Contribuyente");
            dataTable.Columns.Add("U_IDA_Sucursal");
            dataTable.Columns.Add("U_IDA_AplicaITF");
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

        private void btnImportar_Click(object sender, RoutedEventArgs e)
        {
            int rows = dataTable.Rows.Count;

            int j = 0;

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                List<SocioNegocio> listaSocioNegocio = new List<SocioNegocio>();

                DataRow row = dataTable.Rows[i];

                SocioNegocio socioNegocio = new SocioNegocio();
               
                socioNegocio.CardCode = row["CardCode"].ToString();
                socioNegocio.CardName = row["CardName"].ToString();
                socioNegocio.CardType =Convert.ToChar(row["CardType"]);
                socioNegocio.LicTradNum = row["LicTradNum"].ToString();
                socioNegocio.Currency = row["Currency"].ToString();
                socioNegocio.TipoPersona = row["TipoPersona"].ToString();
                socioNegocio.Contribuyente = Convert.ToChar(row["Contribuyente"]);
                socioNegocio.Sucursal = Convert.ToChar(row["Sucursal"]);
                socioNegocio.AplicaITF = Convert.ToChar(row["AplicaITF"]);
                socioNegocio.Phone1 = row["Phone1"].ToString();
                socioNegocio.Phone2 = row["Phone2"].ToString();
                socioNegocio.Fax = row["Fax"].ToString();
                socioNegocio.E_mail = row["E_Mail"].ToString();
                socioNegocio.MailAddress = row["MailAddres"].ToString();
                socioNegocio.CntctPrsn = row["CntctPrsn"].ToString();
                socioNegocio.Address =row["Address"].ToString();
                socioNegocio.ZipCode = row["ZipCode"].ToString();
                socioNegocio.DebPayAcct = row["DebPayAcct"].ToString();
                socioNegocio.VatGroup = row["VatGroup"].ToString();
                socioNegocio.UserSign = Vista.Properties.Settings.Default.Usuario;
                socioNegocio.UpdateDate = fechaActual.GetFechaActual();
                socioNegocio.Deleted = 'Y';
                socioNegocio.Balance = 0;
                socioNegocio.BalanceFC = 0;
                socioNegocio.BalanceSys = 0;
                socioNegocio.DNoteBalFC = 0;
                socioNegocio.DNoteBalSy = 0;
                socioNegocio.DNotesBal = 0;

                listaSocioNegocio.Add(socioNegocio);

                var result = cn.InsertBP(listaSocioNegocio);

                if (result.Item1 == 1)
                {

                    j++;

                    dataTable.Rows.Remove(row);

                    dataTable.AcceptChanges();

                    i--;

                    dgImportarSocioNegocio.ItemsSource = dataTable.DefaultView;

                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error en la creacion del socio de negocio " + socioNegocio.CardCode + ": " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");

                }

            }

            if (j == rows)
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Importacion exitosa de " + rows + " socios de negocio", Brushes.LightGreen, Brushes.Black, "001-interface.png");
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error en la importacion de algun socio de negocio. Debe revisar el log del sistema: ", Brushes.Red, Brushes.White, "003-interface-2.png");
            }

        }
    }
}
