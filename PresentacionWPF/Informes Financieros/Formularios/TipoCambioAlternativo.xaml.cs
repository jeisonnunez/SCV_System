using Entidades;
using Negocio;
using Negocio.Controlador_Finanzas;
using Negocio.Controlador_Informes;
using System;
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

namespace Vista.Informes_Financieros.Formularios
{
    /// <summary>
    /// Lógica de interacción para TipoCambioAlternativo.xaml
    /// </summary>
    public partial class TipoCambioAlternativo : Converter
    {
        DataTable dt = new DataTable();

        DataTable dtExcel = new DataTable();

        DataTable dtNewJournalEntry = new DataTable();

        public DateTime dpDesde;

        public DateTime dpHasta;

        ControladorTipoCambioAlternativo cn = new ControladorTipoCambioAlternativo();

        List<AsientoCabecera> listaJournalEntryDef = new List<AsientoCabecera>();      

        List<AsientoDetalle> listaJournalEntryLinesDef = new List<AsientoDetalle>();

        ControladorAsiento cj = new ControladorAsiento();

        private bool sw;

        public int Sw { get; private set; }
        public int TransId { get; private set; }

        public TipoCambioAlternativo()
        {
            InitializeComponent();
        }

        public void ClearDatatable()
        {
            dtExcel.Rows.Clear();

        }

