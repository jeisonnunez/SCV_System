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
using Vista;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para Usuarios.xaml
    /// </summary>
    public partial class Usuarios : Document
    {
        ControladorUsuario cn = new ControladorUsuario();

        private List<Entidades.Usuarios> listaResultado = new List<Entidades.Usuarios>();

        private List<Entidades.Usuarios> listaUsuarios = new List<Entidades.Usuarios>();
        public List<Entidades.Usuarios> ListaUsuarios { get => listaUsuarios; set => listaUsuarios = value; }
        public List<Entidades.Usuarios> ListaResultado { get => listaResultado; set => listaResultado = value; }

        public int UserID;

        public bool sw;

        public string password;
        public Usuarios()
        {
            InitializeComponent();
        }

        private static readonly DependencyProperty PasswordInitializedProperty =
           DependencyProperty.RegisterAttached("PasswordInitialized", typeof(bool), typeof(Usuarios), new PropertyMetadata(false));

        private static readonly DependencyProperty SettingPasswordProperty =
            DependencyProperty.RegisterAttached("SettingPassword", typeof(bool), typeof(Usuarios), new PropertyMetadata(false));

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
            DependencyProperty.RegisterAttached("Password", typeof(string), typeof(Usuarios),
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

        private void imgCrear_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            btnCrear.Content = "Crear";

            LimpiarCampos();

            ReestablecerFondo();

            txtCodigoUsuario.IsReadOnly = false;
        }

        private void imgFin_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (btnCrear.Content.ToString() == "OK" || btnCrear.Content.ToString() == "Crear")
            {              
                var result = cn.FindLast();

                if (result.Item2 == null)
                {
                    ListaResultado = result.Item1;

                    GetUsuario(ListaResultado);

                    btnCrear.Content = "OK";

                    sw = false;
                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
                }
            }

            else if (btnCrear.Content.ToString() == "Actualizar")
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("¿Desea salir sin guardar cambios?", "User", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    btnCrear.Content = "OK";

                    sw = false;
                }
            }
        }

        private void imgRight_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (btnCrear.Content.ToString() == "OK" || btnCrear.Content.ToString() == "Crear")
            {                
                var result = cn.FindNext(UserID);

                if (result.Item2 == null)
                {
                    ListaResultado = result.Item1;

                    GetUsuario(ListaResultado);

                    btnCrear.Content = "OK";

                    sw = false;
                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
                }
            }

            else if (btnCrear.Content.ToString() == "Actualizar")
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("¿Desea salir sin guardar cambios?", "User", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    btnCrear.Content = "OK";

                    sw = false;
                }
            }
        }

        private void imgleft_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (btnCrear.Content.ToString() == "OK" || btnCrear.Content.ToString() == "Crear")
            {
                var result= cn.FindPrevious(UserID);

                if (result.Item2 == null)
                {
                    ListaResultado = result.Item1;

                    GetUsuario(ListaResultado);

                    btnCrear.Content = "OK";

                    sw = false;
                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
                }

            }

            else if (btnCrear.Content.ToString() == "Actualizar")
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("¿Desea salir sin guardar cambios?", "User", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    btnCrear.Content = "OK";

                    sw = false;
                }
            }
        }

        private void imgInicio_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (btnCrear.Content.ToString() == "OK" || btnCrear.Content.ToString() == "Crear")
            { 
                var result= cn.FindFirst();

                if (result.Item2 == null)
                {
                    ListaResultado = result.Item1;

                    GetUsuario(ListaResultado);

                    btnCrear.Content = "OK";

                    sw = false;
                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
                }

            }

            else if (btnCrear.Content.ToString() == "Actualizar")
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("¿Desea salir sin guardar cambios?", "User", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    btnCrear.Content = "OK";

                    sw = false;
                }
            }
        }

        private void imgBuscar_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            InicializacionBasica();
        }

        public void InicializacionBasica()
        {
            btnCrear.Content = "Buscar";

            LimpiarCampos();

            EstablecerFondo();
        }

        public void LoadedWindow()
        {
            InicializacionBasica();
        }

        private void EstablecerFondo()
        {
            txtCodigoUsuario.Background = Brushes.LightBlue;

            txtNombreUsuario.Background = Brushes.LightBlue;           

            chxBloqueado.Background = Brushes.LightBlue; //REVISAR
        }

        private void ReestablecerFondo()
        {
            txtCodigoUsuario.Background = Brushes.White;

            txtNombreUsuario.Background = Brushes.White;           

            chxBloqueado.Background = Brushes.White; //REVISAR
        }

        private void LimpiarCampos()
        {
            txtCodigoUsuario.Text = "";

            txtCodigoUsuario.IsReadOnly = false;

            txtNombreUsuario.Text = "";          

            chxBloqueado.IsChecked = false;
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {

            if (btnCrear.Content.ToString() != "Buscar")
            {
                App.textBox_GotFocus(sender, e);
            }
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (btnCrear.Content.ToString() != "Buscar")
            {
                App.textBox_LostFocus(sender, e);
            }
               
        }

        private void password_GotFocus(object sender, RoutedEventArgs e)
        {
            if (btnCrear.Content.ToString() != "Buscar")
            {
                App.password_GotFocus(sender, e);
            }               

        }

        private void password_LostFocus(object sender, RoutedEventArgs e)
        {
            if (btnCrear.Content.ToString() != "Buscar")
            {
                App.password_LostFocus(sender, e);
            }
              
        }

        private void btnClave_Click(object sender, RoutedEventArgs e)
        {
            Registro ventanaRegistro = new Registro(false);

            ventanaRegistro.ShowDialog();

            if (ventanaRegistro.Visibility == Visibility.Hidden)
            {
                password=ventanaRegistro.GetUsuarioPassword();

                sw = true;

                if (btnCrear.Content.ToString() == "OK")
                {
                    btnCrear.Content = "Actualizar";
                }
                               
            }

            
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        private void btnCrear_Click(object sender, RoutedEventArgs e)
        {
            List<Entidades.Usuarios> listaUsuarios = new List<Entidades.Usuarios>();

            Entidades.Usuarios usuarios = new Entidades.Usuarios();

            switch (btnCrear.Content.ToString())
            {
                case "OK":

                    this.Hide();

                    break;

                case "Buscar":                  

                    usuarios.User_code = txtCodigoUsuario.Text;

                    usuarios.User_name = txtNombreUsuario.Text;

                    usuarios.Locked = cn.EstadoBloqueado(chxBloqueado.IsChecked);

                    listaUsuarios.Add(usuarios);

                    var result = cn.ConsultaUsuarios(listaUsuarios);

                    if (result.Item2 == null)
                    {
                        RecorreListaUsuarios(result.Item1);
                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("En la creacion del usuario: " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
                    }

                    break;

                case "Crear":

                    usuarios.User_code = txtCodigoUsuario.Text;

                    usuarios.User_name = txtNombreUsuario.Text;

                    usuarios.Locked = cn.EstadoBloqueado(chxBloqueado.IsChecked);

                    usuarios.Password = password;

                    usuarios.Deleted = 'Y';

                    listaUsuarios.Add(usuarios);

                    var result1 = cn.regUsuario(listaUsuarios);

                    if (result1.Item1==1)
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Usuario se creo correctamente " + usuarios.User_code, Brushes.LightGreen, Brushes.Black, "001-interface.png");                       

                        var resultUser= cn.ObtenerIdUser(txtCodigoUsuario.Text);

                        UserID = resultUser.Item1;
                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("En la creacion del usuario: " + result1.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
                        
                    }

                    break;

                case "Actualizar":
                   
                    usuarios.UserID = UserID;

                    usuarios.User_code = txtCodigoUsuario.Text;

                    usuarios.User_name = txtNombreUsuario.Text;

                    usuarios.Locked = cn.EstadoBloqueado(chxBloqueado.IsChecked);

                    if (sw == true) {

                        var result2 = cn.UpdatePassword(password, UserID);

                        if (result2.Item1==1)
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Clave de acceso actualizada correctamente ", Brushes.LightGreen, Brushes.Black, "001-interface.png");
                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("En la actualizacion de la clave de acceso: " + result2.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");                            
                        }
                    }

                    listaUsuarios.Add(usuarios);

                    var result3 = cn.UpdateUser(listaUsuarios);

                    if (result3.Item1 == 1)
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Usuario se actualizo correctamente", Brushes.LightGreen, Brushes.Black, "001-interface.png");                      

                        // UserID = cn.ObtenerIdUser(txtCodigoUsuario.Text);

                        sw = false;

                        btnCrear.Content = "OK";
                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error en la actualizacion del usuario: " + result3.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
                        
                    }

                    break;
            }          

        }

        private void RecorreListaUsuarios(List<Entidades.Usuarios> newListaUsuarios)
        {
            if (newListaUsuarios.Count == 1)
            {
                GetUsuario(newListaUsuarios);

                btnCrear.Content = "OK";
            }
            else if (newListaUsuarios.Count == 0)
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se encontro ningun resultado con los parametros establecidos", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");
               
                LimpiarCampos();

                btnCrear.Content = "OK";
            }
            else if(newListaUsuarios.Count > 1)
            {
                ListaUsuarios ventanaListBox = new ListaUsuarios(newListaUsuarios);

                ventanaListBox.ShowDialog();

                if (ventanaListBox.Visibility == Visibility.Hidden)
                {
                    if (ventanaListBox.GetListUsuarios().Count == 0)
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No selecciono ningun elemento", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");

                        LimpiarCampos();
                    }
                    else
                    {

                        GetUsuario(ventanaListBox.GetListUsuarios());

                    }

                    btnCrear.Content = "OK";

                    txtCodigoUsuario.IsReadOnly = true;
                }



            }

            ReestablecerFondo();
        }

        private void GetUsuario(List<Entidades.Usuarios> listaUsuarios)
        {
            foreach (Entidades.Usuarios usuarios in listaUsuarios)
            {
                txtCodigoUsuario.Text = usuarios.User_code;

                txtNombreUsuario.Text = usuarios.User_name;              
                
                chxBloqueado.IsChecked = cn.EstadoBloqueado(usuarios.Locked);

                UserID = usuarios.UserID;

            }
        }

        private void txtCodigoUsuario_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (btnCrear.Content.ToString() == "OK")
            {
                btnCrear.Content = "Actualizar";
            }
        }

        private void chxBloqueado_Click(object sender, RoutedEventArgs e)
        {
            if (btnCrear.Content.ToString() == "OK")
            {
                btnCrear.Content = "Actualizar";
            }
        }

        private void Grid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {          

            if (btnCrear.Content.ToString() == "OK")
            {

                MessageBoxResult messageBoxResult = MessageBox.Show("¿Desea eliminar el usuario?", "User", MessageBoxButton.YesNo, MessageBoxImage.Warning);


                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    string userCode = txtCodigoUsuario.Text;

                    if (userCode != null)
                    {                    
                                               
                        var result= cn.DeleteUser(userCode);

                        if (result.Item1 == 1)
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Usuario: " + userCode + " se elimino correctamente", Brushes.LightGreen, Brushes.Black, "001-interface.png");                            

                            LimpiarCampos();
                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se puede eliminar el usuario: " + userCode + " porque se realizo una transaccion con el mismo: " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
                           
                        }
                    }

                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se selecciono ningun usuario", Brushes.Red, Brushes.Black, "002-interface-1.png");
                        
                    }
                }
            }
        }

    }
}
