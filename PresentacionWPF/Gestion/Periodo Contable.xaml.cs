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
using Entidades;


namespace Vista
{
    /// <summary>
    /// Lógica de interacción para Periodo_Contable.xaml
    /// </summary>
    public partial class Periodo_Contable : Window
    {
        ControladorPeriodoContable cn = new ControladorPeriodoContable();

        ControladorEmpresa ce = new ControladorEmpresa();

        private static bool sw;

        private string oldCode;
        
        private List<Entidades.PeriodosContables> listaSubPeriodos;
              
        public static bool Sw { get => sw; set => sw = value; }
        public List<Entidades.PeriodosContables> ListaSubPeriodos { get => listaSubPeriodos; set => listaSubPeriodos = value; }
        public string OldCode { get => oldCode; set => oldCode = value; }

        public Periodo_Contable()
        {
            InitializeComponent();

            Sw = false;

           
        }

        public Periodo_Contable(bool sw)
        {
            InitializeComponent();

            Sw = sw;
        }

        public void EstableceCamposPeriodo()
        {

            dpHContabilizacion.IsEnabled = false;

            cbEstatusPeriodo.IsEnabled = true;

            cbEstatusPeriodo.IsEnabled = false;

            cbEstatusPeriodo.SelectedIndex = 0;

            txtEjercicio.IsEnabled = true;

            dpEjercicio.IsEnabled = true;

            btnCrear.Content = "Crear";

           
        }

        public void LoadedWindow()
        {
            cbEstatusPeriodo.ItemsSource = LoadPeriodStatus();
        }

        public Periodo_Contable(List<Entidades.PeriodosContables> listaPeriodoActual)
        {
            InitializeComponent();           

            ModificaCamposPeriodo();

            LoadedWindow();
          
          
                try
                {
                    BuscaPeriodo(listaPeriodoActual);

                    btnCrear.Content = "OK";

                }
                catch (Exception e)
                {

                EstableceLogin.GetMenuStatusBar().ShowStatusMessage(e.Message, Brushes.Red, Brushes.White, "003-interface-2.png");
                }
            
            
        }

        private List<EstadosPeriodos> LoadPeriodStatus()
        {
            List<EstadosPeriodos> listEstadosPeriodos = new List<EstadosPeriodos>();

            EstadosPeriodos estadosPeriodos = new EstadosPeriodos();

            estadosPeriodos.PeriodStatName = "Desbloqueado";
            estadosPeriodos.PeriodStatCode = "N";

            listEstadosPeriodos.Add(estadosPeriodos);

            EstadosPeriodos estadosPeriodos1 = new EstadosPeriodos();

            estadosPeriodos1.PeriodStatName = "CierrePeriodo";
            estadosPeriodos1.PeriodStatCode = "C";

            listEstadosPeriodos.Add(estadosPeriodos1);

            EstadosPeriodos estadosPeriodos2 = new EstadosPeriodos();

            estadosPeriodos2.PeriodStatName = "Bloqueado";
            estadosPeriodos2.PeriodStatCode = "Y";

            listEstadosPeriodos.Add(estadosPeriodos2);

            EstadosPeriodos estadosPeriodos3 = new EstadosPeriodos();

            estadosPeriodos3.PeriodStatName = "DesbloqueadoExecVentas";
            estadosPeriodos3.PeriodStatCode = "S";

            listEstadosPeriodos.Add(estadosPeriodos3);

            return listEstadosPeriodos;


        }

        private void ModificaCamposPeriodo()
        {          

            dpHContabilizacion.IsEnabled = true;

            cbEstatusPeriodo.IsEnabled = true;

            txtEjercicio.IsEnabled = false;

            dpEjercicio.IsEnabled = false;

            //((ComboBoxItem)cbEstatusPeriodo.Items[0]).IsSelected = false;

            cbEstatusPeriodo.Background = Brushes.White;
        }

