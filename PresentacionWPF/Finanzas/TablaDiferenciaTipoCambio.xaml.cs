using Entidades;
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
    /// Lógica de interacción para TablaDiferenciaTipoCambio.xaml
    /// </summary>
    public partial class TablaDiferenciaTipoCambio : Converter
    {
        ControladorAsiento cj = new ControladorAsiento();

        ControladorDocumento cn = new ControladorDocumento();

        DataTable dt = new DataTable();

        private string cuentaGananciaCliente;

        private string cuentaGananciaProveedores;

        private string cuentaGananciaCuenta;

        private string cuentaPerdidaCliente;

        private string cuentaPerdidaProveedores;

        private string cuentaPerdidaCuenta;
        public string CuentaGananciaCliente { get => cuentaGananciaCliente; set => cuentaGananciaCliente = value; }
        public string CuentaGananciaProveedores { get => cuentaGananciaProveedores; set => cuentaGananciaProveedores = value; }
        public string CuentaGananciaCuenta { get => cuentaGananciaCuenta; set => cuentaGananciaCuenta = value; }
        public string CuentaPerdidaCliente { get => cuentaPerdidaCliente; set => cuentaPerdidaCliente = value; }
        public string CuentaPerdidaProveedores { get => cuentaPerdidaProveedores; set => cuentaPerdidaProveedores = value; }
        public string CuentaPerdidaCuenta { get => cuentaPerdidaCuenta; set => cuentaPerdidaCuenta = value; }

        DataTable dtNewJournalEntry = new DataTable();

        public int TransId { get; private set; }
        

        public TablaDiferenciaTipoCambio()
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

        public void SetDatePicker(DateTime? date)
        {
           lblFechaEjecucionValue.Text=String.Format("{0:yyyy/MM/dd}", date);

        }

        public void GetAccountResult(DataTable listAccountesult)
        {
            dt = SetSeleccionado(listAccountesult);

            dgDiferenciaTipoCambio.ItemsSource = dt.DefaultView;

            dgDiferenciaTipoCambio.CanUserAddRows = false;

            dgDiferenciaTipoCambio.CanUserDeleteRows = false;

            dgDiferenciaTipoCambio.CanUserSortColumns = false;

            dgDiferenciaTipoCambio.IsReadOnly = false;
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

                            var listJournalEntryLines = CreateListJournalEntryLines(listaJournalEntry, row);

                            var result3 = cj.InsertJournalEntryLines(listJournalEntryLines.Item1);

                            if (listJournalEntryLines.Item2 == result3.Item1)
                            {
                                JDT1 = true;

                                cn.UpdateCreditDebitAccount(dtNewJournalEntry);

                                DeleteDataRow(row);

                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Operacion Realizada Exitosamente: ", Brushes.LightGreen, Brushes.Black, "001-interface.png");

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
                    
                

            }

        }

        private void DeleteDataRow(DataRow row)
        {
            dt.Rows.Remove(row);

            dt.AcceptChanges();

            dgDiferenciaTipoCambio.ItemsSource = dt.DefaultView;
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

            List<AsientoCabecera> listaJournalEntry = new List<AsientoCabecera>();

            AsientoCabecera journalEntry = new AsientoCabecera();

            var result = cn.SelectTransId();

            TransId = result.Item1;

            journalEntry.TransId = result.Item1;
            journalEntry.RefDate = Convert.ToDateTime(lblFechaEjecucionValue.Text);
            journalEntry.TaxDate = Convert.ToDateTime(lblFechaEjecucionValue.Text);
            journalEntry.DueDate = Convert.ToDateTime(lblFechaEjecucionValue.Text);
            journalEntry.Memo = txtComentario_SI.Text;
            journalEntry.BaseRef = 1; //revisar
            journalEntry.Ref1 = txtReferencia1_SI.Text;
            journalEntry.Ref2 = txtReferencia2_SI.Text;
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

            newRow["LineMemo"] = txtComentario_SI.Text;


                if (ConvertDecimalTwoPlaces(row["Diferencia"]) > 0) //
                {
                    sw = true;

                    if (row["Type"].ToString() == "N")
                    {
                    newRow["ContraAct"] = CuentaPerdidaCuenta;

                    }
                    else if (row["Type"].ToString() == "S")
                    {
                    newRow["ContraAct"] = CuentaPerdidaProveedores;
                     }
                    else if (row["Type"].ToString() == "C")
                    {
                    newRow["ContraAct"] = CuentaPerdidaCliente;
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

                    newRow["SYSCred"] = SaldoML/ ConvertDecimalTwoPlaces(row["Rate"]);

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
                        newRow["ContraAct"] = CuentaGananciaCuenta;

                    }
                    else if (row["Type"].ToString() == "S")
                    {
                        newRow["ContraAct"] = CuentaGananciaProveedores;
                    }
                    else if (row["Type"].ToString() == "C")
                    {
                        newRow["ContraAct"] = CuentaGananciaCliente;
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

                newRow1["LineMemo"] = txtComentario_SI.Text;

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

        public void SetAccounts(string cuentaGananciaCliente, string cuentaGananciaProveedores, string cuentaGananciaCuenta, string cuentaPerdidaCliente, string cuentaPerdidaProveedores, string cuentaPerdidaCuenta)
        {
            CuentaGananciaCliente = cuentaGananciaCliente;

            CuentaGananciaProveedores = cuentaGananciaProveedores;

            CuentaGananciaCuenta = cuentaGananciaCuenta;

            CuentaPerdidaCliente = cuentaPerdidaCliente;

            CuentaPerdidaProveedores = cuentaPerdidaProveedores;

            CuentaPerdidaCuenta = cuentaPerdidaCuenta;

        }

        private void Converter_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
