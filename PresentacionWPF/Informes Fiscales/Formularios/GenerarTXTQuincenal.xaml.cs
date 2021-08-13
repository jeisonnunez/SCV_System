using Negocio.Controlador_Informes;
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
    /// Lógica de interacción para GenerarTXTQuincenal.xaml
    /// </summary>
    public partial class GenerarTXTQuincenal : Window
    {
        ControladorGenerarRetencionesIVA cn = new ControladorGenerarRetencionesIVA();

        ControladorLibros cl = new ControladorLibros();

        public TXTFile dtTXT;

        private string code;

        private string quincena;

        public string Quincena { get => quincena; set => quincena = value; }

        public string Code { get => code; set => code = value; }
        public GenerarTXTQuincenal()
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

        private void CreateTableTXT()
        {
            TXTFile tablaTXT = new TXTFile();

            dtTXT = tablaTXT;
        }

        private void btnCrear_Click(object sender, RoutedEventArgs e)
        {
            switch (btnCrear.Content.ToString())
            {
                case "OK":

                    DateTime dt = Convert.ToDateTime(cbPeriodo.SelectedValue.ToString());                    

                    int month = dt.Month;                  

                    int year = dt.Year;

                    var result = cl.ExecuteTXTQuincenal(month.ToString("D2"), year.ToString(), cbQuincena.SelectedValue.ToString());

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
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2 + " ", Brushes.Red, Brushes.White, "003-interface-2.png");
                    }

                    break;

            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void cbQuincena_GotFocus(object sender, RoutedEventArgs e)
        {
            App.comboBox_GotFocus(sender, e);
        }

        private void cbQuincena_LostFocus(object sender, RoutedEventArgs e)
        {
            App.comboBox_LostFocus(sender, e);
        }

        public void LoadedWindow()
        {
            InicializacionBasica();


        }

        private void InicializacionBasica()
        {

            cbQuincena.ItemsSource = cn.GetQuincena();

            var result1 = cl.ConsultaPeriodos();

            if (result1.Item2 == null)
            {
                cbPeriodo.ItemsSource = result1.Item1;

            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result1.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
            }

        }

        private void ClearFiels()
        {
            
            cbQuincena.Text = "";

            cbQuincena.SelectedIndex = -1;

          

        }
    }
}
