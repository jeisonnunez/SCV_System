using Entidades;
using Negocio;
using Negocio.Controlador_Finanzas;
using Negocio.Controlador_Informes_Fiscales;
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
    /// Lógica de interacción para DiferenciaConversion.xaml
    /// </summary>
    public partial class DiferenciaConversion : Window
    {
        ControladorBalance cn = new ControladorBalance();        

        ControladorLibros cl = new ControladorLibros();

        ControladorDiferenciaConversion cd = new ControladorDiferenciaConversion();


        public TablaDiferenciaConversion dtDiferenciaConversion;

        private int sw;

        public int Sw { get => sw; set => sw = value; }

        private bool swSN;

        public bool SwSN { get => swSN; set => swSN = value; }

        DataTable dtAccount = new DataTable();
        public DiferenciaConversion()
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

        private void btnEjecutar_Click(object sender, RoutedEventArgs e)
        {
            switch (btnEjecutar.Content.ToString())
            {
                case "Ejecutar":

                    var result = cd.ExecuteExchangeConversion(dtAccount, txtDesdeSN.Text, txtHastaSN.Text, dpFechaEjecucion.SelectedDate);

                    if (result.Item2 == null)
                    {

                        if (result.Item1.Rows.Count >= 1)
                        {
                            dtDiferenciaConversion.ClearDatatable();

                            dtDiferenciaConversion.Show();

                            dtDiferenciaConversion.SetDatePicker(dpFechaEjecucion.SelectedDate);

                            dtDiferenciaConversion.GetAccountResult(result.Item1);

                            dtDiferenciaConversion.SetAccounts(txtCuentaBeneficioCliente.Text, txtCuentaBeneficioProveedores.Text, txtCuentaBeneficioCuenta.Text, txtCuentaPerdidaCliente.Text, txtCuentaPerdidaProveedores.Text, txtCuentaPerdidaCuenta.Text);
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
        private void seleccionado_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void seleccionado_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void btnWindow_Close(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        private void cbxCuentas_Checked(object sender, RoutedEventArgs e)
        {
            InicializacionBasica();

            dgDiferenciaTipoCambio.Visibility = Visibility.Visible;
        }

        private void InicializacionBasica()
        {
            FindAllAccount();           
        }

        
        private void FindAllAccount()
        {
            var result = cn.FindAllAccountReal();

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

            dgDiferenciaTipoCambio.ItemsSource = dtAccount.DefaultView;

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

        private void CheckedBP()
        {
            cbxSN.IsChecked = true;
        }

        private void CheckedAccount()
        {
            cbxCuentas.IsChecked = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InicializacionBasica();

            CreateTableDiferenciaConversion();

        }

        private void CreateTableDiferenciaConversion()
        {
            TablaDiferenciaConversion tablaDiferenciaConversion = new TablaDiferenciaConversion();

            dtDiferenciaConversion = tablaDiferenciaConversion;
        }

        private void cbxCuentas_Unchecked(object sender, RoutedEventArgs e)
        {
            dtAccount.Rows.Clear();

            dgDiferenciaTipoCambio.ItemsSource = null;

            dgDiferenciaTipoCambio.Visibility = Visibility.Hidden;

        }

        private void imgCuentaBeneficioCliente_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Sw = 0;

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

        private void imgCuentaBeneficioProveedores_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Sw = 1;

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

                        if (Sw == 0)
                        {
                            txtCuentaBeneficioCliente.Background = Brushes.White;

                            dpCuentaBeneficioCliente.Background = Brushes.White;

                            bdCuentaBeneficioCliente.Background = Brushes.White;

                            imgCuentaBeneficioCliente.Visibility = Visibility.Hidden;


                        }
                        else if (Sw == 1)
                        {

                            txtCuentaBeneficioProveedores.Background = Brushes.White;

                            dpCuentaBeneficioProveedores.Background = Brushes.White;

                            bdCuentaBeneficioProveedores.Background = Brushes.White;

                            imgCuentaBeneficioProveedores.Visibility = Visibility.Hidden;

                        }

                        else if (Sw == 2)
                        {

                            txtCuentaBeneficioCuenta.Background = Brushes.White;

                            dpCuentaBeneficioCuenta.Background = Brushes.White;

                            bdCuentaBeneficioCuenta.Background = Brushes.White;

                            imgCuentaBeneficioCuenta.Visibility = Visibility.Hidden;

                        }

                        else if (Sw == 3)
                        {

                            txtCuentaPerdidaCliente.Background = Brushes.White;

                            dpCuentaPerdidaCliente.Background = Brushes.White;

                            bdCuentaPerdidaCliente.Background = Brushes.White;

                            imgCuentaPerdidaCliente.Visibility = Visibility.Hidden;

                        }

                        else if (Sw == 4)
                        {

                            txtCuentaPerdidaProveedores.Background = Brushes.White;

                            dpCuentaPerdidaProveedores.Background = Brushes.White;

                            bdCuentaPerdidaProveedores.Background = Brushes.White;

                            imgCuentaPerdidaProveedores.Visibility = Visibility.Hidden;

                        }

                        else if (Sw == 5)
                        {

                            txtCuentaPerdidaCuenta.Background = Brushes.White;

                            dpCuentaPerdidaCuenta.Background = Brushes.White;

                            bdCuentaPerdidaCuenta.Background = Brushes.White;

                            imgCuentaPerdidaCuenta.Visibility = Visibility.Hidden;

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
                    txtCuentaBeneficioCliente.Text = cuenta.AcctCode;


                }
                else if (Sw == 1)
                {
                    txtCuentaBeneficioProveedores.Text = cuenta.AcctCode;


                }

                else if (Sw == 2)
                {
                    txtCuentaBeneficioCuenta.Text = cuenta.AcctCode;


                }

                else if (Sw == 3)
                {
                    txtCuentaPerdidaCliente.Text = cuenta.AcctCode;


                }

                else if (Sw == 4)
                {
                    txtCuentaPerdidaProveedores.Text = cuenta.AcctCode;


                }

                else if (Sw == 5)
                {
                    txtCuentaPerdidaCuenta.Text = cuenta.AcctCode;


                }




            }
        }

        private void imgCuentaBeneficioCuenta_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Sw = 2;

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

        private void imgCuentaPerdidaCliente_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Sw = 3;

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

        private void imgCuentaPerdidaProveedores_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Sw = 4;

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

        private void imgCuentaPerdidaCuenta_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Sw = 5;

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

        private void txtCuenta_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;


            if (textBox.Name == "txtCuentaBeneficioCliente")
            {
                dpCuentaBeneficioCliente.Background = Brushes.LightBlue;

                bdCuentaBeneficioCliente.Background = Brushes.LightBlue;

                txtCuentaBeneficioCliente.Background = Brushes.LightBlue;

                imgCuentaBeneficioCliente.Visibility = Visibility.Visible;
            }
            else if (textBox.Name == "txtCuentaBeneficioProveedores")
            {

                dpCuentaBeneficioProveedores.Background = Brushes.LightBlue;

                bdCuentaBeneficioProveedores.Background = Brushes.LightBlue;

                txtCuentaBeneficioProveedores.Background = Brushes.LightBlue;

                imgCuentaBeneficioProveedores.Visibility = Visibility.Visible;
            }

            else if (textBox.Name == "txtCuentaBeneficioCuenta")
            {

                dpCuentaBeneficioCuenta.Background = Brushes.LightBlue;

                bdCuentaBeneficioCuenta.Background = Brushes.LightBlue;

                txtCuentaBeneficioCuenta.Background = Brushes.LightBlue;

                imgCuentaBeneficioCuenta.Visibility = Visibility.Visible;
            }

            else if (textBox.Name == "txtCuentaPerdidaCliente")
            {

                dpCuentaPerdidaCliente.Background = Brushes.LightBlue;

                bdCuentaPerdidaCliente.Background = Brushes.LightBlue;

                txtCuentaPerdidaCliente.Background = Brushes.LightBlue;

                imgCuentaPerdidaCliente.Visibility = Visibility.Visible;
            }

            else if (textBox.Name == "txtCuentaPerdidaProveedores")
            {

                dpCuentaPerdidaProveedores.Background = Brushes.LightBlue;

                bdCuentaPerdidaProveedores.Background = Brushes.LightBlue;

                txtCuentaPerdidaProveedores.Background = Brushes.LightBlue;

                imgCuentaPerdidaProveedores.Visibility = Visibility.Visible;
            }

            else if (textBox.Name == "txtCuentaPerdidaCuenta")
            {

                dpCuentaPerdidaCuenta.Background = Brushes.LightBlue;

                bdCuentaPerdidaCuenta.Background = Brushes.LightBlue;

                txtCuentaPerdidaCuenta.Background = Brushes.LightBlue;

                imgCuentaPerdidaCuenta.Visibility = Visibility.Visible;
            }


        }

        private void txtCuenta_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Name == "txtCuentaBeneficioCliente")
            {
                dpCuentaBeneficioCliente.Background = Brushes.White;

                bdCuentaBeneficioCliente.Background = Brushes.White;

                txtCuentaBeneficioCliente.Background = Brushes.White;

                imgCuentaBeneficioCliente.Visibility = Visibility.Hidden;
            }
            else if (textBox.Name == "txtCuentaBeneficioProveedores")
            {

                dpCuentaBeneficioProveedores.Background = Brushes.White;

                bdCuentaBeneficioProveedores.Background = Brushes.White;

                txtCuentaBeneficioProveedores.Background = Brushes.White;

                imgCuentaBeneficioProveedores.Visibility = Visibility.Hidden;
            }

            else if (textBox.Name == "txtCuentaBeneficioCuenta")
            {

                dpCuentaBeneficioCuenta.Background = Brushes.White;

                bdCuentaBeneficioCuenta.Background = Brushes.White;

                txtCuentaBeneficioCuenta.Background = Brushes.White;

                imgCuentaBeneficioCuenta.Visibility = Visibility.Hidden;
            }

            else if (textBox.Name == "txtCuentaPerdidaCliente")
            {

                dpCuentaPerdidaCliente.Background = Brushes.White;

                bdCuentaPerdidaCliente.Background = Brushes.White;

                txtCuentaPerdidaCliente.Background = Brushes.White;

                imgCuentaPerdidaCliente.Visibility = Visibility.Hidden;
            }

            else if (textBox.Name == "txtCuentaPerdidaProveedores")
            {

                dpCuentaPerdidaProveedores.Background = Brushes.White;

                bdCuentaPerdidaProveedores.Background = Brushes.White;

                txtCuentaPerdidaProveedores.Background = Brushes.White;

                imgCuentaPerdidaProveedores.Visibility = Visibility.Hidden;
            }

            else if (textBox.Name == "txtCuentaPerdidaCuenta")
            {

                dpCuentaPerdidaCuenta.Background = Brushes.White;

                bdCuentaPerdidaCuenta.Background = Brushes.White;

                txtCuentaPerdidaCuenta.Background = Brushes.White;

                imgCuentaPerdidaCuenta.Visibility = Visibility.Hidden;
            }
        }
    }
}
