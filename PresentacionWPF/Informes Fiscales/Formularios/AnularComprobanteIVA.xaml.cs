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

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para AnularComprobanteIVA.xaml
    /// </summary>
    public partial class AnularComprobanteIVA : Window
    {
        ControladorLibros cn = new ControladorLibros();

        ControladorAnularComprobanteIVA ca = new ControladorAnularComprobanteIVA();
        public AnularComprobanteIVA()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnCrear_Click(object sender, RoutedEventArgs e)
        {
            if (cbDesde.SelectedIndex > -1 && cbHasta.SelectedIndex > -1)
            {
                if (String.IsNullOrWhiteSpace(cbDesde.SelectedValue.ToString()) == false && String.IsNullOrWhiteSpace(cbHasta.SelectedValue.ToString()) == false)
                {
                    var result = ca.ExecuteAnularComprobantesIVA(cbDesde.SelectedValue.ToString(), cbHasta.SelectedValue.ToString());

                    if (result.Item2 == null)
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage(result.Item3, Brushes.LightGreen, Brushes.Black, "001-interface.png");
                        InicializacionBasica();
                    }
                    else
                    {
                        
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al anular algun comprobante de retencion de IVA: " + result.Item2 + " " + result.Item3, Brushes.Red, Brushes.White, "003-interface-2.png");

                        InicializacionBasica();
                    }
                        
                  
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
            var result1 = cn.ConsultaComprobantesIVA();

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
    }
}
