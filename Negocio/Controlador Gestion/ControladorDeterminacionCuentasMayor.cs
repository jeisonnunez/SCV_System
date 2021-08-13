using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Datos.Modelo_Gestion;
using Entidades;

namespace Negocio.Controlador_Gestion
{
    public class ControladorDeterminacionCuentasMayor: Negocios
    {
        ModeloDeterminacionCuentasMayor cn = new ModeloDeterminacionCuentasMayor();

        public Tuple <List<Entidades.DeterminacionCuentasMayor>,string> FindDeterminacionCuentasMayor()
        {
            return cn.FindDeterminacionCuentasMayor();
        }

        public Tuple<int,string> UpdateDeterminacionCuentasMayor(List<DeterminacionCuentasMayor> listaDeterminacion)
        {
            return cn.UpdateDeterminacionCuentasMayor(listaDeterminacion);
        }
    }
}
