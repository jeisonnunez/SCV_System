using Entidades;
using Negocio;
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
    /// Lógica de interacción para ReconciliacionSN.xaml
    /// </summary>
    public partial class ReconciliacionSN : Window
    {
        ControladorBalance cn = new ControladorBalance();

        ControladorReconciliacionSN cr = new ControladorReconciliacionSN();

        private ReconciliacionInterna dtReconciliacionInterna;
        public ReconciliacionSN()
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        private void btnWindow_Close(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void btnReconciliar_Click(object sender, RoutedEventArgs e)
        {
            switch (btnReconciliar.Content.ToString())
            {
                case "Reconciliar":

                    var result = cr.ExecuteReconciliacionSN(txtSN.Text,dpDFechaContabilizacion.SelectedDate,dpHFechaContabilizacion.SelectedDate);

                    if (result.Item2 == null)
                    {

                        if (result.Item1.Rows.Count >= 1)
                        {
                            dtReconciliacionInterna.ClearDatatable();

                            dtReconciliacionInterna.Show();

                            dtReconciliacionInterna.SetDatePicker(dpReconciliacion.SelectedDate,dpDFechaContabilizacion.SelectedDate,dpHFechaContabilizacion.SelectedDate);

                            dtReconciliacionInterna.SetResult(result.Item1);

                            dtReconciliacionInterna.SetField(txtSN.Text);
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

        private void imgSN_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
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

                        txtSN.Background = Brushes.White;

                        dpSN.Background = Brushes.White;

                        bdSN.Background = Brushes.White;

                        imgSN.Visibility = Visibility.Hidden;

                        

                    }
                }
            }
        }

        private void GetSocioNegocio(List<SocioNegocio> listSuppliers)
        {
            foreach (SocioNegocio Suppliers in listSuppliers)
            {
                
                txtSN.Text = Suppliers.CardCode;               

            }
        }


        private void txtSN_GotFocus(object sender, RoutedEventArgs e)
        {
            dpSN.Background = Brushes.LightBlue;

            bdSN.Background = Brushes.LightBlue;

            txtSN.Background = Brushes.LightBlue;

            imgSN.Visibility = Visibility.Visible;
        }

        private void txtSN_LostFocus(object sender, RoutedEventArgs e)
        {
            dpSN.Background = Brushes.White;

            bdSN.Background = Brushes.White;

            txtSN.Background = Brushes.White;

            imgSN.Visibility = Visibility.Hidden;
        }

        public void LoadedWindow()
        {           

            InicializacionBasica();

            CreateTableReconciliacion();
        }

        private void InicializacionBasica()
        {
            txtSN.Text = "";

            dpReconciliacion.SelectedDate = null;

            dpDFechaContabilizacion.SelectedDate = null;

            dpHFechaContabilizacion.SelectedDate = null;

        }

        private void CreateTableReconciliacion()
        {
            ReconciliacionInterna tablaReconciliacionInterna = new ReconciliacionInterna();

            dtReconciliacionInterna = tablaReconciliacionInterna;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
