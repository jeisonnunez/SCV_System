using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ControladorPeriodoContable: Negocios
    {
        ModeloPeriodoContable cn = new ModeloPeriodoContable();

        public Tuple<int, string> ActualizaPeriodo(List<PeriodosContables> listaSubPeriodos)
        {
            return cn.ActualizaPeriodo(listaSubPeriodos);
        }

        public Tuple<DateTime?, string> ConsultaPeriodoActual(string category)
        {
            return cn.ConsultaPeriodoActual(category);
        }

        public char EstatusPeriodo(string estatus)
        {
            char caracter = 'N';

            if (estatus == (EstadosPeriodos.Desbloqueado).ToString())
            {
                caracter = (char)EstadosPeriodos.Desbloqueado;

            }
            else

            if (estatus == (EstadosPeriodos.Bloqueado).ToString())
            {
                caracter = (char)EstadosPeriodos.Bloqueado;

            }

            else if (estatus == "Cierre Periodo")
            {
                caracter = (char)EstadosPeriodos.CierrePeriodo;

            }

            else if (estatus == "Desbloqueado Exep Ventas")
            {
                caracter = (char)EstadosPeriodos.DesbloqueadoExecVentas;

            }

            return caracter;
        }

        public string EstatusPeriodo(char caracter)
        {
            string estatus = "";

            if (caracter == (char)EstadosPeriodos.Desbloqueado)
            {
                estatus = EstadosPeriodos.Desbloqueado.ToString();

            }
            else if (caracter == (char)EstadosPeriodos.Bloqueado)
            {
                estatus = EstadosPeriodos.Bloqueado.ToString();

            }

            else if (caracter == (char)EstadosPeriodos.CierrePeriodo)
            {
                estatus = "Cierre Periodo";

            }

            else if (caracter == (char)EstadosPeriodos.DesbloqueadoExecVentas)
            {
                estatus = "Desbloqueado Excp Ventas";
            }

            return estatus;
        }

        enum EstadosPeriodos { Desbloqueado = 'N', CierrePeriodo = 'C', Bloqueado = 'Y', DesbloqueadoExecVentas = 'S' };
    }
}
