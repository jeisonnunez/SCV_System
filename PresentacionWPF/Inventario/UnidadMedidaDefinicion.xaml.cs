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
using Vista.Inventario.ModelUnidadMedidaDefinicion;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para UnidadMedidaDefinicion.xaml
    /// </summary>
    public partial class UnidadMedidaDefinicion : Document
    {
        ControladorUnidadesMedida cn = new ControladorUnidadesMedida();

        List<ModelUnidadMedidaDefinicion> listUnidadesMedida = new List<ModelUnidadMedidaDefinicion>();

        DataTable dt = new DataTable();
        public UnidadMedidaDefinicion()
        {
            InitializeComponent();
        }

        private void dgUnidadesMedidas_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            btnOk.Content = "Actualizar";
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

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
                if (IsValid(dgUnidadesMedidas) == true)//no existe ningun error
                {
                    List<Entidades.UnidadesMedida> listaUnidadesMedida = new List<Entidades.UnidadesMedida>();

                    int i = 0;

                    foreach (ModelUnidadMedidaDefinicion unidadesMedidaOriginal in listUnidadesMedida)
                    {
                        Entidades.UnidadesMedida unidadesMedida = new Entidades.UnidadesMedida();

                        unidadesMedida.OldUomCode = unidadesMedidaOriginal.OldUomCode;
                        unidadesMedida.UomCode = unidadesMedidaOriginal.UomCode;
                        unidadesMedida.UomName = unidadesMedidaOriginal.UomName;
                        unidadesMedida.UpdateDate =(DateTime)fechaActual.GetFechaActual();
                        unidadesMedida.UserSign = Properties.Settings.Default.Usuario;
                        unidadesMedida.Length = ConvertDecimalTwoPlaces(unidadesMedidaOriginal.Length);
                        unidadesMedida.Width = ConvertDecimalTwoPlaces(unidadesMedidaOriginal.Width);
                        unidadesMedida.Height = ConvertDecimalTwoPlaces(unidadesMedidaOriginal.Height);
                        unidadesMedida.Volume = ConvertDecimalTwoPlaces(unidadesMedidaOriginal.Volume);
                        unidadesMedida.Weight = ConvertDecimalTwoPlaces(unidadesMedidaOriginal.Weight);
                        unidadesMedida.VolUnit = cn.GetVolUnit(unidadesMedidaOriginal.VolUnit);

                        listaUnidadesMedida.Add(unidadesMedida);

                        i++;

                    }

                    var result = cn.InsertaUnidadesMedidaDefinicion(listaUnidadesMedida);

                    if (i == result.Item1)
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Unidades medida se actualizaron correctamente", Brushes.LightGreen, Brushes.Black, "001-interface.png");

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

        private void txtAltQty_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void txtUomCode_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        public void LoadedWindow()
        {
            InicializacionBasica();
        }

        private void InicializacionBasica()
        {
            btnOk.Content = "OK";

            var result = cn.ConsultaUnidadesMedidaDefinicion();

            if (result.Item2 == null)
            {
                dt = result.Item1;

                listUnidadesMedida=ConvertDataTable<ModelUnidadMedidaDefinicion>(dt);

                dgUnidadesMedidas.ItemsSource = listUnidadesMedida;
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
            }

        }
    }
}
