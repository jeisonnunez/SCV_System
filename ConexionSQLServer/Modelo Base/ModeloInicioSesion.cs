using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Datos
{
    public class ModeloInicioSesion: ConexionSQLServer
    {
        public string EstableceConexionString(string baseDatos)
        {
            string connectionString = string.Format("Server=" + ServerName +"; Database=" + baseDatos + "; User Id=sa; Password=SAPB1Admin1; integrated security=false;");

            string error = null;

            try
            {
                if (baseDatos != "master")
                {
                    BaseDatosActual = baseDatos;

                }
            
                AppSetting setting = new AppSetting();

                setting.SaveConnectionString("cn", connectionString);

                return error;

            }
            catch (Exception ex)
            {
                error = ex.Message;

                return error;
            }

        }

        public string EstableceConexionStringTemp(string baseDatos)
        {
            string connectionString = string.Format("Server=" + ServerName + "; Database=" + baseDatos + "; User Id=sa; Password=SAPB1Admin1; integrated security=false;");

            string error = null;

            try
            {
                
                AppSetting setting = new AppSetting();

                setting.SaveConnectionString("cn", connectionString);

                return error;

            }
            catch (Exception ex)
            {
                error = ex.Message;

                return error;
            }

        }

        public void obtenerCadenaConexion()
        {
            AppSetting appSetting = new AppSetting();

            connectionString = appSetting.GetConnectionString("cn");
        }
    }
}
