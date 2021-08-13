using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BeautySolutions.View.ViewModel;
using Entidades;
using LiveCharts;
using MaterialDesignThemes.Wpf;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.Win32;
using Negocio;
using Newtonsoft.Json;
using Vista.Importaciones;
using Vista.Informes_Fiscales.Formularios;
using Vista.Informes_Fiscales.Reportes;
using Vista.Produccion;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public int SwitchView
        {
            get;
            set;
        }

        string strName, imageName;

        ControladorMenu cn = new ControladorMenu();

        DataTable dt = new DataTable();

        public static DataTable dtMessages = new DataTable();

        public static LogMensajes objectLogMensajes;

        public static List<SubItem> listMenus;
        public Menu(string sociedad, string username, bool licenceValidation)
        {
            InitializeComponent();

            CreateDTMessages();

            CreateWindowLogsMensajes();
           
            SetValuesSesion(sociedad,username);

            CreateMenuListView(licenceValidation);

            this.DataContext = new NavigationViewModel();

        }

        private void CreateMenuListView(bool IsEnable)
        {
            
            listMenus= new List<SubItem>();

            //var menuProduccion = new List<SubItem>();

            //menuProduccion.Add(new SubItem("Lista de Materiales", new ListaMateriales(), IsEnable));
            //menuProduccion.Add(new SubItem("Orden de Fabricacion", new OrdenFabricacion(), IsEnable));
            //menuProduccion.Add(new SubItem("Recibo de Produccion", new ReciboProduccion(), IsEnable));
            //menuProduccion.Add(new SubItem("Emision para Produccion", new EmisionProduccion(), IsEnable));

            //listMenus.AddRange(menuProduccion);

            //var item12 = new ItemMenu("PRODUCCION", menuProduccion, PackIconKind.Creation);

            var menuImportacion = new List<SubItem>();

            menuImportacion.Add(new SubItem("Asientos", new ImportacionesAsientos(), IsEnable));
            menuImportacion.Add(new SubItem("Plan de Cuentas", new ImportacionPlanCuentas(), IsEnable));

            listMenus.AddRange(menuImportacion);

            var item11 = new ItemMenu("IMPORTACION", menuImportacion, PackIconKind.DatabaseImport);

            var menuLicencia = new List<SubItem>();

            menuLicencia.Add(new SubItem("Licencia Localizacion", new Licencia(),true));

            listMenus.AddRange(menuLicencia);

            var item10 = new ItemMenu("LICENCIA", menuLicencia, PackIconKind.License);

            var menuCierrePeriodo = new List<SubItem>();

            menuCierrePeriodo.Add(new SubItem("Cierre Periodo", new CierrePeriodo(), IsEnable));

            listMenus.AddRange(menuCierrePeriodo);

            var item9 = new ItemMenu("UTILIDADES", menuCierrePeriodo, PackIconKind.MoneyUsd);

            var menuInformesFiscales = new List<SubItem>();

            menuInformesFiscales.Add(new SubItem("Anular Comprobantes IVA", new AnularComprobanteIVA(), IsEnable));
            menuInformesFiscales.Add(new SubItem("Generar Comprobantes IVA", new GenerarRetencionesIVA(), IsEnable));
            menuInformesFiscales.Add(new SubItem("Generar XML ISLR", new Generar_XML_ISLR(), IsEnable));
            menuInformesFiscales.Add(new SubItem("Generar TXT IVA Semanal", new GenerarTXT(), IsEnable));
            menuInformesFiscales.Add(new SubItem("Generar TXT IVA Quincenal", new GenerarTXTQuincenal(), IsEnable));
            menuInformesFiscales.Add(new SubItem("Comprobante ARCV", new ComprobanteARCV(), IsEnable));
            menuInformesFiscales.Add(new SubItem("Comprobante Ret IVA Anulados", new VerComprobantesRetenciosIVAAnulados(), IsEnable));
            menuInformesFiscales.Add(new SubItem("Comprobante Ret IVA", new ComprobanteIVA(), IsEnable));
            menuInformesFiscales.Add(new SubItem("Comprobante ISLR Operacion", new ComprobanteISLROperacion(), IsEnable));
            menuInformesFiscales.Add(new SubItem("Comprobante ISLR Mensual", new ComprobanteISLRMensual(), IsEnable));          
            menuInformesFiscales.Add(new SubItem("Listado Comprobante de IVA", new ListadoComprobantesIVA(), IsEnable));
            menuInformesFiscales.Add(new SubItem("Libro Compras Semanal Rango", new LibroComprasSemanal(), IsEnable));
            menuInformesFiscales.Add(new SubItem("Libro Ventas Semanal Rango", new Libro_Ventas_Semanal(), IsEnable));
            menuInformesFiscales.Add(new SubItem("Libro Compras", new LibroCompras(), IsEnable));
            menuInformesFiscales.Add(new SubItem("Libro Ventas", new LibroVentas(), IsEnable));

            listMenus.AddRange(menuInformesFiscales);

            var item8 = new ItemMenu("INFORMES FISCALES", menuInformesFiscales, PackIconKind.Layers);

            var menuInformesFinancieros = new List<SubItem>();

            menuInformesFinancieros.Add(new SubItem("Libro Mayor", new LibroMayor(), IsEnable));
            menuInformesFinancieros.Add(new SubItem("Libro Diario", new LibroDiario(), IsEnable));
            menuInformesFinancieros.Add(new SubItem("Balance", new Balance(), IsEnable));

            listMenus.AddRange(menuInformesFinancieros);


            var item7 = new ItemMenu("INFORMES", menuInformesFinancieros, PackIconKind.Paper);

            var menuInventario = new List<SubItem>();
          
            menuInventario.Add(new SubItem("Datos Maestros Articulos", new Articulos(), IsEnable));
            menuInventario.Add(new SubItem("Informe Auditoria Stock", new InformeAuditoriaStock(), IsEnable));
            menuInventario.Add(new SubItem("Grupo Unidades Medida", new GrupoUnidadesMedida(), IsEnable));
            menuInventario.Add(new SubItem("Unidad Medida Definicion", new UnidadMedidaDefinicion(), IsEnable));

            listMenus.AddRange(menuInventario);

            var item6 = new ItemMenu("INVENTARIO", menuInventario, PackIconKind.Dropbox);

            var menuBancos = new List<SubItem>();

            menuBancos.Add(new SubItem("Pagos Recibidos", new PagoRecibido(), IsEnable));
            menuBancos.Add(new SubItem("Pagos Efectuados", new PagoEfectuado(), IsEnable));

            listMenus.AddRange(menuBancos);

            var item5 = new ItemMenu("GESTION BANCOS", menuBancos, PackIconKind.BankTransfer);

            var menuSocios = new List<SubItem>();

            menuSocios.Add(new SubItem("Datos Maestros SN", new SociosNegocio(), IsEnable));
            menuSocios.Add(new SubItem("Reconciliacion SN", new ReconciliacionSN(), IsEnable));
            menuSocios.Add(new SubItem("Gestionar Reconciliaciones", new GestionarReconciliacionesInternasAnteriores(), IsEnable));

            listMenus.AddRange(menuSocios);

            var item4 = new ItemMenu("SOCIOS NEGOCIO", menuSocios, PackIconKind.UserAdd);

            var menuCompras = new List<SubItem>();

            menuCompras.Add(new SubItem("Entrada Mercancia", new EntradaMercancia(), IsEnable));
            menuCompras.Add(new SubItem("Factura Proveedores", new FacturaProveedores(), IsEnable));
            menuCompras.Add(new SubItem("Nota Debito Proveedores", new NotaDebitoProveedores(), IsEnable));
            menuCompras.Add(new SubItem("Nota Credito Proveedores", new NotaCreditoProveedores(), IsEnable));

            listMenus.AddRange(menuCompras);

            var item3 = new ItemMenu("COMPRAS", menuCompras, PackIconKind.ShoppingCart);

            var menuVentas = new List<SubItem>();

            menuVentas.Add(new SubItem("Entrega", new EntregaMercancia(), IsEnable));
            menuVentas.Add(new SubItem("Factura Deudores", new FacturaDeudores(), IsEnable));
            menuVentas.Add(new SubItem("Nota Debito Deudores", new NotaDebitoDeudores(), IsEnable));
            menuVentas.Add(new SubItem("Nota Credito Deudores", new NotaCreditoDeudores(), IsEnable));

            listMenus.AddRange(menuVentas);

            var item2 = new ItemMenu("VENTAS", menuVentas, PackIconKind.Invoice);

            var menuFinancial = new List<SubItem>();

            menuFinancial.Add(new SubItem("Plan de Cuenta", new PlanCuentas(), IsEnable));
            menuFinancial.Add(new SubItem("Tratar Plan de Cuenta", new TratarPlanCuentas(), IsEnable));
            menuFinancial.Add(new SubItem("Asiento", new Asiento(), IsEnable));
            menuFinancial.Add(new SubItem("Diferencia Tipo de Cambio", new DiferenciaTipoCambio(), IsEnable));
            menuFinancial.Add(new SubItem("Diferencia Conversion", new DiferenciaConversion(), IsEnable));

            listMenus.AddRange(menuFinancial);

            var item1 = new ItemMenu("FINANZAS", menuFinancial, PackIconKind.Finance);

            var menuGestion = new List<SubItem>();          

            menuGestion.Add(new SubItem("Tipo de Cambio", new TipoCambio(), IsEnable));
            menuGestion.Add(new SubItem("Seleccionar Sociedad", new Selecionar_Sociedad(false), IsEnable));
            menuGestion.Add(new SubItem("Detalles de Sociedad", new DetallesSociedad(), IsEnable));
            menuGestion.Add(new SubItem("Determinacion de Mayor", new DeterminacionCuentasMayor(), IsEnable));
            menuGestion.Add(new SubItem("Periodos Contables", new PeriodosContables(), IsEnable));
            menuGestion.Add(new SubItem("Usuarios", new Usuarios(), IsEnable));
            menuGestion.Add(new SubItem("Monedas", new Monedas(), IsEnable));
            menuGestion.Add(new SubItem("Nro Comprobantes", new NroComprobante(), IsEnable));
            menuGestion.Add(new SubItem("Clases de Impuesto", new ClasesImpuestos(), IsEnable));
            menuGestion.Add(new SubItem("Codigos Fiscales", new CodigosFiscales(), IsEnable));
            menuGestion.Add(new SubItem("Retencion de Impuesto", new RetencionImpuesto(), IsEnable));

            listMenus.AddRange(menuGestion);

            var item0 = new ItemMenu("GESTION", menuGestion, PackIconKind.Settings);

            Menus.Children.Add(new UserControlMenuItem(item0, this));
            Menus.Children.Add(new UserControlMenuItem(item1, this));
            Menus.Children.Add(new UserControlMenuItem(item2, this));
            Menus.Children.Add(new UserControlMenuItem(item3, this));
            Menus.Children.Add(new UserControlMenuItem(item4, this));
            Menus.Children.Add(new UserControlMenuItem(item5, this));
            Menus.Children.Add(new UserControlMenuItem(item6, this));
            Menus.Children.Add(new UserControlMenuItem(item7, this));
            Menus.Children.Add(new UserControlMenuItem(item8, this));
            Menus.Children.Add(new UserControlMenuItem(item9, this));
            Menus.Children.Add(new UserControlMenuItem(item10, this));
            Menus.Children.Add(new UserControlMenuItem(item11, this));
            //Menus.Children.Add(new UserControlMenuItem(item12, this));


        }

        private void SetValuesSesion(string sociedad, string username)
        {
            lblSociedadActual.Text = sociedad;

            lblUsuarioActual.Text = username;
        }

        private void CreateWindowLogsMensajes()
        {
            LogMensajes log = new LogMensajes();

            objectLogMensajes = log;
        }

        private void CreateDTMessages()
        {
            dtMessages.Columns.Add("ID", typeof(int));
            dtMessages.Columns.Add("Date", typeof(DateTime));
            dtMessages.Columns.Add("Image", typeof(string));
            dtMessages.Columns.Add("Msg", typeof(string));
        }

        internal void SwitchScreen(object sender, bool? isValid)
        {
            
            dynamic screen = sender;

            if (screen != null)
            {
                if(isValid == true)
                {
                    DoubleAnimation animation = new DoubleAnimation(0, 1,
                                (Duration)TimeSpan.FromSeconds(1));
                    screen.BeginAnimation(UIElement.OpacityProperty, animation);

                    if (screen.Visibility == Visibility.Collapsed || screen.Visibility == Visibility.Hidden)
                    {
                        screen.Show();

                        screen.LoadedWindow();
                    }
                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Debe importar un archivo de licencia valido para utilizar esta funcion: ", Brushes.Red, Brushes.White, "003-interface-2.png");
                }

            }
        }

        internal void ShowScreenLog()
        {

                DoubleAnimation animation = new DoubleAnimation(0, 1,
                                 (Duration)TimeSpan.FromSeconds(1));
                objectLogMensajes.BeginAnimation(UIElement.OpacityProperty, animation);

                if (objectLogMensajes.Visibility == Visibility.Collapsed || objectLogMensajes.Visibility == Visibility.Hidden)
                {
                    objectLogMensajes.Show();

                    objectLogMensajes.LoadedWindow();
                }

            
        }

        public void ShowStatusMessage(string message, SolidColorBrush solidColorBrush, SolidColorBrush foreground, string path)
        {
            int cont = dtMessages.Rows.Count;

            cont = cont + 1; //numero de mensaje

            DateTime date= (DateTime)fechaActual.GetFechaActual(); //fecha actual 

            SetDTMessage(cont,date,path,message);

            DoubleAnimation animation = new DoubleAnimation(0, 1,
                                 (Duration)TimeSpan.FromSeconds(1));

            StatusMessage.BeginAnimation(UIElement.OpacityProperty, animation);
            statusImg.BeginAnimation(UIElement.OpacityProperty, animation);
            StatusMessage.Foreground = foreground;
            StatusMessage.Content = message;
            StatusMessage.Background = solidColorBrush;
            StatusMessage.FontWeight = FontWeights.UltraBold;            
            statusImg.Background = solidColorBrush;
            img.Source = new BitmapImage(new Uri(path, UriKind.Relative));
            var timer = new System.Timers.Timer();
            timer.Interval = 4000; //2 seconds
            timer.Elapsed += delegate (object sender, System.Timers.ElapsedEventArgs e)
            {
                //stop the timer
                timer.Stop();
                //remove the StatusMessage text using a dispatcher, because timer operates in another thread
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    StatusMessage.Content = "";
                    StatusMessage.Background = Brushes.Transparent;
                    StatusMessage.Foreground = Brushes.Transparent;
                    statusImg.Background = Brushes.Transparent;
                    img.Source = new BitmapImage(new Uri("", UriKind.Relative));
                }));
            };
            timer.Start();
        }

        private void SetDTMessage(int cont, DateTime date, string path, string message)
        {
            DataRow newRow = dtMessages.NewRow();

            newRow["ID"] = cont;

            newRow["Date"] = date;

            newRow["Image"] = path;

            newRow["Msg"] = message;

            dtMessages.Rows.Add(newRow);
           
            dtMessages.AcceptChanges();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Image_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                FileDialog fldlg = new OpenFileDialog();
                fldlg.InitialDirectory = Environment.SpecialFolder.MyPictures.ToString();
                fldlg.Filter = "Image File (*.jpg;*.bmp;*.gif;*.png)|*.jpg;*.bmp;*.gif;*.png";
                fldlg.ShowDialog();
                {
                    strName = fldlg.SafeFileName;
                    imageName = fldlg.FileName;
                    ImageSourceConverter isc = new ImageSourceConverter();
                    imgLogo.SetValue(Image.SourceProperty, isc.ConvertFromString(imageName));
                }
                fldlg = null;

                insertImageData();
            }
            catch (Exception ex)
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + "No se selecciono ninguna imagen", Brushes.Red, Brushes.White, "003-interface-2.png");
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
                        BindImage();
                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error1: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                    }

                }
            }
            catch (Exception ex)
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error2: " + ex.Message, Brushes.Red, Brushes.White, "003-interface-2.png");
            }
        }

        private void BindImage()
        {
            var BindImage = cn.BindImage();

            if (BindImage.Item2 == null)
            {
                dt = BindImage.Item1;

                LoadLogo();
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + BindImage.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BindImage();
        }

       
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("¿Esta Seguro que desea salir?", "Menu", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                Properties.Settings.Default.Usuario = 0;

                Properties.Settings.Default.Save();

                Application.Current.Shutdown();
            }
         

        }

        private void btnSetting_Click(object sender, RoutedEventArgs e)
        {
            ContextMenu cm = this.FindResource("cmButton") as ContextMenu;
            cm.PlacementTarget = sender as Button;   
            cm.IsOpen = true;
        }

        private void btnMessages_Click(object sender, RoutedEventArgs e)
        {
            ShowScreenLog();
        }


        private void Rojo_Click(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter();

            stpMenu.Background = (Brush)bc.ConvertFrom("#4CB5AE");
            stpTop.Background = (Brush)bc.ConvertFrom("#4CB5AE");
            stpTopMiddle.Background = (Brush)bc.ConvertFrom("#4CB5AE");
            gdMenu.Background= (Brush)bc.ConvertFrom("#4CB5AE");


        }

        private void Naranja_Click(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter();

            stpMenu.Background = (Brush)bc.ConvertFrom("#EF8354");
            stpTop.Background = (Brush)bc.ConvertFrom("#EF8354");
            stpTopMiddle.Background = (Brush)bc.ConvertFrom("#EF8354");
            gdMenu.Background = (Brush)bc.ConvertFrom("#EF8354");
        }

        private void Azul_Click(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter();
          
            stpMenu.Background = (Brush)bc.ConvertFrom("#FF2196F3");
            stpTop.Background = (Brush)bc.ConvertFrom("#FF2196F3");
            stpTopMiddle.Background = (Brush)bc.ConvertFrom("#FF2196F3");
            gdMenu.Background = (Brush)bc.ConvertFrom("#FF2196F3");


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
           SwitchView = 1;
            
        }

        private void LoadLogo()
        {
            foreach (DataRow row in dt.Rows)
            {
                //Store binary data read from the database in a byte array
                byte[] blob = (byte[])row[1];
                MemoryStream stream = new MemoryStream();
                stream.Write(blob, 0, blob.Length);
                stream.Position = 0;

                System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();

                MemoryStream ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);
                bi.StreamSource = ms;
                bi.EndInit();
                imgLogo.Source = bi;

                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Modificación de logo exitosa: ", Brushes.LightGreen, Brushes.Black, "001-interface.png");
            }

        }
    }

}
