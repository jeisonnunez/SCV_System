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
using Entidades;
using Negocio;
using Negocio.Controlador_Inicio;
using Vista.Inicio.ValidationErrorUsuarioSitio;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para UsuarioSitio.xaml
    /// </summary>
    public partial class UsuarioSitio : Window
    {
        ControladorUsuarioSitio cn = new ControladorUsuarioSitio();

        Empresa objectEnterprise;
        public UsuarioSitio()
        {
            InitializeComponent();
        }

        private static readonly DependencyProperty PasswordInitializedProperty =
           DependencyProperty.RegisterAttached("PasswordInitialized", typeof(bool), typeof(UsuarioSitio), new PropertyMetadata(false));

        private static readonly DependencyProperty SettingPasswordProperty =
            DependencyProperty.RegisterAttached("SettingPassword", typeof(bool), typeof(UsuarioSitio), new PropertyMetadata(false));

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
            DependencyProperty.RegisterAttached("Password", typeof(string), typeof(UsuarioSitio),
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

        private Tuple<bool, string> ValidateFormUsuarioSitio()
        {
            bool sw = false;

            string firstError = null;

            foreach (KeyValuePair<string, string> errrors in ValidationErrorUsuarioSitio.ErrorCollectionMessages)
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

            return Tuple.Create(sw, firstError);
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var resultValidateForm = ValidateFormUsuarioSitio();

            bool sw = resultValidateForm.Item1;

            string error = resultValidateForm.Item2;

            if (sw == true)
            {
                App.GetMainWindowStatusBar().ShowStatusMessage("Error en algun campo del formulario: " + error, Brushes.Red, Brushes.White, "003-interface-2.png");
            }
            else
            {
                var resultConnectionTest = cn.VerifyUserAdministratorSQLServer(txtUsuarioSitio.Text,txtPasswordSQL.Password, "");

                if(resultConnectionTest.Item1==true && resultConnectionTest.Item2 == null)
                {                    
                    objectEnterprise.Show();

                    objectEnterprise.LoadedWindow();

                    this.Hide();

                    App.GetMainWindowStatusBar().Hide();
                }
                else
                {
                    App.GetMainWindowStatusBar().ShowStatusMessage("Error. Usuario y/o contraseña incorrecta: " + resultConnectionTest.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                }
            }
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CreateTableEnterprise();
        }

        private void CreateTableEnterprise()
        {
            Empresa empresa = new Empresa();

            objectEnterprise = empresa;
        }
    }
}
