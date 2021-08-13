using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
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

namespace Vista.Informes_Fiscales.Reportes_WPF
{
    /// <summary>
    /// Lógica de interacción para CrystalReportISLROperacion.xaml
    /// </summary>
    public partial class CrystalReportComprobanteISLROperacion : CrystalReport
    {
        private string supplier;
        public string Supplier { get => supplier; set => supplier = value; }

        private string numAtCard;
        public string NumAtCard { get => numAtCard; set => numAtCard = value; }

        private string tipoTrans;
        public string TipoTrans { get => tipoTrans; set => tipoTrans = value; }
        public CrystalReportComprobanteISLROperacion()
        {
            InitializeComponent();
        }

        public void LoadedWindow(string TipoTrans, string Supplier, string NumAtCard)
        {
            this.TipoTrans = TipoTrans;

            this.Supplier = Supplier;

            this.NumAtCard = NumAtCard;

            InicializacionBasic();
        }

        private void InicializacionBasic()
        {
            try
            {
                ReportDocument rpt = new ReportDocument();

                rpt.Load(AppDomain.CurrentDomain.BaseDirectory + "Reportes\\Comprobante ISLR Operacion.rpt");

                GetInfoCrystalReport();

                TableLogOnInfo crTableLogoninfo = new TableLogOnInfo();

                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in rpt.Database.Tables)
                {
                    crTableLogoninfo = CrTable.LogOnInfo;
                    crTableLogoninfo.ConnectionInfo = CRConnectionInfo;
                    CrTable.ApplyLogOnInfo(crTableLogoninfo);
                }
                foreach (ReportDocument subreport in rpt.Subreports)
                {
                    foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in subreport.Database.Tables)
                    {
                        crTableLogoninfo = CrTable.LogOnInfo;
                        crTableLogoninfo.ConnectionInfo = CRConnectionInfo;
                        CrTable.ApplyLogOnInfo(crTableLogoninfo);
                    }
                }

                rpt.SetParameterValue("Documento", NumAtCard);

                rpt.SetParameterValue("Tipo", TipoTrans);

                rpt.SetParameterValue("Proveedor", Supplier);

                viewer.Owner = this;

                viewer.ViewerCore.ReportSource = rpt;

            }
            catch (Exception ex)
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + ex.Message, Brushes.Red, Brushes.White, "003-interface-2.png");
            }

        }

        private void CrystalReport_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }
    }
}
