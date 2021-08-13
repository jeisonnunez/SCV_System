using Entidades;
using Microsoft.Win32;
using Negocio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Xml;
using Vista.Clases_Basicas;
using WPFtoolkitFrameworkStalin.ViewModels;

namespace Vista
{
    public class EstableceLogin: Interface
    {
        ControladorRegistro cn = new ControladorRegistro();
        public static Window Screen { get; set; }

        public static Menu menuStatusBar;

        public static TablaRetencionImpuesto retencionImpuesto;

        public static MediosPago mediosPagoVentana;

        const string pathPublicKey = "MIIEKzCCAxOgAwIBAgIUb+M592dyNwZZABfMgD2H80q7RKswDQYJKoZIhvcNAQELBQAwgaQxCzAJBgNVBAYTAlZFMQswCQYDVQQIDAJEQzELMAkGA1UEBwwCREMxETAPBgNVBAoMCFNDVl9URVNUMR0wGwYDVQQLDBRTT0ZUV0FSRSBFTkdJTkVFUklORzEeMBwGA1UEAwwVd3d3LnBydWViYXNqZWlzb24uY29tMSkwJwYJKoZIhvcNAQkBFhpqZWlzb25udW5lejEyM0Bob3RtYWlsLmNvbTAeFw0yMDA3MDYwNTA3MzRaFw0yMTA3MDYwNTA3MzRaMIGkMQswCQYDVQQGEwJWRTELMAkGA1UECAwCREMxCzAJBgNVBAcMAkRDMREwDwYDVQQKDAhTQ1ZfVEVTVDEdMBsGA1UECwwUU09GVFdBUkUgRU5HSU5FRVJJTkcxHjAcBgNVBAMMFXd3dy5wcnVlYmFzamVpc29uLmNvbTEpMCcGCSqGSIb3DQEJARYaamVpc29ubnVuZXoxMjNAaG90bWFpbC5jb20wggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQDg7JXi9sOUKxH8PNYhgaJRb/OFn5mjKoLcsjb7Ph2vKCtLhvp4B3RdFlx+/vKTiVgRUBZS+9uKUhORBOA8yO5/EEBFd2GLs8jDRri9Yjc4HFDjpadPUnzYh0UtDPrREh28fop0D2x4TfFSeIC2Aze7Unov+v687TmYENOOwctqE4bInTB1kwyXDryLqeOuGl+lv4Vmucj5s8x8PZOWpMJb8OPLq9tJlzDHBDxjRCJTq+YRqkc4Q0ZLiRAQA55hie9/YbZH2EWOAiQbvdea52MBWQcKigDwcfP1Xd276d3FEKWYZzJzx1a3vRU1G0wgeYx6GSsf/coLhPc5K0EONcXFAgMBAAGjUzBRMB0GA1UdDgQWBBSOgIrH8IUPp4Ou8BxgaH3/CLETRTAfBgNVHSMEGDAWgBSOgIrH8IUPp4Ou8BxgaH3/CLETRTAPBgNVHRMBAf8EBTADAQH/MA0GCSqGSIb3DQEBCwUAA4IBAQDECfBLIpgL14QY7GSch4YM5tHdKzKlJY2TNlG8XKhULStiy2Nu8UesIFcUpdar7qQgQevDESzIvJk3mHEP6JPNO1jWasLlpdgXWpUM6zxnu7gTC0ZcbYJtNtuazLMa+fLngkqivCzQ9pLjFOTxaaSRQDnug38XfX6v7Qg6+wIq1A6dhAMyq2IQtJApLMWqAF3he/7ZmPknn/AaxHtNNXEaO4OGOOWUfVfRhCinMk8i8sTkHU4qboLTdNRRPl7u66ecOy092VNVTTnBKUnpGTdt3Xz3kI8fK78+qLGXLV4JpIC+3EObLusNSgqt/0wM3n8EIfLb27nxh/b0mHVSfOvt";

        public static byte[] PublicKey;

        public static bool IsValid;

