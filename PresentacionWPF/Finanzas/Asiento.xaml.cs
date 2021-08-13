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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Entidades;
using Negocio;
using Vista;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para Asiento.xaml
    /// </summary>
    public partial class Asiento : Document
    {
        ControladorAsiento cn = new ControladorAsiento();

        DataTable dt=new DataTable();

        private int transId;

        private decimal rate;

        private decimal rateFC;
       
        private string selectedDate;

        private List<Entidades.AsientoCabecera> listaAsientoCabecera = new List<Entidades.AsientoCabecera>();

        private DataTable listaAsientoDetalle = new DataTable();
        public List<AsientoCabecera> ListaAsientoCabecera { get => listaAsientoCabecera; set => listaAsientoCabecera = value; }
        public DataTable ListaAsientoDetalle { get => listaAsientoDetalle; set => listaAsientoDetalle = value; }
        public int TransId { get => transId; set => transId = value; }
        public decimal Rate { get => rate; set => rate = value; }       
        public decimal RateFC { get => rateFC; set => rateFC = value; }
        public string SelectedDate { get => selectedDate; set => selectedDate = value; }

        public Asiento()
        {
            InitializeComponent();


        }

        public void DisableElements()
        {
            btnOk.Content = "OK";

            btnOk.IsEnabled = false;

            imgBuscar.IsEnabled = false;

            imgCrear.IsEnabled = false;

            imgFin.IsEnabled = false;

            imgInicio.IsEnabled = false;

            imgleft.IsEnabled = false;

            imgRight.IsEnabled = false;
        }

        public void EnabledElements()
        {
            btnOk.Content = "Buscar";

            btnOk.IsEnabled = true;

            imgBuscar.IsEnabled = true;

            imgCrear.IsEnabled = true;

            imgFin.IsEnabled = true;

            imgInicio.IsEnabled = true;

            imgleft.IsEnabled = true;

            imgRight.IsEnabled = true;
        }

        private void comboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (btnOk.Content.ToString() != "Buscar")
                App.comboBox_LostFocus(sender, e);
        }

        private void comboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (btnOk.Content.ToString() != "Buscar")
                App.comboBox_GotFocus(sender, e);
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (btnOk.Content.ToString() != "Buscar")
                App.textBox_LostFocus(sender, e);
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (btnOk.Content.ToString() != "Buscar")
                App.textBox_GotFocus(sender, e);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
          
            App.Window_Closing(sender, e);
        }

        private void btnWindow_Close(object sender, RoutedEventArgs e)
        {
          
            this.Hide();
        }

        private void imgCrear_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            btnOk.Content = "Crear";

            LimpiarCampos();

            EstablecerOrigen();

            ReestablecerFondo();

            EnabledDatepicker();
        }

        private void EstablecerOrigen()
        {
            txtOrigen.Text = "AS";

            dgAsiento.CanUserAddRows = true;

            dgAsiento.CanUserDeleteRows = true;

            dgAsiento.CanUserSortColumns = true;

            dpFechaContabilizacion.SelectedDate = fechaActual.GetFechaActual();

            dpFechaVencimiento.SelectedDate = fechaActual.GetFechaActual();

            dpFechaDocumento.SelectedDate = fechaActual.GetFechaActual();
            
            var result = cn.SelectTransId();

            if (result.Item2 == null)
            {
                txtNumero.Text = result.Item1.ToString();
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
            }

            var result1 = cn.SelectBaseRef();

            if (result1.Item2 == null)
            {
                txtNroOrigen.Text = result1.Item1.ToString();
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result1.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
            }

            
        }

        public void ReestablecerFondo()
        {
            txtNumero.Background = Brushes.LightGray;
            txtNumero.IsReadOnly = true; 
            txtNroOrigen.Background = Brushes.LightGray;
            txtNroOrigen.IsReadOnly = true;
            txtComentario.Background = Brushes.White;
            txtReferencia1.Background = Brushes.White;
            txtReferencia2.Background = Brushes.White;
            dpFechaContabilizacion.Background = Brushes.White;
            dpFechaDocumento.Background = Brushes.White;
            dpFechaVencimiento.Background = Brushes.White;
            ReestablecerDatetime(dpFechaContabilizacion);
            ReestablecerDatetime(dpFechaDocumento);
            ReestablecerDatetime(dpFechaVencimiento);
        }

        private void LimpiarCampos()
        {
           
            txtNroOrigen.Text = "";
            txtComentario.Text = "";
            txtReferencia1.Text = "";
            txtReferencia2.Text = "";            
            txtOrigen.Text = "";

            dt.Rows.Clear();
           
            dgAsiento.ItemsSource = dt.DefaultView;


        }

        private void imgFin_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (btnOk.Content.ToString() == "OK" || btnOk.Content.ToString() == "Crear" || btnOk.Content.ToString() == "Buscar")
            {
                ReestablecerFondo();

                var result= cn.FindLastJournalEntry();

                if (result.Item2 == null)
                {
                    ListaAsientoCabecera = result.Item1;

                    GetJournalEntry(ListaAsientoCabecera);

                    var result1= cn.FindJournalEntryLines(TransId);

                    if (result1.Item2 == null)
                    {
                        ListaAsientoDetalle = result1.Item1;

                        GetJournalEntryLines(ListaAsientoDetalle);

                        btnOk.Content = "OK";
                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result1.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                    }

                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                }
            }

            else if (btnOk.Content.ToString() == "Actualizar")
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("¿Desea salir sin guardar cambios?", "Journal  Entry", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    btnOk.Content = "OK";

                }
            }
        }

        public void GetJournalEntryLines(DataTable listJournalEntryLines)
        {
           // var result = listJournalEntryLines.AsEnumerable()
           //.GroupBy(row => new
           //{
           //    LineMemo = row.Field<string>("LineMemo"),
           //    ShortName = row.Field<string>("ShortName"),
           //    Account = row.Field<string>("Account"),
           //    AcctName = row.Field<string>("AcctName"),
           //    TransID = row.Field<int>("TransId"),
           //    LineID = row.Field<int>("Line_ID"),

           //})
           //.Select(g =>
           //{
           //    var row = g.First();
           //    row.SetField("Debit", g.Sum(r => r.Field<decimal>("Debit")));
           //    row.SetField("Credit", g.Sum(r => r.Field<decimal>("Credit")));
           //    row.SetField("SYSDeb", g.Sum(r => r.Field<decimal>("SYSDeb")));
           //    row.SetField("SYSCred", g.Sum(r => r.Field<decimal>("SYSCred")));
           //    row.SetField("FCDebit", g.Sum(r => r.Field<decimal>("FCDebit")));
           //    row.SetField("FCCredit", g.Sum(r => r.Field<decimal>("FCCredit")));
           //    row.SetField("LineMemo", g.Select(r => r.Field<string>("LineMemo")));
           //    row.SetField("ShortName", g.Select(r => r.Field<string>("ShortName")));
           //    row.SetField("Account", g.Select(r => r.Field<string>("Account")));
           //    row.SetField("AcctName", g.Select(r => r.Field<string>("AcctName")));
           //    row.SetField("TransId", g.Select(r => r.Field<int>("TransId")).FirstOrDefault());
           //    row.SetField("Line_ID", g.Select(r => r.Field<int>("Line_ID")).FirstOrDefault());

           //    return row;
           //});


           // var resultTable = result.CopyToDataTable();

        



           // //resultTable = ChangeTypeColumn(resultTable);

           dt = AddCurrencyCode(listJournalEntryLines);

            dgAsiento.ItemsSource = dt.DefaultView;

            dgAsiento.CanUserAddRows = false;

            dgAsiento.CanUserDeleteRows = false;

            dgAsiento.CanUserSortColumns = false;
        }

        //public DataTable ChangeTypeColumn(DataTable dataTable)
        //{
        //    DataTable dtCloned = dataTable.Clone();

        //    int i = 0;

        //    foreach (DataColumn column in dataTable.Columns)
        //    {
        //        dtCloned.Columns[i].DataType = typeof(string);

        //        i++;
        //    }

           
        //    foreach (DataRow row in dataTable.Rows)
        //    {
        //        dtCloned.ImportRow(row);
        //    }

        //    return dtCloned;
        //}

        private DataTable AddCurrencyCode(DataTable dt)
        {

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {

                    if (column.ToString() == "Debit" && ConvertDecimalTwoPlaces(row["Debit"]) != 0)
                    {
                        row["Debit"] = Properties.Settings.Default.MainCurrency + " " + ConvertDecimalTwoPlaces(row["Debit"]);

                    }

                    else if (column.ToString() == "Credit" && ConvertDecimalTwoPlaces(row["Credit"]) != 0)
                    {
                        row["Credit"] = Properties.Settings.Default.MainCurrency + " " + ConvertDecimalTwoPlaces(row["Credit"]);
                    }

                    else if (column.ToString() == "SYSDeb" && ConvertDecimalTwoPlaces(row["SYSDeb"]) != 0)
                    {
                        row["SYSDeb"] = Properties.Settings.Default.SysCurrency + " " + ConvertDecimalTwoPlaces(row["SYSDeb"]);
                    }

                    else if (column.ToString() == "SYSCred" && ConvertDecimalTwoPlaces(row["SYSCred"]) != 0)
                    {
                        row["SYSCred"] = Properties.Settings.Default.SysCurrency + " " + ConvertDecimalTwoPlaces(row["SYSCred"]);
                    }

                    else if (column.ToString() == "FCDebit" && ConvertDecimalTwoPlaces(row["FCDebit"]) != 0)
                    {
                        row["FCDebit"] = row["FCCurrency"] + " " + ConvertDecimalTwoPlaces(row["FCDebit"]);
                    }

                    else if (column.ToString() == "FCCredit" && ConvertDecimalTwoPlaces(row["FCCredit"]) != 0)
                    {
                        row["FCCredit"] = row["FCCurrency"] + " " + ConvertDecimalTwoPlaces(row["FCCredit"]);
                    }

                    else if (column.ToString() == "Credit" && ConvertDecimalTwoPlaces(row["Credit"]) == 0)
                    {
                        row["Credit"] = "";
                    }

                    else if (column.ToString() == "SYSDeb" && ConvertDecimalTwoPlaces(row["SYSDeb"]) == 0)
                    {
                        row["SYSDeb"] = "";
                    }

                    else if (column.ToString() == "SYSCred" && ConvertDecimalTwoPlaces(row["SYSCred"]) == 0)
                    {
                        row["SYSCred"] = "";
                    }

                    else if (column.ToString() == "FCDebit" && ConvertDecimalTwoPlaces(row["FCDebit"]) == 0)
                    {
                        row["FCDebit"] = "";
                    }

                    else if (column.ToString() == "FCCredit" && ConvertDecimalTwoPlaces(row["FCCredit"]) == 0)
                    {
                        row["FCCredit"] = "";
                    }

                    else if (column.ToString() == "Debit" && ConvertDecimalTwoPlaces(row["Debit"]) == 0)
                    {
                        row["Debit"] = "";

                    }
                    else if (column.ToString() == "ShortName")
                    {
                        row["AcctName"] = cn.FindAcctName(row["ShortName"].ToString());

                    }

                }

            }

            return dt;
        }

        public void GetJournalEntry(List<AsientoCabecera> listaAsientoCabecera)
        {
            foreach (AsientoCabecera journalEntry in listaAsientoCabecera)
            {
                TransId= journalEntry.TransId;
                txtNumero.Text = journalEntry.TransId.ToString();
                dpFechaContabilizacion.SelectedDate = journalEntry.RefDate;
                dpFechaDocumento.SelectedDate = journalEntry.TaxDate;
                dpFechaVencimiento.SelectedDate = journalEntry.DueDate;
                txtComentario.Text = journalEntry.Memo;
                txtNroOrigen.Text =journalEntry.BaseRef.ToString();
                txtReferencia1.Text = journalEntry.Ref1;
                txtReferencia2.Text = journalEntry.Ref2;
                txtOrigen.Text =cn.GetTransType(Convert.ToInt32(journalEntry.TransType));
            }

            ReadOnlyFieldJE();

        }

        private void ReadOnlyFieldJE()
        {
            txtNumero.IsReadOnly = true;
            txtOrigen.IsReadOnly = true;
            txtNroOrigen.IsReadOnly = true;
            dpFechaContabilizacion.IsEnabled = false;            
            ReadOnlyDatetime(dpFechaContabilizacion);
            ReadOnlyDatetime(dpFechaVencimiento);
            ReadOnlyDatetime(dpFechaDocumento);
            txtNumero.Background = Brushes.LightGray;
            txtOrigen.Background = Brushes.LightGray;
            txtNroOrigen.Background = Brushes.LightGray;
        }

        private void EnabledDatepicker()
        {
            dpFechaContabilizacion.IsEnabled = true;
            
        }

        private void imgRight_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (btnOk.Content.ToString() == "OK" || btnOk.Content.ToString() == "Crear" || btnOk.Content.ToString() == "Buscar")
            {
                ReestablecerFondo();

                var result= cn.FindNextJournalEntry(txtNumero.Text);

                if (result.Item2 == null)
                {
                    ListaAsientoCabecera = result.Item1;

                    GetJournalEntry(ListaAsientoCabecera);

                    var result1 = cn.FindJournalEntryLines(TransId);

                    if (result1.Item2 == null)
                    {
                        ListaAsientoDetalle = result1.Item1;

                        GetJournalEntryLines(ListaAsientoDetalle);

                        btnOk.Content = "OK";

                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result1.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                    }
                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                }

            }

            else if (btnOk.Content.ToString() == "Actualizar")
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("¿Desea salir sin guardar cambios?", "Journal Entry", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    btnOk.Content = "OK";

                }
            }
        }

        private void imgleft_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (btnOk.Content.ToString() == "OK" || btnOk.Content.ToString() == "Crear" || btnOk.Content.ToString() == "Buscar")
            {
                ReestablecerFondo();

                var result= cn.FindPreviousJournalEntry(txtNumero.Text);

                if (result.Item2 == null)
                {
                    ListaAsientoCabecera = result.Item1;

                    GetJournalEntry(ListaAsientoCabecera);

                    var result1= cn.FindJournalEntryLines(TransId);

                    if (result1.Item2 == null)
                    {
                        ListaAsientoDetalle = result1.Item1;

                        GetJournalEntryLines(ListaAsientoDetalle);

                        btnOk.Content = "OK";

                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result1.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                    }
                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                }




            }

            else if (btnOk.Content.ToString() == "Actualizar")
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("¿Desea salir sin guardar cambios?", "Journal Entry", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    btnOk.Content = "OK";

                }
            }
        }

        private void imgInicio_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (btnOk.Content.ToString() == "OK" || btnOk.Content.ToString() == "Crear" || btnOk.Content.ToString() == "Buscar")
            {
                ReestablecerFondo();

                var result= cn.FindFirstJournalEntry();

                if (result.Item2 == null)
                {
                    ListaAsientoCabecera = result.Item1;

                    GetJournalEntry(ListaAsientoCabecera);

                    var result1= cn.FindJournalEntryLines(TransId);

                    if (result1.Item2 == null)
                    {
                        ListaAsientoDetalle = result1.Item1;

                        GetJournalEntryLines(ListaAsientoDetalle);

                        btnOk.Content = "OK";
                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result1.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                    }
                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                }



            }

            else if (btnOk.Content.ToString() == "Actualizar")
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("¿Desea salir sin guardar cambios?", "Journal  Entry", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    btnOk.Content = "OK";

                }
            }
        }

        public void LoadedWindow()
        {
            LoadCurrency();

            InicializacionBasica();

            EnabledElements();
        }

        private void imgBuscar_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            InicializacionBasica();
        }

        public void InicializacionBasica()
        {
            btnOk.Content = "Buscar";

            LimpiarCampos();

            EstablecerFondo();

            EnabledDatepicker();

            dgAsiento.CanUserAddRows = true;

            dgAsiento.CanUserDeleteRows = true;

            dgAsiento.CanUserSortColumns = true;

            dt.Rows.Clear();

            dgAsiento.ItemsSource = dt.Rows;
        }

        private void EstablecerFondo()
        {
            txtNumero.Background = Brushes.LightBlue;
            txtNumero.IsReadOnly = false;
            txtComentario.Background = Brushes.LightBlue;
            txtReferencia1.Background = Brushes.LightBlue;
            txtReferencia2.Background = Brushes.LightBlue;
            dpFechaContabilizacion.Background = Brushes.LightBlue;
            dpFechaDocumento.Background = Brushes.LightBlue;
            dpFechaVencimiento.Background = Brushes.LightBlue;
            establecerDatetime(dpFechaContabilizacion);
            establecerDatetime(dpFechaDocumento);
            establecerDatetime(dpFechaVencimiento);
        }

        public void establecerDatetime(DatePicker dpk)
        {
            TextBox tb = (TextBox)dpk.Template.FindName("PART_TextBox", dpk);

            tb.BorderThickness = new Thickness(1);
            tb.Background= Brushes.LightBlue;
            tb.BorderBrush = Brushes.Gray;

        }

        public void ReestablecerDatetime(DatePicker dpk)
        {
            TextBox tb = (TextBox)dpk.Template.FindName("PART_TextBox", dpk);
                       
            tb.Background = Brushes.White;
           
        }

        public void ReadOnlyDatetime(DatePicker dpk)
        {
            TextBox tb = (TextBox)dpk.Template.FindName("PART_TextBox", dpk);

            tb.IsReadOnly = true;
            tb.Background = Brushes.LightGray;

        }

       
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            List<AsientoCabecera> listaJournalEntry = new List<AsientoCabecera>();

            AsientoCabecera journalEntry = new AsientoCabecera();

            List<AsientoDetalle> listaJournalEntryLines = new List<AsientoDetalle>();            

            switch (btnOk.Content.ToString())
            {
                case "OK":

                    this.Hide();

                    break;

                case "Buscar":

                    if (String.IsNullOrWhiteSpace(txtNumero.Text)==false)
                    {
                        journalEntry.TransId = Convert.ToInt32(txtNumero.Text);
                    }
                    else
                    {
                        journalEntry.TransId = 0;
                    }

                    journalEntry.RefDate = dpFechaContabilizacion.SelectedDate;
                    journalEntry.TaxDate = dpFechaDocumento.SelectedDate;
                    journalEntry.DueDate = dpFechaVencimiento.SelectedDate;
                    journalEntry.Memo = txtComentario.Text;

                    try
                    {
                        journalEntry.BaseRef = Convert.ToInt32(txtNroOrigen.Text);
                    }
                    catch(Exception ex)
                    {
                        journalEntry.BaseRef = 0;
                    }
                    
                    journalEntry.Ref1 = txtReferencia1.Text;
                    journalEntry.Ref2 = txtReferencia2.Text;                   

                    listaJournalEntry.Add(journalEntry);

                    var result = cn.ConsultaJournalEntry(listaJournalEntry);

                    if (result.Item2 == null)
                    {
                        RecorreListaJornalEntry(result.Item1);
                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                    }

                    break;

                case "Crear":                   

                    bool sw=cn.VerificaDebeHaber(dt);

                    if (sw == true)
                    {

                        journalEntry.TransId = Convert.ToInt32(txtNumero.Text);
                        journalEntry.RefDate = dpFechaContabilizacion.SelectedDate;
                        journalEntry.TaxDate = dpFechaDocumento.SelectedDate;
                        journalEntry.DueDate = dpFechaVencimiento.SelectedDate;
                        journalEntry.Memo = txtComentario.Text;
                        var baseRef = cn.GetBaseRef("30");

                        journalEntry.BaseRef = baseRef.Item1;
                        journalEntry.Ref1 = txtReferencia1.Text;
                        journalEntry.Ref2 = txtReferencia2.Text;
                        journalEntry.UserSign = Properties.Settings.Default.Usuario;
                        journalEntry.UpdateDate = fechaActual.GetFechaActual();
                        var result11= cn.GetPeriodCode(journalEntry.RefDate);
                        journalEntry.FinncPriod = result11.Item1;
                        journalEntry.LocTotal = cn.CalculateLocTotal(dt);
                        journalEntry.SysTotal = cn.CalculateSysTotal(dt);
                        journalEntry.FcTotal = cn.CalculateFCTotal(dt); //
                        journalEntry.TransType = cn.GetTransType(txtOrigen.Text);


                        string currency = cn.GetFC(dt);

                        if (String.IsNullOrWhiteSpace(currency))
                        {
                            journalEntry.TransCurr = Properties.Settings.Default.MainCurrency;
                        }
                        else
                        {
                            journalEntry.TransCurr = currency;
                        }

                        listaJournalEntry.Add(journalEntry);

                        var result1 = cn.InsertJournalEntry(listaJournalEntry);

                        if (result1.Item1 == 1)
                        {
                            int i = 0;

                            dt = CalculateContraAct(dt);

                            foreach (DataRow row in dt.Rows)
                            {
                                AsientoDetalle journalEntryLines = new AsientoDetalle();

                                journalEntryLines.TransId = journalEntry.TransId;
                                journalEntryLines.Line_ID = i;
                                journalEntryLines.RefDate = journalEntry.RefDate;
                                journalEntryLines.DueDate = journalEntry.DueDate;
                                journalEntryLines.TaxDate = journalEntry.TaxDate;                               
                                journalEntryLines.Account = row["Account"].ToString();
                                journalEntryLines.ShortName = row["ShortName"].ToString();
                                journalEntryLines.LineMemo = row["LineMemo"].ToString();
                                journalEntryLines.ContraAct = row["ContraAct"].ToString();
                                journalEntryLines.TransType = journalEntry.TransType;
                                journalEntryLines.Debit = cn.RemoveCurrency(row["Debit"].ToString());
                                journalEntryLines.Credit = cn.RemoveCurrency(row["Credit"].ToString());
                                journalEntryLines.FCDebit = cn.RemoveCurrency(row["FCDebit"].ToString());
                                journalEntryLines.FCCredit = cn.RemoveCurrency(row["FCCredit"].ToString());
                                journalEntryLines.SysCred =cn.RemoveCurrency(row["SYSCred"].ToString());
                                journalEntryLines.SysDeb = cn.RemoveCurrency(row["SYSDeb"].ToString());
                                journalEntryLines.BalDueDeb = journalEntryLines.Debit;
                                journalEntryLines.BalDueCred = journalEntryLines.Credit;
                                journalEntryLines.BalFcDeb = journalEntryLines.FCDebit;
                                journalEntryLines.BalFcCred = journalEntryLines.FCCredit;
                                journalEntryLines.BalScCred = journalEntryLines.SysCred;
                                journalEntryLines.BalScDeb = journalEntryLines.SysDeb;
                                journalEntryLines.UserSign = Properties.Settings.Default.Usuario;
                                journalEntryLines.FinncPriod = journalEntry.FinncPriod;
                                journalEntryLines.FCCurrency = journalEntry.TransCurr;
                                journalEntryLines.DataSource = 'N';

                                listaJournalEntryLines.Add(journalEntryLines);

                                i++;

                            }

                            var result2 = cn.InsertJournalEntryLines(listaJournalEntryLines);

                            if (i == result2.Item1)
                            {
                                cn.UpdateCreditDebitAccount(dt);

                                btnOk.Content = "OK";

                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Operacion realizada exitosamente", Brushes.LightGreen, Brushes.Black, "001-interface.png");
                            }
                            else
                            {
                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento " + result2.Item2 , Brushes.Red, Brushes.Black, "003-interface-2.png");                                
                            }


                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error en la creacion del asiento " + journalEntry.TransId + " " + result1.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
                           
                        }
                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Transaccion no ponderada", Brushes.Red, Brushes.Black, "003-interface-2.png");
                    }

                    

                    break;

                case "Actualizar":

                    bool sw1 = cn.VerificaDebeHaber(dt);

                    if (sw1 == true)
                    {

                        journalEntry.TransId = Convert.ToInt32(txtNumero.Text);                        
                        journalEntry.TaxDate = dpFechaDocumento.SelectedDate;
                        journalEntry.DueDate = dpFechaVencimiento.SelectedDate;
                        journalEntry.Memo = txtComentario.Text;                        
                        journalEntry.Ref1 = txtReferencia1.Text;
                        journalEntry.Ref2 = txtReferencia2.Text;
                        journalEntry.UserSign = Properties.Settings.Default.Usuario;
                        journalEntry.UpdateDate = fechaActual.GetFechaActual();
                       
                        listaJournalEntry.Add(journalEntry);

                        var result3 = cn.UpdateJournalEntry(listaJournalEntry);

                        if (result3.Item1 == 1)
                        {
                            int i = 0;

                            foreach (DataRow row in dt.Rows)
                            {
                                AsientoDetalle journalEntryLines = new AsientoDetalle();

                                journalEntryLines.TransId = journalEntry.TransId;
                                journalEntryLines.Line_ID = Convert.ToInt32(row["Line_ID"]);
                                journalEntryLines.DueDate = journalEntry.DueDate;
                                journalEntryLines.TaxDate = journalEntry.TaxDate;
                                journalEntryLines.LineMemo = row["LineMemo"].ToString();   
                                journalEntryLines.UserSign = Properties.Settings.Default.Usuario;

                                listaJournalEntryLines.Add(journalEntryLines);

                                i++;

                            }

                            var result4 = cn.UpdateJournalEntryLines(listaJournalEntryLines);

                            if (i == result4.Item1)
                            {
                                
                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Operacion realizada exitosamente", Brushes.LightGreen, Brushes.Black, "001-interface.png");
                            }
                            else
                            {
                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al actualizar el asiento: " + result4.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
                            }

                            btnOk.Content = "OK";

                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error en la actualizacion del asiento " + journalEntry.TransId + " " + result3.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
                            
                        }
                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Transaccion no ponderada", Brushes.Red, Brushes.Black, "003-interface-2.png");
                    }

                    

                    break;
            }

        }

        private DataTable CalculateContraAct(DataTable dt)
        {
            DataTable newDt = dt.Copy();

            int con = 0;

            int rows = newDt.Rows.Count;

            for (int i = 0; i < newDt.Rows.Count; i++)
            {
                DataRow row = newDt.Rows[i];

                if (i == (0 + con))
                {
                    if(con+1== rows) //es la ultima linea
                    {
                        row["ContraAct"] = newDt.Rows[(0 + con) -1]["ShortName"].ToString();
                    }
                    else
                    {
                        row["ContraAct"] = newDt.Rows[(0 + con) + 1]["ShortName"].ToString();
                    }                    

                }
                else if (i == (1 + con))
                {
                    row["ContraAct"] = newDt.Rows[(1 + con) - 1]["ShortName"].ToString();

                }
                else if (i == (2 + con))
                {
                    row["ContraAct"] = newDt.Rows[(2 + con) - 2]["ShortName"].ToString();
                }
                else if (i == (3 + con))
                {
                    row["ContraAct"] = newDt.Rows[(3 + con) - 2]["ShortName"].ToString();
                }
                else if (i == (4 + con))
                {
                    row["ContraAct"] = newDt.Rows[(4 + con) - 3]["ShortName"].ToString();
                }
                else if (i == (5 + con))
                {
                    row["ContraAct"] = newDt.Rows[(5 + con) - 1]["ShortName"].ToString();
                }


                if (i % 5 == 0 && i!=0)
                {
                    con = con + 6;
                }



            }

            return newDt;

          
        }

        private void RecorreListaJornalEntry(List<AsientoCabecera> newListJournalEntry)
        {
            if (newListJournalEntry.Count == 1)
            {
                GetJournalEntry(newListJournalEntry);

                var result = cn.FindJournalEntryLines(TransId);

                if (result.Item2 == null)
                {
                    GetJournalEntryLines(result.Item1);

                    btnOk.Content = "OK";
                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                }

                
            }
            else if (newListJournalEntry.Count == 0)
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se encontro ningun resultado con los parametros establecidos", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");

                LimpiarCampos();

                btnOk.Content = "OK";
            }

            else if (newListJournalEntry.Count > 1)
            {
                ListaJournalEntry ventanaListBox = new ListaJournalEntry(newListJournalEntry);

                ventanaListBox.ShowDialog();

                if (ventanaListBox.Visibility == Visibility.Hidden)
                {
                    if (ventanaListBox.GetListJournalEntry().Count == 0)
                    {                        
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No selecciono ningun elemento", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");

                        LimpiarCampos();
                    }
                    else
                    {

                        GetJournalEntry(ventanaListBox.GetListJournalEntry());

                        var result1 = cn.FindJournalEntryLines(TransId);

                        if (result1.Item2 == null)
                        {
                            GetJournalEntryLines(result1.Item1);

                            btnOk.Content = "OK";
                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result1.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                        }

                      
                    }

                    btnOk.Content = "OK";
                }


            }

            ReestablecerFondo();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCurrency();

            dt.Columns.Add("ShortName");
            dt.Columns.Add("AcctName");
            dt.Columns.Add("Account");
            dt.Columns.Add("FCDebit");
            dt.Columns.Add("FCCredit");
            dt.Columns.Add("ContraAct");
            dt.Columns.Add("Debit");
            dt.Columns.Add("Credit");
            dt.Columns.Add("SYSDeb");
            dt.Columns.Add("SYSCred");
            dt.Columns.Add("LineMemo");           

            dt.NewRow();

            dgAsiento.ItemsSource = dt.DefaultView;

            LimpiarCampos();

            EstablecerOrigen();

            ReestablecerFondo();

            dpFechaContabilizacion.SelectedDate = fechaActual.GetFechaActual();
            dpFechaVencimiento.SelectedDate = fechaActual.GetFechaActual();
            dpFechaDocumento.SelectedDate = fechaActual.GetFechaActual();

            var result = cn.SelectTransId();

            if (result.Item2 == null)
            {
                txtNumero.Text = result.Item1.ToString();
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
            }

            var result1 = cn.SelectBaseRef();

            if (result1.Item2 == null)
            {
                txtNroOrigen.Text = result1.Item1.ToString();
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result1.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
            }

           
        }

        private void LoadCurrency()
        {
            var currencys = cn.GetCurrency();

            Properties.Settings.Default.MainCurrency= currencys.Item1;

            Properties.Settings.Default.SysCurrency = currencys.Item2;

            Properties.Settings.Default.Save();
        }

      
        private void dpFechaContabilizacion_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? fecha = dpFechaContabilizacion.SelectedDate;

            SelectedDate= String.Format("{0:yyyy/MM/dd}", fecha);

            if (String.IsNullOrWhiteSpace(SelectedDate) == false)
            {                 
                var result= cn.FindRate(Convert.ToDateTime(SelectedDate));

                if (result.Item2 == null)
                {
                    Rate = result.Item1;

                    if (Rate == 0)
                    {
                        ShowTipoCambio();
                    }
                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                }

            }

        }

        private void ShowTipoCambio()
        {          
            TipoCambio ventanaTipoCambio = new TipoCambio();

            DoubleAnimation animation = new DoubleAnimation(0, 1,
                                 (Duration)TimeSpan.FromSeconds(1));
            ventanaTipoCambio.BeginAnimation(UIElement.OpacityProperty, animation);

            ventanaTipoCambio.Show();
           
        }

        private void txtDebit_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Text != "")
            {
                SetSysDeb();
            }
        }

        private void txtFCDebit_LostFocus(object sender, RoutedEventArgs e)
        {
            //totDebitME.Text = "";

            //totDebitME.Text=Convert.ToString(cn.SumFCDebit(dt));

            TextBox textBox = (TextBox)sender;

            if (textBox.Text != "")
            {

                Debit();
            }
        }

        private void SetSysDeb()
        {
            try
            {

                var row_list = GetDataGridRows(dgAsiento);

                DataRowView row_Selected = dgAsiento.SelectedItem as DataRowView;

                foreach (DataGridRow single_row in row_list)
                {
                    if (single_row.IsSelected == true)
                    {
                        string result;

                        result = Convert.ToString(row_Selected["Debit"]).Substring(0, 3);//verificar

                        TextBlock txtSysDeb = FindChild<TextBlock>(single_row, "txtSysDeb");

                        TextBox txtDebit = FindChild<TextBox>(single_row, "txtDebit");

                        if (result == Properties.Settings.Default.MainCurrency)
                        {


                        }
                        else
                        {
                            row_Selected["Debit"] = Properties.Settings.Default.MainCurrency + " " + ConvertDecimalTwoPlaces(row_Selected["Debit"]);

                            txtDebit.Text = Properties.Settings.Default.MainCurrency + " " + ConvertDecimalTwoPlaces(row_Selected["Debit"]).ToString("N2",nfi);

                           
                        }

                        var result1 = cn.FindRate(Convert.ToDateTime(SelectedDate));

                        if (result1.Item2 == null)
                        {
                            Rate = result1.Item1;

                            if (Rate == 0)
                            {
                                ShowTipoCambio();

                            }
                            else
                            {

                                row_Selected["SYSDeb"] = Properties.Settings.Default.SysCurrency + " " + (ConvertDecimalTwoPlaces((row_Selected["Debit"])) / Rate).ToString("N2", nfi);

                                txtSysDeb.Text = Properties.Settings.Default.SysCurrency + " " + ConvertDecimalTwoPlaces(row_Selected["SYSDeb"]).ToString("N2",nfi);

                                //totDebitoSys.Text = "";

                                //totDebitoSys.Text= Vista.Properties.Settings.Default.SysCurrency + " " + cn.SumSysDebit(dt);

                            }
                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result1.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                        }

                           
                       
                    }
                }
            }

            catch (Exception ex)
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error" + ex.Message, Brushes.Red, Brushes.Black, "003-interface-2.png");
            }
        }

        private void SetSysCred()
        {
            try
            {

                var row_list = GetDataGridRows(dgAsiento);

                DataRowView row_Selected = dgAsiento.SelectedItem as DataRowView;

                foreach (DataGridRow single_row in row_list)
                {
                    if (single_row.IsSelected == true)
                    {
                        string result;

                        result = Convert.ToString(row_Selected["Credit"]).Substring(0, 3);

                        TextBlock txtSysCred = FindChild<TextBlock>(single_row, "txtSysCred");

                        TextBox txtCredit = FindChild<TextBox>(single_row, "txtCredit");

                        if (result == Properties.Settings.Default.MainCurrency)
                        {

                            
                        }
                        else
                        {
                            row_Selected["Credit"] = Properties.Settings.Default.MainCurrency + " " + ConvertDecimalTwoPlaces(row_Selected["Credit"]);

                            txtCredit.Text = Properties.Settings.Default.MainCurrency + " " + ConvertDecimalTwoPlaces(row_Selected["Credit"]).ToString("N2",nfi);
                        }

                        var result1 = cn.FindRate(Convert.ToDateTime(SelectedDate));

                        if (result1.Item2 == null)
                        {
                            Rate = result1.Item1;

                            if (Rate == 0)
                            {
                                ShowTipoCambio();

                            }
                            else
                            {
                                row_Selected["SYSCred"] = Properties.Settings.Default.SysCurrency + " " + (ConvertDecimalTwoPlaces((row_Selected["Credit"])) / Rate).ToString("N2", nfi);

                                txtSysCred.Text = Properties.Settings.Default.SysCurrency + " " + ConvertDecimalTwoPlaces(row_Selected["SYSCred"]).ToString("N2",nfi);

                                //totCreditoSys.Text = "";

                                //totCreditoSys.Text=Vista.Properties.Settings.Default.SysCurrency + " " + cn.SumSysCredit(dt);

                            }
                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result1.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                        }
                            

                    }
                }
            }

            catch (Exception ex)
            {                
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error" + ex.Message, Brushes.Red, Brushes.Black, "003-interface-2.png");
            }
        }
    

       
        private void txtFCCredit_LostFocus(object sender, RoutedEventArgs e)
        {
            //totCreditME.Text = "";

            //totCreditME.Text=Convert.ToString(cn.SumFCCredit(dt));

            TextBox textBox = (TextBox)sender;

            if (textBox.Text != "")
            {
                Credit();
            }
        }
        
       
        private void txtCredit_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Text != "")
            {
                SetSysCred();
            }
        }

        private void Credit()
        {
            object result;

            try
            {               

                var row_list = GetDataGridRows(dgAsiento);

                DataRowView row_Selected = dgAsiento.SelectedItem as DataRowView;

                foreach (DataGridRow single_row in row_list)
                {
                    if (single_row.IsSelected == true)
                    {                       

                        result = cn.VerificaFCCurrency(Convert.ToString(row_Selected["FCCredit"]).Substring(0, 3));

                        TextBlock txtCredit = FindChild<TextBlock>(single_row, "txtCredit");

                        TextBlock txtDebit= FindChild<TextBlock>(single_row, "txtDebit");

                        txtDebit.IsEnabled= false;

                        if (result != null)
                        {
                            var result1= cn.FindRateFCCurrency(Convert.ToDateTime(SelectedDate), Convert.ToString(row_Selected["FCCredit"]).Substring(0, 3));

                            if (result1.Item2 == null)
                            {

                                RateFC = result1.Item1;

                                if (RateFC == 0)
                                {
                                    ShowTipoCambio();

                                }
                                else
                                {


                                    row_Selected["Credit"] = Properties.Settings.Default.MainCurrency + " " + (ConvertDecimalTwoPlaces(row_Selected["FCCredit"]) * RateFC).ToString("N2",nfi);

                                    txtCredit.Text = Properties.Settings.Default.MainCurrency + " " + ConvertDecimalTwoPlaces(row_Selected["Credit"]).ToString("N2",nfi);

                                    //totCredit.Text = "";

                                    //totCredit.Text= Vista.Properties.Settings.Default.MainCurrency + " " + cn.SumCredit(dt);

                                    SetSysCred();
                                }
                            }
                            else
                            {
                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error" + result1.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
                            }
                        }
                        else
                        {
                            row_Selected["Credit"] = "";

                            txtCredit.Text = row_Selected["Credit"].ToString();                           

                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("La moneda no es valida", Brushes.Red, Brushes.Black, "003-interface-2.png");
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error" + ex.Message, Brushes.Red, Brushes.Black, "003-interface-2.png");

                result = 0;
            }
        }

        private void Debit()
        {
            try
            {

                var row_list = GetDataGridRows(dgAsiento);

                DataRowView row_Selected = dgAsiento.SelectedItem as DataRowView;

                foreach (DataGridRow single_row in row_list)
                {
                    if (single_row.IsSelected == true)
                    {
                        object result;

                        result = cn.VerificaFCCurrency(Convert.ToString(row_Selected["FCDebit"]).Substring(0, 3));

                        TextBlock txtDebit = FindChild<TextBlock>(single_row, "txtDebit");

                        if (result != null)
                        {
                            var result1= cn.FindRateFCCurrency(Convert.ToDateTime(SelectedDate), Convert.ToString(row_Selected["FCDebit"]).Substring(0, 3));

                            if (result1.Item2 == null)
                            {

                                RateFC = result1.Item1;

                                if (RateFC == 0)
                                {
                                    ShowTipoCambio();

                                }
                                else
                                {
                                    row_Selected["Debit"] = Properties.Settings.Default.MainCurrency + " " + (ConvertDecimalTwoPlaces(row_Selected["FCDebit"]) * RateFC).ToString("N2",nfi);

                                    txtDebit.Text = row_Selected["Debit"].ToString();

                                    //totDebit.Text = "";

                                    //totDebit.Text= Vista.Properties.Settings.Default.MainCurrency + " " + cn.SumDebit(dt);

                                    SetSysDeb();
                                }
                            }
                            else
                            {
                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result1.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
                            }
                        }
                        else
                        {
                            row_Selected["Debit"] = "";

                            txtDebit.Text = ConvertDecimalTwoPlaces(row_Selected["Debit"]).ToString("N2",nfi);

                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("La moneda no es valida", Brushes.Red, Brushes.Black, "003-interface-2.png");
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error" + ex.Message, Brushes.Red, Brushes.Black, "003-interface-2.png");
            }
        }

        private void imgShortName_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var row_list = GetDataGridRows(dgAsiento);

                DataRowView row_Selected = dgAsiento.CurrentItem as DataRowView;

                foreach (DataGridRow single_row in row_list)
                {
                    if (single_row.IsEditing == true)
                    {
                        TextBox txtShortName = FindChild<TextBox>(single_row, "txtShortName");

                        TextBlock txtAcctName = FindChild<TextBlock>(single_row, "txtAcctName");

                        TextBlock txtAccount = FindChild<TextBlock>(single_row, "txtAccount");

                        
                        var result = cn.ConsultaCuentasNoAsociadas();

                        if (result.Item2 == null)
                        {
                            RecorreListaAccount(result.Item1, txtShortName, txtAcctName, txtAccount, row_Selected);
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

        private void RecorreListaAccount(List<Cuenta> listAccountResultante, TextBox txtShortName, TextBlock txtAcctName,TextBlock txtAccount, DataRowView row_Selected)
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
                        GetAcctCode(ventanaListaCuentaAsociada.GetListAccount(),txtShortName,txtAcctName,txtAccount,row_Selected);
                    }
                }
            }
        }

        private void GetAcctCode(List<Cuenta> listaCuenta, TextBox txtShortName, TextBlock txtAcctName, TextBlock txtAccount, DataRowView row_Selected)
        {
            foreach (Cuenta cuenta in listaCuenta)
            {
                txtShortName.Text = cuenta.AcctCode;

                row_Selected["ShortName"] = cuenta.AcctCode;

                row_Selected["Account"]= cuenta.AcctCode;

                row_Selected["AcctName"] = cuenta.AcctName;

                txtAcctName.Text = cuenta.AcctName;

                txtAccount.Text= cuenta.AcctCode;

              

                dgAsiento.UpdateLayout();

               
            }
        }

        private void imgAccount_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void txtLineMemo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (btnOk.Content.ToString() == "OK")
            {
                btnOk.Content = "Actualizar";
            }
        }

        private void txtShortName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab && Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                try
                {
                    var row_list = GetDataGridRows(dgAsiento);

                    DataRowView row_Selected = dgAsiento.SelectedItem as DataRowView;

                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            TextBox txtShortName = FindChild<TextBox>(single_row, "txtShortName");

                            TextBlock txtAcctName = FindChild<TextBlock>(single_row, "txtAcctName");

                            TextBlock txtAccount = FindChild<TextBlock>(single_row, "txtAccount");

                            var result = cn.FindBP();

                            if (result.Item2 == null)
                            {
                                RecorreListaSN(result.Item1, txtShortName, txtAcctName, txtAccount, row_Selected);
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
        }

        private void RecorreListaSN(List<SocioNegocio> listSuppliers, TextBox txtShortName, TextBlock txtAcctName, TextBlock txtAccount, DataRowView row_Selected)
        {
            if (listSuppliers.Count == 1)
            {
                GetSocioNegocio(listSuppliers, txtShortName, txtAcctName, txtAccount, row_Selected);


            }
            else if (listSuppliers.Count == 0)
            {

                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se encontro ningun resultado con los parametros establecidos", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");

            }

            else if (listSuppliers.Count > 1)
            {
                ListaSociosNegocios ventanaListBox = new ListaSociosNegocios(listSuppliers);

                ventanaListBox.ShowDialog();

                if (ventanaListBox.Visibility == Visibility.Hidden)
                {
                    if (ventanaListBox.GetListSN().Count == 0)
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se selecciono ningun elemento", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");

                    }
                    else
                    {

                        GetSocioNegocio(ventanaListBox.GetListSN(), txtShortName, txtAcctName, txtAccount, row_Selected);                        

                    }
                }
            }
        }

        private void GetSocioNegocio(List<SocioNegocio> listSuppliers, TextBox txtShortName, TextBlock txtAcctName, TextBlock txtAccount, DataRowView row_Selected)
        {
            foreach (SocioNegocio Suppliers in listSuppliers)
            {
                txtShortName.Text = Suppliers.CardCode;

                txtAccount.Text = Suppliers.DebPayAcct;

                row_Selected["Account"] = Suppliers.DebPayAcct;

                row_Selected["AcctName"] = Suppliers.CardName;

                txtAcctName.Text = row_Selected["AcctName"].ToString();

            }
        }

      
    }
}
