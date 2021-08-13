using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
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
    /// Lógica de interacción para ImportacionFacturaProveedores.xaml
    /// </summary>
    public partial class ImportacionFacturaProveedores : Document
    {
        System.Data.DataTable dtJournalEntry = new System.Data.DataTable();

        System.Data.DataTable dtPurchaseInvoiceLines = new System.Data.DataTable();

        System.Data.DataTable dtPurchaseInvoice= new System.Data.DataTable();
        public ImportacionFacturaProveedores()
        {
            InitializeComponent();
        }

        private void LoadDatatableJournalEntry()
        {
            dtJournalEntry.Columns.Add("ShortName");
            dtJournalEntry.Columns.Add("AcctName");
            dtJournalEntry.Columns.Add("ContraAct");
            dtJournalEntry.Columns.Add("Account");
            dtJournalEntry.Columns.Add("FCDebit");
            dtJournalEntry.Columns.Add("FCCredit");
            dtJournalEntry.Columns.Add("Debit");
            dtJournalEntry.Columns.Add("Credit");
            dtJournalEntry.Columns.Add("SYSDeb");
            dtJournalEntry.Columns.Add("SYSCred");
            dtJournalEntry.Columns.Add("LineMemo");
        }

        private void LoadColumnDgFacturaProveedoresCabecera()
        {
            dtPurchaseInvoice.Columns.Add("DocNum");
            dtPurchaseInvoice.Columns.Add("DocDate");
            dtPurchaseInvoice.Columns.Add("TaxDate");
            dtPurchaseInvoice.Columns.Add("DocDueDate");
            dtPurchaseInvoice.Columns.Add("NumAtCard");
            dtPurchaseInvoice.Columns.Add("NumControl");
            dtPurchaseInvoice.Columns.Add("TipoTrans");
            dtPurchaseInvoice.Columns.Add("Comments");
            dtPurchaseInvoice.Columns.Add("DocType");
            dtPurchaseInvoice.Columns.Add("DocStatus");
            dtPurchaseInvoice.Columns.Add("Address2");
            dtPurchaseInvoice.Columns.Add("InvntSttus");
            dtPurchaseInvoice.Columns.Add("VatSum");
            dtPurchaseInvoice.Columns.Add("VatSumFC");
            dtPurchaseInvoice.Columns.Add("VatSumSy");
            dtPurchaseInvoice.Columns.Add("DocCurr");
            dtPurchaseInvoice.Columns.Add("CardCode");
            dtPurchaseInvoice.Columns.Add("CardName");
            dtPurchaseInvoice.Columns.Add("JrnlMemo");
            dtPurchaseInvoice.Columns.Add("LicTradNum");         
            dtPurchaseInvoice.Columns.Add("CtlAccount");
            dtPurchaseInvoice.Columns.Add("DocSubType");
           
        }

        private void LoadColumnDgFacturaProveedoresDetalle()
        {
            dtPurchaseInvoiceLines.Columns.Add("ItemCode");
            dtPurchaseInvoiceLines.Columns.Add("Dscription");
            dtPurchaseInvoiceLines.Columns.Add("Price");
            dtPurchaseInvoiceLines.Columns.Add("Quantity", typeof(int));
            dtPurchaseInvoiceLines.Columns.Add("LineStatus");
            dtPurchaseInvoiceLines.Columns.Add("Currency");
            dtPurchaseInvoiceLines.Columns.Add("Rate");
            dtPurchaseInvoiceLines.Columns.Add("VatGroup");
            dtPurchaseInvoiceLines.Columns.Add("WtLiable");
            dtPurchaseInvoiceLines.Columns.Add("LineTotal");
            dtPurchaseInvoiceLines.Columns.Add("AcctCode");
            dtPurchaseInvoiceLines.Columns.Add("VatPrcnt");
            dtPurchaseInvoiceLines.Columns.Add("VatSum");
            dtPurchaseInvoiceLines.Columns.Add("VatSumFrgn");
            dtPurchaseInvoiceLines.Columns.Add("VatSumSy");
            dtPurchaseInvoiceLines.Columns.Add("TotalSumSy");
            dtPurchaseInvoiceLines.Columns.Add("GTotal");
            dtPurchaseInvoiceLines.Columns.Add("GTotalFC");
            dtPurchaseInvoiceLines.Columns.Add("GTotalSC");
            dtPurchaseInvoiceLines.Columns.Add("TotalFrgn");
            dtPurchaseInvoiceLines.Columns.Add("DocDate");    
            dtPurchaseInvoiceLines.Columns.Add("Address");           
            dtPurchaseInvoiceLines.Columns.Add("InvQty");
            dtPurchaseInvoiceLines.Columns.Add("OpenQty");
            dtPurchaseInvoiceLines.Columns.Add("OpenInvQty");
            dtPurchaseInvoiceLines.Columns.Add("BaseOpnQty");
            dtPurchaseInvoiceLines.Columns.Add("AcctName");
            dtPurchaseInvoiceLines.Columns.Add("UgpEntry");
            dtPurchaseInvoiceLines.Columns.Add("NumPerMsr");
            dtPurchaseInvoiceLines.Columns.Add("NumPerMsr2");
            dtPurchaseInvoiceLines.Columns.Add("UomCode");
            dtPurchaseInvoiceLines.Columns.Add("UomCode2");
            dtPurchaseInvoiceLines.Columns.Add("UomEntry");
            dtPurchaseInvoiceLines.Columns.Add("UomEntry2");
            dtPurchaseInvoiceLines.Columns.Add("unitMsr");
            dtPurchaseInvoiceLines.Columns.Add("unitMsr2");            
            dtPurchaseInvoiceLines.Columns.Add("StartValue");

           
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

                    dtPurchaseInvoice = formofDataTable(ws);
                    //ds.Tables.Add(td);//This will give the DataTable from Excel file in Dataset
                }

                RemoveDataRowFacturaProveedoresCabecera(dtPurchaseInvoice);

                //dgImportarSaldosCabecera.ItemsSource = dataTableAsiento.DefaultView;
                wb.Close();
            }
        }

        private void RemoveDataRowFacturaDetalles(int transId)
        {
            var query = dtPurchaseInvoiceLines.AsEnumerable().Where(r => r.Field<int>("TransId") == transId);

            foreach (var row in query.ToList())
                row.Delete();

            dtPurchaseInvoiceLines.AcceptChanges();

            dgImportarFacturaProveedoresDetalle.ItemsSource = dtPurchaseInvoiceLines.DefaultView;
        }

        private void RemoveDataRowFacturaCabecera(DataRow row)
        {
            dtPurchaseInvoice.Rows.Remove(row);

            dtPurchaseInvoice.AcceptChanges();

            dgImportarFacturaProveedoresCabecera.ItemsSource = dtPurchaseInvoice.DefaultView;
        }

        private void RemoveDataRowFacturaProveedoresCabecera(System.Data.DataTable dataTable)
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

            dtPurchaseInvoice = ConvertDecimalFacturaCabecera(dataTable);

            dgImportarFacturaProveedoresCabecera.ItemsSource = dtPurchaseInvoice.DefaultView;


        }

        private void RemoveDataRowAsientoDetalle(System.Data.DataTable dataTable)
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

            dtPurchaseInvoiceLines = ConvertDecimalFacturaDetalle(dataTable);

            dgImportarFacturaProveedoresDetalle.ItemsSource = dtPurchaseInvoiceLines.DefaultView;


        }

        private System.Data.DataTable ConvertDecimalFacturaCabecera(System.Data.DataTable dt)
        {
            System.Data.DataTable newDt = dt.Copy();

            foreach (DataRow row in newDt.Rows)
            {
                foreach (DataColumn column in newDt.Columns)
                {

                    if (column.ToString() == "Debit")
                    {
                        row["Debit"] = ConvertDecimalTwoPlaces(row["Debit"]);

                    }

                    else if (column.ToString() == "Credit")
                    {
                        row["Credit"] = ConvertDecimalTwoPlaces(row["Credit"]);
                    }

                    else if (column.ToString() == "SYSDeb")
                    {
                        row["SYSDeb"] = ConvertDecimalTwoPlaces(row["SYSDeb"]);
                    }

                    else if (column.ToString() == "SYSCred")
                    {
                        row["SYSCred"] = ConvertDecimalTwoPlaces(row["SYSCred"]);
                    }

                    else if (column.ToString() == "FCDebit")
                    {
                        row["FCDebit"] = ConvertDecimalTwoPlaces(row["FCDebit"]);
                    }

                    else if (column.ToString() == "FCCredit")
                    {
                        row["FCCredit"] = ConvertDecimalTwoPlaces(row["FCCredit"]);
                    }

                }

            }

            return newDt;
        }

        private System.Data.DataTable ConvertDecimalFacturaDetalle(System.Data.DataTable dt)
        {
            System.Data.DataTable newDt = dt.Copy();

            foreach (DataRow row in newDt.Rows)
            {
                foreach (DataColumn column in newDt.Columns)
                {

                    if (column.ToString() == "Debit")
                    {
                        row["Debit"] = ConvertDecimalTwoPlaces(row["Debit"]);

                    }

                    else if (column.ToString() == "Credit")
                    {
                        row["Credit"] = ConvertDecimalTwoPlaces(row["Credit"]);
                    }

                    else if (column.ToString() == "SYSDeb")
                    {
                        row["SYSDeb"] = ConvertDecimalTwoPlaces(row["SYSDeb"]);
                    }

                    else if (column.ToString() == "SYSCred")
                    {
                        row["SYSCred"] = ConvertDecimalTwoPlaces(row["SYSCred"]);
                    }

                    else if (column.ToString() == "FCDebit")
                    {
                        row["FCDebit"] = ConvertDecimalTwoPlaces(row["FCDebit"]);
                    }

                    else if (column.ToString() == "FCCredit")
                    {
                        row["FCCredit"] = ConvertDecimalTwoPlaces(row["FCCredit"]);
                    }

                }

            }

            return newDt;
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

                dtPurchaseInvoice.Rows.Clear();

                dtPurchaseInvoice.AcceptChanges();

                dtPurchaseInvoice.TableName = worksheetName;
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
                    dtPurchaseInvoice.LoadDataRow(singleDValue, System.Data.LoadOption.PreserveChanges);
                }

                return dtPurchaseInvoice;
            }
            catch (Exception ex)
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + ex.Message, Brushes.Red, Brushes.White, "003-interface-2.png");

                return dtPurchaseInvoice;
            }


        }

        public System.Data.DataTable formofDataTableDetalle(Microsoft.Office.Interop.Excel.Worksheet ws)
        {
            try
            {
                string worksheetName = ws.Name;

                dtPurchaseInvoiceLines.Rows.Clear();

                dtPurchaseInvoiceLines.AcceptChanges();

                dtPurchaseInvoiceLines.TableName = worksheetName;
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
                    dtPurchaseInvoiceLines.LoadDataRow(singleDValue, System.Data.LoadOption.PreserveChanges);
                }

                return dtPurchaseInvoiceLines;
            }
            catch (Exception ex)
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + ex.Message, Brushes.Red, Brushes.White, "003-interface-2.png");

                return dtPurchaseInvoiceLines;
            }

        }

        private void btnImportar_Copy_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBuscar_Copy_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancelar_Copy_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
