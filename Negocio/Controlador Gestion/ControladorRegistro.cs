using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ControladorRegistro: ControladorLogo
    {
        ModeloRegistro cn = new ModeloRegistro();

        public Tuple <string,string> obtenerBaseDatos()
        {
            return cn.obtenerBaseDatos();
        }

        public Tuple<int, string> regUsuario(List<Usuarios> listaUsuarios)
        {
            return cn.InsertarUsuario(listaUsuarios);
        }

        public Tuple<int, string> DatosSociedad(List<Sociedad> listaSociedad)
        {
            return cn.DatosSociedad(listaSociedad);
        }

        public Tuple<int, string> InsertaCuentasPrimerNivel(List<Cuenta> listaCuentas)
        {
            return cn.InsertaCuentasPrimerNivel(listaCuentas);

        }

        public Tuple<int, string> InsertaMonedasBasicas(List<Monedas> listaMonedas)
        {
            return cn.InsertaMonedasBasicas(listaMonedas);
        }

        public Tuple<int, string> InsertaRetencionImpuesto(List<RetencionImpuesto> listaRetencion)
        {
            return cn.InsertaRetencionImpuesto(listaRetencion);
        }

        public Tuple<int, string> InsertaClasesImpuestos(List<ClasesImpuestos> listaClasesImpuestos)
        {
            return cn.InsertaClasesImpuestos(listaClasesImpuestos);
        }

        public Tuple<int, string> InsertaCodigosFiscales(List<CodigosFiscales> listaCodigosFiscales)
        {
            return cn.InsertaCodigosFiscales(listaCodigosFiscales);
        }

        public Tuple<int, string> InsertaMeses()
        {
            return cn.InsertaMeses();
        }

        public Tuple<int, string> InsertaAlicuotas()
        {
            return cn.InsertaAlicuotas();
        }

        public Tuple<int, string> InsertaDeterminacionCuentasMayor()
        {
            return cn.InsertaDeterminacionCuentasMayor();
        }
    }
}
