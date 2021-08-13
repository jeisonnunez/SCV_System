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
using Negocio.Controlador_Informes_Fiscales;
using Vista.Informes_Fiscales.Reportes_WPF;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para LibroCompras.xaml
    /// </summary>
    public partial class LibroCompras : Window
    {
        private string code;
        public string Code { get => code; set => code = value; }

        ControladorLibros cn = new ControladorLibros();

        public CrystalReportLibroCompras CRLibroCompras;

        public LibroCompras()
        {
            InitializeComponent();
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CreateCrystaLibroCompras();

            InicializacionBasica();
        }

        private void CreateCrystaLibroCompras()
        {
            CrystalReportLibroCompras cr = new CrystalReportLibroCompras();

            CRLibroCompras = cr;
        }

        public void LoadedWindow()
        {
            InicializacionBasica();
        }

        private void InicializacionBasica()
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

        private void btnCrear_Click(object sender, RoutedEventArgs e)
        {
            if (cbPeriodo.SelectedIndex > -1)
            {
                if(String.IsNullOrWhiteSpace(cbPeriodo.SelectedValue.ToString()) == false)
                {
                    Code = cbPeriodo.SelectedValue.ToString();

                    CRLibroCompras.Show();

                    CRLibroCompras.LoadedWindow(Code);

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
