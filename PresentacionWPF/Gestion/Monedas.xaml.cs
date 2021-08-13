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
using Vista.Gestion.ValidateErrorsMonedas;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para Monedas.xaml
    /// </summary>
    public partial class Monedas : Document
    {
        ControladorMoneda cn = new ControladorMoneda();

        DataTable dt;

        List<ValidateErrorsMonedas> listMonedas = new List<ValidateErrorsMonedas>();
        public Monedas()
        {
            InitializeComponent();
          
        }

        private void InicializacionBasica()
        {
            btnOk.Content = "OK";                        

            var result = cn.ConsultaMonedas();

            if (result.Item2 == null)
            {
                dt = result.Item1;

                listMonedas = ConvertDataTable<ValidateErrorsMonedas>(dt);

                dgMoneda.ItemsSource = listMonedas;
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
            }

        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (btnOk.Content.ToString() == "OK")
            {
                
                this.Hide();
            }

            else if (btnOk.Content.ToString() == "Actualizar")
            {
                if (IsValid(dgMoneda) == true)//no existe ningun error
                {
                    List<Entidades.Monedas> listaMonedas = new List<Entidades.Monedas>();

                    int i = 0;

                    foreach (ValidateErrorsMonedas Monedas in listMonedas)
                    {
                        Entidades.Monedas monedas = new Entidades.Monedas();

                        monedas.OldCurrCode = Monedas.OldCurrCode;
                        monedas.CurrCode = Monedas.CurrCode;
                        monedas.CurrName = Monedas.CurrName;
                        monedas.DocCurrCod = Monedas.DocCurrCod;
                        monedas.UserSign = Properties.Settings.Default.Usuario;
                        monedas.Locked = 'N';

                        listaMonedas.Add(monedas);

                        i++;

                    }

                    var result = cn.InsertaMonedas(listaMonedas);

                    if (i == result.Item1)
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Monedas se actualizaron correctamente", Brushes.LightGreen, Brushes.Black, "001-interface.png");

                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error. Una moneda no se actualizo correctamente: " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");

                    }

                    InicializacionBasica();
                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error. Existe algun elemento del formulario con erorres: ", Brushes.Red, Brushes.White, "003-interface-2.png");
                }
                
            }
        }

        
        private void dgMoneda_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            btnOk.Content = "Actualizar";
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
          
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {            
            
            App.Window_Closing(sender, e);
        }

        public void LoadedWindow()
        {
            InicializacionBasica();
        }


        private void btnWindow_Close(object sender, RoutedEventArgs e)
        {           
            this.Hide();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InicializacionBasica();
        }

        private void ExitCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ExitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            
            DataRowView row_Selected = dgMoneda.SelectedItem as DataRowView;

            string moneda;

            MessageBoxResult messageBoxResult = MessageBox.Show("Desea eliminar el registro", "Currency", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            
            if (messageBoxResult == MessageBoxResult.Yes)
            {

                if (row_Selected != null)
                {

                    moneda = row_Selected["CurrCode"].ToString();

                    var result = cn.EliminaMoneda(moneda);

                    if (result.Item1 == 1)
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Moneda: " + moneda + " se elimino correctamente", Brushes.LightGreen, Brushes.Black, "001-interface.png");

                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se puede eliminar la moneda: " + moneda + " porque se realizo uns transaccion con la misma: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                    }

                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se selecciono ninguna moneda", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");
                }

            }

            InicializacionBasica();
        }

        

    }
    public static class CustomCommands1
    {
        public static readonly RoutedUICommand Exit = new RoutedUICommand
            (
                "Eliminar",
                "Eliminar",
                typeof(CustomCommands1),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.F4, ModifierKeys.Alt)
                }
            );

        //Define more commands here, just like the one above
    }
}

