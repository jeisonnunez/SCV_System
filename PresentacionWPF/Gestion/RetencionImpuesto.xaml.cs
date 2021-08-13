using Entidades;
using Negocio;
using System;
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
using Vista.Gestion.ModelRetencionImpuestos;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para RetencionImpuesto.xaml
    /// </summary>
    public partial class RetencionImpuesto : Document
    {
        ControladorRetencionImpuesto cn = new ControladorRetencionImpuesto();    
        public ObservableCollection<string> Categorias { get; set; }
        public ObservableCollection<string> TipoBase { get; set; }
        public ObservableCollection<string> TipoRetencion { get; set; }

        List<ModelRetencionImpuestos> listRetencionImpuestos = new List<ModelRetencionImpuestos>();

        DataTable dt;
        public RetencionImpuesto()
        {
            InitializeComponent();
        }

        private void InicializacionBasica()
        {
            btnOk.Content = "OK";

            var result = cn.ConsultaRetencion();

            if (result.Item2 == null)
            {
                dt = result.Item1;

                listRetencionImpuestos = ConvertDataTable<ModelRetencionImpuestos>(dt);

                dgRetencionImp.ItemsSource = listRetencionImpuestos;
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
            }
           
        }

        public void LoadedWindow()
        {
            InicializacionBasica();
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {         

            App.Window_Closing(sender, e);
        }

        private void btnWindow_Close(object sender, RoutedEventArgs e)
        {          

            this.Hide();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
         
            Categorias = new ObservableCollection<string>() { "Pago", "Factura" };

            TipoBase = new ObservableCollection<string>() { "Neto", "IVA" };

            TipoRetencion = new ObservableCollection<string>() { "IVA", "ISLR" };

            //cbCategorias.ItemsSource = Categorias;

            //cbTipoBase.ItemsSource = TipoBase;

            //cbTipoRetencion.ItemsSource = TipoRetencion;

            InicializacionBasica();


        }

        public class ListCategoria : List<string>
        {
            public ListCategoria()
            {
                this.Add("Pago");
                this.Add("Factura");

            }
        }

        public class ListTipoBase : List<string>
        {
            public ListTipoBase()
            {
                this.Add("Neto");
                this.Add("IVA");

            }
        }

        public class ListTipoRetencion : List<string>
        {
            public ListTipoRetencion()
            {
                this.Add("IVA");
                this.Add("ISLR");

            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (btnOk.Content.ToString() == "OK")
            {
                InicializacionBasica();

                this.Hide();
            }

            else if (btnOk.Content.ToString() == "Actualizar")
            {
                if (IsValid(dgRetencionImp) == true)
                {
                    List<Entidades.RetencionImpuesto> listaRetenciones = new List<Entidades.RetencionImpuesto>();

                    int i = 0;

                    foreach (ModelRetencionImpuestos row in listRetencionImpuestos)
                    {
                        Entidades.RetencionImpuesto retencionImpuesto = new Entidades.RetencionImpuesto();

                        retencionImpuesto.OldWtCode = row.OldWtCode;
                        retencionImpuesto.Wt_Code = row.WTCode;
                        retencionImpuesto.Wt_Name = row.WTName;
                        retencionImpuesto.Rate = ConvertDecimalTwoPlaces(row.Rate);
                        retencionImpuesto.EffecDate = Convert.ToDateTime(row.EffecDate);
                        retencionImpuesto.Category = Convert.ToChar(cn.GetCategoria(row.Category));
                        retencionImpuesto.BaseType = Convert.ToChar(cn.GetBaseType(row.BaseType));
                        retencionImpuesto.PrctBsAmnt =ConvertDecimalTwoPlaces(row.PrctBsAmnt);
                        retencionImpuesto.OffclCode = row.Offclcode;
                        retencionImpuesto.Account = row.Account;
                        retencionImpuesto.UserSign = Vista.Properties.Settings.Default.Usuario;
                        retencionImpuesto.Inactive = Convert.ToChar(cn.GetInactive(Convert.ToBoolean(row.Inactive)));
                        retencionImpuesto.TipoRetencion = row.U_IDA_TipoRetencion;
                        retencionImpuesto.BaseMinima = ConvertDecimalTwoPlaces(row.U_IDA_BaseMinima);
                        retencionImpuesto.Sustraendo = ConvertDecimalTwoPlaces(row.U_IDA_Sustraendo);

                        listaRetenciones.Add(retencionImpuesto);

                        i++;

                    }

                    var result = cn.InsertaRetenciones(listaRetenciones);

                    if (i == result.Item1)
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Retenciones de impuesto se actualizaron correctamente", Brushes.LightGreen, Brushes.Black, "001-interface.png");


                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error.Una retencion de impuesto no se actualizo correctamente: " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");

                    }


                    InicializacionBasica();

                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error. Existe algun elemento del formulario con erorres: ", Brushes.Red, Brushes.White, "003-interface-2.png");
                }


            }

        }

        private void dgRetencionImp_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            btnOk.Content = "Actualizar";

        }

        private void inactivos_Checked(object sender, RoutedEventArgs e)
        {

            //foreach (DataRowView row_Selected in dgRetencionImp.SelectedItems)
            //{

            //    if (row_Selected != null)
            //    {
            //        row_Selected["Inactive"] = true;
            //    }
            //}

        }

        private void inactivos_Unchecked(object sender, RoutedEventArgs e)
        {
            //foreach (DataRowView row_Selected in dgRetencionImp.SelectedItems)
            //{

            //    if (row_Selected != null)
            //    {
            //        row_Selected["Inactive"] = false;
            //    }
            //}

        }

        private void inactivos_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            btnOk.Content = "Actualizar";
        }

        private void ExitCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ExitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            DataRowView row_Selected = dgRetencionImp.SelectedItem as DataRowView;

            string retencion;

            MessageBoxResult messageBoxResult = MessageBox.Show("Desea eliminar el registro", "Retenciones", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (messageBoxResult == MessageBoxResult.Yes)
            {

                if (row_Selected != null)
                {

                    retencion = row_Selected["WTCode"].ToString();

                    var result = cn.EliminaRetencion(retencion);

                    if (result.Item1 == 1)
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Retencion de impuesto: " + retencion + " se elimino correctamente", Brushes.LightGreen, Brushes.Black, "001-interface.png");

                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se puede eliminar la retencion de impuesto: " + retencion + " porque se realizo uns transaccion con la misma: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                    }

                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se selecciono ninguna moneda", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");
                }

            }

            InicializacionBasica();
        }

        private void imgAccount_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var row_list = GetDataGridRows(dgRetencionImp);

                DataRowView row_Selected = dgRetencionImp.SelectedItem as DataRowView;

                foreach (DataGridRow single_row in row_list)
                {
                    if (single_row.IsSelected == true)
                    {
                        TextBox txtShortName = FindChild<TextBox>(single_row, "txtAccount");                      

                        var result = cn.ConsultaCuentasNoAsociadas();

                        if (result.Item2 == null)
                        {
                            RecorreListaAccount(result.Item1, txtShortName, row_Selected);

                            btnOk.Content = "Actualizar";
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

        private void RecorreListaAccount(List<Cuenta> listAccountResultante, TextBox txtShortName, DataRowView row_Selected)
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

                    }
                    else
                    {
                        GetAcctCode(ventanaListaCuentaAsociada.GetListAccount(), txtShortName, row_Selected);
                    }
                }
            }
        }

        private void GetAcctCode(List<Cuenta> listaCuenta, TextBox txtShortName, DataRowView row_Selected)
        {
            foreach (Cuenta cuenta in listaCuenta)
            {
                txtShortName.Text = cuenta.AcctCode;

                row_Selected["Account"] = cuenta.AcctCode;
                
            }
        }

        private void txtAccount_KeyDown(object sender, KeyEventArgs e)
        {
            btnOk.Content = "Actualizar";
        }
    }

    public static class CustomCommandsRet
    {
        public static readonly RoutedUICommand Exit = new RoutedUICommand
            (
                "Eliminar",
                "Eliminar",
                typeof(CustomCommandsRet),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.F4, ModifierKeys.Alt)
                }
            );

        //Define more commands here, just like the one above
    }
}
