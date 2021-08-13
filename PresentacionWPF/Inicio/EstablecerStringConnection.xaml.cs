using Negocio.Controlador_Inicio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
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
    /// Lógica de interacción para EstablecerStringConnection.xaml
    /// </summary>
    public partial class EstablecerStringConnection : Window
    {
        ControladorUsuarioSitio cn = new ControladorUsuarioSitio();
        public EstablecerStringConnection()
        {
            InitializeComponent();
        }

        private static readonly DependencyProperty PasswordInitializedProperty =
            DependencyProperty.RegisterAttached("PasswordInitialized", typeof(bool), typeof(EstablecerStringConnection), new PropertyMetadata(false));

        private static readonly DependencyProperty SettingPasswordProperty =
            DependencyProperty.RegisterAttached("SettingPassword", typeof(bool), typeof(EstablecerStringConnection), new PropertyMetadata(false));

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
            DependencyProperty.RegisterAttached("Password", typeof(string), typeof(EstablecerStringConnection),
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


        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            App.textBox_LostFocus(sender, e);
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
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

        private void txtClave_LostFocus(object sender, RoutedEventArgs e)
        {
            App.password_LostFocus(sender, e);
        }

        private void txtClave_GotFocus(object sender, RoutedEventArgs e)
        {
            App.password_GotFocus(sender, e);
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var resultConnectionTest = cn.VerifyUserAdministratorSQLServer(txtUsuarioSitio.Text, txtPasswordSQL.Password, txtServidor.Text);

            if (resultConnectionTest.Item1 == true && resultConnectionTest.Item2 == null)
            {
                if(SetConnectionString(txtUsuarioSitio.Text, txtPasswordSQL.Password, txtServidor.Text) == true)
                {
                    this.Hide();

                    App.GetMainWindowStatusBar().ShowStatusMessage("Cambio de string de conexion exitoso ", Brushes.LightGreen, Brushes.Black, "001-interface.png");

                   
                }
                else
                {                    

                   
                }

            }
            else
            {
                App.GetMainWindowStatusBar().ShowStatusMessage("Error. Usuario y/o contraseña incorrecta: " + resultConnectionTest.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
            }
        }

        private bool SetConnectionString(string user, string Password, string machineName)
        {
            try
            {
               // ((MessageBox.Show("Connection Success", "Install", MessageBoxButton.OK, MessageBoxImage.Information);

                string dataSource = "Server =" + machineName;
                string userid = "User Id=" + user;
                string password = "Password=" + Password;

                dataSource = dataSource + ";" + userid + ";" + password;
                dataSource = dataSource + ";integrated security=false;";

                ExeConfigurationFileMap map = new ExeConfigurationFileMap();
                //MessageBox.Show(Assembly.GetExecutingAssembly().Location + ".config");
                //Getting the path location 
                string configFile = string.Concat(Assembly.GetExecutingAssembly().Location, ".config");
                map.ExeConfigFilename = configFile;
                System.Configuration.Configuration config = System.Configuration.ConfigurationManager.
                OpenMappedExeConfiguration(map, System.Configuration.ConfigurationUserLevel.None);
                string connectionsection = config.ConnectionStrings.ConnectionStrings
                ["cn"].ConnectionString;

                ConnectionStringSettings connectionstring = null;
                if (connectionsection != null)
                {
                    config.ConnectionStrings.ConnectionStrings.Remove("cn");                    

                    App.GetMainWindowStatusBar().ShowStatusMessage("Removiendo conexion de string existente", Brushes.LightBlue, Brushes.Black, "001-interface.png");
                }

                connectionstring = new ConnectionStringSettings("cn", dataSource);
                config.ConnectionStrings.ConnectionStrings.Add(connectionstring);

                config.Save(ConfigurationSaveMode.Modified, true);
                ConfigurationManager.RefreshSection("connectionStrings");

                return true;
            }
            catch(Exception ex)
            {
                App.GetMainWindowStatusBar().ShowStatusMessage("Error General: " + ex.Message, Brushes.Red, Brushes.White, "003-interface-2.png");
                return false;
            }

            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           // LimpiarCampos();

            
        }

        public void LoadedWindow()
        {
            LimpiarCampos();

            ServerName();
        }

        private void ServerName()
        {
            txtServidor.Text = Environment.MachineName;
        }

        private void LimpiarCampos()
        {
            txtServidor.Text = "";

            txtPasswordSQL.Password = "";

            txtUsuarioSitio.Text = "";
        }
    }
}
