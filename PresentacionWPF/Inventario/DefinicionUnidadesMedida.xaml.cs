using Entidades;
using Negocio.Controlador_Inventario;
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

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para DefinicionUnidadesMedida.xaml
    /// </summary>
    public partial class DefinicionUnidadesMedida : Document
    {
        ControladorDefinicionUnidadesMedida cn = new ControladorDefinicionUnidadesMedida();

        DataTable dt = new DataTable();

        private int ugpEntry;

        private int baseUom;

        private string uomCode;

        public int UgpEntry { get => ugpEntry; set => ugpEntry = value; }

        public int BaseUom { get => baseUom; set => baseUom = value; }

        public string UomCode { get => uomCode; set => uomCode = value; }

        public DefinicionUnidadesMedida()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (btnOk.Content.ToString() == "OK")
            {

                this.Hide();
            }

            else if (btnOk.Content.ToString() == "Actualizar")
            {
                if (IsValid(dgDefinicionUnidadesMedidas) == true)//no existe ningun error
                {
                    List<Entidades.GrupoUnidadesMedidaDetalle> listaUnidadesMedida = new List<Entidades.GrupoUnidadesMedidaDetalle>();

                    int i = 1;

                    int j = 0;

                    foreach (DataRow row in dt.Rows)
                    {
                        Entidades.GrupoUnidadesMedidaDetalle medidas = new Entidades.GrupoUnidadesMedidaDetalle();

                        medidas.UgpEntry = UgpEntry;
                        medidas.UomEntry = Convert.ToInt32(row["UomEntry"]);
                        medidas.AltQty = ConvertDecimalTwoPlaces(row["AltQty"]);
                        medidas.BaseQty = ConvertDecimalTwoPlaces(row["BaseQty"]);
                        medidas.LineNum = i;
                        medidas.UserSign = Properties.Settings.Default.Usuario;
                        medidas.LogInstanc = 0;
                        medidas.WghtFactor = 0;
                        medidas.UdfFactor = -1;

                        listaUnidadesMedida.Add(medidas);

                        i++;

                        j++;

                    }

                    var result = cn.InsertaUnidadesMedidaDefinicionDetalle(listaUnidadesMedida);

                    if (j == result.Item1)
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Operacion se realizo correctamente", Brushes.LightGreen, Brushes.Black, "001-interface.png");

                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error. Una unidad de medida no se actualizo correctamente: " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");

                    }

                    InicializacionBasica();
                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error. Existe algun elemento del formulario con erorres: ", Brushes.Red, Brushes.White, "003-interface-2.png");
                }

            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        public void LoadedWindow()
        {
            InicializacionBasica();
        }

        public void SetFields(int UgpEntry, int BaseUom, string UomCode)
        {
            this.UgpEntry = UgpEntry;

            this.BaseUom = BaseUom;

            this.UomCode = UomCode;

            SetUomCode(UomCode);
        }

        private void SetUomCode(string uomCode)
        {
            Style columnStyle = new Style(typeof(TextBlock));

            columnStyle.Setters.Add(
                new Setter(TextBlock.TextProperty, uomCode));

            columnStyle.Setters.Add(
               new Setter(TextBlock.BackgroundProperty, Brushes.LightGray));

            unidadBase.ElementStyle = columnStyle;
        }

        public void InicializacionBasica()
        {
            var result = cn.ConsultaDefinicionUnidadesMedida(UgpEntry);

            if (result.Item2 == null)
            {

                dt = result.Item1.Copy();

                dgDefinicionUnidadesMedidas.ItemsSource = dt.DefaultView;
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
            }

          
        }

        private void dgDefinicionUnidadesMedidas_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            btnOk.Content = "Actualizar";
        }

        private void txtAltQty_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void ExitCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ExitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            DataRowView row_Selected = dgDefinicionUnidadesMedidas.SelectedItem as DataRowView;

            int ugpEntry;

            int lineNum;

            MessageBoxResult messageBoxResult = MessageBox.Show("¿Desea eliminar el la definicion de unidad de medida?", "Grupo Unidad Medida", MessageBoxButton.YesNo, MessageBoxImage.Warning);


            if (messageBoxResult == MessageBoxResult.Yes)
            {

                if (row_Selected != null)
                {

                    ugpEntry =Convert.ToInt32(row_Selected["UgpEntry"]);

                    lineNum = Convert.ToInt32(row_Selected["LineNum"]);

                    var result = cn.EliminaDefinicionGrupoUnidadMedida(ugpEntry, lineNum);

                    if (result.Item1 == 1)
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Definicion de grupo de unidad de medida:" + ugpEntry + " se elimino correctamente", Brushes.LightGreen, Brushes.Black, "001-interface.png");

                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al eliminar la definicion de grupo de unidad de medida: " + ugpEntry + " " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");

                    }

                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se selecciono ningun grupo de unidad de medida", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");

                }


            }

            InicializacionBasica();
        }

        private void txtUomCode_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void imgUomCode_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var row_list = GetDataGridRows(dgDefinicionUnidadesMedidas);

                DataRowView row_Selected = dgDefinicionUnidadesMedidas.CurrentItem as DataRowView;

                foreach (DataGridRow single_row in row_list)
                {
                    if (single_row.IsEditing == true)
                    {
                        TextBox txtUomCode = FindChild<TextBox>(single_row, "txtUomCode");                                        

                        var result = cn.ConsultaUnidadesBaseMedidas(BaseUom);

                        if (result.Item2 == null)
                        {
                            RecorreListaUnidadesMedida(result.Item1,  txtUomCode, row_Selected);
                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error" + ex.Message, Brushes.Red, Brushes.Black, "003-interface-2.png");
            }
        }

        private void RecorreListaUnidadesMedida(List<UnidadesMedida> listUnidadesMedidaResultante,  TextBox txtUomCode, DataRowView row_Selected)
        {
            if (listUnidadesMedidaResultante.Count == 0)
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se encontro ningun resultado con los parametros establecidos", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");
            }

            else if (listUnidadesMedidaResultante.Count > 0)
            {
                ListaUnidadesMedida ventanaListaUnidadesMedidas = new ListaUnidadesMedida(listUnidadesMedidaResultante);

                ventanaListaUnidadesMedidas.ShowDialog();

                if (ventanaListaUnidadesMedidas.Visibility == Visibility.Hidden)
                {
                    if (ventanaListaUnidadesMedidas.GetListUnidadesMedida().Count == 0)
                    {

                    }
                    else
                    {
                        GetUnidadesMedida(ventanaListaUnidadesMedidas.GetListUnidadesMedida(), txtUomCode, row_Selected);
                    }
                }
            }
        }

        private void GetUnidadesMedida(List<UnidadesMedida> listUnidadesMedida, TextBox txtUomCode, DataRowView row_Selected)
        {
            foreach (UnidadesMedida unidadesMedida in listUnidadesMedida)
            {
                txtUomCode.Text = unidadesMedida.UomCode;

                row_Selected["UomCode"] = unidadesMedida.UomCode;

                row_Selected["UomEntry"] = unidadesMedida.UomEntry;
               
               
            }
        }
    }

    public static class CustomCommandsDefinicionGruposUnidadesMedida
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
