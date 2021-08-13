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
    /// Lógica de interacción para EntradaMercancia.xaml
    /// </summary>
    public partial class EntradaMercancia : Document
    {
        ControladorEntradaMercancia cn = new ControladorEntradaMercancia();

        ControladorSocioNegocio cs = new ControladorSocioNegocio();

        ControladorArticulos ca = new ControladorArticulos();

        ControladorRetencionImpuesto cr = new ControladorRetencionImpuesto();

        ControladorDefinicionUnidadesMedida cu = new ControladorDefinicionUnidadesMedida();

        ControladorAsiento cj = new ControladorAsiento();

        DataTable dtArticulo = new DataTable() { TableName = "dtArticulo" };      

        DataTable dt = new DataTable();

        DataGrid dg = new DataGrid();

        DataTable dtRetenciones;

        DataTable dtJournalEntry = new DataTable();

        DataTable dtNewJournalEntry = new DataTable();

        private bool sw;

        private List<DocumentoCabecera> listaEntradaMercancia = new List<DocumentoCabecera>();

        private DataTable listaEntradaMercanciaLines = new DataTable();

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

        public ObservableCollection<string> TipoTransaccion { get; set; }

        public ObservableCollection<string> WtLiable { get; set; }

        public ObservableCollection<string> ClaseFactura { get; set; }
        public decimal Rate { get => rate; set => rate = value; }
        public decimal RateFC { get => rateFC; set => rateFC = value; }
        public string SelectedDate { get => selectedDate; set => selectedDate = value; }
        public int DocNum { get => docNum; set => docNum = value; }
        public bool SwReadOnly { get; private set; }

        private List<ArticuloDetalle> listArticuloDetalleOld = new List<ArticuloDetalle>();
        public List<ArticuloDetalle> ListArticuloDetalleOld { get => listArticuloDetalleOld; set => listArticuloDetalleOld = value; }
        public List<DocumentoCabecera> ListaEntradaMercancia { get => listaEntradaMercancia; set => listaEntradaMercancia = value; }
        public DataTable ListaEntradaMercanciaLines { get => listaEntradaMercanciaLines; set => listaEntradaMercanciaLines = value; }
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
        public string CtlAccount { get => ctlAccount; set => ctlAccount = value; }
        public int TransId { get => transId; set => transId = value; }
        public string LicTradNum { get => licTradNum; set => licTradNum = value; }
        public char DocType { get; private set; }
        public int DocEntryDeleted { get; private set; }

        public EntradaMercancia()
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

            SwReadOnly = false;

            dgArticulo.CanUserAddRows = true;

            dgArticulo.CanUserDeleteRows = true;

            dgArticulo.CanUserSortColumns = true;

            cbMoneda.ItemsSource = cn.CreateCurrencyTable("", "");

            cbClase.SelectedValue = "Articulo";

            dtArticulo.Rows.Clear();

            dtArticulo.AcceptChanges();

            dtArticulo.NewRow();

            dgArticulo.ItemsSource = dtArticulo.DefaultView;
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

                ListaEntradaMercancia.Clear();

                ListaEntradaMercanciaLines.Clear();

                var result= cn.FindLastEntradaMercancia();

                if (result.Item2 == null)
                {
                    SwReadOnly = true;

                    ListaEntradaMercancia = result.Item1;

                    GetEntradaMercancia(ListaEntradaMercancia);

                    var result1= cn.FindEntradaMercanciaLines(DocEntry);

                    if (result1.Item2 == null)
                    {
                        ListaEntradaMercanciaLines = result1.Item1;

                        GetEntradaMercanciaLines(ListaEntradaMercanciaLines);

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

                ListaEntradaMercancia.Clear();

                ListaEntradaMercanciaLines.Clear();

                var result= cn.FindNextEntradaMercancia(txtNro.Text);

                if (result.Item2 == null)
                {
                    SwReadOnly = true;

                    ListaEntradaMercancia = result.Item1;

                    GetEntradaMercancia(ListaEntradaMercancia);

                    var result1= cn.FindEntradaMercanciaLines(DocEntry);

                    if (result1.Item2 == null)
                    {
                        ListaEntradaMercanciaLines = result1.Item1;

                        GetEntradaMercanciaLines(ListaEntradaMercanciaLines);

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

                }

            }

            return dt;
        }

        private void GetEntradaMercanciaLines(DataTable listaPurchaseInvoiceLines)
        {
            dt = AddCurrencyCode(listaPurchaseInvoiceLines);

            dg.ItemsSource = dt.DefaultView;

            dg.CanUserAddRows = false;

            dg.CanUserDeleteRows = false;

            dg.CanUserSortColumns = false;
        }

        private void GetEntradaMercancia(List<DocumentoCabecera> listaPurchaseInvoice)
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

                ListaEntradaMercancia.Clear();

                ListaEntradaMercanciaLines.Clear();

                var result= cn.FindPreviousEntradaMercancia(txtNro.Text);

                if (result.Item2 == null)
                {
                    SwReadOnly = true;

                    ListaEntradaMercancia = result.Item1;

                    GetEntradaMercancia(ListaEntradaMercancia);

                    var result1= cn.FindEntradaMercanciaLines(DocEntry);

                    if (result1.Item2 == null)
                    {
                        ListaEntradaMercanciaLines = result1.Item1;

                        GetEntradaMercanciaLines(ListaEntradaMercanciaLines);

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

                ListaEntradaMercancia.Clear();

                ListaEntradaMercanciaLines.Clear();

                var result = cn.FindFirstEntradaMercancia();

                if (result.Item2 == null)
                {
                    SwReadOnly = true;

                    ListaEntradaMercancia = result.Item1;

                    GetEntradaMercancia(ListaEntradaMercancia);

                    var result1 = cn.FindEntradaMercanciaLines(DocEntry);

                    if (result1.Item2 == null)
                    {
                        ListaEntradaMercanciaLines = result1.Item1;

                        GetEntradaMercanciaLines(ListaEntradaMercanciaLines);

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

            ClaseFactura = new ObservableCollection<string>() {"Articulo" };

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
            dtArticulo.Columns.Add("ItemCode");
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
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
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

           // ClearDgArticulo();           

        }

        private void ClearDgArticulo()
        {
            dtArticulo.Rows.Clear();

            dtArticulo.NewRow();

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
            var result = cn.FindSuppliers();

            if (result.Item2 == null)
            {
                RecorreListaSN(result.Item1);
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: ", Brushes.Red, Brushes.White, "003-interface-2.png");
            }

        }

        private void RecorreListaSN(List<SocioNegocio> listSuppliers)
        {
            if (listSuppliers.Count == 1)
            {
                GetSocioNegocio(listSuppliers);

                var result1 = cn.FindSupplierCurrency(Supplier);

                if (result1.Item2 == null)
                {
                    cbMoneda.ItemsSource = result1.Item1;
                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result1.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
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

                        var result1 = cn.FindSupplierCurrency(Supplier);

                        if (result1.Item2 == null)
                        {
                            cbMoneda.ItemsSource = result1.Item1;
                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result1.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
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

                txtEntradaDiario.Text = "Entrada Mercancia - " + Suppliers.CardCode;
            
                VatGroup = Suppliers.VatGroup;

                CtlAccount = Suppliers.DebPayAcct;

                LicTradNum= Suppliers.LicTradNum;
            }
        }

        private void imgAcctCode_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                bool? sw = null;

                var row_list = GetDataGridRows(dgArticulo);

                DataRowView row_Selected = dgArticulo.SelectedItem as DataRowView;

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
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error" + ex.Message, Brushes.Red, Brushes.Black, "003-interface-2.png");

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

        private void RecorreListaItemCode(List<Entidades.Articulos> listArticulos, TextBox txtItemCode, TextBlock txtPrice, TextBlock txtLineTotal, TextBlock txtTaxCode, TextBlock txtUomCode, TextBlock txtNumPerMsr, DataRowView row_Selected)
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

        private List<DocumentoCabecera> CreateListPurchase()
        {
            List<DocumentoCabecera> listPurchaseInvoice = new List<DocumentoCabecera>();

            DocumentoCabecera PurchaseInvoice = new DocumentoCabecera();

            PurchaseInvoice.DocNum = Convert.ToInt32(txtNro.Text);
            PurchaseInvoice.DocDate = dpContabilizacion.SelectedDate;
            PurchaseInvoice.TaxDate = dpDocumento.SelectedDate;
            PurchaseInvoice.DocDueDate = dpVencimiento.SelectedDate;
            PurchaseInvoice.NumAtCard = txtNroFactura.Text;            
            PurchaseInvoice.Comments = txtComentario.Text;
            PurchaseInvoice.DocType = cn.GetDocType(cbClase.SelectedValue.ToString());
            DocType = PurchaseInvoice.DocType;
            PurchaseInvoice.Canceled = 'N';
            PurchaseInvoice.UserSign = Properties.Settings.Default.Usuario;
            PurchaseInvoice.UpdateDate = fechaActual.GetFechaActual();
            PurchaseInvoice.DocStatus = cn.GetDocStatus(txtEstado.Text);
            var result10 = cn.GetPeriodCode(PurchaseInvoice.DocDate);
            PurchaseInvoice.FinncPriod = result10.Item1;
            PurchaseInvoice.Address2 = txtDestino.Text;
            PurchaseInvoice.InvntSttus = cn.CalculaInvStatus(cbClase.SelectedValue.ToString());
            PurchaseInvoice.VatSum = cn.CalculateVatSum(dt);
            PurchaseInvoice.VatSumFC = cn.CalculateVatSumFC(dt);
            PurchaseInvoice.VatSumSy1 = cn.CalculateVatSumSy(dt);
            PurchaseInvoice.DocCurr = cbMoneda.SelectedValue.ToString();
            PurchaseInvoice.ObjType = Convert.ToString(cn.GetTransType("EP"));
            PurchaseInvoice.CardCode = txtProveedor.Text;
            PurchaseInvoice.CardName = txtNombre.Text;
            PurchaseInvoice.JrnlMemo = txtEntradaDiario.Text;
            PurchaseInvoice.LicTradNum = LicTradNum;

            var result4 = cn.SelectTransId();

            TransId = result4.Item1;
            PurchaseInvoice.TransId = result4.Item1;
            PurchaseInvoice.VatPaid = 0;
            PurchaseInvoice.VatPaidFC = 0;
            PurchaseInvoice.VatPaidSys = 0;
            PurchaseInvoice.PaidToDate = 0;
            PurchaseInvoice.PaidSum = 0;
            PurchaseInvoice.PaidSumFc = 0;
            PurchaseInvoice.PaidSumSc = 0;
            PurchaseInvoice.WTApplied = 0;
            PurchaseInvoice.WTAppliedF = 0;
            PurchaseInvoice.WTAppliedS = 0;
            PurchaseInvoice.WTSum = 0;
            PurchaseInvoice.WTSumFC = 0;
            PurchaseInvoice.WTSumSC = 0;
            PurchaseInvoice.SysRate = Rate;

            if (PurchaseInvoice.DocCurr == Properties.Settings.Default.MainCurrency)
            {
                PurchaseInvoice.DocRate = 1;
            }
            else
            {
                PurchaseInvoice.DocRate = RateFC;
            }

            PurchaseInvoice.CtlAccount = CtlAccount;
            PurchaseInvoice.BaseAmnt = cn.CalculateBaseAmnt(dt);
            PurchaseInvoice.BaseAmntFC = cn.CalculateBaseAmntFC(dt);
            PurchaseInvoice.BaseAmntSC = cn.CalculateBaseAmntSC(dt);
            PurchaseInvoice.DocTotal = PurchaseInvoice.VatSum + PurchaseInvoice.BaseAmnt - PurchaseInvoice.WTSum;
            PurchaseInvoice.DocTotalFC = PurchaseInvoice.VatSumFC + PurchaseInvoice.BaseAmntFC - PurchaseInvoice.WTSumFC;
            PurchaseInvoice.DocTotalSy = PurchaseInvoice.VatSumSy1 + PurchaseInvoice.BaseAmntSC - PurchaseInvoice.WTSumSC;
            PurchaseInvoice.DocSubType = "--";
            PurchaseInvoice.Max1099 = PurchaseInvoice.VatSum + PurchaseInvoice.BaseAmnt;


            DocTotal1 = PurchaseInvoice.DocTotal;

            DocTotalFC1 = PurchaseInvoice.DocTotalFC;

            DocTotalSy1 = PurchaseInvoice.DocTotalSy;

            listPurchaseInvoice.Add(PurchaseInvoice);

            return listPurchaseInvoice;
        }

        private Tuple<List<DocumentoDetalle>, int> CreateListPurchaseLines(List<DocumentoCabecera> listPurchaseInvoice)
        {
            int DocNum = 0;

            string DocCurr = null;

            DateTime? DocDate = null;

            string CardCode = null;

            int FinncPriod = 0;

            string ObjType = null;

            string Address2 = null;

            char InvntSttus = 'S';

            foreach (DocumentoCabecera PurchaseInvoice in listPurchaseInvoice)
            {
                DocNum = PurchaseInvoice.DocNum;
                DocCurr = PurchaseInvoice.DocCurr;
                DocDate = PurchaseInvoice.DocDate;
                CardCode = PurchaseInvoice.CardCode;
                FinncPriod = PurchaseInvoice.FinncPriod;
                ObjType = PurchaseInvoice.ObjType;
                Address2 = PurchaseInvoice.Address2;
                InvntSttus = PurchaseInvoice.InvntSttus;

            }

            List<DocumentoDetalle> listPurchaseInvoiceLines = new List<DocumentoDetalle>();

            int i = 0;

            foreach (DataRow row in dtArticulo.Rows)
            {
                DocumentoDetalle PurchaseInvoiceLines = new DocumentoDetalle();

                var result1 = cn.FindDocEntry(DocNum);
                DocEntryDeleted = result1.Item1;
                PurchaseInvoiceLines.DocEntry = result1.Item1;
                PurchaseInvoiceLines.LineNum = i;
                PurchaseInvoiceLines.LineStatus = 'C';
                PurchaseInvoiceLines.ItemCode = FindItemCode(row["ItemCode"].ToString());
                PurchaseInvoiceLines.Dscription = row["Dscription"].ToString();
                PurchaseInvoiceLines.NumPerMsr = ConvertDecimalTwoPlaces(row["NumPerMsr"]);
                PurchaseInvoiceLines.NumPerMsr2 = ConvertDecimalTwoPlaces(row["NumPerMsr2"]);
                PurchaseInvoiceLines.UomEntry = row["UomEntry"] == null || row["UomEntry"].ToString() == "" ? 0 : Convert.ToInt32(row["UomEntry"]);
                PurchaseInvoiceLines.UomEntry2 = row["UomEntry2"] == null || row["UomEntry2"].ToString() == "" ? 0 : Convert.ToInt32(row["UomEntry2"]);
                PurchaseInvoiceLines.UomCode = row["UomCode"] == null || row["UomCode"].ToString() == "" ? null : row["UomCode"].ToString();
                PurchaseInvoiceLines.UomCode2 = row["UomCode2"] == null || row["UomCode2"].ToString() == "" ? null : row["UomCode2"].ToString();
                PurchaseInvoiceLines.unitMsr = row["unitMsr"] == null || row["unitMsr"].ToString() == "" ? null : row["unitMsr"].ToString();
                PurchaseInvoiceLines.unitMsr2 = row["unitMsr2"] == null || row["unitMsr2"].ToString() == "" ? null : row["unitMsr2"].ToString();
                PurchaseInvoiceLines.Quantity = ConvertDecimalTwoPlaces(row["Quantity"].ToString());
                PurchaseInvoiceLines.OpenQty = PurchaseInvoiceLines.Quantity;
                PurchaseInvoiceLines.InvQty = PurchaseInvoiceLines.Quantity * PurchaseInvoiceLines.NumPerMsr;
                PurchaseInvoiceLines.OpenInvQty = PurchaseInvoiceLines.Quantity * PurchaseInvoiceLines.NumPerMsr;
                str = regex.Replace(row["Price"].ToString(), String.Empty);
                PurchaseInvoiceLines.Price = ConvertDecimalTwoPlaces(str);
                PurchaseInvoiceLines.Currency = DocCurr;
                str = regex.Replace(row["LineTotal"].ToString(), String.Empty);
                PurchaseInvoiceLines.LineTotal = ConvertDecimalTwoPlaces(str);
                PurchaseInvoiceLines.AcctCode = row["AcctCode"].ToString();
                PurchaseInvoiceLines.DocDate = DocDate;
                PurchaseInvoiceLines.BaseCard = CardCode;
                str = regex.Replace(row["TotalSumSy"].ToString(), String.Empty);
                PurchaseInvoiceLines.TotalSumSy = ConvertDecimalTwoPlaces(str);
                str = regex.Replace(row["TotalFrgn"].ToString(), String.Empty);
                PurchaseInvoiceLines.TotalFrgn = ConvertDecimalTwoPlaces(str);
                PurchaseInvoiceLines.VatSum = ConvertDecimalTwoPlaces(row["VatSum"].ToString());
                PurchaseInvoiceLines.VatSumFrgn = ConvertDecimalTwoPlaces(row["VatSumFrgn"].ToString());
                PurchaseInvoiceLines.VatSumSy = ConvertDecimalTwoPlaces(row["VatSumSy"].ToString());
                PurchaseInvoiceLines.VatGroup = row["VatGroup"].ToString();
                PurchaseInvoiceLines.VatPrcnt = ConvertDecimalTwoPlaces(row["VatPrcnt"].ToString());
                PurchaseInvoiceLines.FinncPriod = FinncPriod;
                PurchaseInvoiceLines.ObjType = ObjType;
                PurchaseInvoiceLines.Address = Address2;
                PurchaseInvoiceLines.Gtotal = ConvertDecimalTwoPlaces(row["GTotal"].ToString());
                PurchaseInvoiceLines.GtotalFC = ConvertDecimalTwoPlaces(row["GTotalFC"].ToString());
                PurchaseInvoiceLines.GtotalSC = ConvertDecimalTwoPlaces(row["GTotalSC"].ToString());
                PurchaseInvoiceLines.StockSum = ConvertDecimalTwoPlaces(row["StockSum"].ToString());
                PurchaseInvoiceLines.StockSum = GetItemOfService(dtArticulo, PurchaseInvoiceLines.LineTotal);
                PurchaseInvoiceLines.StockSumFc = ConvertDecimalTwoPlaces(row["StockSumFc"].ToString());
                PurchaseInvoiceLines.StockSumFc = GetItemOfService(dtArticulo, PurchaseInvoiceLines.TotalFrgn);
                PurchaseInvoiceLines.StockSumSc = ConvertDecimalTwoPlaces(row["StockSumSc"].ToString());
                PurchaseInvoiceLines.StockSumSc = GetItemOfService(dtArticulo, PurchaseInvoiceLines.TotalSumSy);
                PurchaseInvoiceLines.InvntSttus = InvntSttus;
                PurchaseInvoiceLines.WtLiable = cn.GetWTLiable(cbSujetoRetencionItem.SelectedValuePath.ToString());
                PurchaseInvoiceLines.DataSource = 'N';
                PurchaseInvoiceLines.IsTax = cn.TraduceChar(row["IsTax"] == null || row["IsTax"].ToString() == "" ? false : Convert.ToBoolean(row["IsTax"]));
                PurchaseInvoiceLines.StartValue = cn.TraduceChar(row["StartValue"] == null || row["StartValue"].ToString() == "" ? false : Convert.ToBoolean(row["StartValue"]));

                listPurchaseInvoiceLines.Add(PurchaseInvoiceLines);

                i++;

            }

            return Tuple.Create(listPurchaseInvoiceLines, i);
        }

        private Tuple<List<DocumentoDetalle>, int> CreateListPurchaseLinesPreliminar(List<DocumentoCabecera> listPurchaseInvoice)
        {
            int DocNum = 0;

            string DocCurr = null;

            DateTime? DocDate = null;

            string CardCode = null;

            int FinncPriod = 0;

            string ObjType = null;

            string Address2 = null;

            char InvntSttus = 'S';

            foreach (DocumentoCabecera PurchaseInvoice in listPurchaseInvoice)
            {
                DocNum = PurchaseInvoice.DocNum;
                DocCurr = PurchaseInvoice.DocCurr;
                DocDate = PurchaseInvoice.DocDate;
                CardCode = PurchaseInvoice.CardCode;
                FinncPriod = PurchaseInvoice.FinncPriod;
                ObjType = PurchaseInvoice.ObjType;
                Address2 = PurchaseInvoice.Address2;
                InvntSttus = PurchaseInvoice.InvntSttus;

            }

            List<DocumentoDetalle> listPurchaseInvoiceLines = new List<DocumentoDetalle>();

            int i = 0;

            foreach (DataRow row in dtArticulo.Rows)
            {
                DocumentoDetalle PurchaseInvoiceLines = new DocumentoDetalle();

                var result1 = cn.FindDocEntryPreliminar(DocNum);
                DocEntryDeleted = result1.Item1;
                PurchaseInvoiceLines.DocEntry = result1.Item1;
                PurchaseInvoiceLines.LineNum = i;
                PurchaseInvoiceLines.LineStatus = 'C';
                PurchaseInvoiceLines.ItemCode = FindItemCode(row["ItemCode"].ToString());
                PurchaseInvoiceLines.Dscription = row["Dscription"].ToString();
                PurchaseInvoiceLines.NumPerMsr = ConvertDecimalTwoPlaces(row["NumPerMsr"]);
                PurchaseInvoiceLines.NumPerMsr2 = ConvertDecimalTwoPlaces(row["NumPerMsr2"]);
                PurchaseInvoiceLines.UomEntry = row["UomEntry"] == null || row["UomEntry"].ToString() == "" ? 0 : Convert.ToInt32(row["UomEntry"]);
                PurchaseInvoiceLines.UomEntry2 = row["UomEntry2"] == null || row["UomEntry2"].ToString() == "" ? 0 : Convert.ToInt32(row["UomEntry2"]);
                PurchaseInvoiceLines.UomCode = row["UomCode"] == null || row["UomCode"].ToString() == "" ? null : row["UomCode"].ToString();
                PurchaseInvoiceLines.UomCode2 = row["UomCode2"] == null || row["UomCode2"].ToString() == "" ? null : row["UomCode2"].ToString();
                PurchaseInvoiceLines.unitMsr = row["unitMsr"] == null || row["unitMsr"].ToString() == "" ? null : row["unitMsr"].ToString();
                PurchaseInvoiceLines.unitMsr2 = row["unitMsr2"] == null || row["unitMsr2"].ToString() == "" ? null : row["unitMsr2"].ToString();
                PurchaseInvoiceLines.Quantity = ConvertDecimalTwoPlaces(row["Quantity"].ToString());
                PurchaseInvoiceLines.OpenQty = PurchaseInvoiceLines.Quantity;
                PurchaseInvoiceLines.InvQty = PurchaseInvoiceLines.Quantity * PurchaseInvoiceLines.NumPerMsr;
                PurchaseInvoiceLines.OpenInvQty = PurchaseInvoiceLines.Quantity * PurchaseInvoiceLines.NumPerMsr;
                str = regex.Replace(row["Price"].ToString(), String.Empty);
                PurchaseInvoiceLines.Price = ConvertDecimalTwoPlaces(str);
                PurchaseInvoiceLines.Currency = DocCurr;
                str = regex.Replace(row["LineTotal"].ToString(), String.Empty);
                PurchaseInvoiceLines.LineTotal = ConvertDecimalTwoPlaces(str);
                PurchaseInvoiceLines.AcctCode = row["AcctCode"].ToString();
                PurchaseInvoiceLines.DocDate = DocDate;
                PurchaseInvoiceLines.BaseCard = CardCode;
                str = regex.Replace(row["TotalSumSy"].ToString(), String.Empty);
                PurchaseInvoiceLines.TotalSumSy = ConvertDecimalTwoPlaces(str);
                str = regex.Replace(row["TotalFrgn"].ToString(), String.Empty);
                PurchaseInvoiceLines.TotalFrgn = ConvertDecimalTwoPlaces(str);
                PurchaseInvoiceLines.VatSum = ConvertDecimalTwoPlaces(row["VatSum"].ToString());
                PurchaseInvoiceLines.VatSumFrgn = ConvertDecimalTwoPlaces(row["VatSumFrgn"].ToString());
                PurchaseInvoiceLines.VatSumSy = ConvertDecimalTwoPlaces(row["VatSumSy"].ToString());
                PurchaseInvoiceLines.VatGroup = row["VatGroup"].ToString();
                PurchaseInvoiceLines.VatPrcnt = ConvertDecimalTwoPlaces(row["VatPrcnt"].ToString());
                PurchaseInvoiceLines.FinncPriod = FinncPriod;
                PurchaseInvoiceLines.ObjType = ObjType;
                PurchaseInvoiceLines.Address = Address2;
                PurchaseInvoiceLines.Gtotal = ConvertDecimalTwoPlaces(row["GTotal"].ToString());
                PurchaseInvoiceLines.GtotalFC = ConvertDecimalTwoPlaces(row["GTotalFC"].ToString());
                PurchaseInvoiceLines.GtotalSC = ConvertDecimalTwoPlaces(row["GTotalSC"].ToString());
                PurchaseInvoiceLines.StockSum = ConvertDecimalTwoPlaces(row["StockSum"].ToString());
                PurchaseInvoiceLines.StockSum = GetItemOfService(dtArticulo, PurchaseInvoiceLines.LineTotal);
                PurchaseInvoiceLines.StockSumFc = ConvertDecimalTwoPlaces(row["StockSumFc"].ToString());
                PurchaseInvoiceLines.StockSumFc = GetItemOfService(dtArticulo, PurchaseInvoiceLines.TotalFrgn);
                PurchaseInvoiceLines.StockSumSc = ConvertDecimalTwoPlaces(row["StockSumSc"].ToString());
                PurchaseInvoiceLines.StockSumSc = GetItemOfService(dtArticulo, PurchaseInvoiceLines.TotalSumSy);
                PurchaseInvoiceLines.InvntSttus = InvntSttus;
                PurchaseInvoiceLines.WtLiable = cn.GetWTLiable(cbSujetoRetencionItem.SelectedValuePath.ToString());
                PurchaseInvoiceLines.DataSource = 'N';
                PurchaseInvoiceLines.IsTax = cn.TraduceChar(row["IsTax"] == null || row["IsTax"].ToString() == "" ? false : Convert.ToBoolean(row["IsTax"]));
                PurchaseInvoiceLines.StartValue = cn.TraduceChar(row["StartValue"] == null || row["StartValue"].ToString() == "" ? false : Convert.ToBoolean(row["StartValue"]));

                listPurchaseInvoiceLines.Add(PurchaseInvoiceLines);

                i++;

            }

            return Tuple.Create(listPurchaseInvoiceLines, i);
        }

        private bool CreateListItem(List<DocumentoCabecera> listPurchaseInvoice, List<DocumentoDetalle> listPurchaseInvoiceLines)
        {
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

            decimal DocRate = 0;

            decimal SysRate = 0;


            foreach (DocumentoCabecera PurchaseInvoice in listPurchaseInvoice)
            {
                DocNum = PurchaseInvoice.DocNum;
                DocDueDate = PurchaseInvoice.DocDueDate;
                TaxDate = PurchaseInvoice.TaxDate;
                CardCode = PurchaseInvoice.CardCode;
                CardName = PurchaseInvoice.CardName;
                Comments = PurchaseInvoice.Comments;
                DocRate = PurchaseInvoice.DocRate;
                SysRate = PurchaseInvoice.SysRate;
                JrnlMemo = PurchaseInvoice.JrnlMemo;
                UserSign = PurchaseInvoice.UserSign;
                UpdateDate = PurchaseInvoice.UpdateDate;

            }

            List<ArticuloDetalle> listItemPurchase = new List<ArticuloDetalle>();

            foreach (DocumentoDetalle PurchaseInvoiceLines in listPurchaseInvoiceLines)
            {
                ArticuloDetalle ItemPurchaseLines = new ArticuloDetalle();

                if (sw == true)
                {
                    ItemPurchaseLines.TransType = Convert.ToInt32(PurchaseInvoiceLines.ObjType);
                    ItemPurchaseLines.CreatedBy = PurchaseInvoiceLines.DocEntry;
                    ItemPurchaseLines.BASE_REF = DocNum.ToString();
                    ItemPurchaseLines.DocLineNum = PurchaseInvoiceLines.LineNum;
                    ItemPurchaseLines.DocDate = PurchaseInvoiceLines.DocDate;
                    ItemPurchaseLines.TaxDate = TaxDate;
                    ItemPurchaseLines.DocDueDate = DocDueDate;
                    ItemPurchaseLines.CardCode = CardCode;
                    ItemPurchaseLines.CardName = CardName;
                    ItemPurchaseLines.Comments = Comments;
                    ItemPurchaseLines.JrnlMemo = JrnlMemo;
                    ItemPurchaseLines.ItemCode = PurchaseInvoiceLines.ItemCode;
                    ItemPurchaseLines.Dscription = PurchaseInvoiceLines.Dscription;
                    ItemPurchaseLines.InQty = PurchaseInvoiceLines.Quantity * PurchaseInvoiceLines.NumPerMsr;
                    ItemPurchaseLines.OutQty = 0;
                    ItemPurchaseLines.Price = PurchaseInvoiceLines.Price;
                    ItemPurchaseLines.Currency = PurchaseInvoiceLines.Currency;
                    ItemPurchaseLines.Rate = DocRate;
                    ItemPurchaseLines.SysRate = SysRate;
                    ItemPurchaseLines.Type = 'E';
                    ItemPurchaseLines.UserSign = UserSign;
                    ItemPurchaseLines.CalcPrice = PurchaseInvoiceLines.LineTotal / (PurchaseInvoiceLines.Quantity * PurchaseInvoiceLines.NumPerMsr);
                    ItemPurchaseLines.OpenQty = PurchaseInvoiceLines.Quantity * PurchaseInvoiceLines.NumPerMsr;
                    ItemPurchaseLines.CreateDate = UpdateDate;
                    ItemPurchaseLines.CostMethod = 'F';
                    ItemPurchaseLines.TransValue = PurchaseInvoiceLines.LineTotal;
                    ItemPurchaseLines.OpenValue = ItemPurchaseLines.TransValue;
                    ItemPurchaseLines.InvntAct = PurchaseInvoiceLines.AcctCode;
                    var balance = cn.CalculateBalanceItem(ItemPurchaseLines.ItemCode);
                    ItemPurchaseLines.Balance = balance.Item1 + ItemPurchaseLines.TransValue;

                    listItemPurchase.Add(ItemPurchaseLines);

                    var QueryItemPurchase = cn.InsertOINM(listItemPurchase);

                    if (QueryItemPurchase.Item1 == 1)
                    {
                        GetFindTransaccion(listItemPurchase);

                        listItemPurchase.Clear();

                        sw = true;

                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + QueryItemPurchase.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                        sw = false;
                    }

                }
            }

            return sw;
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

        private List<AsientoCabecera> CreateJournalEntry(List<DocumentoCabecera> listPurchaseInvoice)
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


            foreach (DocumentoCabecera PurchaseInvoice in listPurchaseInvoice)
            {
                TransId = PurchaseInvoice.TransId;
                DocCurr = PurchaseInvoice.DocCurr;
                DocDate = PurchaseInvoice.DocDate;
                DocDueDate = PurchaseInvoice.DocDueDate;
                TaxDate = PurchaseInvoice.TaxDate;
                FinncPriod = PurchaseInvoice.FinncPriod;
                ObjType = PurchaseInvoice.ObjType;
                JrnlMemo = PurchaseInvoice.JrnlMemo;
                DocTotal = PurchaseInvoice.DocTotal;
                DocTotalFC = PurchaseInvoice.DocTotalFC;
                DocTotalSy = PurchaseInvoice.DocTotalSy;

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

        private Tuple<List<AsientoDetalle>, int> CreateListJournalEntryLines(List<AsientoCabecera> listaJournalEntry)
        {
            dtNewJournalEntry = CreateDatatableJournalEntryLines(dt);

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

        private void btnCrear_Click(object sender, RoutedEventArgs e)
        {
            List<DocumentoCabecera> listPurchaseInvoice = new List<DocumentoCabecera>();

            DocumentoCabecera PurchaseInvoice = new DocumentoCabecera();

            List<DocumentoDetalle> listPurchaseInvoiceLines = new List<DocumentoDetalle>();

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
                        PurchaseInvoice.DocNum = Convert.ToInt32(txtNro.Text);
                    }
                    else
                    {
                        PurchaseInvoice.DocNum = 0;
                    }

                    PurchaseInvoice.DocDate = dpContabilizacion.SelectedDate;
                    PurchaseInvoice.TaxDate = dpDocumento.SelectedDate;
                    PurchaseInvoice.DocDueDate = dpVencimiento.SelectedDate;
                    PurchaseInvoice.CardCode = txtProveedor.Text;
                    PurchaseInvoice.CardName = txtNombre.Text;
                    PurchaseInvoice.NumAtCard = txtNroFactura.Text;                   
                    PurchaseInvoice.Comments = txtComentario.Text;

                    listPurchaseInvoice.Add(PurchaseInvoice);

                    var result1 = cn.FindEntradaMercancia(listPurchaseInvoice);

                    if (result1.Item2 == null)
                    {
                        RecorreListaPurchaseInvoice(result1.Item1);
                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result1.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                    }

                    break;

                case "Crear":

                    bool? OPDN = null;

                    bool? PDN1 = null;
                
                    bool? OJDT = null;

                    bool? JDT1 = null;

                    bool? OINM = null;

                    try
                    {
                      
                        listPurchaseInvoice = CreateListPurchase();

                        var result5 = cn.InsertEntradaMercancia(listPurchaseInvoice);

                        if (result5.Item1 == 1)
                        {
                            OPDN = true;  //Inserto factura cabecera

                            var listPurchase = CreateListPurchaseLines(listPurchaseInvoice);

                            var result6 = cn.InsertEntradaMercanciaLines(listPurchase.Item1);

                            if (listPurchase.Item2 == result6.Item1)
                            {
                                PDN1 = true;                                

                                bool listItemPurchase = CreateListItem(listPurchaseInvoice, listPurchase.Item1);

                                if (listItemPurchase == true)
                                {
                                    OINM = true;

                                    //Create Journal Entry

                                    listaJournalEntry = CreateJournalEntry(listPurchaseInvoice);

                                    //Contruir asiento

                                    var result2 = cj.InsertJournalEntry(listaJournalEntry);

                                    if (result2.Item1 == 1)
                                    {
                                        OJDT = true;

                                        var listJournalEntryLines = CreateListJournalEntryLines(listaJournalEntry);

                                        var result4 = cj.InsertJournalEntryLines(listJournalEntryLines.Item1);

                                        if (listJournalEntryLines.Item2 == result4.Item1)
                                        {
                                            JDT1 = true;

                                            cn.UpdateCreditDebitAccount(dtNewJournalEntry);

                                            cn.UpdateItemDebit(listPurchase.Item1);

                                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Operacion se realizo exitosamente", Brushes.LightGreen, Brushes.Black, "001-interface.png");

                                            LimpiarCampos();

                                            btnCrear.Content = "OK";

                                        }
                                        else
                                        {
                                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: " + result4.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");

                                            JDT1 = false;

                                            DeletedAllInsertItem(OPDN, PDN1, OJDT, JDT1, OINM);

                                            var listArticuloDetalleOld = cn.DeleteOldRecord(ListArticuloDetalleOld);

                                            if (listArticuloDetalleOld.Item1 == ListArticuloDetalleOld.Count)
                                            {

                                            }
                                            else
                                            {
                                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error actualizar registros OINM: " + listArticuloDetalleOld.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                                            }

                                            ListArticuloDetalleOld.Clear();

                                        }

                                    }
                                    else
                                    {
                                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: " + result2.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");

                                        OJDT = false;

                                        DeletedAllInsertItem(OPDN, PDN1, OJDT, JDT1, OINM);

                                        var listArticuloDetalleOld = cn.DeleteOldRecord(ListArticuloDetalleOld);

                                        if (listArticuloDetalleOld.Item1 == ListArticuloDetalleOld.Count)
                                        {

                                        }
                                        else
                                        {
                                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error actualizar registros OINM: " + listArticuloDetalleOld.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                                        }

                                        ListArticuloDetalleOld.Clear();

                                    }
                                }
                                else
                                {
                                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: ", Brushes.Red, Brushes.White, "003-interface-2.png");

                                    OINM = false;

                                    DeletedAllInsertItem(OPDN, PDN1, OJDT, JDT1, OINM);

                                    var listArticuloDetalleOld = cn.DeleteOldRecord(ListArticuloDetalleOld);

                                    if (listArticuloDetalleOld.Item1 == ListArticuloDetalleOld.Count)
                                    {

                                    }
                                    else
                                    {
                                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error actualizar registros OINM: " + listArticuloDetalleOld.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                                    }

                                    ListArticuloDetalleOld.Clear();
                                }

                            }
                            else
                            {
                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: " + result6.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                                PDN1 = false;
                                DeletedAllInsertItem(OPDN, PDN1, OJDT, JDT1, OINM);
                            }

                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: " + result5.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                            OPDN = false;
                            DeletedAllInsertItem(OPDN, PDN1, OJDT, JDT1, OINM);
                        }


                    }
                    catch (Exception ex)

                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error General: " + ex.Message, Brushes.Red, Brushes.White, "003-interface-2.png");

                        if (OINM != null)
                        {

                            DeletedAllInsertItem(OPDN, PDN1, OJDT, JDT1, OINM);

                            var listArticuloDetalleOld = cn.DeleteOldRecord(ListArticuloDetalleOld);

                            if (listArticuloDetalleOld.Item1 == ListArticuloDetalleOld.Count)
                            {

                            }
                            else
                            {
                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error actualizar registros OINM: " + listArticuloDetalleOld.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                            }

                            ListArticuloDetalleOld.Clear();

                        }
                        else
                        {
                            DeletedAllInsertItem(OPDN, PDN1, OJDT, JDT1, OINM);
                        }
                    }

                    

                    break;

                case "Actualizar":

                    PurchaseInvoice.DocEntry = DocEntry;
                    PurchaseInvoice.DocNum = DocNum;
                    PurchaseInvoice.TaxDate = dpDocumento.SelectedDate;
                    PurchaseInvoice.DocDueDate = dpVencimiento.SelectedDate;
                    PurchaseInvoice.NumAtCard = txtNroFactura.Text;                   
                    PurchaseInvoice.Comments = txtComentario.Text;
                    PurchaseInvoice.JrnlMemo = txtEntradaDiario.Text;                   
                    PurchaseInvoice.UserSign = Properties.Settings.Default.Usuario;
                    PurchaseInvoice.UpdateDate = fechaActual.GetFechaActual();
                    PurchaseInvoice.DocSubType = "--";

                    listPurchaseInvoice.Add(PurchaseInvoice);

                    var result3 = cn.UpdateEntradaMercancia(listPurchaseInvoice);

                    if (result3.Item2 == 1)
                    {
                        LimpiarCampos();

                        btnCrear.Content = "OK";

                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Operacion se realizo exitosamente", Brushes.LightGreen, Brushes.Black, "001-interface.png");
                        

                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage(result3.Item1, Brushes.Red, Brushes.White, "003-interface-2.png");

                    }

                    break;
            }
        }

        private void DeletedAllInsertItem(bool? OPDN, bool? PDN1, bool? OJDT, bool? JDT1, bool? OINM)
        {
            if (PDN1 != null)
            {
                var deletePurchaseInvoiceLines = cn.DeleteEntradaMercanciaLines(DocEntryDeleted);

                if (deletePurchaseInvoiceLines.Item2 != null)
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se eliminaron todas las linesa de la factura de compras : " + DocEntryDeleted, Brushes.LightBlue, Brushes.Black, "003-interface-2.png");
                }
            }

            if (OPDN == true)
            {
                var deletePurchaseInvoice = cn.DeleteEntradaMercancia(DocEntryDeleted);

                if (deletePurchaseInvoice.Item2 != null)
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se elimino la factura de proveedores : " + DocEntryDeleted, Brushes.LightBlue, Brushes.Black, "003-interface-2.png");
                }
            }

            
            if (OINM != null)
            {
                var deleteOINM = cn.DeleteOINM(DocEntryDeleted);

                if (deleteOINM.Item2 != null)
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se eliminaron todos los registros de compras asociados a la factura de proveedores : " + DocEntryDeleted, Brushes.LightBlue, Brushes.Black, "003-interface-2.png");
                }
            }

            if (JDT1 != null)
            {
                var deleteJournalEntryLinesSalesInvoice = cn.DeleteJournalEntryLines(TransId);

                if (deleteJournalEntryLinesSalesInvoice.Item2 != null)
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se eliminaron todas las lineas del asiento asociado a la factura de proveedores : " + DocEntryDeleted, Brushes.LightBlue, Brushes.Black, "003-interface-2.png");
                }
            }

            if (OJDT == true)
            {
                var deleteJournalEntrySalesInvoice = cn.DeleteJournalEntry(TransId);

                if (deleteJournalEntrySalesInvoice.Item2 != null)
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se elimino el asiento asociado a la factura de proveedores : " + DocEntryDeleted, Brushes.LightBlue, Brushes.Black, "003-interface-2.png");
                }
            }
        }


        private DataTable CreateDatatableJournalEntryLines(DataTable dtPurchase)
        {
            dtJournalEntry.Rows.Clear();

            int i = 1;

            //Consulta Tabla Facturas
            foreach (DataRow row in dtPurchase.Rows)
            {
                DataRow newRow = dtJournalEntry.NewRow();

                dtJournalEntry.Rows.Add(newRow);

                if (String.IsNullOrWhiteSpace(row["AcctCode"].ToString()) == false)
                {
                    newRow["ShortName"] = row["AcctCode"].ToString();

                    newRow["Account"] = row["AcctCode"].ToString();

                    var accountTransferAc = cn.GetAccountTransferAc(row["ItemCode"].ToString());

                    newRow["ContraAct"] = accountTransferAc.Item1;
                }

                if (String.IsNullOrWhiteSpace(row["LineTotal"].ToString()) == false)
                {
                    str = regex.Replace(row["LineTotal"].ToString(), String.Empty);

                    newRow["Debit"] = ConvertDecimalTwoPlaces(str);

                }

                if (String.IsNullOrWhiteSpace(row["TotalSumSy"].ToString()) == false)
                {
                    str = regex.Replace(row["TotalSumSy"].ToString(), String.Empty);

                    newRow["SYSDeb"] = ConvertDecimalTwoPlaces(str);
                }

                if (String.IsNullOrWhiteSpace(row["TotalFrgn"].ToString()) == false)
                {
                    str = regex.Replace(row["TotalFrgn"].ToString(), String.Empty);

                    newRow["FCDebit"] = ConvertDecimalTwoPlaces(str);
                }


                newRow["LineMemo"] = txtEntradaDiario.Text;

                //Create BP

                DataRow newRow3 = dtJournalEntry.NewRow();

                dtJournalEntry.Rows.Add(newRow3);

                newRow3["ShortName"] = newRow["ContraAct"];

                newRow3["Account"] = newRow["ContraAct"];

                newRow3["ContraAct"] = newRow["Account"];

                newRow3["Credit"] = newRow["Debit"];

                newRow3["SYSCred"] = newRow["SYSDeb"];

                newRow3["FCCredit"] = newRow["FCDebit"];

                newRow3["LineMemo"] = txtEntradaDiario.Text;

                i++;

            }

            return dtJournalEntry;
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

                GetEntradaMercancia(newListPurchase);

                var result = cn.FindEntradaMercanciaLines(DocEntry);

                if (result.Item2 == null)
                {
                    GetEntradaMercanciaLines(result.Item1);

                    btnCrear.Content = "OK";
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

                        GetEntradaMercancia(ventanaListBox.GetListDocument());

                        var result1 = cn.FindEntradaMercanciaLines(DocEntry);

                        if (result1.Item2 == null)
                        {
                            GetEntradaMercanciaLines(result1.Item1);

                            btnCrear.Content = "OK";
                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result1.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
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

            List<DocumentoCabecera> listPurchaseInvoice = new List<DocumentoCabecera>();

            DocumentoCabecera PurchaseInvoice = new DocumentoCabecera();

            List<DocumentoDetalle> listPurchaseInvoiceLines = new List<DocumentoDetalle>();

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
                        dtRetenciones = TablaRetencionImpuesto.GetRetenciones();

                        listPurchaseInvoice = CreateListPurchase();

                        var result5 = cn.InsertEntradaMercanciaPreliminar(listPurchaseInvoice);

                        if (result5.Item1 == 1)
                        {

                            var listPurchase = CreateListPurchaseLinesPreliminar(listPurchaseInvoice);

                            var result6 = cn.InsertEntradaMercanciaLinesPreliminar(listPurchase.Item1);

                            if (listPurchase.Item2 == result6.Item1)
                            {

                                bool listItemPurchase = CreateListItemPreliminar(listPurchaseInvoice, listPurchase.Item1);

                                if (listItemPurchase == true)
                                {

                                    //Create Journal Entry

                                    listaJournalEntry = CreateJournalEntry(listPurchaseInvoice);

                                    //Contruir asiento

                                    var result2 = cj.InsertJournalEntryPreliminar(listaJournalEntry);

                                    if (result2.Item1 == 1)
                                    {

                                        var listJournalEntryLines = CreateListJournalEntryLines(listaJournalEntry);


                                        listaJournalEntryLines = listJournalEntryLines.Item1;

                                        var result3 = cj.InsertJournalEntryLinesPreliminar(listJournalEntryLines.Item1);

                                        if (listJournalEntryLines.Item2 == result3.Item1)
                                        {
                                            sw = true;


                                        }
                                        else
                                        {
                                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: " + result3.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");

                                            sw = false;

                                            ListArticuloDetalleOld.Clear();

                                        }

                                    }
                                    else
                                    {
                                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: " + result2.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");

                                        sw = false;

                                        ListArticuloDetalleOld.Clear();

                                    }
                                }
                                else
                                {
                                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: ", Brushes.Red, Brushes.White, "003-interface-2.png");

                                    sw = false;

                                    ListArticuloDetalleOld.Clear();
                                }

                            }
                            else
                            {
                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: " + result6.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                                sw = false;
                            }

                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: " + result5.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                            sw = false;
                        }


                    }
                    catch (Exception ex)

                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error General: " + ex.Message, Brushes.Red, Brushes.White, "003-interface-2.png");

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

        private bool CreateListItemPreliminar(List<DocumentoCabecera> listPurchaseInvoice, List<DocumentoDetalle> listPurchaseInvoiceLines)
        {
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

            decimal DocRate = 0;

            decimal SysRate = 0;


            foreach (DocumentoCabecera PurchaseInvoice in listPurchaseInvoice)
            {
                DocNum = PurchaseInvoice.DocNum;
                DocDueDate = PurchaseInvoice.DocDueDate;
                TaxDate = PurchaseInvoice.TaxDate;
                CardCode = PurchaseInvoice.CardCode;
                CardName = PurchaseInvoice.CardName;
                Comments = PurchaseInvoice.Comments;
                DocRate = PurchaseInvoice.DocRate;
                SysRate = PurchaseInvoice.SysRate;
                JrnlMemo = PurchaseInvoice.JrnlMemo;
                UserSign = PurchaseInvoice.UserSign;
                UpdateDate = PurchaseInvoice.UpdateDate;

            }

            List<ArticuloDetalle> listItemPurchase = new List<ArticuloDetalle>();

            foreach (DocumentoDetalle PurchaseInvoiceLines in listPurchaseInvoiceLines)
            {
                ArticuloDetalle ItemPurchaseLines = new ArticuloDetalle();

                if (sw == true)
                {
                    ItemPurchaseLines.TransType = Convert.ToInt32(PurchaseInvoiceLines.ObjType);
                    ItemPurchaseLines.CreatedBy = PurchaseInvoiceLines.DocEntry;
                    ItemPurchaseLines.BASE_REF = DocNum.ToString();
                    ItemPurchaseLines.DocLineNum = PurchaseInvoiceLines.LineNum;
                    ItemPurchaseLines.DocDate = PurchaseInvoiceLines.DocDate;
                    ItemPurchaseLines.TaxDate = TaxDate;
                    ItemPurchaseLines.DocDueDate = DocDueDate;
                    ItemPurchaseLines.CardCode = CardCode;
                    ItemPurchaseLines.CardName = CardName;
                    ItemPurchaseLines.Comments = Comments;
                    ItemPurchaseLines.JrnlMemo = JrnlMemo;
                    ItemPurchaseLines.ItemCode = PurchaseInvoiceLines.ItemCode;
                    ItemPurchaseLines.Dscription = PurchaseInvoiceLines.Dscription;
                    ItemPurchaseLines.InQty = PurchaseInvoiceLines.Quantity * PurchaseInvoiceLines.NumPerMsr;
                    ItemPurchaseLines.OutQty = 0;
                    ItemPurchaseLines.Price = PurchaseInvoiceLines.Price;
                    ItemPurchaseLines.Currency = PurchaseInvoiceLines.Currency;
                    ItemPurchaseLines.Rate = DocRate;
                    ItemPurchaseLines.SysRate = SysRate;
                    ItemPurchaseLines.Type = 'E';
                    ItemPurchaseLines.UserSign = UserSign;
                    ItemPurchaseLines.CalcPrice = PurchaseInvoiceLines.LineTotal / (PurchaseInvoiceLines.Quantity * PurchaseInvoiceLines.NumPerMsr);
                    ItemPurchaseLines.OpenQty = PurchaseInvoiceLines.Quantity * PurchaseInvoiceLines.NumPerMsr;
                    ItemPurchaseLines.CreateDate = UpdateDate;
                    ItemPurchaseLines.CostMethod = 'F';
                    ItemPurchaseLines.TransValue = PurchaseInvoiceLines.LineTotal;
                    ItemPurchaseLines.OpenValue = ItemPurchaseLines.TransValue;
                    ItemPurchaseLines.InvntAct = PurchaseInvoiceLines.AcctCode;
                    var balance = cn.CalculateBalanceItemPreliminar(ItemPurchaseLines.ItemCode,true);
                    ItemPurchaseLines.Balance = balance.Item1 + ItemPurchaseLines.TransValue;

                    listItemPurchase.Add(ItemPurchaseLines);

                    var QueryItemPurchase = cn.InsertOINMPreliminar(listItemPurchase);

                    if (QueryItemPurchase.Item1 == 1)
                    {
                        GetFindTransaccion(listItemPurchase);

                        listItemPurchase.Clear();

                        sw = true;

                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + QueryItemPurchase.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                        sw = false;
                    }

                }
            }

            return sw;
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



        private void txtNroFactura_KeyUp(object sender, KeyEventArgs e)
        {

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