        public void IngresoSistema(string nombreSociedad, string username)
        {
            try
            {
                GetPublicKey();

                IsValid = VerifyXmlFile(Properties.Settings.Default.LicenseFile);

                Menu menu = new Menu(nombreSociedad, username, IsValid);

                Screen = menu;

                menuStatusBar = menu;

                menu.Show();

                menu.WindowState = WindowState.Maximized;

                menu.Width = SystemParameters.PrimaryScreenWidth;

                menu.Height = SystemParameters.PrimaryScreenHeight;

                menu.ShowStatusMessage("Inicio de Sesion Exitoso", Brushes.LightGreen, Brushes.Black, "001-interface.png");

                LoadTipoCambio();

                CreateTableRetencion();

                CreateTableMediosPagos();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Centinela 8");
            }
            
           
        }

        public bool VerifyXmlFile(string Name)
        {
            bool validate;

            try
            {
                var resultDatabase = cn.obtenerBaseDatos();                

                var licenceForDatabase = Properties.Settings.Default.LicenseInfo.Where(x => x.DataBase == resultDatabase.Item1).OrderByDescending(x => x.Code).First(); // obtiene el archivo de licencia mas actualizado para la base de datos

                    if(Environment.MachineName== licenceForDatabase.Server)
                    {
                        if(HardwareKey.GET_HARDWAREID == licenceForDatabase.HardwareKey)
                        {

                            var creationLicence = licenceForDatabase.FechaInicio;

                            DateTime now = (DateTime)fechaActual.GetFechaActual();

                            int result = DateTime.Compare(creationLicence, now);

                            if (result <= 0)
                            {
                                var expirationLicence = licenceForDatabase.FechaVencimiento;

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
                

                return validate;
            }
            catch (Exception ex)
            {
                App.GetMainWindowStatusBar().ShowStatusMessage("Error al verificar archivo de licencia: " + ex.Message, Brushes.Red, Brushes.White, "003-interface-2.png");

                validate = false;

                return validate;
            }

           
        }

        private void GetPublicKey()
        {
            try
            {
                //Assembly a = Assembly.LoadFile(pathPublicKey);
                //// Get the type to use.
                //Type myType = a.GetType("Datos.ModeloPublicKey.ModeloPublicKey");
                //// Get the method to call.
                //PropertyInfo publicKey = myType.GetProperty("PublicKey", BindingFlags.Public | BindingFlags.Static);

                //publicKey.GetValue(null).ToString();

                

                PublicKey = System.Text.Encoding.UTF8.GetBytes(pathPublicKey.ToString());
            }
            catch(Exception ex)
            {
                App.GetMainWindowStatusBar().ShowStatusMessage("Error: " + ex.Message, Brushes.Red, Brushes.White, "003-interface-2.png");
            }

            

        }

        private void CreateTableMediosPagos()
        {
            MediosPago mediosPago = new MediosPago();

            mediosPagoVentana = mediosPago;           
        }

        private void CreateTableRetencion()
        {
            TablaRetencionImpuesto tablaRetencionImpuesto = new TablaRetencionImpuesto();

            retencionImpuesto = tablaRetencionImpuesto;
        }

       
        private void LoadTipoCambio()
        {
            TipoCambio ventanaTipoCambio = new TipoCambio();

            DoubleAnimation animation = new DoubleAnimation(0, 1,
                                 (Duration)TimeSpan.FromSeconds(1));
            ventanaTipoCambio.BeginAnimation(UIElement.OpacityProperty, animation);

            ventanaTipoCambio.Show();
        }


        public void UltimaSociedad(string sociedad)
        {
            Properties.Settings.Default.BaseDatos = sociedad;

            Properties.Settings.Default.Save();
            

        }

        public void UsuarioActual(string nombre,int user)
        {
            Properties.Settings.Default.Usuario = user;

            Properties.Settings.Default.NombreUsuario = nombre;

            Properties.Settings.Default.Save();
        }

        public static Window GetMenu()
        {
            return Screen;
        }

        public static Menu GetMenuStatusBar()
        {
            return menuStatusBar;
        }

        public static TablaRetencionImpuesto GetTablaRetencionImpuesto()
        {
            return retencionImpuesto;
        }

        public static MediosPago GetMediosPago()
        {
            return mediosPagoVentana;
        }
    }
}
