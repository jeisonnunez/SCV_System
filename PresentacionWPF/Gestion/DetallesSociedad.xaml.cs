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
using Negocio;
using Vista.Gestion.ValidateErrorsDetallesSociedad;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para DetallesSociedad.xaml
    /// </summary>
    public partial class DetallesSociedad : Document
    {
        ControladorSociedadActual cn = new ControladorSociedadActual();

        DataTable dt;

        List<ValidateErrorsDetallesSociedad> listDetallesSociedad = new List<ValidateErrorsDetallesSociedad>();

        public DetallesSociedad()
        {
            InitializeComponent();

        }

        private void GetDetallesSociedad(List<ValidateErrorsDetallesSociedad> list)
        {
            foreach (ValidateErrorsDetallesSociedad row in list)
            {               
                txtSociedad.Text= row.CompnyName;
                txtDireccion.Text = row.CompnyAddr;
                txtPais.Text = row.Country;
                txtImpresion.Text = row.PrintHeadr;
                txtTelefono1.Text = row.Phone1;
                txtTelefono2.Text = row.Phone2;
                txtFax.Text = row.Fax;
                txtCodigoPostal.Text = row.ZipCode;
                txtEmail.Text = row.E_Mail;
                cbMonedaLocal.SelectedValue = row.MainCurncy;
                cbMonedaSistema.SelectedValue= row.SysCurrncy;
                txtRif.Text = row.TaxIdNum;
                txtHacienda.Text = row.RevOffice;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            InicializacionBasica();

            App.Window_Closing(sender, e);
        }

        private void textbox_TextChanged(object sender, TextCompositionEventArgs e)
        {
            btnOK.Content = "Actualizar";
        }


        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            switch (btnOK.Content.ToString())
            {
                case "OK":

                    this.Hide();

                    break;

                case "Actualizar":

                    if (IsValid(detallesSociedad) == true)
                    {
                        List<Entidades.Sociedad> listaSociedad = new List<Entidades.Sociedad>();

                        int i = 1;

                        Entidades.Sociedad Sociedad = new Entidades.Sociedad();

                        Sociedad.CompnyName = txtSociedad.Text;
                        Sociedad.CompnyAddr = txtDireccion.Text;
                        Sociedad.Country = txtPais.Text;
                        Sociedad.PrintHeadr = txtImpresion.Text;
                        Sociedad.Phone1 = txtTelefono1.Text;
                        Sociedad.Phone2 = txtTelefono2.Text;
                        Sociedad.Fax = txtFax.Text;
                        Sociedad.E_Mail = txtEmail.Text;
                        Sociedad.ZipCode = txtCodigoPostal.Text;
                        Sociedad.MainCurncy = cbMonedaLocal.SelectedValue.ToString();
                        Sociedad.SysCurncy = cbMonedaSistema.SelectedValue.ToString();
                        Sociedad.TaxIdNum = txtRif.Text;
                        Sociedad.RevOffice = txtHacienda.Text;
                        Sociedad.UserSign = Convert.ToInt32(Properties.Settings.Default.Usuario);
                        Sociedad.UpdateDate = Convert.ToDateTime(Entidades.fechaActual.GetFechaActual());

                        listaSociedad.Add(Sociedad);

                        var result = cn.ActualizaSociedad(listaSociedad);

                        if (i == result.Item1)
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("La sociedad se actualizo correctamente", Brushes.LightGreen, Brushes.Black, "001-interface.png");

                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al actualizar la sociedad: " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");

                        }

                        InicializacionBasica();
                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error. Existe algun elemento del formulario con erorres: ", Brushes.Red, Brushes.White, "003-interface-2.png");

                    }



                    break;

            }               
        }        

        private void comboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            App.comboBox_LostFocus(sender, e);
        }

        private void comboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            App.comboBox_GotFocus(sender, e);
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            App.textBox_LostFocus(sender, e);
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            App.textBox_GotFocus(sender, e);
        }

        private void comboBoxItem_GotFocus(object sender, RoutedEventArgs e)
        {
            App.comboBoxItem_GotFocus(sender, e);
        }

        private void comboBoxItem_LostFocus(object sender, RoutedEventArgs e)
        {
            //InicializacionBasica();

            App.comboBoxItem_LostFocus(sender, e);
        }

        private void btnWindow_Close(object sender, RoutedEventArgs e)
        {
           
            this.Hide();
        }

        public void LoadedWindow()
        {
            InicializacionBasica();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InicializacionBasica();
        }

        private void InicializacionBasica()
        {
            btnOK.Content = "OK";

            var result = cn.ConsultaSociedadActual();

            if (result.Item2 == null)
            {
                dt = result.Item1;

                var result1= cn.ConsultaMonedas();

                if (result1.Item2 == null)
                {
                    cbMonedaSistema.ItemsSource = result1.Item1;

                    cbMonedaLocal.ItemsSource= result1.Item1;

                    listDetallesSociedad = ConvertDataTable<ValidateErrorsDetallesSociedad>(dt);                    

                    GetDetallesSociedad(listDetallesSociedad);
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

        private void cbMonedaLocal_DropDownOpened(object sender, EventArgs e)
        {
            btnOK.Content = "Actualizar";
        }
    }
}
