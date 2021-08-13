using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocio
{
    public class ControladorSocioNegocio : ControladorDocumento
    {
        ModeloSocioNegocio cn = new ModeloSocioNegocio();
        public Tuple<List<SocioNegocio>,string> FindLast()
        {
            return cn.FindLast();
        }

        public Tuple<List<SocioNegocio>, string> FindNext(string codigo)
        {
            return cn.FindNext(codigo);
        }

        public Tuple<List<SocioNegocio>, string> FindPrevious(string codigo)
        {
            return cn.FindPrevious(codigo);
        }

        public Tuple<List<SocioNegocio>, string> FindFirst()
        {
            return cn.FindFirst();
        }

        public string CardType(char cardType)
        {
            string socioNegocio = "";

            if (cardType == (char)TipoSocioNegocio.Proveedor)
            {
                socioNegocio = TipoSocioNegocio.Proveedor.ToString();
            }
            else if (cardType ==(char)TipoSocioNegocio.Cliente)
            {
                socioNegocio = TipoSocioNegocio.Cliente.ToString();
            }

            return socioNegocio;
        }

        public char CardType(string socioNegocio)
        {
            char tipoSN='S';

            if (socioNegocio == TipoSocioNegocio.Proveedor.ToString())
            {
                tipoSN = (char)TipoSocioNegocio.Proveedor;
            }
            else if (socioNegocio == TipoSocioNegocio.Cliente.ToString())
            {
                tipoSN = (char)TipoSocioNegocio.Cliente;
            }

            return tipoSN;
        }

        public string YesNo(char opcion)
        {
            string seleccion = "";

            if (opcion == (char)YesNoEnum.SI)
            {
                seleccion = YesNoEnum.SI.ToString();
            }
            else if (opcion == (char)YesNoEnum.NO)
            {
                seleccion = YesNoEnum.NO.ToString();
            }

            return seleccion;
        }

        public char YesNo(string seleccion)
        {
            char opcion = 'S';

            if (seleccion == YesNoEnum.SI.ToString())
            {
                opcion= (char)YesNoEnum.SI;
            }
            else if (seleccion == YesNoEnum.NO.ToString())
            {
                opcion = (char)YesNoEnum.NO;
            }

            return opcion;
        }

        public Tuple<List<SocioNegocio>, string> ConsultaSocioNegocio(List<SocioNegocio> listaSocioNegocio)
        {
            return cn.ConsultaSocioNegocio(listaSocioNegocio);
        }

        enum TipoSocioNegocio {Proveedor='S', Cliente='C'};

        enum YesNoEnum {SI='Y', NO='N' };

        public Tuple <int,string> InsertBP(List<SocioNegocio> listaSocioNegocio)
        {
            return cn.InsertBP(listaSocioNegocio);
        }

        public Tuple<int, string> UpdateBP(List<SocioNegocio> listaSocioNegocio)
        {
            return cn.UpdateBP(listaSocioNegocio);
        }

       

        public Tuple<List<CodigosFiscales>,string> ConsultaCodigosFiscales()
        {
            return cn.ConsultaCodigosFiscales();
        }

        public Tuple<int,string> DeleteBusinessPartner(string cardCode)
        {
            return cn.DeleteBusinessPartner(cardCode);
        }
    }
}
