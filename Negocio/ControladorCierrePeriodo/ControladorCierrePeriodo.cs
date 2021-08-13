using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Datos.ModeloCierrePeriodo;

namespace Negocio.ControladorCierrePeriodo
{
    public class ControladorCierrePeriodo:ControladorReportes
    {
        ModeloCierrePeriodo cn = new ModeloCierrePeriodo();
        public Tuple<DataTable, string> ExecuteClosePeriod(DateTime starDate, DateTime endDate, string cuentaArrastre, string cuentaCierre, DataTable dtAccount = null, string desdeSN = null, string hastaSN = null)
        {
            DataTable newDt = new DataTable();

            DataTable dtClone;

            var result = cn.ExecuteClosePeriod(starDate, endDate, cuentaArrastre, cuentaCierre, dtAccount, desdeSN, hastaSN);

            newDt = result.Item1.Copy();

            dtClone = AddColumnAccount(newDt);

            return Tuple.Create(dtClone, result.Item2);
        }

    }
}
