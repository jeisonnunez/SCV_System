using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocio
{
    public class ControladorUsuario : ControladorRegistro
    {
        ModeloUsuario cn = new ModeloUsuario();
        public Tuple<List<Usuarios>,string> ConsultaUsuarios(List<Usuarios> listaUsuarios)
        {
            return cn.ConsultaUsuarios(listaUsuarios);
        }
        public char EstadoBloqueado(bool? estado)
        {
            char caracter = 'N';

            if (estado == true)
            {
                caracter = (char)EstadosLock.Bloqueado;

            }
            else

            if (estado == false)
            {
                caracter = (char)EstadosLock.Desbloqueado;

            }


            return caracter;
        }

        public bool? EstadoBloqueado(char caracter)
        {
            bool? estado = null;

            if (caracter == (char)EstadosLock.Bloqueado)
            {
                estado = true;

            }
            else

            if (caracter == (char)EstadosLock.Desbloqueado)
            {
                estado = false;
            }


            return estado;
        }

        enum EstadosLock { Desbloqueado = 'N', Bloqueado = 'Y' };

        public Tuple<List<Usuarios>,string> FindLast()
        {
            return cn.FindLast();
        }

        public Tuple<List<Usuarios>, string> FindNext(int userid)
        {
            return cn.FindNext(userid);
        }

        public Tuple<List<Usuarios>, string> FindPrevious(int userid)
        {
            return cn.FindPrevious(userid);
        }

        public Tuple<List<Usuarios>, string> FindFirst()
        {
            return cn.FindFirst();
        }
       
        public Tuple<int,string> UpdateUser(List<Usuarios> listaUsuarios)
        {
            return cn.UpdateUser(listaUsuarios);
        }

        public Tuple<int, string> UpdatePassword(string password, int userID)
        {
            return cn.UpdatePassword(password, userID);
        }

        public Tuple<int, string> DeleteUser(string userCode)
        {
            return cn.DeleteUser(userCode);
        }
    }
}
