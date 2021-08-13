using Entidades;
using Negocio;
using Negocio.Controlador_Informes_Fiscales;
using Negocio.ControladorCierrePeriodo;
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
    /// Lógica de interacción para CierrePeriodo.xaml
    /// </summary>
    public partial class CierrePeriodo : Window
    {
        ControladorBalance cn = new ControladorBalance();       

        ControladorLibros cl = new ControladorLibros();

        ControladorCierrePeriodo cp = new ControladorCierrePeriodo();

        ControladorTipoCambio ct = new ControladorTipoCambio();

       
        public TablaCierrePeriodo dtCierrePeriodo;

        private bool sw;

        public bool Sw { get => sw; set => sw = value; }

        private bool swSN;

        public bool SwSN { get => swSN; set => swSN = value; }

        DataTable dtAccount = new DataTable();
        
        public CierrePeriodo()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {

            App.textBox_GotFocus(sender, e);
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            App.textBox_LostFocus(sender, e);
        }

        private void comboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            App.comboBox_LostFocus(sender, e);
        }

        private void comboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            App.comboBox_GotFocus(sender, e);
        }

        private void txtSN_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Name == "txtDesdeSN")
            {
                dpDesdeSN.Background = Brushes.LightBlue;

                bdDesdeSN.Background = Brushes.LightBlue;

                txtDesdeSN.Background = Brushes.LightBlue;

                imgDesdeSN.Visibility = Visibility.Visible;
            }
            else if (textBox.Name == "txtHastaSN")
            {

                dpHastaSN.Background = Brushes.LightBlue;

                bdHastaSN.Background = Brushes.LightBlue;

                txtHastaSN.Background = Brushes.LightBlue;

                imgHastaSN.Visibility = Visibility.Visible;
            }
        }

        private void txtSN_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Name == "txtDesdeSN")
            {
                dpDesdeSN.Background = Brushes.White;

                bdDesdeSN.Background = Brushes.White;

                txtDesdeSN.Background = Brushes.White;

                imgDesdeSN.Visibility = Visibility.Hidden;
            }
            else if (textBox.Name == "txtHastaSN")
            {

                dpHastaSN.Background = Brushes.White;

                bdHastaSN.Background = Brushes.White;

                txtHastaSN.Background = Brushes.White;

                imgHastaSN.Visibility = Visibility.Hidden;
            }
        }

        private void txtCuenta_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;


            if (textBox.Name == "txtCuentaCierre")
            {
                dpCuentaCierre.Background = Brushes.LightBlue;

                bdCuentaCierre.Background = Brushes.LightBlue;

                txtCuentaCierre.Background = Brushes.LightBlue;

                imgCuentaCierre.Visibility = Visibility.Visible;
            }
            else if (textBox.Name == "txtCuentaArrastre")
            {

                dpCuentaArrastre.Background = Brushes.LightBlue;

                bdCuentaArrastre.Background = Brushes.LightBlue;

                txtCuentaArrastre.Background = Brushes.LightBlue;

                imgCuentaArrastre.Visibility = Visibility.Visible;
            }
        }

        private void txtCuenta_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Name == "txtCuentaCierre")
            {
                dpCuentaCierre.Background = Brushes.White;

                bdCuentaCierre.Background = Brushes.White;

                txtCuentaCierre.Background = Brushes.White;

                imgCuentaCierre.Visibility = Visibility.Hidden;
            }
            else if (textBox.Name == "txtCuentaArrastre")
            {

                dpCuentaArrastre.Background = Brushes.White;

                bdCuentaArrastre.Background = Brushes.White;

                txtCuentaArrastre.Background = Brushes.White;

                imgCuentaArrastre.Visibility = Visibility.Hidden;
            }
        }

        public void InicializacionBasica()
        {
            FindAllAcount();

            LoadComboBoxPeriodo();


        }

        private void CheckedBP()
        {
            cbxSN.IsChecked = true;
        }

        private void CheckedAccount()
        {
            cbxCuentas.IsChecked = true;
        }

        private void LoadComboBoxPeriodo()
        {
            var result1 = ct.ConsultaYears();

            if (result1.Item2 == null)
            {
                cbPeriodo.ItemsSource = null;
                cbPeriodo.ItemsSource = result1.Item1;

            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result1.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
            }
        }

        private void FindAllAcount()
        {
            var result = cn.FindAllAccount();

            if (result.Item2 == null)
            {
                GetAccount(result.Item1);
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
            }
        }

        private void GetAccount(DataTable listAccount)
        {
            dtAccount = SetSeleccionado(listAccount);

            dgCierrePeriodo.ItemsSource = dtAccount.DefaultView;

            dgCierrePeriodo.CanUserAddRows = false;

            dgCierrePeriodo.CanUserDeleteRows = false;

            dgCierrePeriodo.CanUserSortColumns = false;

            dgCierrePeriodo.IsReadOnly = false;
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

            foreach (DataRow row in dtAccount.Rows)
            {
                foreach (DataColumn column in dtAccount.Columns)
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

            foreach (DataRow row in dtAccount.Rows)
            {
                foreach (DataColumn column in dtAccount.Columns)
                {

                    if (column.ToString() == "Seleccionado")
                    {
                        row["Seleccionado"] = false;

                    }

                }

            }


        }

        public void LoadedWindow()
        {
            InicializacionBasica();

            CheckedBP();

            CheckedAccount();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InicializacionBasica();

            CreateTableCierrePeriodo();
           
        }

        private void CreateTableCierrePeriodo()
        {
            TablaCierrePeriodo tablaCierrePeriodo = new TablaCierrePeriodo();

            dtCierrePeriodo = tablaCierrePeriodo;
        }

        private void cbPeriodo_LostFocus(object sender, RoutedEventArgs e)
        {
            App.comboBox_LostFocus(sender, e);
        }

        private void cbPeriodo_GotFocus(object sender, RoutedEventArgs e)
        {
            App.comboBox_GotFocus(sender, e);
        }

        private void cbPeriodo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbPeriodo.SelectedIndex > -1)
            {
                if (String.IsNullOrWhiteSpace(cbPeriodo.SelectedValue.ToString()) == false)
                {
                    var result1 = cl.ConsultaPeriodosForYear(cbPeriodo.SelectedValue.ToString());

                    if (result1.Item2 == null)
                    {
                        cbDesde.ItemsSource = null;

                        cbHasta.ItemsSource = null;

                        cbDesde.ItemsSource = result1.Item1;

                        cbHasta.ItemsSource = result1.Item1;

                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result1.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                    }
                }
            }
           
        }

        private void cbxCuentas_Checked(object sender, RoutedEventArgs e)
        {
            InicializacionBasica();

            dgCierrePeriodo.Visibility = Visibility.Visible;
        }

        private void cbxCuentas_Unchecked(object sender, RoutedEventArgs e)
        {
            dtAccount.Rows.Clear();

            dgCierrePeriodo.ItemsSource = null;

            dgCierrePeriodo.Visibility = Visibility.Hidden;
            
        }

        private void cbxSN_Checked(object sender, RoutedEventArgs e)
        {
            lblSNDesde.Visibility = Visibility.Visible;
            lblSNHasta.Visibility = Visibility.Visible;
            txtDesdeSN.Text = "";
            txtHastaSN.Text = "";
            bdDesdeSN.Visibility = Visibility.Visible;
            bdHastaSN.Visibility = Visibility.Visible;
            dpDesdeSN.Visibility = Visibility.Visible;
            dpHastaSN.Visibility = Visibility.Visible;
            imgDesdeSN.Visibility = Visibility.Hidden;
            imgHastaSN.Visibility = Visibility.Hidden;
            txtDesdeSN.Visibility = Visibility.Visible;
            txtHastaSN.Visibility = Visibility.Visible;
        }

        private void cbxSN_Unchecked(object sender, RoutedEventArgs e)
        {
            lblSNDesde.Visibility = Visibility.Hidden;
            lblSNHasta.Visibility = Visibility.Hidden;
            txtDesdeSN.Text = "";
            txtHastaSN.Text = "";
            bdDesdeSN.Visibility = Visibility.Hidden;
            bdHastaSN.Visibility = Visibility.Hidden;
            dpDesdeSN.Visibility = Visibility.Hidden;
            dpHastaSN.Visibility = Visibility.Hidden;
            imgDesdeSN.Visibility = Visibility.Hidden;
            imgHastaSN.Visibility = Visibility.Hidden;
            txtDesdeSN.Visibility = Visibility.Hidden;
            txtHastaSN.Visibility = Visibility.Hidden;
        }

        private void chkSelectAll_Checked(object sender, RoutedEventArgs e)
        {
            SetChecked();
        }

        private void chkSelectAll_Unchecked(object sender, RoutedEventArgs e)
        {
            SetUnChecked();
        }

        private void btnEjecutar_Click(object sender, RoutedEventArgs e)
        {
            switch (btnEjecutar.Content.ToString())
            {
                case "Ejecutar":

                    DateTime starDate= StarDatetimeDay(cbDesde.SelectedValue.ToString());

                    DateTime endDate = EndDatetimeDay(cbHasta.SelectedValue.ToString());

                    var result = cp.ExecuteClosePeriod(starDate,endDate,txtCuentaArrastre.Text,txtCuentaCierre.Text,dtAccount,txtDesdeSN.Text,txtHastaSN.Text);

                    if (result.Item2 == null)
                    {

                        if (result.Item1.Rows.Count >= 1)
                        {
                            dtCierrePeriodo.ClearDatatable();

                            dtCierrePeriodo.Show();

                            dtCierrePeriodo.SetDatePicker(starDate, endDate);                          

                            dtCierrePeriodo.GetAccountResult(result.Item1);

                            dtCierrePeriodo.SetAccounts(txtCuentaArrastre.Text, txtCuentaCierre.Text);
                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se encontraron resultados con los parametros establecidos: " + result.Item2, Brushes.LightBlue, Brushes.Black, "002-interface-1.png");
                        }
                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                    }

                    break;

            }
        }

       

        private DateTime StarDatetimeDay(string dateString)
        {
            string dateForYear = dateString;

            string dateForMonth = dateString;

            string year= dateForYear.Substring(0, 4); //get year

            string month = dateForMonth.Substring(5); //get month

            DateTime Date = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), 1);

            return Date;

        }

        private DateTime EndDatetimeDay(string dateString)
        {
            string dateForYear = dateString;

            string dateForMonth = dateString;

            string year = dateForYear.Substring(0, 4); //get year

            string month = dateForMonth.Substring(5); //get month

            int endDay = DateTime.DaysInMonth(Convert.ToInt32(year), Convert.ToInt32(month));

            DateTime Date = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), endDay);

            return Date;

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void imgCuentaArrastre_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Sw = true;

            var result = cl.ConsultaCuentasNoAsociadas();

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

                        if (Sw == false)
                        {
                            txtCuentaCierre.Background = Brushes.White;

                            dpCuentaCierre.Background = Brushes.White;

                            bdCuentaCierre.Background = Brushes.White;

                            imgCuentaCierre.Visibility = Visibility.Hidden;

                           
                        }
                        else if (Sw == true)
                        {

                            txtCuentaArrastre.Background = Brushes.White;

                            dpCuentaArrastre.Background = Brushes.White;

                            bdCuentaArrastre.Background = Brushes.White;

                            imgCuentaArrastre.Visibility = Visibility.Hidden;

                        }

                    }
                }
            }
        }

        private void GetAcctCode(List<Cuenta> listaCuenta)
        {
            foreach (Cuenta cuenta in listaCuenta)
            {
                if (Sw == false)
                {
                    txtCuentaCierre.Text = cuenta.AcctCode;

                    lblCuentaCierreNombre.Visibility = Visibility.Visible;

                    lblCuentaCierreNombre.Text = cuenta.AcctName;
                }
                else if (Sw == true)
                {
                    txtCuentaArrastre.Text = cuenta.AcctCode;

                    lblCuentaArrastreNombre.Visibility = Visibility.Visible;

                    lblCuentaArrastreNombre.Text= cuenta.AcctName;
                }
                    

            }
        }

        private void imgCuentaCierre_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Sw = false;

            var result = cl.ConsultaCuentasNoAsociadas();

            if (result.Item2 == null)
            {
                RecorreListaAccount(result.Item1);
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
            }
        }

        private void imgHastaSN_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SwSN = true;

            var result = cn.FindBP();

            if (result.Item2 == null)
            {
                RecorreListaSN(result.Item1);
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
            }
        }

        private void imgDesdeSN_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SwSN = false;

            var result = cn.FindBP();

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


            }
            else if (listSuppliers.Count == 0)
            {

                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se encontro ningun resultado con los parametros establecidos", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");

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

                    }
                    else
                    {

                        GetSocioNegocio(ventanaListBox.GetListSN());

                        if (SwSN == false)
                        {
                            txtDesdeSN.Background = Brushes.White;

                            dpDesdeSN.Background = Brushes.White;

                            bdDesdeSN.Background = Brushes.White;
                            
                        }
                        else if (SwSN == true)
                        {

                            txtHastaSN.Background = Brushes.White;

                            dpHastaSN.Background = Brushes.White;

                            bdHastaSN.Background = Brushes.White;
                            
                        }

                    }
                }
            }
        }

        private void GetSocioNegocio(List<SocioNegocio> listSuppliers)
        {
            foreach (SocioNegocio Suppliers in listSuppliers)
            {
                if (SwSN == false)
                {
                    txtDesdeSN.Text = Suppliers.CardCode;
                }
                else if (SwSN == true)
                {

                    txtHastaSN.Text = Suppliers.CardCode;
                }


            }
        }

        private void seleccionado_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void seleccionado_Unchecked(object sender, RoutedEventArgs e)
        {

        }
    }
}
