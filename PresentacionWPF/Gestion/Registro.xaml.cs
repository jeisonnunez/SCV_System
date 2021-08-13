using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
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
using Entidades;
using Microsoft.Win32;
using Negocio;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para Registro.xaml
    /// </summary>
    public partial class Registro : Document
    {
        EstableceLogin estableceLogin = new EstableceLogin();

        ControladorRegistro cn = new ControladorRegistro();

        DataTable dt = new DataTable();

        public string strName, imageName;

        private string password;

        private static string empresa;

        private static bool closeWindow;

        public BitmapImage bitMap;
        public static bool CloseWindow { get => closeWindow; set => closeWindow = value; }
        public static string Empresa { get => empresa; set => empresa = value; }
        public string Password { get => password; set => password = value; }

        private static readonly DependencyProperty PasswordInitializedProperty =
            DependencyProperty.RegisterAttached("PasswordInitialized", typeof(bool), typeof(Registro), new PropertyMetadata(false));

        private static readonly DependencyProperty SettingPasswordProperty =
            DependencyProperty.RegisterAttached("SettingPassword", typeof(bool), typeof(Registro), new PropertyMetadata(false));

        public static string GetPassword(DependencyObject obj)
        {
            return (string)obj.GetValue(PasswordProperty);
        }
        public static void SetPassword(DependencyObject obj, string value)
        {
            obj.SetValue(PasswordProperty, value);
        }
        // We play a trick here. If we set the initial value to something, it'll be set to something else when the binding kicks in,
        // and HandleBoundPasswordChanged will be called, which allows us to set up our event subscription.
        // If the binding sets us to a value which we already are, then this doesn't happen. Therefore start with a value that's
        // definitely unique.
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.RegisterAttached("Password", typeof(string), typeof(Registro),
                new FrameworkPropertyMetadata(Guid.NewGuid().ToString(), HandleBoundPasswordChanged)
                {
                    BindsTwoWayByDefault = true,
                    DefaultUpdateSourceTrigger = UpdateSourceTrigger.LostFocus // Match the default on Binding
                });

        private static void HandleBoundPasswordChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = dp as PasswordBox;
            if (passwordBox == null)
                return;

            // If we're being called because we set the value of the property we're bound to (from inside 
            // HandlePasswordChanged, then do nothing - we already have the latest value).
            if ((bool)passwordBox.GetValue(SettingPasswordProperty))
                return;

            // If this is the initial set (see the comment on PasswordProperty), set ourselves up
            if (!(bool)passwordBox.GetValue(PasswordInitializedProperty))
            {
                passwordBox.SetValue(PasswordInitializedProperty, true);
                passwordBox.PasswordChanged += HandlePasswordChanged;
            }

            passwordBox.Password = e.NewValue as string;
        }

        private static void HandlePasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = (PasswordBox)sender;
            passwordBox.SetValue(SettingPasswordProperty, true);
            SetPassword(passwordBox, passwordBox.Password);
            passwordBox.SetValue(SettingPasswordProperty, false);
        }



        public Registro(bool close, string empresa=null)
        {
            InitializeComponent();

            CloseWindow = close;

            if (CloseWindow == false)
            {
                DeshabilitarUsuario();
            }
            else
            {
                HabilitarUsuario();
            }

            Empresa = empresa;

            
        }

        private void DeshabilitarUsuario()
        {
            lblUsername.Visibility = Visibility.Hidden;
            txtUser.Visibility = Visibility.Hidden;
        }

        private void HabilitarUsuario()
        {
            lblUsername.Visibility = Visibility.Visible;
            txtUser.Visibility = Visibility.Visible;
        }

        private void btnRegistro_Click(object sender, RoutedEventArgs e)
        {

            if (txtPassword.Password.Equals(txtPasswordConfirm.Password))
            {
                if (CloseWindow == true)
                {

                    List<Entidades.Usuarios> listaUsuario = CreateListUsuario();

                    var result = cn.regUsuario(listaUsuario);

                    if (result.Item1 == 1)
                    {
                        App.GetMainWindowStatusBar().ShowStatusMessage("Usuario: " + txtUser.Text + " agregado a la sociedad " + Empresa, Brushes.LightBlue, Brushes.Black, "001-interface.png");
                                                
                        List<Sociedad> listaEmpresa = CreateListSociedad();

                        var result1 = cn.DatosSociedad(listaEmpresa);

                        if (result1.Item1 == 1)
                        {
                            
                            var result10 = cn.obtenerBaseDatos();

                            if (result10.Item2 == null)
                            {
                                estableceLogin.UltimaSociedad(result10.Item1);
                            }
                            else
                            {
                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result1.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
                            }

                            var result11 = cn.ObtenerIdUser(txtUser.Text);

                            estableceLogin.UsuarioActual(txtUser.Text, result11.Item1);

                            List<Cuenta> listaCuenta = CreateListCuenta();

                            var result2 = cn.InsertaCuentasPrimerNivel(listaCuenta);

                            if (result2.Item1 != 8)
                            {
                                App.GetMainWindowStatusBar().ShowStatusMessage("Error al crear cuentas contables de primer nivel: " + result2.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");

                            }

                            List<Entidades.Monedas> listaMonedas = CreateListMonedas();

                            var result3 = cn.InsertaMonedasBasicas(listaMonedas);

                            if (result3.Item1 != 3)
                            {
                                App.GetMainWindowStatusBar().ShowStatusMessage("Error al crear monedas primarias: " + result3.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                                
                            }
                                                       

                            List<Entidades.RetencionImpuesto> listaRetencion = CreateListRetencion();

                            var result4 = cn.InsertaRetencionImpuesto(listaRetencion);

                            if (result4.Item1 != 179)
                            {
                                App.GetMainWindowStatusBar().ShowStatusMessage("Error al crear retenciones de impuesto: " + result4.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                               
                            }

                            List<Entidades.ClasesImpuestos> listaClasesImpuestos = CreateListClasesImpuestos();

                            var result5 = cn.InsertaClasesImpuestos(listaClasesImpuestos);

                            if (result5.Item1 != 7)
                            {
                                App.GetMainWindowStatusBar().ShowStatusMessage("Error al crear clases de impuesto: " + result5.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                            }

                            List<Entidades.CodigosFiscales> listaCodigosFiscales = CreateListCodigosFiscales();

                            var result6 = cn.InsertaCodigosFiscales(listaCodigosFiscales);

                            if (result6.Item1 != 14)
                            {
                                App.GetMainWindowStatusBar().ShowStatusMessage("Error al crear codigos fiscales: " + result6.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                            }

                            var result7 = cn.InsertaMeses();

                            if (result7.Item1 != 12)
                            {
                                App.GetMainWindowStatusBar().ShowStatusMessage("Error al crear meses: " + result7.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                            }

                            var result8 = cn.InsertaAlicuotas();

                            if (result8.Item1 != 6)
                            {
                                App.GetMainWindowStatusBar().ShowStatusMessage("Error al crear alicuotas: " + result8.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                            }

                            var result9 = cn.InsertaDeterminacionCuentasMayor();

                            if (result9.Item1 != 1)
                            {
                                App.GetMainWindowStatusBar().ShowStatusMessage("Error al crear determinacion de mayor: " + result9.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                            }


                            LogoDatabase();                           

                            listaRetencion.Clear();

                            listaCuenta.Clear();

                            listaMonedas.Clear();

                            listaUsuario.Clear();

                            listaEmpresa.Clear();

                            listaClasesImpuestos.Clear();

                            listaCodigosFiscales.Clear();

                            estableceLogin.IngresoSistema(Empresa, txtUser.Text);

                            this.Hide();

                            App.GetMainWindowStatusBar().Hide();

                        }
                        else
                        {
                            App.GetMainWindowStatusBar().ShowStatusMessage("Error al insertar datos de la sociedad: " + result1.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                        }

                    }
                    else
                    {
                        App.GetMainWindowStatusBar().ShowStatusMessage("Error en el registro del usuario: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                        
                    }
                }
                else
                {
                    SetUsuarioPassword(txtPassword.Password);

                    btnRegistro.Content = "OK";

                    this.Hide();
                    
                }
            }
            else
            {
                App.GetMainWindowStatusBar().ShowStatusMessage("Error: La clave de acceso no coincide ", Brushes.Red, Brushes.White, "003-interface-2.png");
            }
        }

        public string GetUsuarioPassword()
        {
            return Password;
        }

        private void SetUsuarioPassword(string password)
        {
            Password = password;

        }

        private void LogoDatabase()
        {
            try
            {
                FileDialog fldlg = new OpenFileDialog();
                fldlg.InitialDirectory = Environment.SpecialFolder.MyPictures.ToString();
                fldlg.Filter = "Image File (*.jpg;*.bmp;*.gif;*.png)|*.jpg;*.bmp;*.gif;*.png";
                //fldlg.ShowDialog();
                // {
                strName = "Etapa1.PNG";
                imageName = "C:\\Users\\jamara\\Documents\\Etapa1.PNG";
                ImageSourceConverter isc = new ImageSourceConverter();
                SetValue(Image.SourceProperty, isc.ConvertFromString(imageName));
                // }
                fldlg = null;

                insertImageData();
            }
            catch (Exception ex)
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + ex.Message, Brushes.Red, Brushes.White, "003-interface-2.png");
            }

        }

        private void insertImageData()
        {
            try
            {
                if (imageName != "")
                {
                    //Initialize a file stream to read the image file
                    FileStream fs = new FileStream(imageName, FileMode.Open, FileAccess.Read);

                    //Initialize a byte array with size of stream
                    byte[] imgByteArr = new byte[fs.Length];

                    //Read data from the file stream and put into the byte array
                    fs.Read(imgByteArr, 0, Convert.ToInt32(fs.Length));

                    //Close a file stream
                    fs.Close();

                    var result = cn.InsertLogo(strName, imgByteArr);

                    if (result.Item2 == null)
                    {
                       
                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                    }

                }
            }
            catch (Exception ex)
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + ex.Message, Brushes.Red, Brushes.White, "003-interface-2.png");
            }
        }
             
        private List<Entidades.CodigosFiscales> CreateListCodigosFiscales()
        {
            List<Entidades.CodigosFiscales> listaCodigosFiscales = new List<Entidades.CodigosFiscales>();

            Entidades.CodigosFiscales codigosFiscales = new Entidades.CodigosFiscales();

            codigosFiscales.UserSign = Properties.Settings.Default.Usuario;

            listaCodigosFiscales.Add(codigosFiscales);

            return listaCodigosFiscales;
            

        }

        private List<Entidades.ClasesImpuestos> CreateListClasesImpuestos()
        {
            List<Entidades.ClasesImpuestos> listaClasesImpuestos = new List<Entidades.ClasesImpuestos>();

            Entidades.ClasesImpuestos clasesImpuestos = new Entidades.ClasesImpuestos();

            clasesImpuestos.UserSign = Properties.Settings.Default.Usuario;

            listaClasesImpuestos.Add(clasesImpuestos);

            return listaClasesImpuestos;
        }

        private List<Entidades.RetencionImpuesto> CreateListRetencion()
        {
            List<Entidades.RetencionImpuesto> listaRetencionImpuesto = new List<Entidades.RetencionImpuesto>();

            Entidades.RetencionImpuesto retencionImpuesto = new Entidades.RetencionImpuesto();

            retencionImpuesto.UserSign =Properties.Settings.Default.Usuario;

            listaRetencionImpuesto.Add(retencionImpuesto);

            return listaRetencionImpuesto;
        }

        private List<Entidades.Monedas> CreateListMonedas()
        {
            List<Entidades.Monedas> listaMonedas = new List<Entidades.Monedas>();

            Entidades.Monedas monedasBasicas = new Entidades.Monedas();

            monedasBasicas.UserSign= Properties.Settings.Default.Usuario;

            listaMonedas.Add(monedasBasicas);

            return listaMonedas;
        }

        private List<Cuenta> CreateListCuenta()
        {
            List<Cuenta> listaCuenta = new List<Cuenta>();

            Cuenta cuentas = new Cuenta();
            
            cuentas.UserSign = Properties.Settings.Default.Usuario;           

            listaCuenta.Add(cuentas);
           
            return listaCuenta;


        }

        private List<Entidades.Usuarios> CreateListUsuario()
        {
            List<Entidades.Usuarios> listaUsuario = new List<Entidades.Usuarios>();
            
            Entidades.Usuarios usuarios = new Entidades.Usuarios();

            usuarios.Password = txtPassword.Password;

            if (CloseWindow == true)
            {
                usuarios.User_code = txtUser.Text;

                usuarios.User_name = txtUser.Text;

            }

            usuarios.Locked = 'N';

            listaUsuario.Add(usuarios);

            return listaUsuario;
        }

        private List<Sociedad> CreateListSociedad()
        {
            List<Sociedad> listaEmpresa = new List<Sociedad>();

            Sociedad sociedad = new Sociedad();

            sociedad.CompnyName = Empresa;

            sociedad.UpdateDate = fechaActual.GetFechaActual();

            var result = cn.ObtenerIdUser(txtUser.Text);

            sociedad.UserSign = result.Item1;

            sociedad.MainCurncy = "VES";

            sociedad.SysCurncy = "USD";

            listaEmpresa.Add(sociedad);

            return listaEmpresa;
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            App.textBox_LostFocus(sender, e);
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            App.textBox_GotFocus(sender, e);
        }
              

        private void password_GotFocus(object sender, RoutedEventArgs e)
        {
            App.password_GotFocus(sender, e);

        }

        private void password_LostFocus(object sender, RoutedEventArgs e)
        {
            App.password_LostFocus(sender, e);



        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (CloseWindow == true)
            {
                Application.Current.Shutdown();
            }
            else
            {
                this.Hide();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (CloseWindow == true)
            {
                Application.Current.Shutdown();
            }
            else
            {
                App.Window_Closing(sender, e);
            }

        }       

        private void txtPasswordConfirm_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (CloseWindow == false)
            {
                btnRegistro.Content = "Actualizar";
            }
        }
    }
}