        public void SetDataTable(DataTable dataTable)
        {
            dtExcel = AddCurrencyCode(dataTable);           


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

                    else if (column.ToString() == "SaldoSYS" && Convert.ToDecimal(row["SaldoSYS"]) != 0)
                    {
                        row["SaldoSYS"] = Properties.Settings.Default.SysCurrency + " " + (ConvertDecimalTwoPlaces(row["SaldoSYS"])).ToString("N", nfi);
                    }

                    else if (column.ToString() == "SYSDeb" && Convert.ToDecimal(row["SYSDeb"]) != 0)
                    {
                        row["SYSDeb"] = Properties.Settings.Default.SysCurrency + " " + (ConvertDecimalTwoPlaces(row["SYSDeb"])).ToString("N", nfi);
                    }

                    else if (column.ToString() == "SYSCred" && Convert.ToDecimal(row["SYSCred"]) != 0)
                    {
                        row["SYSCred"] = Properties.Settings.Default.SysCurrency + " " + (ConvertDecimalTwoPlaces(row["SYSCred"])).ToString("N", nfi);
                    }

                    else if (column.ToString() == "FCDebit" && Convert.ToDecimal(row["FCDebit"]) != 0)
                    {
                        if (row["FCCurrency"].ToString() == "****")
                        {
                            row["FCDebit"] = row["FCCurrency"].ToString();
                        }
                        else
                        {
                            row["FCDebit"] = row["FCCurrency"].ToString() + " " + (ConvertDecimalTwoPlaces(row["FCDebit"])).ToString("N", nfi);
                        }


                    }

                    else if (column.ToString() == "FCCredit" && Convert.ToDecimal(row["FCCredit"]) != 0)
                    {
                        if (row["FCCurrency"].ToString() == "****")
                        {
                            row["FCCredit"] = row["FCCurrency"].ToString();
                        }
                        else
                        {
                            row["FCCredit"] = row["FCCurrency"].ToString() + " " + (ConvertDecimalTwoPlaces(row["FCCredit"])).ToString("N", nfi);
                        }
                    }

                    else if (column.ToString() == "FCSaldo" && Convert.ToDecimal(row["FCSaldo"]) != 0)
                    {
                        if (row["FCCurrency"].ToString() == "****")
                        {
                            row["FCSaldo"] = row["FCCurrency"].ToString();
                        }
                        else
                        {
                            row["FCSaldo"] = row["FCCurrency"].ToString() + " " + (ConvertDecimalTwoPlaces(row["FCSaldo"])).ToString("N", nfi);
                        }
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


        private DataTable CreateDatatableJournalEntryLines(DataRow row)
        {
            decimal SaldoML = 0;

            bool? sw = null;

            DataTable dtResult = new DataTable();

            dtResult.Columns.Add("ShortName");
            dtResult.Columns.Add("AcctName");
            dtResult.Columns.Add("Account");
            dtResult.Columns.Add("FCDebit");
            dtResult.Columns.Add("FCCredit");
            dtResult.Columns.Add("Debit");
            dtResult.Columns.Add("Credit");
            dtResult.Columns.Add("SYSDeb");
            dtResult.Columns.Add("SYSCred");
            dtResult.Columns.Add("LineMemo");
            dtResult.Columns.Add("ContraAct");
            //Create Row Cuenta a tratar

            DataRow newRow = dtResult.NewRow();

            newRow["ShortName"] = row["ShortName"];

            newRow["Account"] = row["Account"];

            newRow["LineMemo"] = "Diferencia Tipo Cambio";


            if (ConvertDecimalTwoPlaces(row["Diferencia"]) > 0) //
            {
                sw = true;

                if (row["Type"].ToString() == "N")
                {
                    newRow["ContraAct"] = txtCuentaPerdida.Text;

                }
                else if (row["Type"].ToString() == "S")
                {
                    newRow["ContraAct"] = txtCuentaPerdida.Text;
                }
                else if (row["Type"].ToString() == "C")
                {
                    newRow["ContraAct"] = txtCuentaPerdida.Text;
                }


                if (ConvertDecimalTwoPlaces(row["Diferencia"]) > 0)
                {
                    SaldoML = ConvertDecimalTwoPlaces(row["Diferencia"]);
                }
                else if (ConvertDecimalTwoPlaces(row["Diferencia"]) < 0)
                {
                    SaldoML = ConvertDecimalTwoPlaces(row["Diferencia"]) * (-1);
                }

                newRow["Credit"] = SaldoML;

                newRow["SYSCred"] = SaldoML / ConvertDecimalTwoPlaces(row["Rate"]);

                newRow["FCCredit"] = 0;

                newRow["Debit"] = 0;

                newRow["SYSDeb"] = 0;

                newRow["FCDebit"] = 0;
            }
            else if (ConvertDecimalTwoPlaces(row["Diferencia"]) < 0)
            {
                sw = false;

                if (row["Type"].ToString() == "N")
                {
                    newRow["ContraAct"] = txtCuentaBeneficio.Text;

                }
                else if (row["Type"].ToString() == "S")
                {
                    newRow["ContraAct"] = txtCuentaBeneficio.Text;
                }
                else if (row["Type"].ToString() == "C")
                {
                    newRow["ContraAct"] = txtCuentaBeneficio.Text;
                }


                if (ConvertDecimalTwoPlaces(row["Diferencia"]) > 0)
                {
                    SaldoML = ConvertDecimalTwoPlaces(row["Diferencia"]);
                }
                else if (ConvertDecimalTwoPlaces(row["Diferencia"]) < 0)
                {
                    SaldoML = ConvertDecimalTwoPlaces(row["Diferencia"]) * (-1);
                }


                newRow["Debit"] = SaldoML;

                newRow["SYSDeb"] = SaldoML / ConvertDecimalTwoPlaces(row["Rate"]);

                newRow["FCDebit"] = 0;

                newRow["Credit"] = 0;

                newRow["SYSCred"] = 0;

                newRow["FCCredit"] = 0;
            }


            dtResult.Rows.Add(newRow);


            //Create Row Cuenta a Arrastre

            DataRow newRow1 = dtResult.NewRow();

            newRow1["ShortName"] = newRow["ContraAct"];

            newRow1["Account"] = newRow["ContraAct"];

            newRow1["ContraAct"] = newRow["ShortName"];

            newRow1["LineMemo"] = "Diferencia Tipo Cambio";

            if (sw == true)
            {
                newRow1["Debit"] = SaldoML;

                newRow1["SYSDeb"] = newRow["SYSCred"];

                newRow1["FCDebit"] = newRow["FCCredit"];

                newRow1["Credit"] = 0;

                newRow1["SYSCred"] = 0;

                newRow1["FCCredit"] = 0;
            }
            else if (sw == false)
            {
                newRow1["Credit"] = SaldoML;

                newRow1["SYSCred"] = newRow["SYSDeb"];

                newRow1["FCCredit"] = newRow["FCDebit"];

                newRow1["Debit"] = 0;

                newRow1["SYSDeb"] = 0;

                newRow1["FCDebit"] = 0;
            }


            dtResult.Rows.Add(newRow1);


            SaldoML = 0;

            return dtResult;
        }
        private void btnEjecutar_Click(object sender, RoutedEventArgs e)
        {
            var dataSetResult = cn.CreateDatasetPreliminarTipoCambio();

            if (dataSetResult == null)
            {
                var result = cn.ExecuteExchangeRateDifference(txtCuentaBeneficio.Text, txtCuentaPerdida.Text, dt, ConvertDecimalTwoPlaces(txtTipoCambio.Text), dpDFechaVencimiento.SelectedDate, dpHFechaVencimiento.SelectedDate);

                int rows=result.Item1.Rows.Count;

                if (result.Item2 == null)
                {
                    var loadTables = cn.LoadTables();

                    if (loadTables == null)
                    {
                        for (int i = 0; i < result.Item1.Rows.Count; i++)
                        {
                            DataRow row = result.Item1.Rows[i];

                            List<AsientoCabecera> listaJournalEntry = new List<AsientoCabecera>();

                            AsientoCabecera journalEntry = new AsientoCabecera();

                            List<AsientoDetalle> listaJournalEntryLines = new List<AsientoDetalle>();

                            listaJournalEntry.Clear();

                            bool? OJDT = null;

                            bool? JDT1 = null;

                            listaJournalEntry = CreateJournalEntry(row);

                            var result2 = cj.InsertJournalEntryPreliminarAlternativo(listaJournalEntry);

                            if (result2.Item1 == 1)
                            {

                                var listJournalEntryLines = CreateListJournalEntryLines(listaJournalEntry,row);

                                listaJournalEntryLines = listJournalEntryLines.Item1;

                                var result3 = cj.InsertJournalEntryLinesPreliminarAlternativo(listJournalEntryLines.Item1);

                                if (listJournalEntryLines.Item2 == result3.Item1)
                                {
                                    sw = true;

                                  

                                }
                                else
                                {
                                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: " + result3.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                                    sw = false;


                                }

                            }
                            else
                            {
                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: " + result2.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                                sw = false;


                            }
                        }

                        bool SW;

                        //verificar si existe algun error antes de ejecutar

                        listaJournalEntryDef.Clear();

                        listaJournalEntryLinesDef.Clear();

                        listaJournalEntryDef = ConvertToAsientoCabecera(cn.GetOJDT());

                        listaJournalEntryLinesDef = ConvertToAsientoDetalle(cn.GetJDT1());

                        var result4 = cj.InsertJournalEntryPreliminarTest(listaJournalEntryDef);

                        if (listaJournalEntryDef.Count==result4.Item1)
                        {
                            
                            var result3 = cj.InsertJournalEntryLinesPreliminarTest(listaJournalEntryLinesDef);

                            if (listaJournalEntryLinesDef.Count == result3.Item1)
                            {
                                SW = true;

                            }
                            else
                            {
                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: " + result3.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                                SW = false;

                            }

                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: " + result4.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                            SW = false;


                        }

                        if (SW == true) {

                            var resultDiferenciaConversion = cn.ExecuteExchangeConversion(txtCuentaBeneficio.Text, txtCuentaPerdida.Text, dt, ConvertDecimalTwoPlaces(txtTipoCambio.Text));

                            if (resultDiferenciaConversion.Item2 == null)
                            {

                                for (int i = 0; i < resultDiferenciaConversion.Item1.Rows.Count; i++)
                                {
                                    DataRow row1 = resultDiferenciaConversion.Item1.Rows[i];

                                    List<AsientoCabecera> listaJournalEntryConversion = new List<AsientoCabecera>();

                                    AsientoCabecera journalEntryConversion = new AsientoCabecera();

                                    List<AsientoDetalle> listaJournalEntryLinesConversion = new List<AsientoDetalle>();

                                    listaJournalEntryConversion.Clear();

                                    bool? OJDT = null;

                                    bool? JDT1 = null;

                                    bool? OACT = null;

                                    //Create Journal Entry

                                    listaJournalEntryConversion = CreateJournalEntryConversion(row1);

                                    //Contruir asiento

                                    var result2 = cj.InsertJournalEntryPreliminarTest(listaJournalEntryConversion);

                                    if (result2.Item1 == 1)
                                    {
                                        OJDT = true;

                                        var listJournalEntryLines = CreateListJournalEntryLinesConversion(listaJournalEntryConversion, row1);

                                        var result3 = cj.InsertJournalEntryLinesPreliminarTest(listJournalEntryLines.Item1);

                                        if (listJournalEntryLines.Item2 == result3.Item1)
                                        {

                                        }
                                        else
                                        {
                                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: " + result3.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");

                                            JDT1 = false;


                                        }

                                    }
                                    else
                                    {
                                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: " + result2.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                                        OJDT = false;

                                    }
                                }


                                //Consulta Nuevo Balance

                                var executeBalanceAlternativo = cn.ExecuteBalanceAlternate(dt, Balance.dpDesde,Balance.dpHasta, Balance.monedaLocalySystema, Balance.monedaSystema, Balance.monedaExtranjera, Balance.cuentaCero);

                                if (executeBalanceAlternativo.Item2 == null)
                                {
                                    ClearDatatable();

                                    SetDataTable(executeBalanceAlternativo.Item1);

                                    var resultClearDataSet = cn.ResetDataSetPreliminar();

                                    var resultDeleteJounralEntrys = cn.DeleteJournalEntrysTest();

                                    GenerateExcel(dtExcel);
                                }
                                else
                                {
                                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + executeBalanceAlternativo.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                                }

                                    
                            }
                            else
                            {
                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + resultDiferenciaConversion.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                            }
                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al insertar algun registro en la tabla de asientos temporales", Brushes.Red, Brushes.White, "003-interface-2.png");
                        }

                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + loadTables, Brushes.Red, Brushes.White, "003-interface-2.png");
                    }

                   
                    //if (result.Item1.Rows.Count >= 1)
                    //{
                    //    //dtDiferenciaTipoCambio.ClearDatatable();

                        //    //dtDiferenciaTipoCambio.Show();


                        //}
                        //else
                        //{
                        //    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se encontraron resultados con los parametros establecidos: " + result.Item2, Brushes.LightBlue, Brushes.Black, "002-interface-1.png");
                        //}
                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                }

            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + dataSetResult, Brushes.Red, Brushes.White, "003-interface-2.png");
            }
        }

