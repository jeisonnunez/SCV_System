using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ControladorEmpresa: ControladorInicioSesion
    {
        ModeloEmpresa cn = new ModeloEmpresa();

        public Tuple<int, string> SubPeriodosContables(List<SubPeriodo> listaSubPeriodos)
        {
            return cn.SubPeriodosContables(listaSubPeriodos);
        }

        public Tuple<int, string> EstablecerPeriodoContable(List<PeriodoContable> listaPeriodoContable)
        {
            return cn.EstablecerPeriodoContable(listaPeriodoContable);
        }

        public Tuple<int, string> CrearEmpresa(string empresa)
        {
            return cn.CreateSociedad(empresa);
        }

        public bool VerifiedDatabase(string database)
        {
            return cn.VerifiedDatabase(database);
        }

    }
}
