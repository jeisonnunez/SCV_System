using Entidades;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Modelo_Base
{
    public class ModeloCrystalReport:ConexionSQLServer
    {
        public Tuple<List<CrystalReport>,string> GetCrystalReport()
        {
            List<CrystalReport> listCrystalReport = new List<CrystalReport>();

            string error = null;

            try
            {
                CrystalReport CrystalReport = new CrystalReport();

                CrystalReport.ServerName = ServerName;
                CrystalReport.User_ID = UserID;
                CrystalReport.Password = Password;
                CrystalReport.DataBase = BaseDatosActual;

                listCrystalReport.Add(CrystalReport);

                return Tuple.Create(listCrystalReport, error);

            }
            catch (Exception e)
            {
                return Tuple.Create(listCrystalReport, e.Message);
            }
        }

    }
}
