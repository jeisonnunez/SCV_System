using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocio
{
    public class ControladorArticulos:Negocios
    {
        ModeloArticulos cn = new ModeloArticulos();

        public Tuple<List<Articulos>, string> FindNext(string codigo)
        {
            return cn.FinNext(codigo);
        }

        public Tuple <List<Articulos>, string> FindPrevious(string codigo)
        {
            return cn.FinPrevious(codigo);
        }

        public Tuple <List<Articulos>,string> FindFirst()
        {
            return cn.FindFirst();
        }

        public Tuple<List<Articulos>, string> FindLast()
        {
            return cn.FindLast();
        }

        public bool? EstadoCompra(char prchseItem)
        {
            bool? estado=null;

            if (prchseItem ==(char)Estados.Valido)
            {
                estado = true;
            }
            else if (prchseItem == (char)Estados.NoValido)
            {
                estado = false;
            }

            return estado;
        }

        public bool? EstadoVenta(char sellItem)
        {
            bool? estado = null;

            if (sellItem == (char)Estados.Valido)
            {
                estado = true;
            }
            else if (sellItem == (char)Estados.NoValido)
            {
                estado = false;
            }

            return estado;
        }

        public bool? EstadoInventario(char invnItem)
        {
            bool? estado = null;

            if (invnItem == (char)Estados.Valido)
            {
                estado = true;
            }
            else if (invnItem == (char)Estados.NoValido)
            {
                estado = false;
            }

            return estado;
        }

        public bool? EstadoImpuesto(char vatLiable)
        {
            bool? estado = null;

            if (vatLiable == (char)Estados.Valido)
            {
                estado = true;
            }
            else if (vatLiable == (char)Estados.NoValido)
            {
                estado = false;
            }

            return estado;
        }

        public char EstadoCompra(bool? estado)
        {
           char caracter = 'N';

            if (estado == true)
            {
                caracter = (char)Estados.Valido;
            }
            else if (estado == false)
            {
                caracter = (char)Estados.NoValido;
            }

            return caracter;
        }

        public char EstadoVenta(bool? estado)
        {
            char caracter = 'N';

            if (estado == true)
            {
                caracter = (char)Estados.Valido;
            }
            else if (estado == false)
            {
                caracter = (char)Estados.NoValido;
            }

            return caracter;
        }

        public char EstadoInventario(bool? estado)
        {
            char caracter = 'N';

            if (estado == true)
            {
                caracter = (char)Estados.Valido;
            }
            else if (estado == false)
            {
                caracter = (char)Estados.NoValido;
            }

            return caracter;
        }

        public char EstadoImpuesto(bool? estado)
        {
           char caracter = 'N';

            if (estado == true)
            {
                caracter = (char)Estados.Valido;
            }
            else if (estado == false)
            {
                caracter = (char)Estados.NoValido;
            }

            return caracter;
        }

        public Tuple <List<Articulos>,string> ConsultaItems(List<Articulos> listaArticulo)
        {
            return cn.ConsultaItems(listaArticulo);
        }

        public Tuple<int, string> InsertItems(List<Articulos> listaArticulo)
        {
            return cn.InsertItems(listaArticulo);
        }

        public Tuple<int, string> UpdateItems(List<Articulos> listaArticulo)
        {
            return cn.UpdateItems(listaArticulo);
        }

        enum Estados {Valido='Y', NoValido='N' };

        public Tuple<int,string> DeleteItem(string itemCode)
        {
            return cn.DeleteItem(itemCode);
        }

        public Tuple<List<Articulos>, string>  ConsultaItems()
        {
            return cn.ConsultaItems();
        }
    }
}
