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
using Vista.Informes_Fiscales.Reportes_WPF;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para CrystalReportAnularComprobantesIVA.xaml
    /// </summary>
    public partial class CrystalReportAnularComprobantesIVA : CrystalReport
    {
        private string comprobanteB;

        private string comprobanteT;
        public string ComprobanteB { get => comprobanteB; set => comprobanteB = value; }
        public string ComprobanteT { get => comprobanteT; set => comprobanteT = value; }
        public CrystalReportAnularComprobantesIVA()
        {
            InitializeComponent();
        }

        public void LoadedWindow(string ComprobanteB, string ComprobanteT)
        {
            this.ComprobanteB = ComprobanteB;

            this.ComprobanteT = ComprobanteT;

            InicializacionBasic();
        }

        private void InicializacionBasic()
        {
            try
            {
                ReportDocument rpt = new ReportDocument();

                rpt.Load(AppDomain.CurrentDomain.BaseDirectory + "Reportes\\Comprobantes de Retencion de IVA Anulados.rpt");

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

                rpt.SetParameterValue("ComprobanteB", ComprobanteB);

                rpt.SetParameterValue("ComprobanteT", ComprobanteT);

                viewer.Owner = this;

                viewer.ViewerCore.ReportSource = rpt;

            }
            catch (Exception ex)
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + ex.Message, Brushes.Red, Brushes.White, "003-interface-2.png");
            }

        }

        private void AnularComprobanteIVA_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }
    }
}
