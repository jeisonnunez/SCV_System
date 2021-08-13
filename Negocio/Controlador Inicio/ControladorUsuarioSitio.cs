using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Datos.Modelo_Base;

namespace Negocio.Controlador_Inicio
{
    public class ControladorUsuarioSitio
    {       
        ModeloUsuarioSitio cn = new ModeloUsuarioSitio();

        public Tuple<bool,string>VerifyUserAdministratorSQLServer(string user, string password, string server)
        {
            return cn.VerifyUserAdministratorSQLServer(user,password, server);
        }
    }
}
