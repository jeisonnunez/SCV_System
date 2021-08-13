using Entidades;
using Negocio.Controlador_Socio_Negocio;
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
    /// Lógica de interacción para GestionarReconciliacionesInternasAnteriores.xaml
    /// </summary>
    public partial class GestionarReconciliacionesInternasAnteriores : Window
    {
        private bool swSN;

        public bool SwSN { get => swSN; set => swSN = value; }

        public TablaReconciliacionesInternasAnteriores dtReconciliacionesInternas;

        ControladorGestionarReconciliacionesAnteriores cn = new ControladorGestionarReconciliacionesAnteriores();

        public GestionarReconciliacionesInternasAnteriores()
        {
            InitializeComponent();
        }

        public void LoadedWindow()
        {

        }

        private void imgHastaSN_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SwSN = true;

            var result = cn.FindBP();

            if (result.Item2 == null)
            {
                RecorreListaSN(result.Item1);
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
            }
        }

        private void InicializacionBasica()
        {
            
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            App.textBox_LostFocus(sender, e);
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            App.textBox_GotFocus(sender, e);
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

                        if (SwSN == false)
                        {
                            txtDesdeSN.Background = Brushes.White;

                            dpDesdeSN.Background = Brushes.White;

                            bdDesdeSN.Background = Brushes.White;

                        }
                        else if (SwSN == true)
                        {

                            txtHastaSN.Background = Brushes.White;

                            dpHastaSN.Background = Brushes.White;

                            bdHastaSN.Background = Brushes.White;

                        }

                    }
                }
            }
        }


        private void GetSocioNegocio(List<SocioNegocio> listSuppliers)
        {
            foreach (SocioNegocio Suppliers in listSuppliers)
            {
                if (SwSN == false)
                {
                    txtDesdeSN.Text = Suppliers.CardCode;
                }
                else if (SwSN == true)
                {

                    txtHastaSN.Text = Suppliers.CardCode;
                }


            }
        }

        private void txtSN_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Name == "txtDesdeSN")
            {
                dpDesdeSN.Background = Brushes.LightBlue;

                bdDesdeSN.Background = Brushes.LightBlue;

                txtDesdeSN.Background = Brushes.LightBlue;

                imgDesdeSN.Visibility = Visibility.Visible;
            }
            else if (textBox.Name == "txtHastaSN")
            {

                dpHastaSN.Background = Brushes.LightBlue;

                bdHastaSN.Background = Brushes.LightBlue;

                txtHastaSN.Background = Brushes.LightBlue;

                imgHastaSN.Visibility = Visibility.Visible;
            }
        }

        private void txtSN_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Name == "txtDesdeSN")
            {
                dpDesdeSN.Background = Brushes.White;

                bdDesdeSN.Background = Brushes.White;

                txtDesdeSN.Background = Brushes.White;

                imgDesdeSN.Visibility = Visibility.Hidden;
            }
            else if (textBox.Name == "txtHastaSN")
            {

                dpHastaSN.Background = Brushes.White;

                bdHastaSN.Background = Brushes.White;

                txtHastaSN.Background = Brushes.White;

                imgHastaSN.Visibility = Visibility.Hidden;
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void imgDesdeSN_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SwSN = false;

            var result = cn.FindBP();

            if (result.Item2 == null)
            {
                RecorreListaSN(result.Item1);
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CreateTableGestionarReconciliacionesAnteriores();
        }

        private void CreateTableGestionarReconciliacionesAnteriores()
        {
            TablaReconciliacionesInternasAnteriores tablaReconciliacionesInternas = new TablaReconciliacionesInternasAnteriores();

            dtReconciliacionesInternas = tablaReconciliacionesInternas;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            switch (btnOK.Content.ToString())
            {
                case "OK":

                    var result = cn.ExecuteReconciliacionesAnteriores(txtDesdeSN.Text, txtHastaSN.Text, dpDFechaReconciliacion.SelectedDate, dpHFechaReconciliacion.SelectedDate, Convert.ToInt32(txtDNroReconciliacion.Text),Convert.ToInt32(txtHNroReconciliacion.Text));

                    if (result.Item2 == null)
                    {

                        if (result.Item1.Rows.Count >= 1)
                        {
                            dtReconciliacionesInternas.ClearDatatable();

                            dtReconciliacionesInternas.Show();                            

                            dtReconciliacionesInternas.SetResult(result.Item1);
                           
                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se encontraron resultados con los parametros establecidos: " + result.Item2, Brushes.LightBlue, Brushes.Black, "002-interface-1.png");
                        }
                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                    }

                    break;

            }
        }
    }
}
