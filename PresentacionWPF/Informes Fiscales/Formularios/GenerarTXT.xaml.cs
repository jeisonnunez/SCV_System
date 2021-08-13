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
    /// Lógica de interacción para GenerarTXT.xaml
    /// </summary>
    public partial class GenerarTXT : Window
    {
        ControladorLibros cn = new ControladorLibros();

        public TXTFile dtTXT;
        public GenerarTXT()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CreateTableTXT();
        }

        private void btnCrear_Click(object sender, RoutedEventArgs e)
        {
            switch (btnCrear.Content.ToString())
            {
                case "OK":

                    DateTime date = Convert.ToDateTime(txtFechaContabilizacion.Text);

                    var result = cn.ExecuteTXT(date);

                    if (result.Item2 == null)
                    {

                        if (result.Item1.Rows.Count >= 1)
                        {
                            dtTXT.ClearDatatable();

                            dtTXT.Show();

                            dtTXT.LoadedWindow();

                            dtTXT.SetDataTable(result.Item1);

                            dtTXT.CreateTXTFile();
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

        private void CreateTableTXT()
        {
            TXTFile tablaTXT = new TXTFile();

            dtTXT = tablaTXT;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        public void LoadedWindow()
        {
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            txtFechaContabilizacion.Text = "";
        }

        private void RecorreListaDate(List<DateTXT> listDateTXT)
        {
            if (listDateTXT.Count == 1)
            {
                GetDate(listDateTXT);


            }
            else if (listDateTXT.Count == 0)
            {

                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se encontro ningun resultado con los parametros establecidos", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");

            }

            else if (listDateTXT.Count > 1)
            {
                ListaDateTXT ventanaListBox = new ListaDateTXT(listDateTXT);

                ventanaListBox.ShowDialog();

                if (ventanaListBox.Visibility == Visibility.Hidden)
                {
                    if (ventanaListBox.GetListDateTXT().Count == 0)
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se selecciono ningun elemento", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");

                    }
                    else
                    {

                        GetDate(ventanaListBox.GetListDateTXT());

                        txtFechaContabilizacion.Background = Brushes.White;

                        dpFechaContabilizacion.Background = Brushes.White;

                        bdFechaContabilizacion.Background = Brushes.White;

                    }
                }
            }
        }

        private void GetDate(List<DateTXT> listDateTXT)
        {
            foreach (DateTXT DateTXT in listDateTXT)
            {
                txtFechaContabilizacion.Text = DateTXT.RefDate.ToString();
            }
        }

        private void txtFechaContabilizacion_GotFocus(object sender, RoutedEventArgs e)
        {
            dpFechaContabilizacion.Background = Brushes.LightBlue;

            bdFechaContabilizacion.Background = Brushes.LightBlue;

            txtFechaContabilizacion.Background = Brushes.LightBlue;

            imgFechaContabilizacion.Visibility = Visibility.Visible;
        }

        private void txtFechaContabilizacion_LostFocus(object sender, RoutedEventArgs e)
        {
            dpFechaContabilizacion.Background = Brushes.White;

            bdFechaContabilizacion.Background = Brushes.White;

            txtFechaContabilizacion.Background = Brushes.White;

            imgFechaContabilizacion.Visibility = Visibility.Hidden;
        }

        private void imgFechaContabilizacion_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var result = cn.ConsultaDateTXT();

            if (result.Item2 == null)
            {
                RecorreListaDate(result.Item1);
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
            }
        }
    }
}
