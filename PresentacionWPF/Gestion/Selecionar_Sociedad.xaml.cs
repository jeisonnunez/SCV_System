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
using BeautySolutions.View.ViewModel;
using Microsoft.SqlServer.Management.Smo.Broker;
using Negocio;
using Vista.Gestion.ValidateErrorsSeleccionarSociedad;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para Selecionar_Sociedad.xaml
    /// </summary>
    public partial class Selecionar_Sociedad : Window
    {
        ControladorSeleccionarSociedad cn = new ControladorSeleccionarSociedad();

        EstableceLogin estableceLogin = new EstableceLogin();

        UsuarioSitio objectUserSite;

        private static bool closeWindow;
        public static bool CloseWindow { get => closeWindow; set => closeWindow = value; }

        private static bool closeMenu;  
        public static bool CloseMenu { get => closeMenu; set => closeMenu = value; }

        private static readonly DependencyProperty PasswordInitializedProperty =
           DependencyProperty.RegisterAttached("PasswordInitialized", typeof(bool), typeof(Selecionar_Sociedad), new PropertyMetadata(false));

        private static readonly DependencyProperty SettingPasswordProperty =
            DependencyProperty.RegisterAttached("SettingPassword", typeof(bool), typeof(Selecionar_Sociedad), new PropertyMetadata(false));

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
            DependencyProperty.RegisterAttached("Password", typeof(string), typeof(Selecionar_Sociedad),
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

        public Selecionar_Sociedad(bool close)
        {
            CloseWindow = close;

            InitializeComponent();

            btnOk.Click += delegate (object sender, RoutedEventArgs e) { button_Click(sender, e,new Window()) ; };
        }

     
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (CloseWindow == true)
            {
                Application.Current.Shutdown();
            }
            else
            {
                
                cn.EstableceConexionString(cn.obtenerEmpresa());

                cn.obtenerCadenaConexion();

                this.Hide();
            }

        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {            
            objectUserSite.Show();

            this.Hide();
        }

        private void Selected_Company(object sender, SelectionChangedEventArgs e)
        {

            DataGrid dg = (DataGrid)sender;

            DataRowView row_Selected = dg.SelectedItem as DataRowView;

            if (row_Selected != null)
            {
                txtBBDD.Text = row_Selected["name"].ToString();
            }
        }

        private Tuple<bool, string> ValidateFormSeleccionarSociedad()
        {
            bool sw = false;

            string firstError = null;

            foreach (KeyValuePair<string, string> errrors in ValidateErrorsSeleccionarSociedad.ErrorCollectionMessages)
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

        private void button_Click(object sender, RoutedEventArgs e, Window menu)
        {
            CloseMenu = CloseWindow;

            if (CloseMenu == false)
            {
                var resultValidateForm = ValidateFormSeleccionarSociedad();

                bool sw = resultValidateForm.Item1;

                string error = resultValidateForm.Item2;

                if (sw == true)
                {
                    App.GetMainWindowStatusBar().ShowStatusMessage("Error en algun campo del formulario: " + error, Brushes.Red, Brushes.White, "003-interface-2.png");
                }
            }
            else
            {
                var resultValidateForm = ValidateFormSeleccionarSociedad();

                bool sw = resultValidateForm.Item1;

                string error = resultValidateForm.Item2;

                if (sw == true)
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error en algun campo del formulario: " + error, Brushes.Red, Brushes.White, "003-interface-2.png");
                }
                else
                {
                    List<Entidades.Usuarios> listaUsuario = new List<Entidades.Usuarios>();

                    Entidades.Usuarios usuarios = new Entidades.Usuarios();

                    usuarios.Password = txtClave.Password;

                    usuarios.User_code = txtUsuario.Text;

                    usuarios.Sociedad = txtBBDD.Text;

                    listaUsuario.Add(usuarios);

                    cn.EstableceConexionString("master");

                    cn.obtenerCadenaConexion();

                    var result = cn.conSQL(listaUsuario);

                    if (result.Item1 != null)
                    {
                        cn.EstableceConexionString(txtBBDD.Text);

                        cn.obtenerCadenaConexion();

                        estableceLogin.UltimaSociedad(txtBBDD.Text);

                        var result1 = cn.ObtenerIdUser(txtUsuario.Text);

                        estableceLogin.UsuarioActual(txtUsuario.Text, result1.Item1);

                        if (CloseMenu == false)
                        {

                            foreach (SubItem subItem in Menu.listMenus)
                            {
                                subItem.Screen.Close();
                            }

                            EstableceLogin.GetMenu().Close();

                            EstableceLogin.GetMediosPago().Close();

                            for (int i = TablaRetencionImpuesto.dt.Columns.Count - 1; i >= 0; i--)
                            {
                                DataColumn dc = TablaRetencionImpuesto.dt.Columns[i];

                                TablaRetencionImpuesto.dt.Columns.Remove(dc);

                            }

                            EstableceLogin.GetTablaRetencionImpuesto().Close();

                            EstableceLogin.GetMenuStatusBar().Close();

                            Menu.dtMessages.Clear();

                            for (int i = Menu.dtMessages.Columns.Count - 1; i >= 0; i--)
                            {
                                DataColumn dc = Menu.dtMessages.Columns[i];

                                Menu.dtMessages.Columns.Remove(dc);

                            }

                            Menu.dtMessages.AcceptChanges();

                            Menu.listMenus.Clear();


                        }

                        var result2 = cn.GetNameSociety();

                        estableceLogin.IngresoSistema(result2.Item1, txtUsuario.Text);

                        this.Hide();

                        App.GetMainWindowStatusBar().Hide();

                    }
                    else
                    {
                        App.GetMainWindowStatusBar().ShowStatusMessage("Error. Usuario y/o contraseña incorrecta: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                    }
                }
            }

           

        }

        private void btnChangeUser_Click(object sender, RoutedEventArgs e)
        {
            txtUsuario.Text = "";

            txtClave.Password = "";
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

        private void dgCompany_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (CloseWindow == true)
            {
                Application.Current.Shutdown();
            }
            else
            {
                cn.EstableceConexionString(cn.obtenerEmpresa());

                cn.obtenerCadenaConexion();

                App.Window_Closing(sender, e);
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CreateTableUserSite();            
        }

        private void CreateTableUserSite()
        {
            UsuarioSitio siteUser = new UsuarioSitio();

            objectUserSite = siteUser;
        }

        public void LoadedWindow()
        {
            InicializacionBasica();
        }

        public void InicializacionBasica()
        {
            try
            {
                var result = cn.ConsultaSociedades();

                if (result.Item2 == null)
                {
                    dgCompany.ItemsSource = result.Item1;

                    cn.EstableceConexionString(cn.obtenerEmpresa());

                    cn.obtenerCadenaConexion();
                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                }
            }
            catch(Exception ex)
            {
                App.GetMainWindowStatusBar().ShowStatusMessage("Error: " + ex.Message, Brushes.Red, Brushes.White, "003-interface-2.png");
            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {

        }


    }
}
