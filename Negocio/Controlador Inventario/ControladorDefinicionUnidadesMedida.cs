using Datos.Modelo_Inventario;
using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Negocio.Controlador_Inventario
{
    public class ControladorDefinicionUnidadesMedida : Negocios
    {
        ModeloDefinicionUnidadesMedida cn = new ModeloDefinicionUnidadesMedida();
        public Tuple<DataTable, string> ConsultaDefinicionUnidadesMedida(int upgEntry)
        {
            DataTable dt = new DataTable();

            DataTable newDt = new DataTable();

            var result = cn.ConsultaDefinicionUnidadesMedida(upgEntry);

            dt = result.Item1.Copy();

            dt.AcceptChanges();

            newDt = AddColumnOld(dt, "OldUgpEntry", "UgpEntry");

            return Tuple.Create(newDt, result.Item2);
        }

        public Tuple<List<UnidadesMedida>, string> ConsultaDefinicionUnidadesMedidaSpecific(int upgEntry)
        { 
                  
            return cn.ConsultaDefinicionUnidadesMedidaSpecific(upgEntry);
        }

        public Tuple<int,string> EliminaDefinicionGrupoUnidadMedida(int ugpEntry, int lineNum)
        {
            return cn.EliminaDefinicionGrupoUnidadMedida(ugpEntry, lineNum);
        }

        public Tuple<int,string> InsertaUnidadesMedidaDefinicionDetalle(List<GrupoUnidadesMedidaDetalle> listaUnidadesMedida)
        {
            return cn.InsertaUnidadesMedidaDefinicionDetalle(listaUnidadesMedida);
        }

        public Tuple<List<UnidadesMedida>,string> ConsultaUnidadesBaseMedidas(int baseUom)
        {
            return cn.ConsultaUnidadesBaseMedidas(baseUom);
        }

        public Tuple<List<UnidadesMedida>, string> ConsultaUnidadesBaseMedidas()
        {
            return cn.ConsultaUnidadesBaseMedidas();
        }

        public Tuple<DataTable,string> ConsultaDefinicionUnidadesMedidaFirstLine(int ugpEntry)
        {
            return cn.ConsultaDefinicionUnidadesMedidaFirstLine(ugpEntry);
        }
    }
}
