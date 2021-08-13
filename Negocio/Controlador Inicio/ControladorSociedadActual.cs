using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ControladorSociedadActual:Negocios
    {
        ModeloSociedadActual cn = new ModeloSociedadActual();
        public Tuple<DataTable,string> ConsultaSociedadActual()
        {
            var result= cn.ConsultaSociedadActual();

            DataTable dt = result.Item1;

            dt = ChangeTypeColumnDetallesSociedad(dt);

            return Tuple.Create(dt,result.Item2);
        }

        public DataTable ChangeTypeColumnDetallesSociedad(DataTable dataTable)
        {
            DataTable dtCloned = dataTable.Clone();

            int i = 0;

            foreach (DataColumn column in dataTable.Columns)
            {
                if (dtCloned.Columns[i].ColumnName.ToString() == "UpdateDate")
                {
                    dtCloned.Columns[i].DataType = typeof(DateTime);

                }
                else if (dtCloned.Columns[i].ColumnName.ToString() == "UserSign")
                {
                    dtCloned.Columns[i].DataType = typeof(int);
                }
                else
                {
                    dtCloned.Columns[i].DataType = typeof(string);
                }

                   
                


                i++;
            }

            foreach (DataRow row in dataTable.Rows)
            {
                dtCloned.ImportRow(row);
            }

            return dtCloned;
        }

        public new Tuple<List<Monedas>,string> ConsultaMonedas()
        {
            var result = cn.ConsultaMonedas();

            DataTable dt = result.Item1;

            return Tuple.Create(GetMonedas(dt),result.Item2); 
        }


        public Tuple<int,string> ActualizaSociedad(List<Sociedad> listaSociedad)
        {
            return cn.ActualizaSociedad(listaSociedad);
        }

       public List<Monedas> GetMonedas(DataTable dt)
        {
            List<Monedas> listaMonedas = new List<Monedas>();

            string currCode;

            string currName;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                currCode= dt.Rows[i]["CurrCode"].ToString();
                currName = dt.Rows[i]["CurrName"].ToString();
               
                listaMonedas.Add(new Monedas(currCode,currName));
            }

            return listaMonedas;
        } 
    }
}
