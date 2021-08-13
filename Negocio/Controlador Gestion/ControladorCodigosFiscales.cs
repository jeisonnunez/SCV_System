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
    public class ControladorCodigosFiscales:ControladorDocumento
    {
        ModeloCodigosFiscales cn = new ModeloCodigosFiscales();
        public Tuple <List<Alicuota>, string> ConsultaAlicuota()
        {                                      
            var result= cn.ConsultaAlicuota();
           
            return Tuple.Create(GetAlicuota(result.Item1), result.Item2);
        }

        public List<Alicuota> GetAlicuota(DataTable dt)
        {
            List<Alicuota> listaAlicuota = new List<Alicuota>();

            string Code;

            string Name;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Code = dt.Rows[i]["Code"].ToString();
                Name = dt.Rows[i]["Name"].ToString();

                listaAlicuota.Add(new Alicuota(Code, Name));
            }

            return listaAlicuota;
        }

        public Tuple <List<CodigosFiscales>,string> ConsultaCodigosFiscales(List<CodigosFiscales> listaCodigosFiscales)
        {
            return cn.ConsultaCodigosFiscales(listaCodigosFiscales);
        }

        public char EstadoCompras(bool? estado)
        {
            char caracter = 'N';

            if (estado == true)
            {
                caracter = (char)EstadosCompras.Activo;

            }
            else

            if (estado == false)
            {
                caracter = (char)EstadosCompras.Inactivo;

            }


            return caracter;
        }

        public char EstadoVentas(bool? estado)
        {
            char caracter = 'N';

            if (estado == true)
            {
                caracter = (char)EstadosVentas.Activo;

            }
            else

            if (estado == false)
            {
                caracter = (char)EstadosVentas.Inactivo;

            }


            return caracter;
        }

        public char EstadoLock(bool? estado)
        {
            char caracter = 'N';

            if (estado == true)
            {
                caracter = (char)EstadosVentas.Activo;

            }
            else

            if (estado == false)
            {
                caracter = (char)EstadosVentas.Inactivo;

            }


            return caracter;
        }

        public bool? EstadoComprasInverso(char caracter)
        {
            bool? estado=null;

            if (caracter == (char)EstadosCompras.Activo)
            {
                estado=true;

            }
            else

            if (caracter == (char)EstadosCompras.Inactivo)
            {
                estado = false;
            }


            return estado;
        }

        public Tuple<List<CodigosFiscales>, string> FindFirst()
        {
            return cn.FindFirst();
        }

        public Tuple <List<CodigosFiscales>,string> FindLast()
        {
            return cn.FindLast();
        }

        public Tuple<List<CodigosFiscales>, string> FindNext(string codigo)
        {
            return cn.FindNext(codigo);
        }

        public Tuple<List<CodigosFiscales>, string> FindPrevious(string codigo)
        {
            return cn.FindPrevious(codigo);
        }

        public bool? EstadoVentasInverso(char caracter)
        {
            bool? estado = null;

            if (caracter == (char)EstadosVentas.Activo)
            {
                estado = true;

            }
            else

            if (caracter == (char)EstadosVentas.Inactivo)
            {
                estado = false;
            }


            return estado;
        }

      

        public bool? EstadoLockInverso(char caracter)
        {
            bool? estado = null;

            if (caracter == (char)EstadosLock.Bloqueado)
            {
                estado = true;

            }
            else

            if (caracter == (char)EstadosLock.Desbloqueado)
            {
                estado = false;
            }


            return estado;
        }

        enum EstadosCompras { Activo = 'Y', Inactivo = 'N' };

        enum EstadosVentas { Activo = 'Y', Inactivo = 'N' };

        enum EstadosLock { Desbloqueado = 'N', Bloqueado = 'Y' };

        public Tuple<int,string> ActualizaCodigosFiscales(List<CodigosFiscales> listaCodigosFiscales)
        {
            return cn.ActualizaCodigoFiscal(listaCodigosFiscales);
        }

        public Tuple<int,string> InsertaCodigosFiscales(List<CodigosFiscales> listaCodigosFiscales)
        {
            return cn.InsertaCodigoFiscal(listaCodigosFiscales);
        }

        public Tuple<int, string> DeleteCodigoFiscal(string codigoFiscal)
        {
            return cn.DeleteCodigoFiscal(codigoFiscal);
        }

        public Tuple<DataTable,string> FindCodigosFiscalesLines(string code)
        {
            DataTable dtClone;

            var result = cn.FindCodigosFiscalesLines(code);

            dtClone = ChangeTypeColumn(result.Item1);

            return Tuple.Create(dtClone, result.Item2);            
        }

        public Tuple<int,string> UpdateCodigosFiscalesLines(List<CodigosFiscalesLine> listaCodigosFiscalesLines)
        {
            return cn.UpdateCodigosFiscalesLines(listaCodigosFiscalesLines);
        }

        public Tuple<int,string> InsertCodigosFiscalesLines(List<CodigosFiscalesLine> listaCodigosFiscalesLines)
        {
            return cn.InsertCodigosFiscalesLines(listaCodigosFiscalesLines);
        }

        public Tuple<List<ClasesImpuestos>, string> ConsultaClasesImpuestos()
        {
            return cn.ConsultaClasesImpuestos();
        }
    }
}
