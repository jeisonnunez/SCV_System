using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ControladorInicioSesion
    {
        ModeloInicioSesion cn = new ModeloInicioSesion();
        public void obtenerCadenaConexion()
        {
            cn.obtenerCadenaConexion();
        }

        public string EstableceConexionString(string cadenaConexion)
        {
            return cn.EstableceConexionString(cadenaConexion);
        }

        public string EstableceConexionStringTemp(string cadenaConexion)
        {
            return cn.EstableceConexionStringTemp(cadenaConexion);
        }

    }
}
