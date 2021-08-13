using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Datos.ModeloInformesFiscales;

namespace Negocio.Controlador_Informes_Fiscales
{
    public class ControladorAnularComprobanteIVA:Negocios
    {
        ModeloAnularComprobantesIVA cn = new ModeloAnularComprobantesIVA();

        public Tuple<int, string,string> ExecuteAnularComprobantesIVA(string desde, string hasta)
        {
            return cn.ExecuteAnularComprobantesIVA(desde, hasta);
        }
    }
}
