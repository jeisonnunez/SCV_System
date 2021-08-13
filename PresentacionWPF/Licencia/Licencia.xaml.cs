using BeautySolutions.View.ViewModel;
using Entidades;
using Microsoft.Win32;
using Negocio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
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
using System.Xml;
using Vista.Clases_Basicas;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para Licencia.xaml
    /// </summary>
    public partial class Licencia : Window
    {
        private string fileNameXML;

        public string FileNameXML { get => fileNameXML; set => fileNameXML = value; }
        public bool IsValid { get => isValid; set => isValid = value; }

        private bool isValid;

        ControladorRegistro cn = new ControladorRegistro();
        
        public Licencia()
        {
            InitializeComponent();
        }

        public void SaveLicenceFile()
        {
            Properties.Settings.Default.LicenseFile = FileNameXML;            

            XmlDocument document = new XmlDocument();

            document.Load(FileNameXML);

            XmlNode xmlNode = document.GetElementsByTagName("Signature")[0];            

            XmlNode infoLicense = document.GetElementsByTagName("LICENCE")[0];

            LicenceInfo licenseInfo = new LicenceInfo();

            var result10 = cn.obtenerBaseDatos();

            int code;

            try
            {
                code = (from n in Properties.Settings.Default.LicenseInfo                            
                            select n).Count();
            }
            catch(Exception ex)
            {
                code = 0;

                Properties.Settings.Default.LicenseInfo = new List<LicenceInfo>();
            }


            code = code + 1;

            licenseInfo.Code = code;
            licenseInfo.CreateDate = (DateTime)fechaActual.GetFechaActual();
            licenseInfo.UserSign = Properties.Settings.Default.Usuario;
            licenseInfo.Server = infoLicense.Attributes["SERVER"].Value;
            licenseInfo.HardwareKey = infoLicense.Attributes["HARDWAREKEY"].Value;
            licenseInfo.FechaInicio = DateTime.ParseExact(infoLicense.Attributes["CREATION"].Value, "yyyyMMdd", CultureInfo.InvariantCulture);
            licenseInfo.FechaVencimiento = DateTime.ParseExact(infoLicense.Attributes["EXPIRED"].Value, "yyyyMMdd", CultureInfo.InvariantCulture);
            licenseInfo.Signature = xmlNode.InnerText;
            licenseInfo.Object = "SCV_SYSTEM";           
            licenseInfo.DataBase = result10.Item1;

            Properties.Settings.Default.LicenseInfo.Add(licenseInfo);

            Properties.Settings.Default.Save();

        }

        public static bool VerifyXmlFile(string Name)
        {
            bool validate;

            try
            {
                XmlDocument document = new XmlDocument();

                document.Load(Name);

                X509Certificate2 x509 = new X509Certificate2(EstableceLogin.PublicKey);

                SignedXml signedXml = new SignedXml(document);

                XmlNode xmlNode = document.GetElementsByTagName("Signature")[0];

                signedXml.LoadXml((XmlElement)xmlNode);

                if (signedXml.CheckSignature(x509, true) == true)
                {
                    XmlNode infoLicense = document.GetElementsByTagName("LICENCE")[0];

                    if (Environment.MachineName == infoLicense.Attributes["SERVER"].Value)
                    {
                        if (HardwareKey.GET_HARDWAREID == infoLicense.Attributes["HARDWAREKEY"].Value)
                        {

                            var creationLicence = DateTime.ParseExact(infoLicense.Attributes["CREATION"].Value, "yyyyMMdd",
                                   CultureInfo.InvariantCulture);

                            DateTime now = (DateTime)fechaActual.GetFechaActual();

                            int result = DateTime.Compare(creationLicence, now);

                            if (result <= 0)
                            {
                                var expirationLicence = DateTime.ParseExact(infoLicense.Attributes["EXPIRED"].Value, "yyyyMMdd",
                                CultureInfo.InvariantCulture);

                                int resultExpiration = DateTime.Compare(expirationLicence, now);

                                if (resultExpiration >= 0)
                                {
                                    validate = true;
                                }
                                else
                                {
                                    validate = false;
                                }

                            }
                            else
                            {
                                validate = false;
                            }


                        }
                        else
                        {
                            validate = false;
                        }
                    }
                    else
                    {
                        validate = false;
                    }
                }
                else
                {
                    validate = false;
                }

                return validate;
            }
            catch (Exception ex)
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al verificar archivo de licencia: " + ex.Message, Brushes.Red, Brushes.White, "003-interface-2.png");

                validate = false;

                return validate;
            }


        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {

            App.textBox_GotFocus(sender, e);
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            App.textBox_LostFocus(sender, e);
        }

        
        private void txtArchivoLicencia_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {          
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.Multiselect = false;

            fileDialog.Filter= "XML Files (*.xml)|*.xml";

            if (fileDialog.ShowDialog() ==true)
            {
                //Get the path of specified file
                FileNameXML = fileDialog.FileName;

                txtArchivoLicencia.Text = FileNameXML;

            }
        }

        private void UpdateFuncionMenu()
        {
            foreach (SubItem subItem in Menu.listMenus.Where(w => w.IsValid == false))
            {
                subItem.IsValid = true;
            }
        }

        private void btnCrear_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(FileNameXML) == false)
            {
                IsValid = VerifyXmlFile(FileNameXML);

                if (IsValid == true)//licencia valida
                {
                    SaveLicenceFile();

                    UpdateFuncionMenu();

                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Archivo de licencia importado satisfactoriamente: ", Brushes.LightGreen, Brushes.Black, "001-interface.png");

                    txtArchivoLicencia.Text = "";
                }
                else //licencia Invalida
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Archivo de licencia invalido: ", Brushes.Red, Brushes.White, "003-interface-2.png");
                }
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se ha seleccionado ningun archivo de licencia: ", Brushes.LightBlue, Brushes.White, "002-interface-1.png");
            }
        }

       

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ClearField();
        }

        private void ClearField()
        {
            txtArchivoLicencia.Text = "";
            txtServidor.Text = "";
            txtFechaInicio.Text = "";
            txtHardWareKey.Text = "";
            txtfechaVencimiento.Text = "";

        }

        public void LoadedWindow()
        {
            ClearField();

            InicializacionBasica();
        }

        private void InicializacionBasica()
        {

            if (EstableceLogin.IsValid == true)
            {
                txtServidor.Text = Environment.MachineName;

                txtHardWareKey.Text = HardwareKey.GET_HARDWAREID;

                var resultDatabase = cn.obtenerBaseDatos();

                var licenceForDatabase = Properties.Settings.Default.LicenseInfo.Where(x => x.DataBase == resultDatabase.Item1).OrderByDescending(x => x.Code).First(); // obtiene el archivo de licencia mas actualizado para la base de datos

                txtFechaInicio.Text= String.Format("{0:yyyyMMdd}", licenceForDatabase.FechaInicio);

                txtfechaVencimiento.Text = String.Format("{0:yyyyMMdd}", licenceForDatabase.FechaVencimiento);
            }
            else
            {
                txtServidor.Text = Environment.MachineName;

                txtHardWareKey.Text = HardwareKey.GET_HARDWAREID;

                txtFechaInicio.Text = "";

                txtfechaVencimiento.Text = "";

                txtFechaInicio.IsReadOnly = true;

                txtfechaVencimiento.IsReadOnly = true;
            }

           

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        
    }
}

