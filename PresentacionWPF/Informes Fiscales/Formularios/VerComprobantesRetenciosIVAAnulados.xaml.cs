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

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para VerComprobantesRetenciosIVAAnulados.xaml
    /// </summary>
    public partial class VerComprobantesRetenciosIVAAnulados : Window
    {
        ControladorLibros cn = new ControladorLibros();

        private string desde;

        private string hasta;
        public string Desde { get => desde; set => desde = value; }
        public string Hasta { get => hasta; set => hasta = value; }

        public CrystalReportAnularComprobantesIVA CRComprobanteRetencionIVAAnulados;

        public VerComprobantesRetenciosIVAAnulados()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CreateCrystalReportComprobanteRetencionIVA();

            InicializacionBasica();
        }

        private void CreateCrystalReportComprobanteRetencionIVA()
        {
            CrystalReportAnularComprobantesIVA cr = new CrystalReportAnularComprobantesIVA();

            CRComprobanteRetencionIVAAnulados = cr;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void comboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            App.comboBox_LostFocus(sender, e);
        }

        private void comboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            App.comboBox_GotFocus(sender, e);
        }

        public void LoadedWindow()
        {
            InicializacionBasica();
        }

        private void InicializacionBasica()
        {
            var result1 = cn.ConsultaComprobantesIVAAnulados();

            if (result1.Item2 == null)
            {
                cbDesde.ItemsSource = result1.Item1;

                cbHasta.ItemsSource = result1.Item1;

            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result1.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
            }
        }

        private void btnCrear_Click(object sender, RoutedEventArgs e)
        {
            if (cbDesde.SelectedIndex > -1 && cbHasta.SelectedIndex > -1)
            {
                if (String.IsNullOrWhiteSpace(cbDesde.SelectedValue.ToString()) == false && String.IsNullOrWhiteSpace(cbHasta.SelectedValue.ToString()) == false)
                {
                    Desde = cbDesde.SelectedValue.ToString();

                    Hasta = cbHasta.SelectedValue.ToString();

                    CRComprobanteRetencionIVAAnulados.Show();

                    CRComprobanteRetencionIVAAnulados.LoadedWindow(Desde, Hasta);

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
