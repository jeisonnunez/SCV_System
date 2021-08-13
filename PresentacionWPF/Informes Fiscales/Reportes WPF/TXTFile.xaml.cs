using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para TXTFile.xaml
    /// </summary>
    public partial class TXTFile : Window
    {
        DataTable dtTXT = new DataTable();

        PortalRetencionesIVA portal;
        public TXTFile()
        {
            InitializeComponent();
        }

        public void ClearDatatable()
        {
            dtTXT.Rows.Clear();

        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InicializacionBasica();

            CreateWebBrowser();
        }

        private void CreateWebBrowser()
        {
            PortalRetencionesIVA portalRetencionesIVA = new PortalRetencionesIVA();

            portal = portalRetencionesIVA;
        }

        public void LoadedWindow()
        {
            InicializacionBasica();
        }

        public void SetDataTable(DataTable dataTable)
        {
            dtTXT = dataTable;
        }

        public void CreateTXTFile()
        {
            foreach (DataRow row in dtTXT.Rows)
            {
              
                txtFile.Text += row["U_IDA_TaxIdNo"] + "\t" + row["U_IDA_Periodo"] + "\t" + row["U_IDA_FechaFact"] + "\t" + row["U_IDA_TipoOperacion"] + "\t" + row["U_IDA_TipoTran"] + "\t" + row["U_IDA_LicTradNum"] + "\t" + row["U_IDA_Factura"] + "\t" + row["U_IDA_NroControl"] + "\t" + row["U_IDA_Total"] + "\t" + row["U_IDA_BaseImp"]  + "\t" + row["U_IDA_Retencion"] + "\t" + row["U_IDA_Fafe"] + "\t" + row["U_IDA_NroComp"] + "\t" + row["U_IDA_Exento"] + "\t" + row["U_IDA_Alicuota"] + "\t" + row["U_IDA_NroFile"] + Environment.NewLine;

             

            }

            txtFile.Text=txtFile.Text.Replace(",", ".");

        }

        private void InicializacionBasica()
        {
            txtFile.Text = "";
        }

        private void txtFile_LostFocus(object sender, RoutedEventArgs e)
        {
            App.textBox_LostFocus(sender, e);
        }

        private void txtFile_GotFocus(object sender, RoutedEventArgs e)
        {
            App.textBox_GotFocus(sender, e);
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void imgTXT_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();

            dlg.Filter = "Text File (*.txt)|*.txt";

            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            if (dlg.ShowDialog() == true)
            {
                File.WriteAllText(dlg.FileName, txtFile.Text);

                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Archivo TXT: " + dlg.FileName + "  se genero correctamente", Brushes.LightGreen, Brushes.Black, "001-interface.png");
            }
        }

        private void imgPortal_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            portal.Show();

            portal.LoadedWindow();
        }
    }
}
