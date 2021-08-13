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

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para ComprobanteARCV.xaml
    /// </summary>
    public partial class ComprobanteARCV : Window
    {
        ControladorTipoCambio cn = new ControladorTipoCambio();

        private string supplier;
        public string Supplier { get => supplier; set => supplier = value; }

        private string year;
        public string Year { get => year; set => year = value; }

        public CrystalReportComprobanteARCV CRComprobanteARCV;
        public ComprobanteARCV()
        {
            InitializeComponent();
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            App.textBox_LostFocus(sender, e);
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            App.textBox_GotFocus(sender, e);
        }
        private void comboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            App.comboBox_LostFocus(sender, e);
        }

        private void comboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            App.comboBox_GotFocus(sender, e);
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        private void btnWindow_Close(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        public void LoadedWindow()
        {
            InicializacionBasica();
        }



        private void InicializacionBasica()
        {
            var result1 = cn.ConsultaYears();

            if (result1.Item2 == null)
            {
                cbYear.ItemsSource = result1.Item1;

            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result1.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CreateCrystalReportComprobanteARCV();

            InicializacionBasica();
        }

        private void CreateCrystalReportComprobanteARCV()
        {
            CrystalReportComprobanteARCV cr = new CrystalReportComprobanteARCV();

            CRComprobanteARCV = cr;
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

        private void btnCrear_Click(object sender, RoutedEventArgs e)
        {
            if (cbYear.SelectedIndex > -1 && String.IsNullOrWhiteSpace(txtProveedor.Text.ToString()) == false)
            {
                if (String.IsNullOrWhiteSpace(cbYear.SelectedValue.ToString()) == false)
                {
                    Year = cbYear.SelectedValue.ToString();

                    Supplier = txtProveedor.Text;

                    CRComprobanteARCV.Show();

                    CRComprobanteARCV.LoadedWindow(Supplier,Year);

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
    }
}
