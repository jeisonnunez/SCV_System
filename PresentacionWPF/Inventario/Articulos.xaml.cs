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
using System.Data;
using WPFtoolkitFrameworkStalin.ViewModels;
using Negocio.Controlador_Inventario;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para Articulos.xaml
    /// </summary>
    public partial class Articulos : Document
    {
        ControladorArticulos cn = new ControladorArticulos();

        ControladorGrupoUnidadesMedida cg = new ControladorGrupoUnidadesMedida();

        ControladorDefinicionUnidadesMedida cu = new ControladorDefinicionUnidadesMedida();

        private List<Entidades.Articulos> listaArticulos = new List<Entidades.Articulos>();

        private string oldItemCode;
        public string OldItemCode { get => oldItemCode; set => oldItemCode = value; }
        public List<Entidades.Articulos> ListaArticulos { get => listaArticulos; set => listaArticulos = value; }
        public int UgpEntry { get; private set; }
        public string UgpCode { get; private set; }
        public string UomName { get; private set; }
        public int UomEntry { get; private set; }

        DataTable dt = new DataTable();

        DataTable dtCuentas = new DataTable();

        public Articulos()
        {
            InitializeComponent();
        }

        private void comboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (btnCrear.Content.ToString() != "Buscar")
                App.comboBox_LostFocus(sender, e);
        }

        

        private void comboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (btnCrear.Content.ToString() != "Buscar")
                App.comboBox_GotFocus(sender, e);
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (btnCrear.Content.ToString() != "Buscar")
                App.textBox_LostFocus(sender, e);
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (btnCrear.Content.ToString() != "Buscar")
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

        private void imgCrear_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            InicializacionBasica();

            LimpiarDG();

            ClearDatagrid();
        }

        

        
        private void InicializacionBasica()
        {
            btnCrear.Content = "Crear";

            LimpiarCampos();

            ReestablecerFondo();

            LoadMetodoValoracion();

            LoadGrupoUnidad();

          

        }

        

        private void LoadGrupoUnidad()
        {
            var result = cg.ConsultaGrupoUnidadesMedidas();

            if (result.Item2 == null)
            {
                cbGrupoUnidad.ItemsSource = result.Item1;
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error" + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
            }
        }

        private void LoadMetodoValoracion()
        {
            var result1 = cn.ConsultaMetodoValoracion();
            
            cbMetodoValoracion.ItemsSource = result1;           
        }

        private void ClearDatagrid()
        {
            try
            {
                var row_list = GetDataGridRows(dgCuentas);                

                foreach (DataGridRow single_row in row_list)
                {                    

                    TextBlock txtAcctName = FindChild<TextBlock>(single_row, "txtAcctName");

                    txtAcctName.Text = "";

                }
            }

            catch (Exception ex)
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error" + ex.Message, Brushes.Red, Brushes.Black, "003-interface-2.png");
            }
        }

        

        private void LimpiarDG()
        {
            foreach (DataRow row in dtCuentas.Rows)
            {
                foreach (DataColumn column in dtCuentas.Columns)
                {

                    if (column.ToString() == "AcctCode")
                    {
                        row["AcctCode"] = "";

                    }

                   
                    else if (column.ToString() == "AcctName")
                    {
                        row["AcctName"] = "";

                    }

                }

            }

            //dgCuentas.ItemsSource = null;

            //dgCuentas.ItemsSource = dt.DefaultView;

            dt.Rows.Clear();

            dgItems.ItemsSource = dt.Rows;


        }

        public void LoadedWindow()
        {
            InicializacionBasica();
        }

        private void EstablecerFondo()
        {
            Style style = Application.Current.FindResource("ComboBoxStyleHover") as Style;

            txtNroArticulo.Background = Brushes.LightBlue;
            txtDescripcion.Background = Brushes.LightBlue;
            cbListaPrecio.Style = style;
          
        }

        private void ReestablecerFondo()
        {
            Style style = Application.Current.FindResource("ComboBoxStyle") as Style;

            txtNroArticulo.Background = Brushes.White;
            txtDescripcion.Background = Brushes.White;
            cbListaPrecio.Style = style;
            
            
        }

        private void LimpiarCampos()
        {
            txtNroArticulo.Text = "";
            txtDescripcion.Text = "";
            cbListaPrecio.SelectedValue = "";

            txtArticuloUnidad.Text = "";

            txtCodigoUnidad.Text = "";
            
            cbxCompra.IsChecked = false;
            cbxImpuesto.IsChecked = false;           
            cbxInventario.IsChecked = false;
            cbxVenta.IsChecked = false;

            dgItems.ItemsSource = dt.DefaultView;

        }

        private void imgFin_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (btnCrear.Content.ToString() == "OK" || btnCrear.Content.ToString() == "Crear" || btnCrear.Content.ToString() == "Buscar")
            {
                ReestablecerFondo();

              

                var result= cn.FindLast();

                if (result.Item2 == null) {
                                       
                    ListaArticulos = result.Item1;

                    GetArticulos(ListaArticulos);

                    btnCrear.Content = "OK";
                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                }
            }

            else if (btnCrear.Content.ToString() == "Actualizar")
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("¿Desea salir sin guardar cambios?", "Item", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    btnCrear.Content = "OK";

                }
            }
        }

        private void GetArticulos(List<Entidades.Articulos> listaArticulos)
        {
            foreach (Entidades.Articulos item in listaArticulos)
            {
                OldItemCode = item.ItemCode;

                txtNroArticulo.Text = item.ItemCode;

                txtDescripcion.Text = item.ItemName;

                cbxCompra.IsChecked = cn.EstadoCompra(item.PrchseItem);

                cbxVenta.IsChecked = cn.EstadoVenta(item.SellItem);

                cbxInventario.IsChecked = cn.EstadoInventario(item.InvnItem);

                cbxImpuesto.IsChecked = cn.EstadoImpuesto(item.VatLiable);

                cbMetodoValoracion.SelectedValue = item.EvalSystem;

                UgpEntry = item.UgpEntry;

                cbGrupoUnidad.SelectedValue = item.UgpEntry.ToString();

                txtCodigoUnidad.Text = item.InvntryUomCode;

                txtArticuloUnidad.Text = ConvertDecimalTwoPlaces(item.NumInCnt).ToString("N2", nfi);

                txtNombreUnidad.Text= item.InvntryUomName;



            }

            dgItems.ItemsSource = listaArticulos;

            LoadedDgCuentas(listaArticulos);

           
        }

      
        private void LoadedDgCuentas(List<Entidades.Articulos> listaArticulos)
        {
            foreach (Entidades.Articulos item in listaArticulos)
            {
                foreach (DataRow row in dtCuentas.Rows)
                {
                    foreach (DataColumn column in dtCuentas.Columns)
                    {

                        if (column.ToString() == "AcctCode" && row["ID"].ToString() == "Cuenta Existencia")
                        {
                            row["AcctCode"] = item.BalInvntAc;
                            row["AcctName"] = cn.FindAcctName(item.BalInvntAc);

                        }
                        else if (column.ToString() == "AcctCode" && row["ID"].ToString() == "Cuenta Ingresos")
                        {
                            row["AcctCode"] = item.RevenuesAc;
                            row["AcctName"] = cn.FindAcctName(item.RevenuesAc);

                        }
                        else if (column.ToString() == "AcctCode" && row["ID"].ToString() == "Cuenta Gastos")
                        {
                            row["AcctCode"] = item.ExpensesAc;
                            row["AcctName"] = cn.FindAcctName(item.ExpensesAc);

                        }
                        else if (column.ToString() == "AcctCode" && row["ID"].ToString() == "Costo Venta")
                        {
                            row["AcctCode"] = item.SaleCostAc;
                            row["AcctName"] = cn.FindAcctName(item.SaleCostAc);

                        }

                        else if (column.ToString() == "AcctCode" && row["ID"].ToString() == "Cuenta Dotacion")
                        {
                            row["AcctCode"] = item.TransferAc;
                            row["AcctName"] = cn.FindAcctName(item.TransferAc);

                        }
                    }
                }
            }
        }

        private void imgRight_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (btnCrear.Content.ToString() == "OK" || btnCrear.Content.ToString() == "Crear" || btnCrear.Content.ToString() == "Buscar")
            {
                ReestablecerFondo();

                

                var result= cn.FindNext(txtNroArticulo.Text);

                if (result.Item2 == null)
                {

                    ListaArticulos = result.Item1;

                    GetArticulos(ListaArticulos);

                    btnCrear.Content = "OK";

                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                }

            }

            else if (btnCrear.Content.ToString() == "Actualizar")
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("¿Desea salir sin guardar cambios?", "Item", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    btnCrear.Content = "OK";

                }
            }
        }

        private void imgleft_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (btnCrear.Content.ToString() == "OK" || btnCrear.Content.ToString() == "Crear" || btnCrear.Content.ToString() == "Buscar")
            {
                ReestablecerFondo();                

                var result= cn.FindPrevious(txtNroArticulo.Text);

                if (result.Item2 == null) { 

                    ListaArticulos = result.Item1;

                    GetArticulos(ListaArticulos);

                    btnCrear.Content = "OK";
                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                }
            }

            else if (btnCrear.Content.ToString() == "Actualizar")
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("¿Desea salir sin guardar cambios?", "Item", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    btnCrear.Content = "OK";

                }
            }
        }

        private void imgInicio_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (btnCrear.Content.ToString() == "OK" || btnCrear.Content.ToString() == "Crear" || btnCrear.Content.ToString() == "Buscar")
            {
                ReestablecerFondo();                

                var result= cn.FindFirst();

                if (result.Item2 == null)
                {
                    ListaArticulos = result.Item1;

                    GetArticulos(ListaArticulos);

                    btnCrear.Content = "OK";
                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                }
            }

            else if (btnCrear.Content.ToString() == "Actualizar")
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("¿Desea salir sin guardar cambios?", "Item", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    btnCrear.Content = "OK";

                }
            }
        }

        private void imgBuscar_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            btnCrear.Content = "Buscar";

            LimpiarCampos();

            EstablecerFondo();
        }

        private void btnCrear_Click(object sender, RoutedEventArgs e)
        {
            List<Entidades.Articulos> listaArticulo = new List<Entidades.Articulos>();

            Entidades.Articulos articulos = new Entidades.Articulos();

            switch (btnCrear.Content.ToString())
            {
                case "OK":

                    this.Hide();

                    break;

                case "Buscar":

                    articulos.ItemCode = txtNroArticulo.Text;
                    articulos.ItemName = txtDescripcion.Text;
                    articulos.PrchseItem =cn.EstadoCompra(cbxCompra.IsChecked);
                    articulos.SellItem = cn.EstadoVenta(cbxVenta.IsChecked);
                    articulos.InvnItem = cn.EstadoInventario(cbxInventario.IsChecked);
                    articulos.EvalSystem = cbMetodoValoracion.SelectedValue.ToString();
                    articulos.UgpEntry =Convert.ToInt32(cbGrupoUnidad.SelectedValue.ToString());

                    listaArticulo.Add(articulos);

                    var result = cn.ConsultaItems(listaArticulo);

                    if (result.Item2 == null)
                    {
                        RecorreListaItems(result.Item1);
                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                    }

                    break;

                case "Crear":

                    articulos=GetAcctInventary(articulos);

                    articulos.ItemCode = txtNroArticulo.Text;
                    articulos.ItemName = txtDescripcion.Text;
                    articulos.PrchseItem = cn.EstadoCompra(cbxCompra.IsChecked);
                    articulos.SellItem = cn.EstadoVenta(cbxVenta.IsChecked);
                    articulos.InvnItem = cn.EstadoInventario(cbxInventario.IsChecked);
                    articulos.VatLiable = cn.EstadoImpuesto(cbxImpuesto.IsChecked);
                    articulos.EvalSystem = cbMetodoValoracion.SelectedValue.ToString();
                    articulos.UserSign = Properties.Settings.Default.Usuario;
                    articulos.UpdateDate1 = fechaActual.GetFechaActual();
                    articulos.Deleted = 'Y';
                    articulos.OnHand = 0;
                    articulos.IsCommited = 0;
                    articulos.OnOrders = 0;
                    articulos.StockValue = 0;
                    articulos.UgpEntry = Convert.ToInt32(cbGrupoUnidad.SelectedValue.ToString());
                    articulos.InvntryUomCode = txtNombreUnidad.Text;
                    articulos.InvntryUomName = UomName;
                    articulos.NumInCnt = ConvertDecimalTwoPlaces(txtArticuloUnidad.Text);
                    articulos.InvntryUomEntry = UomEntry;


                    listaArticulo.Add(articulos);

                    var result1 = cn.InsertItems(listaArticulo);

                    if (result1.Item1 == 1)
                    {    
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Articulo " + articulos.ItemCode + " se creo correctamente", Brushes.LightGreen, Brushes.Black, "001-interface.png");
                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error en la creacion del articulo " + articulos.ItemCode + " " + result1.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                        
                    }

                    btnCrear.Content = "OK";

                    break;

                case "Actualizar":

                    articulos = GetAcctInventary(articulos);
                    articulos.OldItemCode = OldItemCode;
                    articulos.ItemCode = txtNroArticulo.Text;
                    articulos.ItemName = txtDescripcion.Text;
                    articulos.PrchseItem = cn.EstadoCompra(cbxCompra.IsChecked);
                    articulos.SellItem = cn.EstadoVenta(cbxVenta.IsChecked);
                    articulos.InvnItem = cn.EstadoInventario(cbxInventario.IsChecked);
                    articulos.VatLiable = cn.EstadoImpuesto(cbxImpuesto.IsChecked);
                    articulos.EvalSystem = cbMetodoValoracion.SelectedValue.ToString();
                    articulos.UserSign = Properties.Settings.Default.Usuario;
                    articulos.UpdateDate1 = fechaActual.GetFechaActual();
                    articulos.UgpEntry = Convert.ToInt32(cbGrupoUnidad.SelectedValue.ToString());
                    articulos.InvntryUomCode = txtNombreUnidad.Text;
                    articulos.InvntryUomName = UomName;
                    articulos.NumInCnt = ConvertDecimalTwoPlaces(txtArticuloUnidad.Text);
                    articulos.InvntryUomEntry = UomEntry;

                    listaArticulo.Add(articulos);

                    var result2 = cn.UpdateItems(listaArticulo);

                    if (result2.Item1 == 1)
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Articulo " + articulos.ItemCode + " se actualizo correctamente", Brushes.LightGreen, Brushes.Black, "001-interface.png");
                        
                    }
                    else
                    {                       
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error en la actualizacion del articulo " + articulos.ItemCode + " " + result2.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
                    }

                    btnCrear.Content = "OK";

                    break;
            }
        }

        private Entidades.Articulos GetAcctInventary(Entidades.Articulos articulos)
        {
            foreach (DataRow row in dtCuentas.Rows)
            {
                foreach (DataColumn column in dtCuentas.Columns)
                {

                    if (column.ToString() == "AcctCode" && row["ID"].ToString() == "Cuenta Existencia")
                    {
                        articulos.BalInvntAc = row["AcctCode"].ToString();

                    }
                    else if (column.ToString() == "AcctCode" && row["ID"].ToString() == "Cuenta Ingresos")
                    {
                        articulos.RevenuesAc = row["AcctCode"].ToString();

                    }
                    else if (column.ToString() == "AcctCode" && row["ID"].ToString() == "Cuenta Gastos")
                    {
                        articulos.ExpensesAc = row["AcctCode"].ToString();

                    }
                    else if (column.ToString() == "AcctCode" && row["ID"].ToString() == "Costo Venta")
                    {
                        articulos.SaleCostAc = row["AcctCode"].ToString();

                    }

                    else if (column.ToString() == "AcctCode" && row["ID"].ToString() == "Cuenta Dotacion")
                    {
                        articulos.TransferAc = row["AcctCode"].ToString();

                    }
                }
            }

            return articulos;
        }

        private void RecorreListaItems(List<Entidades.Articulos> newlistaItems)
        {
            if (newlistaItems.Count == 1)
            {
                

                GetArticulos(newlistaItems);

                btnCrear.Content = "OK";
            }
            else if (newlistaItems.Count == 0)
            {                
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se encontro ningun resultado con los parametros establecidos", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");

                LimpiarCampos();

                btnCrear.Content = "OK";
            }

            else if (newlistaItems.Count > 1)
            {
                ListaArticulos ventanaListBox = new ListaArticulos(newlistaItems);

                ventanaListBox.ShowDialog();

                if (ventanaListBox.Visibility == Visibility.Hidden)
                {
                    if (ventanaListBox.GetListItem().Count == 0)
                    {                        
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No selecciono ningun elemento", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");

                        LimpiarCampos();
                    }
                    else
                    {
                      

                        GetArticulos(ventanaListBox.GetListItem());

                    }

                    btnCrear.Content = "OK";
                }

               
            }

            ReestablecerFondo();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDgCuentas();
        }

        private void LoadDgCuentas()
        {
            dtCuentas.Columns.Add("ID");
            dtCuentas.Columns.Add("AcctCode");
            dtCuentas.Columns.Add("AcctName");

            DataRow newRow = dtCuentas.NewRow();

            dtCuentas.Rows.Add(newRow);

            newRow["ID"] = "Cuenta Gastos";

            newRow["AcctCode"] = "";

            newRow["AcctName"] = "";

            DataRow newRow1 = dtCuentas.NewRow();

            dtCuentas.Rows.Add(newRow1);

            newRow1["ID"] = "Cuenta Ingresos";

            newRow1["AcctCode"] = "";

            newRow1["AcctName"] = "";

            DataRow newRow2 = dtCuentas.NewRow();

            dtCuentas.Rows.Add(newRow2);

            newRow2["ID"] = "Cuenta Existencia";

            newRow2["AcctCode"] = "";

            newRow2["AcctName"] = "";

            DataRow newRow3 = dtCuentas.NewRow();

            dtCuentas.Rows.Add(newRow3);

            newRow3["ID"] = "Costo Venta";

            newRow3["AcctCode"] = "";

            newRow3["AcctName"] = "";

            DataRow newRow4 = dtCuentas.NewRow();

            dtCuentas.Rows.Add(newRow4);

            newRow4["ID"] = "Cuenta Dotacion";

            newRow4["AcctCode"] = "";

            newRow4["AcctName"] = "";

            dgCuentas.ItemsSource = dtCuentas.DefaultView;
        }

        private void cbxInventario_Click(object sender, RoutedEventArgs e)
        {
            if (btnCrear.Content.ToString() == "OK")
            {
                btnCrear.Content = "Actualizar";
            }
        }

        private void txtNroArticulo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (btnCrear.Content.ToString() == "OK")
            {
                btnCrear.Content = "Actualizar";
            }
        }

        private void cbPrecioUnidad_DropDownOpened(object sender, EventArgs e)
        {
            if (btnCrear.Content.ToString() == "OK")
            {
                btnCrear.Content = "Actualizar";
            }
        }

        private void txtDescripcion_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (btnCrear.Content.ToString() == "OK")
            {
                btnCrear.Content = "Actualizar";
            }
        }

        private void Deleted_Click(object sender, RoutedEventArgs e)
        {
            if (btnCrear.Content.ToString() == "OK" && String.IsNullOrWhiteSpace(txtNroArticulo.Text) == false)
            {

                MessageBoxResult messageBoxResult = MessageBox.Show("¿Desea eliminar el articulo?", "Item", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    string itemCode = txtNroArticulo.Text;
                    
                    var result = cn.DeleteItem(itemCode);

                        if (result.Item1 == 1)
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Articulo: " + itemCode + " se elimino correctamente", Brushes.LightGreen, Brushes.Black, "001-interface.png");

                            LimpiarCampos();
                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se puede eliminar el articulo: " + itemCode + " porque se realizo una transaccion con el mismo: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                        }                                        
                }

            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se selecciono ningun articulo", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");
            }
        }

        private void imgAccount_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var row_list = GetDataGridRows(dgCuentas);

                DataRowView row_Selected = dgCuentas.SelectedItem as DataRowView;

                foreach (DataGridRow single_row in row_list)
                {
                    if (single_row.IsSelected == true)
                    {
                        TextBox txtShortName = FindChild<TextBox>(single_row, "txtAccount");

                        TextBlock txtAcctName = FindChild<TextBlock>(single_row, "txtAcctName");                        

                        var result = cn.ConsultaCuentasNoAsociadas();

                        if (result.Item2 == null)
                        {
                            RecorreListaAccount(result.Item1, txtShortName, txtAcctName, row_Selected);
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

        private void RecorreListaAccount(List<Cuenta> listAccountResultante, TextBox txtShortName, TextBlock txtAcctName, DataRowView row_Selected)
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
                        GetAcctCode(ventanaListaCuentaAsociada.GetListAccount(), txtShortName, txtAcctName, row_Selected);
                    }
                }
            }
        }

        private void GetAcctCode(List<Cuenta> listaCuenta, TextBox txtShortName, TextBlock txtAcctName, DataRowView row_Selected)
        {
            foreach (Cuenta cuenta in listaCuenta)
            {
                txtShortName.Text = cuenta.AcctCode;               

                row_Selected["AcctCode"] = cuenta.AcctCode;

                row_Selected["AcctName"] = cuenta.AcctName;

                txtAcctName.Text = row_Selected["AcctName"].ToString();
            }
        }

        private void cbMetodoValoracion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (btnCrear.Content.ToString() == "OK")
            {
                btnCrear.Content = "Actualizar";
            }
        }

        
        private void cbGrupoUnidad_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbGrupoUnidad.SelectedIndex > -1)
            {


                UgpEntry = Convert.ToInt32(cbGrupoUnidad.SelectedValue.ToString());

                var result = cu.ConsultaDefinicionUnidadesMedidaFirstLine(UgpEntry);

                if (result.Item2 == null)
                {
                    RecorreDefinicionUnidadesMedidaFirst(result.Item1);

                    if(btnCrear.Content.ToString()=="OK")
                    {
                        btnCrear.Content = "Actualizar";
                    }
                   
                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                }

            }


        }

        private void RecorreDefinicionUnidadesMedidaFirst(DataTable dt)
        {
           foreach(DataRow row in dt.Rows)
            {
                UomName= row["UomName"].ToString();

                UomEntry = Convert.ToInt32(row["UomEntry"]);

                txtCodigoUnidad.Text= row["UomCode"].ToString();

                txtNombreUnidad.Text =row["UomName"].ToString();

                txtArticuloUnidad.Text =ConvertDecimalTwoPlaces(ConvertDecimalTwoPlaces(row["BaseQty"]) / ConvertDecimalTwoPlaces(row["AltQty"])).ToString("N2",nfi);

                break;
            }

           
        }

        private void txtCodigoUnidad_GotFocus(object sender, RoutedEventArgs e)
        {
            if (btnCrear.Content.ToString() == "Buscar")
            {

            }
            else
            {
                dpCodigoUnidad.Background = Brushes.LightBlue;

                bdCodigUnidad.Background = Brushes.LightBlue;

                txtCodigoUnidad.Background = Brushes.LightBlue;

                imgCodigoUnidad.Visibility = Visibility.Visible;
            }
        }

        private void txtCodigoUnidad_LostFocus(object sender, RoutedEventArgs e)
        {
            if (btnCrear.Content.ToString() == "Buscar")
            {

            }
            else
            {
                dpCodigoUnidad.Background = Brushes.White;

                bdCodigUnidad.Background = Brushes.White;

                txtCodigoUnidad.Background = Brushes.White;
            }

            imgCodigoUnidad.Visibility = Visibility.Hidden;
        }

        private void imgCodigoUnidad_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var result = cu.ConsultaDefinicionUnidadesMedidaSpecific(UgpEntry);

            if (result.Item2 == null)
            {
                RecorreListaUnidadesMedida(result.Item1);
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error" + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
            }
        }

        private void RecorreListaUnidadesMedida(List<UnidadesMedida> listUnidadesMedidas)
        {
            if (listUnidadesMedidas.Count == 1)
            {
                GetUnidadesMedida(listUnidadesMedidas);                

                
            }
            else if (listUnidadesMedidas.Count == 0)
            {

                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se encontro ningun resultado con los parametros establecidos", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");

               
            }

            else if (listUnidadesMedidas.Count > 1)
            {
                ListaUnidadesMedida ventanaListBox = new ListaUnidadesMedida(listUnidadesMedidas);

                ventanaListBox.ShowDialog();

                if (ventanaListBox.Visibility == Visibility.Hidden)
                {
                    if (ventanaListBox.GetListUnidadesMedida().Count == 0)
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se selecciono ningun elemento", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");

                        
                    }
                    else
                    {

                        GetUnidadesMedida(ventanaListBox.GetListUnidadesMedida());                     

                        dpCodigoUnidad.Background = Brushes.White;

                        bdCodigUnidad.Background = Brushes.White;

                        txtCodigoUnidad.Background = Brushes.White;



                    }

                    //btnCrear.Content = "OK";
                }

            }


        }

        private void GetUnidadesMedida(List<UnidadesMedida> listUnidadMedida)
        {
            foreach (UnidadesMedida UnidadMedida in listUnidadMedida)
            {
                UomName= UnidadMedida.UomName;

                UomEntry = UnidadMedida.UomEntry;

                txtCodigoUnidad.Text = UnidadMedida.UomCode;

                txtNombreUnidad.Text = UnidadMedida.UomName;

                txtArticuloUnidad.Text = ConvertDecimalTwoPlaces(UnidadMedida.BaseQty/UnidadMedida.AltQty).ToString("N2", nfi);
            }

           
        }
    }
}
