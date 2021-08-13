using Entidades;
using Negocio;
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
using Vista.Informes_Fiscales.Reportes_WPF;

namespace Vista.Informes_Fiscales.Formularios
{
    /// <summary>
    /// Lógica de interacción para ListadoComprobantesIVA.xaml
    /// </summary>
    public partial class ListadoComprobantesIVA : Window
    {
        ControladorTipoCambio cn = new ControladorTipoCambio();

        private bool sw;

        private string supplier;
        public string Supplier { get => supplier; set => supplier = value; }

        private string supplier1;
        public string Supplier1 { get => supplier1; set => supplier1 = value; }

        private DateTime? desde;
        public DateTime? Desde { get => desde; set => desde = value; }

        private DateTime? hasta;
        public DateTime? Hasta { get => hasta; set => hasta = value; }
        public bool Sw { get => sw; set => sw = value; }

        public CrystalReportListadoComprobantesIVA CRListadoComprobantesIVA;

        public ListadoComprobantesIVA()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CreateCrystalReportListadoComprobantesIVA();
        }

        private void CreateCrystalReportListadoComprobantesIVA()
        {
            CrystalReportListadoComprobantesIVA cr = new CrystalReportListadoComprobantesIVA();

            CRListadoComprobantesIVA = cr;
        }

        private void btnCrear_Click(object sender, RoutedEventArgs e)
        {
            
           if ((String.IsNullOrWhiteSpace(txtProveedor.ToString()) == false) && (String.IsNullOrWhiteSpace(txtProveedor1.ToString()) == false) && (String.IsNullOrWhiteSpace(dpDesde.SelectedDate.ToString()) == false) && (String.IsNullOrWhiteSpace(dpHasta.SelectedDate.ToString()) == false))
           {
                Desde = dpDesde.SelectedDate;

                Hasta = dpHasta.SelectedDate;

                Supplier = txtProveedor.Text;

                Supplier1 = txtProveedor1.Text;

                CRListadoComprobantesIVA.Show();

                CRListadoComprobantesIVA.LoadedWindow(Supplier,Supplier1,Desde,Hasta);

           }
           else
           {
               EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Parametros vacios", Brushes.Red, Brushes.White, "003-interface-2.png");
           }
           
        }

        public void LoadedWindow()
        {
            InicializacionBasica();
        }

        private void InicializacionBasica()
        {
           
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void imgProveedor_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Sw = false;

            var result = cn.FindSuppliers();

            if (result.Item2 == null)
            {
                RecorreListaSN(result.Item1);
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
            }
        }

        private void imgProveedor1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Sw = true;

            var result = cn.FindSuppliers();

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

                        if (Sw==false)
                        {
                            txtProveedor.Background = Brushes.White;

                            dpProveedor.Background = Brushes.White;

                            bdProveedor.Background = Brushes.White;

                            txtProveedor.Background = Brushes.White;
                        }
                        else if (Sw == true)
                        {

                            txtProveedor1.Background = Brushes.White;

                            dpProveedorH.Background = Brushes.White;

                            bdProveedorH.Background = Brushes.White;

                            txtProveedor1.Background = Brushes.White;
                        }

                    }
                }
            }
        }

        private void comboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            App.comboBox_LostFocus(sender, e);
        }

        private void comboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            App.comboBox_GotFocus(sender, e);
        }

        private void GetSocioNegocio(List<SocioNegocio> listSuppliers)
        {
            foreach (SocioNegocio Suppliers in listSuppliers)
            {
                if (Sw == false)
                {
                    txtProveedor.Text = Suppliers.CardCode;
                }
                else if (Sw == true)
                {

                    txtProveedor1.Text = Suppliers.CardCode;
                }

                
            }
        }

        private void txtProveedor_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Name == "txtProveedor")
            {
                dpProveedor.Background = Brushes.LightBlue;

                bdProveedor.Background = Brushes.LightBlue;

                txtProveedor.Background = Brushes.LightBlue;

                imgProveedor.Visibility = Visibility.Visible;
            }
            else if(textBox.Name == "txtProveedor1")
            {

                dpProveedorH.Background = Brushes.LightBlue;

                bdProveedorH.Background = Brushes.LightBlue;

                txtProveedor1.Background = Brushes.LightBlue;

                imgProveedorH.Visibility = Visibility.Visible;
            }
        }

        private void txtProveedor_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Name == "txtProveedor")
            {
                dpProveedor.Background = Brushes.White;

                bdProveedor.Background = Brushes.White;

                txtProveedor.Background = Brushes.White;

                imgProveedor.Visibility = Visibility.Hidden;
            }
            else if (textBox.Name == "txtProveedor1")
            {
                dpProveedorH.Background = Brushes.White;

                bdProveedorH.Background = Brushes.White;

                txtProveedor1.Background = Brushes.White;

                imgProveedorH.Visibility = Visibility.Hidden;
            }
        }

       
    }
}
