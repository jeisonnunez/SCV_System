using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Datos.Modelo_Finanzas;

namespace Negocio.Controlador_Finanzas
{
    public class ControladorDiferenciaConversion: ControladorReportes
    {
        ModeloDiferenciaConversion cn = new ModeloDiferenciaConversion();

        public Tuple<DataTable, string> ExecuteExchangeConversion(DataTable dtAccount, string txtDesdeSN, string txtHastaSN, DateTime? fechaEjecucion)
        {
            DataTable newDt = new DataTable();

            DataTable dtClone;

            var result = cn.ExecuteExchangeConversion(dtAccount, txtDesdeSN, txtHastaSN, fechaEjecucion);

            newDt = result.Item1.Copy();

            dtClone = AddColumnAccount(newDt);

            return Tuple.Create(dtClone, result.Item2);
        }
    }
}
