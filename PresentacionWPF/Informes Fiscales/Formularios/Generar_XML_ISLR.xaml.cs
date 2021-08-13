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

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para Generar_XML_ISLR.xaml
    /// </summary>
    public partial class Generar_XML_ISLR : Window
    {
        ControladorLibros cn = new ControladorLibros();

        public XMLFile dtXML;
        public Generar_XML_ISLR()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void btnCrear_Click(object sender, RoutedEventArgs e)
        {
            switch (btnCrear.Content.ToString())
            {
                case "OK":

                    string code = txtCodigoPeriodo.Text;

                    var result = cn.ExecuteXML(code);

                    if (result.Item2 == null)
                    {

                        if (result.Item1.Rows.Count >= 1)
                        {
                            dtXML.ClearDatatable();

                            dtXML.Show();

                            dtXML.LoadedWindow();

                            dtXML.SetDataTable(result.Item1);

                            dtXML.CreateXMLFile();
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

        private void txtCodigoPeriodo_GotFocus(object sender, RoutedEventArgs e)
        {
            dpCodigoPeriodo.Background = Brushes.LightBlue;

            bdCodigoPeriodo.Background = Brushes.LightBlue;

            txtCodigoPeriodo.Background = Brushes.LightBlue;

            imgCodigoPeriodo.Visibility = Visibility.Visible;
        }

        private void txtCodigoPeriodo_LostFocus(object sender, RoutedEventArgs e)
        {
            dpCodigoPeriodo.Background = Brushes.White;

            bdCodigoPeriodo.Background = Brushes.White;

            txtCodigoPeriodo.Background = Brushes.White;

            imgCodigoPeriodo.Visibility = Visibility.Hidden;
        }

        private void imgCodigoPeriodo_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var result = cn.ConsultaPeriodoISLR();

            if (result.Item2 == null)
            {
                RecorreListaPeriodo(result.Item1);
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
            }
        }

        private void RecorreListaPeriodo(List<PeriodoISLR> listPeriodoISLR)
        {
            if (listPeriodoISLR.Count == 1)
            {
                GetPeriodo(listPeriodoISLR);


            }
            else if (listPeriodoISLR.Count == 0)
            {

                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se encontro ningun resultado con los parametros establecidos", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");

            }

            else if (listPeriodoISLR.Count > 1)
            {
                ListaPeriodoISLR ventanaListBox = new ListaPeriodoISLR(listPeriodoISLR);

                ventanaListBox.ShowDialog();

                if (ventanaListBox.Visibility == Visibility.Hidden)
                {
                    if (ventanaListBox.GetListPeriodoISLR().Count == 0)
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se selecciono ningun elemento", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");

                    }
                    else
                    {

                        GetPeriodo(ventanaListBox.GetListPeriodoISLR());

                        txtCodigoPeriodo.Background = Brushes.White;

                        dpCodigoPeriodo.Background = Brushes.White;

                        bdCodigoPeriodo.Background = Brushes.White;

                    }
                }
            }
        }

        private void GetPeriodo(List<PeriodoISLR> listPeriodoISLR)
        {
            foreach (PeriodoISLR PeriodoISLR in listPeriodoISLR)
            {
                txtCodigoPeriodo.Text = PeriodoISLR.Code;
            }
        }

        public void LoadedWindow()
        {
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            txtCodigoPeriodo.Text = "";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CreateTableXMLISLR();
        }

        private void CreateTableXMLISLR()
        {
            XMLFile tablaXML = new XMLFile();

            dtXML = tablaXML;
        }
    }
}
