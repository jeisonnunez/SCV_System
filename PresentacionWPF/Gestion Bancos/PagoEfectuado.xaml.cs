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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Entidades;
using Negocio;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para PagoEfectuado.xaml
    /// </summary>
    public partial class PagoEfectuado : Document
    {

        ControladorPagoEfectuado cn = new ControladorPagoEfectuado();

        ControladorFacturaProveedores cp = new ControladorFacturaProveedores();

        ControladorAsiento cj = new ControladorAsiento();

        DataTable dtWithHoldingTax = new DataTable();

        private int docNum;

        private string supplier;

        private string vatGroup;

        private string cuentaAsociada;

        private int docEntry;

        private List<Payment> listaPayment = new List<Payment>();

        private DataTable listaPaymentLines = new DataTable();

        private bool sw;

        private string selectedDate;

        private decimal rate;

        private decimal rateFC;

        private string currency;

        private int transId;

        DataTable dtJournalEntry = new DataTable();

        DataTable dt = new DataTable();

        DataTable dtDocuments = new DataTable();

        private string str;

        private string strNumber;

        private decimal amount;

        private string acctCode;

        private int docEntryPayment;
        public string Supplier { get => supplier; set => supplier = value; }
        public string VatGroup { get => vatGroup; set => vatGroup = value; }
        public string CuentaAsociada { get => cuentaAsociada; set => cuentaAsociada = value; }
        public List<Payment> ListaPayment { get => listaPayment; set => listaPayment = value; }
        public DataTable ListaPaymentLines { get => listaPaymentLines; set => listaPaymentLines = value; }
        public int DocEntry { get => docEntry; set => docEntry = value; }
        public bool Sw { get => sw; set => sw = value; }
        public string SelectedDate { get => selectedDate; set => selectedDate = value; }
        public decimal Rate { get => rate; set => rate = value; }
        public decimal RateFC { get => rateFC; set => rateFC = value; }
        public string Currency { get => currency; set => currency = value; }
        public int TransId { get => transId; set => transId = value; }       
        public int DocNum { get => docNum; set => docNum = value; }
        public string U_IDA_CompIVA { get; private set; }
        public decimal U_IDA_MontoCompIVA { get; private set; }
        public string U_IDA_CompISLR { get; private set; }
        public DateTime? U_IDA_DateCompISLR { get; private set; }
        public DateTime? U_IDA_FechaComp { get; private set; }
        public string AcctCode { get => acctCode; set => acctCode = value; }
        public decimal SysPurchase { get => sysPurchase; set => sysPurchase = value; }
        public decimal RatePurchase { get; private set; }
        public decimal RatePurchaseFC { get; private set; }
        public decimal FCPurchase { get => fCPurchase; set => fCPurchase = value; }
        public decimal LocalPurchase { get => localPurchase; set => localPurchase = value; }
        public decimal DocRatePayment { get; private set; }
        public decimal SysRatePayment { get; private set; }
        public decimal RatePayment { get; private set; }
        public string DocCur { get; private set; }
        public int DocEntryPayment { get => docEntryPayment; set => docEntryPayment = value; }
        public string CurrNamePayment { get; private set; }
        public string DocCurPayment { get; private set; }
        public List<Payment> Payment { get => payment; set => payment = value; }
        public int DocNumPayment { get; private set; }

        private decimal sysPurchase;

        private decimal fCPurchase;

        private decimal localPurchase;

        private List<Payment> payment;

        public List<DocumentoCabecera> ListaPurchaseInvoice = new List<DocumentoCabecera>();

        private DataTable dtNewJournalEntry=new DataTable();

        public PagoEfectuado()
        {
            InitializeComponent();
        }        

        private void cbxPagoCuenta_Checked(object sender, RoutedEventArgs e)
        {
            lblCuentaAsociada.Visibility = Visibility.Visible;
            txtCuentaAsociada.Visibility = Visibility.Visible;
            txtPagoCuenta.Background = Brushes.White;
            txtPagoCuenta.IsReadOnly = false;

            SetStateDatatableDisabled();

           // UnchechedDatatable(dt);
        }

        private void SetStateDatatableEnabled()
        {
            
            EnabledCheckBoxDatatable();

            dgPagoEfectuado.IsReadOnly = false;
            
        }

        private void EnabledCheckBoxDatatable()
        {
            var row_list = GetDataGridRows(dgPagoEfectuado);

            DataRowView row_Selected = dgPagoEfectuado.SelectedItem as DataRowView;

            foreach (DataGridRow single_row in row_list)
            {
                CheckBox checkBox = FindChild<CheckBox>(single_row, "seleccionado");

                checkBox.IsEnabled = true;
            }
        }

        private void DisabledCheckBoxDatatable()
        {
            var row_list = GetDataGridRows(dgPagoEfectuado);

            DataRowView row_Selected = dgPagoEfectuado.SelectedItem as DataRowView;

            foreach (DataGridRow single_row in row_list)
            {
                CheckBox checkBox = FindChild<CheckBox>(single_row, "seleccionado");

                checkBox.IsEnabled = false;
            }
        }

        private void UnchechedDatatable(DataTable dt)
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

        private void cbxPagoCuenta_Unchecked(object sender, RoutedEventArgs e)
        {
            lblCuentaAsociada.Visibility = Visibility.Hidden;
            txtCuentaAsociada.Visibility = Visibility.Hidden;
            txtPagoCuenta.Background = Brushes.LightGray;
            txtPagoCuenta.IsReadOnly = true;

            SetStateDatatableEnabled();
        }

        private void SetStateDatatableDisabled()
        {
            DisabledCheckBoxDatatable();

            dgPagoEfectuado.IsReadOnly = true;
        }

        private void textBox_LostFocusCuentaAsociada(object sender, RoutedEventArgs e)
        {
            if (btnCrear.Content.ToString() == "Actualizar" || btnCrear.Content.ToString() == "Crear")
            {
                if (txtCuentaAsociada.Visibility == Visibility.Visible)
                {
                    App.textBox_LostFocus(sender, e);
                }

            }

        }

        private void textBox_GotFocusCuentaAsociada(object sender, RoutedEventArgs e)
        {
            if (btnCrear.Content.ToString() == "Actualizar" || btnCrear.Content.ToString() == "Crear")
            {
                if (txtCuentaAsociada.Visibility == Visibility.Visible)
                {
                    App.textBox_GotFocus(sender, e);
                }

            }

        }

        private void textBox_LostFocusPagoCuenta(object sender, RoutedEventArgs e)
        {
            if (btnCrear.Content.ToString() == "Actualizar" || btnCrear.Content.ToString() == "Crear")
            {
                if (txtPagoCuenta.IsReadOnly == true)
                {
                    App.textBox_LostFocus(sender, e);
                }

            }

        }

        private void ReadOnlyDatetime(DatePicker dp)
        {
            TextBox tb = (TextBox)dp.Template.FindName("PART_TextBox", dp);

            tb.IsReadOnly = true;
            tb.Background = Brushes.LightGray;
        }

        public void establecerDatetime(DatePicker dpk)
        {
            TextBox tb = (TextBox)dpk.Template.FindName("PART_TextBox", dpk);

            tb.BorderThickness = new Thickness(1);
            tb.Background = Brushes.LightBlue;
            tb.BorderBrush = Brushes.Gray;

        }

        private void textBox_GotFocusPagoCuenta(object sender, RoutedEventArgs e)
        {
            if (btnCrear.Content.ToString() == "Actualizar" || btnCrear.Content.ToString() == "Crear")
            {
                if (txtPagoCuenta.IsReadOnly == true)
                {
                    App.textBox_GotFocus(sender, e);
                }

            }

        }


        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (btnCrear.Content.ToString() != "Buscar")
                App.textBox_LostFocus(sender, e);
        }

        private void textBox_LostFocus1(object sender, RoutedEventArgs e)
        {
            if (btnCrear.Content.ToString() == "Actualizar" || btnCrear.Content.ToString() == "Crear")
            {

                App.textBox_LostFocus(sender, e);
            }
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (btnCrear.Content.ToString() != "Buscar")
                App.textBox_GotFocus(sender, e);
        }

        private void textBox_GotFocus1(object sender, RoutedEventArgs e)
        {
            if (btnCrear.Content.ToString() == "Actualizar" || btnCrear.Content.ToString() == "Crear")
            {

                App.textBox_GotFocus(sender, e);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Sw = true;

            ClearDataTableWithHoldingTax();

            ClearDatataTableDocuments();

            App.Window_Closing(sender, e);
        }

        private void btnWindow_Close(object sender, RoutedEventArgs e)
        {
            Sw = true;

            ClearDataTableWithHoldingTax();

            ClearDatataTableDocuments();

            this.Hide();

           
        }

        public void LoadedWindow()
        {
            InicializacionBasic();
        }

        private void InicializacionBasic()
        {
            btnCrear.Content = "Crear";

            LimpiarCampos();

            ReestablecerFondo();

            EnabledDatepicker();

            InicialiacionBasica();

            Sw = true;

            dgPagoEfectuado.CanUserAddRows = true;

            dgPagoEfectuado.CanUserDeleteRows = true;

            dgPagoEfectuado.CanUserSortColumns = true;

            dt.Rows.Clear();

            dgPagoEfectuado.ItemsSource = dt.Rows;
        }

        private void imgCrear_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            InicializacionBasic();
        }

        public void InicialiacionBasica()
        {
            dpContabilizacion.SelectedDate = fechaActual.GetFechaActual();

            dpVencimiento.SelectedDate = fechaActual.GetFechaActual();

            dpDocumento.SelectedDate = fechaActual.GetFechaActual();

            Sw = true;

            var result = cn.SelectDocNum();

            if (result.Item2 == null)
            {
                txtNro.Text = result.Item1.ToString();
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
            }

            var result1 = cn.SelectTransId();

            if (result1.Item2 == null)
            {
                TransId = result1.Item1;
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result1.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
            }

           
        }

        private void imgFin_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (btnCrear.Content.ToString() == "OK" || btnCrear.Content.ToString() == "Crear" || btnCrear.Content.ToString() == "Buscar")
            {
                ReestablecerFondo();

                ListaPayment.Clear();

                ListaPaymentLines.Clear();

                var result = cn.FindLastPaymentMade();

                if (result.Item2 == null)
                {
                    ListaPayment = result.Item1;

                    GetPayment(ListaPayment);

                    VerificaCurrency();

                    var result1 = cn.FindPaymentMadeLines(DocEntryPayment);

                    if (result1.Item3 == null)
                    {
                        var result3 = cn.FindPurchaseInvoiceSupplierSpecific(result1.Item1, result1.Item2);

                        if (result3.Item2 == null)
                        {
                            ListaPaymentLines = result3.Item1;

                            GetPaymentLinesReadOnly(ListaPaymentLines);

                            Sw = false;

                            SetStateDatatableDisabled();

                            ClearDataTableWithHoldingTax();

                            ClearDatataTableDocuments();

                            btnCrear.Content = "OK";
                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result3.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                        }

                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result1.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                    }


                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                }
            }

            else if (btnCrear.Content.ToString() == "Actualizar")
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("¿Desea salir sin guardar cambios?", "Journal Entry", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    btnCrear.Content = "OK";

                }
            }
        }

        private void GetPaymentLines(DataTable listaPaymentLines)
        {
            dt = AddCurrencyCode(listaPaymentLines);

            dgPagoEfectuado.ItemsSource = dt.DefaultView;

            dgPagoEfectuado.CanUserAddRows = false;

            dgPagoEfectuado.CanUserDeleteRows = false;

            dgPagoEfectuado.CanUserSortColumns = false;

            dgPagoEfectuado.IsReadOnly = false;
        }

        private DataTable AddCurrencyCode(DataTable dt)
        {
            
            foreach (DataRow row in dt.Rows)
            {

                row["PaidWithHoldingTax"] = row["DocCur"] + " " + ConvertDecimalTwoPlaces(row["Paid"].ToString());

                row["PaidFCWithHoldingTax"] = row["DocCur"] + " " + ConvertDecimalTwoPlaces(row["PaidFC"].ToString());

                row["PaidSysWithHoldingTax"] = Properties.Settings.Default.SysCurrency + " " + ConvertDecimalTwoPlaces(row["PaidSys"].ToString());

                row["PaidWithOutHoldingTax"] = ConvertDecimalTwoPlaces(row["DocTotal"].ToString()) - ConvertDecimalTwoPlaces(row["Paid"].ToString());

                row["PaidWithOutHoldingTax"] = row["DocCur"] + " " + row["PaidWithOutHoldingTax"];

                row["PaidFCWithOutHoldingTax"]= ConvertDecimalTwoPlaces(row["DocTotalFrgn"].ToString()) - ConvertDecimalTwoPlaces(row["PaidFC"]);

                row["PaidFCWithOutHoldingTax"] = row["DocCur"] + " " + row["PaidFCWithOutHoldingTax"];

                row["PaidSysWithOutHoldingTax"] =ConvertDecimalTwoPlaces(row["DocTotalSy"].ToString()) - ConvertDecimalTwoPlaces(row["PaidSys"]);

                row["PaidSysWithOutHoldingTax"] = Properties.Settings.Default.SysCurrency + " " + row["PaidSysWithOutHoldingTax"];

                row["DocTotal"] = row["DocCur"] + " " + ConvertDecimalTwoPlaces(row["DocTotal"].ToString());

                row["DocTotalFrgn"] = row["DocCur"] + " " + ConvertDecimalTwoPlaces(row["DocTotalFrgn"].ToString());

                row["DocTotalSy"] = Properties.Settings.Default.SysCurrency + " " + ConvertDecimalTwoPlaces(row["DocTotalSy"].ToString());

                row["Paid"] =  ConvertDecimalTwoPlaces(row["DocTotal"].ToString()) - ConvertDecimalTwoPlaces(row["Paid"].ToString()) - ConvertDecimalTwoPlaces(row["WTImporte"].ToString());

                row["Paid"] = row["DocCur"] + " " + ConvertDecimalTwoPlaces(row["Paid"].ToString());

                row["PaidFC"] = ConvertDecimalTwoPlaces(row["DocTotalFrgn"].ToString()) - ConvertDecimalTwoPlaces(row["PaidFC"]) - ConvertDecimalTwoPlaces(row["WTImporteFC"].ToString());

                row["PaidFC"] = row["DocCur"] + " " + ConvertDecimalTwoPlaces(row["PaidFC"].ToString());

                row["PaidSys"] = ConvertDecimalTwoPlaces(row["DocTotalSy"].ToString()) - ConvertDecimalTwoPlaces(row["PaidSys"]) - ConvertDecimalTwoPlaces(row["WTImporteSC"]);

                row["PaidSys"] = Properties.Settings.Default.SysCurrency + " " + ConvertDecimalTwoPlaces(row["PaidSys"].ToString());

                row["Seleccionado"] = false;

                row["WTImporte"] = row["DocCur"] + " " + ConvertDecimalTwoPlaces(row["WTImporte"].ToString());

                row["WTImporteFC"] = row["DocCur"] + " " + ConvertDecimalTwoPlaces(row["WTImporteFC"].ToString());

                row["WTImporteSC"] = Properties.Settings.Default.SysCurrency + " " + ConvertDecimalTwoPlaces(row["WTImporteSC"].ToString());

            }

            return dt;
        }

        private DataTable AddCurrencyCodeReadOnly(DataTable dt)
        {

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {

                    if (column.ToString() == "DocTotal")
                    {
                        row["DocTotal"] = row["DocCur"] + " " + String.Format(row["DocTotal"].ToString());
                    }

                    else if (column.ToString() == "Paid")
                    {
                        str = regex.Replace(row["DocTotal"].ToString(), String.Empty);

                        row["Paid"] = ConvertDecimalTwoPlaces(str) - ConvertDecimalTwoPlaces(row["Paid"].ToString()) - ConvertDecimalTwoPlaces(row["WTImporte"].ToString());

                        row["Paid"] = row["DocCur"] + " " + String.Format(row["Paid"].ToString());
                    }

                    else if (column.ToString() == "PaidWithHoldingTax")
                    {
                        str = regex.Replace(row["Paid"].ToString(), String.Empty);

                        row["PaidWithHoldingTax"] = ConvertDecimalTwoPlaces(str);

                        row["PaidWithHoldingTax"] = row["DocCur"] + " " + String.Format(row["PaidWithHoldingTax"].ToString());
                    }

                    else if (column.ToString() == "DocTotalFrgn")
                    {
                        row["DocTotalFrgn"] = row["DocCur"] + " " + String.Format(row["DocTotalFrgn"].ToString());
                    }

                    else if (column.ToString() == "PaidFC")
                    {
                        str = regex.Replace(row["DocTotalFrgn"].ToString(), String.Empty);

                        row["PaidFC"] = ConvertDecimalTwoPlaces(str) - ConvertDecimalTwoPlaces(row["PaidFC"]) - ConvertDecimalTwoPlaces(row["WTImporteFC"].ToString());

                        row["PaidFC"] = row["DocCur"] + " " + String.Format(row["PaidFC"].ToString());
                    }

                    else if (column.ToString() == "PaidFCWithHoldingTax")
                    {
                        str = regex.Replace(row["PaidFC"].ToString(), String.Empty);

                        row["PaidFCWithHoldingTax"] = ConvertDecimalTwoPlaces(str);

                        row["PaidFCWithHoldingTax"] = row["DocCur"] + " " + String.Format(row["PaidFCWithHoldingTax"].ToString());
                    }

                    else if (column.ToString() == "DocTotalSy")
                    {
                        row["DocTotalSy"] = Properties.Settings.Default.SysCurrency + " " + String.Format(row["DocTotalSy"].ToString());
                    }

                    else if (column.ToString() == "PaidSys")
                    {
                        str = regex.Replace(row["DocTotalSy"].ToString(), String.Empty);

                        row["PaidSys"] = ConvertDecimalTwoPlaces(str) - ConvertDecimalTwoPlaces(row["PaidSys"]) - ConvertDecimalTwoPlaces(row["WTImporteSC"]);

                        row["PaidSys"] = Properties.Settings.Default.SysCurrency + " " + String.Format(row["PaidSys"].ToString());
                    }

                    else if (column.ToString() == "PaidSysWithHoldingTax")
                    {
                        str = regex.Replace(row["PaidSys"].ToString(), String.Empty);

                        row["PaidSysWithHoldingTax"] = ConvertDecimalTwoPlaces(str);

                        row["PaidSysWithHoldingTax"] = Properties.Settings.Default.SysCurrency + " " + String.Format(row["PaidSysWithHoldingTax"].ToString());
                    }

                    else if (column.ToString() == "Seleccionado")
                    {
                        row["Seleccionado"] = true;

                    }

                    else if (column.ToString() == "WTImporte")
                    {
                        row["WTImporte"] = row["DocCur"] + " " + ConvertDecimalTwoPlaces(row["WTImporte"].ToString());
                    }

                    else if (column.ToString() == "WTImporteFC")
                    {
                        row["WTImporteFC"] = row["DocCur"] + " " + ConvertDecimalTwoPlaces(row["WTImporteFC"].ToString());
                    }

                    else if (column.ToString() == "WTImporteSC")
                    {
                        row["WTImporteSC"] = Properties.Settings.Default.SysCurrency + " " + ConvertDecimalTwoPlaces(row["WTImporteSC"].ToString());
                    }


                }

            }

            return dt;
        }

        private void GetPayment(List<Payment> listaPayment)
        {
            Payment = listaPayment;

            foreach (Payment payment in listaPayment)
            {

                DocEntryPayment = payment.DocEntry;               
                DocNum = payment.DocNum;
                txtNro.Text = payment.DocNum.ToString();
                dpContabilizacion.SelectedDate =payment.DocDate;
                dpDocumento.SelectedDate = payment.TaxDate;
                dpVencimiento.SelectedDate = payment.DocDueDate;
                txtProveedor.Text = payment.CardCode;
                txtNombre.Text = payment.CardName;
                txtPagarA.Text = payment.Address;
                txtReferencia.Text = payment.CounterRef;
                txtNroOperacion.Text = payment.TransId.ToString();
                txtCuentaAsociada.Text = payment.BpAct;

                Currency = payment.DocCurr;
                DocCurPayment = payment.DocCurr;

                var result10 = cn.GetCurrencyName(payment.DocCurr);

                CurrNamePayment = result10.Item1;                     
                
                txtComentario.Text = payment.JrnlMemo;
                cbxPagoCuenta.IsChecked = Convert.ToBoolean(cn.GetPayNoDoc(payment.PayNoDoc));
                txtPagoCuenta.Text = payment.NoDocSum.ToString();

                
                if (payment.DocCurr == Properties.Settings.Default.MainCurrency)
                {
                    txtImporteVencido.Text = Properties.Settings.Default.MainCurrency + " " + payment.DocTotal;
                    txtImporteVencido_ME.Text = "";                   

                }
                else
                {
                    txtImporteVencido.Text = Properties.Settings.Default.MainCurrency + " " + payment.DocTotal;
                    txtImporteVencido_ME.Text = payment.DocCurr + " " + payment.DocTotalFC;
                }


            }
            ReadOnlyField();
        }

        private void ReadOnlyField()
        {
            txtNro.Background = Brushes.LightGray;
            txtProveedor.Background = Brushes.LightGray;
            bdProveedor.Background = Brushes.LightGray;
            dpProveedor.Background = Brushes.LightGray;
            imgProveedor.Visibility = Visibility.Hidden;

            txtNombre.Background = Brushes.LightGray;
            txtCuentaAsociada.Background = Brushes.LightGray;
            txtNroOperacion.Background = Brushes.LightGray;
            txtPagoCuenta.Background = Brushes.LightGray;
            txtImporteVencido.Background = Brushes.LightGray;
            txtSaldoPendiente.Background = Brushes.LightGray;
            txtNro.IsReadOnly = true;
            txtProveedor.IsReadOnly = true;
            txtNombre.IsReadOnly = true;
            txtCuentaAsociada.IsReadOnly = true;
            txtNroOperacion.IsReadOnly = true;
            txtPagoCuenta.IsReadOnly = true;
            txtImporteVencido.IsReadOnly = true;
            txtSaldoPendiente.IsReadOnly = true;
            ReadOnlyDatetime(dpContabilizacion);
            ReadOnlyDatetime(dpVencimiento);
        }

        private void imgRight_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (btnCrear.Content.ToString() == "OK" || btnCrear.Content.ToString() == "Crear" || btnCrear.Content.ToString() == "Buscar")
            {
                ReestablecerFondo();

                ListaPayment.Clear();

                ListaPaymentLines.Clear();

                var result = cn.FindNextPaymentMade(txtNro.Text);

                if (result.Item2 == null)
                {
                    ListaPayment = result.Item1;

                    GetPayment(ListaPayment);

                    VerificaCurrency();

                    var result1 = cn.FindPaymentMadeLines(DocEntryPayment);

                    if (result1.Item3 == null)
                    {
                        var result3 = cn.FindPurchaseInvoiceSupplierSpecific(result1.Item1, result1.Item2);

                        if (result3.Item2 == null)
                        {
                            ListaPaymentLines = result3.Item1;

                            GetPaymentLinesReadOnly(ListaPaymentLines);

                            Sw = false;

                            SetStateDatatableDisabled();

                            ClearDataTableWithHoldingTax();

                            ClearDatataTableDocuments();

                            btnCrear.Content = "OK";
                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result3.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                        }

                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result1.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                    }


                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                }
            }

            else if (btnCrear.Content.ToString() == "Actualizar")
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("¿Desea salir sin guardar cambios?", "Journal Entry", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    btnCrear.Content = "OK";

                }
            }
        }

        private void imgleft_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (btnCrear.Content.ToString() == "OK" || btnCrear.Content.ToString() == "Crear" || btnCrear.Content.ToString() == "Buscar")
            {
                ReestablecerFondo();

                ListaPayment.Clear();

                ListaPaymentLines.Clear();

                var result = cn.FindPreviousPaymentMade(txtNro.Text);

                if (result.Item2 == null)
                {
                    ListaPayment = result.Item1;

                    GetPayment(ListaPayment);

                    VerificaCurrency();

                    var result1 = cn.FindPaymentMadeLines(DocEntryPayment);

                    if (result1.Item3 == null)
                    {
                        var result3 = cn.FindPurchaseInvoiceSupplierSpecific(result1.Item1, result1.Item2);

                        if (result3.Item2 == null)
                        {
                            ListaPaymentLines = result3.Item1;

                            GetPaymentLinesReadOnly(ListaPaymentLines);

                            Sw = false;

                            SetStateDatatableDisabled();

                            ClearDataTableWithHoldingTax();

                            ClearDatataTableDocuments();

                            btnCrear.Content = "OK";
                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result3.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                        }

                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result1.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                    }


                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                }
            }

            else if (btnCrear.Content.ToString() == "Actualizar")
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("¿Desea salir sin guardar cambios?", "Journal Entry", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    btnCrear.Content = "OK";

                }
            }
        }

        private void imgInicio_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (btnCrear.Content.ToString() == "OK" || btnCrear.Content.ToString() == "Crear" || btnCrear.Content.ToString() == "Buscar")
            {
                ReestablecerFondo();

                ListaPayment.Clear();

                ListaPaymentLines.Clear();

                var result = cn.FindFirstPaymentMade();

                if (result.Item2 == null)
                {
                    ListaPayment = result.Item1;

                    GetPayment(ListaPayment);

                    VerificaCurrency();

                    var result1 = cn.FindPaymentMadeLines(DocEntryPayment);

                    if (result1.Item3 == null)
                    {
                        var result3= cn.FindPurchaseInvoiceSupplierSpecific(result1.Item1, result1.Item2);

                        if (result3.Item2 == null)
                        {
                            ListaPaymentLines = result3.Item1;

                            GetPaymentLinesReadOnly(ListaPaymentLines);

                            Sw = false;

                            SetStateDatatableDisabled();

                            ClearDataTableWithHoldingTax();

                            ClearDatataTableDocuments();

                            btnCrear.Content = "OK";
                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result3.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                        }

                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result1.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                    }


                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                }
            }

            else if (btnCrear.Content.ToString() == "Actualizar")
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("¿Desea salir sin guardar cambios?", "Journal Entry", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    btnCrear.Content = "OK";

                }
            }
        }

        private void imgBuscar_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            btnCrear.Content = "Buscar";

            LimpiarCampos();

            EstablecerFondo();

            EnabledDatepicker();

            dgPagoEfectuado.CanUserAddRows = true;

            dgPagoEfectuado.CanUserDeleteRows = true;

            dgPagoEfectuado.CanUserSortColumns = true;

            dt.Rows.Clear();

            dgPagoEfectuado.ItemsSource = dt.Rows;
        }

        
        private void EnabledDatepicker()
        {
            dpContabilizacion.IsEnabled = true;
        }

        private void EstablecerFondo()
        {
            txtNro.Background = Brushes.LightBlue;
            txtNro.IsReadOnly = false;           
            txtProveedor.Background = Brushes.LightBlue;
            txtProveedor.IsReadOnly = false;
            bdProveedor.Background = Brushes.LightBlue;
            dpProveedor.Background = Brushes.LightBlue;
            txtNombre.Background = Brushes.LightBlue;
            txtNombre.IsReadOnly = false;
            txtPagarA.Background = Brushes.LightBlue;
            txtReferencia.Background = Brushes.LightBlue;
            txtNroOperacion.Background = Brushes.LightBlue;
            txtNroOperacion.IsReadOnly = false;
            txtComentario.Background = Brushes.LightBlue;
            dpContabilizacion.Background = Brushes.LightBlue;
            dpDocumento.Background = Brushes.LightBlue;
            dpVencimiento.Background = Brushes.LightBlue;
            establecerDatetime(dpContabilizacion);
            establecerDatetime(dpDocumento);
            establecerDatetime(dpVencimiento);
        }

        private void imgMoney_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Sw == false)
            {
                EstableceLogin.GetMediosPago().ClearMediosPago();                

                EstableceLogin.GetMediosPago().Show();

                EstableceLogin.GetMediosPago().DisabledButton();               

                EstableceLogin.GetMediosPago().SetCurrency(DocCurPayment, CurrNamePayment);

                EstableceLogin.GetMediosPago().DisabledFields();

                EstableceLogin.GetMediosPago().SetFields(Payment);


            }
            else
            {
                EstableceLogin.GetMediosPago().LoadCurrency(Supplier);

                EstableceLogin.GetMediosPago().Show();

                EstableceLogin.GetMediosPago().EnabledFields();

                if (cbxPagoCuenta.IsChecked == true)
                {
                    EstableceLogin.GetMediosPago().SetAmount(Properties.Settings.Default.MainCurrency + " " + txtPagoCuenta.Text, Properties.Settings.Default.MainCurrency + " " + txtPagoCuenta.Text, Rate, RateFC);
                }
                else
                {
                    EstableceLogin.GetMediosPago().SetAmount(txtImporteVencido.Text, txtImporteVencido.Text, Rate, RateFC);
                }

                EstableceLogin.GetMediosPago().SetCurrency(Properties.Settings.Default.MainCurrency);

                EstableceLogin.GetMediosPago().SetFecha(dpContabilizacion.SelectedDate);

            }
        }

        private void FindRate()
        {
            DateTime? fecha = dpContabilizacion.SelectedDate;

            SelectedDate = String.Format("{0:yyyy/MM/dd}", fecha);

            if (String.IsNullOrWhiteSpace(SelectedDate) == false)
            {
                var result = cn.FindRate(Convert.ToDateTime(SelectedDate));

                if (result.Item2 == null)
                {
                    Rate = result.Item1;

                    if (Rate == 0)
                    {
                        ShowTipoCambio();
                    }
                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                }

            }
        }

        private void FindRateFC(string currencyFC)
        {
            DateTime? fecha = dpContabilizacion.SelectedDate;

            SelectedDate = String.Format("{0:yyyy/MM/dd}", fecha);

            if (String.IsNullOrWhiteSpace(SelectedDate) == false)
            {
                var result = cn.FindRateFCCurrency(Convert.ToDateTime(SelectedDate), currencyFC);

                if (result.Item2 == null)
                {
                    RateFC = result.Item1;

                    if (RateFC == 0)
                    {
                        ShowTipoCambio();
                    }
                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                }



            }
        }

        private void ShowTipoCambio()
        {
            TipoCambio ventanaTipoCambio = new TipoCambio();

            DoubleAnimation animation = new DoubleAnimation(0, 1,
                                 (Duration)TimeSpan.FromSeconds(1));
            ventanaTipoCambio.BeginAnimation(UIElement.OpacityProperty, animation);

            ventanaTipoCambio.ShowDialog();

        }



        private void RecorreListaSN(List<SocioNegocio> listSuppliers)
        {
            if (listSuppliers.Count == 1)
            {
                GetSocioNegocio(listSuppliers);

                txtProveedor.Background = Brushes.White;

                dpProveedor.Background = Brushes.White;

                bdProveedor.Background = Brushes.White;

                VerificaCurrency();

                var result=cn.FindPurchaseInvoiceSupplier(Supplier);

                if (result.Item2 == null)
                {
                    GetPaymentLines(result.Item1);
                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                }

                //btnCrear.Content = "OK";
            }
            else if (listSuppliers.Count == 0)
            {

                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se encontro ningun resultado con los parametros establecidos", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");

                LimpiarCampos();

                //btnCrear.Content = "OK";
            }

            else if (listSuppliers.Count > 1)
            {
                ListaSociosNegocios ventanaListBox = new ListaSociosNegocios(listSuppliers);

                ventanaListBox.ShowDialog();

                if (ventanaListBox.Visibility == Visibility.Hidden)
                {
                    if (ventanaListBox.GetListSN().Count == 0)
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se selecciono ningun elemento", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");

                        LimpiarCampos();
                    }
                    else
                    {

                        GetSocioNegocio(ventanaListBox.GetListSN());

                        txtProveedor.Background = Brushes.White;

                        dpProveedor.Background = Brushes.White;

                        bdProveedor.Background = Brushes.White;

                        VerificaCurrency();

                        var result = cn.FindPurchaseInvoiceSupplier(Supplier);

                        if (result.Item2 == null)
                        {
                            GetPaymentLines(result.Item1);
                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                        }

                    }

                    //btnCrear.Content = "OK";
                }
                ReestablecerFondo();
            }
        }

        private void VerificaCurrency()
        {
            if (Currency == "##" || Currency!=Properties.Settings.Default.MainCurrency)
            {
                txtImporteVencido_ME.Visibility = Visibility.Visible;
                lblImporteVencido_ME.Visibility = Visibility.Visible;
            }
            else
            {
                txtImporteVencido_ME.Visibility = Visibility.Hidden;
                lblImporteVencido_ME.Visibility = Visibility.Hidden;
            }
        }

        private Payment GetMediosPago(List<Payment> listPayment, Payment payment)
        {
            foreach (Payment oldPayment in listPayment)
            {
                payment.CashAcct = oldPayment.CashAcct;
                payment.TrsfrAcct = oldPayment.TrsfrAcct;
                payment.TrsfrDate = oldPayment.TrsfrDate;
                payment.TrsfrRef = oldPayment.TrsfrRef;
                payment.CashSum = oldPayment.CashSum;
                payment.CashSumFC = oldPayment.CashSumFC;
                payment.CashSumSy = oldPayment.CashSumSy;
                payment.TrsfrSum = oldPayment.TrsfrSum;
                payment.TrsfrSumFC = oldPayment.TrsfrSumFC;
                payment.TrsfrSumSy = oldPayment.TrsfrSumSy;
                payment.DocTotal = oldPayment.DocTotal;
                payment.DocTotalFC = oldPayment.DocTotalFC;
                payment.DocTotalSy = oldPayment.DocTotalSy;

                break;
            }

            VerifyAccount(payment);

            return payment;
        }

        private void VerifyAccount(Payment payment)
        {
            if(String.IsNullOrWhiteSpace(payment.TrsfrAcct) == false)
            {
                AcctCode = payment.TrsfrAcct;
            }
            else if (String.IsNullOrWhiteSpace(payment.CashAcct) == false)
            {
                AcctCode = payment.CashAcct;
            }
        }

        private void GetSocioNegocio(List<SocioNegocio> listSuppliers)
        {
            foreach (SocioNegocio Suppliers in listSuppliers)
            {
                Supplier = Suppliers.CardCode;

                txtProveedor.Text = Suppliers.CardCode;

                txtNombre.Text = Suppliers.CardName;

                txtPagarA.Text = Suppliers.Address;

                txtComentario.Text = "Pago Efectuado - " + Suppliers.CardCode;

                txtCuentaAsociada.Text = Suppliers.DebPayAcct;  
                
                CuentaAsociada= Suppliers.DebPayAcct;

                VatGroup = Suppliers.VatGroup;

                Currency = Suppliers.Currency;
            }
        }

        private void ReestablecerFondo()
        {
            txtNro.IsReadOnly = true;

            txtNroOperacion.IsReadOnly = true;

            txtNro.Background = Brushes.LightGray;

            txtNroOperacion.Background = Brushes.LightGray;

            txtProveedor.Background = Brushes.White;

            dpProveedor.Background = Brushes.White;

            bdProveedor.Background = Brushes.White;

            txtNombre.Background = Brushes.White;

            txtPagarA.Background = Brushes.White;

            txtReferencia.Background = Brushes.White;

            txtComentario.Background = Brushes.White;

            dpContabilizacion.Background = Brushes.White;

            dpDocumento.Background = Brushes.White;

            dpVencimiento.Background = Brushes.White;

            ReestablecerDatetime(dpContabilizacion);

            ReestablecerDatetime(dpDocumento);

            ReestablecerDatetime(dpVencimiento);


        }

        

        public void ReestablecerDatetime(DatePicker dpk)
        {
            TextBox tb = (TextBox)dpk.Template.FindName("PART_TextBox", dpk);

            tb.Background = Brushes.White;

        }


        private void LimpiarCampos()
        {
            txtProveedor.Text = "";

            txtNombre.Text = "";

            txtPagarA.Text = "";

            dpContabilizacion.SelectedDate = null;

            dpDocumento.SelectedDate = null;

            dpVencimiento.SelectedDate = null;

            txtReferencia.Text = "";

            txtNroOperacion.Text = "";

            txtComentario.Text = "";

            txtCuentaAsociada.Text = "";

            txtImporteVencido.Text = Properties.Settings.Default.MainCurrency + " " + String.Format("{0:#,#.00}", "0,00");

            txtPagoCuenta.Text =  String.Format("{0:#,#.00}", "0,00");

            txtSaldoPendiente.Text = "";

            txtImporteVencido_ME.Text =  String.Format("{0:#,#.00}", "0,00");

            ClearDg();
        }

        private void ClearDg()
        {
            dt.Rows.Clear();

            dt.NewRow();

            dgPagoEfectuado.ItemsSource = dt.Rows;
        }

        private void txtProveedor_GotFocus(object sender, RoutedEventArgs e)
        {
            if (btnCrear.Content.ToString() == "Buscar" || btnCrear.Content.ToString() == "OK")
            {

            }
            else
            {
                dpProveedor.Background = Brushes.LightBlue;

                bdProveedor.Background = Brushes.LightBlue;

                txtProveedor.Background = Brushes.LightBlue;

                imgProveedor.Visibility = Visibility.Visible;
            }
        }

        private void txtProveedor_LostFocus(object sender, RoutedEventArgs e)
        {
            if (btnCrear.Content.ToString() == "Buscar" || btnCrear.Content.ToString() == "OK")
            {

            }
            else
            {
                dpProveedor.Background = Brushes.White;

                bdProveedor.Background = Brushes.White;

                txtProveedor.Background = Brushes.White;
            }

            imgProveedor.Visibility = Visibility.Hidden;
        }

        private void imgProveedor_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var result = cn.FindSuppliers();

            if (result.Item2 == null)
            {
                RecorreListaSN(result.Item1);

                
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
            }

        }

        private void dpContabilizacion_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            FindRate();
        }

        private void seleccionado_Checked(object sender, RoutedEventArgs e)
        {

            if (Sw == true)
            {
                var row_list = GetDataGridRows(dgPagoEfectuado);

                DataRowView row_Selected = dgPagoEfectuado.SelectedItem as DataRowView;

                foreach (DataGridRow single_row in row_list)
                {

                    CheckBox checkBox = FindChild<CheckBox>(single_row, "seleccionado");

                    if (checkBox.IsFocused == true)
                    {
                        if (single_row.IsSelected == true)
                        {

                            DockPanel dpSeleccionado = FindChild<DockPanel>(single_row, "dpCheckBox");

                            TextBlock txtDocNum = FindChild<TextBlock>(single_row, "txtDocNum");

                            TextBlock txtWTImporteRetencion = FindChild<TextBlock>(single_row, "txtWTImporteRetencion");

                            TextBlock txtObjType = FindChild<TextBlock>(single_row, "txtObjType");

                            TextBlock dpFecha = FindChild<TextBlock>(single_row, "dpFecha");

                            TextBlock txtDocTotal = FindChild<TextBlock>(single_row, "txtDocTotal");

                            TextBlock txtSaldoVencido = FindChild<TextBlock>(single_row, "txtSaldoVencido");

                            TextBlock txtU_IDA_CompIVA = FindChild<TextBlock>(single_row, "txtU_IDA_CompIVA");

                            TextBlock txtU_IDA_MontoCompIVA = FindChild<TextBlock>(single_row, "txtU_IDA_MontoCompIVA");

                            TextBlock txtU_IDA_CompISLR = FindChild<TextBlock>(single_row, "txtU_IDA_CompISLR");

                            TextBlock txtU_IDA_DateCompISLR = FindChild<TextBlock>(single_row, "txtU_IDA_DateCompISLR");

                            TextBlock txtU_IDA_FechaComp = FindChild<TextBlock>(single_row, "txtU_IDA_FechaComp");

                            DockPanel dp = FindChild<DockPanel>(single_row, "dp");

                            dp.Background = Brushes.LightBlue;

                            dpSeleccionado.Background = Brushes.LightBlue;

                            txtDocNum.Background = Brushes.LightBlue;

                            txtWTImporteRetencion.Background = Brushes.LightBlue;

                            txtObjType.Background = Brushes.LightBlue;

                            dpFecha.Background = Brushes.LightBlue;

                            txtDocTotal.Background = Brushes.LightBlue;

                            txtSaldoVencido.Background = Brushes.LightBlue;

                            txtU_IDA_CompIVA.Background = Brushes.LightBlue;

                            txtU_IDA_MontoCompIVA.Background = Brushes.LightBlue;

                            txtU_IDA_CompISLR.Background = Brushes.LightBlue;

                            txtU_IDA_DateCompISLR.Background = Brushes.LightBlue;

                            txtU_IDA_FechaComp.Background = Brushes.LightBlue;

                            AddDocument(row_Selected);

                            txtDocNum.Text = row_Selected["DocNum"].ToString();

                            txtWTImporteRetencion.Text = row_Selected["DocNum"].ToString();

                            txtObjType.Text = row_Selected["TransType"].ToString();

                            dpFecha.Text = row_Selected["DocDate"].ToString();

                            txtDocTotal.Text = row_Selected["DocTotal"].ToString();

                            txtWTImporteRetencion.Text = row_Selected["WTImporte"].ToString();

                            txtSaldoVencido.Text = row_Selected["Paid"].ToString();

                            txtU_IDA_CompIVA.Text = row_Selected["U_IDA_CompIVA"].ToString();

                            txtU_IDA_FechaComp.Text = row_Selected["U_IDA_FechaComp"].ToString();

                            txtU_IDA_MontoCompIVA.Text = row_Selected["U_IDA_MontoCompIVA"].ToString();

                            txtU_IDA_CompISLR.Text = row_Selected["U_IDA_CompISLR"].ToString();

                            txtU_IDA_DateCompISLR.Text = row_Selected["U_IDA_DateCompISLR"].ToString();

                            //DocNum = Convert.ToInt32(txtDocNum.Text);

                            //U_IDA_CompIVA = txtU_IDA_CompIVA.Text;

                            //U_IDA_MontoCompIVA = ConvertDecimalTwoPlaces(txtU_IDA_CompIVA.Text);

                            //U_IDA_CompISLR = txtU_IDA_CompISLR.Text;

                            //if (String.IsNullOrWhiteSpace(txtU_IDA_DateCompISLR.Text) == false)
                            //{
                            //    U_IDA_DateCompISLR = Convert.ToDateTime(txtU_IDA_DateCompISLR.Text);
                            //}
                            //else
                            //{
                            //    U_IDA_DateCompISLR = null;
                            //}

                            //if (String.IsNullOrWhiteSpace(txtU_IDA_FechaComp.Text) == false)
                            //{
                            //    U_IDA_FechaComp = Convert.ToDateTime(txtU_IDA_FechaComp.Text);
                            //}
                            //else
                            //{
                            //    U_IDA_FechaComp = null;
                            //}

                            str = regexString.Replace(txtSaldoVencido.Text, String.Empty);

                            if (str == Properties.Settings.Default.MainCurrency)
                            {
                                txtImporteVencido.Text = txtSaldoVencido.Text;
                                txtImporteVencido_ME.Text = txtSaldoVencido.Text;
                            }
                            else
                            {
                                strNumber = regex.Replace(txtSaldoVencido.Text, String.Empty);

                                amount = (ConvertDecimalTwoPlaces(strNumber) * Rate);

                                amount = ConvertDecimalTwoPlaces(amount);

                                txtImporteVencido.Text = Properties.Settings.Default.MainCurrency + " " + amount.ToString();
                                txtImporteVencido_ME.Text = txtSaldoVencido.Text;

                            }

                            if (row_Selected["TransType"].ToString() == "TT")
                            {
                                var result = cn.GetRetencionImpuesto(Convert.ToInt32(row_Selected["DocEntry"].ToString()),(cn.GetTransType(row_Selected["TransType"].ToString())).ToString());

                                if (result.Item2 == null)
                                {
                                    AppendRowToDatatable(result.Item1);
                                }
                                else
                                {
                                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al intentan recuperar las retenciones de impuestos de categoria pago asociadas a la factura: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                                }
                            }

                        }
                    }


                }
            }else if (Sw == false)
            {
                var row_list = GetDataGridRows(dgPagoEfectuado);

                DataRowView row_Selected = dgPagoEfectuado.SelectedItem as DataRowView;

                foreach (DataGridRow single_row in row_list)
                {

                    CheckBox checkBox = FindChild<CheckBox>(single_row, "seleccionado");

                    if (checkBox.IsChecked == true)
                    {
                        
                            DockPanel dpSeleccionado = FindChild<DockPanel>(single_row, "dpCheckBox");

                            TextBlock txtDocNum = FindChild<TextBlock>(single_row, "txtDocNum");

                            TextBlock txtWTImporteRetencion = FindChild<TextBlock>(single_row, "txtWTImporteRetencion");

                            TextBlock txtObjType = FindChild<TextBlock>(single_row, "txtObjType");

                            TextBlock dpFecha = FindChild<TextBlock>(single_row, "dpFecha");

                            TextBlock txtDocTotal = FindChild<TextBlock>(single_row, "txtDocTotal");

                            TextBlock txtSaldoVencido = FindChild<TextBlock>(single_row, "txtSaldoVencido");

                            TextBlock txtU_IDA_CompIVA = FindChild<TextBlock>(single_row, "txtU_IDA_CompIVA");

                            TextBlock txtU_IDA_MontoCompIVA = FindChild<TextBlock>(single_row, "txtU_IDA_MontoCompIVA");

                            TextBlock txtU_IDA_CompISLR = FindChild<TextBlock>(single_row, "txtU_IDA_CompISLR");

                            TextBlock txtU_IDA_DateCompISLR = FindChild<TextBlock>(single_row, "txtU_IDA_DateCompISLR");

                            TextBlock txtU_IDA_FechaComp = FindChild<TextBlock>(single_row, "txtU_IDA_FechaComp");

                            DockPanel dp = FindChild<DockPanel>(single_row, "dp");

                            dp.Background = Brushes.LightBlue;

                            dpSeleccionado.Background = Brushes.LightBlue;

                            txtDocNum.Background = Brushes.LightBlue;

                            txtWTImporteRetencion.Background = Brushes.LightBlue;

                            txtObjType.Background = Brushes.LightBlue;

                            dpFecha.Background = Brushes.LightBlue;

                            txtDocTotal.Background = Brushes.LightBlue;

                            txtSaldoVencido.Background = Brushes.LightBlue;

                            txtU_IDA_CompIVA.Background = Brushes.LightBlue;

                            txtU_IDA_MontoCompIVA.Background = Brushes.LightBlue;

                            txtU_IDA_CompISLR.Background = Brushes.LightBlue;

                            txtU_IDA_DateCompISLR.Background = Brushes.LightBlue;

                            txtU_IDA_FechaComp.Background = Brushes.LightBlue;

                            

                        
                    }


                }
            }

            StateNoDocPay();

            

            

        }

        private void AppendRowToDatatable(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {

                var row = dt.Rows[dt.Rows.Count - 1];

                dtWithHoldingTax.ImportRow(row);

                dtWithHoldingTax.AcceptChanges();

            }
        }

        private DataTable AppendRowDatatableJournalEntry(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {

                var row = dt.Rows[dt.Rows.Count - 1];

                dtJournalEntry.ImportRow(row);

                dtJournalEntry.AcceptChanges();

            }

            return dtJournalEntry;
        }

        private void RemoveRowToDatatable(int DocEntry)
        {
            var query = dtWithHoldingTax.AsEnumerable().Where(r => r.Field<int>("AbsEntry") == DocEntry);

            foreach (var row in query.ToList())
                row.Delete();

            dtWithHoldingTax.AcceptChanges();
        }

        private void StateNoDocPay()
        {
            bool sw=true;
           
            var row_list = GetDataGridRows(dgPagoEfectuado);

            DataRowView row_Selected = dgPagoEfectuado.SelectedItem as DataRowView;

            foreach (DataGridRow single_row in row_list)
            {
                CheckBox checkBox = FindChild<CheckBox>(single_row, "seleccionado");

                if (checkBox.IsChecked == true && sw==true)
                {
                    sw = false;
                }
            }

            if (sw == true)
            {
                cbxPagoCuenta.IsEnabled = true;

                txtImporteVencido.IsReadOnly = false;
            }
            else
            {
                cbxPagoCuenta.IsEnabled = false;

                txtImporteVencido.IsReadOnly = true;
            }
        }

        private void seleccionado_Unchecked(object sender, RoutedEventArgs e)
        {
           
            var row_list = GetDataGridRows(dgPagoEfectuado);            

            DataRowView row_Selected = dgPagoEfectuado.SelectedItem as DataRowView;            

            foreach (DataGridRow single_row in row_list)
            {
                
                CheckBox checkBox = FindChild<CheckBox>(single_row, "seleccionado");

                if (checkBox.IsFocused == true)
                {
                    if (single_row.IsSelected == true)
                    {
                        var bc = new BrushConverter();

                        DockPanel dpSeleccionado = FindChild<DockPanel>(single_row, "dpCheckBox");

                        TextBlock txtDocNum = FindChild<TextBlock>(single_row, "txtDocNum");

                        TextBlock txtWTImporteRetencion = FindChild<TextBlock>(single_row, "txtWTImporteRetencion");

                        TextBlock txtObjType = FindChild<TextBlock>(single_row, "txtObjType");

                        TextBlock dpFecha = FindChild<TextBlock>(single_row, "dpFecha");

                        TextBlock txtDocTotal = FindChild<TextBlock>(single_row, "txtDocTotal");

                        TextBlock txtSaldoVencido = FindChild<TextBlock>(single_row, "txtSaldoVencido");

                        TextBlock txtU_IDA_CompIVA = FindChild<TextBlock>(single_row, "txtU_IDA_CompIVA");

                        TextBlock txtU_IDA_MontoCompIVA = FindChild<TextBlock>(single_row, "txtU_IDA_MontoCompIVA");

                        TextBlock txtU_IDA_CompISLR = FindChild<TextBlock>(single_row, "txtU_IDA_CompISLR");

                        TextBlock txtU_IDA_DateCompISLR = FindChild<TextBlock>(single_row, "txtU_IDA_DateCompISLR");

                        TextBlock txtU_IDA_FechaComp = FindChild<TextBlock>(single_row, "txtU_IDA_FechaComp");

                        txtDocNum.Text = row_Selected["DocNum"].ToString();

                        txtWTImporteRetencion.Text = row_Selected["DocNum"].ToString();

                        txtObjType.Text = row_Selected["TransType"].ToString();

                        dpFecha.Text = row_Selected["DocDate"].ToString();

                        txtDocTotal.Text = row_Selected["DocTotal"].ToString();

                        txtWTImporteRetencion.Text = row_Selected["WTImporte"].ToString();

                        txtSaldoVencido.Text = row_Selected["Paid"].ToString();

                        txtU_IDA_CompIVA.Text = row_Selected["U_IDA_CompIVA"].ToString();

                        txtU_IDA_FechaComp.Text = row_Selected["U_IDA_FechaComp"].ToString();

                        txtU_IDA_MontoCompIVA.Text = row_Selected["U_IDA_MontoCompIVA"].ToString();

                        txtU_IDA_CompISLR.Text = row_Selected["U_IDA_CompISLR"].ToString();

                        txtU_IDA_DateCompISLR.Text = row_Selected["U_IDA_DateCompISLR"].ToString();                        

                        DockPanel dp = FindChild<DockPanel>(single_row, "dp");

                        dp.Background = Brushes.White;

                        txtDocNum.Background = Brushes.White;

                        txtWTImporteRetencion.Background = Brushes.White;

                        dpSeleccionado.Background = Brushes.White;

                        txtObjType.Background = (Brush)bc.ConvertFrom("#FFE5E5E5");

                        dpFecha.Background = (Brush)bc.ConvertFrom("#FFE5E5E5");

                        txtDocTotal.Background = (Brush)bc.ConvertFrom("#FFE5E5E5");

                        txtSaldoVencido.Background = (Brush)bc.ConvertFrom("#FFE5E5E5");

                        txtU_IDA_CompIVA.Background = Brushes.White;

                        txtU_IDA_MontoCompIVA.Background = Brushes.White;

                        txtU_IDA_CompISLR.Background = Brushes.White;

                        txtU_IDA_DateCompISLR.Background = Brushes.White;

                        txtU_IDA_FechaComp.Background = Brushes.White;

                        txtImporteVencido.Text = "";

                        txtImporteVencido_ME.Text = "";

                        //DocNum = 0;

                        //U_IDA_CompIVA = "";

                        //U_IDA_MontoCompIVA = 0;

                        //U_IDA_CompISLR = "";

                        //U_IDA_DateCompISLR = null;

                        //U_IDA_FechaComp = null;

                        if (row_Selected["TransType"].ToString() == "TT")
                        {
                            RemoveRowToDatatable(Convert.ToInt32(row_Selected["DocEntry"].ToString()));
                        }

                        RemoveDocument(row_Selected);


                    }
                }

                
                

            }

            StateNoDocPay();
        }



        private void RemoveDocument(DataRowView row_Selected)
        {
            int docEntry;

            string transType;

            docEntry =Convert.ToInt32(row_Selected["DocEntry"]);

            transType = row_Selected["TransType"].ToString();


            var query = dtDocuments.AsEnumerable().Where(r => r.Field<int>("DocEntry") == docEntry && r.Field<string>("TransType") == transType);

            foreach (var row in query.ToList())
                row.Delete();

            dtDocuments.AcceptChanges();
        }

        private void AddDocument(DataRowView row_Selected)
        {
            DataRow newRow = dtDocuments.NewRow();

            newRow["DocEntry"] =row_Selected["DocEntry"];
            newRow["DocNum"] = row_Selected["DocNum"];
            newRow["TransType"] = row_Selected["TransType"];
            newRow["DocDate"] = row_Selected["DocDate"];
            newRow["DocCur"] = row_Selected["DocCur"];
            newRow["DocTotalSy"] = row_Selected["DocTotalSy"];
            newRow["PaidSys"] = row_Selected["PaidSys"];
            newRow["DocTotal"] = row_Selected["DocTotal"];
            newRow["DocTotalFrgn"] = row_Selected["DocTotalFrgn"];
            newRow["Paid"] = row_Selected["Paid"];
            newRow["PaidFC"] = row_Selected["PaidFC"];
            newRow["DocSubType"] = row_Selected["DocSubType"];
            newRow["LineMemo"] = row_Selected["LineMemo"];
            newRow["DocRate"] = row_Selected["DocRate"];
            newRow["SysRate"] = row_Selected["SysRate"];
            newRow["TransId"] = row_Selected["TransId"];
            newRow["WTSum"] = row_Selected["WTSum"];
            newRow["WTSumFC"] = row_Selected["WTSumFC"];
            newRow["WTSumSC"] = row_Selected["WTSumSC"];
            newRow["VatSumFC"] = row_Selected["VatSumFC"];
            newRow["VatSumSy"] = row_Selected["VatSumSy"];
            newRow["VatSum"] = row_Selected["VatSum"];
            newRow["Line_ID"] = row_Selected["Line_ID"];
            newRow["CtlAccount"] = row_Selected["CtlAccount"];
            newRow["U_IDA_CompIVA"] = row_Selected["U_IDA_CompIVA"];
            newRow["U_IDA_FechaComp"] = row_Selected["U_IDA_FechaComp"];
            newRow["U_IDA_MontoCompIVA"] = row_Selected["U_IDA_MontoCompIVA"];
            newRow["U_IDA_CompISLR"] = row_Selected["U_IDA_CompISLR"];
            newRow["U_IDA_DateCompISLR"] = row_Selected["U_IDA_DateCompISLR"];
            newRow["PaidWithHoldingTax"] = row_Selected["PaidWithHoldingTax"];
            newRow["PaidSysWithHoldingTax"] = row_Selected["PaidSysWithHoldingTax"];
            newRow["PaidFCWithHoldingTax"] = row_Selected["PaidFCWithHoldingTax"];
            newRow["PaidWithOutHoldingTax"] = row_Selected["PaidWithOutHoldingTax"];
            newRow["PaidSysWithOutHoldingTax"] = row_Selected["PaidSysWithOutHoldingTax"];
            newRow["PaidFCWithOutHoldingTax"] = row_Selected["PaidFCWithOutHoldingTax"];
            newRow["WTImporte"] = row_Selected["WTImporte"];
            newRow["WTImporteFC"] = row_Selected["WTImporteFC"];
            newRow["WTImporteSC"] = row_Selected["WTImporteSC"];           

            dtDocuments.Rows.Add(newRow);

            dtDocuments.AcceptChanges();
        }

        private void seleccionado_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void imgDocumento_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Document_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDatatableJournalEntry();

            LoadDatatableDocuments();

            LoadDatatableTaxHolding();

            LimpiarCampos();

            ReestablecerFondo();

            InicialiacionBasica();


        }

        private void LoadDatatableDocuments()
        {
            dtDocuments.Columns.Add("DocEntry",typeof(int));
            dtDocuments.Columns.Add("DocNum");
            dtDocuments.Columns.Add("TransType", typeof(string));
            dtDocuments.Columns.Add("DocDate");
            dtDocuments.Columns.Add("DocCur");
            dtDocuments.Columns.Add("DocTotalSy");
            dtDocuments.Columns.Add("PaidSys");
            dtDocuments.Columns.Add("DocTotal");
            dtDocuments.Columns.Add("DocTotalFrgn");
            dtDocuments.Columns.Add("Paid");
            dtDocuments.Columns.Add("PaidFC");
            dtDocuments.Columns.Add("DocSubType");
            dtDocuments.Columns.Add("LineMemo");          
            dtDocuments.Columns.Add("DocRate");
            dtDocuments.Columns.Add("TransId");
            dtDocuments.Columns.Add("SysRate");
            dtDocuments.Columns.Add("WTSum");
            dtDocuments.Columns.Add("WTSumFC");
            dtDocuments.Columns.Add("WTSumSC");
            dtDocuments.Columns.Add("VatSum");
            dtDocuments.Columns.Add("VatSumFC");
            dtDocuments.Columns.Add("VatSumSy");
            dtDocuments.Columns.Add("Line_ID");
            dtDocuments.Columns.Add("CtlAccount");
            dtDocuments.Columns.Add("WTImporte");
            dtDocuments.Columns.Add("WTImporteFC");
            dtDocuments.Columns.Add("WTImporteSC");
            dtDocuments.Columns.Add("U_IDA_CompIVA");
            dtDocuments.Columns.Add("U_IDA_FechaComp");
            dtDocuments.Columns.Add("U_IDA_MontoCompIVA");
            dtDocuments.Columns.Add("U_IDA_CompISLR");
            dtDocuments.Columns.Add("U_IDA_DateCompISLR");
            dtDocuments.Columns.Add("PaidWithHoldingTax");
            dtDocuments.Columns.Add("PaidSysWithHoldingTax");
            dtDocuments.Columns.Add("PaidFCWithHoldingTax");
            dtDocuments.Columns.Add("PaidWithOutHoldingTax");
            dtDocuments.Columns.Add("PaidSysWithOutHoldingTax");
            dtDocuments.Columns.Add("PaidFCWithOutHoldingTax");

        }

        private void LoadDatatableTaxHolding()
        {
            dtWithHoldingTax.Columns.Add("AbsEntry", typeof(int));
            dtWithHoldingTax.Columns.Add("WTCode", typeof(string));
            dtWithHoldingTax.Columns.Add("Rate", typeof(decimal));
            dtWithHoldingTax.Columns.Add("TaxbleAmnt", typeof(decimal));
            dtWithHoldingTax.Columns.Add("TaxbleAmntSC", typeof(decimal));
            dtWithHoldingTax.Columns.Add("TaxbleAmntFC", typeof(decimal));
            dtWithHoldingTax.Columns.Add("WTAmnt", typeof(decimal));
            dtWithHoldingTax.Columns.Add("WTAmntFC", typeof(decimal));
            dtWithHoldingTax.Columns.Add("WTAmntSC", typeof(decimal));
            dtWithHoldingTax.Columns.Add("Category", typeof(string));
            dtWithHoldingTax.Columns.Add("Account", typeof(string));
            dtWithHoldingTax.Columns.Add("Type", typeof(string));
            dtWithHoldingTax.Columns.Add("BaseType", typeof(string));
            dtWithHoldingTax.Columns.Add("LineNum", typeof(int));
            dtWithHoldingTax.Columns.Add("BaseAmnt", typeof(decimal));
            dtWithHoldingTax.Columns.Add("BaseAmntSC", typeof(decimal));
            dtWithHoldingTax.Columns.Add("BaseAmntFC", typeof(decimal));
            dtWithHoldingTax.Columns.Add("ObjType", typeof(string));
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

        private void btnCrear_Click(object sender, RoutedEventArgs e)
        {
            List<Payment> listPayment = new List<Payment>();

            Payment payment = new Payment();

            Payment oldPayment = new Payment();

            List<PaymentDetails> listPaymentDetails = new List<PaymentDetails>();            

            List<AsientoCabecera> listaJournalEntry = new List<AsientoCabecera>();

            AsientoCabecera journalEntry = new AsientoCabecera();

            List<AsientoDetalle> listaJournalEntryLines = new List<AsientoDetalle>();

            string str;

            switch (btnCrear.Content.ToString())
            {
                case "OK":

                    ClearDataTableWithHoldingTax();

                    ClearDatataTableDocuments();

                    this.Hide();

                    break;

                case "Buscar":

                    if (String.IsNullOrWhiteSpace(txtNro.Text) == false)
                    {
                        payment.DocNum = Convert.ToInt32(txtNro.Text);
                    }
                    else
                    {
                        payment.DocNum = 0;
                    }

                    payment.DocDate = dpContabilizacion.SelectedDate;
                    payment.TaxDate = dpDocumento.SelectedDate;
                    payment.DocDueDate = dpVencimiento.SelectedDate;
                    payment.CardCode = txtProveedor.Text;
                    payment.CardName = txtNombre.Text;
                    payment.Address = txtPagarA.Text;
                    payment.CounterRef = txtReferencia.Text;
                    payment.JrnlMemo = txtComentario.Text;

                    try
                    {
                        payment.TransId = Convert.ToInt32(txtNroOperacion.Text);
                    }
                    catch(Exception ex)
                    {
                        payment.TransId = 0;
                    }
                    
                    listPayment.Add(payment);

                    var result = cn.FindPayment(listPayment);

                    if (result.Item2 == null)
                    {
                        RecorreListaPayment(result.Item1);
                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                    }

                    ClearDataTableWithHoldingTax();

                    ClearDatataTableDocuments();

                    break;

                case "Crear":

                    if (cbxPagoCuenta.IsChecked == true)
                    {
                        oldPayment = GetMediosPago(EstableceLogin.GetMediosPago().GetListMediosPago(), payment); //Recuperar lista

                        var listPaymentResult = CreateListPayment(oldPayment);                        

                        var result5 = cn.InsertPaymentMade(listPaymentResult.Item1);

                        if (result5.Item1 == 1)
                        {
                            //Create Journal Entry

                            listaJournalEntry = CreateJournalEntry(listPaymentResult.Item1);

                            var result3 = cj.InsertJournalEntry(listaJournalEntry);                            

                            if (result3.Item1 == 1)
                            {

                                var listJournalEntryLines = CreateListJournalEntryLinesNoDocPay(listaJournalEntry, listPaymentResult.Item2);                                

                                var result7 = cj.InsertJournalEntryLines(listJournalEntryLines.Item1);

                                    if (listJournalEntryLines.Item2 == result7.Item1)
                                    {
                                        cn.UpdateCreditDebitAccount(dtNewJournalEntry);

                                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Operacion se realizo exitosamente", Brushes.LightGreen, Brushes.Black, "001-interface.png");

                                        LimpiarCampos();

                                        btnCrear.Content = "OK";

                                    }
                                    else
                                    {
                                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: " + result7.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");

                                    }

                                }
                                else
                                {
                                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: " + result3.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");

                                }
                            
                        }
                        else
                        {

                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error en la creacion de la factura: " + result5.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                        }
                    }
                    else
                    {
                        oldPayment = GetMediosPago(EstableceLogin.GetMediosPago().GetListMediosPago(), payment); //Recuperar lista

                        var listPaymentResult = CreateListPaymentComplete(oldPayment);    //crea lista de pagos                    

                        var result5 = cn.InsertPaymentMade(listPaymentResult.Item1); //inserta pago de cabecera

                        if (result5.Item1 == 1)
                        {                           
                            var resultDocEntry = cn.FindDocEntry(DocNumPayment);

                            DocEntryPayment = resultDocEntry.Item1;

                            var listPaymentLines = CreateListPaymentLines(listPaymentResult.Item2, dtDocuments);

                            var result6 = cn.InsertPaymentMadeLines(listPaymentLines);

                            if (result6.Item1 == listPaymentLines.Count)
                            {                                   

                                //Create Journal Entry

                                    listaJournalEntry = CreateJournalEntry(listPaymentResult.Item1);                                  

                                    var result2 = cj.InsertJournalEntry(listaJournalEntry);

                                    if (result2.Item1 == 1)
                                    {
                                        var listJournalEntryLines = CreateListJournalEntryLines(listaJournalEntry, listPaymentResult.Item2);

                                        var result3 = cj.InsertJournalEntryLines(listJournalEntryLines.Item1);

                                        if (listJournalEntryLines.Item2== result3.Item1)
                                        {

                                        bool swPaymentDetails = true;

                                        foreach(PaymentDetails rowPaymentDetails in listPaymentLines)
                                        {
                                            if (rowPaymentDetails.InvType == "18") //revisa el pago se realizo sobre una factura o un asiento
                                            {
                                                var updatePurchase = cn.UpdatePaymentMadePurchase(rowPaymentDetails); //Modifica saldo y status de factura

                                                if (updatePurchase.Item2 == null && ((updatePurchase.Item1 == listPaymentLines.Count*2) || (updatePurchase.Item1 == listPaymentLines.Count)))
                                                {


                                                }
                                                else
                                                {
                                                    swPaymentDetails = false;
                                                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: " + updatePurchase.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                                                }
                                            }

                                        }


                                        if (swPaymentDetails == true)
                                        {
                                            //Reducir saldo adeudado de asiento de socio de negocio

                                            bool swSaldoAdeudado = true;

                                            foreach (PaymentDetails rowPaymentDetails in listPaymentLines)
                                            {
                                                if (rowPaymentDetails.InvType == "18" && swSaldoAdeudado==true) //revisa el pago se realizo sobre una factura
                                                {
                                                    var saldoAdeudadoFactura = cn.ReduceSaldoAdeudadoSN(txtProveedor.Text, rowPaymentDetails.DocTransId);

                                                    if (saldoAdeudadoFactura.Item2 == null) {

                                                    }
                                                    else
                                                    {
                                                        swSaldoAdeudado = false;
                                                    }
                                                   
                                                }else if(rowPaymentDetails.InvType == "30" && swSaldoAdeudado == true)//revisa el pago se realizo sobre un asiento
                                                {
                                                    var saldoAdeudadoAsiento = cn.ReduceSaldoAdeudadoSN(txtProveedor.Text, rowPaymentDetails.DocTransId, rowPaymentDetails.Line_ID);

                                                    if (saldoAdeudadoAsiento.Item2 == null)
                                                    {

                                                    }
                                                    else
                                                    {
                                                        swSaldoAdeudado = false;
                                                    }
                                                }

                                            }

                                            if (swSaldoAdeudado == true)
                                            {
                                                cn.UpdateCreditDebitAccount(dtNewJournalEntry);

                                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Operacion se realizo exitosamente", Brushes.LightGreen, Brushes.Black, "001-interface.png");

                                                LimpiarCampos();
                                            }
                                            else
                                            {
                                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: " , Brushes.Red, Brushes.White, "003-interface-2.png");
                                            }

                                        }
                                        else
                                        {
                                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: " + result3.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");

                                        }
                                    }

                                    }
                                    else
                                    {
                                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: " + result2.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");

                                    }
                                }
                                else
                                {
                                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error en la creacion de la factura: " + result6.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                                }

                            //}
                            //else
                            //{
                            //    var deletePaymentMade = cn.DeletePaymentMade(payment.DocNum);

                            //    if (deletePaymentMade.Item2 != null)
                            //    {
                            //        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error en la creacion de la factura: " + deletePaymentMade.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                            //    }
                              
                            //    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error en la creacion de la factura: " + result13.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                            //}


                        }
                        else
                        {

                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error en la creacion de la factura: " + result5.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                        }
                        

                        btnCrear.Content = "OK";
                    }

                    ClearDataTableWithHoldingTax();

                    ClearDatataTableDocuments();

                    break;

                case "Actualizar":

                    payment.DocEntry = DocEntryPayment;
                    payment.DocDate = dpContabilizacion.SelectedDate;
                    payment.TaxDate = dpDocumento.SelectedDate;
                    payment.DocDueDate = dpVencimiento.SelectedDate;
                    payment.CardCode = txtProveedor.Text;
                    payment.CardName = txtNombre.Text;
                    payment.Address = txtPagarA.Text;
                    payment.CounterRef = txtReferencia.Text;
                    payment.JrnlMemo = txtComentario.Text;

                    listaPayment.Add(payment);

                    var result9 = cn.UpdatePaymentMade(listaPayment);

                    if (result9.Item1 == 1 && result9.Item2==null)
                    {
                        LimpiarCampos();

                        btnCrear.Content = "OK";

                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Operacion se realizo exitosamente", Brushes.LightGreen, Brushes.Black, "001-interface.png");
                        
                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " +result9.Item1, Brushes.Red, Brushes.White, "003-interface-2.png");

                    }

                    ClearDataTableWithHoldingTax();

                    ClearDatataTableDocuments();

                    break;
            }
        }

        private void ClearDatataTableDocuments()
        {
            dtDocuments.Rows.Clear();
        }

        private void ClearDataTableWithHoldingTax()
        {
            dtWithHoldingTax.Rows.Clear();
        }

        private Tuple<List<AsientoDetalle>, int> CreateListJournalEntryLines(List<AsientoCabecera> listaJournalEntry, Payment item2)
        {
            dtNewJournalEntry = CreateDatatableJournalEntryLines(item2);

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
                if (row["ShortName"].ToString() == txtProveedor.Text)
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
                    journalEntryLines.BalDueDeb = 0;
                    journalEntryLines.BalDueCred = 0;
                    journalEntryLines.BalFcDeb = 0;
                    journalEntryLines.BalFcCred = 0;
                    journalEntryLines.BalScCred = 0;
                    journalEntryLines.BalScDeb = 0;
                    journalEntryLines.UserSign = Properties.Settings.Default.Usuario;
                    journalEntryLines.FinncPriod = FinncPriod;
                    journalEntryLines.FCCurrency = TransCurr;
                    journalEntryLines.DataSource = 'N';

                    listaJournalEntryLines.Add(journalEntryLines);
                }
                else
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
                }


                k++;

            }

            return Tuple.Create(listaJournalEntryLines, k);
        }

        private List<PaymentDetails> CreateListPaymentLines(Payment payment, DataTable dtDocuments)
        {
            List<PaymentDetails> listPaymentDetails = new List<PaymentDetails>();

            listPaymentDetails = GetPurchase(dtDocuments, payment); //lista de detalles de pagos      

            return listPaymentDetails;
        }

        private Tuple<List<Payment>, Payment> CreateListPaymentComplete(Payment oldPayment)
        {
            List<Payment> listPayment = new List<Payment>();

            Payment payment = new Payment();

            DocNumPayment = Convert.ToInt32(txtNro.Text);
            payment.DocNum = Convert.ToInt32(txtNro.Text);
            payment.DocType = 'S';
            payment.Canceled = 'N';
            payment.Comments = txtComentario.Text;
            payment.DocDate = dpContabilizacion.SelectedDate;
            payment.TaxDate = dpDocumento.SelectedDate;
            payment.DocDueDate = dpVencimiento.SelectedDate;
            payment.CardCode = txtProveedor.Text;
            payment.CardName = txtNombre.Text;
            payment.Address = txtPagarA.Text;
            payment.UserSign = Properties.Settings.Default.Usuario;
            payment.UpdateDate = fechaActual.GetFechaActual();
            var result10 = cn.GetPeriodCode(payment.DocDate);
            payment.FinncPriod = result10.Item1;
            payment.ObjType = Convert.ToString(cn.GetTransType("PP"));
            payment.CashAcct = oldPayment.CashAcct;
            payment.TrsfrAcct = oldPayment.TrsfrAcct;
            payment.TrsfrDate = oldPayment.TrsfrDate;
            payment.TrsfrRef = oldPayment.TrsfrRef;
            payment.CashSum = oldPayment.CashSum;
            payment.CashSumFC = oldPayment.CashSumFC;
            payment.CashSumSy = oldPayment.CashSumSy;
            payment.TrsfrSum = oldPayment.TrsfrSum;
            payment.TrsfrSumFC = oldPayment.TrsfrSumFC;
            payment.TrsfrSumSy = oldPayment.TrsfrSumSy;
            payment.DocTotal = oldPayment.DocTotal;
            payment.DocTotalFC = oldPayment.DocTotalFC;
            payment.DocTotalSy = oldPayment.DocTotalSy;
            payment.CheckAcct = "";
            payment.CheckSum = 0;
            payment.CheckSumFC = 0;
            payment.CheckSumSy = 0;
            payment.PayNoDoc = 'N';
            payment.NoDocSum = 0;
            payment.NoDocSumFC = 0;
            payment.NoDocSumSy = 0;
            payment.CounterRef = txtReferencia.Text;
            var result4 = cn.SelectTransId();
            payment.TransId = result4.Item1;
            payment.JrnlMemo = txtComentario.Text;

            var find = FindTransTypeAndDocEntry();          

            var result12 = cn.GetDocumentAccount(find.Item1, cn.GetTransType(find.Item2).ToString());
            payment.BpAct = result12.Item1;

            payment.SysRate = Rate;

            payment.DocCurr = EstableceLogin.GetMediosPago().GetCurrency();

            DocCur = EstableceLogin.GetMediosPago().GetCurrency();

            if (payment.DocCurr == Properties.Settings.Default.MainCurrency)
            {
                DocRatePayment = 0;

                SysRatePayment = payment.SysRate;
            }
            else
            {

                FindRateFC(DocCur);

                payment.DocRate = RateFC;

                DocRatePayment = payment.DocRate;

                SysRatePayment = payment.SysRate;
            }

            listPayment.Add(payment);

            return Tuple.Create(listPayment, payment);
        }

        public Tuple<int, string> FindTransTypeAndDocEntry()
        {
            int DocEntry = 0;

            string transType = null;

           
            try
            {

                DataRow dataRow = dtDocuments.AsEnumerable().First();

                DocEntry = Convert.ToInt32(dataRow["DocEntry"]);

                transType = dataRow["TransType"].ToString();

                return Tuple.Create(DocEntry, transType);

            }
            catch (Exception e)
            {

                return Tuple.Create(DocEntry, e.Message);

            }
        }

        private Tuple<List<AsientoDetalle>, int> CreateListJournalEntryLinesNoDocPay(List<AsientoCabecera> listaJournalEntry, Payment item2)
        {
            dtNewJournalEntry = CreateDatatableJournalEntryLinesNoDocPay(item2);

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

        
        private List<AsientoCabecera> CreateJournalEntry(List<Payment> listPayment)
        {
            int TransId = 0;

            string DocCurr = null;

            DateTime? DocDate = null;

            DateTime? DocDueDate = null;

            DateTime? TaxDate = null;

            int FinncPriod = 0;

            string ObjType = null;

            string JrnlMemo = null;

            decimal DocTotal = 0;

            decimal DocTotalFC = 0;

            decimal DocTotalSy = 0;


            foreach (Payment payment in listPayment)
            {
                TransId = payment.TransId;
                DocCurr = payment.DocCurr;
                DocDate = payment.DocDate;
                DocDueDate = payment.DocDueDate;
                TaxDate = payment.TaxDate;
                FinncPriod = payment.FinncPriod;
                ObjType = payment.ObjType;
                JrnlMemo = payment.JrnlMemo;
                DocTotal = payment.DocTotal;
                DocTotalFC = payment.DocTotalFC;
                DocTotalSy = payment.DocTotalSy;

            }

            List<AsientoCabecera> listaJournalEntry = new List<AsientoCabecera>();

            AsientoCabecera journalEntry = new AsientoCabecera();

            journalEntry.TransId = TransId;
            journalEntry.RefDate = DocDate;
            journalEntry.TaxDate = TaxDate;
            journalEntry.DueDate = DocDueDate;
            journalEntry.Memo = JrnlMemo;
            var baseRef = cn.GetBaseRef(ObjType);
            journalEntry.BaseRef = baseRef.Item1;
            journalEntry.Ref1 = "";
            journalEntry.Ref2 = "";
            journalEntry.UserSign = Properties.Settings.Default.Usuario;
            journalEntry.UpdateDate = fechaActual.GetFechaActual();
            journalEntry.FinncPriod = FinncPriod;
            journalEntry.LocTotal = DocTotal;
            journalEntry.SysTotal = DocTotalSy;
            journalEntry.FcTotal = DocTotalFC;
            journalEntry.TransType = Convert.ToInt32(ObjType);
            journalEntry.TransCurr = DocCurr;

            listaJournalEntry.Add(journalEntry);

            return listaJournalEntry;
        }

        private Tuple<List<Payment>,Payment> CreateListPayment(Payment oldPayment)
        {
            List<Payment> listPayment = new List<Payment>();

            Payment payment = new Payment();

            payment.DocNum = Convert.ToInt32(txtNro.Text);
            payment.DocType = 'S';
            payment.Canceled = 'N';
            payment.Comments = txtComentario.Text;
            payment.DocDate = dpContabilizacion.SelectedDate;
            payment.TaxDate = dpDocumento.SelectedDate;
            payment.DocDueDate = dpVencimiento.SelectedDate;
            payment.CardCode = txtProveedor.Text;
            payment.CardName = txtNombre.Text;
            payment.Address = txtPagarA.Text;
            payment.UserSign = Properties.Settings.Default.Usuario;
            payment.UpdateDate = fechaActual.GetFechaActual();
            var result10 = cn.GetPeriodCode(payment.DocDate);
            payment.FinncPriod = result10.Item1;
            payment.ObjType = Convert.ToString(cn.GetTransType("PP"));
            payment.CashAcct = oldPayment.CashAcct;
            payment.TrsfrAcct = oldPayment.TrsfrAcct;
            payment.TrsfrDate = oldPayment.TrsfrDate;
            payment.TrsfrRef = oldPayment.TrsfrRef;
            payment.CashSum = oldPayment.CashSum;
            payment.CashSumFC = oldPayment.CashSumFC;
            payment.CashSumSy = oldPayment.CashSumSy;
            payment.TrsfrSum = oldPayment.TrsfrSum;
            payment.TrsfrSumFC = oldPayment.TrsfrSumFC;
            payment.TrsfrSumSy = oldPayment.TrsfrSumSy;
            payment.DocTotal = oldPayment.DocTotal;
            payment.DocTotalFC = oldPayment.DocTotalFC;
            payment.DocTotalSy = oldPayment.DocTotalSy;
            payment.CheckAcct = "";
            payment.CheckSum = 0;
            payment.CheckSumFC = 0;
            payment.CheckSumSy = 0;
            payment.PayNoDoc = 'Y';

            if (payment.TrsfrSum != 0)
            {
                payment.NoDocSum = payment.TrsfrSum;
                payment.NoDocSumFC = payment.TrsfrSumFC;
                payment.NoDocSumSy = payment.TrsfrSumSy;
            }
            else if (payment.CashSum != 0)
            {
                payment.NoDocSum = payment.CashSum;
                payment.NoDocSumFC = payment.CashSumFC;
                payment.NoDocSumSy = payment.CashSumSy;
            }

            payment.CounterRef = txtReferencia.Text;
            var result4 = cn.SelectTransId();
            payment.TransId = result4.Item1;
            payment.JrnlMemo = txtComentario.Text;
            payment.BpAct = txtCuentaAsociada.Text;

            payment.SysRate = Rate;

            payment.DocCurr = EstableceLogin.GetMediosPago().GetCurrency();

            DocCur = EstableceLogin.GetMediosPago().GetCurrency();

            if (payment.DocCurr == Properties.Settings.Default.MainCurrency)
            {
                payment.DocRate = 0;

                RatePayment = payment.SysRate;
            }
            else
            {
                FindRateFC(DocCur);

                payment.DocRate = RateFC;

                RatePayment = payment.DocRate;
            }

            listPayment.Add(payment);

            return Tuple.Create(listPayment,payment);
        }

        private DataTable CreateDatatableJournalEntryLinesNoDocPay(Payment payment)
        {
            dtJournalEntry.Rows.Clear();

            if (DocCur == Properties.Settings.Default.MainCurrency) //Si el pago es en moneda local
            {

             dtJournalEntry = CreateJounalEntryBasicPayNoDoc(dtJournalEntry, payment);

            }
            else //Si el pago es en moneda del extranjera
            {
                
             dtJournalEntry = CreateJounalEntryBasicPayNoDoc(dtJournalEntry, payment);
             
            }

            return dtJournalEntry;
        }

        

        private DataTable CreateJounalEntryBasicPayNoDoc(DataTable dtJournalEntry, Payment payment)
        {
            DataRow newRow = dtJournalEntry.NewRow();

            dtJournalEntry.Rows.Add(newRow);

            newRow["ShortName"] = AcctCode;

            newRow["ContraAct"] = txtProveedor.Text;

            newRow["Account"] = AcctCode;

            if (String.IsNullOrWhiteSpace(payment.TrsfrAcct) == false)
            {
                newRow["Credit"] = payment.TrsfrSum;
                newRow["SYSCred"] = payment.TrsfrSumSy;
                newRow["FCCredit"] = payment.TrsfrSumFC;

            }
            else if (String.IsNullOrWhiteSpace(payment.CashAcct) == false)
            {
                newRow["Credit"] = payment.CashSum;
                newRow["SYSCred"] = payment.CashSumSy;
                newRow["FCCredit"] = payment.CashSumFC;
            }

            newRow["LineMemo"] = txtComentario.Text;

            //Create BP

            DataRow newRow1 = dtJournalEntry.NewRow();

            dtJournalEntry.Rows.Add(newRow1);

            newRow1["ShortName"] = txtProveedor.Text;

            newRow1["ContraAct"] = AcctCode;

            newRow1["Account"] = txtCuentaAsociada.Text;

            if (String.IsNullOrWhiteSpace(payment.TrsfrAcct) == false)
            {
                newRow1["Debit"] = payment.TrsfrSum;
                newRow1["SYSDeb"] = payment.TrsfrSumSy;
                newRow1["FCDebit"] = payment.TrsfrSumFC;

            }
            else if (String.IsNullOrWhiteSpace(payment.CashAcct) == false)
            {
                newRow1["Debit"] = payment.CashSum;
                newRow1["SYSDeb"] = payment.CashSumSy;
                newRow1["FCDebit"] = payment.CashSumFC;
            }

            newRow1["LineMemo"] = txtComentario.Text;

            return dtJournalEntry;
        }

       
        private DataTable CreateDatatableJournalEntryLines(Payment payment)
        {           
            dtJournalEntry.Rows.Clear();

            //--------------------------------------Recorrer Tabla-------------------------------------------

            foreach (DataRow row in dtDocuments.Rows)
            {

                if (row["DocCur"].ToString() == Properties.Settings.Default.MainCurrency) //Si la factura es en moneda local
                {
                    if(DocCur== Properties.Settings.Default.MainCurrency)
                    {
                        if (ConvertDecimalTwoPlaces(row["SysRate"].ToString()) == SysRatePayment) //Si el diferencial es el mismo
                        {
                            dtJournalEntry = CreateJounalEntryBasic(dtJournalEntry, payment, row);

                        }
                        else //Diferencial no es igual
                        {

                            if (ConvertDecimalTwoPlaces(row["SysRate"].ToString()) < SysRatePayment)
                            {
                                dtJournalEntry = CreateJounalEntryBasicMainCurrency(dtJournalEntry, payment, row);
                            }
                            else
                            {
                                dtJournalEntry = CreateJounalEntryBasicMainCurrency1(dtJournalEntry, payment, row);
                            }

                        }

                    }
                    else 
                    {
                        
                            if (ConvertDecimalTwoPlaces(row["SysRate"].ToString()) == DocRatePayment) //Si el diferencial es el mismo
                            {
                                dtJournalEntry = CreateJounalEntryBasicPayFRCurrency(dtJournalEntry, payment, row);

                            }
                            else //Diferencial no es igual
                            {

                                if (ConvertDecimalTwoPlaces(row["SysRate"].ToString()) < DocRatePayment)
                                {
                                    dtJournalEntry = CreateJounalEntryBasicFRCurrency(dtJournalEntry, payment, row);
                                }
                                else
                                {
                                    dtJournalEntry = CreateJounalEntryBasicFRCurrency1(dtJournalEntry, payment, row);
                                }

                            }                        
                    }

                }
                else  //Si la factura moneda de sistema
                {
                    if (DocCur == Properties.Settings.Default.MainCurrency) //si el pago es en moneda local
                    {
                        if (ConvertDecimalTwoPlaces(row["DocRate"].ToString()) == SysRatePayment) //Si la tasa es igual a la de la factura
                        {
                            dtJournalEntry = CreateJounalEntryBasicFrgnPayMain(dtJournalEntry, payment, row);
                        }
                        else //Diferencial no es igual
                        {
                            if (ConvertDecimalTwoPlaces(row["DocRate"].ToString()) < SysRatePayment)
                            {
                                dtJournalEntry = CreateJounalEntryBasicFCCurrencyMainPay(dtJournalEntry, payment, row);
                            }
                            else
                            {
                                dtJournalEntry = CreateJounalEntryBasicFCCurrencyMainPay1(dtJournalEntry, payment, row);
                            }
                        }

                    }
                    else   //si el pago es en moneda extranjera
                    {
                        if (ConvertDecimalTwoPlaces(row["DocRate"].ToString()) == DocRatePayment) //Si la tasa es igual a la de la factura
                        {
                            dtJournalEntry = CreateJounalEntryBasicFrgn(dtJournalEntry, payment, row);
                        }
                        else //Diferencial no es igual
                        {
                            if (ConvertDecimalTwoPlaces(row["DocRate"].ToString()) < DocRatePayment)
                            {
                                dtJournalEntry = CreateJounalEntryBasicFCCurrency(dtJournalEntry, payment, row);
                            }
                            else
                            {
                                dtJournalEntry = CreateJounalEntryBasicFCCurrency1(dtJournalEntry, payment, row);
                            }
                        }
                    }

                }

            }
            
            return dtJournalEntry;
    
        }

        private DataTable CreateJounalEntryBasicFCCurrency1(DataTable dtJournalEntry, Payment payment, DataRow row)
        {
            //Create Line Account

            decimal diferrenceAmount = 0;

            decimal diferrenceAmountMain = 0;

            DataRow newRow = dtJournalEntry.NewRow();

            dtJournalEntry.Rows.Add(newRow);

            newRow["ShortName"] = AcctCode;

            newRow["Account"] = AcctCode;

            newRow["ContraAct"] = txtProveedor.Text;

            if (String.IsNullOrWhiteSpace(payment.TrsfrAcct) == false)
            {
                newRow["Credit"] = payment.TrsfrSum;
                newRow["SYSCred"] = payment.TrsfrSumSy;
                newRow["FCCredit"] = payment.TrsfrSumFC;

            }
            else if (String.IsNullOrWhiteSpace(payment.CashAcct) == false)
            {
                newRow["Credit"] = payment.CashSum;
                newRow["SYSCred"] = payment.CashSumSy;
                newRow["FCCredit"] = payment.CashSumFC;
            }

            newRow["LineMemo"] = txtComentario.Text;

            //Create BP

            DataRow newRow1 = dtJournalEntry.NewRow();

            dtJournalEntry.Rows.Add(newRow1);

            newRow1["ShortName"] = txtProveedor.Text;

            newRow1["ContraAct"] = AcctCode;

            var result = cn.GetDocumentAccount(Convert.ToInt32(row["DocEntry"]), cn.GetTransType(row["TransType"].ToString()).ToString());

            newRow1["Account"] = result.Item1;

            newRow1["Debit"] = ConvertDecimalTwoPlaces(row["PaidFCWithOutHoldingTax"]);

            newRow1["SYSDeb"] = ConvertDecimalTwoPlaces(row["PaidSysWithOutHoldingTax"]);

            newRow1["FCDebit"] = ConvertDecimalTwoPlaces(row["PaidWithOutHoldingTax"]);

            newRow1["LineMemo"] = txtComentario.Text;

            //TaxHoldingPaid

            if ((ConvertDecimalTwoPlaces(row["PaidWithHoldingTax"]) == 0) && (ConvertDecimalTwoPlaces(row["PaidSysWithHoldingTax"]) == 0)) //la factura tiene pago asociado
            {
                var dtRetencionesTipoPago = GetRetencionesTipoPago(Convert.ToInt32(row["DocEntry"].ToString()), (cn.GetTransType(row["TransType"].ToString())).ToString()); //Buscar las retenciones de tipo pago en la factura

                if (dtRetencionesTipoPago.Item1.Rows.Count > 0 && dtRetencionesTipoPago.Item2 == null) //Cuenta la cantidad de retenciones de tipo pago asociadas a la factura
                {
                    foreach (DataRow rowRetenciones in dtRetencionesTipoPago.Item1.Rows) //Recorre coleccion
                    {
                        DataRow newRow4 = dtJournalEntry.NewRow();

                        dtJournalEntry.Rows.Add(newRow4);

                        newRow4["ShortName"] = rowRetenciones["Account"];

                        newRow4["Account"] = rowRetenciones["Account"];

                        newRow4["ContraAct"] = txtProveedor.Text;

                        newRow4["Credit"] = DocRatePayment * ConvertDecimalTwoPlaces(rowRetenciones["TaxbleAmnt"]);

                        newRow4["SYSCred"] = ConvertDecimalTwoPlaces(newRow4["Credit"]) / SysRatePayment;

                        diferrenceAmount = diferrenceAmount + ConvertDecimalTwoPlaces(newRow4["SYSCred"]);

                        diferrenceAmountMain = diferrenceAmountMain + ConvertDecimalTwoPlaces(newRow4["Credit"]);

                        newRow4["FCCredit"] = ConvertDecimalTwoPlaces(newRow4["Credit"]) / DocRatePayment;

                        newRow4["LineMemo"] = txtComentario.Text;
                    }
                }
                else
                {
                    diferrenceAmount = 0;
                }
            }

            //Create Line Diferencial 1

            DataRow newRow2 = dtJournalEntry.NewRow();

            dtJournalEntry.Rows.Add(newRow2);

            var result2 = cn.FindBeneficioDiferenciaTipoCambio();

            newRow2["ShortName"] = result2.Item1;

            newRow2["Account"] = result2.Item1;

            newRow2["ContraAct"] = txtProveedor.Text;

            newRow2["Credit"] = ConvertDecimalTwoPlaces(newRow1["Debit"])-(ConvertDecimalTwoPlaces(newRow["Credit"])+ diferrenceAmountMain);

            newRow2["SYSCred"] = ConvertDecimalTwoPlaces(newRow2["Credit"]) / SysRatePayment;

            newRow2["FCCredit"] = 0;

            newRow2["LineMemo"] = txtComentario.Text;

            //Create Line Diferencial 2

            DataRow newRow3 = dtJournalEntry.NewRow();

            dtJournalEntry.Rows.Add(newRow3);

            var result3 = cn.FindPerdidaDiferenciaConversion();

            newRow2["ContraAct"] = txtProveedor.Text;

            newRow3["ShortName"] = result3.Item1;

            newRow3["Account"] = result3.Item1;

            newRow3["Debit"] = 0;

            newRow3["SYSDeb"] = ConvertDecimalTwoPlaces(newRow2["SYSCred"]);

            newRow3["FCDebit"] = 0;

            newRow3["LineMemo"] = txtComentario.Text;

            return dtJournalEntry;
        }

        private DataTable CreateJounalEntryBasicFrgnPayMain1(DataTable dtJournalEntry, Payment payment, DataRow row)
        {
            //Create Line Account

            decimal diferrenceAmount = 0;

            decimal diferrenceAmountMain = 0;

            DataRow newRow = dtJournalEntry.NewRow();

            dtJournalEntry.Rows.Add(newRow);

            newRow["ShortName"] = AcctCode;

            newRow["Account"] = AcctCode;

            newRow["ContraAct"] = txtProveedor.Text;

            if (String.IsNullOrWhiteSpace(payment.TrsfrAcct) == false)
            {
                newRow["Credit"] = payment.TrsfrSum;
                newRow["SYSCred"] = payment.TrsfrSumSy;
                newRow["FCCredit"] = payment.TrsfrSumFC;

            }
            else if (String.IsNullOrWhiteSpace(payment.CashAcct) == false)
            {
                newRow["Credit"] = payment.CashSum;
                newRow["SYSCred"] = payment.CashSumSy;
                newRow["FCCredit"] = payment.CashSumFC;
            }

            newRow["LineMemo"] = txtComentario.Text;

            //Create BP

            DataRow newRow1 = dtJournalEntry.NewRow();

            dtJournalEntry.Rows.Add(newRow1);

            newRow1["ShortName"] = txtProveedor.Text;

            newRow1["ContraAct"] = AcctCode;

            var result = cn.GetDocumentAccount(Convert.ToInt32(row["DocEntry"]), cn.GetTransType(row["TransType"].ToString()).ToString());

            newRow1["Account"] = result.Item1;

            newRow1["Debit"] = row["PaidFC"];

            newRow1["SYSDeb"] = row["PaidSys"];

            newRow1["FCDebit"] = row["Paid"];

            newRow1["LineMemo"] = txtComentario.Text;

            //TaxHoldingPaid

            if ((ConvertDecimalTwoPlaces(row["PaidWithHoldingTax"]) == ConvertDecimalTwoPlaces(row["PaidFC"])) && (ConvertDecimalTwoPlaces(row["PaidSysWithHoldingTax"]) == ConvertDecimalTwoPlaces(row["PaidSys"]))) //la factura tiene pago asociado
            {
                var dtRetencionesTipoPago = GetRetencionesTipoPago(Convert.ToInt32(row["DocEntry"].ToString()), (cn.GetTransType(row["TransType"].ToString())).ToString()); //Buscar las retenciones de tipo pago en la factura

                if (dtRetencionesTipoPago.Item1.Rows.Count > 0 && dtRetencionesTipoPago.Item2 == null) //Cuenta la cantidad de retenciones de tipo pago asociadas a la factura
                {
                    foreach (DataRow rowRetenciones in dtRetencionesTipoPago.Item1.Rows) //Recorre coleccion
                    {
                        DataRow newRow4 = dtJournalEntry.NewRow();

                        dtJournalEntry.Rows.Add(newRow1);

                        newRow4["ShortName"] = rowRetenciones["Account"];

                        newRow4["Account"] = rowRetenciones["Account"];

                        newRow4["ContraAct"] = txtProveedor.Text;

                        newRow4["Credit"] = DocRatePayment * ConvertDecimalTwoPlaces(row["TaxbleAmntFC"]);

                        newRow4["SYSCred"] = ConvertDecimalTwoPlaces(newRow4["Credit"]) / SysRatePayment;

                        diferrenceAmount = diferrenceAmount + ConvertDecimalTwoPlaces(newRow4["SYSCred"]);

                        diferrenceAmountMain = diferrenceAmountMain + ConvertDecimalTwoPlaces(newRow4["Credit"]);

                        newRow4["FCCredit"] = ConvertDecimalTwoPlaces(newRow4["Credit"]) / DocRatePayment;

                        newRow4["LineMemo"] = txtComentario.Text;
                    }
                }
                else
                {
                    diferrenceAmount = 0;
                }
            }

            //Create Line Diferencial 1

            DataRow newRow2 = dtJournalEntry.NewRow();

            dtJournalEntry.Rows.Add(newRow2);

            var result2 = cn.FindBeneficioDiferenciaTipoCambio();

            newRow2["ShortName"] = result2.Item1;

            newRow2["Account"] = result2.Item1;

            newRow2["ContraAct"] = txtProveedor.Text;

            newRow2["Credit"] = ConvertDecimalTwoPlaces(newRow1["Debit"]) - (ConvertDecimalTwoPlaces(newRow["Credit"]) + diferrenceAmountMain);

            newRow2["SYSCred"] = ConvertDecimalTwoPlaces(newRow2["Credit"]) / SysRatePayment;

            newRow2["FCCredit"] = 0;

            newRow2["LineMemo"] = txtComentario.Text;

            //Create Line Diferencial 2

            DataRow newRow3 = dtJournalEntry.NewRow();

            dtJournalEntry.Rows.Add(newRow3);

            var result3 = cn.FindPerdidaDiferenciaConversion();

            newRow2["ContraAct"] = txtProveedor.Text;

            newRow3["ShortName"] = result3.Item1;

            newRow3["Account"] = result3.Item1;

            newRow3["Debit"] = 0;

            newRow3["SYSDeb"] = ConvertDecimalTwoPlaces(newRow2["SYSCred"]);

            newRow3["FCDebit"] = 0;

            newRow3["LineMemo"] = txtComentario.Text;

            return dtJournalEntry;
        }

        private DataTable CreateJounalEntryBasicFCCurrency(DataTable dtJournalEntry, Payment payment, DataRow row)
        {
            //Create Line Account

            decimal diferrenceAmount = 0;

            decimal diferrenceAmountMain = 0;

            DataRow newRow = dtJournalEntry.NewRow();

            dtJournalEntry.Rows.Add(newRow);

            newRow["ShortName"] = AcctCode;

            newRow["Account"] = AcctCode;

            newRow["ContraAct"] = txtProveedor.Text;

            if (String.IsNullOrWhiteSpace(payment.TrsfrAcct) == false)
            {
                newRow["Credit"] = payment.TrsfrSum;
                newRow["SYSCred"] = payment.TrsfrSumSy;
                newRow["FCCredit"] = payment.TrsfrSumFC;

            }
            else if (String.IsNullOrWhiteSpace(payment.CashAcct) == false)
            {
                newRow["Credit"] = payment.CashSum;
                newRow["SYSCred"] = payment.CashSumSy;
                newRow["FCCredit"] = payment.CashSumFC;
            }

            newRow["LineMemo"] = txtComentario.Text;

            //Create BP

            DataRow newRow1 = dtJournalEntry.NewRow();

            dtJournalEntry.Rows.Add(newRow1);

            newRow1["ShortName"] = txtProveedor.Text;

            var result = cn.GetDocumentAccount(Convert.ToInt32(row["DocEntry"]), cn.GetTransType(row["TransType"].ToString()).ToString());

            newRow1["Account"] = result.Item1;

            newRow1["ContraAct"] = AcctCode;

            newRow1["Debit"] = ConvertDecimalTwoPlaces(row["PaidFCWithOutHoldingTax"]);

            newRow1["SYSDeb"] = ConvertDecimalTwoPlaces(row["PaidSysWithOutHoldingTax"]);

            newRow1["FCDebit"] = ConvertDecimalTwoPlaces(row["PaidWithOutHoldingTax"]);

            newRow1["LineMemo"] = txtComentario.Text;

            if ((ConvertDecimalTwoPlaces(row["PaidWithHoldingTax"]) == 0) && (ConvertDecimalTwoPlaces(row["PaidSysWithHoldingTax"]) == 0)) //la factura tiene pago asociado
            {
                var dtRetencionesTipoPago = GetRetencionesTipoPago(Convert.ToInt32(row["DocEntry"].ToString()), (cn.GetTransType(row["TransType"].ToString())).ToString()); //Buscar las retenciones de tipo pago en la factura

                if (dtRetencionesTipoPago.Item1.Rows.Count > 0 && dtRetencionesTipoPago.Item2 == null) //Cuenta la cantidad de retenciones de tipo pago asociadas a la factura
                {
                    foreach (DataRow rowRetenciones in dtRetencionesTipoPago.Item1.Rows) //Recorre coleccion
                    {
                        DataRow newRow4 = dtJournalEntry.NewRow();

                        dtJournalEntry.Rows.Add(newRow4);

                        newRow4["ShortName"] = rowRetenciones["Account"];

                        newRow4["Account"] = rowRetenciones["Account"];

                        newRow4["ContraAct"] = txtProveedor.Text;

                        newRow4["Credit"] = DocRatePayment * ConvertDecimalTwoPlaces(rowRetenciones["TaxbleAmnt"]);

                        newRow4["SYSCred"] = ConvertDecimalTwoPlaces(newRow4["Credit"]) / SysRatePayment;

                        diferrenceAmount = diferrenceAmount + ConvertDecimalTwoPlaces(newRow4["SYSCred"]);

                        diferrenceAmountMain= diferrenceAmountMain+ ConvertDecimalTwoPlaces(newRow4["Credit"]);

                        newRow4["FCCredit"] = ConvertDecimalTwoPlaces(newRow4["Credit"]) / DocRatePayment;

                        newRow4["LineMemo"] = txtComentario.Text;
                    }
                }
                else
                {
                    diferrenceAmount = 0;
                }
            }

            //Create Line Diferencial 1

            DataRow newRow2 = dtJournalEntry.NewRow();

            dtJournalEntry.Rows.Add(newRow2);

            var result2 = cn.FindPerdidaDiferenciaTipoCambio();

            newRow2["Account"] = result2.Item1;

            newRow2["ShortName"] = result2.Item1;

            newRow2["ContraAct"] = txtProveedor.Text;

            newRow2["Debit"] = (ConvertDecimalTwoPlaces(newRow["Credit"]) + diferrenceAmountMain) - ConvertDecimalTwoPlaces(newRow1["Debit"]);

            newRow2["SYSDeb"] = ConvertDecimalTwoPlaces(newRow2["Debit"]) / SysRatePayment;

            newRow2["FCDebit"] = 0;

            newRow2["LineMemo"] = txtComentario.Text;

            //Create Line Diferencial 2

            DataRow newRow3 = dtJournalEntry.NewRow();

            dtJournalEntry.Rows.Add(newRow3);

            var result3 = cn.FindBeneficioDiferenciaConversion();

            newRow3["Account"] = result3.Item1;

            newRow3["ShortName"] = result3.Item1;

            newRow3["ContraAct"] = txtProveedor.Text;

            newRow3["Credit"] = 0;

            newRow3["SYSCred"] =ConvertDecimalTwoPlaces(newRow2["SYSDeb"]);

            newRow3["FCCredit"] = 0;

            newRow3["LineMemo"] = txtComentario.Text;

            return dtJournalEntry;
        }

        private DataTable CreateJounalEntryBasicFCCurrencyMainPay(DataTable dtJournalEntry, Payment payment, DataRow row)
        {
            //Create Line Account

            decimal diferrenceAmount = 0;

            decimal diferrenceAmountMain = 0;

            DataRow newRow = dtJournalEntry.NewRow();

            dtJournalEntry.Rows.Add(newRow);

            newRow["ShortName"] = AcctCode;

            newRow["Account"] = AcctCode;

            newRow["ContraAct"] = txtProveedor.Text;

            if (String.IsNullOrWhiteSpace(payment.TrsfrAcct) == false)
            {
                newRow["Credit"] = payment.TrsfrSum;
                newRow["SYSCred"] = payment.TrsfrSumSy;
                newRow["FCCredit"] = payment.TrsfrSumFC;

            }
            else if (String.IsNullOrWhiteSpace(payment.CashAcct) == false)
            {
                newRow["Credit"] = payment.CashSum;
                newRow["SYSCred"] = payment.CashSumSy;
                newRow["FCCredit"] = payment.CashSumFC;
            }

            newRow["LineMemo"] = txtComentario.Text;

            //Create BP

            DataRow newRow1 = dtJournalEntry.NewRow();

            dtJournalEntry.Rows.Add(newRow1);

            newRow1["ShortName"] = txtProveedor.Text;

            var result = cn.GetDocumentAccount(Convert.ToInt32(row["DocEntry"]), cn.GetTransType(row["TransType"].ToString()).ToString());

            newRow1["Account"] = result.Item1;

            newRow1["ContraAct"] = AcctCode;

            newRow1["Debit"] = ConvertDecimalTwoPlaces(row["PaidFCWithOutHoldingTax"]);

            newRow1["SYSDeb"] = ConvertDecimalTwoPlaces(row["PaidSysWithOutHoldingTax"]);

            newRow1["FCDebit"] = 0;

            newRow1["LineMemo"] = txtComentario.Text;

            if ((ConvertDecimalTwoPlaces(row["PaidWithHoldingTax"]) == 0) && (ConvertDecimalTwoPlaces(row["PaidSysWithHoldingTax"]) == 0)) //la factura tiene pago asociado
            {
                var dtRetencionesTipoPago = GetRetencionesTipoPago(Convert.ToInt32(row["DocEntry"].ToString()), (cn.GetTransType(row["TransType"].ToString())).ToString()); //Buscar las retenciones de tipo pago en la factura

                if (dtRetencionesTipoPago.Item1.Rows.Count > 0 && dtRetencionesTipoPago.Item2 == null) //Cuenta la cantidad de retenciones de tipo pago asociadas a la factura
                {
                    foreach (DataRow rowRetenciones in dtRetencionesTipoPago.Item1.Rows) //Recorre coleccion
                    {
                        DataRow newRow4 = dtJournalEntry.NewRow();

                        dtJournalEntry.Rows.Add(newRow4);

                        newRow4["ShortName"] = rowRetenciones["Account"];

                        newRow4["Account"] = rowRetenciones["Account"];

                        newRow4["ContraAct"] = txtProveedor.Text;

                        newRow4["Credit"] = ConvertDecimalTwoPlaces(rowRetenciones["TaxbleAmntSC"])*SysRatePayment;

                        newRow4["SYSCred"] = ConvertDecimalTwoPlaces(newRow4["Credit"]) / SysRatePayment;

                        diferrenceAmount = diferrenceAmount + ConvertDecimalTwoPlaces(newRow4["SYSCred"]);

                        diferrenceAmountMain = diferrenceAmountMain + ConvertDecimalTwoPlaces(newRow4["Credit"]);

                        newRow4["FCCredit"] = 0;

                        newRow4["LineMemo"] = txtComentario.Text;
                    }
                }
                else
                {
                    diferrenceAmount = 0;
                }
            }

            //Create Line Diferencial 1

            DataRow newRow2 = dtJournalEntry.NewRow();

            dtJournalEntry.Rows.Add(newRow2);

            var result2 = cn.FindPerdidaDiferenciaTipoCambio();

            newRow2["Account"] = result2.Item1;

            newRow2["ShortName"] = result2.Item1;

            newRow2["ContraAct"] = txtProveedor.Text;

            newRow2["Debit"] = (ConvertDecimalTwoPlaces(newRow["Credit"]) + diferrenceAmountMain) - ConvertDecimalTwoPlaces(newRow1["Debit"]);

            newRow2["SYSDeb"] = ConvertDecimalTwoPlaces(newRow2["Debit"]) / SysRatePayment;

            newRow2["FCDebit"] = 0;

            newRow2["LineMemo"] = txtComentario.Text;

            //Create Line Diferencial 2

            DataRow newRow3 = dtJournalEntry.NewRow();

            dtJournalEntry.Rows.Add(newRow3);

            var result3 = cn.FindBeneficioDiferenciaConversion();

            newRow3["Account"] = result3.Item1;

            newRow3["ShortName"] = result3.Item1;

            newRow3["ContraAct"] = txtProveedor.Text;

            newRow3["Credit"] = 0;

            newRow3["SYSCred"] = ConvertDecimalTwoPlaces(newRow2["SYSDeb"]);

            newRow3["FCCredit"] = 0;

            newRow3["LineMemo"] = txtComentario.Text;

            return dtJournalEntry;
        }

        private DataTable CreateJounalEntryBasicFCCurrencyMainPay1(DataTable dtJournalEntry, Payment payment, DataRow row)
        {
            //Create Line Account

            decimal diferrenceAmount = 0;

            decimal diferrenceAmountMain = 0;

            DataRow newRow = dtJournalEntry.NewRow();

            dtJournalEntry.Rows.Add(newRow);

            newRow["ShortName"] = AcctCode;

            newRow["Account"] = AcctCode;

            newRow["ContraAct"] = txtProveedor.Text;

            if (String.IsNullOrWhiteSpace(payment.TrsfrAcct) == false)
            {
                newRow["Credit"] = payment.TrsfrSum;
                newRow["SYSCred"] = payment.TrsfrSumSy;
                newRow["FCCredit"] = payment.TrsfrSumFC;

            }
            else if (String.IsNullOrWhiteSpace(payment.CashAcct) == false)
            {
                newRow["Credit"] = payment.CashSum;
                newRow["SYSCred"] = payment.CashSumSy;
                newRow["FCCredit"] = payment.CashSumFC;
            }

            newRow["LineMemo"] = txtComentario.Text;

            //Create BP

            DataRow newRow1 = dtJournalEntry.NewRow();

            dtJournalEntry.Rows.Add(newRow1);

            newRow1["ShortName"] = txtProveedor.Text;

            newRow1["ContraAct"] = AcctCode;

            var result = cn.GetDocumentAccount(Convert.ToInt32(row["DocEntry"]), cn.GetTransType(row["TransType"].ToString()).ToString());

            newRow1["Account"] = result.Item1;

            newRow1["Debit"] = ConvertDecimalTwoPlaces(row["PaidFCWithOutHoldingTax"]);

            newRow1["SYSDeb"] = ConvertDecimalTwoPlaces(row["PaidSysWithOutHoldingTax"]);

            newRow1["FCDebit"] = 0;

            newRow1["LineMemo"] = txtComentario.Text;

            //TaxHoldingPaid

            if ((ConvertDecimalTwoPlaces(row["PaidWithHoldingTax"]) == 0) && (ConvertDecimalTwoPlaces(row["PaidSysWithHoldingTax"]) == 0)) //la factura tiene pago asociado
            {
                var dtRetencionesTipoPago = GetRetencionesTipoPago(Convert.ToInt32(row["DocEntry"].ToString()), (cn.GetTransType(row["TransType"].ToString())).ToString()); //Buscar las retenciones de tipo pago en la factura

                if (dtRetencionesTipoPago.Item1.Rows.Count > 0 && dtRetencionesTipoPago.Item2 == null) //Cuenta la cantidad de retenciones de tipo pago asociadas a la factura
                {
                    foreach (DataRow rowRetenciones in dtRetencionesTipoPago.Item1.Rows) //Recorre coleccion
                    {
                        DataRow newRow4 = dtJournalEntry.NewRow();

                        dtJournalEntry.Rows.Add(newRow4);

                        newRow4["ShortName"] = rowRetenciones["Account"];

                        newRow4["Account"] = rowRetenciones["Account"];

                        newRow4["ContraAct"] = txtProveedor.Text;

                        newRow4["Credit"] = ConvertDecimalTwoPlaces(rowRetenciones["TaxbleAmntSC"]) * SysRatePayment;

                        newRow4["SYSCred"] = ConvertDecimalTwoPlaces(newRow4["Credit"]) / SysRatePayment;

                        diferrenceAmount = diferrenceAmount + ConvertDecimalTwoPlaces(newRow4["SYSCred"]);

                        diferrenceAmountMain = diferrenceAmountMain + ConvertDecimalTwoPlaces(newRow4["Credit"]);

                        newRow4["FCCredit"] = 0;

                        newRow4["LineMemo"] = txtComentario.Text;
                    }
                }
                else
                {
                    diferrenceAmount = 0;
                }
            }

            //Create Line Diferencial 1

            DataRow newRow2 = dtJournalEntry.NewRow();

            dtJournalEntry.Rows.Add(newRow2);

            var result2 = cn.FindBeneficioDiferenciaTipoCambio();

            newRow2["ShortName"] = result2.Item1;

            newRow2["Account"] = result2.Item1;

            newRow2["ContraAct"] = txtProveedor.Text;

            newRow2["Credit"] = ConvertDecimalTwoPlaces(newRow1["Debit"]) - (ConvertDecimalTwoPlaces(newRow["Credit"]) + diferrenceAmountMain);

            newRow2["SYSCred"] = ConvertDecimalTwoPlaces(newRow2["Credit"]) / SysRatePayment;

            newRow2["FCCredit"] = 0;

            newRow2["LineMemo"] = txtComentario.Text;

            //Create Line Diferencial 2

            DataRow newRow3 = dtJournalEntry.NewRow();

            dtJournalEntry.Rows.Add(newRow3);

            var result3 = cn.FindPerdidaDiferenciaConversion();

            newRow2["ContraAct"] = txtProveedor.Text;

            newRow3["ShortName"] = result3.Item1;

            newRow3["Account"] = result3.Item1;

            newRow3["Debit"] = 0;

            newRow3["SYSDeb"] = ConvertDecimalTwoPlaces(newRow2["SYSCred"]);

            newRow3["FCDebit"] = 0;

            newRow3["LineMemo"] = txtComentario.Text;

            return dtJournalEntry;
        }

        private DataTable CreateJounalEntryBasicMainCurrency1(DataTable dtJournalEntry, Payment payment, DataRow row)
        {
            //Create Line Account

            DataRow newRow = dtJournalEntry.NewRow();

            decimal diferrenceAmount = 0;

            dtJournalEntry.Rows.Add(newRow);

            newRow["ShortName"] = AcctCode;

            newRow["Account"] = AcctCode;

            newRow["ContraAct"] = txtProveedor.Text;

            if (String.IsNullOrWhiteSpace(payment.TrsfrAcct) == false)
            {
                newRow["Credit"] = payment.TrsfrSum;
                newRow["SYSCred"] = payment.TrsfrSumSy;
                newRow["FCCredit"] = payment.TrsfrSumFC;

            }
            else if (String.IsNullOrWhiteSpace(payment.CashAcct) == false)
            {
                newRow["Credit"] = payment.CashSum;
                newRow["SYSCred"] = payment.CashSumSy;
                newRow["FCCredit"] = payment.CashSumFC;
            }

            newRow["LineMemo"] = txtComentario.Text;

            //Create BP

            DataRow newRow1 = dtJournalEntry.NewRow();

            dtJournalEntry.Rows.Add(newRow1);

            var result = cn.GetDocumentAccount(Convert.ToInt32(row["DocEntry"]), cn.GetTransType(row["TransType"].ToString()).ToString());

            newRow1["ShortName"] = txtProveedor.Text;

            newRow1["Account"] = result.Item1;

            newRow1["ContraAct"] = AcctCode;

            newRow1["Debit"] = ConvertDecimalTwoPlaces(row["PaidWithOutHoldingTax"]);

            newRow1["SYSDeb"] = ConvertDecimalTwoPlaces(row["PaidSysWithOutHoldingTax"]);

            newRow1["FCDebit"] = ConvertDecimalTwoPlaces(row["PaidFCWithOutHoldingTax"]);

            newRow1["LineMemo"] = txtComentario.Text;

            if ((ConvertDecimalTwoPlaces(row["PaidWithHoldingTax"]) == 0) && (ConvertDecimalTwoPlaces(row["PaidSysWithHoldingTax"]) == 0)) //la factura tiene pago asociado
            {
                var dtRetencionesTipoPago = GetRetencionesTipoPago(Convert.ToInt32(row["DocEntry"].ToString()), (cn.GetTransType(row["TransType"].ToString())).ToString()); //Buscar las retenciones de tipo pago en la factura

                if (dtRetencionesTipoPago.Item1.Rows.Count > 0 && dtRetencionesTipoPago.Item2 == null) //Cuenta la cantidad de retenciones de tipo pago asociadas a la factura
                {
                    foreach (DataRow rowRetenciones in dtRetencionesTipoPago.Item1.Rows) //Recorre coleccion
                    {
                        DataRow newRow3 = dtJournalEntry.NewRow();

                        dtJournalEntry.Rows.Add(newRow3);

                        newRow3["ShortName"] = rowRetenciones["Account"];

                        newRow3["Account"] = rowRetenciones["Account"];

                        newRow3["ContraAct"] = txtProveedor.Text;

                        newRow3["Credit"] = ConvertDecimalTwoPlaces(rowRetenciones["TaxbleAmnt"]);

                        newRow3["SYSCred"] = ConvertDecimalTwoPlaces(newRow3["Credit"]) / Rate;

                        diferrenceAmount = diferrenceAmount + ConvertDecimalTwoPlaces(newRow3["SYSCred"]);

                        newRow3["FCCredit"] = ConvertDecimalTwoPlaces(newRow1["FCDebit"]) - ConvertDecimalTwoPlaces(newRow["FCCredit"]);

                        newRow3["LineMemo"] = txtComentario.Text;
                    }
                }
                else
                {
                    diferrenceAmount = 0;
                }
            }
           

            //Create Line Diferencial

            DataRow newRow2 = dtJournalEntry.NewRow();

            dtJournalEntry.Rows.Add(newRow2);          

            var result2 = cn.FindPerdidaDiferenciaConversion();

            newRow2["Account"] = result2.Item1;

            newRow2["ShortName"] = result2.Item1;

            newRow2["ContraAct"] = txtProveedor.Text;

            newRow2["Debit"] = 0;

            newRow2["SYSDeb"] = (ConvertDecimalTwoPlaces(newRow["SYSCred"]) + diferrenceAmount) - (ConvertDecimalTwoPlaces(newRow1["SYSDeb"])); //

            newRow2["FCDebit"] = 0;

            newRow2["LineMemo"] = txtComentario.Text;

            return dtJournalEntry;
        }

        private DataTable CreateJounalEntryBasicFRCurrency1(DataTable dtJournalEntry, Payment payment, DataRow row)
        {
            //Create Line Account

            DataRow newRow = dtJournalEntry.NewRow();

            decimal diferrenceAmount = 0;

            dtJournalEntry.Rows.Add(newRow);

            newRow["ShortName"] = AcctCode;

            newRow["Account"] = AcctCode;

            newRow["ContraAct"] = txtProveedor.Text;

            if (String.IsNullOrWhiteSpace(payment.TrsfrAcct) == false)
            {
                newRow["Credit"] = payment.TrsfrSum;
                newRow["SYSCred"] = payment.TrsfrSumSy;
                newRow["FCCredit"] = payment.TrsfrSumFC;

            }
            else if (String.IsNullOrWhiteSpace(payment.CashAcct) == false)
            {
                newRow["Credit"] = payment.CashSum;
                newRow["SYSCred"] = payment.CashSumSy;
                newRow["FCCredit"] = payment.CashSumFC;
            }

            newRow["LineMemo"] = txtComentario.Text;

            //Create BP

            DataRow newRow1 = dtJournalEntry.NewRow();

            dtJournalEntry.Rows.Add(newRow1);

            var result = cn.GetDocumentAccount(Convert.ToInt32(row["DocEntry"]), cn.GetTransType(row["TransType"].ToString()).ToString());

            newRow1["ShortName"] = txtProveedor.Text;

            newRow1["Account"] = result.Item1;

            newRow1["ContraAct"] = AcctCode;

            newRow1["Debit"] = ConvertDecimalTwoPlaces(row["PaidWithOutHoldingTax"]);

            newRow1["SYSDeb"] = ConvertDecimalTwoPlaces(row["PaidSysWithOutHoldingTax"]);

            newRow1["FCDebit"] = ConvertDecimalTwoPlaces(row["PaidWithOutHoldingTax"]) / DocRatePayment;

            newRow1["LineMemo"] = txtComentario.Text;

            if ((ConvertDecimalTwoPlaces(row["PaidWithHoldingTax"]) == 0) && (ConvertDecimalTwoPlaces(row["PaidSysWithHoldingTax"]) == 0)) //la factura tiene pago asociado
            {
                var dtRetencionesTipoPago = GetRetencionesTipoPago(Convert.ToInt32(row["DocEntry"].ToString()), (cn.GetTransType(row["TransType"].ToString())).ToString()); //Buscar las retenciones de tipo pago en la factura

                if (dtRetencionesTipoPago.Item1.Rows.Count > 0 && dtRetencionesTipoPago.Item2 == null) //Cuenta la cantidad de retenciones de tipo pago asociadas a la factura
                {
                    foreach (DataRow rowRetenciones in dtRetencionesTipoPago.Item1.Rows) //Recorre coleccion
                    {
                        DataRow newRow3 = dtJournalEntry.NewRow();

                        dtJournalEntry.Rows.Add(newRow3);

                        newRow3["ShortName"] = rowRetenciones["Account"];

                        newRow3["Account"] = rowRetenciones["Account"];

                        newRow3["ContraAct"] = txtProveedor.Text;

                        newRow3["Credit"] = ConvertDecimalTwoPlaces(rowRetenciones["TaxbleAmnt"]);

                        newRow3["SYSCred"] = ConvertDecimalTwoPlaces(newRow3["Credit"]) / SysRatePayment;

                        diferrenceAmount = diferrenceAmount + ConvertDecimalTwoPlaces(newRow3["SYSCred"]);

                        newRow3["FCCredit"] = ConvertDecimalTwoPlaces(newRow3["Credit"]) / DocRatePayment;

                        newRow3["LineMemo"] = txtComentario.Text;
                    }
                }
                else
                {
                    diferrenceAmount = 0;
                }
            }
           
            //Create Line Diferencial

            DataRow newRow2 = dtJournalEntry.NewRow();

            dtJournalEntry.Rows.Add(newRow2);

            var result2 = cn.FindPerdidaDiferenciaConversion();

            newRow2["Account"] = result2.Item1;

            newRow2["ShortName"] = result2.Item1;

            newRow2["ContraAct"] = txtProveedor.Text;

            newRow2["Debit"] = 0;

            newRow2["SYSDeb"] = (ConvertDecimalTwoPlaces(newRow["SYSCred"]) + diferrenceAmount) - (ConvertDecimalTwoPlaces(newRow1["SYSDeb"])); //

            newRow2["FCDebit"] = 0;

            newRow2["LineMemo"] = txtComentario.Text;

            return dtJournalEntry;
        }

        private DataTable CreateJounalEntryBasicMainCurrency(DataTable dtJournalEntry, Payment payment, DataRow row)
        {
            //Create Line Account

            DataRow newRow = dtJournalEntry.NewRow();

            decimal diferrenceAmount = 0;

            dtJournalEntry.Rows.Add(newRow);

            newRow["ShortName"] = AcctCode;

            newRow["Account"] = AcctCode;

            newRow["ContraAct"] = txtProveedor.Text;

            if (String.IsNullOrWhiteSpace(payment.TrsfrAcct) == false)
            {
                newRow["Credit"] = payment.TrsfrSum;
                newRow["SYSCred"] = payment.TrsfrSumSy;
                newRow["FCCredit"] = payment.TrsfrSumFC;

            }
            else if (String.IsNullOrWhiteSpace(payment.CashAcct) == false)
            {
                newRow["Credit"] = payment.CashSum;
                newRow["SYSCred"] = payment.CashSumSy;
                newRow["FCCredit"] = payment.CashSumFC;
            }


            newRow["LineMemo"] = txtComentario.Text;

            //Create BP

            DataRow newRow1 = dtJournalEntry.NewRow();

            dtJournalEntry.Rows.Add(newRow1);

            var result = cn.GetDocumentAccount(Convert.ToInt32(row["DocEntry"]), cn.GetTransType(row["TransType"].ToString()).ToString());           
           
            newRow1["Account"] = result.Item1;

            newRow1["ContraAct"] = AcctCode;

            newRow1["ShortName"] = txtProveedor.Text;

            newRow1["Debit"] = ConvertDecimalTwoPlaces(row["PaidWithOutHoldingTax"]);

            newRow1["SYSDeb"] = ConvertDecimalTwoPlaces(row["PaidSysWithOutHoldingTax"]);

            newRow1["FCDebit"] = ConvertDecimalTwoPlaces(row["PaidFCWithOutHoldingTax"]);

            newRow1["LineMemo"] = txtComentario.Text;

            if ((ConvertDecimalTwoPlaces(row["PaidWithHoldingTax"]) == 0) && (ConvertDecimalTwoPlaces(row["PaidSysWithHoldingTax"]) == 0)) //la factura tiene pago asociado
            {
                var dtRetencionesTipoPago = GetRetencionesTipoPago(Convert.ToInt32(row["DocEntry"].ToString()), (cn.GetTransType(row["TransType"].ToString())).ToString()); //Buscar las retenciones de tipo pago en la factura

                if (dtRetencionesTipoPago.Item1.Rows.Count > 0 && dtRetencionesTipoPago.Item2 == null) //Cuenta la cantidad de retenciones de tipo pago asociadas a la factura
                {
                    foreach (DataRow rowRetenciones in dtRetencionesTipoPago.Item1.Rows) //Recorre coleccion
                    {
                        DataRow newRow3 = dtJournalEntry.NewRow();

                        dtJournalEntry.Rows.Add(newRow3);

                        newRow3["ShortName"] = rowRetenciones["Account"];

                        newRow3["Account"] = rowRetenciones["Account"];

                        newRow3["ContraAct"] = txtProveedor.Text;

                        newRow3["Credit"] = ConvertDecimalTwoPlaces(rowRetenciones["TaxbleAmnt"]);

                        newRow3["SYSCred"] = ConvertDecimalTwoPlaces(newRow3["Credit"]) / Rate;

                        diferrenceAmount = diferrenceAmount + ConvertDecimalTwoPlaces(newRow3["SYSCred"]);

                        newRow3["FCCredit"] = ConvertDecimalTwoPlaces(newRow1["FCDebit"]) - ConvertDecimalTwoPlaces(newRow["FCCredit"]);

                        newRow3["LineMemo"] = txtComentario.Text;
                    }
                }
                else
                {
                    diferrenceAmount = 0;
                }
            }
            

            //Create Line Diferencial

            DataRow newRow2 = dtJournalEntry.NewRow();

            dtJournalEntry.Rows.Add(newRow2);

            var result2 = cn.FindBeneficioDiferenciaConversion();

            newRow2["Account"] = result2.Item1;

            newRow2["ShortName"] = result2.Item1;

            newRow2["ContraAct"] = txtProveedor.Text;

            newRow2["Credit"] = 0;

            newRow2["SYSCred"] = ConvertDecimalTwoPlaces(newRow1["SYSDeb"]) - (ConvertDecimalTwoPlaces(newRow["SYSCred"]) + diferrenceAmount);

            newRow2["FCCredit"] = 0;

            newRow2["LineMemo"] = txtComentario.Text;

            

            return dtJournalEntry;
        }

        private DataTable CreateJounalEntryBasicFRCurrency(DataTable dtJournalEntry, Payment payment, DataRow row)
        {
            //Create Line Account

            DataRow newRow = dtJournalEntry.NewRow();

            decimal diferrenceAmount = 0;

            dtJournalEntry.Rows.Add(newRow);

            newRow["ShortName"] = AcctCode;

            newRow["Account"] = AcctCode;

            newRow["ContraAct"] = txtProveedor.Text;

            if (String.IsNullOrWhiteSpace(payment.TrsfrAcct) == false)
            {
                newRow["Credit"] = payment.TrsfrSum;
                newRow["SYSCred"] = payment.TrsfrSumSy;
                newRow["FCCredit"] = payment.TrsfrSumFC;

            }
            else if (String.IsNullOrWhiteSpace(payment.CashAcct) == false)
            {
                newRow["Credit"] = payment.CashSum;
                newRow["SYSCred"] = payment.CashSumSy;
                newRow["FCCredit"] = payment.CashSumFC;
            }


            newRow["LineMemo"] = txtComentario.Text;

            //Create BP

            DataRow newRow1 = dtJournalEntry.NewRow();

            dtJournalEntry.Rows.Add(newRow1);

            var result = cn.GetDocumentAccount(Convert.ToInt32(row["DocEntry"]), cn.GetTransType(row["TransType"].ToString()).ToString());

            newRow1["Account"] = result.Item1;

            newRow1["ContraAct"] = AcctCode;

            newRow1["ShortName"] = txtProveedor.Text;           

            newRow1["Debit"] = ConvertDecimalTwoPlaces(row["PaidWithOutHoldingTax"]);

            newRow1["SYSDeb"] = ConvertDecimalTwoPlaces(row["PaidSysWithOutHoldingTax"]);

            newRow1["FCDebit"] = ConvertDecimalTwoPlaces(row["PaidWithOutHoldingTax"]) / DocRatePayment;

            newRow1["LineMemo"] = txtComentario.Text;

            if ((ConvertDecimalTwoPlaces(row["PaidWithHoldingTax"]) == 0) && (ConvertDecimalTwoPlaces(row["PaidSysWithHoldingTax"]) == 0)) //la factura tiene pago asociado
            {
                var dtRetencionesTipoPago = GetRetencionesTipoPago(Convert.ToInt32(row["DocEntry"].ToString()), (cn.GetTransType(row["TransType"].ToString())).ToString()); //Buscar las retenciones de tipo pago en la factura

                if (dtRetencionesTipoPago.Item1.Rows.Count > 0 && dtRetencionesTipoPago.Item2 == null) //Cuenta la cantidad de retenciones de tipo pago asociadas a la factura
                {
                    foreach (DataRow rowRetenciones in dtRetencionesTipoPago.Item1.Rows) //Recorre coleccion
                    {
                        DataRow newRow3 = dtJournalEntry.NewRow();

                        dtJournalEntry.Rows.Add(newRow3);

                        newRow3["ShortName"] = rowRetenciones["Account"];

                        newRow3["Account"] = rowRetenciones["Account"];

                        newRow3["ContraAct"] = txtProveedor.Text;

                        newRow3["Credit"] = ConvertDecimalTwoPlaces(rowRetenciones["TaxbleAmnt"]);

                        newRow3["SYSCred"] = ConvertDecimalTwoPlaces(newRow3["Credit"]) / SysRatePayment;

                        diferrenceAmount = diferrenceAmount + ConvertDecimalTwoPlaces(newRow3["SYSCred"]);

                        newRow3["FCCredit"] = ConvertDecimalTwoPlaces(newRow3["Credit"]) / DocRatePayment;

                        newRow3["LineMemo"] = txtComentario.Text;
                    }
                }
                else
                {
                    diferrenceAmount = 0;
                }
            }
            
            //Create Line Diferencial

            DataRow newRow2 = dtJournalEntry.NewRow();

            dtJournalEntry.Rows.Add(newRow2);

            var result2 = cn.FindBeneficioDiferenciaConversion();

            newRow2["Account"] = result2.Item1;

            newRow2["ShortName"] = result2.Item1;

            newRow2["ContraAct"] = txtProveedor.Text;

            newRow2["Credit"] = 0;

            newRow2["SYSCred"] = ConvertDecimalTwoPlaces(newRow1["SYSDeb"]) - (ConvertDecimalTwoPlaces(newRow["SYSCred"]) + diferrenceAmount);

            newRow2["FCCredit"] = 0;

            newRow2["LineMemo"] = txtComentario.Text;



            return dtJournalEntry;
        }

        private DataTable CreateJounalEntryBasicFrgn(DataTable dtJournalEntry, Payment payment, DataRow row)
        {
            DataRow newRow = dtJournalEntry.NewRow();

            dtJournalEntry.Rows.Add(newRow);

            newRow["ShortName"] = AcctCode;

            newRow["Account"] = AcctCode;

            newRow["ContraAct"] = txtProveedor.Text;

            if (String.IsNullOrWhiteSpace(payment.TrsfrAcct) == false)
            {
                newRow["Credit"] = payment.TrsfrSum;
                newRow["SYSCred"] = payment.TrsfrSumSy;
                newRow["FCCredit"] = payment.TrsfrSumFC;

            }
            else if (String.IsNullOrWhiteSpace(payment.CashAcct) == false)
            {
                newRow["Credit"] = payment.CashSum;
                newRow["SYSCred"] = payment.CashSumSy;
                newRow["FCCredit"] = payment.CashSumFC;
            }

            newRow["LineMemo"] = txtComentario.Text;

            //Create BP

            DataRow newRow1 = dtJournalEntry.NewRow();

            dtJournalEntry.Rows.Add(newRow1);

            newRow1["ShortName"] = txtProveedor.Text;

            var result = cn.GetDocumentAccount(Convert.ToInt32(row["DocEntry"]), cn.GetTransType(row["TransType"].ToString()).ToString());

            newRow1["Account"] = result.Item1;

            newRow1["ContraAct"] = AcctCode;

            newRow1["Debit"] = ConvertDecimalTwoPlaces(row["PaidFCWithOutHoldingTax"]);

            newRow1["SYSDeb"] = ConvertDecimalTwoPlaces(row["PaidSysWithOutHoldingTax"]);

            newRow1["FCDebit"] = ConvertDecimalTwoPlaces(row["PaidWithOutHoldingTax"]);         

            newRow1["LineMemo"] = txtComentario.Text;

            //Verifica si la factura tiene algun pago realizado

            if ((ConvertDecimalTwoPlaces(row["PaidWithHoldingTax"]) == 0) && (ConvertDecimalTwoPlaces(row["PaidSysWithHoldingTax"]) == 0)) //la factura tiene pago asociado
            {
                var dtRetencionesTipoPago = GetRetencionesTipoPago(Convert.ToInt32(row["DocEntry"].ToString()), (cn.GetTransType(row["TransType"].ToString())).ToString()); //Buscar las retenciones de tipo pago en la factura

                if (dtRetencionesTipoPago.Item1.Rows.Count > 0 && dtRetencionesTipoPago.Item2 == null) //Cuenta la cantidad de retenciones de tipo pago asociadas a la factura
                {
                    foreach (DataRow rowRetenciones in dtRetencionesTipoPago.Item1.Rows) //Recorre coleccion
                    {
                        DataRow newRow2 = dtJournalEntry.NewRow();

                        dtJournalEntry.Rows.Add(newRow2);

                        newRow2["ShortName"] = rowRetenciones["Account"];

                        newRow2["Account"] = rowRetenciones["Account"];

                        newRow2["ContraAct"] = txtProveedor.Text;

                        newRow2["Credit"] = ConvertDecimalTwoPlaces(rowRetenciones["TaxbleAmnt"]);

                        newRow2["SYSCred"] = ConvertDecimalTwoPlaces(newRow2["Credit"]) / SysRatePayment;

                        newRow2["FCCredit"] = ConvertDecimalTwoPlaces(newRow2["Credit"]) / DocRatePayment;

                        newRow2["LineMemo"] = txtComentario.Text;
                    }
                }
                else
                {

                }
            }

            return dtJournalEntry;
        }

        private DataTable CreateJounalEntryBasicFrgnPayMain(DataTable dtJournalEntry, Payment payment, DataRow row)
        {
            DataRow newRow = dtJournalEntry.NewRow();

            dtJournalEntry.Rows.Add(newRow);

            newRow["ShortName"] = AcctCode;

            newRow["Account"] = AcctCode;

            newRow["ContraAct"] = txtProveedor.Text;

            if (String.IsNullOrWhiteSpace(payment.TrsfrAcct) == false)
            {
                newRow["Credit"] = payment.TrsfrSum;
                newRow["SYSCred"] = payment.TrsfrSumSy;
                newRow["FCCredit"] = payment.TrsfrSumFC;

            }
            else if (String.IsNullOrWhiteSpace(payment.CashAcct) == false)
            {
                newRow["Credit"] = payment.CashSum;
                newRow["SYSCred"] = payment.CashSumSy;
                newRow["FCCredit"] = payment.CashSumFC;
            }

            newRow["LineMemo"] = txtComentario.Text;

            //Create BP

            DataRow newRow1 = dtJournalEntry.NewRow();

            dtJournalEntry.Rows.Add(newRow1);

            newRow1["ShortName"] = txtProveedor.Text;

            var result = cn.GetDocumentAccount(Convert.ToInt32(row["DocEntry"]), cn.GetTransType(row["TransType"].ToString()).ToString());

            newRow1["Account"] = result.Item1;

            newRow1["ContraAct"] = AcctCode;

            newRow1["Debit"] = ConvertDecimalTwoPlaces(row["PaidFCWithOutHoldingTax"]);

            newRow1["SYSDeb"] = ConvertDecimalTwoPlaces(row["PaidSysWithOutHoldingTax"]);
           
            newRow1["FCDebit"] = 0;

            newRow1["LineMemo"] = txtComentario.Text;

            //Verifica si la factura tiene algun pago realizado

            if ((ConvertDecimalTwoPlaces(row["PaidWithHoldingTax"]) == 0) && (ConvertDecimalTwoPlaces(row["PaidSysWithHoldingTax"]) == 0)) //la factura tiene pago asociado
            {
                var dtRetencionesTipoPago = GetRetencionesTipoPago(Convert.ToInt32(row["DocEntry"].ToString()), (cn.GetTransType(row["TransType"].ToString())).ToString()); //Buscar las retenciones de tipo pago en la factura

                if (dtRetencionesTipoPago.Item1.Rows.Count > 0 && dtRetencionesTipoPago.Item2 == null) //Cuenta la cantidad de retenciones de tipo pago asociadas a la factura
                {
                    foreach (DataRow rowRetenciones in dtRetencionesTipoPago.Item1.Rows) //Recorre coleccion
                    {
                        DataRow newRow2 = dtJournalEntry.NewRow();

                        dtJournalEntry.Rows.Add(newRow2);

                        newRow2["ShortName"] = rowRetenciones["Account"];

                        newRow2["Account"] = rowRetenciones["Account"];

                        newRow2["ContraAct"] = txtProveedor.Text;

                        newRow2["Credit"] = ConvertDecimalTwoPlaces(rowRetenciones["TaxbleAmnt"]);

                        newRow2["SYSCred"] = ConvertDecimalTwoPlaces(newRow2["Credit"]) / SysRatePayment;

                        newRow2["FCCredit"] = 0;

                        newRow2["LineMemo"] = txtComentario.Text;
                    }
                }
                else
                {

                }
            }

            return dtJournalEntry;
        }

        private DataTable CreateJounalEntryBasic(DataTable dtJournalEntry, Payment payment, DataRow row)
        {
            decimal diferrenceAmount = 0;

            DataRow newRow = dtJournalEntry.NewRow();

            dtJournalEntry.Rows.Add(newRow);

            newRow["ShortName"] = AcctCode;

            newRow["Account"] = AcctCode;

            newRow["ContraAct"] = txtProveedor.Text;

            if (String.IsNullOrWhiteSpace(payment.TrsfrAcct) == false)
            {
                newRow["Credit"] = payment.TrsfrSum;
                newRow["SYSCred"] = payment.TrsfrSumSy;
                newRow["FCCredit"] = payment.TrsfrSumFC;

            }
            else if (String.IsNullOrWhiteSpace(payment.CashAcct) == false)
            {
                newRow["Credit"] = payment.CashSum;
                newRow["SYSCred"] = payment.CashSumSy;
                newRow["FCCredit"] = payment.CashSumFC;
            }

            newRow["LineMemo"] = txtComentario.Text;

            //Create BP

            DataRow newRow1 = dtJournalEntry.NewRow();

            dtJournalEntry.Rows.Add(newRow1);

            newRow1["ShortName"] = txtProveedor.Text;

            var result = cn.GetDocumentAccount(Convert.ToInt32(row["DocEntry"]), cn.GetTransType(row["TransType"].ToString()).ToString());

            newRow1["Account"] = result.Item1;

            newRow1["ContraAct"] = AcctCode;         

            newRow1["Debit"] = ConvertDecimalTwoPlaces(row["PaidWithOutHoldingTax"]);

            newRow1["SYSDeb"] = ConvertDecimalTwoPlaces(row["PaidSysWithOutHoldingTax"]);

            newRow1["FCDebit"] = ConvertDecimalTwoPlaces(row["PaidFCWithOutHoldingTax"]);

            newRow1["LineMemo"] = txtComentario.Text;

            //Verifica si la factura tiene algun pago realizado

            if ((ConvertDecimalTwoPlaces(row["PaidWithHoldingTax"])==0) && (ConvertDecimalTwoPlaces(row["PaidSysWithHoldingTax"])==0)) //la factura tiene pago asociado
            {
                var dtRetencionesTipoPago = GetRetencionesTipoPago(Convert.ToInt32(row["DocEntry"].ToString()), (cn.GetTransType(row["TransType"].ToString())).ToString()); //Buscar las retenciones de tipo pago en la factura

                if (dtRetencionesTipoPago.Item1.Rows.Count > 0 && dtRetencionesTipoPago.Item2 == null) //Cuenta la cantidad de retenciones de tipo pago asociadas a la factura
                {
                    foreach (DataRow rowRetenciones in dtRetencionesTipoPago.Item1.Rows) //Recorre coleccion
                    {
                        DataRow newRow2 = dtJournalEntry.NewRow();

                        dtJournalEntry.Rows.Add(newRow2);

                        newRow2["ShortName"] = rowRetenciones["Account"];

                        newRow2["Account"] = rowRetenciones["Account"];

                        newRow2["ContraAct"] = txtProveedor.Text;

                        newRow2["Credit"] = ConvertDecimalTwoPlaces(rowRetenciones["TaxbleAmnt"]);

                        newRow2["SYSCred"] = ConvertDecimalTwoPlaces(newRow2["Credit"]) / SysRatePayment;

                        diferrenceAmount = diferrenceAmount + ConvertDecimalTwoPlaces(newRow2["SYSCred"]);

                        newRow2["FCCredit"] = ConvertDecimalTwoPlaces(newRow1["FCDebit"]) - ConvertDecimalTwoPlaces(newRow["FCCredit"]);

                        newRow2["LineMemo"] = txtComentario.Text;
                    }
                }
                else
                {
                    diferrenceAmount = 0;
                }
            }
            

            return dtJournalEntry;
        }

        private DataTable CreateJounalEntryBasicPayFRCurrency(DataTable dtJournalEntry, Payment payment, DataRow row)
        {
            DataRow newRow = dtJournalEntry.NewRow();

            dtJournalEntry.Rows.Add(newRow);

            newRow["ShortName"] = AcctCode;

            newRow["Account"] = AcctCode;

            newRow["ContraAct"] = txtProveedor.Text;

            if (String.IsNullOrWhiteSpace(payment.TrsfrAcct) == false)
            {
                newRow["Credit"] = payment.TrsfrSum;
                newRow["SYSCred"] = payment.TrsfrSumSy;
                newRow["FCCredit"] = payment.TrsfrSumFC;

            }
            else if (String.IsNullOrWhiteSpace(payment.CashAcct) == false)
            {
                newRow["Credit"] = payment.CashSum;
                newRow["SYSCred"] = payment.CashSumSy;
                newRow["FCCredit"] = payment.CashSumFC;
            }

            newRow["LineMemo"] = txtComentario.Text;

            //Create BP

            DataRow newRow1 = dtJournalEntry.NewRow();

            dtJournalEntry.Rows.Add(newRow1);

            newRow1["ShortName"] = txtProveedor.Text;

            var result = cn.GetDocumentAccount(Convert.ToInt32(row["DocEntry"]), cn.GetTransType(row["TransType"].ToString()).ToString());

            newRow1["Account"] = result.Item1;

            newRow1["ContraAct"] = AcctCode;

            newRow1["Debit"] = ConvertDecimalTwoPlaces(row["PaidWithOutHoldingTax"]);

            newRow1["SYSDeb"] = ConvertDecimalTwoPlaces(row["PaidSysWithOutHoldingTax"]);

            newRow1["FCDebit"] = ConvertDecimalTwoPlaces(row["PaidWithOutHoldingTax"]) / DocRatePayment;

            newRow1["LineMemo"] = txtComentario.Text;

            //Verifica si la factura tiene algun pago realizado

            if ((ConvertDecimalTwoPlaces(row["PaidWithHoldingTax"]) == 0) && (ConvertDecimalTwoPlaces(row["PaidSysWithHoldingTax"]) == 0)) //la factura tiene pago asociado
            {
                var dtRetencionesTipoPago = GetRetencionesTipoPago(Convert.ToInt32(row["DocEntry"].ToString()), (cn.GetTransType(row["TransType"].ToString())).ToString()); //Buscar las retenciones de tipo pago en la factura

                if (dtRetencionesTipoPago.Item1.Rows.Count > 0 && dtRetencionesTipoPago.Item2 == null) //Cuenta la cantidad de retenciones de tipo pago asociadas a la factura
                {
                    foreach (DataRow rowRetenciones in dtRetencionesTipoPago.Item1.Rows) //Recorre coleccion
                    {
                        DataRow newRow2 = dtJournalEntry.NewRow();

                        dtJournalEntry.Rows.Add(newRow2);

                        newRow2["ShortName"] = rowRetenciones["Account"];

                        newRow2["Account"] = rowRetenciones["Account"];

                        newRow2["ContraAct"] = txtProveedor.Text;

                        newRow2["Credit"] = ConvertDecimalTwoPlaces(rowRetenciones["TaxbleAmnt"]);

                        newRow2["SYSCred"] = ConvertDecimalTwoPlaces(newRow2["Credit"]) / SysRatePayment;

                        newRow2["FCCredit"] = ConvertDecimalTwoPlaces(newRow2["Credit"]) / DocRatePayment;

                        newRow2["LineMemo"] = txtComentario.Text;
                    }
                }
                else
                {

                }
            }
           

            return dtJournalEntry;
        }

        private Tuple<DataTable,string> GetRetencionesTipoPago(int DocEntry, string TransType)
        {
            DataTable newDt = new DataTable();

            string error = null;

            try
            {

                newDt = dtWithHoldingTax.AsEnumerable().Where(r => ((int)r["AbsEntry"]).Equals(DocEntry) && ((string)r["ObjType"]).Equals(TransType)).CopyToDataTable();
                               
                return Tuple.Create(newDt,error);

            }
            catch (Exception e)
            {

                return Tuple.Create(newDt, e.Message);

            }
        }

        private List<PaymentDetails> GetPurchase(DataTable listPurchase, Payment payment)
        {
            List<PaymentDetails> listPaymentDetails = new List<PaymentDetails>();

            int i = 0;

            foreach (DataRow Purchase in listPurchase.Rows)
            {
                PaymentDetails paymentDetails = new PaymentDetails();

                paymentDetails.DocNum = DocEntryPayment;
                paymentDetails.InvoiceId = i;
                paymentDetails.DocEntry = Convert.ToInt32(Purchase["DocEntry"]);
                paymentDetails.SumApplied =ConvertDecimalTwoPlaces(Purchase["Paid"]);
                paymentDetails.AppliedFC = ConvertDecimalTwoPlaces(Purchase["PaidFC"]);
                paymentDetails.AppliedSys = ConvertDecimalTwoPlaces(Purchase["PaidSys"]);
                paymentDetails.VatApplied = ConvertDecimalTwoPlaces(Purchase["VatSum"]);
                paymentDetails.VatAppldFC = ConvertDecimalTwoPlaces(Purchase["VatSumFC"]);
                paymentDetails.VatAppldSy = ConvertDecimalTwoPlaces(Purchase["VatSumSy"]);
                paymentDetails.WtInvCatS = ConvertDecimalTwoPlaces(Purchase["WTSum"]);
                paymentDetails.WtInvCatSF = ConvertDecimalTwoPlaces(Purchase["WTSumFC"]);
                paymentDetails.WtInvCatSS = ConvertDecimalTwoPlaces(Purchase["WTSumSC"]);
                paymentDetails.DocTransId = ConvertDecimalTwoPlaces(Purchase["TransId"]);              
                paymentDetails.DocSubType = Purchase["DocSubType"].ToString();
                paymentDetails.U_IDA_CompIVA = Purchase["U_IDA_CompIVA"].ToString();

                if (String.IsNullOrWhiteSpace(Purchase["U_IDA_FechaComp"].ToString()) == false)
                {
                    paymentDetails.U_IDA_FechaComp = Convert.ToDateTime(Purchase["U_IDA_FechaComp"].ToString());
                }
                else
                {
                    paymentDetails.U_IDA_FechaComp = null;
                }

                if (String.IsNullOrWhiteSpace(Purchase["U_IDA_DateCompISLR"].ToString()) == false)
                {
                    paymentDetails.U_IDA_DateCompISLR = Convert.ToDateTime(Purchase["U_IDA_DateCompISLR"].ToString());
                }
                else
                {
                    paymentDetails.U_IDA_DateCompISLR = null;
                }


                paymentDetails.U_IDA_MontoCompIVA =ConvertDecimalTwoPlaces(Purchase["U_IDA_MontoCompIVA"].ToString());
                paymentDetails.U_IDA_CompISLR = Purchase["U_IDA_CompISLR"].ToString();
             
                paymentDetails.Line_ID = Convert.ToInt32(Purchase["Line_ID"]);

                if (Purchase["DocCur"].ToString() == Properties.Settings.Default.MainCurrency)
                {
                    paymentDetails.DocRate = 0;
                }
                else
                {
                    paymentDetails.DocRate = payment.DocRate;
                }

                paymentDetails.InvType = cn.GetTransType(Purchase["TransType"].ToString()).ToString();
                paymentDetails.ObjType = payment.ObjType;

                listPaymentDetails.Add(paymentDetails);

                i++;
               
            }

            return listPaymentDetails;
        }

        private void RecorreListaPayment(List<Payment> newListPurchase)
        {
            if (newListPurchase.Count == 1)
            {
                ReestablecerFondo();

                GetPayment(newListPurchase);

                var result = cn.FindPaymentMadeLines(DocEntryPayment);

                if (result.Item3 == null)
                {
                    var result3 = cn.FindPurchaseInvoiceSupplierSpecific(result.Item1, result.Item2);

                    if (result3.Item2 == null)
                    {
                        ListaPaymentLines = result3.Item1;

                        GetPaymentLinesReadOnly(ListaPaymentLines);

                        SetStateDatatableDisabled();

                        Sw = false;

                        btnCrear.Content = "OK";
                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result3.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                    }
                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                }

            }
            else if (newListPurchase.Count == 0)
            {

                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se encontro ningun resultado con los parametros establecidos", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");

                ReestablecerFondo();

                LimpiarCampos();

                btnCrear.Content = "OK";
            }

            else if (newListPurchase.Count > 1)
            {
                ListaPagos ventanaListBox = new ListaPagos(newListPurchase);

                ventanaListBox.ShowDialog();

                if (ventanaListBox.Visibility == Visibility.Hidden)
                {
                    if (ventanaListBox.GetListPayment().Count == 0)
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No selecciono ningun elemento", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");

                        ReestablecerFondo();

                        LimpiarCampos();

                        btnCrear.Content = "OK";

                    }
                    else
                    {
                        ReestablecerFondo();

                        GetPayment(ventanaListBox.GetListPayment());

                        var result = cn.FindPaymentMadeLines(DocEntryPayment);

                        if (result.Item3 == null)
                        {
                            var result3 = cn.FindPurchaseInvoiceSupplierSpecific(result.Item1, result.Item2);

                            if (result3.Item2 == null)
                            {
                                ListaPaymentLines = result3.Item1;

                                GetPaymentLinesReadOnly(ListaPaymentLines);

                                Sw = false;

                                btnCrear.Content = "OK";
                            }
                            else
                            {
                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result3.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                            }
                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                        }

                    }

                    btnCrear.Content = "OK";
                }


            }

           
        }

        private void GetPaymentLinesReadOnly(DataTable listaPaymentLines)
        {
            dt = AddCurrencyCodeReadOnly(listaPaymentLines);

            dgPagoEfectuado.ItemsSource = dt.DefaultView;

            dgPagoEfectuado.CanUserAddRows = false;

            dgPagoEfectuado.CanUserDeleteRows = false;

            dgPagoEfectuado.CanUserSortColumns = false;

            dgPagoEfectuado.IsReadOnly = true;
            
        }

        private void txtComentario_KeyDown(object sender, KeyEventArgs e)
        {
            if(btnCrear.Content.ToString()=="OK")
            btnCrear.Content = "Actualizar";
        }

        private void dpDocumento_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (btnCrear.Content.ToString() == "OK")
                btnCrear.Content = "Actualizar";
        }

        private void RegistroDiario_Click(object sender, RoutedEventArgs e)
        {
            var result = CreateJournalEntryPreliminar();

            if (result.Item1 == true)
            {
                GetPresentationPreliminarJournalEntry(result.Item2, result.Item3);
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error el la visualizacion de la presentacion preliminar: ", Brushes.Red, Brushes.White, "003-interface-2.png");
            }
        }

        private Tuple<bool, List<AsientoCabecera>, DataTable> CreateJournalEntryPreliminar()
        {
            var dataSetPreliminar = cn.CreateDataSetPreliminarPaymentMade();

            List<Payment> listPayment = new List<Payment>();

            Payment payment = new Payment();

            Payment oldPayment = new Payment();

            List<PaymentDetails> listPaymentDetails = new List<PaymentDetails>();

            List<AsientoCabecera> listaJournalEntry = new List<AsientoCabecera>();

            AsientoCabecera journalEntry = new AsientoCabecera();

            List<AsientoDetalle> listaJournalEntryLines = new List<AsientoDetalle>();

            bool sw = false;

            if (dataSetPreliminar == null)
            {
                if (cbxPagoCuenta.IsChecked == true)
                {
                    oldPayment = GetMediosPago(EstableceLogin.GetMediosPago().GetListMediosPago(), payment); //Recuperar lista

                    var listPaymentResult = CreateListPayment(oldPayment);

                    var result5 = cn.InsertPaymentMadePreliminar(listPaymentResult.Item1);

                    if (result5.Item1 == 1)
                    {
                        //Create Journal Entry
                        listaJournalEntry = CreateJournalEntry(listPaymentResult.Item1);

                        var result3 = cj.InsertJournalEntryPreliminar(listaJournalEntry);

                        if (result3.Item1 == 1)
                        {
                            var listJournalEntryLines = CreateListJournalEntryLinesNoDocPay(listaJournalEntry, listPaymentResult.Item2);

                            listaJournalEntryLines = listJournalEntryLines.Item1;

                            var result7 = cj.InsertJournalEntryLinesPreliminar(listJournalEntryLines.Item1);

                            if (listJournalEntryLines.Item2 == result7.Item1)
                            {
                                sw = true;

                            }
                            else
                            {
                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: " + result7.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                                sw = false;
                            }

                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: " + result3.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                            sw = false;
                        }

                    }
                    else
                    {
                        sw = false;
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error en la creacion de la factura: " + result5.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                    }
                }
                else
                {
                    oldPayment = GetMediosPago(EstableceLogin.GetMediosPago().GetListMediosPago(), payment); //Recuperar lista

                    var listPaymentResult = CreateListPaymentCompletePreliminar(oldPayment);

                    var result5 = cn.InsertPaymentMadePreliminar(listPaymentResult.Item1);

                    //if (result5.Item1 == 1)
                    //{
                    //    var result13 = cn.FindPurchaseInvoiceSpecificPreliminar(DocNum);

                    //    if (result13.Item2 == null)
                    //    {
                    //        var resultDocEntry = cn.FindDocEntryPreliminar(DocNumPayment);

                    //        DocEntryPayment = resultDocEntry.Item1;

                    //        var listPaymentLines = CreateListPaymentLines(listPaymentResult.Item2, result13.Item1);

                    //        var result6 = cn.InsertPaymentMadeLinesPreliminar(listPaymentLines.Item1);

                    //        if (result6.Item1 == 1)
                    //        {
                    //            //Create Journal Entry
                    //            listaJournalEntry = CreateJournalEntry(listPaymentResult.Item1);

                    //            var result2 = cj.InsertJournalEntryPreliminar(listaJournalEntry);

                    //            if (result2.Item1 == 1)
                    //            {
                    //                var listJournalEntryLines = CreateListJournalEntryLines(listaJournalEntry, listPaymentResult.Item2);

                    //                listaJournalEntryLines = listJournalEntryLines.Item1;

                    //                var result3 = cj.InsertJournalEntryLinesPreliminar(listJournalEntryLines.Item1);

                    //                if (listJournalEntryLines.Item2 == result3.Item1)
                    //                {
                    //                    sw = true;

                    //                }
                    //                else
                    //                {
                    //                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: " + result3.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                    //                    sw = false;
                    //                }

                    //            }
                    //            else
                    //            {
                    //                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: " + result2.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                    //                sw = false;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error en la creacion de la factura: " + result6.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");

                    //            sw = false;
                    //        }

                    //    }
                    //    else
                    //    {
                    //        sw = false;
                    //        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error en la creacion de la factura: " + result13.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                    //    }


                    //}
                    //else
                    //{
                    //    sw = false;
                    //    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error en la creacion de la factura: " + result5.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                    //}



                }
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + dataSetPreliminar, Brushes.Red, Brushes.White, "003-interface-2.png");
                sw = false;
            }

            var dataSetPreliminarClear = cn.ResetDataSetPreliminar();

            DataTable dtJournalEntryLines = ToDataTable(listaJournalEntryLines);

            dtJournalEntryLines = cn.ChangeTypeColumn(dtJournalEntryLines);

            dtJournalEntryLines = ChangeNameColumnDatatable(dtJournalEntryLines);

            return Tuple.Create(sw, listaJournalEntry, dtJournalEntryLines);
        }

        private Tuple<List<Payment>, Payment> CreateListPaymentCompletePreliminar(Payment oldPayment)
        {
            List<Payment> listPayment = new List<Payment>();

            Payment payment = new Payment();

            DocNumPayment = Convert.ToInt32(txtNro.Text);
            payment.DocNum = Convert.ToInt32(txtNro.Text);
            payment.DocType = 'S';
            payment.Canceled = 'N';
            payment.Comments = txtComentario.Text;
            payment.DocDate = dpContabilizacion.SelectedDate;
            payment.TaxDate = dpDocumento.SelectedDate;
            payment.DocDueDate = dpVencimiento.SelectedDate;
            payment.CardCode = txtProveedor.Text;
            payment.CardName = txtNombre.Text;
            payment.Address = txtPagarA.Text;
            payment.UserSign = Properties.Settings.Default.Usuario;
            payment.UpdateDate = fechaActual.GetFechaActual();
            var result10 = cn.GetPeriodCode(payment.DocDate);
            payment.FinncPriod = result10.Item1;
            payment.ObjType = Convert.ToString(cn.GetTransType("PP"));
            payment.CashAcct = oldPayment.CashAcct;
            payment.TrsfrAcct = oldPayment.TrsfrAcct;
            payment.TrsfrDate = oldPayment.TrsfrDate;
            payment.TrsfrRef = oldPayment.TrsfrRef;
            payment.CashSum = oldPayment.CashSum;
            payment.CashSumFC = oldPayment.CashSumFC;
            payment.CashSumSy = oldPayment.CashSumSy;
            payment.TrsfrSum = oldPayment.TrsfrSum;
            payment.TrsfrSumFC = oldPayment.TrsfrSumFC;
            payment.TrsfrSumSy = oldPayment.TrsfrSumSy;
            payment.DocTotal = oldPayment.DocTotal;
            payment.DocTotalFC = oldPayment.DocTotalFC;
            payment.DocTotalSy = oldPayment.DocTotalSy;
            payment.CheckAcct = "";
            payment.CheckSum = 0;
            payment.CheckSumFC = 0;
            payment.CheckSumSy = 0;
            payment.PayNoDoc = 'N';
            payment.NoDocSum = 0;
            payment.NoDocSumFC = 0;
            payment.NoDocSumSy = 0;
            payment.CounterRef = txtReferencia.Text;
            var result4 = cn.SelectTransId();
            payment.TransId = result4.Item1;
            payment.JrnlMemo = txtComentario.Text;
            var result12 = cn.GetDocumentAccountPreliminar(DocNum,true);
            payment.BpAct = result12.Item1;

            payment.SysRate = Rate;

            payment.DocCurr = EstableceLogin.GetMediosPago().GetCurrency();

            DocCur = EstableceLogin.GetMediosPago().GetCurrency();

            if (payment.DocCurr == Properties.Settings.Default.MainCurrency)
            {
                payment.DocRate = 0;

                RatePayment = payment.SysRate;
            }
            else
            {

                FindRateFC(DocCur);

                payment.DocRate = RateFC;

                RatePayment = payment.DocRate;
            }

            listPayment.Add(payment);

            return Tuple.Create(listPayment, payment);
        }
    }

    public class PagoEfectuadoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string input = value as string;
            switch (input)
            {
                case "Y":
                    return Brushes.Black;
                case "N":
                    return Brushes.DarkBlue;

                default:
                    return Brushes.Black;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
