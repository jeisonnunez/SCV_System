using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ControladorLogin: ControladorInicioSesion
    {
        ModeloConsultaLogin cn = new ModeloConsultaLogin();
        public Tuple<object, string> conSQL(List<Usuarios> usuarios)
        {
            return cn.ConsultaLogin(usuarios);
        }

        public Tuple<int, string> ObtenerIdUser(string username)
        {
            return cn.ObtenerIdUser(username);
        }

        public Tuple<string, string> GetNameSociety()
        {
            return cn.GetNameSociety();
        }

        public string CommitTransaction()
        {
            return cn.CommitTransaction();
        }
    }
}
