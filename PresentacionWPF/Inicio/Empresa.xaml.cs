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
using Entidades;
using Negocio;
using Vista.Inicio.ValidationErrorEmpresa;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para Empresa.xaml
    /// </summary>
    public partial class Empresa : Window
    {
        private Periodo_Contable objectPeriodoContable;

        private Registro objectRegistros;

        ControladorEmpresa cn = new ControladorEmpresa();

        ControladorSubPeriodos subPeriodos = new ControladorSubPeriodos();

        private static int sw = 0;
       
        private List<SubPeriodo> listaSubPeriodos;

        private static List<PeriodoContable> listaPeriodoContable;

        private string sociedad;
        public static int Sw { get => sw; set => sw = value; }       
        public List<SubPeriodo> ListaSubPeriodos { get => listaSubPeriodos; set => listaSubPeriodos = value; }
        public static List<PeriodoContable> ListaPeriodoContable { get => listaPeriodoContable; set => listaPeriodoContable = value; }
        public string Sociedad { get => sociedad; set => sociedad = value; }

        public Empresa()
        {
            InitializeComponent();
        }

        private Tuple<bool, string> ValidateFormEmpresa()
        {
            bool sw = false;

            string firstError = null;

            foreach (KeyValuePair<string, string> errrors in ValidationErrorEmpresa.ErrorCollectionMessages)
            {
                if (errrors.Value != null)
                {
                    sw = true;

                    firstError = errrors.Value;
                }

                if (sw == true)
                {
                    break;
                }
            }

            return Tuple.Create(sw, firstError);
        }

        private void btnCrearEmpresa_Click(object sender, RoutedEventArgs e)
        {
            var resultValidateForm = ValidateFormEmpresa();

            bool sw = resultValidateForm.Item1;

            string errorForm = resultValidateForm.Item2;

            if (sw == true)
            {
                App.GetMainWindowStatusBar().ShowStatusMessage("Error en algun campo del formulario: " + errorForm, Brushes.Red, Brushes.White, "003-interface-2.png");
            }
            else
            {
                Sw = 1; //revisar

                if (Sw == 1)
                {
                    var result = cn.CrearEmpresa(txtDatabase.Text);

                    if (result.Item1 != 0)
                    {
                        Sociedad = txtEmpresa.Text;

                        App.GetMainWindowStatusBar().ShowStatusMessage("La base de datos SCV_" + txtDatabase.Text + " se creo correctamente", Brushes.LightBlue, Brushes.Black, "001-interface.png");

                        string error = cn.EstableceConexionString("SCV_" + txtDatabase.Text);

                        if (error != null)
                        {
                            App.GetMainWindowStatusBar().ShowStatusMessage("Error: " + error, Brushes.Red, Brushes.White, "003-interface-2.png");
                        }

                        cn.obtenerCadenaConexion();

                        var result1 = cn.EstablecerPeriodoContable(ListaPeriodoContable);

                        if (result1.Item1 == 1)
                        {

                            ListaSubPeriodos = subPeriodos.EstableceSubPeriodos(ListaPeriodoContable);

                            var result2 = cn.SubPeriodosContables(ListaSubPeriodos);

                            if (result2.Item1 == 12)
                            {

                                this.Hide();

                                CreateObjectRegistro();

                                objectRegistros.Show();
                            }
                            else
                            {
                                App.GetMainWindowStatusBar().ShowStatusMessage("Error en la creacion  de los subperiodos contables: " + result2.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");

                            }


                        }
                        else
                        {
                            App.GetMainWindowStatusBar().ShowStatusMessage("Error en la creacion del periodo contable: " + result1.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                        }

                    }
                    else
                    {
                        App.GetMainWindowStatusBar().ShowStatusMessage("Error en la creacion de la base de datos: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");

                    }
                }
                else
                {
                    App.GetMainWindowStatusBar().ShowStatusMessage("Error. Debe crear el periodo contable: ", Brushes.Red, Brushes.White, "003-interface-2.png");
                }

            }
        }

        private void btnPeriodos_Click(object sender, RoutedEventArgs e)
        {            
            objectPeriodoContable.Show();

            objectPeriodoContable.LoadedWindow();

            objectPeriodoContable.EstableceCamposPeriodo();
        }

        static public void SetSw(int sw)
        {
            Sw = sw;
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            App.textBox_GotFocus(sender, e);

        }

        private void btnWindow_Close(object sender, RoutedEventArgs e)
        {

            this.Hide();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            App.textBox_LostFocus(sender, e);
        }

        public static void EstableceCamposPeriodo(List<PeriodoContable> listaPeriodos)
        {
            ListaPeriodoContable = listaPeriodos;
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CreateObjectPeriodoContable();

            
        }


        public void LoadedWindow()
        {
            txtDatabase.Text = "";

            txtEmpresa.Text = "";
        }

        private void CreateObjectRegistro()
        {
            Registro registro = new Registro(true, Sociedad);

            objectRegistros = registro;
        }

        private void CreateObjectPeriodoContable()
        {            

            Periodo_Contable periodo = new Periodo_Contable();

            objectPeriodoContable = periodo;
        }
    }
}
