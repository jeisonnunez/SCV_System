using Datos.Modelo_Inventario;
using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Controlador_Inventario
{
    public class ControladorGrupoUnidadesMedida: Negocios
    {
        ModeloGrupoUnidadesMedida cn = new ModeloGrupoUnidadesMedida();

        public Tuple<DataTable, string> ConsultaGrupoUnidadesMedida()
        {
            DataTable dt = new DataTable();

            DataTable newDt = new DataTable();

            var result = cn.ConsultaGrupoUnidadesMedida();

            dt = result.Item1.Copy();

            dt.AcceptChanges();

            newDt = AddColumnOld(dt, "OldUgpCode", "UgpCode");           

            return Tuple.Create(newDt, result.Item2);

        }

        public Tuple<List<GrupoUnidadesMedidaCabecera>, string> ConsultaGrupoUnidadesMedidas()
        {
            return cn.ConsultaGrupoUnidadesMedidas();
        }

        public Tuple<int,string> EliminaGrupoUnidadMedida(string ugpCode)
        {
            return cn.EliminaGrupoUnidadMedida(ugpCode);
        }

        public Tuple<int,string> InsertaGrupoUnidadesMedidaCabecera(List<GrupoUnidadesMedidaCabecera> listaGrupoUnidadesMedida)
        {
            return cn.InsertaGrupoUnidadesMedidaCabecera(listaGrupoUnidadesMedida);
        }
    }
}
