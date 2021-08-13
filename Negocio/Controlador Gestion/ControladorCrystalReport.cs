using Datos.Modelo_Base;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Controlador_Gestion
{
    public class ControladorCrystalReport:Negocios
    {
        ModeloCrystalReport cn = new ModeloCrystalReport();
        public Tuple<List<CrystalReport>, string> GetCrystalReport()
        {
            return cn.GetCrystalReport();
        }
    }
}