        private void btnCrear_Click(object sender, RoutedEventArgs e)
        {

            if (btnCrear.Content.ToString() == "Crear")
            {

            List<PeriodoContable> listaPeriodos = new List<PeriodoContable>();

            PeriodoContable Periodo = new PeriodoContable();

            Periodo.PeriodCat = txtCodigoPeriodo.Text;

            Periodo.PeriodName = txtNombrePeriodo.Text;

            Periodo.SubType = 'M';

            Periodo.PeriodNum = Convert.ToInt32(txtCantidadPeriodos.Text);

            Periodo.F_RefDate = dpDContabilizacion.SelectedDate;

            Periodo.T_RefDate = dpHContabilizacion.SelectedDate;

            Periodo.F_TaxDate = dpDDocumento.SelectedDate;

            Periodo.T_TaxDate = dpHDocumento.SelectedDate;

            Periodo.F_DueDate = dpDVencimiento.SelectedDate;

            Periodo.T_DueDate = dpHVencimiento.SelectedDate;

            Periodo.FinancYear = dpEjercicio.SelectedDate;

            Periodo.Year = txtEjercicio.Text;

            Periodo.UpdateDate = fechaActual.GetFechaActual();

                 if (Sw==false)
                 {
                    Periodo.UserSign = 1;

                    listaPeriodos.Add(Periodo);

                    Empresa.EstableceCamposPeriodo(listaPeriodos);

                  
                 }
                 else if(Sw==true)
                 {

                 Periodo.UserSign = Properties.Settings.Default.Usuario;

                 listaPeriodos.Add(Periodo);

                    var result = ce.EstablecerPeriodoContable(listaPeriodos);

                    if (result.Item1 == 1)
                    {
                        ControladorSubPeriodos subPeriodos = new ControladorSubPeriodos();

                        List<SubPeriodo> listaSubPeriodos = subPeriodos.EstableceSubPeriodos(listaPeriodos);

                        var result2 = ce.SubPeriodosContables(listaSubPeriodos);

                        if (result2.Item1 == 12)
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Periodos contables se crearon exitosamente: " , Brushes.LightGreen, Brushes.Black, "001-interface.png");

                            this.Hide();
                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error en la creacion del los subperiodos contables: " + result2.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                        }
                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error en la creacion del periodo contable: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                    }
                }

                       

             this.Hide();
                
            }

           else if (btnCrear.Content.ToString() == "Actualizar")
           {
                ListaSubPeriodos=new List<Entidades.PeriodosContables>();

                Entidades.PeriodosContables subPeriodos = new Entidades.PeriodosContables();

                subPeriodos.OldCode1 = OldCode;

                subPeriodos.Code = txtCodigoPeriodo.Text;

                subPeriodos.Name = txtNombrePeriodo.Text;

                subPeriodos.F_RefDate = dpDContabilizacion.SelectedDate;

                subPeriodos.T_RefDate = dpHContabilizacion.SelectedDate;

                subPeriodos.F_DueDate = dpDDocumento.SelectedDate;

                subPeriodos.T_DueDate = dpHDocumento.SelectedDate;

                subPeriodos.F_TaxDate = dpDVencimiento.SelectedDate;

                subPeriodos.T_TaxDate = dpHVencimiento.SelectedDate;

                subPeriodos.PeriodStat = Convert.ToChar(cbEstatusPeriodo.SelectedValue);

                subPeriodos.UserSign2 = Properties.Settings.Default.Usuario;

                subPeriodos.UpdateDate = Convert.ToDateTime(fechaActual.GetFechaActual());

                subPeriodos.Category = txtEjercicio.Text;

                ListaSubPeriodos.Add(subPeriodos);

                var result1=cn.ActualizaPeriodo(ListaSubPeriodos);

                if (result1.Item1 == 1)
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Periodo Actualizado correctamente: ", Brushes.LightGreen, Brushes.Black, "001-interface.png");
                                        
                    BuscaPeriodo(ListaSubPeriodos);

                    btnCrear.Content = "OK";

                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al actualizar periodos: " + result1.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                }

                ListaSubPeriodos.Clear();
            }

            else if (btnCrear.Content.ToString() == "OK")
            {
                this.Hide();

            }
            
            
        }

        private void BuscaPeriodo(List<Entidades.PeriodosContables> listSubPeriodos)
        {                      

            foreach (Entidades.PeriodosContables subPeriodos in listSubPeriodos)
            {
                OldCode = subPeriodos.OldCode1;

                txtCodigoPeriodo.Text = subPeriodos.Code;

                txtNombrePeriodo.Text = subPeriodos.Name;

                txtSubPeriodos.Text = "Meses";

                txtCantidadPeriodos.Text = "12";

                dpDContabilizacion.SelectedDate = subPeriodos.F_RefDate;

                dpHContabilizacion.SelectedDate= subPeriodos.T_RefDate;

                dpDDocumento.SelectedDate=subPeriodos.F_DueDate;

                dpHDocumento.SelectedDate=subPeriodos.T_DueDate;

                dpDVencimiento.SelectedDate=subPeriodos.F_TaxDate;

                dpHVencimiento.SelectedDate=subPeriodos.T_TaxDate;
               
                cbEstatusPeriodo.SelectedValue = subPeriodos.PeriodStat;     

                txtEjercicio.Text = subPeriodos.Category;

                var result2 = cn.ConsultaPeriodoActual(subPeriodos.Category);

                if (result2.Item1 == null)
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error. Usuario y/o contraseña incorrecta: " + result2.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                }
                else { 

                dpEjercicio.SelectedDate = result2.Item1;
                }
            }
        }

       

        private void dpDContabilizacion_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (btnCrear.Content.ToString()=="Crear")
            {
                dpHContabilizacion.SelectedDate = Convert.ToDateTime(dpDContabilizacion.SelectedDate).AddDays(-1).AddYears(1);

            }else if (btnCrear.Content.ToString() == "OK")

            {
                btnCrear.Content = "Actualizar";
            }
        }

     
        private void dp_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (btnCrear.Content.ToString() == "OK")
            {
                btnCrear.Content = "Actualizar";
            }
        }

        private void cbEstatusPeriodo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (btnCrear.Content.ToString() == "OK")
            {
                btnCrear.Content = "Actualizar";
            }

           
        }

        private void comboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            App.comboBox_LostFocus(sender, e);
        }

        private void comboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            App.comboBox_GotFocus(sender, e);
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            App.textBox_LostFocus(sender, e);
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            App.textBox_GotFocus(sender, e);
        }

        private void comboBoxItem_GotFocus(object sender, RoutedEventArgs e)
        {
            App.comboBoxItem_GotFocus(sender, e);
        }

        private void comboBoxItem_LostFocus(object sender, RoutedEventArgs e)
        {
            App.comboBoxItem_LostFocus(sender, e);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        private void btnWindow_Close(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void txtCodigoPeriodo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (btnCrear.Content.ToString()=="OK")
            {
                btnCrear.Content = "Actualizar";
            }
        }
    }
}
