using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio
{
    public class ControladorClasesImpuestos:ControladorDocumento
    {
        ModeloClasesImpuesto cn = new ModeloClasesImpuesto();
        public Tuple <DataTable,string> ConsultaClasesImpuestos()
        {
            DataTable newDt = new DataTable();

            var result = cn.ConsultaClasesImpuestos();

            newDt = result.Item1.Copy();

            newDt = ChangeTypeColumnString(newDt);
           
            return Tuple.Create(newDt, result.Item2);
        


        }

        public Tuple<int, string> EliminaClasesImpuesto(string claseImpuesto)
        {
            return cn.EliminaClasesImpuesto(claseImpuesto);
        }

        public Tuple<int,string> InsertaClasesImpuestos(List<ClasesImpuestos> listaClasesImpuestos)
        {
            return cn.InsertaClasesImpuestos(listaClasesImpuestos);

        }
               
    }
}