        private List<AsientoDetalle> ConvertToAsientoDetalle(DataTable dataTable)
        {
            List<AsientoDetalle> newList = new List<AsientoDetalle>();

            foreach (DataRow row in dataTable.Rows)
            {
                AsientoDetalle list = new AsientoDetalle();

                list.TransId = Convert.ToInt32(row["TransId"]);
                list.TransType = Convert.ToInt32(row["TransType"]);
                list.RefDate = Convert.ToDateTime(row["RefDate"]);
                list.DueDate = Convert.ToDateTime(row["DueDate"]);
                list.TaxDate = Convert.ToDateTime(row["TaxDate"]);
                list.LineMemo = row["LineMemo"].ToString();
                list.Line_ID =Convert.ToInt32(row["Line_ID"]);
                list.Account =row["Account"].ToString();
                list.ShortName = row["ShortName"].ToString();
                list.Debit = ConvertDecimalTwoPlaces(row["Debit"].ToString());
                list.Credit = ConvertDecimalTwoPlaces(row["Credit"].ToString());
                list.FCCredit = ConvertDecimalTwoPlaces(row["FCCredit"].ToString());
                list.FCDebit = ConvertDecimalTwoPlaces(row["FCDebit"].ToString());
                list.SysCred = ConvertDecimalTwoPlaces(row["SYSCred"].ToString());
                list.SysDeb = ConvertDecimalTwoPlaces(row["SYSDeb"].ToString());
                list.ContraAct = row["ContraAct"].ToString();
                list.FinncPriod = Convert.ToInt32(row["FinncPriod"]);               
                list.UserSign = Convert.ToInt32(row["UserSign"]);
                list.BalDueCred = ConvertDecimalTwoPlaces(row["BalDueCred"].ToString());
                list.BalDueDeb = ConvertDecimalTwoPlaces(row["BalDueDeb"].ToString());
                list.BalFcCred = ConvertDecimalTwoPlaces(row["BalFcCred"].ToString());
                list.BalFcDeb = ConvertDecimalTwoPlaces(row["BalFcDeb"].ToString());
                list.BalScCred = ConvertDecimalTwoPlaces(row["BalScCred"].ToString());
                list.BalScDeb = ConvertDecimalTwoPlaces(row["BalScDeb"].ToString());
                list.DataSource = Convert.ToChar(row["DataSource"]);
                list.FCCurrency = row["FCCurrency"].ToString();

                newList.Add(list);
            }

            return newList;
        }

