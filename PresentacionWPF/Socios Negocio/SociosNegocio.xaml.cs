using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Vista;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para SociosNegocio.xaml
    /// </summary>
    public partial class SociosNegocio : Window
    {
        ControladorPlanCuentas cp = new ControladorPlanCuentas();

        ControladorSocioNegocio cn = new ControladorSocioNegocio();

        private List<SocioNegocio> listaSocioNegocio = new List<SocioNegocio>();

        private List<Cuenta> listaCuentasAsociadas = new List<Cuenta>();

        private string oldCardCode;
        public ObservableCollection<string> TipoPersona { get; set; }

        public ObservableCollection<string> YesNo { get; set; }

        public ObservableCollection<string> TipoSN { get; set; }
        public List<SocioNegocio> ListaSocioNegocio { get => listaSocioNegocio; set => listaSocioNegocio = value; }
        public string OldCardCode { get => oldCardCode; set => oldCardCode = value; }
        public List<Cuenta> ListaCuentasAsociadas { get => listaCuentasAsociadas; set => listaCuentasAsociadas = value; }

        public DataTable dtDirecciones = new DataTable();

       
        public SociosNegocio()
        {
            InitializeComponent();
        }

        private void comboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if(btnCrear.Content.ToString()!="Buscar")
            App.comboBox_LostFocus(sender, e);
        }

        private void comboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (btnCrear.Content.ToString() != "Buscar")
                App.comboBox_GotFocus(sender, e);
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (btnCrear.Content.ToString() != "Buscar")
                App.textBox_LostFocus(sender, e);
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (btnCrear.Content.ToString() != "Buscar")
                App.textBox_GotFocus(sender, e);
        }

        private void comboBoxItem_GotFocus(object sender, RoutedEventArgs e)
        {
            if (btnCrear.Content.ToString() != "Buscar")
                App.comboBoxItem_GotFocus(sender, e);
        }

        private void comboBoxItem_LostFocus(object sender, RoutedEventArgs e)
        {
            if (btnCrear.Content.ToString() != "Buscar")
                App.comboBoxItem_LostFocus(sender, e);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            InicializacionBasica();

            App.Window_Closing(sender, e);
        }

        public void LoadedWindow()
        {
            InicializacionBasica();
        }

        public void InicializacionBasica()
        {
            var result = cp.ConsultaMonedas();

            if (result.Item2 == null)
            {
                cbMoneda.ItemsSource = result.Item1;
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
            }

            btnCrear.Content = "Buscar";

            LimpiarCampos();

            EstablecerFondo();

        }

        private void btnWindow_Close(object sender, RoutedEventArgs e)
        {
            InicializacionBasica();

            this.Hide();
        }

        private void imgCrear_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            btnCrear.Content = "Crear";

            LimpiarCampos();

            ReestablecerFondo();
        }

        private void ReestablecerFondo()
        {
            Style style = Application.Current.FindResource("ComboBoxStyle") as Style;

            txtCodigo.Background = Brushes.White;
            txtNombre.Background = Brushes.White;
            cbSN.Style = style;
            txtRIF.Background = Brushes.White;
            cbMoneda.Style = style;            
            cbTipoPersona.Style = style;
            cbContribuyente.Style = style;
            cbSucursal.Style = style;
            cbITF.Style = style;
            cbSaldoMoneda.Style = style;
            tabDirecciones.Visibility = Visibility.Visible;
            tabFinanzas.Visibility = Visibility.Visible;
            txtPhone1.Background = Brushes.White;
            txtPhone2.Background = Brushes.White;
            txtFax.Background = Brushes.White;
            txtCorreo.Background = Brushes.White;
            txtSitioWeb.Background = Brushes.White;
            txtPersonaContacto.Background = Brushes.White;
        }

        private void LimpiarCampos()
        {
            txtCodigo.Text = "";
            cbSN.SelectedValue = "";
            txtNombre.Text = "";
            txtRIF.Text = "";
            txtPhone1.Text = "";
            txtPhone2.Text = "";
            txtFax.Text = "";
            txtCorreo.Text = "";
            txtSitioWeb.Text = "";
            txtPersonaContacto.Text = "";
            txtCuentaAsociada.Text = "";
            txtImpuesto.Text = "";
            cbMoneda.SelectedValue = "";
            cbTipoPersona.SelectedValue = "";
            cbContribuyente.SelectedValue = "";
            cbSucursal.SelectedValue = "";
            cbITF.SelectedValue = "";
            cbSaldoMoneda.SelectedValue = "";
            txtSaldoCuenta.Text = "";
            txtPedidosEntrada.Text = "";
        }

        private void imgFin_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (btnCrear.Content.ToString() == "OK" || btnCrear.Content.ToString() == "Crear" || btnCrear.Content.ToString() == "Buscar")
            {
                ReestablecerFondo();

                var result= cn.FindLast();

                if (result.Item2 == null)
                {
                    ListaSocioNegocio = result.Item1;

                    GetSocioNegocio(ListaSocioNegocio);

                    btnCrear.Content = "OK";
                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                }

            }

            else if (btnCrear.Content.ToString() == "Actualizar")
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("¿Desea salir sin guardar cambios?", "User", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);

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

                var result = cn.FindNext(txtCodigo.Text);

                if (result.Item2 == null)
                {
                    ListaSocioNegocio = result.Item1;

                    GetSocioNegocio(ListaSocioNegocio);

                    btnCrear.Content = "OK";
                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                }

                
            }

            else if (btnCrear.Content.ToString() == "Actualizar")
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("¿Desea salir sin guardar cambios?", "User", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);

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
                
                var result = cn.FindPrevious(txtCodigo.Text);

                if (result.Item2 == null)
                {
                    ListaSocioNegocio = result.Item1;

                    GetSocioNegocio(ListaSocioNegocio);

                    btnCrear.Content = "OK";
                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                }

                
            }

            else if (btnCrear.Content.ToString() == "Actualizar")
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("¿Desea salir sin guardar cambios?", "User", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);

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
                
                var result = cn.FindFirst();

                if (result.Item2 == null)
                {
                    ListaSocioNegocio = result.Item1;

                    GetSocioNegocio(ListaSocioNegocio);

                    btnCrear.Content = "OK";
                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                }

            }

            else if (btnCrear.Content.ToString() == "Actualizar")
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("¿Desea salir sin guardar cambios?", "User", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    btnCrear.Content = "OK";

                }
            }
        }

        private void GetSocioNegocio(List<SocioNegocio> listaSocioNegocio)
        {
            foreach(SocioNegocio socioNegocio in listaSocioNegocio)
            {
                OldCardCode = socioNegocio.OldCardCode;
                txtCodigo.Text = socioNegocio.CardCode;
                txtNombre.Text = socioNegocio.CardName;
                cbSN.SelectedValue = cn.CardType(socioNegocio.CardType);
                cbTipoPersona.SelectedValue = socioNegocio.TipoPersona;
                txtRIF.Text = socioNegocio.LicTradNum;
                cbMoneda.SelectedValue = socioNegocio.Currency;
                cbSucursal.SelectedValue = cn.YesNo(socioNegocio.Sucursal);
                cbContribuyente.SelectedValue = cn.YesNo(socioNegocio.Contribuyente);
                cbITF.SelectedValue = cn.YesNo(socioNegocio.AplicaITF);
                txtSaldoCuenta.Text = socioNegocio.Balance.ToString();
                txtPedidosEntrada.Text = socioNegocio.DNotesBal.ToString();
                txtPhone1.Text = socioNegocio.Phone1;
                txtPhone2.Text = socioNegocio.Phone2;
                txtFax.Text = socioNegocio.Fax;
                txtCorreo.Text = socioNegocio.E_mail;
                txtSitioWeb.Text = socioNegocio.MailAddress;
                txtPersonaContacto.Text = socioNegocio.CntctPrsn;
                txtCuentaAsociada.Text = socioNegocio.DebPayAcct;
                txtImpuesto.Text = socioNegocio.VatGroup;
                txtDireccion.Text = socioNegocio.Address;
            }
        }

        private void imgBuscar_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            InicializacionBasica();
        }

        private void EstablecerFondo()
        {
            Style style = Application.Current.FindResource("ComboBoxStyleHover") as Style;

            txtCodigo.Background = Brushes.LightBlue;
            txtNombre.Background = Brushes.LightBlue;
            cbSN.Style = style;
            txtRIF.Background = Brushes.LightBlue;
            cbMoneda.Style = style;            
            cbTipoPersona.Style = style;
            cbContribuyente.Style = style;
            cbSucursal.Style = style;
            cbITF.Style = style;
            cbSaldoMoneda.Style = style;
            tabDirecciones.Visibility = Visibility.Hidden;
            tabFinanzas.Visibility = Visibility.Hidden;
            txtPhone1.Background=Brushes.LightBlue;
            txtPhone2.Background = Brushes.LightBlue;
            txtFax.Background = Brushes.LightBlue;
            txtCorreo.Background = Brushes.LightBlue;
            txtSitioWeb.Background = Brushes.LightBlue;
            txtPersonaContacto.Background = Brushes.LightBlue;

        }

        private void txt_GotFocus(object sender, RoutedEventArgs e)
        {
            dpCuentaAsociada.Background = Brushes.LightBlue;
            bdCuentaAsociada.Background = Brushes.LightBlue;
            imgCuentaAsociada.Visibility = Visibility.Visible;
        }

        private void txt_LostFocus(object sender, RoutedEventArgs e)
        {
            dpCuentaAsociada.Background = Brushes.White;
            bdCuentaAsociada.Background = Brushes.White;
            imgCuentaAsociada.Visibility = Visibility.Hidden;
        }

        private void txt_GotFocus1(object sender, RoutedEventArgs e)
        {
            dpImpuesto.Background = Brushes.LightBlue;
            bdImpuesto.Background = Brushes.LightBlue;
            imgImpuesto.Visibility = Visibility.Visible;
        }

        private void txt_LostFocus1(object sender, RoutedEventArgs e)
        {
            dpImpuesto.Background = Brushes.White;
            bdImpuesto.Background = Brushes.White;
            imgImpuesto.Visibility = Visibility.Hidden;
        }


        private void img_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var result = cn.ConsultaCuentasAsociadas();

            if (result.Item2 == null)
            {
                RecorreListaAccount(result.Item1);
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
            }

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

        private void GetAcctCode(List<Cuenta> listaCuenta)
        {
            foreach (Cuenta cuenta in listaCuenta)
            {                
                txtCuentaAsociada.Text = cuenta.AcctCode;
               
            }
        }

        public class ListTipoPersona : List<string>
        {
            public ListTipoPersona()
            {
                this.Add("PJDO");
                this.Add("PJND");
                this.Add("PNNR");
                this.Add("PNSR");
                this.Add("PNRE");

            }
        }

        public class ListYesNo : List<string>
        {
            public ListYesNo()
            {
                this.Add("SI");
                this.Add("NO");
               
            }
        }

        public class ListSN : List<string>
        {
            public ListSN()
            {
                this.Add("Proveedor");
                this.Add("Cliente");

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CreateRowDireccion();

            var result = cp.ConsultaMonedas();

            if (result.Item2 == null)
            {
                cbMoneda.ItemsSource = result.Item1;
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
            }

            TipoSN = new ObservableCollection<string>() {"Proveedor","Cliente"};

            cbSN.ItemsSource = TipoSN;

            TipoPersona = new ObservableCollection<string>() { "PJDO", "PJND", "PNNR", "PNSR", "PNRE" };

            cbTipoPersona.ItemsSource = TipoPersona;

            YesNo = new ObservableCollection<string>() { "SI", "NO" };

            cbSucursal.ItemsSource = YesNo;

            cbITF.ItemsSource = YesNo;

            cbContribuyente.ItemsSource = YesNo;

            EstablecerFondo();

            InicializacionBasica();
        }

        private void CreateRowDireccion()
        {
            //dtDirecciones.Columns.Add("Datos", typeof(string));

            //DataRow idDireccion = dtDirecciones.NewRow();
            //DataRow direccion1 = dtDirecciones.NewRow();
            //DataRow direccion2 = dtDirecciones.NewRow();
            //DataRow calle = dtDirecciones.NewRow();
            //DataRow ciudad = dtDirecciones.NewRow();
            //DataRow codigoPostal = dtDirecciones.NewRow();
            //DataRow estado = dtDirecciones.NewRow();
            //DataRow pais = dtDirecciones.NewRow();


            //idDireccion["Datos"] = "";
            //direccion1["Datos"] = "";
            //direccion2["Datos"] = "";
            //calle["Datos"] = "";
            //ciudad["Datos"] = "";
            //codigoPostal["Datos"] = "";
            //estado["Datos"] = "";
            //pais["Datos"] = "";
           

            //dtDirecciones.Rows.Add(idDireccion);
            //dtDirecciones.Rows.Add(direccion1);
            //dtDirecciones.Rows.Add(direccion2);
            //dtDirecciones.Rows.Add(calle);
            //dtDirecciones.Rows.Add(ciudad);
            //dtDirecciones.Rows.Add(codigoPostal);
            //dtDirecciones.Rows.Add(estado);
            //dtDirecciones.Rows.Add(pais);

            //dgDirecciones.ItemsSource = dtDirecciones.DefaultView;
        }

       

        private void btnCrear_Click(object sender, RoutedEventArgs e)
        {
            List<SocioNegocio> listaSocioNegocio = new List<SocioNegocio>();

            SocioNegocio socioNegocio = new SocioNegocio();

            switch (btnCrear.Content.ToString())
            {
                case "OK":

                    this.Hide();

                    break;

                case "Buscar":

                    socioNegocio.CardCode = txtCodigo.Text;
                    socioNegocio.CardName = txtNombre.Text;
                    socioNegocio.CardType = cn.CardType(cbSN.Text.ToString());
                    socioNegocio.LicTradNum = txtRIF.Text;
                    socioNegocio.Currency = cbMoneda.Text.ToString();
                    socioNegocio.TipoPersona = cbTipoPersona.Text.ToString();
                    socioNegocio.Contribuyente =cn.YesNo(cbContribuyente.Text.ToString());
                    socioNegocio.Sucursal = cn.YesNo(cbSucursal.Text.ToString());
                    socioNegocio.AplicaITF = cn.YesNo(cbITF.Text.ToString());

                    listaSocioNegocio.Add(socioNegocio);

                    var result = cn.ConsultaSocioNegocio(listaSocioNegocio);

                    if (result.Item2 == null)
                    {
                        RecorreListaSN(result.Item1);
                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                    }
                    
                    break;

                case "Crear":

                    socioNegocio.CardCode = txtCodigo.Text;
                    socioNegocio.CardName = txtNombre.Text;
                    socioNegocio.CardType = cn.CardType(cbSN.SelectedValue.ToString());
                    socioNegocio.LicTradNum = txtRIF.Text;
                    socioNegocio.Currency = cbMoneda.SelectedValue.ToString();
                    socioNegocio.TipoPersona = cbTipoPersona.SelectedValue.ToString();
                    socioNegocio.Contribuyente = cn.YesNo(cbContribuyente.SelectedValue.ToString());
                    socioNegocio.Sucursal = cn.YesNo(cbSucursal.SelectedValue.ToString());
                    socioNegocio.AplicaITF = cn.YesNo(cbITF.SelectedValue.ToString());
                    socioNegocio.Phone1 = txtPhone1.Text;
                    socioNegocio.Phone2 = txtPhone2.Text;
                    socioNegocio.Fax = txtFax.Text;
                    socioNegocio.E_mail = txtCorreo.Text;
                    socioNegocio.MailAddress = txtSitioWeb.Text;
                    socioNegocio.CntctPrsn = txtPersonaContacto.Text;
                    socioNegocio.Address = txtDireccion.Text;
                    socioNegocio.DebPayAcct = txtCuentaAsociada.Text;
                    socioNegocio.VatGroup = txtImpuesto.Text;
                    socioNegocio.UserSign = Vista.Properties.Settings.Default.Usuario;
                    socioNegocio.UpdateDate = fechaActual.GetFechaActual();
                    socioNegocio.Deleted = 'Y';
                    socioNegocio.Balance = 0;
                    socioNegocio.BalanceFC = 0;
                    socioNegocio.BalanceSys = 0;
                    socioNegocio.DNoteBalFC = 0;
                    socioNegocio.DNoteBalSy = 0;
                    socioNegocio.DNotesBal = 0;

                    listaSocioNegocio.Add(socioNegocio);

                    var result1 = cn.InsertBP(listaSocioNegocio);

                    if (result1.Item1 == 1)
                    {                       
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Socio negocio " + socioNegocio.CardCode + " se creo correctamente", Brushes.LightGreen, Brushes.Black, "001-interface.png");

                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error en la creacion del socio de negocio " + socioNegocio.CardCode + ": " + result1.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");                        
                    }

                    btnCrear.Content = "OK";

                    break;

                case "Actualizar":

                    socioNegocio.OldCardCode = OldCardCode;
                    socioNegocio.CardCode = txtCodigo.Text;
                    socioNegocio.CardName = txtNombre.Text;
                    socioNegocio.CardType = cn.CardType(cbSN.SelectedValue.ToString());
                    socioNegocio.LicTradNum = txtRIF.Text;
                    socioNegocio.Currency = cbMoneda.SelectedValue.ToString();
                    socioNegocio.TipoPersona = cbTipoPersona.SelectedValue.ToString();
                    socioNegocio.Contribuyente = cn.YesNo(cbContribuyente.SelectedValue.ToString());
                    socioNegocio.Sucursal = cn.YesNo(cbSucursal.SelectedValue.ToString());
                    socioNegocio.AplicaITF = cn.YesNo(cbITF.SelectedValue.ToString());
                    socioNegocio.Phone1 = txtPhone1.Text;
                    socioNegocio.Phone2 = txtPhone2.Text;
                    socioNegocio.Fax = txtFax.Text;
                    socioNegocio.E_mail = txtCorreo.Text;
                    socioNegocio.MailAddress = txtSitioWeb.Text;
                    socioNegocio.CntctPrsn = txtPersonaContacto.Text;
                    socioNegocio.Address = txtDireccion.Text;
                    socioNegocio.DebPayAcct = txtCuentaAsociada.Text;
                    socioNegocio.VatGroup = txtImpuesto.Text;
                    socioNegocio.UserSign = Vista.Properties.Settings.Default.Usuario;
                    socioNegocio.UpdateDate = fechaActual.GetFechaActual();

                    listaSocioNegocio.Add(socioNegocio);

                    var result2 = cn.UpdateBP(listaSocioNegocio);

                    if (result2.Item1 == 1)
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Socio negocio " + socioNegocio.CardCode + " se actualizo correctamente", Brushes.LightGreen, Brushes.Black, "001-interface.png");
                       
                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error en la actualizacion del socio de negocio " + socioNegocio.CardCode + ": " + result2.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
                        
                    }

                    btnCrear.Content = "OK";

                    break;
            }
        }

        private void RecorreListaSN(List<SocioNegocio> newlistaSN)
        {
            if (newlistaSN.Count == 1)
            {
                GetSocioNegocio(newlistaSN);

                btnCrear.Content = "OK";
            }
            else if (newlistaSN.Count == 0)
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se encontro ningun resultado con los parametros establecidos", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");

                LimpiarCampos();

                btnCrear.Content = "OK";
            }

            else if (newlistaSN.Count > 1)
            {
                ListaSociosNegocios ventanaListBox = new ListaSociosNegocios(newlistaSN);

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

                    }

                    btnCrear.Content = "OK";                   
                }
                ReestablecerFondo();
            }
        }

        private void cbSN_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbSN.SelectedIndex > -1)
            {
                if (cbSN.SelectedValue.ToString() == "Proveedor")
                {
                    lblPedidosEntrada.Text = "Entrada Mercancia";
                }
                else
                {
                    lblPedidosEntrada.Text = "Entrega";
                }
            }

        }

        private void txtCodigo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (btnCrear.Content.ToString() == "OK")
            {
                btnCrear.Content = "Actualizar";
            }
        }

        private void cbSN_DropDownOpened(object sender, EventArgs e)
        {
            if (btnCrear.Content.ToString() == "OK")
            {
                btnCrear.Content = "Actualizar";
            }
        }

        private void imgImpuesto_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var result = cn.ConsultaCodigosFiscales();

            if (result.Item2 == null)
            {
                RecorreListaCodigosFiscales(result.Item1);
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " +  result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
            }
            

        }

        private void RecorreListaCodigosFiscales(List<Entidades.CodigosFiscales> listCodigosResultantes)
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
                        GetCodigos(ventanaListaCodigos.GetListCodigosFiscales());
                    }
                }
            }
        }

        private void GetCodigos(List<Entidades.CodigosFiscales> listaCodigos)
        {
            foreach (Entidades.CodigosFiscales codigo in listaCodigos)
            {
                txtImpuesto.Text = codigo.Code;

            }
        }

       
        private void Deleted_Click(object sender, RoutedEventArgs e)
        {
            if (btnCrear.Content.ToString() == "OK")
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("¿Desea eliminar el usuario?", "User", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    string cardCode = txtCodigo.Text;

                    if (cardCode != null)
                    {
                       
                       var  result = cn.DeleteBusinessPartner(cardCode);

                        if (result.Item1 == 1)
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Socio negocio: " + cardCode + " se elimino correctamente", Brushes.LightGreen, Brushes.Black, "001-interface.png");

                            LimpiarCampos();
                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se puede eliminar el socio de negocio: " + cardCode + " porque se realizo una transaccion con el mismo: " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");

                        }

                    }

                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se selecciono ningun socio de negocio", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");

                    }

                }

            }
        }

        private void imgCuentaAsociada_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
