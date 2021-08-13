using Negocio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Vista.Gestion.ValidateErrorsNroComprobante;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para NroComprobante.xaml
    /// </summary>
    public partial class NroComprobante : Document
    {
        public ObservableCollection<string> TipoSerie { get; set; }

        ControladorNroComprobante cn = new ControladorNroComprobante();

        DataTable dt;

        List<ValidateErrorsNroComprobante> listNroComprobante = new List<ValidateErrorsNroComprobante>();
        public NroComprobante()
        {
            
            InitializeComponent();
            
        }

        private void InicializacionBasica()
        {
            btnOk.Content = "OK";

            var result = cn.ConsultaNroComprobante();

            if (result.Item2 == null)
            {
               
                dt = result.Item1;

                listNroComprobante = ConvertDataTable<ValidateErrorsNroComprobante>(dt);

                dgComprobante.ItemsSource = listNroComprobante;
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
            }

        }

        private void dgComprobante_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            btnOk.Content = "Actualizar";

        }

        public void LoadedWindow()
        {
            InicializacionBasica();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {

            if (btnOk.Content.ToString() == "OK")
            {               
                this.Hide();
            }

            else if (btnOk.Content.ToString() == "Actualizar")
            {
                if (IsValid(dgComprobante) == true)
                {
                    List<Entidades.NroComprobante> listaNroComprobante = new List<Entidades.NroComprobante>();

                    int i = 0;

                    foreach (ValidateErrorsNroComprobante nroComprobante in listNroComprobante)
                    {
                        Entidades.NroComprobante NroComprobante = new Entidades.NroComprobante();

                        NroComprobante.OldCode = nroComprobante.OldCode;
                        NroComprobante.Code = nroComprobante.Code;
                        NroComprobante.NombreSerie = nroComprobante.NombreSerie;
                        NroComprobante.Descripcion = nroComprobante.Descripcion;
                        NroComprobante.UserSign = Properties.Settings.Default.Usuario;
                        NroComprobante.TipoSerie = nroComprobante.TipoSerie;
                        NroComprobante.Inicio = Convert.ToInt32(nroComprobante.Inicio);
                        NroComprobante.Siguiente = Convert.ToInt32(nroComprobante.Siguiente);
                        NroComprobante.Fin = Convert.ToInt32(nroComprobante.Fin);
                        NroComprobante.Activo = Convert.ToChar(cn.EstadoNroComprobantes(Convert.ToBoolean(nroComprobante.Activo)));
                        NroComprobante.UpdateDate = Convert.ToDateTime(Entidades.fechaActual.GetFechaActual());

                        listaNroComprobante.Add(NroComprobante);

                        i++;

                    }

                    var result = cn.InsertaNroComprobante(listaNroComprobante);

                    if (i == result.Item1)
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Nro de comprobantes se actualizaron correctamente", Brushes.LightGreen, Brushes.Black, "001-interface.png");

                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error. Una numeracion de comprobantes no se actualizo correctamente: " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");

                    }

                    InicializacionBasica();
                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error. Existe algun elemento del formulario con erorres: ", Brushes.Red, Brushes.White, "003-interface-2.png");
                }

                
            }

        }

        private void cbTipoSerie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnOk.Content = "Actualizar";
            
        }

        private void activo_Checked(object sender, RoutedEventArgs e)
        {
            //foreach (DataRowView row_Selected in dgComprobante.SelectedItems)
            //{

            //    if (row_Selected != null)
            //    {
            //        row_Selected["Activo"] = true;
            //    }
            //}
        }

        private void activo_Unchecked(object sender, RoutedEventArgs e)
        {
            //foreach (DataRowView row_Selected in dgComprobante.SelectedItems)
            //{

            //    if (row_Selected != null)
            //    {
            //        row_Selected["Activo"] = false;
            //    }
            //}
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        private void btnWindow_Close(object sender, RoutedEventArgs e)
        {          
            this.Hide();
        }

      
        public class ListTipoSerie : List<string>
        {
            public ListTipoSerie()
            {
                this.Add("IVA");
                this.Add("ISLR");

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            TipoSerie = new ObservableCollection<string>() { "IVA", "ISLR" };

            //cbTipoSerie.ItemsSource = TipoSerie;

            InicializacionBasica();

            //LoadCombobox();

        }

        private void LoadCombobox()
        {
            try
            {
                var row_list = GetDataGridRows(dgComprobante);

                foreach (DataGridRow single_row in row_list)
                {

                    ComboBox cb = FindChild<ComboBox>(single_row, "cbPrueba");

                    cb.ItemsSource = TipoSerie;

                }
            }

            catch (Exception ex)
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error" + ex.Message, Brushes.Red, Brushes.Black, "003-interface-2.png");
            }
        }


        private void activo_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            btnOk.Content = "Actualizar";
        }

      
        private void txtCodeCurrency_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            btnOk.Content = "Actualizar";
        }

        private void ExitCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ExitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            DataRowView row_Selected = dgComprobante.SelectedItem as DataRowView;

            string nroComprobante;

            MessageBoxResult messageBoxResult = MessageBox.Show("¿Desea eliminar el numero de comprobante?", "Nro Comprobante", MessageBoxButton.YesNo, MessageBoxImage.Warning);


            if (messageBoxResult == MessageBoxResult.Yes)
            {

                if (row_Selected != null)
                {

                    nroComprobante = row_Selected["Code"].ToString();

                    var result = cn.EliminaNroComprobante(nroComprobante);

                    if (result.Item1 == 1)
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Numeracion de comprobante:" + nroComprobante + " se elimino correctamente", Brushes.LightGreen, Brushes.Black, "001-interface.png");

                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al eliminar numero de comprobante: " + nroComprobante + " " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");

                    }

                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se selecciono ninguna numeracion de comprobantes", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");

                }


            }

            InicializacionBasica();
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
}
