using Entidades;
using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
    /// Lógica de interacción para ImportacionesAsientos.xaml
    /// </summary>
    public partial class ImportacionesAsientos : Document
    {
        System.Data.DataTable dataTableAsiento = new System.Data.DataTable();

        System.Data.DataTable dataTableAsientoDetalle = new System.Data.DataTable();

        ControladorAsiento cn = new ControladorAsiento();

       
        public ImportacionesAsientos()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadColumnAsientoCabecera();

            LoadColumnAsientoDetalle();

            
        }

        private void LoadColumnAsientoDetalle()
        {
            dataTableAsientoDetalle.Columns.Add("TransId", typeof(int));
            dataTableAsientoDetalle.Columns.Add("Line_ID");
            dataTableAsientoDetalle.Columns.Add("ShortName");
            dataTableAsientoDetalle.Columns.Add("Account");           
            dataTableAsientoDetalle.Columns.Add("Debit");
            dataTableAsientoDetalle.Columns.Add("Credit");
            dataTableAsientoDetalle.Columns.Add("SYSDeb");
            dataTableAsientoDetalle.Columns.Add("SYSCred");            
            dataTableAsientoDetalle.Columns.Add("FCDebit");
            dataTableAsientoDetalle.Columns.Add("FCCredit");
            dataTableAsientoDetalle.Columns.Add("FCCurrency");
            dataTableAsientoDetalle.Columns.Add("LineMemo");
            dataTableAsientoDetalle.Columns.Add("ContraAct");
            
        }

        private void LoadColumnAsientoCabecera()
        {
            dataTableAsiento.Columns.Add("TransId", typeof(int));
            dataTableAsiento.Columns.Add("RefDate");
            dataTableAsiento.Columns.Add("Memo");
            dataTableAsiento.Columns.Add("DueDate");
            dataTableAsiento.Columns.Add("TaxDate");
            dataTableAsiento.Columns.Add("TransCurr");
            dataTableAsiento.Columns.Add("LocTotal");            
            dataTableAsiento.Columns.Add("SysTotal");
            dataTableAsiento.Columns.Add("FcTotal");

        }

        public void LoadedWindow()
        {
            dataTableAsiento.Rows.Clear();

            dgImportarSaldosCabecera.ItemsSource = dataTableAsiento.DefaultView;
           
            dataTableAsientoDetalle.Rows.Clear();

            dgImportarSaldosDetalle.ItemsSource = dataTableAsientoDetalle.DefaultView;
        }

        private System.Data.DataTable ConvertDecimal(System.Data.DataTable dt)
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

        private System.Data.DataTable SetDatatableJournalEntry(int TransId)
        {
            System.Data.DataTable newDt = new System.Data.DataTable();

           
            try
            {

                newDt = dataTableAsiento.AsEnumerable().Where(r => ((int)r["TransId"]).Equals(TransId)).CopyToDataTable();

                return newDt;

            }
            catch (Exception e)
            {

                return newDt;

            }
        }

        private System.Data.DataTable SetDatatableJournalEntryLines(int TransId)
        {
            System.Data.DataTable newDt = new System.Data.DataTable();

            newDt = dataTableAsientoDetalle.Clone();

            dataTableAsientoDetalle.AcceptChanges();

            try
            {

                newDt = dataTableAsientoDetalle.AsEnumerable().Where(r => (r["TransId"]).Equals(TransId)).CopyToDataTable();

                return newDt;

            }
            catch (Exception e)
            {

                return newDt;

            }
        }

        private System.Data.DataTable CalculateContraAct(System.Data.DataTable dt)
        {
            System.Data.DataTable newDt = dt.Copy();

            int con = 0;

            int rows = newDt.Rows.Count;

            for (int i = 0; i < newDt.Rows.Count; i++)
            {
                DataRow row = newDt.Rows[i];

                if (i == (0 + con))
                {
                    if (con + 1 == rows) //es la ultima linea
                    {
                        row["ContraAct"] = newDt.Rows[(0 + con) - 1]["ShortName"].ToString();
                    }
                    else
                    {
                        row["ContraAct"] = newDt.Rows[(0 + con) + 1]["ShortName"].ToString();
                    }

                }
                else if (i == (1 + con))
                {
                    row["ContraAct"] = newDt.Rows[(1 + con) - 1]["ShortName"].ToString();

                }
                else if (i == (2 + con))
                {
                    row["ContraAct"] = newDt.Rows[(2 + con) - 2]["ShortName"].ToString();
                }
                else if (i == (3 + con))
                {
                    row["ContraAct"] = newDt.Rows[(3 + con) - 2]["ShortName"].ToString();
                }
                else if (i == (4 + con))
                {
                    row["ContraAct"] = newDt.Rows[(4 + con) - 3]["ShortName"].ToString();
                }
                else if (i == (5 + con))
                {
                    row["ContraAct"] = newDt.Rows[(5 + con) - 1]["ShortName"].ToString();
                }


                if (i % 5 == 0 && i != 0)
                {
                    con = con + 6;
                }



            }

            return newDt;


        }

        private System.Data.DataTable ConvertDecimalCabecera(System.Data.DataTable dt)
        {
            System.Data.DataTable newDt = dt.Copy();

            foreach (DataRow row in newDt.Rows)
            {
                foreach (DataColumn column in newDt.Columns)
                {

                    if (column.ToString() == "LocTotal")
                    {
                        row["LocTotal"] = ConvertDecimalTwoPlaces(row["LocTotal"]);

                    }

                    if (column.ToString() == "FcTotal")
                    {
                        row["FcTotal"] = ConvertDecimalTwoPlaces(row["FcTotal"]);
                    }

                    if (column.ToString() == "SysTotal")
                    {
                        row["SysTotal"] = ConvertDecimalTwoPlaces(row["SysTotal"]);
                    }

                    

                }

            }

            return newDt;
        }

        public Tuple<bool,decimal,decimal,decimal> VerificaDebeHaber(System.Data.DataTable dt)
        {
            bool sw = true;

            decimal mlDiference = 0;

            decimal msDiference = 0;

            decimal meDiference = 0;

            decimal SysDeb = 0;

            decimal SysCred = 0;

            decimal Debit = 0;

            decimal Credit = 0;

            decimal FCDebit = 0;

            decimal FCCredit = 0;

            foreach (DataRow row in dt.Rows)
            {
                Debit = ConvertDecimalTwoPlaces(row["Debit"]) + ConvertDecimalTwoPlaces(Debit);

                Credit = ConvertDecimalTwoPlaces(row["Credit"]) + ConvertDecimalTwoPlaces(Credit);

                SysDeb = ConvertDecimalTwoPlaces(row["SYSDeb"]) + ConvertDecimalTwoPlaces(SysDeb);

                SysCred = ConvertDecimalTwoPlaces(row["SYSCred"]) + ConvertDecimalTwoPlaces(SysCred);

                FCDebit = ConvertDecimalTwoPlaces(row["FCDebit"]) + ConvertDecimalTwoPlaces(FCDebit);

                FCCredit = ConvertDecimalTwoPlaces(row["FCCredit"]) + ConvertDecimalTwoPlaces(FCCredit);

            }

            if (ConvertDecimalTwoPlaces(Debit) == ConvertDecimalTwoPlaces(Credit))
            {
                sw = true;

                if (ConvertDecimalTwoPlaces(SysDeb) == ConvertDecimalTwoPlaces(SysCred))
                {
                    sw = true;

                    if (ConvertDecimalTwoPlaces(FCDebit) == ConvertDecimalTwoPlaces(FCCredit))
                    {
                        sw = true;
                    }
                    else
                    {
                        sw = false;
                    }
                }
                else
                {
                    sw = false;
                }

            }
            else
            {

                sw = false;
            }

            if (sw == false)// hay diferencias
            {
                
                    mlDiference = Debit - Credit;
               
                    msDiference = SysDeb - SysCred;               

                    meDiference = FCDebit - FCCredit;
                
            }            


            return Tuple.Create(sw,mlDiference,msDiference,meDiference);
        }

        private void btnImportar_Click(object sender, RoutedEventArgs e)
        {
            
            for (int i = 0; i < dataTableAsiento.Rows.Count; i++)
            {               

                DataRow row = dataTableAsiento.Rows[i];               

                System.Data.DataTable dataTableResultJournalEntry = SetDatatableJournalEntry(Convert.ToInt32(row["TransId"]));

                System.Data.DataTable dataTableResultJournalEntryLines = SetDatatableJournalEntryLines(Convert.ToInt32(row["TransId"]));

                var sw = VerificaDebeHaber(dataTableResultJournalEntryLines);               

                if (sw.Item1 == true)
                {
                    int j=TransacctionJournalEntry(dataTableResultJournalEntry, dataTableResultJournalEntryLines,i, row);

                    i = j;
                }
                else
                {
                    MessageBoxResult messageBoxResult = MessageBox.Show("¿Desea agregar la diferencia a la cuenta de redondeo?", "Importacion Asiento", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);

                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        
                        var cuentaRedondeo = cn.FindCuentaRedondeo();

                        if (cuentaRedondeo.Item2 == null && cuentaRedondeo.Item1!="")
                        {
                            string result= AddDiferenceCuentaRedondeo(cuentaRedondeo.Item1, dataTableResultJournalEntryLines.Rows.Count,row, sw);

                            if (result == null)
                            {
                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Operacion Realizada Exitosamente" , Brushes.LightGreen, Brushes.Black, "001-interface.png");
                            }
                            else
                            {
                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result, Brushes.Red, Brushes.White, "003-interface-2.png");
                            }
                        }
                        else if(cuentaRedondeo.Item2 != null)
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error cuenta de redondeo: " + cuentaRedondeo.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");

                        }else if(cuentaRedondeo.Item1 == "")
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Cuenta de redondeo no se ha definido: ", Brushes.Red, Brushes.White, "003-interface-2.png");
                        }


                        
                    }
                    else if(messageBoxResult == MessageBoxResult.No)
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Transaccion no ponderada", Brushes.Red, Brushes.White, "003-interface-2.png");
                    }
                    else
                    {

                    }
                       
                }
            }
            
        }


        private string AddDiferenceCuentaRedondeo(string cuentaRedondeo, int rows, DataRow row, Tuple<bool, decimal, decimal, decimal> sw)
        {
            string error = null;

            try
            {               

                DataRow newRow = dataTableAsientoDetalle.NewRow();

                dataTableAsientoDetalle.Rows.Add(newRow);

                newRow["TransId"] = Convert.ToInt32(row["TransId"]);

                newRow["Line_ID"] = rows;

                newRow["Account"] = cuentaRedondeo;

                newRow["ShortName"] = cuentaRedondeo;

                if (sw.Item2 > 0)
                {
                    newRow["Debit"] = 0;
                    newRow["Credit"] = sw.Item2;
                }
                else if(sw.Item2 < 0)
                {
                    newRow["Debit"] = sw.Item2*(-1);
                    newRow["Credit"] = 0;
                }
                else
                {
                    newRow["Debit"] = 0;
                    newRow["Credit"] = 0;
                }

                if (sw.Item3 > 0)
                {
                    newRow["SYSDeb"] = 0;
                    newRow["SYSCred"] = sw.Item3;
                }
                else if(sw.Item3 < 0)
                {
                    newRow["SYSDeb"] = sw.Item3 * (-1);
                    newRow["SYSCred"] = 0;
                }
                else
                {
                    newRow["SYSDeb"] = 0;
                    newRow["SYSCred"] = 0;
                }

                if (sw.Item4 > 0)
                {
                    newRow["FCDebit"] = 0;
                    newRow["FCCredit"] = sw.Item4;
                }
                else if (sw.Item4 < 0)
                {
                    newRow["FCDebit"] = sw.Item4 * (-1);
                    newRow["FCCredit"] = 0;
                }
                else
                {
                    newRow["FCDebit"] = 0;
                    newRow["FCCredit"] = 0;
                }

                newRow["FCCurrency"] = row["TransCurr"].ToString();
                newRow["LineMemo"] = "Ajuste redondeo";
                newRow["ContraAct"] = "";

                dataTableAsientoDetalle.AcceptChanges();

                dgImportarSaldosDetalle.ItemsSource = dataTableAsientoDetalle.DefaultView;

                return error;


            }catch(Exception ex)
            {
                return ex.Message;
            }
        }

        private int TransacctionJournalEntry(System.Data.DataTable dataTableResultJournalEntry, System.Data.DataTable dataTableResultJournalEntryLines, int i, DataRow row)
        {
            bool swFinncPriod = true;

            int transId =Convert.ToInt32(row["TransId"]);

            List<AsientoDetalle> listJournalEntryLinesUpdate = new List<AsientoDetalle>();           

            List<AsientoCabecera> listJournalEntry = new List<AsientoCabecera>();

            AsientoCabecera journalEntry = new AsientoCabecera();

            List<AsientoDetalle> listJournalEntryLines = new List<AsientoDetalle>();

            foreach (DataRow rowJournalEntry in dataTableResultJournalEntry.Rows)
            {

                journalEntry.TransId = Convert.ToInt32(rowJournalEntry["TransId"]);
                journalEntry.RefDate = DateTime.ParseExact(rowJournalEntry["RefDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                journalEntry.TaxDate = DateTime.ParseExact(rowJournalEntry["TaxDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                journalEntry.DueDate = DateTime.ParseExact(rowJournalEntry["DueDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                journalEntry.Memo = rowJournalEntry["Memo"].ToString();
                var baseRef = cn.GetBaseRef("30");
                journalEntry.BaseRef = baseRef.Item1;
                journalEntry.Ref1 = rowJournalEntry["Memo"].ToString();
                journalEntry.Ref2 = rowJournalEntry["Memo"].ToString();
                journalEntry.UserSign = Properties.Settings.Default.Usuario;
                journalEntry.UpdateDate = fechaActual.GetFechaActual();
                var result11 = cn.GetPeriodCode(journalEntry.RefDate);
                journalEntry.FinncPriod = result11.Item1;
                journalEntry.LocTotal = ConvertDecimalTwoPlaces(rowJournalEntry["LocTotal"]);
                journalEntry.SysTotal = ConvertDecimalTwoPlaces(rowJournalEntry["SysTotal"]);
                journalEntry.FcTotal = ConvertDecimalTwoPlaces(rowJournalEntry["FcTotal"]);
                journalEntry.TransType = cn.GetTransType("AS");
                journalEntry.TransCurr = rowJournalEntry["TransCurr"].ToString();

                swFinncPriod = VerifiedFinncPriod(result11.Item1);

                listJournalEntry.Add(journalEntry);

                break;
            }

            //if (swFinncPriod == true)
            //{
            System.Data.DataTable newDataTableResultJournalEntryLines = CalculateContraAct(dataTableResultJournalEntryLines);

            foreach (DataRow rowDataTableResultJournalEntryLines in newDataTableResultJournalEntryLines.Rows)
            {
                AsientoDetalle journalEntryLines = new AsientoDetalle();

                journalEntryLines.TransId = journalEntry.TransId;
                journalEntryLines.Line_ID = Convert.ToInt32(rowDataTableResultJournalEntryLines["Line_ID"]);
                journalEntryLines.RefDate = journalEntry.RefDate;
                journalEntryLines.DueDate = journalEntry.DueDate;
                journalEntryLines.TaxDate = journalEntry.TaxDate;
                journalEntryLines.Account = rowDataTableResultJournalEntryLines["Account"].ToString();
                journalEntryLines.ShortName = rowDataTableResultJournalEntryLines["ShortName"].ToString();
                journalEntryLines.LineMemo = rowDataTableResultJournalEntryLines["LineMemo"].ToString();
                journalEntryLines.ContraAct = rowDataTableResultJournalEntryLines["ContraAct"].ToString();
                journalEntryLines.TransType = journalEntry.TransType;
                journalEntryLines.Debit = ConvertDecimalTwoPlaces(rowDataTableResultJournalEntryLines["Debit"].ToString());
                journalEntryLines.Credit = ConvertDecimalTwoPlaces(rowDataTableResultJournalEntryLines["Credit"].ToString());
                journalEntryLines.FCDebit = ConvertDecimalTwoPlaces(rowDataTableResultJournalEntryLines["FCDebit"].ToString());
                journalEntryLines.FCCredit = ConvertDecimalTwoPlaces(rowDataTableResultJournalEntryLines["FCCredit"].ToString());
                journalEntryLines.SysCred = ConvertDecimalTwoPlaces(rowDataTableResultJournalEntryLines["SYSCred"].ToString());
                journalEntryLines.SysDeb = ConvertDecimalTwoPlaces(rowDataTableResultJournalEntryLines["SYSDeb"].ToString());
                journalEntryLines.BalDueDeb = journalEntryLines.Debit;
                journalEntryLines.BalDueCred = journalEntryLines.Credit;
                journalEntryLines.BalFcDeb = journalEntryLines.FCDebit;
                journalEntryLines.BalFcCred = journalEntryLines.FCCredit;
                journalEntryLines.BalScCred = journalEntryLines.SysCred;
                journalEntryLines.BalScDeb = journalEntryLines.SysDeb;
                journalEntryLines.UserSign = Properties.Settings.Default.Usuario;
                journalEntryLines.FinncPriod = journalEntry.FinncPriod;
                journalEntryLines.FCCurrency = journalEntry.TransCurr;
                journalEntryLines.DataSource = 'N';               

                listJournalEntryLines.Add(journalEntryLines);



            }

            var queryResult = cn.InsertJournalEntryComplete(listJournalEntry, listJournalEntryLines);

            if (queryResult.Item2 == null)
            {               

                RemoveDataRowAsientoDetalles(transId);

                RemoveDataRowAsientoCabecera(row);               

                i--;

            }
            else
            {
                if (queryResult.Item1 == 1)
                {
                    int ident = transId - 1;

                    var checkidentOjdt = cn.checkidentOjdt(ident);

                    if (checkidentOjdt == null)
                    {

                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + checkidentOjdt, Brushes.Red, Brushes.White, "003-interface-2.png");
                    }
                }

                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + queryResult.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");

               
            }


            return i;
            //}
            //else
            //{
            //    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Debe crear el periodo contable para la fecha: ", Brushes.Red, Brushes.White, "003-interface-2.png");
            //}
        }

        

        private bool VerifiedFinncPriod(int value)
        {
            bool sw;

            if (value == 0)
            {
                sw = false;
            }
            else
            {
                sw = true;
            }

            return sw;
        }

        private void RemoveDataRowAsientoDetalles(int transId)
        {
            var query = dataTableAsientoDetalle.AsEnumerable().Where(r => r.Field<int>("TransId") == transId);

            foreach (var row in query.ToList())
                row.Delete();

            dataTableAsientoDetalle.AcceptChanges();

            dgImportarSaldosDetalle.ItemsSource = dataTableAsientoDetalle.DefaultView;
        }

        private void RemoveDataRowAsientoCabecera(DataRow row)
        {
            dataTableAsiento.Rows.Remove(row);

            dataTableAsiento.AcceptChanges();

            dgImportarSaldosCabecera.ItemsSource = dataTableAsiento.DefaultView;
        }

        private void RemoveDataRowAsientoCabecera(System.Data.DataTable dataTable)
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

            dataTableAsiento = ConvertDecimalCabecera(dataTable);

            dgImportarSaldosCabecera.ItemsSource = dataTableAsiento.DefaultView;


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

            dataTableAsientoDetalle = ConvertDecimal(dataTable);            
          
            dgImportarSaldosDetalle.ItemsSource = dataTableAsientoDetalle.DefaultView;


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

                dataTableAsiento.Rows.Clear();

                dataTableAsiento.AcceptChanges();

                dataTableAsiento.TableName = worksheetName;
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
                    dataTableAsiento.LoadDataRow(singleDValue, System.Data.LoadOption.PreserveChanges);
                }

                return dataTableAsiento;
            }
            catch(Exception ex)
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + ex.Message, Brushes.Red, Brushes.White, "003-interface-2.png");

                return dataTableAsiento;
            }

            
        }

        public System.Data.DataTable formofDataTableDetalle(Microsoft.Office.Interop.Excel.Worksheet ws)
        {
            try
            {
                string worksheetName = ws.Name;

                dataTableAsientoDetalle.Rows.Clear();

                dataTableAsientoDetalle.AcceptChanges();

                dataTableAsientoDetalle.TableName = worksheetName;
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
                    dataTableAsientoDetalle.LoadDataRow(singleDValue, System.Data.LoadOption.PreserveChanges);
                }

                return dataTableAsientoDetalle;
            }
            catch (Exception ex)
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + ex.Message, Brushes.Red, Brushes.White, "003-interface-2.png");

                return dataTableAsientoDetalle;
            }
            
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
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

                    dataTableAsiento = formofDataTable(ws);
                    //ds.Tables.Add(td);//This will give the DataTable from Excel file in Dataset
                }

                RemoveDataRowAsientoCabecera(dataTableAsiento);

                //dgImportarSaldosCabecera.ItemsSource = dataTableAsiento.DefaultView;
                wb.Close();
            }
        }

     
        private void btnCancelar_Copy_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void btnImportar_Copy_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBuscar_Copy_Click(object sender, RoutedEventArgs e)
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

                    dataTableAsientoDetalle = formofDataTableDetalle(ws);
                    //ds.Tables.Add(td);//This will give the DataTable from Excel file in Dataset
                }

                RemoveDataRowAsientoDetalle(dataTableAsientoDetalle);

                //dgImportarSaldosDetalle.ItemsSource = dataTableAsientoDetalle.DefaultView;

                wb.Close();
            }
        }
    }
}
