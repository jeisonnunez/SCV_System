using Entidades;
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

using Vista.Gestion.ValitadeErrorsCodigosFiscales;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para CodigosFiscales.xaml
    /// </summary>
    public partial class CodigosFiscales : Document
    {
        ControladorCodigosFiscales cn = new ControladorCodigosFiscales();

        ControladorSocioNegocio cs = new ControladorSocioNegocio();

        List<Entidades.CodigosFiscales> listaResultado = new List<Entidades.CodigosFiscales>();

        List<ValidateErrorsCodigosFiscales> listCodigosFiscales = new List<ValidateErrorsCodigosFiscales>();        

        private string codes;

        private decimal rate=0;

        DataTable dt=new DataTable();
        public string Code { get => codes; set => codes = value; }
        public DataTable ListaCodigoFiscalDetalle { get; private set; }
        public decimal Rate { get => rate; set => rate = value; }

        public CodigosFiscales()
        {
            InitializeComponent();
        }

        private void comboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (btnOk.Content.ToString() != "Buscar")
            {
                App.comboBox_LostFocus(sender, e);
            }
        }

        private void comboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (btnOk.Content.ToString() != "Buscar")
            {
                App.comboBox_GotFocus(sender, e);
            }
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (btnOk.Content.ToString() != "Buscar")
            {
                App.textBox_LostFocus(sender, e);
            }
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if(btnOk.Content.ToString() != "Buscar")
            {
                App.textBox_GotFocus(sender, e);
            }
          
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
         
            App.Window_Closing(sender, e);
        }

        private void btnWindow_Close(object sender, RoutedEventArgs e)
        {
           
            this.Hide();
        }

        public void InicializacionBasica()
        {
            var result = cn.ConsultaAlicuota();

            if (result.Item2 == null)
            {
                cbAlicuota.ItemsSource = result.Item1;
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

        private void imgCrear_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            btnOk.Content = "Crear";

            LimpiarCampos();

            ReestablecerFondo();

            LimpiarDatatable();

            txtCodigo.IsReadOnly = false;
        }

        private void LimpiarDatatable()
        {
            dt.Rows.Clear();

            DataRow dataRow = dt.NewRow();

            dt.Rows.Add(dataRow);

            dt.AcceptChanges();

            dgCodigosFiscales.ItemsSource = dt.DefaultView;
        }

        private void imgFin_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (btnOk.Content.ToString() == "OK" || btnOk.Content.ToString() =="Crear")
            {
                var result = cn.FindLast();

                if (result.Item2 == null)
                {
                    listaResultado = result.Item1;

                    listCodigosFiscales = ConvertValidateErrorsCodigosFiscales(listaResultado);

                    GetCodigoFiscal(listCodigosFiscales);

                    var resultLines = cn.FindCodigosFiscalesLines(Code);

                    if (resultLines.Item2 == null)
                    {
                        ListaCodigoFiscalDetalle = resultLines.Item1;

                        listCodigosFiscales = ConvertDataTable<ValidateErrorsCodigosFiscales>(ListaCodigoFiscalDetalle);

                        GetCodigosFiscalLines(listCodigosFiscales);

                        btnOk.Content = "OK";
                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + resultLines.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                    }
                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                }

                    
            }

            else if (btnOk.Content.ToString() == "Actualizar")
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("¿Desea salir sin guardar cambios?", "Codigos Fiscales", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    btnOk.Content = "OK";
                }
            }
        }

        private void imgRight_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (btnOk.Content.ToString() == "OK" || btnOk.Content.ToString() == "Crear")
            {
                var result= cn.FindNext(txtCodigo.Text);

                if (result.Item2 == null)
                {
                   
                    listaResultado = result.Item1;

                    listCodigosFiscales = ConvertValidateErrorsCodigosFiscales(listaResultado);

                    GetCodigoFiscal(listCodigosFiscales);

                    var resultLines = cn.FindCodigosFiscalesLines(Code);

                    if (resultLines.Item2 == null)
                    {
                        ListaCodigoFiscalDetalle = resultLines.Item1;

                        listCodigosFiscales = ConvertDataTable<ValidateErrorsCodigosFiscales>(ListaCodigoFiscalDetalle);

                        GetCodigosFiscalLines(listCodigosFiscales);

                        btnOk.Content = "OK";
                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + resultLines.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                    }
                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                }

                
            }

            else if (btnOk.Content.ToString() == "Actualizar")
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("¿Desea salir sin guardar cambios?", "Codigos Fiscales", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    btnOk.Content = "OK";
                }
            }
        }

        private List<ValidateErrorsCodigosFiscales> ConvertValidateErrorsCodigosFiscales(List<Entidades.CodigosFiscales> listaResultado)
        {
            List<ValidateErrorsCodigosFiscales> newList = new List<ValidateErrorsCodigosFiscales>();

            foreach(Entidades.CodigosFiscales row in listaResultado)
            {
                ValidateErrorsCodigosFiscales list = new ValidateErrorsCodigosFiscales();

                list.Old_Code = row.Old_Code;
                list.Code = row.Code;
                list.Name = row.Name;
                list.Rate = row.Rate.ToString();
                list.UpdateDate = row.UpdateDate;
                list.UserSign = row.UserSign;
                list.ValidForAP = (bool)cn.EstadoComprasInverso(row.ValidForAP);
                list.ValidForAR = (bool)cn.EstadoVentasInverso(row.ValidForAR);
                list.Lock1 = (bool)cn.EstadoLockInverso(row.Lock1);
                list.U_IDA_Alicuota = row.U_IDA_Alicuota;

                newList.Add(list);

            }

            return newList;
        }

        private void imgleft_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (btnOk.Content.ToString() == "OK" || btnOk.Content.ToString() == "Crear")
            {
                var result= cn.FindPrevious(txtCodigo.Text);

                if (result.Item2 == null)
                {
                    listaResultado = result.Item1;

                    listCodigosFiscales = ConvertValidateErrorsCodigosFiscales(listaResultado);

                    GetCodigoFiscal(listCodigosFiscales);

                    var resultLines = cn.FindCodigosFiscalesLines(Code);

                    if (resultLines.Item2 == null)
                    {
                        ListaCodigoFiscalDetalle = resultLines.Item1;

                        listCodigosFiscales = ConvertDataTable<ValidateErrorsCodigosFiscales>(ListaCodigoFiscalDetalle);

                        GetCodigosFiscalLines(listCodigosFiscales);

                        btnOk.Content = "OK";
                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + resultLines.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                    }
                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                }

                
            }
            else if (btnOk.Content.ToString() == "Actualizar")
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("¿Desea salir sin guardar cambios?", "Codigos Fiscales", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    btnOk.Content = "OK";
                }
            }
        }

        private void imgInicio_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (btnOk.Content.ToString() == "OK" || btnOk.Content.ToString() == "Crear")
            {
                var result= cn.FindFirst();

                if (result.Item2 == null)
                {
                    listaResultado = result.Item1;

                    listCodigosFiscales = ConvertValidateErrorsCodigosFiscales(listaResultado);

                    GetCodigoFiscal(listCodigosFiscales);

                    var resultLines = cn.FindCodigosFiscalesLines(Code);

                    if (resultLines.Item2 == null)
                    {
                        ListaCodigoFiscalDetalle = resultLines.Item1;

                        listCodigosFiscales = ConvertDataTable<ValidateErrorsCodigosFiscales>(ListaCodigoFiscalDetalle);

                        GetCodigosFiscalLines(listCodigosFiscales);

                        btnOk.Content = "OK";
                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + resultLines.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                    }

                   
                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                }

                
            }
            else if (btnOk.Content.ToString() == "Actualizar")
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("¿Desea salir sin guardar cambios?", "Codigos Fiscales", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    btnOk.Content = "OK";
                }
            }
        }

        private void GetCodigosFiscalLines(List<ValidateErrorsCodigosFiscales> dt)
        {

            dgCodigosFiscales.ItemsSource = dt;

            dgCodigosFiscales.CanUserAddRows = false;

            dgCodigosFiscales.CanUserDeleteRows = false;

            dgCodigosFiscales.CanUserSortColumns = false;
        }

        private void imgBuscar_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            btnOk.Content = "Buscar";

            LimpiarCampos();

            LimpiarDatagrid();

            EstablecerFondo();

            txtCodigo.IsReadOnly = false;

        }

        private void LimpiarDatagrid()
        {
            dgCodigosFiscales.ItemsSource = dt.DefaultView;

            dgCodigosFiscales.IsReadOnly = true;

            dgCodigosFiscales.IsEnabled = true;
        }

        private void EstablecerFondo()
        {
            txtCodigo.Background = Brushes.LightBlue;

            txtNombre.Background = Brushes.LightBlue;

            txtTipoImpositivo.Background = Brushes.LightBlue;

            cbxCompras.Background = Brushes.LightBlue;

            cbxInactivo.Background = Brushes.LightBlue;

            cbxVentas.Background = Brushes.LightBlue;

            cbAlicuota.Background = Brushes.LightBlue;
        }

        private void ReestablecerFondo()
        {         

            txtCodigo.Background = Brushes.White;

            txtNombre.Background = Brushes.White;

            txtTipoImpositivo.Background = Brushes.LightGray;           

            cbAlicuota.Background = Brushes.White;
        }

        private void LimpiarCampos()
        {
            txtCodigo.Text= "";

            txtNombre.Text = "";

            txtTipoImpositivo.Text = "";

            cbxCompras.IsChecked = false;

            cbxInactivo.IsChecked = false;

            cbxVentas.IsChecked = false;

            cbAlicuota.Text = "";
        }

        private void GetCodigoFiscal(List<ValidateErrorsCodigosFiscales> listaCodigosFiscales)
        {
            foreach (ValidateErrorsCodigosFiscales codigosFiscales in listaCodigosFiscales)
            {
                Code= codigosFiscales.Code;
                txtCodigo.Text = codigosFiscales.Code;
                txtNombre.Text = codigosFiscales.Name;
                txtTipoImpositivo.Text = codigosFiscales.Rate;
                cbAlicuota.SelectedValue = codigosFiscales.U_IDA_Alicuota;
                cbxCompras.IsChecked = codigosFiscales.ValidForAP;
                cbxVentas.IsChecked = codigosFiscales.ValidForAR;
                cbxInactivo.IsChecked = codigosFiscales.Lock1;              

         
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CreateTableCodigosFiscales();

            InicializacionBasica();
        }

        private void CreateTableCodigosFiscales()
        {
            
            dt.Columns.Add("Code");
            dt.Columns.Add("Name");
            dt.Columns.Add("Rate");
            dt.Columns.Add("SalesTax");
            dt.Columns.Add("PurchTax");

            dt.NewRow();

            dgCodigosFiscales.ItemsSource = dt.DefaultView;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            List<Entidades.CodigosFiscales> listaCodigosFiscales = new List<Entidades.CodigosFiscales>();

            Entidades.CodigosFiscales codigosFiscales = new Entidades.CodigosFiscales();

            List<Entidades.CodigosFiscalesLine> listaCodigosFiscalesLines = new List<Entidades.CodigosFiscalesLine>();

            switch (btnOk.Content.ToString())
            {
                case "OK":

                    this.Hide();

                    break;

                case "Buscar":                 

                    codigosFiscales.Code = txtCodigo.Text;
                    codigosFiscales.Name = txtNombre.Text;
                    codigosFiscales.U_IDA_Alicuota = cbAlicuota.Text.ToString();
                    codigosFiscales.ValidForAP = cn.EstadoCompras(cbxCompras.IsChecked);
                    codigosFiscales.ValidForAR = cn.EstadoVentas(cbxVentas.IsChecked);
                    codigosFiscales.Lock1 = cn.EstadoLock(cbxInactivo.IsChecked);

                    listaCodigosFiscales.Add(codigosFiscales);

                    var result = cn.ConsultaCodigosFiscales(listaCodigosFiscales);

                    if (result.Item2 == null)
                    {
                        RecorreListaCodigosFiscales(result.Item1);
                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                    }

                    break;

                case "Crear":

                    if (IsValid(dgCodigosFiscales) == true && IsValid(codigosFiscalesWindow) == true)
                    {
                        codigosFiscales.Code = txtCodigo.Text;
                        codigosFiscales.Name = txtNombre.Text;
                        codigosFiscales.Rate = Convert.ToDecimal(txtTipoImpositivo.Text);
                        codigosFiscales.U_IDA_Alicuota = cbAlicuota.Text.ToString();
                        codigosFiscales.ValidForAP = cn.EstadoCompras(cbxCompras.IsChecked);
                        codigosFiscales.ValidForAR = cn.EstadoVentas(cbxVentas.IsChecked);
                        codigosFiscales.Lock1 = cn.EstadoLock(cbxInactivo.IsChecked);
                        codigosFiscales.UserSign = Properties.Settings.Default.Usuario;
                        codigosFiscales.UpdateDate = Entidades.fechaActual.GetFechaActual();
                        codigosFiscales.Freight = 'Y';

                        listaCodigosFiscales.Add(codigosFiscales);

                        var result1 = cn.InsertaCodigosFiscales(listaCodigosFiscales);

                        if (result1.Item1 == 1)
                        {
                            int i = 0;

                            foreach (ValidateErrorsCodigosFiscales row in listCodigosFiscales)
                            {
                                CodigosFiscalesLine codigoFiscalLine = new CodigosFiscalesLine();

                                codigoFiscalLine.STCCode = txtCodigo.Text;
                                codigoFiscalLine.Line_ID = 0;
                                codigoFiscalLine.STACode = row.Code_OSTA;
                                codigoFiscalLine.EfctivRate =ConvertDecimalTwoPlaces(txtTipoImpositivo.Text);

                                listaCodigosFiscalesLines.Add(codigoFiscalLine);

                                i++;

                            }

                            var result4 = cn.InsertCodigosFiscalesLines(listaCodigosFiscalesLines);

                            if (i == result4.Item1)
                            {
                                btnOk.Content = "OK";

                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Operacion realizada exitosamente", Brushes.LightGreen, Brushes.Black, "001-interface.png");
                            }
                            else
                            {
                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al actualizar el asiento: " + result4.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
                            }

                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el codigo fiscal: " + result1.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");

                        }
                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error. Existe algun elemento del formulario con erorres: ", Brushes.Red, Brushes.White, "003-interface-2.png");
                    }




                    break;

                case "Actualizar":

                    if (IsValid(dgCodigosFiscales) == true && IsValid(codigosFiscalesWindow) == true)
                    {
                        codigosFiscales.Code = txtCodigo.Text;
                        codigosFiscales.Name = txtNombre.Text;
                        codigosFiscales.Rate = Convert.ToDecimal(txtTipoImpositivo.Text);
                        codigosFiscales.U_IDA_Alicuota = cbAlicuota.Text.ToString();
                        codigosFiscales.ValidForAP = cn.EstadoCompras(cbxCompras.IsChecked);
                        codigosFiscales.ValidForAR = cn.EstadoVentas(cbxVentas.IsChecked);
                        codigosFiscales.Lock1 = cn.EstadoLock(cbxInactivo.IsChecked);
                        codigosFiscales.UserSign = Properties.Settings.Default.Usuario;
                        codigosFiscales.UpdateDate = Entidades.fechaActual.GetFechaActual();

                        listaCodigosFiscales.Add(codigosFiscales);

                        var result2 = cn.ActualizaCodigosFiscales(listaCodigosFiscales);

                        if (result2.Item1 == 1)
                        {
                            int i = 0;

                            foreach (ValidateErrorsCodigosFiscales row in listCodigosFiscales)
                            {
                                CodigosFiscalesLine codigoFiscalLine = new CodigosFiscalesLine();

                                codigoFiscalLine.STCCode = txtCodigo.Text;
                                codigoFiscalLine.Line_ID = 0;
                                codigoFiscalLine.STACode = row.Code_OSTA;
                                codigoFiscalLine.EfctivRate = ConvertDecimalTwoPlaces(txtTipoImpositivo.Text);

                                listaCodigosFiscalesLines.Add(codigoFiscalLine);

                                i++;

                            }

                            var result4 = cn.UpdateCodigosFiscalesLines(listaCodigosFiscalesLines);

                            if (i == result4.Item1)
                            {
                                btnOk.Content = "OK";

                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Operacion realizada exitosamente", Brushes.LightGreen, Brushes.Black, "001-interface.png");
                            }
                            else
                            {
                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al actualizar el asiento: " + result4.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
                            }


                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al actualizar el codigo fiscal: " + result2.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");

                        }
                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error. Existe algun elemento del formulario con erorres: ", Brushes.Red, Brushes.White, "003-interface-2.png");
                    }

                    break;
            }

        }

        private void RecorreListaCodigosFiscales(List<Entidades.CodigosFiscales> newListaCodigosFiscales)
        {
            List<ValidateErrorsCodigosFiscales> list = new List<ValidateErrorsCodigosFiscales>();

            if (newListaCodigosFiscales.Count == 1)
            {
                list = ConvertValidateErrorsCodigosFiscales(newListaCodigosFiscales);

                GetCodigoFiscal(list);

                var resultLines = cn.FindCodigosFiscalesLines(Code);

                if (resultLines.Item2 == null)
                {
                    ListaCodigoFiscalDetalle = resultLines.Item1;

                    listCodigosFiscales = ConvertDataTable<ValidateErrorsCodigosFiscales>(ListaCodigoFiscalDetalle);

                    GetCodigosFiscalLines(listCodigosFiscales);

                    btnOk.Content = "OK";
                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + resultLines.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                }

               
            }
            else if(newListaCodigosFiscales.Count == 0)
            {                
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se encontro ningun resultado con los parametros establecidos", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");

                LimpiarCampos();

                btnOk.Content = "OK";
            }
            else if (newListaCodigosFiscales.Count > 1)
            {
                ListBoxCodigosFiscales ventanaListBox = new ListBoxCodigosFiscales(newListaCodigosFiscales);

                ventanaListBox.ShowDialog();

                if (ventanaListBox.Visibility==Visibility.Hidden)
                {
                    if(ventanaListBox.GetListCodigosFiscales().Count == 0)
                    {                       
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No selecciono ningun elemento", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");

                        LimpiarCampos();
                    }
                    else
                    {
                        list = ConvertValidateErrorsCodigosFiscales(ventanaListBox.GetListCodigosFiscales());

                        GetCodigoFiscal(list);                      

                        var resultLines = cn.FindCodigosFiscalesLines(Code);

                        if (resultLines.Item2 == null)
                        {
                            ListaCodigoFiscalDetalle = resultLines.Item1;

                            listCodigosFiscales = ConvertDataTable<ValidateErrorsCodigosFiscales>(ListaCodigoFiscalDetalle);

                            GetCodigosFiscalLines(listCodigosFiscales);

                            btnOk.Content = "OK";

                            txtCodigo.IsReadOnly = true;
                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + resultLines.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                        }

                    }

                   
                   
                }

            }

            ReestablecerFondo();
        }

        private void TextBox_GotFocus_1(object sender, RoutedEventArgs e)
        {
            //dp.Background = Brushes.LightBlue;
            //bd.Background = Brushes.LightBlue;
            ////img.Visibility = Visibility.Visible;

       
        }

        private void txt_LostFocus(object sender, RoutedEventArgs e)
        {
            //dp.Background = Brushes.White;
            //bd.Background = Brushes.White;
            //img.Visibility = Visibility.Hidden;
        }

        private void img_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var row_list = GetDataGridRows(dgCodigosFiscales);

                DataRowView row_Selected = dgCodigosFiscales.SelectedItem as DataRowView;

                foreach (DataGridRow single_row in row_list)
                {
                    if (single_row.IsSelected == true)
                    {
                        TextBox txt = FindChild<TextBox>(single_row, "txt");

                        TextBlock txtName = FindChild<TextBlock>(single_row, "txtName");

                        TextBlock txtRate = FindChild<TextBlock>(single_row, "txtRate");

                        TextBlock txtPurchTax = FindChild<TextBlock>(single_row, "txtPurchTax");

                        TextBlock txtSalesTax = FindChild<TextBlock>(single_row, "txtSalesTax");                                              

                        var result = cn.ConsultaClasesImpuestos();

                        if (result.Item2 == null)
                        {
                            RecorreListaTaxCode(result.Item1, txt, txtName, txtRate, txtPurchTax,txtSalesTax, row_Selected);
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

        private void RecorreListaTaxCode(List<Entidades.ClasesImpuestos> listClasesImpuestos, TextBox txtTaxCode, TextBlock txtName, TextBlock txtRate, TextBlock txtPurchTax, TextBlock txtSalesTax, DataRowView row_Selected)
        {
            if (listClasesImpuestos.Count == 0)
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se encontro ningun resultado con los parametros establecidos", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");
            }

            else if (listClasesImpuestos.Count > 0)
            {
                ListaClasesImpuestos ventanaListaCodigos = new ListaClasesImpuestos(listClasesImpuestos);

                ventanaListaCodigos.ShowDialog();

                if (ventanaListaCodigos.Visibility == Visibility.Hidden)
                {
                    if (ventanaListaCodigos.GetListClasesImpuesto().Count == 0)
                    {

                    }
                    else
                    {
                        GetCodigos(ventanaListaCodigos.GetListClasesImpuesto(), txtTaxCode, txtName, txtRate, txtPurchTax, txtSalesTax, row_Selected);
                    }
                }
            }
        }

        private void GetCodigos(List<Entidades.ClasesImpuestos> listaCodigos, TextBox txtTaxCode, TextBlock txtName, TextBlock txtRate, TextBlock txtPurchTax, TextBlock txtSalesTax, DataRowView row_Selected)
        {
            Rate = 0;

            foreach (Entidades.ClasesImpuestos codigo in listaCodigos)
            {
                txtTaxCode.Text = codigo.Code;

                row_Selected["Name_OSTA"] = codigo.Name;

                txtName.Text = row_Selected["Name_OSTA"].ToString();

                row_Selected["Rate_OSTA"] = codigo.Rate;

                Rate = Rate + ConvertDecimalTwoPlaces(codigo.Rate);

                txtRate.Text = row_Selected["Rate_OSTA"].ToString();

                row_Selected["PurchTax"] = codigo.PurchTax;

                txtPurchTax.Text = row_Selected["PurchTax"].ToString();

                row_Selected["SalesTax"] = codigo.SalesTax;

                txtSalesTax.Text = row_Selected["SalesTax"].ToString();

            }

            txtTipoImpositivo.Text = Rate.ToString("N",nfi);

        }

        private void dgCodigosFiscales_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void txtCodigo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (btnOk.Content.ToString() == "OK")
            {
                btnOk.Content = "Actualizar";
            }
        }

        private void cbAlicuota_DropDownOpened(object sender, EventArgs e)
        {
            if (btnOk.Content.ToString() == "OK")
            {
                btnOk.Content = "Actualizar";
            }
        }

        private void cbxCompras_Click(object sender, RoutedEventArgs e)
        {
            if (btnOk.Content.ToString() == "OK")
            {
                btnOk.Content = "Actualizar";
            }
           
        }
              

        private void Deleted_Click(object sender, RoutedEventArgs e)
        {
            if (btnOk.Content.ToString() == "OK")
            {

                MessageBoxResult messageBoxResult = MessageBox.Show("¿Desea eliminar el codigo fiscal?", "Codigo Fiscal", MessageBoxButton.YesNo, MessageBoxImage.Warning);


                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    string codigoFiscal = txtCodigo.Text;

                    if (codigoFiscal != null)
                    {
                       
                        var result = cn.DeleteCodigoFiscal(codigoFiscal);

                        if (result.Item1 == 1)
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Codigo Fiscal : " + codigoFiscal + " se elimino correctamente", Brushes.LightGreen, Brushes.Black, "001-interface.png");

                            LimpiarCampos();
                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se puede eliminar el codigo fiscal : " + codigoFiscal + " porque se realizo una transaccion con el mismo: " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");

                        }
                    }

                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se selecciono codigo fiscal", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");
                    }

                }


            }
        }

        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            btnOk.Content = "Actualizar";
        }
    }
}