        private List<AsientoCabecera> ConvertToAsientoCabecera(DataTable dataTable)
        {
            List<AsientoCabecera> newList = new List<AsientoCabecera>();

            foreach(DataRow row in dataTable.Rows)
            {
                AsientoCabecera list = new AsientoCabecera();

                list.TransId = Convert.ToInt32(row["TransId"]);
                list.TransType = Convert.ToInt32(row["TransType"]);
                list.RefDate = Convert.ToDateTime(row["RefDate"]);
                list.DueDate = Convert.ToDateTime(row["DueDate"]);
                list.TaxDate = Convert.ToDateTime(row["TaxDate"]);
                list.Memo = row["Memo"].ToString();
                list.LocTotal =ConvertDecimalTwoPlaces(row["LocTotal"]);
                list.FcTotal = ConvertDecimalTwoPlaces(row["FcTotal"]);
                list.SysTotal = ConvertDecimalTwoPlaces(row["SysTotal"]);
                list.FinncPriod = Convert.ToInt32(row["FinncPriod"]);

                if (String.IsNullOrWhiteSpace(row["UpdateDate"].ToString()) == false){

                    list.UpdateDate = Convert.ToDateTime(row["UpdateDate"]);
                }
                else
                {
                    list.UpdateDate = DateTime.Now;
                }

                list.UserSign = Convert.ToInt32(row["UserSign"]);
                list.TransCurr = row["TransCurr"].ToString();
                list.BaseRef = Convert.ToInt32(row["BaseRef"]);
                list.Ref1 = row["Ref1"].ToString();
                list.Ref2 = row["Ref2"].ToString();
                

                newList.Add(list);
            }

            return newList;
        }

        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }


        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (column.ColumnName != "TransType")
                    {


                        if (pro.Name == column.ColumnName)
                        {
                            pro.SetValue(obj, dr[column.ColumnName], null);
                        }
                        else
                        {
                            continue;
                        }

                    }
                   
                       
                }
            }
            return obj;
        }

        private void txtCuenta_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;


            if (textBox.Name == "txtCuentaBeneficio")
            {
                dpCuentaBeneficioCliente.Background = Brushes.LightBlue;

                bdCuentaBeneficioCliente.Background = Brushes.LightBlue;

                txtCuentaBeneficio.Background = Brushes.LightBlue;

                imgCuentaBeneficioCliente.Visibility = Visibility.Visible;
            }
           
            else if (textBox.Name == "txtCuentaPerdida")
            {

                dpCuentaPerdidaCliente.Background = Brushes.LightBlue;

                bdCuentaPerdidaCliente.Background = Brushes.LightBlue;

                txtCuentaPerdida.Background = Brushes.LightBlue;

                imgCuentaPerdidaCliente.Visibility = Visibility.Visible;
            }



        }

        private void txtCuenta_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Name == "txtCuentaBeneficio")
            {
                dpCuentaBeneficioCliente.Background = Brushes.White;

                bdCuentaBeneficioCliente.Background = Brushes.White;

                txtCuentaBeneficio.Background = Brushes.White;

                imgCuentaBeneficioCliente.Visibility = Visibility.Hidden;
            }
           

            else if (textBox.Name == "txtCuentaPerdida")
            {

                dpCuentaPerdidaCliente.Background = Brushes.White;

                bdCuentaPerdidaCliente.Background = Brushes.White;

                txtCuentaPerdida.Background = Brushes.White;

                imgCuentaPerdidaCliente.Visibility = Visibility.Hidden;
            }

            
        }

        private DataTable CreateDatatableJournalEntryLinesConversion(DataRow row)
        {
            decimal SaldoMS = 0;

            bool? sw = null;

            DataTable dtResult = new DataTable();

            dtResult.Columns.Add("ShortName");
            dtResult.Columns.Add("AcctName");
            dtResult.Columns.Add("Account");
            dtResult.Columns.Add("FCDebit");
            dtResult.Columns.Add("FCCredit");
            dtResult.Columns.Add("Debit");
            dtResult.Columns.Add("Credit");
            dtResult.Columns.Add("SYSDeb");
            dtResult.Columns.Add("SYSCred");
            dtResult.Columns.Add("LineMemo");
            dtResult.Columns.Add("ContraAct");
            //Create Row Cuenta a tratar

            DataRow newRow = dtResult.NewRow();

            newRow["ShortName"] = row["ShortName"];

            newRow["Account"] = row["Account"];

            newRow["LineMemo"] = "Diferencia Conversion";


            if (ConvertDecimalTwoPlaces(row["Diferencia"]) > 0) //
            {
                sw = true;

                if (row["Type"].ToString() == "N")
                {
                    newRow["ContraAct"] = txtCuentaPerdida.Text;

                }
                else if (row["Type"].ToString() == "S")
                {
                    newRow["ContraAct"] = txtCuentaPerdida.Text;
                }
                else if (row["Type"].ToString() == "C")
                {
                    newRow["ContraAct"] = txtCuentaPerdida.Text;
                }


                if (ConvertDecimalTwoPlaces(row["Diferencia"]) > 0)
                {
                    SaldoMS = ConvertDecimalTwoPlaces(row["Diferencia"]);
                }
                else if (ConvertDecimalTwoPlaces(row["Diferencia"]) < 0)
                {
                    SaldoMS = ConvertDecimalTwoPlaces(row["Diferencia"]) * (-1);
                }

                newRow["Credit"] = 0;

                newRow["SYSCred"] = SaldoMS;

                newRow["FCCredit"] = 0;

                newRow["Debit"] = 0;

                newRow["SYSDeb"] = 0;

                newRow["FCDebit"] = 0;
            }
            else if (ConvertDecimalTwoPlaces(row["Diferencia"]) < 0)
            {
                sw = false;

                if (row["Type"].ToString() == "N")
                {
                    newRow["ContraAct"] = txtCuentaBeneficio.Text;

                }
                else if (row["Type"].ToString() == "S")
                {
                    newRow["ContraAct"] = txtCuentaBeneficio.Text;
                }
                else if (row["Type"].ToString() == "C")
                {
                    newRow["ContraAct"] = txtCuentaBeneficio.Text;
                }


                if (ConvertDecimalTwoPlaces(row["Diferencia"]) > 0)
                {
                    SaldoMS = ConvertDecimalTwoPlaces(row["Diferencia"]);
                }
                else if (ConvertDecimalTwoPlaces(row["Diferencia"]) < 0)
                {
                    SaldoMS = ConvertDecimalTwoPlaces(row["Diferencia"]) * (-1);
                }


                newRow["Debit"] = 0;

                newRow["SYSDeb"] = SaldoMS;

                newRow["FCDebit"] = 0;

                newRow["Credit"] = 0;

                newRow["SYSCred"] = 0;

                newRow["FCCredit"] = 0;
            }


            dtResult.Rows.Add(newRow);


            //Create Row Cuenta a Arrastre

            DataRow newRow1 = dtResult.NewRow();

            newRow1["ShortName"] = newRow["ContraAct"];

            newRow1["Account"] = newRow["ContraAct"];

            newRow1["ContraAct"] = newRow["ShortName"];

            newRow1["LineMemo"] = "Diferencia Conversion";

            if (sw == true)
            {
                newRow1["Debit"] = 0;

                newRow1["SYSDeb"] = newRow["SYSCred"];

                newRow1["FCDebit"] = 0;

                newRow1["Credit"] = 0;

                newRow1["SYSCred"] = 0;

                newRow1["FCCredit"] = 0;
            }
            else if (sw == false)
            {
                newRow1["Credit"] = 0;

                newRow1["SYSCred"] = newRow["SYSDeb"];

                newRow1["FCCredit"] = 0;

                newRow1["Debit"] = 0;

                newRow1["SYSDeb"] = 0;

                newRow1["FCDebit"] = 0;
            }


            dtResult.Rows.Add(newRow1);

            SaldoMS = 0;

            return dtResult;
        }

        private Tuple<List<AsientoDetalle>, int> CreateListJournalEntryLinesConversion(List<AsientoCabecera> listaJournalEntry, DataRow rowActual)
        {
            dtNewJournalEntry = CreateDatatableJournalEntryLinesConversion(rowActual);

            int TransId = 0;

            string TransCurr = null;

            DateTime? RefDate = null;

            DateTime? DueDate = null;

            DateTime? TaxDate = null;

            string Memo = null;

            int FinncPriod = 0;

            int TransType = 0;

            foreach (AsientoCabecera journalEntry in listaJournalEntry)
            {
                TransId = journalEntry.TransId;
                TransCurr = journalEntry.TransCurr;
                RefDate = journalEntry.RefDate;
                TaxDate = journalEntry.TaxDate;
                DueDate = journalEntry.DueDate;
                Memo = journalEntry.Memo;
                FinncPriod = journalEntry.FinncPriod;
                TransType = journalEntry.TransType;

            }

            int k = 0;

            List<AsientoDetalle> listaJournalEntryLines = new List<AsientoDetalle>();

            foreach (DataRow row in dtNewJournalEntry.Rows)
            {
                AsientoDetalle journalEntryLines = new AsientoDetalle();

                journalEntryLines.TransId = TransId;
                journalEntryLines.Line_ID = k;
                journalEntryLines.RefDate = RefDate;
                journalEntryLines.DueDate = DueDate;
                journalEntryLines.TaxDate = TaxDate;
                journalEntryLines.Account = row["Account"].ToString();
                journalEntryLines.ShortName = row["ShortName"].ToString();
                journalEntryLines.ContraAct = row["ContraAct"].ToString();
                journalEntryLines.LineMemo = Memo;
                journalEntryLines.TransType = TransType;
                journalEntryLines.Debit = ConvertDecimalTwoPlaces(row["Debit"].ToString());
                journalEntryLines.Credit = ConvertDecimalTwoPlaces(row["Credit"].ToString());
                journalEntryLines.FCDebit = ConvertDecimalTwoPlaces(row["FCDebit"].ToString());
                journalEntryLines.FCCredit = ConvertDecimalTwoPlaces(row["FCCredit"].ToString());
                journalEntryLines.SysCred = ConvertDecimalTwoPlaces(row["SYSCred"].ToString());
                journalEntryLines.SysDeb = ConvertDecimalTwoPlaces(row["SYSDeb"].ToString());
                journalEntryLines.BalDueDeb = journalEntryLines.Debit;
                journalEntryLines.BalDueCred = journalEntryLines.Credit;
                journalEntryLines.BalFcDeb = journalEntryLines.FCDebit;
                journalEntryLines.BalFcCred = journalEntryLines.FCCredit;
                journalEntryLines.BalScCred = journalEntryLines.SysCred;
                journalEntryLines.BalScDeb = journalEntryLines.SysDeb;
                journalEntryLines.UserSign = Properties.Settings.Default.Usuario;
                journalEntryLines.FinncPriod = FinncPriod;
                journalEntryLines.FCCurrency = TransCurr;
                journalEntryLines.DataSource = 'N';

                listaJournalEntryLines.Add(journalEntryLines);

                k++;

            }

            return Tuple.Create(listaJournalEntryLines, k);
        }

        private List<AsientoCabecera> CreateJournalEntryConversion(DataRow row)
        {
            decimal SaldoMS = 0;

            List<AsientoCabecera> listaJournalEntry = new List<AsientoCabecera>();

            AsientoCabecera journalEntry = new AsientoCabecera();

            var transIdPreliminar = cn.SelectTransIdPreliminar();

            TransId = transIdPreliminar.Item1;

            journalEntry.TransId = transIdPreliminar.Item1;
            journalEntry.RefDate = Balance.dpHasta.Value;
            journalEntry.TaxDate = Balance.dpHasta.Value;
            journalEntry.DueDate = Balance.dpHasta.Value;
            journalEntry.Memo = "Diferencia Conversion";
            journalEntry.BaseRef = 1; //revisar
            journalEntry.Ref1 = "Diferencia Conversion";
            journalEntry.Ref2 = "Diferencia Conversion";
            journalEntry.UserSign = Properties.Settings.Default.Usuario;
            journalEntry.UpdateDate = fechaActual.GetFechaActual();
            var result1 = cn.GetPeriodCode(journalEntry.RefDate);
            journalEntry.FinncPriod = result1.Item1;
            journalEntry.LocTotal = 0;

            if (ConvertDecimalTwoPlaces(row["Diferencia"]) < 0)
            {

                SaldoMS = ConvertDecimalTwoPlaces(row["Diferencia"]) * (-1);


            }
            else if (ConvertDecimalTwoPlaces(row["Diferencia"]) > 0)
            {
                SaldoMS = ConvertDecimalTwoPlaces(row["Diferencia"]);
            }


            journalEntry.SysTotal = SaldoMS;

            journalEntry.FcTotal = 0;
            journalEntry.TransType = cn.GetTransType("CB");
            journalEntry.TransCurr = Properties.Settings.Default.MainCurrency;

            listaJournalEntry.Add(journalEntry);

            return listaJournalEntry;

        }

        private Tuple<List<AsientoDetalle>, int> CreateListJournalEntryLines(List<AsientoCabecera> listaJournalEntry, DataRow rowActual)
        {
            dtNewJournalEntry = CreateDatatableJournalEntryLines(rowActual);

            int TransId = 0;

            string TransCurr = null;

            DateTime? RefDate = null;

            DateTime? DueDate = null;

            DateTime? TaxDate = null;

            string Memo = null;

            int FinncPriod = 0;

            int TransType = 0;

            foreach (AsientoCabecera journalEntry in listaJournalEntry)
            {
                TransId = journalEntry.TransId;
                TransCurr = journalEntry.TransCurr;
                RefDate = journalEntry.RefDate;
                TaxDate = journalEntry.TaxDate;
                DueDate = journalEntry.DueDate;
                Memo = journalEntry.Memo;
                FinncPriod = journalEntry.FinncPriod;
                TransType = journalEntry.TransType;

            }

            int k = 0;

            List<AsientoDetalle> listaJournalEntryLines = new List<AsientoDetalle>();

            foreach (DataRow row in dtNewJournalEntry.Rows)
            {
                AsientoDetalle journalEntryLines = new AsientoDetalle();

                journalEntryLines.TransId = TransId;
                journalEntryLines.Line_ID = k;
                journalEntryLines.RefDate = RefDate;
                journalEntryLines.DueDate = DueDate;
                journalEntryLines.TaxDate = TaxDate;
                journalEntryLines.Account = row["Account"].ToString();
                journalEntryLines.ShortName = row["ShortName"].ToString();
                journalEntryLines.ContraAct = row["ContraAct"].ToString();
                journalEntryLines.LineMemo = Memo;
                journalEntryLines.TransType = TransType;
                journalEntryLines.Debit = ConvertDecimalTwoPlaces(row["Debit"].ToString());
                journalEntryLines.Credit = ConvertDecimalTwoPlaces(row["Credit"].ToString());
                journalEntryLines.FCDebit = ConvertDecimalTwoPlaces(row["FCDebit"].ToString());
                journalEntryLines.FCCredit = ConvertDecimalTwoPlaces(row["FCCredit"].ToString());
                journalEntryLines.SysCred = ConvertDecimalTwoPlaces(row["SYSCred"].ToString());
                journalEntryLines.SysDeb = ConvertDecimalTwoPlaces(row["SYSDeb"].ToString());
                journalEntryLines.BalDueDeb = journalEntryLines.Debit;
                journalEntryLines.BalDueCred = journalEntryLines.Credit;
                journalEntryLines.BalFcDeb = journalEntryLines.FCDebit;
                journalEntryLines.BalFcCred = journalEntryLines.FCCredit;
                journalEntryLines.BalScCred = journalEntryLines.SysCred;
                journalEntryLines.BalScDeb = journalEntryLines.SysDeb;
                journalEntryLines.UserSign = Properties.Settings.Default.Usuario;
                journalEntryLines.FinncPriod = FinncPriod;
                journalEntryLines.FCCurrency = TransCurr;
                journalEntryLines.DataSource = 'N';

                listaJournalEntryLines.Add(journalEntryLines);

                k++;

            }

            return Tuple.Create(listaJournalEntryLines, k);
        }

        private List<AsientoCabecera> CreateJournalEntry(DataRow row)
        {
            decimal SaldoML = 0;

            List<AsientoCabecera> listaJournalEntry = new List<AsientoCabecera>();

            AsientoCabecera journalEntry = new AsientoCabecera();

            var transIdPreliminar=cn.SelectTransIdPreliminar();           

            TransId = transIdPreliminar.Item1;

            journalEntry.TransId = transIdPreliminar.Item1;
            journalEntry.RefDate = Balance.dpHasta.Value;
            journalEntry.TaxDate = Balance.dpHasta.Value;
            journalEntry.DueDate = Balance.dpHasta.Value;
            journalEntry.Memo = "Diferencia Tipo Cambio";
            journalEntry.BaseRef = 1; //revisar
            journalEntry.Ref1 = "Diferencia Tipo Cambio";
            journalEntry.Ref2 = "Diferencia Tipo Cambio";
            journalEntry.UserSign = Properties.Settings.Default.Usuario;
            journalEntry.UpdateDate = fechaActual.GetFechaActual();
            var result1 = cn.GetPeriodCode(journalEntry.RefDate);
            journalEntry.FinncPriod = result1.Item1;

            if (ConvertDecimalTwoPlaces(row["Diferencia"]) < 0)
            {

                SaldoML = ConvertDecimalTwoPlaces(row["Diferencia"]) * (-1);
                journalEntry.LocTotal = SaldoML;

            }
            else if (ConvertDecimalTwoPlaces(row["Diferencia"]) > 0)
            {
                journalEntry.LocTotal = ConvertDecimalTwoPlaces(row["Diferencia"]);
            }


            journalEntry.SysTotal = journalEntry.LocTotal / ConvertDecimalTwoPlaces(row["Rate"]);

            journalEntry.FcTotal = 0;
            journalEntry.TransType = cn.GetTransType("CB");
            journalEntry.TransCurr = Properties.Settings.Default.MainCurrency;

            listaJournalEntry.Add(journalEntry);

            return listaJournalEntry;

        }

        public void SetDatatable(DataTable dataTable)
        {
            dt = dataTable.Copy();
        }

        private void imgCuentaBeneficioCliente_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Sw = 0;

            var result = cn.ConsultaCuentasNoAsociadas();

            if (result.Item2 == null)
            {
                RecorreListaAccount(result.Item1);
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
            }
        }

        private void imgCuentaPerdidaCliente_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Sw = 1;

            var result = cn.ConsultaCuentasNoAsociadas();

            if (result.Item2 == null)
            {
                RecorreListaAccount(result.Item1);
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
            }
        }

        private void RecorreListaAccount(List<Cuenta> listAccount)
        {
            if (listAccount.Count == 1)
            {
                GetAcctCode(listAccount);


            }
            else if (listAccount.Count == 0)
            {

                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se encontro ningun resultado con los parametros establecidos", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");

            }

            else if (listAccount.Count > 1)
            {
                ListaCuentas ventanaListBox = new ListaCuentas(listAccount);

                ventanaListBox.ShowDialog();

                if (ventanaListBox.Visibility == Visibility.Hidden)
                {
                    if (ventanaListBox.GetListAccount().Count == 0)
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se selecciono ningun elemento", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");

                    }
                    else
                    {

                        GetAcctCode(ventanaListBox.GetListAccount());

                        if (Sw == 0)
                        {
                            txtCuentaBeneficio.Background = Brushes.White;

                            dpCuentaBeneficioCliente.Background = Brushes.White;

                            bdCuentaBeneficioCliente.Background = Brushes.White;

                            imgCuentaBeneficioCliente.Visibility = Visibility.Hidden;


                        }
                        

                        else if (Sw == 1)
                        {

                            txtCuentaPerdida.Background = Brushes.White;

                            dpCuentaPerdidaCliente.Background = Brushes.White;

                            bdCuentaPerdidaCliente.Background = Brushes.White;

                            imgCuentaPerdidaCliente.Visibility = Visibility.Hidden;

                        }

                        

                    }
                }
            }
        }

        private void GetAcctCode(List<Cuenta> listaCuenta)
        {
            foreach (Cuenta cuenta in listaCuenta)
            {
                if (Sw == 0)
                {
                    txtCuentaBeneficio.Text = cuenta.AcctCode;


                }
               

                else if (Sw == 1)
                {
                    txtCuentaPerdida.Text = cuenta.AcctCode;


                }





            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            App.textBox_GotFocus(sender, e);
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            App.textBox_LostFocus(sender, e);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InicializacionBasica();
        }

        public void SetDatetimes(DateTime dpDesdeVencimiento, DateTime dpHastaVencimiento)
        {
            dpDesde = dpDesdeVencimiento;

            dpHasta = dpHastaVencimiento;
        }

        private void InicializacionBasica()
        {
            dpDFechaVencimiento.SelectedDate = null;
            dpHFechaVencimiento.SelectedDate = null;
            txtTipoCambio.Text = "";
        }

        public void LoadedWindow()
        {
            InicializacionBasica();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }
    }
}
