using CrystalDecisions.Shared;
using Negocio.Controlador_Gestion;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Vista.Informes_Fiscales.Reportes_WPF
{
    public class CrystalReport:Window
    {
        ControladorCrystalReport cn = new ControladorCrystalReport();

        private ConnectionInfo cRConnectionInfo;

        public ConnectionInfo CRConnectionInfo { get => cRConnectionInfo; set => cRConnectionInfo = value; }
        public CrystalReport() : base()
        {

        }

        protected void GetInfoCrystalReport()
        {
            var result=cn.GetCrystalReport();

            if (result.Item2 == null)
            {
                foreach (Entidades.CrystalReport crystalReport in result.Item1)
                {
                    ConnectionInfo crConnectionInfo = new ConnectionInfo();
                    crConnectionInfo.ServerName = crystalReport.ServerName;
                    crConnectionInfo.DatabaseName = crystalReport.DataBase;
                    crConnectionInfo.UserID = crystalReport.User_ID;
                    crConnectionInfo.Password = crystalReport.Password;

                    CRConnectionInfo = crConnectionInfo;
                   
                }
                
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al obtener datos para generar reportes ", Brushes.Red, Brushes.White, "003-interface-2.png");
            }
        }
    }
}
