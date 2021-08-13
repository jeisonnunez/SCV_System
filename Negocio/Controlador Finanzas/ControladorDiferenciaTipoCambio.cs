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
    public class ControladorDiferenciaTipoCambio: ControladorReportes
    {
        ModeloDiferenciaTipoCambio cn = new ModeloDiferenciaTipoCambio();

        public Tuple<DataTable, string> ExecuteExchangeRateDifference(string cuentaGananciaCliente, string cuentaGananciaProveedores, string cuentaGananciaCuenta, string cuentaPerdidaCliente, string cuentaPerdidaProveedores, string cuentaPerdidaCuenta, DataTable dtAccount, string txtDesdeSN, string txtHastaSN, string currency, DateTime? fechaEjecucion, DateTime? dFechaVencimiento = null, DateTime? hFechaVencimiento = null)
        {
            DataTable newDt = new DataTable();

            DataTable dtClone;

            var result = cn.ExecuteExchangeRateDifference(cuentaGananciaCliente, cuentaGananciaProveedores, cuentaGananciaCuenta, cuentaPerdidaCliente, cuentaPerdidaProveedores, cuentaPerdidaCuenta, dtAccount, txtDesdeSN, txtHastaSN, currency, fechaEjecucion, dFechaVencimiento, hFechaVencimiento);

            newDt = result.Item1.Copy();

            dtClone = AddColumnAccount(newDt);

            return Tuple.Create(dtClone, result.Item2);
        }
    }
}
