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
    /// Lógica de interacción para CrystalReportComprobanteISLRMensual.xaml
    /// </summary>
    public partial class CrystalReportComprobanteISLRMensual : CrystalReport
    {
        private string code;
        public string Code { get => code; set => code = value; }

        private string supplier;
        public string Supplier { get => supplier; set => supplier = value; }
        public CrystalReportComprobanteISLRMensual()
        {
            InitializeComponent();
        }

        public void LoadedWindow(string Code, string Supplier)
        {
            this.Code = Code;

            this.Supplier = Supplier;

            InicializacionBasic();
        }

        private void InicializacionBasic()
        {
            try
            {
                ReportDocument rpt = new ReportDocument();

                //Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);

                rpt.Load(AppDomain.CurrentDomain.BaseDirectory + "Reportes\\Comprobante ISLR Mensual.rpt");


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

               

                rpt.SetParameterValue("Periodo", Code);

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
