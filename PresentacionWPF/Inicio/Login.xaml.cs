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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Negocio;
using HashCode;
using System.Configuration;
using Microsoft.SqlServer.Management.Smo;
using System.Globalization;
using System.Data;
using Entidades;
using System.Windows.Controls.Primitives;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Reflection;
using Vista.Inicio.ValidationErrorLogin;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Document
    {
        ControladorLogin cn = new ControladorLogin();       

        EstableceLogin estableceLogin = new EstableceLogin();

        Selecionar_Sociedad objectSeleccionarSociedad;

        EstablecerStringConnection objectEstablecerConnectionString;

        private static readonly DependencyProperty PasswordInitializedProperty =
            DependencyProperty.RegisterAttached("PasswordInitialized", typeof(bool), typeof(Login), new PropertyMetadata(false));

        private static readonly DependencyProperty SettingPasswordProperty =
            DependencyProperty.RegisterAttached("SettingPassword", typeof(bool), typeof(Login), new PropertyMetadata(false));

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
            DependencyProperty.RegisterAttached("Password", typeof(string), typeof(Login),
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
        public Login()
        {
            InitializeComponent();
        }

        private Tuple<bool,string> ValidateFormLogin()
        {
            bool sw = false;

            string firstError = null;

            foreach (KeyValuePair<string, string> errrors in ValidationErrorsLogin.ErrorCollectionMessages)
            {
                if (errrors.Value != null)
                {
                    sw = true;

                    firstError = errrors.Value;
                }

                if (sw == true)
                {
                    break;
                }
            }

            return Tuple.Create(sw,firstError);
        }

        private void btnLogin_Click(object sender1, RoutedEventArgs e1)
        {
            try
            {
                if (IsValid(login) == true)
                {
                    //Begin Transaction

                    var resultValidateForm = ValidateFormLogin();

                    bool sw = resultValidateForm.Item1;

                    string error = resultValidateForm.Item2;

                    if (sw == true)
                    {
                        App.GetMainWindowStatusBar().ShowStatusMessage("Error en algun campo del formulario: " + error, Brushes.Red, Brushes.White, "003-interface-2.png");
                    }
                    else
                    {
                        List<Entidades.Usuarios> listaUsuario = new List<Entidades.Usuarios>();

                        Entidades.Usuarios usuarios = new Entidades.Usuarios();

                        usuarios.Password = txtClave.Password;

                        usuarios.User_code = txtUsuario.Text;

                        usuarios.Sociedad = txtSociedad.Text;

                        listaUsuario.Add(usuarios);

                        var result = cn.conSQL(listaUsuario);



                        if (result.Item1 != null)
                        {
                            //MessageBox.Show("Centinela");

                            cn.EstableceConexionString(txtSociedad.Text);

                            //MessageBox.Show("Centinela1");

                            cn.obtenerCadenaConexion();

                            //MessageBox.Show("Centinela2");

                            estableceLogin.UltimaSociedad(txtSociedad.Text);

                            //MessageBox.Show("Centinela3");

                            var result1 = cn.ObtenerIdUser(txtUsuario.Text);

                           // MessageBox.Show("Centinela4");

                            estableceLogin.UsuarioActual(txtUsuario.Text, result1.Item1);

                            //MessageBox.Show("Centinela5");

                            var result2 = cn.GetNameSociety();

                            //MessageBox.Show("Centinela6");

                            estableceLogin.IngresoSistema(result2.Item1, txtUsuario.Text);

                            //MessageBox.Show("Centinela7");

                            this.Hide();

                            App.GetMainWindowStatusBar().Hide();

                            //if (result3 == null)
                            //{
                            //    this.Hide();

                            //    App.GetMainWindowStatusBar().Hide();

                            //}
                            //else
                            //{
                            //    Application.Current.Shutdown();
                            //}

                            //End transaction

                        }
                        else
                        {
                            App.GetMainWindowStatusBar().ShowStatusMessage("Error. Usuario y/o contraseña incorrecta: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                        }
                    }

                }
                else
                {
                    App.GetMainWindowStatusBar().ShowStatusMessage("Error en algun campo del formulario", Brushes.Red, Brushes.White, "003-interface-2.png");
                }
            }
            catch(Exception ex)
            {
                App.GetMainWindowStatusBar().ShowStatusMessage("Error General: " + ex.Message, Brushes.Red, Brushes.White, "003-interface-2.png");
            }
           


        }        
        
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnSociedad_Click(object sender, RoutedEventArgs e)
        {      
            objectSeleccionarSociedad.Show();

            objectSeleccionarSociedad.LoadedWindow();

            this.Hide();
        }
       
        private void gLogin_Loaded(object sender, RoutedEventArgs e)
        {

            txtSociedad.Text = Properties.Settings.Default.BaseDatos;

            CreateWindowSeleccionarSociedad();

            CreateWindowEstablecerConnectionString();

        }

        private void CreateWindowEstablecerConnectionString()
        {
            EstablecerStringConnection establecerStringConnection = new EstablecerStringConnection();

            objectEstablecerConnectionString = establecerStringConnection;
        }

        private void CreateWindowSeleccionarSociedad()
        {
            Selecionar_Sociedad selecionar_Sociedad = new Selecionar_Sociedad(true);

            objectSeleccionarSociedad = selecionar_Sociedad;
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {            
            App.textBox_GotFocus(sender, e);
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
          App.textBox_LostFocus(sender, e);
        }

        private void password_GotFocus(object sender, RoutedEventArgs e)
        {
            App.password_GotFocus(sender, e);
          
        }

        private void password_LostFocus(object sender, RoutedEventArgs e)
        {
            App.password_LostFocus(sender, e);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtUsuario.Focus();



            //try{

              

            //    string a = "werty";

            //    int b = Convert.ToInt32(a);

                

            //}catch(Exception ex)
            //{
            //    //ex.InnerException.ToString();
            //    ex.Source.ToString();
            //    ex.Message.ToString();
            //    ex.Data.Keys.ToString();

            //    MessageBox.Show(ex.StackTrace.ToString());
              
            //    ex.HResult.ToString();

            //    string error="0x" + ex.HResult.ToString("x");

            //    long eq=Convert.ToInt64(0x80131537);

            //    ex.TargetSite.ToString();
            //    //ex.HelpLink.ToString();

            //    // Get stack trace for the exception with source file information
            //    var st = new StackTrace(ex, true);
            //    // Get the top stack frame
            //    var frame = st.GetFrame(st.FrameCount-1);
            //    // Get the line number from the stack frame
            //    var line = frame.GetFileLineNumber();

               
            //}
        }

        private void login_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.btnLogin.Click += new System.Windows.RoutedEventHandler(this.btnLogin_Click);
            }
        }

        private void txtSociedad_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.Tab)
            //{
            //    txtUsuario.Focus();
            //}
        }

        private void txtUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                txtClave.Focus();
            }
        }

        private void txtClave_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                btnLogin.Focus();
            }
        }

        private void txtSociedad_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                btnLogin.Focus();
            }
        }

        private void txtSociedad_PreviewKeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void btnStringConnection_Click(object sender, RoutedEventArgs e)
        {
            objectEstablecerConnectionString.LoadedWindow();

            objectEstablecerConnectionString.Show();
        }
    }
}
