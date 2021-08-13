using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
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
using HtmlAgilityPack;
using Negocio;
using Vista.Gestion.ModelTipoCambio;
using System.Threading.Tasks;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para TipoCambio.xaml
    /// </summary>
    public partial class TipoCambio : Document
    {
        ControladorTipoCambio cn = new ControladorTipoCambio();

        string year = "";

        string month = "";

        DataTable dt;

        DataTable dtTipoCambio;

        DataTable dtTableEnd;

        private bool handle = true;

        private bool handle1 = true;
        public TipoCambio()
        {
            InitializeComponent();
            
            var result = cn.ConsultaMeses();

            if (result.Item2 == null)
            {
                cbMes.ItemsSource = result.Item1;

            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
            }

            var result1 = cn.ConsultaYears();

            if (result1.Item2 == null)
            {
                cbAno.ItemsSource = result1.Item1;

            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result1.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
            }

            InicializacionBasica();
        }

        private void InicializacionBasica()
        {

            btnOK.Content = "OK";

            var result = cn.ConsultaMeses();

            if (result.Item2 == null)
            {
                cbMes.ItemsSource = result.Item1;

            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
            }

            var result1 = cn.ConsultaYears();

            if (result1.Item2 == null)
            {
                cbAno.ItemsSource = result1.Item1;

            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result1.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
            }

            TaskTipoCambioOficialUSD();            

            var result2= cn.ConsultaTipoCambio();

            if (result2.Item2 == null)
            {
                dt = result2.Item1;

            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result2.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
            }

            var result3 = cn.ConsultaTiposCambiosDefinidos();

            if (result3.Item2 == null)
            {
                dtTipoCambio = result3.Item1;

            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result3.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
            }

            dtTableEnd = cn.AsignaTiposCambio(dt, dtTipoCambio);

            dgTipoCambio.ItemsSource = null;

            dgTipoCambio.ItemsSource = dtTableEnd.DefaultView;

            

           TipoCambioActual();

        }

        private void TaskTipoCambioOficialUSD()
        {
            var task = Task.Run(() => GetTipoCambioOficialUSD());

            task.Wait(TimeSpan.FromSeconds(3));       
        }

        private void GetTipoCambioOficialUSD()
        {
            try
            {
                var findUSDColumn = cn.FindUSDColumn();

                if (findUSDColumn.Item2 == null && findUSDColumn.Item1==true)
                {
                    string url = "http://www.bcv.org.ve";

                    var web = new HtmlWeb();

                    HtmlDocument doc = web.Load(url);

                    var datePage = doc.DocumentNode.SelectNodes("//*[@id=\"block-views-612b6aaa739877c430ffe6a2079ce5a8\"]/div/div[2]/div/div[8]/span")[0].InnerText; // obtiene el dia de la pagina del banco central

                    DateTime datetimePage = Convert.ToDateTime(datePage); //convierte el valor en campo de tipo fecha

                    var rateUSDPage = doc.DocumentNode.SelectNodes("//*[@id=\"dolar\"]/div/div/div[2]/strong")[0].InnerText; //obtiene tasa del dia en la pagina

                    decimal rateUSD = Convert.ToDecimal(rateUSDPage); //convierte valor a tipo decimal

                    var result = cn.FindRateUSD(datetimePage);

                    if (result.Item2 == null)
                    {
                        if (result.Item1 == 0) // no se definio la tasa
                        {
                            List<Entidades.TipoCambio> listaTipoCambio = new List<Entidades.TipoCambio>();                           

                            Entidades.TipoCambio tipoCambio = new Entidades.TipoCambio();

                            tipoCambio.RateDate = datetimePage;

                            tipoCambio.Currency = "USD";

                            tipoCambio.Rate = rateUSD;

                            tipoCambio.UserSign = Properties.Settings.Default.Usuario;

                            listaTipoCambio.Add(tipoCambio);

                            var resultUpdate = cn.InsertaTipoCambio(listaTipoCambio);

                            if (resultUpdate.Item1==1)
                            {

                            }
                            else
                            {
                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error en la actualizacion del tipo de cambio: " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");

                            }
                        }
                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                    }
                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("La moneda USD no existe: ", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");
                }
            }
            catch (Exception ex)
            {
                //EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se pudo obtener el tipo de cambio: ", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");
            }

            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InicializacionBasica();
        }

        public void LoadedWindow()
        {
            InicializacionBasica();
        }

        private void TipoCambioActual()
        {
            int mesActual = Entidades.fechaActual.GetMesActual();

            if (mesActual < 10)
            {
                cbMes.SelectedValue = "0" + mesActual;
            }
            else
            {
                cbMes.SelectedValue = mesActual;
            }


            cbAno.SelectedValue = Entidades.fechaActual.GetYearActual().ToString();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           

            App.Window_Closing(sender, e);
        }

        private void btnWindow_Close(object sender, RoutedEventArgs e)
        {
           

            this.Hide();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (btnOK.Content.ToString() == "OK")
            {
                this.Hide();
            }

            if (btnOK.Content.ToString() == "Actualizar")
            {
                List<Entidades.TipoCambio> listaTipoCambio = new List<Entidades.TipoCambio>();

                int i = 0;

                foreach (DataRow row in dtTableEnd.Rows)
                {
                    foreach (DataColumn column in dtTableEnd.Columns)
                    {

                        if (row[column].ToString() != "" && column.ToString() != "Dia")
                        {

                            Entidades.TipoCambio tipoCambio = new Entidades.TipoCambio();

                            tipoCambio.RateDate = Convert.ToDateTime(row["Dia"]);

                            tipoCambio.Currency = column.ToString();

                            tipoCambio.Rate = Convert.ToDecimal(row[column].ToString());

                            tipoCambio.UserSign = Properties.Settings.Default.Usuario;

                            listaTipoCambio.Add(tipoCambio);

                            i++;
                        }
                    }
                }

                var result = cn.InsertaTipoCambio(listaTipoCambio);

                if (i == result.Item1)
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Tipos de cambios se actualizaron correctamente", Brushes.LightGreen, Brushes.Black, "001-interface.png");
                   
                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error en la actualizacion del tipo de cambio: " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
                   
                }

                InicializacionBasica();
            }
        }

        private void dgTipoCambio_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
           
            btnOK.Content = "Actualizar";
        }

        private void Handle()
        {
            try { 

                    //cbAno.SelectedValue.ToString();

                    int mes;

                    DataView dv = dgTipoCambio.ItemsSource as DataView;

                    month = cbMes.SelectedValue.ToString();

                    mes = Convert.ToInt32(month);

                    FiltraCombobox(year, mes, dv);

            }catch
            {
                int mes;

                DataView dv = dgTipoCambio.ItemsSource as DataView;

                cbMes.SelectedValue = month;

                mes = Convert.ToInt32(month);

                FiltraCombobox(year, mes, dv);
            }
        }

        private void Handle1()
        {
            try
            {

                int mes;

                DataView dv = dgTipoCambio.ItemsSource as DataView;

                year = cbAno.SelectedValue.ToString();

                mes = Convert.ToInt32(month);

                FiltraCombobox(year, mes, dv);

            }
            catch 
            {

                int mes;

                DataView dv = dgTipoCambio.ItemsSource as DataView;

                cbAno.SelectedValue = year;

                mes = Convert.ToInt32(month);

                FiltraCombobox(year, mes, dv);
            }
        }
        private void cbMes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ComboBox cmb = sender as ComboBox;
                handle = !cmb.IsDropDownOpen;
                Handle();

                //int mes;           

                //DataView dv = dgTipoCambio.ItemsSource as DataView;

                //month = cbMes.SelectedValue.ToString();

                //mes = Convert.ToInt32(month);

                //FiltraCombobox(year, mes, dv);

            }catch
            {
                //int mes;

                //DataView dv = dgTipoCambio.ItemsSource as DataView;

                //cbMes.SelectedValue = month;

                //mes = Convert.ToInt32(month);

                //FiltraCombobox(year, mes, dv);
            }
        }

        private void cbAno_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

            ComboBox cmb = sender as ComboBox;

            handle1 = !cmb.IsDropDownOpen;

            Handle1();

            int mes;           

            DataView dv = dgTipoCambio.ItemsSource as DataView;
                
            year = cbAno.SelectedValue.ToString();

            mes = Convert.ToInt32(month);

            FiltraCombobox(year,mes,dv);

            }catch
            {

                int mes;

                DataView dv = dgTipoCambio.ItemsSource as DataView;

                cbAno.SelectedValue = year;

                mes = Convert.ToInt32(month);

                FiltraCombobox(year, mes, dv);
            }
        }

        private void FiltraCombobox(string year, int mes, DataView dv)
        {
            int mesSiguiente;

            string mesSeleccionado;

            mesSiguiente = mes + 1;

            mesSeleccionado = mes.ToString();

            if (mesSeleccionado != "" && year != "")
            {

                if (dv != null)
                {
                    if (mesSiguiente == 13)
                    {
                        
                        dv.RowFilter = "Dia >= #" + year + "-" + mes + "-01" + "# AND " + "Dia <= #" + year + "-" + mes + "-31" + "#";
                    }
                    else
                    {
                        dv.RowFilter = "Dia >= #" + year + "-" + mes + "-01" + "# AND " + "Dia < #" + year + "-" + mesSiguiente + "-01" + "#";
                    }

                }
            }
        }

        private void dgTipoCambio_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Dia")
            {
                DataGridTextColumn dataGridTextColumn = e.Column as DataGridTextColumn;

                Style style= this.FindResource("TextBlockStyleValidation") as Style;

                dataGridTextColumn.ElementStyle = style;

                 e.Column.IsReadOnly = true; // Makes the column as read only

                //Binding myBinding = new Binding();

                //    myBinding.Path = new PropertyPath("Dia");
                //   myBinding.Mode = BindingMode.TwoWay;
                //  myBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                //  myBinding.ValidatesOnNotifyDataErrors = true;
                //myBinding.ValidatesOnNotifyDataErrors = true;

                //    dataGridTextColumn.Binding = myBinding;


                e.Column.Width = 70;

                dataGridTextColumn.Binding=new Binding(e.PropertyName) { StringFormat = "{0:dd}" };

              


            }

            else if (e.Column.Header.ToString() == "USD")
            {

               //// DataGridTextColumn dataGridTextColumn = e.Column as DataGridTextColumn;



               //// dataGridTextColumn.ElementStyle = this.TryFindResource("TextBlockStyleValidation") as Style;

               //// //dataGridTextColumn.name

               ////dataGridTextColumn.Binding = new Binding("USD") { Source = this, Mode=BindingMode.TwoWay, UpdateSourceTrigger= UpdateSourceTrigger.PropertyChanged, ValidatesOnDataErrors=true, NotifyOnValidationError=true};


               //// //BindingOperations.SetBinding(TextBlock.TextProperty, myBinding);

               //// BindingOperations.SetBinding(dataGridTextColumn, TextBlock.TextProperty, new Binding("Test")
               //// {
               ////     Source = this
               //// });

                //    DataGridTextColumn dataGridTextColumn = e.Column as DataGridTextColumn;

                //DataGridTemplateColumn dataGridTemplateColumn = new DataGridTemplateColumn();

                //dataGridTemplateColumn.Header = e.Column.Header.ToString();

                //FrameworkElementFactory textBlockFactory = new FrameworkElementFactory(typeof(TextBlock));

                //Binding myBinding = new Binding();

                //myBinding.Path = new PropertyPath("USD");
                //myBinding.Mode = BindingMode.TwoWay;
                //myBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

                //myBinding.ValidatesOnDataErrors = true;
                //myBinding.NotifyOnValidationError = true;

                //textBlockFactory.SetBinding(TextBlock.TextProperty, myBinding);

                //textBlockFactory.SetValue(TextBlock.TextProperty, myBinding);

                //textBlockFactory.SetResourceReference(TextBlock.StyleProperty, this.TryFindResource("TextBlockStyleValidation"));

                //textBlockFactory.SetValue(TextBlock.StyleProperty, this.TryFindResource("TextBlockStyleValidation"));





                //DataTemplate template = new DataTemplate();


                //template.VisualTree = textBlockFactory;

                //dataGridTemplateColumn.CellTemplate = template;

                //dataGridTemplateColumn.CellTemplate = (DataTemplate)this.TryFindResource("TextBlockStyleValidation");



                //FrameworkElementFactory textBoxFactory = new FrameworkElementFactory(typeof(TextBox));

                //Binding myBindingv1 = new Binding();

                //myBindingv1.Path = new PropertyPath("USD");
                //myBindingv1.Mode = BindingMode.TwoWay;
                //myBindingv1.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

                //myBindingv1.ValidatesOnDataErrors = true;
                //myBindingv1.NotifyOnValidationError = true;

                //textBoxFactory.SetBinding(TextBox.TextProperty, myBindingv1);

                //textBoxFactory.SetValue(TextBox.TextProperty, myBindingv1);

                //template = new DataTemplate();


                //template.VisualTree = textBoxFactory;

                //dataGridTemplateColumn.CellEditingTemplate = template;






            }
        }

        private void cbMes_DropDownClosed(object sender, EventArgs e)
        {
            if (handle) Handle();
            handle = true;
        }

        private void cbAno_DropDownClosed(object sender, EventArgs e)
        {
            if (handle1) Handle1();
            handle1 = true;
        }



        
    }
}
