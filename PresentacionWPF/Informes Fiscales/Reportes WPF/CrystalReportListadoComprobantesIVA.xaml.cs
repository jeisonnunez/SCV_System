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
    /// Lógica de interacción para CrystalReportListadoComproantesIVA.xaml
    /// </summary>
    public partial class CrystalReportListadoComprobantesIVA : CrystalReport
    {
        private string supplier;
        public string Supplier { get => supplier; set => supplier = value; }

        private string supplier1;
        public string Supplier1 { get => supplier1; set => supplier1 = value; }

        private DateTime? desde;
        public DateTime? Desde { get => desde; set => desde = value; }

        private DateTime? hasta;
        public DateTime? Hasta { get => hasta; set => hasta = value; }
        public CrystalReportListadoComprobantesIVA()
        {
            InitializeComponent();
        }

        public void LoadedWindow(string Supplier, string Supplier1, DateTime? desde, DateTime? hasta)
        {
            this.Supplier = Supplier;

            this.Supplier1 = Supplier1;

            this.Desde = desde;

            this.Hasta = hasta;

            InicializacionBasic();
        }

        private void InicializacionBasic()
        {
            try
            {
                ReportDocument rpt = new ReportDocument();

                rpt.Load(AppDomain.CurrentDomain.BaseDirectory + "Reportes\\Listado de Comprobantes de IVA.rpt");

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

                rpt.SetParameterValue("Proveedor", Supplier);

                rpt.SetParameterValue("ProveedorH", Supplier1);

                rpt.SetParameterValue("FechaD", Desde);

                rpt.SetParameterValue("FechaH", Hasta);

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
