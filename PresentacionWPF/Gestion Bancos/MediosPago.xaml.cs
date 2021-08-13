using System;
using System.Collections.Generic;
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
    /// Lógica de interacción para MediosPago.xaml
    /// </summary>
    public partial class MediosPago : Document
    {
        public static List<Payment> ListaMediosPago = new List<Payment>();

        private string businessPartner;

        private string importeTotal;

        private string saldoVencido;

        private string importeTotalME;

        private string saldoVencidoME;

        private decimal rate;

        private decimal rateFC;

        private string selectedDate;

        private string currency;

        private string str;

        private decimal number;
        public string BusinessPartner { get => businessPartner; set => businessPartner = value; }
        public string ImporteTotal { get => importeTotal; set => importeTotal = value; }
        public string SaldoVencido { get => saldoVencido; set => saldoVencido = value; }
        public string ImporteTotalME { get => importeTotalME; set => importeTotalME = value; }
        public string SaldoVencidoME { get => saldoVencidoME; set => saldoVencidoME = value; }
        public decimal Rate { get => rate; set => rate = value; }
        public decimal RateFC { get => rateFC; set => rateFC = value; }
        public string SelectedDate { get => selectedDate; set => selectedDate = value; }
        public string Currency { get => currency; set => currency = value; }
        public string Str { get => str; set => str = value; }
        public decimal Amount { get => amount; set => amount = value; }
        public decimal Number { get => number; set => number = value; }
        public string CurrCode { get => currCode; set => currCode = value; }

        private string currCode;

        private decimal amount;

        ControladorMediosPago cn = new ControladorMediosPago();

        public MediosPago()
        {
            InitializeComponent();
        }

        private void comboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            App.comboBox_LostFocus(sender, e);
        }

        private void comboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            App.comboBox_GotFocus(sender, e);
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            App.textBox_LostFocus(sender, e);

        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            App.textBox_GotFocus(sender, e);
        }

        private void textBox_LostFocus1(object sender, RoutedEventArgs e)
        {
            App.textBox_LostFocus(sender, e);

            TextBox txt = (TextBox)sender;

            Str= regex.Replace(txt.Text, String.Empty);

            Number = ConvertDecimalTwoPlaces(Str);

            txt.Text = Currency + " " + Number;

        }

        private void textBox_GotFocus1(object sender, RoutedEventArgs e)
        {
            App.textBox_GotFocus(sender, e);           
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            ListaMediosPago.Clear();

            List<Payment> listaMediosPagos = new List<Payment>();

            Payment mediosPago = new Payment();

            switch (btnOK.Content.ToString())
            {
                case "OK":

                    mediosPago.CashAcct = txtCuentaE.Text;
                    mediosPago.TrsfrAcct = txtCuentaT.Text;
                    mediosPago.TrsfrDate = dpFechaTransferencia.SelectedDate;
                    mediosPago.TrsfrRef = txtReferencia.Text;                    

                    if (cbMoneda.SelectedValue.ToString()==Properties.Settings.Default.MainCurrency)
                    {
                        Str = regex.Replace(txtTotal1.Text, String.Empty);
                        mediosPago.CashSum = ConvertDecimalTwoPlaces(Str);
                        mediosPago.CashSumFC = 0;
                        mediosPago.CashSumSy = mediosPago.CashSum / Rate;
                        
                        Str= regex.Replace(txtTotal.Text, String.Empty);
                        mediosPago.TrsfrSum= ConvertDecimalTwoPlaces(Str);
                        mediosPago.TrsfrSumFC = 0;
                        mediosPago.TrsfrSumSy= mediosPago.TrsfrSum / Rate;                       

                        if (String.IsNullOrWhiteSpace(txtTotal1.Text) == false)
                        {
                            txtPagado.Text = txtTotal1.Text;
                            mediosPago.DocTotal = mediosPago.CashSum;
                            mediosPago.DocTotalFC = mediosPago.CashSumFC;
                            mediosPago.DocTotalSy = mediosPago.CashSumSy;

                        }
                        else if (String.IsNullOrWhiteSpace(txtTotal.Text) == false)
                        {
                            txtPagado.Text = txtTotal.Text;
                            mediosPago.DocTotal = mediosPago.TrsfrSum;
                            mediosPago.DocTotalFC = mediosPago.TrsfrSumFC;
                            mediosPago.DocTotalSy = mediosPago.TrsfrSumSy;
                        }

                        listaMediosPagos.Add(mediosPago);

                        ListaMediosPago = listaMediosPagos;

                    }
                    else
                    {
                        Str = regex.Replace(txtTotal1.Text, String.Empty);
                        mediosPago.CashSumFC = ConvertDecimalTwoPlaces(Str);
                        mediosPago.CashSum = mediosPago.CashSumFC * RateFC;
                        mediosPago.CashSumSy = mediosPago.CashSum/Rate;

                        Str = regex.Replace(txtTotal.Text, String.Empty);
                        mediosPago.TrsfrSumFC = ConvertDecimalTwoPlaces(Str);
                        mediosPago.TrsfrSum = mediosPago.TrsfrSumFC*RateFC;
                        mediosPago.TrsfrSumSy = mediosPago.TrsfrSum/Rate;                       

                        if (String.IsNullOrWhiteSpace(txtTotal1.Text) == false)
                        {
                            txtPagado.Text = txtTotal1.Text;
                            mediosPago.DocTotal = mediosPago.CashSum;
                            mediosPago.DocTotalFC = mediosPago.CashSumFC;
                            mediosPago.DocTotalSy = mediosPago.CashSumSy;

                        }
                        else if (String.IsNullOrWhiteSpace(txtTotal.Text) == false)
                        {
                            txtPagado.Text = txtTotal.Text;
                            mediosPago.DocTotal = mediosPago.TrsfrSum;
                            mediosPago.DocTotalFC = mediosPago.TrsfrSumFC;
                            mediosPago.DocTotalSy = mediosPago.TrsfrSumSy;
                        }

                        listaMediosPagos.Add(mediosPago);

                        ListaMediosPago = listaMediosPagos;

                    }

                    this.Hide();

                    break;
            }
        }

        private void btnWindow_Close(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        public void ClearMediosPago()
        {
            ListaMediosPago.Clear();
        }

        public List<Payment> GetListMediosPago()
        {
            return ListaMediosPago;
        }

        public void SetMediosPago(List<Payment> listMediosPago)
        {
            ListaMediosPago = listMediosPago;
        }

        public void DisabledButton()
        {
            btnOK.Background = Brushes.LightGray;

            btnOK.IsEnabled = false;
        }

        public void LoadCurrency(string BP)
        {
            BusinessPartner = BP;

            var result1 = cn.FindSupplierCurrency(BusinessPartner);

            if (result1.Item2 == null)
            {
                cbMoneda.ItemsSource = result1.Item1;
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result1.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
            }
        }

        

        public void SetAmount(string importeTotal, string saldoVencido, decimal rate, decimal rateFC)
        {
            ImporteTotal = importeTotal;

            SaldoVencido = saldoVencido;

            txtImporteTotal.Text = ImporteTotal;

            txtSaldoVencido.Text = SaldoVencido;

            Rate = rate;

            RateFC = rateFC;
        }

        public void EnabledFields()
        {
            cbMoneda.IsReadOnly = false;
            cbMoneda.Background = Brushes.White;
            txtCuentaT.IsReadOnly = false;
            txtCuentaT.Background = Brushes.White;
            txtReferencia.IsReadOnly = false;
            txtReferencia.Background = Brushes.White;
            txtTotal.IsReadOnly = false;
            txtTotal.Background = Brushes.White;
            txtImporteTotal.IsReadOnly = false;
            txtImporteTotal.Background = Brushes.White;
            txtSaldoVencido.IsReadOnly = false;
            txtSaldoVencido.Background = Brushes.LightGray;
            txtPagado.IsReadOnly = false;
            txtPagado.Background = Brushes.LightGray;
            txtCuentaE.IsReadOnly = false;
            txtCuentaE.Background = Brushes.White;
            txtGastosBancarios.IsReadOnly = false;
            txtGastosBancarios.Background = Brushes.White;
            txtTotal1.IsReadOnly = false;
            txtTotal1.Background = Brushes.White;            
            ReestablecerDatetime(dpFechaTransferencia);
        }
        public void DisabledFields()
        {
            cbMoneda.IsReadOnly = true;
            cbMoneda.Background = Brushes.LightGray;
            txtCuentaT.IsReadOnly = true;
            txtCuentaT.Background = Brushes.LightGray;
            txtReferencia.IsReadOnly = true;
            txtReferencia.Background = Brushes.LightGray;
            txtTotal.IsReadOnly = true;
            txtTotal.Background = Brushes.LightGray;
            txtImporteTotal.IsReadOnly = true;
            txtImporteTotal.Background = Brushes.LightGray;
            txtSaldoVencido.IsReadOnly = true;
            txtSaldoVencido.Background = Brushes.LightGray;
            txtPagado.IsReadOnly = true;
            txtPagado.Background = Brushes.LightGray;
            txtCuentaE.IsReadOnly = true;
            txtCuentaE.Background = Brushes.LightGray;
            txtTotal1.IsReadOnly = true;
            txtTotal1.Background = Brushes.LightGray;
            txtGastosBancarios.IsReadOnly = true;
            txtGastosBancarios.Background = Brushes.LightGray;
            ReadOnlyDatetime(dpFechaTransferencia);
            establecerDatetime(dpFechaTransferencia);
        }

        public void ReestablecerDatetime(DatePicker dpk)
        {
            TextBox tb = (TextBox)dpk.Template.FindName("PART_TextBox", dpk);
            tb.IsReadOnly = false;
            tb.Background = Brushes.White;

        }

        public void establecerDatetime(DatePicker dpk)
        {
            TextBox tb = (TextBox)dpk.Template.FindName("PART_TextBox", dpk);

            tb.BorderThickness = new Thickness(1);
            tb.Background = Brushes.LightBlue;
            tb.BorderBrush = Brushes.Gray;

        }

        private void ReadOnlyDatetime(DatePicker dp)
        {
            TextBox tb = (TextBox)dp.Template.FindName("PART_TextBox", dp);

            tb.IsReadOnly = true;
            tb.Background = Brushes.LightGray;
        }

        public void SetCurrency(string mainCurrency)
        {
            cbMoneda.SelectedValue = mainCurrency;
        }

        public void SetCurrency(string currCode, string currName)
        {
            cbMoneda.ItemsSource = cn.CreateCurrencyTable(currCode, currName);
            cbMoneda.SelectedValue = currCode;
            CurrCode = currCode;
        }

        public void SetFields(List<Payment> listaPayment)
        {
            HideUnhideFields(CurrCode);

            foreach (Payment payment in listaPayment)
            {
                txtCuentaT.Text = payment.TrsfrAcct;
                dpFechaTransferencia.SelectedDate = payment.TrsfrDate;
                txtReferencia.Text = payment.TrsfrRef;

                if (payment.DocCurr == Properties.Settings.Default.MainCurrency)
                {
                    if (payment.TrsfrSum == 0)
                    {
                        txtTotal.Text = "";
                    }
                    else
                    {
                        txtTotal.Text = payment.DocCurr + " " + payment.TrsfrSum;
                    }

                    if (payment.CashSum == 0)
                    {
                        txtTotal1.Text = "";
                    }
                    else
                    {
                        txtTotal1.Text = payment.DocCurr + " " + payment.CashSum;
                    }

                    txtImporteTotal.Text = payment.DocCurr + " " + payment.DocTotal;
                    txtPagado.Text = payment.DocCurr + " " + payment.DocTotal;
                    txtImporteTotal_ME.Text = "";


                }
                else
                {
                    if (payment.TrsfrSumFC == 0)
                    {
                        txtTotal.Text = "";
                    }
                    else
                    {
                        txtTotal.Text = payment.DocCurr + " " + payment.TrsfrSumFC;
                    }

                    if (payment.CashSumFC == 0)
                    {
                        txtTotal1.Text = "";
                    }
                    else
                    {
                        txtTotal1.Text = payment.DocCurr + " " + payment.CashSumFC;
                    }

                    txtImporteTotal.Text = Properties.Settings.Default.MainCurrency + " " + payment.DocTotal;
                    txtImporteTotal_ME.Text= payment.DocCurr + " " + payment.DocTotalFC;
                    txtPagado.Text = payment.DocCurr + " " + payment.DocTotalFC;

                }


               
                
            }
        }

        public void HideUnhideFields(string currCode)
        {
            if (currCode == Properties.Settings.Default.MainCurrency)
            {
                txtCuentaE.Visibility = Visibility.Visible;
                txtCuentaT.Visibility = Visibility.Visible;
                txtGastosBancarios.Visibility = Visibility.Visible;
                dpFechaTransferencia.Visibility = Visibility.Visible;
                txtReferencia.Visibility = Visibility.Visible;
                txtTotal.Visibility = Visibility.Visible;
                txtTotal1.Visibility = Visibility.Visible;
                txtImporteTotal.Visibility = Visibility;
                txtImporteTotal_ME.Visibility = Visibility.Hidden;
                txtSaldoVencido.Visibility = Visibility.Visible;
                txtSaldoVencido_ME.Visibility = Visibility.Hidden;
                txtPagado.Visibility = Visibility.Visible;
            }
            else
            {
                txtCuentaE.Visibility = Visibility.Visible;
                txtCuentaT.Visibility = Visibility.Visible;
                txtGastosBancarios.Visibility = Visibility.Visible;
                dpFechaTransferencia.Visibility = Visibility.Visible;
                txtReferencia.Visibility = Visibility.Visible;
                txtTotal.Visibility = Visibility.Visible;
                txtTotal1.Visibility = Visibility.Visible;
                txtImporteTotal.Visibility = Visibility;
                txtImporteTotal_ME.Visibility = Visibility.Visible;
                txtSaldoVencido.Visibility = Visibility.Visible;
                txtSaldoVencido_ME.Visibility = Visibility.Visible;
                txtPagado.Visibility = Visibility.Visible;
            }
        }

        public string GetCurrency()
        {
            return Currency;
        }

        private void cbMoneda_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbMoneda.SelectedIndex > -1)
            {
                Currency = cbMoneda.SelectedValue.ToString();

                ClearMontoPago();

                if (cbMoneda.SelectedValue.ToString() == Properties.Settings.Default.MainCurrency)
                {
                    txtImporteTotal_ME.Visibility = Visibility.Hidden;
                    txtSaldoVencido_ME.Visibility = Visibility.Hidden;
                    cbMoneda.SelectedValue = Properties.Settings.Default.MainCurrency;
                }
                else
                {
                    if (FindRateFC() == true)
                    {
                        txtImporteTotal_ME.Visibility = Visibility.Visible;

                        txtSaldoVencido_ME.Visibility = Visibility.Visible;

                        Str = regex.Replace(txtSaldoVencido.Text, String.Empty);

                        Amount=ConvertDecimalTwoPlaces(Str);

                        Amount = Amount / RateFC;

                        Amount = ConvertDecimalTwoPlaces(Amount);

                        txtImporteTotal_ME.Text = Currency + " " + Amount.ToString();

                        txtSaldoVencido_ME.Text = txtImporteTotal_ME.Text;


                    }
                    else
                    {
                        txtImporteTotal_ME.Visibility = Visibility.Hidden;
                        txtSaldoVencido_ME.Visibility = Visibility.Hidden;
                        cbMoneda.SelectedValue = Properties.Settings.Default.MainCurrency;
                    }
                }
            }
        }

        private void ClearMontoPago()
        {
            txtTotal.Text = "";
            txtTotal1.Text = "";
        }

        private void ShowTipoCambio()
        {
            TipoCambio ventanaTipoCambio = new TipoCambio();

            DoubleAnimation animation = new DoubleAnimation(0, 1,
                                 (Duration)TimeSpan.FromSeconds(1));
            ventanaTipoCambio.BeginAnimation(UIElement.OpacityProperty, animation);

            ventanaTipoCambio.ShowDialog();

        }


        private bool FindRateFC()
        {
            bool sw=false;
            
            if (String.IsNullOrWhiteSpace(SelectedDate) == false)
            {
                var result = cn.FindRateFCCurrency(Convert.ToDateTime(SelectedDate),Currency);

                if (result.Item2 == null)
                {
                    RateFC = result.Item1;

                    if (RateFC == 0)
                    {
                        ShowTipoCambio();

                        var result1 = cn.FindRateFCCurrency(Convert.ToDateTime(SelectedDate), Currency);

                        if (result1.Item2 == null)
                        {
                            RateFC = result1.Item1;

                            if (RateFC == 0)
                            {
                                sw = false;
                            }
                            else
                            {
                                sw = true;
                            }
                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result1.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                        }

                    }
                    else
                    {
                        sw = true;
                    }
                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                }



            }

            return sw;
        }

        public void SetFecha(DateTime? selectedDate)
        {
            DateTime? fecha = selectedDate;

            SelectedDate = String.Format("{0:yyyy/MM/dd}", fecha);           
        }

        private void txtCuentaT_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void imgCuentaT_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var result = cn.ConsultaCuentasNoAsociadas();

            if (result.Item2 == null)
            {
                RecorreListaAccount(result.Item1);
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
            }
        }

        private void txt_GotFocus(object sender, RoutedEventArgs e)
        {
            dpCuentaT.Background = Brushes.LightBlue;
            bdCuentaT.Background = Brushes.LightBlue;
            imgCuentaT.Visibility = Visibility.Visible;
            txtCuentaT.Background = Brushes.LightBlue;
        }

        private void txt_LostFocus(object sender, RoutedEventArgs e)
        {
            dpCuentaT.Background = Brushes.White;
            bdCuentaT.Background = Brushes.White;
            imgCuentaT.Visibility = Visibility.Hidden;
            txtCuentaT.Background = Brushes.White;
        }

        private void RecorreListaAccount(List<Cuenta> listAccountResultante)
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
                        GetAcctCode(ventanaListaCuentaAsociada.GetListAccount());
                    }
                }
            }
        }

        private void RecorreListaAccount1(List<Cuenta> listAccountResultante)
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
                        GetAcctCode1(ventanaListaCuentaAsociada.GetListAccount());
                    }
                }
            }
        }

        private void GetAcctCode(List<Cuenta> listaCuenta)
        {
            foreach (Cuenta cuenta in listaCuenta)
            {
                txtCuentaT.Text = cuenta.AcctCode;

            }
        }

        private void GetAcctCode1(List<Cuenta> listaCuenta)
        {
            foreach (Cuenta cuenta in listaCuenta)
            {
                txtCuentaE.Text = cuenta.AcctCode;

            }
        }

        private void imgCuentaE_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var result = cn.ConsultaCuentasNoAsociadas();

            if (result.Item2 == null)
            {
                RecorreListaAccount1(result.Item1);
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
            }
        }

        private void txtCuentaE_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtCuentaE_GotFocus(object sender, RoutedEventArgs e)
        {
            dpCuentaE.Background = Brushes.LightBlue;
            bdCuentaT_Copy.Background = Brushes.LightBlue;
            imgCuentaE.Visibility = Visibility.Visible;
            txtCuentaE.Background= Brushes.LightBlue;
        }

        private void txtCuentaE_LostFocus(object sender, RoutedEventArgs e)
        {
            dpCuentaE.Background = Brushes.White;
            bdCuentaT_Copy.Background = Brushes.White;
            txtCuentaE.Background = Brushes.White;
            imgCuentaE.Visibility = Visibility.Hidden;
        }

        private void txtTotal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.B && Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                if (Currency == Properties.Settings.Default.MainCurrency)
                {
                    txtTotal.Text =  txtImporteTotal.Text;
                }
                else
                {
                    txtTotal.Text = txtImporteTotal_ME.Text;
                }
                
               
            }
        }

        private void txtTotal1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.B && Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                if (Currency == Properties.Settings.Default.MainCurrency)
                {
                    txtTotal1.Text =  txtImporteTotal.Text;
                }
                else
                {
                    txtTotal1.Text =  txtImporteTotal_ME.Text;
                }
               
            }
        }

        private void tabEfectivo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            txtCuentaT.Text = "";
            txtTotal.Text = "";
            dpFechaTransferencia.SelectedDate = null;
            txtReferencia.Text = "";
        }

        private void tabTransferencia_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            txtCuentaE.Text = "";
            txtTotal1.Text = "";
        }
    }
}
