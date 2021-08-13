using Negocio;
using System;
using System.Collections.Generic;
using System.Data;
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
using Entidades;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para PeriodosContables.xaml
    /// </summary>
    public partial class PeriodosContables : Window
    {
        ControladorPeriodos cn = new ControladorPeriodos();

        ControladorPeriodoContable controlador = new ControladorPeriodoContable();

        DataTable dt;
    
        public PeriodosContables()
        {
            InitializeComponent();
        }

        private void dgPeriodosContables_GotFocus(object sender, RoutedEventArgs e)
        {
            //DataGridRow dataGridRow = dgPeriodosContables.ItemContainerGenerator.ContainerFromItem(dgPeriodosContables.SelectedItem) as DataGridRow;
            //if (dataGridRow != null)
            //    dataGridRow.Background = Brushes.Blue;

        }

        private void dgPeriodosContables_LostFocus(object sender, RoutedEventArgs e)
        {
            //DataGridRow dataGridRow = dgPeriodosContables.ItemContainerGenerator.ContainerFromItem(dgPeriodosContables.SelectedItem) as DataGridRow;
            //if (dataGridRow != null)
            //    dataGridRow.Background = Brushes.Transparent;
        }

      

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            InicializacionBasica();

            App.Window_Closing(sender, e);
        }

        private void btnWindow_Close(object sender, RoutedEventArgs e)
        {
            InicializacionBasica();

            this.Hide();
        }

       
        private void InicializacionBasica()
        {
            btnOk.Content = "OK";

            var result = cn.ConsultaPeriodos();

            if (result.Item2 == null)
            {
                dt = result.Item1;

                dgPeriodosContables.ItemsSource = dt.DefaultView;
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
            }
          
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InicializacionBasica();
        }

        public void LoadedWindow()
        {
            InicializacionBasica();
        }


        private void img_PreviewMouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            DataRowView row_Selected = dgPeriodosContables.SelectedItem as DataRowView;

            if (row_Selected != null)
            {
                List<Entidades.PeriodosContables> periodosContables = new List<Entidades.PeriodosContables>();

                Entidades.PeriodosContables periodo = new Entidades.PeriodosContables();

                periodo.OldCode1= row_Selected["OldCode"].ToString();

                periodo.Code = row_Selected["Code"].ToString();

                periodo.Name = row_Selected["Name"].ToString();

                periodo.F_RefDate = Convert.ToDateTime(row_Selected["F_RefDate"].ToString());

                periodo.T_RefDate = Convert.ToDateTime(row_Selected["T_RefDate"].ToString());

                periodo.F_DueDate = Convert.ToDateTime(row_Selected["F_DueDate"].ToString());

                periodo.T_DueDate = Convert.ToDateTime(row_Selected["T_DueDate"].ToString());

                periodo.F_TaxDate = Convert.ToDateTime(row_Selected["F_TaxDate"].ToString());

                periodo.T_TaxDate = Convert.ToDateTime(row_Selected["T_TaxDate"].ToString());

                periodo.PeriodStat = Convert.ToChar(controlador.EstatusPeriodo(row_Selected["PeriodStat"].ToString()));

                periodo.UserSign2 = Convert.ToInt32(Properties.Settings.Default.Usuario);

                periodo.UpdateDate = Convert.ToDateTime(fechaActual.GetFechaActual());

                periodo.Category = row_Selected["Category"].ToString();

                periodosContables.Add(periodo);

                Periodo_Contable periodoSeleccionado = new Periodo_Contable(periodosContables);

                periodoSeleccionado.ShowDialog();

              

                InicializacionBasica();

            }
        }

        private void btnPeriodoNuevo_Click(object sender, RoutedEventArgs e)
        {
            Periodo_Contable ventanaPeriodoContable = new Periodo_Contable(true);

            ventanaPeriodoContable.ShowDialog();

            InicializacionBasica();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
