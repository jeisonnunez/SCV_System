using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocio
{
    public class ControladorTablaRetencionImpuesto : Negocios
    {
        ModeloTablaRetencionImpuesto cn = new ModeloTablaRetencionImpuesto();
        public Tuple<List<RetencionImpuesto>,string> FindHoldingTax()
        {
            return cn.FindHoldingTax();
        }
    }
}
