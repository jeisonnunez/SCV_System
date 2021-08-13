using System;
using System.Collections;
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
using Negocio;
using Vista.Gestion.ValidateErrorsClasesImpuestos;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para ClasesImpuestos.xaml
    /// </summary>
    public partial class ClasesImpuestos : Document
    {
        ControladorClasesImpuestos cn = new ControladorClasesImpuestos();

        DataTable dt;

        List<ValidateErrorsClasesImpuestos> listClasesImpuesto = new List<ValidateErrorsClasesImpuestos>();

        public ClasesImpuestos()
        {
            InitializeComponent();
        }
        
        private void InicializacionBasica()
        {
            btnOk.Content = "OK";

            var result= cn.ConsultaClasesImpuestos();

            if (result.Item2 == null)
            {
                dt = result.Item1;                

                listClasesImpuesto = ConvertDataTable<ValidateErrorsClasesImpuestos>(dt);

                dgClasesImpuestos.ItemsSource = listClasesImpuesto;
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
            }

           
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //InicializacionBasica();

            App.Window_Closing(sender, e);
        }

        private void btnWindow_Close(object sender, RoutedEventArgs e)
        {
            //InicializacionBasica();

            this.Hide();
        }
        public void LoadedWindow()
        {
            InicializacionBasica();
        }

        private void RecorreListaAccount(List<Cuenta> listAccountResultante, TextBox textBox)
        {
            if (listAccountResultante.Count == 0)
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se encontro ningun resultado con los parametros establecidos", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");

            }

            else if (listAccountResultante.Count > 0)
            {
                ListaCuentas ventanaListaCuentaAsociada = new ListaCuentas(listAccountResultante);

                ventanaListaCuentaAsociada.ShowDialog();

                if (ventanaListaCuentaAsociada.Visibility == Visibility.Hidden)
                {
                    if (ventanaListaCuentaAsociada.GetListAccount().Count == 0)
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se encontro ningun resultado con los parametros establecidos", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");
                    }
                    else
                    {
                        GetAcctCode(ventanaListaCuentaAsociada.GetListAccount(),textBox);
                    }
                }
            }
        }

        private void GetAcctCode(List<Cuenta> listaCuenta, TextBox textBox)
        {
            foreach (Cuenta cuenta in listaCuenta)
            {
                textBox.Text = cuenta.AcctCode;

            
            }
        }

        private void dgClasesImpuestos_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            btnOk.Content = "Actualizar";
        }

       
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (btnOk.Content.ToString() == "OK")
            {
                InicializacionBasica();

                this.Hide();
            }

            if (btnOk.Content.ToString() == "Actualizar")
            {
                if (IsValid(dgClasesImpuestos)==true) //no existe ningun error
                {
                    List<Entidades.ClasesImpuestos> listaClasesImpuestos = new List<Entidades.ClasesImpuestos>();

                    int i = 0;

                    foreach (ValidateErrorsClasesImpuestos clasesImpuestos in listClasesImpuesto)
                    {
                        Entidades.ClasesImpuestos clasesImpuestosNew = new Entidades.ClasesImpuestos();

                        clasesImpuestosNew.Code = clasesImpuestos.Code;
                        clasesImpuestosNew.Name = clasesImpuestos.Name;
                        clasesImpuestosNew.Rate =ConvertDecimalTwoPlaces(clasesImpuestos.Rate);
                        clasesImpuestosNew.SalesTax = clasesImpuestos.SalesTax;
                        clasesImpuestosNew.PurchTax = clasesImpuestos.PurchTax;
                        clasesImpuestosNew.UserSign = Properties.Settings.Default.Usuario;

                        listaClasesImpuestos.Add(clasesImpuestosNew);

                        i++;

                    }

                    var result = cn.InsertaClasesImpuestos(listaClasesImpuestos);

                    if (i == result.Item1)
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Clases de impuestos se actualizaron correctamente", Brushes.LightGreen, Brushes.Black, "001-interface.png");
                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error. Una clase de impuesto actualizo correctamente: " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
                    }

                    InicializacionBasica();
                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error. Existe algun elemento del formulario con erorres: ", Brushes.Red, Brushes.White, "003-interface-2.png");
                }
                
               
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InicializacionBasica();
        }

        private void img_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {            
            try
            {
                var row_list = GetDataGridRows(dgClasesImpuestos);

               
                foreach (DataGridRow single_row in row_list)
                {
                    if (single_row.IsSelected == true)
                    {
                        TextBox txtSalesTax = FindChild<TextBox>(single_row, "txtSalesTax");

                        var result = cn.ConsultaCuentasNoAsociadas();

                        if (result.Item2 == null)
                        {
                            RecorreListaAccount(result.Item1, txtSalesTax);
                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
                        }

                    }
                }
            }

            catch(Exception ex)
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error" + ex.Message, Brushes.Red, Brushes.Black, "003-interface-2.png");
            }

        }

        private void imgPuchTax_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            try
            {
                var row_list = GetDataGridRows(dgClasesImpuestos);

                DataRowView row_Selected = dgClasesImpuestos.SelectedItem as DataRowView;

                foreach (DataGridRow single_row in row_list)
                {
                    if (single_row.IsSelected == true)
                    {
                        TextBox txtPurchTax = FindChild<TextBox>(single_row, "txtPurchTax");

                        var result = cn.ConsultaCuentasNoAsociadas();

                        if (result.Item2 == null)
                        {
                            RecorreListaAccount(result.Item1, txtPurchTax);
                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
                        }

                        
                    }
                }
            }

            catch(Exception ex)
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error" + ex.Message, Brushes.Red, Brushes.Black, "003-interface-2.png");
            }
        }

        private void ExitCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ExitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
           
            DataRowView row_Selected = dgClasesImpuestos.SelectedItem as DataRowView;

            string claseImpuesto;

            MessageBoxResult messageBoxResult = MessageBox.Show("Desea eliminar el registro", "Clase Impuesto", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            
            if (messageBoxResult == MessageBoxResult.Yes)
            {

                if (row_Selected != null)
                {
                    claseImpuesto = row_Selected["Code"].ToString();

                    var result= cn.EliminaClasesImpuesto(claseImpuesto);

                    if (result.Item1 == 1)
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Clase de impuesto :" + claseImpuesto + " se elimino correctamente", Brushes.LightGreen, Brushes.Black, "001-interface.png");

                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se puede eliminar el impuesto: " + claseImpuesto + " " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");

                    }
                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se selecciono ninguna clase de impuesto", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");

                }
            }

            InicializacionBasica();
        }


    }

    public static class CustomCommands
    {
        public static readonly RoutedUICommand Exit = new RoutedUICommand
            (
                "Eliminar",
                "Eliminar",
                typeof(CustomCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.F4, ModifierKeys.Alt)
                }
            );

        //Define more commands here, just like the one above
    }
}
