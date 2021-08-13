using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Datos.ModeloInformesFiscales;

namespace Negocio.Controlador_Informes
{
    public class ControladorGenerarRetencionesIVA:Negocios
    {
        ModeloGenerarRetencionesIVA cn = new ModeloGenerarRetencionesIVA();      

        public List<Entidades.Quincena> GetQuincena()
        {
            List<Entidades.Quincena> listaQuincena = new List<Entidades.Quincena>();

            string Code;

            string Name;

            Code = "01";

            Name = "Primera";

            listaQuincena.Add(new Entidades.Quincena(Code, Name));

            Code = "02";

            Name = "Segunda";

            listaQuincena.Add(new Entidades.Quincena(Code, Name));

            return listaQuincena;
        }

        public List<Entidades.Tipo> GetTipo()
        {
            List<Entidades.Tipo> listaTipo = new List<Entidades.Tipo>();

            string Code;

            string Name;

            Code = "01";

            Name = "Quincenal";

            listaTipo.Add(new Entidades.Tipo(Code, Name));

            Code = "02";

            Name = "Operacion";

            listaTipo.Add(new Entidades.Tipo(Code, Name));

            Code = "03";

            Name = "Mensual";

            listaTipo.Add(new Entidades.Tipo(Code, Name));

            return listaTipo;
        }

        public List<Entidades.Meses> GetMonths()
        {
            List<Entidades.Meses> listaMeses = new List<Entidades.Meses>();

            string Code;

            string Name;

            Code = "01";

            Name = "Enero";

            listaMeses.Add(new Entidades.Meses(Code, Name));

            Code = "02";

            Name = "Febrero";

            listaMeses.Add(new Entidades.Meses(Code, Name));

            Code = "03";

            Name = "Marzo";

            listaMeses.Add(new Entidades.Meses(Code, Name));

            Code = "04";

            Name = "Abril";

            listaMeses.Add(new Entidades.Meses(Code, Name));

            Code = "05";

            Name = "Mayo";

            listaMeses.Add(new Entidades.Meses(Code, Name));

            Code = "06";

            Name = "Junio";

            listaMeses.Add(new Entidades.Meses(Code, Name));

            Code = "07";

            Name = "Julio";

            listaMeses.Add(new Entidades.Meses(Code, Name));

            Code = "08";

            Name = "Agosto";

            listaMeses.Add(new Entidades.Meses(Code, Name));

            Code = "09";

            Name = "Septiembre";

            listaMeses.Add(new Entidades.Meses(Code, Name));

            Code = "10";

            Name = "Octubre";

            listaMeses.Add(new Entidades.Meses(Code, Name));

            Code = "11";

            Name = "Noviembre";

            listaMeses.Add(new Entidades.Meses(Code, Name));

            Code = "12";

            Name = "Diciembre";

            listaMeses.Add(new Entidades.Meses(Code, Name));

            return listaMeses;
        }

       

        public object ExecuteGenerarRetencionesIVAQuincenal(string v1, string v2)
        {
            throw new NotImplementedException();
        }

        public Tuple<int,string,string> ExecuteGenerarRetencionesIVA(string txtProveedor, string txtProveedor1, string year, string month, string quincena, string tipo)
        {
            return cn.ExecuteGenerarRetencionesIVA(txtProveedor, txtProveedor1, year, month, quincena,tipo);
        }
    }
}
