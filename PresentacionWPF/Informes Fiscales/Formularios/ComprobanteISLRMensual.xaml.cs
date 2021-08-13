using Entidades;
using Negocio.Controlador_Informes_Fiscales;
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
    /// Lógica de interacción para ComprobanteISLRMensual.xaml
    /// </summary>
    public partial class ComprobanteISLRMensual : Window
    {
        private string code;
        public string Code { get => code; set => code = value; }

        private string supplier;
        public string Supplier { get => supplier; set => supplier = value; }

        public CrystalReportComprobanteISLRMensual CRComprobanteISLRMensual;

        ControladorLibros cn = new ControladorLibros();
        public ComprobanteISLRMensual()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                CreateCrystalReportComprobanteISLRMensual();

                InicializacionBasica();

                Console.WriteLine("Todo Bien");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);

                MessageBox.Show(ex.Message);

                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + ex.Message, Brushes.Red, Brushes.White, "003-interface-2.png");
            }
           
        }

        private void CreateCrystalReportComprobanteISLRMensual()
        {
            CrystalReportComprobanteISLRMensual cr = new CrystalReportComprobanteISLRMensual();

            CRComprobanteISLRMensual = cr;
        }

        public void LoadedWindow()
        {
            InicializacionBasica();
        }

        private void InicializacionBasica()
        {
            try
            {
                var result1 = cn.ConsultaPeriodos();

                if (result1.Item2 == null)
                {
                    cbPeriodo.ItemsSource = result1.Item1;

                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result1.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                }
            }
            catch (Exception ex)
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + ex.Message, Brushes.Red, Brushes.White, "003-interface-2.png");
            }
            
        }

        private void btnCrear_Click(object sender, RoutedEventArgs e)
        {
            if (cbPeriodo.SelectedIndex > -1)
            {
                if ((String.IsNullOrWhiteSpace(cbPeriodo.SelectedValue.ToString()) == false) && (String.IsNullOrWhiteSpace(txtProveedor.Text.ToString()) == false))
                {
                    Code = cbPeriodo.SelectedValue.ToString();

                    Supplier = txtProveedor.Text;

                    CRComprobanteISLRMensual.Show();

                    CRComprobanteISLRMensual.LoadedWindow(Code, Supplier);

                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se ha seleccionado ningun periodo", Brushes.Red, Brushes.White, "003-interface-2.png");
                }

            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se ha seleccionado ningun periodo", Brushes.Red, Brushes.White, "003-interface-2.png");
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void imgProveedor_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
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

                        txtProveedor.Background = Brushes.White;

                        dpProveedor.Background = Brushes.White;

                        bdProveedor.Background = Brushes.White;

                        txtProveedor.Background = Brushes.White;

                    }
                }
            }
        }

        private void GetSocioNegocio(List<SocioNegocio> listSuppliers)
        {
            foreach (SocioNegocio Suppliers in listSuppliers)
            {
                txtProveedor.Text = Suppliers.CardCode;
            }
        }

        private void txtProveedor_GotFocus(object sender, RoutedEventArgs e)
        {

            dpProveedor.Background = Brushes.LightBlue;

            bdProveedor.Background = Brushes.LightBlue;

            txtProveedor.Background = Brushes.LightBlue;

            imgProveedor.Visibility = Visibility.Visible;

        }

        private void txtProveedor_LostFocus(object sender, RoutedEventArgs e)
        {
            dpProveedor.Background = Brushes.White;

            bdProveedor.Background = Brushes.White;

            txtProveedor.Background = Brushes.White;

            imgProveedor.Visibility = Visibility.Hidden;
        }

        private void comboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            App.comboBox_LostFocus(sender, e);
        }

        private void comboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            App.comboBox_GotFocus(sender, e);
        }


    }
}
