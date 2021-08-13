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
    /// Lógica de interacción para LibroComprasSemanal.xaml
    /// </summary>
    public partial class LibroComprasSemanal : Window
    {
        private DateTime? desde;
        public DateTime? Desde { get => desde; set => desde = value; }

        private DateTime? hasta;
        public DateTime? Hasta { get => hasta; set => hasta = value; }

        public CrystalReportLibroComprasSemanal CRLibroComprasSemanal;
        public LibroComprasSemanal()
        {
            InitializeComponent();
        }

        public void LoadedWindow()
        {

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void btnCrear_Click(object sender, RoutedEventArgs e)
        {
            if ((String.IsNullOrWhiteSpace(dpDesde.SelectedDate.ToString()) == false) && (String.IsNullOrWhiteSpace(dpHasta.SelectedDate.ToString()) == false))
            {
                Desde = dpDesde.SelectedDate;

                Hasta = dpHasta.SelectedDate;

                CRLibroComprasSemanal.Show();

                CRLibroComprasSemanal.LoadedWindow(Desde,Hasta);

            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Fechas Vacias", Brushes.Red, Brushes.White, "003-interface-2.png");
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CreateCrystalLibroComprasSemanal();
        }

        private void CreateCrystalLibroComprasSemanal()
        {
            CrystalReportLibroComprasSemanal cr = new CrystalReportLibroComprasSemanal();

            CRLibroComprasSemanal = cr;
        }
    }
}
