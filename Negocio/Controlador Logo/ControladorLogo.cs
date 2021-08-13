using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio
{
    public class ControladorLogo:Negocios
    {
        ModeloLogo cn = new ModeloLogo();
        public Tuple<int, string> InsertLogo(string strName, byte[] imgByteArr)
        {
            return cn.InsertLogo(strName, imgByteArr);
        }

        public Tuple<DataTable, string> BindImage()
        {
            return cn.BindImage();
        }
    }
}
