using Microsoft.Win32;
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
using System.Xml;
using System.Xml.Linq;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para XMLFile.xaml
    /// </summary>
    public partial class XMLFile : Window
    {
        DataTable dtXML = new DataTable();

        XmlDocument XmlDoc = new XmlDocument();

        PortalRetencionesISLR portal = new PortalRetencionesISLR();
        public XMLFile()
        {
            InitializeComponent();
        }

        public void ClearDatatable()
        {
            dtXML.Rows.Clear();

        }

        public void SetDataTable(DataTable dataTable)
        {
            dtXML = dataTable;
        }

        public void LoadedWindow()
        {
            InicializacionBasica();
        }


        private void InicializacionBasica()
        {
            wbXML.Navigate(new Uri("about:blank"));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CreateWebBrowser();

            InicializacionBasica();
        }

        private void CreateWebBrowser()
        {
            PortalRetencionesISLR portalRetencionesISLR = new PortalRetencionesISLR();

            portal = portalRetencionesISLR;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void imgXML_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();

            dlg.Filter = "XML Files (*.xml)|*.xml";

            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            if (dlg.ShowDialog() == true)
            {
                XmlDoc.Save(dlg.FileName);

                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Archivo XML: " + dlg.FileName + "  se genero correctamente", Brushes.LightGreen, Brushes.Black, "001-interface.png");
            }
        }

        public void CreateXMLFile()
        {
            string rifAgente = GetRifAgente(dtXML);

            string periodo = GetPeriodo(dtXML);

            string fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".xml"; //archivo temporal

            using (XmlWriter writer = XmlWriter.Create(fileName))
            {
                XmlDocument doc = new XmlDocument();

                XmlNode docTitle = doc.CreateXmlDeclaration("1.0", "iso-8859-1", null);

                doc.AppendChild(docTitle);

                XmlNode relacionRetencionesISLR = doc.CreateElement("RelacionRetencionesISLR");

                doc.AppendChild(relacionRetencionesISLR);

                XmlAttribute relacionRetencionesISLRRifAgente = doc.CreateAttribute("RifAgente");

                relacionRetencionesISLRRifAgente.Value = rifAgente;

                relacionRetencionesISLR.Attributes.Append(relacionRetencionesISLRRifAgente);

                XmlAttribute relacionRetencionesISLRPeriodo = doc.CreateAttribute("Periodo");

                relacionRetencionesISLRPeriodo.Value = periodo;

                relacionRetencionesISLR.Attributes.Append(relacionRetencionesISLRPeriodo);

                //----------------------------------------------------------------------------------

                foreach (DataRow row in dtXML.Rows)
                {
                   
                    XmlNode detalleRetencion = doc.CreateElement("DetalleRetencion");

                    relacionRetencionesISLR.AppendChild(detalleRetencion);

                    //------------------------------------------------------------------------------

                    XmlNode rifRetenido = doc.CreateElement("RifRetenido");

                    rifRetenido.AppendChild(doc.CreateTextNode(row["RifRetenido"].ToString()));

                    detalleRetencion.AppendChild(rifRetenido);

                    //-----------------------------------------------------

                    XmlNode NumeroFactura = doc.CreateElement("NumeroFactura");

                    NumeroFactura.AppendChild(doc.CreateTextNode(row["NumeroFactura"].ToString()));

                    detalleRetencion.AppendChild(NumeroFactura);

                    //------------------------------------------------------------------------------

                    XmlNode NumeroControl = doc.CreateElement("NumeroControl");

                    NumeroControl.AppendChild(doc.CreateTextNode(row["NumeroControl"].ToString()));

                    detalleRetencion.AppendChild(NumeroControl);

                    //-----------------------------------------------------

                    XmlNode FechaOperacion = doc.CreateElement("FechaOperacion");

                    FechaOperacion.AppendChild(doc.CreateTextNode(row["FechaOperacion"].ToString()));

                    detalleRetencion.AppendChild(FechaOperacion);

                    //------------------------------------------------------------------------------

                    XmlNode CodigoConcepto = doc.CreateElement("CodigoConcepto");

                    CodigoConcepto.AppendChild(doc.CreateTextNode(row["CodigoConcepto"].ToString()));

                    detalleRetencion.AppendChild(CodigoConcepto);

                    //-----------------------------------------------------

                    XmlNode MontoOperacion = doc.CreateElement("MontoOperacion");

                    MontoOperacion.AppendChild(doc.CreateTextNode(row["MontoOperacion"].ToString()));

                    detalleRetencion.AppendChild(MontoOperacion);

                    //-----------------------------------------------------

                    XmlNode PorcentajeRetencion = doc.CreateElement("PorcentajeRetencion");

                    PorcentajeRetencion.AppendChild(doc.CreateTextNode(row["PorcentajeRetencion"].ToString()));

                    detalleRetencion.AppendChild(PorcentajeRetencion);

                }

                writer.Close(); //cierra el escritor

                doc.Save(fileName); //guarda el archivo temporal

                wbXML.Navigate(new Uri(fileName)); //muestra archivo en WebBrowser

                XmlDoc = doc;

                
            }

        }

        private string GetPeriodo(DataTable dtXML)
        {
            string periodo = null;

            DataTable result = dtXML.AsEnumerable()
                     .GroupBy(x => x.Field<string>("Periodo"))
                     .Select(x => x.First()).CopyToDataTable();

            result.AcceptChanges();

            foreach (DataRow row in result.Rows)
            {
                periodo = row["Periodo"].ToString();
            }

            return periodo;
        }

        private string GetRifAgente(DataTable dtXML)
        {
            string agente = null;

            DataTable result = dtXML.AsEnumerable()
                     .GroupBy(x => x.Field<string>("RifAgente"))
                     .Select(x => x.First()).CopyToDataTable();

            result.AcceptChanges();

            foreach (DataRow row in result.Rows)
            {
                agente = row["RifAgente"].ToString();
            }

            return agente;
        }

        private void imgPortal_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            portal.Show();

            portal.LoadedWindow();
        }
    }
}
