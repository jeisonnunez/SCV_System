using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para TablaCierrePeriodo.xaml
    /// </summary>
    public partial class TablaCierrePeriodo : Converter
    {
        ControladorAsiento cj = new ControladorAsiento();

        ControladorDocumento cn = new ControladorDocumento();

        DataTable dt = new DataTable();

        private string cuentaArrastre;

        private string cuentaCierre;

        public string CuentaArrastre { get => cuentaArrastre; set => cuentaArrastre = value; }
        public string CuentaCierre { get => cuentaCierre; set => cuentaCierre = value; }
        
        DataTable dtNewJournalEntry = new DataTable();

        public int TransId { get; private set; }


        public TablaCierrePeriodo()
        {
            InitializeComponent();
        }

        public void ClearDatatable()
        {
            dt.Rows.Clear();

        }

        

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {

            App.textBox_GotFocus(sender, e);
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            App.textBox_LostFocus(sender, e);
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        public void SetDatePicker(DateTime dpDesde, DateTime dpHasta)
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

        public void GetAccountResult(DataTable listAccountesult)
        {
            dt = SetSeleccionado(listAccountesult);

            dgCierrePeriodo.ItemsSource = dt.DefaultView;

            dgCierrePeriodo.CanUserAddRows = false;

            dgCierrePeriodo.CanUserDeleteRows = false;

            dgCierrePeriodo.CanUserSortColumns = false;

            dgCierrePeriodo.IsReadOnly = false;
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

        private DataTable SetSeleccionado(DataTable dt)
        {

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {

                    if (column.ToString() == "Seleccionado")
                    {
                        row["Seleccionado"] = false;

                    }

                }

            }

            return dt;
        }

        private void SetChecked()
        {

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {

                    if (column.ToString() == "Seleccionado")
                    {
                        row["Seleccionado"] = true;

                    }

                }

            }


        }

        private void SetUnChecked()
        {

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {

                    if (column.ToString() == "Seleccionado")
                    {
                        row["Seleccionado"] = false;

                    }

                }

            }


        }

       

        private void chkSelectAll_Checked(object sender, RoutedEventArgs e)
        {
            SetChecked();
        }

        private void chkSelectAll_Unchecked(object sender, RoutedEventArgs e)
        {
            SetUnChecked();
        }

        private void seleccionado_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void seleccionado_Unchecked(object sender, RoutedEventArgs e)
        {

        }

       

        private void btnEjecutar_Click(object sender, RoutedEventArgs e)
        {
           
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];

                if (Convert.ToBoolean(row["Seleccionado"]) == true)
                {
                    List<AsientoCabecera> listaJournalEntry = new List<AsientoCabecera>();

                    AsientoCabecera journalEntry = new AsientoCabecera();

                    List<AsientoDetalle> listaJournalEntryLines = new List<AsientoDetalle>();

                    if (row["ActType"].ToString() == "E" || row["ActType"].ToString()=="I") //verifica si es cuenta real
                    {
                        listaJournalEntry.Clear();

                        bool? OJDT = null;

                        bool? JDT1 = null;

                        bool? OACT = null;

                        //Create Journal Entry

                        listaJournalEntry = CreateJournalEntry(row);

                        //Contruir asiento

                        var result2 = cj.InsertJournalEntry(listaJournalEntry);

                        if (result2.Item1 == 1)
                        {
                            OJDT = true;

                            var listJournalEntryLines = CreateListJournalEntryLines(listaJournalEntry,row);

                            var result3 = cj.InsertJournalEntryLines(listJournalEntryLines.Item1);

                            if (listJournalEntryLines.Item2 == result3.Item1)
                            {
                                JDT1 = true;

                                cn.UpdateCreditDebitAccount(dtNewJournalEntry);

                                DeleteDataRow(row);

                                i--;
                            }
                            else
                            {
                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: " + result3.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");

                                JDT1 = false;

                                DeletedAllInsert(OJDT, JDT1, OACT);
                            }

                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: " + result2.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                            OJDT = false;
                            DeletedAllInsert(OJDT, JDT1, OACT);
                        }
                    }
                    else if(row["ActType"].ToString() == "N")//verifica si es cuenta de ingreso o gasto
                    {
                        listaJournalEntry.Clear();

                        bool? OJDT = null;

                        bool? JDT1 = null;

                        bool? OACT = null;

                        //Create Journal Entry

                        listaJournalEntry = CreateJournalEntry(row);

                        //Contruir asiento

                        var result2 = cj.InsertJournalEntry(listaJournalEntry);

                        if (result2.Item1 == 1)
                        {
                            OJDT = true;

                            var listJournalEntryLines = CreateListJournalEntryLines(listaJournalEntry,row);

                            var result3 = cj.InsertJournalEntryLines(listJournalEntryLines.Item1);

                            if (listJournalEntryLines.Item2 == result3.Item1)
                            {
                                JDT1 = true;

                                cn.UpdateCreditDebitAccount(dtNewJournalEntry);


                                //LimpiarCampos();

                            }
                            else
                            {
                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: " + result3.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");

                                JDT1 = false;

                                DeletedAllInsert(OJDT, JDT1, OACT);
                            }

                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: " + result2.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                            OJDT = false;
                            DeletedAllInsert(OJDT, JDT1, OACT);
                        }

                        if (JDT1 == true)
                        {
                            listaJournalEntry.Clear();

                            bool? OJDT1 = null;

                            bool? JDT11 = null;

                            bool? OACT1 = null;

                            //Create Journal Entry

                            listaJournalEntry = CreateJournalEntryOpen(row);

                            //Contruir asiento

                            var result3 = cj.InsertJournalEntry(listaJournalEntry);

                            if (result3.Item1 == 1)
                            {
                                OJDT1 = true;

                                var listJournalEntryLines = CreateListJournalEntryLinesOpen(listaJournalEntry,row);

                                var result4 = cj.InsertJournalEntryLines(listJournalEntryLines.Item1);

                                if (listJournalEntryLines.Item2 == result4.Item1)
                                {
                                    JDT11 = true;

                                    cn.UpdateCreditDebitAccount(dtNewJournalEntry);

                                    DeleteDataRow(row);

                                    i--;

                                }
                                else
                                {
                                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: " + result4.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");

                                    JDT11 = false;

                                    DeletedAllInsert(OJDT1, JDT11, OACT1);
                                }

                            }
                            else
                            {
                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: " + result3.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                                OJDT1 = false;
                                DeletedAllInsert(OJDT1, JDT11, OACT1);
                            }
                        }
                    }

                    

                }
                
            }
           
        }

        private void DeleteDataRow(DataRow row)
        {
            dt.Rows.Remove(row);

            dt.AcceptChanges();           

            dgCierrePeriodo.ItemsSource = dt.DefaultView;
        }

        private void DeletedAllInsert(bool? OJDT, bool? JDT1, bool? OACT)
        {

            if (JDT1 != null)
            {
                var deleteJournalEntryLinesSalesInvoice = cn.DeleteJournalEntryLines(TransId);

                if (deleteJournalEntryLinesSalesInvoice.Item2 != null)
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se eliminaron todas las lineas del asiento : " + TransId, Brushes.LightBlue, Brushes.Black, "003-interface-2.png");
                }
            }

            if (OJDT == true)
            {
                var deleteJournalEntrySalesInvoice = cn.DeleteJournalEntry(TransId);

                if (deleteJournalEntrySalesInvoice.Item2 != null)
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se elimino el asiento: " + TransId, Brushes.LightBlue, Brushes.Black, "003-interface-2.png");
                }
            }

            if (OACT != null)
            {

            }

        }

        private List<AsientoCabecera> CreateJournalEntry(DataRow row)
        {
            decimal SaldoML = 0;

            decimal SaldoMS = 0;
           
            List<AsientoCabecera> listaJournalEntry = new List<AsientoCabecera>();

            AsientoCabecera journalEntry = new AsientoCabecera();

            var result = cn.SelectTransId();

            TransId = result.Item1;

            journalEntry.TransId = result.Item1;
            journalEntry.RefDate = dpFechaHasta.SelectedDate;
            journalEntry.TaxDate = dpFechaDocumento.SelectedDate;
            journalEntry.DueDate = dpFechaVencimiento.SelectedDate;
            journalEntry.Memo = txtComentario.Text;
            var baseRef = cn.GetBaseRef("-3");

            journalEntry.BaseRef = baseRef.Item1;
            journalEntry.Ref1 = txtReferencia1.Text;
            journalEntry.Ref2 = txtReferencia2.Text;
            journalEntry.UserSign = Properties.Settings.Default.Usuario;
            journalEntry.UpdateDate = fechaActual.GetFechaActual();
            var result1 = cn.GetPeriodCode(journalEntry.RefDate);           
            journalEntry.FinncPriod = result1.Item1;

            if (ConvertDecimalTwoPlaces(row["SaldoML"]) < 0){

                SaldoML = ConvertDecimalTwoPlaces(row["SaldoML"]) * (-1);
                journalEntry.LocTotal = SaldoML;

            }
            else if(ConvertDecimalTwoPlaces(row["SaldoML"]) > 0)
            {
                journalEntry.LocTotal = ConvertDecimalTwoPlaces(row["SaldoML"]);
            }

            if (ConvertDecimalTwoPlaces(row["SaldoMS"]) < 0)
            {

                SaldoMS = ConvertDecimalTwoPlaces(row["SaldoMS"]) * (-1);
                journalEntry.SysTotal = SaldoMS;

            }
            else if(ConvertDecimalTwoPlaces(row["SaldoMS"]) > 0){

                journalEntry.SysTotal = ConvertDecimalTwoPlaces(row["SaldoMS"]);
            }

            journalEntry.FcTotal = ConvertDecimalTwoPlaces(row["SaldoME"]);
            journalEntry.TransType = cn.GetTransType("CB");
            journalEntry.TransCurr = Properties.Settings.Default.MainCurrency;

            listaJournalEntry.Add(journalEntry);

            return listaJournalEntry;

        }

        private List<AsientoCabecera> CreateJournalEntryOpen(DataRow row)
        {
            decimal SaldoML = 0;

            decimal SaldoMS = 0;

            List<AsientoCabecera> listaJournalEntry = new List<AsientoCabecera>();

            AsientoCabecera journalEntry = new AsientoCabecera();

            var result = cn.SelectTransId();           

            TransId = result.Item1;
            journalEntry.TransId = result.Item1;
            journalEntry.RefDate = dpFechaContabilizacion_SI.SelectedDate;
            journalEntry.TaxDate = dpFechaDocumento_SI.SelectedDate;
            journalEntry.DueDate = dpFechaVencimiento_SI.SelectedDate;
            journalEntry.Memo = txtComentario_SI.Text;
            var baseRef = cn.GetBaseRef("-2");

            journalEntry.BaseRef = baseRef.Item1;
            journalEntry.Ref1 = txtReferencia1_SI.Text;
            journalEntry.Ref2 = txtReferencia2_SI.Text;
            journalEntry.UserSign = Properties.Settings.Default.Usuario;
            journalEntry.UpdateDate = fechaActual.GetFechaActual();
            var result1 = cn.GetPeriodCode(journalEntry.RefDate);
            journalEntry.FinncPriod = result1.Item1;

            if (ConvertDecimalTwoPlaces(row["SaldoML"]) < 0)
            {

                SaldoML = ConvertDecimalTwoPlaces(row["SaldoML"]) * (-1);
                journalEntry.LocTotal = SaldoML;

            }
            else if (ConvertDecimalTwoPlaces(row["SaldoML"]) > 0)
            {
                journalEntry.LocTotal = ConvertDecimalTwoPlaces(row["SaldoML"]);
            }

            if (ConvertDecimalTwoPlaces(row["SaldoMS"]) < 0)
            {

                SaldoMS = ConvertDecimalTwoPlaces(row["SaldoMS"]) * (-1);
                journalEntry.SysTotal = SaldoMS;

            }
            else if (ConvertDecimalTwoPlaces(row["SaldoMS"]) > 0)
            {

                journalEntry.SysTotal = ConvertDecimalTwoPlaces(row["SaldoMS"]);
            }

            journalEntry.FcTotal = ConvertDecimalTwoPlaces(row["SaldoME"]);
            journalEntry.TransType = cn.GetTransType("SI");
            journalEntry.TransCurr = Properties.Settings.Default.MainCurrency;

            listaJournalEntry.Add(journalEntry);

            return listaJournalEntry;

        }

        private DataTable CreateDatatableJournalEntryLines(DataRow row)
        {
            decimal SaldoML = 0;

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

            if (row["ActType"].ToString() == "N")//verifica si es cuenta real
            {
                //Create Row Cuenta a tratar

                DataRow newRow = dtResult.NewRow();

                newRow["ShortName"] = row["ShortName"];

                newRow["Account"] = row["Account"];

                newRow["LineMemo"] = txtComentario.Text;

                newRow["ContraAct"] = CuentaArrastre;

                if (ConvertDecimalTwoPlaces(row["SaldoML"]) > 0) //
                {
                    sw = true;

                    if(ConvertDecimalTwoPlaces(row["SaldoML"]) > 0)
                    {
                        SaldoML = ConvertDecimalTwoPlaces(row["SaldoML"]);
                    }
                    else if(ConvertDecimalTwoPlaces(row["SaldoML"]) < 0)
                    {
                        SaldoML = ConvertDecimalTwoPlaces(row["SaldoML"]) * (-1);
                    }

                    newRow["Credit"] = SaldoML;

                    if (ConvertDecimalTwoPlaces(row["SaldoMS"]) > 0)
                    {
                        SaldoMS = ConvertDecimalTwoPlaces(row["SaldoMS"]);
                    }
                    else if (ConvertDecimalTwoPlaces(row["SaldoMS"]) < 0)
                    {
                        SaldoMS = ConvertDecimalTwoPlaces(row["SaldoMS"]) * (-1);
                    }

                    newRow["SYSCred"] = SaldoMS;

                    newRow["FCCredit"] = ConvertDecimalTwoPlaces(row["SaldoME"]);

                    newRow["Debit"] = 0;

                    newRow["SYSDeb"] = 0;

                    newRow["FCDebit"] = 0;
                }
                else if (ConvertDecimalTwoPlaces(row["SaldoML"]) < 0)
                {
                    sw = false;

                    if (ConvertDecimalTwoPlaces(row["SaldoML"]) > 0)
                    {
                        SaldoML = ConvertDecimalTwoPlaces(row["SaldoML"]);
                    }
                    else if (ConvertDecimalTwoPlaces(row["SaldoML"]) < 0)
                    {
                        SaldoML = ConvertDecimalTwoPlaces(row["SaldoML"]) * (-1);
                    }

                    newRow["Debit"] = SaldoML;

                    if (ConvertDecimalTwoPlaces(row["SaldoMS"]) > 0)
                    {
                        SaldoMS = ConvertDecimalTwoPlaces(row["SaldoMS"]);
                    }
                    else if (ConvertDecimalTwoPlaces(row["SaldoMS"]) < 0)
                    {
                        SaldoMS = ConvertDecimalTwoPlaces(row["SaldoMS"]) * (-1);
                    }

                    newRow["SYSDeb"] = SaldoMS;

                    newRow["FCDebit"] = row["SaldoME"];

                    newRow["Credit"] = 0;

                    newRow["SYSCred"] = 0;

                    newRow["FCCredit"] = 0;
                }


                dtResult.Rows.Add(newRow);


                //Create Row Cuenta a Arrastre

                DataRow newRow1 = dtResult.NewRow();

                newRow1["ShortName"] = CuentaArrastre;

                newRow1["Account"] = CuentaArrastre;

                newRow1["ContraAct"] = newRow["ShortName"];

                newRow1["LineMemo"] = txtComentario.Text;

                if (sw == true)
                {
                    newRow1["Debit"] = SaldoML;

                    newRow1["SYSDeb"] = SaldoMS;

                    newRow1["FCDebit"] = newRow["FCCredit"];

                    newRow1["Credit"] = 0;

                    newRow1["SYSCred"] = 0;

                    newRow1["FCCredit"] = 0;
                }
                else if (sw == false)
                {
                    newRow1["Credit"] = SaldoML;

                    newRow1["SYSCred"] = SaldoMS;

                    newRow1["FCCredit"] = newRow["FCDebit"];

                    newRow1["Debit"] = 0;

                    newRow1["SYSDeb"] = 0;

                    newRow1["FCDebit"] = 0;
                }


                dtResult.Rows.Add(newRow1);


            }
            else if (row["ActType"].ToString() == "E" || row["ActType"].ToString() == "I")//verifica si es cuenta de ingreso o gasto
            {
                //Create Row Cuenta a tratar

                DataRow newRow = dtResult.NewRow();

                newRow["ShortName"] = row["ShortName"];

                newRow["Account"] = row["Account"];

                newRow["LineMemo"] = txtComentario.Text;

                newRow["ContraAct"] = CuentaCierre;

                if (ConvertDecimalTwoPlaces(row["SaldoML"]) > 0) //
                {
                    sw = true;

                    if (ConvertDecimalTwoPlaces(row["SaldoML"]) > 0)
                    {
                        SaldoML = ConvertDecimalTwoPlaces(row["SaldoML"]);
                    }
                    else if (ConvertDecimalTwoPlaces(row["SaldoML"]) < 0)
                    {
                        SaldoML = ConvertDecimalTwoPlaces(row["SaldoML"]) * (-1);
                    }

                    newRow["Credit"] = SaldoML;

                    if (ConvertDecimalTwoPlaces(row["SaldoMS"]) > 0)
                    {
                        SaldoMS = ConvertDecimalTwoPlaces(row["SaldoMS"]);
                    }
                    else if (ConvertDecimalTwoPlaces(row["SaldoMS"]) < 0)
                    {
                        SaldoMS = ConvertDecimalTwoPlaces(row["SaldoMS"]) * (-1);
                    }

                    newRow["SYSCred"] = SaldoMS;

                    newRow["FCCredit"] = row["SaldoME"];

                    newRow["Debit"] = 0;

                    newRow["SYSDeb"] = 0;

                    newRow["FCDebit"] = 0;
                }
                else if (ConvertDecimalTwoPlaces(row["SaldoML"]) < 0)
                {
                    sw = false;

                    if (ConvertDecimalTwoPlaces(row["SaldoML"]) > 0)
                    {
                        SaldoML = ConvertDecimalTwoPlaces(row["SaldoML"]);
                    }
                    else if (ConvertDecimalTwoPlaces(row["SaldoML"]) < 0)
                    {
                        SaldoML = ConvertDecimalTwoPlaces(row["SaldoML"]) * (-1);
                    }

                    newRow["Debit"] = SaldoML;

                    if (ConvertDecimalTwoPlaces(row["SaldoMS"]) > 0)
                    {
                        SaldoMS = ConvertDecimalTwoPlaces(row["SaldoMS"]);
                    }
                    else if (ConvertDecimalTwoPlaces(row["SaldoMS"]) < 0)
                    {
                        SaldoMS = ConvertDecimalTwoPlaces(row["SaldoMS"]) * (-1);
                    }

                    newRow["SYSDeb"] = SaldoMS;

                    newRow["FCDebit"] = row["SaldoME"];

                    newRow["Credit"] = 0;

                    newRow["SYSCred"] = 0;

                    newRow["FCCredit"] = 0;
                }


                dtResult.Rows.Add(newRow);


                //Create Row Cuenta a Arrastre

                DataRow newRow1 = dtResult.NewRow();

                newRow1["ShortName"] = CuentaCierre;

                newRow1["Account"] = CuentaCierre;

                newRow1["ContraAct"] = newRow["ShortName"];

                newRow1["LineMemo"] = txtComentario.Text;

                if (sw == true)
                {
                    newRow1["Debit"] = SaldoML;

                    newRow1["SYSDeb"] = SaldoMS;

                    newRow1["FCDebit"] = newRow["FCCredit"];

                    newRow1["Credit"] = 0;

                    newRow1["SYSCred"] = 0;

                    newRow1["FCCredit"] = 0;
                }
                else if (sw == false)
                {
                    newRow1["Credit"] = SaldoML;

                    newRow1["SYSCred"] = SaldoMS;

                    newRow1["FCCredit"] = newRow["FCDebit"];

                    newRow1["Debit"] = 0;

                    newRow1["SYSDeb"] = 0;

                    newRow1["FCDebit"] = 0;
                }


                dtResult.Rows.Add(newRow1);
            }

            SaldoML = 0;

            SaldoMS = 0;

            return dtResult;
        }

        private DataTable CreateDatatableJournalEntryLinesOpen(DataRow row)
        {
            decimal SaldoML = 0;

            decimal SaldoMS = 0;

            bool? sw=null;

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

            if (row["ActType"].ToString() == "N")//verifica si es cuenta real
            {
                //Create Row Cuenta a tratar

                DataRow newRow = dtResult.NewRow();

                newRow["ShortName"] = row["ShortName"];

                newRow["Account"] = row["Account"];

                newRow["LineMemo"] = txtComentario_SI.Text;

                newRow["ContraAct"] = CuentaArrastre;

                if (ConvertDecimalTwoPlaces(row["SaldoML"]) < 0) //
                {
                    sw = true;

                    if (ConvertDecimalTwoPlaces(row["SaldoML"]) > 0)
                    {
                        SaldoML = ConvertDecimalTwoPlaces(row["SaldoML"]);
                    }
                    else if (ConvertDecimalTwoPlaces(row["SaldoML"]) < 0)
                    {
                        SaldoML = ConvertDecimalTwoPlaces(row["SaldoML"]) * (-1);
                    }

                    newRow["Credit"] = SaldoML;

                    if (ConvertDecimalTwoPlaces(row["SaldoMS"]) > 0)
                    {
                        SaldoMS = ConvertDecimalTwoPlaces(row["SaldoMS"]);
                    }
                    else if (ConvertDecimalTwoPlaces(row["SaldoMS"]) < 0)
                    {
                        SaldoMS = ConvertDecimalTwoPlaces(row["SaldoMS"]) * (-1);
                    }

                    newRow["SYSCred"] = SaldoMS;

                    newRow["FCCredit"] = row["SaldoME"];

                    newRow["Debit"] = 0;

                    newRow["SYSDeb"] = 0;

                    newRow["FCDebit"] = 0;
                }
                else if (ConvertDecimalTwoPlaces(row["SaldoML"]) > 0) 
                {
                    sw = false;

                    if (ConvertDecimalTwoPlaces(row["SaldoML"]) > 0)
                    {
                        SaldoML = ConvertDecimalTwoPlaces(row["SaldoML"]);
                    }
                    else if (ConvertDecimalTwoPlaces(row["SaldoML"]) < 0)
                    {
                        SaldoML = ConvertDecimalTwoPlaces(row["SaldoML"]) * (-1);
                    }

                    newRow["Debit"] = SaldoML;

                    if (ConvertDecimalTwoPlaces(row["SaldoMS"]) > 0)
                    {
                        SaldoMS = ConvertDecimalTwoPlaces(row["SaldoMS"]);
                    }
                    else if (ConvertDecimalTwoPlaces(row["SaldoMS"]) < 0)
                    {
                        SaldoMS = ConvertDecimalTwoPlaces(row["SaldoMS"]) * (-1);
                    }

                    newRow["SYSDeb"] = SaldoMS;

                    newRow["FCDebit"] = row["SaldoME"];

                    newRow["Credit"] = 0;

                    newRow["SYSCred"] = 0;

                    newRow["FCCredit"] = 0;
                }

                
                dtResult.Rows.Add(newRow);


                //Create Row Cuenta a Arrastre

                DataRow newRow1 = dtResult.NewRow();

                newRow1["ShortName"] = CuentaArrastre;

                newRow1["Account"] = CuentaArrastre;

                newRow1["ContraAct"] = newRow["ShortName"];

                newRow1["LineMemo"] = txtComentario_SI.Text;

                if (sw == true)
                {
                    newRow1["Debit"] = SaldoML;

                    newRow1["SYSDeb"] = SaldoMS;

                    newRow1["FCDebit"] = newRow["FCCredit"];

                    newRow1["Credit"] = 0;

                    newRow1["SYSCred"] = 0;

                    newRow1["FCCredit"] = 0;
                }
                else if (sw == false)
                {
                    newRow1["Credit"] = SaldoML;

                    newRow1["SYSCred"] = SaldoMS;

                    newRow1["FCCredit"] = newRow["FCDebit"];

                    newRow1["Debit"] = 0;

                    newRow1["SYSDeb"] = 0;

                    newRow1["FCDebit"] = 0;
                }


                dtResult.Rows.Add(newRow1);


            }

            SaldoML = 0;

            SaldoMS = 0;
            
            return dtResult;
        }

        private Tuple<List<AsientoDetalle>, int> CreateListJournalEntryLinesOpen(List<AsientoCabecera> listaJournalEntry, DataRow rowActual)
        {
            
            dtNewJournalEntry = CreateDatatableJournalEntryLinesOpen(rowActual);

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
                journalEntryLines.UserSign = Properties.Settings.Default.Usuario;
                journalEntryLines.FinncPriod = FinncPriod;
                journalEntryLines.FCCurrency = TransCurr;
                journalEntryLines.DataSource = 'N';

                listaJournalEntryLines.Add(journalEntryLines);

                k++;

            }

            return Tuple.Create(listaJournalEntryLines, k);
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

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void imgReturn_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
        }

        public void SetAccounts(string cuentaArrastre, string cuentaCierre)
        {
            CuentaArrastre = cuentaArrastre;

            CuentaCierre = cuentaCierre;
        }
    }
}
