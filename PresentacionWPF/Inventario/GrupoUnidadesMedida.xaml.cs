using Microsoft.Office.Interop.Excel;
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
using Negocio.Controlador_Inventario;
using System.Data;
using Entidades;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para GrupoUnidadesMedida.xaml
    /// </summary>
    public partial class GrupoUnidadesMedida : Document
    {
        ControladorGrupoUnidadesMedida cn = new ControladorGrupoUnidadesMedida();

        ControladorDefinicionUnidadesMedida cu = new ControladorDefinicionUnidadesMedida();

        private DefinicionUnidadesMedida objectDefinicionUnidadesMedida;

        private System.Data.DataTable dt = new System.Data.DataTable(); 
        public GrupoUnidadesMedida()
        {
            InitializeComponent();
        }

        private void btnDefinicion_Click(object sender, RoutedEventArgs e)
        {
            int index = dgGrupoUnidadesMedidas.SelectedIndex;

            if (index>-1)
            {
                DataRowView row_Selected = dgGrupoUnidadesMedidas.SelectedItem as DataRowView;

                objectDefinicionUnidadesMedida.Show();

                if ((String.IsNullOrWhiteSpace(row_Selected["UgpEntry"].ToString())==true) && (String.IsNullOrWhiteSpace(row_Selected["BaseUom"].ToString()) == true) && (String.IsNullOrWhiteSpace(row_Selected["UomCode"].ToString()) == true))
                {

                }
                else
                {
                    objectDefinicionUnidadesMedida.SetFields((Convert.ToInt32(row_Selected["UgpEntry"])), (Convert.ToInt32(row_Selected["BaseUom"])), row_Selected["UomCode"].ToString());
                }


                objectDefinicionUnidadesMedida.LoadedWindow();                

            }


        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (btnOk.Content.ToString() == "OK")
            {

                this.Hide();
            }

            else if (btnOk.Content.ToString() == "Actualizar")
            {
                if (IsValid(dgGrupoUnidadesMedidas) == true)//no existe ningun error
                {
                    List<Entidades.GrupoUnidadesMedidaCabecera> listaGrupoUnidadesMedida = new List<Entidades.GrupoUnidadesMedidaCabecera>();

                    int i = 0;

                    foreach (DataRow row in dt.Rows)
                    {
                        Entidades.GrupoUnidadesMedidaCabecera medidasCabecera = new Entidades.GrupoUnidadesMedidaCabecera();

                        medidasCabecera.OldUgpCode= row["OldUgpCode"].ToString();//revisar
                        medidasCabecera.UgpCode = row["UgpCode"].ToString();
                        medidasCabecera.UgpName = row["UgpName"].ToString();
                        medidasCabecera.BaseUom = Convert.ToInt32(row["BaseUom"].ToString());                       
                        medidasCabecera.UserSign = Properties.Settings.Default.Usuario;
                        medidasCabecera.LogInstanc = 0;
                        medidasCabecera.UpdateDate =(DateTime)fechaActual.GetFechaActual();
                        

                        listaGrupoUnidadesMedida.Add(medidasCabecera);

                        i++;

                    }

                    var result = cn.InsertaGrupoUnidadesMedidaCabecera(listaGrupoUnidadesMedida);

                    if (i*2 == result.Item1)
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Operacion se realizo correctamente", Brushes.LightGreen, Brushes.Black, "001-interface.png");

                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error. Un grupo de unidad de medida no se actualizo correctamente: " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");

                    }

                    InicializacionBasica();
                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error. Existe algun elemento del formulario con erorres: ", Brushes.Red, Brushes.White, "003-interface-2.png");
                }

            }

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CreateObjectDefinicionUnidadesMedida();
        }

        private void CreateObjectDefinicionUnidadesMedida()
        {
            DefinicionUnidadesMedida definicionUnidades = new DefinicionUnidadesMedida();

            objectDefinicionUnidadesMedida = definicionUnidades;
        }

        public void LoadedWindow()
        {
            InicializacionBasica();
        }

        private void InicializacionBasica()
        {
            btnOk.Content = "OK";

            var result = cn.ConsultaGrupoUnidadesMedida();

            if (result.Item2 == null)
            {

                dt = result.Item1.Copy();                

                dgGrupoUnidadesMedidas.ItemsSource = dt.DefaultView;
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
            }
        }

        private void ExitCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ExitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            DataRowView row_Selected = dgGrupoUnidadesMedidas.SelectedItem as DataRowView;

            string ugpCode;

            MessageBoxResult messageBoxResult = MessageBox.Show("¿Desea eliminar el grupo de unidad de medida?", "Grupo Unidad Medida", MessageBoxButton.YesNo, MessageBoxImage.Warning);


            if (messageBoxResult == MessageBoxResult.Yes)
            {

                if (row_Selected != null)
                {

                    ugpCode = row_Selected["UgpCode"].ToString();

                    var result = cn.EliminaGrupoUnidadMedida(ugpCode);

                    if (result.Item1 == 1)
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Grupo de unidad de medida:" + ugpCode + " se elimino correctamente", Brushes.LightGreen, Brushes.Black, "001-interface.png");

                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al eliminar el grupo de unidad de medida: " + ugpCode + " " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");

                    }

                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se selecciono ningun grupo de unidad de medida", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");

                }


            }

            InicializacionBasica();
        }

        private void dgGrupoUnidadesMedidas_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            btnOk.Content = "Actualizar";
        }

        private void txtUgpCode_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void txtUomCode_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void imgUomCode_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var row_list = GetDataGridRows(dgGrupoUnidadesMedidas);

                DataRowView row_Selected = dgGrupoUnidadesMedidas.CurrentItem as DataRowView;

                foreach (DataGridRow single_row in row_list)
                {
                    if (single_row.IsEditing == true)
                    {
                        System.Windows.Controls.TextBox txtUomCode = FindChild<System.Windows.Controls.TextBox>(single_row, "txtUomCode");

                        var result = cu.ConsultaUnidadesBaseMedidas();

                        if (result.Item2 == null)
                        {
                            RecorreListaUnidadesMedida(result.Item1, txtUomCode, row_Selected);
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

        private void RecorreListaUnidadesMedida(List<UnidadesMedida> listUnidadesMedidaResultante, System.Windows.Controls.TextBox txtUomCode, DataRowView row_Selected)
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

        private void GetUnidadesMedida(List<UnidadesMedida> listUnidadesMedida, System.Windows.Controls.TextBox txtUomCode, DataRowView row_Selected)
        {
            foreach (UnidadesMedida unidadesMedida in listUnidadesMedida)
            {
                txtUomCode.Text = unidadesMedida.UomCode;

                row_Selected["UomCode"] = unidadesMedida.UomCode;

                row_Selected["BaseUom"] = unidadesMedida.UomEntry;


            }
        }
    }

    public static class CustomCommandsGrupoUnidadesMedida
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
