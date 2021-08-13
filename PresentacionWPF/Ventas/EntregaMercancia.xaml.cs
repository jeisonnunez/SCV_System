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
using Negocio;
using Entidades;
using System.Windows.Media.Animation;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows.Interactivity;
using GalaSoft.MvvmLight.Command;
using System.Globalization;
using Negocio.Controlador_Inventario;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para EntregaMercancia.xaml
    /// </summary>
    public partial class EntregaMercancia : Document
    {
        ControladorEntregaMercancia cn = new ControladorEntregaMercancia();

        ControladorSocioNegocio cs = new ControladorSocioNegocio();

        ControladorArticulos ca = new ControladorArticulos();

        ControladorRetencionImpuesto cr = new ControladorRetencionImpuesto();

        ControladorAsiento cj = new ControladorAsiento();

        ControladorDefinicionUnidadesMedida cu = new ControladorDefinicionUnidadesMedida();

        DataTable dtArticulo = new DataTable() { TableName = "dtArticulo" };

        DataTable dt = new DataTable();

        DataGrid dg = new DataGrid();     

        DataTable dtJournalEntry = new DataTable();

        DataTable dtNewJournalEntry = new DataTable();

        private bool sw;

        private List<DocumentoCabecera> listaEntregaMercancia = new List<DocumentoCabecera>();

        private DataTable listaEntregaMercanciaLines = new DataTable();

        private DataTable listaRetencionImpuesto = new DataTable();

        private int docNum;

        private int docEntry;

        private decimal rate;

        private decimal rateFC;

        private string selectedDate;

        private string supplier;

        private string total;

        private string totaliva;

        private string currency;

        private string vatGroup;

        private string str;

        private decimal DocTotal;

        private decimal DocTotalSy;

        private decimal DocTotalFC;

        private string ctlAccount;

        private int transId;

        private string licTradNum;

        private List<ArticuloDetalle> listArticuloDetalleOld = new List<ArticuloDetalle>();

        private List<ArticuloDetalle> listArticuloDetalleNew = new List<ArticuloDetalle>();

        private List<int> listTransSeq = new List<int>();

        public ObservableCollection<string> TipoTransaccion { get; set; }

        public ObservableCollection<string> WtLiable { get; set; }

        public ObservableCollection<string> ClaseFactura { get; set; }
        public decimal Rate { get => rate; set => rate = value; }
        public decimal RateFC { get => rateFC; set => rateFC = value; }
        public string SelectedDate { get => selectedDate; set => selectedDate = value; }
        public int DocNum { get => docNum; set => docNum = value; }
        public List<DocumentoCabecera> ListaEntregaMercancia { get => listaEntregaMercancia; set => listaEntregaMercancia = value; }
        public DataTable ListaEntregaMercanciaLines { get => listaEntregaMercanciaLines; set => listaEntregaMercanciaLines = value; }
        public string Supplier { get => supplier; set => supplier = value; }
        public int DocEntry { get => docEntry; set => docEntry = value; }
        public string Total { get => total; set => total = value; }
        public string Totaliva { get => totaliva; set => totaliva = value; }
        public string Currency { get => currency; set => currency = value; }
        public string VatGroup { get => vatGroup; set => vatGroup = value; }
        public decimal DocTotal1 { get => DocTotal; set => DocTotal = value; }
        public decimal DocTotalSy1 { get => DocTotalSy; set => DocTotalSy = value; }
        public decimal DocTotalFC1 { get => DocTotalFC; set => DocTotalFC = value; }
        public DataTable ListaRetencionImpuesto { get => listaRetencionImpuesto; set => listaRetencionImpuesto = value; }
        public bool Sw { get => sw; set => sw = value; }
        public bool SwReadOnly { get; private set; }
        public List<ArticuloDetalle> ListArticuloDetalleOld { get => listArticuloDetalleOld; set => listArticuloDetalleOld = value; }
        public int DocNumDeleted { get; private set; }
        public char DocType { get; private set; }      
        public int DocEntryDeleted { get; private set; }
        public string CtlAccount { get => ctlAccount; set => ctlAccount = value; }
        public int TransId { get => transId; set => transId = value; }
        public string LicTradNum { get => licTradNum; set => licTradNum = value; }
        public List<ArticuloDetalle> ListArticuloDetalleNew { get => listArticuloDetalleNew; set => listArticuloDetalleNew = value; }
        public List<int> ListTransSeq { get => listTransSeq; set => listTransSeq = value; }

        public EntregaMercancia()
        {
            InitializeComponent();
        }

        private void LoadCurrency()
        {
            var currencys = cn.GetCurrency();

            Properties.Settings.Default.MainCurrency = currencys.Item1;

            Properties.Settings.Default.SysCurrency = currencys.Item2;

            Properties.Settings.Default.Save();
        }

       

        public class ListClaseFactura : List<string>
        {
            public ListClaseFactura()
            {
              
                this.Add("Articulo");

            }
        }


        private void comboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (btnCrear.Content.ToString() != "Buscar")
                App.comboBox_LostFocus(sender, e);
        }

        private void comboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (btnCrear.Content.ToString() != "Buscar")
                App.comboBox_GotFocus(sender, e);
        }

        private void textBox_LostFocus1(object sender, RoutedEventArgs e)
        {
            if (btnCrear.Content.ToString() == "Actualizar" || btnCrear.Content.ToString() == "Crear")
            {

                App.textBox_LostFocus(sender, e);
            }

        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (btnCrear.Content.ToString() != "Buscar")
                App.textBox_LostFocus(sender, e);
        }

        private void textBox_GotFocus1(object sender, RoutedEventArgs e)
        {
            if (btnCrear.Content.ToString() == "Actualizar" || btnCrear.Content.ToString() == "Crear")
            {

                App.textBox_GotFocus(sender, e);
            }
        }
        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (btnCrear.Content.ToString() != "Buscar")
                App.textBox_GotFocus(sender, e);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {            
            App.Window_Closing(sender, e);
        }

        private void btnWindow_Close(object sender, RoutedEventArgs e)
        {           
            this.Hide();
        }

        private void imgCrear_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            InicializacionBasic();
        }

        public void LoadedWindow()
        {
            InicializacionBasic();
        }


        public void InicializacionBasic()
        {
            btnCrear.Content = "Crear";

            LimpiarCampos();

            ReestablecerFondo();

            EnabledDatepicker();

            InicialiacionBasica();
            
            Sw = true;

            SwReadOnly = false;

            dg.CanUserAddRows = true;

            dg.CanUserDeleteRows = true;

            dg.CanUserSortColumns = true;

            cbMoneda.ItemsSource = cn.CreateCurrencyTable("", "");

            cbClase.SelectedValue = "Articulo";

            dt.Rows.Clear();
           
            dg.ItemsSource = dt.DefaultView;


        }

        private void EnabledDatepicker()
        {
            dpContabilizacion.IsEnabled = true;
        }

        private void imgFin_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (btnCrear.Content.ToString() == "OK" || btnCrear.Content.ToString() == "Crear" || btnCrear.Content.ToString() == "Buscar")
            {
                ReestablecerFondo();

                ListaEntregaMercancia.Clear();

                ListaEntregaMercanciaLines.Clear();

                var result= cn.FindLastEntregaMercancia();

                if (result.Item2 == null)
                {
                    SwReadOnly = true;

                    ListaEntregaMercancia = result.Item1;

                    GetEntregaMercancia(ListaEntregaMercancia);

                    var result1= cn.FindEntregaMercanciaLines(DocEntry);

                    if (result1.Item2 == null)
                    {
                        ListaEntregaMercanciaLines = result1.Item1;

                        GetEntregaMercanciaLines(ListaEntregaMercanciaLines);

                        btnCrear.Content = "OK";

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

        private void imgRight_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (btnCrear.Content.ToString() == "OK" || btnCrear.Content.ToString() == "Crear" || btnCrear.Content.ToString() == "Buscar")
            {
                ReestablecerFondo();

                ListaEntregaMercancia.Clear();

                ListaEntregaMercanciaLines.Clear();

                var result = cn.FindNextEntregaMercancia(txtNro.Text);

                if (result.Item2 == null)
                {
                    SwReadOnly = true;

                    ListaEntregaMercancia = result.Item1;

                    GetEntregaMercancia(ListaEntregaMercancia);

                    var result1 = cn.FindEntregaMercanciaLines(DocEntry);

                    if (result1.Item2 == null)
                    {
                        ListaEntregaMercanciaLines = result1.Item1;

                        GetEntregaMercanciaLines(ListaEntregaMercanciaLines);

                        btnCrear.Content = "OK";

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

        
        private DataTable AddCurrencyCodeRetenciones(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {

                    if (column.ToString() == "TaxbleAmnt" && Convert.ToDecimal(row["TaxbleAmnt"]) != 0)
                    {
                        row["TaxbleAmnt"] = Properties.Settings.Default.MainCurrency + " " + String.Format(row["TaxbleAmnt"].ToString());

                    }

                    else if (column.ToString() == "TaxbleAmntSC" && Convert.ToDecimal(row["TaxbleAmntSC"]) != 0)
                    {
                        row["TaxbleAmntSC"] = Properties.Settings.Default.SysCurrency + " " + String.Format(row["TaxbleAmntSC"].ToString());
                    }

                    else if (column.ToString() == "TaxbleAmntFC" && Convert.ToDecimal(row["TaxbleAmntFC"]) != 0)
                    {
                        row["TaxbleAmntFC"] = Currency + " " + String.Format(row["TaxbleAmntFC"].ToString());
                    }

                    else if (column.ToString() == "WTAmnt" && Convert.ToDecimal(row["WTAmnt"]) != 0)
                    {
                        row["WTAmnt"] = Properties.Settings.Default.MainCurrency + " " + String.Format(row["WTAmnt"].ToString());
                    }

                    else if (column.ToString() == "WTAmntSC" && Convert.ToDecimal(row["WTAmntSC"]) != 0)
                    {
                        row["WTAmntSC"] = Properties.Settings.Default.SysCurrency + " " + String.Format(row["WTAmntSC"].ToString());
                    }

                    else if (column.ToString() == "WTAmntFC" && Convert.ToDecimal(row["WTAmntFC"]) != 0)
                    {
                        row["WTAmntFC"] = Currency + " " + String.Format(row["WTAmntFC"].ToString());
                    }

                    else if (column.ToString() == "BaseAmnt" && Convert.ToDecimal(row["BaseAmnt"]) != 0)
                    {
                        row["BaseAmnt"] = Properties.Settings.Default.MainCurrency + " " + String.Format(row["BaseAmnt"].ToString());
                    }

                    else if (column.ToString() == "BaseAmntSC" && Convert.ToDecimal(row["BaseAmntSC"]) != 0)
                    {
                        row["BaseAmntSC"] = Properties.Settings.Default.SysCurrency + " " + String.Format(row["BaseAmntSC"].ToString());
                    }

                    else if (column.ToString() == "BaseAmntFC" && Convert.ToDecimal(row["BaseAmntFC"]) != 0)
                    {
                        row["BaseAmntFC"] = Currency + " " + String.Format(row["BaseAmntFC"].ToString());
                    }

                    else if (column.ToString() == "WTName")
                    {
                        row["WTName"] = cn.FindWTName(row["WTCode"].ToString());

                    }

                }

            }

            return dt;
        }

        private DataTable AddCurrencyCode(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {

                    if (column.ToString() == "Price" && Convert.ToDecimal(row["Price"]) != 0)
                    {
                        row["Price"] = Currency + " " + String.Format(row["Price"].ToString());

                    }

                    else if (column.ToString() == "LineTotal" && Convert.ToDecimal(row["LineTotal"]) != 0)
                    {
                        row["LineTotal"] = Properties.Settings.Default.MainCurrency + " " + String.Format(row["LineTotal"].ToString());
                    }

                    else if (column.ToString() == "TotalSumSy" && Convert.ToDecimal(row["TotalSumSy"]) != 0)
                    {
                        row["TotalSumSy"] = Properties.Settings.Default.SysCurrency + " " + String.Format(row["TotalSumSy"].ToString());
                    }

                    else if (column.ToString() == "TotalFrgn" && Convert.ToDecimal(row["TotalFrgn"]) != 0)
                    {
                        row["TotalFrgn"] = Currency + " " + String.Format(row["TotalFrgn"].ToString());
                    }

                    else if (column.ToString() == "AcctCode")
                    {
                        row["AcctName"] = cn.FindAcctName(row["AcctCode"].ToString());

                    }

                    else if (column.ToString() == "StartValue")
                    {

                        if(row["StartValue"].ToString() == "N")
                        {
                            row["StartValue"] = false;
                        }
                        else if (row["StartValue"].ToString() == "Y")
                        {
                            row["StartValue"] = true;
                        }
                        else
                        {
                            row["StartValue"] = false;
                        }

                    }

                    else if (column.ToString() == "IsTax")
                    {

                        if (row["IsTax"].ToString() == "N")
                        {
                            row["IsTax"] = false;
                        }
                        else if (row["IsTax"].ToString() == "Y")
                        {
                            row["IsTax"] = true;
                        }
                        else
                        {
                            row["IsTax"] = false;
                        }

                    }

                }

            }

            return dt;
        }

        private void GetEntregaMercanciaLines(DataTable listaPurchaseInvoiceLines)
        {
            dt = AddCurrencyCode(listaPurchaseInvoiceLines);

            dg.ItemsSource = dt.DefaultView;

            dg.CanUserAddRows = false;

            dg.CanUserDeleteRows = false;

            dg.CanUserSortColumns = false;
        }

        private void GetEntregaMercancia(List<DocumentoCabecera> listaPurchaseInvoice)
        {
            foreach (DocumentoCabecera purchase in listaPurchaseInvoice)
            {
                DocEntry = purchase.DocEntry;
                DocNum = purchase.DocNum;
                txtNro.Text = purchase.DocNum.ToString();
                dpContabilizacion.SelectedDate = purchase.DocDate;
                dpDocumento.SelectedDate = purchase.TaxDate;
                dpVencimiento.SelectedDate = purchase.DocDueDate;
                txtProveedor.Text = purchase.CardCode;
                txtNombre.Text = purchase.CardName;
                txtNroFactura.Text = purchase.NumAtCard;               
                var result10 = cn.GetCurrencyName(purchase.DocCurr);
                cbMoneda.ItemsSource = cn.CreateCurrencyTable(purchase.DocCurr, result10.Item1);
                cbMoneda.SelectedValue = purchase.DocCurr;                
                txtEstado.Text = cn.GetDocStatus(purchase.DocStatus);
                cbClase.SelectedValue = cn.GetDocType(purchase.DocType);
                txtDestino.Text = purchase.Address2;
                txtComentario.Text = purchase.Comments;               
                txtEntradaDiario.Text = purchase.JrnlMemo;

                if (cbMoneda.SelectedValue.ToString() == Properties.Settings.Default.MainCurrency)
                {
                    txtTotalAntesDescuento.Text = cbMoneda.SelectedValue.ToString() + " " + purchase.BaseAmnt;
                    txtTotalDocumento.Text = cbMoneda.SelectedValue.ToString() + " " + purchase.DocTotal;
                    txtRetenciones.Text = cbMoneda.SelectedValue.ToString() + " " + purchase.WTSum;
                    txtImpuesto.Text = cbMoneda.SelectedValue.ToString() + " " + purchase.VatSum;

                }
                else
                {
                    txtTotalAntesDescuento.Text = cbMoneda.SelectedValue.ToString() + " " + purchase.BaseAmntFC;
                    txtTotalDocumento.Text = cbMoneda.SelectedValue.ToString() + " " + purchase.DocTotalFC;
                    txtRetenciones.Text = cbMoneda.SelectedValue.ToString() + " " + purchase.WTSumFC;
                    txtImpuesto.Text = cbMoneda.SelectedValue.ToString() + " " + purchase.VatSumFC;
                }


            }

            ReadOnlyFieldOPCH();

        }

        private void ReadOnlyFieldOPCH()
        {
            txtNro.Background = Brushes.LightGray;
            txtEstado.Background = Brushes.LightGray;
            txtProveedor.Background = Brushes.LightGray;
            txtNombre.Background = Brushes.LightGray;
            cbMoneda.Background = Brushes.LightGray;
            cbClase.Background = Brushes.LightGray;
            dpProveedor.Background = Brushes.LightGray;
            bdProveedor.Background = Brushes.LightGray;
            txtDestino.Background = Brushes.LightGray;
            txtPagar.Background = Brushes.LightGray;            
            txtNro.IsReadOnly = true;
            txtEstado.IsReadOnly = true;
            txtProveedor.IsReadOnly = true;
            txtNombre.IsReadOnly = true;
            dpContabilizacion.IsEnabled = false;
            cbMoneda.IsReadOnly = true;
            cbMoneda.IsEnabled = false;
            cbClase.IsReadOnly = true;
            cbClase.IsEnabled = false;
            txtTotalAntesDescuento.IsReadOnly = true;
            txtImporte.IsReadOnly = true;
            txtRetenciones.IsReadOnly = true;
            txtTotalDocumento.IsReadOnly = true;
            txtImporte.IsReadOnly = true;
            txtSaldo.IsReadOnly = true;
            txtDestino.IsReadOnly = true;
            txtPagar.IsReadOnly = true;          
            //dg.IsReadOnly = true;

            ReadOnlyDatetime(dpContabilizacion);


        }

        private void ReadOnlyDatetime(DatePicker dp)
        {
            TextBox tb = (TextBox)dp.Template.FindName("PART_TextBox", dp);

            tb.IsReadOnly = true;
            tb.Background = Brushes.LightGray;
        }

        private void imgleft_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (btnCrear.Content.ToString() == "OK" || btnCrear.Content.ToString() == "Crear" || btnCrear.Content.ToString() == "Buscar")
            {
                ReestablecerFondo();

                ListaEntregaMercancia.Clear();

                ListaEntregaMercanciaLines.Clear();

                var result = cn.FindPreviousEntregaMercancia(txtNro.Text);

                if (result.Item2 == null)
                {
                    SwReadOnly = true;

                    ListaEntregaMercancia = result.Item1;

                    GetEntregaMercancia(ListaEntregaMercancia);

                    var result1 = cn.FindEntregaMercanciaLines(DocEntry);

                    if (result1.Item2 == null)
                    {
                        ListaEntregaMercanciaLines = result1.Item1;

                        GetEntregaMercanciaLines(ListaEntregaMercanciaLines);

                        btnCrear.Content = "OK";

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

                ListaEntregaMercancia.Clear();

                ListaEntregaMercanciaLines.Clear();
                
                var result = cn.FindFirstEntregaMercancia();

                if (result.Item2 == null)
                {
                    SwReadOnly = true;

                    ListaEntregaMercancia = result.Item1;

                    GetEntregaMercancia(ListaEntregaMercancia);

                    var result1 = cn.FindEntregaMercanciaLines(DocEntry);

                    if (result1.Item2 == null)
                    {
                        ListaEntregaMercanciaLines = result1.Item1;

                        GetEntregaMercanciaLines(ListaEntregaMercanciaLines);

                        btnCrear.Content = "OK";

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

            cbMoneda.ItemsSource = cn.CreateCurrencyTable("", "");

            dg.CanUserAddRows = true;

            dg.CanUserDeleteRows = true;

            dg.CanUserSortColumns = true;

            dt.Rows.Clear();

            dg.ItemsSource = dt.DefaultView;
        }

        private void EstablecerFondo()
        {
            Style style = Application.Current.FindResource("ComboBoxStyleHover") as Style;

            txtNro.Background = Brushes.LightBlue;
            txtNro.IsReadOnly = false;
            txtEstado.Background = Brushes.LightBlue;
            txtEstado.IsReadOnly = false;
            txtProveedor.Background = Brushes.LightBlue;
            txtProveedor.IsReadOnly = false;
            bdProveedor.Background = Brushes.LightBlue;
            dpProveedor.Background = Brushes.LightBlue;
            txtNombre.Background = Brushes.LightBlue;
            txtNombre.IsReadOnly = false;
            txtNroFactura.Background = Brushes.LightBlue;         
            txtComentario.Background = Brushes.LightBlue;           
            cbMoneda.Style = style;
            cbClase.Style = style;
            dpContabilizacion.Background = Brushes.LightBlue;
            dpDocumento.Background = Brushes.LightBlue;
            dpVencimiento.Background = Brushes.LightBlue;
            establecerDatetime(dpContabilizacion);
            establecerDatetime(dpDocumento);
            establecerDatetime(dpVencimiento);
        }

        public void establecerDatetime(DatePicker dpk)
        {
            TextBox tb = (TextBox)dpk.Template.FindName("PART_TextBox", dpk);

            tb.BorderThickness = new Thickness(1);
            tb.Background = Brushes.LightBlue;
            tb.BorderBrush = Brushes.Gray;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCurrency();

            LoadColumnDgArticulo();
          
            LoadDatatableJournalEntry();

            LimpiarCampos();

            ReestablecerFondo();

            InicialiacionBasica();

            ClaseFactura = new ObservableCollection<string>() { "Articulo" };

            cbClase.ItemsSource = ClaseFactura;

            WtLiable = new ObservableCollection<string>() { "SI", "NO" };
            
            cbSujetoRetencionItem.ItemsSource = WtLiable;

            cbClase.SelectedValue = "Articulo";

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

       

        private void LoadColumnDgArticulo()
        {
            dtArticulo.Columns.Add("ItemCode", typeof(string));
            dtArticulo.Columns.Add("Dscription");
            dtArticulo.Columns.Add("Price");
            dtArticulo.Columns.Add("Quantity", typeof(int));
            dtArticulo.Columns.Add("LineStatus");
            dtArticulo.Columns.Add("Currency");
            dtArticulo.Columns.Add("Rate");
            dtArticulo.Columns.Add("VatGroup");
            dtArticulo.Columns.Add("WtLiable");
            dtArticulo.Columns.Add("LineTotal");
            dtArticulo.Columns.Add("AcctCode");
            dtArticulo.Columns.Add("VatPrcnt");
            dtArticulo.Columns.Add("VatSum");
            dtArticulo.Columns.Add("VatSumFrgn");
            dtArticulo.Columns.Add("VatSumSy");
            dtArticulo.Columns.Add("TotalSumSy");
            dtArticulo.Columns.Add("GTotal");
            dtArticulo.Columns.Add("GTotalFC");
            dtArticulo.Columns.Add("GTotalSC");
            dtArticulo.Columns.Add("TotalFrgn");
            dtArticulo.Columns.Add("DocDate");
            dtArticulo.Columns.Add("InvntSttus");
            dtArticulo.Columns.Add("FinncPriod");
            dtArticulo.Columns.Add("ObjType");
            dtArticulo.Columns.Add("Address");
            dtArticulo.Columns.Add("StockSum");
            dtArticulo.Columns.Add("StockSumFc");
            dtArticulo.Columns.Add("StockSumSc");
            dtArticulo.Columns.Add("InvQty");
            dtArticulo.Columns.Add("OpenQty");
            dtArticulo.Columns.Add("OpenInvQty");
            dtArticulo.Columns.Add("BaseOpnQty");
            dtArticulo.Columns.Add("AcctName");
            dtArticulo.Columns.Add("UgpEntry");
            dtArticulo.Columns.Add("NumPerMsr");
            dtArticulo.Columns.Add("NumPerMsr2");
            dtArticulo.Columns.Add("UomCode");
            dtArticulo.Columns.Add("UomCode2");
            dtArticulo.Columns.Add("UomEntry");
            dtArticulo.Columns.Add("UomEntry2");
            dtArticulo.Columns.Add("unitMsr");
            dtArticulo.Columns.Add("unitMsr2");
            dtArticulo.Columns.Add("IsTax");
            dtArticulo.Columns.Add("StartValue");

            dtArticulo.NewRow();

            dgArticulo.ItemsSource = dtArticulo.DefaultView;

        }

        private void txt_GotFocus(object sender, RoutedEventArgs e)
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

        private void txt_LostFocus(object sender, RoutedEventArgs e)
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

        public void InicialiacionBasica()
        {
            dpContabilizacion.SelectedDate = fechaActual.GetFechaActual();

            dpVencimiento.SelectedDate = fechaActual.GetFechaActual();

            dpDocumento.SelectedDate = fechaActual.GetFechaActual();

            var docNum = cn.SelectDocNum();

            if (docNum.Item2 == null)
            {
                txtNro.Text = Convert.ToString(docNum.Item1);
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + docNum.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
            }
        }

        private void dpFechaContabilizacion_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            FindRate();

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

        private void FindRateFC()
        {
            DateTime? fecha = dpContabilizacion.SelectedDate;

            SelectedDate = String.Format("{0:yyyy/MM/dd}", fecha);

            if (String.IsNullOrWhiteSpace(SelectedDate) == false)
            {
                var result= cn.FindRateFCCurrency(Convert.ToDateTime(SelectedDate), Currency);

                if (result.Item2 == null)
                {
                    RateFC = result.Item1;
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

        private void ReestablecerFondo()
        {
            Style style = Application.Current.FindResource("ComboBoxStyle") as Style;

            txtNro.IsReadOnly = true;

            txtEstado.Text = "Abierto";

            txtEstado.IsReadOnly = true;

            txtNro.Background = Brushes.White;

            txtEstado.Background = Brushes.White;

            txtProveedor.Background = Brushes.White;

            txtNombre.Background = Brushes.White;

            txtNroFactura.Background = Brushes.White;
           
            cbMoneda.Style = style;

            cbClase.Style = style;

            txtDestino.Background = Brushes.White;

            txtPagar.Background = Brushes.White;

            txtEntradaDiario.Background = Brushes.White;

            txtEntradaDiario.IsReadOnly = true;          

            txtDestino.IsReadOnly = true;

            txtPagar.IsReadOnly = true;

            dpContabilizacion.Background = Brushes.White;

            dpDocumento.Background = Brushes.White;

            dpVencimiento.Background = Brushes.White;

            txtComentario.Background = Brushes.White;

            bdProveedor.Background = Brushes.White;

            dpProveedor.Background = Brushes.White;

            cbClase.IsEnabled = true;

            cbMoneda.IsEnabled = true;

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

            txtNroFactura.Text = "";
         
            txtComentario.Text = "";

            txtDestino.Text = "";

            txtPagar.Text = "";

            txtEntradaDiario.Text = "";
                      
            txtImporte.Text = "";

            txtSaldo.Text = "";

            txtTotalAntesDescuento.Text = Properties.Settings.Default.MainCurrency + " " + String.Format("{0:#,#.00}", "0,00");

            txtTotalDocumento.Text = Properties.Settings.Default.MainCurrency + " " + String.Format("{0:#,#.00}", "0,00");

            txtImpuesto.Text = Properties.Settings.Default.MainCurrency + " " + String.Format("{0:#,#.00}", "0,00");

            txtRetenciones.Text = Properties.Settings.Default.MainCurrency + " " + String.Format("{0:#,#.00}", "0,00");

            ClearDgArticulo();           

        }

        private void ClearDgArticulo()
        {
            dtArticulo.Rows.Clear();
            
            dgArticulo.ItemsSource = dtArticulo.DefaultView;

        }

       
        private void cbMoneda_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EnabledDatagrid();

            Prueba();
        }

        private void Prueba()
        {
            if (cbMoneda.SelectedIndex > -1)
            {
                Currency = cbMoneda.SelectedValue.ToString();

                txtTotalDocumento.Text = Currency + " " + String.Format("{0:#,#.00}", "0.00");

                if (cbMoneda.SelectedValue.ToString() == Properties.Settings.Default.MainCurrency)
                {

                    dttotali.Visibility = Visibility.Visible;

                    dttotalmei.Visibility = Visibility.Hidden;



                }
                else
                {

                    dttotali.Visibility = Visibility.Hidden;

                    dttotalmei.Visibility = Visibility.Visible;


                }
            }
        }

        private void cbClase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
                dgArticulo.Visibility = Visibility.Visible;

                dt = dtArticulo;

                dg = dgArticulo;


                if (String.IsNullOrWhiteSpace(Currency) == false)
                {
                    Prueba();
                }

            


        }

        private void imgProveedor_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var result = cn.FindCustomer();

            if (result.Item2 == null)
            {
                RecorreListaSN(result.Item1);
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
            }

           
        }

        private void RecorreListaSN(List<SocioNegocio> listSuppliers)
        {
            if (listSuppliers.Count == 1)
            {
                GetSocioNegocio(listSuppliers);

                var result= cn.FindSupplierCurrency(Supplier);

                if (result.Item2 == null)
                {
                    cbMoneda.ItemsSource = result.Item1;
                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
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

                        txtProveedor.Background = Brushes.White;

                        var result = cn.FindSupplierCurrency(Supplier);

                        if (result.Item2 == null)
                        {
                            cbMoneda.ItemsSource = result.Item1;
                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
                        }

                    }

                    //btnCrear.Content = "OK";
                }
                ReestablecerFondo();
            }
        }

        private void EnabledDatagrid()
        {
            dgArticulo.IsEnabled = true;

        }

        private void GetSocioNegocio(List<SocioNegocio> listSuppliers)
        {
            foreach (SocioNegocio Suppliers in listSuppliers)
            {
                Supplier = Suppliers.CardCode;

                txtProveedor.Text = Suppliers.CardCode;

                txtNombre.Text = Suppliers.CardName;

                txtDestino.Text = Suppliers.Address;

                txtEntradaDiario.Text = "Entrega Mercancia - " + Suppliers.CardCode;
              
                VatGroup = Suppliers.VatGroup;

                CtlAccount = Suppliers.DebPayAcct;

                LicTradNum = Suppliers.LicTradNum;
            }
        }

        private void imgAcctCode_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                bool? sw = null;

                var row_list = GetDataGridRows(dg);

                DataRowView row_Selected = dg.SelectedItem as DataRowView;

                foreach (DataGridRow single_row in row_list)
                {
                    if (single_row.IsSelected == true)
                    {
                        TextBox txtAcctCode = FindChild<TextBox>(single_row, "txtAcctCode");

                        TextBlock txtAcctName = FindChild<TextBlock>(single_row, "txtAcctName");

                        TextBlock txtTaxCode = new TextBlock();

                        sw = IdentifyDatatable();

                        if (sw == true)
                        {
                            txtTaxCode = FindChild<TextBlock>(single_row, "txtTaxCode");
                        }
                        else
                        {

                            txtTaxCode = FindChild<TextBlock>(single_row, "txtTaxCodeItem");
                        }

                        var result = cn.ConsultaCuentasNoAsociadas();

                        if (result.Item2 == null)
                        {
                            RecorreListaAccount(result.Item1, txtAcctCode, txtAcctName, txtTaxCode, row_Selected);
                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
                        }  
                    }
                }
            }

            catch (Exception ex)
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + ex.Message, Brushes.Red, Brushes.Black, "003-interface-2.png");

            }

        }

        private void RecorreListaAccount(List<Cuenta> listAccountResultante, TextBox txtAcctCode, TextBlock txtAcctName, TextBlock txtTaxCode, DataRowView row_Selected)
        {
            if (listAccountResultante.Count == 0)
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se encontro ningun resultado con los parametros establecidos", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");

            }

            else if (listAccountResultante.Count > 0)
            {
                ListaCuentas ventanaListaCuentaAsociada = new ListaCuentas(listAccountResultante);

                ventanaListaCuentaAsociada.ShowDialog();

                if (ventanaListaCuentaAsociada.Visibility == Visibility.Hidden)
                {
                    if (ventanaListaCuentaAsociada.GetListAccount().Count == 0)
                    {

                    }
                    else
                    {
                        GetAcctCode(ventanaListaCuentaAsociada.GetListAccount(), txtAcctCode, txtAcctName, txtTaxCode, row_Selected);
                    }
                }
            }
        }

        private void GetAcctCode(List<Cuenta> listaCuenta, TextBox txtAcctCode, TextBlock txtAcctName, TextBlock txtTaxCode, DataRowView row_Selected)
        {
            foreach (Cuenta cuenta in listaCuenta)
            {
                txtAcctCode.Text = cuenta.AcctCode;

                row_Selected["AcctName"] = cuenta.AcctName;

                txtAcctName.Text = row_Selected["AcctName"].ToString();

                row_Selected["VatGroup"] = VatGroup;

                txtTaxCode.Text = row_Selected["VatGroup"].ToString();

                row_Selected["VatPrcnt"] = cn.FindTaxRate(txtTaxCode.Text);

                row_Selected["VatPrcnt"] = Convert.ToDecimal(row_Selected["VatPrcnt"]);
            }
        }


        private void imgTaxCode_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var row_list = GetDataGridRows(dg);

                DataRowView row_Selected = dg.SelectedItem as DataRowView;

                foreach (DataGridRow single_row in row_list)
                {
                    if (single_row.IsSelected == true)
                    {
                        TextBox txtTaxCode = FindChild<TextBox>(single_row, "txtTaxCode");

                        var result = cs.ConsultaCodigosFiscales();

                        if (result.Item2 == null)
                        {
                            RecorreListaTaxCode(result.Item1, txtTaxCode, row_Selected);
                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
                        }

                    }
                }
            }

            catch (Exception ex)
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error" + ex.Message, Brushes.Red, Brushes.Black, "003-interface-2.png");

            }
        }

        private void RecorreListaTaxCode(List<Entidades.CodigosFiscales> listCodigosResultantes, TextBox txtTaxCode, DataRowView row_Selected)
        {
            if (listCodigosResultantes.Count == 0)
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se encontro ningun resultado con los parametros establecidos", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");
            }

            else if (listCodigosResultantes.Count > 0)
            {
                ListaCodigosFiscales ventanaListaCodigos = new ListaCodigosFiscales(listCodigosResultantes);

                ventanaListaCodigos.ShowDialog();

                if (ventanaListaCodigos.Visibility == Visibility.Hidden)
                {
                    if (ventanaListaCodigos.GetListCodigosFiscales().Count == 0)
                    {

                    }
                    else
                    {
                        GetCodigos(ventanaListaCodigos.GetListCodigosFiscales(), txtTaxCode, row_Selected);
                    }
                }
            }
        }

        private void GetCodigos(List<Entidades.CodigosFiscales> listaCodigos, TextBox txtTaxCode, DataRowView row_Selected)
        {
            foreach (Entidades.CodigosFiscales codigo in listaCodigos)
            {
                txtTaxCode.Text = codigo.Code;

                row_Selected["VatPrcnt"] = codigo.Rate;

                row_Selected["VatPrcnt"] = Convert.ToDecimal(row_Selected["VatPrcnt"]);


            }
        }

        private void imgItemCode_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var row_list = GetDataGridRows(dgArticulo);

                DataRowView row_Selected = dgArticulo.SelectedItem as DataRowView;

                foreach (DataGridRow single_row in row_list)
                {
                    if (single_row.IsSelected == true)
                    {
                        TextBox txtItemCode = FindChild<TextBox>(single_row, "txtItemCode");

                        TextBlock txtPrice = FindChild<TextBlock>(single_row, "txtPrice");

                        TextBlock txtLineTotal = FindChild<TextBlock>(single_row, "txtLineTotal");

                        TextBlock txtTaxCode = FindChild<TextBlock>(single_row, "txtTaxCodeItem");

                        TextBlock txtUomCode = FindChild<TextBlock>(single_row, "txtUomCode");

                        TextBlock txtNumPerMsr = FindChild<TextBlock>(single_row, "txtNumPerMsr");

                        var result = ca.ConsultaItems();

                        if (result.Item2 == null)
                        {
                            RecorreListaItemCode(result.Item1, txtItemCode, txtPrice, txtLineTotal, txtTaxCode, txtUomCode, txtNumPerMsr, row_Selected);
                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
                        }

                    }
                }
            }

            catch (Exception ex)
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error" + ex.Message, Brushes.Red, Brushes.Black, "003-interface-2.png");
            }
        }

        private void RecorreListaItemCode (List<Entidades.Articulos> listArticulos, TextBox txtItemCode, TextBlock txtPrice, TextBlock txtLineTotal, TextBlock txtTaxCode, TextBlock txtUomCode, TextBlock txtNumPerMsr, DataRowView row_Selected)
        {
            if (listArticulos.Count == 0)
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se encontro ningun resultado con los parametros establecidos", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");

            }

            else if (listArticulos.Count > 0)
            {
                ListaArticulos ventanaListaCodigos = new ListaArticulos(listArticulos);

                ventanaListaCodigos.ShowDialog();

                if (ventanaListaCodigos.Visibility == Visibility.Hidden)
                {
                    if (ventanaListaCodigos.GetListItem().Count == 0)
                    {

                    }
                    else
                    {
                        GetItem(ventanaListaCodigos.GetListItem(), txtItemCode, txtPrice, txtLineTotal, txtTaxCode, txtUomCode, txtNumPerMsr, row_Selected);
                    }
                }
            }
        }

        private void GetItem(List<Entidades.Articulos> list, TextBox txtItemCode, TextBlock txtPrice, TextBlock txtLineTotal, TextBlock txtTaxCode, TextBlock txtUomCode, TextBlock txtNumPerMsr, DataRowView row_Selected)
        {
            foreach (Entidades.Articulos item in list)
            {
                txtItemCode.Text = item.ItemCode;

                row_Selected["VatGroup"] = VatGroup;

                row_Selected["UgpEntry"] = item.UgpEntry;

                txtUomCode.Text = item.InvntryUomCode;

                row_Selected["UomCode"] = item.InvntryUomCode;

                row_Selected["UomCode2"] = item.InvntryUomCode;

                row_Selected["unitMsr"] = item.InvntryUomName;

                row_Selected["unitMsr2"] = item.InvntryUomName;

                row_Selected["UomEntry"] = item.InvntryUomEntry;

                row_Selected["UomEntry2"] = item.InvntryUomEntry;

                row_Selected["NumPerMsr"] = item.NumInCnt;

                row_Selected["NumPerMsr2"] = item.NumInCnt;

                txtNumPerMsr.Text = ConvertDecimalTwoPlaces(item.NumInCnt).ToString("N2", nfi);

                txtTaxCode.Text = row_Selected["VatGroup"].ToString();

                row_Selected["VatPrcnt"] = cn.FindTaxRate(txtTaxCode.Text);

                row_Selected["VatPrcnt"] = Convert.ToDecimal(row_Selected["VatPrcnt"]);

            }
        }

        public class ListSujetoRetencion : List<string>
        {
            public ListSujetoRetencion()
            {
                this.Add("SI");
                this.Add("NO");

            }
        }

        private DataTable CalculateQuantity(DataTable dt)
        {
            DataTable newDt = new DataTable();

            newDt = dt.Copy();

            string[] ColumnsToBeDeleted = { "Dscription", "Price", "LineStatus", "Currency", "Rate", "VatGroup",

            "WtLiable","LineTotal","AcctCode","VatPrcnt","VatSum","VatSumFrgn","VatSumSy","TotalSumSy","GTotal",

            "GTotalFC", "GTotalSC","TotalFrgn","DocDate","InvntSttus","FinncPriod", "ObjType", "Address", "StockSum",

            "StockSumFc", "StockSumSc", "InvQty", "OpenQty", "OpenInvQty", "OpenInvQty", "OpenInvQty", "BaseOpnQty", "AcctName"
            };

            foreach (string ColName in ColumnsToBeDeleted)
            {
                if (newDt.Columns.Contains(ColName))
                    newDt.Columns.Remove(ColName);
            }

            var result = newDt.AsEnumerable()
            .GroupBy(row => new
            {

                ItemCode = row.Field<string>("ItemCode")
            })
            .Select(g =>
            {
                var row = g.First();
                row.SetField("Quantity", g.Sum(r => r.Field<int>("Quantity")));

                return row;
            });

            var resultTable = result.CopyToDataTable();

            return resultTable;
        }

        private Tuple<bool, List<ArticuloDetalle>> CreateListItem(List<DocumentoCabecera> listSalesInvoice, List<DocumentoDetalle> listSalesInvoiceLines)
        {
            List<ArticuloDetalle> listItemJE = new List<ArticuloDetalle>();

            bool sw = true;

            int DocNum = 0;

            string CardCode = null;

            string CardName = null;

            string Comments = null;

            DateTime? DocDueDate = null;

            DateTime? TaxDate = null;

            DateTime? UpdateDate = null;

            int UserSign = 0;

            string JrnlMemo = null;


            foreach (DocumentoCabecera SalesInvoice in listSalesInvoice)
            {
                DocNum = SalesInvoice.DocNum;
                DocDueDate = SalesInvoice.DocDueDate;
                TaxDate = SalesInvoice.TaxDate;
                CardCode = SalesInvoice.CardCode;
                CardName = SalesInvoice.CardName;
                Comments = SalesInvoice.Comments;
                JrnlMemo = SalesInvoice.JrnlMemo;
                UserSign = SalesInvoice.UserSign;
                UpdateDate = SalesInvoice.UpdateDate;

            }

            int l = 0;

            List<ArticuloDetalle> listItemSales = new List<ArticuloDetalle>();

            foreach (DocumentoDetalle SalesInvoiceLines in listSalesInvoiceLines)
            {
                ArticuloDetalle ItemSalesLines = new ArticuloDetalle();

                decimal quantityDemand = SalesInvoiceLines.Quantity * SalesInvoiceLines.NumPerMsr; //Obtengo Cantidad a vender

                decimal quantityOnHand;

                //-----------------------------------------------------------------------------------------

                while (quantityDemand > 0 && sw == true)
                {
                    decimal quantity = 0;

                    var FindFirstItemTransaccion = cn.FindFirstItemTransaccion(SalesInvoiceLines.ItemCode);

                    GetFindTransaccion(FindFirstItemTransaccion.Item1);

                    foreach (ArticuloDetalle articuloDetalle in FindFirstItemTransaccion.Item1) //Recorro lista de primer articulo disponible
                    {

                        quantityOnHand = articuloDetalle.OpenQty;//obtengo cantidad disponible

                        if (quantityDemand <= quantityOnHand) //si cantidad demandada es menor o igual a la disponible. utilizada demandada
                        {
                            quantity = quantityDemand;

                            //Insert OINM 

                            ArticuloDetalle ItemSalesLinesJE = new ArticuloDetalle();

                            decimal calcPrice = articuloDetalle.CalcPrice; // 
                            int TransSeq = articuloDetalle.TransSeq;
                            ItemSalesLinesJE.Rate = articuloDetalle.Rate;
                            ItemSalesLinesJE.SysRate = articuloDetalle.SysRate;
                            ItemSalesLinesJE.Currency = articuloDetalle.Currency;
                            ItemSalesLinesJE.InvntAct = articuloDetalle.InvntAct;
                            ItemSalesLinesJE.ItemCode = articuloDetalle.ItemCode;

                            ItemSalesLines.TransType = Convert.ToInt32(SalesInvoiceLines.ObjType);
                            ItemSalesLines.CreatedBy = SalesInvoiceLines.DocEntry;
                            ItemSalesLines.BASE_REF = DocNum.ToString();
                            ItemSalesLines.DocLineNum = SalesInvoiceLines.LineNum;
                            ItemSalesLines.DocDate = SalesInvoiceLines.DocDate;
                            ItemSalesLines.TaxDate = TaxDate;
                            ItemSalesLines.DocDueDate = DocDueDate;
                            ItemSalesLines.CardCode = CardCode;
                            ItemSalesLines.CardName = CardName;
                            ItemSalesLines.Comments = Comments;
                            ItemSalesLines.JrnlMemo = JrnlMemo;
                            ItemSalesLines.ItemCode = SalesInvoiceLines.ItemCode;
                            ItemSalesLines.Dscription = SalesInvoiceLines.Dscription;
                            ItemSalesLines.InQty = 0;
                            ItemSalesLines.OutQty = quantity;
                            ItemSalesLines.Price = SalesInvoiceLines.Price * quantity; //revisar;
                            ItemSalesLines.Currency = ItemSalesLinesJE.Currency;
                            ItemSalesLines.Rate = ItemSalesLinesJE.Rate;
                            ItemSalesLines.SysRate = ItemSalesLinesJE.SysRate;
                            ItemSalesLines.Type = 'E';
                            ItemSalesLines.UserSign = UserSign;
                            ItemSalesLines.CalcPrice = calcPrice;
                            ItemSalesLines.OpenQty = 0;
                            ItemSalesLines.CreateDate = UpdateDate;
                            ItemSalesLines.CostMethod = 'F';
                            ItemSalesLines.TransValue = (calcPrice * quantity) * (-1);
                            ItemSalesLines.OpenValue = calcPrice * quantity;
                            ItemSalesLines.InvntAct = ItemSalesLinesJE.InvntAct;
                            var balance = cn.CalculateBalanceItem(ItemSalesLines.ItemCode);
                            ItemSalesLines.Balance = balance.Item1 + ItemSalesLines.TransValue;

                            listItemSales.Add(ItemSalesLines);

                            ItemSalesLinesJE.CalcPrice = ItemSalesLines.OpenValue;
                            ItemSalesLinesJE.OpenQty = quantity;

                            var QueryItemSales = cn.InsertOINM(listItemSales);

                            if (QueryItemSales.Item1 == 1)
                            {

                                var getTransSeq = cn.GetLastTransSeq();

                                if (getTransSeq.Item2 == null)
                                {
                                    GetTransSeqNew(getTransSeq.Item1);

                                    listItemSales.Clear();

                                    var QueryItemPurchaseUpdate = cn.UpdateOINMRes(TransSeq, quantity, ItemSalesLines.OpenValue);

                                    if (QueryItemPurchaseUpdate.Item2 == null)
                                    {
                                        l++;

                                        listItemJE.Add(ItemSalesLinesJE);

                                        GetFindTransaccionNew(ItemSalesLinesJE);                                        

                                        quantityDemand = 0;
                                    }
                                    else
                                    {
                                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + QueryItemPurchaseUpdate.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                                        sw = false;
                                    }
                                }
                                else
                                {
                                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + getTransSeq.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                                    sw = false;
                                }


                            }
                            else
                            {
                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + QueryItemSales.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                                listItemSales.Clear();
                                sw = false;
                            }


                        }

                        else if (quantityDemand > quantityOnHand) //si cantidad demandada es mayor a la disponible. utilizada disponible
                        {
                            quantity = quantityOnHand;

                            //Insert OINM 

                            ArticuloDetalle ItemSalesLinesJE = new ArticuloDetalle();

                            decimal calcPrice = articuloDetalle.CalcPrice; // 
                            decimal openValue = articuloDetalle.OpenValue;
                            int TransSeq = articuloDetalle.TransSeq;
                            ItemSalesLinesJE.Rate = articuloDetalle.Rate;
                            ItemSalesLinesJE.SysRate = articuloDetalle.SysRate;
                            ItemSalesLinesJE.Currency = articuloDetalle.Currency;
                            ItemSalesLinesJE.InvntAct = articuloDetalle.InvntAct;
                            ItemSalesLinesJE.ItemCode = articuloDetalle.ItemCode;

                            ItemSalesLines.TransType = Convert.ToInt32(SalesInvoiceLines.ObjType);
                            ItemSalesLines.CreatedBy = SalesInvoiceLines.DocEntry;
                            ItemSalesLines.BASE_REF = DocNum.ToString();
                            ItemSalesLines.DocLineNum = SalesInvoiceLines.LineNum;
                            ItemSalesLines.DocDate = SalesInvoiceLines.DocDate;
                            ItemSalesLines.TaxDate = TaxDate;
                            ItemSalesLines.DocDueDate = DocDueDate;
                            ItemSalesLines.CardCode = CardCode;
                            ItemSalesLines.CardName = CardName;
                            ItemSalesLines.Comments = Comments;
                            ItemSalesLines.JrnlMemo = JrnlMemo;
                            ItemSalesLines.ItemCode = SalesInvoiceLines.ItemCode;
                            ItemSalesLines.Dscription = SalesInvoiceLines.Dscription;
                            ItemSalesLines.InQty = 0;
                            ItemSalesLines.OutQty = quantity;
                            ItemSalesLines.Price = SalesInvoiceLines.Price * quantity; //revisar
                            ItemSalesLines.Currency = ItemSalesLinesJE.Currency;
                            ItemSalesLines.Rate = ItemSalesLinesJE.Rate;
                            ItemSalesLines.SysRate = ItemSalesLinesJE.SysRate;
                            ItemSalesLines.Type = 'E';
                            ItemSalesLines.UserSign = UserSign;
                            ItemSalesLines.CalcPrice = calcPrice;
                            ItemSalesLines.OpenQty = 0;
                            ItemSalesLines.CreateDate = UpdateDate;
                            ItemSalesLines.CostMethod = 'F';
                            ItemSalesLines.TransValue = openValue * (-1);
                            ItemSalesLines.OpenValue = openValue;
                            ItemSalesLines.InvntAct = ItemSalesLinesJE.InvntAct;
                            var balance = cn.CalculateBalanceItem(ItemSalesLines.ItemCode);
                            ItemSalesLines.Balance = balance.Item1 + ItemSalesLines.TransValue;

                            listItemSales.Add(ItemSalesLines);

                            ItemSalesLinesJE.CalcPrice = ItemSalesLines.OpenValue;
                            ItemSalesLinesJE.OpenQty = quantity;

                            var QueryItemSales = cn.InsertOINM(listItemSales);

                            if (QueryItemSales.Item1 == 1)
                            {

                                var getTransSeq = cn.GetLastTransSeq();

                                if (getTransSeq.Item2 == null)
                                {
                                    GetTransSeqNew(getTransSeq.Item1);

                                    listItemSales.Clear();

                                    var QueryItemPurchaseUpdate = cn.UpdateOINMRes(TransSeq, quantity, ItemSalesLines.OpenValue);

                                    if (QueryItemPurchaseUpdate.Item2 == null)
                                    {
                                        l++;

                                        listItemJE.Add(ItemSalesLinesJE);

                                        GetFindTransaccionNew(ItemSalesLinesJE);
                                      
                                        quantityDemand = quantityDemand - quantity;
                                    }
                                    else
                                    {
                                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + QueryItemPurchaseUpdate.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                                        sw = false;
                                    }
                                }
                                else
                                {
                                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + getTransSeq.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                                    sw = false;
                                }


                            }
                            else

                            {
                                listItemSales.Clear();
                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + QueryItemSales.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                                sw = false;
                            }
                        }
                    }
                }
            }

            return Tuple.Create(sw, listItemJE);
        }

        private Tuple<bool, List<ArticuloDetalle>> CreateListItemPreliminar(List<DocumentoCabecera> listSalesInvoice, List<DocumentoDetalle> listSalesInvoiceLines)
        {
            List<ArticuloDetalle> listItemJE = new List<ArticuloDetalle>();

            bool sw = true;

            int DocNum = 0;

            string CardCode = null;

            string CardName = null;

            string Comments = null;

            DateTime? DocDueDate = null;

            DateTime? TaxDate = null;

            DateTime? UpdateDate = null;

            int UserSign = 0;

            string JrnlMemo = null;


            foreach (DocumentoCabecera SalesInvoice in listSalesInvoice)
            {
                DocNum = SalesInvoice.DocNum;
                DocDueDate = SalesInvoice.DocDueDate;
                TaxDate = SalesInvoice.TaxDate;
                CardCode = SalesInvoice.CardCode;
                CardName = SalesInvoice.CardName;
                Comments = SalesInvoice.Comments;
                JrnlMemo = SalesInvoice.JrnlMemo;
                UserSign = SalesInvoice.UserSign;
                UpdateDate = SalesInvoice.UpdateDate;

            }

            int l = 0;

            List<ArticuloDetalle> listItemSales = new List<ArticuloDetalle>();

            foreach (DocumentoDetalle SalesInvoiceLines in listSalesInvoiceLines)
            {
                ArticuloDetalle ItemSalesLines = new ArticuloDetalle();

                decimal quantityDemand = SalesInvoiceLines.Quantity * SalesInvoiceLines.NumPerMsr; //Obtengo Cantidad a vender

                decimal quantityOnHand;

                //-----------------------------------------------------------------------------------------

                while (quantityDemand > 0 && sw == true)
                {
                    decimal quantity = 0;

                    var FindFirstItemTransaccion = cn.FindFirstItemTransaccionPreliminar(SalesInvoiceLines.ItemCode);

                    GetFindTransaccion(FindFirstItemTransaccion.Item1);

                    foreach (ArticuloDetalle articuloDetalle in FindFirstItemTransaccion.Item1) //Recorro lista de primer articulo disponible
                    {

                        quantityOnHand = articuloDetalle.OpenQty; //obtengo cantidad disponible

                        if (quantityDemand <= quantityOnHand) //si cantidad demandada es menor o igual a la disponible. utilizada demandada
                        {
                            quantity = quantityDemand;

                            //Insert OINM 

                            ArticuloDetalle ItemSalesLinesJE = new ArticuloDetalle();

                            decimal calcPrice = articuloDetalle.CalcPrice; // 
                            int TransSeq = articuloDetalle.TransSeq;
                            ItemSalesLinesJE.Rate = articuloDetalle.Rate;
                            ItemSalesLinesJE.SysRate = articuloDetalle.SysRate;
                            ItemSalesLinesJE.Currency = articuloDetalle.Currency;
                            ItemSalesLinesJE.InvntAct = articuloDetalle.InvntAct;
                            ItemSalesLinesJE.ItemCode = articuloDetalle.ItemCode;

                            ItemSalesLines.TransType = Convert.ToInt32(SalesInvoiceLines.ObjType);
                            ItemSalesLines.CreatedBy = SalesInvoiceLines.DocEntry;
                            ItemSalesLines.BASE_REF = DocNum.ToString();
                            ItemSalesLines.DocLineNum = SalesInvoiceLines.LineNum;
                            ItemSalesLines.DocDate = SalesInvoiceLines.DocDate;
                            ItemSalesLines.TaxDate = TaxDate;
                            ItemSalesLines.DocDueDate = DocDueDate;
                            ItemSalesLines.CardCode = CardCode;
                            ItemSalesLines.CardName = CardName;
                            ItemSalesLines.Comments = Comments;
                            ItemSalesLines.JrnlMemo = JrnlMemo;
                            ItemSalesLines.ItemCode = SalesInvoiceLines.ItemCode;
                            ItemSalesLines.Dscription = SalesInvoiceLines.Dscription;
                            ItemSalesLines.InQty = 0;
                            ItemSalesLines.OutQty = quantity;
                            ItemSalesLines.Price = SalesInvoiceLines.Price * quantity; //revisar;
                            ItemSalesLines.Currency = ItemSalesLinesJE.Currency;
                            ItemSalesLines.Rate = ItemSalesLinesJE.Rate;
                            ItemSalesLines.SysRate = ItemSalesLinesJE.SysRate;
                            ItemSalesLines.Type = 'E';
                            ItemSalesLines.UserSign = UserSign;
                            ItemSalesLines.CalcPrice = calcPrice;
                            ItemSalesLines.OpenQty = 0;
                            ItemSalesLines.CreateDate = UpdateDate;
                            ItemSalesLines.CostMethod = 'F';
                            ItemSalesLines.TransValue = (calcPrice * quantity) * (-1);
                            ItemSalesLines.OpenValue = calcPrice * quantity;
                            ItemSalesLines.InvntAct = ItemSalesLinesJE.InvntAct;
                            var balance = cn.CalculateBalanceItemPreliminar(ItemSalesLines.ItemCode,false);
                            ItemSalesLines.Balance = balance.Item1 + ItemSalesLines.TransValue;

                            listItemSales.Add(ItemSalesLines);

                            ItemSalesLinesJE.CalcPrice = ItemSalesLines.OpenValue;
                            ItemSalesLinesJE.OpenQty = quantity;

                            var QueryItemSales = cn.InsertOINMPreliminar(listItemSales);

                            if (QueryItemSales.Item1 == 1)
                            {

                                var getTransSeq = cn.GetLastTransSeqPreliminar();

                                if (getTransSeq.Item2 == null)
                                {
                                    GetTransSeqNew(getTransSeq.Item1);

                                    listItemSales.Clear();

                                    var QueryItemPurchaseUpdate = cn.UpdateOINMResPreliminar(TransSeq, quantity, ItemSalesLines.OpenValue);

                                    if (QueryItemPurchaseUpdate.Item2 == null)
                                    {
                                        l++;

                                        listItemJE.Add(ItemSalesLinesJE);

                                        GetFindTransaccionNew(ItemSalesLinesJE);

                                        quantityDemand = 0;
                                    }
                                    else
                                    {
                                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + QueryItemPurchaseUpdate.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                                        sw = false;
                                    }
                                }
                                else
                                {
                                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + getTransSeq.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                                    sw = false;
                                }


                            }
                            else
                            {
                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + QueryItemSales.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                                listItemSales.Clear();
                                sw = false;
                            }


                        }

                        else if (quantityDemand > quantityOnHand) //si cantidad demandada es mayor a la disponible. utilizada disponible
                        {
                            quantity = quantityOnHand;

                            //Insert OINM 

                            ArticuloDetalle ItemSalesLinesJE = new ArticuloDetalle();

                            decimal calcPrice = articuloDetalle.CalcPrice; // 
                            decimal openValue = articuloDetalle.OpenValue;
                            int TransSeq = articuloDetalle.TransSeq;
                            ItemSalesLinesJE.Rate = articuloDetalle.Rate;
                            ItemSalesLinesJE.SysRate = articuloDetalle.SysRate;
                            ItemSalesLinesJE.Currency = articuloDetalle.Currency;
                            ItemSalesLinesJE.InvntAct = articuloDetalle.InvntAct;
                            ItemSalesLinesJE.ItemCode = articuloDetalle.ItemCode;

                            ItemSalesLines.TransType = Convert.ToInt32(SalesInvoiceLines.ObjType);
                            ItemSalesLines.CreatedBy = SalesInvoiceLines.DocEntry;
                            ItemSalesLines.BASE_REF = DocNum.ToString();
                            ItemSalesLines.DocLineNum = SalesInvoiceLines.LineNum;
                            ItemSalesLines.DocDate = SalesInvoiceLines.DocDate;
                            ItemSalesLines.TaxDate = TaxDate;
                            ItemSalesLines.DocDueDate = DocDueDate;
                            ItemSalesLines.CardCode = CardCode;
                            ItemSalesLines.CardName = CardName;
                            ItemSalesLines.Comments = Comments;
                            ItemSalesLines.JrnlMemo = JrnlMemo;
                            ItemSalesLines.ItemCode = SalesInvoiceLines.ItemCode;
                            ItemSalesLines.Dscription = SalesInvoiceLines.Dscription;
                            ItemSalesLines.InQty = 0;
                            ItemSalesLines.OutQty = quantity;
                            ItemSalesLines.Price = SalesInvoiceLines.Price * quantity; //revisar
                            ItemSalesLines.Currency = ItemSalesLinesJE.Currency;
                            ItemSalesLines.Rate = ItemSalesLinesJE.Rate;
                            ItemSalesLines.SysRate = ItemSalesLinesJE.SysRate;
                            ItemSalesLines.Type = 'E';
                            ItemSalesLines.UserSign = UserSign;
                            ItemSalesLines.CalcPrice = calcPrice;
                            ItemSalesLines.OpenQty = 0;
                            ItemSalesLines.CreateDate = UpdateDate;
                            ItemSalesLines.CostMethod = 'F';
                            ItemSalesLines.TransValue = openValue * (-1);
                            ItemSalesLines.OpenValue = openValue;
                            ItemSalesLines.InvntAct = ItemSalesLinesJE.InvntAct;
                            var balance = cn.CalculateBalanceItemPreliminar(ItemSalesLines.ItemCode,false);
                            ItemSalesLines.Balance = balance.Item1 + ItemSalesLines.TransValue;

                            listItemSales.Add(ItemSalesLines);

                            ItemSalesLinesJE.CalcPrice = ItemSalesLines.OpenValue;
                            ItemSalesLinesJE.OpenQty = quantity;

                            var QueryItemSales = cn.InsertOINMPreliminar(listItemSales);

                            if (QueryItemSales.Item1 == 1)
                            {

                                var getTransSeq = cn.GetLastTransSeqPreliminar();

                                if (getTransSeq.Item2 == null)
                                {
                                    GetTransSeqNew(getTransSeq.Item1);

                                    listItemSales.Clear();

                                    var QueryItemPurchaseUpdate = cn.UpdateOINMResPreliminar(TransSeq, quantity, ItemSalesLines.OpenValue);

                                    if (QueryItemPurchaseUpdate.Item2 == null)
                                    {
                                        l++;

                                        listItemJE.Add(ItemSalesLinesJE);

                                        GetFindTransaccionNew(ItemSalesLinesJE);

                                        quantityDemand = quantityDemand - quantity;
                                    }
                                    else
                                    {
                                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + QueryItemPurchaseUpdate.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                                        sw = false;
                                    }
                                }
                                else
                                {
                                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + getTransSeq.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                                    sw = false;
                                }


                            }
                            else

                            {
                                listItemSales.Clear();
                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + QueryItemSales.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                                sw = false;
                            }
                        }
                    }
                }
            }

            return Tuple.Create(sw, listItemJE);
        }

        private void GetFindTransaccionNew(ArticuloDetalle articuloDetalle)
        {
           
                ArticuloDetalle newItemDetails = new ArticuloDetalle();

                newItemDetails.ItemCode = articuloDetalle.ItemCode;
                newItemDetails.Currency = articuloDetalle.Currency;
                newItemDetails.Rate = articuloDetalle.Rate;
                newItemDetails.CalcPrice = articuloDetalle.CalcPrice;
                newItemDetails.OpenQty = articuloDetalle.OpenQty;
                newItemDetails.SysRate = articuloDetalle.SysRate;

                ListArticuloDetalleNew.Add(newItemDetails);
            
        }
        private void GetTransSeqNew(int TransSeq)
        {
            ListTransSeq.Add(TransSeq);
        }


        private void GetFindTransaccion(List<ArticuloDetalle> listArticuloDetalle)
        {
            foreach (ArticuloDetalle articuloDetalle in listArticuloDetalle)
            {
                ArticuloDetalle newItemDetails = new ArticuloDetalle();

                newItemDetails.TransSeq = articuloDetalle.TransSeq;
                newItemDetails.TransType = articuloDetalle.TransType;
                newItemDetails.CreatedBy = articuloDetalle.CreatedBy;
                newItemDetails.BASE_REF = articuloDetalle.BASE_REF;
                newItemDetails.DocLineNum = articuloDetalle.DocLineNum;
                newItemDetails.DocDate = articuloDetalle.DocDate;
                newItemDetails.DocDueDate = articuloDetalle.DocDueDate;
                newItemDetails.CardCode = articuloDetalle.CardCode;
                newItemDetails.CardName = articuloDetalle.CardName;
                newItemDetails.Comments = articuloDetalle.Comments;
                newItemDetails.JrnlMemo = articuloDetalle.JrnlMemo;
                newItemDetails.ItemCode = articuloDetalle.ItemCode;
                newItemDetails.Dscription = articuloDetalle.Dscription;
                newItemDetails.InQty = articuloDetalle.InQty;
                newItemDetails.OutQty = articuloDetalle.OutQty;
                newItemDetails.Price = articuloDetalle.Price;
                newItemDetails.Currency = articuloDetalle.Currency;
                newItemDetails.Rate = articuloDetalle.Rate;
                newItemDetails.TaxDate = articuloDetalle.TaxDate;
                newItemDetails.UserSign = articuloDetalle.UserSign;
                newItemDetails.CalcPrice = articuloDetalle.CalcPrice;
                newItemDetails.OpenQty = articuloDetalle.OpenQty;
                newItemDetails.CreateDate = articuloDetalle.CreateDate;
                newItemDetails.Balance = articuloDetalle.Balance;
                newItemDetails.TransValue = articuloDetalle.TransValue;
                newItemDetails.InvntAct = articuloDetalle.InvntAct;
                newItemDetails.OpenValue = articuloDetalle.OpenValue;
                newItemDetails.CostMethod = articuloDetalle.CostMethod;
                newItemDetails.Type = articuloDetalle.Type;
                newItemDetails.SysRate = articuloDetalle.SysRate;

                ListArticuloDetalleOld.Add(newItemDetails);
            }
        }

        private Tuple<List<DocumentoDetalle>, int> CreateListSalesLinesPreliminar(List<DocumentoCabecera> listSalesInvoice)
        {
            int DocNum = 0;

            string DocCurr = null;

            DateTime? DocDate = null;

            string CardCode = null;

            int FinncPriod = 0;

            string ObjType = null;

            string Address2 = null;

            char InvntSttus = 'S';

            foreach (DocumentoCabecera SalesInvoice in listSalesInvoice)
            {
                DocNum = SalesInvoice.DocNum;
                DocCurr = SalesInvoice.DocCurr;
                DocDate = SalesInvoice.DocDate;
                CardCode = SalesInvoice.CardCode;
                FinncPriod = SalesInvoice.FinncPriod;
                ObjType = SalesInvoice.ObjType;
                Address2 = SalesInvoice.Address2;
                InvntSttus = SalesInvoice.InvntSttus;

            }

            List<DocumentoDetalle> listSalesInvoiceLines = new List<DocumentoDetalle>();

            int i = 0;

            foreach (DataRow row in dt.Rows)
            {
                DocumentoDetalle SalesInvoiceLines = new DocumentoDetalle();

                var result3 = cn.FindDocEntryPreliminar(DocNum);
                DocEntryDeleted = result3.Item1;
                SalesInvoiceLines.DocEntry = result3.Item1;
                SalesInvoiceLines.LineNum = i;
                SalesInvoiceLines.LineStatus = 'C';
                SalesInvoiceLines.ItemCode = FindItemCode(row["ItemCode"].ToString());
                SalesInvoiceLines.Dscription = row["Dscription"].ToString();
                SalesInvoiceLines.NumPerMsr = ConvertDecimalTwoPlaces(row["NumPerMsr"]);
                SalesInvoiceLines.NumPerMsr2 = ConvertDecimalTwoPlaces(row["NumPerMsr2"]);
                SalesInvoiceLines.UomEntry = row["UomEntry"] == null || row["UomEntry"].ToString() == "" ? 0 : Convert.ToInt32(row["UomEntry"]);
                SalesInvoiceLines.UomEntry2 = row["UomEntry2"] == null || row["UomEntry2"].ToString() == "" ? 0 : Convert.ToInt32(row["UomEntry2"]);
                SalesInvoiceLines.UomCode = row["UomCode"] == null || row["UomCode"].ToString() == "" ? null : row["UomCode"].ToString();
                SalesInvoiceLines.UomCode2 = row["UomCode2"] == null || row["UomCode2"].ToString() == "" ? null : row["UomCode2"].ToString();
                SalesInvoiceLines.unitMsr = row["unitMsr"] == null || row["unitMsr"].ToString() == "" ? null : row["unitMsr"].ToString();
                SalesInvoiceLines.unitMsr2 = row["unitMsr2"] == null || row["unitMsr2"].ToString() == "" ? null : row["unitMsr2"].ToString();
                SalesInvoiceLines.Quantity = ConvertDecimalTwoPlaces(row["Quantity"].ToString());
                SalesInvoiceLines.OpenQty = SalesInvoiceLines.Quantity;
                SalesInvoiceLines.InvQty = SalesInvoiceLines.Quantity * SalesInvoiceLines.NumPerMsr;
                SalesInvoiceLines.OpenInvQty = SalesInvoiceLines.Quantity * SalesInvoiceLines.NumPerMsr;
                str = regex.Replace(row["LineTotal"].ToString(), String.Empty);
                SalesInvoiceLines.Price = ConvertDecimalTwoPlaces(str);
                SalesInvoiceLines.Currency = DocCurr;
                SalesInvoiceLines.LineTotal = ConvertDecimalTwoPlaces(str);
                SalesInvoiceLines.AcctCode = row["AcctCode"].ToString();
                SalesInvoiceLines.DocDate = DocDate;
                SalesInvoiceLines.BaseCard = CardCode;
                str = regex.Replace(row["TotalSumSy"].ToString(), String.Empty);
                SalesInvoiceLines.TotalSumSy = ConvertDecimalTwoPlaces(str);
                str = regex.Replace(row["TotalFrgn"].ToString(), String.Empty);
                SalesInvoiceLines.TotalFrgn = ConvertDecimalTwoPlaces(str);
                SalesInvoiceLines.VatSum = ConvertDecimalTwoPlaces(row["VatSum"].ToString());
                SalesInvoiceLines.VatSumFrgn = ConvertDecimalTwoPlaces(row["VatSumFrgn"].ToString());
                SalesInvoiceLines.VatSumSy = ConvertDecimalTwoPlaces(row["VatSumSy"].ToString());
                SalesInvoiceLines.VatGroup = row["VatGroup"].ToString();
                SalesInvoiceLines.VatPrcnt = ConvertDecimalTwoPlaces(row["VatPrcnt"].ToString());
                SalesInvoiceLines.FinncPriod = FinncPriod;
                SalesInvoiceLines.ObjType = ObjType;
                SalesInvoiceLines.Address = Address2;
                SalesInvoiceLines.Gtotal = ConvertDecimalTwoPlaces(row["GTotal"].ToString());
                SalesInvoiceLines.GtotalFC = ConvertDecimalTwoPlaces(row["GTotalFC"].ToString());
                SalesInvoiceLines.GtotalSC = ConvertDecimalTwoPlaces(row["GTotalSC"].ToString());
                SalesInvoiceLines.StockSum = ConvertDecimalTwoPlaces(row["StockSum"].ToString());
                SalesInvoiceLines.StockSum = GetItemOfService(dt, SalesInvoiceLines.LineTotal);
                SalesInvoiceLines.StockSumFc = ConvertDecimalTwoPlaces(row["StockSumFc"].ToString());
                SalesInvoiceLines.StockSumFc = GetItemOfService(dt, SalesInvoiceLines.TotalFrgn);
                SalesInvoiceLines.StockSumSc = ConvertDecimalTwoPlaces(row["StockSumSc"].ToString());
                SalesInvoiceLines.StockSumSc = GetItemOfService(dt, SalesInvoiceLines.TotalSumSy);
                SalesInvoiceLines.InvntSttus = InvntSttus;
                SalesInvoiceLines.WtLiable = cn.GetWTLiable(cbSujetoRetencionItem.SelectedValuePath.ToString());
                SalesInvoiceLines.DataSource = 'N';
                SalesInvoiceLines.IsTax = cn.TraduceChar(row["IsTax"] == null || row["IsTax"].ToString() == "" ? false : Convert.ToBoolean(row["IsTax"]));
                SalesInvoiceLines.StartValue = cn.TraduceChar(row["StartValue"] == null || row["StartValue"].ToString() == "" ? false : Convert.ToBoolean(row["StartValue"]));

                listSalesInvoiceLines.Add(SalesInvoiceLines);

                i++;

            }

            return Tuple.Create(listSalesInvoiceLines, i);
        }

        private List<DocumentoCabecera> CreateListSales()
        {
            List<DocumentoCabecera> listSalesInvoice = new List<DocumentoCabecera>();

            DocumentoCabecera SalesInvoice = new DocumentoCabecera();

            DocNumDeleted = Convert.ToInt32(txtNro.Text);

            SalesInvoice.DocNum = Convert.ToInt32(txtNro.Text);
            SalesInvoice.DocDate = dpContabilizacion.SelectedDate;
            SalesInvoice.TaxDate = dpDocumento.SelectedDate;
            SalesInvoice.DocDueDate = dpVencimiento.SelectedDate;
            SalesInvoice.NumAtCard = txtNroFactura.Text;            
            SalesInvoice.Comments = txtComentario.Text;
            SalesInvoice.DocType = cn.GetDocType(cbClase.SelectedValue.ToString());
            DocType = SalesInvoice.DocType;
            SalesInvoice.Canceled = 'N';
            SalesInvoice.UserSign = Properties.Settings.Default.Usuario;
            SalesInvoice.UpdateDate = fechaActual.GetFechaActual();
            SalesInvoice.DocStatus = cn.GetDocStatus(txtEstado.Text);
            var result10 = cn.GetPeriodCode(SalesInvoice.DocDate);
            SalesInvoice.FinncPriod = result10.Item1;
            SalesInvoice.Address2 = txtDestino.Text;
            SalesInvoice.InvntSttus = cn.CalculaInvStatus(cbClase.SelectedValue.ToString());
            SalesInvoice.VatSum = cn.CalculateVatSum(dt);
            SalesInvoice.VatSumFC = cn.CalculateVatSumFC(dt);
            SalesInvoice.VatSumSy1 = cn.CalculateVatSumSy(dt);
            SalesInvoice.DocCurr = cbMoneda.SelectedValue.ToString();
            SalesInvoice.ObjType = Convert.ToString(cn.GetTransType("NE"));
            SalesInvoice.CardCode = txtProveedor.Text;
            SalesInvoice.CardName = txtNombre.Text;
            SalesInvoice.JrnlMemo = txtEntradaDiario.Text;
            SalesInvoice.LicTradNum = LicTradNum;
            var result = cn.SelectTransId();
            TransId = result.Item1;
            SalesInvoice.TransId = result.Item1;
            SalesInvoice.VatPaid = 0;
            SalesInvoice.VatPaidFC = 0;
            SalesInvoice.VatPaidSys = 0;
            SalesInvoice.PaidToDate = 0;
            SalesInvoice.PaidSum = 0;
            SalesInvoice.PaidSumFc = 0;
            SalesInvoice.PaidSumSc = 0;
            SalesInvoice.WTApplied = 0;
            SalesInvoice.WTAppliedF = 0;
            SalesInvoice.WTAppliedS = 0;
            SalesInvoice.WTSum = 0;
            SalesInvoice.WTSumFC = 0;
            SalesInvoice.WTSumSC = 0;
            SalesInvoice.SysRate = Rate;

            if (SalesInvoice.DocCurr == Properties.Settings.Default.MainCurrency)
            {
                SalesInvoice.DocRate = 1;
            }
            else
            {
                SalesInvoice.DocRate = RateFC;
            }

            SalesInvoice.CtlAccount = CtlAccount;
            SalesInvoice.BaseAmnt = cn.CalculateBaseAmnt(dt);
            SalesInvoice.BaseAmntFC = cn.CalculateBaseAmntFC(dt);
            SalesInvoice.BaseAmntSC = cn.CalculateBaseAmntSC(dt);
            SalesInvoice.DocTotal = SalesInvoice.VatSum + SalesInvoice.BaseAmnt - SalesInvoice.WTSum;
            SalesInvoice.DocTotalFC = SalesInvoice.VatSumFC + SalesInvoice.BaseAmntFC - SalesInvoice.WTSumFC;
            SalesInvoice.DocTotalSy = SalesInvoice.VatSumSy1 + SalesInvoice.BaseAmntSC - SalesInvoice.WTSumSC;
            SalesInvoice.DocSubType = "--";
            SalesInvoice.Max1099 = SalesInvoice.VatSum + SalesInvoice.BaseAmnt;

            DocTotal1 = SalesInvoice.DocTotal;

            DocTotalFC1 = SalesInvoice.DocTotalFC;

            DocTotalSy1 = SalesInvoice.DocTotalSy;

            listSalesInvoice.Add(SalesInvoice);

            return listSalesInvoice;
        }

        private void btnCrear_Click(object sender, RoutedEventArgs e)
        {
            List<DocumentoCabecera> listSalesInvoice = new List<DocumentoCabecera>();

            DocumentoCabecera SalesInvoice = new DocumentoCabecera();

            List<DocumentoDetalle> listSalesInvoiceLines = new List<DocumentoDetalle>();

            List<Entidades.TablaRetencionImpuesto> listTablaRetenciones = new List<Entidades.TablaRetencionImpuesto>();

            List<AsientoCabecera> listaJournalEntry = new List<AsientoCabecera>();

            AsientoCabecera journalEntry = new AsientoCabecera();

            List<AsientoDetalle> listaJournalEntryLines = new List<AsientoDetalle>();

            string str;

            switch (btnCrear.Content.ToString())
            {
                case "OK":

                    this.Hide();

                    break;

                case "Buscar":

                    if (String.IsNullOrWhiteSpace(txtNro.Text) == false)
                    {
                        SalesInvoice.DocNum = Convert.ToInt32(txtNro.Text);
                    }
                    else
                    {
                        SalesInvoice.DocNum = 0;
                    }

                    SalesInvoice.DocDate = dpContabilizacion.SelectedDate;
                    SalesInvoice.TaxDate = dpDocumento.SelectedDate;
                    SalesInvoice.DocDueDate = dpVencimiento.SelectedDate;
                    SalesInvoice.CardCode = txtProveedor.Text;
                    SalesInvoice.CardName = txtNombre.Text;
                    SalesInvoice.NumAtCard = txtNroFactura.Text;
                    SalesInvoice.Comments = txtComentario.Text;

                    listSalesInvoice.Add(SalesInvoice);

                    var result = cn.FindEntregaMercancia(listSalesInvoice);

                    if (result.Item2 == null)
                    {
                        RecorreListaPurchaseInvoice(result.Item1);
                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                    }

                    break;

                case "Crear":

                    bool? ODLN = null;

                    bool? DLN1 = null;
                    
                    bool? OJDT = null;

                    bool? JDT1 = null;

                    bool? OINM = null;

                    try
                    {
                       
                        DataTable dtItems = CalculateQuantity(dt);

                        bool sw = VerificaQuantity(dtItems);

                        if (sw == true)
                        {
                            listSalesInvoice = CreateListSales();

                            var result2 = cn.InsertEntregaMercancia(listSalesInvoice);

                            if (result2.Item1 == 1)
                            {
                                ODLN = true; //Inserto entrega mercancia cabecera

                                var listSales = CreateListSalesLines(listSalesInvoice);

                                var result3 = cn.InsertEntregaMercanciaLines(listSales.Item1);

                                if (listSales.Item2 == result3.Item1)
                                {
                                    DLN1 = true;                                    

                                    var listItemPurchase = CreateListItem(listSalesInvoice, listSales.Item1);

                                    if (listItemPurchase.Item1 == true)
                                    {

                                        OINM = true;

                                        //Create Journal Entry

                                        listaJournalEntry = CreateJournalEntry(listSalesInvoice);

                                        //Contruir asiento

                                        var result7 = cj.InsertJournalEntry(listaJournalEntry);

                                        if (result7.Item1 == 1)
                                        {
                                            OJDT = true;

                                            var listJournalEntryLines = CreateListJournalEntryLinesItem(listaJournalEntry, listItemPurchase.Item2);

                                            var result8 = cj.InsertJournalEntryLines(listJournalEntryLines.Item1);

                                            if (listJournalEntryLines.Item2 == result8.Item1)
                                            {
                                                JDT1 = true;

                                                cn.UpdateCreditDebitAccount(dtNewJournalEntry);

                                                DataTable newDtItem = ToDataTable(ListArticuloDetalleNew);

                                                newDtItem = GroupByListItem(newDtItem);
                                               
                                                cn.UpdateItemCredit(newDtItem, Properties.Settings.Default.MainCurrency);

                                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Operacion se realizo exitosamente", Brushes.LightGreen, Brushes.Black, "001-interface.png");

                                                LimpiarCampos();

                                                ListArticuloDetalleOld.Clear();

                                                ListTransSeq.Clear();

                                                ListArticuloDetalleNew.Clear();

                                                btnCrear.Content = "OK";

                                            }
                                            else
                                            {
                                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: " + result3.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");

                                                JDT1 = false;

                                                DeletedAllInsertItem(ODLN, DLN1, OJDT, JDT1, OINM);

                                                var listArticuloDetalleOld = cn.DeleteOldRecord(ListArticuloDetalleOld);

                                                if (listArticuloDetalleOld.Item1 == ListArticuloDetalleOld.Count)
                                                {
                                                    var listArticuloDetalleNew = cn.DeleteNewRecord(ListTransSeq);

                                                    if (listArticuloDetalleNew.Item1 == ListTransSeq.Count)
                                                    {

                                                    }
                                                    else
                                                    {
                                                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al eliminar registros OINM: " + listArticuloDetalleOld.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                                                    }
                                                }
                                                else
                                                {
                                                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error actualizar registros OINM: " + listArticuloDetalleOld.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                                                }

                                                ListArticuloDetalleOld.Clear();

                                                ListTransSeq.Clear();

                                                ListArticuloDetalleNew.Clear();
                                            }

                                        }
                                        else
                                        {
                                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: " + result7.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");

                                            OJDT = false;

                                            DeletedAllInsertItem(ODLN, DLN1, OJDT, JDT1, OINM);

                                            var listArticuloDetalleOld = cn.DeleteOldRecord(ListArticuloDetalleOld);

                                            if (listArticuloDetalleOld.Item1 == ListArticuloDetalleOld.Count)
                                            {
                                                var listArticuloDetalleNew = cn.DeleteNewRecord(ListTransSeq);

                                                if (listArticuloDetalleNew.Item1 == ListTransSeq.Count)
                                                {

                                                }
                                                else
                                                {
                                                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al eliminar registros OINM: " + listArticuloDetalleOld.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                                                }
                                            }
                                            else
                                            {
                                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error actualizar registros OINM: " + listArticuloDetalleOld.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                                            }

                                            ListArticuloDetalleOld.Clear();

                                            ListTransSeq.Clear();

                                            ListArticuloDetalleNew.Clear();
                                        }


                                    }
                                    else
                                    {
                                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: ", Brushes.Red, Brushes.White, "003-interface-2.png");

                                        OINM = false;

                                        DeletedAllInsertItem(ODLN, DLN1, OJDT, JDT1, OINM);

                                        var listArticuloDetalleOld = cn.DeleteOldRecord(ListArticuloDetalleOld);

                                        if (listArticuloDetalleOld.Item1 == ListArticuloDetalleOld.Count)
                                        {
                                            var listArticuloDetalleNew = cn.DeleteNewRecord(ListTransSeq);

                                            if (listArticuloDetalleNew.Item1 == ListTransSeq.Count)
                                            {

                                            }
                                            else
                                            {
                                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al eliminar registros OINM: " + listArticuloDetalleOld.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                                            }
                                        }
                                        else
                                        {
                                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error actualizar registros OINM: " + listArticuloDetalleOld.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                                        }

                                        ListArticuloDetalleOld.Clear();

                                        ListTransSeq.Clear();

                                        ListArticuloDetalleNew.Clear();
                                    }

                                }
                                else
                                {
                                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: " + result3.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                                    DLN1 = false;
                                    DeletedAllInsertItem(ODLN, DLN1, OJDT, JDT1, OINM);

                                }
                            }
                            else
                            {
                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error en la creacion de la factura: " + result2.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                                ODLN = false;
                                DeletedAllInsertItem(ODLN, DLN1, OJDT, JDT1, OINM);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error General: " + ex.Message, Brushes.Red, Brushes.White, "003-interface-2.png");

                        if (OINM != null)
                        {

                            DeletedAllInsertItem(ODLN, DLN1, OJDT, JDT1, OINM);

                            var listArticuloDetalleOld = cn.DeleteOldRecord(ListArticuloDetalleOld);

                            if (listArticuloDetalleOld.Item1 == ListArticuloDetalleOld.Count)
                            {
                                var listArticuloDetalleNew = cn.DeleteNewRecord(ListTransSeq);

                                if (listArticuloDetalleNew.Item1 == ListTransSeq.Count)
                                {

                                }
                                else
                                {
                                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al eliminar registros OINM: " + listArticuloDetalleOld.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                                }
                            }
                            else
                            {
                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error actualizar registros OINM: " + listArticuloDetalleOld.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                            }

                            ListArticuloDetalleOld.Clear();

                            ListTransSeq.Clear();

                            ListArticuloDetalleNew.Clear();

                        }
                        else
                        {
                            DeletedAllInsertItem(ODLN, DLN1, OJDT, JDT1, OINM);
                        }

                    }


                    break;

                case "Actualizar":

                    SalesInvoice.DocEntry = DocEntry;
                    SalesInvoice.DocNum = DocNum;
                    SalesInvoice.TaxDate = dpDocumento.SelectedDate;
                    SalesInvoice.DocDueDate = dpVencimiento.SelectedDate;
                    SalesInvoice.NumAtCard = txtNroFactura.Text;                   
                    SalesInvoice.Comments = txtComentario.Text;
                    SalesInvoice.JrnlMemo = txtEntradaDiario.Text;                   
                    SalesInvoice.UserSign = Properties.Settings.Default.Usuario;
                    SalesInvoice.UpdateDate = fechaActual.GetFechaActual();
                    SalesInvoice.DocSubType = "--";

                    listSalesInvoice.Add(SalesInvoice);

                    var result4 = cn.UpdateEntregaMercancia(listSalesInvoice);

                    if (result4.Item2 == 1)
                    {
                        LimpiarCampos();

                        btnCrear.Content = "OK";

                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Operacion se realizo exitosamente", Brushes.LightGreen, Brushes.Black, "001-interface.png");
                        

                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage(result4.Item1, Brushes.Red, Brushes.White, "003-interface-2.png");

                    }

                    break;
            }
        }

        private void VerifiedDatatable(DataTable newDtItem)
        {
            foreach (DataRow row in newDtItem.Rows)
            {
                foreach(DataColumn column in newDtItem.Columns)
                {
                    var value = row[column].ToString();
                }
            }
        }

        private Tuple<List<AsientoDetalle>, int> CreateListJournalEntryLinesItem(List<AsientoCabecera> listaJournalEntry, List<ArticuloDetalle> listItem)
        {
            dtNewJournalEntry = CreateDatatableJournalEntryLinesItem(dt, listItem);

            // dtNewJournalEntry=PrepareJournalEntryLines(dtNewJournalEntry);

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


            List<AsientoDetalle> listaJournalEntryLines = new List<AsientoDetalle>();

            int k = 0;

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

        private DataTable CreateDatatableJournalEntryLinesItem(DataTable dt, List<ArticuloDetalle> listItem)
        {
            dtJournalEntry.Rows.Clear();

            //Create costo de venta de articulo

            foreach (ArticuloDetalle articuloDetalle in listItem)
            {
                DataRow newRow4 = dtJournalEntry.NewRow();

                dtJournalEntry.Rows.Add(newRow4);

                var SalesCostAct = cn.FindSalesCostAct(articuloDetalle.ItemCode);

                newRow4["ShortName"] = articuloDetalle.InvntAct;

                newRow4["Account"] = articuloDetalle.InvntAct;

                newRow4["ContraAct"] = SalesCostAct.Item1;

                newRow4["Credit"] = articuloDetalle.CalcPrice;

                if (articuloDetalle.Currency == Properties.Settings.Default.MainCurrency)
                {
                    newRow4["FCCredit"] = 0;
                    newRow4["SYSCred"] = ConvertDecimalTwoPlaces(newRow4["Credit"]) / articuloDetalle.SysRate;
                }
                else
                {
                    newRow4["FCCredit"] = ConvertDecimalTwoPlaces(newRow4["Credit"]) * articuloDetalle.Rate;
                    newRow4["SYSCred"] = ConvertDecimalTwoPlaces(newRow4["Credit"]) / articuloDetalle.SysRate;
                }


                newRow4["LineMemo"] = txtEntradaDiario.Text;

                //------------------------------------------------------------------------

                DataRow newRow5 = dtJournalEntry.NewRow();

                dtJournalEntry.Rows.Add(newRow5);
             
                newRow5["ShortName"] = SalesCostAct.Item1;

                newRow5["Account"] = SalesCostAct.Item1;

                newRow5["ContraAct"] = articuloDetalle.InvntAct;

                newRow5["Debit"] = ConvertDecimalTwoPlaces(newRow4["Credit"]);

                newRow5["FCDebit"] = ConvertDecimalTwoPlaces(newRow4["FCCredit"]);

                newRow5["SYSDeb"] = ConvertDecimalTwoPlaces(newRow4["SYSCred"]);

                newRow5["LineMemo"] = txtEntradaDiario.Text;


            }

            return dtJournalEntry;
        }

        private List<AsientoCabecera> CreateJournalEntry(List<DocumentoCabecera> listSalesInvoice)
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


            foreach (DocumentoCabecera SalesInvoice in listSalesInvoice)
            {
                TransId = SalesInvoice.TransId;
                DocCurr = SalesInvoice.DocCurr;
                DocDate = SalesInvoice.DocDate;
                DocDueDate = SalesInvoice.DocDueDate;
                TaxDate = SalesInvoice.TaxDate;
                FinncPriod = SalesInvoice.FinncPriod;
                ObjType = SalesInvoice.ObjType;
                JrnlMemo = SalesInvoice.JrnlMemo;
                DocTotal = SalesInvoice.DocTotal;
                DocTotalFC = SalesInvoice.DocTotalFC;
                DocTotalSy = SalesInvoice.DocTotalSy;

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

        private Tuple<List<DocumentoDetalle>, int> CreateListSalesLines(List<DocumentoCabecera> listSalesInvoice)
        {
            int DocNum = 0;

            string DocCurr = null;

            DateTime? DocDate = null;

            string CardCode = null;

            int FinncPriod = 0;

            string ObjType = null;

            string Address2 = null;

            char InvntSttus = 'S';

            foreach (DocumentoCabecera SalesInvoice in listSalesInvoice)
            {
                DocNum = SalesInvoice.DocNum;
                DocCurr = SalesInvoice.DocCurr;
                DocDate = SalesInvoice.DocDate;
                CardCode = SalesInvoice.CardCode;
                FinncPriod = SalesInvoice.FinncPriod;
                ObjType = SalesInvoice.ObjType;
                Address2 = SalesInvoice.Address2;
                InvntSttus = SalesInvoice.InvntSttus;

            }

            List<DocumentoDetalle> listSalesInvoiceLines = new List<DocumentoDetalle>();

            int i = 0;

            foreach (DataRow row in dt.Rows)
            {
                DocumentoDetalle SalesInvoiceLines = new DocumentoDetalle();

                var result3 = cn.FindDocEntry(DocNum);
                DocEntryDeleted = result3.Item1;
                SalesInvoiceLines.DocEntry = result3.Item1;
                SalesInvoiceLines.LineNum = i;
                SalesInvoiceLines.LineStatus = 'C';
                SalesInvoiceLines.ItemCode = FindItemCode(row["ItemCode"].ToString());
                SalesInvoiceLines.Dscription = row["Dscription"].ToString();
                SalesInvoiceLines.NumPerMsr = ConvertDecimalTwoPlaces(row["NumPerMsr"]);
                SalesInvoiceLines.NumPerMsr2 = ConvertDecimalTwoPlaces(row["NumPerMsr2"]);
                SalesInvoiceLines.UomEntry = row["UomEntry"] == null || row["UomEntry"].ToString() == "" ? 0 : Convert.ToInt32(row["UomEntry"]);
                SalesInvoiceLines.UomEntry2 = row["UomEntry2"] == null || row["UomEntry2"].ToString() == "" ? 0 : Convert.ToInt32(row["UomEntry2"]);
                SalesInvoiceLines.UomCode = row["UomCode"] == null || row["UomCode"].ToString() == "" ? null : row["UomCode"].ToString();
                SalesInvoiceLines.UomCode2 = row["UomCode2"] == null || row["UomCode2"].ToString() == "" ? null : row["UomCode2"].ToString();
                SalesInvoiceLines.unitMsr = row["unitMsr"] == null || row["unitMsr"].ToString() == "" ? null : row["unitMsr"].ToString();
                SalesInvoiceLines.unitMsr2 = row["unitMsr2"] == null || row["unitMsr2"].ToString() == "" ? null : row["unitMsr2"].ToString();
                SalesInvoiceLines.Quantity = ConvertDecimalTwoPlaces(row["Quantity"].ToString());
                SalesInvoiceLines.OpenQty = SalesInvoiceLines.Quantity;
                SalesInvoiceLines.InvQty = SalesInvoiceLines.Quantity * SalesInvoiceLines.NumPerMsr;
                SalesInvoiceLines.OpenInvQty = SalesInvoiceLines.Quantity * SalesInvoiceLines.NumPerMsr;
                str = regex.Replace(row["LineTotal"].ToString(), String.Empty);
                SalesInvoiceLines.Price = ConvertDecimalTwoPlaces(str);
                SalesInvoiceLines.Currency = DocCurr;
                SalesInvoiceLines.LineTotal = ConvertDecimalTwoPlaces(str);
                SalesInvoiceLines.AcctCode = row["AcctCode"].ToString();
                SalesInvoiceLines.DocDate = DocDate;
                SalesInvoiceLines.BaseCard = CardCode;
                str = regex.Replace(row["TotalSumSy"].ToString(), String.Empty);
                SalesInvoiceLines.TotalSumSy = ConvertDecimalTwoPlaces(str);
                str = regex.Replace(row["TotalFrgn"].ToString(), String.Empty);
                SalesInvoiceLines.TotalFrgn = ConvertDecimalTwoPlaces(str);
                SalesInvoiceLines.VatSum = ConvertDecimalTwoPlaces(row["VatSum"].ToString());
                SalesInvoiceLines.VatSumFrgn = ConvertDecimalTwoPlaces(row["VatSumFrgn"].ToString());
                SalesInvoiceLines.VatSumSy = ConvertDecimalTwoPlaces(row["VatSumSy"].ToString());
                SalesInvoiceLines.VatGroup = row["VatGroup"].ToString();
                SalesInvoiceLines.VatPrcnt = ConvertDecimalTwoPlaces(row["VatPrcnt"].ToString());
                SalesInvoiceLines.FinncPriod = FinncPriod;
                SalesInvoiceLines.ObjType = ObjType;
                SalesInvoiceLines.Address = Address2;
                SalesInvoiceLines.Gtotal = ConvertDecimalTwoPlaces(row["GTotal"].ToString());
                SalesInvoiceLines.GtotalFC = ConvertDecimalTwoPlaces(row["GTotalFC"].ToString());
                SalesInvoiceLines.GtotalSC = ConvertDecimalTwoPlaces(row["GTotalSC"].ToString());               
                SalesInvoiceLines.StockSum = GetItemOfService(dt, SalesInvoiceLines.LineTotal);               
                SalesInvoiceLines.StockSumFc = GetItemOfService(dt, SalesInvoiceLines.TotalFrgn);              
                SalesInvoiceLines.StockSumSc = GetItemOfService(dt, SalesInvoiceLines.TotalSumSy);
                SalesInvoiceLines.InvntSttus = InvntSttus;
                SalesInvoiceLines.WtLiable = cn.GetWTLiable(cbSujetoRetencionItem.SelectedValuePath.ToString());
                SalesInvoiceLines.DataSource = 'N';
                SalesInvoiceLines.IsTax = cn.TraduceChar(row["IsTax"] == null || row["IsTax"].ToString() == "" ? false : Convert.ToBoolean(row["IsTax"]));
                SalesInvoiceLines.StartValue = cn.TraduceChar(row["StartValue"] == null || row["StartValue"].ToString() == "" ? false : Convert.ToBoolean(row["StartValue"]));

                listSalesInvoiceLines.Add(SalesInvoiceLines);

                i++;

            }

            return Tuple.Create(listSalesInvoiceLines, i);
        }

        private bool VerificaQuantity(DataTable dt)
        {
            bool sw = true;

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() == "ItemCode" && sw == true)
                    {
                        if (String.IsNullOrWhiteSpace(row["ItemCode"].ToString()) == false)
                        {
                            var quantity = cn.FindQuantityItem(row["ItemCode"].ToString());

                            if (quantity.Item2 == null)
                            {
                                if (Convert.ToInt32(row["Quantity"]) <= quantity.Item1)
                                {

                                }
                                else
                                {
                                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Inventario recae en una cantidad negativa: " + row["ItemCode"].ToString(), Brushes.Red, Brushes.White, "003-interface-2.png");

                                    sw = false;
                                }
                            }
                            else
                            {
                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage(quantity.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");

                                sw = false;
                            }
                        }
                    }
                }
            }

            return sw;
        }

        private void DeletedAllInsertItem(bool? ODLN, bool? DLN1, bool? oJDT, bool? jDT1, bool? oINM)
        {
            if (DLN1 != null)
            {
                var deleteSalesInvoiceLines = cn.DeleteEntregaMercanciaLines(DocEntryDeleted);

                if (deleteSalesInvoiceLines.Item2 != null)
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se eliminaron todas las lineas de la entrega de mercancia : " + DocEntryDeleted, Brushes.LightBlue, Brushes.Black, "003-interface-2.png");
                }
            }

            if (ODLN == true)
            {
                var deleteSalesInvoice = cn.DeleteEntregaMercancia(DocEntryDeleted);

                if (deleteSalesInvoice.Item2 != null)
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se elimino la entrega de mercancia : " + DocNumDeleted, Brushes.LightBlue, Brushes.Black, "003-interface-2.png");
                }
            }


            if (oINM != null)
            {
                var deleteOINM = cn.DeleteOINM(DocEntryDeleted);

                if (deleteOINM.Item2 != null)
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se eliminaron todos los registros de ventas asociados a la entrega de mercancia : " + DocEntryDeleted, Brushes.LightBlue, Brushes.Black, "003-interface-2.png");
                }
            }

            if (jDT1 != null)
            {
                var deleteJournalEntryLinesSalesInvoice = cn.DeleteJournalEntryLines(TransId);

                if (deleteJournalEntryLinesSalesInvoice.Item2 != null)
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se eliminaron todas las lineas del asiento asociado a la entrega de mercancia : " + DocEntryDeleted, Brushes.LightBlue, Brushes.Black, "003-interface-2.png");
                }
            }

            if (oJDT == true)
            {
                var deleteJournalEntrySalesInvoice = cn.DeleteJournalEntry(TransId);

                if (deleteJournalEntrySalesInvoice.Item2 != null)
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se elimino el asiento asociado a la entrega de mercancia : " + DocEntryDeleted, Brushes.LightBlue, Brushes.Black, "003-interface-2.png");
                }
            }

        }


        private decimal GetItemOfService(DataTable dt, decimal lineTotal)
        {
            decimal StockSum = 0;

            if (dt.TableName.ToString() == "dtArticulo")
            {
                StockSum = lineTotal;
            }

            return StockSum;

        }

        private bool? IdentifyDatatable()
        {
            bool? sw = null;

            if (dt.TableName.ToString() == "dtServicio")
            {
                sw = true;
            }
            else if (dt.TableName.ToString() == "dtArticulo")
            {
                sw = false;
            }

            return sw;
        }

        private string FindItemCode(string itemCode)
        {
            string str = null;

            if (String.IsNullOrWhiteSpace(itemCode.ToString()) == false)
            {
                str = itemCode;
            }
            else
            {
                str = "";
            }

            return str;
        }


        private void RecorreListaPurchaseInvoice(List<DocumentoCabecera> newListPurchase)
        {
            if (newListPurchase.Count == 1)
            {
                ReestablecerFondo();

                GetEntregaMercancia(newListPurchase);

                var result = cn.FindEntregaMercanciaLines(DocEntry);

                if (result.Item2 == null)
                {
                    GetEntregaMercanciaLines(result.Item1);

                    btnCrear.Content = "OK";
                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "002-interface-1.png");
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
                ListaFacturaProveedores ventanaListBox = new ListaFacturaProveedores(newListPurchase);

                ventanaListBox.ShowDialog();

                if (ventanaListBox.Visibility == Visibility.Hidden)
                {
                    if (ventanaListBox.GetListDocument().Count == 0)
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No selecciono ningun elemento", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");

                        ReestablecerFondo();

                        LimpiarCampos();

                        btnCrear.Content = "OK";

                    }
                    else
                    {
                        ReestablecerFondo();

                        GetEntregaMercancia(ventanaListBox.GetListDocument());

                        var result = cn.FindEntregaMercanciaLines(DocEntry);

                        if (result.Item2 == null)
                        {
                            GetEntregaMercanciaLines(result.Item1);

                            btnCrear.Content = "OK";
                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "002-interface-1.png");
                        }

                    }

                    btnCrear.Content = "OK";
                }


            }

            //ReestablecerFondo();
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
            var dataSetPreliminar = cn.CreateDataSetPreliminar();

            List<DocumentoCabecera> listSalesInvoice = new List<DocumentoCabecera>();

            DocumentoCabecera SalesInvoice = new DocumentoCabecera();

            List<DocumentoDetalle> listSalesInvoiceLines = new List<DocumentoDetalle>();

            List<Entidades.TablaRetencionImpuesto> listTablaRetenciones = new List<Entidades.TablaRetencionImpuesto>();

            List<AsientoCabecera> listaJournalEntry = new List<AsientoCabecera>();

            AsientoCabecera journalEntry = new AsientoCabecera();

            List<AsientoDetalle> listaJournalEntryLines = new List<AsientoDetalle>();

            bool sw = false;

            if (dataSetPreliminar == null)
            {
                 if (cbClase.SelectedValue.ToString() == "Articulo")
                 {
                    try
                    {
                      
                        DataTable dtItems = CalculateQuantity(dt);

                        bool swQuantity = VerificaQuantity(dtItems);

                        if (swQuantity == true)
                        {
                            listSalesInvoice = CreateListSales();

                            var result2 = cn.InsertEntregaMercanciaPreliminar(listSalesInvoice);

                            if (result2.Item1 == 1)
                            {

                                var listSales = CreateListSalesLinesPreliminar(listSalesInvoice);

                                var result4 = cn.InsertEntregaMercanciaLinesPreliminar(listSales.Item1);

                                if (listSales.Item2 == result4.Item1)
                                {

                                    var listItemPurchase = CreateListItemPreliminar(listSalesInvoice, listSales.Item1);

                                    if (listItemPurchase.Item1 == true)
                                    {

                                        //Create Journal Entry

                                        listaJournalEntry = CreateJournalEntry(listSalesInvoice);

                                        //Contruir asiento

                                        var result7 = cj.InsertJournalEntryPreliminar(listaJournalEntry);

                                        if (result7.Item1 == 1)
                                        {

                                            var listJournalEntryLines = CreateListJournalEntryLinesItem(listaJournalEntry, listItemPurchase.Item2);


                                            listaJournalEntryLines = listJournalEntryLines.Item1;

                                            var result3 = cj.InsertJournalEntryLinesPreliminar(listJournalEntryLines.Item1);

                                            if (listJournalEntryLines.Item2 == result3.Item1)
                                            {
                                                sw = true;

                                                ListArticuloDetalleOld.Clear();

                                                ListTransSeq.Clear();

                                                ListArticuloDetalleNew.Clear();

                                            }
                                            else
                                            {
                                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: " + result3.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");

                                                sw = false;

                                                ListArticuloDetalleOld.Clear();

                                                ListTransSeq.Clear();

                                                ListArticuloDetalleNew.Clear();
                                            }

                                        }
                                        else
                                        {
                                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: " + result7.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");

                                            sw = false;

                                            ListArticuloDetalleOld.Clear();

                                            ListTransSeq.Clear();

                                            ListArticuloDetalleNew.Clear();
                                        }


                                    }
                                    else
                                    {
                                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: ", Brushes.Red, Brushes.White, "003-interface-2.png");

                                        sw = false;

                                        ListArticuloDetalleOld.Clear();

                                        ListTransSeq.Clear();

                                        ListArticuloDetalleNew.Clear();
                                    }

                                }
                                else
                                {
                                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: " + result4.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                                    sw = false;



                                }
                            }
                            else
                            {
                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error en la creacion de la factura: " + result2.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                                sw = false;


                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error General: " + ex.Message, Brushes.Red, Brushes.White, "003-interface-2.png");

                        ListArticuloDetalleOld.Clear();

                        ListTransSeq.Clear();

                        ListArticuloDetalleNew.Clear();

                        sw = false;


                    }
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

        private DataTable ChangeNameColumnDatatable(DataTable dt)
        {
            dt.Columns["SysCred"].ColumnName = "SYSCred";

            dt.Columns["SysDeb"].ColumnName = "SYSDeb";

            return dt;
        }

        private void imgTaxCodeME_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var row_list = GetDataGridRows(dg);

                DataRowView row_Selected = dg.SelectedItem as DataRowView;

                foreach (DataGridRow single_row in row_list)
                {
                    if (single_row.IsSelected == true)
                    {
                        TextBox txtTaxCode = FindChild<TextBox>(single_row, "txtTaxCodeME");

                        var result = cs.ConsultaCodigosFiscales();

                        if (result.Item2 == null)
                        {
                            RecorreListaTaxCode(result.Item1, txtTaxCode, row_Selected);
                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
                        }

                    }
                }
            }

            catch (Exception ex)
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error" + ex.Message, Brushes.Red, Brushes.Black, "003-interface-2.png");
            }
        }

        private void imgTaxCodeItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var row_list = GetDataGridRows(dg);

                DataRowView row_Selected = dg.SelectedItem as DataRowView;

                foreach (DataGridRow single_row in row_list)
                {
                    if (single_row.IsSelected == true)
                    {
                        TextBox txtTaxCodeItem = FindChild<TextBox>(single_row, "txtTaxCodeItem");

                        var result = cs.ConsultaCodigosFiscales();

                        if (result.Item2 == null)
                        {
                            RecorreListaTaxCode(result.Item1, txtTaxCodeItem, row_Selected);
                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error" + ex.Message, Brushes.Red, Brushes.Black, "003-interface-2.png");
            }
        }



        private void txtLineTotal_LostFocus(object sender, RoutedEventArgs e)
        {

            string str = null;

            decimal totalDocumento = 0;

            decimal subtotal = 0;

            decimal iva = 0;

            decimal retenciones = 0;

            TextBox textBox = (TextBox)sender;

            if (textBox.Text != "")
            {
                textBox.Text = RemoveCurrency(textBox.Text);

                textBox.Text = AddCurrency(textBox.Text);

                this.dg.CommitEdit();

                this.dg.CommitEdit();

                subtotal = CalculateSubTotal(dt);

                txtTotalAntesDescuento.Text = Currency + " " + subtotal.ToString();

                str = regex.Replace(txtTotalAntesDescuento.Text, String.Empty);

                totalDocumento = ConvertDecimalTwoPlaces(str);

                str = regex.Replace(txtImpuesto.Text, String.Empty);

                iva = ConvertDecimalTwoPlaces(str);

                str = regex.Replace(txtRetenciones.Text, String.Empty);

                retenciones = ConvertDecimalTwoPlaces(str);

                totalDocumento = subtotal + iva - retenciones;

                txtTotalDocumento.Text = Currency + " " + totalDocumento.ToString();

            }

        }



        private string AddCurrency(string LineTotal)
        {
            string LineTotalWithCurrency = null;

            LineTotalWithCurrency = Currency + " " + LineTotal;

            return LineTotalWithCurrency;
        }

        private string RemoveCurrency(string LineTotal)
        {
            string LineTotalWithOutCurrency = null;

            LineTotalWithOutCurrency = regex.Replace(LineTotal, String.Empty);

            return LineTotalWithOutCurrency;
        }

        private decimal CalculateSubTotal(DataTable dtServicio)
        {
            decimal subtotal = 0;

            string str = null;

            decimal amount = 0;

            decimal amountSy = 0;

            decimal amountFC = 0;

            decimal tax = 0;

            decimal taxFC = 0;

            decimal taxSy = 0;

            decimal gtotal = 0;

            decimal gtotalSC = 0;

            decimal gtotalFC = 0;

            foreach (DataRow row in dtServicio.Rows)
            {
                foreach (DataColumn column in dtServicio.Columns)
                {
                    if (column.ToString() == "LineTotal")
                    {
                        if (String.IsNullOrWhiteSpace(row["LineTotal"].ToString()) == false)
                        {
                            row["TotalFrgn"] = amountFC;

                            row["VatSumFrgn"] = taxFC;

                            str = regex.Replace(row["LineTotal"].ToString(), String.Empty);

                            amount = ConvertDecimalTwoPlaces(str);

                            row["TotalSumSy"] = amount / Rate;

                            row["TotalSumSy"] = ConvertDecimalTwoPlaces(row["TotalSumSy"]);

                            amountSy = ConvertDecimalTwoPlaces(row["TotalSumSy"]);

                            taxSy = ConvertDecimalTwoPlaces(row["VatSumSy"]);

                            tax = ConvertDecimalTwoPlaces(row["VatSum"]);

                            row["GTotal"] = amount + tax;

                            row["GTotalSC"] = amountSy + taxSy;

                            row["GTotalFC"] = amountFC + taxFC;

                            subtotal = amount + subtotal;

                            subtotal = ConvertDecimalTwoPlaces(subtotal);
                        }
                    }
                }
            }

            return subtotal;
        }

        private void txtTaxCode_LostFocus(object sender, RoutedEventArgs e)
        {
            string str = null;

            decimal totalDocumento = 0;

            decimal subTotal = 0;

            decimal iva = 0;

            decimal retenciones = 0;

            decimal impuesto = 0;

            TextBox textBox = (TextBox)sender;

            if (textBox.Text != "")
            {

                this.dg.CommitEdit();

                this.dg.CommitEdit();

                if (Currency == Properties.Settings.Default.MainCurrency)
                {
                    impuesto = CalculateTax(dt);
                }
                else
                {
                    impuesto = CalculateTaxFC(dt);
                }

                txtImpuesto.Text = Currency + " " + impuesto.ToString();

                str = regex.Replace(txtTotalAntesDescuento.Text, String.Empty);

                subTotal = ConvertDecimalTwoPlaces(str);

                str = regex.Replace(txtImpuesto.Text, String.Empty);

                iva = ConvertDecimalTwoPlaces(str);

                str = regex.Replace(txtRetenciones.Text, String.Empty);

                retenciones = ConvertDecimalTwoPlaces(str);

                totalDocumento = subTotal + iva - retenciones;

                txtTotalDocumento.Text = Currency + " " + totalDocumento.ToString();
            }
        }

        private void txtTaxCodeItem_LostFocus(object sender, RoutedEventArgs e)
        {
            string str = null;

            decimal totalDocumento = 0;

            decimal subTotal = 0;

            decimal iva = 0;

            decimal retenciones = 0;

            decimal impuesto = 0;

            TextBox textBox = (TextBox)sender;

            if (textBox.Text != "")
            {
                this.dg.CommitEdit();

                this.dg.CommitEdit();

                if (Currency == Properties.Settings.Default.MainCurrency)
                {
                    impuesto = CalculateTax(dt);
                }
                else
                {
                    impuesto = CalculateTaxFC(dt);
                }

                txtImpuesto.Text = Currency + " " + impuesto.ToString();

                str = regex.Replace(txtTotalAntesDescuento.Text, String.Empty);

                subTotal = ConvertDecimalTwoPlaces(str);

                str = regex.Replace(txtImpuesto.Text, String.Empty);

                iva = ConvertDecimalTwoPlaces(str);

                str = regex.Replace(txtRetenciones.Text, String.Empty);

                retenciones = ConvertDecimalTwoPlaces(str);

                totalDocumento = subTotal + iva - retenciones;

                txtTotalDocumento.Text = Currency + " " + totalDocumento.ToString();
            }
        }

        private void txtLineTotal_LostFocus_1(object sender, RoutedEventArgs e)
        {
            string str = null;

            decimal totalDocumento = 0;

            decimal subtotal = 0;

            decimal iva = 0;

            decimal retenciones = 0;

            TextBox textBox = (TextBox)sender;

            if (textBox.Text != "")
            {
                textBox.Text = RemoveCurrency(textBox.Text);

                textBox.Text = AddCurrency(textBox.Text);

                this.dg.CommitEdit();

                this.dg.CommitEdit();

                subtotal = CalculateSubTotal(dt);

                txtTotalAntesDescuento.Text = Currency + " " + subtotal.ToString();

                str = regex.Replace(txtTotalAntesDescuento.Text, String.Empty);

                totalDocumento = ConvertDecimalTwoPlaces(str);

                str = regex.Replace(txtImpuesto.Text, String.Empty);

                iva = ConvertDecimalTwoPlaces(str);

                str = regex.Replace(txtRetenciones.Text, String.Empty);

                retenciones = ConvertDecimalTwoPlaces(str);

                totalDocumento = subtotal + iva - retenciones;

                txtTotalDocumento.Text = Currency + " " + totalDocumento.ToString();
            }
        }


        private void txtTotalFrgn_LostFocus(object sender, RoutedEventArgs e)
        {
            decimal subtotalFC = 0;

            TextBox textBox = (TextBox)sender;

            if (textBox.Text != "")
            {

                textBox.Text = RemoveCurrency(textBox.Text);

                textBox.Text = AddCurrency(textBox.Text);

                this.dg.CommitEdit();

                this.dg.CommitEdit();

                subtotalFC = CalculateSubTotalFC(dt);

                txtTotalAntesDescuento.Text = Currency + " " + subtotalFC.ToString();
            }

        }

        private decimal CalculateSubTotalFC(DataTable dtServicio)
        {
            decimal subtotalFC = 0;

            string str = null;

            decimal amountFC = 0;

            decimal amount = 0;

            decimal amountSy = 0;

            decimal tax = 0;

            decimal taxFC = 0;

            decimal taxSy = 0;

            decimal gtotal = 0;

            decimal gtotalSC = 0;

            decimal gtotalFC = 0;

            foreach (DataRow row in dtServicio.Rows)
            {
                foreach (DataColumn column in dtServicio.Columns)
                {
                    if (column.ToString() == "TotalFrgn")
                    {
                        if (String.IsNullOrWhiteSpace(row["TotalFrgn"].ToString()) == false)
                        {
                            FindRateFC();

                            if (RateFC == 0)
                            {
                                ShowTipoCambio();
                            }
                            else
                            {

                                str = regex.Replace(row["TotalFrgn"].ToString(), String.Empty);

                                amountFC = ConvertDecimalTwoPlaces(str);

                                row["LineTotal"] = amountFC * RateFC;

                                row["LineTotal"] = ConvertDecimalTwoPlaces(row["LineTotal"]);

                                amount = ConvertDecimalTwoPlaces(row["LineTotal"]);

                                row["TotalSumSy"] = amount / Rate;

                                amountSy = ConvertDecimalTwoPlaces(row["TotalSumSy"]);

                                tax = ConvertDecimalTwoPlaces(row["VatSum"]);

                                taxFC = ConvertDecimalTwoPlaces(row["VatSumFrgn"]);

                                taxSy = ConvertDecimalTwoPlaces(row["VatSumSy"]);

                                gtotal = amount + tax;

                                row["GTotal"] = ConvertDecimalTwoPlaces(gtotal);

                                gtotalSC = amountSy + taxSy;

                                row["GTotalSC"] = ConvertDecimalTwoPlaces(gtotalSC);

                                gtotalFC = amountFC + taxFC;

                                row["GTotalFC"] = ConvertDecimalTwoPlaces(gtotalFC);

                                subtotalFC = amountFC + subtotalFC;

                                subtotalFC = ConvertDecimalTwoPlaces(subtotalFC);
                            }
                        }
                    }

                }

            }

            return subtotalFC;
        }

        private void txtImpuesto_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private decimal CalculateTaxFC(DataTable dtServicio)
        {
            decimal impuestoFC = 0;

            decimal impuesto = 0;

            string str = null;

            decimal amountFC = 0;

            decimal vatSumFrgn = 0;

            decimal VatPrcnt = 0;

            decimal vatsum = 0;

            decimal vatsumSy = 0;

            decimal amount = 0;

            decimal amountSy = 0;

            foreach (DataRow row in dtServicio.Rows)
            {
                foreach (DataColumn column in dtServicio.Columns)
                {
                    if (column.ToString() == "VatSumFrgn")
                    {
                        if (String.IsNullOrWhiteSpace(row["TotalFrgn"].ToString()) == false)
                        {
                            str = regex.Replace(row["TotalFrgn"].ToString(), String.Empty);

                            amountFC = ConvertDecimalTwoPlaces(str);

                            str = regex.Replace(row["LineTotal"].ToString(), String.Empty);

                            amount = ConvertDecimalTwoPlaces(str);

                            str = regex.Replace(row["TotalSumSy"].ToString(), String.Empty);

                            amountSy = ConvertDecimalTwoPlaces(str);

                            VatPrcnt = ConvertDecimalTwoPlaces(row["VatPrcnt"]);

                            vatSumFrgn = amountFC * (VatPrcnt / 100);

                            vatSumFrgn = ConvertDecimalTwoPlaces(vatSumFrgn);

                            row["VatSumFrgn"] = vatSumFrgn;

                            row["GTotalFC"] = amountFC + vatSumFrgn;

                            row["GTotalFC"] = ConvertDecimalTwoPlaces(row["GTotalFC"]);

                            row["VatSum"] = vatSumFrgn * RateFC;

                            row["VatSum"] = ConvertDecimalTwoPlaces(row["VatSum"]);

                            vatsum = ConvertDecimalTwoPlaces(row["VatSum"]);

                            impuesto = ConvertDecimalTwoPlaces(row["VatSum"]);

                            row["GTotal"] = amount + vatsum;

                            row["GTotal"] = ConvertDecimalTwoPlaces(row["GTotal"]);

                            row["VatSumSy"] = impuesto / Rate;

                            vatsumSy = ConvertDecimalTwoPlaces(row["VatSumSy"]);

                            row["GTotalSC"] = amountSy + vatsumSy;

                            row["GTotalSC"] = ConvertDecimalTwoPlaces(row["GTotalSC"]);

                            impuestoFC = vatSumFrgn + impuestoFC;

                            impuestoFC = ConvertDecimalTwoPlaces(impuestoFC);

                        }
                    }

                }

            }

            return impuestoFC;
        }



        private decimal CalculateTax(DataTable dtServicio)
        {
            decimal impuesto = 0;

            decimal VatPrcnt = 0;

            decimal vatsum = 0;

            decimal vatsumSy = 0;

            string str = null;

            decimal amount = 0;

            decimal amountSy = 0;

            foreach (DataRow row in dtServicio.Rows)
            {
                foreach (DataColumn column in dtServicio.Columns)
                {
                    if (column.ToString() == "VatSum")
                    {
                        if (String.IsNullOrWhiteSpace(row["LineTotal"].ToString()) == false)
                        {
                            str = regex.Replace(row["LineTotal"].ToString(), String.Empty);

                            amount = ConvertDecimalTwoPlaces(str);

                            str = regex.Replace(row["TotalSumSy"].ToString(), String.Empty);

                            amountSy = ConvertDecimalTwoPlaces(str);

                            VatPrcnt = ConvertDecimalTwoPlaces(row["VatPrcnt"]);

                            vatsum = amount * (VatPrcnt / 100);

                            vatsum = ConvertDecimalTwoPlaces(vatsum);

                            row["VatSum"] = vatsum;

                            row["GTotal"] = amount + vatsum;

                            row["GTotal"] = ConvertDecimalTwoPlaces(row["GTotal"]);

                            row["VatSumFrgn"] = 0;

                            row["GTotalFC"] = 0;

                            row["VatSumSy"] = vatsum / Rate;

                            vatsumSy = ConvertDecimalTwoPlaces(row["VatSumSy"]);

                            row["VatSumSy"] = ConvertDecimalTwoPlaces(row["VatSumSy"]);

                            row["GTotalSC"] = amountSy + vatsumSy;

                            row["GTotalSC"] = ConvertDecimalTwoPlaces(row["GTotalSC"]);

                            impuesto = vatsum + impuesto;

                            impuesto = ConvertDecimalTwoPlaces(impuesto);
                        }
                    }

                }

            }

            return impuesto;
        }

        private void txtNroFactura_KeyDown(object sender, KeyEventArgs e)
        {
            if (SwReadOnly == true && btnCrear.Content.ToString() == "OK")
            {
                btnCrear.Content = "Actualizar";
            }
        }

        private void dpVencimiento_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SwReadOnly == true && btnCrear.Content.ToString() == "OK")
            {
                btnCrear.Content = "Actualizar";
            }
        }


        private void txtNroFactura_KeyUp(object sender, KeyEventArgs e)
        {
        }

       
        private void txtUomCode_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void imgUomCode_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var row_list = GetDataGridRows(dgArticulo);

                DataRowView row_Selected = dgArticulo.CurrentItem as DataRowView;

                foreach (DataGridRow single_row in row_list)
                {
                    if (single_row.IsEditing == true)
                    {
                        TextBox txtUomCode = FindChild<TextBox>(single_row, "txtUomCode");

                        TextBlock txtNumPerMsr = FindChild<TextBlock>(single_row, "txtNumPerMsr");

                        var result = cu.ConsultaDefinicionUnidadesMedidaSpecific(Convert.ToInt32(row_Selected["UgpEntry"]));

                        if (result.Item2 == null)
                        {
                            RecorreListaUnidadesMedida(result.Item1, txtUomCode, txtNumPerMsr, row_Selected);
                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error" + ex.Message, Brushes.Red, Brushes.Black, "003-interface-2.png");
            }
        }

        private void RecorreListaUnidadesMedida(List<UnidadesMedida> listUnidadesMedidaResultante, System.Windows.Controls.TextBox txtUomCode, TextBlock txtNumPerMsr, DataRowView row_Selected)
        {
            if (listUnidadesMedidaResultante.Count == 0)
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se encontro ningun resultado con los parametros establecidos", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");
            }

            else if (listUnidadesMedidaResultante.Count > 0)
            {
                ListaUnidadesMedida ventanaListaUnidadesMedidas = new ListaUnidadesMedida(listUnidadesMedidaResultante);

                ventanaListaUnidadesMedidas.ShowDialog();

                if (ventanaListaUnidadesMedidas.Visibility == Visibility.Hidden)
                {
                    if (ventanaListaUnidadesMedidas.GetListUnidadesMedida().Count == 0)
                    {

                    }
                    else
                    {
                        GetUnidadesMedida(ventanaListaUnidadesMedidas.GetListUnidadesMedida(), txtUomCode, txtNumPerMsr, row_Selected);
                    }
                }
            }
        }

        private void GetUnidadesMedida(List<UnidadesMedida> listUnidadesMedida, System.Windows.Controls.TextBox txtUomCode, TextBlock txtNumPerMsr, DataRowView row_Selected)
        {
            foreach (UnidadesMedida unidadesMedida in listUnidadesMedida)
            {
                txtUomCode.Text = unidadesMedida.UomCode;

                row_Selected["UomCode"] = unidadesMedida.UomCode;

                row_Selected["UomEntry"] = unidadesMedida.UomEntry;

                row_Selected["unitMsr"] = unidadesMedida.UomName;

                txtNumPerMsr.Text = ConvertDecimalTwoPlaces(unidadesMedida.BaseQty / unidadesMedida.AltQty).ToString("N2", nfi);

                row_Selected["NumPerMsr"] = ConvertDecimalTwoPlaces(unidadesMedida.BaseQty / unidadesMedida.AltQty).ToString("N2", nfi);


            }
        }
    }
}
